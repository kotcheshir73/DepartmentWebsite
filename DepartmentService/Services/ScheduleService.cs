﻿using DepartmentDAL.Context;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentService.ViewModels;
using DepartmentService.BindingModels;
using DepartmentDAL;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using DepartmentDAL.Models;
using DepartmentDAL.Enums;

namespace DepartmentService.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly DepartmentDbContext _context;

        private readonly IClassroomService _serviceC;

        private readonly ISeasonDatesService _serviceSD;

        public ScheduleService(DepartmentDbContext context, IClassroomService serviceC, ISeasonDatesService serviceSD)
        {
            _context = context;
            _serviceC = serviceC;
            _serviceSD = serviceSD;
        }

        public List<ClassroomViewModel> GetClassrooms()
        {
            return _serviceC.GetClassrooms();
        }

        public SeasonDatesViewModel GetCurrentDates()
        {
            try
            {
                var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                if (currentSetting == null)
                {
                    return null;
                }
                return _serviceSD.GetSeasonDates(new BindingModels.SeasonDatesGetBindingModel { Title = currentSetting.Value });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<SemesterRecordViewModel> GetSemesterRecords(ClassroomGetBindingModel model)
        {
            var records = _context.SemesterRecords.Where(sr => sr.ClassroomId == model.Id);
            return ModelFactory.CreateSemesterRecords(records).ToList();
        }

        public ResultService ClearSemesterRecords(ClassroomGetBindingModel model)
        {
            try
            {
                var records = _context.SemesterRecords.Where(sr => sr.ClassroomId == model.Id);
                _context.SemesterRecords.RemoveRange(records);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch(Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService LoadScheduleHTMLForClassrooms(LoadHTMLForClassroomsBindingModel model)
        {
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.Default;

            string strHTML = web.DownloadString(model.ScheduleUrl + "raspisan.htm");

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(strHTML);

            var nodes = document.DocumentNode.SelectNodes("//table/tr/td");
            foreach (var node in nodes)
            {
                if(node.InnerText != "\r\n")
                {
                    var elem = node.ChildNodes.FirstOrDefault(e => e.Name.ToLower() == "a");
                    if (elem != null)
                    {
                        ParsingPage(model.ScheduleUrl + elem.Attributes.First().Value, model.Classrooms, node.InnerText.Remove(0, 2));
                    }
                }
            }
            return ResultService.Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulrUrl"></param>
        /// <param name="classrooms"></param>
        private void ParsingPage(string schedulrUrl, List<string> classrooms, string groupName)
        {
            string[] days = new string[] { "Пнд", "Втр", "Срд", "Чтв", "Птн", "Сбт" };
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.Default;
            string pageHTML = web.DownloadString(schedulrUrl);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageHTML);
            var pageNodes = document.DocumentNode.SelectNodes("//table/tr/td");
            int week = -1;
            int day = -1;
            foreach (var pageNode in pageNodes)
            {
                string text = pageNode.InnerText.Remove(0, 2);
                if (days.Contains(text))
                {
                    if (days[0].Contains(text))
                    {
                        week++;
                    }
                    day++;
                }
                if (week > -1)
                {
                    var elem = pageNode.ChildNodes.First().NextSibling;
                    if (elem.Name.ToLower() == "font")
                    {
                        int n = pageNode.InnerText.IndexOf("\r\n");
                        var lesson = pageNode.InnerText.Remove(n, 2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (lesson[0] == "_")
                        {
                            continue;
                        }
                        var entity = new SemesterRecord();

                        int i = 1;
                        entity.LessonGroupName = groupName;
                        entity.LessonDiscipline += lesson[0];
                        for (; i < lesson.Length; ++i)
                        {
                            if (lesson[i].ToUpper() != lesson[i] || lesson[i].Length < 2)
                            {
                                entity.LessonDiscipline += lesson[i] + " ";
                            }
                            else
                                break;
                        }

                        if (i < lesson.Length - 3)
                        {
                            entity.LessonTeacher = lesson[i++] + " " + lesson[i++] + "." + lesson[i++] + ".";
                        }

                        if (i < lesson.Length)
                        {
                            if (lesson[i] == "-")
                            {
                                i++;
                            }
                            var classroom = lesson[i];
                            if(classrooms.Any(c => classroom.Contains(c)))
                            {
                                entity.ClassroomId = classrooms.FirstOrDefault(c => classroom.Contains(c));
                            }
                        }
                        if(entity.ClassroomId == null)
                        {
                            continue;
                        }
                        entity.LessonType = LessonTypes.Занятие;
                        if (entity.LessonDiscipline.StartsWith("лек."))
                        {
                            entity.LessonType = LessonTypes.Лекция;
                            entity.LessonDiscipline = entity.LessonDiscipline.Remove(0, 4);
                        }
                        if (entity.LessonDiscipline.StartsWith("пр."))
                        {
                            entity.LessonType = LessonTypes.Практика;
                            entity.LessonDiscipline = entity.LessonDiscipline.Remove(0, 3);
                        }
                        if (entity.LessonDiscipline.StartsWith("лаб."))
                        {
                            entity.LessonType = LessonTypes.Лабораторная;
                            entity.LessonDiscipline = entity.LessonDiscipline.Remove(0, 4);
                        }

                        _context.SemesterRecords.Add(entity);
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
