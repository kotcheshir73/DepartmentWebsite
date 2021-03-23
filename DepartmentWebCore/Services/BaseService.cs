using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentWebCore.Services
{
	public class BaseService
	{
		private readonly IClassroomService _serviceC;

		private readonly ILecturerService _serviceL;

		private readonly IStudentGroupService _serviceSG;

		private readonly IAcademicPlanRecordMissionService _academicPlanRecordMissionService;

		private readonly IAcademicYearProcess _academicYearProcess;

		private IMemoryCache cache;

		public BaseService(IClassroomService serviceC, ILecturerService serviceL, IStudentGroupService serviceSG, 
			IAcademicPlanRecordMissionService academicPlanRecordMissionService, IAcademicYearProcess academicYearProcess, 
			IMemoryCache memoryCache)
		{
			_serviceC = serviceC;
			_serviceL = serviceL;
			_serviceSG = serviceSG;
			_academicPlanRecordMissionService = academicPlanRecordMissionService;
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
				var classroomList = _serviceC.GetClassrooms(new ClassroomGetBindingModel { SkipCheck = true, NotUseInSchedule = notUseInSchedule });
				if (classroomList.Succeeded)
				{
					listClassrooms = classroomList.Result.List;
					cache.Set($"Classrooms", listClassrooms, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listClassrooms;
		}

		/// <summary>
		/// Получение преподавателей
		/// </summary>
		/// <returns></returns>
		public List<LecturerViewModel> GetLecturers()
		{
			if (!cache.TryGetValue($"Lecturers", out List<LecturerViewModel> listLecturers))
			{
				var lecturerList = _serviceL.GetLecturers(new LecturerGetBindingModel { SkipCheck = true });
				if (lecturerList.Succeeded)
				{
					listLecturers = lecturerList.Result.List;
					cache.Set($"Lecturers", listLecturers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listLecturers;
		}

		/// <summary>
		/// Получение преподавателей
		/// </summary>
		/// <returns></returns>
		public List<StudentGroupViewModel> GetStudentGroups()
		{
			if (!cache.TryGetValue($"StudentGroups", out List<StudentGroupViewModel> listStudentGroups))
			{
				var studentGroupList = _serviceSG.GetStudentGroups(new StudentGroupGetBindingModel { SkipCheck = true });
				if (studentGroupList.Succeeded)
				{
					listStudentGroups = studentGroupList.Result.List;
					cache.Set($"StudentGroups", listStudentGroups, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
				}
			}

			return listStudentGroups;
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
				var lecturer = _serviceL.GetLecturer(new LecturerGetBindingModel { Id = id, SkipCheck = true });
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
		/// Получение списка дисцпилн преподавателя
		/// </summary>
		/// <param name="lecturerId"></param>
		/// <returns></returns>
		public List<(Guid Id, string Title)> GetDisciplineForLecutrer(Guid lecturerId)
		{
			if (!cache.TryGetValue($"LecturerDisicplie:{lecturerId}", out List<(Guid Id, string Title)> list))
			{
				var missions = _academicPlanRecordMissionService.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel
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
	}
}
