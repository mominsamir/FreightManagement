using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserCommand(long id,string firstName, string lastName, string email, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
            Id = id;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Role { get; }
        public long Id { get; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await  _context.AllUsers.FindAsync(new object[] { request.Id }, cancellationToken);

            if(user == null)
            {
                throw new NotFoundException($"User not found with uid {request.Id}");
            }

            user.FirstName = request.FirstName;
            user.LastName= request.LastName;
            user.Email= request.Email;
            user.Role = request.Role;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
