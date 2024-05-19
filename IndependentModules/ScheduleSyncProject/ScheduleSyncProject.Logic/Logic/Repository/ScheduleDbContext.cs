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
			optionsBuilder.UseSqlServer(@_databaseConfig.ConnectionString);
		}

		base.OnConfiguring(optionsBuilder);
	}
}