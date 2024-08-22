using System;
using AfterNoonV2.Application.Abstractions.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Queries.Role.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQueryRequest, GetRoleQueryResponse>
{
    readonly IRoleService _roleService;

    public GetRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetRoleQueryResponse> Handle(GetRoleQueryRequest request, CancellationToken cancellationToken)
    {
        var (id, name) = await _roleService.GetRoleAsync(request.Id);

        return new GetRoleQueryResponse
        {
            Id = id,
            Name = name
        };
    }
}
