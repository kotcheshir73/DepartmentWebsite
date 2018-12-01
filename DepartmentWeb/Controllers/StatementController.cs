using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentService;
using DepartmentService.IServices;

namespace DepartmentWeb.Controllers
{
    public class StatementController : Controller
    {
        private IAcademicYearService _serviceAY;
        private IStatementService _serviceS;
        private IStatementRecordService _serviceSR;
        private IStatementRecordExtendedService _serviceSRE;
        private ILecturerService _serviceL;

        public StatementController( IAcademicYearService serviceAY, IStatementService serviceS, IStatementRecordService serviceSR, IStatementRecordExtendedService serviceSRE, ILecturerService serviceL)
        {
            _serviceS = serviceS;
            _serviceAY = serviceAY;
            _serviceSR = serviceSR;
            _serviceSRE = serviceSRE;
            _serviceL = serviceL;

        }
        // GET: Statement
        public ActionResult Index()
        {            
            var tmpAY = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            var tmpL = _serviceL.GetLecturer(new DepartmentService.BindingModels.LecturerGetBindingModel() { Id = new Guid("0F121F0F-5BEF-4BAD-AE3F-DF06CD435EC7") });

            var tuple = new Tuple<DepartmentService.ViewModels.AcademicYearPageViewModel, DepartmentService.ViewModels.LecturerViewModel>(tmpAY.Result, tmpL.Result);

            return View(tuple);

            //return View(tmpAY.Result);
        }

        [HttpPost]
        public ActionResult getTableStatements(string yearId)
        {
            if (string.IsNullOrEmpty(yearId))
            {
                return PartialView();
            }
            var tmp = _serviceS.GetStatements(new DepartmentService.BindingModels.StatementGetBindingModel()
            {
                LecturerId = new Guid("0F121F0F-5BEF-4BAD-AE3F-DF06CD435EC7"),
                AcademicYearId = new Guid(yearId)
            });
            return PartialView("~/Views/Statement/StatementList.cshtml", tmp.Result.List);
        }

        /*
        [HttpPost]
        public ActionResult StatementList(List<DepartmentService.ViewModels.StatementViewModel> statementViewModels)
        {

            foreach (var tmp in statementViewModels)
            {
                var element = _serviceS.GetStatement(new DepartmentService.BindingModels.StatementGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceS.UpdateStatement(new DepartmentService.BindingModels.StatementSetBindingModel()
                {
                    Id = element.Result.Id,
                    Date = DateTime.Parse(tmp.Date),
                    AcademicPlanRecordId = element.Result.AcademicPlanRecordId,
                    Course = element.Result.Course,
                    LecturerId = element.Result.LecturerId,
                    Semester = element.Result.Semester,
                    StudentGroupId = element.Result.StudentGroupId,
                    TypeOfTest = element.Result.TypeOfTest
                });
            }

            var tmpAY = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            var tmpL = _serviceL.GetLecturer(new DepartmentService.BindingModels.LecturerGetBindingModel() { Id = new Guid("0F121F0F-5BEF-4BAD-AE3F-DF06CD435EC7") });

            var tuple = new Tuple<DepartmentService.ViewModels.AcademicYearPageViewModel, DepartmentService.ViewModels.LecturerViewModel>(tmpAY.Result, tmpL.Result);

            return View("Index", tuple);
        }
        */

        [HttpPost]
        public ActionResult StatementRecord(List<DepartmentService.ViewModels.StatementRecordViewModel> statementRecordViewModels)
        {
            
            foreach(var tmp in statementRecordViewModels)
            {
                var element = _serviceSR.GetStatementRecord(new DepartmentService.BindingModels.StatementRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceSR.UpdateStatementRecord(new DepartmentService.BindingModels.StatementRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    StatementId = element.Result.StatementId,
                    StudentId = element.Result.StudentId,
                    Score = tmp.Score
                });
                if (tmp.Name != null)
                {
                    var elementTo = _serviceSRE.GetStatementRecordExtendeds(new DepartmentService.BindingModels.StatementRecordExtendedGetBindingModel()
                    {
                        StatementRecordId = tmp.Id
                    });
                    _serviceSRE.UpdateStatementRecordExtended(new DepartmentService.BindingModels.StatementRecordExtendedSetBindingModel()
                    {
                        Id = elementTo.Result.List[0].Id,
                        StatementRecordId = tmp.Id,
                        Name = tmp.Name
                    });
                }
            }

            var tmpAY = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            var tmpL = _serviceL.GetLecturer(new DepartmentService.BindingModels.LecturerGetBindingModel() { Id = new Guid("0F121F0F-5BEF-4BAD-AE3F-DF06CD435EC7") });

            var tuple = new Tuple<DepartmentService.ViewModels.AcademicYearPageViewModel, DepartmentService.ViewModels.LecturerViewModel>(tmpAY.Result, tmpL.Result);

            return View("Index" ,tuple);
        }

        public ActionResult StatementRecord(string id)
        {
            var tmp = _serviceSR.GetStatementRecords(new DepartmentService.BindingModels.StatementRecordGetBindingModel()
            {
                StatementId=new Guid(id)
            });
            return View(tmp.Result.List);
        }

        [HttpPost]
        public ActionResult StatementEditDate(DepartmentService.ViewModels.StatementViewModel statementViewModels)
        {
            try
            {
                var element = _serviceS.GetStatement(new DepartmentService.BindingModels.StatementGetBindingModel()
                {
                    Id = statementViewModels.Id
                }).Result;
                _serviceS.UpdateStatement(new DepartmentService.BindingModels.StatementSetBindingModel()
                {
                    Id = element.Id,
                    AcademicPlanRecordId = element.AcademicPlanRecordId,
                    Course = element.Course,
                    LecturerId = element.LecturerId,
                    Semester = element.Semester,
                    StudentGroupId = element.StudentGroupId,
                    TypeOfTest = element.TypeOfTest,
                    Date = DateTime.Parse(statementViewModels.Date)
                });
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }

            var tmpAY = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            var tmpL = _serviceL.GetLecturer(new DepartmentService.BindingModels.LecturerGetBindingModel() { Id = new Guid("0F121F0F-5BEF-4BAD-AE3F-DF06CD435EC7") });

            var tuple = new Tuple<DepartmentService.ViewModels.AcademicYearPageViewModel, DepartmentService.ViewModels.LecturerViewModel>(tmpAY.Result, tmpL.Result);

            return View("Index", tuple);
        }

        public ActionResult StatementEditDate(string id)
        {
            var tmp = _serviceS.GetStatement(new DepartmentService.BindingModels.StatementGetBindingModel()
            {
                Id = new Guid(id)
            });
            return View(tmp.Result);
        }
    }
}