using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Products.Queries.GetFuelProduct;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Queries.ProductSearch
{
    public class QueryFuelProductSearch : IRequest<PaginatedList<FuelProductListDto>>
    {
        public QueryFuelProductSearch(
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

    public class QueryFuelProductSearchHandler : IRequestHandler<QueryFuelProductSearch, PaginatedList<FuelProductListDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryFuelProductSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<FuelProductListDto>> Handle(QueryFuelProductSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.FuelProducts;

            // add where clause

            // add sort clause

            var data = await query.Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(fuelProduct =>
                        new FuelProductListDto
                        {
                            Id = fuelProduct.Id,
                            Name = fuelProduct.Name,
                            Grade = fuelProduct.Grade.ToString(),
                            UOM = fuelProduct.UOM.ToString(),
                            IsActive = fuelProduct.IsActive,
                        }
                 )
                .ToListAsync(cancellationToken: cancellationToken);

            var count = await query.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<FuelProductListDto>(data, count, 1, 1);

        }
    }
}
