using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class StudyProcessController : Controller
    {
        private static IStudyProcessService _serviceSP;

        private static IDisciplineBlockService _serviceDB;

        private static IDisciplineService _serviceD;

        private readonly BaseService _baseService;

        private readonly IAcademicYearService _serviceAY;

        private readonly IAcademicPlanService _serviceAP;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        private readonly IAcademicPlanRecordMissionService _serviceAPRM;

        private readonly ITimeNormService _serviceTN;

        private readonly IContingentService _serviceC;

        private readonly IStreamLessonService _serviceSL;

        private readonly IStreamLessonRecordService _serviceSLR;

        private readonly ILecturerWorkloadService _serviceLW;

        private const string defaultMenu = "AcademicPlans";

        public StudyProcessController(IStudyProcessService serviceSP, IDisciplineBlockService serviceDB, IDisciplineService serviceD,
             BaseService baseService, IAcademicYearService serviceAY, IAcademicPlanService serviceAP, IAcademicPlanRecordService serviceAPR,
             IAcademicPlanRecordElementService serviceAPRE, IAcademicPlanRecordMissionService serviceAPRM, ITimeNormService serviceTN,
             IContingentService serviceC, IStreamLessonService serviceSL, IStreamLessonRecordService serviceSLR,
             ILecturerWorkloadService serviceLW)
        {
            _serviceSP = serviceSP;
            _serviceDB = serviceDB;
            _serviceD = serviceD;
            _baseService = baseService;
            _serviceAY = serviceAY;
            _serviceAP = serviceAP;
            _serviceAPR = serviceAPR;
            _serviceAPRE = serviceAPRE;
            _serviceAPRM = serviceAPRM;
            _serviceTN = serviceTN;
            _serviceC = serviceC;
            _serviceSL = serviceSL;
            _serviceSLR = serviceSLR;
            _serviceLW = serviceLW;
        }

        public IActionResult Index(Guid? Id)
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Error");
            }

            AcademicYearViewModel academicYearView = new AcademicYearViewModel();

            if (Id == null)
            {
                Id = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel()).Result.List.LastOrDefault().Id;
            }
            if (Id != Guid.Empty)
            {
                var academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel { Id = Id });
                if (academicYear.Succeeded)
                {
                    academicYearView = academicYear.Result;
                }
            }

            return View(academicYearView);
        }

        public ActionResult Menu(Guid Id, string menuElement = defaultMenu)
        {
            List<SubmenuModel> menu = new List<SubmenuModel> {
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Академические планы",
                    ActionName = "AcademicPlans"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Потоки",
                    ActionName = "StreamLessons"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Нормы времени",
                    ActionName = "TimeNorms"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Контингент",
                    ActionName = "Contingents"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Нагрузка преподавателей",
                    ActionName = "LecturerWorkloads"
                }
            };

            menu.ForEach(x => { if (x.ActionName == menuElement) x.isActive = true; else { x.isActive = false; } });

            return PartialView(menu);
        }

        public ActionResult Table(Guid Id, string menuElement = defaultMenu)
        {
            List<string> tableHead = new List<string>();
            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            switch (menuElement)
            {
                case "AcademicPlans":
                    var names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanViewModel));
                    tableHead = names.displayNames;
                    actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                            (true, new MenuElementModel
                                {
                                    Name = "Создать контингент",
                                    Controller = "StudyProcess",
                                    Action = ""
                                }
                            ),
                            (false, new MenuElementModel
                                {
                                    Name = "Загрузить из xml",
                                    Controller = "StudyProcess",
                                    Action = "",
                                    AdditionalParameters = new Dictionary<string, string>{ {"ButtonName", "↥" } }
                                }
                            )
                    };

                    if (Id != Guid.Empty)
                    {
                        var academicPlans = _serviceAP.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = Id });
                        if (academicPlans.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(academicPlans.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "AcademicPlan":
                    names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanRecordViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var academicPlanRecords = _serviceAPR.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = Id });
                        var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
                        if (academicPlanRecords.Succeeded && academicPlan.Succeeded)
                        {
                            academicPlanRecords.Result.List = academicPlanRecords.Result.List
                                .Where(x =>
                                academicPlan.Result.AcademicCoursesStrings.Contains(((int)Enum.Parse(typeof(Semesters), x.Semester) / 2 + (int)Enum.Parse(typeof(Semesters), x.Semester) % 2).ToString()))
                                .ToList();
                            tableBody = _serviceSP.GetPropertiesValues(academicPlanRecords.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "AcademicPlanRecord":
                    names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanRecordElementViewModel));
                    tableHead = names.displayNames;
                    actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                            (true, new MenuElementModel
                                {
                                    Name = "Перенести в другую нагрузку",
                                    Controller = "StudyProcess",
                                    Action = ""
                                }
                            )
                    };

                    if (Id != Guid.Empty)
                    {
                        var academicPlanRecordElements = _serviceAPRE.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = Id });
                        if (academicPlanRecordElements.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(academicPlanRecordElements.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "AcademicPlanRecordElement":
                    names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanRecordMissionViewModel));
                    tableHead = names.displayNames;
                    var academicPlanRecordMissions = _serviceAPRM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel { AcademicPlanRecordElementId = Id });

                    if (Id != Guid.Empty)
                    {
                        if (academicPlanRecordMissions.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(academicPlanRecordMissions.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "StreamLessons":
                    names = _serviceSP.GetPropertiesNames(typeof(StreamLessonViewModel));
                    tableHead = names.displayNames;
                    actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                            (true, new MenuElementModel
                                {
                                    Name = "Создать потоки",
                                    Controller = "StudyProcess",
                                    Action = ""
                                }
                            )
                    };

                    if (Id != Guid.Empty)
                    {
                        var streamLessons = _serviceSL.GetStreamLessons(new StreamLessonGetBindingModel { AcademicYearId = Id });
                        if (streamLessons.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(streamLessons.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "StreamLesson":
                    names = _serviceSP.GetPropertiesNames(typeof(StreamLessonRecordViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var streamLessonRecords = _serviceSLR.GetStreamLessonRecords(new StreamLessonRecordGetBindingModel { StreamLessonId = Id });
                        if (streamLessonRecords.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(streamLessonRecords.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "TimeNorms":
                    names = _serviceSP.GetPropertiesNames(typeof(TimeNormViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var timeNorms = _serviceTN.GetTimeNorms(new TimeNormGetBindingModel { AcademicYearId = Id });
                        if (timeNorms.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(timeNorms.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "Contingents":
                    names = _serviceSP.GetPropertiesNames(typeof(ContingentViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var contingents = _serviceC.GetContingents(new ContingentGetBindingModel { AcademicYearId = Id });
                        if (contingents.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(contingents.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "LecturerWorkloads":
                    names = _serviceSP.GetPropertiesNames(typeof(LecturerWorkloadViewModel));
                    tableHead = names.displayNames;
                    actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                            (true, new MenuElementModel
                                {
                                    Name = "Создать нагрузку",
                                    Controller = "StudyProcess",
                                    Action = ""
                                }
                            )
                    };

                    if (Id != Guid.Empty)
                    {
                        var lecturerWorkloads = _serviceLW.GetLecturerWorkloads(new LecturerWorkloadGetBindingModel { AcademicYearId = Id });
                        if (lecturerWorkloads.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(lecturerWorkloads.Result.List, names.propertiesNames);
                        }
                    }
                    break;
            }
            ViewBag.menuElement = menuElement;
            return PartialView((tableHead, tableBody, actions, Id));
        }

        public IActionResult Create(Guid Id, string menuElement = "AcademicYear")
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Error");
            }

            switch (menuElement)
            {
                case "AcademicYear":
                    var academicYearView = new AcademicYearViewModel();
                    return View("Index", academicYearView);

                case "AcademicPlans":
                    var academicPlanView = new AcademicPlanViewModel() { AcademicYearId = Id, AcademicCourses = 0 };
                    var educationDirections = _baseService.GetEducationDirections();
                    var academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel() { Id = Id });
                    if (educationDirections != null && academicYear.Succeeded)
                    {
                        academicPlanView.AcademicYear = academicYear.Result.Title;
                        ViewBag.EducationDirections = educationDirections;
                        ViewBag.AcademicCourses = Enum.GetValues(typeof(AcademicCourse)).Cast<AcademicCourse>().ToList();
                        ViewBag.menuElement = "AcademicPlan";
                        return View("AcademicPlan", academicPlanView);
                    }
                    break;

                case "AcademicPlan":
                    var academicPlanRecordView = new AcademicPlanRecordViewModel() { AcademicPlanId = Id };
                    var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
                    var semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                    if (academicPlan.Succeeded)
                    {
                        var disciplines = _serviceD.GetDisciplines(new DisciplineGetBindingModel { });
                        var contingents = _serviceC.GetContingents(new ContingentGetBindingModel { AcademicPlanId = Id });
                        if (disciplines.Succeeded && contingents.Succeeded)
                        {
                            ViewBag.AcademicPlan = academicPlan.Result;
                            ViewBag.Disciplines = disciplines.Result.List;
                            ViewBag.Contingents = contingents.Result.List;
                            ViewBag.Semesters = semesters;
                            ViewBag.menuElement = "AcademicPlanRecord";
                            return View("AcademicPlanRecord", academicPlanRecordView);
                        }
                    }
                    break;

                case "AcademicPlanRecord":
                    var academicPlanRecordElementView = new AcademicPlanRecordElementViewModel() { AcademicPlanRecordId = Id };
                    var timeNorms = _serviceTN.GetTimeNorms(new TimeNormGetBindingModel { AcademicPlanRecordId = Id });
                    var academicPlanRecord = _serviceAPR.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = Id });
                    if (timeNorms.Succeeded && academicPlanRecord.Succeeded)
                    {
                        academicPlanRecordElementView.Discipline = academicPlanRecord.Result.Discipline;
                        ViewBag.TimeNorms = timeNorms.Result.List;
                        ViewBag.menuElement = "AcademicPlanRecordElement";
                        return View("AcademicPlanRecordElement", academicPlanRecordElementView);
                    }
                    break;

                case "AcademicPlanRecordElement":
                    var academicPlanRecordMissionView = new AcademicPlanRecordMissionViewModel() { AcademicPlanRecordElementId = Id };
                    var academicPlanRecordElement = _serviceAPRE.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = Id });
                    var lecturers = _baseService.GetLecturers();
                    if (academicPlanRecordElement.Succeeded && lecturers != null)
                    {
                        ViewBag.AcademicPlanRecordElement = academicPlanRecordElement.Result;
                        ViewBag.Lecturers = lecturers.OrderBy(x => x.LastName);
                        ViewBag.menuElement = "AcademicPlanRecordMission";
                        return View("AcademicPlanRecordMission", academicPlanRecordMissionView);
                    }
                    break;

                case "StreamLessons":
                    var streamLessonView = new StreamLessonViewModel() { AcademicYearId = Id };
                    academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel() { Id = Id });
                    if (academicYear.Succeeded)
                    {
                        streamLessonView.AcademicYear = academicYear.Result.Title;
                    }
                    semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                    ViewBag.Semesters = semesters;
                    ViewBag.menuElement = "StreamLesson";
                    return View("StreamLesson", streamLessonView);

                case "StreamLesson":
                    var streamLessonRecordView = new StreamLessonRecordViewModel() { StreamLessonId = Id };
                    var streamLesson = _serviceSL.GetStreamLesson(new StreamLessonGetBindingModel { Id = Id });
                    if (streamLesson.Succeeded)
                    {
                        var academicPlans = _serviceAP.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
                        if (academicPlans.Succeeded)
                        {
                            streamLessonRecordView.StreamLessonName = streamLesson.Result.StreamLessonName;
                            ViewBag.AcademicPlans = academicPlans.Result.List;
                            ViewBag.menuElement = "StreamLessonRecord";
                            return View("StreamLessonRecord", streamLessonRecordView);
                        }
                    }
                    break;

                case "TimeNorms":
                    var timeNormView = new TimeNormViewModel() { AcademicYearId = Id };
                    academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel() { Id = Id });
                    var disciplineBlocks = _serviceDB.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
                    var educationDirectionQualifications = Enum.GetValues(typeof(EducationDirectionQualification)).Cast<EducationDirectionQualification>().Select(v => v.ToString()).ToList();
                    var kindOfLoadTypes = Enum.GetValues(typeof(KindOfLoadType)).Cast<KindOfLoadType>().Select(v => v.ToString()).ToList();
                    var timeNormKoefs = Enum.GetValues(typeof(TimeNormKoef)).Cast<TimeNormKoef>().Select(v => v.ToString()).ToList();
                    if (disciplineBlocks.Succeeded && academicYear.Succeeded)
                    {
                        timeNormView.AcademicYear = academicYear.Result.Title;
                        ViewBag.DisciplineBlocks = disciplineBlocks.Result.List;
                        ViewBag.EducationDirectionQualifications = educationDirectionQualifications;
                        ViewBag.KindOfLoadTypes = kindOfLoadTypes;
                        ViewBag.TimeNormKoefs = timeNormKoefs;
                        ViewBag.menuElement = "TimeNorm";
                        return View("TimeNorm", timeNormView);
                    }
                    break;

                case "Contingents":
                    var contingentView = new ContingentViewModel() { AcademicYearId = Id };
                    academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel() { Id = Id });
                    educationDirections = _baseService.GetEducationDirections();
                    if (educationDirections != null && academicYear.Succeeded)
                    {
                        contingentView.AcademicYear = academicYear.Result.Title;
                        ViewBag.EducationDirections = educationDirections;
                        ViewBag.menuElement = "Contingent";
                        return View("Contingent", contingentView);
                    }
                    break;

                case "LecturerWorkloads":
                    var lecturerWorkloadView = new LecturerWorkloadViewModel() { AcademicYearId = Id };
                    academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel() { Id = Id });
                    lecturers = _baseService.GetLecturers();
                    if (lecturers != null && academicYear.Succeeded)
                    {
                        lecturerWorkloadView.AcademicYear = academicYear.Result.Title;
                        ViewBag.Lecturers = lecturers.OrderBy(x => x.LastName);
                        ViewBag.menuElement = "LecturerWorkload";
                        return View("LecturerWorkload", lecturerWorkloadView);
                    }
                    break;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid Id, string menuElement)
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Error");
            }

            switch (menuElement)
            {
                case "AcademicPlans":
                    var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
                    if (academicPlan.Succeeded)
                    {
                        var educationDirections = _baseService.GetEducationDirections();
                        if (educationDirections != null)
                        {
                            ViewBag.EducationDirections = educationDirections;
                            ViewBag.AcademicCourses = Enum.GetValues(typeof(AcademicCourse)).Cast<AcademicCourse>().ToList();
                            ViewBag.menuElement = "AcademicPlan";
                            return View("AcademicPlan", academicPlan.Result);
                        }
                    }
                    break;

                case "AcademicPlan":
                    var academicPlanRecord = _serviceAPR.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = Id });
                    if (academicPlanRecord.Succeeded)
                    {
                        academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = academicPlanRecord.Result.AcademicPlanId });
                        if (academicPlan.Succeeded)
                        {
                            var disciplines = _serviceD.GetDisciplines(new DisciplineGetBindingModel { });
                            var contingents = _serviceC.GetContingents(new ContingentGetBindingModel { AcademicPlanId = academicPlan.Result.Id });
                            var semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                            if (disciplines.Succeeded && contingents.Succeeded)
                            {
                                ViewBag.AcademicPlan = academicPlan.Result;
                                ViewBag.Disciplines = disciplines.Result.List;
                                ViewBag.Contingents = contingents.Result.List;
                                ViewBag.Semesters = semesters;
                                ViewBag.menuElement = "AcademicPlanRecord";
                                return View("AcademicPlanRecord", academicPlanRecord.Result);
                            }
                        }
                    }
                    break;

                case "AcademicPlanRecord":
                    var academicPlanRecordElement = _serviceAPRE.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = Id });
                    if (academicPlanRecordElement.Succeeded)
                    {
                        var timeNorms = _serviceTN.GetTimeNorms(new TimeNormGetBindingModel { AcademicPlanRecordId = academicPlanRecordElement.Result.AcademicPlanRecordId });
                        if (timeNorms.Succeeded)
                        {
                            ViewBag.TimeNorms = timeNorms.Result.List;
                            ViewBag.menuElement = "AcademicPlanRecordElement";
                            return View("AcademicPlanRecordElement", academicPlanRecordElement.Result);
                        }
                    }
                    break;

                case "AcademicPlanRecordElement":
                    var academicPlanRecordMission = _serviceAPRM.GetAcademicPlanRecordMission(new AcademicPlanRecordMissionGetBindingModel { Id = Id });
                    if (academicPlanRecordMission.Succeeded)
                    {
                        academicPlanRecordElement = _serviceAPRE.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = academicPlanRecordMission.Result.AcademicPlanRecordElementId });
                        var lecturers = _baseService.GetLecturers();
                        if (academicPlanRecordElement.Succeeded && lecturers != null)
                        {
                            ViewBag.AcademicPlanRecordElement = academicPlanRecordElement.Result;
                            ViewBag.Lecturers = lecturers.OrderBy(x => x.LastName);
                            ViewBag.menuElement = "AcademicPlanRecordMission";
                            return View("AcademicPlanRecordMission", academicPlanRecordMission.Result);
                        }
                    }
                    break;

                case "StreamLessons":
                    var streamLesson = _serviceSL.GetStreamLesson(new StreamLessonGetBindingModel { Id = Id });
                    if (streamLesson.Succeeded)
                    {
                        var semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                        ViewBag.Semesters = semesters;
                        ViewBag.menuElement = "StreamLesson";
                        return View("StreamLesson", streamLesson.Result);
                    }
                    break;

                case "StreamLesson":
                    var streamLessonRecord = _serviceSLR.GetStreamLessonRecord(new StreamLessonRecordGetBindingModel { Id = Id });
                    if (streamLessonRecord.Succeeded)
                    {
                        streamLesson = _serviceSL.GetStreamLesson(new StreamLessonGetBindingModel { Id = streamLessonRecord.Result.StreamLessonId });
                        if (streamLesson.Succeeded)
                        {
                            var academicPlans = _serviceAP.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
                            if (academicPlans.Succeeded)
                            {
                                ViewBag.AcademicPlans = academicPlans.Result.List;
                                ViewBag.menuElement = "StreamLessonRecord";
                                return View("StreamLessonRecord", streamLessonRecord.Result);
                            }
                        }
                    }
                    break;

                case "TimeNorms":
                    var timeNorm = _serviceTN.GetTimeNorm(new TimeNormGetBindingModel { Id = Id });
                    if (timeNorm.Succeeded)
                    {
                        var disciplineBlocks = _serviceDB.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
                        var educationDirectionQualifications = Enum.GetValues(typeof(EducationDirectionQualification)).Cast<EducationDirectionQualification>().Select(v => v.ToString()).ToList();
                        var kindOfLoadTypes = Enum.GetValues(typeof(KindOfLoadType)).Cast<KindOfLoadType>().Select(v => v.ToString()).ToList();
                        var timeNormKoefs = Enum.GetValues(typeof(TimeNormKoef)).Cast<TimeNormKoef>().Select(v => v.ToString()).ToList();
                        if (disciplineBlocks.Succeeded)
                        {
                            ViewBag.DisciplineBlocks = disciplineBlocks.Result.List;
                            ViewBag.EducationDirectionQualifications = educationDirectionQualifications;
                            ViewBag.KindOfLoadTypes = kindOfLoadTypes;
                            ViewBag.TimeNormKoefs = timeNormKoefs;
                            ViewBag.menuElement = "TimeNorm";
                            return View("TimeNorm", timeNorm.Result);
                        }
                    }
                    break;

                case "Contingents":
                    var contingent = _serviceC.GetContingent(new ContingentGetBindingModel { Id = Id });
                    if (contingent.Succeeded)
                    {
                        var educationDirections = _baseService.GetEducationDirections();
                        if (educationDirections != null)
                        {
                            ViewBag.EducationDirections = educationDirections;
                            ViewBag.menuElement = "Contingent";
                            return View("Contingent", contingent.Result);
                        }
                    }
                    break;

                case "LecturerWorkloads":
                    var lecturerWorkload = _serviceLW.GetLecturerWorkload(new LecturerWorkloadGetBindingModel { Id = Id });
                    if (lecturerWorkload.Succeeded)
                    {
                        var lecturers = _baseService.GetLecturers();
                        if (lecturers != null)
                        {
                            ViewBag.Lecturers = lecturers.OrderBy(x => x.LastName);
                            ViewBag.menuElement = "LecturerWorkload";
                            return View("LecturerWorkload", lecturerWorkload.Result);
                        }
                    }
                    break;
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public void Delete(Guid Id, string menuElement)
        {
            ResultService result = new ResultService();
            switch (menuElement)
            {
                case "AcademicPlans":
                    result = _serviceAP.DeleteAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
                    break;

                case "AcademicPlan":
                    result = _serviceAPR.DeleteAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = Id });
                    break;

                case "AcademicPlanRecord":
                    result = _serviceAPRE.DeleteAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = Id });
                    break;

                case "AcademicPlanRecordElement":
                    result = _serviceAPRM.DeleteAcademicPlanRecordMission(new AcademicPlanRecordMissionGetBindingModel { Id = Id });
                    break;

                case "StreamLessons":
                    result = _serviceSL.DeleteStreamLesson(new StreamLessonGetBindingModel { Id = Id });
                    break;

                case "StreamLesson":
                    result = _serviceSLR.DeleteStreamLessonRecord(new StreamLessonRecordGetBindingModel { Id = Id });
                    break;

                case "TimeNorms":
                    result = _serviceTN.DeleteTimeNorm(new TimeNormGetBindingModel { Id = Id });
                    break;

                case "Contingents":
                    result = _serviceC.DeleteContingent(new ContingentGetBindingModel { Id = Id });
                    break;

                case "LecturerWorkloads":
                    result = _serviceLW.DeleteLecturerWorkload(new LecturerWorkloadGetBindingModel { Id = Id });
                    break;
            }

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public IActionResult AcademicYear(AcademicYearSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceAY.CreateAcademicYear(new AcademicYearSetBindingModel
                {
                    Title = model.Title
                });
            }
            else
            {
                result = _serviceAY.UpdateAcademicYear(new AcademicYearSetBindingModel
                {
                    Id = model.Id,
                    Title = model.Title
                });
            }
            if (result.Succeeded)
            {
                if (result.Result != null)
                {
                    if (result.Result is Guid)
                    {
                        model.Id = (Guid)result.Result;
                    }
                }
            }
            else
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }

            return Json(model.Id);
            // return RedirectToAction("Index", new { Id = model.Id });
        }

        [HttpPost]
        public void AcademicPlan(AcademicPlanSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceAP.CreateAcademicPlan(new AcademicPlanSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    AcademicCourses = model.AcademicCourses

                });
            }
            else
            {
                result = _serviceAP.UpdateAcademicPlan(new AcademicPlanSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    AcademicCourses = model.AcademicCourses
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void AcademicPlanRecord(AcademicPlanRecordSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceAPR.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                {
                    AcademicPlanId = model.AcademicPlanId,
                    DisciplineId = model.DisciplineId,
                    ContingentId = model.ContingentId,
                    Semester = model.Semester,
                    Zet = model.Zet,
                    IsUseInWorkload = model.IsUseInWorkload,
                    InDepartment = model.InDepartment,
                    IsActiveSemester = model.IsActiveSemester,

                    IsChild = model.IsChild,
                    IsParent = model.IsParent,
                    IsFacultative = model.IsFacultative
                });
            }
            else
            {
                result = _serviceAPR.UpdateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanId = model.AcademicPlanId,
                    DisciplineId = model.DisciplineId,
                    ContingentId = model.ContingentId,
                    Semester = model.Semester,
                    Zet = model.Zet,
                    IsUseInWorkload = model.IsUseInWorkload,
                    InDepartment = model.InDepartment,
                    IsActiveSemester = model.IsActiveSemester,

                    IsChild = model.IsChild,
                    IsParent = model.IsParent,
                    IsFacultative = model.IsFacultative
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void AcademicPlanRecordElement(AcademicPlanRecordElementSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceAPRE.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                {
                    AcademicPlanRecordId = model.AcademicPlanRecordId,
                    TimeNormId = model.TimeNormId,
                    PlanHours = model.PlanHours,
                    FactHours = model.FactHours

                });
            }
            else
            {
                result = _serviceAPRE.UpdateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanRecordId = model.AcademicPlanRecordId,
                    TimeNormId = model.TimeNormId,
                    PlanHours = model.PlanHours,
                    FactHours = model.FactHours
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void AcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceAPRM.CreateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                {
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    LecturerId = model.LecturerId,
                    Hours = model.Hours


                });
            }
            else
            {
                result = _serviceAPRM.UpdateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    LecturerId = model.LecturerId,
                    Hours = model.Hours
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void StreamLesson(StreamLessonSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSL.CreateStreamLesson(new StreamLessonSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    StreamLessonName = model.StreamLessonName,
                    StreamLessonHours = model.StreamLessonHours,
                    Semester = model.Semester
                });
            }
            else
            {
                result = _serviceSL.UpdateStreamLesson(new StreamLessonSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    StreamLessonName = model.StreamLessonName,
                    StreamLessonHours = model.StreamLessonHours,
                    Semester = model.Semester
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void StreamLessonRecord(StreamLessonRecordSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSLR.CreateStreamLessonRecord(new StreamLessonRecordSetBindingModel
                {
                    StreamLessonId = model.StreamLessonId,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    IsMain = model.IsMain
                });
            }
            else
            {
                result = _serviceSLR.UpdateStreamLessonRecord(new StreamLessonRecordSetBindingModel
                {
                    Id = model.Id,
                    StreamLessonId = model.StreamLessonId,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    IsMain = model.IsMain
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void TimeNorm(TimeNormSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceTN.CreateTimeNorm(new TimeNormSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    DisciplineBlockId = model.DisciplineBlockId,
                    TimeNormName = model.TimeNormName,
                    TimeNormShortName = model.TimeNormShortName,
                    TimeNormOrder = model.TimeNormOrder,
                    TimeNormEducationDirectionQualification = model.TimeNormEducationDirectionQualification,
                    KindOfLoadName = model.KindOfLoadName,
                    KindOfLoadAttributeName = model.KindOfLoadAttributeName,
                    KindOfLoadBlueAsteriskName = model.KindOfLoadBlueAsteriskName,
                    KindOfLoadBlueAsteriskAttributeName = model.KindOfLoadBlueAsteriskAttributeName,
                    KindOfLoadBlueAsteriskPracticName = model.KindOfLoadBlueAsteriskPracticName,
                    KindOfLoadType = model.KindOfLoadType,
                    Hours = model.Hours,
                    NumKoef = model.NumKoef,
                    TimeNormKoef = model.TimeNormKoef,
                    UseInLearningProgress = model.UseInLearningProgress,
                    UseInSite = model.UseInSite
                });
            }
            else
            {
                result = _serviceTN.UpdateTimeNorm(new TimeNormSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    DisciplineBlockId = model.DisciplineBlockId,
                    TimeNormName = model.TimeNormName,
                    TimeNormShortName = model.TimeNormShortName,
                    TimeNormOrder = model.TimeNormOrder,
                    TimeNormEducationDirectionQualification = model.TimeNormEducationDirectionQualification,
                    KindOfLoadName = model.KindOfLoadName,
                    KindOfLoadAttributeName = model.KindOfLoadAttributeName,
                    KindOfLoadBlueAsteriskName = model.KindOfLoadBlueAsteriskName,
                    KindOfLoadBlueAsteriskAttributeName = model.KindOfLoadBlueAsteriskAttributeName,
                    KindOfLoadBlueAsteriskPracticName = model.KindOfLoadBlueAsteriskPracticName,
                    KindOfLoadType = model.KindOfLoadType,
                    Hours = model.Hours,
                    NumKoef = model.NumKoef,
                    TimeNormKoef = model.TimeNormKoef,
                    UseInLearningProgress = model.UseInLearningProgress,
                    UseInSite = model.UseInSite
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void Contingent(ContingentSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceC.CreateContingent(new ContingentSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    ContingentName = model.ContingentName,
                    Course = (int)Math.Pow(2.0, Convert.ToDouble(model.Course) - 1.0),
                    CountGroups = model.CountGroups,
                    CountStudents = model.CountStudents,
                    CountSubgroups = model.CountSubgroups
                });
            }
            else
            {
                result = _serviceC.UpdateContingent(new ContingentSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    ContingentName = model.ContingentName,
                    Course = (int)Math.Pow(2.0, Convert.ToDouble(model.Course) - 1.0),
                    CountGroups = model.CountGroups,
                    CountStudents = model.CountStudents,
                    CountSubgroups = model.CountSubgroups
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void LecturerWorkload(LecturerWorkloadSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceLW.CreateLecturerWorkload(new LecturerWorkloadSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    LecturerId = model.LecturerId,
                    Workload = model.Workload
                });
            }
            else
            {
                result = _serviceLW.UpdateLecturerWorkload(new LecturerWorkloadSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    LecturerId = model.LecturerId,
                    Workload = model.Workload
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public JsonResult GetAcademicPlanRecords(Guid AcademicPlanId)
        {
            var academicPlanRecords = _serviceAPR.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = AcademicPlanId });
            if (academicPlanRecords.Succeeded)
            {
                return Json(academicPlanRecords.Result.List);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult GetAcademicPlanRecordElements(Guid AcademicPlanRecordId)
        {
            var academicPlanRecordElements = _serviceAPRE.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = AcademicPlanRecordId });
            if (academicPlanRecordElements.Succeeded)
            {
                return Json(academicPlanRecordElements.Result.List);
            }
            return Json(null);
        }
    }
}
