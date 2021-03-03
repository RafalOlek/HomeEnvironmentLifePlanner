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

    [Route("api/[controller]")]
    [ApiController]
    public class BankStatementSubPositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BankStatementSubPositionController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bankStatmentSubPositions = await _context.BankStatementSubPositions.ToListAsync();
            return Ok(bankStatmentSubPositions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bankStatmentSubPosition = await _context.BankStatementSubPositions.FirstOrDefaultAsync(a => a.BsS_Id == id);
            return Ok(bankStatmentSubPosition);
        }
        [HttpGet("parent/{bspid}")]
        public async Task<IActionResult> GetBSPChildren(int bspId)
        {
            var bankStatmentSubPositions = await _context.BankStatementSubPositions.Where(a => a.BsS_BSPID == bspId).ToListAsync();
            return Ok(bankStatmentSubPositions);
        }
        [HttpPost]
        public async Task<IActionResult> Post(BankStatementSubPosition bss)
        {
            _context.Add(bss);
            await _context.SaveChangesAsync();
            return Ok(bss);
        }
        [HttpPut("setCat/{categoryId}")]
        public async Task<IActionResult> Put(BankStatementSubPosition bss, int categoryId)
        {

            bss.BsS_CATID = categoryId;
            _context.Entry(bss).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(bss);
        }
        [HttpPut]
        public async Task<IActionResult> Put(BankStatementSubPosition bss)
        {
            _context.Entry(bss).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bss = new BankStatementSubPosition { BsS_Id = id };
            _context.Remove(bss);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


