using DatabaseContext;
using Enums;
using ScheduleInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools;

namespace ScheduleImplementations.Helpers
{
    //https://time.ulstu.ru/api/1.0/timetable
    class ImportScheduleFromTimeTableAPI
    {
        private static List<SemesterRecordSetBindingModel> _findRecords;

        private static HttpClient _client;

        public static async Task<ResultService> ImportHtml(ImportToSemesterRecordsBindingModel model)
        {
            var apiURL = model.ScheduleUrls.First();
            if (string.IsNullOrEmpty(apiURL))
            {
                return ResultService.Error("Ошибка", "Не определен url адреса api-сервера", ResultServiceStatusCode.Error);
            }

            GetClient(apiURL);
            if (_client == null)
            {
                return ResultService.Error("Ошибка", "Не удалось создать клиента http", ResultServiceStatusCode.Error);
            }

            List<string> studentGroupsNames = null;
            List<string> lecturerNames = null;
            List<string> classroomsNames = null;

            using (var context = DepartmentUserManager.GetContext)
            {
                studentGroupsNames = context.StudentGroups.Select(x => x.GroupName).ToList();
                lecturerNames = context.Lecturers.Select(x => $"{x.LastName} {x.FirstName[0]} {x.Patronymic[0]}").ToList();
                classroomsNames = context.Classrooms.Select(x => x.Number).ToList();
            }
            if (studentGroupsNames == null || studentGroupsNames.Count == 0)
            {
                return ResultService.Error("Ошибка", "Список групп пуст", ResultServiceStatusCode.Error);
            }
            if (lecturerNames == null || lecturerNames.Count == 0)
            {
                return ResultService.Error("Ошибка", "Список преподавателей пуст", ResultServiceStatusCode.Error);
            }
            if (classroomsNames == null || classroomsNames.Count == 0)
            {
                return ResultService.Error("Ошибка", "Список аудиторий пуст", ResultServiceStatusCode.Error);
            }

            var resError = new ResultService();
            _findRecords = new List<SemesterRecordSetBindingModel>();

            foreach (var studentGroup in studentGroupsNames)
            {
                await LoadLessons(apiURL, studentGroup, model.ScheduleDate);
            }
            foreach (var lecturer in lecturerNames)
            {
                await LoadLessons(apiURL, lecturer, model.ScheduleDate);
            }
            foreach (var classroom in classroomsNames)
            {
                await LoadLessons(apiURL, classroom, model.ScheduleDate);
            }

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

        private static void GetClient(string url)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static async Task<ResultService> LoadLessons(string url, string filter, DateTime date)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return ResultService.Error("Ошибка", "Фильтр пуст", ResultServiceStatusCode.Error);
            }

            var response = await _client.GetAsync($"{url}?filter={filter}");
            if (!response.IsSuccessStatusCode)
            {
                return ResultService.Error("Ошибка получения данных", $"Не удалось получить ответ по расписанию по фильтру {filter}",
                    ResultServiceStatusCode.Error);
            }
            var res = await response.Content.ReadAsStringAsync();
            var schedules = JsonSerializer.Deserialize<TimeTableAPIScheduleAnswer>(res);
            if (schedules == null || schedules.response == null || schedules.response.weeks == null)
            {
                return ResultService.Error("Ошибка получения данных", $"Не удалось получить данные расписания по фильтру {filter}",
                    ResultServiceStatusCode.Error);
            }

            var resError = new ResultService();
            int week = -1; // 0 - первая неделя, 1 - вторая неделя
            foreach (var scheduleWeek in schedules.response.weeks)
            {
                week++;
                int day = -1;
                foreach (var scheduleDay in scheduleWeek.days)
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
                            var entity = GetRecord(scheduleLesson, date, week, day, lesson);
                            if (entity == null)
                            {
                                continue;
                            }

                            var result = CheckNewSemesterRecordForConflict(entity);
                            if (!result.Succeeded)
                            {
                                foreach (var err in result.Errors)
                                {
                                    resError.AddError(err.Key, err.Value);
                                }
                                continue;
                            }

                            _findRecords.Add(entity);
                        }
                    }
                }
            }

            return resError;
        }

        private static SemesterRecordSetBindingModel GetRecord(TimeTableAPIScheduleRecord scheduleRecord, DateTime date, int week, int day, int lesson)
        {
            var entity = new SemesterRecordSetBindingModel
            {
                Id = Guid.Empty,
                ScheduleDate = ScheduleHelper.GetDateWithTime(date, week, day, lesson),
                Week = week,
                Day = day,
                Lesson = lesson,
                LessonStudentGroup = scheduleRecord.group,
                LessonClassroom = scheduleRecord.room,
                LessonLecturer = scheduleRecord.teacher,
                LessonDiscipline = scheduleRecord.nameOfLesson
            };

            // оперделяем тип занятия
            var matchType = Regex.Match(entity.LessonDiscipline.Trim(), @"^(\w)+\.");
            entity.LessonType = LessonTypes.нд;
            if (matchType.Success)
            {
                switch (matchType.Value.ToLower())
                {
                    case "лек.":
                        entity.LessonType = LessonTypes.лек;
                        break;
                    case "пр.":
                        entity.LessonType = LessonTypes.пр;
                        break;
                    case "лаб.":
                        entity.LessonType = LessonTypes.лаб;
                        break;
                }
                if (entity.LessonType != LessonTypes.нд)
                {
                    entity.LessonDiscipline = entity.LessonDiscipline.Remove(0, matchType.Value.Length).Trim();
                }
            }
            var subgroupMatch = Regex.Match(entity.LessonDiscipline, @"\-(\s)?\d(\s)?(п/г)$");
            if (subgroupMatch.Success)
            {
                entity.LessonDiscipline = entity.LessonDiscipline.Remove(entity.LessonDiscipline.Length - subgroupMatch.Value.Length);
            }
            // может запись уже добавляли в рамках других поисков
            var exsistRec = _findRecords.FirstOrDefault(x => x.Week == entity.Week && x.Day == entity.Day && x.Lesson == entity.Lesson &&
                                x.LessonStudentGroup == entity.LessonStudentGroup && x.LessonClassroom == entity.LessonClassroom &&
                                x.LessonLecturer == entity.LessonLecturer && x.LessonDiscipline == entity.LessonDiscipline);
            if (exsistRec != null)
            {
                return null;
            }

            using (var context = DepartmentUserManager.GetContext)
            {
                ScheduleHelper.GetStudentGroup(context, entity);
                ScheduleHelper.GetClassroom(context, entity);
                ScheduleHelper.GetLecturer(context, entity);
                ScheduleHelper.GetDiscipline(context, entity);
            }
            return entity;
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

            if (string.IsNullOrEmpty(record.LessonLecturer) || string.IsNullOrEmpty(record.LessonStudentGroup) ||
                string.IsNullOrEmpty(record.LessonDiscipline) || string.IsNullOrEmpty(record.LessonClassroom))
            {
                return ResultService.Error("Не все заполнено:", string.Format("дата {0} {1} {2}\r\nГруппа: {3}r\nДисциплина {4}\r\nПреподаватель: {5}r\nАудитория: {6}\r\n",
                    record.Week, record.Day, record.Lesson,
                    record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
            }

            // выбираем уже добавленные записи на эту пару
            var selectRecordsOnDate = _findRecords.Where(x => x.ScheduleDate == record.ScheduleDate);

            //ищем другие занятия этой группы (тип занятия должен совпадать, либо быть неизвестен, тогда предполагаем разибение на подгруппы)
            var exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonStudentGroup == record.LessonStudentGroup);
            if (exsistRecord != null && !(exsistRecord.LessonType == record.LessonType || exsistRecord.LessonType == LessonTypes.нд || record.LessonType == LessonTypes.нд))
            {
                return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                    record.Week, record.Day, record.Lesson,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
            }

            if (!Regex.IsMatch(record.LessonClassroom, @"д(\.)?о(\.)?т(\.)?", RegexOptions.IgnoreCase))
            //ищем другие занятия в этой аудитории (если потоковая пара, то дисциплина и преподаваетль должны совпадать)
            {
                exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonClassroom == record.LessonClassroom);
                if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
                {
                    if (!Regex.IsMatch(exsistRecord.LessonClassroom, @"6(.|..|.. )?-(.|..)?([\d]+([\w]+)*|[\w. ]+)"))
                        return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }
            }

            //ищем другие занятия этого преподавателя
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonLecturer == record.LessonLecturer);
            if (exsistRecord != null && !string.IsNullOrEmpty(record.LessonLecturer) && exsistRecord.LessonClassroom != record.LessonClassroom)
            {
                return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                    record.Week, record.Day, record.Lesson,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);

            }

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
                        var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
                                                    (x.ClassroomId == record.ClassroomId || x.LessonClassroom == record.LessonClassroom) &&
                                                    ((x.DisciplineId == record.DisciplineId && record.DisciplineId != null) || x.LessonDiscipline == record.LessonDiscipline) &&
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

                #region проход по дисциплинам
                var disciplines = context.Disciplines.Where(x => !x.IsDeleted).ToList();
                foreach (var discipline in disciplines)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(x => x.DisciplineId == discipline.Id && !x.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем эту пару в списке загруженных
                        var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
                                                    ((x.ClassroomId == record.ClassroomId && record.ClassroomId != null) || x.LessonClassroom == record.LessonClassroom) &&
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
                        var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
                                                    ((x.ClassroomId == record.ClassroomId && record.ClassroomId != null) || x.LessonClassroom == record.LessonClassroom) &&
                                                    ((x.DisciplineId == record.DisciplineId && record.DisciplineId != null) || x.LessonDiscipline == record.LessonDiscipline) &&
                                                    (x.LecturerId == record.LecturerId || x.LessonLecturer == record.LessonLecturer) &&
                                                    ((x.StudentGroupId == record.StudentGroupId && record.StudentGroupId != null) || x.LessonStudentGroup == record.LessonStudentGroup));

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
                        var searchRecord = _findRecords.FirstOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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