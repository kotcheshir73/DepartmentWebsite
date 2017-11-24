﻿using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace DepartmentService.Services
{
	public class EducationalProcessService : IEducationalProcessService
	{
		private readonly DepartmentDbContext _context;

		public EducationalProcessService(DepartmentDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Получение списка курсов, которые есть в учбеном плане
		/// </summary>
		/// <param name="academicPlan"></param>
		/// <returns></returns>
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

		#region Загрузка записей учебного плана из xml
		/// <summary>
		/// Парсинг учебных планов из xml
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public ResultService LoadFromXMLAcademicPlanRecord(EducationalProcessLoadFromXMLBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				ResultService result = new ResultService();
				try
				{
					var academicPlan = _context.AcademicPlans
						.FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
					if (academicPlan == null)
					{
						return ResultService.Error("Error:", "Учебный план not found",
							ResultServiceStatusCode.NotFound);
					}
					// Получаем список курсов, в которых этот план действует
					List<AcademicCourse> courses = GetCourses(academicPlan);
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
					var currentSetting = _context.CurrentSettings
						.FirstOrDefault(cs => cs.Key == "Кафедра");
					if (currentSetting == null)
					{
						return ResultService.Error("Error:", "CurrentSetting not found",
							ResultServiceStatusCode.NotFound);
					}
					var settingDisciplineBlockModules = _context.CurrentSettings
						.FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
					if (settingDisciplineBlockModules == null)
					{
						return ResultService.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
							ResultServiceStatusCode.NotFound);
					}
					var disciplineBlockModuls = _context.DisciplineBlocks
						.FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockModules.Value));
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
						// парсим дисциплины из xml
						ParseDisicpline(new ParseDisciplineBindingModel
						{
							AcademicPlanId = model.Id,
							Counter = counter,
							DisciplineBlockId = disciplineBlockModuls.Id,
							KafedraNumber = currentSetting.Value,
							Node = mainRootElementNode,
							Result = result,
							Semesters = semesters
						});
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
							ParsePractic(new ParsePracticBindingModel
							{
								PracticName = "Учебная практика",
								Result = result,
								Node = practicNode,
								Counter = counter,
								KafedraNumber = currentSetting.Value,
								AcademicPlanId = model.Id,
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
								ParsePractic(new ParsePracticBindingModel
								{
									PracticName = "Производственная практика",
									Result = result,
									Node = practicNode,
									Counter = counter,
									KafedraNumber = currentSetting.Value,
									AcademicPlanId = model.Id,
									DisciplineBlockId = disciplineBlockPractic.Id,
									Semesters = semesters
								});
							}
						}
						#endregion
						if ((semesters.Contains(Semesters.Восьмой) &&
									academicPlan.AcademicLevel == AcademicLevel.Бакалавриат) ||
							(semesters.Contains(Semesters.Четвертый) &&
									academicPlan.AcademicLevel == AcademicLevel.Магистратура))
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
							ParseFinal(new ParseFinalBindingModel
							{
								AcademicPlanId = model.Id,
								AcademicLevel = textAcademicLevel,
								DisciplineBlockId = disciplineBlockGIA.Id,
								Node = mainRootElementNode,
								SemesterNumber = semNumber
							});
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

		/// <summary>
		/// Парсим дисциплины из xml
		/// </summary>
		/// <param name="model"></param>
		private void ParseDisicpline(ParseDisciplineBindingModel model)
		{
			// Извлекаем все nodes с названием Строка - это записи по дисциплинам
			XmlNodeList elementsMainNode = model.Node.SelectNodes("Строка");
			if (elementsMainNode != null)
			{
				foreach (XmlNode elementNode in elementsMainNode)
				{//получаем информацию по каждой дисциплине
					model.Counter++;
					//ищем название дисциплины
					XmlNode disciplineAttributes = elementNode.Attributes.GetNamedItem("Дис");
					if (disciplineAttributes == null)
					{
						model.Result.AddError("Not_Found", string.Format("Дисциплина не найдена. Строка {0}", model.Counter));
						continue;
					}
					//ищем номер кафедрв
					XmlNode kafedraNode = elementNode.Attributes.GetNamedItem("Кафедра");
					if (kafedraNode == null)
					{
						model.Result.AddError("Not_Found", string.Format("Кафедра не найдена. Дисциплина {0}",
							disciplineAttributes.Value));
						continue;
					}
					if (kafedraNode.Value != model.KafedraNumber)
					{//не наша кафедра, пропускаем запись
						continue;
					}
					//ищем дисцилпину, если не находим, создаем							
					var discipline = _context.Disciplines
						.FirstOrDefault(d => d.DisciplineName == disciplineAttributes.Value &&
										!d.IsDeleted);
					if (discipline == null)
					{
						_context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineRecordBindingModel
						{
							DisciplineName = disciplineAttributes.Value,
							DisciplineBlockId = model.DisciplineBlockId
						}));
						_context.SaveChanges();

						discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
													disciplineAttributes.Value);
					}
					//получем nodes по семестрам, в которых проводится дисциплина
					XmlNodeList elementSemNodes = elementNode.SelectNodes("Сем");
					if (elementsMainNode != null)
					{
						foreach (XmlNode elementSemNode in elementSemNodes)
						{//идем по семестрам (тегам "Сем"), смотрим их атрибуты
							XmlAttributeCollection elementSemNodeAttributes = elementSemNode.Attributes;
							if (elementSemNodeAttributes != null)
							{
								// извлекаем номер семестра
								XmlNode semNode = elementSemNodeAttributes.GetNamedItem("Ном");
								if (semNode == null)
								{
									model.Result.AddError("Not_Found", string.Format("Семестр не найден. Строка {0}", model.Counter));
									continue;
								}
								Semesters sem = (Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(semNode.Value));
								if (model.Semesters.Contains(sem))
								{
									#region Записи по видам нагрузок
									// извлекаем все записи по нагрузкам, которые фигурируют по дисиуцплине в этом семестра
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
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Лекция' не найден"));
													}
												}
												break;
											case "Пр"://Практика
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Практ"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Практика' не найден"));
													}
												}
												break;
											case "Лаб"://Лабораторные
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Лаборатор"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Лабораторные' не найден"));
													}
												}
												break;
											case "КР"://Курсовая работа
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Курсовая работа"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Курсовая работа' не найден"));
													}
												}
												break;
											case "КП"://Курсовой проект
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Курсовой проект"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Курсовой проект' не найден"));
													}
												}
												break;
											case "Реф"://Реферат
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Реферат"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Реферат' не найден"));
													}
												}
												break;
											case "РГР"://РГР
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("РГР"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'РГР' не найден"));
													}
												}
												break;
											case "Зач"://Зачет
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Зачет"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Зачет' не найден"));
													}
												}
												break;
											case "ЗачО"://Зачет с оценкой
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Зачет с оценкой"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Зачет с оценкой' не найден"));
													}
												}
												break;
											case "Экз"://Экзамен
												{
													kindOfLoad = _context.KindOfLoads.FirstOrDefault(kl =>
													kl.KindOfLoadName.Contains("Экзамен"));
													if (kindOfLoad == null)
													{
														model.Result.AddError("Not_Found", string.Format("Вид нагрузки 'Экзамен' не найден"));
													}
												}
												break;
										}
										// если нашли один из видов нагрузки - создаем запись
										if (kindOfLoad != null)
										{
											var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
												apr.AcademicPlanId == model.AcademicPlanId &&
												apr.DisciplineId == discipline.Id &&
												apr.KindOfLoadId == kindOfLoad.Id &&
												apr.Semester == sem &&
												!apr.IsDeleted);
											if (record == null)
											{
												_context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
												{
													AcademicPlanId = model.AcademicPlanId,
													DisciplineId = discipline.Id,
													KindOfLoadId = kindOfLoad.Id,
													Semester = sem.ToString(),
													Hours = Convert.ToInt32(elementSemNodeAttribute.Value)
												}));
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
						model.Result.AddError("Not_found", string.Format("Семестры не найдены в строке {0}",
							model.Counter));
						continue;
					}
				}
			}
			else
			{
				throw new Exception("Неверная структура xml. Не найден элемент /СтрокиПлана");
			}
		}

		/// <summary>
		/// Парсим практики из xml
		/// </summary>
		/// <param name="model"></param>
		private void ParsePractic(ParsePracticBindingModel model)
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
					_context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineRecordBindingModel
					{
						DisciplineName = disciplineAttributes.Value,
						DisciplineBlockId = model.DisciplineBlockId
					}));
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
															apr.AcademicPlanId == model.AcademicPlanId &&
															apr.DisciplineId == discipline.Id &&
															apr.KindOfLoadId == kindOfLoad.Id &&
															apr.Semester == sem &&
															!apr.IsDeleted);
					if (record == null)
					{
						_context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
						{
							AcademicPlanId = model.AcademicPlanId,
							DisciplineId = discipline.Id,
							KindOfLoadId = kindOfLoad.Id,
							Semester = sem.ToString(),
							Hours = Convert.ToInt32(weekNumNode.Value)
						}));
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

		/// <summary>
		/// Парсим госэкзамен и защиты из xml
		/// </summary>
		/// <param name="model"></param>
		private void ParseFinal(ParseFinalBindingModel model)
		{
			#region ГАК
			XmlNode vkrNode = model.Node.SelectSingleNode("ВКР");
			if (vkrNode != null)
			{
				string[] disciplineNames = new string[]
				{
									string.Format("Руководство ВКР {0}", model.AcademicLevel),
									string.Format("Работа в ГЭК (защита ВКР {0})", model.AcademicLevel),
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
							_context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineRecordBindingModel
							{
								DisciplineBlockId = model.DisciplineBlockId,
								DisciplineName = discpName
							}));
							_context.SaveChanges();

							discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
						}

						var sem = (Semesters)Enum.ToObject(typeof(Semesters), model.SemesterNumber);
						var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
																	apr.AcademicPlanId == model.AcademicPlanId &&
																	apr.DisciplineId == discipline.Id &&
																	apr.KindOfLoadId == kindOfLoad.Id &&
																	apr.Semester == sem &&
																	!apr.IsDeleted);
						if (record == null)
						{
							_context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
							{
								AcademicPlanId = model.AcademicPlanId,
								DisciplineId = discipline.Id,
								KindOfLoadId = kindOfLoad.Id,
								Semester = sem.ToString(),
								Hours = 1
							}));
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
			XmlNode examenNode = model.Node.SelectSingleNode("ИтоговыйЭкзамен");
			if (examenNode != null)
			{
				string[] disciplineNames = new string[]
				{
									string.Format("Гос.экзамен {0}", model.AcademicLevel),
									string.Format("Гос.экзамен {0} (консультации)", model.AcademicLevel)
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
							_context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineRecordBindingModel
							{
								DisciplineBlockId = model.DisciplineBlockId,
								DisciplineName = discpName
							}));
							_context.SaveChanges();

							discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
						}
						var sem = (Semesters)Enum.ToObject(typeof(Semesters), model.SemesterNumber);
						var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
																	apr.AcademicPlanId == model.AcademicPlanId &&
																	apr.DisciplineId == discipline.Id &&
																	apr.KindOfLoadId == kindOfLoad.Id &&
																	apr.Semester == sem &&
																	!apr.IsDeleted);
						if (record == null)
						{
							_context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
							{
								AcademicPlanId = model.AcademicPlanId,
								DisciplineId = discipline.Id,
								KindOfLoadId = kindOfLoad.Id,
								Semester = sem.ToString(),
								Hours = 1
							}));
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
		}
		#endregion

		#region Формирование/перерасчет учебной нагрузки на год
		/// <summary>
		/// Формирование/перерасчет учебной нагрузки на год
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
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
											_context.LoadDistributionRecords.Add(ModelFacotryFromBindingModel.CreateLoadDistributionRecord(new LoadDistributionRecordRecordBindingModel
											{
												AcademicPlanRecordId = apRecord.Id,
												ContingentId = contingent.Id,
												LoadDistributionId = model.Id.Value,
												TimeNormId = timeNorm.Id,
												Load = load
											}));
										}
										else
										{
											record.Load = load;
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

		/// <summary>
		/// Вычисление нагрузки
		/// </summary>
		/// <param name="formula"></param>
		/// <param name="apRecords"></param>
		/// <returns></returns>
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
		#endregion
	}
}
