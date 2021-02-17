using FreightManagement.Domain.Common;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.Vendor
{
    public class Vendor: AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Email Email { get; set; }

        public VendorStatus Status { get; set; }

    }

    public enum VendorStatus
    {
        ACTIVE,
        INACTIVE
    }
}
