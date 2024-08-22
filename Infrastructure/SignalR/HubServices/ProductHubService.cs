using AfterNoonV2.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AfterNoonV2.SignalR.HubServices;

public class ProductHubService : IProductHubService
{
    readonly IHubContext<ProductHub> _context;

    public ProductHubService(IHubContext<ProductHub> context)
    {
        _context = context;
    }

    public async Task ProductAddedMessageAsync(string message)
    {
        await _context.Clients.All.SendAsync(ReciveFunctionNames.ProductAddedMessage, message);
    }
}
