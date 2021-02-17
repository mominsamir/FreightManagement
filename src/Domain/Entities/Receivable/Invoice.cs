using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;
using System;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Receivable
{
    public class Invoice : AuditableEntity
    {

        public long Id { get; set; }
        public Customer Customer { get; set; }
        public string InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Taxes { get; set; }
        public double Total { get; set; }
        public string Notes { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.RECEVIED;

        private List<InvoiceItem> _invoiceItem;

        public IEnumerable<InvoiceItem> InvoiceItems { get { return _invoiceItem; } }

        public Invoice()
        {
            _invoiceItem = new List<InvoiceItem>();
        }
    }

    public enum InvoiceStatus
    {
        RECEVIED,
        APPROVED,
        PAID
    }
}
