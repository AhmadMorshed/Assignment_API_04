using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Services.Services.BasketService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.BasketService
{
    public class BasketServie : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServie(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
      
        =>await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
           var basket=await _basketRepository.GetBasketAsync(basketId);
            if(basket==null)
                return new CustomerBasketDto();

            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id is null)
                input.Id = GenerateRandomBasketId();

                var customerBasket = _mapper.Map<CustomerBasket>(input);

            var updateBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            var mappedUpdateBasket=_mapper.Map<CustomerBasketDto>(updateBasket);
            return mappedUpdateBasket;


        }
        private string GenerateRandomBasketId()
        {
            Random random = new Random();

            int randomDigits = random.Next(1000,10000);
            return $"BS-{randomDigits}";
        }
    }
}
