using FreightManagement.Domain.Common;
using System.Collections.Generic;
using FreightManagement.Domain.ValueObjects;
using System.Linq;

namespace FreightManagement.Domain.Entities.Customers

{
   public class Customer : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public Address BillingAddress { get; set; }
        public bool IsActive { get; private set; } = true;
        private readonly List<Location> _locations;
        public IEnumerable<Location> Locations => _locations;

        public Customer()
        {
            IsActive = true;
            _locations = new List<Location>();
        }

        public void AddLocation(Location location)
        {
            _locations.Add(location);
        }

        public Location FindLocationByIndex(int id)
        {
            return _locations.ElementAt(id);
        }

        public void RemoveLocation(long locationId)
        {
            var index = _locations.FindIndex(l => l.Id == locationId);
            _locations.RemoveAt(index);
        }

        public void MarkActive()
        {
            IsActive = true;
        }

        public void MarkInActive()
        {
            IsActive = false;
        }

    }
}
