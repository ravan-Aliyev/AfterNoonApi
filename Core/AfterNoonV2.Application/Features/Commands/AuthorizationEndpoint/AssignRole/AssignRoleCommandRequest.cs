using System;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.AuthorizationEndpoint.AssignRole;

public class AssignRoleCommandRequest : IRequest<AssignRoleCommandResponse>
{
    public string[] Roles { get; set; }
    public string Code { get; set; }
    public string Menu { get; set; }
    public Type? Type { get; set; }
}
