using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using System.Collections.Generic;

namespace FreightManagement.Application.Users.Queries.UserConfiguration
{
    public class UserConfigurationDto
    {
        public UserDto User { get; set; }
        public IEnumerable<Menu>  Menus { get; set; }

    }

    public class Menu
    {
        public MenuItem Item { get; set; }
        public IEnumerable<MenuItem> Children { get; set; }
    }

    public class MenuItem
    {
        public string Label { get; set; }
        public string Key { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
    }
}
