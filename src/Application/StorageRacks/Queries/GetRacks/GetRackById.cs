using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.StorageRack;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.StorageRacks.Queries.GetRacks
{
    public class GetRackById : IRequest<RackDto>
    {
        public long Id {get; set;}

    }

    public class GetRackByIdHandler : IRequestHandler<GetRackById, RackDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRackByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RackDto> Handle(GetRackById request, CancellationToken cancellationToken)
        {
            var rack = await _context.Racks.FindAsync(request.Id);

            if (rack == null)
            {
                throw new NotFoundException(nameof(Rack), request.Id);
            }

/*            return _mapper.Map<RackDto>(rack);*/
            return new RackDto {
                Id = rack.Id,
                IRSCode = rack.IRSCode,
                Name = rack.Name,
                IsActive = rack.IsActive,
                Address = rack.Address
            };
        }
    }

}
