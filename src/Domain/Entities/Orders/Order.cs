using FreightManagement.Domain.Common;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Domain.Entities.Orders
{
    public class Order: AuditableEntity
    {
        public long Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate  { get; set; }
        public DateTime ShipDate { get; set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Received;

        private List<OrderItem> _orderItems;

        public IEnumerable<OrderItem> OrderItems { get { return _orderItems; } }

        public Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(Customer customer, DateTime orderdate, DateTime shipDate)
        {
            Customer = customer;
            OrderDate = orderdate;
            ShipDate = shipDate;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(FuelProduct product,Location location,  double qnt, string loadCode)
        {
            _orderItems.Add ( new OrderItem(product, location, qnt, loadCode, this));
        }

        public void RemoveOrderItem(long orderItemId)
        {
            var index =  _orderItems.FindIndex(i => i.Id == orderItemId);
            _orderItems.RemoveAt(index);
        }

        public void MarkShipped()
        {
            Status = OrderStatus.Shipped;
        }

        public void MarkDelivered()
        {
            Status = OrderStatus.Delivered;
        }

        public void MarkCancelled()
        {
            Status = OrderStatus.Cancelled;
        }

        public double TotalQuantity()
        {
            return _orderItems.Sum(i => i.Quantituy);
        }

    }

    public enum OrderStatus
    {
        Received = 0,
        Shipped = 1,
        Delivered = 2,
        Cancelled = 3,
    }
}
