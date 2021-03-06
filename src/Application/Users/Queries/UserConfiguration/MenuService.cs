using System.Collections.Generic;

namespace FreightManagement.Application.Users.Queries.UserConfiguration
{
    public static class MenuService
    {
        public static IEnumerable<Menu> Get(string role)
        {
            switch (role)
            {
                case "ADMIN":
                    return AdminMenus();
                case "DISPATCHER":
                    return DispatcherMenus();
                case "DRIVER":
                    return DriverMenus();
            }
            return new List<Menu>();
        }

        private static IEnumerable<Menu> AdminMenus()
        {
            var menus = new List<Menu>();

            var orderMenu = new Menu
            {
                Item = new MenuItem { Label = "Orders & Dispatch", Key = "Order", URL="/", Icon ="" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Orders", Key = "Order", URL="/dispatch/orders" },
                    new MenuItem { Label = "Schedule", Key = "Schedules", URL="/dispatch/schedules" },
                    new MenuItem { Label = "Dispatchs", Key = "Dispatch",URL="/dispatch/dispatchs" }
                }
            };
            menus.Add(orderMenu);

            var vehiclsMenu = new Menu
            {
                Item = new MenuItem { Label = "Vehicles", Key = "Vehicles", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Trucks", Key = "Trucks", URL="/dispatch/trucks" },
                    new MenuItem { Label = "Trailers", Key = "Trailers",URL="/dispatch/trailers" }
                }
            };
            menus.Add(vehiclsMenu);

            var ProductMenu = new Menu
            {
                Item = new MenuItem { Label = "Products", Key = "Key", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Fuel Product", Key = "Fuel Product ", URL="/dispatch/fuelProducts" },
                }
            };
            menus.Add(ProductMenu);

            var racksMenu = new Menu
            {
                Item = new MenuItem { Label = "Racks", Key = "terminls", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Rack", Key = "racks", URL="/dispatch/racks" }
                }
            };
            menus.Add(racksMenu);

            var vendorMenu = new Menu
            {
                Item = new MenuItem { Label = "Vendors & Customers", Key = "vendors", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Vendors", Key = "vendorsList", URL="/dispatch/vendors" },
                    new MenuItem { Label = "Customers", Key = "customerList", URL="/dispatch/customers" }
                }
            };
            menus.Add(vendorMenu);

            var userMenu = new Menu
            {
                Item = new MenuItem { Label = "Users", Key = "Users", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Users", Key = "userList", URL="/dispatch/Users" }
                }
            };
            menus.Add(userMenu);

            return menus;
        }

        private static IEnumerable<Menu> DispatcherMenus()
        {
            var menus = new List<Menu>();

            var ProductMenu = new Menu
            {
                Item = new MenuItem { Label = "Products", Key = "Key", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Fuel Product", Key = "Fuel Product ", URL="/dispatch/fuelProducts" },
                }
            };
            menus.Add(ProductMenu);

            var orderMenu = new Menu
            {
                Item = new MenuItem { Label = "Orders & Dispatch", Key = "Order", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Orders", Key = "Order", URL="/dispatch/orders" },
                    new MenuItem { Label = "Schedule", Key = "Schedules", URL="/dispatch/schedules" },
                    new MenuItem { Label = "Dispatchs", Key = "Dispatch",URL="/dispatch/dispatchs" }
                }
            };
            menus.Add(orderMenu);

            var vehiclsMenu = new Menu
            {
                Item = new MenuItem { Label = "Vehicles", Key = "Vehicles", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Trucks", Key = "Trucks", URL="/dispatch/trucks" },
                    new MenuItem { Label = "Trailers", Key = "Trailers",URL="/dispatch/trailers" }
                }
            };
            menus.Add(vehiclsMenu);

            var racksMenu = new Menu
            {
                Item = new MenuItem { Label = "Racks", Key = "terminls", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Rack", Key = "racks", URL="/dispatch/racks" }
                }
            };
            menus.Add(racksMenu);

            return menus;
        }

        private static IEnumerable<Menu> DriverMenus()
        {
            var menus = new List<Menu>();

            var orderMenu = new Menu
            {
                Item = new MenuItem { Label = "Orders & Dispatch", Key = "Order", URL = "/", Icon = "" },
                Children = new List<MenuItem>() {
                    new MenuItem { Label = "Schedule", Key = "Schedules", URL="/dispatch/schedules" },
                    new MenuItem { Label = "Dispatchs", Key = "Dispatch",URL="/dispatch/dispatchs" }
                }
            };
            menus.Add(orderMenu);

            return menus;

        }



    }
}