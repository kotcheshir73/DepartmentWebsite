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

                if(tmp.LecturerStudyPost == "отсутствует")
                {
                    tmp.LecturerStudyPost = "";
                }
                else
                {
                    tmp.LecturerStudyPost = tmp.LecturerStudyPost.ToLower();
                }

                if (tmp.LectureDepartmentPost == "ЗаведующийКафедрой")
                {
                    tmp.LectureDepartmentPost = "Заведующий кафедрой,";
                }else if (tmp.LectureDepartmentPost == "ЗаместительЗаведующегоКафедрой")
                {
                    tmp.LectureDepartmentPost = "Заместитель заведующего кафедрой,";
                }else
                {
                    tmp.LectureDepartmentPost = "";
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

            if (model.Result.LecturerStudyPost == "отсутствует")
            {
                model.Result.LecturerStudyPost = "";
            }

            if (model.Result.LectureDepartmentPost == "ЗаведующийКафедрой")
            {
                model.Result.LectureDepartmentPost = "Заведующий кафедрой,";
            }
            else if (model.Result.LectureDepartmentPost == "ЗаместительЗаведующегоКафедрой")
            {
                model.Result.LectureDepartmentPost = "Заместитель заведующего кафедрой,";
            }
            else
            {
                model.Result.LectureDepartmentPost = "";
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