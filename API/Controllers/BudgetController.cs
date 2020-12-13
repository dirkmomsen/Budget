using API.Constants.Identity;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
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
    [Authorize(Policy = Policy.RequireUserRole)]
    public class BudgetController : BaseApiController
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IBudgetTypeRepository _budgetTypeRepository;
        private readonly IMapper _mapper;

        public int UserId
        {
            get
            {
                return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }

        public BudgetController(IBudgetRepository budgetRepository, IBudgetTypeRepository budgetTypeRepository, IMapper mapper)
        {
            _budgetRepository = budgetRepository;
            _budgetTypeRepository = budgetTypeRepository;
            _mapper = mapper;
        }

        // GET: api/<BudgetController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var budgets = await _budgetRepository.GetBudgetsAsync(UserId);
            var output = budgets.Select(b => _mapper.Map<BudgetDto>(b));

            return Ok(output);
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var budget = await _budgetRepository.GetBudgetByIdAsync(id, UserId);

            if (budget is null)
                return NotFound("Budget not found for this user");

            var output = _mapper.Map<BudgetDto>(budget);

            return Ok(output);
        }

        // POST api/<BudgetController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBudgetDto budgetDto)
        {
            //this should move to a validation filter
            var budgetType = await _budgetTypeRepository.GetBudgetTypeByIdAsync(budgetDto.TypeId);

            if (budgetType is null)
                return BadRequest("Invalid budget type");

            var budget = _mapper.Map<Budget>(budgetDto);

            budget.UserBudgets.Add(new()
            {
                UserId = UserId,
                Administrator = true
            });

            _budgetRepository.AddBudget(budget);
            await _budgetRepository.SaveAllAsync();

            var output = _mapper.Map<BudgetDto>(budget);

            return Created($"budget/{budget.Id}", output);

        }

        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateBudgetDto budgetDto)
        {
            var budgetType = await _budgetTypeRepository.GetBudgetTypeByIdAsync(budgetDto.TypeId);

            if (budgetType is null)
                return NotFound("BudgetType does not exist");

            var budget = await _budgetRepository.GetBudgetByIdAsync(id, UserId);

            if (budget is null) return NotFound("Budget does not exist");

            _mapper.Map(budgetDto, budget);

            _budgetRepository.UpdateBudget(budget);
            var saved = await _budgetRepository.SaveAllAsync();

            if (saved is false) return BadRequest("Failed to save Budget");

            var output = _mapper.Map<BudgetDto>(budget);

            return Ok(output);
        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var budget = await _budgetRepository.GetBudgetByIdAsync(id, UserId);

            if (budget is null) return NotFound();

            var adminUserBudgets = budget.UserBudgets
                .Where(x => x.Administrator)
                .ToList();

            if (adminUserBudgets.Count <= 0) return Unauthorized();

            _budgetRepository.DeleteBudget(budget);

            var saved = await _budgetRepository.SaveAllAsync();

            return NoContent();
        }
    }
}
