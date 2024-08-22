using AfterNoonV2.Application.ViewModel.Basket;
using AfterNoonV2.Domain.Entities;

namespace AfterNoonV2.Application.Services;

public interface IBasketService
{
    Task<List<BasketItem>> GetBasketItemsAsync();
    Task AddBasketItemAsync(VM_Add_BasketItem model);
    Task UpdateBasketItemAsync(VM_Update_BasketItem model);
    Task RemoveBasketItemAsync(string basketId);
}
