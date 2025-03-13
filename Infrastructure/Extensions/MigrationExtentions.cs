using Microsoft.EntityFrameworkCore;
using verticalSliceArchitecture.Data;
using verticalSliceArchitecture.Domain;

namespace verticalSliceArchitecture.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static async Task ApplyMigrationsAndSeedAsync(this IApplicationBuilder app)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
                if (!await dbContext.Roles.AnyAsync())
                {
                    dbContext.Roles.AddRange(
                        new Role { Name = "Admin" },
                        new Role { Name = "User" },
                        new Role { Name = "Manager" }
                    );
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying migrations: {ex.Message}");
            }

        }
    }
}
