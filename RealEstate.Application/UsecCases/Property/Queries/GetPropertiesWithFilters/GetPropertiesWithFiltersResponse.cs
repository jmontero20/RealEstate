using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters
{
    public class GetPropertiesWithFiltersResponse
    {
        public int PropertyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public IList<string> ImageUrls { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
    }
}
