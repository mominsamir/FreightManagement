using FreightManagement.Domain.Common;

namespace FreightManagement.Domain.Entities.Vehicles
{
    // TODO: I never use inheritance get my parents fields 
    // TODO: Inheritance should be use to get parents behaviour 
    public class Vehicle : AuditableEntity
    {
        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public VehicleStatus Status { get; set; }
    }

    public enum VehicleStatus
    {
        ACTIVE,
        UNDER_MAINTNCE,
        OUT_OF_SERVICE
    }
}
