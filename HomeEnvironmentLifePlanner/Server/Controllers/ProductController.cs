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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("groupList")]
        public async Task<IActionResult> PRGGetList()
        {
            var productGroups = await _context.ProductGroups.ToListAsync();
            return Ok(productGroups);
        }
        [HttpGet("group/{id}")]
        public async Task<IActionResult> PRGGetSingle(int id)
        {
            var productGroup = await _context.ProductGroups.FirstOrDefaultAsync(a => a.PrG_Id == id);
            return Ok(productGroup);
        }
        [HttpGet("group/getAssignedProducts/{id}")]
        public async Task<IActionResult> PRGGetChildren(int id)
        {
            var prdList = await _context.Products.Where(x=>x.PrD_PRGID==id).ToListAsync();
            return Ok(prdList);
        }
        [HttpPost("group")]
        public async Task<IActionResult> PRGPost(ProductGroup productGroup )
        {
            _context.Add(productGroup);
            await _context.SaveChangesAsync();
            return Ok(productGroup.PrG_Id);
        }
  
        [HttpPost("group/addChild/{parentId}")]
        public async Task<IActionResult> PRGPostChild(ProductGroup productGroup, int? parentId)
        {
            productGroup.PrG_ParentId = parentId;
            _context.Add(productGroup);
            await _context.SaveChangesAsync();
            return Ok(productGroup.PrG_Id);
        }
        [HttpPut("group")]
        public async Task<IActionResult> PRGPut(ProductGroup productGroup)
        {
            _context.Entry(productGroup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("group/{id}")]
        public async Task<IActionResult> PRGDelete(int id)
        {
            var productGroup = new ProductGroup { PrG_Id = id };
            if (_context.Products.Where(x => x.PrD_PRGID == id).Any())
            {
                return Content(@"<script language='javascript' type='text/javascript'>alert('Nie można usunąć grupy. XYZ!');</script>");
            }
            else
            {
                _context.Remove(productGroup);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpGet("productList")]
        public async Task<IActionResult> PRDGetList()
        {
            var products = await _context.Products.Include(x => x.ProductGroup).Where(x => x.PrD_PRGID == x.ProductGroup.PrG_Id).ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> PRDGetSingle (int id)
        {
            var product = await _context.Products.Include(x => x.ProductGroup).Where(x => x.PrD_PRGID == x.ProductGroup.PrG_Id).FirstOrDefaultAsync(a => a.PrD_Id == id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> PRDPost(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product.PrD_Id);
        }
        [HttpPut]
        public async Task<IActionResult> PRDPut(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> PRDDelete(int id)
        {
            var product = new Product { PrD_Id = id };
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }




    }

}
