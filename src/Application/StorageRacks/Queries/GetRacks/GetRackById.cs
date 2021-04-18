using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.StorageRack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.StorageRacks.Queries.GetRacks
{
    public class GetRackById : IRequest<ModelView<RackDto>>
    {
        public GetRackById(long id)
        {
            Id = id;
        }

        public long Id {get;}
    }

    public class GetRackByIdHandler : IRequestHandler<GetRackById, ModelView<RackDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetRackByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelView<RackDto>> Handle(GetRackById request, CancellationToken cancellationToken)
        {
            var rack = await _context.Racks.Where(l=> l.Id == request.Id )
                .SingleOrDefaultAsync(cancellationToken);

            if (rack == null)
                throw new NotFoundException(nameof(Rack), request.Id);

            return new ModelView<RackDto>(
                new RackDto
                {
                    Id = rack.Id,
                    IRSCode = rack.IRSCode,
                    Name = rack.Name,
                    IsActive = rack.IsActive,
                    Address = rack.Address
                },true,false,true
            );
        }
    }

}
