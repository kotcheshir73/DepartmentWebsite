using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _serviceE;

        public EventController(IEventService serviceE)
        {
            _serviceE = serviceE;
        }

        public IActionResult Index()
        {
            var tmp = _serviceE.GetEvents(new WebInterfaces.BindingModels.EventGetBindingModel { });
            //if(tmp.Result == null)
            //{
            //    return View();
            //}
            return View(tmp.Result);
        }

        [Authorize(Roles = "Преподаватель")]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        public async Task<IActionResult> CreateEvent(EventWithFilesModel model)
        {
            var newEventId = _serviceE.CreateEvent(new WebInterfaces.BindingModels.EventSetBindingModel {
                Content = model.Content,
                DepartmentUser = User.Identity.Name,
                Title = model.Title                
            });
            await FileService.SaveFilesForEvent(model.fileForUpload, newEventId.Result.ToString());
            return RedirectToAction("Index", "Event");
        }

        [Authorize(Roles = "Преподаватель")]
        public IActionResult EditEvent(Guid id)
        {
            var element = _serviceE.GetEvent(new EventGetBindingModel
            {
                Id = id
            }).Result;
            var files = FileService.GetFilesForEvent(id.ToString());
            return View(new EventWithFilesModel
            {
                Id = element.Id,
                Content = element.Content,
                Tag = element.Tag,
                Title = element.Title,
                fileForDownload = files
            });
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        public async Task<IActionResult> EditEvent(EventWithFilesModel model)
        {
            _serviceE.UpdateEvent(new EventUpdateBindingModel
            {
                Id = model.Id,
                Content = model.Content,
                Tag = model.Tag,
                Title = model.Title
            });
            await FileService.SaveFilesForEvent(model.fileForUpload, model.Id.ToString());
            return RedirectToAction("Index", "Event");
        }

        [Authorize(Roles = "Преподаватель")]
        public IActionResult DeleteFile(string path)
        {
            FileService.DeleteFileByPathForEvent(path);
            return RedirectToAction("EditEvent", "Event", new { id = new Guid(path.Split('\\')[0]) }, null);
        }

        [Authorize(Roles = "Преподаватель")]
        public ActionResult DeleteEvent(Guid id)
        {
            _serviceE.DeleteEvent(new EventGetBindingModel
            {
                Id = id
            });
            FileService.DeleteDirectoryByPathForEvent(id.ToString());
            return RedirectToAction("Index", "Event");
        }

        public IActionResult Event(Guid id)
        {
            var element = _serviceE.GetEvent(new EventGetBindingModel
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