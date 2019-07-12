using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using WebImplementations.Helpers;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebDisciplineService : IWebDisciplineService
    {
        public ResultService<WebDisciplinePageViewModel> GetDisciplines(WebDisciplineGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordElements
                        .Where(x => !x.IsDeleted)
                        .Where(x => x.TimeNorm.TimeNormShortName == "Экз" || x.TimeNorm.TimeNormShortName == "ЗсО" || x.TimeNorm.TimeNormShortName == "Зач" || 
                                                            x.TimeNorm.TimeNormShortName == "КР" || x.TimeNorm.TimeNormShortName == "КП")
                                                            // TODO - практики, ВКР, Госы
                        .Where(x => x.TimeNorm.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id)
                        .Where(x => x.AcademicPlanRecord.ContingentId == model.ContingentId)

                        .Include(x => x.TimeNorm)
                        .Include(x => x.AcademicPlanRecord.Discipline.DisciplineBlock)
                        .Include(x => x.AcademicPlanRecord.Contingent.EducationDirection);


                    var result = query.Select(x => new WebDisciplineViewModel
                                                    {
                                                        Id = x.AcademicPlanRecord.DisciplineId,
                                                        DisciplineName = x.AcademicPlanRecord.Discipline.DisciplineName,
                                                        DisciplineDescription = x.AcademicPlanRecord.Discipline.DisciplineDescription,
                                                        Semester = (int)x.AcademicPlanRecord.Semester.Value,
                                                    })
                                    .Distinct()
                                    .OrderBy(x => x.Semester).ThenBy(x => x.DisciplineName)
                                    .ToList();

                    var educDir = query.FirstOrDefault().AcademicPlanRecord.Contingent.EducationDirection;

                    WebDisciplinePageViewModel resultModel = new WebDisciplinePageViewModel
                    {
                        Course = query.FirstOrDefault().AcademicPlanRecord.Contingent.Course.ToString(),
                        EducationDirectionName = educDir.Title + " "
                            + (educDir.Description.Contains("бакалавр") ? "- Бакалавриат" : "- Магистратура"),
                        List = result
                    };

                    return ResultService<WebDisciplinePageViewModel>.Success(resultModel);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebDisciplinePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebDisciplineViewModel> GetDiscipline(WebDisciplineGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService UpdateDiscipline(WebDisciplineSetBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
