using System;
using System.Collections.Generic;

namespace BaseInterfaces.BindingModels
{
    /// <summary>
    /// Загрузка студентов из файла
    /// </summary>
    public class StudentLoadDocBindingModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }
    }

    /// <summary>
    /// Список студентов на зачисление, + приказ
    /// </summary>
    public class StudentEnrollmentBindingModel
    {
        public string EnrollmentOrderNumber { get; set; }

        public DateTime EnrollmentOrderDate { get; set; }

        public List<StudentSetBindingModel> StudentList { get; set; }
    }

    /// <summary>
    /// Список студентов на зачисление, + приказ
    /// </summary>
    public class StudentEnrollmentTransferBindingModel
    {
        public string EnrollmentTransferOrderNumber { get; set; }

        public DateTime EnrollmentTransferOrderDate { get; set; }

        public List<StudentSetBindingModel> StudentList { get; set; }
    }

    /// <summary>
    /// Список студентов на перевод из одной группы в другую + причина перевода (включая восстановление после академа)
    /// </summary>
    public class StudentTransferBindingModel
    {
        public Guid? NewStudentGroupId { get; set; }

        public Guid? OldStudentGroupId { get; set; }

        public List<Tuple<Guid, bool>> StudentList { get; set; }

        public DateTime TransferOrderDate { get; set; }

        public string TransferOrderNumber { get; set; }
    }

    /// <summary>
    /// Список студентов на отчисление + приказ
    /// </summary>
    public class StudentDeductionBindingModel
    {
        public List<Tuple<Guid, string>> Studnets { get; set; }

        public string DeductionOrderNumber { get; set; }

        public DateTime DeductionOrderDate { get; set; }

        public string DeductionReason { get; set; }
    }

    /// <summary>
    /// Список студентов на перевод в академ + приказ
    /// </summary>
    public class StudentAcademBindingModel
    {
        public List<Guid> StudnetIds { get; set; }

        public DateTime AcademOrderDate { get; set; }

        public string AcademOrderNumber { get; set; }
    }

    /// <summary>
    /// Список студентов на восстановление
    /// </summary>
    public class StudentRecoveryBindingModel
    {
        public List<Guid> StudnetIds { get; set; }

        public DateTime RecoveryOrderDate { get; set; }

        public string RecoveryOrderNumber { get; set; }

        public Guid StudentGroupId { get; set; }
    }

    /// <summary>
    /// Список студентов, закончивших обучение
    /// </summary>
    public class FinishEducationBindingModel
    {
        public List<Guid> StudnetIds { get; set; }

        public DateTime FinishEducationOrderDate { get; set; }

        public string FinishEducationOrderNumber { get; set; }
    }

    /// <summary>
    /// Список приказов по студенту
    /// </summary>
    public class StudentOrdersShowBindingModel
    {
        public Guid Id { get; set; }
    }
}