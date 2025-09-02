using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Commands.CreateProperty
{
    public class CreatePropertyResponse
    {
        public int PropertyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CodeInternal { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
