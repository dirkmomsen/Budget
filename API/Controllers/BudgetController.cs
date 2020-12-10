using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public int UserId
        {
            get
            {
                return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        public BudgetController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<BudgetController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var budgets = await _context.Budgets
                .Include(b => b.UserBudgets)
                .ThenInclude(ub => ub.User)
                .Where(b => b.Users.Any(u => u.Id == UserId))
                .Where(b => b.Deleted == false)
                .ToListAsync();

            return Ok(budgets.Select(_mapper.Map<BudgetDto>));
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var budget = await _context.Budgets
                .Include(b => b.UserBudgets)
                .ThenInclude(ub => ub.User)
                .Where(b => b.Users.Any(u => u.Id == UserId))
                .Where(b => b.Deleted == false)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (budget == null)
                return NotFound("Budget not found for this user");

            return Ok(_mapper.Map<BudgetDto>(budget));
        }

        // POST api/<BudgetController>
        [HttpPost]
        public async Task Post([FromBody] BudgetDto budgetDto)
        {
            //budgetDto.Users.

            //var budget = _mapper.Map<Budget>(budgetDto);
            //budget.Us

            //_context.Budgets.Add();
            //await _context.SaveChangesAsync();


        }

        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] BudgetDto budgetDto)
        {

        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var budget = await _context.Budgets
                .Include(x => x.UserBudgets)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (budget is null) return NotFound();

            var userBudget = budget.UserBudgets
                .Where(x => x.UserId == UserId)
                .Where(x => x.Administrator)
                .ToList();

            if (userBudget.Count <= 0) return Unauthorized();

            budget.Deleted = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<bool> BudgetExistsAsync(int id)
        {
            return await _context.Budgets.AnyAsync(x => x.Id == id);
        }
    }
}
