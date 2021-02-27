using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Commands.CreateTruck
{
    public class CreateTruckCommand : IRequest<long>
    {
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public IEnumerable<string> CheckList { get; set; }
    }

    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, long>
    {
        public readonly IApplicationDbContext _context;
        public CreateTruckCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = new Truck {
                NumberPlate = request.NumberPlate,
                VIN = request.VIN,
                NextMaintanceDate = DateTime.Now
            };

            foreach(var note in request.CheckList)
            {
                truck.AddNewCheckListItem(note);
            }

            _context.Trucks.Add(truck);

            await _context.SaveChangesAsync(cancellationToken);

            return truck.Id;
        }
    }
}
