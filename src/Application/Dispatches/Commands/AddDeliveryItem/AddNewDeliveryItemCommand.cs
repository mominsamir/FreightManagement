using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.AddDeliveryItem
{
    public class AddNewDeliveryItemCommand : IRequest 
    {
        public AddNewDeliveryItemCommand(long dispatchId, long loadingId, long locationId, long unloadedQnt)
        {
            DispatchId = dispatchId;
            LoadingId = loadingId;
            LocationId = locationId;
            UnloadedQnt = unloadedQnt;
        }
        public long DispatchId { get; }
        public long LoadingId { get; }
        public long LocationId { get; }
        public long UnloadedQnt { get; }
    }

    public class AddNewDeliveryItemCommandHandler : IRequestHandler<AddNewDeliveryItemCommand>
    {
        private readonly IApplicationDbContext _context;
        public AddNewDeliveryItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewDeliveryItemCommand request, CancellationToken cancellationToken)
        {
            var dispatch = await _context.Dispatches.FindAsync(new object[] { request.DispatchId }, cancellationToken);

            if (dispatch is null)
                throw new NotFoundException($"Dispatch not found with {request.DispatchId}.");

            var location = await _context.Locations.FindAsync(new object[] { request.LocationId }, cancellationToken);

            if (location is null)
                throw new NotFoundException($"Location not found with {request.LocationId}.");


            dispatch.AddParitalDelivery(request.LoadingId, location, request.UnloadedQnt);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
