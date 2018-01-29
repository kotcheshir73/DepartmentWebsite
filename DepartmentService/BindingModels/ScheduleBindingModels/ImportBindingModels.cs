using System.Collections.Generic;

namespace DepartmentService.BindingModels
{
	/// <summary>
	/// Импорт данных с общего сайта
	/// </summary>
	public class ImportToSemesterFromHTMLBindingModel
	{
		public string ScheduleUrl { get; set; }

        public bool IsFirstHalfSemester { get; set; }
	}
	/// <summary>
	/// Загружаем данные по зачетам
	/// </summary>
	public class ImportToOffsetFromExcel
	{
		public string FileName { get; set; }
	}

	/// <summary>
	/// Загружаем данные по экзаменам
	/// </summary>
	public class ImportToExaminationFromExcel
	{
		public string FileName { get; set; }
	}
}
