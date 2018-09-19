using DepartmentModel.Models.ProcessAccountingModels;
using DepartmentProcessAccountingService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService
{
    public static class ModelFactoryToViewModel
    {
        public static AcademicYearProcessViewModel CreateAcademicYearProcessViewModel(AcademicYearProcess entity)
        {
            return new AcademicYearProcessViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                DepartmentProcessId = entity.DepartmentProcessId,
                IsConfirmed = entity.IsConfirmed,
                Status = entity.Status,
                UserConfirmedId = entity.UserConfirmedId,
                UserId = entity.UserId
            };
        }

        public static DepartmentProcessViewModel CreateDepartmentProcessViewModel(DepartmentProcess entity)
        {
            return new DepartmentProcessViewModel
            {
                Id = entity.Id,
                Confirmability = entity.Confirmability,
                DateFinish = entity.DateFinish,
                DateStart = entity.DateStart,
                Description = entity.Description,
                Executor = entity.Executor,
                Periodicity = entity.Periodicity,
                SemesterDateFinish = entity.SemesterDateFinish,
                SemesterDateFinishIndent = entity.SemesterDateFinishIndent,
                SemesterDateStart = entity.SemesterDateStart,
                SemesterDateStartIndent = entity.SemesterDateStartIndent,
                Title = entity.Title
            };
        }

        public static ProcessDirectionRecordViewModel CreateProcessDirectionRecordViewModel(ProcessDirectionRecord entity)
        {
            return new ProcessDirectionRecordViewModel
            {
                Id = entity.Id,
                AcademicCourse = entity.AcademicCourse,
                DepartmentProcessId = entity.DepartmentProcessId,
                EducationDirectionId = entity.EducationDirectionId
            };
        }
    }
}
