using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Vendors;
using System;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Payables
{
    public  class Invoice : AuditableEntity
    {

        public long Id { get; set; }
        public Vendor Vendor { get; set; }
        public string InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Taxes { get; set; }
        public double Total { get; set; }
        public string Notes { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.RECEVIED;


        private List<InvoiceItem> _invoiceItem;

        public Invoice()
        {
            _invoiceItem = new List<InvoiceItem>();
        }

        public virtual IEnumerable<InvoiceItem> InvoiceItems { get { return _invoiceItem; } }

    }

    public enum InvoiceStatus
    {
        RECEVIED,
        APPROVED,
        PAID
    }
}
