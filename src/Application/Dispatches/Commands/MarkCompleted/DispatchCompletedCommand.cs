using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.MarkCompleted
{
    public class DispatchCompletedCommand : IRequest
    {
        public DispatchCompletedCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class DispatchCompletedCommandHandler : IRequestHandler<DispatchCompletedCommand>
    {
        private readonly IApplicationDbContext _context;
        public DispatchCompletedCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DispatchCompletedCommand request, CancellationToken cancellationToken)
        {
            var dispatch = await _context.Dispatches.FindAsync(new object[] { request.Id }, cancellationToken);

            if (dispatch == null)
            {
                throw new NotFoundException($"Dispatch not found with {request.Id}.");
            }

            dispatch.MarkDelivered();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
