using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Trailers.Queries.GetTrailer;
using FreightManagement.Application.Trucks.Queries;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById
{
    public class QueryDriverScheduleById : IRequest<ModelView<DriverScheduleDto>>
    {
        public QueryDriverScheduleById(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class QueryDriverScheduleByIdHandler : IRequestHandler<QueryDriverScheduleById, ModelView<DriverScheduleDto>>
    {
        private readonly IApplicationDbContext _context;


        public QueryDriverScheduleByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelView<DriverScheduleDto>> Handle(QueryDriverScheduleById request, CancellationToken cancellationToken)
        {
            var schedule = await _context.DriverScheduleLists
                .Include(s=> s.Driver)
                .Include(s => s.Trailer)
                .Include(s => s.Truck)
                .Include(b => b.CheckList)
                .ThenInclude(b => b.CheckList).ThenInclude(b=> b.Vehicle)
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if(schedule == null)
            {
                throw new NotFoundException($"Schedule not found with {request.Id}");
            }

            var dto = new DriverScheduleDto(
                schedule.Id,
                schedule.StartTime,
                schedule.EndTime,
                new UserDto(
                    schedule.Driver.Id,
                    schedule.Driver.FirstName,
                    schedule.Driver.LastName,
                    schedule.Driver.Email,
                    schedule.Driver.Role,
                    schedule.Driver.IsActive
                ),
                new TrailerListDto(
                    schedule.Trailer.Id,
                    schedule.Trailer.NumberPlate,
                    schedule.Trailer.VIN,
                    schedule.Trailer.Capacity,
                    schedule.Trailer.Compartment,
                    schedule.Trailer.Status.ToString()
                ),
                new TruckListDto(
                    schedule.Truck.Id,
                    schedule.Truck.NumberPlate,
                    schedule.Truck.VIN,
                    schedule.Truck.NextMaintanceDate,
                    schedule.Truck.Status.ToString()
                ),
                schedule.Status.ToString(),
                schedule.CheckList.Select(l => new DriverCheckListDto(l.Id, l.CheckList.Note, l.IsChecked))
            );

            return new ModelView<DriverScheduleDto>(dto, true, false, true);
        }
    }
}
