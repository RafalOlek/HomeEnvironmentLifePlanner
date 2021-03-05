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
    public class BankStatementPositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BankStatementPositionController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bankStatmentPositions = await _context.BankStatementPositions
                 .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                 .ToListAsync();
            return Ok(bankStatmentPositions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bankStatmentPosition = await _context.BankStatementPositions
                 .Include(x => x.BankStatementSubPositions.Where(x=>x.BankStatementPosition.BsP_Id==x.BsS_BSPID))
                 .FirstOrDefaultAsync(a => a.BsP_Id == id);
            return Ok(bankStatmentPosition);
        }
        [HttpGet("parent/{bshid}")]
        public async Task<IActionResult> GetBSHChildren(int bshId)
        {
            try
            {
                var bankStatmentPositions = await _context.BankStatementPositions
                        .Include(x => x.BankStatementSubPositions.Where(x => x.BankStatementPosition.BsP_Id == x.BsS_BSPID))
                      .Where(a => a.BsP_BSHID == bshId  ).ToListAsync();
                return Ok(bankStatmentPositions);
            }
            catch(Exception ex)
            {
                return NoContent();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(BankStatementPosition bsp )
        {
            _context.Entry(bsp).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(bsp);
        }
    }
}


