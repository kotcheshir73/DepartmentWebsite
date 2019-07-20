using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _serviceC;

        public CommentController(ICommentService serviceC)
        {
            _serviceC = serviceC;
        }

        public IActionResult Index(Guid? disciplineId, Guid? newsId)
        {
            var list = _serviceC.GetComments(new CommentGetBindingModel
            {
                DisciplineId = disciplineId,
                NewsId = newsId
            });

            if(!list.Succeeded)
            {
                return new EmptyResult();
            }

            return PartialView(list.Result);
        }

        [HttpPost]
        [Authorize]
        public void Create(CommentSetBindingModel model)
        {        
            if(model.DisciplineId.HasValue || model.NewsId.HasValue)
            {
                _serviceC.CreateComment(new CommentSetBindingModel
                {
                    Content = model.Content,
                    DepartmentUserId = new Guid(User.Identity.Name),
                    NewsId = model.NewsId,
                    DisciplineId = model.DisciplineId
                });
            }
        }

        [HttpPost]
        [Authorize]
        public void Edit(CommentSetBindingModel model)
        {
            _serviceC.UpdateComment(new CommentSetBindingModel
            {
                Id = model.Id,
                Content = model.Content
            });
        }

        [HttpPost]
        [Authorize]
        public void Delete(Guid id)
        {
            _serviceC.DeleteComment(new CommentGetBindingModel
            {
                Id = id
            });
        }

        [HttpPost]
        [Authorize]
        public void Answer(CommentSetBindingModel model)
        {
            if (model.ParentId.HasValue)
            {
                _serviceC.CreateComment(new CommentSetBindingModel
                {
                    Content = model.Content,
                    DepartmentUserId = new Guid(User.Identity.Name),
                    ParentId = model.ParentId
                });
            }
        }

        [HttpGet]
        public ActionResult Answers(Guid parentId)
        {
            var list = _serviceC.GetAnswers(new CommentGetBindingModel
            {
                ParentId = parentId
            });

            if (!list.Succeeded)
            {
                return new EmptyResult();
            }

            return PartialView(list.Result);
        }
    }
}