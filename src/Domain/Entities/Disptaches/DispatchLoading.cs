using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Products;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Disptaches
{
   public class DispatchLoading : AuditableEntity
    {

        public long Id { get; set; }

        public FuelProduct FuelProduct { get; set; }

        public string LoadCode { get; set; }

        public string BillOfLoading { get; set; }

        public double GrossQnt { get; set; }

        private List<DisptachDelivery> _deliveries;

        public Dispatch Dispatch { get; set; }

        public IEnumerable<DisptachDelivery> Deliveries { get { return _deliveries; } }

        public DispatchLoading()
        {
            _deliveries = new List<DisptachDelivery>();
        }

    }
}
