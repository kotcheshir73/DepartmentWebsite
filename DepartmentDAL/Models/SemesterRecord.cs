using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий занятие в семестре
    /// </summary>
    public class SemesterRecord
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonTeacher { get; set; }

        public long StudentGroupId { get; set; }

        public string ClassroomId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Classroom Classroom { get; set; }
    }
}
