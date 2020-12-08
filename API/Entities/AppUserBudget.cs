using API.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUserBudget
    {
        [Column("UserId")]
        public int UserId { get; set; }
        public AppUser User { get; set; }

        [Column("BudgetId")]
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }

        public bool Administrator { get; set; } = false;
    }
}
