using System;
using MediatR;

namespace AfterNoonV2.Application.Features.Queries.Role.GetRole;

public class GetRoleQueryRequest : IRequest<GetRoleQueryResponse>
{
    public string Id { get; set; }
}
