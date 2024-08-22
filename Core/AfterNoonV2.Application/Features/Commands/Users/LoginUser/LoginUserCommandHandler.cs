using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.Abstractions.Token;
using AfterNoonV2.Application.DTOs;
using AfterNoonV2.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Features.Commands.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token? token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);

            if (token != null)
                return new LoginUserCommandSuccessResponse()
                {
                    Token = token,
                };

            return new LoginUserCommandFailResponse()
            {
                Message = "Can not login"
            };
        }
    }
}
