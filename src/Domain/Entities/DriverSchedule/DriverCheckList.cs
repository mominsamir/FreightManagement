using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Disptach;

namespace FreightManagement.Domain.Entities.DriverSchedule
{
    public class DriverCheckList : AuditableEntity
    {
        public long Id { get; set; }

        public string CheckListItem { get; set; }

        public bool IsChecked { get; set; }

        public ScheduleDriverTruckTrailer ScheduleDriverTruckTrailer { get; set; }

        public DriverCheckList(string note, ScheduleDriverTruckTrailer scheduleDriverTruckTrailer)
        {
            CheckListItem = note;
            ScheduleDriverTruckTrailer = scheduleDriverTruckTrailer;
        }


    }
}
