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
    public class IndividualPlanRecordService : IIndividualPlanRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public IndividualPlanRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<IndividualPlanRecordPageViewModel> GetIndividualPlanRecords(IndividualPlanRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.IndividualPlanRecords.Where(aprm => !aprm.IsDeleted).AsQueryable();

                if(model.IndividualPlanKindOfWorkId.HasValue)
                {
                    query = query.Where(aprm => aprm.IndividualPlanKindOfWorkId == model.IndividualPlanKindOfWorkId);
                }
                if(model.LecturerId.HasValue)
                {
                    query = query.Where(aprm => aprm.LecturerId == model.LecturerId);
                }
                if (!string.IsNullOrEmpty(model.Title))
                {
                    query = query.Where(aprm => aprm.IndividualPlanKindOfWorks.IndividualPlanTitle.Title == model.Title);
                }

                query = query.OrderBy(apre => apre.IndividualPlanKindOfWorkId).ThenBy(apre => apre.LecturerId);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(apre => apre.IndividualPlanKindOfWorks.IndividualPlanTitle); // для вложенных запросов




                var result = new IndividualPlanRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateIndividualPlanRecordViewModel).ToList() // в CreateIndividualPlanRecordViewModel прописываем те столбцы из связанных таблиц которые нам нужны
                    // и еще в DepartmentService.ViewModels добавляем те строки из др таблицы которые надо выводить
                };

                return ResultService<IndividualPlanRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanRecordViewModel> GetIndividualPlanRecord(IndividualPlanRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanRecords
                                .FirstOrDefault(aprm => aprm.Id == model.Id && !aprm.IsDeleted);
                if (entity == null)
                {
                    return ResultService<IndividualPlanRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<IndividualPlanRecordViewModel>.Success(ModelFactoryToViewModel.CreateIndividualPlanRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<IndividualPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateIndividualPlanRecord(model);

                _context.IndividualPlanRecords.Add(entity);
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

        public ResultService UpdateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                //_context.IndividualPlanRecords.Attach(entity);   //не уверен что так правильно

                entity = ModelFacotryFromBindingModel.CreateIndividualPlanRecord(model, entity);

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

        public ResultService CreateAllFindIndividualPlanRecords(AcademicYearSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных");
                }
                var kindOfWorks = _context.IndividualPlanKindOfWorks.Where(record => !record.IsDeleted);
                var lecturers = _context.Lecturers.Where(record => !record.IsDeleted);
                foreach (var lec in lecturers)
                {
                    var lecturersTimes = _context.IndividualPlanRecords.Where(record => !record.IsDeleted && record.LecturerId == lec.Id && record.AcademicYearId == model.Id);
                    foreach (var kindOfW in kindOfWorks)
                    {
                        if (lecturersTimes.FirstOrDefault(record => record.IndividualPlanKindOfWorkId == kindOfW.Id) == null)
                        {
                            var entity = ModelFacotryFromBindingModel.CreateIndividualPlanRecord(new IndividualPlanRecordSetBindingModel()
                            {
                                AcademicYearId = model.Id,
                                LecturerId = lec.Id,
                                IndividualPlanKindOfWorkId = kindOfW.Id,
                                PlanAutumn = 0.0,
                                PlanSpring = 0.0,
                                FactAutumn = 0.0,
                                FactSpring = 0.0

                            });
                            _context.IndividualPlanRecords.Add(entity);
                        }
                    }
                }
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

        public ResultService DeleteIndividualPlanRecord(IndividualPlanRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по элементам записей учебного плана");
                }

                var entity = _context.IndividualPlanRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
