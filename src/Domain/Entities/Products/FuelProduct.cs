namespace FreightManagement.Domain.Entities.Products
{
    public class FuelProduct : Product
    {
        public FuelGrade Grade { get; set; }
    }

    public enum FuelGrade
    {
        REGULAR,
        PLUS,
        SUPER,
        DIESEL_CLR,
        DIESEL_DYD
    }
}
