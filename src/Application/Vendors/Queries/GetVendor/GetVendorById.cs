using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Vendors.Queries.GetVendor
{
    public class GetVendorById : IRequest<VendorDto>
    {
        public GetVendorById(long id)
        {
            Id = id;
        }

        public long Id { get;}
    }

    public class GetVendorByIdHandler : IRequestHandler<GetVendorById, VendorDto>
    {
        private readonly IApplicationDbContext _context;

        public GetVendorByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VendorDto> Handle(GetVendorById request, CancellationToken cancellationToken)
        {
            var vendor = await _context.Vendors.Where(v=> v.Id ==request.Id).SingleOrDefaultAsync(cancellationToken);

            if( vendor == null)
                throw new NotFoundException(nameof(vendor), request.Id);

            return new VendorDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email.ToString(),
                IsActive = vendor.IsActive,
                Address = vendor.Address
            };
        }

    }
}
