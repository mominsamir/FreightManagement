using FreightManagement.Application.Common.Security;
using FreightManagement.Domain.Entities;
using FreightManagement.Domain.Entities.Products;
using FreightManagement.Domain.Entities.StorageRack;
using FreightManagement.Domain.Entities.Users;
using FreightManagement.Domain.Entities.Vehicles;
using FreightManagement.Domain.Entities.Vendors;
using FreightManagement.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreightManagement.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            /*            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

                        if (userManager.Users.All(u => u.Email != administrator.UserName))
                        {
                            await userManager.CreateAsync(administrator, "Administrator1!");
                            await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
                        }*/
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" },
                        new TodoItem { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync();
            }

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

            if (!context.AllUsers.Any())
            {
                context.AllUsers.Add(user);
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
        }
    }
}
