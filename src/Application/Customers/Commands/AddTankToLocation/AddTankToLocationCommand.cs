using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.AddTankToLocation
{
    public class AddTankToLocationCommand : IRequest
    {
        public long LocationId;
        public string Name;
        public FuelGrade fuelGrade;
        public double Capactity;
    }

    public class AddTankToLocationCommandHandler : IRequestHandler<AddTankToLocationCommand>
    {
        private readonly IApplicationDbContext _contex;

        public AddTankToLocationCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }
        public async Task<Unit> Handle(AddTankToLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _contex.Locations.FindAsync(new object[] { request.LocationId }, cancellationToken);

            location.AddNewTank(request.Name,request.fuelGrade, request.Capactity);

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
