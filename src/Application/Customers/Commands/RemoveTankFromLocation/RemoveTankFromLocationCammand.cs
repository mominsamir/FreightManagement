using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.RemoveTankFromLocation
{
    public class RemoveTankFromLocationCammand : IRequest 
    {
        public RemoveTankFromLocationCammand(long id, long tankId)
        {
            Id = id;
            TankId = tankId;
        }

        public long Id { get; }
        public long TankId { get; }
    }

    public class RemoveTankFromLocationCammandHandler : IRequestHandler<RemoveTankFromLocationCammand>
    {
        private readonly IApplicationDbContext _context;
        public RemoveTankFromLocationCammandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveTankFromLocationCammand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations.Include(l=> l.Tanks)
                .Where( l=> l.Id == request.Id ).SingleOrDefaultAsync(cancellationToken);

            if (location == null)
                throw new NotFoundException($"Location with Id {request.Id} not found" );

            location.RemoveTank(request.TankId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
