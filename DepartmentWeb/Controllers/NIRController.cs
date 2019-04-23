using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class NIRController : Controller
    {
        //private IIndividualPlanRecordService _serviceIPR;
        //private IIndividualPlanNIRContractualWorkService _serviceCW;
        //private IIndividualPlanNIRScientificArticleService _serviceSA;
        //public NIRController(IIndividualPlanRecordService serviceIPRS, IIndividualPlanNIRContractualWorkService serviceCW, IIndividualPlanNIRScientificArticleService serviceSA)
        //{
        //    _serviceIPR = serviceIPRS;
        //    _serviceCW = serviceCW;
        //    _serviceSA = serviceSA;
        //}
        
        //public ActionResult Index()
        //{
        //    var tmp = _serviceIPR.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Title = "Научно-исследовательская работа"
        //    });

        //    return View(tmp.Result.List);
        //}

        //[HttpPost]
        //public ActionResult Index(List<DepartmentService.ViewModels.IndividualPlanRecordViewModel> individualPlanRecordViewModels)
        //{

        //    foreach (var tmp in individualPlanRecordViewModels)
        //    {
        //        var element = _serviceIPR.GetIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
        //        {
        //            Id = tmp.Id
        //        });
        //        _serviceIPR.UpdateIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordSetBindingModel()
        //        {
        //            Id = element.Result.Id,
        //            LecturerId = element.Result.LecturerId,
        //            AcademicYearId = element.Result.AcademicYearId,
        //            IndividualPlanKindOfWorkId = element.Result.IndividualPlanKindOfWorkId,
        //            PlanAutumn = tmp.PlanAutumn,
        //            PlanSpring = tmp.PlanSpring,
        //            FactAutumn = tmp.FactAutumn,
        //            FactSpring = tmp.FactSpring
        //        });
        //    }

        //    var tmp2 = _serviceIPR.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Title = "Научно-исследовательская работа"
        //    });

        //    return View("NIR", tmp2.Result.List);
        //}

        //public ActionResult ContractualWork()
        //{
        //    var tmp = _serviceCW.GetIndividualPlanNIRContractualWorks(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469")
        //    });

        //    return View(tmp.Result.List);
        //}
        
        //public ActionResult CreateContractualWork()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateContractualWork(IndividualPlanNIRContractualWorkViewModel model)
        //{
        //    _serviceCW.CreateIndividualPlanNIRContractualWork(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkSetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        JobContent = model.JobContent,
        //        PlannedTerm = model.PlannedTerm,
        //        Post = model.Post,
        //        ReadyMark = model.ReadyMark
        //    });

        //    var tmp = _serviceCW.GetIndividualPlanNIRContractualWorks(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469")
        //    });

        //    return View("ContractualWork", tmp.Result.List);
        //}

        //public ActionResult UpdateContractualWork(Guid id)
        //{
        //    var element = _serviceCW.GetIndividualPlanNIRContractualWork(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        Id = id
        //    }).Result;
        //    return View(element);
        //}

        //[HttpPost]
        //public ActionResult UpdateContractualWork(IndividualPlanNIRContractualWorkViewModel model)
        //{
        //    var element = _serviceCW.GetIndividualPlanNIRContractualWork(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        Id = model.Id
        //    }).Result;
        //    _serviceCW.UpdateIndividualPlanNIRContractualWork(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkSetBindingModel
        //    {
        //        Id = element.Id,
        //        LecturerId = element.LecturerId,
        //        JobContent = model.JobContent,
        //        PlannedTerm = model.PlannedTerm,
        //        Post = model.Post,
        //        ReadyMark = model.ReadyMark
        //    });

        //    var tmp = _serviceCW.GetIndividualPlanNIRContractualWorks(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469")
        //    });

        //    return View("ContractualWork", tmp.Result.List);
        //}

        //public ActionResult DeleteContractualWork(Guid id)
        //{
        //    _serviceCW.DeleteIndividualPlanNIRContractualWork(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        Id = id
        //    });

        //    var tmp = _serviceCW.GetIndividualPlanNIRContractualWorks(new DepartmentService.BindingModels.IndividualPlanNIRContractualWorkGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469")
        //    });

        //    return View("ContractualWork", tmp.Result.List);
        //}






        //public ActionResult ScientificArticleOfPrint()
        //{
        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Печать"
        //    });

        //    return View(tmp.Result.List);
        //}

        //public ActionResult CreateScientificArticleOfPrint()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateScientificArticleOfPrint(IndividualPlanNIRScientificArticleViewModel model)
        //{
        //    _serviceSA.CreateIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleSetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Name = model.Name,
        //        Publishing = model.Publishing,
        //        TypeOfPublication = model.TypeOfPublication,
        //        Volume = model.Volume,
        //        Year = model.Year,
        //        Status = "Печать"
        //    });

        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Печать"
        //    });

        //    return View("ScientificArticleOfPrint", tmp.Result.List);
        //}

        //public ActionResult UpdateScientificArticleOfPrint(Guid id)
        //{
        //    var element = _serviceSA.GetIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        Id = id
        //    }).Result;
        //    return View(element);
        //}

        //[HttpPost]
        //public ActionResult UpdateScientificArticleOfPrint(IndividualPlanNIRScientificArticleViewModel model)
        //{
        //    var element = _serviceSA.GetIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        Id = model.Id
        //    }).Result;
        //    _serviceSA.UpdateIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleSetBindingModel
        //    {
        //        Id = element.Id,
        //        LecturerId = element.LecturerId,
        //        Name = model.Name,
        //        Status = "Печать",
        //        Publishing = model.Publishing,
        //        TypeOfPublication = model.TypeOfPublication,
        //        Volume = model.Volume,
        //        Year = model.Year
        //    });

        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Печать"
        //    });

        //    return View("ScientificArticleOfPrint", tmp.Result.List);
        //}

        //public ActionResult DeleteScientificArticleOfPrint(Guid id)
        //{
        //    _serviceSA.DeleteIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        Id = id
        //    });

        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Печать"
        //    });

        //    return View("ScientificArticleOfPrint", tmp.Result.List);
        //}

        //public ActionResult ScientificArticleOfPublished()
        //{
        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Опубликовано"
        //    });

        //    return View(tmp.Result.List);
        //}

        //public ActionResult CreateScientificArticleOfPublished()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateScientificArticleOfPublished(IndividualPlanNIRScientificArticleViewModel model)
        //{
        //    _serviceSA.CreateIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleSetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Name = model.Name,
        //        Publishing = model.Publishing,
        //        TypeOfPublication = model.TypeOfPublication,
        //        Volume = model.Volume,
        //        Year = model.Year,
        //        Status = "Опубликовано"
        //    });

        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Опубликовано"
        //    });

        //    return View("ScientificArticleOfPrint", tmp.Result.List);
        //}

        //public ActionResult UpdateScientificArticleOfPublished(Guid id)
        //{
        //    var element = _serviceSA.GetIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        Id = id
        //    }).Result;
        //    return View(element);
        //}

        //[HttpPost]
        //public ActionResult UpdateScientificArticleOfPublished(IndividualPlanNIRScientificArticleViewModel model)
        //{
        //    var element = _serviceSA.GetIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        Id = model.Id
        //    }).Result;
        //    _serviceSA.UpdateIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleSetBindingModel
        //    {
        //        Id = element.Id,
        //        LecturerId = element.LecturerId,
        //        Name = model.Name,
        //        Status = "Опубликовано",
        //        Publishing = model.Publishing,
        //        TypeOfPublication = model.TypeOfPublication,
        //        Volume = model.Volume,
        //        Year = model.Year
        //    });

        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Печать"
        //    });

        //    return View("ScientificArticleOfPrint", tmp.Result.List);
        //}

        //public ActionResult DeleteScientificArticleOfPublished(Guid id)
        //{
        //    _serviceSA.DeleteIndividualPlanNIRScientificArticle(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        Id = id
        //    });

        //    var tmp = _serviceSA.GetIndividualPlanNIRScientificArticles(new DepartmentService.BindingModels.IndividualPlanNIRScientificArticleGetBindingModel
        //    {
        //        LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
        //        Status = "Опубликовано"
        //    });

        //    return View("ScientificArticleOfPrint", tmp.Result.List);
        //}
    }
}