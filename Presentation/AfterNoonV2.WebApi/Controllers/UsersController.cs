using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.Enums;
using AfterNoonV2.Application.Features.Commands.AuthorizationEndpoint.AssignRole;
using AfterNoonV2.Application.Features.Commands.Users.AssignRoleToUser;
using AfterNoonV2.Application.Features.Commands.Users.CreateUser;
using AfterNoonV2.Application.Features.Commands.Users.GoogleLogin;
using AfterNoonV2.Application.Features.Commands.Users.LoginUser;
using AfterNoonV2.Application.Features.Commands.Users.PasswordReset;
using AfterNoonV2.Application.Features.Commands.Users.RefreshTokenLogin;
using AfterNoonV2.Application.Features.Commands.Users.UpdatePassword;
using AfterNoonV2.Application.Features.Commands.Users.VerifyResetToken;
using AfterNoonV2.Application.Features.Queries.Users.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfterNoonV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = "Users", Definition = "Get All Users", Action = ActionsEnum.Reading)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request)
        {
            List<GetAllUsersQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = "Users", Definition = "Assign Role To User", Action = ActionsEnum.Updating)]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserRequest request)
        {
            AssignRoleToUserResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromQuery] RefreshTokenLoginCommandRequest request)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
        {
            PasswordResetCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
    }
}
