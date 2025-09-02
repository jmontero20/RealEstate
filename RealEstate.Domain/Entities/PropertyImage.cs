using RealEstate.Domain.Comon;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage : BaseEntity
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        // Navigation properties
        public virtual Property Property { get; set; } = null!;
    }
}
