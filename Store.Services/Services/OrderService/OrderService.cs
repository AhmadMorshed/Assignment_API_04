using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Services.Services.BasketService;
using Store.Services.Services.OrderService.Dtos;
using Store.Services.Services.OrderService;
using Store.Services.Services.PaymentService;
namespace Store.Services.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketService basketService,IUnitOfWork unitOfWork,IMapper mapper,IPaymentService paymentService) 
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            //Get Basket 
            var basket = await _basketService.GetBasketAsync(input.BasketId);

            if (basket is null)
                throw new Exception("Basket Not Exist");

            #region Fill Order Item List With Items in The Basket
            var orderItems = new List<OrderItemDto>();

            foreach (var basketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(basketItem.ProductId);
                if (productItem is null)
                    throw new Exception($"Product with Id : {basketItem.ProductId} Not Exist");

                var itemOrdered = new ProductItem
                {
                    ProductId = productItem.Id,
                    ProductName = productItem.Name,
                    PictureUrl = productItem.PictureUrl,
                };

                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    ProductItem = itemOrdered
                };

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedOrderItem);

            }

            #endregion
            #region Get Delivary Meyhod
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);
            if (deliveryMethod is null)
                throw new Exception("Delivary Method Not Provided");
            #endregion
            #region Calculate Subtotal
            var subtotal = orderItems.Sum(item => item.Quantity * item.Price);
            #endregion
            #region To Do => Payment
            var specs = new OrderWithPaymentIntentSpecifaction(basket.PaymentIntentId);

            var exitstingOrder = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (exitstingOrder is null)

                await _paymentService.CreateOrUpdatePaymentIntent(basket);
            #endregion
            #region Create Orders
            var mappedShipingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
            var mappedOrderItems=_mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                DeliveryMethodId = deliveryMethod.Id,
                ShippingAddress=mappedShipingAddress,
                BuyerEmail=input.BuyerEmail,
                orderItems=mappedOrderItems,
                SubTotal=subtotal,
                PaymentIntentId = basket.PaymentIntentId,

            };

            try
            {
                await _unitOfWork.Repository<Order, Guid>().AddAsync(order);
                await _unitOfWork.CompleteAsync();

                var mappedOrder=_mapper.Map<OrderDetailsDto>(order);
                return mappedOrder;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
            #endregion
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDelivaryMethodsAsync()
         => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();
        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemSpecification(buyerEmail);
            var orders=await _unitOfWork.Repository<Order,Guid>().GetAllWithSpecificationAsync(specs);

            if(orders is {Count: <= 0  })
            
                throw new Exception("You Do Not have any Orders Yet");
            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);
            return mappedOrders;

        }
           



        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if(order is null)
                throw new Exception($"there Is no Order With Id : {id}");


            var mappedOrders = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrders;
        }


    }
}
