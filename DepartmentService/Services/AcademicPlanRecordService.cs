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
				DisciplineId = model.DisciplineId,
				KindOfLoadId = model.KindOfLoadId,
				Semester = (Semesters)Enum.ToObject(typeof(Semesters), model.Semester),
				Hours = model.Hours,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.AcademicPlanRecords.Add(entity);
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
				entity.DisciplineId = model.DisciplineId;
				entity.KindOfLoadId = model.KindOfLoadId;
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


		public ResultService LoadFromXMLAcademicPlanRecord(AcademicPlanRecordLoadFromXMLBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				ResultService result = new ResultService();
				try
				{
					var academicPlan = _context.AcademicPlans.FirstOrDefault(ap => ap.Id == model.Id);
					if (academicPlan == null)
					{
						return ResultService.Error("Error:", "Учебный план not found",
							ResultServiceStatusCode.NotFound);
					}
					// Получаем список курсов, в которых этот план действует
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
					if (courses.Count == 0)
					{
						return ResultService.Error("Error:", "Semesters not found",
							ResultServiceStatusCode.NotFound);
					}
					// получаем список семестров из курсов (будем из xml дергать только те записи, у которых семестр попадает в этот диапазон)
					List<Semesters> semesters = new List<Semesters>();
					foreach (var course in courses)
					{
						int courseInt = (int)Math.Log((double)course, 2) + 1;
						semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2 - 1)));
						semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2)));
					}
					//Получаем номер кафедры
					var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Кафедра");
					if (currentSetting == null)
					{
						return ResultService.Error("Error:", "CurrentSetting not found",
							ResultServiceStatusCode.NotFound);
					}
					var settingDisciplineBlockModules = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
					if (settingDisciplineBlockModules == null)
					{
						return ResultService.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
							ResultServiceStatusCode.NotFound);
					}
					var disciplineBlockModuls = _context.DisciplineBlocks.FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockModules.Value));
					if (disciplineBlockModuls == null)
					{
						return ResultService.Error("Error:", "disciplineBlock(Дисциплины (модули)) not found",
							ResultServiceStatusCode.NotFound);
					}
					XmlDocument newXmlDocument = new XmlDocument();
					newXmlDocument.Load(new XmlTextReader(model.FileName));
					XmlNode mainRootElementNode = newXmlDocument.SelectSingleNode("/Документ/План/СтрокиПлана");
					int counter = 0;
					if (mainRootElementNode != null)
					{
						#region получаем перечень дисциплин по учебному плану
						XmlNodeList elementsMainNode = mainRootElementNode.SelectNodes("Строка");
						if (elementsMainNode != null)
						{
							foreach (XmlNode elementNode in elementsMainNode)
							{//получаем информацию по каждой дисциплине
								counter++;
								//дисциплина
								XmlNode disciplineAttributes = elementNode.Attributes.GetNamedItem("Дис");
								if (disciplineAttributes == null)
								{
									result.AddError("Not_Found", string.Format("Дисциплина не найдена. Строка {0}", counter));
									continue;
								}
								//кафедра
								XmlNode kafedraNode = elementNode.Attributes.GetNamedItem("Кафедра");
								if (kafedraNode == null)
								{
									result.AddError("Not_Found", string.Format("Кафедра не найдена. Дисциплина {0}",
										disciplineAttributes.Value));
									continue;
								}
								if (kafedraNode.Value != currentSetting.Value)
								{//не наша кафедра
									continue;
								}
								//ищем дисцилпину, если не находим, создаем							
								var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
																disciplineAttributes.Value);
								if (discipline == null)
								{
									_context.Disciplines.Add(new Discipline
									{
										DisciplineName = disciplineAttributes.Value,
										DisciplineBlockId = disciplineBlockModuls.Id,
										DateCreate = DateTime.Now,
										IsDeleted = false
									});
									_context.SaveChanges();

									discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
																disciplineAttributes.Value);
								}
								//семестры
								XmlNodeList elementSemNodes = elementNode.SelectNodes("Сем");
								if (elementsMainNode != null)
								{//получаем перечень семестров, в которые проводится дисциплина
									foreach (XmlNode elementSemNode in elementSemNodes)
									{//идем по семестрам (тегам "Сем"), смотрим их аттрибуты
										XmlAttributeCollection elementSemNodeAttributes = elementSemNode.Attributes;
										if (elementSemNodeAttributes != null)
										{
											XmlNode semNode = elementSemNodeAttributes.GetNamedItem("Ном");
											if (semNode == null)
											{
												result.AddError("Not_Found", string.Format("Семестр не найден. Строка {0}", counter));
												continue;
											}
											Semesters sem = (Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(semNode.Value));
											if (semesters.Contains(sem))
											{
												#region Записи по видам нагрузок
												foreach (XmlAttribute elementSemNodeAttribute in elementSemNodeAttributes)
												{
													KindOfLoad kindOfLoad = null;
													switch (elementSemNodeAttribute.Name)
													{
														case "Лек"://Лекции
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																  kl.KindOfLoadName.Contains("Лекц"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Лекция' не найден"));
																}
															}
															break;
														case "Пр"://Практика
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Практ"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Практика' не найден"));
																}
															}
															break;
														case "Лаб"://Лабораторные
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Лаборатор"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Лабораторные' не найден"));
																}
															}
															break;
														case "КР"://Курсовая работа
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Курсовая работа"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Курсовая работа' не найден"));
																}
															}
															break;
														case "КП"://Курсовой проект
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Курсовой проект"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Курсовой проект' не найден"));
																}
															}
															break;
														case "Реф"://Реферат
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Реферат"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Реферат' не найден"));
																}
															}
															break;
														case "РГР"://РГР
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("РГР"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'РГР' не найден"));
																}
															}
															break;
														case "Зач"://Зачет
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Зачет"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Зачет' не найден"));
																}
															}
															break;
														case "ЗачО"://Зачет с оценкой
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Зачет с оценкой"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Зачет с оценкой' не найден"));
																}
															}
															break;
														case "Экз"://Экзамен
															{
																kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
																kl.KindOfLoadName.Contains("Экзамен"));
																if (kindOfLoad == null)
																{
																	result.AddError("Not_Found", string.Format("Вид нагрузки 'Экзамен' не найден"));
																}
															}
															break;
													}
													if (kindOfLoad != null)
													{
														var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
															apr.AcademicPlanId == model.Id &&
															apr.DisciplineId == discipline.Id &&
															apr.KindOfLoadId == kindOfLoad.Id &&
															apr.Semester == sem &&
															!apr.IsDeleted);
														if (record == null)
														{
															_context.AcademicPlanRecords.Add(new AcademicPlanRecord
															{
																AcademicPlanId = model.Id,
																DisciplineId = discipline.Id,
																KindOfLoadId = kindOfLoad.Id,
																Semester = sem,
																Hours = Convert.ToInt32(elementSemNodeAttribute.Value),
																DateCreate = DateTime.Now,
																IsDeleted = false
															});
														}
														else
														{
															record.Hours = Convert.ToInt32(elementSemNodeAttribute.Value);
															_context.Entry(record).State = EntityState.Modified;
														}
														_context.SaveChanges();
													}
												}
												#endregion
											}
										}
									}
								}
								else
								{
									result.AddError("Not_found", string.Format("Семестры не найдены в строке {0}", counter));
									continue;
								}
							}
						}
						else
						{
							throw new Exception("Неверная структура xml. Не найден элемент /СтрокиПлана");
						}
						#endregion
					}
					else
					{
						throw new Exception("Неверная структура xml. Не найден элемент /Документ/План/СтрокиПлана");
					}
					mainRootElementNode = newXmlDocument.SelectSingleNode("/Документ/План/СпецВидыРаботНов");
					if (mainRootElementNode != null)
					{
						#region Практики
						var settingDisciplineBlockPractic = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Практика");
						if (settingDisciplineBlockPractic == null)
						{
							return ResultService.Error("Error:", "В настройках не указан disciplineBlock(Практика)",
								ResultServiceStatusCode.NotFound);
						}
						var disciplineBlockPractic = _context.DisciplineBlocks.FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockPractic.Value));
						if (disciplineBlockPractic == null)
						{
							return ResultService.Error("Error:", "disciplineBlock(Практика) not found",
								ResultServiceStatusCode.NotFound);
						}
						XmlNode studyPracticNode = mainRootElementNode.SelectSingleNode("УчебПрактики");
						if (studyPracticNode != null)
						{
							XmlNode practicNode = studyPracticNode.SelectSingleNode("ПрочаяПрактика");
							parsePractic(new parsePracticBindingModel
							{
								PracticName = "Учебная практика",
								Result = result,
								Node = practicNode,
								Counter = counter,
								KafedraNumber = currentSetting.Value,
								ApId = model.Id,
								DisciplineBlockId = disciplineBlockPractic.Id,
								Semesters = semesters
							});
						}
						XmlNode practicsNode = mainRootElementNode.SelectSingleNode("ПрочиеПрактики");
						if (practicsNode != null)
						{
							XmlNodeList practicsNodes = practicsNode.SelectNodes("ПрочаяПрактика");
							foreach (XmlNode practicNode in practicsNodes)
							{
								parsePractic(new parsePracticBindingModel
								{
									PracticName = "Производственная практика",
									Result = result,
									Node = practicNode,
									Counter = counter,
									KafedraNumber = currentSetting.Value,
									ApId = model.Id,
									DisciplineBlockId = disciplineBlockPractic.Id,
									Semesters = semesters
								});
							}
						}
						#endregion
						if (semesters.Contains(Semesters.Восьмой))
						{
							#region ГЭК и ГАК
							var settingDisciplineBlockGIA = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "ГИА");
							if (settingDisciplineBlockGIA == null)
							{
								return ResultService.Error("Error:", "В настройках не указан disciplineBlock(ГИА)",
									ResultServiceStatusCode.NotFound);
							}
							var disciplineBlockGIA = _context.DisciplineBlocks.FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockGIA.Value));
							if (disciplineBlockGIA == null)
							{
								return ResultService.Error("Error:", "disciplineBlock(ГИА) not found",
									ResultServiceStatusCode.NotFound);
							}
							string textAcademicLevel = "";
							int semNumber = 0;
							switch (academicPlan.AcademicLevel)
							{
								case AcademicLevel.Бакалавриат:
									textAcademicLevel = "бакалавра";
									semNumber = 8;
									break;
								case AcademicLevel.Магистратура:
									textAcademicLevel = "магистра";
									semNumber = 4;
									break;
							}
							#region ГАК
							XmlNode vkrNode = mainRootElementNode.SelectSingleNode("ВКР");
							if (vkrNode != null)
							{
								string[] disciplineNames = new string[]
								{
									string.Format("Руководство ВКР {0}", textAcademicLevel),
									string.Format("Работа в ГЭК (защита ВКР {0})", textAcademicLevel),
									"Нормоконтроль на тех. направлениях"
								};
								foreach (var discpName in disciplineNames)
								{
									var kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													  kl.KindOfLoadName.Contains(discpName));
									if (kindOfLoad != null)
									{
										var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
										if (discipline == null)
										{
											_context.Disciplines.Add(new Discipline
											{
												DisciplineBlockId = disciplineBlockGIA.Id,
												DisciplineName = discpName,
												DateCreate = DateTime.Now,
												IsDeleted = false
											});
											_context.SaveChanges();

											discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
										}

										var sem = (Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(semNumber));
										var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
																					apr.AcademicPlanId == model.Id &&
																					apr.DisciplineId == discipline.Id &&
																					apr.KindOfLoadId == kindOfLoad.Id &&
																					apr.Semester == sem &&
																					!apr.IsDeleted);
										if (record == null)
										{
											_context.AcademicPlanRecords.Add(new AcademicPlanRecord
											{
												AcademicPlanId = model.Id,
												DisciplineId = discipline.Id,
												KindOfLoadId = kindOfLoad.Id,
												Semester = sem,
												Hours = 1,
												DateCreate = DateTime.Now,
												IsDeleted = false
											});
										}
										else
										{
											record.Hours = 1;
											_context.Entry(record).State = EntityState.Modified;
										}
										_context.SaveChanges();

									}
								}
							}
							#endregion
							#region ГЭК
							XmlNode examenNode = mainRootElementNode.SelectSingleNode("ИтоговыйЭкзамен");
							if (examenNode != null)
							{
								string[] disciplineNames = new string[]
								{
									string.Format("Гос.экзамен {0}", textAcademicLevel),
									string.Format("Гос.экзамен {0} (консультации)", textAcademicLevel)
								};
								foreach (var discpName in disciplineNames)
								{
									var kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
												  kl.KindOfLoadName.Contains(discpName));
									if (kindOfLoad != null)
									{
										var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
										if (discipline == null)
										{
											_context.Disciplines.Add(new Discipline
											{
												DisciplineBlockId = disciplineBlockGIA.Id,
												DisciplineName = discpName,
												DateCreate = DateTime.Now,
												IsDeleted = false
											});
											_context.SaveChanges();

											discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
										}
										var sem = (Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(semNumber));
										var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
																					apr.AcademicPlanId == model.Id &&
																					apr.DisciplineId == discipline.Id &&
																					apr.KindOfLoadId == kindOfLoad.Id &&
																					apr.Semester == sem &&
																					!apr.IsDeleted);
										if (record == null)
										{
											_context.AcademicPlanRecords.Add(new AcademicPlanRecord
											{
												AcademicPlanId = model.Id,
												DisciplineId = discipline.Id,
												KindOfLoadId = kindOfLoad.Id,
												Semester = sem,
												Hours = 1,
												DateCreate = DateTime.Now,
												IsDeleted = false
											});
										}
										else
										{
											record.Hours = 1;
											_context.Entry(record).State = EntityState.Modified;
										}
										_context.SaveChanges();
									}
								}
							}
							#endregion
							#endregion
						}
					}
					else
					{
						throw new Exception("Неверная структура xml. Не найден элемент /Документ/План/СпецВидыРаботНов");
					}
					transaction.Commit();
					return result;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}

		private void parsePractic(parsePracticBindingModel model)
		{
			var kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
											  kl.KindOfLoadName.Contains(model.PracticName));
			if (kindOfLoad == null)
			{
				model.Result.AddError("Not_Found", string.Format("Нагрузка под практику не найдена"));
				return;
			}
			XmlNode disciplineAttributes = model.Node.Attributes.GetNamedItem("Наименование");
			if (disciplineAttributes == null)
			{
				model.Result.AddError("Not_Found", string.Format("Наименование практики не найдено. Строка {0}", model.Counter));
				return;
			}
			//кафедра
			XmlNode kafedraNode = model.Node.Attributes.GetNamedItem("Кафедра");
			if (kafedraNode == null)
			{
				model.Result.AddError("Not_Found", string.Format("Кафедра не найдена. Практика {0}",
					disciplineAttributes.Value));
				return;
			}
			if (kafedraNode.Value == model.KafedraNumber)
			{//наша кафедра
			 //ищем дисцилпину, если не находим, создаем							
				var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
												disciplineAttributes.Value);
				if (discipline == null)
				{
					_context.Disciplines.Add(new Discipline
					{
						DisciplineName = disciplineAttributes.Value,
						DisciplineBlockId = model.DisciplineBlockId,
						DateCreate = DateTime.Now,
						IsDeleted = false
					});
					_context.SaveChanges();

					discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
												disciplineAttributes.Value);
				}

				if (discipline.DisciplineName == "Преддипломная практика")
				{
					kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
							  kl.KindOfLoadName.Contains(discipline.DisciplineName));
					if (kindOfLoad == null)
					{
						model.Result.AddError("Not_Found", string.Format("Практика не найдена. Строка {0}", model.Counter));
						return;
					}
				}

				XmlNode semesterNode = model.Node.SelectSingleNode("Семестр");
				if (semesterNode == null)
				{
					model.Result.AddError("Not_Found", string.Format("Не найден тег семестр. Практика {0}",
						disciplineAttributes.Value));
					return;
				}
				XmlNode semNode = semesterNode.Attributes.GetNamedItem("Ном");
				if (semNode == null)
				{
					model.Result.AddError("Not_Found", string.Format("Не найден номер семестра. Практика {0}",
						disciplineAttributes.Value));
					return;
				}
				var sem = (Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(semNode.Value));
				if (model.Semesters.Contains(sem))
				{
					XmlNode weekNumNode = semesterNode.Attributes.GetNamedItem("ПланНед");
					if (weekNumNode == null)
					{
						model.Result.AddError("Not_Found", string.Format("Не найдено количество недель. Практика {0}",
							disciplineAttributes.Value));
						return;
					}
					var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
															apr.AcademicPlanId == model.ApId &&
															apr.DisciplineId == discipline.Id &&
															apr.KindOfLoadId == kindOfLoad.Id &&
															apr.Semester == sem &&
															!apr.IsDeleted);
					if (record == null)
					{
						_context.AcademicPlanRecords.Add(new AcademicPlanRecord
						{
							AcademicPlanId = model.ApId,
							DisciplineId = discipline.Id,
							KindOfLoadId = kindOfLoad.Id,
							Semester = sem,
							Hours = Convert.ToInt32(weekNumNode.Value),
							DateCreate = DateTime.Now,
							IsDeleted = false
						});
					}
					else
					{
						record.Hours = Convert.ToInt32(weekNumNode.Value);
						_context.Entry(record).State = EntityState.Modified;
					}
					_context.SaveChanges();
				}
			}
		}
	}

	class parsePracticBindingModel
	{
		public string PracticName { get; set; }

		public ResultService Result { get; set; }

		public XmlNode Node { get; set; }

		public int Counter { get; set; }

		public string KafedraNumber { get; set; }

		public long ApId { get; set; }

		public long DisciplineBlockId { get; set; }

		public List<Semesters> Semesters { get; set; }
	}
}
