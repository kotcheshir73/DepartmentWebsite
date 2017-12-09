using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Helpers;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
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

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates(_context).Id;

                var selectedRecords = _context.ConsultationRecords.Where(sr => sr.SeasonDatesId == currentDates);

				if (model.DateBegin.HasValue)
				{
					if (!string.IsNullOrEmpty(model.ClassroomId))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                        }
                        selectedRecords = selectedRecords
							.Where(sr => sr.ClassroomId == model.ClassroomId &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
					}
					if (!string.IsNullOrEmpty(model.GroupName))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию групп");
                        }
                        selectedRecords = selectedRecords
							.Where(sr => sr.LessonGroup == model.GroupName &&
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
					if (!string.IsNullOrEmpty(model.ClassroomId))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.ClassroomId == model.ClassroomId);
					}
					if (!string.IsNullOrEmpty(model.GroupName))
                    {
                        if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                        {
                            throw new Exception("Нет доступа на чтение данных по расписанию групп");
                        }
                        selectedRecords = selectedRecords.Where(sr => sr.LessonGroup == model.GroupName);
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
                                            ScheduleHelper.GetCurrentDates(_context);
                    ScheduleHelper.CheckCreateConsultation(_context, record, seasonDate);

                    result.Add(ModelFactoryToViewModel.CreateConsultationRecordShortViewModel(records[i], record));
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
				return ResultService<ConsultationRecordViewModel>.Success(ModelFactoryToViewModel.CreateConsultationRecordViewModel(entity));
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

                var seasonDate = ScheduleHelper.GetCurrentDates(_context);

                ScheduleHelper.CheckCreateConsultation(_context, model, seasonDate);

                var entity = ModelFacotryFromBindingModel.CreateConsultationRecord(model);

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
				entity = ModelFacotryFromBindingModel.CreateConsultationRecord(model, entity);
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
	}
}
