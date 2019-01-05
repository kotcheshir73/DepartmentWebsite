using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementation.Implementations
{
    public class ExaminationTemplateService : IExaminationTemplateService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.СоставлениеЭкзаменов;

        public ExaminationTemplateService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
            //    {
            //        throw new Exception("Нет доступа на чтение данных по направлениям");
            //    }

            //    int countPages = 0;
            //    var query = _context.ExaminationTemplates.Where(ed => !ed.IsDeleted).AsQueryable();

            //    query = query.OrderBy(ed => ed.Cipher);

            //    if (model.PageNumber.HasValue && model.PageSize.HasValue)
            //    {
            //        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
            //        query = query
            //                    .Skip(model.PageSize.Value * model.PageNumber.Value)
            //                    .Take(model.PageSize.Value);
            //    }

            //    var result = new EducationDirectionPageViewModel
            //    {
            //        MaxCount = countPages,
            //        List = query.Select(ModelFactoryToViewModel.CreateEducationDirectionViewModel).ToList()
            //    };

            //    return ResultService<EducationDirectionPageViewModel>.Success(result);
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    return ResultService<EducationDirectionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            //}
            //catch (Exception ex)
            //{
            //    return ResultService<EducationDirectionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            //}
        }

        public ResultService<ExaminationTemplateViewModel> GetExaminationTemplate(ExaminationTemplateGetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService CreateExaminationTemplate(ExaminationTemplateSetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService UpdateExaminationTemplate(ExaminationTemplateSetBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ResultService DeleteExaminationTemplate(ExaminationTemplateGetBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}