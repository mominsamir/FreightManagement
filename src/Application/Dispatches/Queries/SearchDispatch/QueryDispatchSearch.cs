using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Dispatches.Queries.QueryDispatch;
using FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById;
using FreightManagement.Application.Trailers.Queries.GetTrailer;
using FreightManagement.Application.Trucks.Queries;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Dispatches.Queries.SearchDispatch
{
    public class QueryDispatchSearch: IRequest<PaginatedList<DispatchDto>>
    {
        public QueryDispatchSearch(
            int page,
            int pageSize,
            IEnumerable<Sort> sortData,
            IEnumerable<Filter> filterData
        )
        {
            Page = page;
            PageSize = pageSize;
            SortData = sortData;
            FilterData = filterData;
        }

        public int Page { get; } = 1;
        public int PageSize { get; } = 10;
        public IEnumerable<Sort> SortData { get; }
        public IEnumerable<Filter> FilterData { get; } = new List<Filter>();
    }

    public class QueryDispatchSearchHandler : IRequestHandler<QueryDispatchSearch, PaginatedList<DispatchDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryDispatchSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<DispatchDto>> Handle(QueryDispatchSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Dispatches
                .Include(l => l.DriverSchedule)
                .ThenInclude(l => l.Driver)
                .Include(l => l.DriverSchedule)
                .ThenInclude(l => l.Truck)
                .Include(l => l.DriverSchedule)
                .ThenInclude(l => l.Trailer)
                .AsNoTracking()
                .AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query
                .Select(d =>
                    new DispatchDto(
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
                            new List<DriverCheckListDto>()
                        ),
                        d.DispatchDateTime,
                        d.Status,
                        d.Miles,
                        d.DispatchStartTime,
                        d.DispatchEndTime,
                        d.RackArrivalTime,
                        d.RackLeftOnTime,
                        d.LoadingStartTime,
                        d.LoadingEndTime
                     )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<DispatchDto>(data, count, 1, 1);
        }


    }
}
