using AcademicYearInterfaces.BindingModels;
using BaseInterfaces.BindingModels;
using Enums;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace ExaminationImplementations.Implementations
{
    public class ExaminationProcess : IExaminationProcess
    {
        public ResultService CreateAllFindStatement(AcademicYearGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Ведомости, AccessType.View, "Ведомости");

                using (var context = DepartmentUserManager.GetContext)
                {
                    //TODO получаем записи учебного плана и на основе этого создаем ведомости
                    //var statement = context.Statements.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id);
                    //var timeNorm = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id
                    //    && (x.KindOfLoadName == "Экзамен" || x.KindOfLoadName == "Зачет" || x.KindOfLoadName == "Зачет с оценкой" || x.KindOfLoadName == "Курсовая работа")); //Получение трех норм времени для поиска ведомостей
                    //foreach (var tn in timeNorm)
                    //{
                    //    //Поиск найзначеных часов преподавателям
                    //    var APRM = context.AcademicPlanRecordMissions.Where(x => !x.IsDeleted).Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Contingent)
                    //        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                    //        .Where(x => x.AcademicPlanRecordElement.TimeNormId == tn.Id);
                    //    string nameTN = tn.KindOfLoadName == "Зачет с оценкой" ? "Диференцированный_зачет" : tn.KindOfLoadName == "Курсовая работа" ? "Курсовая_работа" : tn.KindOfLoadName;
                    //    foreach (var APRMRecord in APRM)
                    //    {
                    //        var studentGroup = context.StudentGroups.Where(x => !x.IsDeleted && x.EducationDirectionId == APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirectionId
                    //            && x.Course == APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.Course);
                    //        foreach (var SGRecord in studentGroup)
                    //        {
                    //            if (statement.FirstOrDefault(record => !record.IsDeleted
                    //                 && record.AcademicPlanRecordId == APRMRecord.AcademicPlanRecordElement.AcademicPlanRecordId
                    //                 && record.LecturerId == APRMRecord.LecturerId
                    //                 && record.StudentGroupId == SGRecord.Id) == null)
                    //            {
                    //                var entity = ExaminationModelFacotryFromBindingModel.CreateStatement(new StatementSetBindingModel()
                    //                {
                    //                    LecturerId = APRMRecord.LecturerId,
                    //                    AcademicPlanRecordId = APRMRecord.AcademicPlanRecordElement.AcademicPlanRecordId,
                    //                    Semester = APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.Semester.ToString(),
                    //                    Course = APRMRecord.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.Course.ToString(),
                    //                    TypeOfTest = nameTN,
                    //                    StudentGroupId = SGRecord.Id
                    //                });
                    //                context.Statements.Add(entity);
                    //            }
                    //        }
                    //    }
                    //}
                    //context.SaveChanges();
                }
                CreateAllFindStatementRecord(model);

                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAllFindStatementRecord(AcademicYearGetBindingModel model)
        {
            try
            {
                //if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                //{
                //    throw new Exception("Нет доступа на чтение данных");
                //}
                //var statement = _context.Statements.Where(record => !record.IsDeleted && record.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id);
                //foreach (var sttmnt in statement)
                //{
                //    var students = _context.Students.Where(record => !record.IsDeleted && record.StudentGroupId == sttmnt.StudentGroupId);
                //    var statementRecord = _context.StatementRecords.Where(record => !record.IsDeleted && record.StatementId == sttmnt.Id);
                //    foreach (var stdnt in students)
                //    {
                //        if (statementRecord.FirstOrDefault(record => record.StatementId == sttmnt.Id
                //             && record.StudentId == stdnt.Id) == null)
                //        {
                //            var entity = ExaminationModelFacotryFromBindingModel.CreateStatementRecord(new StatementRecordSetBindingModel()
                //            {
                //                StatementId = sttmnt.Id,
                //                StudentId = stdnt.Id,
                //                Score = "Не допущен"
                //            });
                //            if (sttmnt.TypeOfTest == TypeOfTest.Курсовая_работа)
                //            {
                //                var entityTo = ExaminationModelFacotryFromBindingModel.CreateStatementRecordExtended(new StatementRecordExtendedSetBindingModel()
                //                {
                //                    StatementRecordId = entity.Id,
                //                    Name = "Нет темы"
                //                });
                //                _context.StatementRecordExtendeds.Add(entityTo);
                //            }
                //            _context.StatementRecords.Add(entity);
                //        }
                //    }
                //}
                //_context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<object[]>> GetSummaryStatement(StudentGroupGetBindingModel model)
        {
            try
            {
                List<object[]> list = new List<object[]>();
                using (var context = DepartmentUserManager.GetContext)
                {
                    var tmpStatements = context.Statements.Where(x => !x.IsDeleted && x.StudentGroupId == model.Id).AsQueryable();
                    tmpStatements = tmpStatements.OrderBy(x => x.Semester).ThenBy(x => x.Discipline.DisciplineName);
                    tmpStatements = tmpStatements.Include(x => x.AcademicYear).Include(x => x.Lecturer).Include(x => x.StudentGroup).Include(x => x.Discipline);
                    var statements = new StatementPageViewModel
                    {
                        List = tmpStatements.Select(ExaminationModelFactoryToViewModel.CreateStatementViewModel).ToList()
                    }.List;

                    var students = context.Students.Where(x => !x.IsDeleted && x.StudentGroupId == model.Id && x.StudentState == StudentState.Учится);
                    var statementRecord = context.StatementRecords.Where(record => !record.IsDeleted).AsQueryable();

                    List<object> element = new List<object>
                    {
                        "Студенты\\Предметы"
                    };

                    foreach (var record in statements)
                    {
                        element.Add(record.DisciplineName + '(' + record.TypeOfTest.Substring(0, 3) + ')');
                    }

                    list.Add(element.ToArray());

                    foreach (var student in students)
                    {
                        element = new List<object>
                        {
                            student.ToString()
                        };

                        foreach (var statement in statements)
                        {
                            var tmp = statementRecord.FirstOrDefault(x => x.StatementId == statement.Id && x.StudentId == student.Id);
                            switch (tmp.Score.ToLower())
                            {
                                case "отлично":
                                    element.Add("5");
                                    break;
                                case "хорошо":
                                    element.Add("4");
                                    break;
                                case "удовлетворительно":
                                    element.Add("3");
                                    break;
                                case "не удовлетворительно":
                                case "неудовлетворительно":
                                    element.Add("2");
                                    break;
                                case "не допущен":
                                    element.Add("-");
                                    break;
                                default:
                                    element.Add(tmp.Score);
                                    break;
                            }

                        }

                        list.Add(element.ToArray());
                    }

                    return ResultService<List<object[]>>.Success(list);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<object[]>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}