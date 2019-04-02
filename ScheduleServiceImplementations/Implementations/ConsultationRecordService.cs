﻿using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using ScheduleServiceImplementations.Helpers;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.Interfaces;
using ScheduleServiceInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace ScheduleServiceImplementations.Services
{
    public class ConsultationRecordService : IConsultationRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

		public ConsultationRecordService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<List<ConsultationRecordShortViewModel>> GetConsultationSchedule(ScheduleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по расписанию");
                }

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates().Id;

                var selectedRecords = _context.ConsultationRecords.Where(sr => sr.SeasonDatesId == currentDates);

				if (model.DateBegin.HasValue)
				{
					if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                        }
                        selectedRecords = selectedRecords
							.Where(sr => sr.LessonClassroom == model.ClassroomNumber &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
					}
					if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию групп");
                        }
                        selectedRecords = selectedRecords
							.Where(sr => sr.LessonGroup == model.StudentGroupName &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
					}
					if (model.LecturerId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию преподавателей");
                        }
                        selectedRecords = selectedRecords
							.Where(sr => sr.LecturerId == model.LecturerId.Value &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию дисциплины");
                        }
                        selectedRecords = selectedRecords
                            .Where(sr => sr.DisciplineId == model.DisciplineId.Value &&
                                        sr.DateConsultation >= model.DateBegin.Value &&
                                        sr.DateConsultation <= model.DateEnd.Value);
                    }
                }
				else
				{
					if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.LessonClassroom == model.ClassroomNumber);
					}
					if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию групп");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.LessonGroup == model.StudentGroupName);
					}
					if (model.LecturerId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию преподавателей");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.LecturerId == model.LecturerId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию дисциплины");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.DisciplineId == model.DisciplineId.Value);
                    }
                }

                selectedRecords = selectedRecords
                                        .Include(sr => sr.Classroom)
                                        .Include(sr => sr.Discipline)
                                        .Include(sr => sr.Lecturer)
                                        .Include(sr => sr.StudentGroup);

                var records = selectedRecords.ToList();
                
				ConsultationRecordRecordBindingModel record;
				List<ConsultationRecordShortViewModel> result = new List<ConsultationRecordShortViewModel>();
				for (int i = 0; i < records.Count; ++i)
				{
					record = new ConsultationRecordRecordBindingModel
					{
						ClassroomId = model.ClassroomId,
						LecturerId = model.LecturerId,
						// TODO посомтреть по группе
						DateConsultation = records[i].DateConsultation
					};
                    var seasonDate = model.SeasonDateId.HasValue ? 
                                            _context.SeasonDates.FirstOrDefault(sd => sd.Id == model.SeasonDateId.Value) :
                                            ScheduleHelper.GetCurrentDates();
                    ScheduleHelper.CheckCreateConsultation(_context, record, seasonDate);

                    result.Add(ScheduleModelFactoryToViewModel.CreateConsultationRecordShortViewModel(records[i], record));
                }

				return ResultService<List<ConsultationRecordShortViewModel>>.Success(result.OrderBy(e => e.Id).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ConsultationRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ConsultationRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ConsultationRecordViewModel> GetConsultationRecord(ScheduleGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по расписанию");
                }

                var entity = _context.ConsultationRecords
                                .Where(sr => sr.Id == model.Id)
                                .Include(sr => sr.Classroom).Include(sr => sr.Discipline).Include(sr => sr.Lecturer).Include(sr => sr.StudentGroup)
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService<ConsultationRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
				return ResultService<ConsultationRecordViewModel>.Success(ScheduleModelFactoryToViewModel.CreateConsultationRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ConsultationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ConsultationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateConsultationRecord(ConsultationRecordRecordBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по расписанию");
                }

                var seasonDate = ScheduleHelper.GetCurrentDates();

                ScheduleHelper.CheckCreateConsultation(_context, model, seasonDate);

                var entity = ScheduleModelFacotryFromBindingModel.CreateConsultationRecord(model, seasonDate: seasonDate);

                _context.ConsultationRecords.Add(entity);
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

		public ResultService UpdateConsultationRecord(ConsultationRecordRecordBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по расписанию");
                }

                var entity = _context.ConsultationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ScheduleModelFacotryFromBindingModel.CreateConsultationRecord(model, entity);
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

		public ResultService DeleteConsultationRecord(ScheduleGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по расписанию");
                }

                var entity = _context.ConsultationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				_context.ConsultationRecords.Remove(entity);
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

        public ResultService ClearConsultationRecords(ScheduleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по расписанию");
                }

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates().Id;

                var selectedRecords = _context.ConsultationRecords.Where(sr => sr.SeasonDatesId == currentDates);

                if (model.DateBegin.HasValue)
                {
                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                        }
                        selectedRecords = selectedRecords
                            .Where(sr => sr.LessonClassroom == model.ClassroomNumber &&
                                        sr.DateConsultation >= model.DateBegin.Value &&
                                        sr.DateConsultation <= model.DateEnd.Value);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию групп");
                        }
                        selectedRecords = selectedRecords
                            .Where(sr => sr.LessonGroup == model.StudentGroupName &&
                                        sr.DateConsultation >= model.DateBegin.Value &&
                                        sr.DateConsultation <= model.DateEnd.Value);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию преподавателей");
                        }
                        selectedRecords = selectedRecords
                            .Where(sr => sr.LecturerId == model.LecturerId.Value &&
                                        sr.DateConsultation >= model.DateBegin.Value &&
                                        sr.DateConsultation <= model.DateEnd.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию дисциплины");
                        }
                        selectedRecords = selectedRecords
                            .Where(sr => sr.DisciplineId == model.DisciplineId.Value &&
                                        sr.DateConsultation >= model.DateBegin.Value &&
                                        sr.DateConsultation <= model.DateEnd.Value);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.ClassroomId == model.ClassroomId);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию групп");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.LessonClassroom == model.ClassroomNumber);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию преподавателей");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.LecturerId == model.LecturerId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию дисциплины");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.DisciplineId == model.DisciplineId.Value);
                    }
                }

                _context.ConsultationRecords.RemoveRange(selectedRecords);
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