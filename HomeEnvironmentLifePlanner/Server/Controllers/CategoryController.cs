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
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _context.Categories.Include(x => x.CategoryType).Where(x=>x.CaT_CTYID==x.CategoryType.CtY_Id).Include(y => y.CaT_Children).FirstOrDefaultAsync(a => a.CaT_Id == id);
            return Ok(category);
        }
        //[HttpGet("GetChildren/{parentId}")]
        //public async Task<IActionResult> GetChildren(int parentId)
        //{
        //    var category = await _context.Categories.Include(x => x.CategoryType).Where(x => x.CaT_CTYID == x.CategoryType.CtY_Id).Where(x=>x.CaT_ParentId==parentId).ToListAsync();
        //    return Ok(category);
        //}
        [HttpPost]
        public async Task<IActionResult> Post(Category category )
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category.CaT_Id);
        }
        [HttpPost("{parentId}")]
        public async Task<IActionResult> Post(Category category,int? parentId)
        {
            if (parentId != 1 && category.CaT_Id==1)
                category.CaT_CTYID = _context.Categories.Where(x => x.CaT_Id == parentId).FirstOrDefault().CaT_CTYID; 
            category.CaT_ParentId = parentId;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category.CaT_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = new Category { CaT_Id = id };
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
