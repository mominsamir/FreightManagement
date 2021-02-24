using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.RemoveTankFromLocation
{
    public class RemoveTankFromLocationCammand : IRequest 
    {
        public long locationId;
        public long tankId;
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
            var location = await _context.Locations.FindAsync(request.locationId, cancellationToken);
            if (location == null)
            {
                throw new NotFoundException(string.Format("Location with Id {0} not found", request.locationId));
            }

            location.RemoveTank(request.tankId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
