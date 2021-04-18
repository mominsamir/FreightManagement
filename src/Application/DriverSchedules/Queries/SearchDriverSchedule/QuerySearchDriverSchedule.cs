using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
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

namespace FreightManagement.Application.DriverSchedules.Queries.SearchDriverSchedule
{
    public class QuerySearchDriverSchedule : IRequest<PaginatedList<DriverScheduleListDto>>
    {
        public QuerySearchDriverSchedule(
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

    public class QueryLocationSearchHandler : IRequestHandler<QuerySearchDriverSchedule, PaginatedList<DriverScheduleListDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryLocationSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<DriverScheduleListDto>> Handle(QuerySearchDriverSchedule request, CancellationToken cancellationToken)
        {

            var query = _contex.DriverScheduleLists.AsNoTracking().AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query
                .Select(c =>
                        new DriverScheduleListDto(
                            c.Id,
                            c.StartTime,
                            c.EndTime,
                            new UserDto(
                                    c.Driver.Id,
                                    c.Driver.FirstName,
                                    c.Driver.LastName,
                                    c.Driver.Email,
                                    c.Driver.Role,
                                    c.Driver.IsActive
                                ),
                            new TrailerListDto(
                                c.Trailer.Id, 
                                c.Trailer.NumberPlate, 
                                c.Trailer.VIN,
                                c.Trailer.Capacity,
                                c.Trailer.Compartment,
                                c.Trailer.Status.ToString()
                                ),
                            new TruckListDto(
                                c.Truck.Id,
                                c.Truck.NumberPlate,
                                c.Truck.VIN,
                                c.Truck.NextMaintanceDate,
                                c.Trailer.Status.ToString()
                            ),
                          c.Status.ToString()
                      )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<DriverScheduleListDto>(data, count, 1, 1);
        }

    }
}
