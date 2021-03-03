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
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return Ok(accounts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AcC_Id == id);
            return Ok(account);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Account account )
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return Ok(account.AcC_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var account = new Account { AcC_Id = id };
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
