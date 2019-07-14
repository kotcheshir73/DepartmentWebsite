using Microsoft.AspNetCore.Mvc;
using System;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IWebLecturerService _serviceL;

        public LecturerController(IWebLecturerService serviceL)
        {
            _serviceL = serviceL;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var list = _serviceL.GetLecturers(new WebLecturerGetBindingModel());
            if(list.Succeeded)
            {
                foreach (var tmp in list.Result.List)
                {
                    if (tmp.Rank2.ToLower() == "отсутствует")
                    {
                        tmp.Rank2 = "";
                    }
                    else
                    {
                        string str = tmp.Rank2;
                        tmp.Rank2 = "";
                        for (int i = 0; i < str.Length; i++)
                        {
                            tmp.Rank2 += str[i] + ".";
                        }
                        tmp.Rank2 += ",";
                    }

                    if (tmp.LecturerPost == "отсутствует")
                    {
                        tmp.LecturerPost = "";
                    }
                    else
                    {
                        tmp.LecturerPost = tmp.LecturerPost.ToLower();
                    }

                    if (tmp.Post == "ЗаведующийКафедрой")
                    {
                        tmp.Post = "Зав. кафедрой,";
                    }
                    else if (tmp.Post == "ЗаместительЗаведующегоКафедрой")
                    {
                        tmp.Post = "Зам. зав. кафедрой,";
                    }
                    else
                    {
                        tmp.Post = "";
                    }
                }
            }

            return View(list.Result.List);
        }

        [HttpGet]
        public ActionResult Lecturer(Guid id)
        {
            var model = _serviceL.GetLecturer(new WebLecturerGetBindingModel { Id = id });

            if (!model.Succeeded)
            {
                return new EmptyResult();
            }

            if (model.Result.Rank2.ToLower() == "отсутствует")
            {
                model.Result.Rank2 = "";
            }
            else
            {
                string str = model.Result.Rank2;
                model.Result.Rank2 = "";
                for (int i = 0; i < str.Length; i++)
                {
                    model.Result.Rank2 += str[i] + ".";
                }
            }

            if (model.Result.LecturerPost == "отсутствует")
            {
                model.Result.LecturerPost = "";
            }

            if (model.Result.Post == "ЗаведующийКафедрой")
            {
                model.Result.Post = "Заведующий кафедрой,";
            }
            else if (model.Result.Post == "ЗаместительЗаведующегоКафедрой")
            {
                model.Result.Post = "Заместитель заведующего кафедрой,";
            }
            else
            {
                model.Result.Post = "";
            }

            return View(model.Result);
        }
    }
}