using FreightManagement.Domain.Common;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.Terminal
{
    public class Terminal: AuditableEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string IRSCode { get; set; }

        public Address Address { get; set; }

        public bool isActive { get; set; }
    }
}
