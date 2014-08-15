
using System;
using System.Web;
using Funq;
using ServiceStack;
using Northwind.ServiceInterface;
using ServiceStack.OrmLite;
using ServiceStack.Data;

namespace Northwind
{
    public class AppHost : AppHostBase
	{
		public AppHost() : base("Northwind Web Services", typeof(CustomersService).Assembly) {}

		public override void Configure(Container container)
		{
			container.Register<IDbConnectionFactory>(
				new OrmLiteConnectionFactory("~/Northwind.sqlite".MapHostAbsolutePath(), SqliteDialect.Provider));

			//Use Redis Cache
			//container.Register<ICacheClient>(new PooledRedisClientManager());

			VCardFormat.Register(this);
		}
	}

	public class Global : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			new AppHost().Init();
		}
	}
}