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
    public class GraficRecordService : IGraficRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public GraficRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService<GraficRecordPageViewModel> GetGraficRecords(GraficRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                int countPages = 0;
                var query = _context.GraficRecords.Where(record => !record.IsDeleted).AsQueryable();

                if(model.GraficId.HasValue)
                {
                    query = query.Where(record => record.GraficId == model.GraficId);
                }
                if(model.TimeNormId.HasValue)
                {
                    query = query.Where(record => record.TimeNormId == model.TimeNormId);
                }
                query = query.Include(record => record.Grafic.AcademicPlanRecord.Discipline).Include(record => record.TimeNorm);
                
                var result = new GraficRecordPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateGraficRecordViewModel).OrderBy(x => x.TimeNormName).ThenBy(x => x.WeekNumber).ToList()
                };

                return ResultService<GraficRecordPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<GraficRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<GraficRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<GraficRecordViewModel> GetGraficRecord(GraficRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.GraficRecords
                                .FirstOrDefault(record => record.Id == model.Id && !record.IsDeleted);
                if (entity == null)
                {
                    return ResultService<GraficRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<GraficRecordViewModel>.Success(ModelFactoryToViewModel.CreateGraficRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<GraficRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<GraficRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAllFindGraficRecord(AcademicYearGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных");
                }
                var Grafic = _context.Grafics.Where(record => !record.IsDeleted && record.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id)
                    .Include(record => record.AcademicPlanRecord.Contingent);
                foreach (var grfic in Grafic)
                {
                    var timeNorms = _context.TimeNorms.Where(record => !record.IsDeleted && record.AcademicYearId == model.Id
                    && (record.KindOfLoadName == "Лекционное занятие" || record.KindOfLoadName == "Практическое занятие" || record.KindOfLoadName == "Лабораторная работа")); //Получение трех норм времени для поиска 

                    var GraficRecord = _context.GraficRecords.Where(record => !record.IsDeleted && record.GraficId == grfic.Id);
                    foreach (var timenor in timeNorms)
                    {
                        if (GraficRecord.FirstOrDefault(record => record.GraficId == grfic.Id
                             && record.TimeNormId == timenor.Id) == null)
                        {
                            int countOfWeek = 0;
                            if (Convert.ToInt32(grfic.AcademicPlanRecord.Semester.Value) % 2 != 0)
                            {
                                countOfWeek = 16;
                            }
                            else
                            {
                                if (Convert.ToInt32(grfic.AcademicPlanRecord.Contingent.Course) == 8)
                                {
                                    countOfWeek = 7;
                                }
                                else { countOfWeek = 14; }
                            }

                            for (int i = 1; i <= countOfWeek; i++)
                            {
                                var entity = ModelFacotryFromBindingModel.CreateGraficRecord(new GraficRecordSetBindingModel()
                                {
                                    GraficId = grfic.Id,
                                    TimeNormId = timenor.Id,
                                    WeekNumber = i,
                                    Hours = 0.00
                                });
                                _context.GraficRecords.Add(entity);

                                var entityClassroom = ModelFacotryFromBindingModel.CreateGraficClassroom(new GraficClassroomSetBindingModel()
                                {
                                    GraficId = grfic.Id,
                                    TimeNormId = timenor.Id,
                                    ClassroomDescription = ""
                                });
                                _context.GraficClassrooms.Add(entityClassroom);

                            }

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

        public ResultService CreateGraficRecord(GraficRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateGraficRecord(model);

                _context.GraficRecords.Add(entity);
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

        public ResultService UpdateGraficRecord(GraficRecordSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.GraficRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ModelFacotryFromBindingModel.CreateGraficRecord(model, entity);

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

        public ResultService DeleteGraficRecord(GraficRecordGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных о ведомостях");
                }

                var entity = _context.GraficRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
