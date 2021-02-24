using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.UpdateLocationStatus
{
    public class UpdateLocationStatusCommand : IRequest 
    {
        public long locationId;
        public bool status;
    }

    public class UpdateLocationStatusCommandHandler : IRequestHandler<UpdateLocationStatusCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateLocationStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLocationStatusCommand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations.FindAsync(request.locationId, cancellationToken);

            if (location == null)
            {
                throw new NotFoundException(string.Format("Location not found with id {0}", request.locationId));
            }

            if (request.status) { location.MarkActive(); } else { location.MarkInActive(); }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
