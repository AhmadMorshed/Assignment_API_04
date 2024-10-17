using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.OrderService.Dtos
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShippingAddres { get; set; }
        public string DeliveryMethodName { get; set; }
        public OrderStatus orderStatus { get; set; }
        public OrderPaymentStatus orderPaymentStatus { get; set; }
        public IReadOnlyList<OrderItem> orderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? BasketId { get; set; }
        public string? PaymentIntentId { get; set; }




    }
}
