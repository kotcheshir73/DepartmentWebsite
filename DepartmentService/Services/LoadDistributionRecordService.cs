using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
	public class LoadDistributionRecordService : ILoadDistributionRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicPlanRecordService _serviceAPR;

		private readonly IContingentService _serviceC;

		private readonly ILecturerService _serviceL;

		private readonly ITimeNormService _serviceTN;

		public LoadDistributionRecordService(DepartmentDbContext context, IAcademicPlanRecordService serviceAPR, IContingentService serviceC, 
			ILecturerService serviceL, ITimeNormService serviceTN)
		{
			_context = context;
			_serviceAPR = serviceAPR;
			_serviceC = serviceC;
			_serviceL = serviceL;
			_serviceTN = serviceTN;
		}


		public ResultService<List<AcademicPlanRecordViewModel>> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
		{
			return _serviceAPR.GetAcademicPlanRecords(model);
		}

		public ResultService<List<ContingentViewModel>> GetContingents()
		{
			return _serviceC.GetContingents();
		}

		public ResultService<List<LecturerViewModel>> GetLecturers()
		{
			return _serviceL.GetLecturers();
		}

		public ResultService<List<TimeNormViewModel>> GetTimeNorms()
		{
			return _serviceTN.GetTimeNorms();
		}

		
		public ResultService<List<LoadDistributionRecordViewModel>> GetLoadDistributionRecords(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				if(!model.LoadDistributionId.HasValue)
				{
					throw new Exception("Отсутсвует идентификатор рапределения нагрузок");
				}
				return ResultService<List<LoadDistributionRecordViewModel>>.Success(
					ModelFactory.CreateLoadDistributionRecords(_context.LoadDistributionRecords
					.Where(e => e.LoadDistributionId == model.LoadDistributionId.Value)
						.Include(e => e.AcademicPlanRecord).Include(e => e.Contingent).Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.Discipline).Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear).Include(e => e.Contingent.StudentGroup)
						.Include(e => e.TimeNorm.KindOfLoad)
							.Where(e => /*(int)e.AcademicPlanRecord.Semester % 2 == model.SemesterTime && */!e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<LoadDistributionRecordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<LoadDistributionRecordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LoadDistributionRecordViewModel> GetLoadDistributionRecord(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributionRecords
						.Include(e => e.AcademicPlanRecord).Include(e => e.Contingent).Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.Discipline).Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear).Include(e => e.Contingent.StudentGroup)
						.Include(e => e.TimeNorm.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<LoadDistributionRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<LoadDistributionRecordViewModel>.Success(
					ModelFactory.CreateLoadDistributionRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model)
		{
			var entity = new LoadDistributionRecord
			{
				LoadDistributionId = model.LoadDistributionId,
				AcademicPlanRecordId = model.AcademicPlanRecordId,
				ContingentId = model.ContingentId,
				TimeNormId = model.TimeNormId,
				Load = model.Load,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.LoadDistributionRecords.Add(entity);
				_context.SaveChanges();
				return ResultService.Success(entity.Id);
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

		public ResultService UpdateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributionRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
				entity.ContingentId = model.ContingentId;
				entity.TimeNormId = model.TimeNormId;
				entity.Load = model.Load;

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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

		public ResultService DeleteLoadDistributionRecord(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributionRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.IsDeleted = true;
				entity.DateDelete = DateTime.Now;

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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

		public ResultService MakeLoadDistribution(LoadDistributionRecordGetBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var academicYearId = _context.LoadDistributions.FirstOrDefault(ld => ld.Id == model.LoadDistributionId.Value).AcademicYearId;
					var academicPlans = _context.AcademicPlans.Where(ap => ap.AcademicYearId == academicYearId);
					foreach(var academicPlan in academicPlans)
					{//получаем список учебных планов за нужный учебный год
						List<AcademicCourse> courses = new List<AcademicCourse>();
						if((academicPlan.AcademicCourses & AcademicCourse.Course_1) == AcademicCourse.Course_1)
						{
							courses.Add(AcademicCourse.Course_1);
						}
						if ((academicPlan.AcademicCourses & AcademicCourse.Course_2) == AcademicCourse.Course_2)
						{
							courses.Add(AcademicCourse.Course_2);
						}
						if ((academicPlan.AcademicCourses & AcademicCourse.Course_3) == AcademicCourse.Course_3)
						{
							courses.Add(AcademicCourse.Course_3);
						}
						if ((academicPlan.AcademicCourses & AcademicCourse.Course_4) == AcademicCourse.Course_4)
						{
							courses.Add(AcademicCourse.Course_4);
						}
						if ((academicPlan.AcademicCourses & AcademicCourse.Course_5) == AcademicCourse.Course_5)
						{
							courses.Add(AcademicCourse.Course_5);
						}
						if ((academicPlan.AcademicCourses & AcademicCourse.Course_6) == AcademicCourse.Course_6)
						{
							courses.Add(AcademicCourse.Course_6);
						}
						if(courses.Count == 0)
						{
							continue;
						}
						List<Semesters> semesters = new List<Semesters>();
						foreach(var course in courses)
						{
							semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32((int)course * 2 - 1)));
							semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32((int)course * 2)));
						}
						var apRecords = _context.AcademicPlanRecords.Include(apr => apr.KindOfLoad).Where(apr => apr.AcademicPlanId == academicPlan.Id && semesters.Contains(apr.Semester));
						if(apRecords.Count() == 0)
						{
							transaction.Rollback();
							return ResultService.Error("not_found", string.Format("Для одного из учебных планов отсуствуют записи"), 
								ResultServiceStatusCode.NotFound);
						}
						foreach(var apRecord in apRecords)
						{//идем по записям учебного плана
							var timeNorms = _context.TimeNorms.Where(tn => tn.KindOfLoadId == apRecord.KindOfLoadId);
							if(timeNorms.Count() == 0)
							{
								transaction.Rollback();
								return ResultService.Error("not_found", string.Format("Для вида нагрузок {0} отсуствуют нормы времени",
									_context.KindOfLoads.Single(kl => kl.Id == apRecord.KindOfLoadId).KindOfLoadName), ResultServiceStatusCode.NotFound);
							}
							foreach(var timeNorm in timeNorms)
							{//получаем список норм времени, привязанных к виду нагрузки по записи учебного плана.
							 //их может быть от 1 до нескольких, на каждую нужно создать запись
								foreach (var course in courses)
								{//для всех курсов, входящих в учебный план ищем записи по контингенту
									var contingents = _context.Contingents.Include(c => c.StudentGroup).Where(c => c.StudentGroup.Course == course &&
									c.StudentGroup.EducationDirectionId == academicPlan.EducationDirectionId);
									if(contingents.Count() == 0)
									{
										transaction.Rollback();
										return ResultService.Error("not_found", string.Format("Для курса {0} отсуствуют записи по контингенту",
											Math.Log((int)course, 2) + 1), ResultServiceStatusCode.NotFound);
									}
									foreach(var contingent in contingents)
									{//для каждой найденной записи по контингенту, формируем запись по учебной нагрузки
										decimal load = 0;
										switch(apRecord.KindOfLoad.KindOfLoadType)
										{
											case KindOfLoadType.Группа:
												//Расчет времени по группе: берем время, указанное в учебнном плане, умножаем на время, указаное в нормах времени и на 1, так как 1 группа
												load = apRecord.Hours * timeNorm.Hours * 1;
												break;
											case KindOfLoadType.Подгруппа:
												//Расчет времени по группе: берем время, указанное в учебнном плане, умножаем на время, указаное в нормах времени и на количество подгрупп
												load = apRecord.Hours * timeNorm.Hours * contingent.CountSubgroups;
												break;
											case KindOfLoadType.Студенты:
												//Расчет времени по группе: берем время, указанное в учебнном плане, умножаем на время, указаное в нормах времени и на количество студентов
												load = apRecord.Hours * timeNorm.Hours * contingent.CountStudetns;
												break;
										}
										var record = _context.LoadDistributionRecords.FirstOrDefault(ldr => ldr.LoadDistributionId == model.LoadDistributionId.Value
										&& ldr.AcademicPlanRecordId == apRecord.Id 
										&& ldr.TimeNormId == timeNorm.Id
										&& ldr.ContingentId == contingent.Id);
										ResultService result = null;
										if (record == null)
										{
											result = CreateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
											{
												LoadDistributionId = model.LoadDistributionId.Value,
												AcademicPlanRecordId = apRecord.Id,
												TimeNormId = timeNorm.Id,
												ContingentId = contingent.Id,
												Load = load
											});
										}
										else
										{
											result = UpdateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
											{
												LoadDistributionId = model.LoadDistributionId.Value,
												AcademicPlanRecordId = apRecord.Id,
												TimeNormId = timeNorm.Id,
												ContingentId = contingent.Id,
												Load = load
											});
										}
										if(!result.Succeeded)
										{
											transaction.Rollback();
											return result;
										}
									}
								}
							}
						}
					}
					transaction.Commit();
					return ResultService.Success();
				}
				catch (DbEntityValidationException ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}
	}
}
