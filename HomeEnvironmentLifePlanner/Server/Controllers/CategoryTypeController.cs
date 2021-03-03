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
    public class CategoryTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryTypeController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currencies = await _context.CategoryTypes.ToListAsync();
            return Ok(currencies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categoryType = await _context.CategoryTypes.FirstOrDefaultAsync(a => a.CtY_Id == id);
            return Ok(categoryType);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryType categoryType)
        {
            _context.Add(categoryType);
            await _context.SaveChangesAsync();
            return Ok(categoryType.CtY_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(CategoryType categoryType)
        {
            _context.Entry(categoryType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryType = new CategoryType { CtY_Id = id };


            _context.Remove(categoryType);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }

}
