using System;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Role.CreateRole;

public class CreateRoleCommandRequest : IRequest<CreateRoleCommandResponse>
{
    public string Name { get; set; }
}
