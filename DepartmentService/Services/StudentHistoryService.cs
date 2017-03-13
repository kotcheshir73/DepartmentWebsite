using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;

namespace DepartmentService.Services
{
    public class StudentHistoryService : IStudentHistoryService
    {
        private readonly DepartmentDbContext _context;

        public StudentHistoryService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<StudentHistoryViewModel> GetStudentHistorys(StudentHistoryGetBindingModel model)
        {
            if (!string.IsNullOrEmpty(model.NumberOfBook))
            {
                return ModelFactory.CreateStudentHistorys(
                                    _context.StudentHistorys
                                       .Where(sh => sh.StudentId == model.NumberOfBook))
                                .ToList();
            }
            return ModelFactory.CreateStudentHistorys(
                    _context.StudentHistorys)
                .ToList();
        }

        public StudentHistoryViewModel GetStudentHistory(StudentHistoryGetBindingModel model)
        {
            var entity = _context.StudentHistorys
                            .FirstOrDefault(e => e.Id == model.Id);
            if (entity == null)
                return null;
            return ModelFactory.CreateStudentHistoryViewModel(entity);
        }

        public ResultService CreateStudentHistory(StudentHistoryRecordBindingModel model)
        {
            var entity = new StudentHistory
            {
                StudentId = model.NumberOfBook,
                DateCreate = model.DateCreate,
                TextMessage = model.TextMessage
            };
            try
            {
                _context.StudentHistorys.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateStudentHistory(StudentHistoryRecordBindingModel model)
        {
            try
            {
                var entity = _context.StudentHistorys
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.DateCreate = model.DateCreate;
                entity.TextMessage = model.TextMessage;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteStudentHistory(StudentHistoryGetBindingModel model)
        {
            try
            {
                var entity = _context.StudentHistorys
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.StudentHistorys.Remove(entity);

                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }
    }
}
