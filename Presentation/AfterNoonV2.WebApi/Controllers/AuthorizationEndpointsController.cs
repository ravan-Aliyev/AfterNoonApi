using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.Enums;
using AfterNoonV2.Application.Features.Commands.AuthorizationEndpoint.AssignRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfterNoonV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = "Authorization", Definition = "Assign Role", Action = ActionsEnum.Writing)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommandRequest request)
        {
            request.Type = typeof(Program);
            AssignRoleCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
    }
}
