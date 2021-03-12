namespace FreightManagement.Application.Users.Queries.ConfirmUserIdentity
{
    public class UserDto
    {
        public UserDto(long id, string firstName, string lastName, string email, string role, bool isActive)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
            IsActive = isActive;
        }

        public long Id { get;  }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get;  }
        public string Role { get; }
        public bool IsActive { get; }

        /*        public void Mapping(Profile profile)
                {
                    profile.CreateMap<User, UserDto>()
                        .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
                }
        */
    }
}