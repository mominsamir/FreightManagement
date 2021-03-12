using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Trailers.Queries.GetRacks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailers.Queries.TrailerSearch
{
    public class QueryTrailerSearch : IRequest<PaginatedList<TrailerListDto>>
    {
        public QueryTrailerSearch(
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

    public class QueryRackSearchHandler : IRequestHandler<QueryTrailerSearch, PaginatedList<TrailerListDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryRackSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<TrailerListDto>> Handle(QueryTrailerSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Trailers.AsNoTracking();

            // add where clause

            // add sort clause

            var data = await query.Skip((request.Page - 1) * request.PageSize)
                .OrderByDescending(i => i.NumberPlate)
                .Take(request.PageSize)
                .Select(t=>
                        new TrailerListDto(
                            t.Id,
                            t.NumberPlate,
                            t.VIN,
                            t.Capacity,
                            t.Compartment,
                            t.Status.ToString()
                        )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<TrailerListDto>(data, count, 1, 1);

        }

    }
}
