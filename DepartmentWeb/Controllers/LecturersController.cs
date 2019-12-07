using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class LecturersController : Controller
    {

        private ILecturerService _serviceL;

        public LecturersController( ILecturerService serviceL)
        {            
            _serviceL = serviceL;
        }
        
        // GET: Lecturer
        public ActionResult Index()
        {
            var list = DepartmentWeb.Services.LecturerService.GetLecturers(new BaseInterfaces.BindingModels.LecturerGetBindingModel());
            foreach(var tmp in list.Result)
            {
                if (tmp.Rank2.ToLower() == "отсутствует")
                {
                    tmp.Rank2 = "";
                }else
                {
                    string str = tmp.Rank2;
                    tmp.Rank2 = "";
                    for (int i=0; i< str.Length; i++)
                    {
                        tmp.Rank2 += str[i] + ".";
                    }
                    tmp.Rank2 += ",";
                }

                if(tmp.LecturerPost == "отсутствует")
                {
                    tmp.LecturerPost = "";
                }
                else
                {
                    tmp.LecturerPost = tmp.LecturerPost.ToLower();
                }

                if (tmp.Post == "ЗаведующийКафедрой")
                {
                    tmp.Post = "Заведующий кафедрой,";
                }else if (tmp.Post == "ЗаместительЗаведующегоКафедрой")
                {
                    tmp.Post = "Заместитель заведующего кафедрой,";
                }else
                {
                    tmp.Post = "";
                }
            }
            return View(list.Result);
        }
                
        public ActionResult Lecturer(string id)
        {
            var model = Services.LecturerService.GetLecturer(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });
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
                model.Result.Rank2 += ",";
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

        public ActionResult LecturerDis(string id)
        {
            var list = Services.DisciplineService.GetDisciplines(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });

            return PartialView("LecturerDis", list.Result);
        }
    }
}