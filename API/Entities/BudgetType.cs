using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class BudgetType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Budget> Budgets { get; set; }

        public BudgetType()
        {
            Budgets = new List<Budget>();
        }
    }
}
