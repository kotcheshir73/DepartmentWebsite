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

        public static SeasonDatesViewModel CreateSeasonDatesViewModel(SeasonDates entity)
        {
            return new SeasonDatesViewModel
            {
                Id = entity.Id,
                DateBeginExamination = entity.DateBeginExamination.ToLongDateString(),
                DateBeginOffset = entity.DateBeginOffset.ToLongDateString(),
                DateBeginSemester = entity.DateBeginSemester.ToLongDateString(),
                DateEndExamination = entity.DateEndExamination.ToLongDateString(),
                DateEndOffset = entity.DateEndOffset.ToLongDateString(),
                DateEndSemester = entity.DateEndSemester.ToLongDateString(),
                DateBeginPractice = entity.DateBeginPractice.HasValue ? entity.DateBeginPractice.Value.ToLongDateString() : "",
                DateEndPractice = entity.DateEndPractice.HasValue ? entity.DateEndPractice.Value.ToLongDateString() : "",
                Title = entity.Title
            };
        }

        public static IEnumerable<SeasonDatesViewModel> CreateSeasonDaties(
            IEnumerable<SeasonDates> entities)
        {
            return entities.Select(e => CreateSeasonDatesViewModel(e)).OrderBy(e => e.Id);
        }

        public static SemesterRecordViewModel CreateSemesterRecordViewModel(SemesterRecord entity)
        {
            return new SemesterRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                IsStreaming = entity.IsStreaming,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                LessonType = entity.LessonType.ToString(),
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static IEnumerable<SemesterRecordViewModel> CreateSemesterRecords(
            IEnumerable<SemesterRecord> entities)
        {
            return entities.Select(e => CreateSemesterRecordViewModel(e)).OrderBy(e => e.Id);
        }
    }
}
