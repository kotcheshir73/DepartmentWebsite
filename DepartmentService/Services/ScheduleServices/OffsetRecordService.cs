using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class OffsetRecordService : IOffsetRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        public OffsetRecordService(DepartmentDbContext context)
		{
			_context = context;
        }

        public ResultService<List<OffsetRecordShortViewModel>> GetOffsetSchedule(ScheduleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по расписанию");
                }

                var currentDates = model.SeasonDateId ?? ScheduleHelpService.GetCurrentDates(_context).Id;

                var selectedRecords = _context.OffsetRecords.Where(sr => sr.SeasonDatesId == currentDates);

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

                selectedRecords = selectedRecords
										.Include(sr => sr.Classroom)
										.Include(sr => sr.Discipline)
										.Include(sr => sr.Lecturer)
										.Include(sr => sr.StudentGroup);

				var records = selectedRecords.ToList();
				List<OffsetRecordShortViewModel> result = new List<OffsetRecordShortViewModel>();
				for (int i = 0; i < records.Count; ++i)
				{
					result.Add(ModelFactoryToViewModel.CreateOffsetRecordShortViewModel(records[i]));
				}

				return ResultService<List<OffsetRecordShortViewModel>>.Success(result.OrderBy(e => e.Id).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<OffsetRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<OffsetRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<OffsetRecordViewModel> GetOffsetRecord(ScheduleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по расписанию");
				}

				var entity = _context.OffsetRecords
								.Where(sr => sr.Id == model.Id)
								.Include(sr => sr.Classroom).Include(sr => sr.Discipline).Include(sr => sr.Lecturer).Include(sr => sr.StudentGroup)
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService<OffsetRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				return ResultService<OffsetRecordViewModel>.Success(ModelFactoryToViewModel.CreateOffsetRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<OffsetRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<OffsetRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateOffsetRecord(OffsetRecordRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по расписанию");
				}

				var seasonDate = ScheduleHelpService.GetCurrentDates(_context);

				var entry = _context.OffsetRecords.FirstOrDefault(sr => sr.Week == model.Week && sr.Day == model.Day && sr.Lesson == model.Lesson &&
																				sr.ClassroomId == model.ClassroomId &&
																				sr.SeasonDatesId == seasonDate.Id);

				if (entry != null)
				{
					return ResultService.Error("Error:", "Exsist OffsetRecord", ResultServiceStatusCode.ExsistItem);
				}

				var entity = ModelFacotryFromBindingModel.CreateOffsetRecord(model);

				_context.OffsetRecords.Add(entity);
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

		public ResultService UpdateOffsetRecord(OffsetRecordRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по расписанию");
				}

				var entity = _context.OffsetRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateOffsetRecord(model, entity);
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

		public ResultService DeleteOffsetRecord(ScheduleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по расписанию");
				}

				var entity = _context.OffsetRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				_context.OffsetRecords.Remove(entity);
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
