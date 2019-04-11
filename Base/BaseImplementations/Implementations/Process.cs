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
        public ResultService EnrollmentStudents(StudentEnrollmentBindingModel model)
        {
            if (model.StudentList.Count <= 0)
            {
                return ResultService.Error("Error:", "Студенты не найдены", ResultServiceStatusCode.NotFound);
            }
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
                        for (int i = 0; i < model.StudentList.Count; ++i)
                        {
                            #region приказы
                            // ищем приказ о зачислении
                            var enrollmentOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.EnrollmentOrderNumber && x.DateCreate == model.EnrollmentOrderDate.Date &&
                                    x.StudentOrderType == StudentOrderType.Зачисление);
                            if(enrollmentOrder == null)
                            {
                                // если нет, то создаем
                                enrollmentOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                                {
                                    OrderNumber = model.EnrollmentOrderNumber,
                                    OrderDate = model.EnrollmentOrderDate.Date,
                                    StudentOrderType = StudentOrderType.Зачисление.ToString()
                                });
                                context.StudentOrders.Add(enrollmentOrder);
                                context.SaveChanges();
                            }
                            else if(enrollmentOrder.IsDeleted)
                            {
                                enrollmentOrder.IsDeleted = false;
                                context.SaveChanges();
                            }
                            // ищем блок приказа для направления
                            var enrollmentOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == enrollmentOrder.Id && x.EducationDirectionId == studentGroup.EducationDirectionId);
                            if (enrollmentOrderBlock == null)
                            {
                                enrollmentOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                                {
                                    StudentOrderId = enrollmentOrder.Id,
                                    StudentOrderType = StudentOrderType.Зачисление.ToString(),
                                    EducationDirectionId = studentGroup.EducationDirectionId
                                });
                                context.StudentOrderBlocks.Add(enrollmentOrderBlock);
                                context.SaveChanges();
                            }
                            else if(enrollmentOrderBlock.IsDeleted)
                            {
                                enrollmentOrderBlock.IsDeleted = false;
                                context.SaveChanges();
                            }
                            // ищем приказ о распределении
                            var distributionOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.DistributionOrderNumber && x.DateCreate == model.DistributionOrderDate.Date &&
                                    x.StudentOrderType == StudentOrderType.Распределение);
                            if (distributionOrder == null)
                            {
                                // если нет, то создаем
                                distributionOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                                {
                                    OrderNumber = model.DistributionOrderNumber,
                                    OrderDate = model.DistributionOrderDate.Date,
                                    StudentOrderType = StudentOrderType.Распределение.ToString()
                                });
                                context.StudentOrders.Add(distributionOrder);
                                context.SaveChanges();
                            }
                            else if (distributionOrder.IsDeleted)
                            {
                                distributionOrder.IsDeleted = false;
                                context.SaveChanges();
                            }
                            // ищем блок приказа для группы
                            var distributionOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == distributionOrder.Id && x.EducationDirectionId == studentGroup.EducationDirectionId);
                            if (distributionOrderBlock == null)
                            {
                                distributionOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                                {
                                    StudentOrderId = distributionOrder.Id,
                                    StudentOrderType = StudentOrderType.Распределение.ToString(),
                                    EducationDirectionId = studentGroup.EducationDirectionId
                                });
                                context.StudentOrderBlocks.Add(distributionOrderBlock);
                                context.SaveChanges();
                            }
                            else if (distributionOrderBlock.IsDeleted)
                            {
                                distributionOrderBlock.IsDeleted = false;
                                context.SaveChanges();
                            }
                            #endregion

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

                            var enrolleSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == enrollmentOrderBlock.Id && x.StudentId == entity.Id);
                            if(enrolleSOS == null)
                            {
                                enrolleSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                                {
                                    StudentOrderBlockId = enrollmentOrderBlock.Id,
                                    StudentId = entity.Id
                                });
                                context.StudentOrderBlockStudents.Add(enrolleSOS);
                                context.SaveChanges();
                            }
                            else if(enrolleSOS.IsDeleted)
                            {
                                enrolleSOS.IsDeleted = false;
                                context.SaveChanges();
                            }

                            var distributionSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == distributionOrderBlock.Id && x.StudentId == entity.Id &&
                                x.StudentGroupToId == studentGroup.Id);
                            if (distributionSOS == null)
                            {
                                distributionSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                                {
                                    StudentOrderBlockId = distributionOrderBlock.Id,
                                    StudentId = entity.Id,
                                    StudentGroupToId = studentGroup.Id
                                });
                                context.StudentOrderBlockStudents.Add(distributionSOS);
                                context.SaveChanges();
                            }
                            else if (distributionSOS.IsDeleted)
                            {
                                distributionSOS.IsDeleted = false;
                                context.SaveChanges();
                            }

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
                        entity.StudentGroupId = model.NewStudentGroupId;

                        context.SaveChanges();

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

        public ResultService ToAcademStudents(StudentAcademBindingModel model)
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

        public ResultService FromAcademStudents(StudentAcademBindingModel model)
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
                        entity.StudentState = StudentState.Учится;
                        entity.StudentGroup = null;

                        context.SaveChanges();

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

        public ResultService RecoveryStudents(StudentRecoveryBindingModel model)
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
                        entity.StudentState = StudentState.Учится;
                        entity.StudentGroup = null;

                        context.SaveChanges();

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

        public ResultService TransferSpecStudents(StudentTransferBindingModel model)
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
                        entity.StudentGroupId = model.NewStudentGroupId;

                        context.SaveChanges();

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