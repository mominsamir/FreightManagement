using FreightManagement.Domain.Common;


namespace FreightManagement.Domain.Entities.Products
{
    public class Product: AuditableEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public UnitOfMeasure UOM { get; set; }

    }


    public enum UnitOfMeasure
    {
        UNIT,
        GALLON,
        PACKAGE
    }

}
