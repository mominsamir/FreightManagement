using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Terminal.Commands.ActivateTerminal
{
    public class RackStatusCommand : IRequest
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class RackStatusCommandCommandHandler : IRequestHandler<RackStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public RackStatusCommandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RackStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Racks.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Terminal), request.Id);
            }

            if(entity.IsActive == request.IsActive)
            {
                return Unit.Value;
            }

            entity.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
