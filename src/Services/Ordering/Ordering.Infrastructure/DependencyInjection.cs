using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services
		,IConfiguration configuration)
	{
		//Add ef core configuration
		var connectionString = configuration.GetConnectionString("Database");

		//Add services to the container.
		services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
		services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

		services.AddDbContext<ApplicationDbContext>((sp, opt) =>
		{
			opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
			opt.UseSqlServer(connectionString);
		});

		return services;
	}
}
