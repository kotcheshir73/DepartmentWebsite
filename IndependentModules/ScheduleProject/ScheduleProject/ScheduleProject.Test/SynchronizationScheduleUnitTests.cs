using Microsoft.Extensions.Logging;
using Moq;
using ScheduleProject.Logic.Infrastructure;
using ScheduleProject.Logic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			new CoreRepository(new ScheduleDbContext()), mockLogger.Object);
	}

	[Test]
	public async Task TestMeth()
	{
		await _synchronizationScheduleLogic.Test();
	}
}