using FreightManagement.Domain.Common;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.StorageRack
{
    public class Rack: AuditableEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string IRSCode { get; set; }

        public Address Address { get; set; }

        public bool IsActive { get; set; }
    }
}
