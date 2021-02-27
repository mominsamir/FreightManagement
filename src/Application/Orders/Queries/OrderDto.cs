using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Orders;
using FreightManagement.Domain.Entities.Products;
using System;
using System.Collections.Generic;


namespace FreightManagement.Application.Orders.Queries
{
    public class OrderDto : IMapFrom<OrderDto>
    {
        public long Id { get; }
        public Customer Customer { get; }
        public DateTime OrderDate { get;}
        public DateTime ShipDate { get; }
        public OrderStatus Status { get; }
        public IEnumerable<OrderItemDto> OrderItems { get; }

        public OrderDto() { }
        public OrderDto(long id, Customer customer, DateTime orderDate, DateTime shipDate, OrderStatus status, IEnumerable<OrderItemDto> orderItems)
        {
            Id = id;
            Customer = customer;
            OrderDate = orderDate;
            ShipDate = shipDate;
            Status = status;
            OrderItems = orderItems;
        }
    }

    public class OrderItemDto
    {
        public OrderItemDto(long id, Location location, FuelProduct fuelProduct, double quantity, string loadCode)
        {
            Id = id;
            Location = location;
            FuelProduct = fuelProduct;
            Quantity = quantity;
            LoadCode = loadCode;
        }

        public long Id { get;  }
        public Location Location { get;  }
        public FuelProduct FuelProduct { get; }
        public double Quantity { get; }
        public string LoadCode { get;  }
    }
}
