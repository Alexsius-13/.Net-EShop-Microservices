namespace Ordering.API;

public static class DependencyInjection
{
	public static IServiceCollection AddApiServices(this IServiceCollection services)
	{
		//Add carter configuration

		return services;
	}

	public static WebApplication UseApiServices(this WebApplication app)
	{
		//Use services example (app.MapCarter());

		return app;
	}
}
