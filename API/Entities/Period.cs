using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Period : BaseEntity
    {
        public string Name { get; set; }
        public int Days { get; set; }

        public ICollection<BudgetItem> Items { get; set; }

        public ICollection<Budget> Budgets { get; set; }

        public Period()
        {
            Items = new List<BudgetItem>();
            Budgets = new List<Budget>();
        }
    }
}
