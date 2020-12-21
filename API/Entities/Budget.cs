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
        public ICollection<AppUserBudget> UserBudgets { get; set; }
        public string Name { get; set; }

        public int TypeId { get; set; }
        public BudgetType Type { get; set; }

        public int IntervalId { get; set; }
        public Interval Interval { get; set; }

        public ICollection<BudgetItem> Items { get; set; }

        public Budget()
        {
            Users = new List<AppUser>();
            UserBudgets = new List<AppUserBudget>();
        }
    }
}
