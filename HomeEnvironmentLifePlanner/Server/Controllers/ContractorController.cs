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
    public class ContractorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ContractorController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currencies = await _context.Contractors.Include(x => x.ContractorGroup).Where(x => x.CtR_CTGID == x.ContractorGroup.CtG_Id).ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contractor = await _context.Contractors.Include(x=>x.ContractorGroup).Where(x=>x.CtR_CTGID==x.ContractorGroup.CtG_Id).FirstOrDefaultAsync(a => a.CtR_Id == id);
            return Ok(contractor);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Contractor contractor )
        {
            _context.Add(contractor);
            await _context.SaveChangesAsync();
            return Ok(contractor.CtR_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Contractor contractor)
        {
            _context.Entry(contractor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contractor = new Contractor { CtR_Id = id };
            _context.Remove(contractor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
