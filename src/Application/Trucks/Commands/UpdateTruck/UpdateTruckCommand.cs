using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Commands.UpdateTruck
{
    public class UpdateTruckCommand : IRequest
    {
        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
    }

    public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand>
    {
        private readonly IApplicationDbContext _contex;
        public UpdateTruckCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<Unit> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = await _contex.Trucks.FindAsync(request.Id);

            truck.NumberPlate = request.NumberPlate;
            truck.VIN = request.VIN;

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
