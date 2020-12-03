using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class BudgetItems : BaseEntity
    {
        public string Description { get; set; }
        public ItemType Type { get; set; }
    }
}
