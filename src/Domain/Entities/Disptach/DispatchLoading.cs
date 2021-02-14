using FreightManagement.Domain.Common;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Disptach
{
   public class DispatchLoading : AuditableEntity
    {
        public DispatchLoading()
        {
            Deliveries = new List<DisptachDelivery>();
        }

        public long Id { get; set; }

        public string ProductDesc { get; set; }

        public string LoadCode { get; set; }

        public double GrossQnt { get; set; }


        public List<DisptachDelivery> Deliveries { get; private set; }
    }
}
