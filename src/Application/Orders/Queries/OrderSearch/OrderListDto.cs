using FreightManagement.Application.Customers.Queries.GetCustomerById;
using FreightManagement.Domain.Entities.Orders;
using System;

namespace FreightManagement.Application.Orders.Queries.OrderSearch
{
    public class OrderListDto
    {

        public OrderListDto(long id, CustomerDto customer, DateTime orderDate, DateTime shipDate, OrderStatus status, double totalQnt)
        {
            Id = id;
            Customer = customer;
            OrderDate = orderDate;
            ShipDate = shipDate;
            Status = status;
            StatusLabel = status.ToString();
            TotalQnt = totalQnt;
        }

        public long Id { get; }
        public CustomerDto Customer { get; }
        public DateTime OrderDate { get; }
        public DateTime ShipDate { get; }
        public OrderStatus Status { get; }
        public string StatusLabel { get; }
        public double TotalQnt { get; }


    }
}
