using AfterNoonV2.Application.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Basket.DeleteBasketItem;

public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommandRequest, DeleteBasketItemCommandResponse>
{
    readonly IBasketService _basketService;

    public DeleteBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<DeleteBasketItemCommandResponse> Handle(DeleteBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.RemoveBasketItemAsync(request.BasketItemId);
        return new();
    }
}
