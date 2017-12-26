using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DepartmentService.Helpers
{
    class ImportScheduleFromExcel
    {
        private static DepartmentDbContext _context;

        private static SeasonDates _seasonDate;

        public static ResultService ImportOffsets(DepartmentDbContext context, ImportToOffsetFromExcel model)
        {
            try
            {
                _context = context;
                _seasonDate = ScheduleHelper.GetCurrentDates();

                var excel = new Application();
                var resError = new ResultService();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                        for (int w = 0; w < workbook.Worksheets.Count; ++w)
                        {
                            var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(w + 1);//Получаем ссылку на лист
                            var excelcell = excelworksheet.get_Range("A2", "A2");

                            // заведем прерываетль, чтобы прекратить обход, если лист пустой
                            int counter = 0;
                            // идем вниз по первой колонки, пока не встретим текст
                            while (excelcell.Value2 == null)
                            {
                                excelcell = excelcell.get_Offset(1, 0);
                                counter++;
                                if (counter > 10)
                                    break;
                            }
                            counter = 0;
                            while (excelcell.Value2 != null && excelcell.Value2.ToString().ToLower() == "дни недели")
                            {
                                counter++;
                                if (counter > 10)
                                    break;
                                // идем по первой строке с группами
                                // берем имя группы
                                var excelGroupNameCell = excelcell.get_Offset(0, 1);
                                while (excelGroupNameCell.Value2 != null)
                                {
                                    // в день может быть 2 зачета, 6 дней зачетной недели, получается 12 шагов
                                    for (int i = 0; i < 12; ++i)
                                    {
                                        // под каждый зачет выделяется 3 строки
                                        // в первой строке - название зачета (за искл. физ-ры)
                                        var excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 1, 0);
                                        if (excelDiscNameCell.Value2 != null)
                                        {
                                            if (!Regex.IsMatch(excelDiscNameCell.Value2.ToString(), @"\w+"))
                                            {
                                                continue;
                                            }
                                            var excelLecturerName = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                                            var excelLessonAndClassroomsName = excelGroupNameCell.get_Offset(i * 3 + 3, 0);
                                            var currentRecord = new OffsetRecordRecordBindingModel
                                            {
                                                Week = 0,
                                                Day = i / 2,
                                                LessonDiscipline = excelDiscNameCell.Value2,
                                                LessonGroup = excelGroupNameCell.Value2,
                                                LessonLecturer = excelLecturerName.Value2
                                            };

                                            // определяем группу
                                            var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName.ToLower() == currentRecord.LessonGroup.ToLower() && !sg.IsDeleted);
                                            if (group != null)
                                            {
                                                currentRecord.StudentGroupId = group.Id;
                                            }

                                            // определяем дисциплину
                                            var shortName = ScheduleHelper.CalcShortDisciplineName(currentRecord.LessonDiscipline);
                                            var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineShortName == shortName);
                                            if (discipline != null)
                                            {
                                                currentRecord.DisciplineId = discipline.Id;
                                            }

                                            // определяем преподавателя
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

                                            // определяем пары и аудитории
                                            string lessonsAndClassroom = excelLessonAndClassroomsName.Value2.ToLower();
                                            var lessonsMassive = Regex.Match(lessonsAndClassroom, @"(\dп(\.)*(\,)*(\ )*)+");
                                            var classroomMatches = Regex.Matches(Regex.Replace(lessonsAndClassroom, @"(\dп(\.)*(\,)*(\ )*)+", "").Replace(" ", ""), @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                                            var lessons = Regex.Matches(lessonsMassive.Value, @"\d");
                                            for (int j = 0; j < lessons.Count; ++j)
                                            {
                                                var lesson = lessons[j].Value;
                                                currentRecord.Lesson = Convert.ToInt32(Regex.Match(lesson, @"\d").Value) - 1;
                                                for (int k = 0; k < classroomMatches.Count; ++k)
                                                {
                                                    currentRecord.LessonClassroom = classroomMatches[k].Value;
                                                    var classroom = _context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Id) && !c.IsDeleted);
                                                    if (classroom != null)
                                                    {
                                                        currentRecord.ClassroomId = classroom.Id;
                                                    }
                                                    var result = CheckNewOffsetRecordForConflictAndSave(currentRecord);
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
                                        // физ-ра, чтоб ее
                                        else
                                        {
                                            excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                                            if (excelDiscNameCell.Value2 != null)
                                            {
                                                if (excelDiscNameCell.Value2 == "Элективный курс по ФК" || excelDiscNameCell.Value2 == "ФИЗ – РА")
                                                {
                                                    var currentRecord = new OffsetRecordRecordBindingModel
                                                    {
                                                        Week = 0,
                                                        Day = i / 2,
                                                        LessonDiscipline = "Физкультура",
                                                        LessonGroup = excelGroupNameCell.Value2,
                                                        LessonLecturer = ""
                                                    };
                                                    if (!string.IsNullOrEmpty(currentRecord.LessonGroup))
                                                    {
                                                        var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == currentRecord.LessonGroup && !sg.IsDeleted);
                                                        if (group != null)
                                                        {
                                                            currentRecord.StudentGroupId = group.Id;
                                                        }
                                                        var result = CheckNewOffsetRecordForConflictAndSave(currentRecord);
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
                                    // переходим к следующей группе
                                    excelGroupNameCell = excelGroupNameCell.get_Offset(0, 1);
                                }
                                excelcell = excelcell.get_Offset(37, 0);
                            }
                        }

                        transaction.Commit();
                        return resError;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        excel.Quit();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        /// <summary>
        /// Проверяем добавляемый зачет на конфликты
        /// </summary>
        /// <param name="record"></param>
        private static ResultService CheckNewOffsetRecordForConflictAndSave(OffsetRecordRecordBindingModel record)
        {
            try
            {
                // если у пары не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
                // то такая пара нас не интересует
                if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null && record.DisciplineId == null)
                {
                    return ResultService.Success();
                }

                // если уже есть такой зачет, то ничего не делаем
                var exsistRecord = _context.OffsetRecords.FirstOrDefault(r => r.Week == record.Week &&
                                        r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == _seasonDate.Id &&
                                        r.LessonClassroom == record.LessonClassroom && r.LessonGroup == record.LessonGroup &&
                                        r.LessonDiscipline == record.LessonDiscipline && r.LessonLecturer == record.LessonLecturer);
                if (exsistRecord != null)
                {
                    return ResultService.Success();
                }

                //ищем зачет другой группы в этой аудитории
                if (!string.IsNullOrEmpty(record.LessonClassroom))
                {
                    exsistRecord = _context.OffsetRecords.FirstOrDefault(r => r.Week == record.Week &&
                                   r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == _seasonDate.Id &&
                                   r.LessonClassroom == record.LessonClassroom && r.LessonGroup != record.LessonGroup);
                    if (exsistRecord != null)
                    {//если на этой неделе в этот день этой парой в этой аудитории уже есть зачет
                        return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                    }
                }

                //ищем зачет этой группы в другой аудитории
                exsistRecord = _context.OffsetRecords.FirstOrDefault(r => r.Week == record.Week &&
                                              r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == _seasonDate.Id &&
                                              r.LessonGroup == record.LessonGroup && r.LessonClassroom != record.LessonClassroom);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть занятие
                    return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                        record.Week, record.Day, record.Lesson,
                        exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup), ResultServiceStatusCode.Error);
                }

                var entity = ModelFacotryFromBindingModel.CreateOffsetRecord(record, seasonDate: _seasonDate);

                _context.OffsetRecords.Add(entity);
                _context.SaveChanges();

                return ResultService.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ResultService ImportExaminations(DepartmentDbContext context, ImportToExaminationFromExcel model)
        {
            try
            {
                _context = context;
                _seasonDate = ScheduleHelper.GetCurrentDates();
                var dateBeginExamination = Convert.ToDateTime(_seasonDate.DateBeginExamination);
                var lessons = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("экзамен") || slt.Title.Contains("консультация")).ToList();

                var excel = new Application();
                var resError = new ResultService();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                        for (int w = 0; w < workbook.Worksheets.Count; ++w)
                        {
                            var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(w + 1);//Получаем ссылку на лист
                            var excelcell = excelworksheet.get_Range("A2", "A2");

                            // заведем прерываетль, чтобы прекратить обход, если лист пустой
                            int counter = 0;
                            // идем вниз по первой колонки, пока не встретим текст
                            while (excelcell.Value2 == null)
                            {
                                excelcell = excelcell.get_Offset(1, 0);
                                counter++;
                                if (counter > 10)
                                    break;
                            }
                            counter = 0;
                            while (excelcell.Value2 != null && excelcell.Value2.ToString().ToLower() == "дни недели")
                            {
                                counter++;
                                if (counter > 10)
                                    break;
                                // идем по первой строке с группами
                                // берем имя группы
                                var excelGroupNameCell = excelcell.get_Offset(0, 1);
                                while (excelGroupNameCell.Value2 != null)
                                {
                                    DateTime? dayConsult = null;
                                    string LessonConsultationClassroom = string.Empty;
                                    string ConsultationClassroomId = string.Empty;
                                    // 3 недели экзаменов = 21 день, по 3 ячейки на день
                                    for (int i = 0; i < 21; ++i)
                                    {
                                        // в первой строке - название экзамена
                                        var excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 1, 0);
                                        if (excelDiscNameCell.Value2 != null)
                                        {
                                            if (!Regex.IsMatch(excelDiscNameCell.Value2.ToString(), @"\w+"))
                                            {
                                                continue;
                                            }
                                            var excelLecturerName = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                                            var excelTimeAndClassroomsName = excelGroupNameCell.get_Offset(i * 3 + 3, 0);
                                            if (!dayConsult.HasValue)
                                            {
                                                resError.AddError("Не найдена дата консультации", string.Format("{0} {1} {2}", dateBeginExamination.AddDays(i).ToShortDateString(),
                                                    excelLecturerName.Value2, excelDiscNameCell.Value2));
                                            }
                                            var currentRecord = new ExaminationRecordRecordBindingModel
                                            {
                                                DateConsultation = dayConsult.Value,
                                                DateExamination = dateBeginExamination.AddDays(i),
                                                LessonDiscipline = excelDiscNameCell.Value2,
                                                LessonGroup = excelGroupNameCell.Value2,
                                                LessonLecturer = excelLecturerName.Value2
                                            };

                                            if (!string.IsNullOrEmpty(LessonConsultationClassroom))
                                            {
                                                currentRecord.LessonConsultationClassroom = LessonConsultationClassroom;
                                            }
                                            if (!string.IsNullOrEmpty(ConsultationClassroomId))
                                            {
                                                currentRecord.ConsultationClassroomId = ConsultationClassroomId;
                                            }

                                            // определяем группу
                                            var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == currentRecord.LessonGroup && !sg.IsDeleted);
                                            if (group != null)
                                            {
                                                currentRecord.StudentGroupId = group.Id;
                                            }

                                            // определяем дисциплину
                                            var shortName = ScheduleHelper.CalcShortDisciplineName(currentRecord.LessonDiscipline);
                                            var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineShortName == shortName);
                                            if (discipline != null)
                                            {
                                                currentRecord.DisciplineId = discipline.Id;
                                            }

                                            // определяем преподавателя
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

                                            // определяем время и аудиторию
                                            string timeAndClassroom = excelTimeAndClassroomsName.Value2.ToLower();
                                            var timeMatch = Regex.Match(timeAndClassroom, @"\d{1,2}[(\:)*(\.)*(\-)*]+\d{2}"); if (timeMatch.Success)
                                            {
                                                timeAndClassroom = timeAndClassroom.Replace(timeMatch.Value, "");
                                                currentRecord.DateExamination = currentRecord.DateExamination.AddHours(Convert.ToInt32(Regex.Match(timeMatch.Value, @"^\d{1,2}").Value))
                                                                                    .AddMinutes(Convert.ToInt32(Regex.Match(timeMatch.Value, @"\d{2}$").Value));
                                                if (currentRecord.DateExamination.ToShortTimeString() !=
                                                    lessons.FirstOrDefault(l => l.Title.Contains("Дневной")).DateBeginLesson.ToShortTimeString())
                                                {
                                                    resError.AddError("Неверное время дневного экзамена", string.Format("{0} {1} {2} {3}", currentRecord.DateExamination.ToShortDateString(),
                                                        currentRecord.DateExamination.ToShortTimeString(), excelLecturerName.Value2, excelDiscNameCell.Value2));
                                                }
                                            }
                                            else
                                            {
                                                var time = lessons.FirstOrDefault(l => l.Title.Contains("Утренний")).DateBeginLesson;
                                                currentRecord.DateExamination = currentRecord.DateExamination.AddHours(time.Hour).AddMinutes(time.Minute);
                                            }
                                            var classroomMatch = Regex.Match(timeAndClassroom, @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                                            if (classroomMatch.Success)
                                            {
                                                currentRecord.LessonClassroom = classroomMatch.Value;
                                                var classroom = _context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Id) && !c.IsDeleted);
                                                if (classroom != null)
                                                {
                                                    currentRecord.ClassroomId = classroom.Id;
                                                }
                                            }
                                            var result = CheckNewExaminationRecordForConflictAndSave(currentRecord);
                                            if (!result.Succeeded)
                                            {
                                                foreach (var err in result.Errors)
                                                {
                                                    resError.AddError(err.Key, err.Value);
                                                }
                                            }

                                            LessonConsultationClassroom = string.Empty;
                                            ConsultationClassroomId = string.Empty;
                                            dayConsult = null;
                                        }
                                        else
                                        {
                                            excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                                            if (excelDiscNameCell.Value2 != null && excelDiscNameCell.Value2 == "К")
                                            {
                                                dayConsult = dateBeginExamination.AddDays(i);
                                                var excelTimeAndClassroom = excelGroupNameCell.get_Offset(i * 3 + 3, 0);
                                                if (excelTimeAndClassroom.Value2 != null)
                                                {
                                                    string timeAndClassroom = excelTimeAndClassroom.Value2;
                                                    var timeMatch = Regex.Match(timeAndClassroom, @"\d{1,2}[(\:)*(\.)*(\-)*]+\d{2}");
                                                    if (timeMatch.Success)
                                                    {
                                                        timeAndClassroom = timeAndClassroom.Replace(timeMatch.Value, "");
                                                        dayConsult = dayConsult.Value.AddHours(Convert.ToInt32(Regex.Match(timeMatch.Value, @"^\d{1,2}").Value))
                                                                                            .AddMinutes(Convert.ToInt32(Regex.Match(timeMatch.Value, @"\d{2}$").Value));
                                                        if (dayConsult.Value.ToShortTimeString() !=
                                                            lessons.FirstOrDefault(l => l.Title.Contains("Вторая")).DateBeginLesson.ToShortTimeString())
                                                        {
                                                            resError.AddError("Неверное время консультации", string.Format("{0} {1} {2}", dateBeginExamination.AddDays(i).ToShortDateString(),
                                                                dayConsult.Value.ToShortTimeString(), excelDiscNameCell.Value2));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var time = lessons.FirstOrDefault(l => l.Title.Contains("Первая")).DateBeginLesson;
                                                        dayConsult = dayConsult.Value.AddHours(time.Hour).AddMinutes(time.Minute);
                                                    }
                                                    var classroomMatch = Regex.Match(timeAndClassroom, @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                                                    if (classroomMatch.Success)
                                                    {
                                                        LessonConsultationClassroom = classroomMatch.Value;
                                                        var classroom = _context.Classrooms.FirstOrDefault(c => LessonConsultationClassroom.Contains(c.Id) && !c.IsDeleted);
                                                        if (classroom != null)
                                                        {
                                                            ConsultationClassroomId = classroom.Id;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    var time = lessons.FirstOrDefault(l => l.Title.Contains("Первая")).DateBeginLesson;
                                                    dayConsult = dayConsult.Value.AddHours(time.Hour).AddMinutes(time.Minute);
                                                }
                                            }
                                        }
                                    }
                                    // переходим к следующей группе
                                    excelGroupNameCell = excelGroupNameCell.get_Offset(0, 1);
                                }
                                excelcell = excelcell.get_Offset(64, 0);
                            }
                        }
                        transaction.Commit();
                        return resError;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        excel.Quit();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        /// <summary>
        /// Проверяем добавляемую запись консультация/экзамен на конфликты
        /// </summary>
        /// <param name="record"></param>
        private static ResultService CheckNewExaminationRecordForConflictAndSave(ExaminationRecordRecordBindingModel record)
        {
            try
            {
                // если у консультации/экзамена не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
                // то такая консультация/экзамен нас не интересует
                if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null && record.DisciplineId == null && record.ConsultationClassroomId == null)
                {
                    return ResultService.Success();
                }

                //ищем консультацию/экзамен другой группы в этой аудитории
                var exsistRecord = _context.ExaminationRecords.FirstOrDefault(r =>
                                        ((r.DateConsultation == record.DateConsultation &&
                                        ((r.ConsultationClassroomId == record.ConsultationClassroomId && !string.IsNullOrEmpty(record.ConsultationClassroomId)) ||
                                        (r.LessonClassroom == record.LessonClassroom && string.IsNullOrEmpty(record.ConsultationClassroomId)))) ||
                                        (r.DateExamination == record.DateExamination && r.LessonClassroom == record.LessonClassroom))
                                        && r.SeasonDatesId == record.SeasonDatesId && r.LessonGroup != record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой в этой аудитории уже есть консультация/экзамен
                    return ResultService.Error("Конфликт (аудитории):", string.Format("конс {0} экз {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                        record.DateConsultation, record.DateExamination,
                        exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                //ищем консультацию/экзамен этой группы в другой аудитории
                exsistRecord = _context.ExaminationRecords.FirstOrDefault(r =>
                                        ((r.DateConsultation == record.DateConsultation &&
                                        ((r.ConsultationClassroomId != record.ConsultationClassroomId && !string.IsNullOrEmpty(record.ConsultationClassroomId)) ||
                                        (r.LessonClassroom != record.LessonClassroom && string.IsNullOrEmpty(record.ConsultationClassroomId)))) ||
                                        (r.DateExamination == record.DateExamination && r.LessonClassroom != record.LessonClassroom))
                                        && r.SeasonDatesId == record.SeasonDatesId && r.LessonGroup == record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть консультация/экзамен
                    return ResultService.Error("Конфликт (аудитории):", string.Format("конс {0} экз {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                        record.DateConsultation, record.DateExamination,
                        exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                var entity = ModelFacotryFromBindingModel.CreateExaminationRecord(record, seasonDate: _seasonDate);

                _context.ExaminationRecords.Add(entity);
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
