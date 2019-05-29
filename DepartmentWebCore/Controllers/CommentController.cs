using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseInterfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _serviceC;
        private readonly IDisciplineService _serviceD;

        public CommentController(ICommentService serviceC, IDisciplineService serviceD)
        {
            _serviceC = serviceC;
            _serviceD = serviceD;
        }
        //public IActionResult AddComment(CommentSetBindingModel model)
        //{
        //    return PartialView(model);
        //}

        [HttpPost]
        public IActionResult Create(CommentSetBindingModel model)
        {        
            
            if (model.EventId != null)
            {
                _serviceC.CreateComment(new CommentSetBindingModel
                {
                    Content = model.Content,
                    DepartmentUser = User.Identity.Name,
                    EventId = model.EventId,
                    ParentId = model.ParentId
                });
                return RedirectToAction("Event", "Event", new { id = model.EventId }, null);
            }else if(model.DisciplineId!=null)
            {
                _serviceC.CreateComment(new CommentSetBindingModel
                {
                    Content = model.Content,
                    DepartmentUser = User.Identity.Name,
                    DisciplineId = model.DisciplineId,
                    ParentId = model.ParentId
                });
                var tmp = _serviceD.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel
                {
                    Id = model.DisciplineId
                });
                return RedirectToAction("DisContent", "Discipline", new { id = tmp.Result.DisciplineName }, null);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteCommentEvent(WebProcessLevelCommentViewModel comment, Guid eventId)
        {
            _serviceC.DeleteComment(new CommentGetBindingModel
            {
                Id = comment.Id
            });

            //foreach(var com in comment.commentList)
            //{
            //    _serviceC.DeleteComment( new CommentGetBindingModel
            //    {
            //        Id = com.Id
            //    });
            //}
            //_serviceC.DeleteComment(new CommentGetBindingModel
            //{
            //    Id = comment.Id
            //});
            return RedirectToAction("Event", "Event", new { id = eventId }, null);
        }

        
    }
}