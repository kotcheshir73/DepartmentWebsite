using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class ExaminationRecordService : IExaminationRecordService
	{
		private readonly DepartmentDbContext _context;

		public ExaminationRecordService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<ExaminationRecordViewModel> GetExaminationRecord(ExaminationRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.ExaminationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<ExaminationRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<ExaminationRecordViewModel>.Success(
					ModelFactoryToViewModel.CreateExaminationRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ExaminationRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ExaminationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model)
		{
			var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
			if (currentSetting == null)
			{
				return ResultService.Error("Error:", "CurrentSetting not found",
					ResultServiceStatusCode.NotFound);
			}
			var seasonDate = _context.SeasonDates.FirstOrDefault(sd => sd.Title == currentSetting.Value);
			if (seasonDate == null)
			{
				return ResultService.Error("Error:", "SeasonDate not found",
					ResultServiceStatusCode.NotFound);
			}

			var entry = _context.ExaminationRecords.FirstOrDefault(sr => sr.DateConsultation == model.DateConsultation && sr.DateExamination == model.DateExamination &&
																			sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == seasonDate.Id);

			if (entry != null)
			{
				return ResultService.Error("Error:", "Exsist ExaminationRecord",
					ResultServiceStatusCode.ExsistItem);
			}
			var entity = ModelFacotryFromBindingModel.CreateExaminationRecord(model);
			try
			{
				_context.ExaminationRecords.Add(entity);
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

		public ResultService UpdateExaminationRecord(ExaminationRecordRecordBindingModel model)
		{
			try
			{
				var entity = _context.ExaminationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateExaminationRecord(model, entity);
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

		public ResultService DeleteExaminationRecord(ExaminationRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.ExaminationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.ExaminationRecords.Remove(entity);
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
