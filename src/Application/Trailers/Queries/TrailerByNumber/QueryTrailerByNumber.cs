using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Trailers.Queries.GetTrailer;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailers.Queries.TrailerByNumber
{

    public class QueryTrailerByNumber : IRequest<PaginatedList<TrailerDto>>
    {
        public QueryTrailerByNumber(string name)
        {
            Number = name;
        }

        public string Number { get; }
    }

    public class QueryTrailerByNumberHandler : IRequestHandler<QueryTrailerByNumber, PaginatedList<TrailerDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryTrailerByNumberHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<TrailerDto>> Handle(QueryTrailerByNumber request, CancellationToken cancellationToken)
        {

            var query = _contex.Trailers.AsNoTracking().AsQueryable();

            var filters = new List<Filter>(){ new Filter(nameof(Truck.NumberPlate), request.Number, FieldOperator.STARTS_WITH.ToString())};
            
            //new Filter(nameof(Truck.Status), VehicleStatus.ACTIVE.ToString(), FieldOperator.EQUAL.ToString())
            
            var result = await query
                .WhereRules(filters)
                .OrderBy(u => u.NumberPlate)
                .Select(u => new TrailerDto(u.Id, u.NumberPlate, u.VIN, u.Compartment, u.Capacity, u.Status))
                .ToListAsync(cancellationToken);

            if (result.Any())
                return new PaginatedList<TrailerDto>(result, 1, 1, 1);
            else
                return new PaginatedList<TrailerDto>(new List<TrailerDto>(), 1, 1, 1);

        }
    }
}