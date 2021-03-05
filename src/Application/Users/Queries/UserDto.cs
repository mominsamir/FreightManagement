using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Users;

namespace FreightManagement.Application.Users.Queries
{
    public class UserDto : IMapFrom<User>
    {
        public UserDto(long id, string firstName, string lastName, string email, string role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public long Id { get;  }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get;  }
        public string Role { get; }

    }
}