using ExcelDataReader;
using HomeEnvironmentLifePlanner.Server.Data;
using HomeEnvironmentLifePlanner.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Server.Controllers
{
    [DisableRequestSizeLimit]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ShoppingListController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("headerList")]
        public async Task<IActionResult> SLHGetList()
        {
            try
            {
                var shoppingLists = await _context.ShoppingListHeaders
                                        .Include(x => x.ShoppingListPositions.Where(x => x.SlP_SLHID == x.ShoppingListHeader.SlH_Id))
                                        .ToListAsync();
                return Ok(shoppingLists);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [HttpGet("header/{id}")]
        public async Task<IActionResult> SLHGetSingle(int id)
        {
            var shoppingLists = await _context.ShoppingListHeaders
                               .Include(x => x.ShoppingListPositions.Where(x => x.SlP_SLHID == x.ShoppingListHeader.SlH_Id))
                               .FirstOrDefaultAsync(a => a.SlH_Id == id);
            return Ok(shoppingLists);
        }
        [HttpGet("header/getChildren/{slhid}")]
        public async Task<IActionResult> SLHGetChildren(int slhId)
        {
            try
            {
                var shoppingListPositions = await _context.ShoppingListPositions
                      .Where(a => a.SlP_SLHID == slhId).ToListAsync();
                return Ok(shoppingListPositions);
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
       
    }

}
