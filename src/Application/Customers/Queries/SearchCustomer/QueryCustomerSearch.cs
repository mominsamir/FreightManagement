using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Queries.GetCustomerById;
using FreightManagement.Domain.Entities.Customers;
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

    public class QueryCustomerSearchHandler : IRequestHandler<QueryCustomerSearch, PaginatedList<CustomerDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryCustomerSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<CustomerDto>> Handle(QueryCustomerSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.Customers.AsNoTracking().AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query
                .Select(c =>
                        new CustomerDto(
                            c.Id,
                            c.Name,
                            c.Email.Value,
                            c.BillingAddress,
                            c.IsActive,
                            new List<Location>()
                      )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<CustomerDto>(data, count, 1, 1);
        }

    }

}
