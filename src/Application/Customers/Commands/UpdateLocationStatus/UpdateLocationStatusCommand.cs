using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.UpdateLocationStatus
{
    public class UpdateLocationStatusCommand : IRequest 
    {
        public UpdateLocationStatusCommand(long id, bool status)
        {
            Id = id;
            Status = status;
        }

        public long Id { get; }
        public bool Status { get; }
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
            var location = await _context.Locations.FindAsync(new object[] { request.Id }, cancellationToken);

            if (location == null)
                throw new NotFoundException($"Location not found with id {request.Id }");

            if (request.Status) { location.MarkActive(); } else { location.MarkInActive(); }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
