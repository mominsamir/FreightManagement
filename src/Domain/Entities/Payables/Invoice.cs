using FreightManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreightManagement.Domain.Entities.Payables
{
    public  class Invoice : AuditableEntity
    {
        public long Id { get; set; }

    }
}
