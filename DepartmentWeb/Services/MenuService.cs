using BaseImplementations;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tools;

namespace DepartmentWeb.Services
{
    public class MenuService
    {

        public static ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

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
    }
}