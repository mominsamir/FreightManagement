using FreightManagement.Domain.Common;
using System.Collections.Generic;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.Customers

{
   public class Customer : AuditableEntity
    {

        public Customer()
        {
            Locations = new List<Location>();
        }

        public long Id { get; set; }

        public List<Location> Locations { get; set; }
        public Address BillingAddress { get; set; }

    }
}
