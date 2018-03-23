using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.Services
{
    public class LaboratoryProcessService : ILaboratoryProcessService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        public LaboratoryProcessService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService MakeClone(LaboratoryProcessMakeCloneBindingModels model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по материально-техническим ценностям");
                }

                var entity = _context.MaterialTechnicalValues
                                .FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                var newEntity = new MaterialTechnicalValue
                {
                    ClassroomId = entity.ClassroomId,
                    DateCreate = entity.DateCreate,
                    InventoryNumber = entity.InventoryNumber,
                    FullName = entity.FullName,
                    Description = entity.Description,
                    Location = entity.Location,
                    Cost = entity.Cost
                };

                _context.MaterialTechnicalValues.Add(newEntity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValueDocByClassrooms()
        {
            try
            {
                return ResultService.Success();
            }
            catch(Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
