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
    public class CurrencyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CurrencyController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currencies = await _context.Currencies.ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(a => a.CuR_Id == id);
            return Ok(currency);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Currency currency )
        {
            _context.Add(currency);
            await _context.SaveChangesAsync();
            return Ok(currency.CuR_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Currency currency)
        {
            _context.Entry(currency).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currency = new Currency { CuR_Id = id };
            _context.Remove(currency);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
