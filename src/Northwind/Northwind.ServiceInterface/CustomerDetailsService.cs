using System;
using System.Net;
using Northwind.ServiceModel;
using Northwind.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Northwind.ServiceInterface
{
    public class CustomerDetailsService : Service
    {
        public CustomerDetailsResponse Get(CustomerDetails request)
        {
            var customer = Db.SingleById<Customer>(request.Id);
            if (customer == null)
                throw new HttpError(HttpStatusCode.NotFound,
                    new ArgumentException("Customer does not exist: " + request.Id));

            using (var ordersService = base.ResolveService<OrdersService>())
            {
                var ordersResponse = ordersService.Get(new Orders { CustomerId = customer.Id });
                return new CustomerDetailsResponse
                {
                    Customer = customer,
                    CustomerOrders = ordersResponse.Results,
                };
            }
        }
    }
}
