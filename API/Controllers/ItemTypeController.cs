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
            var itemTypes = await _itemTypeRepository.GetItemTypesAsync();

            var output = itemTypes.Select(it => _mapper.Map<ItemTypeDto>(it));

            return Ok(output);
        }

        // GET api/<ItemTypeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(id);

            if (itemType is null)
                return NotFound("ItemType does not exist");

            var output = _mapper.Map<ItemTypeDto>(itemType);

            return Ok(output);
        }

        // POST api/<ItemTypeController>
        [HttpPost]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Post([FromBody] CreateItemTypeDto itemTypeDto)
        {
            var itemType = _mapper.Map<ItemType>(itemTypeDto);

            _itemTypeRepository.Add(itemType);
            await _itemTypeRepository.SaveAllAsync();

            var output = _mapper.Map<ItemTypeDto>(itemType);

            return Created($"itemType/{itemType.Id}", output);
        }

        // PUT api/<ItemTypeController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Put(int id, [FromBody] CreateItemTypeDto itemTypeDto)
        {
            var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(id);

            if (itemType is null)
                return NotFound("ItemType does not exist");

            _mapper.Map(itemTypeDto, itemType);

            _itemTypeRepository.Update(itemType);
            var saved = await _itemTypeRepository.SaveAllAsync();

            if (saved is false)
                return BadRequest("Failed to save ItemType");

            var output = _mapper.Map<ItemTypeDto>(itemType);

            return Ok(output);
        }

        // DELETE api/<ItemTypeController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.RequireAdminRole)]
        public async Task<IActionResult> Delete(int id)
        {
            var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(id);

            if (itemType is null)
                return NotFound("ItemType does not exist");

            _itemTypeRepository.Delete(itemType);
            var saved = _itemTypeRepository.SaveAllAsync();

            return NoContent();
        }
    }
}
