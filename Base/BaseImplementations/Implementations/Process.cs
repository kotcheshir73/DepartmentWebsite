using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using Models.Base;
using System;
using System.IO;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
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

            using (var context = DepartmentUserManager.GetContext)
            {
                var studentGroup = context.StudentGroups.FirstOrDefault(x => x.Id == model.StudentList.First().StudentGroupId);

                if (studentGroup == null)
                {
                    return ResultService.Error("Error:", "Группа не найдена", ResultServiceStatusCode.NotFound);
                }
                else if (studentGroup.IsDeleted)
                {
                    return ResultService.Error("Error:", "Группа была удалена", ResultServiceStatusCode.WasDelete);
                }
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
                            var exsistEntity = context.Students.FirstOrDefault(x => x.NumberOfBook == entity.NumberOfBook && entity.NumberOfBook != "н/а");
                            if (exsistEntity == null)
                            {
                                context.Students.Add(entity);
                                context.SaveChanges();
                            }
                            else
                            {
                                entity = ModelFacotryFromBindingModel.CreateStudent(model.StudentList[i], exsistEntity);
                                entity.StudentState = StudentState.Учится;
                                if (exsistEntity.IsDeleted)
                                {
                                    exsistEntity.IsDeleted = false;
                                    context.SaveChanges();
                                }
                            }

                            var entityHistory = new StudentHistory
                            {
                                StudentId = entity.Id,
                                DateCreate = DateTime.Now,
                                TextMessage = string.Format("Студент зачислен в группу {0} по приказу №{1} от {2}", studentGroup.GroupName,
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
        }

        public ResultService TransferStudents(StudentTransferBindingModel model)
        {
            DepartmentUserManager.CheckAccess(AccessOperation.Студенты_учащиеся, AccessType.Change, "Студенты");

            using (var context = DepartmentUserManager.GetContext)
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
                    else if (newGroup.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Группа удалена", ResultServiceStatusCode.WasDelete);
                    }

                    for (int i = 0; i < model.StudentList.Count; ++i)
                    {
                        Guid id = model.StudentList[i].Id;

                        var entity = context.Students.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
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
                            TextMessage = string.Format("Студент переведен{3} в группу {0} на основании: {1} {2}", entity.StudentGroup.GroupName,
                                model.TransferOrderNumber, model.TransferDate.ToShortDateString(), model.IsConditionally? " условно" : string.Empty)
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

            using (var context = DepartmentUserManager.GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.StudnetIds.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Список студентов пуст", ResultServiceStatusCode.NotFound);
                    }

                    for (int i = 0; i < model.StudnetIds.Count; ++i)
                    {
                        Guid id = model.StudnetIds[i];

                        var entity = context.Students.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        entity.StudentState = StudentState.Отчислен;
                        entity.StudentGroupId = null;

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

            using (var context = DepartmentUserManager.GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.StudnetIds.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Студенты не найдены", ResultServiceStatusCode.NotFound);
                    }

                    for (int i = 0; i < model.StudnetIds.Count; ++i)
                    {
                        Guid id = model.StudnetIds[i];

                        var entity = context.Students.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
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
                            TextMessage = string.Format("Студент ушел в академ. Приказ №{0} от {1}",
                                model.ToAcademOrderNumber, model.ToAcademDate.ToShortDateString())
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