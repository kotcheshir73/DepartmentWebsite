using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using DepartmentDAL.Enums;

namespace DepartmentService.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly DepartmentDbContext _context;

        public ClassroomService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<ClassroomViewModel> GetClassrooms()
        {
            return ModelFactory.CreateClassrooms(
                    _context.Classrooms
                        .Where(e => !e.IsDeleted))
                .ToList();
        }

        public ClassroomViewModel GetClassroom(ClassroomGetBindingModel model)
        {
            var entity = _context.Classrooms
                            .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
            if (entity == null)
                return null;
            return ModelFactory.CreateClassroomViewModel(entity);
        }

        public ResultService CreateClassroom(ClassroomRecordBindingModel model)
        {
            var entity = new Classroom
            {
                Id = model.Id,
                Capacity = model.Capacity,
                ClassroomType = (ClassroomTypes)Enum.Parse(typeof(ClassroomTypes), model.ClassroomType),
                IsDeleted = false
            };
            try
            {
                _context.Classrooms.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateClassroom(ClassroomRecordBindingModel model)
        {
            try
            {
                var entity = _context.Classrooms
                                .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.Capacity = model.Capacity;
                entity.ClassroomType = (ClassroomTypes)Enum.Parse(typeof(ClassroomTypes), model.ClassroomType);

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteClassroom(ClassroomGetBindingModel model)
        {
            try
            {
                var entity = _context.Classrooms
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
