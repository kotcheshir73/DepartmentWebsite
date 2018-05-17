﻿using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentModel.Models.HelperModels;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.Enums;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Xml;

namespace DepartmentService.Services
{
    public class EducationalProcessService : IEducationalProcessService
    {
        private readonly DepartmentDbContext _context;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        public EducationalProcessService(DepartmentDbContext context,
            ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            _context = context;
            _serviceSR = serviceSR;
            _serviceOR = serviceOR;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
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
                        int magistersCorrect = academicPlan.AcademicLevel == AcademicLevel.Магистратура ? 4 : 0;
                        int courseInt = (int)Math.Log((double)course, 2) + 1 - magistersCorrect;
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
                        XmlNodeList practicsNodes = mainRootElementNode.ChildNodes;
                        foreach (XmlNode practicElemNode in practicsNodes)
                        {
                            if (practicElemNode.Name.Contains("Практик"))
                            {
                                XmlNode practicNode = practicElemNode.SelectSingleNode("ПрочаяПрактика");
                                ParsePractic(new ParseDisciplineBindingModel
                                {
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
                        CreateAPR(elementSemNodes, model, discipline.Id);
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
        private void ParsePractic(ParseDisciplineBindingModel model)
        {
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

                XmlNodeList semesterNode = model.Node.SelectNodes("Семестр");
                if (semesterNode == null)
                {
                    model.Result.AddError("Not_Found", string.Format("Не найден тег семестр. Практика {0}",
                        disciplineAttributes.Value));
                    return;
                }

                CreateAPR(semesterNode, model, discipline.Id);
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
                Dictionary<string, string> disciplineNames = new Dictionary<string, string>
                {
                    { "Руководство", string.Format("Руководство ВКР {0}", model.AcademicLevel) },
                    { "ГАК", string.Format("Работа в ГЭК (защита ВКР {0})", model.AcademicLevel) },
                    { "Консультации", "Нормоконтроль на тех. направлениях" },
                };

                foreach (XmlNode elemNode in vkrNode.ChildNodes)
                {
                    if (disciplineNames.ContainsKey(elemNode.Name))
                    {
                        var discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == disciplineNames[elemNode.Name]);
                        if (discipline == null)
                        {
                            _context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineRecordBindingModel
                            {
                                DisciplineBlockId = model.DisciplineBlockId,
                                DisciplineName = disciplineNames[elemNode.Name]
                            }));
                            _context.SaveChanges();

                            discipline = _context.Disciplines.FirstOrDefault(d => d.DisciplineName == disciplineNames[elemNode.Name]);
                        }

                        var sem = (Semesters)Enum.ToObject(typeof(Semesters), model.SemesterNumber);
                        var contingent = GetContingent(sem, model.AcademicPlanId);

                        var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
                                                                    apr.AcademicPlanId == model.AcademicPlanId &&
                                                                    apr.DisciplineId == discipline.Id &&
                                                                    apr.ContingentId == contingent.Id &&
                                                                    apr.Semester == sem &&
                                                                    !apr.IsDeleted);
                        if (record == null)
                        {
                            _context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
                            {
                                AcademicPlanId = model.AcademicPlanId,
                                DisciplineId = discipline.Id,
                                ContingentId = contingent.Id,
                                Semester = sem.ToString(),
                                Zet = 0
                            }));
                        }
                        else
                        {
                            _context.Entry(record).State = EntityState.Modified;
                        }
                        _context.SaveChanges();

                        if (elemNode.Name != "ГАК")
                        {
                            CreateAPRE(elemNode.ChildNodes.Item(0).Attributes, record.Id);
                        }
                        else
                        {
                            CreateAPRE(elemNode.Attributes, record.Id);
                        }
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
                    var contingent = GetContingent(sem, model.AcademicPlanId);

                    var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
                                                                apr.AcademicPlanId == model.AcademicPlanId &&
                                                                apr.DisciplineId == discipline.Id &&
                                                                apr.ContingentId == contingent.Id &&
                                                                apr.Semester == sem &&
                                                                !apr.IsDeleted);
                    if (record == null)
                    {
                        _context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
                        {
                            AcademicPlanId = model.AcademicPlanId,
                            DisciplineId = discipline.Id,
                            ContingentId = contingent.Id,
                            Semester = sem.ToString(),
                            Zet = 0
                        }));
                    }
                    _context.SaveChanges();

                    //var kindOfLoad = _context.KindOfLoads.FirstOrDefault(x => x.KindOfLoadName == discpName);

                    //var recordelement = _context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                    //    apre.AcademicPlanRecordId == record.Id &&
                    //    apre.KindOfLoadId == kindOfLoad.Id &&
                    //    !apre.IsDeleted);
                    //if (recordelement == null)
                    //{
                    //    _context.AcademicPlanRecordElements.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                    //    {
                    //        AcademicPlanRecordId = record.Id,
                    //        KindOfLoadId = kindOfLoad.Id,
                    //        PlanHours = 1,
                    //        FactHours = 0
                    //    }));
                    //}
                    //_context.SaveChanges();
                }
            }
            #endregion
        }

        private void CreateAPR(XmlNodeList elementSemNodes, ParseDisciplineBindingModel model, Guid disciplineId)
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
                        // извлекаем зет

                        XmlNode zetNode = elementSemNodeAttributes.GetNamedItem("ЗЕТ");
                        if (zetNode == null)
                        {
                            zetNode = elementSemNodeAttributes.GetNamedItem("ПланЗЕТ");
                            if (zetNode == null)
                            {
                                model.Result.AddError("Not_Found", string.Format("Зет не найдено. Строка {0}", model.Counter));
                                continue;
                            }
                        }
                        var zet = Convert.ToInt32(zetNode.Value);

                        var contingent = GetContingent(sem, model.AcademicPlanId);

                        var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
                            apr.AcademicPlanId == model.AcademicPlanId &&
                            apr.DisciplineId == disciplineId &&
                            apr.ContingentId == contingent.Id &&
                            apr.Semester == sem &&
                            !apr.IsDeleted);
                        if (record == null)
                        {
                            _context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
                            {
                                AcademicPlanId = model.AcademicPlanId,
                                DisciplineId = disciplineId,
                                ContingentId = contingent.Id,
                                Semester = sem.ToString(),
                                Zet = zet
                            }));
                        }
                        else
                        {
                            record.Zet = zet;
                            _context.Entry(record).State = EntityState.Modified;
                        }
                        _context.SaveChanges();

                        record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
                            apr.AcademicPlanId == model.AcademicPlanId &&
                            apr.DisciplineId == disciplineId &&
                            apr.Semester == sem &&
                            !apr.IsDeleted);

                        CreateAPRE(elementSemNodeAttributes, record.Id);

                    }
                    else
                    {
                        //model.Result.AddError("Not_found", string.Format("Семестры не найдены в строке {0}",
                        //    model.Counter));
                        continue;
                    }
                }
            }
        }

        private void CreateAPRE(XmlAttributeCollection elementSemNodeAttributes, Guid apreId)
        {
            //ищем вид нагрузки
            foreach (TimeNorm tiemNorm in _context.TimeNorms)
            {
                XmlNode elemNode = elementSemNodeAttributes.GetNamedItem(tiemNorm.KindOfLoadAttributeName);
                if (elemNode != null)
                {
                    int hours = Convert.ToInt32(elemNode.Value);
                    var recordelement = _context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                        apre.AcademicPlanRecordId == apreId &&
                        apre.TimeNormId == tiemNorm.Id &&
                        !apre.IsDeleted);
                    if (recordelement == null)
                    {
                        _context.AcademicPlanRecordElements.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                        {
                            AcademicPlanRecordId = apreId,
                            TimeNormId = tiemNorm.Id,
                            PlanHours = hours,
                            FactHours = 0
                        }));
                    }
                    else
                    {
                        recordelement.PlanHours = hours;
                        _context.Entry(recordelement).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
        }

        private Contingent GetContingent(Semesters semester, Guid AcademicPlanId)
        {
            var academicPlan = _context.AcademicPlans.Include(x => x.EducationDirection).FirstOrDefault(x => x.Id == AcademicPlanId);
            AcademicCourse cource = AcademicCourse.Course_1;
            switch (semester)
            {
                case Semesters.Первый:
                case Semesters.Второй:
                    cource = AcademicCourse.Course_1;
                    break;
                case Semesters.Третий:
                case Semesters.Четвертый:
                    cource = AcademicCourse.Course_2;
                    break;
                case Semesters.Пятый:
                case Semesters.Шестой:
                    cource = AcademicCourse.Course_3;
                    break;
                case Semesters.Седьмой:
                case Semesters.Восьмой:
                    cource = AcademicCourse.Course_4;
                    break;
            }
            var contingent = _context.Contingents.FirstOrDefault(x => x.EducationDirectionId == academicPlan.EducationDirectionId && x.Course == cource &&
                                                                    x.AcademicYearId == academicPlan.AcademicYearId && !x.IsDeleted);
            if (contingent == null)
            {
                throw new Exception(string.Format("Не найден контингент на направление {0} курс {1}", academicPlan.EducationDirection.ShortName, cource));
            }
            return contingent;
        }
        #endregion

        #region Загрузка учебных планов по синей звездочке
        /// <summary>
        /// Парсинг учебных планов из xml
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultService LoadFromBlueAsteriskAcademicPlanRecord(EducationalProcessLoadFromXMLBindingModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                ResultService result = new ResultService();
                try
                {
                    var academicPlan = _context.AcademicPlans.FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                    if (academicPlan == null)
                    {
                        return ResultService.Error("Error:", "Учебный план not found", ResultServiceStatusCode.NotFound);
                    }
                    // Получаем список курсов, в которых этот план действует
                    List<AcademicCourse> courses = GetCourses(academicPlan);
                    if (courses.Count == 0)
                    {
                        return ResultService.Error("Error:", "Semesters not found", ResultServiceStatusCode.NotFound);
                    }
                    // получаем список семестров из курсов (будем из xml дергать только те записи, у которых семестр попадает в этот диапазон)
                    List<Semesters> semesters = new List<Semesters>();
                    foreach (var course in courses)
                    {
                        int courseInt = (int)Math.Log((double)course, 2) + 1;
                        semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2 - 1)));
                        semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2)));
                    }

                    XmlDocument newXmlDocument = new XmlDocument();
                    newXmlDocument.Load(new XmlTextReader(model.FileName));
                    XmlNode mainRootElementNode = newXmlDocument.SelectSingleNode("/Документ").FirstChild.FirstChild;
                    if (mainRootElementNode != null)
                    {
                        var modelParse = new ParseBlueAsterisk
                        {
                            AcademicYearId = academicPlan.AcademicYearId,
                            AcademicPlanId = academicPlan.Id,
                            Node = mainRootElementNode,
                            Semesters = semesters
                        };

                        result = GetObjectTypeCodes(modelParse);
                        if (!result.Succeeded)
                        {
                            return result;
                        }

                        result = GetDisciplineBlockWithCodes(modelParse);
                        if (!result.Succeeded)
                        {
                            return result;
                        }

                        result = GetDisciplinesWithCodes(modelParse);
                        if (!result.Succeeded)
                        {
                            return result;
                        }

                        result = GetTimeNormWithCodes(modelParse);
                        if (!result.Succeeded)
                        {
                            return result;
                        }

                        result = LoadPlanRecords(modelParse);
                    }
                    else
                    {
                        throw new Exception("Неверная структура xml. Не найден элемент /Документ/diffgr/dsMMISDB");
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

        private ResultService GetObjectTypeCodes(ParseBlueAsterisk model)
        {
            try
            {
                model.ObjectTypes = new List<BlueAsteriskTypeObject>
                {
                    new BlueAsteriskTypeObject { TypeName = "Базовая", IncludeInCalc = true },
                    new BlueAsteriskTypeObject { TypeName = "Выборная", IncludeInCalc = true },
                    new BlueAsteriskTypeObject { TypeName = "Альтернативная", IncludeInCalc = false }
                };
                model.BlockTypes = new List<BlueAsteriskBlockType>();

                // не работает XmlNodeList selectedNodes = mainRootElementNode.SelectNodes("СправочникТипОбъекта"), тоди условие не парвильно задано, толи из-за кириллицы
                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);
                    if (node.Name == "СправочникВидОбъекта")
                    {
                        XmlNode attribute = node.Attributes.GetNamedItem("Наименование");
                        var obj = model.ObjectTypes.FirstOrDefault(x => x.TypeName == attribute.Value);
                        if (obj != null)
                        {
                            obj.Code = node.Attributes.GetNamedItem("Код").Value;
                        }
                    }
                    if (node.Name == "ПланыЦиклы")
                    {
                        XmlNode attribute = node.Attributes.GetNamedItem("Идентификатор");
                        var obj = model.BlockTypes.FirstOrDefault(x => x.Identificator == attribute.Value);
                        if (obj == null)
                        {
                            obj = new BlueAsteriskBlockType { Identificator = attribute.Value };
                            model.BlockTypes.Add(obj);
                        }
                        obj.BlockName = node.Attributes.GetNamedItem("Цикл").Value;
                        obj.Code = node.Attributes.GetNamedItem("Код").Value;
                        obj.IsFacultative = Convert.ToBoolean(node.Attributes.GetNamedItem("Факультативы").Value);
                    }
                }

                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private ResultService GetDisciplineBlockWithCodes(ParseBlueAsterisk model)
        {
            try
            {
                model.DisciplineBlocks = new List<DisciplineBlock>();

                // не работает XmlNodeList selectedNodes = mainRootElementNode.SelectNodes("СправочникТипОбъекта"), тоди условие не парвильно задано, толи из-за кириллицы
                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);
                    if (node.Name == "СправочникТипОбъекта")
                    {
                        XmlNode attribute = node.Attributes.GetNamedItem("Название");
                        var disciplineBlock = _context.DisciplineBlocks.FirstOrDefault(x => x.DisciplineBlockBlueAsteriskName == attribute.Value);
                        if (disciplineBlock != null)
                        {
                            attribute = node.Attributes.GetNamedItem("Код");
                            disciplineBlock.DisciplineBlockBlueAsteriskCode = attribute.Value;

                            model.DisciplineBlocks.Add(disciplineBlock);
                        }
                    }
                }

                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private ResultService GetDisciplinesWithCodes(ParseBlueAsterisk model)
        {
            try
            {
                #region Получаем настройки
                //Получаем номер кафедры
                var kafedraNumber = _context.CurrentSettings.FirstOrDefault(x => x.Key == "Кафедра");
                if (kafedraNumber == null)
                {
                    throw new Exception("CurrentSetting for Кафедра not found");
                }
                #endregion
                model.Disciplines = new List<Discipline>();

                // выбираем все строки по дисциплинам
                // не работает XmlNodeList selectedNodes = mainRootElementNode.SelectNodes("ПланыСтроки"), тоди условие не парвильно задано, толи из-за кириллицы
                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);
                    if (node.Name == "ПланыСтроки")
                    {
                        //смотрим код кафедры, нужно отобрать только наши
                        XmlNode attribute = node.Attributes.GetNamedItem("КодКафедры");
                        if (attribute == null)
                        {
                            // для модулей и дисциплин по выбору кафедра указывается во вложенных дисциплинах
                            // будем считать, что все вложенные дисциплины отностся к одной кафедре
                            attribute = model.Node.ChildNodes.Item(i + 1).Attributes.GetNamedItem("КодКафедры");
                            if (attribute == null)
                            {
                                continue;
                            }
                        }
                        if (attribute.Value == kafedraNumber.Value)
                        {
                            attribute = node.Attributes.GetNamedItem("ТипОбъекта");
                            var disciplineBlock = model.DisciplineBlocks.FirstOrDefault(x => x.DisciplineBlockBlueAsteriskCode == attribute.Value);
                            if (disciplineBlock == null)
                            {
                                throw new Exception(string.Format("Не найден блок дисциплин с кодом {0}.{1}Имеющиеся кода: {2}", attribute.Value,
                                    Environment.NewLine, model.DisciplineBlocks.Select(x => string.Format("{0} = {1}{2}", x.DisciplineBlockBlueAsteriskName,
                                    x.DisciplineBlockBlueAsteriskCode, Environment.NewLine))));
                            }

                            attribute = node.Attributes.GetNamedItem("ВидОбъекта");
                            BlueAsteriskTypeObject ObjectType = model.ObjectTypes.FirstOrDefault(x => x.Code == attribute.Value);

                            attribute = node.Attributes.GetNamedItem("КодБлока");
                            BlueAsteriskBlockType BlockType = model.BlockTypes.FirstOrDefault(x => x.Code == attribute.Value);
                            
                            Discipline parentDiscipline = null;
                            // Для дисциплин по выбору есть родительская
                            attribute = node.Attributes.GetNamedItem("КодРодителя");
                            if (attribute != null && ObjectType.TypeName != "Базовая")
                            {
                                parentDiscipline = model.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskCode == attribute.Value);
                                if (parentDiscipline == null)
                                {
                                    throw new Exception(string.Format("Не найдена родительская дисциплина с кодом {0}", attribute.Value));
                                }
                                parentDiscipline.IsParent = true;
                                _context.SaveChanges();
                            }

                            // ищем название дисциплины
                            attribute = node.Attributes.GetNamedItem("Дисциплина");
                            var discipline = _context.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskName == attribute.Value);
                            if (discipline == null)
                            {
                                discipline = _context.Disciplines.FirstOrDefault(x => x.DisciplineName == attribute.Value);
                                if (discipline == null)
                                {
                                    _context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineRecordBindingModel
                                    {
                                        DisciplineName = attribute.Value,
                                        DisciplineBlockId = disciplineBlock.Id,
                                        DisciplineParentId = parentDiscipline == null ? (Guid?)null : parentDiscipline.Id,
                                        DisciplineBlueAsteriskName = attribute.Value
                                    }));
                                    _context.SaveChanges();

                                    discipline = _context.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskName == attribute.Value);
                                }
                                else
                                {
                                    discipline.DisciplineBlueAsteriskName = attribute.Value;
                                    _context.SaveChanges();
                                }
                            }
                            else if (string.IsNullOrEmpty(discipline.DisciplineBlueAsteriskName))
                            {
                                discipline.DisciplineBlueAsteriskName = attribute.Value;
                                _context.SaveChanges();
                            }
                            if (parentDiscipline != null && discipline.DisciplineParentId != parentDiscipline.Id)
                            {
                                discipline.DisciplineParentId = parentDiscipline.Id;
                                _context.SaveChanges();
                            }

                            attribute = node.Attributes.GetNamedItem("Код");
                            discipline.DisciplineBlueAsteriskCode = attribute.Value;
                            discipline.NotSelected = !ObjectType.IncludeInCalc;
                            if (BlockType != null && BlockType.IsFacultative)
                            {
                                discipline.NotSelected = true;
                            }

                            attribute = node.Attributes.GetNamedItem("ВидПрактики");
                            if (attribute != null)
                            {
                                discipline.DisciplineBlueAsteriskPracticCode = attribute.Value;
                            }

                            model.Disciplines.Add(discipline);
                        }
                    }
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private ResultService GetTimeNormWithCodes(ParseBlueAsterisk model)
        {
            try
            {
                Dictionary<string, string> practics = new Dictionary<string, string>();
                model.TimeNorms = _context.TimeNorms.Where(x => x.AcademicYearId == model.AcademicYearId && !x.IsDeleted).ToList();
                // не работает XmlNodeList selectedNodes = mainRootElementNode.SelectNodes("СправочникВидыРабот"), тоди условие не парвильно задано, толи из-за кириллицы
                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);
                    if (node.Name == "СправочникВидыРабот")
                    {
                        XmlNode attribute = node.Attributes.GetNamedItem("Название");
                        var timeNormSelected = model.TimeNorms.Where(x => x.KindOfLoadBlueAsteriskName == attribute.Value).ToList();
                        if (timeNormSelected.Count > 0)
                        {
                            attribute = node.Attributes.GetNamedItem("Код");
                            foreach (var timeNorm in timeNormSelected)
                            {
                                timeNorm.KindOfLoadBlueAsteriskCode = attribute.Value;
                            }
                        }
                    }
                    if (node.Name == "СправочникВидыПрактик")
                    {
                        XmlNode attributeName = node.Attributes.GetNamedItem("Наименование");
                        XmlNode attributeCode = node.Attributes.GetNamedItem("Код");
                        practics.Add(attributeName.Value, attributeCode.Value);
                    }
                }
                foreach (var tn in model.TimeNorms.Where(x => !string.IsNullOrEmpty(x.KindOfLoadBlueAsteriskPracticName)))
                {
                    tn.KindOfLoadBlueAsteriskPracticCode = practics[tn.KindOfLoadBlueAsteriskPracticName];
                }
                var notFoundTMS = model.TimeNorms.Where(x => string.IsNullOrEmpty(x.KindOfLoadBlueAsteriskCode) && !string.IsNullOrEmpty(x.KindOfLoadBlueAsteriskAttributeName));
                if (notFoundTMS.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var kol in model.TimeNorms)
                    {
                        sb.AppendLine(string.Format("Не найден код для нагрузки {0}{1}", kol.KindOfLoadName, Environment.NewLine));
                    }
                    throw new Exception(sb.ToString());
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private ResultService LoadPlanRecords(ParseBlueAsterisk model)
        {
            var academicPlan = _context.AcademicPlans.FirstOrDefault(x => x.Id == model.AcademicPlanId);
            // помечаем как удаленные все записи плана, потом все найденные восстановим
            var aprs = _context.AcademicPlanRecords.Where(x => x.AcademicPlanId == model.AcademicPlanId).ToList();
            foreach (var apr in aprs)
            {
                var apres = _context.AcademicPlanRecordElements.Where(x => x.AcademicPlanRecordId == apr.Id);
                foreach (var apre in apres)
                {
                    apre.IsDeleted = true;
                }
                apr.IsDeleted = true;
                _context.SaveChanges();
            }

            for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
            {
                XmlNode node = model.Node.ChildNodes.Item(i);
                if (node.Name == "ПланыНовыеЧасы")
                {
                    XmlNode attribute = node.Attributes.GetNamedItem("КодОбъекта");
                    if (attribute == null)
                    {
                        throw new Exception(string.Format("Не найдена атрибут КодОбъекта"));
                    }
                    var discipline = model.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskCode == attribute.Value);
                    if (discipline != null)
                    {
                        // если это модуль или дисциплина по выбору, то они не учитываются, учет будет по их вложенным записям
                        var disciplineBlock = model.DisciplineBlocks.FirstOrDefault(x => x.Id == discipline.DisciplineBlockId);
                        if (!disciplineBlock.DisciplineBlockUseForGrouping)
                        {
                            continue;
                        }
                        // дисциплина по выбору, альтернативная, не выбранная
                        if (discipline.NotSelected)
                        {
                            continue;
                        }

                        // ищем или создаем APR по дисциплине, если семестр нужный
                        var apr = GetAPR(node, discipline, model);
                        if (apr != null)
                        {
                            List<TimeNorm> timeNorms = new List<TimeNorm>();
                            // если перед нами практика, то выбираем только один нужный тип нагрузки
                            if (!string.IsNullOrEmpty(discipline.DisciplineBlueAsteriskPracticCode))
                            {
                                attribute = node.Attributes.GetNamedItem("КодВидаРаботы");
                                if (attribute == null)
                                {
                                    throw new Exception(string.Format("Не найдена атрибут КодВидаРаботы по дисциплине с кодом {0}", discipline.DisciplineBlueAsteriskCode));
                                }
                                var tn = model.TimeNorms.FirstOrDefault(x => x.KindOfLoadBlueAsteriskPracticCode == discipline.DisciplineBlueAsteriskPracticCode);
                                if (tn == null)
                                {
                                    throw new Exception(string.Format("Не найдена нагрузка на практику с кодом {0}", discipline.DisciplineBlueAsteriskPracticCode));
                                }
                                timeNorms.AddRange(model.TimeNorms.Where(x => x.KindOfLoadBlueAsteriskCode == attribute.Value && x.DisciplineBlockId == disciplineBlock.Id &&
                                                                                    (x.TimeNormAcademicLevel == null || x.TimeNormAcademicLevel == academicPlan.AcademicLevel) &&
                                                                                    x.KindOfLoadBlueAsteriskPracticCode == discipline.DisciplineBlueAsteriskPracticCode));
                            }
                            else
                            {
                                attribute = node.Attributes.GetNamedItem("КодВидаРаботы");
                                if (attribute == null)
                                {
                                    throw new Exception(string.Format("Не найдена атрибут КодВидаРаботы по дисциплине с кодом {0}", discipline.DisciplineBlueAsteriskCode));
                                }
                                timeNorms.AddRange(model.TimeNorms.Where(x => x.KindOfLoadBlueAsteriskCode == attribute.Value && x.DisciplineBlockId == disciplineBlock.Id &&
                                                                                    (x.TimeNormAcademicLevel == null || x.TimeNormAcademicLevel == academicPlan.AcademicLevel)));
                            }
                            foreach (var timeNorm in timeNorms)
                            {
                                int hours = 1;
                                if (!string.IsNullOrEmpty(timeNorm.KindOfLoadBlueAsteriskAttributeName))
                                {
                                    attribute = node.Attributes.GetNamedItem(timeNorm.KindOfLoadBlueAsteriskAttributeName);
                                    if (attribute == null)
                                    {
                                        throw new Exception(string.Format("Не найдена атрибут {1} по дисциплине с кодом {0}", discipline.DisciplineBlueAsteriskCode,
                                            timeNorm.KindOfLoadBlueAsteriskAttributeName));
                                    }
                                    hours = Convert.ToInt32(attribute.Value);
                                }

                                var recordelement = _context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                                    apre.AcademicPlanRecordId == apr.Id &&
                                    apre.TimeNormId == timeNorm.Id);

                                if (recordelement == null)
                                {
                                    _context.AcademicPlanRecordElements.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                                    {
                                        AcademicPlanRecordId = apr.Id,
                                        TimeNormId = timeNorm.Id,
                                        PlanHours = hours,
                                        FactHours = 0
                                    }));
                                }
                                else
                                {
                                    if (recordelement.IsDeleted)
                                    {
                                        recordelement.IsDeleted = false;
                                        recordelement.DateDelete = null;
                                    }
                                    recordelement.PlanHours = hours;
                                }
                                _context.SaveChanges();
                            }
                        }
                    }
                }
            }

            return ResultService.Success();
        }

        private AcademicPlanRecord GetAPR(XmlNode node, Discipline discipline, ParseBlueAsterisk model)
        {
            XmlNode attribute = node.Attributes.GetNamedItem("Курс");
            int kurs = Convert.ToInt32(attribute.Value);
            attribute = node.Attributes.GetNamedItem("Семестр");
            int sem = Convert.ToInt32(attribute.Value);

            Semesters semester = (Semesters)Enum.ToObject(typeof(Semesters), (kurs - 1) * 2 + sem);
            if (model.Semesters.Contains(semester))
            {
                var contingent = GetContingent(semester, model.AcademicPlanId);

                var record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
                            apr.AcademicPlanId == model.AcademicPlanId &&
                            apr.DisciplineId == discipline.Id &&
                            apr.ContingentId == contingent.Id &&
                            apr.Semester == semester);
                if (record == null)
                {
                    _context.AcademicPlanRecords.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordRecordBindingModel
                    {
                        AcademicPlanId = model.AcademicPlanId,
                        DisciplineId = discipline.Id,
                        ContingentId = contingent.Id,
                        Semester = semester.ToString()
                    }));

                    _context.SaveChanges();

                    record = _context.AcademicPlanRecords.FirstOrDefault(apr =>
                            apr.AcademicPlanId == model.AcademicPlanId &&
                            apr.DisciplineId == discipline.Id &&
                            apr.ContingentId == contingent.Id &&
                            apr.Semester == semester &&
                            !apr.IsDeleted);
                }
                else if (record.IsDeleted)
                {
                    record.IsDeleted = false;
                    record.DateDelete = null;
                    _context.SaveChanges();
                }

                return record;
            }

            return null;
        }
        #endregion

        public ResultService CreateContingentForAcademicYear(AcademicYearGetBindingModel model)
        {
            try
            {
                var academicPlans = _context.AcademicPlans.Include(x => x.EducationDirection).Where(x => x.AcademicYearId == model.Id && !x.IsDeleted).ToList();
                AcademicCourse[] cources = new AcademicCourse[] { AcademicCourse.Course_1, AcademicCourse.Course_2, AcademicCourse.Course_3, AcademicCourse.Course_4 };
                foreach (var academicPlan in academicPlans)
                {
                    Contingent contingent = null;
                    #region Ищем контингент или создаем его
                    for (int i = 0; i < cources.Length; ++i)
                    {
                        if ((academicPlan.AcademicCourses & cources[i]) == cources[i])
                        {
                            AcademicCourse cource = cources[i];
                            contingent = _context.Contingents.FirstOrDefault(
                                                    x => x.EducationDirectionId == academicPlan.EducationDirectionId &&
                                                    x.Course == cource &&
                                                    x.AcademicYearId == academicPlan.AcademicYearId &&
                                                    !x.IsDeleted
                                                );
                            if (contingent == null)
                            {
                                contingent = new Contingent
                                {
                                    AcademicYearId = model.Id.Value,
                                    EducationDirectionId = academicPlan.EducationDirectionId,
                                    ContingentName = string.Format("{0}-{1}", academicPlan.EducationDirection.ShortName, i + 1),
                                    CountGroups = 0,
                                    CountStudetns = 0,
                                    CountSubgroups = 0,
                                    Course = cource
                                };
                                _context.Contingents.Add(contingent);
                                _context.SaveChanges();
                            }
                            var studentGroups = _context.StudentGroups.Where(x => x.EducationDirectionId == academicPlan.EducationDirectionId &&
                                                    x.Course == cource && !x.IsDeleted).Select(x => x.Id).ToList();
                            if (studentGroups != null && studentGroups.Count > 0)
                            {
                                contingent.CountGroups = studentGroups.Count;
                                var students = _context.Students.Where(x => studentGroups.Contains(x.StudentGroupId.Value) &&
                                                                        x.StudentState == StudentState.Учится && !x.IsDeleted).Count();
                                contingent.CountStudetns = students;
                                // TODO завести в настройках отдельное поле вместо 15
                                contingent.CountSubgroups = students / 15;
                                _context.SaveChanges();
                            }
                        }
                    }
                    #endregion
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<object[]>> GetAcademicYearLoading(AcademicYearGetBindingModel model)
        {
            try
            {
                // Шаблон записи в массиве: APR_ID, Sem, ED, D, По выбору, курс, студенты, потоки, группы, подгруппы, {apre_id, apre_plan, apre_fact}
                List<object[]> list = new List<object[]>();

                // получаем блоки дисциплин, по которым нужно будет группировать записи
                var disciplineBlocks = _context.DisciplineBlocks.Where(x => !x.IsDeleted && x.DisciplineBlockUseForGrouping).OrderBy(x => x.DisciplineBlockOrder);

                // получаем список видов нагрузки, так как нам надо возвращать массив объектов для вывода в гриде
                var timeNorms = _context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id).OrderBy(x => x.TimeNormOrder);

                foreach (var discBlock in disciplineBlocks)
                {
                    List<object> element = new List<object>() {
                        null,
                        null,
                        null,
                        discBlock.Title,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null
                    };
                    foreach (var tn in timeNorms)
                    {
                        element.Add(null);
                        element.Add(null);
                        element.Add(null);
                    }
                    element.Add(null);
                    list.Add(element.ToArray());

                    var aprs = _context.AcademicPlanRecords
                        .Include(x => x.AcademicPlan)
                        .Include(x => x.AcademicPlan.EducationDirection)
                        .Include(x => x.Discipline)
                        .Include(x => x.Contingent)
                        .Where(x => x.Discipline.DisciplineBlockId == discBlock.Id && x.AcademicPlan.AcademicYearId == model.Id && !x.IsDeleted)
                        .OrderByDescending(x => (int)x.Semester % 2)
                        .ThenBy(x => x.Semester)
                        .ThenBy(x => x.AcademicPlan.EducationDirectionId)
                        .ThenBy(x => x.Discipline.DisciplineName);
                    foreach (var apr in aprs)
                    {
                        List<object> elementApr = new List<object>() {
                            apr.Id,
                            (int)apr.Semester % 2 == 0 ? "весна" : "осень",
                            apr.AcademicPlan.EducationDirection.Cipher,
                            apr.Discipline.DisciplineName,
                            apr.Discipline.DisciplineParentId.HasValue ? "да" : "",
                            Math.Log((double)apr.Contingent.Course, 2) + 1,
                            apr.Contingent.CountStudetns,
                            1,
                            apr.Contingent.CountGroups,
                            apr.Contingent.CountSubgroups
                        };
                        decimal factTotal = 0;
                        foreach (var tn in timeNorms)
                        {
                            var apre = _context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == apr.Id && x.TimeNormId == tn.Id && !x.IsDeleted);
                            if (apre != null)
                            {
                                elementApr.Add(apre.Id);
                                if (apre.PlanHours != 0)
                                {
                                    elementApr.Add(apre.PlanHours.ToString("#.0"));
                                }
                                else
                                {
                                    elementApr.Add(null);
                                }
                                if (apre.FactHours != 0)
                                {
                                    factTotal += apre.FactHours;
                                    elementApr.Add(apre.FactHours);
                                }
                                else
                                {
                                    elementApr.Add(null);
                                }
                            }
                            else
                            {
                                elementApr.Add(null);
                                elementApr.Add(null);
                                elementApr.Add(null);
                            }
                        }
                        if (factTotal != 0)
                        {
                            elementApr.Add(factTotal);
                        }
                        else
                        {
                            elementApr.Add(null);
                        }
                        list.Add(elementApr.ToArray());
                    }
                }

                return ResultService<List<object[]>>.Success(list);
            }
            catch (Exception ex)
            {
                return ResultService<List<object[]>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CalcFactHoursForAcademicYear(AcademicYearGetBindingModel model)
        {
            try
            {
                var timeNorms = _context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id).OrderBy(x => x.TimeNormOrder).ToList();
                foreach (var tn in timeNorms)
                {
                    var apres = _context.AcademicPlanRecordElements
                        .Include(x => x.AcademicPlanRecord)
                        .Include(x => x.AcademicPlanRecord.Contingent)
                        .Include(x => x.AcademicPlanRecord.Discipline)
                        .Include(x => x.AcademicPlanRecord.AcademicPlan)
                        .Where(x => x.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id && x.TimeNormId == tn.Id && !x.IsDeleted).ToList();
                    foreach (var apre in apres)
                    {
                        #region Множитель 1 - количество объектов
                        decimal? countObject = null;
                        switch (tn.KindOfLoadType)
                        {
                            case KindOfLoadType.Поток:
                                var stream = _context.StreamLessonRecords.FirstOrDefault(x => x.StreamLesson.AcademicYearId == model.Id &&
                                                                            x.AcademicPlanRecordElementId == apre.Id && !x.IsDeleted);
                                if (stream != null)
                                {
                                    if (stream.IsMain)
                                    {
                                        countObject = 1;
                                    }
                                }
                                else
                                {
                                    countObject = 1;
                                }
                                break;
                            case KindOfLoadType.Группа:
                                countObject = apre.AcademicPlanRecord.Contingent.CountGroups;
                                break;
                            case KindOfLoadType.Подгруппа:
                                countObject = apre.AcademicPlanRecord.Contingent.CountSubgroups;
                                break;
                            case KindOfLoadType.Студенты:
                                countObject = apre.AcademicPlanRecord.Contingent.CountStudetns;
                                break;
                        }
                        #endregion
                        #region Множитель 2 - количество часов
                        decimal? hours = null;
                        // если есть часы, берем оттуда, иначе из учебных планов
                        if (tn.Hours.HasValue)
                        {
                            hours = tn.Hours;
                        }
                        else
                        {
                            hours = apre.PlanHours;
                        }
                        if (!hours.HasValue)
                        {
                            throw new Exception(string.Format("Не найдены часы для записи по норме времени {0} по дисциплине {1}", tn.TimeNormName,
                                apre.AcademicPlanRecord.Discipline.DisciplineName));
                        }
                        #endregion
                        #region
                        decimal? koef = null;
                        if (tn.NumKoef.HasValue)
                        {
                            koef = tn.NumKoef;
                        }
                        else if (tn.TimeNormKoef != TimeNormKoef.Пусто)
                        {
                            switch (tn.TimeNormKoef)
                            {
                                case TimeNormKoef.КоличествоЗет:
                                    // количество зет сохраняется в часах в записи
                                    koef = apre.PlanHours;
                                    break;
                                case TimeNormKoef.КоличествоНедель:
                                    // количество недель сохраняется в часах в записи
                                    koef = apre.PlanHours;
                                    break;
                            }
                        }
                        #endregion
                        if (countObject.HasValue)
                        {
                            apre.FactHours = countObject.Value * hours.Value;
                            if (koef.HasValue)
                            {
                                apre.FactHours *= koef.Value;
                            }
                            _context.SaveChanges();
                        }
                        else
                        {
                            apre.FactHours = 0;
                        }
                    }
                }

                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordForDiciplinePageViewModel> GetAcademicPlanRecordsForDiscipline(AcademicPlanRecrodsForDiciplineBindingModel model)
        {
            int countPages = 0;
            var query = _context.AcademicPlanRecords.Where(ar => ar.AcademicPlan.AcademicYearId == model.AcademicYearId && ar.DisciplineId == model.DisciplineId && !ar.IsDeleted);
            if (model.PageNumber.HasValue && model.PageSize.HasValue)
            {
                countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                query = query
                            .OrderBy(e => e.Semester).ThenBy(e => e.Discipline.DisciplineName)
                            .Skip(model.PageSize.Value * model.PageNumber.Value)
                            .Take(model.PageSize.Value);
            }

            query = query.Include(ar => ar.AcademicPlan).Include(ar => ar.Discipline)/*.Include(ar => ar.KindOfLoad)*/.Include(ar => ar.AcademicPlan.EducationDirection);

            var result = new AcademicPlanRecordForDiciplinePageViewModel
            {
                MaxCount = countPages,
                List = query.Select(ModelFactoryToViewModel.CreateAcademicPlanRecordForDiciplineViewModel).ToList()
            };

            return ResultService<AcademicPlanRecordForDiciplinePageViewModel>.Success(result);
        }

        public ResultService<ScheduleRecordsForDisciplinePageViewModel> GetScheduleRecordsForDiciplinePageViewModel(ScheduleRecordsForDiciplineBindingModel model)
        {
            List<ScheduleRecordsForDisciplineViewModel> list = new List<ScheduleRecordsForDisciplineViewModel>();
            var modelGet = new ScheduleGetBindingModel { DisciplineId = model.DisciplineId, SeasonDateId = model.SeasonDateId };
            var semesters = _serviceSR.GetSemesterSchedule(modelGet);
            var days = new[] { "Пн.", "Вт.", "Ср.", "Чт.", "Пт.", "Сб." };//дни недели
            if (semesters.Succeeded)
            {
                foreach (var rec in semesters.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("{0} нед., {1} {2} пара", rec.Week + 1, days[rec.Day], rec.Lesson + 1),
                        LessonType = rec.LessonType,
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var offsets = _serviceOR.GetOffsetSchedule(modelGet);
            if (offsets.Succeeded)
            {
                foreach (var rec in offsets.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("{0} нед., {1} {2} пара", rec.Week + 1, days[rec.Day], rec.Lesson + 1),
                        LessonType = "зачет",
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var examinations = _serviceER.GetExaminationSchedule(modelGet);
            if (examinations.Succeeded)
            {
                foreach (var rec in examinations.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("Конс:{0}, Экз:{1}", rec.DateConsultation.ToShortDateString(), rec.DateExamination.ToShortDateString()),
                        LessonType = "экзамен",
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var consultations = _serviceCR.GetConsultationSchedule(modelGet);
            if (consultations.Succeeded)
            {
                foreach (var rec in consultations.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("Дата:{0}, Время:{1}", rec.DateConsultation.ToShortDateString(), rec.DateConsultation.ToShortTimeString()),
                        LessonType = "консультация",
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var result = new ScheduleRecordsForDisciplinePageViewModel
            {
                MaxCount = 0,
                List = list
            };

            return ResultService<ScheduleRecordsForDisciplinePageViewModel>.Success(result);
        }

        public ResultService DuplicateAcademicYearElements(EducationalProcessDuplicateAcademicYear model)
        {
            try
            {
                if (model.DuplicateAcademicPlan)
                {
                    DuplicateAcademicPlan(model);
                }
                if (model.DuplicateTimeNorm)
                {
                    DuplicateTimeNorms(model);
                }
                if (model.DuplicateContingent)
                {
                    DuplicateContingent(model);
                }
                if (model.DuplicateSeasonDate)
                {
                    DuplicateSeasonDate(model);
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private void DuplicateAcademicPlan(EducationalProcessDuplicateAcademicYear model)
        {
            var aps = _context.AcademicPlans.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();
            foreach (var ap in aps)
            {
                _context.AcademicPlans.Add(new AcademicPlan
                {
                    AcademicYearId = model.ToAcademicPlanId,
                    AcademicCourses = ap.AcademicCourses,
                    AcademicLevel = ap.AcademicLevel,
                    EducationDirectionId = ap.EducationDirectionId
                });
                _context.SaveChanges();
            }
        }

        private void DuplicateTimeNorms(EducationalProcessDuplicateAcademicYear model)
        {
            var tns = _context.TimeNorms.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();
            foreach (var tn in tns)
            {
                _context.TimeNorms.Add(new TimeNorm
                {
                    AcademicYearId = model.ToAcademicPlanId,
                    TimeNormName = tn.TimeNormName,
                    TimeNormShortName = tn.TimeNormShortName,
                    TimeNormOrder = tn.TimeNormOrder,
                    KindOfLoadName = tn.KindOfLoadName,
                    KindOfLoadAttributeName = tn.KindOfLoadAttributeName,
                    KindOfLoadBlueAsteriskName = tn.KindOfLoadBlueAsteriskName,
                    KindOfLoadBlueAsteriskAttributeName = tn.KindOfLoadBlueAsteriskAttributeName,
                    KindOfLoadBlueAsteriskPracticName = tn.KindOfLoadBlueAsteriskPracticName,
                    KindOfLoadType = tn.KindOfLoadType,
                    Hours = tn.Hours,
                    NumKoef = tn.NumKoef,
                    TimeNormKoef = tn.TimeNormKoef
                });
                _context.SaveChanges();
            }
        }

        private void DuplicateContingent(EducationalProcessDuplicateAcademicYear model)
        {
            var cs = _context.Contingents.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();
            foreach (var c in cs)
            {
                _context.Contingents.Add(new Contingent
                {
                    AcademicYearId = model.ToAcademicPlanId,
                    ContingentName = c.ContingentName,
                    CountGroups = c.CountGroups,
                    CountStudetns = c.CountStudetns,
                    CountSubgroups = c.CountSubgroups,
                    Course = c.Course,
                    EducationDirectionId = c.EducationDirectionId
                });
                _context.SaveChanges();
            }
        }

        private void DuplicateSeasonDate(EducationalProcessDuplicateAcademicYear model)
        {
            var sds = _context.SeasonDates.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();
            foreach (var sd in sds)
            {
                _context.SeasonDates.Add(new SeasonDates
                {
                    AcademicYearId = model.ToAcademicPlanId,
                    DateBeginExamination = sd.DateBeginExamination,
                    DateBeginFirstHalfSemester = sd.DateBeginFirstHalfSemester,
                    DateBeginOffset = sd.DateBeginOffset,
                    DateBeginPractice = sd.DateBeginPractice,
                    DateBeginSecondHalfSemester = sd.DateBeginSecondHalfSemester,
                    DateEndExamination = sd.DateEndExamination,
                    DateEndFirstHalfSemester = sd.DateEndFirstHalfSemester,
                    DateEndOffset = sd.DateEndOffset,
                    DateEndPractice = sd.DateEndPractice,
                    DateEndSecondHalfSemester = sd.DateEndSecondHalfSemester,
                    Title = sd.Title
                });
                _context.SaveChanges();
            }
        }
    }
}
