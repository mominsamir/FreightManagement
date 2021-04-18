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
        public async Task<ActionResult<dynamic>> Update(int id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            
            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "User Updated." };
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> Create(CreateUserCommand command)
        {
            var id  = await Mediator.Send(command);
            return new { Id = id, sucess = true, message = "User Created." };
        }

        [HttpPut]
        [Route("{id}/resetpassword")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> ResetPassword(long id, ResetPasswordCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "User password reset success." };
        }

        [HttpPut]
        [Route("{id}/activate")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> Activate(long id, UpdateUserStatusToActiveCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "User Activated success." };
        }

        [HttpPut]
        [Route("{id}/inactivate")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<ActionResult<dynamic>> InActivate(long id, UpdateUserStatusToInActiveCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return new { Id = id, sucess = true, message = "User deactivated success." };
        }

    }
}
