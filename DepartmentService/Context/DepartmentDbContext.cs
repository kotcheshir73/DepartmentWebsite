using DepartmentModel.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DepartmentService.Context
{
    [Table("DepartmentDatabase")]
    public class DepartmentDbContext : DbContext
    {
        public DepartmentDbContext() : base("DepartmentDatabase")
        {//настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new DatabaseInitializer());
        }

        public virtual DbSet<AcademicPlan> AcademicPlans { get; set; }
        public virtual DbSet<AcademicPlanRecord> AcademicPlanRecords { get; set; }
        public virtual DbSet<AcademicPlanRecordElement> AcademicPlanRecordElements { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Access> Accesses { set; get; }
        public virtual DbSet<Classroom> Classrooms { set; get; }
        public virtual DbSet<ConsultationRecord> ConsultationRecords { set; get; }
        public virtual DbSet<Contingent> Contingents { get; set; }
        public virtual DbSet<CurrentSettings> CurrentSettings { set; get; }
        public virtual DbSet<Discipline> Disciplines { set; get; }
        public virtual DbSet<DisciplineBlock> DisciplineBlocks { get; set; }
        public virtual DbSet<DisciplineLesson> DisciplineLessons { get; set; }
        public virtual DbSet<DisciplineLessonStudentRecord> DisciplineLessonStudentRecords { get; set; }
        public virtual DbSet<DisciplineLessonTask> DisciplineLessonTasks { get; set; }
        public virtual DbSet<DisciplineLessonTaskStudentRecord> DisciplineLessonTaskStudentRecords { get; set; }
        public virtual DbSet<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
        public virtual DbSet<EducationDirection> EducationDirections { set; get; }
        public virtual DbSet<ExaminationRecord> ExaminationRecords { set; get; }
        public virtual DbSet<KindOfLoad> KindOfLoads { get; set; }
        public virtual DbSet<Lecturer> Lecturers { set; get; }
        public virtual DbSet<LecturerPost> LecturerPosts { get; set; }
        public virtual DbSet<LoadDistribution> LoadDistributions { get; set; }
        public virtual DbSet<LoadDistributionMission> LoadDistributionMissions { get; set; }
        public virtual DbSet<LoadDistributionRecord> LoadDistributionRecords { get; set; }
        public virtual DbSet<MaterialTechnicalValue> MaterialTechnicalValues { set; get; }
        public virtual DbSet<MaterialTechnicalValueGroup> MaterialTechnicalValueGroups { set; get; }
        public virtual DbSet<MaterialTechnicalValueRecord> MaterialTechnicalValueRecords { set; get; }
        public virtual DbSet<Message> Messages { set; get; }
        public virtual DbSet<OffsetRecord> OffsetRecords { set; get; }
        public virtual DbSet<SeasonDates> SeasonDates { set; get; }
        public virtual DbSet<SoftwareRecord> SoftwareRecords { get; set; }
        public virtual DbSet<SemesterRecord> SemesterRecords { set; get; }
        public virtual DbSet<StreamingLesson> StreamingLessons { set; get; }
        public virtual DbSet<StreamLesson> StreamLessons { set; get; }
        public virtual DbSet<StreamLessonRecord> StreamLessonRecords { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<ScheduleLessonTime> ScheduleLessonTimes { set; get; }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<StudentGroup> StudentGroups { set; get; }
        public virtual DbSet<StudentHistory> StudentHistorys { set; get; }
        public virtual DbSet<TimeNorm> TimeNorms { get; set; }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

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
