using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace API.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
        public List<Budget> Budgets { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}