using DatabaseContext;
using System;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebClassroomService : IWebClassroomService
    {
        public ResultService<WebClassroomPageViewModel> GetClassrooms(WebClassroomGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Classrooms.Where(x => !x.IsDeleted && !x.NotUseInSchedule).AsQueryable();

                    query = query.OrderBy(x => x.Number);

                    var result = new WebClassroomPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebClassroomViewModel).ToList()
                    };

                    return ResultService<WebClassroomPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}