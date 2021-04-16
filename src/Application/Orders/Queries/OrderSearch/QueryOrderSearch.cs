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

namespace FreightManagement.Application.Orders.Queries.OrderSearch
{
    public class QueryOrderSearch : IRequest<PaginatedList<OrderListDto>>
    {
        public QueryOrderSearch(
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
                .Include(b=> b.OrderItems).AsNoTracking().AsQueryable()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var data = await query.Select(o =>
                        new OrderListDto(
                        o.Id,
                        new CustomerDto(
                            o.Customer.Id,
                            o.Customer.Name,
                            o.Customer.Email.Value,
                            o.Customer.BillingAddress,
                            o.Customer.IsActive,
                            new List<Location>()
                            ),
                        o.OrderDate,
                        o.ShipDate,
                        o.Status,
                        o.TotalQuantity()
                    )
                 )
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<OrderListDto>(data, count, 1, 1);

        }
    }
}
