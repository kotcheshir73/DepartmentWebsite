using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using System.Data.Entity.Validation;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using System.Xml;

namespace DepartmentService.Services
{
	public class AcademicPlanRecordService : IAcademicPlanRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicPlanService _serviceAP;

		private readonly IDisciplineService _serviceD;

		private readonly IKindOfLoadService _serviceKL;

		public AcademicPlanRecordService(DepartmentDbContext context, IAcademicPlanService serviceAP,
			IDisciplineService serviceD, IKindOfLoadService serviceKL)
		{
			_context = context;
			_serviceAP = serviceAP;
			_serviceD = serviceD;
			_serviceKL = serviceKL;
		}

		public ResultService<List<AcademicPlanRecordViewModel>> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
		{
			try
			{
				if (model.AcademicPlanId.HasValue)
				{
					return ResultService<List<AcademicPlanRecordViewModel>>.Success(
						ModelFactory.CreateAcademicPlanRecords(_context.AcademicPlanRecords
							.Include(ar => ar.Discipline).Include(ar => ar.KindOfLoad)
								.Where(e => e.AcademicPlanId == model.AcademicPlanId.Value && !e.IsDeleted))
						.ToList());
				}
				throw new Exception("Не указан учебный план");
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<AcademicPlanRecordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<AcademicPlanRecordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<AcademicPlanViewModel>> GetAcademicPlans()
		{
			return _serviceAP.GetAcademicPlans();
		}

		public ResultService<List<DisciplineViewModel>> GetDisciplines()
		{
			return _serviceD.GetDisciplines();
		}

		public ResultService<List<KindOfLoadViewModel>> GetKindOfLoads()
		{
			return _serviceKL.GetKindOfLoads();
		}

		public ResultService LoadFromXMLAcademicPlanRecord(AcademicPlanRecordLoadFromXMLBindingModel model)
		{
			try
			{
				XmlDocument newXmlDocument = new XmlDocument();
				newXmlDocument.Load(new XmlTextReader(model.FileName));
				XmlNode mainRootElementNode = newXmlDocument.SelectSingleNode("/Документ/План/СтрокиПлана");
				if (mainRootElementNode != null)
				{//получаем перечень дисциплин по учебному плану
					XmlNodeList elementsMainNode = mainRootElementNode.SelectNodes("Строка");
					if (elementsMainNode != null)
					{
						foreach (XmlNode elementNode in elementsMainNode)
						{//получаем информацию по каждой дисциплине
							DisciplineRecordBindingModel disciplneModel = new DisciplineRecordBindingModel();
							XmlAttributeCollection elementNodeAttributes = elementNode.Attributes;
							if (elementNodeAttributes != null)
							{
								foreach (XmlAttribute elementNodeAttribute in elementNodeAttributes)
								{
									switch (elementNodeAttribute.Name)
									{
										case "Дис"://Получаем название дисциплины
											disciplneModel.DisciplineName = elementNodeAttribute.Value;
											break;
									}
								}
							}
							else
							{
								//TODO сделать обработку ошибки - нет аргументов
								continue;
							}
							//ищем дисциплину или создаем новую
							XmlNodeList elementSemNodes = elementNode.SelectNodes("Сем");
							if (elementsMainNode != null)
							{//получаем перечень семестров, в которые проводится дисциплина
								foreach (XmlNode elementSemNode in elementSemNodes)
								{
									// <VZ ID="101" H="16" IntH="2" />
									//< VZ ID = "103" H = "16" IntH = "10" />
									//< VZ ID = "107" H = "22" />
									//101 - где лучше задать эти кода, в видах нагрузке прописать? Кто их будет знать? Где их можно посмотреть?
								}
							}
							else
							{
								//TODO сделать обработку ошибки - нет семестров
								continue;
							}
						}
						return ResultService.Success();
					}
					throw new Exception("Неверная структура xml. Не найден элемент /СтрокиПлана");
				}
				throw new Exception("Неверная структура xml. Не найден элемент /Документ/План/СтрокиПлана");
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<AcademicPlanRecordViewModel> GetAcademicPlanRecord(AcademicPlanRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.AcademicPlanRecords.Include(ar => ar.Discipline).Include(ar => ar.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<AcademicPlanRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<AcademicPlanRecordViewModel>.Success(
					ModelFactory.CreateAcademicPlanRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicPlanRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AcademicPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model)
		{
			var entity = new AcademicPlanRecord
			{
				AcademicPlanId = model.AcademicPlanId,
				DateCreate = DateTime.Now,
				DisciplineId = model.DisciplineId,
				KindOfLoadId = model.KindOfLoadId,
				Semester = (Semesters)Enum.ToObject(typeof(Semesters), model.Semester),
				Hours = model.Hours,
				IsDeleted = false
			};
			try
			{
				_context.AcademicPlanRecords.Add(entity);
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

		public ResultService UpdateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model)
		{
			try
			{
				var entity = _context.AcademicPlanRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.Semester = (Semesters)Enum.ToObject(typeof(Semesters), model.Semester);
				entity.Hours = model.Hours;

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

		public ResultService DeleteAcademicPlanRecord(AcademicPlanRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.AcademicPlanRecords
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
	}
}
