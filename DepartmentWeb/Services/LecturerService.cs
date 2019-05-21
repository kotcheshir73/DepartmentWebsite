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

        public static ResultService<List<LecturerViewModel>> GetLecturers(LecturerGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Lecturers.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.LecturerPost.Hours).ThenBy(x => x.LastName);
                    
                    query = query.Include(x => x.LecturerPost).Include(x => x.LecturerWorkloads);

                    var result = query.Select(ModelFactoryToViewModel.CreateLecturerViewModel).ToList();

                    List<string> orderList = new List<string>
                    {
                        "ЗаведующийКафедрой",
                        "ЗаместительЗаведующегоКафедрой"
                    };

                    var newRes = new List<LecturerViewModel>();

                    foreach(var item in orderList)
                    {
                        var tmp = result.FirstOrDefault(x => x.Post == item);
                        newRes.Add(tmp);
                        result.Remove(tmp);
                    }
                    foreach(var item in result)
                    {
                        newRes.Add(item);
                    }

                    return ResultService<List<LecturerViewModel>>.Success(newRes);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<LecturerViewModel>>.Error(ex, ResultServiceStatusCode.Error);
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