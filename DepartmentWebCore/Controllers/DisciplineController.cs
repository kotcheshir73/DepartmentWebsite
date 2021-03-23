using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DepartmentWebCore.Controllers
{
	public class DisciplineController : Controller
    {
        private readonly BaseService _baseService;

        private readonly FileService _fileService;

        private readonly string _filePath;

        public DisciplineController(BaseService baseService, FileService fileService, IOptions<CustonConfig> config)
        {
            _baseService = baseService;
            _fileService = fileService;

            _filePath = $"{config.Value.DirectoryPath}\\Disciplines\\";
        }

        public ActionResult Discipline(Guid id)
        {
            var model = _baseService.GetDiscipline(id);
            if (model == null)
            {
                return new EmptyResult();
            }

            ViewBag.DisciplineLecturers = _baseService.GetLecturerForDiscipline(id);

            return View(model);
        }

        [Authorize]
        public ActionResult DisciplineContent(Guid id)
        {
            var model = _baseService.GetLecturerUsersForDiscipline(id);
            if (model == default)
            {
                return new EmptyResult();
            }
            ViewBag.CanAction = model.Users.Contains(new Guid(User.Identity.Name)) || User.IsInRole("Администратор");
            ViewBag.Id = id;

            return PartialView(_fileService.GetDisciplineContext(id, $"{_filePath}\\{model.Title}\\"));
        }

        [Authorize]
        public FileResult Download(Guid id, string fullName)
        {
            var discipline = _baseService.GetDiscipline(id);

            if (discipline == null)
            {
                return null;
            }

            string fileName = _fileService.GetFileName($"{_filePath}\\{discipline.DisciplineName}\\{fullName}");

            return File(_fileService.GetFileForDowmload($"{_filePath}\\{discipline.DisciplineName}\\{fullName}"), _fileService.GetContentType(fileName), fileName);
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
            var discipline = _baseService.GetDiscipline(model.Id);

            if (model.FilesForUpload != null)
            {
                await _fileService.SaveFiles(model.FilesForUpload, $"{_filePath}\\{discipline.DisciplineName}\\{model.FullName}");
            }

            return RedirectToAction("Discipline", new { model.Id });
        }

        [Authorize(Roles = "Преподаватель, Администратор")]
        public void DeleteFile(Guid id, string fullName)
        {
            var discipline = _baseService.GetDiscipline(id);

            _fileService.DeleteFile($"{_filePath}\\{discipline.DisciplineName}\\{fullName}");
        } 
    }
}