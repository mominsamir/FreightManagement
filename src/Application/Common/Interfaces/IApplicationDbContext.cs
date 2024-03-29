﻿using FreightManagement.Domain.Entities;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Disptaches;
using FreightManagement.Domain.Entities.DriversSchedules;
using FreightManagement.Domain.Entities.Orders;
using FreightManagement.Domain.Entities.Payables;
using FreightManagement.Domain.Entities.Products;
using FreightManagement.Domain.Entities.StorageRack;
using FreightManagement.Domain.Entities.Users;
using FreightManagement.Domain.Entities.Vehicles;
using FreightManagement.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationTank> LocationTanks { get; set; }

        public DbSet<Truck> Trucks { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<DriverSchedule> DriverScheduleLists { get; set; }

        public DbSet<DriverCheckList> DriverCheckLists { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Dispatch> Dispatches { get; set; }

        public DbSet<DispatchLoading> DispatchLoadings { get; set; }

        public DbSet<DisptachDelivery> DisptachDeliveries { get; set; }

        public DbSet<Invoice> VendorInvoices { get; set; }

        public DbSet<InvoiceItem> VendorInvoiceItems { get; set; }

        public DbSet<Domain.Entities.Receivable.Invoice> CustomerInvoices { get; set; }

        public DbSet<Domain.Entities.Receivable.InvoiceItem> CustomerInvoiceItems { get; set; }
        public DbSet<Rack> Racks { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<FuelProduct> FuelProducts { get; set; }
        public DbSet<User> AllUsers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
