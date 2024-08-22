using System;
using AfterNoonV2.Application.Abstractions.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Queries.Role.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
{
    readonly IRoleService _roleService;

    public GetAllRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = await _roleService.GetRolesAsync();

        return new()
        {
            Datas = datas
        };
    }
}
