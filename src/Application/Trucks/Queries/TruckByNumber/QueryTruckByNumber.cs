using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Queries.TruckByNumber
{
    public class QueryTruckByNumber : IRequest<PaginatedList<TruckDto>>
    {
        public QueryTruckByNumber(string name)
        {
            Number = name;
        }

        public string Number { get; }
    }

    public class QueryTruckByNumberHandler : IRequestHandler<QueryTruckByNumber, PaginatedList<TruckDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryTruckByNumberHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<TruckDto>> Handle(QueryTruckByNumber request, CancellationToken cancellationToken)
        {

            var query = _contex.Trucks.AsNoTracking().AsQueryable();


            var filters = new List<Filter>(){ 
                new Filter(nameof(Truck.NumberPlate), request.Number, FieldOperator.STARTS_WITH.ToString())
            };
            
            //new Filter(nameof(Truck.Status), "0", FieldOperator.EQUAL.ToString())

            var result = await query
                .WhereRules(filters)
                .OrderBy(u => u.NumberPlate)
                .Select(u => new TruckDto(u.Id, u.NumberPlate, u.VIN, u.NextMaintanceDate, u.Status))
                .ToListAsync(cancellationToken);

            if (result.Any())
                return new PaginatedList<TruckDto>(result, 1, 1, 1);
            else
                return new PaginatedList<TruckDto>(new List<TruckDto>(), 1, 1, 1);

        }
    }
}
