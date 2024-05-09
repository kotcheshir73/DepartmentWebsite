using Microsoft.EntityFrameworkCore;
using ScheduleSyncProject.Logic.Models.SettingModels;

namespace ScheduleSyncProject.Logic.Logic.Repository;

internal class ScheduleDbContext(DatabaseSettings databaseConfig) : DbContext
{
    private readonly DatabaseSettings _databaseConfig = databaseConfig;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_databaseConfig.ConnectionString);
            // optionsBuilder.UseSqlServer(@"Data Source=10.3.1.13\SQLEXPRESS;Initial Catalog=DepartmentDatabaseContext;persist security info=True;user id=sa;password=isadmin;MultipleActiveResultSets=True;TrustServerCertificate=True;");
        }

        base.OnConfiguring(optionsBuilder);
    }
}