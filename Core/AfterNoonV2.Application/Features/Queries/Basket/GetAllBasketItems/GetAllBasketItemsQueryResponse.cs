using AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Application.Features.Queries.Basket.GetAllBasketItems;

public class GetAllBasketItemsQueryResponse
{
    public string BasketItemId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
