using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.UpdateUserStatus
{
    public class UpdateUserStatusToActiveCommand : IRequest
    {
        public UpdateUserStatusToActiveCommand(long id)
        {
            Id = id;
        }


        public long Id { get; }
    }

    public class UpdateUserStatusToActiveCommandHandler : IRequestHandler<UpdateUserStatusToActiveCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserStatusToActiveCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserStatusToActiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.AllUsers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User not found with uid {request.Id}");
            }

            user.IsActive = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
