using API.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Budget : BaseEntity
    {
        public List<AppUser> Users { get; set; }
        public string Name { get; set; }
        public List<BudgetItems> Items { get; set; }


    }
}
