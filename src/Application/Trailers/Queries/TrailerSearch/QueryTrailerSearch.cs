using FreightManagement.Application.Common.Extentions;
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

    public class QueryRackSearchHandler : IRequestHandler<QueryTrailerSearch, PaginatedList<TrailerListDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryRackSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<TrailerListDto>> Handle(QueryTrailerSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Trailers.AsNoTracking().AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query
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
