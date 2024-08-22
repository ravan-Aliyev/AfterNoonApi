using System;
using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;
using AfterNoonV2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AfterNoonV2.Persistance.Services;

public class AuthorizationEndpointService : IAuthorizationEndpointService
{
    readonly IAppService _appService;
    readonly IEndpointReadRepo _endpointReadRepo;
    readonly IEndpointWriteRepo _endpointWriteRepo;
    readonly IMenuReadRepo _menuReadRepo;
    readonly IMenuWriteRepo _menuWriteRepo;
    readonly RoleManager<AppRole> _roleManager;

    public AuthorizationEndpointService(IAppService appService, IEndpointReadRepo endpointReadRepo, IEndpointWriteRepo endpointWriteRepo, IMenuReadRepo menuReadRepo, IMenuWriteRepo menuWriteRepo, RoleManager<AppRole> roleManager)
    {
        _appService = appService;
        _endpointReadRepo = endpointReadRepo;
        _endpointWriteRepo = endpointWriteRepo;
        _menuReadRepo = menuReadRepo;
        _menuWriteRepo = menuWriteRepo;
        _roleManager = roleManager;
    }

    public async Task AssignRoleAsync(string[] roles, string menu, string code, Type type)
    {
        Menu existing = await _menuReadRepo.GetSingleAsync(m => m.Name == menu);

        if (existing == null)
        {
            existing = new Menu { Id = Guid.NewGuid(), Name = menu };
            await _menuWriteRepo.AddAsync(existing);

            await _menuWriteRepo.SaveAsync();
        }


        Enpoint? endpoint = await _endpointReadRepo.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

        if (endpoint == null)
        {
            var action = _appService.GetAuthorizeDefinitionEndPoint(type).FirstOrDefault(m => m.Name == menu)?.Actions.FirstOrDefault(e => e.Code == code);

            endpoint = new()
            {
                Id = Guid.NewGuid(),
                MenuId = existing.Id,
                ActionType = action.ActionType,
                Definiton = action.Definition,
                HttpType = action.HttpType,
                Code = action.Code
            };

            await _endpointWriteRepo.AddAsync(endpoint);
            await _endpointWriteRepo.SaveAsync();
        }
        else
        {
            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);
        }

        var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

        foreach (var role in appRoles)
            endpoint.Roles.Add(role);

        await _endpointWriteRepo.SaveAsync();
    }
}
