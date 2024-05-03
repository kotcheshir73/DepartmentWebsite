using Newtonsoft.Json;
using ScheduleProjectApp.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScheduleProjectApp.Logic
{
	internal class ImportScheduleFromTimeTableAPI
	{
		private static List<SemesterRecordSetBindingModel> _findRecords;

		private static HttpClient _client;

		public static List<Exception> Exceps { get; private set; }

		public static async Task<bool> ImportHtml(ImportToSemesterRecordsBindingModel model)
		{
			Exceps = new();
			var apiURL = model.ScheduleUrls.First();
			if (string.IsNullOrEmpty(apiURL))
			{
				throw new Exception("Не определен url адреса api-сервера");
			}

			GetClient(apiURL, model.ScheduleAuthUrl, model.Login, model.Password);
			if (_client == null)
			{
				throw new Exception("Не удалось создать клиента http");
			}

			_findRecords = new List<SemesterRecordSetBindingModel>();

			var response = await _client.GetAsync($"{apiURL}groups/");
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Не удалось получить ответ по расписанию по группам");
			}
			var res = await response.Content.ReadAsStringAsync();
			var fullStudnetGroups = JsonConvert.DeserializeObject<TimeTableAPIScheduleAllGroupsAnswer>(res);
			foreach (var studentGroup in fullStudnetGroups.response)
			{
				try
				{
					var list = await LoadLessons(apiURL, studentGroup, model.ScheduleDate);
					if (list.Count != 0)
					{
						Exceps.AddRange(list);
					}
				}
				catch (Exception ex)
				{
					Exceps.Add(ex);
				}
			}

			//var result = SaveRecords(model);
			//if (!result.Succeeded)
			//{
			//	foreach (var err in result.Errors)
			//	{
			//		resError.AddError(err.Key, err.Value);
			//	}
			//	return false;
			//}

			return true;
		}

		private static void GetClient(string baseUrl, string url, string login, string password)
		{
			_client = new HttpClient
			{
				BaseAddress = new Uri(baseUrl)
			};
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			// _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
			//var stringContent = new StringContent(JsonConvert.SerializeObject(new { login = login, password = password }), Encoding.UTF8, "application/json");
			//var response = await _client.PostAsync($"{url}", stringContent);
			//if (!response.IsSuccessStatusCode)
			//{
			//    return;
			//}
			//var res = await response.Content.ReadAsStringAsync();
		}

		private static async Task<List<Exception>> LoadLessons(string url, string filter, DateTime date)
		{
			if (string.IsNullOrEmpty(filter))
			{
				throw new Exception("Фильтр пуст");
			}

			var response = await _client.GetAsync($"{url}timetable/?filter={filter}");
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"Не удалось получить ответ по расписанию по фильтру {filter}");
			}
			var res = await response.Content.ReadAsStringAsync();
			var schedules = JsonConvert.DeserializeObject<TimeTableAPIScheduleAnswer>(res);
			if (schedules == null || schedules.response == null || schedules.response.weeks == null)
			{
				throw new Exception($"Не удалось получить данные расписания по фильтру {filter}");
			}

			var days = new Dictionary<int, TimeTableAPIScheduleWeekNumber>();
			if (schedules.response.weeks._14 != null)
			{
				days.Add(14, schedules.response.weeks._14);
			}
			else if (schedules.response.weeks._12 != null)
			{
				days.Add(12, schedules.response.weeks._12);
			}
			else if (schedules.response.weeks._10 != null)
			{
				days.Add(10, schedules.response.weeks._10);
			}
			else if (schedules.response.weeks._8 != null)
			{
				days.Add(8, schedules.response.weeks._8);
			}
			else if (schedules.response.weeks._6 != null)
			{
				days.Add(6, schedules.response.weeks._6);
			}
			else if (schedules.response.weeks._4 != null)
			{
				days.Add(4, schedules.response.weeks._4);
			}
			else if (schedules.response.weeks._2 != null)
			{
				days.Add(2, schedules.response.weeks._2);
			}
			else if (schedules.response.weeks._0 != null)
			{
				days.Add(0, schedules.response.weeks._0);
			}
			else
			{
				days.Add(-1, new TimeTableAPIScheduleWeekNumber());
			}

			if (schedules.response.weeks._15 != null)
			{
				days.Add(15, schedules.response.weeks._15);
			}
			else if (schedules.response.weeks._13 != null)
			{
				days.Add(13, schedules.response.weeks._13);
			}
			else if (schedules.response.weeks._11 != null)
			{
				days.Add(11, schedules.response.weeks._11);
			}
			else if (schedules.response.weeks._9 != null)
			{
				days.Add(9, schedules.response.weeks._9);
			}
			else if (schedules.response.weeks._7 != null)
			{
				days.Add(7, schedules.response.weeks._7);
			}
			else if (schedules.response.weeks._5 != null)
			{
				days.Add(5, schedules.response.weeks._5);
			}
			else if (schedules.response.weeks._3 != null)
			{
				days.Add(3, schedules.response.weeks._3);
			}
			else if (schedules.response.weeks._1 != null)
			{
				days.Add(1, schedules.response.weeks._1);
			}
			else
			{
				days.Add(-2, new TimeTableAPIScheduleWeekNumber());
			}

			var list = new List<Exception>();
			int week = -1; // 0 - первая неделя, 1 - вторая неделя
			foreach (var scheduleWeek in days)
			{
				var datePeriod = scheduleWeek.Key > 7 ? date.AddDays(7 * 8) : date;
				week++;
				if (scheduleWeek.Value.days == null)
				{
					continue;
				}
				int day = -1;
				foreach (var scheduleDay in scheduleWeek.Value.days)
				{
					day++;
					int lesson = -1;
					foreach (var scheduleLes in scheduleDay.lessons)
					{
						lesson++;
						if (scheduleLes == null)
						{
							continue;
						}
						foreach (var scheduleLesson in scheduleLes)
						{
							//var entity = GetRecord(scheduleLesson, datePeriod, week, day, lesson);
							//if (entity == null)
							//{
							//	continue;
							//}
							//entity.Period = scheduleWeek.Key > 7 ? 2 : 1;
							//try
							//{
							//	CheckNewSemesterRecordForConflict(entity);
							//}
							//catch(Exception ex)
							//{
							//	list.Add(ex);
							//	continue;
							//}

							//_findRecords.Add(entity);
						}
					}
				}
			}

			return list;
		}

		//private static SemesterRecordSetBindingModel GetRecord(TimeTableAPIScheduleRecord scheduleRecord, DateTime date, int week, int day, int lesson)
		//{
		//	var entity = new SemesterRecordSetBindingModel
		//	{
		//		Id = Guid.Empty,
		//		ScheduleDate = ScheduleHelper.GetDateWithTime(date, week, day, lesson),
		//		Week = week,
		//		Day = day,
		//		Lesson = lesson,
		//		LessonStudentGroup = scheduleRecord.group,
		//		LessonClassroom = scheduleRecord.room,
		//		LessonLecturer = scheduleRecord.teacher,
		//		LessonDiscipline = scheduleRecord.nameOfLesson
		//	};

		//	// оперделяем тип занятия
		//	var matchType = Regex.Match(entity.LessonDiscipline.Trim(), @"^(\w)+\.");
		//	entity.LessonType = LessonTypes.нд;
		//	if (matchType.Success)
		//	{
		//		switch (matchType.Value.ToLower())
		//		{
		//			case "лек.":
		//				entity.LessonType = LessonTypes.лек;
		//				break;
		//			case "пр.":
		//				entity.LessonType = LessonTypes.пр;
		//				break;
		//			case "лаб.":
		//				entity.LessonType = LessonTypes.лаб;
		//				break;
		//		}
		//		if (entity.LessonType != LessonTypes.нд)
		//		{
		//			entity.LessonDiscipline = entity.LessonDiscipline.Remove(0, matchType.Value.Length).Trim();
		//		}
		//	}
		//	var subgroupMatch = Regex.Match(entity.LessonDiscipline, @"\-(\s)?\d(\s)?(п/г)$");
		//	if (subgroupMatch.Success)
		//	{
		//		entity.LessonDiscipline = entity.LessonDiscipline.Remove(entity.LessonDiscipline.Length - subgroupMatch.Value.Length);
		//	}
		//	// может запись уже добавляли в рамках других поисков
		//	var exsistRec = _findRecords.FirstOrDefault(x => x.Week == entity.Week && x.Day == entity.Day && x.Lesson == entity.Lesson &&
		//						x.LessonStudentGroup == entity.LessonStudentGroup && x.LessonClassroom == entity.LessonClassroom &&
		//						x.LessonLecturer == entity.LessonLecturer && x.LessonDiscipline == entity.LessonDiscipline);
		//	if (exsistRec != null)
		//	{
		//		return null;
		//	}

		//	using (var context = DepartmentUserManager.GetContext)
		//	{
		//		ScheduleHelper.GetStudentGroup(context, entity);
		//		ScheduleHelper.GetClassroom(context, entity);
		//		ScheduleHelper.GetLecturer(context, entity);
		//		ScheduleHelper.GetDiscipline(context, entity);
		//	}
		//	if (entity.ClassroomId == null && entity.StudentGroupId == null && entity.LecturerId == null)
		//	{
		//		return null;
		//	}
		//	return entity;
		//}

		/// <summary>
		/// Проверяем добавляемую пару на конфликты
		/// </summary>
		/// <param name="record"></param>
		private static void CheckNewSemesterRecordForConflict(SemesterRecordSetBindingModel record)
		{
			// если у пары не удалось определить ни номер аудитории, ни группы, ни преподавателя из имеющихся в БД записях
			// то такая пара нас не интересует
			if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null)
			{
				return;
			}

			if (string.IsNullOrEmpty(record.LessonLecturer) || string.IsNullOrEmpty(record.LessonStudentGroup) ||
				string.IsNullOrEmpty(record.LessonDiscipline) || string.IsNullOrEmpty(record.LessonClassroom))
			{
				throw new Exception(string.Format("дата {0} {1} {2}\r\nГруппа: {3}r\nДисциплина {4}\r\nПреподаватель: {5}r\nАудитория: {6}\r\n",
					record.Week, record.Day, record.Lesson,
					record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom));
			}

			// выбираем уже добавленные записи на эту пару
			var selectRecordsOnDate = _findRecords.Where(x => x.ScheduleDate == record.ScheduleDate);

			//ищем другие занятия этой группы (тип занятия должен совпадать, либо быть неизвестен, тогда предполагаем разибение на подгруппы)
			var exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonStudentGroup == record.LessonStudentGroup);
			if (exsistRecord != null && !(exsistRecord.LessonType == record.LessonType || exsistRecord.LessonType == LessonTypes.нд || record.LessonType == LessonTypes.нд))
			{
				throw new Exception(string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
					record.Week, record.Day, record.Lesson,
					exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup));
			}

			if (!Regex.IsMatch(record.LessonClassroom, @"д(\.)?о(\.)?т(\.)?", RegexOptions.IgnoreCase))
			//ищем другие занятия в этой аудитории (если потоковая пара, то дисциплина и преподаваетль должны совпадать)
			{
				exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonClassroom == record.LessonClassroom);
				if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
				{
					if (!Regex.IsMatch(exsistRecord.LessonClassroom, @"6(.|..|.. )?-(.|..)?([\d]+([\w]+)*|[\w. ]+)"))
						throw new Exception(string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
							record.Week, record.Day, record.Lesson,
							exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom));
				}
			}

			//ищем другие занятия этого преподавателя
			exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonLecturer == record.LessonLecturer);
			if (exsistRecord != null && !string.IsNullOrEmpty(record.LessonLecturer) && exsistRecord.LessonClassroom != record.LessonClassroom)
			{
				throw new Exception(string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
					record.Week, record.Day, record.Lesson,
					exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup));

			}
		}

		/// <summary>
		/// Проверка существующего расписания на предмет совпадений, затираем пропавшие, перезаписываем изменившиеся
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		//private static ResultService SaveRecords(ImportToSemesterRecordsBindingModel model)
		//{
		//	using (var context = DepartmentUserManager.GetContext)
		//	{
		//		// получаем записи на требуемый период
		//		var exsistRecordsFirstPeriod = context.SemesterRecords.Where(x => x.ScheduleDate >= model.ScheduleDate && x.ScheduleDate <= model.ScheduleDate.AddDays(13)).ToList();
		//		var exsistRecordsSecondPeriod = context.SemesterRecords.Where(x => x.ScheduleDate >= model.ScheduleDate.AddDays(7 * 8) && x.ScheduleDate <= model.ScheduleDate.AddDays(7 * 8).AddDays(13)).ToList();

		//		#region для начала проходим по аудиториям
		//		var classrooms = context.Classrooms.Where(x => !x.IsDeleted && !x.NotUseInSchedule).ToList();
		//		foreach (var classroom in classrooms)
		//		{
		//			// вытаскиваем пары семестра, связанные с этой аудиторией
		//			var selectedRecords = exsistRecordsFirstPeriod.Where(x => x.ClassroomId == classroom.Id).Union(exsistRecordsSecondPeriod.Where(x => x.ClassroomId == classroom.Id)).ToList();
		//			foreach (var record in selectedRecords)
		//			{
		//				// ищем эту пару в списке загруженных
		//				var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
		//											(x.ClassroomId == record.ClassroomId || x.LessonClassroom == record.LessonClassroom) &&
		//											((x.DisciplineId == record.DisciplineId && record.DisciplineId != null) || x.LessonDiscipline == record.LessonDiscipline) &&
		//											((x.LecturerId == record.LecturerId && record.LecturerId != null) || x.LessonLecturer == record.LessonLecturer) &&
		//											((x.StudentGroupId == record.StudentGroupId && record.StudentGroupId != null) || x.LessonStudentGroup == record.LessonStudentGroup));

		//				if (searchRecord != null)
		//				{
		//					searchRecord.Id = record.Id;
		//					record.Checked = true;
		//				}
		//			}
		//		}
		//		#endregion

		//		#region проход по дисциплинам
		//		var disciplines = context.Disciplines.Where(x => !x.IsDeleted).ToList();
		//		foreach (var discipline in disciplines)
		//		{
		//			//отбираем еще не проверенные записи
		//			var selectedRecords = exsistRecordsFirstPeriod.Where(x => x.DisciplineId == discipline.Id && !x.Checked).Union(exsistRecordsSecondPeriod.Where(x => x.DisciplineId == discipline.Id && !x.Checked)).ToList();
		//			foreach (var record in selectedRecords)
		//			{
		//				// ищем эту пару в списке загруженных
		//				var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
		//											((x.ClassroomId == record.ClassroomId && record.ClassroomId != null) || x.LessonClassroom == record.LessonClassroom) &&
		//											(x.DisciplineId == record.DisciplineId || x.LessonDiscipline == record.LessonDiscipline) &&
		//											((x.LecturerId == record.LecturerId && record.LecturerId != null) || x.LessonLecturer == record.LessonLecturer) &&
		//											((x.StudentGroupId == record.StudentGroupId && record.StudentGroupId != null) || x.LessonStudentGroup == record.LessonStudentGroup));

		//				if (searchRecord != null)
		//				{
		//					searchRecord.Id = record.Id;
		//					record.Checked = true;
		//				}
		//			}
		//		}
		//		#endregion

		//		#region проход по преподавателям
		//		var lecturers = context.Lecturers.Where(x => !x.IsDeleted).ToList();
		//		foreach (var lecturer in lecturers)
		//		{
		//			//отбираем еще не проверенные записи
		//			var selectedRecords = exsistRecordsFirstPeriod.Where(x => x.LecturerId == lecturer.Id && !x.Checked).Union(exsistRecordsSecondPeriod.Where(x => x.LecturerId == lecturer.Id && !x.Checked)).ToList();
		//			foreach (var record in selectedRecords)
		//			{
		//				// ищем эту пару в списке загруженных
		//				var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
		//											((x.ClassroomId == record.ClassroomId && record.ClassroomId != null) || x.LessonClassroom == record.LessonClassroom) &&
		//											((x.DisciplineId == record.DisciplineId && record.DisciplineId != null) || x.LessonDiscipline == record.LessonDiscipline) &&
		//											(x.LecturerId == record.LecturerId || x.LessonLecturer == record.LessonLecturer) &&
		//											((x.StudentGroupId == record.StudentGroupId && record.StudentGroupId != null) || x.LessonStudentGroup == record.LessonStudentGroup));

		//				if (searchRecord != null)
		//				{
		//					searchRecord.Id = record.Id;
		//					record.Checked = true;
		//				}
		//			}
		//		}
		//		#endregion

		//		#region проход по группам
		//		var groups = context.StudentGroups.Where(x => !x.IsDeleted).ToList();
		//		foreach (var group in groups)
		//		{
		//			//отбираем еще не проверенные записи
		//			var selectedRecords = exsistRecordsFirstPeriod.Where(x => x.StudentGroupId == group.Id && !x.Checked).Union(exsistRecordsSecondPeriod.Where(x => x.StudentGroupId == group.Id && !x.Checked)).ToList();
		//			foreach (var record in selectedRecords)
		//			{
		//				// ищем эту пару в списке загруженных
		//				var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
		//											((x.ClassroomId == record.ClassroomId && record.ClassroomId != null) || x.LessonClassroom == record.LessonClassroom) &&
		//											((x.DisciplineId == record.DisciplineId && record.DisciplineId != null) || x.LessonDiscipline == record.LessonDiscipline) &&
		//											((x.LecturerId == record.LecturerId && record.LecturerId != null) || x.LessonLecturer == record.LessonLecturer) &&
		//											(x.StudentGroupId == record.StudentGroupId || x.LessonStudentGroup == record.LessonStudentGroup));

		//				if (searchRecord != null)
		//				{
		//					searchRecord.Id = record.Id;
		//					record.Checked = true;
		//				}
		//			}
		//		}
		//		#endregion

		//		var deletedRecords = exsistRecordsFirstPeriod.Where(x => !x.Checked).Union(exsistRecordsSecondPeriod.Where(x => !x.Checked)).ToList();

		//		using (var transaction = context.Database.BeginTransaction())
		//		{
		//			try
		//			{
		//				if (deletedRecords.Count > 0)
		//				{ // удаляем неопознанные
		//					context.SemesterRecords.RemoveRange(deletedRecords);
		//				}

		//				// получаем опознанные
		//				var knowRecords = _findRecords.Where(x => x.Id != Guid.Empty).ToList();
		//				foreach (var record in knowRecords)
		//				{
		//					var entity = context.SemesterRecords.FirstOrDefault(x => x.Id == record.Id);
		//					if (entity == null)
		//					{
		//						return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
		//					}

		//					record.ScheduleDate = record.Period == 1 ? model.ScheduleDate : model.ScheduleDate.AddDays(7 * 8);
		//					entity = ScheduleModelFacotryFromBindingModel.CreateRecord(record, entity);
		//				}

		//				// получаем новые
		//				var unknowRecords = _findRecords.Where(x => x.Id == Guid.Empty).ToList();
		//				foreach (var record in unknowRecords)
		//				{
		//					record.Id = Guid.NewGuid();
		//					record.ScheduleDate = record.Period == 1 ? model.ScheduleDate : model.ScheduleDate.AddDays(7 * 8);
		//					var entity = record.CreateRecord();

		//					context.SemesterRecords.Add(entity);
		//				}

		//				context.SaveChanges();
		//				transaction.Commit();
		//			}
		//			catch (Exception ex)
		//			{
		//				transaction.Rollback();
		//				return ResultService.Error("Конфликт при сохранении:", ex, ResultServiceStatusCode.Error);
		//			}
		//		}

		//		return ResultService.Success();
		//	}
		//}
	}
}