using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduleSyncProject.Logic.Logic;
using ScheduleSyncProject.Logic.Logic.Repository;
using ScheduleSyncProject.Logic.Models.SettingModels;
using Serilog;

var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)//To specify environment
				.AddEnvironmentVariables()
				.Build();


var serviceProvider = new ServiceCollection()
	.AddLogging(config =>
	 {
		 var logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configuration)
			.CreateLogger();

		 config.AddSerilog(logger);
	 })
	.AddSingleton(configuration.GetSection("TimeTableAPISettings").Get<TimeTableAPISettings>() ?? 
		throw new InvalidOperationException("Cannot get object TimeTableAPISettings from settings"))
	.AddSingleton(configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>() ?? 
		throw new InvalidOperationException("Cannot get object DatabaseSettings from settings"))
	.AddSingleton<ScheduleDbContext>()
	.AddTransient<IImportSchedule, ImportFromTimeTableAPI>()
	.AddTransient<ICoreRepository, CoreRepository>()
	.AddSingleton<SynchronizationScheduleLogic>()
	.BuildServiceProvider();


var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
	Console.WriteLine("Canceling...");
	cts.Cancel();
	e.Cancel = true;
};

var logic = serviceProvider.GetService<SynchronizationScheduleLogic>();
if (logic == null)
{
	serviceProvider.GetService<ILogger<Program>>()?.LogError("logic is null");
	return;
}

await logic.SyncScheduleAsync(s => Console.WriteLine(s), cts.Token);