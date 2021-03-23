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
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class StudyProcessController : Controller
    {
        private static IWebStudyProcessService _serviceSP;

        private static IDisciplineBlockService _serviceDB;

        private static IDisciplineService _serviceD;

        private readonly BaseService _baseService;

        private const string defaultMenu = "AcademicPlans";

        public StudyProcessController(IWebStudyProcessService serviceSP, IDisciplineBlockService serviceDB,
            IDisciplineService serviceD, BaseService baseService)
        {
            _serviceSP = serviceSP;
            _serviceDB = serviceDB;
            _serviceD = serviceD;
            _baseService = baseService;
        }

        public IActionResult Index(Guid? Id)
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Error");
            }

            WebAcademicYearViewModel academicYearView = new WebAcademicYearViewModel();

            if (Id == null)
            {
                Id = _serviceSP.GetAcademicYears(new WebAcademicYearGetBindingModel { }).Result.List.LastOrDefault().Id;
            }
            if (Id != Guid.Empty)
            {
                var academicYear = _serviceSP.GetAcademicYear(new WebAcademicYearGetBindingModel { Id = Id });
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
            List<MenuElementModel> actions = new List<MenuElementModel>();

            switch (menuElement)
            {
                case "AcademicPlans":
                    var names = _serviceSP.GetPropertiesNames(typeof(WebAcademicPlanViewModel));
                    tableHead = names.displayNames;
                    actions = new List<MenuElementModel> {
                            new MenuElementModel
                            {
                                Name = "Создать контингент",
                                Controller = "StudyProcess",
                                Action = ""
                            },
                            new MenuElementModel
                            {
                                Name = "Загрузить из xml",
                                Controller = "StudyProcess",
                                Action = ""
                            },
                            new MenuElementModel
                            {
                                Name = "Загрузить из xml (синяя звездочка)",
                                Controller = "StudyProcess",
                                Action = ""
                            }};

                    if (Id != Guid.Empty)
                    {
                        var academicPlans = _serviceSP.GetAcademicPlans(new WebAcademicPlanGetBindingModel { AcademicYearId = Id });
                        if (academicPlans.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(academicPlans.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "AcademicPlan":
                    names = _serviceSP.GetPropertiesNames(typeof(WebAcademicPlanRecordViewModel));
                    tableHead = names.displayNames;
                    actions = new List<MenuElementModel> {
                            new MenuElementModel
                            {
                                Name = "Загрузить из xml",
                                Controller = "StudyProcess",
                                Action = ""
                            },
                            new MenuElementModel
                            {
                                Name = "Загрузить из xml (синяя звездочка)",
                                Controller = "StudyProcess",
                                Action = ""
                            }};

                    if (Id != Guid.Empty)
                    {
                        var academicPlanRecords = _serviceSP.GetAcademicPlanRecords(new WebAcademicPlanRecordGetBindingModel { AcademicPlanId = Id });
                        if (academicPlanRecords.Succeeded)
                        {

                            tableBody = _serviceSP.GetPropertiesValues(academicPlanRecords.Result.List, names.propertiesNames);

                        }
                    }
                    break;

                case "AcademicPlanRecord":
                    names = _serviceSP.GetPropertiesNames(typeof(WebAcademicPlanRecordElementViewModel));
                    tableHead = names.displayNames;
                    actions = new List<MenuElementModel> {
                            new MenuElementModel
                            {
                                Name = "Перенести в другую нагрузку",
                                Controller = "StudyProcess",
                                Action = ""
                            }};

                    if (Id != Guid.Empty)
                    {
                        var academicPlanRecordElements = _serviceSP.GetAcademicPlanRecordElements(new WebAcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = Id });
                        if (academicPlanRecordElements.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(academicPlanRecordElements.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "AcademicPlanRecordElement":
                    names = _serviceSP.GetPropertiesNames(typeof(WebAcademicPlanRecordMissionViewModel));
                    tableHead = names.displayNames;
                    var academicPlanRecordMissions = _serviceSP.GetAcademicPlanRecordMissions(new WebAcademicPlanRecordMissionGetBindingModel { AcademicPlanRecordElementId = Id });

                    if (Id != Guid.Empty)
                    {
                        if (academicPlanRecordMissions.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(academicPlanRecordMissions.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "StreamLessons":
                    names = _serviceSP.GetPropertiesNames(typeof(WebStreamLessonViewModel));
                    tableHead = names.displayNames;
                    actions = new List<MenuElementModel> {
                            new MenuElementModel
                            {
                                Name = "Создать потоки",
                                Controller = "StudyProcess",
                                Action = ""
                            }};

                    if (Id != Guid.Empty)
                    {
                        var streamLessons = _serviceSP.GetStreamLessons(new WebStreamLessonGetBindingModel { AcademicYearId = Id });
                        if (streamLessons.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(streamLessons.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "StreamLesson":
                    names = _serviceSP.GetPropertiesNames(typeof(WebStreamLessonRecordViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var streamLessonRecords = _serviceSP.GetStreamLessonRecords(new WebStreamLessonRecordGetBindingModel { StreamLessonId = Id });
                        if (streamLessonRecords.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(streamLessonRecords.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "TimeNorms":
                    names = _serviceSP.GetPropertiesNames(typeof(WebTimeNormViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var timeNorms = _serviceSP.GetTimeNorms(new WebTimeNormGetBindingModel { AcademicYearId = Id });
                        if (timeNorms.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(timeNorms.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "Contingents":
                    names = _serviceSP.GetPropertiesNames(typeof(WebContingentViewModel));
                    tableHead = names.displayNames;

                    if (Id != Guid.Empty)
                    {
                        var contingents = _serviceSP.GetContingents(new WebContingentGetBindingModel { AcademicYearId = Id });
                        if (contingents.Succeeded)
                        {
                            tableBody = _serviceSP.GetPropertiesValues(contingents.Result.List, names.propertiesNames);
                        }
                    }
                    break;

                case "LecturerWorkloads":
                    names = _serviceSP.GetPropertiesNames(typeof(WebLecturerWorkloadViewModel));
                    tableHead = names.displayNames;
                    actions = new List<MenuElementModel> {
                            new MenuElementModel
                            {
                                Name = "Создать нагрузку",
                                Controller = "StudyProcess",
                                Action = ""
                            }};

                    if (Id != Guid.Empty)
                    {
                        var lecturerWorkloads = _serviceSP.GetLecturerWorkloads(new WebLecturerWorkloadGetBindingModel { AcademicYearId = Id });
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
                    var academicYearView = new WebAcademicYearViewModel();
                    return View("Index", academicYearView);

                case "AcademicPlans":
                    var academicPlanView = new WebAcademicPlanViewModel() { AcademicYearId = Id, AcademicCourses = 0 };
                    var educationDirections = _baseService.GetEducationDirections();
                    var academicYear = _serviceSP.GetAcademicYear(new WebAcademicYearGetBindingModel() { Id = Id });
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
                    var academicPlanRecordView = new WebAcademicPlanRecordViewModel() { AcademicPlanId = Id };
                    var academicPlan = _serviceSP.GetAcademicPlan(new WebAcademicPlanGetBindingModel { Id = Id });
                    var semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                    if (academicPlan.Succeeded)
                    {
                        var disciplines = _serviceD.GetDisciplines(new DisciplineGetBindingModel { });
                        var contingents = _serviceSP.GetContingents(new WebContingentGetBindingModel { AcademicPlanId = Id });
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
                    var academicPlanRecordElementView = new WebAcademicPlanRecordElementViewModel() { AcademicPlanRecordId = Id };
                    var timeNorms = _serviceSP.GetTimeNorms(new WebTimeNormGetBindingModel { AcademicPlanRecordId = Id });
                    var academicPlanRecord = _serviceSP.GetAcademicPlanRecord(new WebAcademicPlanRecordGetBindingModel { Id = Id });
                    if (timeNorms.Succeeded && academicPlanRecord.Succeeded)
                    {
                        academicPlanRecordElementView.Disciplne = academicPlanRecord.Result.Disciplne;
                        ViewBag.TimeNorms = timeNorms.Result.List;
                        ViewBag.menuElement = "AcademicPlanRecordElement";
                        return View("AcademicPlanRecordElement", academicPlanRecordElementView);
                    }
                    break;

                case "AcademicPlanRecordElement":
                    var academicPlanRecordMissionView = new WebAcademicPlanRecordMissionViewModel() { AcademicPlanRecordElementId = Id };
                    var academicPlanRecordElement = _serviceSP.GetAcademicPlanRecordElement(new WebAcademicPlanRecordElementGetBindingModel { Id = Id });
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
                    var streamLessonView = new WebStreamLessonViewModel() { AcademicYearId = Id };
                    academicYear = _serviceSP.GetAcademicYear(new WebAcademicYearGetBindingModel() { Id = Id });
                    if (academicYear.Succeeded)
                    {
                        streamLessonView.AcademicYear = academicYear.Result.Title;
                    }
                    semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                    ViewBag.Semesters = semesters;
                    ViewBag.menuElement = "StreamLesson";
                    return View("StreamLesson", streamLessonView);

                case "StreamLesson":
                    var streamLessonRecordView = new WebStreamLessonRecordViewModel() { StreamLessonId = Id };
                    var streamLesson = _serviceSP.GetStreamLesson(new WebStreamLessonGetBindingModel { Id = Id });
                    if (streamLesson.Succeeded)
                    {
                        var academicPlans = _serviceSP.GetAcademicPlans(new WebAcademicPlanGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
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
                    var timeNormView = new WebTimeNormViewModel() { AcademicYearId = Id };
                    academicYear = _serviceSP.GetAcademicYear(new WebAcademicYearGetBindingModel() { Id = Id });
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
                    var contingentView = new WebContingentViewModel() { AcademicYearId = Id };
                    academicYear = _serviceSP.GetAcademicYear(new WebAcademicYearGetBindingModel() { Id = Id });
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
                    var lecturerWorkloadView = new WebLecturerWorkloadViewModel() { AcademicYearId = Id };
                    academicYear = _serviceSP.GetAcademicYear(new WebAcademicYearGetBindingModel() { Id = Id });
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
                    var academicPlan = _serviceSP.GetAcademicPlan(new WebAcademicPlanGetBindingModel { Id = Id });
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
                    var academicPlanRecord = _serviceSP.GetAcademicPlanRecord(new WebAcademicPlanRecordGetBindingModel { Id = Id });
                    if (academicPlanRecord.Succeeded)
                    {
                        academicPlan = _serviceSP.GetAcademicPlan(new WebAcademicPlanGetBindingModel { Id = academicPlanRecord.Result.AcademicPlanId });
                        if (academicPlan.Succeeded)
                        {
                            var disciplines = _serviceD.GetDisciplines(new DisciplineGetBindingModel { });
                            var contingents = _serviceSP.GetContingents(new WebContingentGetBindingModel { AcademicPlanId = academicPlan.Result.Id });
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
                    var academicPlanRecordElement = _serviceSP.GetAcademicPlanRecordElement(new WebAcademicPlanRecordElementGetBindingModel { Id = Id });
                    if (academicPlanRecordElement.Succeeded)
                    {
                        var timeNorms = _serviceSP.GetTimeNorms(new WebTimeNormGetBindingModel { AcademicPlanRecordId = academicPlanRecordElement.Result.AcademicPlanRecordId });
                        if (timeNorms.Succeeded)
                        {
                            ViewBag.TimeNorms = timeNorms.Result.List;
                            ViewBag.menuElement = "AcademicPlanRecordElement";
                            return View("AcademicPlanRecordElement", academicPlanRecordElement.Result);
                        }
                    }
                    break;

                case "AcademicPlanRecordElement":
                    var academicPlanRecordMission = _serviceSP.GetAcademicPlanRecordMission(new WebAcademicPlanRecordMissionGetBindingModel { Id = Id });
                    if (academicPlanRecordMission.Succeeded)
                    {
                        academicPlanRecordElement = _serviceSP.GetAcademicPlanRecordElement(new WebAcademicPlanRecordElementGetBindingModel { Id = academicPlanRecordMission.Result.AcademicPlanRecordElementId });
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
                    var streamLesson = _serviceSP.GetStreamLesson(new WebStreamLessonGetBindingModel { Id = Id });
                    if (streamLesson.Succeeded)
                    {
                        var semesters = Enum.GetValues(typeof(Semesters)).Cast<Semesters>().Select(v => v.ToString()).ToList();
                        ViewBag.Semesters = semesters;
                        ViewBag.menuElement = "StreamLesson";
                        return View("StreamLesson", streamLesson.Result);
                    }
                    break;

                case "StreamLesson":
                    var streamLessonRecord = _serviceSP.GetStreamLessonRecord(new WebStreamLessonRecordGetBindingModel { Id = Id });
                    if (streamLessonRecord.Succeeded)
                    {
                        streamLesson = _serviceSP.GetStreamLesson(new WebStreamLessonGetBindingModel { Id = streamLessonRecord.Result.StreamLessonId });
                        if (streamLesson.Succeeded)
                        {
                            var academicPlans = _serviceSP.GetAcademicPlans(new WebAcademicPlanGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
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
                    var timeNorm = _serviceSP.GetTimeNorm(new WebTimeNormGetBindingModel { Id = Id });
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
                    var contingent = _serviceSP.GetContingent(new WebContingentGetBindingModel { Id = Id });
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
                    var lecturerWorkload = _serviceSP.GetLecturerWorkload(new WebLecturerWorkloadGetBindingModel { Id = Id });
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
                    result = _serviceSP.DeleteAcademicPlan(new WebAcademicPlanGetBindingModel { Id = Id });
                    break;

                case "AcademicPlan":
                    result = _serviceSP.DeleteAcademicPlanRecord(new WebAcademicPlanRecordGetBindingModel { Id = Id });
                    break;

                case "AcademicPlanRecord":
                    result = _serviceSP.DeleteAcademicPlanRecordElement(new WebAcademicPlanRecordElementGetBindingModel { Id = Id });
                    break;

                case "AcademicPlanRecordElement":
                    result = _serviceSP.DeleteAcademicPlanRecordMission(new WebAcademicPlanRecordMissionGetBindingModel { Id = Id });
                    break;

                case "StreamLessons":
                    result = _serviceSP.DeleteStreamLesson(new WebStreamLessonGetBindingModel { Id = Id });
                    break;

                case "StreamLesson":
                    result = _serviceSP.DeleteStreamLessonRecord(new WebStreamLessonRecordGetBindingModel { Id = Id });
                    break;

                case "TimeNorms":
                    result = _serviceSP.DeleteTimeNorm(new WebTimeNormGetBindingModel { Id = Id });
                    break;

                case "Contingents":
                    result = _serviceSP.DeleteContingent(new WebContingentGetBindingModel { Id = Id });
                    break;

                case "LecturerWorkloads":
                    result = _serviceSP.DeleteLecturerWorkload(new WebLecturerWorkloadGetBindingModel { Id = Id });
                    break;
            }

            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public IActionResult AcademicYear(WebAcademicYearSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateAcademicYear(new WebAcademicYearSetBindingModel
                {
                    Title = model.Title
                });
            }
            else
            {
                result = _serviceSP.UpdateAcademicYear(new WebAcademicYearSetBindingModel
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
        public void AcademicPlan(WebAcademicPlanSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateAcademicPlan(new WebAcademicPlanSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    AcademicCourses = model.AcademicCourses

                });
            }
            else
            {
                result = _serviceSP.UpdateAcademicPlan(new WebAcademicPlanSetBindingModel
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
        public void AcademicPlanRecord(WebAcademicPlanRecordSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateAcademicPlanRecord(new WebAcademicPlanRecordSetBindingModel
                {
                    AcademicPlanId = model.AcademicPlanId,
                    DisciplineId = model.DisciplineId,
                    ContingentId = model.ContingentId,
                    Semester = model.Semester,
                    Zet = model.Zet,
                    Selectable = model.Selectable,
                    IsSelected = model.IsSelected
                });
            }
            else
            {
                result = _serviceSP.UpdateAcademicPlanRecord(new WebAcademicPlanRecordSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanId = model.AcademicPlanId,
                    DisciplineId = model.DisciplineId,
                    ContingentId = model.ContingentId,
                    Semester = model.Semester,
                    Zet = model.Zet,
                    Selectable = model.Selectable,
                    IsSelected = model.IsSelected
                });
            }
            if (!result.Succeeded)
            {
                TempData["Error"] = result.Errors.LastOrDefault().Value;
            }
        }

        [HttpPost]
        public void AcademicPlanRecordElement(WebAcademicPlanRecordElementSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateAcademicPlanRecordElement(new WebAcademicPlanRecordElementSetBindingModel
                {
                    AcademicPlanRecordId = model.AcademicPlanRecordId,
                    TimeNormId = model.TimeNormId,
                    PlanHours = model.PlanHours,
                    FactHours = model.FactHours

                });
            }
            else
            {
                result = _serviceSP.UpdateAcademicPlanRecordElement(new WebAcademicPlanRecordElementSetBindingModel
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
        public void AcademicPlanRecordMission(WebAcademicPlanRecordMissionSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateAcademicPlanRecordMission(new WebAcademicPlanRecordMissionSetBindingModel
                {
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    LecturerId = model.LecturerId,
                    Hours = model.Hours


                });
            }
            else
            {
                result = _serviceSP.UpdateAcademicPlanRecordMission(new WebAcademicPlanRecordMissionSetBindingModel
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
        public void StreamLesson(WebStreamLessonSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateStreamLesson(new WebStreamLessonSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    StreamLessonName = model.StreamLessonName,
                    StreamLessonHours = model.StreamLessonHours,
                    Semester = model.Semester
                });
            }
            else
            {
                result = _serviceSP.UpdateStreamLesson(new WebStreamLessonSetBindingModel
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
        public void StreamLessonRecord(WebStreamLessonRecordSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateStreamLessonRecord(new WebStreamLessonRecordSetBindingModel
                {
                    StreamLessonId = model.StreamLessonId,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    IsMain = model.IsMain
                });
            }
            else
            {
                result = _serviceSP.UpdateStreamLessonRecord(new WebStreamLessonRecordSetBindingModel
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
        public void TimeNorm(WebTimeNormSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateTimeNorm(new WebTimeNormSetBindingModel
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
                result = _serviceSP.UpdateTimeNorm(new WebTimeNormSetBindingModel
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
        public void Contingent(WebContingentSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateContingent(new WebContingentSetBindingModel
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
                result = _serviceSP.UpdateContingent(new WebContingentSetBindingModel
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
        public void LecturerWorkload(WebLecturerWorkloadSetBindingModel model)
        {
            ResultService result;
            if (model?.Id == Guid.Empty)
            {
                result = _serviceSP.CreateLecturerWorkload(new WebLecturerWorkloadSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    LecturerId = model.LecturerId,
                    Workload = model.Workload
                });
            }
            else
            {
                result = _serviceSP.UpdateLecturerWorkload(new WebLecturerWorkloadSetBindingModel
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
            var academicPlanRecords = _serviceSP.GetAcademicPlanRecords(new WebAcademicPlanRecordGetBindingModel { AcademicPlanId = AcademicPlanId });
            if (academicPlanRecords.Succeeded)
            {
                return Json(academicPlanRecords.Result.List);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult GetAcademicPlanRecordElements(Guid AcademicPlanRecordId)
        {
            var academicPlanRecordElements = _serviceSP.GetAcademicPlanRecordElements(new WebAcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = AcademicPlanRecordId });
            if (academicPlanRecordElements.Succeeded)
            {
                return Json(academicPlanRecordElements.Result.List);
            }
            return Json(null);
        }
    }
}
