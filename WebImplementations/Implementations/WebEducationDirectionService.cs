using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebImplementations.Helpers;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebEducationDirectionService : IWebEducationDirectionService
    {
        public ResultService<WebEducationDirectionPageViewModel> GetEducationDirections(WebEducationDirectionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Contingents
                        .Include(x => x.EducationDirection)
                        .Where(x => !x.EducationDirection.IsDeleted && x.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id)
                        .GroupBy(x => x.EducationDirection);

                    query = query.OrderBy(x => x.Key.Qualification).ThenBy(x => x.Key.Cipher);

                    WebEducationDirectionPageViewModel result = new WebEducationDirectionPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebEducationDirectionViewModel).ToList()
                    };

                    return ResultService<WebEducationDirectionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebEducationDirectionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebEducationDirectionViewModel> GetEducationDirection(WebEducationDirectionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents
                        .Include(x => x.EducationDirection)
                        .Where(x => !x.EducationDirection.IsDeleted && x.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id && x.EducationDirectionId == model.Id)
                        .GroupBy(x => x.EducationDirection)
                        .FirstOrDefault();

                    if (entity == null)
                    {
                        return ResultService<WebEducationDirectionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.Key.IsDeleted)
                    {
                        return ResultService<WebEducationDirectionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebEducationDirectionViewModel>.Success(WebModelFactoryToViewModel.CreateWebEducationDirectionViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebEducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<WebEducationDirectionDisciplineByCoursesViewModel>> GetDisciplinesByCourses(WebEducationDirectionDisciplineListInfoBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordElements.Where(x => !x.IsDeleted)
                        .Include(x => x.TimeNorm)
                        .Include(x => x.AcademicPlanRecord.Discipline.DisciplineBlock)
                        .Include(x => x.AcademicPlanRecord.Contingent.EducationDirection)
                        // TODO практики и госы
                        .Where(x => x.TimeNorm.TimeNormShortName == "Экз" || x.TimeNorm.TimeNormShortName == "ЗсО" || x.TimeNorm.TimeNormShortName == "Зач"
                                    || x.TimeNorm.TimeNormShortName == "КР" || x.TimeNorm.TimeNormShortName == "КП")
                        .Where(x => x.TimeNorm.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id && x.AcademicPlanRecord.ContingentId == model.CourseId);


                    var result = query.Select(x => new WebEducationDirectionDisciplineByCoursesViewModel
                    {
                        DisciplineId = x.AcademicPlanRecord.DisciplineId,
                        DisciplineName = x.AcademicPlanRecord.Discipline.DisciplineName,
                        Semester = (int)x.AcademicPlanRecord.Semester.Value,
                        TimeNormName = x.TimeNorm.KindOfLoadName
                    }).Distinct()
                       .OrderBy(x => x.Semester).ThenBy(x => x.DisciplineName)
                       .ToList();

                    return ResultService<List<WebEducationDirectionDisciplineByCoursesViewModel>>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<WebEducationDirectionDisciplineByCoursesViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}