using DatabaseContext;
using System;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebStudentGroupService : IWebStudentGroupService
    {
        public ResultService<WebStudentGroupPageViewModel> GetStudentGroups(WebStudentGroupGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StudentGroups.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.Course).ThenBy(x => x.EducationDirectionId).ThenBy(x => x.GroupName);

                    var result = new WebStudentGroupPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebStudentGroupViewModel).ToList()
                    };

                    return ResultService<WebStudentGroupPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebStudentGroupPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}