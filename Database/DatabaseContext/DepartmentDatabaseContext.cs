using Microsoft.EntityFrameworkCore;
using Models;
using Models.AcademicYearData;
using Models.Authentication;
using Models.Base;
using Models.Examination;
using Models.LaboratoryHead;
using Models.LearningProgress;
using Models.Schedule;
using Models.Web;
using System;

namespace DatabaseContext
{
	public class DepartmentDatabaseContext : DbContext
	{
		public DepartmentDatabaseContext(DbContextOptions<DepartmentDatabaseContext> options) : base(options) { }

		public DepartmentDatabaseContext() : base() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				// TODO вынести в config
#if RELEASE
				optionsBuilder.UseSqlServer(@"Data Source=10.3.1.13\SQLEXPRESS;Initial Catalog=DepartmentDatabaseContext;persist security info=True;user id=sa;password=isadmin;MultipleActiveResultSets=True;");
#endif

#if DEBUG
				optionsBuilder.UseSqlServer(@"Data Source=CHESHIR\SQLEXPRESS;Initial Catalog=DepartmentDatabaseContext;persist security info=True;user id=admin;password=cheshirSA123;MultipleActiveResultSets=True;");
#endif
			}
			base.OnConfiguring(optionsBuilder);
		}

		public virtual DbSet<DepartmentAccess> DepartmentAccesses { set; get; }
		public virtual DbSet<DepartmentRole> DepartmentRoles { set; get; }
		public virtual DbSet<DepartmentUser> DepartmentUsers { set; get; }
		public virtual DbSet<DepartmentUserRole> DepartmentUserRoles { set; get; }

		#region Base
		public virtual DbSet<Classroom> Classrooms { set; get; }
		public virtual DbSet<Discipline> Disciplines { set; get; }
		public virtual DbSet<DisciplineBlock> DisciplineBlocks { get; set; }
		public virtual DbSet<EducationDirection> EducationDirections { set; get; }
		public virtual DbSet<Lecturer> Lecturers { set; get; }
		public virtual DbSet<LecturerStudyPost> LecturerStudyPosts { get; set; }
		public virtual DbSet<LecturerDepartmentPost> LecturerDepartmentPosts { get; set; }
		public virtual DbSet<Student> Students { set; get; }
		public virtual DbSet<StudentGroup> StudentGroups { set; get; }
		public virtual DbSet<StudentOrder> StudentOrders { set; get; }
		public virtual DbSet<StudentOrderBlock> StudentOrderBlocks { set; get; }
		public virtual DbSet<StudentOrderBlockStudent> StudentOrderBlockStudents { set; get; }
		#endregion

		#region AcademicYears
		public virtual DbSet<AcademicPlan> AcademicPlans { get; set; }
		public virtual DbSet<AcademicPlanRecord> AcademicPlanRecords { get; set; }
		public virtual DbSet<AcademicPlanRecordElement> AcademicPlanRecordElements { get; set; }
		public virtual DbSet<AcademicPlanRecordMission> AcademicPlanRecordMissions { get; set; }
		public virtual DbSet<AcademicYear> AcademicYears { get; set; }
		public virtual DbSet<Contingent> Contingents { get; set; }
		public virtual DbSet<DisciplineTimeDistribution> DisciplineTimeDistributions { get; set; }
		public virtual DbSet<DisciplineTimeDistributionClassroom> DisciplineTimeDistributionClassrooms { get; set; }
		public virtual DbSet<DisciplineTimeDistributionRecord> DisciplineTimeDistributionRecords { get; set; }
		public virtual DbSet<LecturerWorkload> LecturerWorkload { get; set; }
		public virtual DbSet<SeasonDates> SeasonDates { set; get; }
		public virtual DbSet<StreamLesson> StreamLessons { set; get; }
		public virtual DbSet<StreamLessonRecord> StreamLessonRecords { set; get; }
		public virtual DbSet<TimeNorm> TimeNorms { get; set; }
		#endregion

		#region Examination
		public virtual DbSet<Statement> Statements { set; get; }
		public virtual DbSet<StatementRecord> StatementRecords { set; get; }
		public virtual DbSet<ExaminationList> ExaminationLists { set; get; }

		public virtual DbSet<ExaminationTemplate> ExaminationTemplates { get; set; }
		public virtual DbSet<ExaminationTemplateBlock> ExaminationTemplateBlocks { get; set; }
		public virtual DbSet<ExaminationTemplateBlockQuestion> ExaminationTemplateBlockQuestions { get; set; }
		public virtual DbSet<ExaminationTemplateTicket> ExaminationTemplateTickets { get; set; }
		public virtual DbSet<ExaminationTemplateTicketQuestion> ExaminationTemplateTicketQuestions { get; set; }

		public virtual DbSet<TicketTemplate> TicketTemplates { get; set; }
		public virtual DbSet<TicketTemplateBody> TicketTemplateBodies { get; set; }
		public virtual DbSet<TicketTemplateBodyProperties> TicketTemplateBodyProperties { get; set; }

		public virtual DbSet<TicketTemplateFontTable> TicketTemplateFontTables { get; set; }
		public virtual DbSet<TicketTemplateNumbering> TicketTemplateNumberings { get; set; }
		public virtual DbSet<TicketTemplateDocumentSetting> TicketTemplateDocumentSettings { get; set; }
		public virtual DbSet<TicketTemplateStyleDefinition> TicketTemplateStyleDefinitions { get; set; }
		public virtual DbSet<TicketTemplateWebSetting> TicketTemplateWebSettings { get; set; }
		public virtual DbSet<TicketTemplateThemePart> TicketTemplateThemeParts { get; set; }

		public virtual DbSet<TicketTemplateTable> TicketTemplateTables { get; set; }
		public virtual DbSet<TicketTemplateTableProperties> TicketTemplateTableProperties { get; set; }
		public virtual DbSet<TicketTemplateTableGridColumn> TicketTemplateTableGridColumn { get; set; }
		public virtual DbSet<TicketTemplateTableRow> TicketTemplateTableRows { get; set; }
		public virtual DbSet<TicketTemplateTableRowProperties> TicketTemplateTableRowProperties { get; set; }
		public virtual DbSet<TicketTemplateTableCell> TicketTemplateTableCells { get; set; }
		public virtual DbSet<TicketTemplateTableCellProperties> TicketTemplateTableCellProperties { get; set; }
		public virtual DbSet<TicketTemplateParagraph> TicketTemplateParagraphs { get; set; }
		public virtual DbSet<TicketTemplateParagraphProperties> TicketTemplateParagraphProperties { get; set; }
		public virtual DbSet<TicketTemplateParagraphRun> TicketTemplateParagraphRuns { get; set; }
		public virtual DbSet<TicketTemplateParagraphRunProperties> TicketTemplateParagraphRunProperties { get; set; }
		#endregion

		#region LaboratoryHead
		public virtual DbSet<MaterialTechnicalValue> MaterialTechnicalValues { set; get; }
		public virtual DbSet<MaterialTechnicalValueGroup> MaterialTechnicalValueGroups { set; get; }
		public virtual DbSet<MaterialTechnicalValueRecord> MaterialTechnicalValueRecords { set; get; }
		public virtual DbSet<SoftwareRecord> SoftwareRecords { get; set; }
		public virtual DbSet<Software> Softwares { get; set; }
		#endregion

		#region LearningProgress
		public virtual DbSet<DisciplineLesson> DisciplineLessons { get; set; }
		public virtual DbSet<DisciplineLessonTask> DisciplineLessonTasks { get; set; }
		public virtual DbSet<DisciplineLessonTaskVariant> DisciplineLessonTaskVariants { get; set; }
		public virtual DbSet<DisciplineLessonTaskStudentAccept> DisciplineLessonTaskStudentAccepts { get; set; }
		public virtual DbSet<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
		public virtual DbSet<DisciplineLessonConducted> DisciplineLessonConducteds { get; set; }
		public virtual DbSet<DisciplineLessonConductedStudent> DisciplineLessonConductedStudents { get; set; }
		#endregion

		#region Lecturer
		public virtual DbSet<IndividualPlan> IndividualPlans { get; set; }
		public virtual DbSet<IndividualPlanKindOfWork> IndividualPlanKindOfWorks { get; set; }
		public virtual DbSet<IndividualPlanRecord> IndividualPlanRecords { get; set; }
		public virtual DbSet<IndividualPlanTitle> IndividualPlanTitles { get; set; }
		public virtual DbSet<IndividualPlanNIRScientificArticle> IndividualPlanNIRScientificArticles { get; set; }
		public virtual DbSet<IndividualPlanNIRContractualWork> IndividualPlanNIRContractualWorks { get; set; }
		#endregion

		#region Schedule
		public virtual DbSet<ConsultationRecord> ConsultationRecords { set; get; }
		public virtual DbSet<ExaminationRecord> ExaminationRecords { set; get; }
		public virtual DbSet<OffsetRecord> OffsetRecords { set; get; }
		public virtual DbSet<SemesterRecord> SemesterRecords { set; get; }
		#endregion

		#region Web
		public virtual DbSet<News> Newses { set; get; }
		public virtual DbSet<Comment> Comments { set; get; }
		#endregion

		public virtual DbSet<CurrentSettings> CurrentSettings { set; get; }

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
			catch (Exception)
			{
				try
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
				}
				catch { }
				throw;
			}
		}
	}
}