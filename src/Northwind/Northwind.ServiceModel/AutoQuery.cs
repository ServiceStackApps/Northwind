using Northwind.ServiceModel.Types;
using ServiceStack;

namespace Northwind.ServiceModel
{
    [Route("/query/customers")]
    public class QueryCustomers : QueryBase<Customer> { }

    [Route("/query/orders")]
    public class QueryOrders : QueryBase<Order> { }
}