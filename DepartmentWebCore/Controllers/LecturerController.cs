using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DepartmentWebCore.Controllers
{
	public class LecturerController : Controller
    {
        private readonly BaseService _baseService;

        public LecturerController(BaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var listLecturers = _baseService.GetLecturers();
            if (listLecturers != null)
            {
                foreach (var tmp in listLecturers)
                {
                    if (tmp.Rank2.ToLower() == "отсутствует")
                    {
                        tmp.Rank2 = "";
                    }
                    else if (tmp.Rank2.Length < 4)
                    {
                        string str = tmp.Rank2;
                        tmp.Rank2 = "";
                        for (int i = 0; i < str.Length; i++)
                        {
                            tmp.Rank2 += str[i] + ".";
                        }
                        tmp.Rank2 += ",";
                    }
                }
            }

            return View(listLecturers);
        }

        [HttpGet]
        public ActionResult Lecturer(Guid id)
        {
            var model = _baseService.GetLecturer(id);
            if (model == null)
            {
                return new EmptyResult();
            }

            if (model.Rank2.ToLower() == "отсутствует")
            {
                model.Rank2 = "";
            }
            else if (model.Rank2.Length < 4)
            {
                string str = model.Rank2;
                model.Rank2 = "";
                for (int i = 0; i < str.Length; i++)
                {
                    model.Rank2 += str[i] + ".";
                }
            }

            return View(model);
        }

        public ActionResult LecturerDisciplines(Guid id)
        {
            return PartialView(_baseService.GetDisciplineForLecutrer(id));
        }
    }
}