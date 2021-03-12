using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.UpdateUserStatus
{
    public class UpdateUserStatusToInActiveCommand : IRequest
    {
        public UpdateUserStatusToInActiveCommand(long id)
        {
            Id = id;
        }


        public long Id { get; }
    }

    public class UpdateUserStatusToInActiveCommandHandler : IRequestHandler<UpdateUserStatusToInActiveCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserStatusToInActiveCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserStatusToInActiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.AllUsers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User not found with uid {request.Id}");
            }

            user.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
