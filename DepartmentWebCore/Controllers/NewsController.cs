using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        private readonly FileService _fileService;

        private readonly int _pageSize = 10;

        private readonly string _filePath;

        public NewsController(INewsService serviceN, IWebProcess process, FileService fileService, IOptions<CustonConfig> config)
        {
            _serviceN = serviceN;
            _process = process;
            _fileService = fileService;
            _filePath = $"{config.Value.DirectoryPath}\\Newses\\";
        }

        public IActionResult Index(int? page)
        {
            var newses = _serviceN.GetNewses(new NewsGetBindingModel
            {
                PageNumber = page ?? 0,
                PageSize = _pageSize
            });

            if (!newses.Succeeded)
            {
                return new EmptyResult();
            }

            return View(newses.Result);
        }

        [Authorize(Roles = "Преподаватель, Администратор")]
        public IActionResult CreateNews()
        {
            return View(new NewsWithFilesModel());
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель, Администратор")]
        public async Task<IActionResult> CreateNews(NewsWithFilesModel model)
        {
            var newId = _serviceN.CreateNews(new NewsSetBindingModel
            {
                DepartmentUserId = new Guid(User.Identity.Name),
                Title = model.Title,
                Body = model.Body,
                Tag = model.Tag
            });

            if (model.FilesForUpload != null)
            {
                await _fileService.SaveFiles(model.FilesForUpload, $"{_filePath}\\{newId.Result}");
            }

            return RedirectToAction("Index", "News");
        }

        public IActionResult ShowNews(Guid id)
        {
            var news = _serviceN.GetNews(new NewsGetBindingModel { Id = id });

            if (!news.Succeeded)
            {
                return new EmptyResult();
            }

            NewsWithFilesModel model = new NewsWithFilesModel
            {
                Id = news.Result.Id,
                Title = news.Result.Title,
                Body = news.Result.Body,
                Tag = news.Result.Tag,
                Author = news.Result.DepartmentUser,
                Date = news.Result.Date,
                FilesForDownload = _fileService.GetFiles($"{_filePath}\\{news.Result.Id}\\")
            };

            return View(model);
        }

        [Authorize(Roles = "Преподаватель, Администратор")]
        public IActionResult EditNews(Guid id, int page)
        {
            var news = _serviceN.GetNews(new NewsGetBindingModel { Id = id });

            if (!news.Succeeded)
            {
                return new EmptyResult();
            }

            NewsWithFilesModel model = new NewsWithFilesModel
            {
                Id = news.Result.Id,
                DepartmentUserId = news.Result.DepartmentUserId,
                Title = news.Result.Title,
                Body = news.Result.Body,
                Tag = news.Result.Tag,
                Author = news.Result.DepartmentUser,
                Date = news.Result.Date,
                FilesForDownload = _fileService.GetFiles($"{_filePath}\\{news.Result.Id}\\"),
                CurrentPage = page
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель, Администратор")]
        public async Task<IActionResult> EditNews(NewsWithFilesModel model)
        {
            _serviceN.UpdateNews(new NewsSetBindingModel
            {
                Id = model.Id,
                DepartmentUserId = model.DepartmentUserId,
                Title = model.Title,
                Body = model.Body,
                Tag = model.Tag
            });

            if (model.FilesForUpload != null)
            {
                await _fileService.SaveFiles(model.FilesForUpload, $"{_filePath}\\{model.Id}");
            }

            return RedirectToAction("Index", new { page = model.CurrentPage });
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель, Администратор")]
        public void DeleteFile(Guid id, string fileName)
        {
            _fileService.DeleteFile($"{_filePath}\\{id}\\{fileName}");
        }

        [Authorize(Roles = "Преподаватель, Администратор")]
        public IActionResult DeleteNews(Guid id, int page)
        {
            var news = _serviceN.GetNews(new NewsGetBindingModel { Id = id });

            if (!news.Succeeded)
            {
                return new EmptyResult();
            }

            NewsWithFilesModel model = new NewsWithFilesModel
            {
                Id = news.Result.Id,
                DepartmentUserId = news.Result.DepartmentUserId,
                Title = news.Result.Title,
                CurrentPage = page
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель, Администратор")]
        public void DeleteNewsConfirm(Guid id)
        {
            var result = _serviceN.DeleteNews(new NewsGetBindingModel { Id = id });

            if(result.Succeeded)
            {
                _fileService.DeleteDirection($"{_filePath}\\{id}");
            }
        }

        public FileResult Download(Guid id, string fileName)
        {
            return File(_fileService.GetFileForDowmload($"{_filePath}\\{id}\\{fileName}"), _fileService.GetContentType(fileName), fileName);
        }
    }
}