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

                var disciplineIds = _context.AcademicPlanRecordMissions
                    .Include(x => x.AcademicPlanRecordElement)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                    .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId &&
                                        x.LecturerId == user.LecturerId)
                    .Select(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId)
                    .ToList();

                var query = _context.Disciplines.Where(x => !x.IsDeleted && x.DisciplineBlockId == disciplineBlockModuls.Id && disciplineIds.Contains(x.Id)).OrderBy(x => x.DisciplineName);

                return ResultService<List<LearningProcessDisciplineViewModel>>.Success(query.Select(x => new LearningProcessDisciplineViewModel
                {
                    Id = x.Id,
                    DisciplineName = x.DisciplineName
                }).ToList());
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
    }
}
