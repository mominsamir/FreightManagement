using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Users;
using FreightManagement.Domain.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Domain.Entities.DriversSchedules
{
    public class DriverSchedule : AuditableEntity
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public User Driver { get; set; }
        public Trailer Trailer { get; set; }
        public Truck Truck { get; set; }
        public DriverScheduleStatus Status { get; private set; }

        private readonly List<DriverCheckList> _checkList;
        public IEnumerable<DriverCheckList> CheckList { get { return _checkList; } }

        public DriverSchedule()
        {
            _checkList = new List<DriverCheckList>();
            Status = DriverScheduleStatus.SCHEDULE_CREATED;
        }

        public void AddCheckListNotes(IEnumerable<VehicleCheckList> checkLists)
        {
            _checkList.AddRange( checkLists.Select(s => new DriverCheckList(s, this)).ToList());
        }

        public void ToggleCheckListItem(long itemUid, bool IsChecked)
        {
            var checkListItem = _checkList.FirstOrDefault(c => c.Id == itemUid);
            
            if(checkListItem != null)
            {
                if(IsChecked) 
                    checkListItem.Check();
                else
                    checkListItem.Uncheck();
            }
            TryCompleteCheckList();
        }
        
        public void TryCompleteCheckList()
        {
            var unCheckeItems = _checkList.FindAll(s => !s.IsItemChecked());

            if (!unCheckeItems.Any())
            {
                Status = DriverScheduleStatus.CHECKLIST_COMPLETE;
            }
        }

        public void TryCompletedSchedule()
        {
            if (Status == DriverScheduleStatus.CHECKLIST_COMPLETE)
            {
                Status = DriverScheduleStatus.SCHEDULE_COMPLETED;
            }
        }

        public void CancelSchedule()
        {
            if (Status != DriverScheduleStatus.SCHEDULE_COMPLETED)
            {
                Status = DriverScheduleStatus.SCHEDULE_CANCELLED;
            }
        }

    }

    public enum DriverScheduleStatus
    {
        SCHEDULE_CREATED,
        CHECKLIST_COMPLETE,
        SCHEDULE_COMPLETED,
        SCHEDULE_CANCELLED
    }
}
