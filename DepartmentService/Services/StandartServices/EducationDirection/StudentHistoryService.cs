using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
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
                var query = _context.StudentHistorys.Where(sh => !sh.IsDeleted).AsQueryable();

                if (model.StudetnId.HasValue)
                {
                    query = query.Where(sh => sh.StudentId == model.StudetnId);
                }

                query = query.OrderBy(sh => sh.DateCreate);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
                {
                    countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                    query = query
                                .Skip(model.PageSize.Value * model.PageNumber.Value)
                                .Take(model.PageSize.Value);
                }

                query = query.Include(sh => sh.Student);

                var result = new StudentHistoryPageViewModel
                {
                    MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateStudentHistoryViewModel).ToList()
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
								.FirstOrDefault(sh => sh.Id == model.Id && !sh.IsDeleted);
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

                var entity = _context.StudentHistorys.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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

                var entity = _context.StudentHistorys.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
