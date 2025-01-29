var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddMarten(opt =>
{
	opt.Connection(builder.Configuration.GetConnectionString("Database")!);
	opt.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the Http request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
