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

namespace AfterNoonV2.Application.Features.Commands.Users.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token? token = await _authService.GoogleLoginAsync(new()
            {
                Email = request.Email,
                Name = request.Name,
                Id = request.Id,
                IdToken = request.IdToken,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhotoUrl = request.PhotoUrl,
                Provider = request.Provider
            });

            return new GoogleLoginCommandResponse { Token = token };
        }
    }
}
