using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using FreightManagement.Application.Users.Queries.UserConfiguration;
using FreightManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// https://levelup.gitconnected.com/asp-net-5-authorization-and-authentication-with-bearer-and-jwt-2d0cef85dc5d

namespace FreightManagement.WebUI.Controllers
{

    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] QueryConfirmUserIdentify model)
        {
            var user = await Mediator.Send(model);

            if (user == null)
                return NotFound(new { message = "User or password invalid" });

            var token = TokenService.CreateToken(user);
            return new
            {
                token = token
            };
        }

        [HttpGet]
        [Route("user-configration")]
        [Authorize]
        public async Task<ActionResult<UserConfigurationDto>> GetUserConfiguration()
        {
            return await Mediator.Send(new QueryUserConfiguration());
        }

        [HttpGet]
        [Route("tester")]
        [Authorize(Roles = "tester")]
        public string Tester()
        {
            return "You are a Tester";
        }

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";
    }
}
