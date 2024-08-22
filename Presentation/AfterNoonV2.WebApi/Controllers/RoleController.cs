using AfterNoonV2.Application.Constants;
using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.Enums;
using AfterNoonV2.Application.Features.Commands.Role.CreateRole;
using AfterNoonV2.Application.Features.Commands.Role.DeleteRole;
using AfterNoonV2.Application.Features.Commands.Role.UpdateRole;
using AfterNoonV2.Application.Features.Queries.Role.GetAllRoles;
using AfterNoonV2.Application.Features.Queries.Role.GetRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfterNoonV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {
        readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Roles, Definition = "View All Roles", Action = ActionsEnum.Reading)]
        public async Task<IActionResult> GetRolesAsync([FromQuery] GetAllRolesQueryRequest request)
        {
            GetAllRolesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Roles, Definition = "View Role", Action = ActionsEnum.Reading)]
        public async Task<IActionResult> GetRoleAsync([FromRoute] GetRoleQueryRequest request)
        {
            GetRoleQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Roles, Definition = "Create Role", Action = ActionsEnum.Writing)]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleCommandRequest request)
        {
            CreateRoleCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Roles, Definition = "Update Role", Action = ActionsEnum.Updating)]
        public async Task<IActionResult> UpdateRoleAsync([FromBody, FromRoute] UpdateRoleCommandRequest request)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{Name}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Roles, Definition = "Delete Role", Action = ActionsEnum.Deleting)]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] DeleteRoleCommandRequest request)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(request);
            return Ok();
        }


    }
}
