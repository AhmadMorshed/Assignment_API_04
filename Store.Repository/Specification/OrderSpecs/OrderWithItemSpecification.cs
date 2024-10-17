using Store.Data.Entities.OrderEntities;
using System.Linq.Expressions;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithItemSpecification : BaseSpecification<Order>
    {
        public OrderWithItemSpecification(string buyerEmail) 
            : base(order=>order.BuyerEmail==buyerEmail)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.orderItems);
            AddOrderByDescending(order => order.OrderDate);

        }


        public OrderWithItemSpecification(Guid id)
    : base(order => order.Id == id)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.orderItems);
            

        }
    }
}
