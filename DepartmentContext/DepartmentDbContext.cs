using AuthenticationModels.Models;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ScheduleModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TicketModels.Models;
using System.Data.Entity.Validation;

namespace DepartmentContext
{
    [Table("DepartmentDatabase")]
    //https://docs.microsoft.com/ru-ru/aspnet/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity
    public class DepartmentDbContext : IdentityDbContext<DepartmentUser, DepartmentRole, Guid, DepartmentUserLogin, DepartmentUserRole, DepartmentUserClaim>
    {
        public DepartmentDbContext() : base("DepartmentDatabase")
        {//настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new DatabaseInitializer());
        }

        public virtual DbSet<DepartmentAccess> Accesses { set; get; }


        public virtual DbSet<AcademicPlan> AcademicPlans { get; set; }
        public virtual DbSet<AcademicPlanRecord> AcademicPlanRecords { get; set; }
        public virtual DbSet<AcademicPlanRecordElement> AcademicPlanRecordElements { get; set; }
        public virtual DbSet<AcademicPlanRecordMission> AcademicPlanRecordMissions { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Classroom> Classrooms { set; get; }
        public virtual DbSet<Contingent> Contingents { get; set; }
        public virtual DbSet<CurrentSettings> CurrentSettings { set; get; }
        public virtual DbSet<Discipline> Disciplines { set; get; }
        public virtual DbSet<DisciplineBlock> DisciplineBlocks { get; set; }
        public virtual DbSet<DisciplineLesson> DisciplineLessons { get; set; }
        public virtual DbSet<DisciplineLessonTask> DisciplineLessonTasks { get; set; }
        public virtual DbSet<DisciplineLessonTaskVariant> DisciplineLessonTaskVariants { get; set; }
        public virtual DbSet<DisciplineLessonTaskStudentAccept> DisciplineLessonTaskStudentAccepts { get; set; }
        public virtual DbSet<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
        public virtual DbSet<DisciplineLessonConducted> DisciplineLessonConducteds { get; set; }
        public virtual DbSet<DisciplineLessonConductedStudent> DisciplineLessonConductedStudents { get; set; }
        public virtual DbSet<EducationDirection> EducationDirections { set; get; }
        public virtual DbSet<Grafic> Grafics { get; set; }
        public virtual DbSet<GraficRecord> GraficRecords { get; set; }
        public virtual DbSet<GraficClassroom> GraficClassrooms { get; set; }
        public virtual DbSet<IndividualPlanKindOfWork> IndividualPlanKindOfWorks { get; set; }
        public virtual DbSet<IndividualPlanRecord> IndividualPlanRecords { get; set; }
        public virtual DbSet<IndividualPlanTitle> IndividualPlanTitles { get; set; }
        public virtual DbSet<IndividualPlanNIRScientificArticle> IndividualPlanNIRScientificArticles { get; set; }
        public virtual DbSet<IndividualPlanNIRContractualWork> IndividualPlanNIRContractualWorks { get; set; }
        public virtual DbSet<Lecturer> Lecturers { set; get; }
        public virtual DbSet<LecturerPost> LecturerPosts { get; set; }
        public virtual DbSet<LecturerWorkload> LecturerWorkload { get; set; }
        public virtual DbSet<Message> Messages { set; get; }
        public virtual DbSet<SeasonDates> SeasonDates { set; get; }
        public virtual DbSet<Statement> Statements { set; get; }
        public virtual DbSet<StatementRecord> StatementRecords { set; get; }
        public virtual DbSet<StatementRecordExtended> StatementRecordExtendeds { set; get; }
        public virtual DbSet<StreamingLesson> StreamingLessons { set; get; }
        public virtual DbSet<StreamLesson> StreamLessons { set; get; }
        public virtual DbSet<StreamLessonRecord> StreamLessonRecords { set; get; }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<StudentGroup> StudentGroups { set; get; }
        public virtual DbSet<StudentHistory> StudentHistorys { set; get; }
        public virtual DbSet<TimeNorm> TimeNorms { get; set; }

        public virtual DbSet<ConsultationRecord> ConsultationRecords { set; get; }
        public virtual DbSet<ExaminationRecord> ExaminationRecords { set; get; }
        public virtual DbSet<OffsetRecord> OffsetRecords { set; get; }
        public virtual DbSet<SemesterRecord> SemesterRecords { set; get; }
        public virtual DbSet<ScheduleLessonTime> ScheduleLessonTimes { set; get; }

        public virtual DbSet<MaterialTechnicalValue> MaterialTechnicalValues { set; get; }
        public virtual DbSet<MaterialTechnicalValueGroup> MaterialTechnicalValueGroups { set; get; }
        public virtual DbSet<MaterialTechnicalValueRecord> MaterialTechnicalValueRecords { set; get; }
        public virtual DbSet<SoftwareRecord> SoftwareRecords { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }

        public virtual DbSet<ExaminationTemplate> ExaminationTemplates { get; set; }
        public virtual DbSet<ExaminationTemplateBlock> ExaminationTemplateBlocks { get; set; }
        public virtual DbSet<ExaminationTemplateBlockQuestion> ExaminationTemplateBlockQuestions { get; set; }
        public virtual DbSet<ExaminationTemplateTicket> ExaminationTemplateTickets { get; set; }
        public virtual DbSet<ExaminationTemplateTicketQuestion> ExaminationTemplateTicketQuestions { get; set; }

        public virtual DbSet<TicketTemplate> TicketTemplates { get; set; }
        public virtual DbSet<TicketTemplateBody> TicketTemplateBodies { get; set; }
        public virtual DbSet<TicketTemplateTable> TicketTemplateTables { get; set; }
        public virtual DbSet<TicketTemplateTableRow> TicketTemplateTableRows { get; set; }
        public virtual DbSet<TicketTemplateTableCell> TicketTemplateTableCells { get; set; }
        public virtual DbSet<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }
        public virtual DbSet<TicketTemplateParagraphData> TicketTemplateParagraphDatas { get; set; }
        public virtual DbSet<TicketTemplateElementaryUnit> TicketTemplateElementaryUnits { get; set; }
        public virtual DbSet<TicketTemplateElementaryAttribute> TicketTemplateElementaryAttributes { get; set; }

        /// <summary>
        /// Перегружаем метод созранения изменений. Если возникла ошибка - очищаем все изменения
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var ffhf = ResultService.Error(ex, ResultServiceStatusCode.Error);
                return 0;
            }
            catch (Exception ex)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}