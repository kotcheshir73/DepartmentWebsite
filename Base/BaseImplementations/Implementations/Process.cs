﻿using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using Enums;
using System;
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

        public ResultService TransferCourse(StudentTransferBindingModel model)
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
                        Guid id = model.StudentList[i].Item1;

                        var entity = context.Students.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        // ищем приказ о движении контингента
                        var transferOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.TransferOrderNumber && x.DateCreate == model.TransferOrderDate.Date &&
                                x.StudentOrderType == StudentOrderType.Движение);
                        if (transferOrder == null)
                        {
                            // если нет, то создаем
                            transferOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                            {
                                OrderNumber = model.TransferOrderNumber,
                                OrderDate = model.TransferOrderDate.Date,
                                StudentOrderType = StudentOrderType.Движение.ToString()
                            });
                            context.StudentOrders.Add(transferOrder);
                            context.SaveChanges();
                        }
                        else if (transferOrder.IsDeleted)
                        {
                            transferOrder.IsDeleted = false;
                            context.SaveChanges();
                        }
                        // ищем блок приказа для направления
                        var transferOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == transferOrder.Id && x.StudentOrderType == StudentOrderType.ПереводНаКурс);
                        if (transferOrderBlock == null)
                        {
                            transferOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                            {
                                StudentOrderId = transferOrder.Id,
                                StudentOrderType = StudentOrderType.ПереводНаКурс.ToString()
                            });
                            context.StudentOrderBlocks.Add(transferOrderBlock);
                            context.SaveChanges();
                        }
                        else if (transferOrderBlock.IsDeleted)
                        {
                            transferOrderBlock.IsDeleted = false;
                            context.SaveChanges();
                        }
                        var transferSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == transferOrderBlock.Id && x.StudentId == entity.Id);
                        if (transferSOS == null)
                        {
                            transferSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                            {
                                StudentOrderBlockId = transferOrderBlock.Id,
                                StudentId = entity.Id,
                                StudentGroupFromId = entity.StudentGroupId,
                                StudentGroupToId = model.NewStudentGroupId,
                                Info = model.StudentList[i].Item2 ? "условно" : string.Empty
                            });
                            context.StudentOrderBlockStudents.Add(transferSOS);
                            context.SaveChanges();
                        }
                        else if (transferSOS.IsDeleted)
                        {
                            transferSOS.IsDeleted = false;
                            context.SaveChanges();
                        }

                        entity.StudentGroupId = model.NewStudentGroupId;

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

        public ResultService TransferGroup(StudentTransferBindingModel model)
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
                        Guid id = model.StudentList[i].Item1;

                        var entity = context.Students.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        // ищем приказ о движении контингента
                        var transferOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.TransferOrderNumber && x.DateCreate == model.TransferOrderDate.Date &&
                                x.StudentOrderType == StudentOrderType.Движение);
                        if (transferOrder == null)
                        {
                            // если нет, то создаем
                            transferOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                            {
                                OrderNumber = model.TransferOrderNumber,
                                OrderDate = model.TransferOrderDate.Date,
                                StudentOrderType = StudentOrderType.Движение.ToString()
                            });
                            context.StudentOrders.Add(transferOrder);
                            context.SaveChanges();
                        }
                        else if (transferOrder.IsDeleted)
                        {
                            transferOrder.IsDeleted = false;
                            context.SaveChanges();
                        }
                        // ищем блок приказа для направления
                        var transferOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == transferOrder.Id && x.StudentOrderType == StudentOrderType.ПереводВГруппу);
                        if (transferOrderBlock == null)
                        {
                            transferOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                            {
                                StudentOrderId = transferOrder.Id,
                                StudentOrderType = StudentOrderType.ПереводВГруппу.ToString()
                            });
                            context.StudentOrderBlocks.Add(transferOrderBlock);
                            context.SaveChanges();
                        }
                        else if (transferOrderBlock.IsDeleted)
                        {
                            transferOrderBlock.IsDeleted = false;
                            context.SaveChanges();
                        }
                        var transferSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == transferOrderBlock.Id && x.StudentId == entity.Id);
                        if (transferSOS == null)
                        {
                            transferSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                            {
                                StudentOrderBlockId = transferOrderBlock.Id,
                                StudentId = entity.Id,
                                StudentGroupFromId = entity.StudentGroupId,
                                StudentGroupToId = model.NewStudentGroupId
                            });
                            context.StudentOrderBlockStudents.Add(transferSOS);
                            context.SaveChanges();
                        }
                        else if (transferSOS.IsDeleted)
                        {
                            transferSOS.IsDeleted = false;
                            context.SaveChanges();
                        }

                        entity.StudentGroupId = model.NewStudentGroupId;
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
                    if (model.Studnets.Count <= 0)
                    {
                        return ResultService.Error("Error:", "Список студентов пуст", ResultServiceStatusCode.NotFound);
                    }

                    for (int i = 0; i < model.Studnets.Count; ++i)
                    {
                        Guid id = model.Studnets[i].Item1;

                        var entity = context.Students.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                        if (entity == null)
                        {
                            return ResultService.Error("Error:", "Студент не найден", ResultServiceStatusCode.NotFound);
                        }
                        // ищем приказ о движении
                        var deductionOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.DeductionOrderNumber && x.DateCreate == model.DeductionOrderDate.Date &&
                                x.StudentOrderType == StudentOrderType.Движение);
                        if (deductionOrder == null)
                        {
                            // если нет, то создаем
                            deductionOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                            {
                                OrderNumber = model.DeductionOrderNumber,
                                OrderDate = model.DeductionOrderDate.Date,
                                StudentOrderType = StudentOrderType.Движение.ToString()
                            });
                            context.StudentOrders.Add(deductionOrder);
                            context.SaveChanges();
                        }
                        else if (deductionOrder.IsDeleted)
                        {
                            deductionOrder.IsDeleted = false;
                            context.SaveChanges();
                        }
                        StudentOrderType type = StudentOrderType.ОтчислитьПоСобственному;
                        if(model.DeductionReason.Contains("неуспеваем"))
                        {
                            type = StudentOrderType.ОтчислитьЗаНеуспевамость;
                        }
                        else if (model.DeductionReason.Contains("перевод"))
                        {
                            type = StudentOrderType.ОтчислитьВСвязиСПереводом;
                        }
                        // ищем блок приказа для направления
                        var deductionOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == deductionOrder.Id && x.StudentOrderType == type);
                        if (deductionOrderBlock == null)
                        {
                            deductionOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                            {
                                StudentOrderId = deductionOrder.Id,
                                StudentOrderType = type.ToString()
                            });
                            context.StudentOrderBlocks.Add(deductionOrderBlock);
                            context.SaveChanges();
                        }
                        else if (deductionOrderBlock.IsDeleted)
                        {
                            deductionOrderBlock.IsDeleted = false;
                            context.SaveChanges();
                        }
                        var deductionSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == deductionOrderBlock.Id && x.StudentId == entity.Id);
                        if (deductionSOS == null)
                        {
                            deductionSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                            {
                                StudentOrderBlockId = deductionOrderBlock.Id,
                                StudentId = entity.Id,
                                StudentGroupFromId = entity.StudentGroupId,
                                Info = model.Studnets[i].Item2
                            });
                            context.StudentOrderBlockStudents.Add(deductionSOS);
                            context.SaveChanges();
                        }
                        else if (deductionSOS.IsDeleted)
                        {
                            deductionSOS.IsDeleted = false;
                            context.SaveChanges();
                        }

                        entity.StudentState = StudentState.Отчислен;
                        entity.StudentGroupId = null;
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
                        context.SaveChanges();

                        // ищем приказ о движении контингента
                        var academOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.AcademOrderNumber && x.DateCreate == model.AcademOrderDate.Date &&
                                x.StudentOrderType == StudentOrderType.Движение);
                        if (academOrder == null)
                        {
                            // если нет, то создаем
                            academOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                            {
                                OrderNumber = model.AcademOrderNumber,
                                OrderDate = model.AcademOrderDate.Date,
                                StudentOrderType = StudentOrderType.Движение.ToString()
                            });
                            context.StudentOrders.Add(academOrder);
                            context.SaveChanges();
                        }
                        else if (academOrder.IsDeleted)
                        {
                            academOrder.IsDeleted = false;
                            context.SaveChanges();
                        }
                        // ищем блок приказа для направления
                        var academOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == academOrder.Id && x.StudentOrderType == StudentOrderType.ВАкадем);
                        if (academOrderBlock == null)
                        {
                            academOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                            {
                                StudentOrderId = academOrder.Id,
                                StudentOrderType = StudentOrderType.ВАкадем.ToString()
                            });
                            context.StudentOrderBlocks.Add(academOrderBlock);
                            context.SaveChanges();
                        }
                        else if (academOrderBlock.IsDeleted)
                        {
                            academOrderBlock.IsDeleted = false;
                            context.SaveChanges();
                        }
                        var academSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == academOrderBlock.Id && x.StudentId == entity.Id);
                        if (academSOS == null)
                        {
                            academSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                            {
                                StudentOrderBlockId = academOrderBlock.Id,
                                StudentId = entity.Id,
                                StudentGroupFromId = entity.StudentGroupId
                            });
                            context.StudentOrderBlockStudents.Add(academSOS);
                            context.SaveChanges();
                        }
                        else if (academSOS.IsDeleted)
                        {
                            academSOS.IsDeleted = false;
                            context.SaveChanges();
                        }
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
                        context.SaveChanges();

                        // ищем приказ о движении контингента
                        var academOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.AcademOrderNumber && x.DateCreate == model.AcademOrderDate.Date &&
                                x.StudentOrderType == StudentOrderType.Движение);
                        if (academOrder == null)
                        {
                            // если нет, то создаем
                            academOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                            {
                                OrderNumber = model.AcademOrderNumber,
                                OrderDate = model.AcademOrderDate.Date,
                                StudentOrderType = StudentOrderType.Движение.ToString()
                            });
                            context.StudentOrders.Add(academOrder);
                            context.SaveChanges();
                        }
                        else if (academOrder.IsDeleted)
                        {
                            academOrder.IsDeleted = false;
                            context.SaveChanges();
                        }
                        // ищем блок приказа для направления
                        var academOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == academOrder.Id && x.StudentOrderType == StudentOrderType.ИзАкадема);
                        if (academOrderBlock == null)
                        {
                            academOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                            {
                                StudentOrderId = academOrder.Id,
                                StudentOrderType = StudentOrderType.ИзАкадема.ToString()
                            });
                            context.StudentOrderBlocks.Add(academOrderBlock);
                            context.SaveChanges();
                        }
                        else if (academOrderBlock.IsDeleted)
                        {
                            academOrderBlock.IsDeleted = false;
                            context.SaveChanges();
                        }
                        var academSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == academOrderBlock.Id && x.StudentId == entity.Id);
                        if (academSOS == null)
                        {
                            academSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                            {
                                StudentOrderBlockId = academOrderBlock.Id,
                                StudentId = entity.Id,
                                StudentGroupToId = entity.StudentGroupId
                            });
                            context.StudentOrderBlockStudents.Add(academSOS);
                            context.SaveChanges();
                        }
                        else if (academSOS.IsDeleted)
                        {
                            academSOS.IsDeleted = false;
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

        public ResultService FinishEducation(FinishEducationBindingModel model)
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
                        // ищем приказ о движении контингента
                        var finishOrder = context.StudentOrders.FirstOrDefault(x => x.OrderNumber == model.FinishEducationOrderNumber && x.DateCreate == model.FinishEducationOrderDate.Date &&
                                x.StudentOrderType == StudentOrderType.Движение);
                        if (finishOrder == null)
                        {
                            // если нет, то создаем
                            finishOrder = ModelFacotryFromBindingModel.CreateStudentOrder(new StudentOrderSetBindingModel
                            {
                                OrderNumber = model.FinishEducationOrderNumber,
                                OrderDate = model.FinishEducationOrderDate.Date,
                                StudentOrderType = StudentOrderType.Движение.ToString()
                            });
                            context.StudentOrders.Add(finishOrder);
                            context.SaveChanges();
                        }
                        else if (finishOrder.IsDeleted)
                        {
                            finishOrder.IsDeleted = false;
                            context.SaveChanges();
                        }
                        // ищем блок приказа для направления
                        var finishOrderBlock = context.StudentOrderBlocks.FirstOrDefault(x => x.StudentOrderId == finishOrder.Id && x.StudentOrderType == StudentOrderType.ОтчислитьПоЗавершению);
                        if (finishOrderBlock == null)
                        {
                            finishOrderBlock = ModelFacotryFromBindingModel.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                            {
                                StudentOrderId = finishOrder.Id,
                                StudentOrderType = StudentOrderType.ОтчислитьПоЗавершению.ToString()
                            });
                            context.StudentOrderBlocks.Add(finishOrderBlock);
                            context.SaveChanges();
                        }
                        else if (finishOrderBlock.IsDeleted)
                        {
                            finishOrderBlock.IsDeleted = false;
                            context.SaveChanges();
                        }
                        var finishSOS = context.StudentOrderBlockStudents.FirstOrDefault(x => x.StudentOrderBlockId == finishOrderBlock.Id && x.StudentId == entity.Id);
                        if (finishSOS == null)
                        {
                            finishSOS = ModelFacotryFromBindingModel.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                            {
                                StudentOrderBlockId = finishOrderBlock.Id,
                                StudentId = entity.Id,
                                StudentGroupFromId = entity.StudentGroupId
                            });
                            context.StudentOrderBlockStudents.Add(finishSOS);
                            context.SaveChanges();
                        }
                        else if (finishSOS.IsDeleted)
                        {
                            finishSOS.IsDeleted = false;
                            context.SaveChanges();
                        }
                        entity.StudentState = StudentState.Завершил;
                        entity.StudentGroupId = null;

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