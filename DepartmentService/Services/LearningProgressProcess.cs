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
    }
}
