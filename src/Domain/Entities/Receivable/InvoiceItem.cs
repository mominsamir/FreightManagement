
using FreightManagement.Domain.Common;

namespace FreightManagement.Domain.Entities.Receivable
{
    public  class InvoiceItem : AuditableEntity
    {
        public long Id { get; set; }

        public Invoice Invoice { get; set; }

        public string Description { get; set; }

        public double Quantity { get; set; }

        public double Rate { get; set; }

        public double SubTotal { get; set; }

        public double Taxes { get; set; }

        public double LineTotal { get; set; }

    }
}
