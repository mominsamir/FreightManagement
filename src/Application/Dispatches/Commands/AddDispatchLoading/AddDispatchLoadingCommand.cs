using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.AddDispatchLoading
{
    public class AddDispatchLoadingCommand : IRequest
    {
        public AddDispatchLoadingCommand(long dispatchId, long orderId, long rackId)
        {
            DispatchId = dispatchId;
            OrderId = orderId;
            RackId = rackId;
        }

        public long DispatchId { get; }
        public long OrderId { get; }
        public long RackId { get; }

    }

    public class AddDispatchLoadingCommandHandler : IRequestHandler<AddDispatchLoadingCommand>
    {
        private readonly IApplicationDbContext _context;
        public AddDispatchLoadingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddDispatchLoadingCommand request, CancellationToken cancellationToken)
        {
            var dispatch = await _context.Dispatches.FindAsync(new object[] { request.DispatchId}, cancellationToken);

            if (dispatch == null)
            {
                throw new NotFoundException($"Dispatch not found with {request.DispatchId}.");
            }

            var rack = await _context.Racks.FindAsync(new object[] { request.RackId }, cancellationToken);

            if (rack == null)
                throw new NotFoundException($"Rack not found with {request.RackId}.");

            var order = await _context.Orders.Include(i => i.OrderItems)
                .Where(i => i.Id == request.OrderId).SingleOrDefaultAsync(cancellationToken);

            if (order == null)
                throw new NotFoundException($"Order not found with {request.OrderId}.");


            dispatch.AddDispatchLoading(order, rack);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
