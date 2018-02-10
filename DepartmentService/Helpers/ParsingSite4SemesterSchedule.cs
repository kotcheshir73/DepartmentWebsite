using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DepartmentService.Helpers
{
    class ParsingSite4SemesterSchedule
    {
        private static DepartmentDbContext _context;

        private static SeasonDates _seasonDate;

        private static List<SemesterRecordRecordBindingModel> _findRecords;

        public static ResultService ImportHtml(DepartmentDbContext context, ImportToSemesterFromHTMLBindingModel model)
        {
            _context = context;
            _seasonDate = ScheduleHelper.GetCurrentDates();
            WebClient web = new WebClient
            {
                Encoding = Encoding.Default
            };

            string strHTML = web.DownloadString(model.ScheduleUrl + "raspisan.htm");

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(strHTML);

            var nodes = document.DocumentNode.SelectNodes("//table/tr/td");
            var resError = new ResultService();

            _findRecords = new List<SemesterRecordRecordBindingModel>();

            foreach (var node in nodes)
            {
                if (node.InnerText != "\r\n")
                {
                    var elem = node.ChildNodes.FirstOrDefault(e => e.Name.ToLower() == "a");
                    if (elem != null)
                    {
                        try
                        {
                            var res = ParsingPage(model.ScheduleUrl + elem.Attributes.First().Value,
                                                        (node.InnerText.Replace("\r\n", "").Replace(" ", "")), model.IsFirstHalfSemester);
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
        private static ResultService ParsingPage(string schedulrUrl, string groupName, bool isFirstHalfSemester)
        {
            string[] days = new string[] { "Пнд", "Втр", "Срд", "Чтв", "Птн", "Сбт" };
            WebClient web = new WebClient
            {
                Encoding = Encoding.Default
            };
            string pageHTML = web.DownloadString(schedulrUrl);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageHTML);
            var pageNodes = document.DocumentNode.SelectNodes("//table/tr/td");
            int week = -1;
            int day = -1;
            int para = -1;
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
                    para = -1;
                }
                if (week > -1)
                {
                    var elem = pageNode.ChildNodes.First().NextSibling;
                    if (elem.Name.ToLower() == "font")
                    {
                        para++;
                        var lesson = pageNode.InnerText.Replace("\r\n", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (lesson[0] == "_")
                        {
                            // пустая пара, переходим к следующей
                            continue;
                        }
                        var entityFirst = new SemesterRecordRecordBindingModel
                        {
                            Week = week,
                            Day = day,
                            Lesson = para,
                            LessonGroup = groupName,
                            SeasonDatesId = _seasonDate.Id,
                            IsFirstHalfSemester = isFirstHalfSemester
                        };

                        var entitySecond = new SemesterRecordRecordBindingModel
                        {
                            Week = week,
                            Day = day,
                            Lesson = para,
                            LessonGroup = groupName,
                            SeasonDatesId = _seasonDate.Id,
                            IsFirstHalfSemester = isFirstHalfSemester
                        };

                        AnalisString(pageNode.InnerText, entityFirst, entitySecond);

                        var result = CheckNewSemesterRecordForConflict(entityFirst);
                        if (!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                            {
                                resError.AddError(err.Key, err.Value);
                            }
                        }

                        result = CheckNewSemesterRecordForConflict(entitySecond);
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
            return resError;
        }

        /// <summary>
        /// Разбор ячейки расписания. Пытаемся получить название дисциплины, ФИО преподавателя и номер аудитории
        /// </summary>
        /// <param name="text"></param>
        /// <param name="recordFirst"></param>
        /// <param name="recordSecond"></param>
        private static void AnalisString(string text, SemesterRecordRecordBindingModel recordFirst, SemesterRecordRecordBindingModel recordSecond)
        {
            recordFirst.NotParseRecord = recordSecond.NotParseRecord = text;
            text = Regex.Replace(text, @"(\-?)(\s?)\d(\s?)п/г", "").Replace("\r\n", "").TrimStart();

            // ищем в записе наличие аудиторий
            var classroomMatches = Regex.Matches(text, @"а.(\w{0,2})[\d]+(\-\d)*(\/\d)*");
            if (classroomMatches.Count == 0)
            {
                if (text.StartsWith("Физкультура"))
                {
                    recordFirst.LessonDiscipline = "Физкультура";
                    recordFirst.LessonType = LessonTypes.нд.ToString();
                    if (!string.IsNullOrEmpty(recordFirst.LessonGroup))
                    {
                        var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == recordFirst.LessonGroup && !sg.IsDeleted);
                        if (group != null)
                        {
                            recordFirst.StudentGroupId = group.Id;
                        }
                    }
                }
                return;
            }

            for (int clM = 0; clM < classroomMatches.Count; ++clM)
            {
                var currentRecord = clM == 0 ? recordFirst : recordSecond;
                currentRecord.LessonClassroom = classroomMatches[clM].Value;
                var classroom = _context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Number) && !c.IsDeleted);
                if (classroom != null)
                {
                    currentRecord.ClassroomId = classroom.Id;
                }
                // убираем из текста номер аудитории, остается предмет и преподаватель
                var lessonText = text.Substring(0, text.IndexOf(currentRecord.LessonClassroom)).TrimEnd();

                // маленький костыль для второй записи
                if (lessonText.EndsWith("-"))
                {
                    lessonText = lessonText.Substring(0, lessonText.Length - 1).TrimEnd();
                }

                // вычисляем преподавателя
                currentRecord.LessonLecturer = Regex.Match(lessonText, @"[\w]+(\ (\w(\.)?)?)?(\ (\w(\.)?)?)?$").Value;

                if (!string.IsNullOrEmpty(currentRecord.LessonLecturer))
                {
                    var spliters = currentRecord.LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
                    string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
                    string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
                    var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
                                            ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                                            ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                    if (lecturer != null)
                    {
                        currentRecord.LecturerId = lecturer.Id;
                    }
                }

                // оставляем только название предмета
                currentRecord.LessonDiscipline = lessonText.Substring(0, lessonText.IndexOf(currentRecord.LessonLecturer)).TrimEnd();

                // оперделяем тип занятия
                currentRecord.LessonType = LessonTypes.нд.ToString();
                if (currentRecord.LessonDiscipline.StartsWith("лек."))
                {
                    currentRecord.LessonType = LessonTypes.лек.ToString();
                    currentRecord.LessonDiscipline = currentRecord.LessonDiscipline.Remove(0, 4);
                }
                if (currentRecord.LessonDiscipline.StartsWith("пр."))
                {
                    currentRecord.LessonType = LessonTypes.пр.ToString();
                    currentRecord.LessonDiscipline = currentRecord.LessonDiscipline.Remove(0, 3);
                }
                if (currentRecord.LessonDiscipline.StartsWith("лаб."))
                {
                    currentRecord.LessonType = LessonTypes.лаб.ToString();
                    currentRecord.LessonDiscipline = currentRecord.LessonDiscipline.Remove(0, 4);
                }

                // определяем дисциплину
                if (!string.IsNullOrEmpty(currentRecord.LessonDiscipline))
                {
                    var shortName = ScheduleHelper.CalcShortDisciplineName(currentRecord.LessonDiscipline);
                    var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineShortName == shortName);
                    if (discipline != null)
                    {
                        currentRecord.DisciplineId = discipline.Id;
                    }
                }

                //определяем группу
                if (!string.IsNullOrEmpty(currentRecord.LessonGroup))
                {
                    var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == currentRecord.LessonGroup && !sg.IsDeleted);
                    if (group != null)
                    {
                        currentRecord.StudentGroupId = group.Id;
                    }
                }
                text = text.Substring(text.IndexOf(currentRecord.LessonClassroom) + currentRecord.LessonClassroom.Length).TrimStart();
            }
        }

        /// <summary>
        /// Проверяем добавляемую пару на конфликты
        /// </summary>
        /// <param name="record"></param>
        private static ResultService CheckNewSemesterRecordForConflict(SemesterRecordRecordBindingModel record)
        {
            try
            {
                // если у пары не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
                // то такая пара нас не интересует
                if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null && record.DisciplineId == null)
                {
                    return ResultService.Success();
                }

                //ищем занятие другой группы в этой аудитории
                var exsistRecord = _findRecords.FirstOrDefault(r => r.Week == record.Week && r.Day == record.Day && r.Lesson == record.Lesson &&
                                        r.LessonClassroom == record.LessonClassroom && r.LessonGroup != record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой в этой аудитории уже есть занятие
                    if (exsistRecord.LessonDiscipline == record.LessonDiscipline &&
                        exsistRecord.LessonLecturer == record.LessonLecturer &&
                        exsistRecord.LessonType == record.LessonType)
                    {//если совпадает дисицпилна, преподаватель и тип занятия, то это потоковое занятие
                        record.IsStreaming = true;
                        exsistRecord.IsStreaming = true;
                    }
                    else
                    {
                        return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                    }
                }

                //ищем занятие этой группы в другой аудитории
                exsistRecord = _findRecords.FirstOrDefault(r => r.Week == record.Week && r.Day == record.Day && r.Lesson == record.Lesson &&
                                              r.LessonGroup == record.LessonGroup && r.LessonClassroom != record.LessonClassroom);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть занятие
                    if (exsistRecord.LessonType == record.LessonType ||
                                        exsistRecord.LessonType == LessonTypes.нд.ToString() || record.LessonType == LessonTypes.нд.ToString())
                    {//если совпадает тип занятия (или у одного из занятий неизвестен тип), но аудитории разные, то это лабораторные по подгруппам
                        exsistRecord.IsSubgroup = true;
                        record.IsSubgroup = true;
                    }
                    else
                    {
                        return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup), ResultServiceStatusCode.Error);
                    }
                }

                //ищем занятие этого преподавателя в другой аудитории
                exsistRecord = _findRecords.FirstOrDefault(r => r.Week == record.Week && r.Day == record.Day && r.Lesson == record.Lesson &&
                                              r.LessonLecturer == record.LessonLecturer && r.LessonClassroom != record.LessonClassroom);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть занятие
                    {
                        return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup), ResultServiceStatusCode.Error);
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
        private static ResultService SaveRecords(ImportToSemesterFromHTMLBindingModel model)
        {
            // получаем сущестующие записи
            var exsistRecords = _context.SemesterRecords.Where(sr => sr.SeasonDatesId == _seasonDate.Id && sr.IsFirstHalfSemester == model.IsFirstHalfSemester).ToList();

            #region для начала проходим по аудиториям
            var classrooms = _context.Classrooms.Where(c => !c.IsDeleted && !c.NotUseInSchedule).ToList();
            foreach (var classroom in classrooms)
            {
                var selectedRecords = exsistRecords.Where(sr => sr.ClassroomId == classroom.Id).ToList();
                foreach (var record in selectedRecords)
                {
                    // ищем пары (которые еще не опознаны) в этот день в этой аудитории
                    var searchRecords = _findRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
                                                            rec.Id == Guid.Empty &&
                                                            (rec.ClassroomId == record.ClassroomId || rec.LessonClassroom == record.LessonClassroom))
                                                    .ToList();
                    // если пара одна
                    if (searchRecords.Count == 1)
                    {
                        searchRecords[0].Id = record.Id;
                        record.Checked = true;
                    }
                    // если пар несколько (проверяем, что потоковые)
                    else if (searchRecords.Count > 1)
                    {
                        var notStreamRecrods = searchRecords.Where(rec => !rec.IsStreaming).FirstOrDefault();
                        if (notStreamRecrods != null)
                        {
                            return ResultService.Error("Конфликт (аудитории) не потоковая:", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                                notStreamRecrods.Week, notStreamRecrods.Day, notStreamRecrods.Lesson, notStreamRecrods.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                        // ищем потоковую пару по группе
                        var streamRecord = searchRecords.Where(rec =>
                                                        (rec.LessonGroup == record.LessonGroup || rec.StudentGroupId == record.StudentGroupId)).FirstOrDefault();
                        if (streamRecord != null)
                        {
                            streamRecord.Id = record.Id;
                            record.Checked = true;
                        }
                    }
                }
            }
            #endregion

            #region проход по группам
            var groups = _context.StudentGroups.Where(sg => !sg.IsDeleted).ToList();
            foreach (var group in groups)
            {
                //отбираем еще не проверенные записи
                var selectedRecords = exsistRecords.Where(sr => sr.StudentGroupId == group.Id && !sr.Checked).ToList();
                foreach (var record in selectedRecords)
                {
                    // ищем пары (которые еще не опознаны) в этот день в этой группе
                    var searchRecords = _findRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
                                                            rec.Id == Guid.Empty &&
                                                            (rec.StudentGroupId == record.StudentGroupId || rec.LessonGroup == record.LessonGroup))
                                                    .ToList();
                    // если пара одна
                    if (searchRecords.Count == 1)
                    {
                        searchRecords[0].Id = record.Id;
                        record.Checked = true;
                    }
                    // если пар несколько (проверяем, что лабораторные)
                    else if (searchRecords.Count > 1)
                    {
                        // ищем пару группы в аудитории
                        var labRecrod = searchRecords.Where(rec =>
                                                        (rec.LessonClassroom == record.LessonClassroom || rec.ClassroomId == record.ClassroomId)).FirstOrDefault();
                        if (labRecrod != null)
                        {
                            labRecrod.Id = record.Id;
                            record.Checked = true;
                        }
                    }
                }
            }
            #endregion

            #region проход по преподавателям
            var lecturers = _context.Lecturers.Where(l => !l.IsDeleted).ToList();
            foreach (var lecturer in lecturers)
            {
                //отбираем еще не проверенные записи
                var selectedRecords = exsistRecords.Where(sr => sr.LecturerId == lecturer.Id && !sr.Checked).ToList();
                foreach (var record in selectedRecords)
                {
                    // ищем пары (которые еще не опознаны) в этот день этого преподавателя
                    var searchRecords = _findRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
                                                            rec.Id == Guid.Empty &&
                                                            (rec.LecturerId == record.LecturerId || rec.LessonLecturer == record.LessonLecturer))
                                                    .ToList();
                    // если пара одна
                    if (searchRecords.Count == 1)
                    {
                        searchRecords[0].Id = record.Id;
                        record.Checked = true;
                    }
                    // если пар несколько (проверяем, что потоковые)
                    else if (searchRecords.Count > 1)
                    {
                        var notStreamRecrods = searchRecords.Where(rec => !rec.IsStreaming).FirstOrDefault();
                        if (notStreamRecrods != null)
                        {
                            return ResultService.Error("Конфликт (преподаватель) не потоковая:", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                                notStreamRecrods.Week, notStreamRecrods.Day, notStreamRecrods.Lesson, notStreamRecrods.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                        // ищем потоковую пару по группе
                        var streamRecrod = searchRecords.Where(rec =>
                                                        (rec.LessonGroup == record.LessonGroup || rec.StudentGroupId == record.StudentGroupId)).FirstOrDefault();
                        if (streamRecrod != null)
                        {
                            streamRecrod.Id = record.Id;
                            record.Checked = true;
                        }
                    }
                }
            }
            #endregion

            #region проход по дисциплинам
            var disciplines = _context.Disciplines.Where(d => !d.IsDeleted).ToList();
            foreach (var discipline in disciplines)
            {
                //отбираем еще не проверенные записи
                var selectedRecords = exsistRecords.Where(sr => sr.DisciplineId == discipline.Id && !sr.Checked).ToList();
                foreach (var record in selectedRecords)
                {
                    // ищем пары (которые еще не опознаны) в этот день в этой группе
                    var searchRecords = _findRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
                                                            rec.Id == Guid.Empty &&
                                                            (rec.DisciplineId == record.DisciplineId || rec.LessonDiscipline == record.LessonDiscipline))
                                                    .ToList();
                    // если пара одна
                    if (searchRecords.Count == 1)
                    {
                        searchRecords[0].Id = record.Id;
                        record.Checked = true;
                    }
                    // если пар несколько (проверяем, что потоковые)
                    else if (searchRecords.Count > 1)
                    {
                        var notStreamRecrods = searchRecords.Where(rec => !rec.IsStreaming).FirstOrDefault();
                        if (notStreamRecrods != null)
                        {
                            return ResultService.Error("Конфликт (дисциплина) не потоковая:", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                                notStreamRecrods.Week, notStreamRecrods.Day, notStreamRecrods.Lesson, notStreamRecrods.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                        // ищем потоковую пару по группе
                        var streamRecrod = searchRecords.Where(rec =>
                                                        (rec.LessonGroup == record.LessonGroup || rec.StudentGroupId == record.StudentGroupId)).FirstOrDefault();
                        if (streamRecrod != null)
                        {
                            streamRecrod.Id = record.Id;
                            record.Checked = true;
                        }
                    }
                }
            }
            #endregion

            var deletedRecords = exsistRecords.Where(sr => !sr.Checked).ToList();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // удаляем неопознанные
                    _context.SemesterRecords.RemoveRange(deletedRecords);
                    _context.SaveChanges();

                    // получаем опознанные
                    var knowRecords = _findRecords.Where(sr => sr.Id != Guid.Empty).ToList();
                    foreach(var record in knowRecords)
                    {
                        var entity = _context.SemesterRecords.FirstOrDefault(e => e.Id == record.Id);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                        }

                        entity = ModelFacotryFromBindingModel.CreateSemesterRecord(record, entity);
                        _context.SaveChanges();
                    }

                    // получаем новые
                    var unknowRecords = _findRecords.Where(sr => sr.Id == Guid.Empty).ToList();
                    foreach (var record in unknowRecords)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateSemesterRecord(record, seasonDate: _seasonDate);

                        _context.SemesterRecords.Add(entity);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return ResultService.Error("Конфликт при сохранении:", ex.Message, ResultServiceStatusCode.Error);
                }
            }

            return ResultService.Success();
        }
    }
}
