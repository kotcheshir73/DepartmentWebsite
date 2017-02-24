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
    public class ScheduleLessonTimeService : IScheduleLessonTimeService
    {
        private readonly DepartmentDbContext _context;

        public ScheduleLessonTimeService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<ScheduleLessonTimeViewModel> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                return ModelFactory.CreateScheduleLessonTimes(
                        _context.ScheduleLessonTimes)
                    .ToList();
            }
            else
            {
                return ModelFactory.CreateScheduleLessonTimes(
                        _context.ScheduleLessonTimes.Where(slt => slt.Title.Contains(model.Title)))
                    .ToList();
            }
        }

        public ScheduleLessonTimeViewModel GetScheduleLessonTime(ScheduleLessonTimeGetBindingModel model)
        {
            var entity = string.IsNullOrEmpty(model.Title) ? _context.ScheduleLessonTimes.FirstOrDefault(e => e.Id == model.Id) : 
                                                            _context.ScheduleLessonTimes.FirstOrDefault(e => e.Title == model.Title);
            if (entity == null)
                return null;
            return ModelFactory.CreateScheduleLessonTimeViewModel(entity);
        }

        public ResultService CreateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model)
        {
            var entity = new ScheduleLessonTime
            {
                Title = model.Title,
                DateBeginLesson = model.DateBeginLesson,
                DateEndLesson = model.DateEndLesson
            };
            try
            {
                _context.ScheduleLessonTimes.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model)
        {
            try
            {
                var entity = _context.ScheduleLessonTimes
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.Title = model.Title;
                entity.DateBeginLesson = model.DateBeginLesson;
                entity.DateEndLesson = model.DateEndLesson;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteScheduleLessonTime(ScheduleLessonTimeGetBindingModel model)
        {
            try
            {
                var entity = _context.ScheduleLessonTimes
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.ScheduleLessonTimes.Remove(entity);
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
