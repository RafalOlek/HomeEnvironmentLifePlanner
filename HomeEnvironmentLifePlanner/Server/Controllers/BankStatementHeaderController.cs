using ExcelDataReader;
using HomeEnvironmentLifePlanner.Server.Data;
using HomeEnvironmentLifePlanner.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Server.Controllers
{
    [DisableRequestSizeLimit]
    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementHeaderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BankStatementHeaderController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currencies = await _context.BankStatmentHeaders
                                        .Include(x => x.BankStatementPositions.Where(x => x.BsP_BSHID == x.BankStatmentHeader.BsH_Id))
                                       .ThenInclude(x => x.BankStatementSubPositions.Where(x => x.BsS_BSPID == x.BankStatementPosition.BsP_Id))

                                        .ToListAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bankStatmentHeader = await _context.BankStatmentHeaders
                .Include(x => x.BankStatementPositions.Where(x => x.BsP_BSHID == x.BankStatmentHeader.BsH_Id))
                .ThenInclude(x => x.BankStatementSubPositions.Where(x => x.BsS_BSPID == x.BankStatementPosition.BsP_Id))
                .FirstOrDefaultAsync(a => a.BsH_Id == id);
            return Ok(bankStatmentHeader);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> ImportBankStatement(IFormFile file)
        {
            try
            {
                BankStatementPosition bsp;
                if (file != null && file.Length > 0)
                {
                    var imagePath = @"\Upload";
                    var uploadPath = Environment.CurrentDirectory + imagePath;
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    var fullpath = Path.Combine(uploadPath, file.FileName);
                    using (FileStream fs = new FileStream(fullpath, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(fs);

                    }
                    using (var stream = System.IO.File.Open(fullpath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            bool firstRow = true;
                            do
                            {
                                BankStatementHeader bsh = new BankStatementHeader() { BsH_CreateDate = DateTime.Now, BsH_Name = file.FileName };
                                _context.Add(bsh);
                                await _context.SaveChangesAsync();


                                while (reader.Read())
                                {
                                    if (!firstRow)
                                    {
                                        decimal amount = (decimal)reader.GetDouble(3);
                                        bsp = new BankStatementPosition()
                                        {
                                            BsP_ExecutionDate = Convert.ToDateTime(reader.GetDateTime(1)),
                                            BsP_Amount = amount,
                                            BsP_ImportDate = DateTime.Now,
                                            BsP_IsImportedToTransactions = false,
                                            Bsp_IsPreparedToImport=false,
                                            BsP_TransactionType = reader.GetString(8),
                                            BsP_Description = reader.GetString(6),
                                            BsP_SenderReceiver = reader.GetString(5),
                                            BsP_BSHID = bsh.BsH_Id,
                                            BsP_CURID = _context.Currencies.Where(x => x.CuR_Name == reader.GetString(4)).FirstOrDefault().CuR_Id,
                                            BsP_RecommendedContractorId = ContractorSeeker(reader.GetString(6)),
                                        };
                                        _context.Add(bsp);
                                        await _context.SaveChangesAsync();

                                        _context.Add(new BankStatementSubPosition() { BsS_Amount = amount, BsS_BSPID = bsp.BsP_Id, BsS_CATID = 1 });
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                        firstRow = false;
                                }
                                bsh.BsH_DateFrom = _context.BankStatementPositions.Where(x => x.BsP_BSHID == bsh.BsH_Id).Min(x => x.BsP_ExecutionDate);
                                bsh.BsH_DateTo = _context.BankStatementPositions.Where(x => x.BsP_BSHID == bsh.BsH_Id).Max(x => x.BsP_ExecutionDate);
                                _context.Entry(bsh).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            } while (reader.NextResult());
                        }
                    }
                }
                return Ok(200);
            }
            catch (Exception ex)
            {
                return NoContent();
            }

        }
        [HttpPut]
        public async Task<IActionResult> Put(BankStatementHeader bankStatmentHeader)
        {
            var bsp = _context.BankStatementPositions.Where(x => x.BsP_BSHID == bankStatmentHeader.BsH_Id && x.BsP_RecommendedContractorId == null).ToList();
            foreach (var item in bsp)
            {
                int? tmp = ContractorSeeker(item.BsP_Description);
                if (tmp != null)
                {
                    item.BsP_RecommendedContractorId = tmp;
                    _context.Entry(item).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bankStatmentHeader = new BankStatementHeader { BsH_Id = id };
            _context.Remove(bankStatmentHeader);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public int? ContractorSeeker(string BsP_Description)
        {
            if (_context.Contractors.Where(x => BsP_Description.Contains(x.CtR_ReferenceNumber)).Any())
                return _context.Contractors.Where(x => BsP_Description.Contains(x.CtR_ReferenceNumber)).Select(x => x.CtR_Id).FirstOrDefault();
            else
                return null;

        }

    }

}
