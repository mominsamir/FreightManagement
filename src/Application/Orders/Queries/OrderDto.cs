using FreightManagement.Application.Customers.Queries.GetCustomerById;
using FreightManagement.Application.Products.Queries.GetFuelProduct;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Orders;
using FreightManagement.Domain.Entities.Products;
using System;
using System.Collections.Generic;


namespace FreightManagement.Application.Orders.Queries
{
    public class OrderDto
    {
        public long Id { get; }
        public CustomerDto Customer { get; }
        public DateTime OrderDate { get;}
        public DateTime ShipDate { get; }
        public OrderStatus Status { get; }
        public string StatusLabel { get; }
        public IEnumerable<OrderItemDto> OrderItems { get; }

        public OrderDto() { }
        public OrderDto(long id, Customer customer, DateTime orderDate, DateTime shipDate, OrderStatus status, IEnumerable<OrderItemDto> orderItems)
        {
            Id = id;
            Customer = new CustomerDto(
                    customer.Id,
                    customer.Name,
                    customer.Email.Value,
                    customer.BillingAddress,
                    customer.IsActive,
                    customer.Locations
                );
            OrderDate = orderDate;
            ShipDate = shipDate;
            Status = status;
            StatusLabel = status.ToString();
            OrderItems = orderItems;
        }
    }

    public class OrderItemDto
    {
        public OrderItemDto(long id, Location location, FuelProduct fuelProduct, double quantity, string loadCode)
        {
            Id = id;
            Location = new LocationDto(
                    location.Id,
                    location.Name,
                    location.Email.Value,
                    location.IsActive,
                    location.DeliveryAddress
                );
            FuelProduct = new FuelProductDto
            {
                Id = fuelProduct.Id,
                Name =  fuelProduct.Name,
                Grade = fuelProduct.Grade,
                IsActive = fuelProduct.IsActive,
                UOM = fuelProduct.UOM
            };
            Quantity = quantity;
            LoadCode = loadCode;
        }

        public long Id { get;  }
        public LocationDto Location { get;  }
        public FuelProductDto FuelProduct { get; }
        public double Quantity { get; }
        public string LoadCode { get;  }
    }
}
