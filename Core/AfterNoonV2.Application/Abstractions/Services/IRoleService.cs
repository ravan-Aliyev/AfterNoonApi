

namespace AfterNoonV2.Application.Abstractions.Services;

public interface IRoleService
{
    Task<Dictionary<string, string>> GetRolesAsync();
    Task<(string id, string name)> GetRoleAsync(string id);
    Task CreateRoleAsync(string roleName);
    Task DeleteRoleAsync(string roleName);
    Task UpdateRoleAsync(string id, string roleName);
}
