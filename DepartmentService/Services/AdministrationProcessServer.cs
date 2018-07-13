using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentService.Context;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            try
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
                SaveToFile<DisciplineLessonTaskStudentRecord>(folderName);
                SaveToFile<DisciplineStudentRecord>(folderName);
                SaveToFile<EducationDirection>(folderName);
                SaveToFile<ExaminationRecord>(folderName);
                SaveToFile<KindOfLoad>(folderName);
                SaveToFile<LecturerPost>(folderName);
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
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        public ResultService ExportDataFromJson(string folderName)
        {
            // Отключаем отслеживание и проверку изменений
            _context.Configuration.AutoDetectChangesEnabled = false;
            _context.Configuration.ValidateOnSaveEnabled = false;

            LoadFromFile<CurrentSettings>(folderName);
            LoadFromFile<ScheduleLessonTime>(folderName);

            LoadFromFile<EducationDirection>(folderName);
            LoadFromFile<AcademicYear>(folderName);
            LoadFromFile<SeasonDates>(folderName);
            LoadFromFile<AcademicPlan>(folderName);

            LoadFromFile<Classroom>(folderName);
            LoadFromFile<LecturerPost>(folderName);
            LoadFromFile<Lecturer>(folderName);
            LoadFromFile<StudentGroup>(folderName);
            LoadFromFile<StreamingLesson>(folderName);

            LoadFromFile<DisciplineBlock>(folderName);
            LoadFromFile<Discipline>(folderName);

            LoadFromFile<KindOfLoad>(folderName);
            LoadFromFile<TimeNorm>(folderName);
            LoadFromFile<Contingent>(folderName);

            LoadFromFile<Role>(folderName);
            LoadFromFile<Access>(folderName);

            LoadFromFile<Student>(folderName);
            LoadFromFile<StudentHistory>(folderName);

            LoadFromFile<AcademicPlanRecord>(folderName);

            LoadFromFile<ConsultationRecord>(folderName);
            LoadFromFile<ExaminationRecord>(folderName);
            LoadFromFile<OffsetRecord>(folderName);
            LoadFromFile<SemesterRecord>(folderName);

            LoadFromFile<DisciplineLesson>(folderName);
            LoadFromFile<DisciplineLessonStudentRecord>(folderName);
            LoadFromFile<DisciplineLessonTask>(folderName);
            LoadFromFile<DisciplineLessonTaskStudentRecord>(folderName);
            LoadFromFile<DisciplineStudentRecord>(folderName);

            LoadFromFile<LoadDistribution>(folderName);
            LoadFromFile<LoadDistributionMission>(folderName);
            LoadFromFile<LoadDistributionRecord>(folderName);

            LoadFromFile<User>(folderName);
            LoadFromFile<UserRole>(folderName);

            LoadFromFile<Message>(folderName);


            _context.Configuration.AutoDetectChangesEnabled = true;
            _context.Configuration.ValidateOnSaveEnabled = true;

            return ResultService.Success();
        }

        public ResultService CreateBackUp(string fileName)
        {
            try
            {
                _context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("BACKUP DATABASE [DepartmentDatabase] to DISK=N'{0}'", fileName));
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            return ResultService.Success();
        }

        public ResultService RestoreBackUp(string fileName)
        {
            try
            {
               // _context.Database.Delete();
               // var masterContext = new MasterDbContext();
               // masterContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("RESTORE DATABASE [DepartmentDatabase] FROM DISK='{0}'", fileName));
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
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
                _context.Configuration.AutoDetectChangesEnabled = false;
                _context.Configuration.ValidateOnSaveEnabled = false;
                var propInfo = obj.GetType().GetProperty("Id");
                if (propInfo != null && propInfo.PropertyType.Name != "String")
                {
                    var name = obj.GetType().Name;
                    if (name.EndsWith("s"))
                    {
                        if (name == "Access")
                        {
                            name = "Accesses";
                        }
                        name = name.Remove(name.Length - 1);
                    }
                }

                List<T> records = (List<T>)jsonFormatter.ReadObject(fs);
                _context.Set<T>().AddRange(records);
                _context.SaveChanges();

                if (propInfo != null && propInfo.PropertyType.Name != "String")
                {
                    var name = obj.GetType().Name;
                    if (name.EndsWith("s"))
                    {
                        if (name == "Access")
                        {
                            name = "Accesses";
                        }
                        name = name.Remove(name.Length - 1);
                    }
                }

                _context.Configuration.AutoDetectChangesEnabled = true;
                _context.Configuration.ValidateOnSaveEnabled = true;
            }
        }
    }
}
