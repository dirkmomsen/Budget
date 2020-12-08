using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class BudgetDto : BaseDto
    {
        public string Name { get; set; }
        public int TypeId { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
