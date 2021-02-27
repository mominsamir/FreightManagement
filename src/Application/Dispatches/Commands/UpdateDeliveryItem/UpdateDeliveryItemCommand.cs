using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.UpdateDeliveryItem
{
    public class UpdateDeliveryItemCommand :IRequest 
    {
        public UpdateDeliveryItemCommand(long dispatchId, IEnumerable<DeliveryItem> deliveryItems)
        {
            DispatchId = dispatchId;
            DeliveryItems = deliveryItems;
        }

        public long DispatchId { get; }

        public IEnumerable<DeliveryItem> DeliveryItems { get; }

    }

    public class DeliveryItem
    {
        public DeliveryItem(long dispatchItemId, long deliveryItemId, double unloadedQnt)
        {
            DispatchItemId = dispatchItemId;
            DeliveryItemId = deliveryItemId;
            UnloadedQnt = unloadedQnt;
        }

        public long DispatchItemId { get; }
        public long DeliveryItemId { get; }
        public double UnloadedQnt { get; }
    }

    public class UpdateDeliveryItemCommandHandler: IRequestHandler<UpdateDeliveryItemCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateDeliveryItemCommandHandler(IApplicationDbContext contex)
        {
            _context = contex;
        }

        public async Task<Unit> Handle(UpdateDeliveryItemCommand request, CancellationToken cancellationToken)
        {
            var dispatch = await _context.Dispatches.FindAsync(new object[] { request.DispatchId }, cancellationToken);

            if (dispatch == null)
            {
                throw new NotFoundException($"Dispatch not found with {request.DispatchId}.");
            }

            foreach (var item in request.DeliveryItems)
            {
                dispatch.UpdateDeliveryItem(item.DispatchItemId, item.DeliveryItemId, item.UnloadedQnt);
            }

            await _context.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    } 

}
