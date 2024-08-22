using AfterNoonV2.Application.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Basket.UpdateBasketItem;

public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommandRequest, UpdateBasketItemCommandResponse>
{
    readonly IBasketService _basketService;

    public UpdateBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<UpdateBasketItemCommandResponse> Handle(UpdateBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.UpdateBasketItemAsync(new() { BasketItemId = request.BasketItemId, Quantity = request.Quantity });
        return new();
    }
}
