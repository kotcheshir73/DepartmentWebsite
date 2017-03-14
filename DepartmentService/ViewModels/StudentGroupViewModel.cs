namespace DepartmentService.ViewModels
{
    public class StudentGroupViewModel
    {
        public long Id { get; set; }

        public long EducationDirectionId { get; set; }

        public string EducationDirectionCipher { get; set; }
        
        public string GroupName { get; set; }

        public int Kurs { get; set; }

        public int CountStudents { get; set; }

        public int Capacity { get; set; }

        public int SubgroupsCount { get; set; }

        public string StewardId { get; set; }

        public long? CuratorId { get; set; }

        public string Steward { get; set; }

        public string Curator { get; set; }
    }
}
