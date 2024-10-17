using StackExchange.Redis;
using Store.Data.Entities.OrderEntities;
using Order = Store.Data.Entities.OrderEntities.Order;
namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithPaymentIntentSpecifaction : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecifaction(string?paymentIntentId) 
            : base(order=>order.PaymentIntentId==paymentIntentId)
        {
        }
    }
}
