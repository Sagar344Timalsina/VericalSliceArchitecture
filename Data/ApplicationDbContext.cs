using Microsoft.EntityFrameworkCore;
using verticalSliceArchitecture.Domain;

namespace verticalSliceArchitecture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
    }

}
