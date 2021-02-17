﻿using FreightManagement.Domain.Common;
using System.Collections.Generic;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.Customers

{
   public class Customer : AuditableEntity
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public Address BillingAddress { get; set; }
        public bool IsActive { get; set; }

        private List<Location> _locations;

        public IEnumerable<Location> Locations { get { return _locations; } }
        
        public Customer()
        {
            _locations = new List<Location>();
            IsActive = true;
        }

        public void AddLocation(Location location)
        {
            _locations.Add(location);
        }

        public void RemoveLocation(Location location)
        {
            _locations.Remove(location);
        }

    }
}
