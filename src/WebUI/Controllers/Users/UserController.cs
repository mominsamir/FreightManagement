using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Users.Commands.CreateUser;
using FreightManagement.Application.Users.Commands.ResetPassword;
using FreightManagement.Application.Users.Commands.UpdateUser;
using FreightManagement.Application.Users.Commands.UpdateUserStatus;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using FreightManagement.Application.Users.Queries.UserById;
using FreightManagement.Application.Users.Queries.UserByNameAndRole;
using FreightManagement.Application.Users.Queries.UserSearch;
using FreightManagement.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightManagement.WebUI.Controllers.Users
{
    [ApiController]
    public class UserController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<ModelView<UserDto>>> FindUser(long id)
        {
            return await Mediator.Send(new QueryUserById (id ));
        }
        
        [HttpPost]
        [Route("search")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<PaginatedList<UserDto>>> Search(QueryUserSearch query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("{name}/drivers")]
        [Authorize(Roles = "ADMIN,DISPATCHER")]
        public async Task<ActionResult<PaginatedList<UserByRoleDto>>> SearchDrivers(string name)
        {
            return await Mediator.Send(new QueryUserByNameAndRole(name, Role.DRIVER));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<long>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}/resetpassword")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> ResetPassword(long id, ResetPasswordCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/activate")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> Activate(long id, UpdateUserStatusToActiveCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Route("{id}/inactivate")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult> InActivate(long id, UpdateUserStatusToInActiveCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

    }
}
