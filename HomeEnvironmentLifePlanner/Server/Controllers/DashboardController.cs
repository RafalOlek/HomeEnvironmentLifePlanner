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
    
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet("raport1/{year}/{month}")]
        public async Task<IActionResult> GetRaport1( int year, int month)
        {
            var raport1 = await _context.GetAllExpenditureAndRevenueInMonth(year, month).ToListAsync();
            return Ok(raport1);
        }
    }
}
