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
    public class ScheduleStopWordService : IScheduleStopWordService
    {
        private readonly DepartmentDbContext _context;

        public ScheduleStopWordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public List<ScheduleStopWordViewModel> GetScheduleStopWords()
        {
            return ModelFactory.CreateScheduleStopWords(
                    _context.ScheduleStopWords)
                .ToList();
        }

        public ScheduleStopWordViewModel GetScheduleStopWord(ScheduleStopWordGetBindingModel model)
        {
            var entity = _context.ScheduleStopWords
                            .FirstOrDefault(e => e.Id == model.Id);
            if (entity == null)
                return null;
            return ModelFactory.CreateScheduleStopWordViewModel(entity);
        }

        public ResultService CreateScheduleStopWord(ScheduleStopWordRecordBindingModel model)
        {
            var entity = new ScheduleStopWord
            {
                StopWord = model.StopWord,
                StopWordType = (ScheduleStopWordTypes)Enum.Parse(typeof(ScheduleStopWordTypes), model.StopWordType)
            };
            try
            {
                _context.ScheduleStopWords.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateScheduleStopWord(ScheduleStopWordRecordBindingModel model)
        {
            try
            {
                var entity = _context.ScheduleStopWords
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.StopWord = model.StopWord;
                entity.StopWordType = (ScheduleStopWordTypes)Enum.Parse(typeof(ScheduleStopWordTypes), model.StopWordType);

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteScheduleStopWord(ScheduleStopWordGetBindingModel model)
        {
            try
            {
                var entity = _context.ScheduleStopWords
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.ScheduleStopWords.Remove(entity);
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
