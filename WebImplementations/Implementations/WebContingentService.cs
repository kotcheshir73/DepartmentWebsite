using Enums;
using System;
using System.Linq;
using Tools;
using WebImplementations.Helpers;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebContingentService : IWebContingentService
    {
        public ResultService<WebContingentPageViewModel> GetCourseByContingents(WebContingentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Contingents.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.Where(x => x.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id);

                    if (model.EducationDirectionId.HasValue)
                    {
                        query = query.Where(x => x.EducationDirectionId == model.EducationDirectionId);
                    }

                    query = query.OrderBy(x => x.Course);

                    WebContingentPageViewModel result = new WebContingentPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebContingentViewModel).ToList()
                    };

                    return ResultService<WebContingentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebContingentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}