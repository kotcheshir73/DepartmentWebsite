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
                        x.Hours
                    })
                    .GroupBy(x => x.TimeNormId)
                    .ToList();

                List<LearningProcessDisciplineDetailViewModel> result = new List<LearningProcessDisciplineDetailViewModel>();

                foreach (var elem in query)
                {
                    StringBuilder sb = new StringBuilder(elem.First().TimeNormName);
                    sb.Append(": ");
                    foreach (var subElem in elem.OrderBy(x => x.Semester))
                    {
                        sb.Append(subElem.Semester);
                        sb.Append(" - ");
                        sb.Append(subElem.Hours);
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

        public ResultService FormDisciplineLessonTaskss(LearningProcessFormDisciplineLessonTasksBindingModel model)
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
    }
}
