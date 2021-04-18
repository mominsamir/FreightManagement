using FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById;
using FreightManagement.Application.Orders.Queries;
using FreightManagement.Application.StorageRacks.Queries.GetRacks;
using FreightManagement.Domain.Entities.Disptaches;
using System;
using System.Collections.Generic;

namespace FreightManagement.Application.Dispatches.Queries.QueryDispatch
{
    public class DispatchDto
    {
        public DispatchDto()
        {
            DispatchLoadings = new List<DispatchLoadingDto>();
        }
        public DispatchDto(
            long id, 
            DriverScheduleDto driverSchedule, 
            DateTime dispatchDateTime, 
            DispatchStatus status, 
            double miles, 
            DateTime dispatchStartTime, 
            DateTime dispatchEndTime, 
            DateTime rackArrivalTime, 
            DateTime rackLeftOnTime, 
            DateTime loadingStartTime, 
            DateTime loadingEndTime, 
            List<DispatchLoadingDto> dispatchLoadings): this()
        {
            Id = id;
            DriverSchedule = driverSchedule;
            DispatchDateTime = dispatchDateTime;
            Status = status;
            StatusLabel = status.ToString();
            Miles = miles;
            DispatchStartTime = dispatchStartTime;
            DispatchEndTime = dispatchEndTime;
            RackArrivalTime = rackArrivalTime;
            RackLeftOnTime = rackLeftOnTime;
            LoadingStartTime = loadingStartTime;
            LoadingEndTime = loadingEndTime;
            DispatchLoadings = dispatchLoadings;
        }

        public DispatchDto(
            long id,
            DriverScheduleDto driverSchedule,
            DateTime dispatchDateTime,
            DispatchStatus status,
            double miles,
            DateTime dispatchStartTime,
            DateTime dispatchEndTime,
            DateTime rackArrivalTime,
            DateTime rackLeftOnTime,
            DateTime loadingStartTime,
            DateTime loadingEndTime) : this()
        {
            Id = id;
            DriverSchedule = driverSchedule;
            DispatchDateTime = dispatchDateTime;
            Status = status;
            StatusLabel = status.ToString();
            Miles = miles;
            DispatchStartTime = dispatchStartTime;
            DispatchEndTime = dispatchEndTime;
            RackArrivalTime = rackArrivalTime;
            RackLeftOnTime = rackLeftOnTime;
            LoadingStartTime = loadingStartTime;
            LoadingEndTime = loadingEndTime;

        }


        public long Id { get; }
        public DriverScheduleDto DriverSchedule { get; }
        public DateTime DispatchDateTime { get; }
        public DispatchStatus Status { get; }
        public string StatusLabel { get; }
        public double Miles { get; }
        public DateTime DispatchStartTime { get; }
        public DateTime DispatchEndTime { get; }
        public DateTime RackArrivalTime { get; }
        public DateTime RackLeftOnTime { get; }
        public DateTime LoadingStartTime { get; }
        public DateTime LoadingEndTime { get; }
        public List<DispatchLoadingDto> DispatchLoadings { get; }

    }

    public class DispatchLoadingDto
    {

        public DispatchLoadingDto(long id, RackDto rack, OrderItemDto orderItem, string billOfLoading, double loadedQuantity)
        {
            Id = id;
            Rack = rack;
            OrderItem = orderItem;
            BillOfLoading = billOfLoading;
            LoadedQuantity = loadedQuantity;
        }

        public long Id { get; }
        public RackDto Rack { get; }
        public OrderItemDto OrderItem { get; }
        public string BillOfLoading { get; }
        public double LoadedQuantity { get; }
    }
}
