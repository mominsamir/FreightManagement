﻿using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Application.Customers.Queries.GetCustomerById
{
    public class CustomerDto : IMapFrom<CustomerDto>
    {
        public long Id { get;  }
        public string Name { get; }
        public string Email { get;  }
        public Address BillingAddress { get; }
        public bool IsActive { get; }
        public IEnumerable<LocationDto> Locations { get; }

        public CustomerDto() { }
        public CustomerDto(long id, 
            string name,
            string email, 
            Address billingAddress, 
            bool isActive,
            IEnumerable<Location> locations)
        {
            Id = id;
            Name = name;
            Email = email;
            BillingAddress = billingAddress;
            IsActive = isActive;
            Locations = locations
                .Select(l => new LocationDto(l.Id, l.Name, l.Email.Value, l.IsActive, l.DeliveryAddress))
                .ToList();
        }
    }

    public class LocationDto
    {
        public LocationDto(long id, string name, string email, bool isActive, Address deliveryAddress)
        {
            Id = id;
            Name = name;
            Email = email;
            IsActive = isActive;
            DeliveryAddress = deliveryAddress;
        }

        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
        public bool IsActive { get; }
        public Address DeliveryAddress { get; }
    }  
}
