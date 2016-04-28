using System.Linq;
using Northwind.ServiceModel;
using Northwind.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Northwind.ServiceInterface
{
    public class OrdersService : Service
    {
        private const int PageCount = 20;

        public OrdersResponse Get(Orders request)
        {
            var orders = request.CustomerId.IsNullOrEmpty()
                ? Db.Select(Db.From<Order>().OrderByDescending(o => o.OrderDate))
                      .Skip((request.Page.GetValueOrDefault(1) - 1) * PageCount)
                      .Take(PageCount)
                      .ToList()
                : Db.Select<Order>(x => x.CustomerId == request.CustomerId);

            if (orders.Count == 0)
                return new OrdersResponse();

            var orderDetails = Db.Select<OrderDetail>(detail => Sql.In(detail.OrderId, orders.ConvertAll(x => x.Id)));

            var orderDetailsLookup = orderDetails.ToLookup(o => o.OrderId);

            var customerOrders = orders.ConvertAll(o => new CustomerOrder
            {
                Order = o,
                OrderDetails = orderDetailsLookup[o.Id].ToList()
            });

            return new OrdersResponse { Results = customerOrders };
        }
    }
}
