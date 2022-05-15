using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

var configure = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: false)
       .Build();

// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient("OrderService", config =>
{
    config.BaseAddress = new Uri(configure["Services:Orders"]);
});
builder.Services.AddHttpClient("ProductsService", config =>
{
    config.BaseAddress = new Uri(configure["Services:Products"]);
}).AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(5,_=>TimeSpan.FromMilliseconds(500)));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
