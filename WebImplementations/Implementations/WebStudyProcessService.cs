using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace WebImplementations.Implementations
{
    public class WebStudyProcessService : IWebStudyProcessService
    {
        public ResultService<WebAcademicYearPageViewModel> GetAcademicYears(WebAcademicYearGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicYears
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query
                        .OrderBy(x => x.Title);

                    var result = new WebAcademicYearPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebAcademicYearViewModel).ToList()
                    };

                    return ResultService<WebAcademicYearPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicYearPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            throw new NotImplementedException();
        }

        public ResultService<WebAcademicYearViewModel> GetAcademicYear(WebAcademicYearGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicYears
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebAcademicYearViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebAcademicYearViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebAcademicYearViewModel>.Success(WebModelFactoryToViewModel.CreateWebAcademicYearViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicYearViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicYear(WebAcademicYearSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateAcademicYear(model);

                    var exsistEntity = context.AcademicYears.FirstOrDefault(x => x.Title == entity.Title);
                    if (exsistEntity == null)
                    {
                        context.AcademicYears.Add(entity);
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

        public ResultService UpdateAcademicYear(WebAcademicYearSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicYears.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateAcademicYear(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicYear(WebAcademicYearGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicYears.FirstOrDefault(x => x.Id == model.Id);
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

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public ResultService<WebAcademicPlanPageViewModel> GetAcademicPlans(WebAcademicPlanGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlans
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query
                        .OrderBy(x => x.AcademicYear.Title)
                        .ThenBy(x => x.EducationDirection.Cipher)
                        .ThenBy(x => x.AcademicCourses);

                    query = query
                        .Include(x => x.AcademicYear)
                        .Include(x => x.EducationDirection);

                    var result = new WebAcademicPlanPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebAcademicPlanViewModel).ToList()
                    };

                    return ResultService<WebAcademicPlanPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebAcademicPlanViewModel> GetAcademicPlan(WebAcademicPlanGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlans
                                .Include(x => x.AcademicYear)
                                .Include(x => x.EducationDirection)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebAcademicPlanViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebAcademicPlanViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebAcademicPlanViewModel>.Success(WebModelFactoryToViewModel.CreateWebAcademicPlanViewModel(entity));

                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlan(WebAcademicPlanSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateAcademicPlan(model);

                    var exsistEntity = context.AcademicPlans
                        .FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId
                            && x.EducationDirectionId == entity.EducationDirectionId
                            && x.AcademicCourses == entity.AcademicCourses);

                    if (exsistEntity == null)
                    {
                        context.AcademicPlans.Add(entity);
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

        public ResultService UpdateAcademicPlan(WebAcademicPlanSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context
                        .AcademicPlans
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateAcademicPlan(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicPlan(WebAcademicPlanGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlans
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------

        public ResultService<WebAcademicPlanRecordPageViewModel> GetAcademicPlanRecords(WebAcademicPlanRecordGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecords
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicPlanId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanId == model.AcademicPlanId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query
                        .OrderBy(x => x.Semester)
                        .ThenBy(x => x.Discipline.DisciplineName);

                    query = query
                        .Include(x => x.Discipline)
                        .Include(x => x.Contingent);

                    var result = new WebAcademicPlanRecordPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebAcademicPlanRecordViewModel).ToList()
                    };

                    return ResultService<WebAcademicPlanRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebAcademicPlanRecordViewModel> GetAcademicPlanRecord(WebAcademicPlanRecordGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecords
                                .Include(x => x.Discipline)
                                .Include(x => x.Contingent)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebAcademicPlanRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebAcademicPlanRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebAcademicPlanRecordViewModel>.Success(WebModelFactoryToViewModel.CreateWebAcademicPlanRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecord(WebAcademicPlanRecordSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateAcademicPlanRecord(model);

                    var exsistEntity = context.AcademicPlanRecords
                        .FirstOrDefault(x => x.AcademicPlanId == entity.AcademicPlanId
                            && x.ContingentId == entity.ContingentId
                            && x.DisciplineId == entity.DisciplineId
                            && x.Semester == entity.Semester);

                    if (exsistEntity == null)
                    {
                        context.AcademicPlanRecords.Add(entity);
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

        public ResultService UpdateAcademicPlanRecord(WebAcademicPlanRecordSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecords
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = WebModelFacotryFromBindingModel.CreateAcademicPlanRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicPlanRecord(WebAcademicPlanRecordGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecords
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------

        public ResultService<WebAcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(WebAcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordElements
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicPlanRecordId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordId == model.AcademicPlanRecordId);
                    }
                    if (model.TimeNormId.HasValue)
                    {
                        query = query.Where(x => x.TimeNormId == model.TimeNormId);
                    }

                    query = query
                        .OrderBy(x => x.AcademicPlanRecordId)
                        .ThenBy(x => x.TimeNormId);

                    query = query
                        .Include(x => x.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecord.Discipline)
                        .Include(x => x.TimeNorm);

                    var result = new WebAcademicPlanRecordElementPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebAcademicPlanRecordElementViewModel).ToList()
                    };

                    return ResultService<WebAcademicPlanRecordElementPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanRecordElementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebAcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(WebAcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordElements
                        .Include(x => x.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecord.Discipline)
                        .Include(x => x.TimeNorm)
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebAcademicPlanRecordElementViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebAcademicPlanRecordElementViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebAcademicPlanRecordElementViewModel>.Success(WebModelFactoryToViewModel.CreateWebAcademicPlanRecordElementViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanRecordElementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecordElement(WebAcademicPlanRecordElementSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(model);

                    var exsistEntity = context.AcademicPlanRecordElements
                        .FirstOrDefault(x => x.AcademicPlanRecordId == entity.AcademicPlanRecordId
                            && x.TimeNormId == entity.TimeNormId);

                    if (exsistEntity == null)
                    {
                        context.AcademicPlanRecordElements.Add(entity);
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

        public ResultService UpdateAcademicPlanRecordElement(WebAcademicPlanRecordElementSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordElements
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = WebModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicPlanRecordElement(WebAcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordElements
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------

        public ResultService<WebAcademicPlanRecordMissionPageViewModel> GetAcademicPlanRecordMissions(WebAcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordMissions
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicPlanRecordElementId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordElementId == model.AcademicPlanRecordElementId);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }
                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId);
                    }

                    query = query
                        .OrderBy(x => x.AcademicPlanRecordElementId)
                        .ThenBy(x => x.LecturerId);

                    query = query
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                        .Include(x => x.Lecturer);

                    var result = new WebAcademicPlanRecordMissionPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebAcademicPlanRecordMissionViewModel).ToList()
                    };

                    return ResultService<WebAcademicPlanRecordMissionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanRecordMissionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebAcademicPlanRecordMissionViewModel> GetAcademicPlanRecordMission(WebAcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions
                                .Include(x => x.AcademicPlanRecordElement)
                                .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                                .Include(x => x.Lecturer)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebAcademicPlanRecordMissionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebAcademicPlanRecordMissionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebAcademicPlanRecordMissionViewModel>.Success(WebModelFactoryToViewModel.CreateWebAcademicPlanRecordMissionViewModel(entity));

                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAcademicPlanRecordMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecordMission(WebAcademicPlanRecordMissionSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateAcademicPlanRecordMission(model);

                    var exsistEntity = context.AcademicPlanRecordMissions
                        .FirstOrDefault(x => x.AcademicPlanRecordElementId == entity.AcademicPlanRecordElementId
                            && x.LecturerId == entity.LecturerId);

                    if (exsistEntity == null)
                    {
                        context.AcademicPlanRecordMissions.Add(entity);
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

        public ResultService UpdateAcademicPlanRecordMission(WebAcademicPlanRecordMissionSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateAcademicPlanRecordMission(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicPlanRecordMission(WebAcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public ResultService<WebStreamLessonPageViewModel> GetStreamLessons(WebStreamLessonGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StreamLessons
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    query = query.
                        OrderBy(x => x.AcademicYear.Title).
                        ThenBy(x => x.StreamLessonName);

                    query = query
                        .Include(x => x.AcademicYear);

                    var result = new WebStreamLessonPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebStreamLessonViewModel).ToList()
                    };

                    return ResultService<WebStreamLessonPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebStreamLessonPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebStreamLessonViewModel> GetStreamLesson(WebStreamLessonGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessons
                                .Include(x => x.AcademicYear)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebStreamLessonViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebStreamLessonViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebStreamLessonViewModel>.Success(WebModelFactoryToViewModel.CreateWebStreamLessonViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebStreamLessonViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStreamLesson(WebStreamLessonSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateStreamLesson(model);

                    var exsistEntity = context.StreamLessons
                        .FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId
                            && x.StreamLessonName == entity.StreamLessonName
                            && x.Semester == entity.Semester);

                    if (exsistEntity == null)
                    {
                        context.StreamLessons.Add(entity);
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

        public ResultService UpdateStreamLesson(WebStreamLessonSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessons
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateStreamLesson(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStreamLesson(WebStreamLessonGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessons
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------

        public ResultService<WebStreamLessonRecordPageViewModel> GetStreamLessonRecords(WebStreamLessonRecordGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StreamLessonRecords
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.StreamLessonId.HasValue)
                    {
                        query = query.Where(x => x.StreamLessonId == model.StreamLessonId);
                    }

                    query = query
                        .Include(x => x.AcademicPlanRecordElement)
                        .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                        .Include(x => x.StreamLesson);

                    var result = new WebStreamLessonRecordPageViewModel
                    {
                        List = query.
                            Select(WebModelFactoryToViewModel.CreateWebStreamLessonRecordViewModel)
                            .OrderBy(x => x.AcademicPlanRecordElementText)
                            .ToList()
                    };

                    return ResultService<WebStreamLessonRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebStreamLessonRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebStreamLessonRecordViewModel> GetStreamLessonRecord(WebStreamLessonRecordGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessonRecords
                                .Include(x => x.AcademicPlanRecordElement)
                                .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                                .Include(x => x.StreamLesson)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebStreamLessonRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebStreamLessonRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebStreamLessonRecordViewModel>.Success(WebModelFactoryToViewModel.CreateWebStreamLessonRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebStreamLessonRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateStreamLessonRecord(WebStreamLessonRecordSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateStreamLessonRecord(model);

                    var exsistEntity = context.StreamLessonRecords
                        .FirstOrDefault(x => x.StreamLessonId == entity.StreamLessonId
                            && x.AcademicPlanRecordElementId == entity.AcademicPlanRecordElementId);

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

        public ResultService UpdateStreamLessonRecord(WebStreamLessonRecordSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessonRecords
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateStreamLessonRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteStreamLessonRecord(WebStreamLessonRecordGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamLessonRecords
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public ResultService<WebTimeNormPageViewModel> GetTimeNorms(WebTimeNormGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TimeNorms
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.DisciplineBlockId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineBlockId == model.DisciplineBlockId);
                    }

                    if (model.AcademicPlanRecordId.HasValue)
                    {
                        var apr = context.AcademicPlanRecords
                            .Include(x => x.AcademicPlan)
                            .FirstOrDefault(x => x.Id == model.AcademicPlanRecordId);

                        query = query.Where(x => x.AcademicYearId == apr.AcademicPlan.AcademicYearId);
                    }

                    query = query
                        .OrderBy(x => x.TimeNormOrder)
                        .ThenBy(x => x.TimeNormName);

                    query = query
                        .Include(x => x.AcademicYear)
                        .Include(x => x.DisciplineBlock);

                    var result = new WebTimeNormPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebTimeNormViewModel).ToList()
                    };

                    return ResultService<WebTimeNormPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebTimeNormPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebTimeNormViewModel> GetTimeNorm(WebTimeNormGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TimeNorms
                                .Include(x => x.AcademicYear)
                                .Include(x => x.DisciplineBlock)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebTimeNormViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebTimeNormViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebTimeNormViewModel>.Success(WebModelFactoryToViewModel.CreateWebTimeNormViewModel(entity));

                }
            }
            catch (Exception ex)
            {
                return ResultService<WebTimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateTimeNorm(WebTimeNormSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateTimeNorm(model);

                    var exsistEntity = context.TimeNorms
                        .FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId
                            && x.TimeNormName == entity.TimeNormName);

                    if (exsistEntity == null)
                    {
                        context.TimeNorms.Add(entity);
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

        public ResultService UpdateTimeNorm(WebTimeNormSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TimeNorms
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateTimeNorm(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteTimeNorm(WebTimeNormGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TimeNorms
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public ResultService<WebContingentPageViewModel> GetContingents(WebContingentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Contingents
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.AcademicPlanId.HasValue)
                    {
                        var ap = context.AcademicPlans.FirstOrDefault(x => x.Id == model.AcademicPlanId);
                        query = query.Where(x => x.AcademicYearId == ap.AcademicYearId);
                    }

                    query = query
                        .OrderBy(x => x.AcademicYearId)
                        .ThenBy(x => x.EducationDirection.Cipher)
                        .ThenBy(x => x.Course);

                    query = query
                        .Include(x => x.AcademicYear)
                        .Include(x => x.EducationDirection);

                    var result = new WebContingentPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebContingentViewModel).ToList()
                    };

                    return ResultService<WebContingentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebContingentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebContingentViewModel> GetContingent(WebContingentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents
                                .Include(x => x.AcademicYear)
                                .Include(x => x.EducationDirection)
                                .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<WebContingentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebContingentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebContingentViewModel>.Success(WebModelFactoryToViewModel.CreateWebContingentViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebContingentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateContingent(WebContingentSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateContingent(model);

                    var exsistEntity = context.Contingents
                        .FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId
                            && x.EducationDirectionId == entity.EducationDirectionId
                            && x.ContingentName == entity.ContingentName);

                    if (exsistEntity == null)
                    {
                        context.Contingents.Add(entity);
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

        public ResultService UpdateContingent(WebContingentSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateContingent(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteContingent(WebContingentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents
                        .FirstOrDefault(x => x.Id == model.Id);

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

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public ResultService<WebLecturerWorkloadPageViewModel> GetLecturerWorkloads(WebLecturerWorkloadGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.LecturerWorkload
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }

                    query = query
                        .OrderBy(x => x.AcademicYearId)
                        .ThenBy(x => x.Lecturer.LastName);

                    query = query
                        .Include(x => x.AcademicYear)
                        .Include(x => x.Lecturer);

                    var result = new WebLecturerWorkloadPageViewModel
                    {
                        List = query.Select(WebModelFactoryToViewModel.CreateWebLecturerWorkloadViewModel).ToList()
                    };

                    return ResultService<WebLecturerWorkloadPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebLecturerWorkloadPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebLecturerWorkloadViewModel> GetLecturerWorkload(WebLecturerWorkloadGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerWorkload
                                .Include(x => x.AcademicYear)
                                .Include(x => x.Lecturer)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<WebLecturerWorkloadViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebLecturerWorkloadViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<WebLecturerWorkloadViewModel>.Success(WebModelFactoryToViewModel.CreateWebLecturerWorkloadViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebLecturerWorkloadViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateLecturerWorkload(WebLecturerWorkloadSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateLecturerWorkload(model);

                    var exsistEntity = context.LecturerWorkload.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.LecturerId == entity.LecturerId);
                    if (exsistEntity == null)
                    {
                        context.LecturerWorkload.Add(entity);
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

        public ResultService UpdateLecturerWorkload(WebLecturerWorkloadSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerWorkload.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateLecturerWorkload(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteLecturerWorkload(WebLecturerWorkloadGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerWorkload.FirstOrDefault(x => x.Id == model.Id);
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

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------

        public (List<string> displayNames, List<string> propertiesNames) GetPropertiesNames(Type type)
        {
            List<string> displayNames = new List<string>();
            List<string> propertiesNames = new List<string>();

            type.GetProperties().ToList().ForEach(x =>
            {
                object[] dn = x.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (dn.Length > 0)
                {
                    displayNames.Add((dn.FirstOrDefault() as DisplayNameAttribute).DisplayName);
                    propertiesNames.Add(x.Name);
                }
            });

            (List<string> displayNames, List<string> propertiesNames) info = (displayNames, propertiesNames);

            return info;
        }

        public List<List<object>> GetPropertiesValues<T>(List<T> list, List<string> propertiesNames)
        {
            List<List<object>> result = new List<List<object>>();

            foreach (T element in list)
            {
                result.Add(new List<object> { element.GetType().GetProperty("Id").GetValue(element, null) });
                foreach (string propertyName in propertiesNames)
                {
                    object value = element.GetType().GetProperty(propertyName).GetValue(element, null);
                    if (value is bool)
                    {
                        if ((bool)value == true)
                        {
                            result.LastOrDefault().Add("Да");
                        }
                        else
                        {
                            result.LastOrDefault().Add("Нет");
                        }
                    }
                    else
                    {
                        result.LastOrDefault().Add(value);
                    }
                }
            }

            return result;
        }
    }
}
