using FreightManagement.Application.Common.Security;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.DriversSchedules;
using FreightManagement.Domain.Entities.Orders;
using FreightManagement.Domain.Entities.Products;
using FreightManagement.Domain.Entities.StorageRack;
using FreightManagement.Domain.Entities.Users;
using FreightManagement.Domain.Entities.Vehicles;
using FreightManagement.Domain.Entities.Vendors;
using FreightManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreightManagement.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary


            var user = new User
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                Password = PasswordEncoder.ConvertPasswordToHash("Welcome123"),
                Role = Role.ADMIN,
                CreatedBy = "0",
                IsActive = true
            };

            var user1 = new User
            {
                FirstName = "dispatch",
                LastName = "dispatch",
                Email = "dispatch@admin.com",
                Password = PasswordEncoder.ConvertPasswordToHash("Welcome123"),
                Role = Role.DISPATCHER,
                CreatedBy = "0",
                IsActive = true
            };

            var user2 = new User
            {
                FirstName = "Driver",
                LastName = "Driver",
                Email = "Driver@admin.com",
                Password = PasswordEncoder.ConvertPasswordToHash("Welcome123"),
                Role = Role.DRIVER,
                CreatedBy = "0",
                IsActive = true
            };

            var user3 = new User
            {
                FirstName = "Driver1",
                LastName = "Driver1",
                Email = "Driver1@admin.com",
                Password = PasswordEncoder.ConvertPasswordToHash("Welcome123"),
                Role = Role.DRIVER,
                CreatedBy = "0",
                IsActive = true
            };

            var user4 = new User
            {
                FirstName = "Driver3",
                LastName = "Driver3",
                Email = "Driver3@admin.com",
                Password = PasswordEncoder.ConvertPasswordToHash("Welcome123"),
                Role = Role.DRIVER,
                CreatedBy = "0",
                IsActive = true
            };

            if (!context.AllUsers.Any())
            {

                await context.AllUsers.AddRangeAsync(user, user1, user2, user3, user4);
                await context.SaveChangesAsync();
            }

            if (!context.FuelProducts.Any())
            {
                context.FuelProducts.Add(new FuelProduct
                {
                    Name = FuelGrade.REGULAR.ToString(),
                    Grade = FuelGrade.REGULAR,
                    UOM = UnitOfMeasure.GALLON,
                    CreatedBy = user.Id.ToString(),
                });
                context.FuelProducts.Add(new FuelProduct
                {
                    Name = FuelGrade.PLUS.ToString(),
                    Grade = FuelGrade.PLUS,
                    UOM = UnitOfMeasure.GALLON,
                    CreatedBy = user.Id.ToString(),
                });
                context.FuelProducts.Add(new FuelProduct
                {
                    Name = FuelGrade.SUPER.ToString(),
                    Grade = FuelGrade.SUPER,
                    UOM = UnitOfMeasure.GALLON,
                    CreatedBy = user.Id.ToString(),
                });
                context.FuelProducts.Add(new FuelProduct
                {
                    Name = "DIESEL CLR",
                    Grade = FuelGrade.DIESEL_CLR,
                    UOM = UnitOfMeasure.GALLON,
                    CreatedBy = user.Id.ToString(),
                });
                context.FuelProducts.Add(new FuelProduct
                {
                    Name = "DIESEL DYD",
                    Grade = FuelGrade.DIESEL_DYD,
                    UOM = UnitOfMeasure.GALLON,
                    CreatedBy = user.Id.ToString(),
                });
                await context.SaveChangesAsync();
            }

            if (!context.Racks.Any())
            {
                context.Racks.Add(new Rack
                {
                    Name = "KINDER MORGAN",
                    IRSCode = "TX-00-123-222",
                    IsActive = true,
                    Address = new Address("50 tes street", "Houston", "TX", "USA", "00120"),
                    CreatedBy = user.Id.ToString(),
                });
                context.Racks.Add(new Rack
                {
                    Name = "MOTIVA",
                    IRSCode = "TX-00-123-223",
                    IsActive = true,
                    Address = new Address("50 tes street", "Houston", "TX", "USA", "00120"),
                    CreatedBy = user.Id.ToString(),
                });
                await context.SaveChangesAsync();
            }

            if (!context.Trucks.Any())
            {
                var checkListItem = new List<string> {
                    "Truck Check  List Item 1", "Truck Check List Item 2" ,"Truck Check List Item 3" , "Truck Check List Item 4"
                };

                var truck = new Truck
                {
                    NumberPlate = "TRUCK1",
                    VIN = "TX00123222TX00123222",
                    NextMaintanceDate = DateTime.Now,
                    CreatedBy = user.Id.ToString(),
                };

                checkListItem.ForEach(s => truck.AddNewCheckListItem(s));
                context.Trucks.Add(truck);
                var truck2 = new Truck
                {
                    NumberPlate = "TRUCK2",
                    VIN = "TX00123222TX00123223",
                    NextMaintanceDate = DateTime.Now,
                    CreatedBy = user.Id.ToString()
                };
                checkListItem.ForEach(s => truck2.AddNewCheckListItem(s));
                context.Trucks.Add(truck2);
                await context.SaveChangesAsync();
            }

            if (!context.Trailers.Any())
            {
                var checkListItem = new List<string> {
                    "Trailer Check  List Item 1", "Trailer Check List Item 2" ,"Trailer Check List Item 3" , "Trailer Check List Item 4"
                };

                var trailer = new Trailer
                {
                    NumberPlate = "TRAILER1",
                    VIN = "TX00123222TX00123224",
                    Compartment = 5,
                    Capacity = 8000,
                    CreatedBy = user.Id.ToString(),
                };
                checkListItem.ForEach(s => trailer.AddNewCheckListItem(s));
                context.Trailers.Add(trailer);

                var trailer1 = new Trailer
                {
                    NumberPlate = "TRAILER2",
                    VIN = "TX00123222TX00123225",
                    Compartment = 5,
                    Capacity = 8500,
                    CreatedBy = user.Id.ToString(),
                };
                checkListItem.ForEach(s => trailer1.AddNewCheckListItem(s));
                context.Trailers.Add(trailer1);

                await context.SaveChangesAsync();
            }

            if (!context.Vendors.Any())
            {
                context.Vendors.AddRange(new Vendor
                {
                    Name = "MOTIVA",
                    Email = new Email("test@Motiva.com"),
                    IsActive = true,
                    Address = new Address("50 tes street", "Houston", "TX", "USA", "00120"),
                    CreatedBy = user.Id.ToString(),
                }, new Vendor
                {
                    Name = "Valero",
                    Email = new Email("test@Valero.com"),
                    IsActive = true,
                    Address = new Address("52 tes street", "Houston", "TX", "USA", "00120"),
                    CreatedBy = user.Id.ToString(),
                });
                await context.SaveChangesAsync();
            }

            if (!context.Locations.Any())
            {
                var loc = new Location
                {
                    Name = "Bass Mini Mart",
                    Email = new Email("miniMart@gmail.com"),
                    CreatedBy = user.Id.ToString(),
                    DeliveryAddress = new Address("51 tes street", "Houston", "TX", "USA", "00120"),
                };
                loc.AddNewTank("Tank1", FuelGrade.REGULAR, 5000);
                loc.AddNewTank("Tank2", FuelGrade.PLUS, 2000);
                loc.AddNewTank("Tank3", FuelGrade.SUPER, 1000);

                var loc1 = new Location
                {
                    Name = "Phillips Food mart",
                    Email = new Email("phllips@gmail.com"),
                    CreatedBy = user.Id.ToString(),
                    DeliveryAddress = new Address("52 tes street", "Houston", "TX", "USA", "00120"),
                };
                loc1.AddNewTank("Tank1", FuelGrade.REGULAR, 5000);
                loc1.AddNewTank("Tank2", FuelGrade.SUPER, 2000);
                loc1.AddNewTank("Tank3", FuelGrade.DIESEL_CLR, 1000);

                var loc2 = new Location
                {
                    Name = "FoodWay mart",
                    Email = new Email("foodWay@gmail.com"),
                    CreatedBy = user.Id.ToString(),
                    DeliveryAddress = new Address("53 tes street", "Houston", "TX", "USA", "00120"),
                };
                loc2.AddNewTank("Tank1", FuelGrade.REGULAR, 5000);
                loc2.AddNewTank("Tank3", FuelGrade.DIESEL_CLR, 4000);

                context.Locations.AddRange(loc, loc1, loc2);
                await context.SaveChangesAsync();

                if (!context.Customers.Any())
                {
                    var cus = new Customer
                    {
                        Name = "Potent Petrolium",
                        Email = new Email("pttt@gmail.com"),
                        CreatedBy = user.Id.ToString(),
                        BillingAddress = new Address("1010 street", "Houston", "TX", "USA", "00120"),
                    };
                    cus.AddLocation(loc);
                    cus.AddLocation(loc1);
                    cus.AddLocation(loc2);

                    var cus1 = new Customer
                    {
                        Name = "Campbell Oil",
                        Email = new Email("campbell@gmail.com"),
                        CreatedBy = user.Id.ToString(),
                        BillingAddress = new Address("2020 street", "Houston", "TX", "USA", "00120"),
                    };
                    cus1.AddLocation(loc);
                    cus1.AddLocation(loc2);

                    var cus2 = new Customer
                    {
                        Name = "Startex oil inc",
                        Email = new Email("star@gmail.com"),
                        CreatedBy = user.Id.ToString(),
                        BillingAddress = new Address("5030 street", "Houston", "TX", "USA", "00120"),
                    };
                    cus2.AddLocation(loc);

                    context.Customers.AddRange(cus, cus1, cus2);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.DriverScheduleLists.Any())
            {
                var drivers = context.AllUsers.Where(u => u.Role == Role.DRIVER);

                var trailers = context.Trailers.ToList();
                var trucks = context.Trucks.ToList();
                var schedules = new List<DriverSchedule>();
                var select = false;
                foreach (var d in drivers)
                {
                    var truck = trucks.ElementAt(select ? 1 : 0);
                    var trailer = trailers.ElementAt(select ? 1 : 0);
                    var driverSchedule = new DriverSchedule
                    {
                        Driver = d,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddHours(10),
                        Truck = truck,
                        Trailer = trailer,
                    };

                    driverSchedule.AddCheckListNotes(truck.CheckLists);
                    driverSchedule.AddCheckListNotes(trailer.CheckLists);
                    await context.DriverScheduleLists.AddAsync(driverSchedule);
                    select = !select;

                }
                await context.SaveChangesAsync();

            }

            if (!context.Orders.Any())
            {
                var customers =await context.Customers.Include(l => l.Locations).ToListAsync();
                var locations = await context.Locations.ToListAsync();
                var list = new List<Order>();
                customers.ForEach(c =>
                {
                    var order = new Order(c, DateTime.Now, DateTime.Now);
                    
                    var fuelProducts = context.FuelProducts.ToList();

                    fuelProducts.ForEach(f => {
                        order.AddOrderItem(f,locations.FirstOrDefault(), 5000, "LOAD CODE");
                       });
                    context.Orders.Add(order);
                });

                await context.SaveChangesAsync();
            }
        }
    }
}