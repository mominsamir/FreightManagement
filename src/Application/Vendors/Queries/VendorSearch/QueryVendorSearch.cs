using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Vendors.Queries.GetVendor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Vendors.Queries.VendorSearch
{
    public class QueryVendorSearch : IRequest<PaginatedList<VendorDto>>
    {
        public QueryVendorSearch(
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

    public class QueryVendorSearchHandler : IRequestHandler<QueryVendorSearch, PaginatedList<VendorDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryVendorSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<VendorDto>> Handle(QueryVendorSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Vendors.AsNoTracking().AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query
                .Select(t =>
                        new VendorDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Email = t.Email.ToString(),
                            Address = t.Address,
                            IsActive = t.IsActive
                        }
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<VendorDto>(data, count, 1, 1);
        }

    }

}
