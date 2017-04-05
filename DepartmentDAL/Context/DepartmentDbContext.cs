using DepartmentDAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DepartmentDAL.Context
{
    [Table("DepartmentDatabase")]
    public class DepartmentDbContext : DbContext
    {
        public DepartmentDbContext()
        {//настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new CreateDatabaseIfNotExists<DepartmentDbContext>());
        }

		public virtual DbSet<AcademicPlan> AcademicPlans { get; set; }
		public virtual DbSet<AcademicPlanRecord> AcademicPlanRecords { get; set; }
		public virtual DbSet<AcademicYear> AcademicYears { get; set; }
		public virtual DbSet<Access> Access { set; get; }
        public virtual DbSet<Classroom> Classrooms { set; get; }
        public virtual DbSet<ConsultationRecord> ConsultationRecords { set; get; }
		public virtual DbSet<Contingent> Contingents { get; set; }
		public virtual DbSet<CurrentSettings> CurrentSettings { set; get; }
		public virtual DbSet<Discipline> Disciplines { set; get; }
		public virtual DbSet<EducationDirection> EducationDirections { set; get; }
        public virtual DbSet<ExaminationRecord> ExaminationRecords { set; get; }
		public virtual DbSet<KindOfLoad> KindOfLoads { get; set; }
		public virtual DbSet<Lecturer> Lecturers { set; get; }
		public virtual DbSet<LoadDistribution> LoadDistributions { get; set; }
		public virtual DbSet<LoadDistributionMission> LoadDistributionMissions { get; set; }
		public virtual DbSet<LoadDistributionRecord> LoadDistributionRecords { get; set; }
		public virtual DbSet<Message> Messages { set; get; }
        public virtual DbSet<OffsetRecord> OffsetRecords { set; get; }
        public virtual DbSet<SeasonDates> SeasonDates { set; get; }
        public virtual DbSet<SemesterRecord> SemesterRecords { set; get; }
        public virtual DbSet<StreamingLesson> StreamingLessons { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<ScheduleLessonTime> ScheduleLessonTimes { set; get; }
        public virtual DbSet<ScheduleStopWord> ScheduleStopWords { get; set; }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<StudentGroup> StudentGroups { set; get; }
        public virtual DbSet<StudentHistory> StudentHistorys { set; get; }
		public virtual DbSet<TimeNorm> TimeNorms { get; set; }
		public virtual DbSet<User> Users { set; get; }
    }
}
