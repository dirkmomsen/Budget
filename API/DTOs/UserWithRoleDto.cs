using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserWithRoleDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
