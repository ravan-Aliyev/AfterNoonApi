using MediatR;

namespace AfterNoonV2.Application.Features.Queries.Basket.GetAllBasketItems;
public class GetAllBasketItemsQueryRequest : IRequest<List<GetAllBasketItemsQueryResponse>>
{
}
