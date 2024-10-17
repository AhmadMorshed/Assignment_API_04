using Store.Services.Services.BasketService.Dto;
using Store.Services.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentInrentId);
        Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentInrentId);
    }
}
