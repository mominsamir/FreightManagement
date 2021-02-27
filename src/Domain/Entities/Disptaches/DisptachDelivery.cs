using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;

namespace FreightManagement.Domain.Entities.Disptaches
{
    public class DisptachDelivery : AuditableEntity
    {
        public long Id { get; set; }
        public Location Location { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public double DeliveredQnt { get; set; }
        public string ReceivedByName { get; set; }
        public DispatchLoading DispatchLoading { get; set; }

        public void UpdateDeliveredQnt(double qnt, double balanceQnt)
        {
            DeliveredQnt = qnt;

            if (balanceQnt == 0.0)
                DeliveryType = DeliveryType.NIL;
            else if (balanceQnt == qnt)
                DeliveryType = DeliveryType.FULL;
            else if (balanceQnt > qnt)
                DeliveryType = DeliveryType.SPLIT;
            else
                DeliveryType = DeliveryType.OVERFLOW;
        }
    }

    public enum DeliveryType
    {   
        NIL,
        FULL,
        SPLIT,
        OVERFLOW
    }
}
