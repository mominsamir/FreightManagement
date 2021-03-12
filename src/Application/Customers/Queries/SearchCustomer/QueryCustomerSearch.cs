using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Queries.SearchCustomer
{
    public class QueryCustomerSearch : IRequest<PaginatedList<CustomerDto>>
    {
        public QueryCustomerSearch(
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

    public class QueryCustomerSearchHandler : IRequestHandler<QueryCustomerSearch, PaginatedList<CustomerDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryCustomerSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<CustomerDto>> Handle(QueryCustomerSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Customers.AsNoTracking();

            // add where clause

            // add sort clause

            var data = await query.Skip((request.Page - 1) * request.PageSize)
                .OrderByDescending(i => i.Name)
                .Take(request.PageSize)
                .Select(c =>
                        new CustomerDto(
                            c.Id,
                            c.Name,
                            c.Email,
                            c.BillingAddress,
                            c.IsActive
                      )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<CustomerDto>(data, count, 1, 1);
        }

    }

}
