using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace API.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}