using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{

    public class GraficService : IGraficService
    {
        private readonly DepartmentDbContext _context;

        private readonly IGraficRecordService _serviceSR;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        public GraficService(DepartmentDbContext context, IGraficRecordService serviceSR)
        {
            _context = context;
            _serviceSR = serviceSR;
        }

        public ResultService<GraficPageViewModel> GetGrafics(GraficGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }



                int countPages = 0;
                var query = _context.AcademicPlanRecordMissions.Where(record => !record.IsDeleted).AsQueryable();
                if (model.LecturerId.HasValue)
                {
                    query = query.Where(x => x.LecturerId == model.LecturerId);
                }

                if (model.AcademicYearId.HasValue)
                {
                    query = query.Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId);
                }

                query = query.Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan).Include(apre => apre.AcademicPlanRecordElement.AcademicPlanRecord.Grafics);




                var grahp = query.SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Grafics).Distinct();

                /*
                var timeNorms = _context.TimeNorms.Where(record => !record.IsDeleted && record.AcademicYearId == model.Id
                    && (record.KindOfLoadName == "Лекционное занятие" || record.KindOfLoadName == "Практическое занятие" || record.KindOfLoadName == "Лабораторная работа")).ToList();

                var APRM = _context.AcademicPlanRecordMissions.Where(record => !record.IsDeleted && record.LecturerId == model.LecturerId)
                        .Include(record => record.AcademicPlanRecordElement)
                        .Where(record => (record.AcademicPlanRecordElement.TimeNormId == timeNorms.ElementAt(0).Id) || (record.AcademicPlanRecordElement.TimeNormId == timeNorms.ElementAt(1).Id) || (record.AcademicPlanRecordElement.TimeNormId == timeNorms.ElementAt(2).Id));
                //

                var graf = new List<GraficViewModel>();

                var apr = _context.AcademicPlanRecords;

                foreach (var aprm in APRM)
                {
                    var tmp = query.Where(record => record.AcademicPlanRecordId == aprm.AcademicPlanRecordElement.AcademicPlanRecordId);
                    if (tmp.Count() != 0)
                    {
                        graf.AddRange(tmp.ToList());
                    }
                }
                */

                //query = query.OrderBy(record => record.StudentGroup.GroupName).ThenBy(record => record.AcademicPlanRecord.Discipline.DisciplineName);

                grahp = grahp.Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.StudentGroup);

                var result = new GraficPageViewModel
                {
                    List = grahp.Select(ModelFactoryToViewModel.CreateGraficViewModel).ToList()
                };
                
                return ResultService<GraficPageViewModel>.Success(result);
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<GraficPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<GraficPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<GraficViewModel> GetGrafic(GraficGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по элементам записей учебного плана");
                }

                var entity = _context.Grafics
                                .FirstOrDefault(record => record.Id == model.Id && !record.IsDeleted);
                if (entity == null)
                {
                    return ResultService<GraficViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<GraficViewModel>.Success(ModelFactoryToViewModel.CreateGraficViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<GraficViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<GraficViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAllFindGrafic(AcademicYearGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных");
                }
                var Grafic = _context.Grafics.Where(record => !record.IsDeleted && record.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id);
                /*var timeNorm = _context.TimeNorms.FirstOrDefault(record => !record.IsDeleted && record.AcademicYearId == model.Id
                    && record.KindOfLoadName == "Лекционное занятие");*/ //Получение трех норм времени для поиска ведомостей
                var disciplineBlock = _context.DisciplineBlocks.FirstOrDefault(record => record.Title.Contains("Дисциплины"));
                    //Поиск найзначеных часов преподавателям
                    var APR = _context.AcademicPlanRecords.Where(record => !record.IsDeleted)
                        .Include(record => record.Discipline).Include(record =>  record.Contingent).Where(record => record.Discipline.DisciplineBlockId == disciplineBlock.Id);
                    //string nameTN = tn.KindOfLoadName == "Зачет с оценкой" ? "Диференцированный_зачет" : tn.KindOfLoadName;
                    foreach (var APRRecord in APR)
                    {

                        var studentGroup = _context.StudentGroups.Where(record => !record.IsDeleted && record.EducationDirectionId == APRRecord.Contingent.EducationDirectionId
                            && record.Course == APRRecord.Contingent.Course);
                        foreach (var SGRecord in studentGroup)
                        {
                            if (Grafic.FirstOrDefault(record => !record.IsDeleted
                                 && record.AcademicPlanRecordId == APRRecord.Id
                                 && record.StudentGroupId == SGRecord.Id) == null)
                            {
                                var entity = ModelFacotryFromBindingModel.CreateGrafic(new GraficSetBindingModel()
                                {
                                    AcademicPlanRecordId = APRRecord.Id,
                                    StudentGroupId = SGRecord.Id,
                                    Comment = "",
                                    CommentWishesOfTeacher = ""
                                });
                                _context.Grafics.Add(entity);
                            }
                        }
                    }
                
                _context.SaveChanges();
                _serviceSR.CreateAllFindGraficRecord(model);
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

        public ResultService CreateGrafic(GraficSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = ModelFacotryFromBindingModel.CreateGrafic(model);

                _context.Grafics.Add(entity);
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

        public ResultService UpdateGrafic(GraficSetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по элементам записей учебного плана");
                }

                var entity = _context.Grafics.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                //_context.Grafics.Attach(entity);   //не уверен что так правильно

                entity = ModelFacotryFromBindingModel.CreateGrafic(model, entity);

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

        public ResultService DeleteGrafic(GraficGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных о ведомостях");
                }

                var entity = _context.Grafics.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
