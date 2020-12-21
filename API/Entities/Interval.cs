using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Interval : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Length { get; set; }

        public ICollection<BudgetItem> Items { get; set; }

        public ICollection<Budget> Budgets { get; set; }

        public Interval()
        {
            Items = new List<BudgetItem>();
            Budgets = new List<Budget>();
        }
    }
}
