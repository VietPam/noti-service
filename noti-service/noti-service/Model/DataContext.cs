using Microsoft.EntityFrameworkCore;

namespace noti_service.Model
{
    public class DataContext: DbContext
    {
        public DbSet<SqlUser> users { get; set; }
        public DbSet<SqlNoti> notis { get; set; }

        public static string configSql = "Host=ep-silent-hill-65750190.ap-southeast-1.postgres.vercel-storage.com:5432;Database=noti-service;Username=default;Password=8isowjIMlJG4";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(configSql);
        }
    }
}
