using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Vehicles;

namespace FreightManagement.Domain.Entities.DriversSchedules
{
    public class DriverCheckList : AuditableEntity
    {
        public long Id { get; set; }

        public VehicleCheckList CheckList { get; set; }

        public bool IsChecked { get; private set; }

        public DriverSchedule DriverSchedule { get; set; }

        public DriverCheckList() { }
        public DriverCheckList(VehicleCheckList checkList, DriverSchedule scheduleDriverTruckTrailer)
        {
            CheckList = checkList;
            DriverSchedule = scheduleDriverTruckTrailer;
        }

        public void Check()
        {
            IsChecked = true;
        }

        public void Uncheck()
        {
            IsChecked = false;
        }

        public bool IsItemChecked()
        {
            return IsChecked ;
        }

    }

}
