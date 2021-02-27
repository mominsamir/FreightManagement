using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.UpdateDispatchLoading
{
    public class UpdateDispatchLoadingCommand : IRequest
    {
        public long DispatchId { get; }
        public IEnumerable<DispatchLoadingItem> DispatchLoadingItems { get; }
    }

    public class DispatchLoadingItem
    {
        public DispatchLoadingItem(long loadingId, string billOfLading, double loadedQuantity)
        {
            LoadingId = loadingId;
            BillOfLading = billOfLading;
            LoadedQuantity = loadedQuantity;
        }

        public long LoadingId { get; }
        public string BillOfLading { get; }
        public double LoadedQuantity { get; }

    }

    public class UpdateDispatchLoadingCommandHandler : IRequestHandler<UpdateDispatchLoadingCommand>
    {
        public readonly IApplicationDbContext _context; 
        public UpdateDispatchLoadingCommandHandler(IApplicationDbContext contex)
        {

        }
        public async Task<Unit> Handle(UpdateDispatchLoadingCommand request, CancellationToken cancellationToken)
        {
            var dispatch = await _context.Dispatches.FindAsync(new object[] { request.DispatchId }, cancellationToken);

            if (dispatch == null)
            {
                throw new NotFoundException($"Dispatch not found with {request.DispatchId}.");
            }

            foreach(var item in request.DispatchLoadingItems)
            {
                dispatch.UpdateDispatchItem(item.LoadingId, item.BillOfLading, item.LoadedQuantity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
