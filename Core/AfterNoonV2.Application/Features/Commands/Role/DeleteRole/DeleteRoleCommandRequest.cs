using System;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Role.DeleteRole;

public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
{
    public string Name { get; set; }
}
