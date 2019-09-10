using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class StreamLessonRecordService : IStreamLessonRecordService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly string _entity = "Записи потока";

        private readonly IStreamLessonService _serviceS;

        private readonly IAcademicPlanService _serviceAP;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        public StreamLessonRecordService(IStreamLessonService serviceS, IAcademicPlanService serviceAP, IAcademicPlanRecordService serviceAPR, IAcademicPlanRecordElementService serviceAPRE)
        {
            _serviceS = serviceS;
            _serviceAP = serviceAP;
            _serviceAPR = serviceAPR;
            _serviceAPRE = serviceAPRE;
        }
        
        public ResultService<StreamLessonPageViewModel> GetStreamLessons(StreamLessonGetBindingModel model)
        {
            return _serviceS.GetStreamLessons(model);
        }

        public ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model)
        {
            return _serviceAP.GetAcademicPlans(model);
        }

        public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
        {
            return _serviceAPR.GetAcademicPlanRecords(model);
        }

        public ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model)
        {
            return _serviceAPRE.GetAcademicPlanRecordElements(model);
        }

        public ResultService<AcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model)
        {
            return _serviceAPRE.GetAcademicPlanRecordElement(model);
        }

        public ResultService<StreamLessonRecordPageViewModel> GetStreamLessonRecords(StreamLessonRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StreamLessonRecords.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.SteamLessonId.HasValue)
                    {
                        query = query.Where(x => x.StreamLessonId == model.SteamLessonId);
                    }

                    query = query.OrderBy(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection.Cipher);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                        .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                        .Include(x => x.StreamLesson);

                    var result = new StreamLessonRecordPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateStreamLessonRecordViewModel).ToList()
                    };

                    return ResultService<StreamLessonRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<StreamLessonRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<StreamLessonRecordViewModel> GetStreamLessonRecord(StreamLessonRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessonRecords
                                .Include(x => x.AcademicPlanRecordElement)
                                .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                                .Include(x => x.StreamLesson)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<StreamLessonRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StreamLessonRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StreamLessonRecordViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateStreamLessonRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StreamLessonRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStreamLessonRecord(StreamLessonRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateStreamLessonRecord(model);

                    var exsistEntity = context.StreamLessonRecords.FirstOrDefault(x => x.StreamLessonId == entity.StreamLessonId && 
                            x.AcademicPlanRecordElementId == entity.AcademicPlanRecordElementId);
                    if (exsistEntity == null)
                    {
                        context.StreamLessonRecords.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            context.SaveChanges();
                            return ResultService.Success(exsistEntity.Id);
                        }
                        else
                        {
                            return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateStreamLessonRecord(StreamLessonRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessonRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateStreamLessonRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStreamLessonRecord(StreamLessonRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessonRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}