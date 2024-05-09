using Microsoft.Extensions.Logging;
using Moq;
using ScheduleSyncProject.Logic.Logic;
using ScheduleSyncProject.Logic.Logic.Repository;

namespace ScheduleProject.Test;

internal class SynchronizationScheduleUnitTests
{
	private SynchronizationScheduleLogic _synchronizationScheduleLogic;

	[SetUp]
	public void SetUp()
	{
		Mock<IImportSchedule> mockIImportSchedule = new Mock<IImportSchedule>();
		Mock<ILogger<SynchronizationScheduleLogic>> mockLogger = new Mock<ILogger<SynchronizationScheduleLogic>>();
		_synchronizationScheduleLogic = new SynchronizationScheduleLogic(mockIImportSchedule.Object, 
			new CoreRepository(new ScheduleDbContext(null)), mockLogger.Object);
	}

	[Test]
	public async Task TestMeth()
	{
		//await _synchronizationScheduleLogic.Test();
	}
}