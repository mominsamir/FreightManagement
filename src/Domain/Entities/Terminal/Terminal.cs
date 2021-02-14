using FreightManagement.Domain.Common;

namespace FreightManagement.Domain.Entities.Terminal
{
    public class Terminal: AuditableEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
