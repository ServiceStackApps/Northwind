using Northwind.ServiceModel;
using Northwind.ServiceModel.Types;
using ServiceStack.OrmLite;

namespace Northwind.ServiceInterface
{
    public class CustomersService : ServiceStack.Service
    {
        public object Get(Customers request)
        {
            return new CustomersResponse { Customers = Db.Select<Customer>() };
        }
    }
}
