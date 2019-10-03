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
    public class WebLecturerService : IWebLecturerService
    {
        public ResultService<WebLecturerPageViewModel> GetLecturers(WebLecturerGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Lecturers.Where(x => !x.IsDeleted && !x.OnlyForPrivate).AsQueryable();

                    query = query.OrderBy(x => x.LecturerPost.Hours).ThenBy(x => x.LastName);

                    query = query.Include(x => x.LecturerPost).Include(x => x.LecturerWorkloads);

                    var queryResult = query.Select(WebModelFactoryToViewModel.CreateWebLecturerViewModel).ToList();

                    List<string> orderList = new List<string>
                    {
                        "ЗаведующийКафедрой",
                        "ЗаместительЗаведующегоКафедрой"
                    };

                    var result = new WebLecturerPageViewModel
                    {
                        List = new List<WebLecturerViewModel>()
                    };

                    //TODO добавить order к LecturerPost
                    foreach (var item in orderList)
                    {
                        var tmp = queryResult.FirstOrDefault(x => x.Post == item);
                        result.List.Add(tmp);
                        queryResult.Remove(tmp);
                    }
                    foreach (var item in queryResult)
                    {
                        result.List.Add(item);
                    }

                    return ResultService<WebLecturerPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebLecturerPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebLecturerViewModel> GetLecturer(WebLecturerGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Lecturers
                                .Include(x => x.LecturerPost)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<WebLecturerViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebLecturerViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    var query = context.AcademicPlanRecordMissions
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                        .Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id &&
                                        x.LecturerId == model.Id)
                        .Select(x => new
                        {
                            Id = x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId,
                            x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName
                        })
                        .Distinct()
                        .ToList();

                    var lecturer = WebModelFactoryToViewModel.CreateWebLecturerViewModel(entity);

                    if (query != null)
                    {
                        lecturer.Disiplines = query.Select(x => new Tuple<Guid, string>(x.Id, x.DisciplineName)).ToList();
                    }

                    return ResultService<WebLecturerViewModel>.Success(lecturer);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebLecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}