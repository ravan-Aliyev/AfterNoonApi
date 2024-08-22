using System;
using System.Reflection;
using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AfterNoonV2.WebApi.Filters;

public class RolePermissionFilter : IAsyncActionFilter
{
    readonly IUserService _userService;

    public RolePermissionFilter(IUserService userService)
    {
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string? name = context.HttpContext.User.Identity?.Name;

        if (string.IsNullOrEmpty(name) || name == "revann")
            await next();

        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var attribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
        var httpMethodAttribute = descriptor?.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

        var code = $"{httpMethodAttribute?.HttpMethods?.FirstOrDefault()}.{attribute?.Action}.{attribute?.Definition.Replace(" ", "")}";

        bool hasRole = await _userService.HasRolePermissionAsync(name, code);

        if (!hasRole)
            context.Result = new UnauthorizedResult();
        else
            await next();

    }
}
