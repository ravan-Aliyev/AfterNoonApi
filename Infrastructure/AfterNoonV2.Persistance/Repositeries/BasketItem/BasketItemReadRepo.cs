using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Domain.Entities;
using AfterNoonV2.Persistance.Repositeries;

namespace AfterNoonV2.Persistance.Repositeries;

public class BasketItemReadRepo : ReadRepository<BasketItem>, IBasketItemReadRepo
{
    public BasketItemReadRepo(AfterNoonV2DbContext context) : base(context)
    {
    }
}
