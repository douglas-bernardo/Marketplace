using Marketplace;
using Marketplace.Domain;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Marketplace.Infrastructure.Persistence;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClassifiedAds", Version = "v1" });
});

const string connectionString =
    "Host=localhost;Port=5432;Database=Marketplace_Chapter8;Username=ddd;Password=book;Include Error Detail=true";

builder.Services.AddMarketplacePersistence(
    connectionString,
    enableSensitiveLogging: builder.Environment.IsDevelopment());

builder.Services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
builder.Services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
builder.Services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
builder.Services.AddScoped<Marketplace.Api.ClassifiedAdsApplicationService>();

var app = builder.Build();

await app.MigrateDatabaseAsync();

// Configure exception handling for development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// add swagger for easy testing of the API
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClassifiedAds v1"));

app.MapControllers();

app.Run();
