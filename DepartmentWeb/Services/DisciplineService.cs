using AcademicYearImplementations;
using AcademicYearInterfaces.ViewModels;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;

namespace DepartmentWeb.Services
{
    public class DisciplineService
    {

        public static ResultService<List<LearningProcessDisciplineViewModel>> GetDisciplines(BaseInterfaces.BindingModels.LecturerGetBindingModel modelL)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    
                    var query = context.AcademicPlanRecordMissions
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                        .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == GetAcademicYear().Result.Id &&                                        
                                        x.LecturerId == modelL.Id)
                        .Select(x => new LearningProcessDisciplineViewModel
                        {
                            Id = x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId,
                            DisciplineName = x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName
                        })
                        .Distinct()
                        .ToList();

                    return ResultService<List<LearningProcessDisciplineViewModel>>.Success(query);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<LearningProcessDisciplineViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private static ResultService<AcademicYearViewModel> GetAcademicYear()
        {
            try
            {                
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicYears
                                .Where(x => !x.IsDeleted).ToList();

                    if (DateTime.Now.Month < 9)
                    {
                        var model = entity.FirstOrDefault(x => x.Title.Split('/')[1] == DateTime.Now.Year.ToString());
                        return ResultService<AcademicYearViewModel>.Success(AcademicYearModelFactoryToViewModel
                            .CreateAcademicYearViewModel(model));
                    }
                    else {
                        var model = entity.FirstOrDefault(x => x.Title.Split('/')[0] == DateTime.Now.Year.ToString());
                        return ResultService<AcademicYearViewModel>.Success(AcademicYearModelFactoryToViewModel
                            .CreateAcademicYearViewModel(model));
                    }

                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicYearViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

    }
}