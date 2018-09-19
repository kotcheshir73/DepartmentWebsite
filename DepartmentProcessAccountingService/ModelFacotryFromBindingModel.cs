using DepartmentModel.Models.ProcessAccountingModels;
using DepartmentProcessAccountingService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService
{
    public static class ModelFacotryFromBindingModel
    {
        public static AcademicYearProcess CreateAcademicYearProcess(AcademicYearProcessRecordBindingModel model, AcademicYearProcess entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicYearProcess();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.DepartmentProcessId = model.DepartmentProcessId;
            entity.IsConfirmed = model.IsConfirmed;
            entity.Status = model.Status;
            entity.UserConfirmedId = model.UserConfirmedId;
            entity.UserId = model.UserId;
            
            return entity;
        }

        public static DepartmentProcess CreateDepartmentProcess(DepartmentProcessRecordBindingModel model, DepartmentProcess entity = null)
        {
            if (entity == null)
            {
                entity = new DepartmentProcess();
            }
            entity.Confirmability = model.Confirmability;
            entity.DateFinish = model.DateFinish;
            entity.DateStart = model.DateStart;
            entity.Description = model.Description;
            entity.Executor = model.Executor;
            entity.Periodicity = model.Periodicity;
            entity.SemesterDateFinish = model.SemesterDateFinish;
            entity.SemesterDateFinishIndent = model.SemesterDateFinishIndent;
            entity.SemesterDateStart = model.SemesterDateStart;
            entity.SemesterDateStartIndent = model.SemesterDateStartIndent;
            entity.Title = model.Title;
            
            return entity;
        }

        public static ProcessDirectionRecord CreateProcessDirectionRecord(ProcessDirectionRecordRecordBindingModel model, ProcessDirectionRecord entity = null)
        {
            if (entity == null)
            {
                entity = new ProcessDirectionRecord();
            }
            entity.AcademicCourse = model.AcademicCourse;
            entity.DepartmentProcessId = model.DepartmentProcessId;
            entity.EducationDirectionId = model.EducationDirectionId;

            return entity;
        }
    }
}
