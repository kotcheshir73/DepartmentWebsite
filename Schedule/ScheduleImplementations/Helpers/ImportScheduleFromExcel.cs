using DatabaseContext;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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

        private readonly static List<string> Symbols = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        private readonly static int _countRowsInOffset = 3;

        private readonly static int _countOffsetsInDay = 2;

        private readonly static int _countDays = 6;

        public static ResultService ImportOffsets(ImportToOffsetFromExcel model)
        {
            try
            {
                _findOffsetRecords = new List<OffsetRecordSetBindingModel>();
                _findExamRecords = new List<ExaminationRecordSetBindingModel>();

                var resError = new ResultService();
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(model.FileName, false))
                {
                    // получаем книгу
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    // идем по листам книги
                    foreach (WorksheetPart worksheetPart in spreadsheetDocument.WorkbookPart.WorksheetParts)
                    {
                        foreach (SheetData sheetData in worksheetPart.Worksheet.Elements<SheetData>())
                        {
                            int rowindex = 1;
                            int colindex = 0;

                            // заведем прерываетль, чтобы прекратить обход, если лист пустой
                            int counter = 0;
                            var value = string.Empty;
                            while (true)
                            {
                                while (value.ToLower() != "дни недели")
                                {
                                    value = GetValue(workbookPart, sheetData, Symbols[colindex], rowindex);
                                    rowindex++;
                                    counter++;

                                    if (counter > 10)
                                    {
                                        break;
                                    }
                                }

                                if (value.ToLower() != "дни недели")
                                {
                                    break;
                                }
                                rowindex--;
                                var res = LoadRows(model, workbookPart, sheetData, rowindex);
                                rowindex += _countRowsInOffset * _countOffsetsInDay * _countDays - 1;
                                if (!res.Succeeded)
                                {
                                    foreach (var err in res.Errors)
                                    {
                                        resError.AddError(err.Key, err.Value);
                                    }
                                }

                                value = string.Empty;
                                counter = 0;
                            }
                        }
                    }
                }

                var result = SaveOffsetRecords();
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        resError.AddError(err.Key, err.Value);
                    }
                }

                if (_findExamRecords.Count > 0)
                {
                    var resultExams = SaveExamRecords();
                    if (!resultExams.Succeeded)
                    {
                        foreach (var err in resultExams.Errors)
                        {
                            resError.AddError(err.Key, err.Value);
                        }
                    }
                }

                return resError;
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        /// <summary>
        /// Получение строки из Excel-файла
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private static Row GetRow(SheetData sheetData, int rowIndex)
        {
            return sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).FirstOrDefault();
        }

        /// <summary>
        /// Получение ячейки из Excel-файла
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private static Cell GetCell(SheetData sheetData, string columnName, int rowIndex)
        {
            Row row = GetRow(sheetData, rowIndex);

            if (row == null)
            {
                return null;
            }

            return row.Elements<Cell>().Where(c => c.CellReference == $"{columnName}{rowIndex}").FirstOrDefault();
        }

        /// <summary>
        /// Получение значения из ячейки из Excel-файла
        /// </summary>
        /// <param name="workbookPart"></param>
        /// <param name="sheetData"></param>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private static string GetValue(WorkbookPart workbookPart, SheetData sheetData, string columnName, int rowIndex)
        {
            var cell = GetCell(sheetData, columnName, rowIndex);

            if (cell == null)
            {
                return string.Empty;
            }

            if (int.TryParse(cell.CellValue?.Text, out int id))
            {
                return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id).InnerText;
            }

            return string.Empty;
        }

        /// <summary>
        /// Загрузка таблицы с группами и днями зачетов (на одной странице Excel-файла может быть 2 таких таблиц)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="workbookPart"></param>
        /// <param name="sheetData"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private static ResultService LoadRows(ImportToOffsetFromExcel model, WorkbookPart workbookPart, SheetData sheetData, int rowIndex)
        {
            var resError = new ResultService();
            Dictionary<int, Tuple<string, Guid?>> groups = new Dictionary<int, Tuple<string, Guid?>>();

            int colindex = 0;

            using (var context = DepartmentUserManager.GetContext)
            {
                //считываем группы в словарь. потом, когда надо будет в добавляемую запись зачета по номеру колонки проставим группу
                while (true)
                {
                    colindex++;
                    var groupname = GetValue(workbookPart, sheetData, Symbols[colindex], rowIndex);

                    if (string.IsNullOrEmpty(groupname))
                    {
                        break;
                    }
                    if (groupname.ToLower() == "дни недели")
                    {
                        continue;
                    }

                    var groupId = context.StudentGroups.FirstOrDefault(x => x.GroupName.ToLower() == groupname.ToLower() && !x.IsDeleted)?.Id;

                    groups.Add(colindex, new Tuple<string, Guid?>(groupname, groupId));
                }
                rowIndex++;

                // идем по группам
                foreach (var gr in groups)
                {
                    // идем по дням зачетов
                    for (int days = 0; days < _countDays; ++days)
                    {
                        // сколько зачетов может быть в день
                        for (int cur = 0; cur < _countOffsetsInDay; ++cur)
                        {
                            int curIndex = rowIndex + days * _countOffsetsInDay * _countRowsInOffset + cur * _countRowsInOffset;
                            var disicplinename = GetValue(workbookPart, sheetData, Symbols[gr.Key], curIndex);

                            // зачет по физ-ре, а также, если поставлен экзамен, то запись идет со второй строки
                            if (string.IsNullOrEmpty(disicplinename))
                            {
                                disicplinename = GetValue(workbookPart, sheetData, Symbols[gr.Key], curIndex + 1);

                                // коснультация переж экхаменом, пропускаем
                                if(disicplinename.ToUpper() == "К")
                                {
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(disicplinename))
                                {
                                    if (disicplinename.ToLower().Contains("элективный курс по"))
                                    {
                                        var record = new OffsetRecordSetBindingModel
                                        {
                                            Id = Guid.Empty,
                                            LessonDiscipline = "Физкультура",
                                            LessonClassroom = "Спортзал",
                                            LessonLecturer = "Преподаватель",
                                            LessonStudentGroup = gr.Value.Item1,
                                            StudentGroupId = gr.Value.Item2
                                        };
                                        var lesson = GetValue(workbookPart, sheetData, Symbols[gr.Key], curIndex + 2)?.Trim();
                                        record.Lesson = Convert.ToInt32(lesson[0].ToString()) - 1;
                                        record.ScheduleDate = ScheduleHelper.GetDateWithTime(model.ScheduleDate.Date.AddDays(days), record.Lesson);

                                        var res = CheckNewOffsetRecordForConflict(record);
                                        if (!res.Succeeded)
                                        {
                                            foreach (var err in res.Errors)
                                            {
                                                resError.AddError(err.Key, err.Value);
                                            }
                                        }
                                    }
                                    else if (disicplinename.ToUpper().Contains("ЭКЗАМЕН"))
                                    {
                                        // ищем дату консультации, для этого идем вверх по колонке
                                        var consult = string.Empty;
                                        int counter = 0;
                                        int consIndex = rowIndex + days * _countOffsetsInDay * _countRowsInOffset + cur * _countRowsInOffset - counter;
                                        while (consult.ToUpper() != "К" && consIndex > 1)
                                        {
                                            counter++;
                                            consIndex = rowIndex + days * _countOffsetsInDay * _countRowsInOffset + cur * _countRowsInOffset - counter;
                                            consult = GetValue(workbookPart, sheetData, Symbols[gr.Key], consIndex);
                                        }

                                        // мы ничего не нашли, так что пропускаем эту запись
                                        if (consIndex == 1)
                                        {
                                            continue;
                                        }

                                        // ищем инфу о времени консультации
                                        var timeconsult = GetValue(workbookPart, sheetData, Symbols[gr.Key], consIndex + 1)?.Trim();
                                        if (string.IsNullOrEmpty(timeconsult))
                                        {
                                            timeconsult = GetValue(workbookPart, sheetData, Symbols[gr.Key], consIndex + 2)?.Trim();
                                        }

                                        // вытаскиваем инфу по экзамену
                                        cur++;

                                        var examRecord = new ExaminationRecordSetBindingModel
                                        {
                                            Id = Guid.Empty,
                                            LessonDiscipline = GetValue(workbookPart, sheetData, Symbols[gr.Key], rowIndex + days * _countOffsetsInDay * _countRowsInOffset + cur * _countRowsInOffset),
                                            LessonLecturer = GetValue(workbookPart, sheetData, Symbols[gr.Key], rowIndex + days * _countOffsetsInDay * _countRowsInOffset + cur * _countRowsInOffset + 1),
                                            LessonStudentGroup = gr.Value.Item1,
                                            StudentGroupId = gr.Value.Item2
                                        };

                                        ScheduleHelper.GetLecturer(context, examRecord);

                                        ScheduleHelper.GetDiscipline(context, examRecord);

                                        var timeandclassroom = GetValue(workbookPart, sheetData, Symbols[gr.Key], rowIndex + days * _countOffsetsInDay * _countRowsInOffset + cur * _countRowsInOffset + 2);
                                        var timeandclassroomSplit = timeandclassroom.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        var time = timeandclassroomSplit[0];

                                        examRecord.LessonClassroom = timeandclassroomSplit[1].Trim();
                                        ScheduleHelper.GetClassroom(context, examRecord);
                                        examRecord.LessonConsultationClassroom = examRecord.LessonClassroom;
                                        examRecord.ConsultationClassroomId = examRecord.ClassroomId;

                                        examRecord.DateConsultation = model.ScheduleDate.Date.AddDays((consIndex - rowIndex) / (_countOffsetsInDay * _countRowsInOffset));
                                        if (!string.IsNullOrEmpty(timeconsult))
                                        {
                                            var timeconsultSplit = timeconsult.Split(new char[] { ',', ':', '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
                                            examRecord.DateConsultation = examRecord.DateConsultation.AddHours(Convert.ToInt32(timeconsultSplit[0])).AddMinutes(Convert.ToInt32(timeconsultSplit[1]));
                                        }
                                        examRecord.ScheduleDate = model.ScheduleDate.Date.AddDays(days);

                                        var timeSplit = time.Split(new char[] { ',', ':', '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
                                        examRecord.ScheduleDate = examRecord.ScheduleDate.AddHours(Convert.ToInt32(timeSplit[0])).AddMinutes(Convert.ToInt32(timeSplit[1]));

                                        var res = CheckNewExaminationRecordForConflict(examRecord);
                                        if (!res.Succeeded)
                                        {
                                            foreach (var err in res.Errors)
                                            {
                                                resError.AddError(err.Key, err.Value);
                                            }
                                        }
                                    }
                                }
                                // идем к следующему зачету
                                continue;
                            }

                            // зачет может идти несколько пар, создадим несколько записей
                            var lessonsandclassroom = GetValue(workbookPart, sheetData, Symbols[gr.Key], curIndex + 2).Split(new char[] { '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < lessonsandclassroom.Length - 1; ++i)
                            {
                                var record = new OffsetRecordSetBindingModel
                                {
                                    Id = Guid.Empty,
                                    LessonDiscipline = disicplinename,
                                    LessonLecturer = GetValue(workbookPart, sheetData, Symbols[gr.Key], curIndex + 1),
                                    LessonStudentGroup = gr.Value.Item1,
                                    StudentGroupId = gr.Value.Item2
                                };

                                ScheduleHelper.GetLecturer(context, record);

                                ScheduleHelper.GetDiscipline(context, record);


                                record.LessonClassroom = lessonsandclassroom[lessonsandclassroom.Length - 1];
                                ScheduleHelper.GetClassroom(context, record);

                                record.Lesson = Convert.ToInt32(Convert.ToInt32(lessonsandclassroom[i].Trim()[0].ToString())) - 1;
                                record.ScheduleDate = ScheduleHelper.GetDateWithTime(model.ScheduleDate.Date.AddDays(days), record.Lesson);

                                var res = CheckNewOffsetRecordForConflict(record);
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
                }
            }

            return resError;
        }

        /// <summary>
        /// Проверяем добавляемый зачет на конфликты
        /// </summary>
        /// <param name="record"></param>
        private static ResultService CheckNewOffsetRecordForConflict(OffsetRecordSetBindingModel record)
        {
            // если у зачета не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
            // то такой зачет нас не интересует
            if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null)
            {
                return ResultService.Success();
            }
            // выбираем уже добавленные записи на эту пару
            var selectRecordsOnDate = _findOffsetRecords.Where(x => x.ScheduleDate == record.ScheduleDate);

            var exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonClassroom == record.LessonClassroom);
            if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
            {
                return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                    record.ScheduleDate, record.Lesson,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
            }

            //ищем другие занятия этой группы
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonStudentGroup == record.LessonStudentGroup);
            if (exsistRecord != null)
            {
                return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                    record.ScheduleDate, record.Lesson,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
            }

            //ищем другие занятия этого преподавателя
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonLecturer == record.LessonLecturer);
            if (exsistRecord != null && !string.IsNullOrEmpty(record.LessonLecturer) && exsistRecord.LessonClassroom != record.LessonClassroom)
            {
                {
                    return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0} {1}\r\n{2} - {3}\r\n{4} {5} {6}\r\n",
                    record.ScheduleDate, record.Lesson,
                        exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
                }
            }

            _findOffsetRecords.Add(record);

            return ResultService.Success();
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
                var startDate = _findOffsetRecords.OrderBy(x => x.ScheduleDate).First().ScheduleDate.Date;
                var finishDate = _findOffsetRecords.OrderByDescending(x => x.ScheduleDate).First().ScheduleDate.Date.AddDays(1).AddSeconds(-1);
                // получаем сущестующие записи
                var exsistRecords = context.OffsetRecords.Where(x => x.ScheduleDate >= startDate && x.ScheduleDate <= finishDate).ToList();

                #region для начала проходим по аудиториям
                var classrooms = context.Classrooms.Where(x => !x.IsDeleted && !x.NotUseInSchedule).ToList();
                foreach (var classroom in classrooms)
                {
                    var selectedRecords = exsistRecords.Where(x => x.ClassroomId == classroom.Id).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем эту пару в списке загруженных
                        var searchRecord = _findOffsetRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                var disciplines = context.Disciplines.Where(d => !d.IsDeleted).ToList();
                foreach (var discipline in disciplines)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(x => x.DisciplineId == discipline.Id && !x.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем эту пару в списке загруженных
                        var searchRecord = _findOffsetRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                var lecturers = context.Lecturers.Where(l => !l.IsDeleted).ToList();
                foreach (var lecturer in lecturers)
                {
                    //отбираем еще не проверенные записи
                    var selectedRecords = exsistRecords.Where(x => x.LecturerId == lecturer.Id && !x.Checked).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем эту пару в списке загруженных
                        var searchRecord = _findOffsetRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                        var searchRecord = _findOffsetRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                        // удаляем неопознанные
                        context.OffsetRecords.RemoveRange(deletedRecords);
                        context.SaveChanges();

                        // получаем опознанные
                        var knowRecords = _findOffsetRecords.Where(x => x.Id != Guid.Empty).ToList();
                        foreach (var record in knowRecords)
                        {
                            var entity = context.OffsetRecords.FirstOrDefault(x => x.Id == record.Id);
                            if (entity == null)
                            {
                                return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                            }

                            entity = record.CreateRecord(entity);
                            context.SaveChanges();
                        }

                        // получаем новые
                        var unknowRecords = _findOffsetRecords.Where(x => x.Id == Guid.Empty).ToList();
                        foreach (var record in unknowRecords)
                        {
                            record.Id = Guid.NewGuid();
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
                    //var lessons = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("экзамен") || slt.Title.Contains("консультация")).ToList();

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
        private static ResultService CheckNewExaminationRecordForConflict(ExaminationRecordSetBindingModel record)
        {
            // если у консультации/экзамена не удалось определить ни номер аудитории, ни группы, ни преподавателя, ни дисциплины из имеющихся в БД записях
            // то такая консультация/экзамен нас не интересует
            if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null && record.ConsultationClassroomId == null)
            {
                return ResultService.Success();
            }

            // выбираем уже добавленные записи на эту пару
            var selectRecordsOnDate = _findExamRecords.Where(x => x.ScheduleDate == record.ScheduleDate);

            //ищем другие экзамены в этой аудитории (если потоковая пара, то дисциплина и преподаваетль должны совпадать)
            var exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonClassroom == record.LessonClassroom);
            if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
            {
                return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0}\r\n{1} - {2}\r\n{3} {4} {5}\r\n",
                    record.ScheduleDate,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
            }

            //ищем другие экзамены этой группы
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonStudentGroup == record.LessonStudentGroup);
            if (exsistRecord != null)
            {
                return ResultService.Error("Конфликт (группы):", string.Format("дата {0}\r\n{1} - {2}\r\n{3} {4} {5}\r\n",
                    record.ScheduleDate,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
            }

            //ищем другие экзамены этого преподавателя
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonLecturer == record.LessonLecturer);
            if (exsistRecord != null && !string.IsNullOrEmpty(record.LessonLecturer) && exsistRecord.LessonClassroom != record.LessonClassroom)
            {
                {
                    return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0}\r\n{1} - {2}\r\n{3} {4} {5}\r\n",
                    record.ScheduleDate,
                        exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
                }
            }

            selectRecordsOnDate = _findExamRecords.Where(x => x.DateConsultation == record.DateConsultation);

            //ищем другие консультации в этой аудитории (если потоковая пара, то дисциплина и преподаваетль должны совпадать)
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonConsultationClassroom == record.LessonConsultationClassroom);
            if (exsistRecord != null && !(exsistRecord.LessonDiscipline == record.LessonDiscipline && exsistRecord.LessonLecturer == record.LessonLecturer))
            {
                return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0}\r\n{1} - {2}\r\n{3} {4} {5}\r\n",
                    record.DateConsultation,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
            }

            //ищем другие консультации этой группы
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonStudentGroup == record.LessonStudentGroup);
            if (exsistRecord != null)
            {
                return ResultService.Error("Конфликт (группы):", string.Format("дата {0}\r\n{1} - {2}\r\n{3} {4} {5}\r\n",
                    record.DateConsultation,
                    exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
            }

            //ищем другие консультации этого преподавателя
            exsistRecord = selectRecordsOnDate.FirstOrDefault(x => x.LessonLecturer == record.LessonLecturer);
            if (exsistRecord != null && !string.IsNullOrEmpty(record.LessonLecturer) && exsistRecord.LessonConsultationClassroom != record.LessonConsultationClassroom)
            {
                {
                    return ResultService.Error("Конфликт (преподаватель):", string.Format("дата {0}\r\n{1} - {2}\r\n{3} {4} {5}\r\n",
                    record.DateConsultation,
                        exsistRecord.LessonStudentGroup, record.LessonStudentGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonStudentGroup), ResultServiceStatusCode.Error);
                }
            }

            _findExamRecords.Add(record);

            return ResultService.Success();
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
                var startDate = _findExamRecords.OrderBy(x => x.DateConsultation).First().DateConsultation.Date;
                var finishDate = _findExamRecords.OrderByDescending(x => x.ScheduleDate).First().ScheduleDate.Date.AddDays(1).AddSeconds(-1);
                // получаем сущестующие записи
                //var exsistRecords = context.ExaminationRecords.Where(sr => sr.SeasonDatesId == _seasonDate.Id).ToList();
                var exsistRecords = context.ExaminationRecords.Where(x => x.DateConsultation >= startDate && x.ScheduleDate <= finishDate).ToList();

                #region для начала проходим по аудиториям
                var classrooms = context.Classrooms.Where(x => !x.IsDeleted && !x.NotUseInSchedule).ToList();
                foreach (var classroom in classrooms)
                {
                    // вытаскиваем пары семестра, связанные с этой аудиторией
                    var selectedRecords = exsistRecords.Where(x => x.ClassroomId == classroom.Id).ToList();
                    foreach (var record in selectedRecords)
                    {
                        // ищем эту пару в списке загруженных
                        var searchRecord = _findExamRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                        var searchRecord = _findExamRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                        var searchRecord = _findExamRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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
                        var searchRecord = _findExamRecords.SingleOrDefault(x => x.ScheduleDate == record.ScheduleDate && x.Id == Guid.Empty &&
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