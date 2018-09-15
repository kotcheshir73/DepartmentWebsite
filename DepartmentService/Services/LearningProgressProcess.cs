using DepartmentModel;
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

        public ResultService<List<DisciplineLessonTaskVariantViewModel>> GetDisciplineLessonTaskVariants(GetDisciplineLessonTaskVariants model)
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

        public ResultService<List<DisciplineLessonTaskViewModel>> GetDisiplineLessonTasksForDuplicate(GetDisiplineLessonTasksForDuplicate model)
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

        public ResultService DuplicateDisiplineLessonTasks(DuplicateDisiplineLessonTasks model)
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

        public ResultService<List<DisciplineLessonViewModel>> GetDisiplineLessonsForDuplicate(GetDisiplineLessonsForDuplicate model)
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

        public ResultService DuplicateDisiplineLessons(DuplicateDisiplineLessons model)
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

        public ResultService<List<DisciplineStudentRecordViewModel>> GetDisciplineStudentRecordsForFill(DisciplineStudentRecordsForFill model)
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

        public ResultService<List<string>> GetDisciplineLessonSubgroup(DisciplineLessonSubgroup model)
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

        public ResultService<List<DisciplineLessonConductedStudentViewModel>> GetDisciplineLessonConductedStudentsForFill(DisciplineLessonConductedStudentsForFill model)
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
                    int subgroup = Convert.ToInt32(dlc.Subgroup.Split(' ')[1]);
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
    }
}
