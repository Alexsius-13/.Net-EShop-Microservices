using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<DiscountContext>(opt =>
	opt.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();

if(app.Environment.IsDevelopment())
	app.MapGrpcReflectionService();

app.MapGrpcService<DiscountService>();

app.Run();
