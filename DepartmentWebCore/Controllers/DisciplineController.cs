using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class DisciplineController : Controller
    {
        private IWebProcess _process;

        private IWebDisciplineService _serviceWD;

        private readonly FileService _fileService;

        private readonly string _filePath;

        public DisciplineController(IWebProcess process, IWebDisciplineService serviceWD, FileService fileService, IOptions<CustonConfig> config)
        {
            _process = process;
            _serviceWD = serviceWD;
            _fileService = fileService;

            _filePath = $"{config.Value.DirectoryPath}\\Disciplines\\";
        }

        public ActionResult Discipline(Guid id)
        {
            var model = _serviceWD.GetDiscipline(new WebDisciplineGetBindingModel { Id = id });

            if (!model.Succeeded)
            {
                return new EmptyResult();
            }

            return View(model.Result);
        }

        [Authorize]
        public ActionResult DisciplineContent(Guid id)
        {
            var model = _process.GetDisciplineContentInfo(new WebProcessDisciplineContentInfoBindingModel { DisciplineId = id });

            if(!model.Succeeded)
            {
                return PartialView();
            }
            ViewBag.CanAction = model.Result.Lecturers.Contains(new Guid(User.Identity.Name));
            ViewBag.Id = id;

            return PartialView(_fileService.GetDisciplineContext(id, $"{_filePath}\\{model.Result.DisciplineName}\\", _process));
        }

        [Authorize]
        public FileResult Download(Guid id, string fullName)
        {
            var discipline = _serviceWD.GetDisciplineName(new WebDisciplineGetBindingModel { Id = id });

            if (!discipline.Succeeded)
            {
                return null;
            }

            string fileName = _fileService.GetFileName($"{_filePath}\\{discipline.Result.DisciplineName}\\{fullName}");

            return File(_fileService.GetFileForDowmload($"{_filePath}\\{discipline.Result.DisciplineName}\\{fullName}"), _fileService.GetContentType(fileName), fileName);
        }

        [Authorize(Roles = "Преподаватель, Администратор")]
        public ActionResult LoadFile(Guid id, string fullName)
        {
            DisiplineLoadFileModel model = new DisiplineLoadFileModel
            {
                Id = id,
                FullName = fullName
            };

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "Преподаватель, Администратор")]
        public async Task<ActionResult> LoadFile(DisiplineLoadFileModel model)
        {
            var discipline = _serviceWD.GetDisciplineName(new WebDisciplineGetBindingModel { Id = model.Id });

            if (model.FilesForUpload != null)
            {
                await _fileService.SaveFiles(model.FilesForUpload, $"{_filePath}\\{discipline.Result.DisciplineName}\\{model.FullName}");
            }

            return RedirectToAction("Discipline", new { Id = model.Id });
        }

        [Authorize(Roles = "Преподаватель, Администратор")]
        public void DeleteFile(Guid id, string fullName)
        {
            var discipline = _serviceWD.GetDisciplineName(new WebDisciplineGetBindingModel { Id = id });

            _fileService.DeleteFile($"{_filePath}\\{discipline.Result.DisciplineName}\\{fullName}");
        } 
    }
}
