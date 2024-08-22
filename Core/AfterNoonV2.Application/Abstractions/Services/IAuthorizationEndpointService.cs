using System;

namespace AfterNoonV2.Application.Abstractions.Services;

public interface IAuthorizationEndpointService
{
    public Task AssignRoleAsync(string[] roles, string menu, string code, Type type);
}
