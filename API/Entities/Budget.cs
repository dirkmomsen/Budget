using API.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Budget : BaseEntity
    {
        public ICollection<AppUser> Users { get; set; }
        public string Name { get; set; }

        public int TypeId { get; set; }
        public BudgetType Type { get; set; }

        public ICollection<BudgetItem> Items { get; set; }


    }
}
