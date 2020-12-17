using API.Constants.Identity;
using API.DTOs;
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
    public class ItemTypeController : BaseApiController
    {
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IMapper _mapper;

        public ItemTypeController(IItemTypeRepository itemTypeRepository, IMapper mapper)
        {
            _itemTypeRepository = itemTypeRepository;
            _mapper = mapper;
        }

        // GET: api/<ItemTypeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        // GET api/<ItemTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok();
        }

        // POST api/<ItemTypeController>
        [HttpPost]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Post([FromBody] CreateItemTypeDto itemTypeDto)
        {
            return Created($"itemType/{itemTypeDto.Name}", itemTypeDto);
        }

        // PUT api/<ItemTypeController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateItemTypeDto itemTypeDto)
        {
            return Ok(itemTypeDto);
        }

        // DELETE api/<ItemTypeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}
