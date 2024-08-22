using MediatR;

namespace AfterNoonV2.Application.Features.Commands.Basket.UpdateBasketItem;

public class UpdateBasketItemCommandRequest : IRequest<UpdateBasketItemCommandResponse>
{
    public string BasketItemId { get; set; }
    public int Quantity { get; set; }
}
