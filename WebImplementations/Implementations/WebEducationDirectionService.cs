using Enums;
using System;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebEducationDirectionService : IWebEducationDirectionService
    {
        public ResultService<WebEducationDirectionPageViewModel> GetEducationDirections(WebEducationDirectionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.EducationDirections.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.Cipher);

                    WebEducationDirectionPageViewModel result = new WebEducationDirectionPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebEducationDirectionViewModel).ToList()
                    };

                    return ResultService<WebEducationDirectionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebEducationDirectionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebEducationDirectionViewModel> GetEducationDirection(WebEducationDirectionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.EducationDirections
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<WebEducationDirectionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebEducationDirectionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    return ResultService<WebEducationDirectionViewModel>.Success(WebModelFactoryToViewModel.CreateWebEducationDirectionViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebEducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}