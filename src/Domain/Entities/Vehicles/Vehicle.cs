using FreightManagement.Domain.Common;

namespace FreightManagement.Domain.Entities.Vehicles
{
    public class Vehicle : AuditableEntity
    {
        public long Id { get; set; }

        public string NumberPlate { get; set; }

        public VehicleStatus Status { get; set; }
    }

    public enum VehicleStatus
    {
        ACTIVE,
        OUT_OF_SERVICE
    }
}
