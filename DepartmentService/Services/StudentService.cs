using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using System.Text;
using System.Data.Entity.Validation;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace DepartmentService.Services
{
    public class StudentService : IStudentService
    {
        private readonly DepartmentDbContext _context;

        private readonly IStudentGroupService _serviceSG;

        public StudentService(DepartmentDbContext context, IStudentGroupService serviceSG)
        {
            _context = context;
            _serviceSG = serviceSG;
        }

        public List<StudentViewModel> GetStudents(StudentGetBindingModel model)
        {
            if (model.StudentGroupId.HasValue)
            {
                return ModelFactory.CreateStudents(
                    _context.Students
                        .Where(e => e.StudentGroupId == model.StudentGroupId.Value && !e.IsDeleted)
                        )
                .OrderBy(s => s.LastName).ToList();
            }
            return ModelFactory.CreateStudents(
                    _context.Students
                        .Where(e => !e.IsDeleted))
                .ToList();
        }

        public List<StudentGroupViewModel> GetStudentGroups()
        {
            return _serviceSG.GetStudentGroups();
        }

        public StudentViewModel GetStudent(StudentGetBindingModel model)
        {
            var entity = _context.Students
                            .FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
            if (entity == null)
                return null;
            return ModelFactory.CreateStudentViewModel(entity);
        }

        public ResultService LoadStudentsFromFile(StudentLoadDocBindingModel model)
        {
            var word = new Application();
            try
            {
                if (File.Exists(model.FileName))
                {
                    Document document = word.Documents.Open(model.FileName, Type.Missing,
                            true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);                    
                    var table = document.Tables[1];

                    for (int i = 2; i < table.Rows.Count; ++i)
                    {
                        StudentRecordBindingModel createModel = new StudentRecordBindingModel
                        {
                            NumberOfBook = table.Cell(i, 2).Range.Text.Replace("\r\a", ""),
                            LastName = table.Cell(i, 3).Range.Text.Replace("\r\a", ""),
                            FirstName = table.Cell(i, 4).Range.Text.Replace("\r\a", ""),
                            Patronymic = table.Cell(i, 5).Range.Text.Replace("\r\a", ""),
                            Description = table.Cell(i, 6).Range.Text.Replace("\r\a", "") + " " + table.Cell(i, 7).Range.Text.Replace("\r\a", ""),
                            StudentGroupId = model.Id
                        };
                        if(string.IsNullOrEmpty(createModel.NumberOfBook))
                        {
                            break;
                        }
                        if (!string.IsNullOrEmpty(createModel.LastName) && createModel.LastName.Length > 1)
                        {
                            createModel.LastName = createModel.LastName[0] + createModel.LastName.Substring(1).ToLower();
                        }
                        if (!string.IsNullOrEmpty(createModel.FirstName) && createModel.FirstName.Length > 1)
                        {
                            createModel.FirstName = createModel.FirstName[0] + createModel.FirstName.Substring(1).ToLower();
                        }
                        if (!string.IsNullOrEmpty(createModel.Patronymic) && createModel.Patronymic.Length > 1)
                        {
                            createModel.Patronymic = createModel.Patronymic[0] + createModel.Patronymic.Substring(1).ToLower();
                        }
                        var result = CreateStudent(createModel);
                        if (!result.Succeeded)
                        {
                            document.Close();
                            return result;
                        }
                    }
                    document.Close();
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService CreateStudent(StudentRecordBindingModel model)
        {
            var entity = new Student
            {
                NumberOfBook = model.NumberOfBook,
                StudentGroupId = model.StudentGroupId,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Patronymic = model.Patronymic,
                Description = model.Description,
                Photo = model.Photo,
                DateCreate = DateTime.Now,
                IsDeleted = false
            };
            try
            {
                _context.Students.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sr.AppendFormat("ValidationErrors:\r\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ResultService.Error("error", sr.ToString(), 400);
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService UpdateStudent(StudentRecordBindingModel model)
        {
            try
            {
                var entity = _context.Students
                                .FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.LastName = model.LastName;
                entity.FirstName = model.FirstName;
                entity.Patronymic = model.Patronymic;
                entity.Description = model.Description;
                entity.Photo = model.Photo;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sr.AppendFormat("ValidationErrors:\r\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ResultService.Error("error", sr.ToString(), 400);
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService DeleteStudent(StudentGetBindingModel model)
        {
            try
            {
                var entity = _context.Students
                                .FirstOrDefault(e => e.NumberOfBook == model.NumberOfBook && !e.IsDeleted);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }
                entity.IsDeleted = true;
                entity.DateDelete = DateTime.Now;

                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    sr.AppendFormat("ValidationErrors:\r\n");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sr.AppendFormat("- Свойство: \"{0}\", Ошибка: \"{1}\"\r\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return ResultService.Error("error", sr.ToString(), 400);
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }
    }
}
