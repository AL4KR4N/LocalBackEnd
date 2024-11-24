using monchotradebackend.data;
using monchotradebackend.Interface;
using monchotradebackend.service;
using monchotradebackend.repository;

using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:7001");

// Database
var usesSqlite = builder.Configuration.GetValue<bool>("UsesSqlite"); // Add this to appsettings.json
var dbProvider = usesSqlite ? "Sqlite" : "SqlServer";
var connString = builder.Configuration.GetConnectionString(dbProvider);
builder.Services.AddDbContext<MonchoDbContext>(options => 
{
    if (usesSqlite)
    {
        options.UseSqlite(connString, sqlite => 
        {
            sqlite.MigrationsAssembly("monchotradeBackend"); // Nombre exacto de tu proyecto
        });
    }
    else
    {
        options.UseSqlServer(connString, sqlServer => 
        {
            sqlServer.MigrationsAssembly("monchotradeBackend"); // Nombre exacto de tu proyecto
        });
    }
});
// Services
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddTransient<IFileService, FileService>();

// Important: Add these if not present
builder.Services.AddControllers().AddNewtonsoftJson(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MonchoTrade API", Version = "v1" });
});

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("WebAccess", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "https://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonchoTrade API V1");
        // This will serve Swagger UI at the app's root
        c.RoutePrefix = string.Empty;
    });
}

// Middleware order is important
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("WebAccess");

// Add authentication/authorization if you're using it
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

await app.MigrateDbAsync();

app.Run();

