using System;
using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AfterNoonV2.Persistance.Services.User;

public class RoleService : IRoleService
{
    readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<Dictionary<string, string>> GetRolesAsync()
    {
        return await _roleManager.Roles.ToDictionaryAsync(x => x.Id, x => x.Name);
    }

    public async Task<(string id, string name)> GetRoleAsync(string id)
    {
        AppRole role = await _roleManager.FindByIdAsync(id);
        return (role.Id, role.Name);
    }

    public async Task CreateRoleAsync(string roleName)
    {
        await _roleManager.CreateAsync(new AppRole { Name = roleName, Id = Guid.NewGuid().ToString() });
    }

    public async Task DeleteRoleAsync(string roleName)
    {
        AppRole role = await _roleManager.FindByNameAsync(roleName);

        if (role != null)
            await _roleManager.DeleteAsync(role);
    }


    public async Task UpdateRoleAsync(string id, string roleName)
    {
        await _roleManager.UpdateAsync(new AppRole { Id = id, Name = roleName });
    }
}
