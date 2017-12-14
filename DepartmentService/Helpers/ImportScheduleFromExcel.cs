using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;

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

                        var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(1);//Получаем ссылку на лист 1
                        var excelcell = excelworksheet.get_Range("A2", "A2");
                        
                        while (excelcell.Value2 != null)
                        {
                            DateTime dateOffset = Convert.ToDateTime(excelcell.Value2);

                            int week = (dateOffset - Convert.ToDateTime(currentDates.DateBeginOffset)).Days < 7 ? 0 : 1;
                            int day = (dateOffset - Convert.ToDateTime(currentDates.DateBeginOffset)).Days + week * 7;
                            int lesson = Convert.ToInt32(excelcell.get_Offset(0, 5).Value2) - 1;
                            string studentGroupName = excelcell.get_Offset(0, 1).Value2;
                            string disciplineName = ScheduleHelper.CalcShortDisciplineName(excelcell.get_Offset(0, 2).Value2);
                            string lecturerName = excelcell.get_Offset(0, 3).Value2;
                            string classroomId = Convert.ToString(excelcell.get_Offset(0, 4).Value2);

                            var classroom = context.Classrooms.FirstOrDefault(c => c.Id.Contains(classroomId));

                            var lecturer = context.Lecturers.FirstOrDefault(l => l.LastName == lecturerName);

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

                            var result = serviceOR.CreateOffsetRecord(new OffsetRecordRecordBindingModel
                            {
                                Week = week,
                                Day = day,
                                Lesson = lesson,
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
