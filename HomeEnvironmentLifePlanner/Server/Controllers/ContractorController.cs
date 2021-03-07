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
        [HttpGet("groupList")]
        public async Task<IActionResult> CTGGetList()
        {
            var currencies = await _context.ContractorGroups.ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("group/{id}")]
        public async Task<IActionResult> CTGGetSingle(int id)
        {
            var contractorGroup = await _context.ContractorGroups.FirstOrDefaultAsync(a => a.CtG_Id == id);
            return Ok(contractorGroup);
        }
        [HttpGet("group/getAssignedContractors/{id}")]
        public async Task<IActionResult> CTGGetChildren(int id)
        {
            var ctrList = await _context.Contractors.Where(x=>x.CtR_CTGID==id).ToListAsync();
            return Ok(ctrList);
        }
        [HttpPost("group")]
        public async Task<IActionResult> CTGPost(ContractorGroup contractorGroup )
        {
            _context.Add(contractorGroup);
            await _context.SaveChangesAsync();
            return Ok(contractorGroup.CtG_Id);
        }
  
        [HttpPost("group/addChild/{parentId}")]
        public async Task<IActionResult> CTGPostChild(ContractorGroup contractorGroup, int? parentId)
        {
            contractorGroup.CtG_ParentId = parentId;
            _context.Add(contractorGroup);
            await _context.SaveChangesAsync();
            return Ok(contractorGroup.CtG_Id);
        }
        [HttpPut("group")]
        public async Task<IActionResult> CTGPut(ContractorGroup contractorGroup)
        {
            _context.Entry(contractorGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("group/{id}")]
        public async Task<IActionResult> CTGDelete(int id)
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

        [HttpGet("contractorList")]
        public async Task<IActionResult> CTRGetList()
        {
            var currencies = await _context.Contractors.Include(x => x.ContractorGroup).Where(x => x.CtR_CTGID == x.ContractorGroup.CtG_Id).ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CTRGetSingle (int id)
        {
            var contractor = await _context.Contractors.Include(x => x.ContractorGroup).Where(x => x.CtR_CTGID == x.ContractorGroup.CtG_Id).FirstOrDefaultAsync(a => a.CtR_Id == id);
            return Ok(contractor);
        }
        [HttpPost]
        public async Task<IActionResult> CTRPost(Contractor contractor)
        {
            _context.Add(contractor);
            await _context.SaveChangesAsync();
            return Ok(contractor.CtR_Id);
        }
        [HttpPut]
        public async Task<IActionResult> CTRPut(Contractor contractor)
        {
            _context.Entry(contractor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CTRDelete(int id)
        {
            var contractor = new Contractor { CtR_Id = id };
            _context.Remove(contractor);
            await _context.SaveChangesAsync();
            return NoContent();
        }




    }

}
