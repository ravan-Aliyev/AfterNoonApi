using System;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Users.AssignRoleToUser;

public class AssignRoleToUserRequest : IRequest<AssignRoleToUserResponse>
{
    public string UserId { get; set; }
    public string[] Roles { get; set; }
}
