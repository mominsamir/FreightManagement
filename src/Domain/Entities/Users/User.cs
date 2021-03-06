using FreightManagement.Domain.Common;


namespace FreightManagement.Domain.Entities.Users
{
    public class User : AuditableEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }

    public class Role
    {
        public const string ADMIN = "ADMIN";
        public const string DISPATCHER = "DISPATCHER";
        public const string DRIVER = "DRIVER";
    }
}
