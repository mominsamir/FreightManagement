using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Queries.GetLocationById
{
    public class QueryLocationById : IRequest<ModelView<LocationDto>>
    {
        public long Id { get; }

        public QueryLocationById(long id)
        {
            Id = id;
        }
    }

    public class QueryLocationByIdHandler : IRequestHandler<QueryLocationById, ModelView<LocationDto>>
    {
        private readonly IApplicationDbContext _contex;
        public QueryLocationByIdHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ModelView<LocationDto>> Handle(QueryLocationById request, CancellationToken cancellationToken)
        {
            var location = await _contex.Locations.Include(l => l.Tanks)
                .Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            return new ModelView<LocationDto>(new LocationDto(
                    location.Id,
                    location.Name,
                    location.Email.Value,
                    location.IsActive,
                    location.DeliveryAddress,
                    location.Tanks
                ), true, false, true);
              
        }
    }
}
