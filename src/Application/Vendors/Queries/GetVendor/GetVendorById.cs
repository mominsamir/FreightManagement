using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Vendors.Queries.GetVendor
{
    public class GetVendorById : IRequest<VendorDto>
    {
        public long Id { get; set; }
    }

    public class GetVendorByIdHandler : IRequestHandler<GetVendorById, VendorDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetVendorByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VendorDto> Handle(GetVendorById request, CancellationToken cancellationToken)
        {
            var vendor = await _context.Vendors.FindAsync(request.Id);

            if( vendor == null)
            {
                throw new NotFoundException(nameof(vendor), request.Id);
            }

            return new VendorDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                IsActive = vendor.IsActive,
                Address = vendor.Address
            };
            //return _mapper.Map<VendorDto>(vendor);
        }

    }
}
