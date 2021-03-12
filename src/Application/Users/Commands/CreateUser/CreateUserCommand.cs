using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Security;
using FreightManagement.Domain.Entities.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        public CreateUserCommand(string firstName, string lastName, string email, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Role { get; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = PasswordEncoder.ConvertPasswordToHash("Welcome123"),
                Role = request.Role,
                IsActive = true,
            };

            _context.AllUsers.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }


}
