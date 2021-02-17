using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Users;
using FreightManagement.Domain.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Domain.Entities.DriverSchedule
{
    public class ScheduleDriverTruckTrailer : AuditableEntity
    {

        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public User Driver { get; set; }
        public Trailer Trailer { get; set; }
        public Truck Truck { get; set; }

        public List<DriverCheckList> _checkList;

        public IEnumerable<DriverCheckList> CheckList { get { return _checkList; } }

        public void AddCheckListNotes(List<string> checkLists)
        {
            _checkList.AddRange( checkLists.Select(s => new DriverCheckList(s, this)).ToList());
        }

        public void ToogleCheckListItem(long itemUid)
        {
            _checkList.Where(i => i.Id == itemUid).Select(x=> x.IsChecked ?  false: true);
        }




    }
}
