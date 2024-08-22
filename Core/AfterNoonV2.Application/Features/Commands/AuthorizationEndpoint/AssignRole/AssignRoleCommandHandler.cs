using System;
using AfterNoonV2.Application.Abstractions.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.AuthorizationEndpoint.AssignRole;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommandRequest, AssignRoleCommandResponse>
{
    readonly IAuthorizationEndpointService _authorizationEndpointService;

    public AssignRoleCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<AssignRoleCommandResponse> Handle(AssignRoleCommandRequest request, CancellationToken cancellationToken)
    {
        await _authorizationEndpointService.AssignRoleAsync(request.Roles, request.Menu, request.Code, request.Type);

        return new();
    }
}
