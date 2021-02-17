using FreightManagement.Domain.Common;


namespace FreightManagement.Domain.Entities.Customers
{
    public class LocationTank: AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Capactity { get; set; }

        public Location Location { get; set; }
    }
}
