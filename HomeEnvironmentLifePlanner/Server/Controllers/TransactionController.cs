﻿using HomeEnvironmentLifePlanner.Server.Data;
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
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TransactionController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transactions = await _context.Transactions
                .Include(ctr => ctr.Contractor).Where(ctr => ctr.TrN_CTRID == ctr.Contractor.CtR_Id)
                .Include(pyt => pyt.PaymentType).Where(pyt => pyt.TrN_PYTID == pyt.PaymentType.PyT_Id)
                .Include(cur => cur.Currency).Where(cur => cur.TrN_CURID == cur.Currency.CuR_Id)
                .Include(bsp => bsp.BankStatmentPosition).Where(bsp => bsp.TrN_BSPID == bsp.BankStatmentPosition.BsP_Id)
                .ToListAsync();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var transaction = await _context.Transactions
                .Include(ctr => ctr.Contractor).Where(ctr => ctr.TrN_CTRID == ctr.Contractor.CtR_Id)
                .Include(pyt => pyt.PaymentType).Where(pyt => pyt.TrN_PYTID == pyt.PaymentType.PyT_Id)
                .Include(cur => cur.Currency).Where(cur => cur.TrN_CURID == cur.Currency.CuR_Id)
                .Include(bsp => bsp.BankStatmentPosition).Where(bsp => bsp.TrN_BSPID == bsp.BankStatmentPosition.BsP_Id)
                .FirstOrDefaultAsync(a => a.TrN_Id == id);
            return Ok(transaction);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Transaction transaction)
        {
            transaction.TrN_BSPID = null;
            transaction.TrN_CreateDate = DateTime.Now;
            _context.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction.TrN_Id);
        }
        [HttpPut]
        public async Task<IActionResult> Put(Transaction transaction)
        {
            try
            {
                transaction.TrN_BSPID = null;
                transaction.TrN_CreateDate = DateTime.Now;
                _context.Entry(transaction).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = new Transaction { TrN_Id = id };
            _context.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}