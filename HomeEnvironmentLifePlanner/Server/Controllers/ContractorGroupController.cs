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
    public class ContractorGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ContractorGroupController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currencies = await _context.ContractorGroups.ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contractorGroup = await _context.ContractorGroups.FirstOrDefaultAsync(a => a.CtG_Id == id);
            return Ok(contractorGroup);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ContractorGroup contractorGroup )
        {
            _context.Add(contractorGroup);
            await _context.SaveChangesAsync();
            return Ok(contractorGroup.CtG_Id);
        }
        [HttpPost("{parentId}")]
        public async Task<IActionResult> Post(ContractorGroup contractorGroup, int? parentId)
        {
            contractorGroup.CtG_ParentId = parentId;
            _context.Add(contractorGroup);
            await _context.SaveChangesAsync();
            return Ok(contractorGroup.CtG_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(ContractorGroup contractorGroup)
        {
            _context.Entry(contractorGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contractorGroup = new ContractorGroup { CtG_Id = id };
            if (_context.Contractors.Where(x => x.CtR_CTGID == id).Any())
            {
                return Content(@"<script language='javascript' type='text/javascript'>alert('Nie można usunąć grupy. XYZ!');</script>");
            }
            else
            {
                _context.Remove(contractorGroup);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }

}
