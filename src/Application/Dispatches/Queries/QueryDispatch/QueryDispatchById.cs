
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById;
using FreightManagement.Application.Orders.Queries;
using FreightManagement.Application.StorageRacks.Queries.GetRacks;
using FreightManagement.Application.Trailers.Queries.GetTrailer;
using FreightManagement.Application.Trucks.Queries;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Queries.QueryDispatch
{
    public class QueryDispatchById : IRequest<ModelView<DispatchDto>>
    {
        public long Id { get; }

        public QueryDispatchById(long id)
        {
            Id = id;
        }
    }

    public class QueryDispatchByIdHandler : IRequestHandler<QueryDispatchById, ModelView<DispatchDto>>
    {
        private readonly IApplicationDbContext _context;

        public QueryDispatchByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelView<DispatchDto>> Handle(QueryDispatchById request, CancellationToken cancellationToken)
        {
            var d = await _context.Dispatches
                .Include(l =>l.DriverSchedule)
                .ThenInclude(l=> l.Driver)
                .Include(l => l.DriverSchedule)
                .ThenInclude(l => l.Truck)
                .Include(l => l.DriverSchedule)
                .ThenInclude(l => l.Trailer)
                .Include(l => l.DispatchLoading)
                .ThenInclude(l => l.Rack)
                .Include(l => l.DispatchLoading)
                .ThenInclude(l => l.OrderItem)
                .ThenInclude(o=> o.Location)
                .Include(l => l.DispatchLoading)
                .ThenInclude(l => l.OrderItem)
                .ThenInclude(o => o.FuelProduct)
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);


            return new ModelView<DispatchDto>(new DispatchDto(
                d.Id,
                new DriverScheduleDto(
                    d.DriverSchedule.Id,
                    d.DriverSchedule.StartTime,
                    d.DriverSchedule.EndTime,
                    new UserDto(
                        d.DriverSchedule.Driver.Id,
                        d.DriverSchedule.Driver.FirstName,
                        d.DriverSchedule.Driver.LastName,
                        d.DriverSchedule.Driver.Email,
                        d.DriverSchedule.Driver.Role,
                        d.DriverSchedule.Driver.IsActive
                    ),
                    new TrailerListDto(
                        d.DriverSchedule.Trailer.Id,
                        d.DriverSchedule.Trailer.NumberPlate,
                        d.DriverSchedule.Trailer.VIN,
                        d.DriverSchedule.Trailer.Capacity,
                        d.DriverSchedule.Trailer.Compartment,
                        d.DriverSchedule.Trailer.Status.ToString()
                    ),
                    new TruckListDto(
                        d.DriverSchedule.Truck.Id,
                        d.DriverSchedule.Truck.NumberPlate,
                        d.DriverSchedule.Truck.VIN,
                        d.DriverSchedule.Truck.NextMaintanceDate,
                        d.DriverSchedule.Truck.Status.ToString()
                    ),
                    d.DriverSchedule.Status.ToString(),
                    d.DriverSchedule.CheckList.Select(l => new DriverCheckListDto(l.Id, l.CheckList.Note, l.IsChecked))
                ),
                d.DispatchDateTime,
                d.Status,
                d.Miles,
                d.DispatchStartTime,
                d.DispatchEndTime,
                d.RackArrivalTime,
                d.RackLeftOnTime,
                d.LoadingStartTime,
                d.LoadingEndTime,
                d.DispatchLoading.Select(l=>
                    new DispatchLoadingDto(
                        l.Id,
                        new RackDto
                        {
                             Id = l.Rack.Id,
                             IRSCode = l.Rack.IRSCode,
                             Name = l.Rack.Name,
                             IsActive = l.Rack.IsActive,
                             Address = l.Rack.Address
                        },
                        new OrderItemDto(
                            l.OrderItem.Id,
                            l.OrderItem.Location,
                            l.OrderItem.FuelProduct,
                            l.OrderItem.OrderedQuantity,
                            l.OrderItem.LoadCode
                        ),
                        l.BillOfLoading,
                        l.LoadedQuantity
                    )
                ).ToList()
             ),true,true,true
             );
        }
    }

}
