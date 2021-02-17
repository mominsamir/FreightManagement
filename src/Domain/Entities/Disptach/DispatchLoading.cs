using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Products;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Disptach
{
   public class DispatchLoading : AuditableEntity
    {

        public long Id { get; set; }

        public FuelProduct FuelProduct { get; set; }

        public string LoadCode { get; set; }

        public double GrossQnt { get; set; }

        public List<DisptachDelivery> _deliveries { get; private set; }

        public IEnumerable<DisptachDelivery> Deliveries { get { return _deliveries; } }

        public DispatchLoading()
        {
            _deliveries = new List<DisptachDelivery>();
        }

    }
}
