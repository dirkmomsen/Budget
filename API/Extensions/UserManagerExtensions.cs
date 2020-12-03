using API.DTOs;
using API.Entities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<List<UserWithRoleDto>> GetUsersWithRolesAsync(this UserManager<AppUser> userManager, IMapper mapper)
        {
            return await userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .OrderBy(u => u.UserName)
                .Select(u => mapper.Map<UserWithRoleDto>(u))
                .ToListAsync();

            //return await userManager.Users
            //    .Include(u => u.UserRoles)
            //    .ThenInclude(ur => ur.Role)
            //    .OrderBy(u => u.UserName)
            //    .Select(u => new
            //    {
            //        u.Id,
            //        Username = u.UserName,
            //        Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            //    })
            //    .ToListAsync();
        }
    }
}
