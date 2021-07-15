using HomeEnvironmentLifePlanner.Server.Data;
using HomeEnvironmentLifePlanner.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Server.Controllers
{
    [DisableRequestSizeLimit]

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TransactionController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("headerList")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transactions = await _context.TransactionHeaders
                .Include(ctr => ctr.Contractor).Where(ctr => ctr.TrH_CTRID == ctr.Contractor.CtR_Id)
                //.Include(pyt => pyt.PaymentType).Where(pyt => pyt.TrH_PYTID == pyt.PaymentType.PyT_Id)
                .Include(cur => cur.Currency).Where(cur => cur.TrH_CURID == cur.Currency.CuR_Id)
                .Include(bsp => bsp.BankStatmentPosition).Where(bsp => bsp.TrH_BSPID == bsp.BankStatmentPosition.BsP_Id)
                .ToListAsync();
                return Ok(transactions);
            }
            catch (Exception )
            {
                return null;
            }
        }
        [HttpGet("header/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await _context.TransactionHeaders
                .Include(ctr => ctr.Contractor).Where(ctr => ctr.TrH_CTRID == ctr.Contractor.CtR_Id)
                //.Include(pyt => pyt.PaymentType).Where(pyt => pyt.TrH_PYTID == pyt.PaymentType.PyT_Id)
                .Include(cur => cur.Currency).Where(cur => cur.TrH_CURID == cur.Currency.CuR_Id)
                .Include(bsp => bsp.BankStatmentPosition).Where(bsp => bsp.TrH_BSPID == bsp.BankStatmentPosition.BsP_Id)
                .FirstOrDefaultAsync(a => a.TrH_Id == id);
            return Ok(transaction);
        }
        [HttpPost("header")]
        public async Task<IActionResult> Post(TransactionHeader transaction)
        {
            transaction.TrH_BSPID = null;
            transaction.TrH_CreateDate = DateTime.Now;
            _context.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction.TrH_Id);
        }
        [HttpPost("ImportFromBSH")]
        public async Task<IActionResult> ImportFromBSH(BankStatementHeader bsh)
        {
            try
            {
                var bspList = _context.BankStatementPositions
                    .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                    .Where(x => x.BsP_BSHID == bsh.BsH_Id && !x.BsP_IsImportedToTransactions && x.Bsp_IsPreparedToImport).ToList() ;
                
                foreach (var bsp in bspList)
                {
                    TransactionHeader trh = new TransactionHeader()
                    {
                        TrH_CTRID = (int)bsp.BsP_RecommendedContractorId,
                        //  TrH_Amount = bss.BsS_Amount,
                        TrH_CreateDate = DateTime.Now,
                        TrH_CURID = bsp.BsP_CURID,
                        TrH_BSPID = bsp.BsP_Id,
                        TrH_Description = bsp.BsP_Description,
                        TrH_ExecutionDate = bsp.BsP_ExecutionDate
                        
                    };
                    _context.Add(trh);
                    await _context.SaveChangesAsync();
                    foreach (var bss in bsp.BankStatementSubPositions)
                    {
                        TransactionPosition trp = new TransactionPosition()
                        {
                        TrP_Amount=bss.BsS_Amount,
                        TrP_BSSID=bss.BsS_Id,
                        TrP_TRHID=trh.TrH_Id,
                        TrP_CATID=(int)bss.BsS_CATID
                        };
                        _context.Add(trp);
                        await _context.SaveChangesAsync();


                    }

                    bsp.BsP_IsImportedToTransactions = true;
                    bsp.Bsp_IsPreparedToImport = false;
                    _context.Entry(bsp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }

                return Ok();
            }
            catch (Exception ex)
            {
              
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("header")]
        public async Task<IActionResult> Put(TransactionHeader trh)
        {
            try
            {
                trh.TrH_BSPID = null;
                trh.TrH_CreateDate = DateTime.Now;
                _context.Entry(trh).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception )
            {

            }
            return NoContent();
        }
        [HttpDelete("header/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trh = new TransactionHeader { TrH_Id = id };
            _context.Remove(trh);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("header/getChildren/{trhid}")]
        public async Task<IActionResult> TRHGetChildren(int trhId)
        {
            try
            {                 
                var transactionPositions = await _context.TransactionPositions
                      .Where(a => a.TrP_TRHID == trhId).ToListAsync();
                return Ok(transactionPositions);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
    }

}
