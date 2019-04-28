using BaseImplementations;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;

namespace DepartmentWeb.Services
{
    public class LecturerService
    {

        public static ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            try
            {
                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Lecturers.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.LecturerPost.Hours).ThenBy(x => x.Post).ThenBy(x => x.LastName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.LecturerPost).Include(x => x.LecturerWorkloads);

                    var result = new LecturerPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateLecturerViewModel).ToList()
                    };

                    return ResultService<LecturerPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<LecturerPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model)
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
                        return ResultService<LecturerViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<LecturerViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<LecturerViewModel>.Success(ModelFactoryToViewModel.CreateLecturerViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}