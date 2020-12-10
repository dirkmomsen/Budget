using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    public class BudgetTypeController : BaseApiController
    {
        private readonly IBudgetTypeRepository _budgetTypeRepository;
        private readonly IMapper _mapper;

        public BudgetTypeController(IBudgetTypeRepository budgetTypeRepository, IMapper mapper)
        {
            _budgetTypeRepository = budgetTypeRepository;
            _mapper = mapper;
        }

        // GET: api/<BudgetTypeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var budgetTypes = await _budgetTypeRepository.GetBudgetTypesAsync();

            var ouptut = budgetTypes.Select(x => _mapper.Map<BudgetTypeDto>(x));

            return Ok(ouptut);
        }

        // GET api/<BudgetTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var budgetType = await _budgetTypeRepository.GetBudgetTypeByIdAsync(id);

            var ouptut = _mapper.Map<BudgetTypeDto>(budgetType);

            return Ok(ouptut);
        }

        // POST api/<BudgetTypeController>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/<BudgetTypeController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<BudgetTypeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int id)
        {
            var budgetType = await _budgetTypeRepository.GetBudgetTypeByIdAsync(id);

            if (budgetType is null)
                return NotFound();

            _budgetTypeRepository.Delete(budgetType);
            await _budgetTypeRepository.SaveAllAsync();

            return NoContent();
        }
    }
}
