using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Trailers.Queries.GetRacks;
using FreightManagement.Application.Trucks.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById
{
    public class QueryDriverScheduleById : IRequest<ModelView<DriverScheduleDto>>
    {
        public long Id;
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
                .Include(b => b.CheckList).Where(l => l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            var dto = new DriverScheduleDto(
                schedule.Id,
                schedule.StartTime,
                schedule.EndTime,
                schedule.Driver,
                new TrailerDto
                {
                    Id = schedule.Trailer.Id,
                    NumberPlate = schedule.Trailer.NumberPlate,
                    VIN = schedule.Trailer.VIN,
                    Capacity = schedule.Trailer.Capacity,
                    Compartment = schedule.Trailer.Compartment,
                },
                new TruckDto
                {
                    Id = schedule.Truck.Id,
                    NumberPlate = schedule.Truck.NumberPlate,
                    VIN = schedule.Truck.VIN,
                    NextMaintanceDate = schedule.Truck.NextMaintanceDate,
                },
                schedule.Status,
                schedule.CheckList.Select(l => new DriverCheckListDto(l.Id, l.CheckList.Note, l.IsChecked)).ToList()
            );

            return new ModelView<DriverScheduleDto>(dto, true, false, true);
        }
    }
}
