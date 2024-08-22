using System.Reflection;
using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.DTOs.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AfterNoonV2.Infrastructure.Services;

public class AppService : IAppService
{
    public List<Menu> GetAuthorizeDefinitionEndPoint(Type type)
    {
        Assembly assembly = Assembly.GetAssembly(type);

        var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

        if (controllers == null)
            throw new Exception("No controller found in the assembly");

        List<Menu> menus = new();
        foreach (var controller in controllers)
        {
            var methods = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

            if (methods == null)
                continue;

            foreach (var method in methods)
            {
                var authorizeDefinitionAttribute = method.GetCustomAttribute<AuthorizeDefinitionAttribute>();

                if (authorizeDefinitionAttribute == null)
                    continue;

                Menu menu = null;
                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                {
                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                    menus.Add(menu);
                }
                else
                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                Application.DTOs.Configuration.Action action = new()
                {
                    ActionType = authorizeDefinitionAttribute.Action.ToString(),
                    Definition = authorizeDefinitionAttribute.Definition,
                };
                var httpMethodAttribute = method.GetCustomAttribute<HttpMethodAttribute>();
                action.HttpType = httpMethodAttribute?.HttpMethods.FirstOrDefault();

                action.Code = $"{action.HttpType}.{action.ActionType}.{action.Definition.Replace(" ", "")}";

                menu.Actions.Add(action);

            }
        }
        return menus;
    }
}
