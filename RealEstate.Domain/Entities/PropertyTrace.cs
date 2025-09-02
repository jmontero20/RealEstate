using RealEstate.Domain.Comon;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace : BaseEntity
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }

        // Navigation properties
        public virtual Property Property { get; set; } = null!;
    }
}
