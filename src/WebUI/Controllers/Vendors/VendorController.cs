using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Vendors.Commands.CreateVendor;
using FreightManagement.Application.Vendors.Commands.UpdateVendor;
using FreightManagement.Application.Vendors.Queries.GetVendor;
using FreightManagement.Application.Vendors.Queries.VendorSearch;
using FreightManagement.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Vendors
{

    public class VendorController : ApiControllerBase
    {

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<VendorDto>> GetVendor(long id)
        {
            return await Mediator.Send(new GetVendorById ( id));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<VendorDto>>> Search(QueryVendorSearch query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> Create(CreateVendorCommand command)
        {
            var id = await Mediator.Send(command);
            return new { Id = id, sucess = true, message = "Vender Added." };
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> Update(int id, UpdateVendorCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "Vender Updated." };
        }
    }
}
