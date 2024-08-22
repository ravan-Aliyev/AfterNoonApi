namespace AfterNoonV2.Application.Abstractions.Hubs;

public interface IProductHubService
{
    Task ProductAddedMessageAsync(string message);
}
