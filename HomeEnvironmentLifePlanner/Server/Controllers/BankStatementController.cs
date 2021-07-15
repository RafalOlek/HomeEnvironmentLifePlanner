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
    public class BankStatementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BankStatementController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("headerList")]
        public async Task<IActionResult> BSHGetList()
        {
            try
            {
                var currencies = await _context.BankStatmentHeaders
                                        .Include(x => x.BankStatementPositions.Where(x => x.BsP_BSHID == x.BankStatmentHeader.BsH_Id))
                                       .ThenInclude(x => x.BankStatementSubPositions.Where(x => x.BsS_BSPID == x.BankStatementPosition.BsP_Id))

                                        .ToListAsync();
                return Ok(currencies);
            }
            catch (Exception )
            {

            }
            return null;
        }
        [HttpGet("header/{id}")]
        public async Task<IActionResult> BSHGetSingle(int id)
        {
            var bankStatmentHeader = await _context.BankStatmentHeaders
                .Include(x => x.BankStatementPositions.Where(x => x.BsP_BSHID == x.BankStatmentHeader.BsH_Id))
                .ThenInclude(x => x.BankStatementSubPositions.Where(x => x.BsS_BSPID == x.BankStatementPosition.BsP_Id))
                .FirstOrDefaultAsync(a => a.BsH_Id == id);
            return Ok(bankStatmentHeader);
        }
        [HttpGet("header/getChildren/{bshid}")]
        public async Task<IActionResult> BSHGetChildren(int bshId)
        {
            try
            {
                var bankStatmentPositions = await _context.BankStatementPositions
                        .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                        .Include(y=>y.RecommendedContractor)
                      .Where(a => a.BsP_BSHID == bshId &&(a.BsP_RecommendedContractorId == null || a.RecommendedContractor.CtR_Id == a.BsP_RecommendedContractorId)).ToListAsync();
                return Ok(bankStatmentPositions);
            }
            catch (Exception )
            {
                return NoContent();
            }
        }
        [HttpPost("header/import")]
        public async Task<IActionResult> BSHImportBankStatement(IFormFile file)
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
                                            BsP_ExecutionDate = Convert.ToDateTime((reader.GetValue(1) == null || reader.GetValue(1).ToString() == "") ? reader.GetDateTime(0) : reader.GetDateTime(1)),
                                            BsP_Amount = amount,
                                            BsP_ImportDate = DateTime.Now,
                                            BsP_IsImportedToTransactions = false,
                                            Bsp_IsPreparedToImport = false,
                                            BsP_TransactionType = reader.GetString(8),
                                            BsP_Description = reader.GetString(6),
                                            BsP_SenderReceiver = (reader.GetValue(5) == null) ? "" : reader.GetString(5),
                                            BsP_BSHID = bsh.BsH_Id,
                                            BsP_CURID = _context.Currencies.Where(x => x.CuR_Name == reader.GetString(4)).FirstOrDefault().CuR_Id,
                                            BsP_RecommendedContractorId = ContractorSeeker(reader.GetString(6), (reader.GetValue(5) == null) ? "" : reader.GetString(5)),
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
            catch (Exception )
            {
                return NoContent();
            }

        }
        [HttpPut("header/refillCTR")]
        public async Task<IActionResult> BSHPutCTR(BankStatementHeader bankStatmentHeader)
        {
            var bsp = _context.BankStatementPositions.Where(x => x.BsP_BSHID == bankStatmentHeader.BsH_Id && x.BsP_RecommendedContractorId == null).ToList();
            foreach (var item in bsp)
            {
                int? tmp = ContractorSeeker(item.BsP_Description, item.BsP_SenderReceiver);
                if (tmp != null)
                {
                    item.BsP_RecommendedContractorId = tmp;
                    _context.Entry(item).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("header/refillCAT")]
        public async Task<IActionResult> BSHPutCAT(BankStatementHeader bankStatmentHeader)
        {
            var bss = _context.BankStatementPositions
                    .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                    .Where(x => x.BsP_BSHID == bankStatmentHeader.BsH_Id && x.BankStatementSubPositions.Where(y => y.BsS_CATID == 1).Any()).ToList();
            foreach (var item in bss)
            {
                int? tmp = CategorySeeker(item.BsP_Description, item.BsP_SenderReceiver);
                if (tmp != null)
                {
                    item.BankStatementSubPositions.FirstOrDefault().BsS_CATID = tmp;
                    _context.Entry(item).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("header/{id}")]
        public async Task<IActionResult> BSHDelete(int id)
        {
            var bankStatmentHeader = new BankStatementHeader { BsH_Id = id };
            _context.Remove(bankStatmentHeader);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        public int? ContractorSeeker(string BsP_Description, string BsP_SenderReceiver)
        {
            try
            {
                List<Tuple<int, string>> refeList = new List<Tuple<int, string>>();
                foreach (var item in _context.Contractors)
                {
                    foreach (var ite in item.CtR_ReferenceNumber.Split(";"))
                        refeList.Add(new Tuple<int, string>(item.CtR_Id, ite));
                }


                if (refeList.Where(x => BsP_Description.ToUpper().Contains(x.Item2.ToUpper())).Any() && BsP_Description != "")
                    return refeList.Where(x => BsP_Description.ToUpper().Contains(x.Item2.ToUpper())).Select(x => x.Item1).FirstOrDefault();
                else if (refeList.Where(x => BsP_SenderReceiver.ToUpper().Contains(x.Item2.ToUpper())).Any() && BsP_SenderReceiver != "")
                    return refeList.Where(x => BsP_SenderReceiver.ToUpper().Contains(x.Item2.ToUpper())).Select(x => x.Item1).FirstOrDefault();
                else
                    return null;
            }
            catch (Exception )
            {
                return null;
            }

        }
        public int? CategorySeeker(string BsP_Description, string BsP_SenderReceiver)
        {
            List<Tuple<int, string>> refeList = new List<Tuple<int, string>>();
            foreach (var item in _context.Categories)
            {
                foreach (var ite in item.CaT_ReferenceNumber.Split(";"))
                    refeList.Add(new Tuple<int, string>(item.CaT_Id, ite));
            }

            if (refeList.Where(x => BsP_Description.ToUpper().Contains(x.Item2.ToUpper())).Any() && BsP_Description != "")
                return refeList.Where(x => BsP_Description.ToUpper().Contains(x.Item2.ToUpper())).Select(x => x.Item1).FirstOrDefault();
            else if (refeList.Where(x => BsP_SenderReceiver.ToUpper().Contains(x.Item2.ToUpper())).Any() && BsP_SenderReceiver != "")
                return refeList.Where(x => BsP_SenderReceiver.ToUpper().Contains(x.Item2.ToUpper())).Select(x => x.Item1).FirstOrDefault();
            else
                return null;

        }
        [HttpGet("positionList")]
        public async Task<IActionResult> BSPGetList()
        {
            var bankStatmentPositions = await _context.BankStatementPositions
                 .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                 .ToListAsync();
            return Ok(bankStatmentPositions);
        }

        [HttpGet("position/{id}")]
        public async Task<IActionResult> BSPGetSingle(int id)
        {
            var bankStatmentPosition = await _context.BankStatementPositions
                 .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                 .FirstOrDefaultAsync(a => a.BsP_Id == id);
            return Ok(bankStatmentPosition);
        }
        [HttpGet("position/getChildren/{bspid}")]
        public async Task<IActionResult> BSPGetChildren(int bspId)
        {
            var bankStatmentSubPositions = await _context.BankStatementSubPositions.Where(a => a.BsS_BSPID == bspId).ToListAsync();
            return Ok(bankStatmentSubPositions);
        }
        [HttpPut("position")]
        public async Task<IActionResult> BSPPut(BankStatementPosition bsp)
        {
            _context.Entry(bsp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(bsp);
        }

        [HttpGet("subpositionList")]
        public async Task<IActionResult> BSSGetList()
        {
            var bankStatmentSubPositions = await _context.BankStatementSubPositions.ToListAsync();
            return Ok(bankStatmentSubPositions);
        }
        [HttpGet("subposition/{id}")]
        public async Task<IActionResult> BSSGetSingle(int id)
        {
            var bankStatmentSubPosition = await _context.BankStatementSubPositions.FirstOrDefaultAsync(a => a.BsS_Id == id);
            return Ok(bankStatmentSubPosition);
        }
        [HttpPost("subposition")]
        public async Task<IActionResult> BSSPost(BankStatementSubPosition bss)
        {
            _context.Add(bss);
            await _context.SaveChangesAsync();
            return Ok(bss);
        }
        [HttpPut("subposition")]
        public async Task<IActionResult> BSSPut(BankStatementSubPosition bss)
        {
            _context.Entry(bss).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("subposition/{id}")]
        public async Task<IActionResult> BSSDelete(int id)
        {
            var bss = new BankStatementSubPosition { BsS_Id = id };
            _context.Remove(bss);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
