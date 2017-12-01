using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class StudentHistoryService : IStudentHistoryService
	{
		private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Студенты;

        public StudentHistoryService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<StudentHistoryPageViewModel> GetStudentHistorys(StudentHistoryGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по истории студента");
                }

                int countPages = 0;
                var query = _context.StudentHistorys.AsQueryable();

                if (!string.IsNullOrEmpty(model.NumberOfBook))
                {
                    query = query.Where(e => e.StudentId == model.NumberOfBook);
                }
                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .OrderBy(c => c.DateCreate)
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(s => s.Student);

                var result = new StudentHistoryPageViewModel
                {
                    MaxCount = countPages,
                    List = ModelFactoryToViewModel.CreateStudentHistorys(query).ToList()
                };

                return ResultService<StudentHistoryPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentHistoryPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentHistoryPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<StudentHistoryViewModel> GetStudentHistory(StudentHistoryGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по истории студента");
                }

                var entity = _context.StudentHistorys
								.FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService<StudentHistoryViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

				return ResultService<StudentHistoryViewModel>.Success(ModelFactoryToViewModel.CreateStudentHistoryViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentHistoryViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentHistoryViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateStudentHistory(StudentHistoryRecordBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по истории студента");
                }

                var entity = ModelFacotryFromBindingModel.CreateStudentHistory(model);

				_context.StudentHistorys.Add(entity);
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

		public ResultService UpdateStudentHistory(StudentHistoryRecordBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по истории студента");
                }

                var entity = _context.StudentHistorys
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateStudentHistory(model, entity);
				
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

		public ResultService DeleteStudentHistory(StudentHistoryGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по истории студента");
                }

                var entity = _context.StudentHistorys
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				_context.StudentHistorys.Remove(entity);

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
