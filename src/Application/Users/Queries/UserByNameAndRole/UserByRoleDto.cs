
namespace FreightManagement.Application.Users.Queries.UserByNameAndRole
{
    public  class UserByRoleDto
    {
        public UserByRoleDto(long id, string firstName, string lastName, string email, bool isActive)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
        }

        public long Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public bool IsActive { get; }
    }
}
