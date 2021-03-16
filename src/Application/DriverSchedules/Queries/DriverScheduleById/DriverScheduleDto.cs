using FreightManagement.Application.Common.Mappings;
using FreightManagement.Application.Trailers.Queries.GetTrailer;
using FreightManagement.Application.Trucks.Queries;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using FreightManagement.Domain.Entities.DriversSchedules;
using System;
using System.Collections.Generic;

namespace FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById
{
    public class DriverScheduleDto :IMapFrom<DriverSchedule>
    {
        public long Id { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get;  }
        public UserDto Driver { get;  }
        public TrailerListDto Trailer { get;  }
        public TruckListDto Truck { get;  }
        public string Status { get; }
        public IEnumerable<DriverCheckListDto> CheckList { get; } 

        public DriverScheduleDto()
        {
            CheckList = new List<DriverCheckListDto>();
        }
        public DriverScheduleDto(
            long id, 
            DateTime startTime, 
            DateTime endTime,
            UserDto driver,
            TrailerListDto trailer,
            TruckListDto truck, 
            string status, 
            IEnumerable<DriverCheckListDto> checkList
        ):this(){
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Driver = driver;
            Trailer = trailer;
            Truck = truck;
            Status = status;
            CheckList = checkList;
        }
    }

    public class DriverCheckListDto {
        public long Id { get; }
        public string Note { get; }
        public bool IsChecked { get; }

        public DriverCheckListDto(long id, string note, bool isChecked)
        {
            Id = id;
            Note = note;
            IsChecked = isChecked;
        }
    }

    public class DriverScheduleListDto : IMapFrom<DriverSchedule>
    {
        public long Id { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public UserDto Driver { get; }
        public TrailerListDto Trailer { get; }
        public TruckListDto Truck { get; }
        public string Status { get; }

        public DriverScheduleListDto(
            long id,
            DateTime startTime,
            DateTime endTime,
            UserDto driver,
            TrailerListDto trailer,
            TruckListDto truck,
            string status
        )
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Driver = driver;
            Trailer = trailer;
            Truck = truck;
            Status = status;
        }
    }

}
