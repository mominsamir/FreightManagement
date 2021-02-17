using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.DriversSchedule;
using System;
using System.Collections.Generic;

namespace FreightManagement.Domain.Entities.Disptaches
{
  public class Dispatch : AuditableEntity
    {

        public long Id { get; set; }

        public ScheduleDriverTruckTrailer ScheduleDriverTruckTrailer { get; set; }
        
        public DateTime DispatchDateTime { get; set; }

        public DispatchStatus Status { get; set; } = DispatchStatus.RECEIVED;

        public DateTime DispatchStartTime { get; set; }
        public DateTime DispatchEndTime { get; set; }
        public DateTime RackArrivalTime { get; set; }
        public DateTime RackLeftOnTime { get; set; }
        public DateTime LoadingStartTime { get; set; }
        public DateTime LoadingEndTime { get; set; }


        private List<DispatchLoading> _dispatchLoading;

        public List<DispatchLoading> DispatchLoading { get { return _dispatchLoading; } }

        public Dispatch()
        {
            _dispatchLoading = new List<DispatchLoading>();
        }

    }

    public enum DispatchStatus
    {
        RECEIVED,
        SHIPPED,
        DEILVERED
    }

}
