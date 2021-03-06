﻿using FreightManagement.Application.Vendors.Commands.CreateVendor;
using FreightManagement.Application.Vendors.Commands.UpdateVendor;
using FreightManagement.Application.Vendors.Queries.GetVendor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Vendors
{
    [Authorize]
    public class VendorController : ApiControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<VendorDto>> GetRack(long id)
        {
            return await Mediator.Send(new GetVendorById { Id= id});
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateVendorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateVendorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
