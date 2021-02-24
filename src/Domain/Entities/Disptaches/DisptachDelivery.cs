using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;

namespace FreightManagement.Domain.Entities.Disptaches
{
    public class DisptachDelivery : AuditableEntity
    {
        public long Id { get; set; }
        public Location Location { get; set; }
        public double DeliveredQnt { get; set; }
        public string ReceivedByName { get; set; }
        public DispatchLoading DispatchLoading { get; set; }

    }
}
