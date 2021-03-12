﻿using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.StorageRack;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.StorageRacks.Queries.GetRacks
{
    public class GetRackById : IRequest<ModelView<RackDto>>
    {
        public long Id {get; set;}
    }

    public class GetRackByIdHandler : IRequestHandler<GetRackById, ModelView<RackDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRackByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelView<RackDto>> Handle(GetRackById request, CancellationToken cancellationToken)
        {
            var rack = await _context.Racks.FindAsync(new object[] { request.Id },cancellationToken);

            if (rack == null)
            {
                throw new NotFoundException(nameof(Rack), request.Id);
            }

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
