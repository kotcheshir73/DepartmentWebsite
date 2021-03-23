﻿using AcademicYearInterfaces.BindingModels;
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
		private readonly ILecturerService _serviceL;

		private readonly IAcademicPlanRecordMissionService _academicPlanRecordMissionService;

		private readonly IAcademicYearProcess _academicYearProcess;

		private IMemoryCache cache;

		public BaseService(ILecturerService serviceL, IAcademicPlanRecordMissionService academicPlanRecordMissionService, IAcademicYearProcess academicYearProcess, 
			IMemoryCache memoryCache)
		{
			_serviceL = serviceL;
			_academicPlanRecordMissionService = academicPlanRecordMissionService;
			_academicYearProcess = academicYearProcess;
			cache = memoryCache;
		}

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
