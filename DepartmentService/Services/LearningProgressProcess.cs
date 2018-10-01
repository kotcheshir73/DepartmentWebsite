﻿using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.Entity;
using System.Text;

namespace DepartmentService.Services
{
    public class LearningProgressProcess : ILearningProgressProcess
    {
        private readonly DepartmentDbContext _context;

        public LearningProgressProcess(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<Guid> GetCurrentAcademicYear()
        {
            try
            {
                var academicYearKey = _context.CurrentSettings.FirstOrDefault(x => x.Key == "Учебный год");
                if (academicYearKey == null)
                {
                    return ResultService<Guid>.Error("Error:", "В настройках не указан ключ - учебный год", ResultServiceStatusCode.NotFound);
                }
                var academicYear = _context.AcademicYears.FirstOrDefault(x => x.Title.Contains(academicYearKey.Value));
                if (academicYear == null)
                {
                    return ResultService<Guid>.Error("Error:", "academicYear not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<Guid>.Success(academicYear.Id);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<Guid>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<Guid>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<LearningProcessDisciplineViewModel>> GetDisciplines(LearningProcessDisciplineBindingModel model)
        {
            try
            {
                var settingDisciplineBlockModules = _context.CurrentSettings
                        .FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
                if (settingDisciplineBlockModules == null)
                {
                    return ResultService<List<LearningProcessDisciplineViewModel>>.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
                        ResultServiceStatusCode.NotFound);
                }
                var disciplineBlockModuls = _context.DisciplineBlocks
                    .FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockModules.Value));
                if (disciplineBlockModuls == null)
                {
                    return ResultService<List<LearningProcessDisciplineViewModel>>.Error("Error:", "disciplineBlock(Дисциплины (модули)) not found",
                        ResultServiceStatusCode.NotFound);
                }

                var user = _context.Users.FirstOrDefault(x => x.Id == model.UserId);

                if (user == null)
                {
                    return ResultService<List<LearningProcessDisciplineViewModel>>.Error("Error:", "Пользователь не найден",
                        ResultServiceStatusCode.NotFound);
                }
                if (!user.LecturerId.HasValue)
                {
                    return ResultService<List<LearningProcessDisciplineViewModel>>.Error("Error:", "У пользователя нет аккаунта преподавателя",
                        ResultServiceStatusCode.NotFound);
                }

                var query = _context.AcademicPlanRecordMissions
                    .Include(x => x.AcademicPlanRecordElement)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                    .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirectionId == model.EducationDirectionId &&
                                    x.LecturerId == user.LecturerId)
                    .Select(x => new LearningProcessDisciplineViewModel
                    {
                        Id = x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId,
                        DisciplineName = x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName
                    })
                    .Distinct()
                    .ToList();

                return ResultService<List<LearningProcessDisciplineViewModel>>.Success(query);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<LearningProcessDisciplineViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<LearningProcessDisciplineViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<LearningProcessDisciplineDetailViewModel>> GetDisciplineDetails(LearningProcessDisciplineDetailBindingModel model)
        {
            try
            {
                var settingDisciplineBlockModules = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
                if (settingDisciplineBlockModules == null)
                {
                    return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
                        ResultServiceStatusCode.NotFound);
                }
                var disciplineBlockModuls = _context.DisciplineBlocks
                    .FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockModules.Value));
                if (disciplineBlockModuls == null)
                {
                    return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Error("Error:", "disciplineBlock(Дисциплины (модули)) not found",
                        ResultServiceStatusCode.NotFound);
                }

                var user = _context.Users.FirstOrDefault(x => x.Id == model.UserId);

                if (user == null)
                {
                    return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Error("Error:", "Пользователь не найден",
                        ResultServiceStatusCode.NotFound);
                }
                if (!user.LecturerId.HasValue)
                {
                    return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Error("Error:", "У пользователя нет аккаунта преподавателя",
                        ResultServiceStatusCode.NotFound);
                }

                var query = _context.AcademicPlanRecordMissions
                    .Include(x => x.AcademicPlanRecordElement)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                    .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                    .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId &&
                                    x.LecturerId == user.LecturerId && x.AcademicPlanRecordElement.TimeNorm.UseInLearningProgress &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirectionId == model.EducationDirectionId &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId == model.DisciplineId)
                    .OrderBy(x => x.AcademicPlanRecordElement.TimeNorm.TimeNormOrder)
                    .Select(x => new
                    {
                        x.AcademicPlanRecordElement.TimeNormId,
                        x.AcademicPlanRecordElement.TimeNorm.TimeNormName,
                        x.AcademicPlanRecordElement.AcademicPlanRecord.Semester,
                        x.AcademicPlanRecordElement.TimeNorm.TimeNormOrder,
                        x.AcademicPlanRecordElement.PlanHours
                    })
                    .GroupBy(x => x.TimeNormId)
                    .ToList()
                    .OrderBy(x => x.First().TimeNormOrder);

                List<LearningProcessDisciplineDetailViewModel> result = new List<LearningProcessDisciplineDetailViewModel>();

                foreach (var elem in query)
                {
                    StringBuilder sb = new StringBuilder(elem.First().TimeNormName);
                    sb.Append(": ");
                    foreach (var subElem in elem.OrderBy(x => x.Semester))
                    {
                        sb.Append(subElem.Semester);
                        sb.Append(" - ");
                        sb.Append(subElem.PlanHours);
                        sb.Append("; ");
                    }
                    result.Add(new LearningProcessDisciplineDetailViewModel
                    {
                        Id = elem.Key,
                        TimeNormName = elem.First().TimeNormName,
                        Info = sb.ToString()
                    });
                }

                return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<LearningProcessDisciplineDetailViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService FormDisciplineLessons(LearningProcessFormDisciplineLessonsBindingModel model)
        {
            try
            {
                var settingDisciplineBlockModules = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
                if (settingDisciplineBlockModules == null)
                {
                    return ResultService.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
                        ResultServiceStatusCode.NotFound);
                }
                var disciplineBlockModuls = _context.DisciplineBlocks
                    .FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockModules.Value));
                if (disciplineBlockModuls == null)
                {
                    return ResultService.Error("Error:", "disciplineBlock(Дисциплины (модули)) not found",
                        ResultServiceStatusCode.NotFound);
                }

                var semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);

                var aprm = _context.AcademicPlanRecordMissions
                    .Include(x => x.AcademicPlanRecordElement)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                    .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                    .FirstOrDefault(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId &&
                                    x.AcademicPlanRecordElement.TimeNormId == model.TimeNormId &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.Semester == semester &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirectionId == model.EducationDirectionId &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId == model.DisciplineId);
                if (aprm == null)
                {
                    return ResultService.Error("Error:", "Не найдены часы по выбранному типу занятий",
                        ResultServiceStatusCode.NotFound);
                }

                if (aprm.Hours % model.CountLessons != 0)
                {
                    return ResultService.Error("Error:", string.Format("Не возможно равномерно распределить часы ({0}) по указанному количеству занятий {1}", aprm.Hours, model.CountLessons),
                        ResultServiceStatusCode.NotFound);
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    for (int i = 0; i < model.CountLessons; ++i)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateDisciplineLesson(new DisciplineLessonRecordBindingModel
                        {
                            AcademicYearId = model.AcademicYearId,
                            DisciplineId = model.DisciplineId,
                            EducationDirectionId = model.EducationDirectionId,
                            TimeNormId = model.TimeNormId,
                            Semester = model.Semester,
                            Title = string.Format("{0} {1}", aprm.AcademicPlanRecordElement.TimeNorm.TimeNormName, i + 1),
                            Description = "",
                            CountOfPairs = (int)aprm.Hours / model.CountLessons,
                            Order = i + 1
                        });

                        _context.DisciplineLessons.Add(entity);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService FormDisciplineLessonTasks(LearningProcessFormDisciplineLessonTasksBindingModel model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    for (int i = 0; i < model.Tasks.Count; ++i)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                        {
                            DisciplineLessonId = model.DisciplineLessonId,
                            Task = model.TitleTemplate.Replace("[N]", (i + 1).ToString()),
                            Description = model.Tasks[i],
                            IsNecessarily = model.IsNecessarily,
                            MaxBall = model.MaxBall,
                            Order = i + 1
                        });

                        _context.DisciplineLessonTasks.Add(entity);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService FormDisciplineLessonVariants(LearningProcessFormDisciplineLessonTaskVariantsBindingModel model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    for (int i = 0; i < model.Variants.Count; ++i)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                        {
                            DisciplineLessonTaskId = model.DisciplineLessonTaskId,
                            VariantNumber = model.VariantNumberTemplate.Replace("[N]", (i + 1).ToString()),
                            VariantTask = model.Variants[i],
                            Order = i + 1
                        });

                        _context.DisciplineLessonTaskVariants.Add(entity);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonTaskVariantViewModel>> GetDisciplineLessonTaskVariants(GetDisciplineLessonTaskVariantsBindingModel model)
        {
            try
            {
                var query = _context.DisciplineLessonTaskVariants
                    .Include(x => x.DisciplineLessonTask)
                    .Where(x => x.DisciplineLessonTask.DisciplineLessonId == model.DisciplineLessonId)
                    .OrderBy(x => x.DisciplineLessonTask.Order).ThenBy(x => x.Order);
                return ResultService<List<DisciplineLessonTaskVariantViewModel>>.Success(query.Select(ModelFactoryToViewModel.CreateDisciplineLessonTaskVariantViewModel).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonTaskVariantViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonTaskVariantViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonTaskViewModel>> GetDisiplineLessonTasksForDuplicate(GetDisiplineLessonTasksForDuplicateBindingModel model)
        {
            try
            {
                var dlt = _context.DisciplineLessonTasks.FirstOrDefault(x => x.Id == model.DisciplineLessonTaskId);
                if (dlt == null)
                {
                    return ResultService<List<DisciplineLessonTaskViewModel>>.Error("Error:", "Задание не найдено",
                        ResultServiceStatusCode.NotFound);
                }
                var query = _context.DisciplineLessonTasks
                    .Include(x => x.DisciplineLesson)
                    .Where(x => x.DisciplineLessonId == dlt.DisciplineLessonId && x.Id != dlt.Id)
                    .OrderBy(x => x.Order);
                return ResultService<List<DisciplineLessonTaskViewModel>>.Success(query.Select(ModelFactoryToViewModel.CreateDisciplineLessonTaskViewModel).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonTaskViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonTaskViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DuplicateDisiplineLessonTasks(DuplicateDisiplineLessonTasksBindingModel model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var variants = _context.DisciplineLessonTaskVariants.Where(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskFromId).ToList();
                    foreach (var variant in variants)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                        {
                            DisciplineLessonTaskId = model.DisciplineLessonTaskToId,
                            VariantNumber = variant.VariantNumber,
                            VariantTask = variant.VariantTask,
                            Order = variant.Order
                        });

                        _context.DisciplineLessonTaskVariants.Add(entity);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonViewModel>> GetDisiplineLessonsForDuplicate(GetDisiplineLessonsForDuplicateBindingModel model)
        {
            try
            {
                var dl = _context.DisciplineLessons.FirstOrDefault(x => x.Id == model.DisciplineLessonId);
                if (dl == null)
                {
                    return ResultService<List<DisciplineLessonViewModel>>.Error("Error:", "Занятие не найдено",
                        ResultServiceStatusCode.NotFound);
                }
                var query = _context.DisciplineLessons
                    .Include(x => x.AcademicYear).Include(x => x.Discipline).Include(x => x.EducationDirection).Include(x => x.TimeNorm).Include(x => x.DisciplineLessonTasks)
                    .Where(x => x.DisciplineId == dl.DisciplineId && x.AcademicYearId == dl.AcademicYearId && x.EducationDirectionId == dl.EducationDirectionId &&
                                    x.TimeNormId == dl.TimeNormId && x.Id != dl.Id)
                    .OrderBy(x => x.Order);
                return ResultService<List<DisciplineLessonViewModel>>.Success(query.Select(ModelFactoryToViewModel.CreateDisciplineLessonViewModel).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DuplicateDisiplineLessons(DuplicateDisiplineLessonsBindingModel model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var tasks = _context.DisciplineLessonTasks.Where(x => x.DisciplineLessonId == model.DisciplineLessonFromId).ToList();
                    foreach (var task in tasks)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                        {
                            DisciplineLessonId = model.DisciplineLessonToId,
                            Task = task.Task,
                            Description = task.Description,
                            Image = task.Image,
                            IsNecessarily = task.IsNecessarily,
                            MaxBall = task.MaxBall,
                            Order = task.Order
                        });

                        _context.DisciplineLessonTasks.Add(entity);
                        _context.SaveChanges();

                        if (model.CopyVariants)
                        {
                            var variants = _context.DisciplineLessonTaskVariants.Where(x => x.DisciplineLessonTaskId == task.Id).ToList();
                            foreach (var variant in variants)
                            {
                                var entityTask = ModelFacotryFromBindingModel.CreateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                                {
                                    DisciplineLessonTaskId = entity.Id,
                                    VariantNumber = variant.VariantNumber,
                                    VariantTask = variant.VariantTask,
                                    Order = variant.Order
                                });

                                _context.DisciplineLessonTaskVariants.Add(entityTask);
                                _context.SaveChanges();
                            }
                        }
                    }
                    transaction.Commit();
                }
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<Semesters>> GetSemesters(LearningProcessSemesterBindingModel model)
        {
            try
            {
                var settingDisciplineBlockModules = _context.CurrentSettings
                        .FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
                if (settingDisciplineBlockModules == null)
                {
                    return ResultService<List<Semesters>>.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
                        ResultServiceStatusCode.NotFound);
                }
                var disciplineBlockModuls = _context.DisciplineBlocks
                    .FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockModules.Value));
                if (disciplineBlockModuls == null)
                {
                    return ResultService<List<Semesters>>.Error("Error:", "disciplineBlock(Дисциплины (модули)) not found",
                        ResultServiceStatusCode.NotFound);
                }

                var user = _context.Users.FirstOrDefault(x => x.Id == model.UserId);

                if (user == null)
                {
                    return ResultService<List<Semesters>>.Error("Error:", "Пользователь не найден",
                        ResultServiceStatusCode.NotFound);
                }
                if (!user.LecturerId.HasValue)
                {
                    return ResultService<List<Semesters>>.Error("Error:", "У пользователя нет аккаунта преподавателя",
                        ResultServiceStatusCode.NotFound);
                }


                var query = _context.AcademicPlanRecordMissions
                    .Include(x => x.AcademicPlanRecordElement)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                    .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirectionId == model.EducationDirectionId &&
                                    x.LecturerId == user.LecturerId &&
                                    x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId == model.DisciplineId)
                    .Select(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Semester.Value)
                    .Distinct()
                    .ToList();

                return ResultService<List<Semesters>>.Success(query);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<Semesters>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<Semesters>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<StudentGroupViewModel>> GetStudentGroups(LearningProcessStudentGroupBindingModel model)
        {
            try
            {
                List<AcademicCourse> course = new List<AcademicCourse>();
                foreach (var sem in model.Semesters)
                {
                    switch (sem)
                    {
                        case Semesters.Первый:
                        case Semesters.Второй:
                            course.Add(AcademicCourse.Course_1);
                            break;
                        case Semesters.Третий:
                        case Semesters.Четвертый:
                            course.Add(AcademicCourse.Course_2);
                            break;
                        case Semesters.Пятый:
                        case Semesters.Шестой:
                            course.Add(AcademicCourse.Course_3);
                            break;
                        case Semesters.Седьмой:
                        case Semesters.Восьмой:
                            course.Add(AcademicCourse.Course_4);
                            break;
                    }
                }

                var query = _context.StudentGroups
                    .Include(x => x.EducationDirection)
                    .Include(x => x.Students)
                    .Include(x => x.Curator)
                    .Where(x => x.EducationDirectionId == model.EducationDirectionId && course.Contains(x.Course));

                return ResultService<List<StudentGroupViewModel>>.Success(query.Select(ModelFactoryToViewModel.CreateStudentGroupViewModel).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<StudentGroupViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<StudentGroupViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineStudentRecordViewModel>> GetDisciplineStudentRecordsForFill(DisciplineStudentRecordsForFillBindingModel model)
        {
            try
            {
                var students = _context.Students.Where(x => x.StudentGroupId == model.StudentGroupId).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

                List<DisciplineStudentRecordViewModel> list = new List<DisciplineStudentRecordViewModel>();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    int counter = 1;
                    foreach (var st in students)
                    {
                        var dsr = _context.DisciplineStudentRecords
                            .Include(x => x.Student)
                            .Include(x => x.Student.StudentGroup)
                            .Include(x => x.Discipline)
                            .FirstOrDefault(x => x.StudentId == st.Id && x.DisciplineId == model.DisciplineId && x.Semester == model.Semester);
                        if (dsr == null)
                        {
                            dsr = ModelFacotryFromBindingModel.CreateDisciplineStudentRecord(new DisciplineStudentRecordSetBindingModel
                            {
                                DisciplineId = model.DisciplineId,
                                Semester = model.Semester.ToString(),
                                StudentId = st.Id,
                                SubGroup = 0,
                                Variant = string.Format("Вариант {0}", counter++)
                            });
                            _context.DisciplineStudentRecords.Add(dsr);
                            _context.SaveChanges();

                            dsr = _context.DisciplineStudentRecords
                            .Include(x => x.Student)
                            .Include(x => x.Student.StudentGroup)
                            .Include(x => x.Discipline)
                            .FirstOrDefault(x => x.StudentId == st.Id && x.DisciplineId == model.DisciplineId && x.Semester == model.Semester);
                        }

                        list.Add(ModelFactoryToViewModel.CreateDisciplineStudentRecordViewModel(dsr));
                    }

                    transaction.Commit();
                }

                return ResultService<List<DisciplineStudentRecordViewModel>>.Success(list);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineStudentRecordViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineStudentRecordViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<string>> GetDisciplineLessonSubgroup(DisciplineLessonSubgroupBindingModel model)
        {
            try
            {
                Semesters sem = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
                var subgroups = _context.DisciplineStudentRecords
                                        .Include(x => x.Student)
                                        .Where(x => x.DisciplineId == model.DisciplineId &&
                                                x.Student.StudentGroupId == model.StudentGroupId &&
                                                x.Semester == sem)
                                        .Select(x => x.SubGroup)
                                        .Distinct()
                                        .ToList()
                                        .Select(x => string.Format("Подгруппа  {0}", x))
                                        .ToList();

                subgroups.Insert(0, "Группа");

                return ResultService<List<string>>.Success(subgroups);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<string>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<string>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonConductedStudentViewModel>> GetDisciplineLessonConductedStudentsForFill(DisciplineLessonConductedStudentsForFillBindingModel model)
        {
            try
            {
                var dlc = _context.DisciplineLessonConducteds
                    .Include(x => x.DisciplineLesson)
                    .FirstOrDefault(x => x.Id == model.DisciplineLessonConductedId);
                if (dlc == null)
                {
                    return ResultService<List<DisciplineLessonConductedStudentViewModel>>.Error("Error:", "DisciplineLessonConducteds not found", ResultServiceStatusCode.NotFound);
                }


                var students = _context.DisciplineStudentRecords
                    .Where(x => x.Student.StudentGroupId == model.StudentGroupId && x.DisciplineId == dlc.DisciplineLesson.DisciplineId);

                if (dlc.Subgroup.Contains("Подгруппа"))
                {
                    int subgroup = Convert.ToInt32(dlc.Subgroup.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    students = students.Where(x => x.SubGroup == subgroup);
                }

                List<DisciplineLessonConductedStudentViewModel> list = new List<DisciplineLessonConductedStudentViewModel>();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (var st in students
                                            .OrderBy(x => x.Student.LastName)
                                            .ThenBy(x => x.Student.FirstName)
                                            .Select(x => x.StudentId))
                    {
                        var dlcs = _context.DisciplineLessonConductedStudents
                            .Include(x => x.Student)
                            .Include(x => x.Student.StudentGroup)
                            .FirstOrDefault(x => x.StudentId == st && x.DisciplineLessonConductedId == model.DisciplineLessonConductedId);
                        if (dlcs == null)
                        {
                            dlcs = ModelFacotryFromBindingModel.CreateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
                            {
                                DisciplineLessonConductedId = model.DisciplineLessonConductedId,
                                StudentId = st,
                                Status = DisciplineLessonStudentStatus.Явка.ToString()
                            });
                            _context.DisciplineLessonConductedStudents.Add(dlcs);
                            _context.SaveChanges();

                            dlcs = _context.DisciplineLessonConductedStudents
                            .Include(x => x.Student)
                            .Include(x => x.Student.StudentGroup)
                            .FirstOrDefault(x => x.StudentId == st && x.DisciplineLessonConductedId == model.DisciplineLessonConductedId);
                        }

                        list.Add(ModelFactoryToViewModel.CreateDisciplineLessonConductedStudentViewModel(dlcs));
                    }

                    transaction.Commit();
                }

                return ResultService<List<DisciplineLessonConductedStudentViewModel>>.Success(list);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonConductedStudentViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonConductedStudentViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<LessonConductedViewModel>> GetLessonConducteds(LessonConductedsBindingModel model)

        {
            try
            {
                var list = _context.DisciplineLessonConductedStudents
                                        .Include(x => x.DisciplineLessonConducted)
                                        .Include(x => x.DisciplineLessonConducted.DisciplineLesson)
                                        .Include(x => x.Student)
                                        .Where(x => x.DisciplineLessonConducted.DisciplineLesson.DisciplineId == model.DisciplineId &&
                                                        x.DisciplineLessonConducted.DisciplineLesson.TimeNormId == model.TimeNormId &&
                                                        x.Student.StudentGroupId == model.StudentGroupId)
                                        .OrderBy(x => x.Student.LastName)
                                        .ThenBy(x => x.Student.FirstName)
                                        .ToList();

                return ResultService<List<LessonConductedViewModel>>.Success(list.Select(x => new LessonConductedViewModel
                {
                    DisciplineLesson = string.Format("{1}", x.DisciplineLessonConducted.DisciplineLesson.Title, x.DisciplineLessonConducted.DateCreate.ToShortDateString()),
                    Student = string.Format("{0} {1}", x.Student.LastName, x.Student.FirstName),
                    StatusBall = string.Format("{0}{1}", x.Status, x.Ball.HasValue ? string.Format("{0}({1})", " ", x.Ball.Value) : ""),
                    Subgroup = x.DisciplineLessonConducted.Subgroup
                }).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<LessonConductedViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<LessonConductedViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonViewModel>> GetDisciplineLessons(LearningProcessDisciplineLessonBindingModel model)
        {
            try
            {
                Semesters sem = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
                var query = _context.DisciplineLessons
                                    .Include(x => x.AcademicYear)
                                    .Include(x => x.Discipline)
                                    .Include(x => x.EducationDirection)
                                    .Include(x => x.TimeNorm)
                                    .Include(x => x.DisciplineLessonTasks)
                                    .Where(x => !x.IsDeleted &&
                                                x.AcademicYearId == model.AcademicYearId &&
                                                x.DisciplineId == model.DisciplineId &&
                                                x.EducationDirectionId == model.EducationDirectionId &&
                                                x.Semester == sem)
                                    .OrderBy(x => x.Semester)
                                    .ThenBy(x => x.TimeNorm.TimeNormOrder)
                                    .ThenBy(x => x.Order);

                return ResultService<List<DisciplineLessonViewModel>>.Success(query.Select(ModelFactoryToViewModel.CreateDisciplineLessonViewModel).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>> GetDisciplineLessonTaskStudentAcceptForForm(DisciplineLessonTaskStudentAcceptForFormBindingModel model)
        {
            try
            {
                var students = _context.Students.Where(x => x.StudentGroupId == model.StudentGroupId && !x.IsDeleted).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                var dlt = _context.DisciplineLessonTasks.FirstOrDefault(x => x.Id == model.DisciplineLessonTaskId && !x.IsDeleted);
                var variants = _context.DisciplineLessonTaskVariants.Where(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskId && !x.IsDeleted).ToList();
                if (dlt == null)
                {
                    return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error("Error:", "disciplineLessonTask not found", ResultServiceStatusCode.NotFound);
                }

                List<DisciplineLessonTaskStudentAcceptViewModel> list = new List<DisciplineLessonTaskStudentAcceptViewModel>();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (var st in students)
                    {
                        StringBuilder task = new StringBuilder(dlt.Task);
                        task.AppendFormat("({0})", dlt.Description);
                        var dsr = _context.DisciplineStudentRecords.FirstOrDefault(x => x.DisciplineId == dlt.DisciplineLesson.DisciplineId && x.Semester == dlt.DisciplineLesson.Semester &&
                                                                                        x.StudentId == st.Id);
                        if (dsr != null)
                        {
                            var variant = variants.FirstOrDefault(x => x.VariantNumber == dsr.Variant);
                            if (variant != null)
                            {
                                task.AppendFormat(" {0}:{1}", variant.VariantNumber, variant.VariantTask);
                            }
                        }
                        var dltsa = _context.DisciplineLessonTaskStudentAccepts
                                            .Include(x => x.DisciplineLessonTask)
                                            .Include(x => x.Student)
                                            .FirstOrDefault(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskId && x.StudentId == st.Id);
                        if (dltsa == null)
                        {
                            dltsa = ModelFacotryFromBindingModel.CreateDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptSetBindingModel
                            {
                                DisciplineLessonTaskId = model.DisciplineLessonTaskId,
                                StudentId = st.Id,
                                Result = DisciplineLessonTaskStudentResult.Выдано.ToString(),
                                Task = task.ToString(),
                                DateAccept = model.DateAccept,
                                Score = 0,
                                Comment = "",
                                Log = string.Format("Выдано задание {0}", model.DateAccept.ToShortDateString())
                            });
                            _context.DisciplineLessonTaskStudentAccepts.Add(dltsa);
                            _context.SaveChanges();

                            dltsa = _context.DisciplineLessonTaskStudentAccepts
                                            .Include(x => x.DisciplineLessonTask)
                                            .Include(x => x.Student)
                                            .FirstOrDefault(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskId && x.StudentId == st.Id);
                        }
                        else if (dltsa.IsDeleted)
                        {
                            dltsa.IsDeleted = false;
                            dltsa.DateDelete = null;
                            dltsa.Task = task.ToString();
                            dltsa.DateAccept = model.DateAccept;
                            dltsa.Score = 0;
                            dltsa.Comment = "";
                            dltsa.Log = string.Format("Выдано задание {0}", model.DateAccept.ToShortDateString());
                        }

                        list.Add(ModelFactoryToViewModel.CreateDisciplineLessonTaskStudentAcceptViewModel(dltsa));
                    }

                    transaction.Commit();
                }

                return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Success(list);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService SetDisciplineLessonTaskStudentAccept(List<DisciplineLessonTaskStudentAcceptUpdateBindingModel> model)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (var m in model)
                    {
                        var elem = _context.DisciplineLessonTaskStudentAccepts.FirstOrDefault(x => x.Id == m.DisciplineLessonTaskStudentAcceptTaskId);
                        if (elem != null)
                        {
                            elem.Task = m.Task;
                            elem.Comment = m.Comment;

                            _context.SaveChanges();
                        }
                    }

                    transaction.Commit();
                }

                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>> GetDisciplineLessonTaskStudentAcceptForFill(DisciplineLessonTaskStudentAcceptForFillBindingModel model)
        {
            try
            {
                var students = _context.Students.Where(x => x.StudentGroupId == model.StudentGroupId && !x.IsDeleted).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                var dlt = _context.DisciplineLessonTasks.FirstOrDefault(x => x.Id == model.DisciplineLessonTaskId && !x.IsDeleted);
                if (dlt == null)
                {
                    return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error("Error:", "disciplineLessonTask not found", ResultServiceStatusCode.NotFound);
                }

                List<DisciplineLessonTaskStudentAcceptViewModel> list = new List<DisciplineLessonTaskStudentAcceptViewModel>();

                using (var transaction = _context.Database.BeginTransaction())
                {
                    foreach (var st in students)
                    {
                        var dsr = _context.DisciplineStudentRecords.FirstOrDefault(x => x.DisciplineId == dlt.DisciplineLesson.DisciplineId && x.Semester == dlt.DisciplineLesson.Semester &&
                                                                                        x.StudentId == st.Id);
                        if (dsr != null)
                        {
                        }
                        var dltsa = _context.DisciplineLessonTaskStudentAccepts
                                            .Include(x => x.DisciplineLessonTask)
                                            .Include(x => x.Student)
                                            .FirstOrDefault(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskId && x.StudentId == st.Id && !x.IsDeleted);
                        if (dltsa == null)
                        {
                            return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error("Error:", "DisciplineLessonTaskStudentAccepts not found", ResultServiceStatusCode.NotFound);
                        }

                        list.Add(ModelFactoryToViewModel.CreateDisciplineLessonTaskStudentAcceptViewModel(dltsa));
                    }

                    transaction.Commit();
                }

                return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Success(list);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<DisciplineLessonConductedViewModel>> GetFullDisciplineLessonConducteds(FullDisciplineLessonConductedBindingModel model)
        {
            try
            {
                List<DisciplineLessonConductedViewModel> list = new List<DisciplineLessonConductedViewModel>();
                Semesters sem = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
                var disciplineLessons = _context.DisciplineLessons
                                        .Include(x => x.TimeNorm)
                                        .Where(x => !x.IsDeleted &&
                                                    x.AcademicYearId == model.AcademicYearId &&
                                                    x.DisciplineId == model.DisciplineId &&
                                                    x.EducationDirectionId == model.EducationDirectionId &&
                                                    x.TimeNormId == model.TimeNormId &&
                                                    x.Semester == sem)
                                        .OrderBy(x => x.Order)
                                        .ToList();

                foreach (var discipLesson in disciplineLessons)
                {
                    List<string> subGroup = GetDisciplineLessonSubgroup(new DisciplineLessonSubgroupBindingModel
                    {
                        DisciplineId = model.DisciplineId,
                        StudentGroupId = model.StudentGroupId,
                        Semester = model.Semester
                    }).Result;
                    if (discipLesson.TimeNorm.KindOfLoadType == KindOfLoadType.Группа || discipLesson.TimeNorm.KindOfLoadType == KindOfLoadType.Поток)
                    {
                        subGroup.RemoveAll(x => x.Contains("Подгруппа"));
                    }
                    else if (discipLesson.TimeNorm.KindOfLoadType == KindOfLoadType.Подгруппа)
                    {
                        subGroup = GetDisciplineLessonSubgroup(new DisciplineLessonSubgroupBindingModel
                        {
                            DisciplineId = model.DisciplineId,
                            StudentGroupId = model.StudentGroupId,
                            Semester = model.Semester
                        }).Result;
                        subGroup.RemoveAt(0);
                    }
                    for (int i = 0; i < subGroup.Count; ++i)
                    {
                        string sg = subGroup[i];
                        var dlc = _context.DisciplineLessonConducteds
                                                .Include(x => x.DisciplineLesson)
                                                .Include(x => x.StudentGroup)
                                                .FirstOrDefault(x => x.StudentGroupId == model.StudentGroupId && 
                                                                    x.DisciplineLessonId == discipLesson.Id &&
                                                                    x.Subgroup == sg);
                        if (dlc == null)
                        {
                            dlc = ModelFacotryFromBindingModel.CreateDisciplineLessonConducted(new DisciplineLessonConductedSetBindingModel
                            {
                                DisciplineLessonId = discipLesson.Id,
                                StudentGroupId = model.StudentGroupId,
                                Date = DateTime.Now,
                                Subgroup = sg
                            });

                            _context.DisciplineLessonConducteds.Add(dlc);
                            _context.SaveChanges();
                        }

                        list.Add(ModelFactoryToViewModel.CreateDisciplineLessonConductedViewModel(dlc));
                    }
                }

                return ResultService<List<DisciplineLessonConductedViewModel>>.Success(list);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<DisciplineLessonConductedViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<DisciplineLessonConductedViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
