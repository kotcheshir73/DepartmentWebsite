using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;

namespace DepartmentService.Services
{
    public class StudentGroupService : IStudentGroupService
    {
        private readonly DepartmentDbContext _context;

        private readonly IEducationDirectionService _serviceED;

        public StudentGroupService(DepartmentDbContext context, IEducationDirectionService serviceED)
        {
            _context = context;
            _serviceED = serviceED;
        }

        public List<StudentGroupViewModel> GetStudentGroups()
        {
            return ModelFactory.CreateStudentGroups(
                    _context.StudentGroups.Include(s => s.EducationDirection)
                        .Where(e => !e.IsDeleted))
                .ToList();
        }

        public List<EducationDirectionViewModel> GetEducationDirections()
        {
            return _serviceED.GetEducationDirections();
        }

        public StudentGroupViewModel GetStudentGroup(StudentGroupGetBindingModel model)
        {
            var entity = _context.StudentGroups.Include(s => s.EducationDirection)
                            .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
            if (entity == null)
                return null;
            return ModelFactory.CreateStudentGroupViewModel(entity);
        }

        public ResultService CreateStudentGroup(StudentGroupRecordBindingModel model)
        {
            var entity = new StudentGroup
            {
                EducationDirectionId = model.EducationDirectionId,
                DateCreate = DateTime.Now,
                GroupName = model.GroupName,
                IsDeleted = false,
                Kurs = model.Kurs
            };
            try
            {
                _context.StudentGroups.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateStudentGroup(StudentGroupRecordBindingModel model)
        {
            try
            {
                var entity = _context.StudentGroups
                                .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.GroupName = model.GroupName;
                entity.Kurs = model.Kurs;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteStudentGroup(StudentGroupGetBindingModel model)
        {
            try
            {
                var entity = _context.StudentGroups
                                .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
