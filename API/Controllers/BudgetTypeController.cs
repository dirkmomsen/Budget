using API.Constants.Identity;
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
    [Authorize(Policy = Policy.RequireUserOrAdminRole)]
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

            if (budgetType is null)
                return NotFound("BudgetType does not exist");

            var ouptut = _mapper.Map<BudgetTypeDto>(budgetType);

            return Ok(ouptut);
        }

        // POST api/<BudgetTypeController>
        [HttpPost]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Post([FromBody] CreateBudgetTypeDto budgetTypeDto)
        {
            var budgetType = _mapper.Map<BudgetType>(budgetTypeDto);

            _budgetTypeRepository.Add(budgetType);
            await _budgetTypeRepository.SaveAllAsync();

            var output = _mapper.Map<BudgetTypeDto>(budgetType);

            return Created($"budgetType/{budgetType.Id}", output);
        }

        // PUT api/<BudgetTypeController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateBudgetTypeDto budgetTypeDto)
        {
            var budgetType = await _budgetTypeRepository.GetBudgetTypeByIdAsync(id);

            if (budgetType is null)
                return NotFound("BudgetType does not exist");

            _mapper.Map(budgetTypeDto, budgetType);

            _budgetTypeRepository.Update(budgetType);
            var saved = await _budgetTypeRepository.SaveAllAsync();

            if (saved is false)
                return BadRequest("Falied to save BudgetType");

            var output = _mapper.Map<BudgetTypeDto>(budgetType);

            return Ok(output);
        }

        // DELETE api/<BudgetTypeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            var budgetType = await _budgetTypeRepository.GetBudgetTypeByIdAsync(id);

            if (budgetType is null)
                return NotFound("BudgetType does not exist");

            _budgetTypeRepository.Delete(budgetType);
            await _budgetTypeRepository.SaveAllAsync();

            return NoContent();
        }
    }
}
