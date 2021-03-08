using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeEnvironmentLifePlanner.Server.Data;
using HomeEnvironmentLifePlanner.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HomeEnvironmentLifePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ConfigController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("categoryList")]
        public async Task<IActionResult> CATGetList()
        {
            try
            {
                var categorys = await _context.Categories
                    .Include(x => x.CategoryType).Where(x => x.CaT_CTYID == x.CategoryType.CtY_Id)
                    .Include(y => y.CaT_Children).ToListAsync();
                return Ok(categorys);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> CATGetSingle(int id)
        {
            var category = await _context.Categories.Include(x => x.CategoryType).Where(x => x.CaT_CTYID == x.CategoryType.CtY_Id).Include(y => y.CaT_Children).FirstOrDefaultAsync(a => a.CaT_Id == id);
            return Ok(category);
        }
        [HttpPost("category")]
        public async Task<IActionResult> CATPost(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category.CaT_Id);
        }
        [HttpPost("category/{parentId}")]
        public async Task<IActionResult> CATPostParent(Category category, int? parentId)
        {
            if (parentId != 1 && category.CaT_Id == 1)
                category.CaT_CTYID = _context.Categories.Where(x => x.CaT_Id == parentId).FirstOrDefault().CaT_CTYID;
            category.CaT_ParentId = parentId;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category.CaT_Id);
        }
        [HttpPut("category")]
        public async Task<IActionResult> CATPut(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("category/{id}")]
        public async Task<IActionResult> CATDelete(int id)
        {
            var category = new Category { CaT_Id = id };
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("categoryTypeList")]
        public async Task<IActionResult> CTYGetList()
        {
            var currencies = await _context.CategoryTypes.ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("categoryType/{id}")]
        public async Task<IActionResult> CTYGetSingle(int id)
        {
            var categoryType = await _context.CategoryTypes.FirstOrDefaultAsync(a => a.CtY_Id == id);
            return Ok(categoryType);
        }
        [HttpPost("categoryType")]
        public async Task<IActionResult> CTYPost(CategoryType categoryType)
        {
            _context.Add(categoryType);
            await _context.SaveChangesAsync();
            return Ok(categoryType.CtY_Id);
        }
        [HttpPut("categoryType")]
        public async Task<IActionResult> CTYPut(CategoryType categoryType)
        {
            _context.Entry(categoryType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("categoryType/{id}")]
        public async Task<IActionResult> CTYDelete(int id)
        {
            var categoryType = new CategoryType { CtY_Id = id };


            _context.Remove(categoryType);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpGet("currencyList")]
        public async Task<IActionResult> CURGetList()
        {
            var currencies = await _context.Currencies.ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("currency/{id}")]
        public async Task<IActionResult> CURGetSingle(int id)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(a => a.CuR_Id == id);
            return Ok(currency);
        }
        [HttpPost("currency")]
        public async Task<IActionResult> CURPost(Currency currency)
        {
            _context.Add(currency);
            await _context.SaveChangesAsync();
            return Ok(currency.CuR_Id);
        }
        [HttpPut("currency")]
        public async Task<IActionResult> CURPut(Currency currency)
        {
            _context.Entry(currency).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("currency/{id}")]
        public async Task<IActionResult> CURDelete(int id)
        {
            var currency = new Currency { CuR_Id = id };
            _context.Remove(currency);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("paymentTypeList")]
        public async Task<IActionResult> PTYGetList()
        {
            try
            {
                var paymentTypes = await _context.PaymentTypes.Include(x => x.Account).Where(x => x.PyT_ACCID == x.Account.AcC_Id).ToListAsync();
                return Ok(paymentTypes);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("paymentType/{id}")]
        public async Task<IActionResult> PTYGetSingle(int id)
        {
            var paymentType = await _context.PaymentTypes.FirstOrDefaultAsync(a => a.PyT_Id == id);
            return Ok(paymentType);
        }
        [HttpPost("paymentType")]
        public async Task<IActionResult> PTYPost(PaymentType paymentType)
        {
            _context.Add(paymentType);
            await _context.SaveChangesAsync();
            return Ok(paymentType.PyT_Id);
        }
        [HttpPut("paymentType")]
        public async Task<IActionResult> PTYPut(PaymentType paymentType)
        {
            _context.Entry(paymentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("paymentType/{id}")]
        public async Task<IActionResult> PTYDelete(int id)
        {
            var paymentType = new PaymentType { PyT_Id = id };
            _context.Remove(paymentType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("accountList")]
        public async Task<IActionResult> ACCGetList()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return Ok(accounts);
        }
        [HttpGet("account/{id}")]
        public async Task<IActionResult> ACCGetSingle(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AcC_Id == id);
            return Ok(account);
        }
        [HttpPost("account")]
        public async Task<IActionResult> ACCPost(Account account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return Ok(account.AcC_Id);
        }
        [HttpPut("account")]
        public async Task<IActionResult> ACCPut(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("account/{id}")]
        public async Task<IActionResult> ACCDelete(int id)
        {
            var account = new Account { AcC_Id = id };
            _context.Remove(account);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
