using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using HtmlAgilityPack;
using System;
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
                                                        (node.InnerText.Replace("\r\n", "").Replace(" ", "")));
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
        private static ResultService ParsingPage(string schedulrUrl, string groupName)
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
                            SeasonDatesId = _seasonDate.Id
                        };

                        var entitySecond = new SemesterRecordRecordBindingModel
                        {
                            Week = week,
                            Day = day,
                            Lesson = para,
                            LessonGroup = groupName,
                            SeasonDatesId = _seasonDate.Id
                        };

                        AnalisString(pageNode.InnerText, entityFirst, entitySecond);

                        var result = CheckNewSemesterRecordForConflictAndSave(entityFirst);
                        if (!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                            {
                                resError.AddError(err.Key, err.Value);
                            }
                        }

                        result = CheckNewSemesterRecordForConflictAndSave(entitySecond);
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
        private static ResultService CheckNewSemesterRecordForConflictAndSave(SemesterRecordRecordBindingModel record)
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
                var exsistRecord = _context.SemesterRecords.FirstOrDefault(r => r.Week == record.Week &&
                                        r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == record.SeasonDatesId &&
                                        r.LessonClassroom == record.LessonClassroom && r.LessonGroup != record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой в этой аудитории уже есть занятие
                    if (exsistRecord.LessonDiscipline == record.LessonDiscipline &&
                        exsistRecord.LessonLecturer == record.LessonLecturer &&
                        exsistRecord.LessonType.ToString() == record.LessonType)
                    {//если совпадает дисицпилна, преподаватель и тип занятия, то это потоковое занятие
                        record.IsStreaming = true;
                        exsistRecord.IsStreaming = true;

                        _context.SemesterRecords.Add(ModelFacotryFromBindingModel.CreateSemesterRecord(record, seasonDate: _seasonDate));
                        _context.SaveChanges();

                        return ResultService.Success();
                    }
                    else if (exsistRecord.LessonType == LessonTypes.удл)
                    {//занятие было помечено как удаленное, т.е. по факту пара не проводилась, так что просто удаляем ее
                        _context.SemesterRecords.Remove(exsistRecord);
                        _context.SaveChanges();

                        _context.SemesterRecords.Add(ModelFacotryFromBindingModel.CreateSemesterRecord(record, seasonDate: _seasonDate));
                        _context.SaveChanges();

                        return ResultService.Success();
                    }
                    else
                    {
                        return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                    }
                }

                //ищем занятие этой группы в другой аудитории
                exsistRecord = _context.SemesterRecords.FirstOrDefault(r => r.Week == record.Week &&
                                              r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == record.SeasonDatesId &&
                                              r.LessonGroup == record.LessonGroup && r.LessonClassroom != record.LessonClassroom);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть занятие
                    if (exsistRecord.LessonType.ToString() == record.LessonType ||
                                        exsistRecord.LessonType == LessonTypes.нд || record.LessonType == LessonTypes.нд.ToString())
                    {//если совпадает тип занятия (или у одного из занятий неизвестен тип), но аудитории разные, то это лабораторные по подгруппам
                        _context.SemesterRecords.Add(ModelFacotryFromBindingModel.CreateSemesterRecord(record, seasonDate: _seasonDate));
                        _context.SaveChanges();

                        return ResultService.Success();
                    }
                    else if (exsistRecord.LessonType == LessonTypes.удл)
                    {//занятие было помечено как удаленное, т.е. по факту пара не проводилась, так что просто удаляем ее
                        _context.SemesterRecords.Remove(exsistRecord);
                        _context.SaveChanges();

                        _context.SemesterRecords.Add(ModelFacotryFromBindingModel.CreateSemesterRecord(record, seasonDate: _seasonDate));
                        _context.SaveChanges();

                        return ResultService.Success();
                    }
                    else
                    {
                        return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup), ResultServiceStatusCode.Error);
                    }
                }

                var entity = ModelFacotryFromBindingModel.CreateSemesterRecord(record, seasonDate: _seasonDate);

                _context.SemesterRecords.Add(entity);
                _context.SaveChanges();

                return ResultService.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
