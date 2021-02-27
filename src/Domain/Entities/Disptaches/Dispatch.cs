using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.DriversSchedules;
using FreightManagement.Domain.Entities.Orders;
using FreightManagement.Domain.Entities.StorageRack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Domain.Entities.Disptaches
{
  public class Dispatch : AuditableEntity
    {
        public long Id { get; set; }
        public DriverSchedule DriverSchedule { get; set; }
        public DateTime DispatchDateTime { get; set; }
        public DispatchStatus Status { get; private set; }
        public double Miles { get; set; }
        public DateTime DispatchStartTime { get; private set; }
        public DateTime DispatchEndTime { get; private set; }
        public DateTime RackArrivalTime { get; private set; }
        public DateTime RackLeftOnTime { get; private set; }
        public DateTime LoadingStartTime { get; private set; }
        public DateTime LoadingEndTime { get; private set; }


        private readonly List<DispatchLoading> _dispatchLoading;

        public List<DispatchLoading> DispatchLoading { get { return _dispatchLoading; } }

        public Dispatch()
        {
            _dispatchLoading = new List<DispatchLoading>();
            Status = DispatchStatus.RECEIVED;
        }

        public Dispatch(
            DriverSchedule driverSchedule, 
            DateTime dispatchDateTime,
            Rack rack,
            Order order 
            )
        {
            DriverSchedule = driverSchedule;
            DispatchDateTime = dispatchDateTime;
            AddDispatchLoading(order, rack);
        }

        public void AddParitalDelivery(long loadingId, Location location, long unloadedQnt)
        {
            var dispatchLoading = _dispatchLoading.First(l => l.Id == loadingId);
            if(dispatchLoading is not null)
            {
                dispatchLoading.AddParitalDelivery(location, unloadedQnt);
            }
        }

        public void AddDispatchLoading(Order order, Rack rack)
        {
            foreach(var item in order.OrderItems)
            {
                var loading = new DispatchLoading(item, rack, this);
                loading.CreateInitalDelivery();
                _dispatchLoading.Add(loading);
            }
        }

        public void UpdateDispatchItem(long dispatchItemId, string bol, double loadedQnt)
        {
            var dispatchLoading =  _dispatchLoading.First(l => l.Id == dispatchItemId);
            dispatchLoading.LoadedQuantity = loadedQnt;
            dispatchLoading.BillOfLoading= bol;
        }

        public void UpdateDeliveryItem(long dispatchItemId, long deliveryItemId, double unloadedQnt)
        {
            var dispatchLoading = _dispatchLoading.First(l => l.Id == dispatchItemId);
            dispatchLoading.UpdateDelivery(deliveryItemId, unloadedQnt);    
        }

        public void MarkLoaded()
        {
            Status = DispatchStatus.LOADED;
            LoadingStartTime = DateTime.Now;
        }

        public void MarkUnLoaded()
        {
            Status = DispatchStatus.UNLOADED;
            LoadingEndTime = DateTime.Now;
        }

        public void MarkDelivered()
        {
            Status = DispatchStatus.DELIVERED;
            DispatchEndTime = DateTime.Now;
        }

        public void MarkCancelled()
        {
            Status = DispatchStatus.CANCELLED;
        }

        public void CreateDeliveries()
        {
            Status = DispatchStatus.CANCELLED;
            DispatchStartTime = DateTime.Now;
        }

    }

    public enum DispatchStatus
    {
        RECEIVED,
        LOADED,
        UNLOADED,
        DELIVERED,
        CANCELLED
    }

}
