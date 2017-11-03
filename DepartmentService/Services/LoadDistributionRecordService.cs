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
using System.Text.RegularExpressions;

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
				if (!model.LoadDistributionId.HasValue)
				{
					throw new Exception("Отсутсвует идентификатор рапределения нагрузок");
				}
				return ResultService<List<LoadDistributionRecordViewModel>>.Success(
					ModelFactoryToViewModel.CreateLoadDistributionRecords(_context.LoadDistributionRecords
					.Where(e => e.LoadDistributionId == model.LoadDistributionId.Value)
						.Include(e => e.AcademicPlanRecord).Include(e => e.Contingent).Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.AcademicPlan.EducationDirection)
						.Include(e => e.AcademicPlanRecord.Discipline)
						.Include(e => e.AcademicPlanRecord.Discipline.DisciplineBlock)
						.Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear)
						.Include(e => e.TimeNorm.KindOfLoad)
							.Where(e => !e.IsDeleted))
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
						.Include(e => e.Contingent.AcademicYear)
						.Include(e => e.TimeNorm.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<LoadDistributionRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<LoadDistributionRecordViewModel>.Success(
					ModelFactoryToViewModel.CreateLoadDistributionRecordViewModel(entity));
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

		public ResultService MakeLoadDistribution(LoadDistributionGetBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var academicYearId = _context.LoadDistributions
						.FirstOrDefault(ld => ld.Id == model.Id && !ld.IsDeleted).AcademicYearId;
					var academicPlans = _context.AcademicPlans
						.Where(ap => ap.AcademicYearId == academicYearId &&
									!ap.IsDeleted);
					foreach (var academicPlan in academicPlans)
					{//получаем список учебных планов за нужный учебный год
					 // определяем курсы, к которым относится план
						List<AcademicCourse> courses = GetCourses(academicPlan);
						// если курсов нет, значит пропускаем план
						if (courses.Count == 0)
						{
							continue;
						}

						foreach (var course in courses)
						{
							// получаем семестры на основе курсов
							List<Semesters> semesters = new List<Semesters>();
							int courseInt = (int)Math.Log((double)course, 2) + 1;
							semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2 - 1)));
							semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2)));

							var apRecords = _context.AcademicPlanRecords
							.Include(apr => apr.KindOfLoad)
							.Where(apr => apr.AcademicPlanId == academicPlan.Id &&
										semesters.Contains(apr.Semester) &&
										!apr.IsDeleted);
							if (apRecords.Count() == 0)
							{
								transaction.Rollback();
								return ResultService.Error("not_found", string.Format("Для одного из учебных планов отсуствуют записи"),
									ResultServiceStatusCode.NotFound);

							}

							foreach (var apRecord in apRecords)
							{//идем по записям учебного плана
								var timeNorms = _context.TimeNorms.Where(tn => tn.KindOfLoadId == apRecord.KindOfLoadId);
								if (timeNorms.Count() == 0)
								{
									transaction.Rollback();
									return ResultService.Error("not_found", string.Format("Для вида нагрузок {0} отсуствуют нормы времени",
										_context.KindOfLoads.Single(kl => kl.Id == apRecord.KindOfLoadId).KindOfLoadName), ResultServiceStatusCode.NotFound);
								}
								foreach (var timeNorm in timeNorms)
								{//получаем список норм времени, привязанных к виду нагрузки по записи учебного плана.
								 //их может быть от 1 до нескольких, на каждую нужно создать запись
									decimal load = CalcLoad(timeNorm.Formula, apRecords);
									var contingents = _context.Contingents
										.Where(c => c.Course == course &&
												c.EducationDirectionId == academicPlan.EducationDirectionId &&
												!c.IsDeleted);
									foreach (var contingent in contingents)
									{//для каждой найденной записи по контингенту, формируем запись по учебной нагрузки
									 // если требуется учесть студентов или группу
										var match = Regex.Match(timeNorm.Formula, @"""[\w\ ]+""");
										if (match.Success)
										{
											var type = Regex.Match(match.Value, @"[\w\ ]+").Value;
											switch (type)
											{
												case "Группа":
													//Расчет времени по группе: берем время, указанное в учебнном плане, умножаем на время, указаное в нормах времени и на 1, так как 1 группа
													load *= 1;
													break;
												case "Подгруппа":
													//Расчет времени по группе: берем время, указанное в учебнном плане, умножаем на время, указаное в нормах времени и на количество подгрупп
													load *= contingent.CountSubgroups;
													break;
												case "Студенты":
													//Расчет времени по группе: берем время, указанное в учебнном плане, умножаем на время, указаное в нормах времени и на количество студентов
													load *= contingent.CountStudetns;
													break;
											}
										}
										var record = _context.LoadDistributionRecords
											.FirstOrDefault(ldr => ldr.LoadDistributionId == model.Id &&
													ldr.AcademicPlanRecordId == apRecord.Id &&
													ldr.TimeNormId == timeNorm.Id &&
													ldr.ContingentId == contingent.Id &&
													!ldr.IsDeleted);
										if (record == null)
										{
											_context.LoadDistributionRecords.Add(new LoadDistributionRecord
											{
												AcademicPlanRecordId = apRecord.Id,
												ContingentId = contingent.Id,
												LoadDistributionId = model.Id,
												TimeNormId = timeNorm.Id,
												Load = load,
												DateCreate = DateTime.Now,
												IsDeleted = false
											});
										}
										else
										{
											record.Load = load;
											_context.Entry(record).State = EntityState.Modified;
										}
										_context.SaveChanges();
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

		private List<AcademicCourse> GetCourses(AcademicPlan academicPlan)
		{
			List<AcademicCourse> courses = new List<AcademicCourse>();
			if ((academicPlan.AcademicCourses & AcademicCourse.Course_1) == AcademicCourse.Course_1)
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

			return courses;
		}

		private decimal CalcLoad(string formula, IQueryable<AcademicPlanRecord> apRecords)
		{
			decimal load = 0;
			// ищем схему: [<Название вида нагрузки>]<*><число>*"поток/группа/студенты"
			var match = Regex.Match(formula, @"\*[\d\,]+\*");
			// для начала получаем число
			if (match.Success)
			{
				load = Convert.ToDecimal(Regex.Match(match.Value, @"[\d\,]+").Value);
			}
			match = Regex.Match(formula, @"\[[\w\ ]+\]");
			if (match.Success)
			{// среди записей по этой дисциплине ищем вид нагрузки, если он там есть
				var kindOfLoadName = Regex.Match(match.Value, @"[\w\ ]+").Value;
				var apR = apRecords.FirstOrDefault(kol => kol.KindOfLoad.KindOfLoadName.Contains(kindOfLoadName));
				if (apR != null)
				{
					load *= apR.Hours;
				}
			}

			return load;
		}
	}
}
