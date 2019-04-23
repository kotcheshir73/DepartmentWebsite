using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class GraficController : Controller
    {
        private IAcademicYearService _serviceAY;
        private IDisciplineTimeDistributionService _serviceDTD;
        private IDisciplineTimeDistributionRecordService _serviceDTDR;

        public GraficController( IAcademicYearService serviceAY, IDisciplineTimeDistributionService serviceDTD, IDisciplineTimeDistributionRecordService serviceDTDR)
        {           
            _serviceAY = serviceAY;
            _serviceDTD = serviceDTD;
            _serviceDTDR = serviceDTDR;

        }
        // GET: Grafic
        public ActionResult Index()
        {            
            var tmp = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel());            
            return View(tmp.Result);
        }

        [HttpPost]
        public ActionResult getTableGrafics(string yearId)
        {
            if (string.IsNullOrEmpty(yearId))
            {
                return PartialView();
            }
            var tmp = _serviceDTD.GetDisciplineTimeDistributions(new DisciplineTimeDistributionGetBindingModel()
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

        
        public ActionResult ConvertPartial(DisciplineTimeDistributionViewModel graficViewModel)
        {
            var tmp = _serviceDTDR.GetDisciplineTimeDistributionRecords(new DisciplineTimeDistributionRecordGetBindingModel()
            {
                DisciplineTimeDistributionId = graficViewModel.Id
            });

            return PartialView("~/Views/Grafic/Partial.cshtml", tmp.Result.List);
        }

        [HttpPost]
        public ActionResult GraficRecord(List<DisciplineTimeDistributionRecordViewModel> graficRecordViews)
        {
            //код сохранения
            foreach (var tmp in graficRecordViews)
            {
                var element = _serviceDTDR.GetDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceDTDR.UpdateDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    DisciplineTimeDistributionId = element.Result.DisciplineTimeDistributionId,
                    TimeNormId = element.Result.TimeNormId,
                    WeekNumber = element.Result.WeekNumber,
                    Hours = tmp.Hours
                });
            }

            var academicYearViewModel = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel());
            return View("~/Views/Grafic/Index.cshtml", academicYearViewModel.Result);
        }

        public ActionResult GraficRecord(string id)
        {
            var tmp = _serviceDTD.GetDisciplineTimeDistribution(new DisciplineTimeDistributionGetBindingModel()
            {
                Id = new Guid(id)
            });
            
            return View(tmp.Result);
        }
    }
}