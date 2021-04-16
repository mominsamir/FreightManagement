using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.AddTankToLocation
{
    public class AddTankToLocationCommand : IRequest
    {
        public AddTankToLocationCommand(long id, string name, FuelGrade fuelGrade, double capactity)
        {
            Id = id;
            Name = name;
            FuelGrade = fuelGrade;
            Capactity = capactity;
        }

        public long Id { get; }
        public string Name { get; }
        public FuelGrade FuelGrade { get; }
        public double Capactity { get; }
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
            var location = await _contex.Locations.FindAsync(new object[] { request.Id }, cancellationToken);

            location.AddNewTank(request.Name,request.FuelGrade, request.Capactity);

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
