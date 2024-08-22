using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Basket.DeleteBasketItem;

public class DeleteBasketItemCommandRequest : IRequest<DeleteBasketItemCommandResponse>
{
    public string BasketItemId { get; set; }
}
