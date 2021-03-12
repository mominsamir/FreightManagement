using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Queries.OrderSearch
{
    public class QueryOrderSearch : IRequest<PaginatedList<OrderListDto>>
    {
        public QueryOrderSearch(
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

    public class QueryOrderSearchHandler : IRequestHandler<QueryOrderSearch, PaginatedList<OrderListDto>> { 
    
        private readonly IApplicationDbContext _contex;

        public QueryOrderSearchHandler(IApplicationDbContext contex)
        {
                _contex = contex;
        }

        public async Task<PaginatedList<OrderListDto>> Handle(QueryOrderSearch request, CancellationToken cancellationToken)
        {
            var query = _contex.Orders
                .Include(b => b.Customer)
                .Include(b=> b.OrderItems).AsNoTracking();

        // add where clause

        // add sort clause

            var data = await query.Skip((request.Page - 1) * request.PageSize)
                .OrderByDescending(i => i.Id)
                .Take(request.PageSize)
                .Select(o =>
                        new OrderListDto(
                        o.Id,
                        new CustomerDto(
                            o.Customer.Id,
                            o.Customer.Name,
                            o.Customer.Email,
                            o.Customer.BillingAddress,
                            o.Customer.IsActive
                            ),
                        o.OrderDate,
                        o.ShipDate,
                        o.Status.ToString(),
                        o.TotalQuantity()
                    )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<OrderListDto>(data, count, 1, 1);

        }
    }
}
