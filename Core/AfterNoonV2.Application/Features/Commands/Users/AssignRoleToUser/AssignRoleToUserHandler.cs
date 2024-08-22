using System;
using AfterNoonV2.Application.Abstractions.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Users.AssignRoleToUser;

public class AssignRoleToUserHandler : IRequestHandler<AssignRoleToUserRequest, AssignRoleToUserResponse>
{
    readonly IUserService _userService;

    public AssignRoleToUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AssignRoleToUserResponse> Handle(AssignRoleToUserRequest request, CancellationToken cancellationToken)
    {
        await _userService.AssignRoleToUserAsync(request.UserId, request.Roles);

        return new();
    }
}
