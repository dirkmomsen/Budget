﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CreateBudgetDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
}
