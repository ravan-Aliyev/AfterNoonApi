using AfterNoonV2.Application.Abstractions.Hubs;
using AfterNoonV2.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace AfterNoonV2.SignalR;

public static class ServiceRegestration
{
    public static void AddSignalRService(this IServiceCollection services)
    {
        services.AddTransient<IProductHubService, ProductHubService>();
        services.AddSignalR();
    }
}
