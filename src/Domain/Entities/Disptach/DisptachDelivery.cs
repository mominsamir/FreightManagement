

using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;

namespace FreightManagement.Domain.Entities.Disptach
{
    public class DisptachDelivery : AuditableEntity
    {
        public long Id { get; set; }

        public Location Location { get; set; }

        public double DeliveredQnt { get; set; }

        public string BillOfLoading { get; set; }



    }
}
