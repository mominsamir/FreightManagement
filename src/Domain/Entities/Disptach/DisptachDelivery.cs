using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Products;

namespace FreightManagement.Domain.Entities.Disptach
{
    public class DisptachDelivery : AuditableEntity
    {
        public long Id { get; set; }

        public FuelProduct FuelProduct { get; set; }

        public Location Location { get; set; }

        public double DeliveredQnt { get; set; }

        public string BillOfLoading { get; set; }

        public string ReceivedByName { get; set; }

    }
}
