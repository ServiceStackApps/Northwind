using Northwind.ServiceModel;
using ServiceStack;

namespace Northwind.ServiceInterface
{
    public class CachedServices : Service
    {
        public object Get(CachedCustomers request)
        {
            return base.Request.ToOptimizedResultUsingCache(this.Cache, 
                "urn:customers", () =>
                    this.ResolveService<CustomersService>()
                    .Get(new Customers()));
        }

        public object Get(CachedCustomerDetails request)
        {
            var cacheKey = UrnId.Create<CustomerDetails>(request.Id);

            return base.Request.ToOptimizedResultUsingCache(this.Cache, 
                cacheKey, () =>
                    this.ResolveService<CustomerDetailsService>()
                    .Get(new CustomerDetails { Id = request.Id }));
        }

        public object Get(CachedOrders request)
        {
            var cacheKey = UrnId.Create<Orders>(request.CustomerId ?? "all", request.Page.GetValueOrDefault(0).ToString());

            return base.Request.ToOptimizedResultUsingCache(Cache, 
                cacheKey, () => 
                    this.ResolveService<OrdersService>()
                    .Get(new Orders { CustomerId = request.CustomerId, Page = request.Page }));
        }
    }
}
