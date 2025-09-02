using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Commands.UpdateProperty
{
    public class UpdatePropertyResponse
    {
        public int PropertyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public bool PriceChanged { get; set; }
    }
}
