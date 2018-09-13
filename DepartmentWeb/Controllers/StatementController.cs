using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentService;
using DepartmentService.IServices;
using DepartmentWeb.Models;

namespace DepartmentWeb.Controllers
{
    public class StatementController : Controller
    {
        private IAcademicYearService _serviceAY;
        private IStatementService _serviceS;
        private IStatementRecordService _serviceSR;

        public StatementController( IAcademicYearService serviceAY, IStatementService serviceS, IStatementRecordService serviceSR)
        {
            _serviceS = serviceS;
            _serviceAY = serviceAY;
            _serviceSR = serviceSR;

        }
        // GET: Statement
        public ActionResult Index()
        {            
            var tmp = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());            
            return View(tmp.Result);
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
            }

            var year = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            return View("Index", year.Result);
        }

        public ActionResult StatementRecord(string id)
        {
            var tmp = _serviceSR.GetStatementRecords(new DepartmentService.BindingModels.StatementRecordGetBindingModel()
            {
                StatementId=new Guid(id)
            });
            return View(tmp.Result.List);
        }
    }
}