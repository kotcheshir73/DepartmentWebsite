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
    public class StreamingLessonService : IStreamingLessonService
    {
        private readonly DepartmentDbContext _context;

        public StreamingLessonService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<StreamingLessonViewModel> GetStreamingLessons()
        {
            return ModelFactory.CreateStreamingLessons(
                    _context.StreamingLessons
                        .Where(e => !e.IsDeleted))
                .ToList();
        }

        public StreamingLessonViewModel GetStreamingLesson(StreamingLessonGetBindingModel model)
        {
            var entity = _context.StreamingLessons
                            .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
            if (entity == null)
                return null;
            return ModelFactory.CreateStreamingLessonViewModel(entity);
        }

        public ResultService CreateStreamingLesson(StreamingLessonRecordBindingModel model)
        {
            var entity = new StreamingLesson
            {
                Id = model.Id,
                IncomingGroups = model.IncomingGroups,
                DateCreate = DateTime.Now,
                IsDeleted = false,
                StreamName = model.StreamName
            };
            try
            {
                _context.StreamingLessons.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateStreamingLesson(StreamingLessonRecordBindingModel model)
        {
            try
            {
                var entity = _context.StreamingLessons
                                .FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.IncomingGroups = model.IncomingGroups;
                entity.StreamName = model.StreamName;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteStreamingLesson(StreamingLessonGetBindingModel model)
        {
            try
            {
                var entity = _context.StreamingLessons
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
