using AcademicYearImplementations;
using AcademicYearInterfaces.ViewModels;
using BaseImplementations;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Services
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
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineBlock)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                        .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineBlock.Title.Contains("Дисциплины"))
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

        public static ResultService<WebProcessDisciplineListInfoViewModel> GetDisciplinesByCourses(DisciplineGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordElements.Where(x => !x.IsDeleted)
                        .Include(x => x.TimeNorm)
                        .Include(x => x.AcademicPlanRecord.Discipline.DisciplineBlock)
                        .Include(x => x.AcademicPlanRecord.Contingent.EducationDirection)
                                .Where(x => x.AcademicPlanRecord.Discipline.DisciplineBlock.Title.Contains("Дисциплины"))
                        .Where(x => x.TimeNorm.TimeNormShortName == "Экз" || x.TimeNorm.TimeNormShortName == "ЗсО" || x.TimeNorm.TimeNormShortName == "Зач")
                        .Where(x => x.TimeNorm.AcademicYearId == GetAcademicYear().Result.Id)
                        .Where(x => x.AcademicPlanRecord.ContingentId == model.ContingentId);
                        
                        
                     var result = query.Select(x => new WebProcessDisciplineForListViewModel
                        {
                            DisciplineName = x.AcademicPlanRecord.Discipline.DisciplineName,
                            Semester = (int)x.AcademicPlanRecord.Semester.Value,
                            TimeNormName = x.TimeNorm.KindOfLoadName
                        }).Distinct()
                        .OrderBy(x => x.Semester).ThenBy(x=>x.DisciplineName)
                        .ToList();

                    var educDir = query.FirstOrDefault().AcademicPlanRecord.Contingent.EducationDirection;

                    WebProcessDisciplineListInfoViewModel resultModel = new WebProcessDisciplineListInfoViewModel
                    {
                        Course = query.FirstOrDefault().AcademicPlanRecord.Contingent.Course.ToString(),
                        EducationDirectionName = educDir.Title + " " 
                            + (educDir.Description.Contains("бакалавр") ? "- Бакалавриат" : "- Магистратура"),
                        Discipline = result
                    };

                    return ResultService<WebProcessDisciplineListInfoViewModel>.Success(resultModel);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebProcessDisciplineListInfoViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService<AcademicYearViewModel> GetAcademicYear()
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

        public static ResultService<List<WebProcessFolderLoadSetBindingModel>> GetDiscipline(DisciplineGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions
                                .Include(x => x.Lecturer)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineBlock)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                                .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName == model.DisciplineName 
                                    && x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == GetAcademicYear().Result.Id)
                                .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineBlock.Title.Contains("Дисциплины"))
                                .Where(x => (x.AcademicPlanRecordElement.TimeNorm.TimeNormName == "Лекция") 
                                    || (x.AcademicPlanRecordElement.TimeNorm.TimeNormName == "Практическое занятие") 
                                    || (x.AcademicPlanRecordElement.TimeNorm.TimeNormName == "Лабораторное занятие") 
                                    || (x.AcademicPlanRecordElement.TimeNorm.TimeNormName.Contains("Руководство и прием курсовых")))
                                .Select(x => new WebProcessFolderLoadSetBindingModel
                                {
                                    DisciplineName = x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName,
                                    Semestr = x.AcademicPlanRecordElement.AcademicPlanRecord.Semester.ToString(),
                                    TimeNorm = x.AcademicPlanRecordElement.TimeNorm.TimeNormName,
                                    LecturerName = $"{x.Lecturer.LastName} {x.Lecturer.FirstName[0]}.{x.Lecturer.Patronymic[0]}."

                                }).ToList();

                    

                    return ResultService<List<WebProcessFolderLoadSetBindingModel>>.Success(entity);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<WebProcessFolderLoadSetBindingModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

    }
}