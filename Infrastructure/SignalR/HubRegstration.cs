using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AfterNoonV2.SignalR;

public static class HubRegstration
{
    public static void AddHubResgtartion(this WebApplication application)
    {
        application.MapHub<ProductHub>("/product-hub");
    }
}
