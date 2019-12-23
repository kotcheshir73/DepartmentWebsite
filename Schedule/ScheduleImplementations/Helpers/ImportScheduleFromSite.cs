using DatabaseContext;
using Enums;
using HtmlAgilityPack;
using ScheduleInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Tools;

namespace ScheduleImplementations.Helpers
{
    class ImportScheduleFromSite
    {
        private static List<SemesterRecordSetBindingModel> _findRecords;

        private static readonly string[] days = new string[] { "Пнд", "Втр", "Срд", "Чтв", "Птн", "Сбт", "Вск" };

        public static ResultService ImportHtml(ImportToSemesterRecordsBindingModel model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                WebClient web = new WebClient
                {
                    Encoding = Encoding.Default
                };

                var resError = new ResultService();

                _findRecords = new List<SemesterRecordSetBindingModel>();

                foreach (var url in model.ScheduleUrls)
                {
                    // загружаем страницу с группами и вытасикваем из него таблицу
                    string strHTML = web.DownloadString(url);
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(strHTML);
                    var nodes = document.DocumentNode.SelectNodes("//table/tr/td");

                    foreach (var node in nodes)
                    {
                        if (node.InnerText != "\r\n")
                        {
                            var elem = node.ChildNodes.FirstOrDefault(e => e.Name.ToLower() == "a");
                            if (elem != null)
                            {
                                try
                                {
                                    var res = ParsingPage(url.Replace("raspisan.htm", elem.Attributes.First().Value),
                                                                (node.InnerText.Replace("\r\n", "").Replace(" ", "")), model.ScheduleDate);
                                    if (!res.Succeeded)
                                    {
                                        foreach (var err in res.Errors)
                                        {
                                            resError.AddError(err.Key, err.Value);
                                        }
                                    }
                                    Thread.Sleep(100);
                                }
                                catch (Exception ex)
                                {
                                    resError.AddError(ex);
                                }
                            }
                        }
                    }
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
            WebClient web = new WebClient
            {
                Encoding = Encoding.Default
            };
            string pageHTML = web.DownloadString(schedulrUrl);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageHTML);
            var pageNodes = document.DocumentNode.SelectNodes("//table/tr/td");

            int week = -1; // 0 - первая неделя, 1 - вторая неделя
            int day = -1;
            int lesson = -1;
            var resError = new ResultService();
            foreach (var pageNode in pageNodes)
            {
                string text = pageNode.InnerText.Replace("\r\n", "").Replace(" ", "");
                if (days.Contains(text))
                {
                    if (days[0].Contains(text))
                    {
                        week++;
                        day = -1;
                    }
                    day++;
                    lesson = -1;
                }
                if (week > -1)
                {
                    var elem = pageNode.ChildNodes.First().NextSibling;
                    if (elem.Name.ToLower() == "font")
                    {
                        lesson++;
                        var info = pageNode.InnerText.Replace("\r\n", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (string.IsNullOrEmpty(info[0]))
                        {
                            // пустая пара, переходим к следующей
                            continue;
                        }
                        var entity = new SemesterRecordSetBindingModel
                        {
                            Id = Guid.Empty,
                            ScheduleDate = ScheduleHelper.GetDateWithTime(date, week, day, lesson),
                            Week = week,
                            Day = day,
                            Lesson = lesson,
                            LessonStudentGroup = groupName,
                            NotParseRecord = pageNode.InnerText
                        };

                        var list = new List<SemesterRecordSetBindingModel> { entity };

                        AnalisString(pageNode.InnerText, list);

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

            text = Regex.Replace(text, @"(\-?)(\s?)\d(\s?)п/г", "").Replace("\r\n", "").TrimStart();

            using (var context = DepartmentUserManager.GetContext)
            {
                //определяем группу
                if (!string.IsNullOrEmpty(records[0].LessonStudentGroup))
                {
                    var group = context.StudentGroups.FirstOrDefault(x => x.GroupName == records[0].LessonStudentGroup && !x.IsDeleted);
                    if (group != null)
                    {
                        records[0].StudentGroupId = group.Id;
                    }
                }

                // отсекаем физ-ру первым делом
                if (text.Contains("Элективные курсы по физичeской культуре и спорту"))
                {
                    records[0].LessonDiscipline = "Физкультура";
                    records[0].LessonType = LessonTypes.пр;
                    var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == records[0].LessonDiscipline);
                    if (discipline != null)
                    {
                        records[0].DisciplineId = discipline.Id;
                    }
                    return;
                }

                // ищем в записе наличие аудиторий
                var classroomMatches = Regex.Matches(text, @"\d(_|-)[\d]+(/[\d]+)*([\w]+)*");

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
                    var classroom = context.Classrooms.FirstOrDefault(c => records[clM].LessonClassroom.Contains(c.Number) && !c.IsDeleted);
                    if (classroom != null)
                    {
                        records[clM].ClassroomId = classroom.Id;
                    }

                    // убираем из текста номер аудитории, остается предмет и преподаватель
                    var lessonText = text.Substring(0, text.IndexOf(records[clM].LessonClassroom)).TrimEnd();

                    // вычисляем преподавателя
                    records[clM].LessonLecturer = Regex.Match(lessonText, @"([\w]+ \w \w)$").Value;

                    if (!string.IsNullOrEmpty(records[clM].LessonLecturer))
                    {
                        var spliters = records[clM].LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
                        string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
                        string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
                        var lecturer = context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
                                                ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                                                ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                        if (lecturer != null)
                        {
                            records[clM].LecturerId = lecturer.Id;
                        }
                    }

                    // оставляем только название предмета
                    records[clM].LessonDiscipline = lessonText.Substring(0, lessonText.IndexOf(records[clM].LessonLecturer)).TrimEnd();

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
                        var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == records[clM].LessonDiscipline);
                        if (discipline != null)
                        {
                            records[clM].DisciplineId = discipline.Id;
                        }
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
            try
            {
                // если у пары не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
                // то такая пара нас не интересует
                if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null && record.DisciplineId == null)
                {
                    return ResultService.Success();
                }

                // выбираем уже добавленные записи на эту пару
                var selectRecordsOnDate = _findRecords.Where(x => x.ScheduleDate == record.ScheduleDate);

                //ищем другие занятия в этой аудитории (если потоковая пара, то дисциплина и преподаваетль должны совпадать)
                var exsistRecord = selectRecordsOnDate.FirstOrDefault(r => r.LessonClassroom == record.LessonClassroom);
                if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
                {
                    return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                        record.Week, record.Day, record.Lesson,
                        exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                //ищем другие занятия этой группы (тип занятия должен совпадать, либо быть неизвестен, тогда предполагаем разибение на подгруппы)
                exsistRecord = selectRecordsOnDate.FirstOrDefault(r => r.LessonStudentGroup == record.LessonStudentGroup);
                if (exsistRecord != null && !(exsistRecord.LessonType == record.LessonType || exsistRecord.LessonType == LessonTypes.нд || record.LessonType == LessonTypes.нд))
                {
                    return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                        record.Week, record.Day, record.Lesson,
                        exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
                }

                //ищем другие занятия этого преподавателя
                exsistRecord = selectRecordsOnDate.FirstOrDefault(r => r.LessonLecturer == record.LessonLecturer);
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
            catch (Exception)
            {
                throw;
            }
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

                var deletedRecords = exsistRecords.Where(x => !x.Checked).ToList();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (deletedRecords.Count > 0)
                        { // удаляем неопознанные
                            context.SemesterRecords.RemoveRange(deletedRecords);
                            context.SaveChanges();
                        }

                        // получаем опознанные
                        var knowRecords = _findRecords.Where(x => x.Id != Guid.Empty).ToList();
                        foreach (var record in knowRecords)
                        {
                            var entity = context.SemesterRecords.FirstOrDefault(e => e.Id == record.Id);
                            if (entity == null)
                            {
                                return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                            }

                            record.ScheduleDate = model.ScheduleDate;
                            entity = ScheduleModelFacotryFromBindingModel.CreateRecord(record, entity);
                            context.SaveChanges();
                        }

                        // получаем новые
                        var unknowRecords = _findRecords.Where(x => x.Id == Guid.Empty).ToList();
                        foreach (var record in unknowRecords)
                        {
                            record.ScheduleDate = model.ScheduleDate;
                            var entity = record.CreateRecord();

                            context.SemesterRecords.Add(entity);
                            context.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ResultService.Error("Конфликт при сохранении:", ex.Message, ResultServiceStatusCode.Error);
                    }
                }

                return ResultService.Success();
            }
        }
    }
}