using FreightManagement.Domain.Common;


namespace FreightManagement.Entities.Vehicle
{
   public class Truck : AuditableEntity
    {
        public long Id { get; set; }

        public string NumberPlate;
    }
}
