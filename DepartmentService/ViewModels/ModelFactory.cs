using DepartmentDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentService.ViewModels
{
    public static class ModelFactory
    {
        public static EducationDirectionViewModel CreateEducationDirectionViewModel(EducationDirection entity)
        {
            return new EducationDirectionViewModel
            {
                Id = entity.Id,
                Cipher = entity.Cipher,
                Description = entity.Description,
                Title = entity.Title
            };
        }

        public static IEnumerable<EducationDirectionViewModel> CreateEducationDirections(
            IEnumerable<EducationDirection> entities)
        {
            return entities.Select(e => CreateEducationDirectionViewModel(e)).OrderBy(e => e.Cipher);
        }

        public static StudentGroupViewModel CreateStudentGroupViewModel(StudentGroup entity)
        {
            return new StudentGroupViewModel
            {
                Id = entity.Id,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionCipher = entity.EducationDirection.Cipher,
                GroupName = entity.GroupName,
                Kurs = entity.Kurs,
                CountStudents = (entity.Students != null) ? entity.Students.Count : 0
            };
        }

        public static IEnumerable<StudentGroupViewModel> CreateStudentGroups(
            IEnumerable<StudentGroup> entities)
        {
            return entities.Select(e => CreateStudentGroupViewModel(e)).OrderBy(e => e.Kurs).ThenBy(e => e.EducationDirectionId);
        }

        public static ClassroomViewModel CreateClassroomViewModel(Classroom entity)
        {
            return new ClassroomViewModel
            {
                Id = entity.Id,
                ClassroomType = entity.ClassroomType.ToString(),
                Capacity = entity.Capacity
            };
        }

        public static IEnumerable<ClassroomViewModel> CreateClassrooms(
            IEnumerable<Classroom> entities)
        {
            return entities.Select(e => CreateClassroomViewModel(e)).OrderBy(e => e.Id);
        }
    }
}
