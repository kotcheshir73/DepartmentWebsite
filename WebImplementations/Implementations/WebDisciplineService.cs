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

        public ResultService<WebDisciplineContentInfo> GetDisciplineContentInfo(WebDisciplineContentInfoBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Disciplines
                                .FirstOrDefault(x => x.Id == model.DisciplineId);
                    if (entity == null)
                    {
                        return ResultService<WebDisciplineContentInfo>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebDisciplineContentInfo>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    var query = context.AcademicPlanRecordMissions
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                        .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id &&
                                        x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId == model.DisciplineId)
                        .Select(x => x.LecturerId)
                        .Distinct()
                        .ToList();

                    var users = context.DepartmentUsers.Where(x => x.LecturerId != null && query.Contains(x.LecturerId.Value)).Select(x => x.Id);

                    return ResultService<WebDisciplineContentInfo>.Success(new WebDisciplineContentInfo
                    {
                        DisciplineName = entity.DisciplineName,
                        Lecturers = users.ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebDisciplineContentInfo>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<WebDisciplineFolderNames>> GetDisciplineFolderNames(WebDisciplineFolderNamesBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var list = context.AcademicPlanRecordMissions
                                .Include(x => x.Lecturer)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                                .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId == model.DisciplineId
                                    && x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id)
                                .Where(x => x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "Лек"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "Пр"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "Лаб"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "КП"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "КР"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "РГР"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "Реф"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "ЗсО"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "Зач"
                                    || x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName == "Экз")
                                .Select(x => new
                                {
                                    Semestr = x.AcademicPlanRecordElement.AcademicPlanRecord.Semester.ToString(),
                                    TimeNorm = x.AcademicPlanRecordElement.TimeNorm.TimeNormShortName,
                                })
                                .GroupBy(x => x.Semestr);



                    return ResultService<List<WebDisciplineFolderNames>>.Success(list.Select(x => new WebDisciplineFolderNames
                    {
                        Semester = x.Key,
                        FolderNames = x.Select(y => GetFolderName(y.TimeNorm)).ToList()
                    }).ToList());
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<WebDisciplineFolderNames>>.Error(ex, ResultServiceStatusCode.Error);
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

        private string GetFolderName(string source)
        {
            if (source == "КР")
            {
                return "Курсовая работа";
            }
            else if (source == "КП")
            {
                return "Курсовой проект";
            }
            else if(source == "Пр")
            {
                return "Практики";
            }
            else if (source == "Лаб")
            {
                return "Лабораторные";
            }
            else if (source == "Лек")
            {
                return "Лекции";
            }
            else if (source == "РГР")
            {
                return "РГР";
            }
            else if (source == "Реф")
            {
                return "Реферат";
            }
            else if (source == "ЗсО" || source == "Зач")
            {
                return "Зачет";
            }
            else if (source == "Экз")
            {
                return "Экзамен";
            }

            return source;
        }
    }
}