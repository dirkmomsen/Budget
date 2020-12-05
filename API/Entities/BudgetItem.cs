using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class BudgetItem : BaseEntity
    {
        public string Description { get; set; }

        public decimal Value { get; set; }

        public int TypeId { get; set; }
        public ItemType Type { get; set; }

        public int BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}
