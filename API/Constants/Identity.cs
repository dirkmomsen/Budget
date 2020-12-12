using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Constants.Identity
{
    public class Policy
    {
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireUserRole = "RequireUserRole";
        public const string RequireUserOrAdminRole = "RequireUserOrAdminRole";
    }

    public class Role
    {
        public const string User = "User";
        public const string Administrator = "Admin";
    }
}
