using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Orders;
using FreightManagement.Domain.Entities.StorageRack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Domain.Entities.Disptaches
{
   public class DispatchLoading : AuditableEntity
    {
        public long Id { get; set; }
        public Rack Rack { get; set; }
        public OrderItem OrderItem { get; set; }
        public string BillOfLoading { get; set; }
        public double LoadedQuantity { get; set; }
        public Dispatch Dispatch { get; protected set; }

        private List<DisptachDelivery> _deliveries;
        public IEnumerable<DisptachDelivery> Deliveries { get { return _deliveries; } }

        public DispatchLoading()
        {
            _deliveries = new List<DisptachDelivery>();
        }

        public DispatchLoading(OrderItem orderItem, Rack rack, Dispatch dispatch): this()
        {
            OrderItem = orderItem;
            Rack = rack;
            Dispatch = dispatch;
        }

        public void CreateInitalDelivery()
        {
            _deliveries.Add(
                new DisptachDelivery
                {
                    DeliveryType = DeliveryType.NIL,
                    DeliveredQnt = 0.0,
                    DispatchLoading = this,
                    Location = OrderItem.Location
                });
        }

        public void UpdateDelivery(long deliveryId, double deliveredQnt)
        {
            var deliveryItem =  _deliveries.First(i => i.Id == deliveryId);
            deliveryItem.UpdateDeliveredQnt(deliveredQnt, GetDeliveredQnt());
        }

        private double GetDeliveredQnt()
        {
            return _deliveries.Sum(i => i.DeliveredQnt);
        }

        internal void AddParitalDelivery(Location location, long unloadedQnt)
        {
            _deliveries.Add(
                new DisptachDelivery
                    {
                        DeliveryType = DeliveryType.SPLIT,
                        DeliveredQnt = unloadedQnt,
                        DispatchLoading = this,
                        Location = location
                    }
            );
        }
    }
}
