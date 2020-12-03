using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ItemType : BaseEntity
    {
        public string Name { get; set; }
        public char DisplaySymbol { get; set; }
    }
}
