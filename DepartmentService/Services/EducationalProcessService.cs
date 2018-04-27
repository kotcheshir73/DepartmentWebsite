using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.Enums;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
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

                    var kindOfLoad = _context.KindOfLoads.FirstOrDefault(x => x.KindOfLoadName == discpName);

                    var recordelement = _context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                        apre.AcademicPlanRecordId == record.Id &&
                        apre.KindOfLoadId == kindOfLoad.Id &&
                        !apre.IsDeleted);
                    if (recordelement == null)
                    {
                        _context.AcademicPlanRecordElements.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                        {
                            AcademicPlanRecordId = record.Id,
                            KindOfLoadId = kindOfLoad.Id,
                            Hours = 1
                        }));
                    }
                    _context.SaveChanges();
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
            foreach (KindOfLoad kindOfLoadElem in _context.KindOfLoads)
            {
                XmlNode elemNode = elementSemNodeAttributes.GetNamedItem(kindOfLoadElem.AttributeName);
                if (elemNode != null)
                {
                    int hours = Convert.ToInt32(elemNode.Value);
                    var recordelement = _context.AcademicPlanRecordElements.FirstOrDefault(apre =>
                        apre.AcademicPlanRecordId == apreId &&
                        apre.KindOfLoadId == kindOfLoadElem.Id &&
                        !apre.IsDeleted);
                    if (recordelement == null)
                    {
                        _context.AcademicPlanRecordElements.Add(ModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                        {
                            AcademicPlanRecordId = apreId,
                            KindOfLoadId = kindOfLoadElem.Id,
                            Hours = hours
                        }));
                    }
                    else
                    {
                        recordelement.Hours = hours;
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
                    if (academicPlan.AcademicLevel == AcademicLevel.Бакалавриат)
                    {
                        cource = AcademicCourse.Course_1;
                    }
                    else if (academicPlan.AcademicLevel == AcademicLevel.Магистратура)
                    {
                        cource = AcademicCourse.Course_5;
                    }
                    break;
                case Semesters.Третий:
                case Semesters.Четвертый:
                    if (academicPlan.AcademicLevel == AcademicLevel.Бакалавриат)
                    {
                        cource = AcademicCourse.Course_2;
                    }
                    else if (academicPlan.AcademicLevel == AcademicLevel.Магистратура)
                    {
                        cource = AcademicCourse.Course_6;
                    }
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
            var contingent = _context.Contingents.FirstOrDefault(x => x.EducationDirectionId == academicPlan.EducationDirectionId && x.Course == cource);
            if(contingent == null)
            {
                throw new Exception(string.Format("Не найден контингент на направление {0} курс {1}", academicPlan.EducationDirection.Cipher, cource));
            }
            return contingent;
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
                            {/*// идем по записям учебного плана
                                var timeNorms = _context.TimeNorms.Where(tn => tn.KindOfLoadId == apRecord.KindOfLoadId);
                                if (timeNorms.Count() == 0)
                                {
                                    transaction.Rollback();
                                    return ResultService.Error("not_found", string.Format("Для вида нагрузок {0} отсуствуют нормы времени",
                                        _context.KindOfLoads.Single(kl => kl.Id == apRecord.KindOfLoadId).KindOfLoadName), ResultServiceStatusCode.NotFound);
                                }
                               foreach (var timeNorm in timeNorms)
                                { //получаем список норм времени, привязанных к виду нагрузки по записи учебного плана.
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
                                }*/
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
                /*var apR = apRecords.FirstOrDefault(kol => kol.KindOfLoad.KindOfLoadName.Contains(kindOfLoadName));
                if (apR != null)
                {
                    load *= apR.Hours;
                }*/
            }

            return load;
        }
        #endregion

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
    }
}
