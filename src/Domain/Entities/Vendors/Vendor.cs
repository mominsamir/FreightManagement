using FreightManagement.Domain.Common;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.Vendors
{
    public class Vendor: AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public Address Address { get; set; }
        public bool IsActive { get; set; }

    }

}
