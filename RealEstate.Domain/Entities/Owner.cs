using RealEstate.Domain.Comon;

namespace RealEstate.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public int IdOwner { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
