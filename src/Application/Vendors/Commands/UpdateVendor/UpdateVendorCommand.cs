using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vendors;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Vendors.Commands.UpdateVendor
{
    public class UpdateVendorCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand>
    {
        private IApplicationDbContext _context;
        public UpdateVendorCommandHandler(IApplicationDbContext contex)
        {
            _context = contex;
        }

        public async Task<Unit> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Vendors.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Vendor), request.Id);
            }

            entity.Name = request.Name;
            entity.Address = new Address(
                    request.Street,
                    request.City,
                    request.State,
                    request.Country,
                    request.ZipCode
                );
            entity.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
