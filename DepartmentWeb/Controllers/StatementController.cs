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
        private DbContext _context;
        public StatementController(IEducationalProcessService serviceEP, IAcademicYearService serviceAY, IStatementService serviceS, DbContext context)
        {
            _serviceS = serviceS;
            _serviceAY = serviceAY;
            _context = context;

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
            var tmp = _serviceS.GetStatements(new DepartmentService.BindingModels.StatementGetBindingModel()
            {
                LecturerId = new Guid("0F121F0F-5BEF-4BAD-AE3F-DF06CD435EC7"),
                AcademicYearId = new Guid(yearId)
            });
            return PartialView("~/Views/Statement/StatementList.cshtml", tmp.Result.List);
        }
    }
}