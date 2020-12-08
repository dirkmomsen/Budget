using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    public class BudgetController : BaseApiController
    {
        private readonly DataContext _context;

        public int UserId
        {
            get {
                return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }


        public BudgetController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<BudgetController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var budgets = await _context.Budgets
                .Where(b => b.Users.Any(u => u.Id == UserId))
                .ToListAsync();

            return Ok(budgets);
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var budget = await _context.Budgets
                .Where(b => b.Users.Any(u => u.Id == UserId))
                .FirstOrDefaultAsync(b => b.Id == id);

            if (budget == null)
                return NotFound("Budget not found for this user");

            return Ok(budget);
        }

        // POST api/<BudgetController>
        [HttpPost]
        public void Post([FromBody] BudgetDto budget)
        {

        }

        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
