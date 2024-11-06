using Microsoft.EntityFrameworkCore;

namespace monchotradebackend.data;

public static class DataExtension 
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<MonchoDbContext>>();
        
        try 
        {
            var dbContext = services.GetRequiredService<MonchoDbContext>();
            var configuration = services.GetRequiredService<IConfiguration>();
            var usesSqlite = configuration.GetValue<bool>("UsesSqlite");
            
            logger.LogInformation("Starting database migration using provider: {Provider}", 
                usesSqlite ? "SQLite" : "SQL Server");

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation("Applying pending migrations...");
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully");
            }
            else
            {
                logger.LogInformation("No pending migrations found");
            }

            // Opcional: Verificar la conexión a la base de datos
            await dbContext.Database.CanConnectAsync();
            logger.LogInformation("Database connection verified successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database");
            throw new Exception("Database migration failed. See inner exception for details.", ex);
        }
    }

    // Método auxiliar para reiniciar la base de datos (útil durante desarrollo)
    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<MonchoDbContext>>();
        
        try
        {
            var dbContext = services.GetRequiredService<MonchoDbContext>();
            
            logger.LogWarning("Deleting existing database...");
            await dbContext.Database.EnsureDeletedAsync();
            
            logger.LogInformation("Applying migrations to create new database...");
            await dbContext.Database.MigrateAsync();
            
            logger.LogInformation("Database reset completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while resetting the database");
            throw new Exception("Database reset failed. See inner exception for details.", ex);
        }
    }
}