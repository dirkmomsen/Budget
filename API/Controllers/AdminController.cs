using API.Constants.Identity;
using API.DTOs;
using API.Entities.Identity;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Policy = Policy.RequireAdminRole)]
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _userManager
                .GetUsersWithRolesAsync();

            var output = users.Select(u => _mapper.Map<UserWithRoleDto>(u));

            return Ok(output);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserWithRoles(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return NotFound("Could not find user");

            return Ok(user);
        }

        [HttpPost("users/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) 
                return NotFound("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) 
                return BadRequest("Falied to add the roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) 
                return BadRequest("Failed to remove old roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

    }
}
