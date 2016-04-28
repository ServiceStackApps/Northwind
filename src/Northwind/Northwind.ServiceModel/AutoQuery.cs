using Northwind.ServiceModel.Types;
using ServiceStack;

namespace Northwind.ServiceModel
{
    [Route("/query/customers")]
    public class QueryCustomers : QueryDb<Customer> { }

    [Route("/query/orders")]
    public class QueryOrders : QueryDb<Order> { }
}