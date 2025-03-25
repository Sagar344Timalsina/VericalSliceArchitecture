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
        public DbSet<UploadFile> Uploads { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<NepaliMonth> NepaliMonth{ get; set; }
        public DbSet<NepaliCalendar> NepaliCalendar{ get; set; }
        public DbSet<YearlyMonthDays> YearlyMonthDays{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var idProperty = entityType.FindProperty("Id");
                if (idProperty != null && idProperty.ClrType == typeof(int))
                {
                    idProperty.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                }
            }

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<NepaliCalendar>()
              .HasOne(nc => nc.NepaliMonth)
              .WithMany(nm => nm.NepaliCalendars)
              .HasForeignKey(nc => nc.MonthId)
              .OnDelete(DeleteBehavior.Cascade); // Optional, ensures deletion rules

            // Relationship between YearlyMonthDays and NepaliMonth
            modelBuilder.Entity<YearlyMonthDays>()
                .HasOne(ymd => ymd.NepaliMonth)
                .WithMany(nm => nm.YearlyMonthDays) // Ensure this exists in NepaliMonth
                .HasForeignKey(ymd => ymd.MonthId)
                .OnDelete(DeleteBehavior.Cascade); // Optional

        }
    }

}
