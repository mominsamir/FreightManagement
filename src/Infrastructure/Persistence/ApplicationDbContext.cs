using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Common;
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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        private readonly ILogger _logger;

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime,
            ILogger<ApplicationDbContext> logger) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
            _logger = logger;
        }

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.UseSerialColumns();
            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
