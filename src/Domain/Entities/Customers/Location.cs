using FreightManagement.Domain.Common;
using FreightManagement.Domain.ValueObjects;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Customers
{
    public class Location : AuditableEntity
    {
        public Location()
        {
            Tanks = new List<LocationTank>();
        }
        public long Id { get; set; }
        public string Name{ get; set; }
        public Address DeliveryAddress { get; set; }
        public Address BillingAddress { get; set; }
        public Customer Customer { get; set; }
        public List<LocationTank> Tanks { get; set; }
    }
}
