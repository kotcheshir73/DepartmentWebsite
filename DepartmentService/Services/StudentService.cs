using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using System.Text;
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
    public class StudentService : IStudentService
    {
        private readonly DepartmentDbContext _context;

        private readonly IStudentGroupService _serviceSG;

        public StudentService(DepartmentDbContext context, IStudentGroupService serviceSG)
        {
            _context = context;
            _serviceSG = serviceSG;
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

        public List<StudentGroupViewModel> GetStudentGroups()
        {
            return _serviceSG.GetStudentGroups();
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
                NumberOfBook = model.NumberOfBook,
                StudentGroupId = model.StudentGroupId,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Description = model.Description,
                Photo = model.Photo,
                DateCreate = DateTime.Now,
                IsDeleted = false
            };
            try
            {
                _context.Students.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sr.AppendFormat("ValidationErrors:\r\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ResultService.Error("error", sr.ToString(), 400);
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
            catch (DbEntityValidationException ex)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sr.AppendFormat("ValidationErrors:\r\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ResultService.Error("error", sr.ToString(), 400);
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
            catch (DbEntityValidationException ex)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sr.AppendFormat("ValidationErrors:\r\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ResultService.Error("error", sr.ToString(), 400);
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }
    }
}
