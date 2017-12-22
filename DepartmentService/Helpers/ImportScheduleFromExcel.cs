using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DepartmentService.Helpers
{
    class ImportScheduleFromExcel
    {
        public static ResultService ImportOffsets(DepartmentDbContext context, IOffsetRecordService serviceOR, ImportToOffsetFromExcel model)
        {
            try
            {
                var currentDates = ScheduleHelper.GetCurrentDates();

                var excel = new Application();
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
                                            var group = context.StudentGroups.FirstOrDefault(sg => sg.GroupName == currentRecord.LessonGroup && !sg.IsDeleted);
                                            if (group != null)
                                            {
                                                currentRecord.StudentGroupId = group.Id;
                                            }

                                            // определяем дисциплину
                                            var shortName = ScheduleHelper.CalcShortDisciplineName(currentRecord.LessonDiscipline);
                                            var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineShortName == shortName);
                                            if (discipline != null)
                                            {
                                                currentRecord.DisciplineId = discipline.Id;
                                            }

                                            // определяем преподавателя
                                            var spliters = currentRecord.LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
                                            string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
                                            string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
                                            var lecturer = context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
                                                                    ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                                                                    ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                                            if (lecturer != null)
                                            {
                                                currentRecord.LecturerId = lecturer.Id;
                                            }

                                            // определяем пары и аудитории
                                            string lessonsAndClassroom = excelLessonAndClassroomsName.Value2.ToLower();
                                            var lessons = Regex.Matches(lessonsAndClassroom, @"(\dп(\.)*(\,)*(\ )*)+");
                                            var classroomMatches = Regex.Matches(Regex.Replace(lessonsAndClassroom, @"(\dп(\.)*(\,)*(\ )*)+", "").Replace(" ", ""), @"(\w{0,2})[\d]+(\-\d)*(\/\d)*");
                                            for (int j = 0; j < lessons.Count; ++j)
                                            {
                                                var lesson = lessons[j].Value;
                                                currentRecord.Lesson = Convert.ToInt32(Regex.Match(lesson, @"\d").Value) - 1;
                                                for (int k = 0; k < classroomMatches.Count; ++k)
                                                {
                                                    currentRecord.LessonClassroom = classroomMatches[k].Value;
                                                    var classroom = context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Id) && !c.IsDeleted);
                                                    if (classroom != null)
                                                    {
                                                        currentRecord.ClassroomId = classroom.Id;
                                                    }
                                                    if (currentRecord.ClassroomId == null && currentRecord.StudentGroupId == null &&
                                                        currentRecord.LecturerId == null && currentRecord.DisciplineId == null)
                                                    {
                                                        continue;
                                                    }
                                                    context.OffsetRecords.Add(ModelFacotryFromBindingModel.CreateOffsetRecord(currentRecord, seasonDate: currentDates));
                                                    context.SaveChanges();
                                                }
                                            }
                                        }
                                        else
                                        {
                                        }
                                    }
                                    // переходим к следующей группе
                                    excelGroupNameCell = excelGroupNameCell.get_Offset(0, 1);
                                }
                                excelcell = excelcell.get_Offset(37, 0);
                            }
                        }
                            
                        transaction.Commit();
                        return ResultService.Success();
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

        public static ResultService ImportExaminations(DepartmentDbContext context, IExaminationRecordService serviceER, ImportToExaminationFromExcel model)
        {
            try
            {
                var currentDates = ScheduleHelper.GetCurrentDates();

                var excel = new Application();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                        var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(1);//Получаем ссылку на лист 1
                        var excelcell = excelworksheet.get_Range("A2", "A2");

                        while (excelcell.Value2 != null)
                        {
                            DateTime dateConsult = Convert.ToDateTime(excelcell.Value2);
                            DateTime dateExam = Convert.ToDateTime(excelcell.get_Offset(0, 1).Value2);

                            string studentGroupName = excelcell.get_Offset(0, 2).Value2;
                            string disciplineName = ScheduleHelper.CalcShortDisciplineName(excelcell.get_Offset(0, 3).Value2);
                            string lecturerName = excelcell.get_Offset(0, 4).Value2;
                            string classroomId = excelcell.get_Offset(0, 5).Value2;

                            var classroom = context.Classrooms.FirstOrDefault(c => c.Id.Contains(classroomId));

                            var lecturer = context.Lecturers.FirstOrDefault(l => l.LastName.Contains(lecturerName));

                            var group = context.StudentGroups.SingleOrDefault(rec => rec.GroupName.Contains(studentGroupName));

                            var disc = context.Disciplines.SingleOrDefault(rec => rec.DisciplineShortName == disciplineName);

                            long? lecturerId = null;
                            if (lecturer != null)
                            {
                                lecturerId = lecturer.Id;
                            }
                            long? studentGroupId = null;
                            if (group != null)
                            {
                                studentGroupId = group.Id;
                            }
                            long? disciplineId = null;
                            if (disc != null)
                            {
                                disciplineId = disc.Id;
                            }

                            var result = serviceER.CreateExaminationRecord(new ExaminationRecordRecordBindingModel
                            {
                                DateConsultation = dateConsult,
                                DateExamination = dateExam,
                                LessonClassroom = classroomId,
                                LessonDiscipline = disciplineName,
                                LessonGroup = studentGroupName,
                                LessonLecturer = lecturerName,
                                ClassroomId = classroom != null ? classroom.Id : string.Empty,
                                LecturerId = lecturerId,
                                StudentGroupId = studentGroupId,
                                DisciplineId = disciplineId
                            });
                            if (!result.Succeeded)
                            {
                                excel.Quit();
                                return result;
                            }
                            excelcell = excelcell.get_Offset(1, 0);
                        }
                        transaction.Commit();
                        return ResultService.Success();
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
    }
}
