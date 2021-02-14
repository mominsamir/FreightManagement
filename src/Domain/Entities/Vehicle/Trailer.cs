using FreightManagement.Domain.Common;
using System;

namespace FreightManagement.Domain.Entities.Vehicle
{
   public class Trailer : AuditableEntity
    {
        public long Id { get; set; }

        public string NumberPlate { get; set; }
    }
}
