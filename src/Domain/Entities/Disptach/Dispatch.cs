using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Users;
using System;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Disptach
{
  public class Dispatch : AuditableEntity
    {

        public Dispatch()
        {
            Loading = new List<DispatchLoading>();
        }

        public long Id { get; set; }

        public User Driver { get; set; }
        public User Dispatcher { get; set; }

        public DateTime DispatchDate { get; set; }

        public DispatchStatus Status { get; set; }


        public List<DispatchLoading> Loading { get; private set; }
    }

    public enum DispatchStatus
    {
        RECEIVED = 0,
        SHIPPED = 1,
        DEILVERED = 2,
    }

}
