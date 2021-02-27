using FreightManagement.Domain.Common;

namespace FreightManagement.Domain.Entities.Vehicles
{
    public class VehicleCheckList : AuditableEntity
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
