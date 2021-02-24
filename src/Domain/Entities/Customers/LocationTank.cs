using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Products;

namespace FreightManagement.Domain.Entities.Customers
{
    public class LocationTank: AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FuelGrade FuelGrade { get; set; }
        public double Capactity { get; set; }
        public Location Location { get; set; }
    }
}
