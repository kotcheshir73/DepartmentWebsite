using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class GraficClassroomService : IGraficClassroomService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public GraficClassroomService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<GraficClassroomPageViewModel> GetGraficClassrooms(GraficClassroomGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.GraficClassrooms.Where(Classroom => !Classroom.IsDeleted).AsQueryable();

                if(model.GraficId.HasValue)
                {
                    query = query.Where(Classroom => Classroom.GraficId == model.GraficId);
                }
                if(model.TimeNormId.HasValue)
                {
                    query = query.Where(Classroom => Classroom.TimeNormId == model.TimeNormId);
                }
                query = query.Include(Classroom => Classroom.Grafic).Include(Classroom => Classroom.TimeNorm);
                query = query.OrderBy(Classroom => Classroom.GraficId).ThenBy(Classroom => Classroom.TimeNormId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }
                
                var result = new GraficClassroomPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateGraficClassroomViewModel).ToList()
                };

                return ResultService<GraficClassroomPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<GraficClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<GraficClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<GraficClassroomViewModel> GetGraficClassroom(GraficClassroomGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.GraficClassrooms
                                .FirstOrDefault(Classroom => Classroom.Id == model.Id && !Classroom.IsDeleted);
                if (entity == null)
                {
                    return ResultService<GraficClassroomViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<GraficClassroomViewModel>.Success(ModelFactoryToViewModel.CreateGraficClassroomViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<GraficClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<GraficClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateGraficClassroom(GraficClassroomSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateGraficClassroom(model);

                _context.GraficClassrooms.Add(entity);
                _context.SaveChanges();

                return ResultService.Success(entity.Id);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateGraficClassroom(GraficClassroomSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.GraficClassrooms.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateGraficClassroom(model, entity);

                _context.SaveChanges();

                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteGraficClassroom(GraficClassroomGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных о ведомостях");
                }

                var entity = _context.GraficClassrooms.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity.IsDeleted = true;
                entity.DateDelete = DateTime.Now;

                _context.SaveChanges();

                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
