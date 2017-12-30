using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using DepartmentService.IServices;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DepartmentService.Services
{
    public class AdministrationProcessServer : IAdministrationProcessServer
    {
        private readonly DepartmentDbContext _context;

        public AdministrationProcessServer(DepartmentDbContext context)
        {
            _context = context;
        }

        public ResultService CheckAllUsersStatus()
        {


            return ResultService.Success();
        }

        public ResultService ImportDataToJson(string folderName)
        {
            // TODO
            SaveToFile<AcademicPlan>(folderName);
            SaveToFile<AcademicPlanRecord>(folderName);
            SaveToFile<AcademicYear>(folderName);
            SaveToFile<Access>(folderName);
            SaveToFile<Classroom>(folderName);
            SaveToFile<ConsultationRecord>(folderName);
            SaveToFile<Contingent>(folderName);
            SaveToFile<CurrentSettings>(folderName);
            SaveToFile<Discipline>(folderName);
            SaveToFile<DisciplineBlock>(folderName);
            SaveToFile<DisciplineLesson>(folderName);
            SaveToFile<DisciplineLessonStudentRecord>(folderName);
            SaveToFile<DisciplineLessonTask>(folderName);
            SaveToFile<DisciplineLessonTaskImageContext>(folderName);
            SaveToFile<DisciplineLessonTaskTextContext>(folderName);
            SaveToFile<DisciplineLessonTaskStudentRecord>(folderName);
            SaveToFile<DisciplineStudentRecord>(folderName);
            SaveToFile<EducationDirection>(folderName);
            SaveToFile<ExaminationRecord>(folderName);
            SaveToFile<KindOfLoad>(folderName);
            SaveToFile<Lecturer>(folderName);
            SaveToFile<LoadDistribution>(folderName);
            SaveToFile<LoadDistributionMission>(folderName);
            SaveToFile<LoadDistributionRecord>(folderName);
            SaveToFile<Message>(folderName);
            SaveToFile<OffsetRecord>(folderName);
            SaveToFile<SeasonDates>(folderName);
            SaveToFile<SemesterRecord>(folderName);
            SaveToFile<StreamingLesson>(folderName);
            SaveToFile<Role>(folderName);
            SaveToFile<ScheduleLessonTime>(folderName);
            SaveToFile<Student>(folderName);
            SaveToFile<StudentGroup>(folderName);
            SaveToFile<StudentHistory>(folderName);
            SaveToFile<TimeNorm>(folderName);
            SaveToFile<User>(folderName);
            SaveToFile<UserRole>(folderName);

            return ResultService.Success();
        }

        public ResultService ExportDataFromJson(string folderName)
        {
            // Отключаем отслеживание и проверку изменений
            _context.Configuration.AutoDetectChangesEnabled = false;
            _context.Configuration.ValidateOnSaveEnabled = false;

            LoadFromFile<AcademicPlan>(folderName);
            LoadFromFile<AcademicPlanRecord>(folderName);
            LoadFromFile<AcademicYear>(folderName);
            LoadFromFile<Access>(folderName);
            LoadFromFile<Classroom>(folderName);
            LoadFromFile<ConsultationRecord>(folderName);
            LoadFromFile<Contingent>(folderName);
            LoadFromFile<CurrentSettings>(folderName);
            LoadFromFile<Discipline>(folderName);
            LoadFromFile<DisciplineBlock>(folderName);
            LoadFromFile<DisciplineLesson>(folderName);
            LoadFromFile<DisciplineLessonStudentRecord>(folderName);
            LoadFromFile<DisciplineLessonTask>(folderName);
            LoadFromFile<DisciplineLessonTaskImageContext>(folderName);
            LoadFromFile<DisciplineLessonTaskTextContext>(folderName);
            LoadFromFile<DisciplineLessonTaskStudentRecord>(folderName);
            LoadFromFile<DisciplineStudentRecord>(folderName);
            LoadFromFile<EducationDirection>(folderName);
            LoadFromFile<ExaminationRecord>(folderName);
            LoadFromFile<KindOfLoad>(folderName);
            LoadFromFile<Lecturer>(folderName);
            LoadFromFile<LoadDistribution>(folderName);
            LoadFromFile<LoadDistributionMission>(folderName);
            LoadFromFile<LoadDistributionRecord>(folderName);
            LoadFromFile<Message>(folderName);
            LoadFromFile<OffsetRecord>(folderName);
            LoadFromFile<SeasonDates>(folderName);
            LoadFromFile<SemesterRecord>(folderName);
            LoadFromFile<StreamingLesson>(folderName);
            LoadFromFile<Role>(folderName);
            LoadFromFile<ScheduleLessonTime>(folderName);
            LoadFromFile<Student>(folderName);
            LoadFromFile<StudentGroup>(folderName);
            LoadFromFile<StudentHistory>(folderName);
            LoadFromFile<TimeNorm>(folderName);
            LoadFromFile<User>(folderName);
            LoadFromFile<UserRole>(folderName);


            _context.Configuration.AutoDetectChangesEnabled = true;
            _context.Configuration.ValidateOnSaveEnabled = true;

            return ResultService.Success();
        }

        private void SaveToFile<T>(string folderName) where T : class, new()
        {
            var records = _context.Set<T>();
            T obj = new T();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));

            using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, records);
            }
        }

        private void LoadFromFile<T>(string folderName) where T : class, new()
        {
            T obj = new T();
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.OpenOrCreate))
            {
                List<T> records = (List<T>)jsonFormatter.ReadObject(fs);
                _context.Set<T>().AddRange(records);
                _context.SaveChanges();
            }
        }
    }
}
