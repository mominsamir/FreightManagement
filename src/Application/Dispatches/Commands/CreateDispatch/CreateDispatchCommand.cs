using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Disptaches;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Commands.CreateDispatch
{
    public class CreateDispatchCommand : IRequest<long>
    {
        public CreateDispatchCommand(long driverScheduleId, DateTime dispatchDateTime, long rackId, long orderId)
        {
            DriverScheduleId = driverScheduleId;
            DispatchDateTime = dispatchDateTime;
            RackId = rackId;
            OrderId = orderId;
        }

        public long DriverScheduleId { get; }
        public DateTime DispatchDateTime { get; }
        public long RackId { get; }
        public long OrderId { get; }
    }

    public class CreateDispatchCommandHandler : IRequestHandler<CreateDispatchCommand, long>
    {
        private readonly IApplicationDbContext _contex;
        
        public CreateDispatchCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<long> Handle(CreateDispatchCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _contex.DriverScheduleLists.FindAsync(new object[] { request.DriverScheduleId }, cancellationToken);

            if(schedule == null)
            {
                throw new NotFoundException($"Driver schedule not found with {request.DriverScheduleId}.");
            }

            var rack = await _contex.Racks.FindAsync(new object[] { request.RackId}, cancellationToken);

            if (rack == null)
                throw new NotFoundException($"Rack not found with {request.RackId}.");


            var order = await _contex.Orders.Include(i=> i.OrderItems)
                .Where(i=> i.Id == request.OrderId).SingleOrDefaultAsync(cancellationToken);

            if (order == null)
                throw new NotFoundException($"Order not found with {request.OrderId}.");


            var dispatch = new Dispatch(schedule, request.DispatchDateTime,rack,order);

            await _contex.Dispatches.AddAsync(dispatch,cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            return dispatch.Id;
        }
    }
}
