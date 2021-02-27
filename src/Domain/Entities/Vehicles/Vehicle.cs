using FreightManagement.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Domain.Entities.Vehicles
{
    // TODO: I never use inheritance get my parents fields 
    // TODO: Inheritance should be use to get parents behaviour 
    public class Vehicle : AuditableEntity
    {
        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public VehicleStatus Status { get; private set; }

        private readonly List<VehicleCheckList> _checkLists;
        public IEnumerable<VehicleCheckList> CheckLists { get { return _checkLists; } }

        public Vehicle()
        {
            _checkLists = new List<VehicleCheckList>();
        }

        public void MakeActive()
        {
            Status = VehicleStatus.ACTIVE;
        }

        public void MakeOutOfService()
        {
            Status = VehicleStatus.OUT_OF_SERVICE;
        }

        public void MakeUnderMaintanace()
        {
            Status = VehicleStatus.UNDER_MAINTNCE;
        }

        public void AddNewCheckListItem(string checkListNote)
        {
            _checkLists.Add(new VehicleCheckList { IsActive = true, Note = checkListNote });
        }

        public void UpdateCheckListItem(long Id, string checkListNote, bool IsActive)
        {
           var checkList = _checkLists.FirstOrDefault(l => l.Id == Id);
            checkList.Note = checkListNote;
            checkList.IsActive = IsActive;
        }

        public IEnumerable<VehicleCheckList> GetActiveCheckList()
        {
            return _checkLists.Where(l=> l.IsActive).ToList();
        }

    }

    public enum VehicleStatus
    {
        ACTIVE,
        UNDER_MAINTNCE,
        OUT_OF_SERVICE
    }
}
