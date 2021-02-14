using FreightManagement.Domain.Common;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Domain.Entities.Users
{
   public class User: AuditableEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public Email Email{ get; set; }

        public string Password { get; set; }

        public UserStatus Status{ get; set; }
    }

    public enum UserStatus
    {
        ACTIVE,
        INACTIVE
    }
}
