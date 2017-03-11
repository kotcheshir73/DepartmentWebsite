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
    public class StudentService : IStudentService
    {
        private readonly DepartmentDbContext _context;

        public StudentService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<StudentViewModel> GetStudents(StudentGetBindingModel model)
        {
            if (model.StudentGroupId.HasValue)
            {
                return ModelFactory.CreateStudents(
                    _context.Students
                        .Where(e => e.StudentGroupId == model.StudentGroupId.Value && !e.IsDeleted))
                .ToList();
            }
            return ModelFactory.CreateStudents(
                    _context.Students
                        .Where(e => !e.IsDeleted))
                .ToList();
        }

        public StudentViewModel GetStudent(StudentGetBindingModel model)
        {
            var entity = _context.Students
                            .FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
            if (entity == null)
                return null;
            return ModelFactory.CreateStudentViewModel(entity);
        }

        public ResultService CreateStudent(StudentRecordBindingModel model)
        {
            var entity = new Student
            {
                StudentGroupId = model.StudentGroupId,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Description = model.Description,
                Photo = model.Photo,
                DateCreate = DateTime.Now,
                IsDeleted = false,
            };
            try
            {
                _context.Students.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateStudent(StudentRecordBindingModel model)
        {
            try
            {
                var entity = _context.Students
                                .FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.LastName = model.LastName;
                entity.FirstName = model.FirstName;
                entity.Patronymic = model.Patronymic;
                entity.Description = model.Description;
                entity.Photo = model.Photo;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteStudent(StudentGetBindingModel model)
        {
            try
            {
                var entity = _context.Students
                                .FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.IsDeleted = true;
                entity.DateDelete = DateTime.Now;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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
