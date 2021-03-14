using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.StorageRacks.Queries.GetRacks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.StorageRacks.Queries.RackSearch
{
   public class QueryRackSearch : IRequest<PaginatedList<RackDto>>
    {
        public QueryRackSearch(
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

    public class QueryRackSearchHandler : IRequestHandler<QueryRackSearch, PaginatedList<RackDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryRackSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<RackDto>> Handle(QueryRackSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Racks.AsNoTracking().AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query
                .Select(rack =>
                        new RackDto
                        {
                            Id = rack.Id,
                            Name = rack.Name,
                            IRSCode = rack.IRSCode,
                            Address = rack.Address,
                            IsActive = rack.IsActive,
                        }
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<RackDto>(data, count, 1, 1);

        }


    }
}
