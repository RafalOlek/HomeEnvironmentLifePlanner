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
    public class PaymentTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PaymentTypeController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var paymentTypes = await _context.PaymentTypes.Include(x=>x.Account).Where(x=>x.PyT_ACCID==x.Account.AcC_Id).ToListAsync();
                return Ok(paymentTypes);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var paymentType = await _context.PaymentTypes.FirstOrDefaultAsync(a => a.PyT_Id == id);
            return Ok(paymentType);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PaymentType paymentType)
        {
            _context.Add(paymentType);
            await _context.SaveChangesAsync();
            return Ok(paymentType.PyT_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(PaymentType paymentType)
        {
            _context.Entry(paymentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paymentType = new PaymentType { PyT_Id = id };
            _context.Remove(paymentType);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }

}
