using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CreateItemTypeDto
    {
        public string Name { get; set; }
        public char DisplaySymbol { get; set; }
    }
}
