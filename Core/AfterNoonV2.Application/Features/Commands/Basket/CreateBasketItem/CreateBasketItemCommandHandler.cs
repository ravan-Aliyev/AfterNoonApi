using AfterNoonV2.Application.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Basket.CreateBasketItem;

public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommandRequest, CreateBasketItemCommandResponse>
{
    readonly IBasketService _basketService;

    public CreateBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<CreateBasketItemCommandResponse> Handle(CreateBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.AddBasketItemAsync(new() { ProductId = request.ProductId, Quantity = request.Quantity });
        return new();
    }
}
