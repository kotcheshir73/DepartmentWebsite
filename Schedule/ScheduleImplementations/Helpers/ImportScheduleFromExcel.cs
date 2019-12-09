using DatabaseContext;
using Enums;
using Models.AcademicYearData;
using ScheduleImplementations;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tools;

namespace ScheduleServiceImplementations.Helpers
{
    class ImportScheduleFromExcel
    {
        private static SeasonDates _seasonDate;

        private static List<OffsetRecordSetBindingModel> _findOffsetRecords;

        private static List<ExaminationRecordSetBindingModel> _findExamRecords;

        public static ResultService ImportOffsets(ImportToOffsetFromExcel model)
        {
            try
            {
                _seasonDate = DepartmentUserManager.GetCurrentDates();
                _findOffsetRecords = new List<OffsetRecordSetBindingModel>();

                ///var excel = new Application();
                var resError = new ResultService();
                try
                {
                    //var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    //    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    //for (int w = 0; w < workbook.Worksheets.Count; ++w)
                    //{
                    //    var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(w + 1);//Получаем ссылку на лист
                    //    var excelcell = excelworksheet.get_Range("A2", "A2");

                    //    // заведем прерываетль, чтобы прекратить обход, если лист пустой
                    //    int counter = 0;
                    //    // идем вниз по первой колонки, пока не встретим текст
                    //    while (excelcell.Value2 == null)
                    //    {
                    //        excelcell = excelcell.get_Offset(1, 0);
                    //        counter++;
                    //        if (counter > 10)
                    //            break;
                    //    }
                    //    counter = 0;
                    //    while (counter < 10)
                    //    {
                    //        while (excelcell.Value2 != null && excelcell.Value2.ToString().ToLower() == "дни недели")
                    //        {
                    //            counter++;
                    //            if (counter > 10)
                    //                break;
                    //            // идем по первой строке с группами
                    //            // берем имя группы
                    //            var excelGroupNameCell = excelcell.get_Offset(0, 1);
                    //            while (excelGroupNameCell.Value2 != null)
                    //            {
                    //                // в день может быть 2 зачета, 6 дней зачетной недели, получается 12 шагов
                    //                for (int i = 0; i < 12; ++i)
                    //                {
                    //                    // под каждый зачет выделяется 3 строки
                    //                    // в первой строке - название зачета (за искл. физ-ры)
                    //                    var excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 1, 0);
                    //                    if (excelDiscNameCell.Value2 != null)
                    //                    {
                    //                        if (!Regex.IsMatch(excelDiscNameCell.Value2.ToString(), @"\w+"))
                    //                        {
                    //                            continue;
                    //                        }
                    //                        var excelLecturerName = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                    //                        var excelLessonAndClassroomsName = excelGroupNameCell.get_Offset(i * 3 + 3, 0);
                    //                        var currentRecord = new OffsetRecordRecordBindingModel
                    //                        {
                    //                            Week = 0,
                    //                            Day = i / 2,
                    //                            LessonDiscipline = excelDiscNameCell.Value2,
                    //                            LessonGroup = excelGroupNameCell.Value2,
                    //                            LessonLecturer = excelLecturerName.Value2
                    //                        };

                    //                        // определяем группу
                    //                        var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName.ToLower() == currentRecord.LessonGroup.ToLower() && !sg.IsDeleted);
                    //                        if (group != null)
                    //                        {
                    //                            currentRecord.StudentGroupId = group.Id;
                    //                        }

                    //                        // определяем дисциплину
                    //                        var shortName = ScheduleHelper.CalcShortDisciplineName(currentRecord.LessonDiscipline);
                    //                        var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineShortName == shortName);
                    //                        if (discipline != null)
                    //                        {
                    //                            currentRecord.DisciplineId = discipline.Id;
                    //                        }

                    //                        // определяем преподавателя
                    //                        var spliters = currentRecord.LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //                        string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
                    //                        string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
                    //                        string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
                    //                        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
                    //                                                ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                    //                                                ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                    //                        if (lecturer != null)
                    //                        {
                    //                            currentRecord.LecturerId = lecturer.Id;
                    //                        }

                    //                        // определяем пары и аудитории
                    //                        if (excelLessonAndClassroomsName.Value2 == null)
                    //                        {
                    //                            continue;
                    //                        }
                    //                        string lessonsAndClassroom = excelLessonAndClassroomsName.Value2.ToLower();
                    //                        var lessonsMassive = Regex.Match(lessonsAndClassroom, @"(\dп(\.)*(\,)*(\ )*)+");
                    //                        var classroomMatches = Regex.Matches(Regex.Replace(lessonsAndClassroom, @"(\dп(\.)*(\,)*(\ )*)+", "").Replace(" ", ""), @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                    //                        var lessons = Regex.Matches(lessonsMassive.Value, @"\d");
                    //                        for (int j = 0; j < lessons.Count; ++j)
                    //                        {
                    //                            var lesson = lessons[j].Value;
                    //                            currentRecord.Lesson = Convert.ToInt32(Regex.Match(lesson, @"\d").Value) - 1;
                    //                            for (int k = 0; k < classroomMatches.Count; ++k)
                    //                            {
                    //                                currentRecord.LessonClassroom = classroomMatches[k].Value;
                    //                                var classroom = _context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Number) && !c.IsDeleted);
                    //                                if (classroom != null)
                    //                                {
                    //                                    currentRecord.ClassroomId = classroom.Id;
                    //                                }
                    //                                var res = CheckNewOffsetRecordForConflictAndSave(currentRecord);
                    //                                if (!res.Succeeded)
                    //                                {
                    //                                    foreach (var err in res.Errors)
                    //                                    {
                    //                                        resError.AddError(err.Key, err.Value);
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                    // физ-ра, чтоб ее
                    //                    else
                    //                    {
                    //                        excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                    //                        if (excelDiscNameCell.Value2 != null)
                    //                        {
                    //                            if (excelDiscNameCell.Value2.Contains("Элективный курс по ФК") || excelDiscNameCell.Value2.Contains("ФИЗ – РА"))
                    //                            {
                    //                                var currentRecord = new OffsetRecordRecordBindingModel
                    //                                {
                    //                                    Week = 0,
                    //                                    Day = i / 2,
                    //                                    LessonDiscipline = "Физкультура",
                    //                                    LessonGroup = excelGroupNameCell.Value2,
                    //                                    LessonLecturer = ""
                    //                                };
                    //                                if (!string.IsNullOrEmpty(currentRecord.LessonGroup))
                    //                                {
                    //                                    var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == currentRecord.LessonGroup && !sg.IsDeleted);
                    //                                    if (group != null)
                    //                                    {
                    //                                        currentRecord.StudentGroupId = group.Id;
                    //                                    }
                    //                                    var res = CheckNewOffsetRecordForConflictAndSave(currentRecord);
                    //                                    if (!res.Succeeded)
                    //                                    {
                    //                                        foreach (var err in res.Errors)
                    //                                        {
                    //                                            resError.AddError(err.Key, err.Value);
                    //                                        }
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                // переходим к следующей группе
                    //                excelGroupNameCell = excelGroupNameCell.get_Offset(0, 1);
                    //            }
                    //            excelcell = excelcell.get_Offset(36, 0);
                    //        }
                    //        excelcell = excelcell.get_Offset(1, 0);
                    //        counter++;
                    //    }
                    //}

                    var result = SaveOffsetRecords();
                    if (!result.Succeeded)
                    {
                        foreach (var err in result.Errors)
                        {
                            resError.AddError(err.Key, err.Value);
                        }
                    }
                    return resError;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //excel.Quit();
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
        private static ResultService CheckNewOffsetRecordForConflictAndSave(OffsetRecordSetBindingModel record)
        {
            try
            {
                // если у зачета не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
                // то такой зачет нас не интересует
                if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null && record.DisciplineId == null)
                {
                    return ResultService.Success();
                }

                var exsistRecord = _findOffsetRecords.FirstOrDefault(r => r.Week == record.Week && r.Day == record.Day && r.Lesson == record.Lesson &&
                                        r.LessonClassroom == record.LessonClassroom && r.LessonGroup != record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой в этой аудитории уже есть зачет
                    if (exsistRecord.LessonDiscipline == record.LessonDiscipline &&
                        exsistRecord.LessonLecturer == record.LessonLecturer)
                    {//если совпадает дисицпилна, преподаватель и тип занятия, то это потоковый зачет
                    }
                    else
                    {
                        return ResultService.Error("Конфликт (аудитории " + record.LessonClassroom + "):", string.Format("дата {0} {1} {2}\r\n{3} {4} {5} - {6} {7} {8}\r\n",
                            record.Week + 1, record.Day + 1, record.Lesson + 1,
                            exsistRecord.LessonGroup, exsistRecord.LessonDiscipline, exsistRecord.LessonLecturer,
                            record.LessonGroup, record.LessonDiscipline, record.LessonLecturer), ResultServiceStatusCode.Error);
                    }
                }

                //ищем зачет этой группы в другой аудитории
                exsistRecord = _findOffsetRecords.FirstOrDefault(r => r.Week == record.Week && r.Day == record.Day && r.Lesson == record.Lesson &&
                                              r.LessonGroup == record.LessonGroup && r.LessonClassroom != record.LessonClassroom);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть зачет
                    return ResultService.Error("Конфликт (группы " + record.LessonGroup + "):", string.Format("дата {0} {1} {2}\r\n{3} {4} {5} - {6} {7} {8}\r\n",
                        record.Week + 1, record.Day + 1, record.Lesson + 1,
                        exsistRecord.LessonDiscipline, exsistRecord.LessonLecturer, exsistRecord.LessonClassroom,
                        record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                //ищем зачет этого преподавателя в другой аудитории
                exsistRecord = _findOffsetRecords.FirstOrDefault(r => r.Week == record.Week && r.Day == record.Day && r.Lesson == record.Lesson &&
                                              r.LessonLecturer == record.LessonLecturer && r.LessonClassroom != record.LessonClassroom);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть зачет
                    return ResultService.Error("Конфликт (преподаватель " + record.LessonLecturer + "):", string.Format("дата {0} {1} {2}\r\n{3} {4} {5} - {6} {7} {8}\r\n",
                        record.Week + 1, record.Day + 1, record.Lesson + 1,
                        exsistRecord.LessonDiscipline, exsistRecord.LessonGroup, exsistRecord.LessonClassroom,
                        record.LessonDiscipline, record.LessonGroup, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                _findOffsetRecords.Add(record);

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
        private static ResultService SaveOffsetRecords()
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                // получаем сущестующие записи
                var exsistRecords = context.OffsetRecords.Where(sr => sr.SeasonDatesId == _seasonDate.Id).ToList();

                #region для начала проходим по аудиториям
                var classrooms = context.Classrooms.Where(c => !c.IsDeleted && !c.NotUseInSchedule).ToList();
                foreach (var classroom in classrooms)
                {
                    var selectedRecords = exsistRecords.Where(sr => sr.ClassroomId == classroom.Id).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день в этой аудитории
                        var searchRecords = _findOffsetRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
                                                                rec.Id == Guid.Empty &&
                                                                (rec.ClassroomId == record.ClassroomId || rec.LessonClassroom == record.LessonClassroom))
                                                        .ToList();
                        // если пара одна
                        //if (searchRecords.Count == 1)
                        //{
                        //    searchRecords[0].Id = record.Id;
                        //    record.Checked = true;
                        //}
                        //// если пар несколько (проверяем, что потоковые)
                        //else if (searchRecords.Count > 1)
                        //{
                        //    var notStreamRecrods = searchRecords.Where(rec => !rec.IsStreaming).FirstOrDefault();
                        //    if (notStreamRecrods != null)
                        //    {
                        //        return ResultService.Error("Конфликт (аудитории) не потоковая:", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                        //            notStreamRecrods.Week, notStreamRecrods.Day, notStreamRecrods.Lesson, notStreamRecrods.LessonGroup,
                        //            record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        //    }
                        //    // ищем потоковую пару по группе
                        //    var streamRecord = searchRecords.Where(rec =>
                        //                                    (rec.LessonGroup == record.LessonGroup || rec.StudentGroupId == record.StudentGroupId)).FirstOrDefault();
                        //    if (streamRecord != null)
                        //    {
                        //        streamRecord.Id = record.Id;
                        //        record.Checked = true;
                        //    }
                        //}
                    }
                }
                #endregion

                #region проход по группам
                var groups = context.StudentGroups.Where(sg => !sg.IsDeleted).ToList();
                foreach (var group in groups)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(sr => sr.StudentGroupId == group.Id && !sr.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день в этой группе
                        var searchRecords = _findOffsetRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
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
                var lecturers = context.Lecturers.Where(l => !l.IsDeleted).ToList();
                foreach (var lecturer in lecturers)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(sr => sr.LecturerId == lecturer.Id && !sr.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день этого преподавателя
                        var searchRecords = _findOffsetRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
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
                            //var notStreamRecrods = searchRecords.Where(rec => !rec.IsStreaming).FirstOrDefault();
                            //if (notStreamRecrods != null)
                            //{
                            //    return ResultService.Error("Конфликт (преподаватель) не потоковая:", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            //        notStreamRecrods.Week, notStreamRecrods.Day, notStreamRecrods.Lesson, notStreamRecrods.LessonGroup,
                            //        record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                            //}
                            //// ищем потоковую пару по группе
                            //var streamRecrod = searchRecords.Where(rec =>
                            //                                (rec.LessonGroup == record.LessonGroup || rec.StudentGroupId == record.StudentGroupId)).FirstOrDefault();
                            //if (streamRecrod != null)
                            //{
                            //    streamRecrod.Id = record.Id;
                            //    record.Checked = true;
                            //}
                        }
                    }
                }
                #endregion

                #region проход по дисциплинам
                var disciplines = context.Disciplines.Where(d => !d.IsDeleted).ToList();
                foreach (var discipline in disciplines)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(sr => sr.DisciplineId == discipline.Id && !sr.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день в этой группе
                        var searchRecords = _findOffsetRecords.Where(rec => rec.Week == record.Week && rec.Day == record.Day && rec.Lesson == record.Lesson &&
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
                            //var notStreamRecrods = searchRecords.Where(rec => !rec.IsStreaming).FirstOrDefault();
                            //if (notStreamRecrods != null)
                            //{
                            //    return ResultService.Error("Конфликт (дисциплина) не потоковая:", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
                            //        notStreamRecrods.Week, notStreamRecrods.Day, notStreamRecrods.Lesson, notStreamRecrods.LessonGroup,
                            //        record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                            //}
                            //// ищем потоковую пару по группе
                            //var streamRecrod = searchRecords.Where(rec =>
                            //                                (rec.LessonGroup == record.LessonGroup || rec.StudentGroupId == record.StudentGroupId)).FirstOrDefault();
                            //if (streamRecrod != null)
                            //{
                            //    streamRecrod.Id = record.Id;
                            //    record.Checked = true;
                            //}
                        }
                    }
                }
                #endregion

                var deletedRecords = exsistRecords.Where(sr => !sr.Checked).ToList();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаляем неопознанные
                        context.OffsetRecords.RemoveRange(deletedRecords);
                        context.SaveChanges();

                        // получаем опознанные
                        var knowRecords = _findOffsetRecords.Where(sr => sr.Id != Guid.Empty).ToList();
                        foreach (var record in knowRecords)
                        {
                            var entity = context.OffsetRecords.FirstOrDefault(e => e.Id == record.Id);
                            if (entity == null)
                            {
                                return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                            }

                            entity = record.CreateRecord(entity);
                            context.SaveChanges();
                        }

                        // получаем новые
                        var unknowRecords = _findOffsetRecords.Where(sr => sr.Id == Guid.Empty).ToList();
                        foreach (var record in unknowRecords)
                        {
                            record.SeasonDatesId = _seasonDate.Id;
                            var entity = record.CreateRecord();

                            context.OffsetRecords.Add(entity);
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

        public static ResultService ImportExaminations(ImportToExaminationFromExcel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    _seasonDate = DepartmentUserManager.GetCurrentDates();
                    _findExamRecords = new List<ExaminationRecordSetBindingModel>();
                    var dateBeginExamination = Convert.ToDateTime(_seasonDate.DateBeginExamination);
                    var lessons = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("экзамен") || slt.Title.Contains("консультация")).ToList();

                    //var excel = new Application();
                    var resError = new ResultService();

                    try
                    {
                        //var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        //    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                        //for (int w = 0; w < workbook.Worksheets.Count; ++w)
                        //{
                        //    var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(w + 1);//Получаем ссылку на лист
                        //    var excelcell = excelworksheet.get_Range("A2", "A2");

                        //    // заведем прерываетль, чтобы прекратить обход, если лист пустой
                        //    int counter = 0;
                        //    // идем вниз по первой колонки, пока не встретим текст
                        //    while (excelcell.Value2 == null || excelcell.Value2.ToString().ToLower() != "дни недели")
                        //    {
                        //        excelcell = excelcell.get_Offset(1, 0);
                        //        counter++;
                        //        if (counter > 10)
                        //            break;
                        //    }
                        //    counter = 0;
                        //    while (excelcell.Value2 != null && excelcell.Value2.ToString().ToLower() == "дни недели")
                        //    {
                        //        counter++;
                        //        if (counter > 10)
                        //            break;
                        //        // идем по первой строке с группами
                        //        // берем имя группы
                        //        var excelGroupNameCell = excelcell.get_Offset(0, 1);
                        //        while (excelGroupNameCell.Value2 != null)
                        //        {
                        //            DateTime? dayConsult = null;
                        //            string LessonConsultationClassroom = string.Empty;
                        //            Guid? ConsultationClassroomId = null;
                        //            // 3 недели экзаменов = 21 день, по 3 ячейки на день
                        //            for (int i = 0; i < 21; ++i)
                        //            {
                        //                // в первой строке - название экзамена
                        //                var excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 1, 0);
                        //                if (excelDiscNameCell.Value2 != null)
                        //                {
                        //                    if (!Regex.IsMatch(excelDiscNameCell.Value2.ToString(), @"\w+"))
                        //                    {
                        //                        continue;
                        //                    }
                        //                    var excelLecturerName = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                        //                    var excelTimeAndClassroomsName = excelGroupNameCell.get_Offset(i * 3 + 3, 0);
                        //                    if (!dayConsult.HasValue)
                        //                    {
                        //                        resError.AddError("Не найдена дата консультации", string.Format("{0} {1} {2}", dateBeginExamination.AddDays(i).ToShortDateString(),
                        //                            excelLecturerName.Value2, excelDiscNameCell.Value2));
                        //                    }
                        //                    var currentRecord = new ExaminationRecordRecordBindingModel
                        //                    {
                        //                        DateConsultation = dayConsult.Value,
                        //                        DateExamination = dateBeginExamination.AddDays(i),
                        //                        LessonDiscipline = excelDiscNameCell.Value2,
                        //                        LessonGroup = excelGroupNameCell.Value2,
                        //                        LessonLecturer = excelLecturerName.Value2
                        //                    };

                        //                    if (!string.IsNullOrEmpty(LessonConsultationClassroom))
                        //                    {
                        //                        currentRecord.LessonConsultationClassroom = LessonConsultationClassroom;
                        //                    }
                        //                    if (ConsultationClassroomId.HasValue)
                        //                    {
                        //                        currentRecord.ConsultationClassroomId = ConsultationClassroomId;
                        //                    }

                        //                    // определяем группу
                        //                    var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == currentRecord.LessonGroup && !sg.IsDeleted);
                        //                    if (group != null)
                        //                    {
                        //                        currentRecord.StudentGroupId = group.Id;
                        //                    }

                        //                    // определяем дисциплину
                        //                    var shortName = ScheduleHelper.CalcShortDisciplineName(currentRecord.LessonDiscipline);
                        //                    var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineShortName == shortName);
                        //                    if (discipline != null)
                        //                    {
                        //                        currentRecord.DisciplineId = discipline.Id;
                        //                    }

                        //                    // определяем преподавателя
                        //                    var spliters = currentRecord.LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        //                    string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
                        //                    string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
                        //                    string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
                        //                    var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
                        //                                            ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                        //                                            ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                        //                    if (lecturer != null)
                        //                    {
                        //                        currentRecord.LecturerId = lecturer.Id;
                        //                    }

                        //                    // определяем время и аудиторию
                        //                    string timeAndClassroom = excelTimeAndClassroomsName.Value2.ToLower();
                        //                    var timeMatch = Regex.Match(timeAndClassroom, @"\d{1,2}[(\:)*(\.)*(\-)*]+\d{2}"); if (timeMatch.Success)
                        //                    {
                        //                        timeAndClassroom = timeAndClassroom.Replace(timeMatch.Value, "");
                        //                        currentRecord.DateExamination = currentRecord.DateExamination.AddHours(Convert.ToInt32(Regex.Match(timeMatch.Value, @"^\d{1,2}").Value))
                        //                                                            .AddMinutes(Convert.ToInt32(Regex.Match(timeMatch.Value, @"\d{2}$").Value));
                        //                        if (currentRecord.DateExamination.ToShortTimeString() !=
                        //                            lessons.FirstOrDefault(l => l.Title.Contains("Дневной")).DateBeginLesson.ToShortTimeString())
                        //                        {
                        //                            resError.AddError("Неверное время дневного экзамена", string.Format("{0} {1} {2} {3}", currentRecord.DateExamination.ToShortDateString(),
                        //                                currentRecord.DateExamination.ToShortTimeString(), excelLecturerName.Value2, excelDiscNameCell.Value2));
                        //                        }
                        //                    }
                        //                    else
                        //                    {
                        //                        var time = lessons.FirstOrDefault(l => l.Title.Contains("Утренний")).DateBeginLesson;
                        //                        currentRecord.DateExamination = currentRecord.DateExamination.AddHours(time.Hour).AddMinutes(time.Minute);
                        //                    }
                        //                    var classroomMatch = Regex.Match(timeAndClassroom, @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                        //                    if (classroomMatch.Success)
                        //                    {
                        //                        currentRecord.LessonClassroom = classroomMatch.Value;
                        //                        var classroom = _context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Number) && !c.IsDeleted);
                        //                        if (classroom != null)
                        //                        {
                        //                            currentRecord.ClassroomId = classroom.Id;
                        //                        }
                        //                    }
                        //                    var res = CheckNewExaminationRecordForConflictAndSave(currentRecord);
                        //                    if (!res.Succeeded)
                        //                    {
                        //                        foreach (var err in res.Errors)
                        //                        {
                        //                            resError.AddError(err.Key, err.Value);
                        //                        }
                        //                    }

                        //                    LessonConsultationClassroom = string.Empty;
                        //                    ConsultationClassroomId = null;
                        //                    dayConsult = null;
                        //                }
                        //                else
                        //                {
                        //                    excelDiscNameCell = excelGroupNameCell.get_Offset(i * 3 + 2, 0);
                        //                    if (excelDiscNameCell.Value2 != null && excelDiscNameCell.Value2 == "К")
                        //                    {
                        //                        dayConsult = dateBeginExamination.AddDays(i);
                        //                        var excelTimeAndClassroom = excelGroupNameCell.get_Offset(i * 3 + 3, 0);
                        //                        if (excelTimeAndClassroom.Value2 != null)
                        //                        {
                        //                            string timeAndClassroom = excelTimeAndClassroom.Value2;
                        //                            var timeMatch = Regex.Match(timeAndClassroom, @"\d{1,2}[(\:)*(\.)*(\-)*]+\d{2}");
                        //                            if (timeMatch.Success)
                        //                            {
                        //                                timeAndClassroom = timeAndClassroom.Replace(timeMatch.Value, "");
                        //                                dayConsult = dayConsult.Value.AddHours(Convert.ToInt32(Regex.Match(timeMatch.Value, @"^\d{1,2}").Value))
                        //                                                                    .AddMinutes(Convert.ToInt32(Regex.Match(timeMatch.Value, @"\d{2}$").Value));
                        //                                if (dayConsult.Value.ToShortTimeString() !=
                        //                                    lessons.FirstOrDefault(l => l.Title.Contains("Вторая")).DateBeginLesson.ToShortTimeString())
                        //                                {
                        //                                    resError.AddError("Неверное время консультации", string.Format("{0} {1} {2}", dateBeginExamination.AddDays(i).ToShortDateString(),
                        //                                        dayConsult.Value.ToShortTimeString(), excelDiscNameCell.Value2));
                        //                                }
                        //                            }
                        //                            else
                        //                            {
                        //                                var time = lessons.FirstOrDefault(l => l.Title.Contains("Первая")).DateBeginLesson;
                        //                                dayConsult = dayConsult.Value.AddHours(time.Hour).AddMinutes(time.Minute);
                        //                            }
                        //                            var classroomMatch = Regex.Match(timeAndClassroom, @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                        //                            if (classroomMatch.Success)
                        //                            {
                        //                                LessonConsultationClassroom = classroomMatch.Value;
                        //                                var classroom = _context.Classrooms.FirstOrDefault(c => LessonConsultationClassroom.Contains(c.Number) && !c.IsDeleted);
                        //                                if (classroom != null)
                        //                                {
                        //                                    ConsultationClassroomId = classroom.Id;
                        //                                }
                        //                            }
                        //                        }
                        //                        else
                        //                        {
                        //                            var time = lessons.FirstOrDefault(l => l.Title.Contains("Первая")).DateBeginLesson;
                        //                            dayConsult = dayConsult.Value.AddHours(time.Hour).AddMinutes(time.Minute);
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //            // переходим к следующей группе
                        //            excelGroupNameCell = excelGroupNameCell.get_Offset(0, 1);
                        //        }
                        //        excelcell = excelcell.get_Offset(64, 0);
                        //    }
                        //}

                        var result = SaveExamRecords();
                        if (!result.Succeeded)
                        {
                            foreach (var err in result.Errors)
                            {
                                resError.AddError(err.Key, err.Value);
                            }
                        }
                        return resError;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        //excel.Quit();
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
        private static ResultService CheckNewExaminationRecordForConflictAndSave(ExaminationRecordSetBindingModel record)
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
                var exsistRecord = _findExamRecords.FirstOrDefault(r =>
                                        ((r.DateConsultation == record.DateConsultation &&
                                        ((r.ConsultationClassroomId == record.ConsultationClassroomId && record.ConsultationClassroomId.HasValue) ||
                                        (r.LessonClassroom == record.LessonClassroom && !record.ConsultationClassroomId.HasValue))) ||
                                        (r.DateExamination == record.DateExamination && r.LessonClassroom == record.LessonClassroom))
                                        && r.SeasonDatesId == record.SeasonDatesId && r.LessonGroup != record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой в этой аудитории уже есть зачет
                    return ResultService.Error("Конфликт (аудитории):", string.Format("конс {0} экз {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                        record.DateConsultation, record.DateExamination,
                        exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                //ищем консультацию/экзамен этой группы в другой аудитории
                exsistRecord = _findExamRecords.FirstOrDefault(r =>
                                        ((r.DateConsultation == record.DateConsultation &&
                                        ((r.ConsultationClassroomId != record.ConsultationClassroomId && record.ConsultationClassroomId.HasValue) ||
                                        (r.LessonClassroom == record.LessonClassroom && !record.ConsultationClassroomId.HasValue))) ||
                                        (r.DateExamination == record.DateExamination && r.LessonClassroom != record.LessonClassroom))
                                        && r.SeasonDatesId == record.SeasonDatesId && r.LessonGroup == record.LessonGroup);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть зачет
                    return ResultService.Error("Конфликт (группы):", string.Format("конс {0} экз {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                        record.DateConsultation, record.DateExamination,
                        exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                //ищем консультацию/экзамен этого преподавателя в другой аудитории
                exsistRecord = _findExamRecords.FirstOrDefault(r =>
                                        ((r.DateConsultation == record.DateConsultation &&
                                        ((r.ConsultationClassroomId != record.ConsultationClassroomId && record.ConsultationClassroomId.HasValue) ||
                                        (r.LessonClassroom == record.LessonClassroom && !record.ConsultationClassroomId.HasValue))) ||
                                        (r.DateExamination == record.DateExamination && r.LessonClassroom != record.LessonClassroom))
                                        && r.SeasonDatesId == record.SeasonDatesId && r.LessonLecturer == record.LessonLecturer);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой у этой группы уже есть зачет
                    return ResultService.Error("Конфликт (преподаватель):", string.Format("конс {0} экз {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                        record.DateConsultation, record.DateExamination,
                        exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                }

                _findExamRecords.Add(record);

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
        private static ResultService SaveExamRecords()
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                // получаем сущестующие записи
                var exsistRecords = context.ExaminationRecords.Where(sr => sr.SeasonDatesId == _seasonDate.Id).ToList();

                #region для начала проходим по аудиториям
                var classrooms = context.Classrooms.Where(c => !c.IsDeleted && !c.NotUseInSchedule).ToList();
                foreach (var classroom in classrooms)
                {
                    var selectedRecords = exsistRecords.Where(sr => sr.ClassroomId == classroom.Id).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день в этой аудитории
                        var searchRecords = _findExamRecords.Where(rec =>
                                            (rec.DateConsultation == record.DateConsultation && rec.DateExamination == record.DateExamination) &&
                                            rec.Id == Guid.Empty && (rec.ClassroomId == record.ClassroomId || rec.LessonClassroom == record.LessonClassroom))
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
                            return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                                record.DateConsultation.ToShortDateString(), record.DateExamination.ToShortDateString(), record.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                    }
                }
                #endregion

                #region проход по группам
                var groups = context.StudentGroups.Where(sg => !sg.IsDeleted).ToList();
                foreach (var group in groups)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(sr => sr.StudentGroupId == group.Id && !sr.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день в этой группе
                        var searchRecords = _findExamRecords.Where(rec =>
                                            (rec.DateConsultation == record.DateConsultation && rec.DateExamination == record.DateExamination) &&
                                            rec.Id == Guid.Empty && (rec.StudentGroupId == record.StudentGroupId || rec.LessonGroup == record.LessonGroup))
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
                            return ResultService.Error("Конфликт (группа):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                                record.DateConsultation.ToShortDateString(), record.DateExamination.ToShortDateString(), record.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                    }
                }
                #endregion

                #region проход по преподавателям
                var lecturers = context.Lecturers.Where(l => !l.IsDeleted).ToList();
                foreach (var lecturer in lecturers)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(sr => sr.LecturerId == lecturer.Id && !sr.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день этого преподавателя
                        var searchRecords = _findExamRecords.Where(rec =>
                                            (rec.DateConsultation == record.DateConsultation && rec.DateExamination == record.DateExamination) &&
                                            rec.Id == Guid.Empty && (rec.LecturerId == record.LecturerId || rec.LessonLecturer == record.LessonLecturer))
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
                            return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                                record.DateConsultation.ToShortDateString(), record.DateExamination.ToShortDateString(), record.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                    }
                }
                #endregion

                #region проход по дисциплинам
                var disciplines = context.Disciplines.Where(d => !d.IsDeleted).ToList();
                foreach (var discipline in disciplines)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(sr => sr.DisciplineId == discipline.Id && !sr.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем пары (которые еще не опознаны) в этот день в этой группе
                        var searchRecords = _findExamRecords.Where(rec =>
                                            (rec.DateConsultation == record.DateConsultation && rec.DateExamination == record.DateExamination) &&
                                            rec.Id == Guid.Empty && (rec.DisciplineId == record.DisciplineId || rec.LessonDiscipline == record.LessonDiscipline))
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
                            return ResultService.Error("Конфликт (дисциплина):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                                record.DateConsultation.ToShortDateString(), record.DateExamination.ToShortDateString(), record.LessonGroup,
                                record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
                        }
                    }
                }
                #endregion

                var deletedRecords = exsistRecords.Where(sr => !sr.Checked).ToList();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаляем неопознанные
                        context.ExaminationRecords.RemoveRange(deletedRecords);
                        context.SaveChanges();

                        // получаем опознанные
                        var knowRecords = _findExamRecords.Where(sr => sr.Id != Guid.Empty).ToList();
                        foreach (var record in knowRecords)
                        {
                            var entity = context.ExaminationRecords.FirstOrDefault(e => e.Id == record.Id);
                            if (entity == null)
                            {
                                return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                            }

                            entity = record.CreateRecord(entity);
                            context.SaveChanges();
                        }

                        // получаем новые
                        var unknowRecords = _findExamRecords.Where(sr => sr.Id == Guid.Empty).ToList();
                        foreach (var record in unknowRecords)
                        {
                            record.SeasonDatesId = _seasonDate.Id;
                            var entity = record.CreateRecord();

                            context.ExaminationRecords.Add(entity);
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