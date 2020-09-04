using CsQuery;
using CsQuery.Implementation;
using DatabaseContext;
using DocumentFormat.OpenXml.InkML;
using Enums;
using ScheduleInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tools;

namespace ScheduleImplementations.Helpers
{
	class ImportScheduleFromSite
	{
		private static List<SemesterRecordSetBindingModel> _findRecords;

		public static ResultService ImportHtml(ImportToSemesterRecordsBindingModel model)
		{
			var resError = new ResultService();

			_findRecords = new List<SemesterRecordSetBindingModel>();

			foreach (var url in model.ScheduleUrls)
			{
				try
				{
					var web = CQ.CreateFromUrl(url);

					var cells = web.Select("table tr td a");

					foreach (var cell in cells)
					{
						var groupName = cell.Cq().Text();
						if (!string.IsNullOrEmpty(groupName))
						{
							var res = ParsingPage(url.Replace("raspisan.html", (cell as HtmlAnchorElement).Href), groupName, model.ScheduleDate);

							if (!res.Succeeded)
							{
								foreach (var err in res.Errors)
								{
									resError.AddError(err.Key, err.Value);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					resError.AddError(ex);
				}
			}

			_findRecords = _findRecords.Distinct().ToList();
			var result = SaveRecords(model);
			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					resError.AddError(err.Key, err.Value);
				}
			}

			return resError;
		}

		/// <summary>
		/// Разбор страницы html с расписанием одной группы
		/// Вытаскивается строка из ячейки и передается на анализ в метод AnalisString
		/// Полученные из метода AnalisString записи расписания передаются в CheckNewSemesterRecordForConflictAndSave
		/// для проверки наличия пар и сохранения
		/// </summary>
		/// <param name="schedulrUrl"></param>
		/// <param name="classrooms"></param>
		private static ResultService ParsingPage(string schedulrUrl, string groupName, DateTime date)
		{
			// загружаем страницу расписания группы
			var web = CQ.CreateFromUrl(schedulrUrl);

			var tables = web.Select("table");

			int week = -1; // 0 - первая неделя, 1 - вторая неделя
			var resError = new ResultService();

			foreach (var table in tables)
			{
				week++;
				int day = -3;

				foreach (var row in table.Cq().Find("tr"))
				{
					day++;
					if (day < 0)
					{ // пропускаем 2 первые строки, там идут пары и время
						continue;
					}

					int lesson = -2;
					foreach (var cell in row.Cq().Find("td"))
					{
						lesson++;
						if (lesson < 0)
						{ // пропускаем первую строку, там идет день недели
							continue;
						}

						var text = cell.Cq().Text().Trim();

						if (!string.IsNullOrEmpty(text) && text.Length > 2)
						{
							var entity = new SemesterRecordSetBindingModel
							{
								Id = Guid.Empty,
								ScheduleDate = ScheduleHelper.GetDateWithTime(date, week, day, lesson),
								Week = week,
								Day = day,
								Lesson = lesson,
								LessonStudentGroup = groupName,
								NotParseRecord = text
							};

							var list = new List<SemesterRecordSetBindingModel> { entity };

							AnalisString(text, list);

							foreach (var record in list)
							{
								var result = CheckNewSemesterRecordForConflict(record);
								if (!result.Succeeded)
								{
									foreach (var err in result.Errors)
									{
										resError.AddError(err.Key, err.Value);
									}
								}
							}
						}
					}
				}
			}
			return resError;
		}

		/// <summary>
		/// Разбор ячейки расписания. Пытаемся получить название дисциплины, ФИО преподавателя и номер аудитории
		/// </summary>
		/// <param name="text"></param>
		/// <param name="records">Изначально передается список с 1 записью. Если пара разбита на подгруппы, то в список добавяться занятия</param>
		private static void AnalisString(string text, List<SemesterRecordSetBindingModel> records)
		{
			if (records.Count == 0)
			{
				return;
			}

			text = Regex.Replace(text, @"(\-?)(\s?)\d(\s?)п/г", "").TrimStart();

			using (var context = DepartmentUserManager.GetContext)
			{
				//определяем группу
				ScheduleHelper.GetStudentGroup(context, records[0]);

				// отсекаем физ-ру первым делом
				if (text.Contains("Элективные курсы по физичeской культуре и спорту"))
				{
					records[0].LessonDiscipline = "Физкультура";
					records[0].LessonClassroom = "Спортзал";
					records[0].LessonLecturer = "Преподаватель";
					records[0].LessonType = LessonTypes.пр;
					var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == records[0].LessonDiscipline);
					if (discipline != null)
					{
						records[0].DisciplineId = discipline.Id;
					}
					return;
				}

				// Для дистанта
				if (text.EndsWith("+"))
				{
					text = text.Replace("+", "6-ДОТ_Дист");
				}

				// ищем в записе наличие аудиторий
				var classroomMatches = Regex.Matches(text, @"\d(.|..|.. )?(_|-)(.|..)?([\d]+(/[\d]+)*([\w]+)*|[\w. ]+)");

				for (int clM = 0; clM < classroomMatches.Count; ++clM)
				{
					//аудиторий может быть несколько (пара для нескольких подгрупп, тогда создаем новую запись)
					while (clM >= records.Count)
					{
						records.Add(new SemesterRecordSetBindingModel
						{
							Id = Guid.Empty,
							ScheduleDate = records[0].ScheduleDate,
							Week = records[0].Week,
							Day = records[0].Day,
							Lesson = records[0].Lesson,
							LessonStudentGroup = records[0].LessonStudentGroup,
							StudentGroupId = records[0].StudentGroupId,
							NotParseRecord = records[0].NotParseRecord
						});
					}

					records[clM].LessonClassroom = classroomMatches[clM].Value;
					records[clM].LessonDiscipline = text;

					ScheduleHelper.GetClassroom(context, records[clM]);

					// убираем из текста номер аудитории, остается предмет и преподаватель
					var lessonText = text.Substring(0, text.IndexOf(records[clM].LessonClassroom)).TrimEnd();

					// вычисляем преподавателя
					records[clM].LessonLecturer = Regex.Match(lessonText, @"([\w]+ \w \w)$").Value;

					if (!string.IsNullOrEmpty(records[clM].LessonLecturer))
					{
						// оставляем только название предмета
						records[clM].LessonDiscipline = lessonText.Substring(0, lessonText.IndexOf(records[clM].LessonLecturer)).TrimEnd();
					}

					ScheduleHelper.GetLecturer(context, records[clM]);

					// оперделяем тип занятия
					records[clM].LessonType = LessonTypes.нд;
					if (records[clM].LessonDiscipline.ToLower().StartsWith("лек."))
					{
						records[clM].LessonType = LessonTypes.лек;
						records[clM].LessonDiscipline = records[clM].LessonDiscipline.Remove(0, 4);
					}
					if (records[clM].LessonDiscipline.ToLower().StartsWith("пр."))
					{
						records[clM].LessonType = LessonTypes.пр;
						records[clM].LessonDiscipline = records[clM].LessonDiscipline.Remove(0, 3);
					}
					if (records[clM].LessonDiscipline.ToLower().StartsWith("лаб."))
					{
						records[clM].LessonType = LessonTypes.лаб;
						records[clM].LessonDiscipline = records[clM].LessonDiscipline.Remove(0, 4);
					}

					// определяем дисциплину
					if (!string.IsNullOrEmpty(records[clM].LessonDiscipline))
					{
						ScheduleHelper.GetDiscipline(context, records[clM]);
					}
					else if (clM > 0) // иностранный для второй подгруппы не пишут название дисциплины
					{
						records[clM].LessonDiscipline = records[0].LessonDiscipline;
						records[clM].DisciplineId = records[0].DisciplineId;
					}
					else
					{
						records[clM].LessonDiscipline = "нет данных";
					}

					// обрезаем начальный текст, если разбивка на подгруппы идет
					text = text.Substring(text.IndexOf(records[clM].LessonClassroom) + records[clM].LessonClassroom.Length).TrimStart();
				}
			}
		}

		/// <summary>
		/// Проверяем добавляемую пару на конфликты
		/// </summary>
		/// <param name="record"></param>
		private static ResultService CheckNewSemesterRecordForConflict(SemesterRecordSetBindingModel record)
		{
			// если у пары не удалось определить ни номер аудитории, ни группы, ни преподавателя из имеющихся в БД записях
			// то такая пара нас не интересует
			if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null)
			{
				return ResultService.Success();
			}

			// выбираем уже добавленные записи на эту пару
			var selectRecordsOnDate = _findRecords.Where(x => x.ScheduleDate == record.ScheduleDate);

			//ищем другие занятия в этой аудитории (если потоковая пара, то дисциплина и преподаваетль должны совпадать)
			var exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonClassroom == record.LessonClassroom);
			if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
			{
				if (!Regex.IsMatch(exsistRecord.LessonClassroom, @"6(.|..|.. )?-(.|..)?([\d]+([\w]+)*|[\w. ]+)"))
					return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
						record.Week, record.Day, record.Lesson,
						exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
			}

			//ищем другие занятия этой группы (тип занятия должен совпадать, либо быть неизвестен, тогда предполагаем разибение на подгруппы)
			exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonStudentGroup == record.LessonStudentGroup);
			if (exsistRecord != null && !(exsistRecord.LessonType == record.LessonType || exsistRecord.LessonType == LessonTypes.нд || record.LessonType == LessonTypes.нд))
			{
				return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
					record.Week, record.Day, record.Lesson,
					exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
			}

			//ищем другие занятия этого преподавателя
			exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonLecturer == record.LessonLecturer);
			if (exsistRecord != null && !string.IsNullOrEmpty(record.LessonLecturer) && exsistRecord.LessonClassroom != record.LessonClassroom)
			{
				{
					return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
						record.Week, record.Day, record.Lesson,
						exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
				}
			}

			_findRecords.Add(record);

			return ResultService.Success();
		}

		/// <summary>
		/// Проверка существующего расписания на предмет совпадений, затираем пропавшие, перезаписываем изменившиеся
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		private static ResultService SaveRecords(ImportToSemesterRecordsBindingModel model)
		{
			using (var context = DepartmentUserManager.GetContext)
			{
				// получаем записи на требуемый период
				var exsistRecords = context.SemesterRecords.Where(x => x.ScheduleDate >= model.ScheduleDate && x.ScheduleDate <= model.ScheduleDate.AddDays(13)).ToList();

				#region для начала проходим по аудиториям
				var classrooms = context.Classrooms.Where(x => !x.IsDeleted && !x.NotUseInSchedule).ToList();
				foreach (var classroom in classrooms)
				{
					// вытаскиваем пары семестра, связанные с этой аудиторией
					var selectedRecords = exsistRecords.Where(x => x.ClassroomId == classroom.Id).ToList();
					foreach (var record in selectedRecords)
					{
						// ищем эту пару в списке загруженных
						var searchRecord = _findRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
													(x.ClassroomId == record.ClassroomId || x.LessonClassroom == record.LessonClassroom) &&
													(x.DisciplineId == record.DisciplineId || x.LessonDiscipline == record.LessonDiscipline) &&
													(x.LecturerId == record.LecturerId || x.LessonLecturer == record.LessonLecturer) &&
													(x.StudentGroupId == record.StudentGroupId || x.LessonStudentGroup == record.LessonStudentGroup));

						if (searchRecord != null)
						{
							searchRecord.Id = record.Id;
							record.Checked = true;
						}
					}
				}
				#endregion

				#region проход по дисциплинам
				var disciplines = context.Disciplines.Where(x => !x.IsDeleted).ToList();
				foreach (var discipline in disciplines)
				{
					//отбираем еще не проверенные записи
					var selectedRecords = exsistRecords.Where(x => x.DisciplineId == discipline.Id && !x.Checked).ToList();
					foreach (var record in selectedRecords)
					{
						// ищем эту пару в списке загруженных
						var searchRecord = _findRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
													(x.ClassroomId == record.ClassroomId || x.LessonClassroom == record.LessonClassroom) &&
													(x.DisciplineId == record.DisciplineId || x.LessonDiscipline == record.LessonDiscipline) &&
													((x.LecturerId == record.LecturerId && record.LecturerId != null) || x.LessonLecturer == record.LessonLecturer) &&
													((x.StudentGroupId == record.StudentGroupId && record.StudentGroupId != null) || x.LessonStudentGroup == record.LessonStudentGroup));

						if (searchRecord != null)
						{
							searchRecord.Id = record.Id;
							record.Checked = true;
						}
					}
				}
				#endregion

				#region проход по преподавателям
				var lecturers = context.Lecturers.Where(x => !x.IsDeleted).ToList();
				foreach (var lecturer in lecturers)
				{
					//отбираем еще не проверенные записи
					var selectedRecords = exsistRecords.Where(x => x.LecturerId == lecturer.Id && !x.Checked).ToList();
					foreach (var record in selectedRecords)
					{
						// ищем эту пару в списке загруженных
						var searchRecord = _findRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
													(x.ClassroomId == record.ClassroomId || x.LessonClassroom == record.LessonClassroom) &&
													(x.DisciplineId == record.DisciplineId || x.LessonDiscipline == record.LessonDiscipline) &&
													(x.LecturerId == record.LecturerId || x.LessonLecturer == record.LessonLecturer) &&
													(x.StudentGroupId == record.StudentGroupId || x.LessonStudentGroup == record.LessonStudentGroup));

						if (searchRecord != null)
						{
							searchRecord.Id = record.Id;
							record.Checked = true;
						}
					}
				}
				#endregion

				#region проход по группам
				var groups = context.StudentGroups.Where(x => !x.IsDeleted).ToList();
				foreach (var group in groups)
				{
					//отбираем еще не проверенные записи
					var selectedRecords = exsistRecords.Where(x => x.StudentGroupId == group.Id && !x.Checked).ToList();
					foreach (var record in selectedRecords)
					{
						// ищем эту пару в списке загруженных
						var searchRecord = _findRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
													((x.ClassroomId == record.ClassroomId && record.ClassroomId != null) || x.LessonClassroom == record.LessonClassroom) &&
													((x.DisciplineId == record.DisciplineId && record.DisciplineId != null) || x.LessonDiscipline == record.LessonDiscipline) &&
													((x.LecturerId == record.LecturerId && record.LecturerId != null) || x.LessonLecturer == record.LessonLecturer) &&
													(x.StudentGroupId == record.StudentGroupId || x.LessonStudentGroup == record.LessonStudentGroup));

						if (searchRecord != null)
						{
							searchRecord.Id = record.Id;
							record.Checked = true;
						}
					}
				}
				#endregion

				var deletedRecords = exsistRecords.Where(x => !x.Checked).ToList();

				using (var transaction = context.Database.BeginTransaction())
				{
					try
					{
						if (deletedRecords.Count > 0)
						{ // удаляем неопознанные
							context.SemesterRecords.RemoveRange(deletedRecords);
						}

						// получаем опознанные
						var knowRecords = _findRecords.Where(x => x.Id != Guid.Empty).ToList();
						foreach (var record in knowRecords)
						{
							var entity = context.SemesterRecords.FirstOrDefault(x => x.Id == record.Id);
							if (entity == null)
							{
								return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
							}

							record.ScheduleDate = model.ScheduleDate;
							entity = ScheduleModelFacotryFromBindingModel.CreateRecord(record, entity);
						}

						// получаем новые
						var unknowRecords = _findRecords.Where(x => x.Id == Guid.Empty).ToList();
						foreach (var record in unknowRecords)
						{
							record.Id = Guid.NewGuid();
							record.ScheduleDate = model.ScheduleDate;
							var entity = record.CreateRecord();

							context.SemesterRecords.Add(entity);
						}

						context.SaveChanges();
						transaction.Commit();
					}
					catch (Exception ex)
					{
						transaction.Rollback();
						return ResultService.Error("Конфликт при сохранении:", ex, ResultServiceStatusCode.Error);
					}
				}

				return ResultService.Success();
			}
		}
	}
}