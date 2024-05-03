using Microsoft.EntityFrameworkCore;

namespace ScheduleProject.Logic.Infrastructure;

internal class ScheduleDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (optionsBuilder.IsConfigured == false)
		{
			optionsBuilder.UseSqlServer(@"Data Source=10.3.1.13\SQLEXPRESS;Initial Catalog=DepartmentDatabaseContext;persist security info=True;user id=sa;password=isadmin;MultipleActiveResultSets=True;TrustServerCertificate=True;");
		}

		base.OnConfiguring(optionsBuilder);
	}
}