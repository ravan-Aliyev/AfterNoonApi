using System;
using AfterNoonV2.Domain.Entities.Identity;

namespace AfterNoonV2.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQueryResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
