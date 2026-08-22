using Marketplace.ClassifiedAd;
using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Marketplace.Infrastructure.Persistence;
using Marketplace.UserProfile;
using Microsoft.OpenApi;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClassifiedAds", Version = "v1" });
});

// Configure database connection string
const string connectionString =
    "Host=localhost;Port=5432;Database=Marketplace_Chapter8;Username=ddd;Password=book;Include Error Detail=true";

// Register MarketplaceDbContext with the connection string (EF Core)
builder.Services.AddMarketplacePersistence(
    connectionString,
    enableSensitiveLogging: builder.Environment.IsDevelopment());

// Register DbConnection for Dapper usage
builder.Services.AddScoped<DbConnection>(c => new Npgsql.NpgsqlConnection(connectionString));

var purgomalumClient = new PurgomalumClient();
builder.Services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
builder.Services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
builder.Services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
builder.Services.AddScoped<ClassifiedAdsApplicationService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

builder.Services.AddScoped(c => new UserProfileApplicationService(
    c.GetService<IUserProfileRepository>(),
    c.GetService<IUnitOfWork>(),
    text => purgomalumClient.CheckForProfanity(text).GetAwaiter().GetResult()
    ));

var app = builder.Build();

// Migrate the database on startup
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
