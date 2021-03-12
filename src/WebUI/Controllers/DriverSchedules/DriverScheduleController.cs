using FreightManagement.Application.Common.Models;
using FreightManagement.Application.DriverSchedules.Commands.CreateDriverSchedule;
using FreightManagement.Application.DriverSchedules.Commands.UpdateDriverCheckList;
using FreightManagement.Application.DriverSchedules.Commands.UpdateDriverSchedule;
using FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById;
using FreightManagement.Application.DriverSchedules.Queries.SearchDriverSchedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.DriverSchedules
{
    public class DriverScheduleController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<ModelView<DriverScheduleDto>>> GetDriverSchedule(long id)
        {
            return await Mediator.Send(new QueryDriverScheduleById(id));
        }

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<PaginatedList<DriverScheduleListDto>>> Search(QuerySearchDriverSchedule query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<long>> Create(CreateDriverScheduleCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult> Update(long id, UpdateDriverScheduleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}/checklist")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult> UpdateCheckList(long id, UpdateDriverCheckListCommand command)
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
