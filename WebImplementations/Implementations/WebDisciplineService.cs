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
    public class WebDisciplineService : IWebDisciplineService
    {
        public ResultService<WebDisciplineViewModel> GetDiscipline(WebDisciplineGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Disciplines
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<WebDisciplineViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebDisciplineViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    var query = context.AcademicPlanRecordMissions
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.Lecturer)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                        .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id &&
                                        x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId == model.Id)
                        .Select(x => new
                        {
                            Id = x.LecturerId,
                            LecturerFIO = x.Lecturer.ToString()
                        })
                        .Distinct()
                        .ToList();

                    var discipline = WebModelFactoryToViewModel.CreateWebDisciplineViewModel(entity);

                    if (query != null)
                    {
                        discipline.DisciplineLecturers = query.Select(x => new Tuple<Guid, string>(x.Id, x.LecturerFIO)).ToList();
                    }

                    return ResultService<WebDisciplineViewModel>.Success(discipline);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebDisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebDisciplineViewModel> GetDisciplineName(WebDisciplineGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Disciplines
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<WebDisciplineViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebDisciplineViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    var discipline = WebModelFactoryToViewModel.CreateWebDisciplineViewModel(entity);

                    return ResultService<WebDisciplineViewModel>.Success(discipline);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebDisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateDiscipline(WebDisciplineSetBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
