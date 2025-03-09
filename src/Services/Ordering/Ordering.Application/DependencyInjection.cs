using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		//Add mediatr configuration here
		services.AddMediatR(cfr =>
		{
			cfr.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		});

		return services;
	}
}
