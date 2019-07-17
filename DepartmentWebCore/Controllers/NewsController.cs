using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _serviceN;

        private readonly IWebProcess _process;

        private readonly int pageSize = 10;

        public NewsController(INewsService serviceN, IWebProcess process)
        {
            _serviceN = serviceN;
            _process = process;
        }

        public IActionResult Index(int? page)
        {
            var newses = _serviceN.GetNewses(new NewsGetBindingModel
            {
                PageNumber = page ?? 0,
                PageSize = pageSize
            });

            if (!newses.Succeeded)
            {
                return new EmptyResult();
            }

            return View(newses.Result);
        }

        [Authorize(Roles = "Преподаватель")]
        [Authorize(Roles = "Администратор")]
        public IActionResult CreateNews()
        {
            return PartialView(new NewsWithFilesModel());
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        [Authorize(Roles = "Администратор")]
        public void CreateNews(NewsWithFilesModel model)
        {
            var newEventId = _serviceN.CreateNews(new NewsSetBindingModel
            {
                DepartmentUserId = new Guid(User.Identity.Name),
                Title = model.Title,
                Body = model.Body,
                Tag = model.Tag
            });

            if (model.FilesForUpload != null)
            {
               // await FileService.SaveFilesForEvent(model.fileForUpload, newEventId.Result.ToString());
            }
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        public async Task<IActionResult> CreateNews(EventWithFilesModel model)
        {
            var newEventId = _serviceN.CreateNews(new NewsSetBindingModel
            {
                DepartmentUserId = new Guid(User.Identity.Name),
                Title = model.Title,
                Body = model.Content
            });
            if (model.fileForUpload != null)
            {
                await FileService.SaveFilesForEvent(model.fileForUpload, newEventId.Result.ToString());
            }
            return RedirectToAction("Index", "Event");
        }

        [Authorize(Roles = "Преподаватель")]
        public IActionResult EditNews(Guid id)
        {
            var element = _serviceN.GetNews(new NewsGetBindingModel
            {
                Id = id
            }).Result;
            var files = FileService.GetFilesForEvent(id.ToString());
            return View(new EventWithFilesModel
            {
                Id = element.Id,
                Content = element.Body,
                Tag = element.Tag,
                Title = element.Title,
                fileForDownload = files
            });
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        public async Task<IActionResult> EditEvent(EventWithFilesModel model)
        {
            _serviceN.UpdateNews(new NewsSetBindingModel
            {
                Id = model.Id,
                Body = model.Content,
                Tag = model.Tag,
                Title = model.Title
            });
            if (model.fileForUpload != null)
            {
                await FileService.SaveFilesForEvent(model.fileForUpload, model.Id.ToString());
            }
            return RedirectToAction("Index", "Event");
        }

        [Authorize(Roles = "Преподаватель")]
        public IActionResult DeleteFile(string path)
        {
            FileService.DeleteFileByPathForEvent(path);
            return RedirectToAction("EditEvent", "Event", new { id = new Guid(path.Split('\\')[0]) }, null);
        }

        [Authorize(Roles = "Преподаватель")]
        public ActionResult DeleteEvent(Guid id, int page)
        {
            _serviceN.DeleteNews(new NewsGetBindingModel
            {
                Id = id
            });

            FileService.DeleteDirectoryByPathForEvent(id.ToString());
            return RedirectToAction("Index", "Event", new { page });
        }

        public IActionResult News(Guid id)
        {
            var element = _process.GetEventWithComment(new NewsGetBindingModel
            {
                Id = id
            }).Result;
            return View(element);
        }

        public FileResult Download(string path, string fileName)
        {
            return File(FileService.GetFileByPathForEvent(path), "application/vnd.ms-powerpoint", fileName);
        }

        public FileResult PDF(string path, string fileName)
        {
            return File(FileService.GetFileByPathForEvent(path), "application/pdf");
        }
    }
}