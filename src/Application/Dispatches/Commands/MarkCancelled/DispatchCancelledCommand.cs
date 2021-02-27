using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.MarkCancelled
{
    public class DispatchCancelledCommand : IRequest 
    {
        public DispatchCancelledCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class DispatchCancelledCommandHandler : IRequestHandler<DispatchCancelledCommand>
    {
        private readonly IApplicationDbContext _context;
        public DispatchCancelledCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DispatchCancelledCommand request, CancellationToken cancellationToken)
        {
            var dispatch = await _context.Dispatches.FindAsync(new object[] { request.Id }, cancellationToken);

            if (dispatch == null)
            {
                throw new NotFoundException($"Dispatch not found with {request.Id}.");
            }

            dispatch.MarkCancelled();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
