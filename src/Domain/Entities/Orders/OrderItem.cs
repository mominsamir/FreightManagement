using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Products;


namespace FreightManagement.Domain.Entities.Orders
{
    public class OrderItem  : AuditableEntity
    {
        public long Id { get; set; }
        public Location Location { get; set; }
        public FuelProduct FuelProduct { get; set; }
        public double Quantity { get; set; } = 0;
        public string LoadCode { get; set; }
        public Order Order { get; set; }

        public OrderItem() { }

        public OrderItem(FuelProduct product, Location location, double qnt, string loadCode, Order order)
        {
            FuelProduct = product;
            Location = location;
            Quantity = qnt;
            LoadCode = loadCode;
            Order = order;
        }
    }
}
