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
    public class EducationDirectionService : IEducationDirectionService
    {
        private readonly DepartmentDbContext _context;

        public EducationDirectionService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<EducationDirectionViewModel> GetEducationDirections()
        {
            return ModelFactory.CreateEducationDirections(
                    _context.EducationDirections
                        .Where(e => !e.IsDeleted))
                .ToList();
        }

        public EducationDirectionViewModel GetEducationDirection(EducationDirectionGetBindingModel model)
        {
            var entity = _context.EducationDirections
                            .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
            if (entity == null)
                return null;
            return ModelFactory.CreateEducationDirectionViewModel(entity);
        }

        public ResultService CreateEducationDirection(EducationDirectionRecordBindingModel model)
        {
            var entity = new EducationDirection
            {
                Cipher = model.Cipher,
                DateCreate = DateTime.Now,
                Description = model.Description,
                IsDeleted = false,
                Title = model.Title
            };
            try
            {
                _context.EducationDirections.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch(Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateEducationDirection(EducationDirectionRecordBindingModel model)
        {
            try
            {
                var entity = _context.EducationDirections
                                .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if(entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.Cipher = model.Cipher;
                entity.Description = model.Description;
                entity.Title = model.Title;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteEducationDirection(EducationDirectionGetBindingModel model)
        {
            try
            {
                var entity = _context.EducationDirections
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
