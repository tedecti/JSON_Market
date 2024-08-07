using JSON_Market.Data;
using JSON_Market.Repository;
using JSON_Market.Repository.Interfaces;
using JSON_Market.Services;
using JSON_Market.Services.Interfaces;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace JSON_Market;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IFileRepository, FileRepository>();
        builder.Services.AddScoped<IEntityRepository, EntityRepository>();

        builder.Services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
    
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddHttpClient("AuthMicroservice", client =>
        {
            client.BaseAddress = new Uri("http://localhost:8081/api/auth");
        });
        builder.Services.AddTransient<IAuthService, AuthService>();
            
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v2",
                Title = "JSON Market",
                Description = "Test task for .NET Junior Developer",
            });
        });
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            DbInitializer.Initialize(context);
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
                webBuilder.UseUrls("http://*:8080");
            });
}