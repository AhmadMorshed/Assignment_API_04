using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Services.HandleResponses;
using Store.Services.Services.OrderService;
using Store.Services.Services.OrderService.Dtos;
using Stripe.Climate;
using System.Security.Claims;

namespace Store.Web.Controllers
{
    [Authorize]

    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDto>> createOrderAsync(OrderDto input)
        {
            var order=await _orderService.CreateOrderAsync(input);
            if (order is null)
                return BadRequest(new Response(400, "Error While Creating Your Order"));
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrdersForUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders=await _orderService.GetAllOrdersForUserAsync(email);
           return Ok(orders);
        }

        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdAsync(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrderByIdAsync(id);
            return Ok(orders);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetOrderByIdAsync()
            => Ok(await _orderService.GetAllDelivaryMethodsAsync());



}
}
