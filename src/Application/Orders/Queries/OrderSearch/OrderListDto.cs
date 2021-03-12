using FreightManagement.Application.Customers.Queries.GetCustomerById;
using System;

namespace FreightManagement.Application.Orders.Queries.OrderSearch
{
    public class OrderListDto
    {

        public OrderListDto(long id, CustomerDto customer, DateTime orderDate, DateTime shipDate, string status, double totalQnt)
        {
            Id = id;
            Customer = customer;
            OrderDate = orderDate;
            ShipDate = shipDate;
            Status = status;
            TotalQnt = totalQnt;
        }

        public long Id { get; }
        public CustomerDto Customer { get; }
        public DateTime OrderDate { get; }
        public DateTime ShipDate { get; }
        public string Status { get; }
        public double TotalQnt { get; }


    }
}
