using FreightManagement.Domain.Common;

namespace FreightManagement.Domain.Entities.DriversSchedule
{
    public class DriverCheckList : AuditableEntity
    {
        public long Id { get; set; }

        public string CheckListItem { get; set; }

        public bool IsChecked { get; set; }

        public ScheduleDriverTruckTrailer ScheduleDriverTruckTrailer { get; set; }

        public DriverCheckList() { }
        public DriverCheckList(string note, ScheduleDriverTruckTrailer scheduleDriverTruckTrailer)
        {
            CheckListItem = note;
            ScheduleDriverTruckTrailer = scheduleDriverTruckTrailer;
        }


    }
}
