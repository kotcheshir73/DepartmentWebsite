using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DepartmentService.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly DepartmentDbContext _context;

		private readonly IClassroomService _serviceC;

		private readonly IStudentGroupService _serviceG;

		private readonly ILecturerService _serviceL;

		private readonly ISeasonDatesService _serviceSD;

		private readonly IScheduleLessonTimeService _serviceSLT;

		private readonly IStreamingLessonService _serviceSL;

		private readonly ISemesterRecordService _serviceSR;

		private readonly IOffsetRecordService _serviceOR;

		private readonly IExaminationRecordService _serviceER;

		private readonly IConsultationRecordService _serviceCR;

		public ScheduleService(DepartmentDbContext context, IClassroomService serviceC, IStudentGroupService serviceG, ILecturerService serviceL, ISeasonDatesService serviceSD,
			IScheduleLessonTimeService serviceSLT, IStreamingLessonService serviceSL,
			ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER,
			IConsultationRecordService serviceCR)
		{
			_context = context;
			_serviceC = serviceC;
			_serviceG = serviceG;
			_serviceL = serviceL;
			_serviceSD = serviceSD;
			_serviceSLT = serviceSLT;
			_serviceSL = serviceSL;
			_serviceSR = serviceSR;
			_serviceOR = serviceOR;
			_serviceER = serviceER;
			_serviceCR = serviceCR;
		}

		public ResultService<List<ClassroomViewModel>> GetClassrooms()
		{
			return _serviceC.GetClassrooms();
		}

		public ResultService<List<StudentGroupViewModel>> GetStudentGroups()
		{
			return _serviceG.GetStudentGroups();
		}

		public ResultService<List<LecturerViewModel>> GetLecturers()
		{
			return _serviceL.GetLecturers();
		}

		public ResultService<List<SeasonDatesViewModel>> GetSeasonDaties()
		{
			return _serviceSD.GetSeasonDaties();
		}

		public ResultService<List<ScheduleLessonTimeViewModel>> GetScheduleLessonTimes(
			ScheduleLessonTimeGetBindingModel model)
		{
			return _serviceSLT.GetScheduleLessonTimes(model);
		}

		public ResultService<SeasonDatesViewModel> GetCurrentDates()
		{
			try
			{
				var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
				if (currentSetting == null)
				{
					return ResultService<SeasonDatesViewModel>.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				return _serviceSD.GetSeasonDates(new SeasonDatesGetBindingModel { Title = currentSetting.Value });
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<SeasonDatesViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<SeasonDatesViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService UpdateCurrentDates(SeasonDatesGetBindingModel model)
		{
			try
			{
				var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
				if (currentSetting == null)
				{
					return null;
				}

				currentSetting.Value = model.Title;
				_context.Entry(currentSetting).State = EntityState.Modified;
				_context.SaveChanges();

				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		#region GetSchedule
		public ResultService<List<SemesterRecordShortViewModel>> GetScheduleSemester(ScheduleBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService<List<SemesterRecordShortViewModel>>.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var selectedRecords = _context.SemesterRecords.
					Include(sr => sr.Lecturer).Include(sr => sr.Classroom).Include(sr => sr.StudentGroup).AsQueryable();
				if (!string.IsNullOrEmpty(model.ClassroomId))
				{
					selectedRecords = selectedRecords.
						Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == currentDates.Id);
				}
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					selectedRecords = selectedRecords.
						Where(sr => sr.LessonGroup == model.GroupName && sr.SeasonDatesId == currentDates.Id);
				}
				if (model.LecturerId.HasValue)
				{
					selectedRecords = selectedRecords.
						Where(sr => sr.LecturerId == model.LecturerId.Value && sr.SeasonDatesId == currentDates.Id);
				}
				var records = selectedRecords.ToList();
				List<SemesterRecordShortViewModel> result = new List<SemesterRecordShortViewModel>();
				for (int i = 0; i < records.Count; ++i)
				{
					string groups = GetLessonGroup(records[i]);
					if (records[i].IsStreaming && (!string.IsNullOrEmpty(model.ClassroomId) || model.LecturerId.HasValue))
					{//если потоковая пара
						var recs = records.Where(rec => rec.Week == records[i].Week && rec.Day == records[i].Day && rec.Lesson == records[i].Lesson &&
												rec.LessonClassroom == records[i].LessonClassroom && rec.IsStreaming).ToList();
						StringBuilder sb = new StringBuilder();
						foreach (var rec in recs)
						{
							sb.Append(rec.LessonGroup + ";");
							if (records[i] != rec)
							{
								records.Remove(rec);
							}
						}
						groups = sb.Remove(sb.Length - 1, 1).ToString();
						//пытаемся найти запись о потоковом занятии
						var streamingLesson = _context.StreamingLessons.FirstOrDefault(sl => sl.IncomingGroups == groups);
						if (streamingLesson != null)
						{
							groups = streamingLesson.StreamName;
						}
						else
						{
							_serviceSL.CreateStreamingLesson(new StreamingLessonRecordBindingModel
							{
								IncomingGroups = groups,
								StreamName = ""
							});
						}
					}
					if (records[i].LessonType == LessonTypes.удл)
					{//не выводим занятие, если оно удаленное и в эту пару поставили пару
						var recordExists = records.Exists(r => r.Week == records[i].Week && r.Day == records[i].Day && r.Lesson == records[i].Lesson &&
														r.LessonType != LessonTypes.удл);
						if (recordExists)
						{
							records.Remove(records[i--]);
							continue;
						}
					}
					result.Add(new SemesterRecordShortViewModel
					{
						Id = records[i].Id,
						Week = records[i].Week,
						Day = records[i].Day,
						Lesson = records[i].Lesson,
						LessonType = records[i].LessonType.ToString(),
						IsStreaming = records[i].IsStreaming,
						LessonLecturer = GetLessonLecturer(records[i]),
						LessonDiscipline = GetLessonDiscipline(records[i]),
						LessonGroup = groups,
						LessonClassroom = GetLessonClassroom(records[i])
					});
				}

				return ResultService<List<SemesterRecordShortViewModel>>.Success(
					result.OrderBy(e => e.Id).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<SemesterRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<SemesterRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<OffsetRecordShortViewModel>> GetScheduleOffset(ScheduleBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService<List<OffsetRecordShortViewModel>>.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var selectedRecords = _context.OffsetRecords
					.Include(sr => sr.Lecturer).Include(sr => sr.Classroom).Include(sr => sr.StudentGroup).AsQueryable();
				if (!string.IsNullOrEmpty(model.ClassroomId))
				{
					selectedRecords = selectedRecords
						.Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == currentDates.Id);
				}
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					selectedRecords = selectedRecords
						.Where(sr => sr.LessonGroup == model.GroupName && sr.SeasonDatesId == currentDates.Id);
				}
				var records = selectedRecords.ToList();
				List<OffsetRecordShortViewModel> result = new List<OffsetRecordShortViewModel>();
				for (int i = 0; i < records.Count; ++i)
				{
					result.Add(new OffsetRecordShortViewModel
					{
						Id = records[i].Id,
						Week = records[i].Week,
						Day = records[i].Day,
						Lesson = records[i].Lesson,
						LessonLecturer = GetLessonLecturer(records[i]),
						LessonDiscipline = GetLessonDiscipline(records[i]),
						LessonGroup = GetLessonGroup(records[i]),
						LessonClassroom = GetLessonClassroom(records[i])
					});
				}

				return ResultService<List<OffsetRecordShortViewModel>>.Success(
					result.OrderBy(e => e.Id).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<OffsetRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<OffsetRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<ExaminationRecordShortViewModel>> GetScheduleExamination(ScheduleBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService<List<ExaminationRecordShortViewModel>>.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var selectedRecords = _context.ExaminationRecords
					.Include(sr => sr.Lecturer).Include(sr => sr.Classroom).Include(sr => sr.StudentGroup).AsQueryable();
				if (!string.IsNullOrEmpty(model.ClassroomId))
				{
					selectedRecords = selectedRecords
						.Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == currentDates.Id);
				}
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					selectedRecords = selectedRecords
						.Where(sr => sr.LessonGroup == model.GroupName && sr.SeasonDatesId == currentDates.Id);
				}
				var records = selectedRecords.ToList();
				List<ExaminationRecordShortViewModel> result = new List<ExaminationRecordShortViewModel>();
				for (int i = 0; i < records.Count; ++i)
				{
					result.Add(new ExaminationRecordShortViewModel
					{
						Id = records[i].Id,
						DateConsultation = records[i].DateConsultation,
						DateExamination = records[i].DateExamination,
						LessonLecturer = GetLessonLecturer(records[i]),
						LessonDiscipline = GetLessonDiscipline(records[i]),
						LessonGroup = GetLessonGroup(records[i]),
						LessonClassroom = GetLessonClassroom(records[i])
					});
				}

				return ResultService<List<ExaminationRecordShortViewModel>>.Success(
					result.OrderBy(e => e.Id).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ExaminationRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ExaminationRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<ConsultationRecordShortViewModel>> GetScheduleConsultation(ScheduleBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService<List<ConsultationRecordShortViewModel>>.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var selectedRecords = _context.ConsultationRecords
					.Include(sr => sr.Lecturer).Include(sr => sr.Classroom).Include(sr => sr.StudentGroup).AsQueryable();

				if (model.DateBegin.HasValue)
				{
					if (!string.IsNullOrEmpty(model.ClassroomId))
					{
						selectedRecords = selectedRecords
							.Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == currentDates.Id &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
					}
					if (!string.IsNullOrEmpty(model.GroupName))
					{
						selectedRecords = selectedRecords
							.Where(sr => sr.LessonGroup == model.GroupName && sr.SeasonDatesId == currentDates.Id &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
					}
					if (model.LecturerId.HasValue)
					{
						selectedRecords = selectedRecords
							.Where(sr => sr.LecturerId == model.LecturerId.Value && sr.SeasonDatesId == currentDates.Id &&
										sr.DateConsultation >= model.DateBegin.Value &&
										sr.DateConsultation <= model.DateEnd.Value);
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(model.ClassroomId))
					{
						selectedRecords = selectedRecords
							.Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == currentDates.Id);
					}
					if (!string.IsNullOrEmpty(model.GroupName))
					{
						selectedRecords = selectedRecords
							.Where(sr => sr.LessonGroup == model.GroupName && sr.SeasonDatesId == currentDates.Id);
					}
					if (model.LecturerId.HasValue)
					{
						selectedRecords = selectedRecords
							.Where(sr => sr.LecturerId == model.LecturerId.Value && sr.SeasonDatesId == currentDates.Id);
					}
				}

				var records = selectedRecords.ToList();
				ResultService resultCheck;
				ConsultationRecordRecordBindingModel record;
				List<ConsultationRecordShortViewModel> result = new List<ConsultationRecordShortViewModel>();
				for (int i = 0; i < records.Count; ++i)
				{
					record = new ConsultationRecordRecordBindingModel
					{
						ClassroomId = model.ClassroomId,
						LecturerId = model.LecturerId,
						// TODO посомтреть по группе
						DateConsultation = records[i].DateConsultation
					};
					resultCheck = _serviceCR.CheckCreateConsultation(record, currentDates);

					if (resultCheck.Succeeded)
					{
						string groups = GetLessonGroup(records[i]);
						result.Add(new ConsultationRecordShortViewModel
						{
							Id = records[i].Id,
							Week = record.Week.Value,
							Day = record.Day.Value,
							Lesson = record.Lesson.Value,
							DateConsultation = records[i].DateConsultation,
							LessonLecturer = GetLessonLecturer(records[i]),
							LessonDiscipline = GetLessonDiscipline(records[i]),
							LessonGroup = groups,
							LessonClassroom = GetLessonClassroom(records[i])
						});
					}
				}

				return ResultService<List<ConsultationRecordShortViewModel>>.Success(
					result.OrderBy(e => e.Id).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ConsultationRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ConsultationRecordShortViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}
		#endregion

		#region ClearRecords
		public ResultService ClearSemesterRecords(ClassroomGetBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var records = _context.SemesterRecords.Where(sr => sr.ClassroomId == model.Id && sr.SeasonDatesId == currentDates.Id);
				_context.SemesterRecords.RemoveRange(records);
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearSemesterRecords(StudentGroupGetBindingModel model)
		{
			try
			{
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					var resultCurrentDates = GetCurrentDates();
					if (!resultCurrentDates.Succeeded)
					{
						return ResultService.Error("Error:", "CurrentSetting not found",
							ResultServiceStatusCode.NotFound);
					}
					var currentDates = resultCurrentDates.Result;
					var records = _context.SemesterRecords.Include(sr => sr.StudentGroup).Where(sr => sr.StudentGroup != null && sr.StudentGroup.GroupName == model.GroupName
																									&& sr.SeasonDatesId == currentDates.Id);
					_context.SemesterRecords.RemoveRange(records);
					_context.SaveChanges();
				}
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearOffsetRecords(ClassroomGetBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var records = _context.OffsetRecords.Where(sr => sr.ClassroomId == model.Id && sr.SeasonDatesId == currentDates.Id);
				_context.OffsetRecords.RemoveRange(records);
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearOffsetRecords(StudentGroupGetBindingModel model)

		{
			try
			{
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					var resultCurrentDates = GetCurrentDates();
					if (!resultCurrentDates.Succeeded)
					{
						return ResultService.Error("Error:", "CurrentSetting not found",
							ResultServiceStatusCode.NotFound);
					}
					var currentDates = resultCurrentDates.Result;
					var records = _context.OffsetRecords.Include(sr => sr.StudentGroup).Where(sr => sr.StudentGroup != null && sr.StudentGroup.GroupName == model.GroupName
																									&& sr.SeasonDatesId == currentDates.Id);
					_context.OffsetRecords.RemoveRange(records);
					_context.SaveChanges();
				}
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearExaminationRecords(ClassroomGetBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var records = _context.ExaminationRecords.Where(sr => sr.ClassroomId == model.Id && sr.SeasonDatesId == currentDates.Id);
				_context.ExaminationRecords.RemoveRange(records);
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearExaminationRecords(StudentGroupGetBindingModel model)

		{
			try
			{
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					var resultCurrentDates = GetCurrentDates();
					if (!resultCurrentDates.Succeeded)
					{
						return ResultService.Error("Error:", "CurrentSetting not found",
							ResultServiceStatusCode.NotFound);
					}
					var currentDates = resultCurrentDates.Result;
					var records = _context.ExaminationRecords.Include(sr => sr.StudentGroup).Where(sr => sr.StudentGroup != null && sr.StudentGroup.GroupName == model.GroupName
																									&& sr.SeasonDatesId == currentDates.Id);
					_context.ExaminationRecords.RemoveRange(records);
					_context.SaveChanges();
				}
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearConsultationRecords(ClassroomGetBindingModel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;
				var records = _context.ConsultationRecords.Where(sr => sr.ClassroomId == model.Id && sr.SeasonDatesId == currentDates.Id);
				_context.ConsultationRecords.RemoveRange(records);
				_context.SaveChanges();
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ClearConsultationRecords(StudentGroupGetBindingModel model)

		{
			try
			{
				if (!string.IsNullOrEmpty(model.GroupName))
				{
					var resultCurrentDates = GetCurrentDates();
					if (!resultCurrentDates.Succeeded)
					{
						return ResultService.Error("Error:", "CurrentSetting not found",
							ResultServiceStatusCode.NotFound);
					}
					var currentDates = resultCurrentDates.Result;
					var records = _context.ConsultationRecords.Include(sr => sr.StudentGroup).Where(sr => sr.StudentGroup != null && sr.StudentGroup.GroupName == model.GroupName
																									&& sr.SeasonDatesId == currentDates.Id);
					_context.ConsultationRecords.RemoveRange(records);
					_context.SaveChanges();
				}
				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}
		#endregion

		public ResultService LoadScheduleHTMLForClassrooms(LoadHTMLForClassroomsBindingModel model)
		{
			var resultCurrentDates = GetCurrentDates();
			if (!resultCurrentDates.Succeeded)
			{
				return ResultService.Error("Error:", "CurrentSetting not found",
					ResultServiceStatusCode.NotFound);
			}
			var currentDates = resultCurrentDates.Result;
			WebClient web = new WebClient();
			web.Encoding = Encoding.Default;

			string strHTML = web.DownloadString(model.ScheduleUrl + "raspisan.htm");

			HtmlDocument document = new HtmlDocument();

			document.LoadHtml(strHTML);

			var nodes = document.DocumentNode.SelectNodes("//table/tr/td");
			var resError = new ResultService();
			
			foreach (var node in nodes)
			{
				if (node.InnerText != "\r\n")
				{
					var elem = node.ChildNodes.FirstOrDefault(e => e.Name.ToLower() == "a");
					if (elem != null)
					{
						try
						{
							var res = ParsingPage(model.ScheduleUrl + elem.Attributes.First().Value,
														(node.InnerText.Replace("\r\n", "").Replace(" ", "")), currentDates);
							if (!res.Succeeded)
							{
								foreach (var err in res.Errors)
								{
									resError.AddError(err.Key, err.Value);
								}
							}
							Thread.Sleep(100);
						}
						catch (Exception ex)
						{
							resError.AddError(ex);
						}
					}
				}
			}

			return resError;
		}

		public ResultService CheckSemesterRecordsIfNotComplite()
		{
			var records = _context.SemesterRecords.Include(sr => sr.Classroom).Where(sr =>
									(string.IsNullOrEmpty(sr.LessonDiscipline)) ||
									(sr.LessonType == LessonTypes.нд)).ToList();
			bool flag;
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					foreach (var record in records)
					{
						flag = false;
						if (string.IsNullOrEmpty(record.LessonDiscipline))
						{//если нет названия дисциплины
							var searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
		(sr.LessonGroup == record.LessonGroup || (sr.StudentGroupId == record.StudentGroupId && sr.StudentGroupId != null)) &&
		(sr.LessonLecturer == record.LessonLecturer || (sr.LecturerId == record.LecturerId && sr.LecturerId != null)) &&
		(sr.LessonClassroom == record.LessonClassroom || (sr.ClassroomId == record.ClassroomId && !string.IsNullOrEmpty(sr.ClassroomId))) &&
		!sr.IsStreaming && sr.Id != record.Id);
							if (searchMatches != null)
							{
								record.LessonDiscipline = searchMatches.LessonDiscipline;
								if (record.LessonType == LessonTypes.нд)
								{//если по мимо названия дисциплины нет и типа занятия
									record.LessonType = searchMatches.LessonType;
								}
								flag = true;
							}
							else
							{//если в этой аудитории нет такой пары, то ищем по другим аудиториям
								searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
		(sr.LessonGroup == record.LessonGroup || (sr.StudentGroupId == record.StudentGroupId && sr.StudentGroupId != null)) &&
		(sr.LessonLecturer == record.LessonLecturer || (sr.LecturerId == record.LecturerId && sr.LecturerId != null)) &&
		!sr.IsStreaming && sr.Id != record.Id);
								if (searchMatches != null)
								{
									record.LessonDiscipline = searchMatches.LessonDiscipline;
									if (record.LessonType == LessonTypes.нд)
									{//если по мимо названия дисциплины нет и типа занятия
										record.LessonType = searchMatches.LessonType;
									}
									flag = true;
								}
							}
						}
						if (record.LessonType == LessonTypes.нд)
						{//если нет типа занятия
							var searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
													(sr.LessonGroup == record.LessonGroup || sr.StudentGroupId == record.StudentGroupId) &&
													(sr.LessonLecturer == record.LessonLecturer || sr.LecturerId == record.LecturerId) &&
													(sr.LessonClassroom == record.LessonClassroom || sr.ClassroomId == record.ClassroomId) &&
													(sr.LessonDiscipline == record.LessonDiscipline) &&
													(sr.LessonType != record.LessonType) &&
													!sr.IsStreaming && sr.Id != record.Id);
							if (searchMatches != null)
							{
								record.LessonType = searchMatches.LessonType;
								flag = true;
							}
							else
							{
								if (record.Classroom != null)
								{
									searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
													(sr.LessonGroup == record.LessonGroup || sr.StudentGroupId == record.StudentGroupId) &&
													(sr.LessonLecturer == record.LessonLecturer || sr.LecturerId == record.LecturerId) &&
													(sr.ClassroomId == record.ClassroomId && sr.Classroom.ClassroomType == record.Classroom.ClassroomType) &&
													(sr.LessonDiscipline == record.LessonDiscipline) &&
													(sr.LessonType != record.LessonType) &&
													!sr.IsStreaming && sr.Id != record.Id);
									if (searchMatches != null)
									{
										record.LessonType = searchMatches.LessonType;
									}
									else
									{
										switch (record.Classroom.ClassroomType)
										{
											case ClassroomTypes.Дисплейный:
												record.LessonType = LessonTypes.лаб;
												break;
											case ClassroomTypes.Проекторный:
												record.LessonType = LessonTypes.лек;
												break;
											case ClassroomTypes.Обычный:
												record.LessonType = LessonTypes.пр;
												break;
										}
									}
									flag = true;
								}
							}
						}
						if (flag)
						{
							_context.Entry(record).State = EntityState.Modified;
							_context.SaveChanges();
						}
					}
					transaction.Commit();
					return ResultService.Success();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}

		public ResultService ExportSemesterRecordExcel(ExportToExcelClassroomsBindingModel model)
		{
			var excel = new Application();
			try
			{
				if (File.Exists(model.FileName))
				{
					excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing);
				}
				else
				{
					excel.SheetsInNewWorkbook = model.Classrooms.Count;
					excel.Workbooks.Add(Type.Missing);
					excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
						XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				}
				Sheets excelsheets = excel.Workbooks[1].Worksheets;
				for (int r = 1; r <= model.Classrooms.Count; r++)
				{
					var excelworksheet = (Worksheet)excelsheets.get_Item(r);//Получаем ссылку на лист
					excelworksheet.Cells.Clear();
					excelworksheet.Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
					excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
					excelworksheet.PageSetup.RightMargin = 0;
					excelworksheet.PageSetup.LeftMargin = 0;
					excelworksheet.PageSetup.TopMargin = 0;
					excelworksheet.PageSetup.BottomMargin = 0;
					excelworksheet.PageSetup.CenterHorizontally = true;
					excelworksheet.PageSetup.CenterVertically = true;
					#region шапка
					var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
					var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
					var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
					if (!result.Succeeded)
					{
						return ResultService.Error("Error:", "LessonTime not found",
							ResultServiceStatusCode.NotFound);
					}
					var times = result.Result;

					Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days.Length));
					excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
					excelcells.Borders.Weight = XlBorderWeight.xlThin;
					excelcells.HorizontalAlignment = Constants.xlCenter;
					excelcells.VerticalAlignment = Constants.xlCenter;
					excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
											XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 8;

					excelcells = excelworksheet.get_Range("A" + (4 + days.Length), simbols[times.Count] + (4 + days.Length * 2));
					excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
					excelcells.Borders.Weight = XlBorderWeight.xlThin;
					excelcells.HorizontalAlignment = Constants.xlCenter;
					excelcells.VerticalAlignment = Constants.xlCenter;
					excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
											XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 8;

					excelcells = excelworksheet.get_Range("A2", "A2");
					excelcells.Value2 = "I неделя";
					excelcells.ColumnWidth = 9;
					excelcells.RowHeight = 30;
					excelcells = excelworksheet.get_Range("A" + (4 + days.Length), "A" + (4 + days.Length));
					excelcells.Value2 = "II неделя";
					excelcells.RowHeight = 30;
					for (int i = 0; i < days.Length; i++)
					{
						excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
						excelcells.RowHeight = 40;
						excelcells.Value2 = days[i];
						excelcells = excelworksheet.get_Range("A" + (5 + days.Length + i), "A" + (5 + days.Length + i));
						excelcells.RowHeight = 40;
						excelcells.Value2 = days[i];
					}
					for (int j = 0; j < times.Count; j++)
					{
						excelcells = excelworksheet.get_Range(simbols[j + 1] + 2, simbols[j + 1] + 2);
						excelcells.ColumnWidth = 15;
						excelcells.Value2 = times[j].Text;
						excelcells = excelworksheet.get_Range(simbols[j + 1] + (4 + days.Length), simbols[j + 1] + (4 + days.Length));
						excelcells.Value2 = times[j].Text;
					}
					#endregion
					#region тело

					var resultSemester = GetScheduleSemester(new ScheduleBindingModel
					{
						ClassroomId = model.Classrooms[r - 1]
					});
					if (!resultSemester.Succeeded)
					{
						return ResultService.Error("Error:", "ScheduleSemester not found",
							ResultServiceStatusCode.NotFound);
					}
					var list = resultSemester.Result;

					for (int i = 0; i < list.Count; i++)
					{
						excelcells = excelworksheet.get_Range(simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8),
							simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8));
						excelcells.Value2 = list[i].Text;
					}
					#endregion
					#region аудитория
					excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
					excelcells.Merge(Type.Missing);
					excelcells.Font.Bold = true;
					excelcells.Value2 = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
					excelcells.RowHeight = 25;
					excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 14;
					#endregion
				}

				excel.Workbooks[1].Save();
				excel.Quit();
				return ResultService.Success();
			}
			catch (Exception ex)
			{
				excel.Quit();
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ExportOffsetRecordExcel(ExportToExcelClassroomsBindingModel model)
		{
			try
			{
				var excel = new Application();
				if (File.Exists(model.FileName))
				{
					excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing, Type.Missing);
				}
				else
				{
					excel.SheetsInNewWorkbook = model.Classrooms.Count;
					excel.Workbooks.Add(Type.Missing);
					excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
						XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				}
				Sheets excelsheets = excel.Workbooks[1].Worksheets;
				for (int r = 1; r <= model.Classrooms.Count; r++)
				{
					var excelworksheet = (Worksheet)excelsheets.get_Item(r);//Получаем ссылку на лист
					excelworksheet.Cells.Clear();
					excelworksheet.Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
					excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
					excelworksheet.PageSetup.RightMargin = 0;
					excelworksheet.PageSetup.LeftMargin = 0;
					excelworksheet.PageSetup.TopMargin = 0;
					excelworksheet.PageSetup.BottomMargin = 0;
					excelworksheet.PageSetup.CenterHorizontally = true;
					excelworksheet.PageSetup.CenterVertically = true;
					#region шапка
					var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
					var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
					var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
					if (!result.Succeeded)
					{
						return ResultService.Error("Error:", "LessonTime not found",
							ResultServiceStatusCode.NotFound);
					}
					var times = result.Result;

					Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days.Length));
					excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
					excelcells.Borders.Weight = XlBorderWeight.xlThin;
					excelcells.HorizontalAlignment = Constants.xlCenter;
					excelcells.VerticalAlignment = Constants.xlCenter;
					excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
											XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 8;

					excelcells = excelworksheet.get_Range("A" + (4 + days.Length), simbols[times.Count] + (4 + days.Length * 2));
					excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
					excelcells.Borders.Weight = XlBorderWeight.xlThin;
					excelcells.HorizontalAlignment = Constants.xlCenter;
					excelcells.VerticalAlignment = Constants.xlCenter;
					excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
											XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 8;

					excelcells = excelworksheet.get_Range("A2", "A2");
					excelcells.Value2 = "I неделя";
					excelcells.ColumnWidth = 9;
					excelcells.RowHeight = 30;
					excelcells = excelworksheet.get_Range("A" + (4 + days.Length), "A" + (4 + days.Length));
					excelcells.Value2 = "II неделя";
					excelcells.RowHeight = 30;
					for (int i = 0; i < days.Length; i++)
					{
						excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
						excelcells.RowHeight = 40;
						excelcells.Value2 = days[i];
						excelcells = excelworksheet.get_Range("A" + (5 + days.Length + i), "A" + (5 + days.Length + i));
						excelcells.RowHeight = 40;
						excelcells.Value2 = days[i];
					}
					for (int j = 0; j < times.Count; j++)
					{
						excelcells = excelworksheet.get_Range(simbols[j + 1] + 2, simbols[j + 1] + 2);
						excelcells.ColumnWidth = 15;
						excelcells.Value2 = times[j].Text;
						excelcells = excelworksheet.get_Range(simbols[j + 1] + (4 + days.Length), simbols[j + 1] + (4 + days.Length));
						excelcells.Value2 = times[j].Text;
					}
					#endregion
					#region тело
					var resultOffset = GetScheduleOffset(new ScheduleBindingModel { ClassroomId = model.Classrooms[r - 1] });
					if (!resultOffset.Succeeded)
					{
						return ResultService.Error("Error:", "ScheduleOffset not found",
							ResultServiceStatusCode.NotFound);
					}
					var list = resultOffset.Result;
					for (int i = 0; i < list.Count; i++)
					{
						excelcells = excelworksheet.get_Range(simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8),
							simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8));
						excelcells.Value2 = list[i].Text;
					}
					#endregion
					#region аудитория
					excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
					excelcells.Merge(Type.Missing);
					excelcells.Font.Bold = true;
					excelcells.Value2 = model.Classrooms[r - 1];
					excelcells.RowHeight = 25;
					excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 14;
					#endregion
				}

				excel.Workbooks[1].Save();
				excel.Quit();
				return ResultService.Success();
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ExportExaminationRecordExcel(ExportToExcelClassroomsBindingModel model)
		{
			try
			{
				var excel = new Application();

				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;

				var currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
				var days = (Convert.ToDateTime(currentDates.DateEndExamination) - currentdate).Days;

				if (File.Exists(model.FileName))
				{
					excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
						Type.Missing);
				}
				else
				{
					excel.SheetsInNewWorkbook = model.Classrooms.Count;
					excel.Workbooks.Add(Type.Missing);
					excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
						XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				}
				Sheets excelsheets = excel.Workbooks[1].Worksheets;
				for (int r = 1; r <= model.Classrooms.Count; ++r)
				{
					var excelworksheet = (Worksheet)excelsheets.get_Item(r);//Получаем ссылку на лист
					excelworksheet.Cells.Clear();
					excelworksheet.Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
					excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
					excelworksheet.PageSetup.RightMargin = 0;
					excelworksheet.PageSetup.LeftMargin = 0;
					excelworksheet.PageSetup.TopMargin = 0;
					excelworksheet.PageSetup.BottomMargin = 0;
					excelworksheet.PageSetup.CenterHorizontally = true;
					excelworksheet.PageSetup.CenterVertically = true;
					#region шапка
					var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
					var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
					if (!result.Succeeded)
					{
						return ResultService.Error("Error:", "LessonTime not found",
							ResultServiceStatusCode.NotFound);
					}
					var times = result.Result;
					result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
					if (!result.Succeeded)
					{
						return ResultService.Error("Error:", "LessonTime not found",
							ResultServiceStatusCode.NotFound);
					}
					times.AddRange(result.Result);

					Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days));
					excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
					excelcells.Borders.Weight = XlBorderWeight.xlThin;
					excelcells.HorizontalAlignment = Constants.xlCenter;
					excelcells.VerticalAlignment = Constants.xlCenter;
					excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
											XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 8;

					for (int i = 0; i < days; i++)
					{
						excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
						excelcells.ColumnWidth = 8;
						excelcells.RowHeight = 40;
						excelcells.Formula = "DD.MM.YYYY";
						excelcells.Value2 = currentdate.ToShortDateString();
						currentdate = currentdate.AddDays(1);
					}
					for (int i = 0; i < times.Count; i++)
					{
						excelcells = excelworksheet.get_Range(simbols[i + 1] + (2), simbols[i + 1] + (2));
						excelcells.ColumnWidth = 20;
						excelcells.RowHeight = 30;
						excelcells.Value2 = times[i].Text;
					}
					#endregion
					#region тело
					currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
					var resultExamination = GetScheduleExamination(new ScheduleBindingModel { ClassroomId = model.Classrooms[r - 1] });
					if (!resultExamination.Succeeded)
					{
						return ResultService.Error("Error:", "ScheduleExamination not found",
							ResultServiceStatusCode.NotFound);
					}
					var list = resultExamination.Result;

					for (int i = 0; i < list.Count; i++)
					{
						if ((list[i].DateConsultation - currentdate).Days > -1 && (list[i].DateConsultation - currentdate).Days <= days)
						{
							for (int t = 0; t < times.Count; ++t)
							{
								if (list[i].DateConsultation.Hour == times[t].DateBeginLesson.Hour)
								{
									excelcells = excelworksheet.get_Range(simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3),
										simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3));
									excelcells.Value2 = list[i].Text;
									break;
								}
							}
						}
						if ((list[i].DateExamination - currentdate).Days > -1 && (list[i].DateExamination - currentdate).Days <= days)
						{
							for (int t = 0; t < times.Count; ++t)
							{
								if (list[i].DateExamination.Hour == times[t].DateBeginLesson.Hour)
								{
									excelcells = excelworksheet.get_Range(simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3),
										simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3));
									excelcells.Value2 = list[i].Text;
									break;
								}
							}
						}
					}
					#endregion
					#region аудитория
					excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
					excelcells.Merge(Type.Missing);
					excelcells.Font.Bold = true;
					excelcells.Value2 = model.Classrooms[r - 1];
					excelcells.RowHeight = 25;
					excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
					excelcells.Font.Name = "Times New Roman";
					excelcells.Font.Size = 14;
					#endregion
				}

				excel.Workbooks[1].Save();
				excel.Quit();
				return ResultService.Success();
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ExportSemesterRecordHTML(ExportToHTMLClassroomsBindingModel model)
		{
			try
			{
				var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				var times = result.Result;

				for (int i = 0; i < model.Classrooms.Count; ++i)
				{
					if (File.Exists(model.FilePath + "\\" +
						model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt"))
					{
						File.Delete(model.FilePath + "\\" +
							model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt");
					}
					var writer = new StreamWriter(new FileStream(model.FilePath + "\\" + model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt",
						FileMode.Create));
					var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
					var resultSemester = GetScheduleSemester(new ScheduleBindingModel
					{
						ClassroomId = model.Classrooms[i]
					});
					if (!resultSemester.Succeeded)
					{
						return ResultService.Error("Error:", "ScheduleSemester not found",
							ResultServiceStatusCode.NotFound);
					}
					var list = resultSemester.Result;

					#region тело
					writer.WriteLine(string.Format("<p class=\"rteright\">Дата обновления: {0} </ p >", DateTime.Now.ToShortDateString()));
					for (int j = 0; j < 2; j++)
					{
						writer.WriteLine("<table align='center' border='1' cellpadding='1' cellspacing='1'>\r\n\t<tbody>");
						writer.WriteLine("\t\t<tr>");
						writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 40px; background-color: rgb(0, 153, 51)'>");
						if (j == 0)
						{
							writer.WriteLine("\t\t\t<span style='color:#ffffff;'>I</span><span style='color:#ffffff;'> неделя</span></td>");
						}
						else
						{
							writer.WriteLine("\t\t\t<span style='color:#ffffff;'>II</span><span style='color:#ffffff;'> неделя</span></td>");
						}
						for (int t = 0; t < times.Count; ++t)
						{
							writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
							writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;font-size:10px;'>{0}<br />", times[t].Title));
							writer.WriteLine(string.Format("\t\t\t\t{0} - {1}</span></td>", times[t].TimeBeginLesson, times[t].TimeEndLesson));
						}
						for (int k = 0; k < 6; k++)
						{
							writer.WriteLine("\t\t<tr style='height: 40px'>");
							writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
							writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;'>{0}</span></td>", days[k]));
							for (int r = 0; r < 8; r++)
							{
								if (r % 2 != 0)
								{
									writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(255, 255, 255)'>");
								}
								else
								{
									writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(204, 204, 204)'>");
								}
								if (list.Exists(rec => rec.Week == j && rec.Day == k && rec.Lesson == r))
								{
									var record = list.Find(rec => rec.Week == j && rec.Day == k && rec.Lesson == r);
									writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0} {1}<br />{2}<br />{3}</span></td>",
										record.LessonType, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
								}
								else
								{
									writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
								}
							}
							writer.WriteLine("\t\t</tr>");
						}
						writer.WriteLine("\t</tbody>\r\n</table>\r\n<p>&nbsp;</p>");
					}
					#endregion
					writer.Close();
				}
				return ResultService.Success();
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ExportOffsetRecordHTML(ExportToHTMLClassroomsBindingModel model)
		{
			try
			{
				var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				var times = result.Result;

				for (int i = 0; i < model.Classrooms.Count; ++i)
				{
					if (File.Exists(model.FilePath + "\\" +
					model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_off.txt"))
					{
						File.Delete(model.FilePath + "\\" +
						model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_off.txt");
					}
					var writer = new StreamWriter(new FileStream(model.FilePath + "\\" +
						model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_off.txt", FileMode.Create));
					var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
					var resultOffset = GetScheduleOffset(new ScheduleBindingModel
					{
						ClassroomId = model.Classrooms[i]
					});
					if (!resultOffset.Succeeded)
					{
						return ResultService.Error("Error:", "ScheduleOffset not found",
							ResultServiceStatusCode.NotFound);
					}
					var list = resultOffset.Result;

					#region тело
					writer.WriteLine(string.Format("<p class=\"rteright\">Дата обновления: {0} </ p >", DateTime.Now.ToShortDateString()));
					for (int j = 0; j < 2; j++)
					{
						writer.WriteLine("<table align='center' border='1' cellpadding='1' cellspacing='1'>\r\n\t<tbody>");
						writer.WriteLine("\t\t<tr>");
						writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 40px; background-color: rgb(0, 153, 51)'>");
						if (j == 0)
						{
							writer.WriteLine("\t\t\t<span style='color:#ffffff;'>I</span><span style='color:#ffffff;'> неделя</span></td>");
						}
						else
						{
							writer.WriteLine("\t\t\t<span style='color:#ffffff;'>II</span><span style='color:#ffffff;'> неделя</span></td>");
						}
						for (int t = 0; t < times.Count; ++t)
						{
							writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
							writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;font-size:10px;'>{0}<br />", times[t].Title));
							writer.WriteLine(string.Format("\t\t\t\t{0} - {1}</span></td>", times[t].TimeBeginLesson, times[t].TimeEndLesson));
						}
						for (int k = 0; k < 6; k++)
						{
							writer.WriteLine("\t\t<tr style='height: 40px'>");
							writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
							writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;'>{0}</span></td>", days[k]));
							for (int r = 0; r < 8; r++)
							{
								if (r % 2 != 0)
								{
									writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(255, 255, 255)'>");
								}
								else
								{
									writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(204, 204, 204)'>");
								}
								if (list.Exists(rec => rec.Week == j && rec.Day == k && rec.Lesson == r))
								{
									var record = list.Find(rec => rec.Week == j && rec.Day == k && rec.Lesson == r);
									writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>зач. {0}<br />{1}<br />{2}</span></td>",
										record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
								}
								else
								{
									writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
								}
							}
							writer.WriteLine("\t\t</tr>");
						}
						writer.WriteLine("\t</tbody>\r\n</table>\r\n<p>&nbsp;</p>");
					}
					#endregion
					writer.Close();
				}
				return ResultService.Success();
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ExportExaminationRecordHTML(ExportToHTMLClassroomsBindingModel model)
		{
			try
			{
				var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				var times = result.Result;
				result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				times.AddRange(result.Result);

				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;

				for (int i = 0; i < model.Classrooms.Count; ++i)
				{
					if (File.Exists(model.FilePath + "\\" +
					model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_exa.txt"))
					{
						File.Delete(model.FilePath + "\\" +
					model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_exa.txt");
					}
					var writer = new StreamWriter(new FileStream(model.FilePath + "\\" +
					model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_exa.txt", FileMode.Create));

					#region тело
					writer.WriteLine("<table align='center' border='1' cellpadding='1' cellspacing='1'>\r\n\t<tbody>");
					writer.WriteLine("\t\t<tr>");
					writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 40px; background-color: rgb(0, 153, 51)'>");
					writer.WriteLine("\t\t\t<span style='color:#ffffff;'>Сессия</span></td>");
					for (int t = 0; t < times.Count; ++t)
					{
						writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
						writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;font-size:10px;'>{0}<br />", times[t].Title));
						writer.WriteLine(string.Format("\t\t\t\t{0} - {1}</span></td>", times[t].TimeBeginLesson, times[t].TimeEndLesson));
					}


					var currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
					var days = (Convert.ToDateTime(currentDates.DateEndExamination) - currentdate).Days;

					var resultExamination = GetScheduleExamination(new ScheduleBindingModel { ClassroomId = model.Classrooms[i] });
					if (!resultExamination.Succeeded)
					{
						return ResultService.Error("Error:", "ScheduleExamination not found",
							ResultServiceStatusCode.NotFound);
					}
					var list = resultExamination.Result;


					for (int k = 0; k < days; k++)
					{
						writer.WriteLine("\t\t<tr style='height: 40px'>");
						writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
						writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>" + currentdate.ToShortDateString() + "</span></td>");
						for (int r = 0; r < times.Count; r++)
						{
							if (r % 2 != 0)
							{
								writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(255, 255, 255)'>");
							}
							else
							{
								writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(204, 204, 204)'>");
							}
							switch (r)
							{
								case 0:
									if (list.Exists(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
																										times[0].DateBeginLesson.Hour))
									{
										var record = list.Find(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
																													times[0].DateBeginLesson.Hour);
										writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
											record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
									}
									else
									{
										writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
									}
									break;
								case 1:
									if (list.Exists(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
																										times[1].DateBeginLesson.Hour))
									{
										var record = list.Find(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
																													times[1].DateBeginLesson.Hour);
										writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
											record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
									}
									else
									{
										writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
									}
									break;
								case 2:
									writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
									break;
								case 3:
									if (list.Exists(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
																											times[3].DateBeginLesson.Hour))
									{
										var record = list.Find(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
																														times[3].DateBeginLesson.Hour);
										writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
											record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
									}
									else
									{
										writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
									}
									break;
								case 4:
									if (list.Exists(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
																											times[4].DateBeginLesson.Hour))
									{
										var record = list.Find(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
																														times[4].DateBeginLesson.Hour);
										writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
											record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
									}
									else
									{
										writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
									}
									break;
							}
						}
						writer.WriteLine("\t\t</tr>");
						currentdate = currentdate.AddDays(1);
					}
					writer.WriteLine("\t</tbody>\r\n</table>\r\n<p>&nbsp;</p>");
					#endregion
					writer.Close();
				}
				return ResultService.Success();
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		#region Import
		public ResultService ImportExcel(ImportToOffsetFromExcel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;

				var excel = new Application();
				using (var transaction = _context.Database.BeginTransaction())
				{
					try
					{
						var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
							Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

						var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(1);//Получаем ссылку на лист 1
						var excelcell = excelworksheet.get_Range("A2", "A2");

						//while (true)
						//{
						while (excelcell.Value2 != null)
						{
							DateTime dateOffset = Convert.ToDateTime(excelcell.Value2);

							int week = (dateOffset - Convert.ToDateTime(currentDates.DateBeginOffset)).Days < 7 ? 0 : 1;
							int day = (dateOffset - Convert.ToDateTime(currentDates.DateBeginOffset)).Days + week * 7;
							int lesson = Convert.ToInt32(excelcell.get_Offset(0, 5).Value2) - 1;
							string studentGroupName = excelcell.get_Offset(0, 1).Value2;
							string disciplineName = excelcell.get_Offset(0, 2).Value2;
							string lecturerName = excelcell.get_Offset(0, 3).Value2;
							string classroomId = Convert.ToString(excelcell.get_Offset(0, 4).Value2);

							var classroom = _context.Classrooms.FirstOrDefault(c => c.Id.Contains(classroomId));

							var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == lecturerName);

							var group = _context.StudentGroups.SingleOrDefault(rec => rec.GroupName.Contains(studentGroupName));

							//var disc = _context.d.Disciplines.SingleOrDefault(rec => rec.DisciplineShortName == disciplineName);
							//if (disc != null)
							//{
							//    discID = disc.Id;
							//}
							//else
							//{
							//    disc = _db.Disciplines.SingleOrDefault(rec => rec.DisciplineFullName == disciplineName);
							//    if (disc != null)
							//    {
							//        discID = disc.Id;
							//    }
							//    else
							//    {
							//        disc = _db.Disciplines.SingleOrDefault(rec => rec.DisciplineOtherName == disciplineName);
							//        if (disc != null)
							//        {
							//            discID = disc.Id;
							//        }
							//        else
							//        {
							//            excelcell = excelcell.get_Offset(1, 0);
							//            continue;
							//        }
							//    }
							//}
							long? lecturerId = null;
							if (lecturer != null)
							{
								lecturerId = lecturer.Id;
							}
							long? studentGroupId = null;
							if (group != null)
							{
								studentGroupId = group.Id;
							}

							var result = _serviceOR.CreateOffsetRecord(new OffsetRecordRecordBindingModel
							{
								Week = week,
								Day = day,
								Lesson = lesson,
								LessonClassroom = classroomId,
								LessonDiscipline = disciplineName,
								LessonGroup = studentGroupName,
								LessonLecturer = lecturerName,
								ClassroomId = classroom != null ? classroom.Id : string.Empty,
								LecturerId = lecturerId,
								StudentGroupId = studentGroupId
							});
							if (!result.Succeeded)
							{
								excel.Quit();
								return result;
							}
							excelcell = excelcell.get_Offset(1, 0);
						}
						transaction.Commit();
						return ResultService.Success();
						//}
					}
					catch (Exception)
					{
						transaction.Rollback();
						excel.Quit();
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService ImportExcel(ImportToExaminationFromExcel model)
		{
			try
			{
				var resultCurrentDates = GetCurrentDates();
				if (!resultCurrentDates.Succeeded)
				{
					return ResultService.Error("Error:", "CurrentSetting not found",
						ResultServiceStatusCode.NotFound);
				}
				var currentDates = resultCurrentDates.Result;

				var excel = new Application();

				using (var transaction = _context.Database.BeginTransaction())
				{
					try
					{
						var workbook = excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
							Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

						var excelworksheet = (Worksheet)workbook.Worksheets.get_Item(1);//Получаем ссылку на лист 1
						var excelcell = excelworksheet.get_Range("A2", "A2");

						while (excelcell.Value2 != null)
						{
							DateTime dateConsult = Convert.ToDateTime(excelcell.Value2);
							DateTime dateExam = Convert.ToDateTime(excelcell.get_Offset(0, 1).Value2);

							string studentGroupName = excelcell.get_Offset(0, 2).Value2;
							string disciplineName = excelcell.get_Offset(0, 3).Value2;
							string lecturerName = excelcell.get_Offset(0, 4).Value2;
							string classroomId = excelcell.get_Offset(0, 5).Value2;

							var classroom = _context.Classrooms.FirstOrDefault(c => c.Id.Contains(classroomId));

							var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName.Contains(lecturerName));

							var group = _context.StudentGroups.SingleOrDefault(rec => rec.GroupName.Contains(studentGroupName));

							long? lecturerId = null;
							if (lecturer != null)
							{
								lecturerId = lecturer.Id;
							}
							long? studentGroupId = null;
							if (group != null)
							{
								studentGroupId = group.Id;
							}

							var result = _serviceER.CreateExaminationRecord(new ExaminationRecordRecordBindingModel
							{
								DateConsultation = dateConsult,
								DateExamination = dateExam,
								LessonClassroom = classroomId,
								LessonDiscipline = disciplineName,
								LessonGroup = studentGroupName,
								LessonLecturer = lecturerName,
								ClassroomId = classroom != null ? classroom.Id : string.Empty,
								LecturerId = lecturerId,
								StudentGroupId = studentGroupId
							});
							if (!result.Succeeded)
							{
								excel.Quit();
								return result;
							}
							excelcell = excelcell.get_Offset(1, 0);
						}
						transaction.Commit();
						return ResultService.Success();
					}
					catch (Exception)
					{
						transaction.Rollback();
						excel.Quit();
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}
		#endregion

		#region парсинг страниц html
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schedulrUrl"></param>
		/// <param name="classrooms"></param>
		private ResultService ParsingPage(string schedulrUrl, string groupName, SeasonDatesViewModel currentDates)
		{
			string[] days = new string[] { "Пнд", "Втр", "Срд", "Чтв", "Птн", "Сбт" };
			WebClient web = new WebClient();
			web.Encoding = Encoding.Default;
			string pageHTML = web.DownloadString(schedulrUrl);
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(pageHTML);
			var pageNodes = document.DocumentNode.SelectNodes("//table/tr/td");
			int week = -1;
			int day = -1;
			int para = -1;
			var resError = new ResultService();
			foreach (var pageNode in pageNodes)
			{
				string text = pageNode.InnerText.Replace("\r\n", "").Replace(" ", "");
				if (days.Contains(text))
				{
					if (days[0].Contains(text))
					{
						week++;
						day = -1;
					}
					day++;
					para = -1;
				}
				if (week > -1)
				{
					var elem = pageNode.ChildNodes.First().NextSibling;
					if (elem.Name.ToLower() == "font")
					{
						para++;
						var lesson = pageNode.InnerText.Replace("\r\n", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						if (lesson[0] == "_")
						{
							// пустая пара, переходим к следующей
							continue;
						}
						var entityFirst = new SemesterRecordRecordBindingModel();
						var entitySecond = new SemesterRecordRecordBindingModel();

						entityFirst.Week = week;
						entityFirst.Day = day;
						entityFirst.Lesson = para;
						entityFirst.LessonGroup = groupName;
						entityFirst.SeasonDatesId = currentDates.Id;

						entitySecond.Week = week;
						entitySecond.Day = day;
						entitySecond.Lesson = para;
						entitySecond.SeasonDatesId = currentDates.Id;
						AnalisString(pageNode.InnerText, entityFirst, entitySecond);
						var result = CheckNewSemesterRecordForConflictAndSave(entityFirst);
						if (!result.Succeeded)
						{
							foreach (var err in result.Errors)
							{
								resError.AddError(err.Key, err.Value);
							}
						}
						result = CheckNewSemesterRecordForConflictAndSave(entitySecond);
						if (!result.Succeeded)
						{
							foreach (var err in result.Errors)
							{
								resError.AddError(err.Key, err.Value);
							}
						}
					}
				}
			}
			return resError;
		}

		private void AnalisString(string text, SemesterRecordRecordBindingModel recordFirst, SemesterRecordRecordBindingModel recordSecond)
		{
			recordFirst.NotParseRecord = recordSecond.NotParseRecord = text;
			text = Regex.Replace(text, @"(\-?)(\s?)\d(\s?)п/г", "").Replace("\r\n", "").TrimStart();
			// ищем в записе наличие аудиторий
			var classroomMatches = Regex.Matches(text, @"а.(\w{0,2})[\d]+(\-\d)*(\/\d)*");
			if (classroomMatches.Count == 0)
			{
				return;
			}
			for (int clM = 0; clM < classroomMatches.Count; ++clM)
			{
				var currentRecord = clM == 0 ? recordFirst : recordSecond;
				currentRecord.LessonClassroom = classroomMatches[clM].Value;
				var classroom = _context.Classrooms.FirstOrDefault(c => currentRecord.LessonClassroom.Contains(c.Id) && !c.IsDeleted);
				if (classroom != null)
				{
					currentRecord.ClassroomId = classroom.Id;
				}
				// убираем из текста номер аудитории, остается предмет и преподаватель
				var lessonText = text.Substring(0, text.IndexOf(currentRecord.LessonClassroom)).TrimEnd();

				if (lessonText.ToLower().Contains("дударин"))
				{
				}


				// маленький костыль для второй записи
				if (lessonText.EndsWith("-"))
				{
					lessonText = lessonText.Substring(0, lessonText.Length - 1).TrimEnd();
				}
				// вычисляем преподавателя
				currentRecord.LessonLecturer = Regex.Match(lessonText, @"[\w]+(\ (\w(\.)?)?)?(\ (\w(\.)?)?)?$").Value;

				if (!string.IsNullOrEmpty(currentRecord.LessonLecturer))
				{
					var spliters = currentRecord.LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
					string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
					string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
					string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
					var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
											((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
											((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
					if (lecturer != null)
					{
						currentRecord.LecturerId = lecturer.Id;
					}
				}
				// оставляем только название предмета
				currentRecord.LessonDiscipline = lessonText.Substring(0, lessonText.IndexOf(currentRecord.LessonLecturer)).TrimEnd();

				currentRecord.LessonType = LessonTypes.нд.ToString();
				if (currentRecord.LessonDiscipline.StartsWith("лек."))
				{
					currentRecord.LessonType = LessonTypes.лек.ToString();
					currentRecord.LessonDiscipline = currentRecord.LessonDiscipline.Remove(0, 4);
				}
				if (currentRecord.LessonDiscipline.StartsWith("пр."))
				{
					currentRecord.LessonType = LessonTypes.пр.ToString();
					currentRecord.LessonDiscipline = currentRecord.LessonDiscipline.Remove(0, 3);
				}
				if (currentRecord.LessonDiscipline.StartsWith("лаб."))
				{
					currentRecord.LessonType = LessonTypes.лаб.ToString();
					currentRecord.LessonDiscipline = currentRecord.LessonDiscipline.Remove(0, 4);
				}
				//определяем группу
				if (!string.IsNullOrEmpty(recordFirst.LessonGroup))
				{
					var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == recordFirst.LessonGroup && !sg.IsDeleted);
					if (group != null)
					{
						currentRecord.StudentGroupId = group.Id;
					}
				}
				text = text.Substring(text.IndexOf(currentRecord.LessonClassroom) + currentRecord.LessonClassroom.Length).TrimStart();
			}
		}

		/// <summary>
		/// Проверяем добавляемую пару на конфликты
		/// </summary>
		/// <param name="record"></param>
		/// <param name="error"></param>
		private ResultService CheckNewSemesterRecordForConflictAndSave(SemesterRecordRecordBindingModel record)
		{
			try
			{
				if (record.ClassroomId == null && record.StudentGroupId == null && record.LecturerId == null)
				{
					return ResultService.Success();
				}
				//ищем занятие другой группы в этой аудитории
				var exsistRecord = _context.SemesterRecords.FirstOrDefault(r => r.Week == record.Week &&
										r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == record.SeasonDatesId &&
										r.LessonClassroom == record.LessonClassroom && r.LessonGroup != record.LessonGroup);
				if (exsistRecord != null)
				{//если на этой неделе в этот день этой парой в этой аудитории уже есть занятие
					if (exsistRecord.LessonDiscipline == record.LessonDiscipline &&
						exsistRecord.LessonLecturer == record.LessonLecturer &&
						exsistRecord.LessonType.ToString() == record.LessonType)
					{//если совпадает дисицпилна, преподаватель и тип занятия, то это потоковое занятие
						record.IsStreaming = true;
						exsistRecord.IsStreaming = true;

						_context.Entry(exsistRecord).State = EntityState.Modified;
						_context.SaveChanges();

						return _serviceSR.CreateSemesterRecord(record);
					}
					else if (exsistRecord.LessonType == LessonTypes.удл)
					{//занятие было помечено как удаленное, т.е. по факту пара не проводилась, так что просто удаляем ее
						var result = _serviceSR.DeleteSemesterRecord(new SemesterRecordGetBindingModel { Id = exsistRecord.Id });
						if (!result.Succeeded)
						{
							return result;
						}
						return _serviceSR.CreateSemesterRecord(record);
					}
					else
					{
						return ResultService.Error("Конфликт (аудитории):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
							record.Week, record.Day, record.Lesson,
							exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonClassroom), ResultServiceStatusCode.Error);
					}
				}

				//ищем занятие этой группы в другой аудитории
				exsistRecord = _context.SemesterRecords.FirstOrDefault(r => r.Week == record.Week &&
											  r.Day == record.Day && r.Lesson == record.Lesson && r.SeasonDatesId == record.SeasonDatesId &&
											  r.LessonGroup == record.LessonGroup && r.LessonClassroom != record.LessonClassroom);
				if (exsistRecord != null)
				{//если на этой неделе в этот день этой парой у этой группы уже есть занятие
					if (exsistRecord.LessonType.ToString() == record.LessonType ||
										exsistRecord.LessonType == LessonTypes.нд || record.LessonType == LessonTypes.нд.ToString())
					{//если совпадает тип занятия (или у одного из занятий неизвестен тип), но аудитории разные, то это лабораторные по подгруппам
						return _serviceSR.CreateSemesterRecord(record);
					}
					else if (exsistRecord.LessonType == LessonTypes.удл)
					{//занятие было помечено как удаленное, т.е. по факту пара не проводилась, так что просто удаляем ее
						var result = _serviceSR.DeleteSemesterRecord(new SemesterRecordGetBindingModel { Id = exsistRecord.Id });
						if (!result.Succeeded)
						{
							return result;
						}
						return _serviceSR.CreateSemesterRecord(record);
					}
					else
					{
						return ResultService.Error("Конфликт (группы):", string.Format("дата {0} {1} {2}\r\n{3} - {4}\r\n{5} {6} {7}\r\n",
							record.Week, record.Day, record.Lesson,
							exsistRecord.LessonGroup, record.LessonGroup, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup), ResultServiceStatusCode.Error);
					}
				}
				if (record.StudentGroupId != null || record.ClassroomId != null || record.LecturerId != null)
				{
					return _serviceSR.CreateSemesterRecord(record);
				}
				else
				{
					return ResultService.Success();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region Конвертация ScheduleRecord
		private string GetLessonLecturer(ScheduleRecord entity)
		{
			string str = entity.LecturerId.HasValue ? entity.Lecturer.ToString() : entity.LessonLecturer;
			if (!entity.LecturerId.HasValue)
			{
				if (string.IsNullOrEmpty(str))
				{
					str = "";
				}
				else
				{
					var strs = str.Split(' ');
					if (strs.Length == 1)
					{
						str = string.Format("{0}{1}", strs[0][0], strs[0].Substring(1).ToLower());
					}
					else if (strs.Length == 2)
					{
						str = string.Format("{0}{1} {2}", strs[0][0], strs[0].Substring(1).ToLower(), strs[1]);
					}
					else if (strs.Length == 3)
					{
						str = string.Format("{0}{1} {2} {3}", strs[0][0], strs[0].Substring(1).ToLower(), strs[1], strs[2]);
					}
				}
			}
			return str;
		}

		private string GetLessonDiscipline(ScheduleRecord entity)
		{
			string str = entity.LessonDiscipline;

			if (str.Length > 10)
			{
				var strs = str.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < strs.Length; ++i)
				{
					if (strs.Length == 1)
					{
						sb.Append(string.Format("{0}.", strs[0].Substring(0, 8)));
					}
					else if (strs[i].Length == 1)
					{
						sb.Append(strs[i]);
					}
					else if (strs[i].ToUpper() == strs[i])
					{
						sb.Append(strs[i].ToUpper());
					}
					else
					{
						sb.Append(strs[i][0].ToString().ToUpper());
						for (int j = 1; j < strs[i].Length; ++j)
						{
							if (strs[i][j] == '-')
							{
								continue;
							}
							if (strs[i][j].ToString().ToUpper() == strs[i][j].ToString())
							{
								sb.Append(strs[i][j].ToString().ToUpper());
							}
						}
					}
				}
				str = sb.ToString();
			}
			return str;
		}

		private string GetLessonGroup(ScheduleRecord entity)
		{
			return entity.StudentGroupId.HasValue ? entity.StudentGroup.GroupName : entity.LessonGroup;
		}

		private string GetLessonClassroom(ScheduleRecord entity)
		{
			return string.IsNullOrEmpty(entity.ClassroomId) ? entity.LessonClassroom : entity.ClassroomId;
		}
		#endregion
	}
}
