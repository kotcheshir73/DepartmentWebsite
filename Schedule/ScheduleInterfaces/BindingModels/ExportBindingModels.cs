﻿using System;
using System.Collections.Generic;

namespace ScheduleServiceInterfaces.BindingModels
{
	/// <summary>
	/// Выгружаем расписание по аудиториям в excel-файл
	/// </summary>
	public class ExportToExcelClassroomsBindingModel
	{
		public string FileName { get; set; }

		public List<string> Classrooms { get; set; }

		public Guid SeasonDatesId { get; set; }
	}

	/// <summary>
	/// Выгружаем расписание по аудиториям в html-файлы для текущей версии сайта
	/// </summary>
	public class ExportToHTMLClassroomsBindingModel
	{
		public string FilePath { get; set; }

		public List<string> Classrooms { get; set; }

		public Guid SeasonDatesId { get; set; }
	}
}