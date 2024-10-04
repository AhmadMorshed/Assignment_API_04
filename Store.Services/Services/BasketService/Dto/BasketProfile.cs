using AutoMapper;
using Store.Repository.Basket.Models;

namespace Store.Services.Services.BasketService.Dto
{
    public class BasketProfile:Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
