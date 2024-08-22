using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Application.Services;
using AfterNoonV2.Application.ViewModel.Basket;
using AfterNoonV2.Domain.Entities;
using AfterNoonV2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AfterNoonV2.Persistance.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManeger;
        readonly IOrderReadRepo _orderReadRepo;
        readonly IBasketWriteRepo _basketWriteRepo;
        readonly IBasketReadRepo _basketReadRepo;
        readonly IBasketItemWriteRepo _basketItemWriteRepo;
        readonly IBasketItemReadRepo _basketItemReadRepo;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepo orderReadRepo, IBasketWriteRepo basketWriteRepo, IBasketItemWriteRepo basketItemWriteRepo, IBasketItemReadRepo basketItemReadRepo, IBasketReadRepo basketReadRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManeger = userManager;
            _orderReadRepo = orderReadRepo;
            _basketWriteRepo = basketWriteRepo;
            _basketItemWriteRepo = basketItemWriteRepo;
            _basketItemReadRepo = basketItemReadRepo;
            _basketReadRepo = basketReadRepo;
        }

        private async Task<Basket?> ContextUser()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User not found");

            AppUser? appUser = await _userManeger.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == username);


            var _basket = from basket in appUser.Baskets
                          join order in _orderReadRepo.Table
                          on basket.Id equals order.Id into basketOrders
                          from order in basketOrders.DefaultIfEmpty()
                          select new
                          {
                              basket,
                              order
                          };

            Basket? targetBasket = null;
            if (_basket.Any(b => b.order is null))
                targetBasket = _basket.FirstOrDefault(b => b.order is null)?.basket;
            else
            {
                targetBasket = new();
                appUser.Baskets.Add(targetBasket);
            }

            await _basketWriteRepo.SaveAsync();
            return targetBasket;
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUser();
            if (basket == null)
                throw new Exception("Basket not found");

            Basket? result = _basketReadRepo.Table.Include(b => b.BasketItems).ThenInclude(bi => bi.Product).FirstOrDefault(b => b.Id == basket.Id);

            if (result == null)
                throw new Exception("Basket not found");

            return result.BasketItems.ToList();
        }

        public async Task AddBasketItemAsync(VM_Add_BasketItem model)
        {
            Basket? basket = await ContextUser();
            if (basket == null)
                throw new Exception("Basket not found");

            BasketItem existingItem = await _basketItemReadRepo.GetSingleAsync(b => b.BasketId == basket.Id && b.ProductId.ToString() == model.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                _basketItemWriteRepo.Update(existingItem);
            }
            else
            {
                BasketItem basketItem = new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(model.ProductId),
                    Quantity = model.Quantity
                };
                await _basketItemWriteRepo.AddAsync(basketItem);
            }

            await _basketItemWriteRepo.SaveAsync();
        }

        public async Task RemoveBasketItemAsync(string basketId)
        {
            BasketItem basketItem = await _basketItemReadRepo.GetByIdAsync(basketId);
            if (basketItem != null)
            {
                _basketItemWriteRepo.Remove(basketItem);
                await _basketItemWriteRepo.SaveAsync();
            }
        }

        public async Task UpdateBasketItemAsync(VM_Update_BasketItem model)
        {
            BasketItem basketItem = await _basketItemReadRepo.GetByIdAsync(model.BasketItemId);
            if (basketItem != null)
            {
                basketItem.Quantity = model.Quantity;
                _basketItemWriteRepo.Update(basketItem);
                await _basketItemWriteRepo.SaveAsync();
            }
        }
    }
};

