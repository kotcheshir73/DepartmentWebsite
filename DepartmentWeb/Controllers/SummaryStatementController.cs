﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentService;
using DepartmentService.IServices;

namespace DepartmentWeb.Controllers
{
    public class SummaryStatementController : Controller
    {
        private IStatementRecordService _serviceSR;
        private IStudentGroupService _serviceSG;

        public SummaryStatementController(IStatementRecordService serviceSR, IStudentGroupService serviceSG)
        {
            _serviceSR = serviceSR;
            _serviceSG = serviceSG;
        }

        public ActionResult Index()
        {
            var tmp = _serviceSG.GetStudentGroups(new DepartmentService.BindingModels.StudentGroupGetBindingModel());

            return View(tmp.Result.List);
        }

        public ActionResult SummaryStatement(string id)
        {
            var tmp = _serviceSR.GetSummaryStatement(new DepartmentService.BindingModels.StudentGroupGetBindingModel
            {
                Id = new Guid(id)
            });
            return View(tmp.Result);
        }
    }
}