using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Queries.GetLocationById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Queries.SearchLocation
{
    public class QueryLocationSearch : IRequest<PaginatedList<LocationDto>>
    {
        public QueryLocationSearch(
            int page,
            int pageSize,
            IEnumerable<Dictionary<string, string>> sortData,
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
        public IEnumerable<Dictionary<string, string>> SortData { get; }
        public IEnumerable<Filter> FilterData { get; } = new List<Filter>();
    }

    public class QueryLocationSearchHandler : IRequestHandler<QueryLocationSearch, PaginatedList<LocationDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryLocationSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<LocationDto>> Handle(QueryLocationSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Locations.AsNoTracking();

            // add where clause

            // add sort clause

            var data = await query.Skip((request.Page - 1) * request.PageSize)
                .OrderByDescending(i => i.Name)
                .Take(request.PageSize)
                .Select(c =>
                        new LocationDto(
                            c.Id,
                            c.Name,
                            c.Email,
                            c.IsActive,
                            c.DeliveryAddress
                      )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<LocationDto>(data, count, 1, 1);
        }

    }
}
