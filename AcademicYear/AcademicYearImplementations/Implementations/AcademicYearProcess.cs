
using AcademicYearImplementations.Helper;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.HelperModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseImplementations;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using Models.AcademicYearData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class AcademicYearProcess : IAcademicYearProcess
    {
        private readonly ILecturerService _serviceL;

        private readonly IDisciplineTimeDistributionRecordService _serviceDTDR;

        public AcademicYearProcess(ILecturerService serviceL, IDisciplineTimeDistributionRecordService serviceDTDR)
        {
            _serviceL = serviceL;
            _serviceDTDR = serviceDTDR;
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
            using (var context = DepartmentUserManager.GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                ResultService result = new ResultService();
                try
                {
                    var academicPlan = context.AcademicPlans
                        .Include(x => x.EducationDirection)
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
                        int magistersCorrect = academicPlan.EducationDirection.Qualification == EducationDirectionQualification.Магистратура ? 4 : 0;
                        int courseInt = (int)Math.Log((double)course, 2) + 1 - magistersCorrect;
                        semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2 - 1)));
                        semesters.Add((Semesters)Enum.ToObject(typeof(Semesters), Convert.ToInt32(courseInt * 2)));
                    }
                    //Получаем номер кафедры
                    var currentSetting = context.CurrentSettings
                         .FirstOrDefault(cs => cs.Key == "Кафедра");
                    if (currentSetting == null)
                    {
                        return ResultService.Error("Error:", "CurrentSetting not found",
                            ResultServiceStatusCode.NotFound);
                    }
                    var settingDisciplineBlockModules = context.CurrentSettings
                        .FirstOrDefault(cs => cs.Key == "Дисциплины (модули)");
                    if (settingDisciplineBlockModules == null)
                    {
                        return ResultService.Error("Error:", "В настройках не указан disciplineBlock(Дисциплины (модули))",
                            ResultServiceStatusCode.NotFound);
                    }
                    var disciplineBlockModuls = context.DisciplineBlocks
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
                        var settingDisciplineBlockPractic = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Практика");
                        if (settingDisciplineBlockPractic == null)
                        {
                            return ResultService.Error("Error:", "В настройках не указан disciplineBlock(Практика)",
                                ResultServiceStatusCode.NotFound);
                        }
                        var disciplineBlockPractic = context.DisciplineBlocks.FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockPractic.Value));
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
                                    academicPlan.EducationDirection.Qualification == EducationDirectionQualification.Бакалавриат) ||
                            (semesters.Contains(Semesters.Четвертый) &&
                                    academicPlan.EducationDirection.Qualification == EducationDirectionQualification.Магистратура))
                        {
                            #region ГЭК и ГАК
                            var settingDisciplineBlockGIA = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "ГИА");
                            if (settingDisciplineBlockGIA == null)
                            {
                                return ResultService.Error("Error:", "В настройках не указан disciplineBlock(ГИА)",
                                    ResultServiceStatusCode.NotFound);
                            }
                            var disciplineBlockGIA = context.DisciplineBlocks.FirstOrDefault(db => db.Title.Contains(settingDisciplineBlockGIA.Value));
                            if (disciplineBlockGIA == null)
                            {
                                return ResultService.Error("Error:", "disciplineBlock(ГИА) not found",
                                    ResultServiceStatusCode.NotFound);
                            }
                            string textAcademicLevel = "";
                            int semNumber = 0;
                            switch (academicPlan.EducationDirection.Qualification)
                            {
                                case EducationDirectionQualification.Бакалавриат:
                                    textAcademicLevel = "бакалавра";
                                    semNumber = 8;
                                    break;
                                case EducationDirectionQualification.Магистратура:
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
                    using (var context = DepartmentUserManager.GetContext)
                    {
                        //ищем дисцилпину, если не находим, создаем							
                        var discipline = context.Disciplines
                        .FirstOrDefault(d => d.DisciplineName == disciplineAttributes.Value &&
                                        !d.IsDeleted);
                        if (discipline == null)
                        {
                            context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineSetBindingModel
                            {
                                DisciplineName = disciplineAttributes.Value,
                                DisciplineBlockId = model.DisciplineBlockId
                            }));
                            context.SaveChanges();

                            discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
                                                disciplineAttributes.Value);
                    if (discipline == null)
                    {
                        context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineSetBindingModel
                        {
                            DisciplineName = disciplineAttributes.Value,
                            DisciplineBlockId = model.DisciplineBlockId
                        }));
                        context.SaveChanges();

                        discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName ==
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
                        using (var context = DepartmentUserManager.GetContext)
                        {
                            var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == disciplineNames[elemNode.Name]);
                            if (discipline == null)
                            {
                                context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineSetBindingModel
                                {
                                    DisciplineBlockId = model.DisciplineBlockId,
                                    DisciplineName = disciplineNames[elemNode.Name]
                                }));
                                context.SaveChanges();

                                discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == disciplineNames[elemNode.Name]);
                            }

                            var sem = (Semesters)Enum.ToObject(typeof(Semesters), model.SemesterNumber);
                            var contingent = GetContingent(sem, model.AcademicPlanId);

                            var record = context.AcademicPlanRecords.FirstOrDefault(apr =>
                                                                        apr.AcademicPlanId == model.AcademicPlanId &&
                                                                        apr.DisciplineId == discipline.Id &&
                                                                        apr.ContingentId == contingent.Id &&
                                                                        apr.Semester == sem &&
                                                                        !apr.IsDeleted);
                            if (record == null)
                            {
                                context.AcademicPlanRecords.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                                {
                                    AcademicPlanId = model.AcademicPlanId,
                                    DisciplineId = discipline.Id,
                                    ContingentId = contingent.Id,
                                    Semester = sem.ToString(),
                                    Zet = 0
                                }));
                            }
                            context.SaveChanges();

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
                    using (var context = DepartmentUserManager.GetContext)
                    {
                        var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
                        if (discipline == null)
                        {
                            context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineSetBindingModel
                            {
                                DisciplineBlockId = model.DisciplineBlockId,
                                DisciplineName = discpName
                            }));
                            context.SaveChanges();

                            discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == discpName);
                        }
                        var sem = (Semesters)Enum.ToObject(typeof(Semesters), model.SemesterNumber);
                        var contingent = GetContingent(sem, model.AcademicPlanId);

                        var record = context.AcademicPlanRecords.FirstOrDefault(apr =>
                                                                    apr.AcademicPlanId == model.AcademicPlanId &&
                                                                    apr.DisciplineId == discipline.Id &&
                                                                    apr.ContingentId == contingent.Id &&
                                                                    apr.Semester == sem &&
                                                                    !apr.IsDeleted);
                        if (record == null)
                        {
                            context.AcademicPlanRecords.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                            {
                                AcademicPlanId = model.AcademicPlanId,
                                DisciplineId = discipline.Id,
                                ContingentId = contingent.Id,
                                Semester = sem.ToString(),
                                Zet = 0
                            }));
                        }
                        context.SaveChanges();
                    }
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

                        using (var context = DepartmentUserManager.GetContext)
                        {
                            var record = context.AcademicPlanRecords.FirstOrDefault(apr =>
                            apr.AcademicPlanId == model.AcademicPlanId &&
                            apr.DisciplineId == disciplineId &&
                            apr.ContingentId == contingent.Id &&
                            apr.Semester == sem &&
                            !apr.IsDeleted);
                            if (record == null)
                            {
                                context.AcademicPlanRecords.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
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
                            }
                            context.SaveChanges();

                            record = context.AcademicPlanRecords.FirstOrDefault(apr =>
                                apr.AcademicPlanId == model.AcademicPlanId &&
                                apr.DisciplineId == disciplineId &&
                                apr.Semester == sem &&
                                !apr.IsDeleted);

                            CreateAPRE(elementSemNodeAttributes, record.Id);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private void CreateAPRE(XmlAttributeCollection elementSemNodeAttributes, Guid apreId)
        {
            //ищем вид нагрузки
            using (var context = DepartmentUserManager.GetContext)
            {
                foreach (TimeNorm tiemNorm in context.TimeNorms)
                {
                    XmlNode elemNode = elementSemNodeAttributes.GetNamedItem(tiemNorm.KindOfLoadAttributeName);
                    if (elemNode != null)
                    {
                        int hours = Convert.ToInt32(elemNode.Value);
                        var recordelement = context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                            apre.AcademicPlanRecordId == apreId &&
                            apre.TimeNormId == tiemNorm.Id &&
                            !apre.IsDeleted);
                        if (recordelement == null)
                        {
                            context.AcademicPlanRecordElements.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
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
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        private Contingent GetContingent(Semesters semester, Guid AcademicPlanId)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var academicPlan = context.AcademicPlans.Include(x => x.EducationDirection).FirstOrDefault(x => x.Id == AcademicPlanId);
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
                var contingent = context.Contingents.FirstOrDefault(x => x.EducationDirectionId == academicPlan.EducationDirectionId && x.Course == cource &&
                                                                        x.AcademicYearId == academicPlan.AcademicYearId && !x.IsDeleted);
                if (contingent == null)
                {
                    throw new Exception(string.Format("Не найден контингент на направление {0} курс {1}", academicPlan.EducationDirection.ShortName, cource));
                }
                return contingent;
            }
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
            using (var context = DepartmentUserManager.GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                ResultService result = new ResultService();
                try
                {
                    var academicPlan = context.AcademicPlans
                        .FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted && x.EducationDirectionId.HasValue && x.AcademicCourses.HasValue);
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
                            Semesters = semesters,
                            Disciplines = new List<DisciplineSetBindingModel>()
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

                        result = GetTimeNormWithCodes(modelParse);
                        if (!result.Succeeded)
                        {
                            return result;
                        }

                        result = GetNewHours(modelParse);
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
                model.DisicplineTypes = new List<BlueAsteriskDisicplineType>
                {
                    new BlueAsteriskDisicplineType { TypeName = "Базовая" },
                    new BlueAsteriskDisicplineType { TypeName = "Выборная" },
                    new BlueAsteriskDisicplineType { TypeName = "Альтернативная" }
                };

                model.BlockTypes = new List<BlueAsteriskBlockType>();

                // не работает XmlNodeList selectedNodes = mainRootElementNode.SelectNodes("СправочникВидОбъекта"), тоди условие не парвильно задано, толи из-за кириллицы
                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);
                    if (node.Name == "СправочникВидОбъекта")
                    {
                        XmlNode attribute = node.Attributes.GetNamedItem("Наименование");
                        var obj = model.DisicplineTypes.FirstOrDefault(x => x.TypeName == attribute.Value);
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
                model.DisciplineBlocks = new List<DisciplineBlockSetBindingModel>();

                // не работает XmlNodeList selectedNodes = mainRootElementNode.SelectNodes("СправочникТипОбъекта"), тоди условие не парвильно задано, толи из-за кириллицы
                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);
                    if (node.Name == "СправочникТипОбъекта")
                    {
                        XmlNode attribute = node.Attributes.GetNamedItem("Название");
                        using (var context = DepartmentUserManager.GetContext)
                        {
                            var disciplineBlock = context.DisciplineBlocks.FirstOrDefault(x => x.DisciplineBlockBlueAsteriskName == attribute.Value);
                            if (disciplineBlock != null)
                            {
                                attribute = node.Attributes.GetNamedItem("Код");

                                model.DisciplineBlocks.Add(new DisciplineBlockSetBindingModel
                                {
                                    Id = disciplineBlock.Id,
                                    Title = disciplineBlock.Title,
                                    DisciplineBlockOrder = disciplineBlock.DisciplineBlockOrder,
                                    DisciplineBlockUseForGrouping = disciplineBlock.DisciplineBlockUseForGrouping,
                                    DisciplineBlockBlueAsteriskName = disciplineBlock.DisciplineBlockBlueAsteriskName,
                                    DisciplineBlockBlueAsteriskCode = attribute.Value
                                });
                            }
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    Dictionary<string, string> practics = new Dictionary<string, string>();
                    model.TimeNorms = context.TimeNorms
                        .Where(x => x.AcademicYearId == model.AcademicYearId && !x.IsDeleted)
                        .Select(x => new TimeNormSetBindingModel
                        {
                            Id = x.Id,
                            AcademicYearId = x.AcademicYearId,
                            DisciplineBlockId = x.DisciplineBlockId,
                            Hours = x.Hours,
                            KindOfLoadAttributeName = x.KindOfLoadAttributeName,
                            KindOfLoadBlueAsteriskAttributeName = x.KindOfLoadBlueAsteriskAttributeName,
                            KindOfLoadBlueAsteriskName = x.KindOfLoadBlueAsteriskName,
                            KindOfLoadBlueAsteriskPracticName = x.KindOfLoadBlueAsteriskPracticName,
                            KindOfLoadName = x.KindOfLoadName,
                            KindOfLoadType = x.KindOfLoadType.ToString(),
                            NumKoef = x.NumKoef,
                            TimeNormEducationDirectionQualification = x.TimeNormEducationDirectionQualification.ToString(),
                            TimeNormKoef = x.TimeNormKoef.ToString(),
                            TimeNormName = x.TimeNormName,
                            TimeNormOrder = x.TimeNormOrder,
                            TimeNormShortName = x.TimeNormShortName,
                            UseInLearningProgress = x.UseInLearningProgress
                        })
                        .ToList();
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
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private ResultService GetNewHours(ParseBlueAsterisk model)
        {
            try
            {
                model.NewHours = new List<BlueAsteriskNewHour>();

                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);

                    if (node.Name == "ПланыНовыеЧасы")
                    {
                        var attributeObject = node.Attributes.GetNamedItem("КодОбъекта");
                        if (attributeObject == null)
                        {
                            throw new Exception(string.Format("Не найдена атрибут КодОбъекта"));
                        }

                        var attributeTimeNorm = node.Attributes.GetNamedItem("КодВидаРаботы");
                        if (attributeTimeNorm == null)
                        {
                            throw new Exception(string.Format("Не найдена атрибут КодВидаРаботы"));
                        }

                        var attributeKurs = node.Attributes.GetNamedItem("Курс");
                        if (attributeKurs == null)
                        {
                            throw new Exception(string.Format("Не найдена атрибут Курс"));
                        }

                        var attributeSemestr = node.Attributes.GetNamedItem("Семестр");
                        if (attributeSemestr == null)
                        {
                            throw new Exception(string.Format("Не найдена атрибут Семестр"));
                        }

                        Dictionary<string, decimal> timeNorms = new Dictionary<string, decimal>();

                        foreach (var timeNorm in model.TimeNorms.Select(x => x.KindOfLoadBlueAsteriskAttributeName).Distinct())
                        {
                            if (!string.IsNullOrEmpty(timeNorm))
                            {
                                var attribute = node.Attributes.GetNamedItem(timeNorm);
                                if (attribute != null && !timeNorms.ContainsKey(timeNorm))
                                {
                                    var str = attribute.Value;
                                    if (decimal.TryParse(str, out decimal h))
                                    {
                                        timeNorms.Add(timeNorm, h);
                                    }
                                    else
                                    {
                                        if (attribute.Value.Contains('.'))
                                        {
                                            str = attribute.Value.Replace('.', ',');
                                        }
                                        else if (attribute.Value.Contains(','))
                                        {
                                            str = attribute.Value.Replace(',', '.');
                                        }

                                        if (decimal.TryParse(str, out h))
                                        {
                                            timeNorms.Add(timeNorm, h);
                                        }
                                    }
                                }
                            }
                        }

                        model.NewHours.Add(new BlueAsteriskNewHour
                        {
                            Kurs = Convert.ToInt32(attributeKurs.Value),
                            ObjectCode = attributeObject.Value,
                            Semester = Convert.ToInt32(attributeSemestr.Value),
                            TypeNormCode = attributeTimeNorm.Value,
                            TimeNorms = timeNorms
                        });
                    }
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
            using (var context = DepartmentUserManager.GetContext)
            {
                #region Получаем настройки
                //Получаем номер кафедры
                var kafedraNumber = context.CurrentSettings.FirstOrDefault(x => x.Key == "Кафедра");
                if (kafedraNumber == null)
                {
                    throw new Exception("CurrentSetting for Кафедра not found");
                }
                #endregion

                var academicPlan = context.AcademicPlans.Include(x => x.EducationDirection).FirstOrDefault(x => x.Id == model.AcademicPlanId);

                #region помечаем как удаленные все записи плана, потом все найденные восстановим
                var aprs = context.AcademicPlanRecords.Where(x => x.AcademicPlanId == model.AcademicPlanId).ToList();
                foreach (var apr in aprs)
                {
                    var apres = context.AcademicPlanRecordElements.Where(x => x.AcademicPlanRecordId == apr.Id);
                    foreach (var apre in apres)
                    {
                        apre.IsDeleted = true;
                        apre.DateDelete = DateTime.Now;
                    }
                    apr.IsDeleted = true;
                    apr.DateDelete = DateTime.Now;
                    context.SaveChanges();
                }
                #endregion

                for (int i = 0; i < model.Node.ChildNodes.Count; ++i)
                {
                    XmlNode node = model.Node.ChildNodes.Item(i);

                    if (node.Name == "ПланыСтроки")
                    {
                        //смотрим код кафедры, нужно отобрать только наши
                        XmlNode attribute = node.Attributes.GetNamedItem("КодКафедры");

                        // если код кафедры или дисциплина по выбору (блок, включающий дисциплины по выбору)
                        if (attribute == null || attribute.Value == kafedraNumber.Value)
                        {
                            //базовая, выборная или альтернативная
                            attribute = node.Attributes.GetNamedItem("ВидОбъекта");
                            BlueAsteriskDisicplineType objectType = model.DisicplineTypes.FirstOrDefault(x => x.Code == attribute.Value);

                            // ищем дисциплину
                            var discipline = GetDiscipline(node, model);

                            model.Disciplines.Add(discipline);

                            // вытаскиваем часы по дисциплине
                            var hours = model.NewHours.Where(x => x.ObjectCode == discipline.DisciplineBlueAsteriskCode).ToList();
                            foreach (var hour in hours)
                            {
                                var apr = GetAPR(node, hour, discipline, model, objectType);

                                if (apr != null)
                                {

                                    List<TimeNormSetBindingModel> timeNorms = new List<TimeNormSetBindingModel>();
                                    // если перед нами практика, то выбираем только один нужный тип нагрузки
                                    if (!string.IsNullOrEmpty(discipline.DisciplineBlueAsteriskPracticCode))
                                    {
                                        var tn = model.TimeNorms.FirstOrDefault(x => x.KindOfLoadBlueAsteriskPracticCode == discipline.DisciplineBlueAsteriskPracticCode);
                                        if (tn == null)
                                        {
                                            throw new Exception(string.Format("Не найдена нагрузка на практику с кодом {0}", discipline.DisciplineBlueAsteriskPracticCode));
                                        }
                                        timeNorms.AddRange(model.TimeNorms.Where(x => x.KindOfLoadBlueAsteriskCode == hour.TypeNormCode &&
                                                                                    x.DisciplineBlockId == discipline.DisciplineBlockId &&
                                                                                    (string.IsNullOrEmpty(x.TimeNormEducationDirectionQualification) ||
                                                                                        x.TimeNormEducationDirectionQualification == academicPlan.EducationDirection.Qualification.ToString()) &&
                                                                                    x.KindOfLoadBlueAsteriskPracticCode == discipline.DisciplineBlueAsteriskPracticCode));
                                    }
                                    else
                                    {
                                        timeNorms.AddRange(model.TimeNorms.Where(x => x.KindOfLoadBlueAsteriskCode == hour.TypeNormCode &&
                                                                                    x.DisciplineBlockId == discipline.DisciplineBlockId &&
                                                                                            (string.IsNullOrEmpty(x.TimeNormEducationDirectionQualification) ||
                                                                                            x.TimeNormEducationDirectionQualification == academicPlan.EducationDirection.Qualification.ToString())));
                                    }
                                    foreach (var timeNorm in timeNorms)
                                    {
                                        decimal planHours = 1;
                                        if (!string.IsNullOrEmpty(timeNorm.KindOfLoadBlueAsteriskAttributeName))
                                        {
                                            if (!hour.TimeNorms.ContainsKey(timeNorm.KindOfLoadBlueAsteriskAttributeName))
                                            {
                                                throw new Exception(string.Format("Не найдена атрибут {1} по дисциплине с кодом {0}", discipline.DisciplineBlueAsteriskCode,
                                                    timeNorm.KindOfLoadBlueAsteriskAttributeName));
                                            }
                                            planHours = Math.Abs(hour.TimeNorms[timeNorm.KindOfLoadBlueAsteriskAttributeName]);
                                        }

                                        var recordelement = context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                                            apre.AcademicPlanRecordId == apr.Id &&
                                            apre.TimeNormId == timeNorm.Id);

                                        if (recordelement == null)
                                        {
                                            context.AcademicPlanRecordElements.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                                            {
                                                AcademicPlanRecordId = apr.Id,
                                                TimeNormId = timeNorm.Id,
                                                PlanHours = planHours,
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
                                            recordelement.PlanHours = planHours;
                                        }
                                        context.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                return ResultService.Success();
            }
        }

        private DisciplineSetBindingModel GetDiscipline(XmlNode node, ParseBlueAsterisk model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                //Дисциплины, практки или гиа
                var attribute = node.Attributes.GetNamedItem("ТипОбъекта");
                var disciplineBlock = model.DisciplineBlocks.FirstOrDefault(x => x.DisciplineBlockBlueAsteriskCode == attribute.Value);
                if (disciplineBlock == null)
                {
                    throw new Exception(string.Format("Не найден блок дисциплин с кодом {0}.{1}Имеющиеся кода: {2}", attribute.Value,
                        Environment.NewLine, model.DisciplineBlocks.Select(x => string.Format("{0} = {1}{2}", x.DisciplineBlockBlueAsteriskName,
                        x.DisciplineBlockBlueAsteriskCode, Environment.NewLine))));
                }

                attribute = node.Attributes.GetNamedItem("Дисциплина");
                var discipline = context.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskName == attribute.Value);
                if (discipline == null)
                {
                    discipline = context.Disciplines.FirstOrDefault(x => x.DisciplineName == attribute.Value);
                    if (discipline == null)
                    {
                        context.Disciplines.Add(ModelFacotryFromBindingModel.CreateDiscipline(new DisciplineSetBindingModel
                        {
                            DisciplineName = attribute.Value,
                            DisciplineBlockId = disciplineBlock.Id,
                            DisciplineBlueAsteriskName = attribute.Value
                        }));
                        context.SaveChanges();

                        discipline = context.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskName == attribute.Value);
                    }
                    else
                    {
                        discipline.DisciplineBlueAsteriskName = attribute.Value;
                        context.SaveChanges();
                    }
                }

                attribute = node.Attributes.GetNamedItem("Код");

                var attributePractic = node.Attributes.GetNamedItem("ВидПрактики");

                return new DisciplineSetBindingModel
                {
                    Id = discipline.Id,
                    DisciplineBlockId = discipline.DisciplineBlockId,
                    DisciplineBlueAsteriskName = discipline.DisciplineBlueAsteriskName,
                    DisciplineName = discipline.DisciplineName,
                    DisciplineShortName = discipline.DisciplineShortName,
                    DisciplineBlueAsteriskCode = attribute.Value,
                    DisciplineBlueAsteriskPracticCode = attributePractic?.Value
                };
            }
        }

        private AcademicPlanRecord GetAPR(XmlNode node, BlueAsteriskNewHour hour, DisciplineSetBindingModel discipline, ParseBlueAsterisk model, BlueAsteriskDisicplineType ObjectType)
        {
            var attribute = node.Attributes.GetNamedItem("ЗЕТфакт");
            var zet = Convert.ToInt32(attribute.Value);

            Semesters semester = (Semesters)Enum.ToObject(typeof(Semesters), (hour.Kurs - 1) * 2 + hour.Semester);
            if (model.Semesters.Contains(semester))
            {
                var contingent = GetContingent(semester, model.AcademicPlanId);

                using (var context = DepartmentUserManager.GetContext)
                {
                    AcademicPlanRecord recordParent = null;
                    attribute = node.Attributes.GetNamedItem("КодРодителя");
                    if (attribute != null)
                    {
                        var parentDiscipilne = model.Disciplines.FirstOrDefault(x => x.DisciplineBlueAsteriskCode == attribute.Value);
                        if (parentDiscipilne == null)
                        {
                            throw new Exception(string.Format("Не найдена родительская дисциплина с кодом {0}", attribute.Value));
                        }

                        recordParent = context.AcademicPlanRecords.FirstOrDefault(apr =>
                                apr.AcademicPlanId == model.AcademicPlanId &&
                                apr.DisciplineId == parentDiscipilne.Id &&
                                apr.ContingentId == contingent.Id &&
                                apr.Semester == semester);

                        if (recordParent == null)
                        {
                            context.AcademicPlanRecords.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                            {
                                AcademicPlanId = model.AcademicPlanId,
                                DisciplineId = parentDiscipilne.Id,
                                ContingentId = contingent.Id,
                                Semester = semester.ToString(),
                                Selectable = ObjectType.TypeName != "Базовая",
                                Zet = zet,
                                IsParent = true
                            }));

                            context.SaveChanges();

                            recordParent = context.AcademicPlanRecords.FirstOrDefault(apr =>
                                    apr.AcademicPlanId == model.AcademicPlanId &&
                                    apr.DisciplineId == parentDiscipilne.Id &&
                                    apr.ContingentId == contingent.Id &&
                                    apr.Semester == semester &&
                                    !apr.IsDeleted);
                        }
                        else if (recordParent.IsDeleted)
                        {
                            recordParent.IsDeleted = false;
                            recordParent.DateDelete = null;

                            recordParent.IsParent = true;
                            context.SaveChanges();
                        }
                        else if (!recordParent.IsParent)
                        {
                            recordParent.IsParent = true;
                            context.SaveChanges();
                        }

                    }

                    var record = context.AcademicPlanRecords.FirstOrDefault(apr =>
                            apr.AcademicPlanId == model.AcademicPlanId &&
                            apr.DisciplineId == discipline.Id &&
                            apr.ContingentId == contingent.Id &&
                            apr.Semester == semester);

                    if (record == null)
                    {
                        context.AcademicPlanRecords.Add(AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                        {
                            AcademicPlanId = model.AcademicPlanId,
                            DisciplineId = discipline.Id,
                            ContingentId = contingent.Id,
                            Semester = semester.ToString(),
                            Selectable = ObjectType.TypeName != "Базовая",
                            IsSelected = ObjectType.TypeName == "Базовая",
                            Zet = zet,
                            AcademicPlanRecordParentId = recordParent?.Id
                        }));

                        context.SaveChanges();

                        record = context.AcademicPlanRecords.FirstOrDefault(apr =>
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
                        record.Zet = zet;
                        record.Selectable = ObjectType.TypeName != "Базовая";
                        record.IsSelected = ObjectType.TypeName == "Базовая";
                        //record.AcademicPlanRecordParentId = recordParent?.Id;

                        context.SaveChanges();
                    }
                    return record;
                }
            }

            return null;
        }
        #endregion

        public ResultService CreateContingentForAcademicYear(AcademicYearGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var academicPlans = context.AcademicPlans
                    .Include(x => x.EducationDirection)
                    .Where(x => x.AcademicYearId == model.Id && x.EducationDirectionId.HasValue && !x.IsDeleted)
                    .ToList();
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
                                contingent = context.Contingents.FirstOrDefault(
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
                                        EducationDirectionId = academicPlan.EducationDirectionId.Value,
                                        ContingentName = string.Format("{0}-{1}", academicPlan.EducationDirection.ShortName, i + 1),
                                        CountGroups = 0,
                                        CountStudetns = 0,
                                        CountSubgroups = 0,
                                        Course = cource
                                    };
                                    context.Contingents.Add(contingent);
                                    context.SaveChanges();
                                }
                                var studentGroups = context.StudentGroups.Where(x => x.EducationDirectionId == academicPlan.EducationDirectionId &&
                                                        x.Course == cource && !x.IsDeleted).Select(x => x.Id).ToList();
                                if (studentGroups != null && studentGroups.Count > 0)
                                {
                                    contingent.CountGroups = studentGroups.Count;
                                    var students = context.Students.Where(x => studentGroups.Contains(x.StudentGroupId.Value) &&
                                                                            x.StudentState == StudentState.Учится && !x.IsDeleted).Count();
                                    contingent.CountStudetns = students;
                                    // TODO завести в настройках отдельное поле вместо 15
                                    contingent.CountSubgroups = students / 15;
                                    context.SaveChanges();
                                }
                            }
                        }
                        #endregion
                    }
                    return ResultService.Success();
                }
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    // Шаблон записи в массиве: APR_ID, Sem, ED, D, По выбору, курс, студенты, потоки, группы, подгруппы, {apre_id, apre_plan, apre_fact}
                    List<object[]> list = new List<object[]>();

                    // получаем блоки дисциплин, по которым нужно будет группировать записи
                    var disciplineBlocks = context.DisciplineBlocks.Where(x => !x.IsDeleted && x.DisciplineBlockUseForGrouping).OrderBy(x => x.DisciplineBlockOrder);

                    // получаем список видов нагрузки, так как нам надо возвращать массив объектов для вывода в гриде
                    var timeNorms = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id).OrderBy(x => x.TimeNormOrder);

                    // прреп
                    var lecturers = _serviceL.GetLecturers(new LecturerGetBindingModel()).Result.List;

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
                        //TODO: Дописать
                        foreach (var lect in lecturers)
                        {
                            element.Add(null);
                        }

                        element.Add(null);
                        list.Add(element.ToArray());

                        var aprs = context.AcademicPlanRecords
                            .Include(x => x.AcademicPlan)
                            .Include(x => x.AcademicPlan.EducationDirection)
                            .Include(x => x.Discipline)
                            .Include(x => x.Contingent)
                            .Where(x => x.Discipline.DisciplineBlockId == discBlock.Id && x.AcademicPlan.AcademicYearId == model.Id && !x.IsDeleted && x.IsSelected && !x.IsParent)
                            .OrderByDescending(x => (int)x.Semester % 2)
                            .ThenBy(x => x.AcademicPlan.EducationDirection.Qualification)
                            .ThenBy(x => x.Semester)
                            .ThenBy(x => x.AcademicPlan.EducationDirectionId)
                            .ThenBy(x => x.Discipline.DisciplineName);
                        foreach (var apr in aprs)
                        {
                            List<object> elementApr = new List<object>() {
                                apr.Id,
                                apr.Semester.HasValue ? (int)apr.Semester % 2 == 0 ? "весна" : "осень" : null,
                                apr.AcademicPlan.EducationDirectionId.HasValue ? apr.AcademicPlan.EducationDirection.Cipher : null,
                                apr.Discipline.DisciplineName,
                                apr.Selectable ? "да" : "",
                                apr.ContingentId.HasValue ? Math.Log((double)apr.Contingent.Course, 2) + 1 : (double?)null,
                                apr.ContingentId.HasValue ? apr.Contingent.CountStudetns : (int?)null,
                                apr.ContingentId.HasValue ? 1 : (int?)null,
                                apr.ContingentId.HasValue ? apr.Contingent.CountGroups : (int?)null,
                                apr.ContingentId.HasValue ? apr.Contingent.CountSubgroups : (int?)null
                            };
                            decimal factTotal = 0;
                            foreach (var tn in timeNorms)
                            {
                                var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == apr.Id && x.TimeNormId == tn.Id && !x.IsDeleted);
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

                            // Итог - дисциплин
                            if (factTotal != 0)
                            {
                                elementApr.Add(factTotal);
                            }
                            else
                            {
                                elementApr.Add(null);
                            }

                            decimal lectTotal = 0;
                            foreach (var lect in lecturers)
                            {
                                var lectHours = context.AcademicPlanRecordMissions.Where(x => x.LecturerId == lect.Id && x.AcademicPlanRecordElement.AcademicPlanRecordId == apr.Id && !x.IsDeleted);
                                if (lectHours.Count() > 0)
                                {
                                    elementApr.Add(lectHours.Sum(x => x.Hours));
                                    lectTotal += lectHours.Sum(x => x.Hours);
                                }
                                else
                                {
                                    elementApr.Add(null);
                                }
                            }

                            // Итог - дисциплин
                            if (lectTotal != 0)
                            {
                                elementApr.Add(lectTotal);
                            }
                            else
                            {
                                elementApr.Add(null);
                            }

                            // Итог - разница
                            elementApr.Add(factTotal - lectTotal);

                            list.Add(elementApr.ToArray());
                        }
                    }

                    return ResultService<List<object[]>>.Success(list);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<object[]>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<object[]>> GetListAPRE(AcademicYearGetBindingModel modelYear, AcademicPlanRecordGetBindingModel modelPlanRecord)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    List<object[]> list = new List<object[]>();

                    var timeNorms = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == modelYear.Id).OrderBy(x => x.TimeNormOrder);

                    foreach (var timeNorm in timeNorms)
                    {
                        List<object> element = new List<object>();

                        var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == modelPlanRecord.Id && x.TimeNormId == timeNorm.Id && !x.IsDeleted);
                        if (apre != null)
                        {
                            element.Add(apre.Id);
                            element.Add(timeNorm.Id);
                            element.Add(timeNorm.TimeNormName);
                            if (apre.PlanHours != 0)
                            {
                                element.Add(apre.PlanHours.ToString("#.0"));
                            }
                            else
                            {
                                element.Add(null);
                            }
                            if (apre.FactHours != 0)
                            {
                                element.Add(apre.FactHours);
                            }
                            else
                            {
                                element.Add(null);
                            }
                        }
                        else
                        {
                            element.Add(null);
                            element.Add(timeNorm.Id);
                            element.Add(timeNorm.TimeNormName);
                            element.Add(null);
                            element.Add(null);
                        }

                        list.Add(element.ToArray());
                    }

                    return ResultService<List<object[]>>.Success(list);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<object[]>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<object[]>> GetListAPRM(AcademicYearGetBindingModel modelYear, AcademicPlanRecordGetBindingModel modelPlanRecord, LecturerGetBindingModel modelLecturer)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    List<object[]> list = new List<object[]>();

                    var timeNorms = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == modelYear.Id).OrderBy(x => x.TimeNormOrder);

                    foreach (var timeNorm in timeNorms)
                    {
                        List<object> element = new List<object>();

                        var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == modelPlanRecord.Id && x.TimeNormId == timeNorm.Id && !x.IsDeleted);
                        if (apre != null)
                        {
                            element.Add(apre.Id);
                            var aprm = context.AcademicPlanRecordMissions.FirstOrDefault(x => x.AcademicPlanRecordElementId == apre.Id && x.LecturerId == modelLecturer.Id && !x.IsDeleted);
                            if (aprm != null)
                            {
                                element.Add(aprm.Id);
                            }
                            else
                            {
                                element.Add(null);
                            }
                            element.Add(timeNorm.Id);
                            element.Add(timeNorm.TimeNormName);
                            if (apre.PlanHours != 0)
                            {
                                element.Add(apre.PlanHours.ToString("#.0"));
                            }
                            else
                            {
                                element.Add(null);
                            }
                            if (apre.FactHours != 0)
                            {
                                element.Add(apre.FactHours);
                            }
                            else
                            {
                                element.Add(null);
                            }
                            if (aprm != null && aprm.Hours != 0)
                            {
                                element.Add(aprm.Hours);
                            }
                            else
                            {
                                element.Add(null);
                            }

                            list.Add(element.ToArray());
                        }

                    }

                    return ResultService<List<object[]>>.Success(list);
                }
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    var timeNorms = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id).OrderBy(x => x.TimeNormOrder).ToList();
                    foreach (var tn in timeNorms)
                    {
                        var apres = context.AcademicPlanRecordElements
                            .Include(x => x.AcademicPlanRecord)
                            .Include(x => x.AcademicPlanRecord.Contingent)
                            .Include(x => x.AcademicPlanRecord.Discipline)
                            .Include(x => x.AcademicPlanRecord.AcademicPlan)
                            .Where(x => x.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.Id && x.TimeNormId == tn.Id && !x.IsDeleted &&
                                        x.AcademicPlanRecord.IsSelected && !x.AcademicPlanRecord.IsParent).ToList();
                        foreach (var apre in apres)
                        {
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
                            #region Множитель 1 - количество объектов
                            decimal? countObject = null;
                            switch (tn.KindOfLoadType)
                            {
                                case KindOfLoadType.Поток:
                                    var stream = context.StreamLessonRecords.Include(x => x.StreamLesson).FirstOrDefault(x => x.StreamLesson.AcademicYearId == model.Id &&
                                                                                x.AcademicPlanRecordElementId == apre.Id && !x.IsDeleted);
                                    if (stream != null)
                                    {
                                        if (stream.IsMain)
                                        {
                                            countObject = 1;
                                        }
                                        else
                                        {
                                            if (stream.StreamLesson.StreamLessonHours < hours)
                                            {
                                                countObject = 1;
                                                hours = hours = stream.StreamLesson.StreamLessonHours;
                                            }
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
                            #region Множитель 3
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
                                        koef = apre.AcademicPlanRecord.Zet;
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
                                context.SaveChanges();
                            }
                            else
                            {
                                apre.FactHours = 0;
                            }
                        }
                    }

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordForDiciplinePageViewModel> GetAcademicPlanRecordsForDiscipline(AcademicPlanRecordsForDiciplineBindingModel model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                int countPages = 0;
                var query = context.AcademicPlanRecords.Where(ar => ar.AcademicPlan.AcademicYearId == model.AcademicYearId && ar.DisciplineId == model.DisciplineId && !ar.IsDeleted);
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
                    List = query.Select(AcademicYearModelFactoryToViewModel.CreateAcademicPlanRecordForDiciplineViewModel).ToList()
                };

                return ResultService<AcademicPlanRecordForDiciplinePageViewModel>.Success(result);
            }
        }

        #region Duplicate Academic Year
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
            using (var context = DepartmentUserManager.GetContext)
            {
                var aps = context.AcademicPlans.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();

                List<PropertyInfo> propInfos = typeof(AcademicPlan)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => !x.GetMethod.IsVirtual && x.PropertyType.Name != "List`1" && x.Name != "Id" && x.Name != "DateCreate" &&
                                        x.Name != "DateDelete" && x.Name != "IsDeleted").ToList();
                foreach (var ap in aps)
                {
                    AcademicPlan newAP = new AcademicPlan();
                    foreach (var propInfo in propInfos)
                    {
                        newAP.GetType().GetProperty(propInfo.Name).SetValue(newAP, ap.GetType().GetProperty(propInfo.Name).GetValue(ap, null), null);
                    }
                    newAP.AcademicYearId = model.ToAcademicPlanId;
                    context.AcademicPlans.Add(newAP);
                    context.SaveChanges();
                }
                // TODO дубликаты apr и apre
            }
        }

        private void DuplicateTimeNorms(EducationalProcessDuplicateAcademicYear model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var tns = context.TimeNorms.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();

                List<PropertyInfo> propInfos = typeof(TimeNorm)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => !x.GetMethod.IsVirtual && x.PropertyType.Name != "List`1" && x.Name != "Id" && x.Name != "DateCreate" &&
                                        x.Name != "DateDelete" && x.Name != "IsDeleted").ToList();
                foreach (var tn in tns)
                {
                    TimeNorm newTN = new TimeNorm();
                    foreach (var propInfo in propInfos)
                    {
                        newTN.GetType().GetProperty(propInfo.Name).SetValue(newTN, tn.GetType().GetProperty(propInfo.Name).GetValue(tn, null), null);
                    }
                    newTN.AcademicYearId = model.ToAcademicPlanId;
                    context.TimeNorms.Add(newTN);
                    context.SaveChanges();
                }
            }
        }

        private void DuplicateContingent(EducationalProcessDuplicateAcademicYear model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var cs = context.Contingents.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();

                List<PropertyInfo> propInfos = typeof(Contingent)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => !x.GetMethod.IsVirtual && x.PropertyType.Name != "List`1" && x.Name != "Id" && x.Name != "DateCreate" &&
                                        x.Name != "DateDelete" && x.Name != "IsDeleted").ToList();
                foreach (var c in cs)
                {
                    Contingent newC = new Contingent();
                    foreach (var propInfo in propInfos)
                    {
                        newC.GetType().GetProperty(propInfo.Name).SetValue(newC, c.GetType().GetProperty(propInfo.Name).GetValue(c, null), null);
                    }
                    newC.AcademicYearId = model.ToAcademicPlanId;
                    context.Contingents.Add(newC);
                    context.SaveChanges();
                }
            }
        }

        private void DuplicateSeasonDate(EducationalProcessDuplicateAcademicYear model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var sds = context.SeasonDates.Where(x => x.AcademicYearId == model.FromAcademicPlanId && !x.IsDeleted).ToList();

                List<PropertyInfo> propInfos = typeof(SeasonDates)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => !x.GetMethod.IsVirtual && x.PropertyType.Name != "List`1" && x.Name != "Id" && x.Name != "DateCreate" &&
                                        x.Name != "DateDelete" && x.Name != "IsDeleted").ToList();
                foreach (var sd in sds)
                {
                    SeasonDates newSD = new SeasonDates();
                    foreach (var propInfo in propInfos)
                    {
                        newSD.GetType().GetProperty(propInfo.Name).SetValue(newSD, sd.GetType().GetProperty(propInfo.Name).GetValue(sd, null), null);
                    }
                    newSD.AcademicYearId = model.ToAcademicPlanId;
                    context.SeasonDates.Add(newSD);
                    context.SaveChanges();
                }
            }
        }
        #endregion

        public ResultService CreateStreamsForAcademicYear(EducationalProcessCreateStreams model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var apreGroups = context.AcademicPlanRecordElements
                    .Include(x => x.AcademicPlanRecord)
                    .Include(x => x.AcademicPlanRecord.AcademicPlan)
                    .Include(x => x.AcademicPlanRecord.Discipline)
                    .Include(x => x.TimeNorm)
                    .Where(x => x.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId && x.TimeNorm.TimeNormName == "Лекция")
                    .OrderBy(x => x.PlanHours)
                    .ToList()
                    .GroupBy(x => new { x.AcademicPlanRecord.Discipline.DisciplineName, x.AcademicPlanRecord.Semester })
                    .Where(x => x.Count() > 1);
                    foreach (var apreGroup in apreGroups)
                    {
                        StreamLesson sl = context.StreamLessons.FirstOrDefault(x => x.StreamLessonName == apreGroup.Key.DisciplineName && x.Semester == apreGroup.Key.Semester &&
                                                                                    x.AcademicYearId == model.AcademicYearId);
                        if (sl == null)
                        {
                            sl = new StreamLesson
                            {
                                AcademicYearId = model.AcademicYearId,
                                StreamLessonName = apreGroup.Key.DisciplineName
                            };
                            context.StreamLessons.Add(sl);
                            context.SaveChanges();
                        }
                        if (sl.IsDeleted)
                        {
                            sl.IsDeleted = false;
                            sl.DateDelete = null;
                            context.SaveChanges();
                        }
                        var slrs = context.StreamLessonRecords.Where(x => x.StreamLessonId == sl.Id).ToList();
                        foreach (var slr in slrs)
                        {
                            slr.IsDeleted = true;
                            slr.DateDelete = DateTime.Now;
                        }
                        context.SaveChanges();
                        foreach (var apre in apreGroup)
                        {
                            bool isMain = apre == apreGroup.First();
                            if (isMain)
                            {
                                sl.StreamLessonHours = apre.PlanHours;
                            }
                            var slr = slrs.FirstOrDefault(x => x.AcademicPlanRecordElementId == apre.Id);
                            if (slr == null)
                            {
                                slr = new StreamLessonRecord
                                {
                                    AcademicPlanRecordElementId = apre.Id,
                                    IsMain = isMain,
                                    StreamLessonId = sl.Id
                                };
                                context.StreamLessonRecords.Add(slr);
                                context.SaveChanges();
                            }
                            else
                            {
                                if (slr.IsDeleted)
                                {
                                    slr.IsDeleted = false;
                                    slr.DateDelete = null;
                                }
                                slr.IsMain = isMain;
                                context.SaveChanges();
                            }
                        }
                    }
                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineTimeDistributions(AcademicYearGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Учебные_планы, AccessType.View, "Учебные планы");

                using (var context = DepartmentUserManager.GetContext)
                using (var transaction = context.Database.BeginTransaction())
                {
                    var timeNorms = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.Id
                        && (x.KindOfLoadName == "Лекционное занятие" || x.KindOfLoadName == "Практическое занятие" || x.KindOfLoadName == "Лабораторная работа")); //Получение трех норм времени для поиска 
                    var disciplineBlock = context.DisciplineBlocks.FirstOrDefault(x => x.Title.Contains("Дисциплины"));
                    // поиск записей учебных планов
                    var APR = context.AcademicPlanRecords
                            .Where(x => !x.IsDeleted && x.AcademicPlan.AcademicYearId == model.Id && x.Discipline.DisciplineBlockId == disciplineBlock.Id)
                            .Include(x => x.Discipline).Include(x => x.Contingent).Include(x => x.AcademicPlan);
                    foreach (var APRRecord in APR)
                    {
                        var studentGroups = context.StudentGroups
                                        .Where(x => !x.IsDeleted && x.EducationDirectionId == APRRecord.Contingent.EducationDirectionId && x.Course == APRRecord.Contingent.Course);
                        foreach (var studentGroup in studentGroups)
                        {
                            // ищем расчасовку
                            var dTD = context.DisciplineTimeDistributions.FirstOrDefault(x => x.AcademicPlanRecordId == APRRecord.Id && x.StudentGroupId == studentGroup.Id);
                            if (dTD == null)
                            {
                                dTD = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistribution(new DisciplineTimeDistributionSetBindingModel()
                                {
                                    AcademicPlanRecordId = APRRecord.Id,
                                    StudentGroupId = studentGroup.Id,
                                    Comment = "",
                                    CommentWishesOfTeacher = ""
                                });
                                context.DisciplineTimeDistributions.Add(dTD);
                                context.SaveChanges();
                            }
                            else if (dTD.IsDeleted)
                            {
                                dTD.IsDeleted = false;
                                context.SaveChanges();
                            }
                            // ищем информацию по нагрузкам
                            foreach (var timeNorm in timeNorms)
                            {
                                for (int i = 1; i < 3; ++i)
                                {
                                    // ищем для четной недели
                                    var dTDRecord = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.DisciplineTimeDistributionId == dTD.Id && x.TimeNormId == timeNorm.Id && x.WeekNumber == i);
                                    if (dTDRecord == null)
                                    {
                                        dTDRecord = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordSetBindingModel()
                                        {
                                            DisciplineTimeDistributionId = dTD.Id,
                                            TimeNormId = timeNorm.Id,
                                            WeekNumber = i,
                                            Hours = 0.00
                                        });
                                        context.DisciplineTimeDistributionRecords.Add(dTDRecord);
                                    }
                                    else if (dTDRecord.IsDeleted)
                                    {
                                        dTDRecord.IsDeleted = false;
                                        context.SaveChanges();
                                    }
                                    // ищем для нечетной недели
                                    dTDRecord = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.DisciplineTimeDistributionId == dTD.Id && x.TimeNormId == timeNorm.Id && x.WeekNumber == i + 8);
                                    if (dTDRecord == null)
                                    {
                                        dTDRecord = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordSetBindingModel()
                                        {
                                            DisciplineTimeDistributionId = dTD.Id,
                                            TimeNormId = timeNorm.Id,
                                            WeekNumber = i + 8,
                                            Hours = 0.00
                                        });
                                        context.DisciplineTimeDistributionRecords.Add(dTDRecord);
                                    }
                                    else if (dTDRecord.IsDeleted)
                                    {
                                        dTDRecord.IsDeleted = false;
                                        context.SaveChanges();
                                    }
                                }
                                // ищем информацию по аудиториям
                                var dTDCLassroom = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.DisciplineTimeDistributionId == dTD.Id && x.TimeNormId == timeNorm.Id);
                                if (dTDCLassroom == null)
                                {
                                    dTDCLassroom = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionClassroom(new DisciplineTimeDistributionClassroomSetBindingModel()
                                    {
                                        DisciplineTimeDistributionId = dTD.Id,
                                        TimeNormId = timeNorm.Id
                                    });
                                    context.DisciplineTimeDistributionClassrooms.Add(dTDCLassroom);
                                }
                                else if (dTDCLassroom.IsDeleted)
                                {
                                    dTDCLassroom.IsDeleted = false;
                                    context.SaveChanges();
                                }
                            }
                        }
                    }

                    transaction.Commit();
                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<LecturerDisciplineTimeDistribution>> GetLecturerDisciplineTimeDistributions(LecturerDisciplineTimeDistributionsBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Расчасовки, AccessType.View, "Расчасовки");

                using (var context = DepartmentUserManager.GetContext)
                {
                    List<LecturerDisciplineTimeDistribution> list = new List<LecturerDisciplineTimeDistribution>();

                    var user = context.DepartmentUsers.FirstOrDefault(x => x.Id == model.UserId);
                    if (user == null)
                    {
                        return ResultService<List<LecturerDisciplineTimeDistribution>>.Error("Error:", "Пользователь не найден", ResultServiceStatusCode.NotFound);
                    }
                    if (!user.LecturerId.HasValue)
                    {
                        return ResultService<List<LecturerDisciplineTimeDistribution>>.Error("Error:", "У пользователя нет аккаунта преподавателя", ResultServiceStatusCode.NotFound);
                    }

                    // выбираем нагрузку преподавателя в этом учебном году в этом семестре
                    var query = context.AcademicPlanRecordMissions.Where(x => !x.IsDeleted && x.LecturerId == user.LecturerId &&
                                                            x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId)
                                .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan).Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions);

                    var grahp = query.SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions).Distinct()
                                    .Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.AcademicPlanRecord.AcademicPlan.AcademicYear)
                                    .Include(x => x.StudentGroup).Include(x => x.StudentGroup.EducationDirection)
                                    .OrderBy(x => x.AcademicPlanRecord.Discipline.DisciplineName).ThenBy(x => x.StudentGroup.GroupName);

                    foreach (var elem in grahp)
                    {
                        var timeNorms = context.AcademicPlanRecordElements.Where(x => x.AcademicPlanRecordId == elem.AcademicPlanRecordId && !x.IsDeleted)
                                                        .Include(x => x.TimeNorm)
                                                        .Select(x => x.TimeNorm)
                                                        .ToList();

                        LecturerDisciplineTimeDistribution ldtd = new LecturerDisciplineTimeDistribution
                        {
                            DisciplineTimeDistributionId = elem.Id,
                            AcademicYear = elem.AcademicPlanRecord.AcademicPlan.AcademicYear.Title,
                            EducationDirection = $"{elem.StudentGroup.EducationDirection.Cipher} {elem.StudentGroup.EducationDirection.Title}",
                            Discipline = elem.AcademicPlanRecord.Discipline.DisciplineName,
                            Semestr = elem.AcademicPlanRecord.Semester.ToString(),
                            StudentGroup = elem.StudentGroup.GroupName,
                            Comment = elem.Comment,
                            CommentWishesOfTeacher = elem.CommentWishesOfTeacher,
                            LecturerDisciplineTimeDistributionElements = new List<LecturerDisciplineTimeDistributionElement>()
                        };

                        // Ведет ли преподаватель лекции
                        var timeNormLec = timeNorms.FirstOrDefault(y => y.TimeNormShortName == "Лек");
                        if (timeNormLec != null)
                        {
                            // объединение лекций по потокам
                            var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == elem.AcademicPlanRecordId && x.TimeNormId == timeNormLec.Id && !x.IsDeleted);
                            if (apre != null)
                            {
                                var stream = context.StreamLessonRecords.FirstOrDefault(x => x.AcademicPlanRecordElementId == apre.Id && x.StreamLesson.AcademicYearId == model.AcademicYearId && !x.IsDeleted);
                                if (stream != null)
                                {
                                    string comment = string.Format("объединить лекции с: {0}.", string.Join(",", context.StreamLessonRecords
                                            .Where(x => x.StreamLessonId == stream.StreamLessonId && x.Id != stream.Id && !x.IsDeleted)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions)
                                            .SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions)
                                            .Include(x => x.StudentGroup)
                                            .Select(x => x.StudentGroup.ToString())));
                                    if (string.IsNullOrEmpty(ldtd.Comment) || !ldtd.Comment.Contains(comment))
                                    {
                                        ldtd.Comment += comment;
                                    }
                                }
                            }
                            var lec = query.FirstOrDefault(x => x.AcademicPlanRecordElement.TimeNormId == timeNormLec.Id &&
                                                x.AcademicPlanRecordElement.AcademicPlanRecordId == elem.AcademicPlanRecordId);
                            if (lec != null)
                            {
                                var dtdrs = context.DisciplineTimeDistributionRecords.Where(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNormLec.Id && !x.IsDeleted).ToList();
                                var dtdc = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNormLec.Id && !x.IsDeleted);
                                ldtd.LecturerDisciplineTimeDistributionElements.Add(new LecturerDisciplineTimeDistributionElement
                                {
                                    DisciplineTimeDistributionRecordFirstWeekFirstHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Id,
                                    DisciplineTimeDistributionRecordFirstWeekFirstHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours,
                                    DisciplineTimeDistributionRecordSecondWeekFirstHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Id,
                                    DisciplineTimeDistributionRecordSecondWeekFirstHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours,
                                    DisciplineTimeDistributionRecordFirstWeekSecondHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Id,
                                    DisciplineTimeDistributionRecordFirstWeekSecondHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 9).Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Hours,
                                    DisciplineTimeDistributionRecordSecondWeekSecondHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Id,
                                    DisciplineTimeDistributionRecordSecondWeekSecondHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours,
                                    DisciplineTimeDistributionClassroomId = dtdc?.Id,
                                    DisciplineTimeDistributionClassroom = dtdc?.ClassroomDescription,
                                    TimeNorm = "Лекции",
                                    TotalSum = lec.AcademicPlanRecordElement.PlanHours
                                });
                            }
                        }

                        // Ведет ли преподаватель практики
                        var timeNormPrac = timeNorms.FirstOrDefault(y => y.TimeNormShortName == "Пр");
                        if (timeNormPrac != null)
                        {
                            // объединение практик по потокам
                            var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == elem.AcademicPlanRecordId && x.TimeNormId == timeNormPrac.Id && !x.IsDeleted);
                            if (apre != null)
                            {
                                var stream = context.StreamLessonRecords.FirstOrDefault(x => x.AcademicPlanRecordElementId == apre.Id && x.StreamLesson.AcademicYearId == model.AcademicYearId && !x.IsDeleted);
                                if (stream != null)
                                {
                                    string comment = string.Format("объединить практики с: {0}.", string.Join(",", context.StreamLessonRecords
                                            .Where(x => x.StreamLessonId == stream.StreamLessonId && x.Id != stream.Id && !x.IsDeleted)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions)
                                            .SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions)
                                            .Include(x => x.StudentGroup)
                                            .Select(x => x.StudentGroup.ToString())));
                                    if (string.IsNullOrEmpty(ldtd.Comment) || !ldtd.Comment.Contains(comment))
                                    {
                                        ldtd.Comment += comment;
                                    }
                                }
                            }

                            var prac = query.FirstOrDefault(x => x.AcademicPlanRecordElement.TimeNormId == timeNormPrac.Id &&
                                                x.AcademicPlanRecordElement.AcademicPlanRecordId == elem.AcademicPlanRecordId);
                            if (prac != null)
                            {
                                var dtdrs = context.DisciplineTimeDistributionRecords.Where(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNormPrac.Id && !x.IsDeleted).ToList();
                                var dtdc = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNormPrac.Id && !x.IsDeleted);
                                ldtd.LecturerDisciplineTimeDistributionElements.Add(new LecturerDisciplineTimeDistributionElement
                                {
                                    DisciplineTimeDistributionRecordFirstWeekFirstHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Id,
                                    DisciplineTimeDistributionRecordFirstWeekFirstHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours,
                                    DisciplineTimeDistributionRecordSecondWeekFirstHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Id,
                                    DisciplineTimeDistributionRecordSecondWeekFirstHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours,
                                    DisciplineTimeDistributionRecordFirstWeekSecondHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Id,
                                    DisciplineTimeDistributionRecordFirstWeekSecondHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 9).Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Hours,
                                    DisciplineTimeDistributionRecordSecondWeekSecondHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Id,
                                    DisciplineTimeDistributionRecordSecondWeekSecondHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours,
                                    DisciplineTimeDistributionClassroomId = dtdc?.Id,
                                    DisciplineTimeDistributionClassroom = dtdc?.ClassroomDescription,
                                    TimeNorm = "Практ. занятия, семинары",
                                    TotalSum = prac.AcademicPlanRecordElement.PlanHours
                                });
                            }
                        }

                        // Ведет ли преподаватель лабораторные
                        var timeNormLab = timeNorms.FirstOrDefault(y => y.TimeNormShortName == "Лаб");
                        if (timeNormLab != null)
                        {
                            // объединение лабораторные по потокам
                            var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == elem.AcademicPlanRecordId && x.TimeNormId == timeNormLab.Id && !x.IsDeleted);
                            if (apre != null)
                            {
                                var stream = context.StreamLessonRecords.FirstOrDefault(x => x.AcademicPlanRecordElementId == apre.Id && x.StreamLesson.AcademicYearId == model.AcademicYearId && !x.IsDeleted);
                                if (stream != null)
                                {
                                    string comment = string.Format("объединить лабораторные с: {0}.", string.Join(",", context.StreamLessonRecords
                                            .Where(x => x.StreamLessonId == stream.StreamLessonId && x.Id != stream.Id && !x.IsDeleted)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions)
                                            .SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions)
                                            .Include(x => x.StudentGroup)
                                            .Select(x => x.StudentGroup.ToString())));
                                    if (string.IsNullOrEmpty(ldtd.Comment) || !ldtd.Comment.Contains(comment))
                                    {
                                        ldtd.Comment += comment;
                                    }
                                }
                            }

                            var lab = query.FirstOrDefault(x => x.AcademicPlanRecordElement.TimeNormId == timeNormLab.Id &&
                                                x.AcademicPlanRecordElement.AcademicPlanRecordId == elem.AcademicPlanRecordId);
                            if (lab != null)
                            {
                                var dtdrs = context.DisciplineTimeDistributionRecords.Where(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNormLab.Id && !x.IsDeleted).ToList();
                                var dtdc = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNormLab.Id && !x.IsDeleted);
                                ldtd.LecturerDisciplineTimeDistributionElements.Add(new LecturerDisciplineTimeDistributionElement
                                {
                                    DisciplineTimeDistributionRecordFirstWeekFirstHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Id,
                                    DisciplineTimeDistributionRecordFirstWeekFirstHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours,
                                    DisciplineTimeDistributionRecordSecondWeekFirstHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Id,
                                    DisciplineTimeDistributionRecordSecondWeekFirstHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours,
                                    DisciplineTimeDistributionRecordFirstWeekSecondHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Id,
                                    DisciplineTimeDistributionRecordFirstWeekSecondHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 9).Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Hours,
                                    DisciplineTimeDistributionRecordSecondWeekSecondHalfId = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Id,
                                    DisciplineTimeDistributionRecordSecondWeekSecondHalf = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours == 0 ? null : dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours,
                                    DisciplineTimeDistributionClassroomId = dtdc?.Id,
                                    DisciplineTimeDistributionClassroom = dtdc?.ClassroomDescription,
                                    TimeNorm = "Лабораторные занятия",
                                    TotalSum = lab.AcademicPlanRecordElement.PlanHours
                                });
                            }
                        }

                        list.Add(ldtd);
                    }

                    return ResultService<List<LecturerDisciplineTimeDistribution>>.Success(list);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<LecturerDisciplineTimeDistribution>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService SetLecturerDisciplineTimeDistributions(LecturerDisciplineTimeDistributionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Расчасовки, AccessType.Change, "Расчасовки");

                using (var context = DepartmentUserManager.GetContext)
                using (var transaction = context.Database.BeginTransaction())
                {
                    var dtd = context.DisciplineTimeDistributions.FirstOrDefault(x => x.Id == model.DisciplineTimeDistributionId);
                    if (dtd == null)
                    {
                        return ResultService.Error("Error:", "Элемент расчасовка не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (dtd.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент расчасовка был удален", ResultServiceStatusCode.WasDelete);
                    }
                    dtd.Comment = model.Comment;
                    dtd.CommentWishesOfTeacher = model.CommentWishesOfTeacher;

                    context.SaveChanges();

                    foreach (var elem in model.LecturerDisciplineTimeDistributionElements)
                    {
                        var dtdClassroom = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.Id == elem.DisciplineTimeDistributionClassroomId);
                        if (dtdClassroom == null)
                        {
                            return ResultService.Error("Error:", "Элемент аудитория расчасовки не найден", ResultServiceStatusCode.NotFound);
                        }
                        else if (dtdClassroom.IsDeleted)
                        {
                            return ResultService.Error("Error:", "Элемент аудитория расчасовки был удален", ResultServiceStatusCode.WasDelete);
                        }
                        dtdClassroom.ClassroomDescription = elem.DisciplineTimeDistributionClassroom;
                        context.SaveChanges();

                        var tdtdRecordFirstFirst = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.Id == elem.DisciplineTimeDistributionRecordFirstWeekFirstHalfId);
                        if (tdtdRecordFirstFirst == null)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки не найден", ResultServiceStatusCode.NotFound);
                        }
                        else if (tdtdRecordFirstFirst.IsDeleted)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки был удален", ResultServiceStatusCode.WasDelete);
                        }
                        tdtdRecordFirstFirst.Hours = elem.DisciplineTimeDistributionRecordFirstWeekFirstHalf ?? 0;
                        context.SaveChanges();

                        var tdtdRecordSecondFirst = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.Id == elem.DisciplineTimeDistributionRecordSecondWeekFirstHalfId);
                        if (tdtdRecordSecondFirst == null)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки не найден", ResultServiceStatusCode.NotFound);
                        }
                        else if (tdtdRecordSecondFirst.IsDeleted)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки был удален", ResultServiceStatusCode.WasDelete);
                        }
                        tdtdRecordSecondFirst.Hours = elem.DisciplineTimeDistributionRecordSecondWeekFirstHalf ?? 0;
                        context.SaveChanges();

                        var tdtdRecordFirstSecond = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.Id == elem.DisciplineTimeDistributionRecordFirstWeekSecondHalfId);
                        if (tdtdRecordFirstSecond == null)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки не найден", ResultServiceStatusCode.NotFound);
                        }
                        else if (tdtdRecordFirstSecond.IsDeleted)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки был удален", ResultServiceStatusCode.WasDelete);
                        }
                        tdtdRecordFirstSecond.Hours = elem.DisciplineTimeDistributionRecordFirstWeekSecondHalf ?? 0;
                        context.SaveChanges();

                        var tdtdRecordSecondSecond = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.Id == elem.DisciplineTimeDistributionRecordSecondWeekSecondHalfId);
                        if (tdtdRecordSecondSecond == null)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки не найден", ResultServiceStatusCode.NotFound);
                        }
                        else if (tdtdRecordSecondSecond.IsDeleted)
                        {
                            return ResultService.Error("Error:", "Элемент часы расчасовки был удален", ResultServiceStatusCode.WasDelete);
                        }
                        tdtdRecordSecondSecond.Hours = elem.DisciplineTimeDistributionRecordSecondWeekSecondHalf ?? 0;
                        context.SaveChanges();
                    }

                    transaction.Commit();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService ImportDisciplineTimeDistributionsLecturers(ImportDisciplineTimeDistributionsBindingModel model)
        {
            return WordHelper.ImportDisciplineTimeDistributionsLecturers(model);
        }

        public ResultService CreateLecturerWorkloads(AcademicYearGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                using (var transaction = context.Database.BeginTransaction())
                {
                    DepartmentUserManager.CheckAccess(AccessOperation.Учебные_планы, AccessType.View, "Учебные планы");

                    var lecturers = context.Lecturers.Where(x => !x.IsDeleted).ToList();
                    foreach (var lecturer in lecturers)
                    {
                        var lecturerWorkload = context.LecturerWorkload.FirstOrDefault(x => x.LecturerId == lecturer.Id && x.AcademicYearId == model.Id);
                        if (lecturerWorkload == null)
                        {
                            lecturerWorkload = AcademicYearModelFacotryFromBindingModel.CreateLecturerWorkload(new LecturerWorkloadSetBindingModel
                            {
                                LecturerId = lecturer.Id,
                                AcademicYearId = model.Id.Value,
                                Workload = 0
                            });
                            context.LecturerWorkload.Add(lecturerWorkload);
                            context.SaveChanges();
                        }
                        else if (lecturerWorkload.IsDeleted)
                        {
                            lecturerWorkload.IsDeleted = false;
                            context.SaveChanges();
                        }
                    }

                    transaction.Commit();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService ImportLecturerWorkloads(ImportLecturerWorkloadBindingModel model)
        {
            return ExcelHelper.ImportLecturerWorkloads(model);
        }

        public ResultService CreateAllFindIndividualPlans(AcademicYearGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Индивидуальный_план, AccessType.Delete, "Индивидуальный план");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var kindOfWorks = context.IndividualPlanKindOfWorks.Where(x => !x.IsDeleted).ToList();
                    var lecturers = context.Lecturers.Where(x => !x.IsDeleted).ToList();
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var lecturer in lecturers)
                        {
                            var individualPlan = context.IndividualPlans.FirstOrDefault(x => x.LecturerId == lecturer.Id && x.AcademicYearId == model.Id);
                            if (individualPlan == null)
                            {
                                individualPlan = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlan(new IndividualPlanSetBindingModel
                                {
                                    AcademicYearId = model.Id.Value,
                                    LecturerId = lecturer.Id,
                                });
                                context.IndividualPlans.Add(individualPlan);
                                context.SaveChanges();
                            }
                            else if (individualPlan.IsDeleted)
                            {
                                individualPlan.IsDeleted = false;
                                context.SaveChanges();
                            }

                            var lecturersTimes = context.IndividualPlanRecords.Where(record => !record.IsDeleted && record.IndividualPlanId == individualPlan.Id);
                            foreach (var kindOfW in kindOfWorks)
                            {
                                var individualPlanRecord = context.IndividualPlanRecords.FirstOrDefault(x => x.IndividualPlanKindOfWorkId == kindOfW.Id && x.IndividualPlanId == individualPlan.Id);
                                if (lecturersTimes.FirstOrDefault(record => record.IndividualPlanKindOfWorkId == kindOfW.Id) == null)
                                {
                                    var entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanRecord(new IndividualPlanRecordSetBindingModel
                                    {
                                        IndividualPlanId = individualPlan.Id,
                                        IndividualPlanKindOfWorkId = kindOfW.Id,
                                        PlanAutumn = 0.0,
                                        PlanSpring = 0.0,
                                        FactAutumn = 0.0,
                                        FactSpring = 0.0

                                    });
                                    context.IndividualPlanRecords.Add(entity);
                                    context.SaveChanges();
                                }
                            }
                        }

                        transaction.Commit();
                        return ResultService.Success();
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordPageViewModel> GetOtherAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Учебные_планы, AccessType.View, "Учебные планы");

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var apId = context.AcademicPlanRecords.FirstOrDefault(x => x.Id == model.Id)?.AcademicPlanId;
                    var query = context.AcademicPlanRecords.Where(x => !x.IsDeleted && x.AcademicPlanId == apId);

                    query = query.OrderBy(x => x.Semester).ThenBy(x => x.Discipline.DisciplineName);

                    query = query
                        .Include(x => x.Discipline)
                        .Include(x => x.Contingent);

                    var result = new AcademicPlanRecordPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateAcademicPlanRecordViewModel).ToList()
                    };

                    return ResultService<AcademicPlanRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService ChangeAPRFromAPRE(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(AccessOperation.Учебные_планы, AccessType.View, "Учебные планы");

                    var apre = context.AcademicPlanRecordElements.FirstOrDefault(x => x.Id == model.Id);

                    if (apre == null)
                    {
                        return ResultService.Error("Error:", "запись элемента записи учебного плана не найдена", ResultServiceStatusCode.NotFound);
                    }

                    apre.AcademicPlanRecordId = model.AcademicPlanRecordId.Value;
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UseStreamRecord(UseStreamRecordBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                using (var transaction = context.Database.BeginTransaction())
                {
                    DepartmentUserManager.CheckAccess(AccessOperation.Учебные_планы, AccessType.View, "Учебные планы");

                    var mainRecord = context.StreamLessonRecords.FirstOrDefault(x => !x.IsDeleted && x.StreamLessonId == model.StreamLessonId && x.IsMain);
                    var newRecord = context.StreamLessonRecords.FirstOrDefault(x => !x.IsDeleted && x.Id == model.StreamLessonRecordId);

                    if (mainRecord != null)
                    {
                        mainRecord.IsMain = false;
                        context.SaveChanges();
                    }
                    newRecord.IsMain = true;
                    context.SaveChanges();

                    transaction.Commit();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}