﻿using FreightManagement.Application.Common.Mappings;
using FreightManagement.Application.Trailers.Queries.GetRacks;
using FreightManagement.Application.Trucks.Queries;
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
        public long Driver { get;  }
        public TrailerDto Trailer { get;  }
        public TruckDto Truck { get;  }
        public DriverScheduleStatus Status { get; }
        public IEnumerable<DriverCheckListDto> CheckList { get; }
        public DriverScheduleDto() { }
        public DriverScheduleDto(
            long id, 
            DateTime startTime, 
            DateTime endTime, 
            long driver, 
            TrailerDto trailer, 
            TruckDto truck, 
            DriverScheduleStatus status, 
            IEnumerable<DriverCheckListDto> checkList
        ){
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
}
