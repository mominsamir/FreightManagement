using FreightManagement.Application.Common.Models;
using FreightManagement.Application.DriverSchedules.Commands.CreateDriverSchedule;
using FreightManagement.Application.DriverSchedules.Commands.UpdateDriverCheckList;
using FreightManagement.Application.DriverSchedules.Commands.UpdateDriverSchedule;
using FreightManagement.Application.DriverSchedules.Commands.UpdateScheduleStatus;
using FreightManagement.Application.DriverSchedules.Queries.DriverScheduleById;
using FreightManagement.Application.DriverSchedules.Queries.SearchDriverSchedule;
using FreightManagement.Domain.Entities.DriversSchedules;
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
        public async Task<ActionResult<dynamic>> Create(CreateDriverScheduleCommand command)
        {
            var id =  await Mediator.Send(command);

            return Ok(new { Id = id, success = true, message = "Scheduled Created" });
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

            return Ok(new { Id = id, success = true, message = "Scheduled Updated" });
        }

        [HttpPut]
        [Route("{id}/checklist")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<dynamic>> UpdateCheckList(long id, UpdateDriverCheckListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok(new { Id = id, success=true, message = "CheckList Completed"});
        }

        [HttpPut]
        [Route("{id}/complete")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<dynamic>> CompleteCheckList(long id)
        {
            var updateId = await Mediator.Send(new UpdateDriverScheduleStatusCommand(id, DriverScheduleStatus.SCHEDULE_COMPLETED));

            return Ok(new { Id = updateId, success = true, message = "Driver Schdule marked Completed" });
        }

        [HttpPut]
        [Route("{id}/cancel")]
        [Authorize(Roles = "ADMIN,DISPATCHER,DRIVER")]
        public async Task<ActionResult<dynamic>> CencellSchedule(long id)
        {
            var updateId = await Mediator.Send(new UpdateDriverScheduleStatusCommand(id, DriverScheduleStatus.SCHEDULE_CANCELLED));

            return Ok(new { Id = updateId, success = true, message = "Driver Schdule marked Cencelled" });
        }


    }
}
