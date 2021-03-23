using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DepartmentWebCore.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentWebCore.Services
{
	public class BaseService
	{
		private readonly IClassroomService _serviceBC;

		private readonly IDisciplineService _serviceBD;

		private readonly IEducationDirectionService _serviceBED;

		private readonly ILecturerService _serviceBL;

		private readonly IStudentGroupService _serviceBSG;

		private readonly IUserService _serviceAU;

		private readonly IContingentService _serviceAYC;

		private readonly IAcademicPlanRecordElementService _serviceAYAPPE;

		private readonly IAcademicPlanRecordMissionService _serviceAYAPPM;

		private readonly IAcademicYearProcess _academicYearProcess;

		private readonly IMemoryCache cache;

		public BaseService(IClassroomService serviceBC, IDisciplineService serviceBD, IEducationDirectionService serviceBED, 
			ILecturerService serviceBL, IStudentGroupService serviceBSG, IUserService serviceAU, IContingentService serviceAYC,
			IAcademicPlanRecordElementService serviceAYAPPE, IAcademicPlanRecordMissionService serviceAYAPPM, 
			IAcademicYearProcess academicYearProcess, IMemoryCache memoryCache)
		{
			_serviceBC = serviceBC;
			_serviceBD = serviceBD;
			_serviceBED = serviceBED;
			_serviceBL = serviceBL;
			_serviceBSG = serviceBSG;
			_serviceAU = serviceAU;
			_serviceAYC = serviceAYC;
			_serviceAYAPPE = serviceAYAPPE;
			_serviceAYAPPM = serviceAYAPPM;
			_academicYearProcess = academicYearProcess;
			cache = memoryCache;
		}

		/// <summary>
		/// Получения аудиторий
		/// </summary>
		/// <param name="notUseInSchedule">true - в которых не проводятся занятия</param>
		/// <returns></returns>
		public List<ClassroomViewModel> GetClassrooms(bool notUseInSchedule = false)
		{
			if (!cache.TryGetValue($"Classrooms", out List<ClassroomViewModel> listClassrooms))
			{
				var classroomList = _serviceBC.GetClassrooms(new ClassroomGetBindingModel { SkipCheck = true, NotUseInSchedule = notUseInSchedule });
				if (classroomList.Succeeded)
				{
					listClassrooms = classroomList.Result.List;
					cache.Set($"Classrooms", listClassrooms, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listClassrooms;
		}

		/// <summary>
		/// Получение дисциплин
		/// </summary>
		/// <returns></returns>
		public List<DisciplineViewModel> GetDisciplines()
		{
			if (!cache.TryGetValue($"Disciplines", out List<DisciplineViewModel> listDisciplines))
			{
				var disciplineList = _serviceBD.GetDisciplines(new DisciplineGetBindingModel { SkipCheck = true });
				if (disciplineList.Succeeded)
				{
					listDisciplines = disciplineList.Result.List;
					cache.Set($"Disciplines", listDisciplines, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listDisciplines;
		}

		/// <summary>
		/// Получение направлений
		/// </summary>
		/// <returns></returns>
		public List<EducationDirectionViewModel> GetEducationDirections()
		{
			if (!cache.TryGetValue($"EducationDirections", out List<EducationDirectionViewModel> listEducationDirections))
			{
				var educationDirectionList = _serviceBED.GetEducationDirections(new  EducationDirectionGetBindingModel { SkipCheck = true });
				if (educationDirectionList.Succeeded)
				{
					listEducationDirections = educationDirectionList.Result.List;
					cache.Set($"EducationDirections", listEducationDirections, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listEducationDirections;
		}

		/// <summary>
		/// Получение нправления
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public EducationDirectionViewModel GetEducationDirection(Guid id)
		{
			var listEducationDirections = GetEducationDirections();
			EducationDirectionViewModel model = null;
			if (listEducationDirections != null)
			{
				model = listEducationDirections.FirstOrDefault(x => x.Id == id);
			}
			if (model == null)
			{
				var educationDirection = _serviceBED.GetEducationDirection(new EducationDirectionGetBindingModel { Id = id, SkipCheck = true });
				if (!educationDirection.Succeeded)
				{
					return null;
				}
				model = educationDirection.Result;
				listEducationDirections.Add(model);
			}

			return model;
		}

		/// <summary>
		/// Список курсов направления
		/// </summary>
		/// <param name="educationDirectionId"></param>
		/// <returns></returns>
		public List<(Guid Id, string Course)> GetCourses(Guid educationDirectionId)
		{
			if (!cache.TryGetValue($"EducationDirectionsCourses:{educationDirectionId}", out List<(Guid Id, string Course)> courses))
			{
				var contingent = _serviceAYC.GetContingents(new ContingentGetBindingModel
				{
					SkipCheck = true,
					EducationDirectionId = educationDirectionId,
					AcademicYearId = _academicYearProcess.GetCurrentAcademicYear().Result.Id
				});
				if (contingent.Succeeded)
				{
					courses = contingent.Result.List
						.Select(x => (Id: x.Id, Course: $"Курс {Math.Log((double)x.Course, 2) + 1}")).OrderBy(x => x.Course).ToList();
					cache.Set($"EducationDirectionsCourses:{educationDirectionId}", courses, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return courses;
		}

		/// <summary>
		/// Список дисциплин курса направления
		/// </summary>
		/// <param name="contingentId"></param>
		/// <returns></returns>
		public List<EducationDirectionDisciplineByCoursesModel> GetEducationDirectionDisciplineByCourses(Guid contingentId)
		{
			if (!cache.TryGetValue($"EducationDirectionDisciplineByCourses:{contingentId}", out List<EducationDirectionDisciplineByCoursesModel> disciplines))
			{
				var elements = _serviceAYAPPE.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel
				{
					SkipCheck = true,
					ContingentId = contingentId,
					AcademicYearId = _academicYearProcess.GetCurrentAcademicYear().Result.Id,
					TimeNormUseInSite = true,
					AcademicPlanRecordIsSelected = true
				});
				if (elements.Succeeded)
				{
					disciplines = elements.Result.List.Select(x => new EducationDirectionDisciplineByCoursesModel
					{
						DisciplineId = x.DisciplineId,
						DisciplineName = x.Discipline,
						Semester = x.Semester,
						TimeNormName = x.KindOfLoadName
					}).Distinct()
					.OrderBy(x => x.Semester).ThenBy(x => x.DisciplineName)
					.ToList();
					cache.Set($"EducationDirectionDisciplineByCourses:{contingentId}", disciplines, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return disciplines;
		}

		/// <summary>
		/// Получение преподавателя
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public DisciplineViewModel GetDiscipline(Guid id)
		{
			var listDisciplines = GetDisciplines();
			DisciplineViewModel model = null;
			if (listDisciplines != null)
			{
				model = listDisciplines.FirstOrDefault(x => x.Id == id);
			}
			if (model == null)
			{
				var discipline = _serviceBD.GetDiscipline(new DisciplineGetBindingModel { Id = id, SkipCheck = true });
				if (!discipline.Succeeded)
				{
					return null;
				}
				model = discipline.Result;
				listDisciplines.Add(model);
			}

			return model;
		}

		/// <summary>
		/// Получение преподавателей
		/// </summary>
		/// <returns></returns>
		public List<LecturerViewModel> GetLecturers()
		{
			if (!cache.TryGetValue($"Lecturers", out List<LecturerViewModel> listLecturers))
			{
				var lecturerList = _serviceBL.GetLecturers(new LecturerGetBindingModel { SkipCheck = true });
				if (lecturerList.Succeeded)
				{
					listLecturers = lecturerList.Result.List;
					cache.Set($"Lecturers", listLecturers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listLecturers;
		}

		/// <summary>
		/// Получение преподавателя
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public LecturerViewModel GetLecturer(Guid id)
		{
			var listLecturers = GetLecturers();
			LecturerViewModel model = null;
			if (listLecturers != null)
			{
				model = listLecturers.FirstOrDefault(x => x.Id == id);
			}
			if (model == null)
			{
				var lecturer = _serviceBL.GetLecturer(new LecturerGetBindingModel { Id = id, SkipCheck = true });
				if (!lecturer.Succeeded)
				{
					return null;
				}
				model = lecturer.Result;
				listLecturers.Add(model);
			}

			return model;
		}

		/// <summary>
		/// Получение преподавателей
		/// </summary>
		/// <returns></returns>
		public List<StudentGroupViewModel> GetStudentGroups()
		{
			if (!cache.TryGetValue($"StudentGroups", out List<StudentGroupViewModel> listStudentGroups))
			{
				var studentGroupList = _serviceBSG.GetStudentGroups(new StudentGroupGetBindingModel { SkipCheck = true });
				if (studentGroupList.Succeeded)
				{
					listStudentGroups = studentGroupList.Result.List;
					cache.Set($"StudentGroups", listStudentGroups, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listStudentGroups;
		}

		/// <summary>
		/// Получение списка дисциплин преподавателя
		/// </summary>
		/// <param name="lecturerId"></param>
		/// <returns></returns>
		public List<(Guid Id, string Title)> GetDisciplineForLecturer(Guid lecturerId)
		{
			if (!cache.TryGetValue($"LecturerDisicplie:{lecturerId}", out List<(Guid Id, string Title)> list))
			{
				var missions = _serviceAYAPPM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel
				{
					LecturerId = lecturerId,
					AcademicYearId = _academicYearProcess.GetCurrentAcademicYear().Result.Id,
					SkipCheck = true
				});
				if (!missions.Succeeded)
				{
					return null;
				}

				list = new List<(Guid Id, string Title)>();
				foreach (var mis in missions.Result.List.Distinct())
				{
					list.Add((mis.DisciplineId, mis.DisciplineTitle));
				}
				list = list.Distinct().ToList();
				cache.Set($"LecturerDisicplie:{lecturerId}", list, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
			}

			return list;
		}

		/// <summary>
		/// Получение списка преподавателей дисциплины
		/// </summary>
		/// <param name="disciplineId"></param>
		/// <returns></returns>
		public List<(Guid Id, string Title)> GetLecturerForDiscipline(Guid disciplineId)
		{
			if (!cache.TryGetValue($"DisicplineMission:{disciplineId}", out List<AcademicPlanRecordMissionViewModel> missions))
			{
				var records = _serviceAYAPPM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel
				{
					DisciplineId = disciplineId,
					AcademicYearId = _academicYearProcess.GetCurrentAcademicYear().Result.Id,
					SkipCheck = true
				});
				if (!records.Succeeded)
				{
					return null;
				}
				missions = records.Result.List.Distinct().ToList();
				cache.Set($"DisicplineMission:{disciplineId}", missions, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
			}

			var list = new List<(Guid Id, string Title)>();
			foreach (var mis in missions)
			{
				list.Add((mis.LecturerId, mis.LecturerName));
			}
			list = list.Distinct().ToList();

			return list;
		}

		/// <summary>
		/// Получение списка преподавателей-пользователей дисциплины
		/// </summary>
		/// <param name="disciplineId"></param>
		/// <returns></returns>
		public (string Title, List<Guid> Users) GetLecturerUsersForDiscipline(Guid disciplineId)
		{
			if (!cache.TryGetValue($"DisicplineMission:{disciplineId}", out List<AcademicPlanRecordMissionViewModel> missions))
			{
				var records = _serviceAYAPPM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel
				{
					DisciplineId = disciplineId,
					AcademicYearId = _academicYearProcess.GetCurrentAcademicYear().Result.Id,
					SkipCheck = true
				});
				if (!records.Succeeded)
				{
					return default;
				}
				missions = records.Result.List.Distinct().ToList();
				cache.Set($"DisicplineMission:{disciplineId}", missions, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
			}

			var users = _serviceAU.GetUsers(new UserGetBindingModel
			{
				SkipCheck = true,
				LecturerIds = missions.Select(x => x.LecturerId).Distinct().ToList()
			});
			if (!users.Succeeded)
			{
				return default;
			}

			var record = (missions.FirstOrDefault().DisciplineTitle, users.Result.List.Select(x => x.Id).Distinct().ToList());

			return record;
		}

		/// <summary>
		/// Получение списка преподавателей-пользователей дисциплины
		/// </summary>
		/// <param name="disciplineId"></param>
		/// <returns></returns>
		public List<(string Semester, List<string> FolderNames)> GetDisciplineFolderNames(Guid disciplineId)
		{
			if (!cache.TryGetValue($"DisicplineMission:{disciplineId}", out List<AcademicPlanRecordMissionViewModel> missions))
			{
				var records = _serviceAYAPPM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel
				{
					DisciplineId = disciplineId,
					AcademicYearId = _academicYearProcess.GetCurrentAcademicYear().Result.Id,
					SkipCheck = true
				});
				if (!records.Succeeded)
				{
					return null;
				}
				missions = records.Result.List.Distinct().ToList();
				cache.Set($"DisicplineMission:{disciplineId}", missions, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
			}

			var filter = missions.Where(x => x.TimeNormShortName == "Лек" || x.TimeNormShortName == "Пр"
						|| x.TimeNormShortName == "Лаб" || x.TimeNormShortName == "КП" || x.TimeNormShortName == "КР"
						|| x.TimeNormShortName == "РГР" || x.TimeNormShortName == "Реф" || x.TimeNormShortName == "ЗсО"
						|| x.TimeNormShortName == "Зач" || x.TimeNormShortName == "Экз")
				.Select(x => new
				{
					Semestr = x.Semester,
					TimeNorm = x.TimeNormShortName,
				})
				.GroupBy(x => x.Semestr);

			return filter.Select(x => (Semester: x.Key, FolderNames: x.Select(y => GetFolderName(y.TimeNorm)).ToList())).ToList();
		}

		/// <summary>
		/// Получение имени папки по норме времени
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		private string GetFolderName(string source)
		{
			if (source == "КР")
			{
				return "Курсовая работа";
			}
			else if (source == "КП")
			{
				return "Курсовой проект";
			}
			else if (source == "Пр")
			{
				return "Практики";
			}
			else if (source == "Лаб")
			{
				return "Лабораторные";
			}
			else if (source == "Лек")
			{
				return "Лекции";
			}
			else if (source == "РГР")
			{
				return "РГР";
			}
			else if (source == "Реф")
			{
				return "Реферат";
			}
			else if (source == "ЗсО" || source == "Зач")
			{
				return "Зачет";
			}
			else if (source == "Экз")
			{
				return "Экзамен";
			}

			return source;
		}
	}
}
