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
    public class GraficController : Controller
    {
        private IAcademicYearService _serviceAY;
        private IGraficService _serviceG;
        private IGraficRecordService _serviceGR;

        public GraficController( IAcademicYearService serviceAY, IGraficService serviceG, IGraficRecordService serviceGR)
        {           
            _serviceAY = serviceAY;
            _serviceG = serviceG;
            _serviceGR = serviceGR;

        }
        // GET: Grafic
        public ActionResult Index()
        {            
            var tmp = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());            
            return View(tmp.Result);
        }

        [HttpPost]
        public ActionResult getTableGrafics(string yearId)
        {
            if (string.IsNullOrEmpty(yearId))
            {
                return PartialView();
            }
            var tmp = _serviceG.GetGrafics(new DepartmentService.BindingModels.GraficGetBindingModel()
            {  
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                AcademicYearId = new Guid(yearId)
            });
            return PartialView("~/Views/Grafic/GraficList.cshtml", tmp.Result.List);
        }

        //[HttpPost]
        //public ActionResult GraficRecord(List<DepartmentService.ViewModels.GraficRecordViewModel> GraficRecordViewModels)
        //{

        //    foreach(var tmp in GraficRecordViewModels)
        //    {
        //        var element = _serviceSR.GetGraficRecord(new DepartmentService.BindingModels.GraficRecordGetBindingModel()
        //        {
        //            Id = tmp.Id
        //        });
        //        _serviceSR.UpdateGraficRecord(new DepartmentService.BindingModels.GraficRecordSetBindingModel()
        //        {
        //            Id = element.Result.Id,
        //            GraficId = element.Result.GraficId,
        //            StudentId = element.Result.StudentId,
        //            Score = tmp.Score
        //        });
        //    }

        //    var year = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
        //    return View("Index", year.Result);
        //}

        
        public ActionResult ConvertPartial(DepartmentService.ViewModels.GraficViewModel graficViewModel)
        {
            var tmp = _serviceGR.GetGraficRecords(new DepartmentService.BindingModels.GraficRecordGetBindingModel()
            {
                GraficId = graficViewModel.Id
            });

            return PartialView("~/Views/Grafic/Partial.cshtml", tmp.Result.List);
        }

        [HttpPost]
        public ActionResult GraficRecord(List<DepartmentService.ViewModels.GraficRecordViewModel> graficRecordViews)
        {
            //код сохранения
            foreach (var tmp in graficRecordViews)
            {
                var element = _serviceGR.GetGraficRecord(new DepartmentService.BindingModels.GraficRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceGR.UpdateGraficRecord(new DepartmentService.BindingModels.GraficRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    GraficId = element.Result.GraficId,
                    TimeNormId = element.Result.TimeNormId,
                    WeekNumber = element.Result.WeekNumber,
                    Hours = tmp.Hours
                });
            }

            var academicYearViewModel = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            return View("~/Views/Grafic/Index.cshtml", academicYearViewModel.Result);
        }

        public ActionResult GraficRecord(string id)
        {
            var tmp = _serviceG.GetGrafic(new DepartmentService.BindingModels.GraficGetBindingModel()
            {
                Id = new Guid(id)
            });
            
            return View(tmp.Result);
        }
    }
}