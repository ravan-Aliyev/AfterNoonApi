using AfterNoonV2.Application.Services;
using MediatR;

namespace AfterNoonV2.Application.Features.Queries.Basket.GetAllBasketItems
{
    public class GetAllBasketItemsQueryHandler : IRequestHandler<GetAllBasketItemsQueryRequest, List<GetAllBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetAllBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetAllBasketItemsQueryResponse>> Handle(GetAllBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemsAsync();

            return basketItems.Select(bi => new GetAllBasketItemsQueryResponse{
                BasketItemId = bi.Id.ToString(),
                ProductName = bi.Product.Name,
                Quantity = bi.Quantity,
                Price = bi.Product.Price
            }).ToList();
        }
    }
};

