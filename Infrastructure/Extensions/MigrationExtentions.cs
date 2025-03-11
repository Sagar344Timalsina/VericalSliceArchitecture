using verticalSliceArchitecture.Data;

namespace verticalSliceArchitecture.Infrastructure.Extensions
{
    public static class MigrationExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            //using ApplicationDbContext dbContext =
            //    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //dbContext.Database.Migrate();
        }
    }
}
