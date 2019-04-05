using DatabaseContext;
using Interfaces;
using Interfaces.BindingModels;
using Interfaces.Interfaces;
using Interfaces.ViewModels;
using Models.Base;
using Models.Enums;
using System;
using System.IO;
using System.Linq;

namespace Implementations
{
    public class Process : IProcess
    {
        public ResultService<StudentPageViewModel> LoadStudentsFromFile(StudentLoadDocBindingModel model)
        {
            //var word = new Application();
            if (File.Exists(model.FileName))
            {
                //Document document = word.Documents.Open(model.FileName, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //var table = document.Tables[1];

                //try
                //{
                //    var list = new List<StudentViewModel>();
                //    for (int i = 2; i <= table.Rows.Count; ++i)
                //    {
                //        var studentModel = new StudentViewModel
                //        {
                //            NumberOfBook = table.Cell(i, 2).Range.Text.Replace("\r\a", ""),
                //            StudentGroupId = model.Id,
                //            LastName = table.Cell(i, 3).Range.Text.Replace("\r\a", ""),
                //            FirstName = table.Cell(i, 4).Range.Text.Replace("\r\a", ""),
                //            Patronymic = table.Cell(i, 5).Range.Text.Replace("\r\a", ""),
                //            Email = "отсутсвует",
                //            Description = string.Format("{0}  {1}", table.Cell(i, 6).Range.Text.Replace("\r\a", ""),
                //            table.Cell(i, 7).Range.Text.Replace("\r\a", ""))
                //        };

                //        if (string.IsNullOrEmpty(studentModel.NumberOfBook))
                //        {
                //            break;
                //        }
                //        if (!string.IsNullOrEmpty(studentModel.LastName) && studentModel.LastName.Length > 1)
                //        {
                //            studentModel.LastName = studentModel.LastName[0] + studentModel.LastName.Substring(1).ToLower();
                //        }
                //        if (!string.IsNullOrEmpty(studentModel.FirstName) && studentModel.FirstName.Length > 1)
                //        {
                //            studentModel.FirstName = studentModel.FirstName[0] + studentModel.FirstName.Substring(1).ToLower();
                //        }
                //        if (!string.IsNullOrEmpty(studentModel.Patronymic) && studentModel.Patronymic.Length > 1)
                //        {
                //            studentModel.Patronymic = studentModel.Patronymic[0] + studentModel.Patronymic.Substring(1).ToLower();
                //        }

                //        list.Add(studentModel);
                //    }
                //    document.Close();

                //    var result = new StudentPageViewModel
                //    {
                //        MaxCount = list.Count,
                //        List = list
                //    };

                //    return ResultService<StudentPageViewModel>.Success(result);
                //}
                //catch (Exception ex)
                //{
                //    document.Close();
                //    return ResultService<StudentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
                //}
            }
            return ResultService<StudentPageViewModel>.Error("Error:", "File not found", ResultServiceStatusCode.FileNotFound);
        }

        public ResultService EnrollmentStudents(StudentEnrollmentBindingModel model)
        {
            DepartmentUserManager.CheckAccess(AccessOperation.Студенты_учащиеся, AccessType.Change, "Студенты");

            using (var context = new DatabaseContext.DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.StudentList.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Студенты не найдены", ResultServiceStatusCode.NotFound);
                    }
                    for (int i = 0; i < model.StudentList.Count; ++i)
                    {
                        var entity = ModelFacotryFromBindingModel.CreateStudent(model.StudentList[i]);

                        context.Students.Add(entity);

                        context.SaveChanges();

                        var entityHistory = new StudentHistory
                        {
                            StudentId = entity.Id,
                            DateCreate = DateTime.Now,
                            TextMessage = string.Format("Студент зачислен в группу {0} по приказу №{1} от {2}", entity.StudentGroupId,
                            model.OrderNumber, model.OrderDate.ToShortDateString())
                        };

                        context.StudentHistorys.Add(entityHistory);

                        context.SaveChanges();
                    }
                    transaction.Commit();

                    return ResultService.Success();
                }
                catch (Exception ex)
                {
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        public ResultService TransferStudents(StudentTransferBindingModel model)
        {
            DepartmentUserManager.CheckAccess(AccessOperation.Студенты_учащиеся, AccessType.Change, "Студенты");

            using (var context = new DatabaseContext.DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.StudentList.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Список студентов пуст", ResultServiceStatusCode.NotFound);
                    }

                    var newGroup = context.StudentGroups.FirstOrDefault(st => st.Id == model.NewStudentGroupId);
                    if (newGroup == null)
                    {
                        return ResultService.Error("Error:", "Группа не найдена", ResultServiceStatusCode.NotFound);
                    }
                    else if(newGroup.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Группа удалена", ResultServiceStatusCode.WasDelete);
                    }

                    for (int i = 0; i < model.StudentList.Count; ++i)
                    {
                        string numberofBook = model.StudentList[i].NumberOfBook;

                        var entity = context.Students.FirstOrDefault(e => e.NumberOfBook == numberofBook && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        entity.StudentGroup = newGroup;

                        context.SaveChanges();

                        var entityHistory = new StudentHistory
                        {
                            StudentId = entity.Id,
                            DateCreate = DateTime.Now,
                            TextMessage = string.Format("Студент переведен в группу {0} на основании: {1} {2}", entity.StudentGroup.GroupName,
                                model.TransferReason, model.TransferDate.ToShortDateString())
                        };
                        context.StudentHistorys.Add(entityHistory);

                        context.SaveChanges();
                    }
                    transaction.Commit();

                    return ResultService.Success();
                }
                catch (Exception ex)
                {
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        public ResultService DeductionStudents(StudentDeductionBindingModel model)
        {
            DepartmentUserManager.CheckAccess(AccessOperation.Студенты_учащиеся, AccessType.Change, "Студенты");

            using (var context = new DatabaseContext.DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.StudentList.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Список студентов пуст", ResultServiceStatusCode.NotFound);
                    }

                    for (int i = 0; i < model.StudentList.Count; ++i)
                    {
                        string numberofBook = model.StudentList[i].NumberOfBook;

                        var entity = context.Students.FirstOrDefault(e => e.NumberOfBook == numberofBook && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        entity.StudentState = StudentState.Завершил;
                        entity.StudentGroup = null;

                        context.SaveChanges();

                        var entityHistory = new StudentHistory
                        {
                            StudentId = entity.Id,
                            DateCreate = DateTime.Now,
                            TextMessage = string.Format("Студент отчислен на основании: {0}. Приказ №{1} от {2}",
                                model.DeductionReason, model.DeductionOrderNumber, model.DeductionDate.ToShortDateString())
                        };

                        context.StudentHistorys.Add(entityHistory);

                        context.SaveChanges();
                    }
                    transaction.Commit();

                    return ResultService.Success();
                }
                catch (Exception ex)
                {
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        public ResultService ToAcademStudents(StudentToAcademBindingModel model)
        {
            DepartmentUserManager.CheckAccess(AccessOperation.Студенты_учащиеся, AccessType.Change, "Студенты");

            using (var context = new DatabaseContext.DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.StudentList.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Студенты не найдены", ResultServiceStatusCode.NotFound);
                    }

                    for (int i = 0; i < model.StudentList.Count; ++i)
                    {
                        string numberofBook = model.StudentList[i].NumberOfBook;

                        var entity = context.Students.FirstOrDefault(e => e.NumberOfBook == numberofBook && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        entity.StudentState = StudentState.Академ;
                        entity.StudentGroup = null;

                        context.SaveChanges();

                        var entityHistory = new StudentHistory
                        {
                            StudentId = entity.Id,
                            DateCreate = DateTime.Now,
                            TextMessage = string.Format("Студент ушел в академ на основании: {0}. Приказ №{1} от {2}",
                                model.ToAcademReason, model.ToAcademOrderNumber, model.ToAcademDate.ToShortDateString())
                        };

                        context.StudentHistorys.Add(entityHistory);

                        context.SaveChanges();
                    }
                    transaction.Commit();

                    return ResultService.Success();
                }
                catch (Exception ex)
                {
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }
    }
}