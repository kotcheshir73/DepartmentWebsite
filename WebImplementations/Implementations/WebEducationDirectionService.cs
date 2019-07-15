using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;
using WebImplementations.Helpers;
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
                    var query = context.Contingents
                        .Include(x => x.EducationDirection)
                        .Where(x => !x.EducationDirection.IsDeleted && x.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id)
                        .GroupBy(x => x.EducationDirection);

                    query = query.OrderBy(x => x.Key.Qualification).ThenBy(x => x.Key.Cipher);

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
                    var entity = context.Contingents
                        .Include(x => x.EducationDirection)
                        .Where(x => !x.EducationDirection.IsDeleted && x.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id && x.EducationDirectionId == model.Id)
                        .GroupBy(x => x.EducationDirection)
                        .FirstOrDefault();

                    if (entity == null)
                    {
                        return ResultService<WebEducationDirectionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.Key.IsDeleted)
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