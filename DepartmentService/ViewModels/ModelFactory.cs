using DepartmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public static IEnumerable<EducationDirectionViewModel> CreateEducationDirections(IEnumerable<EducationDirection> entities)
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

        public static IEnumerable<StudentGroupViewModel> CreateStudentGroups(IEnumerable<StudentGroup> entities)
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

        public static IEnumerable<ClassroomViewModel> CreateClassrooms(IEnumerable<Classroom> entities)
        {
            return entities.Select(e => CreateClassroomViewModel(e)).OrderBy(e => e.Id);
        }

        public static StudentViewModel CreateStudentViewModel(Student entity)
        {
            return new StudentViewModel
            {
                NumberOfBook = entity.NumberOfBook,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Description = entity.Description,
                Photo = Image.FromStream(new MemoryStream(entity.Photo))
            };
        }

        public static IEnumerable<StudentViewModel> CreateStudents(IEnumerable<Student> entities)
        {
            return entities.Select(e => CreateStudentViewModel(e)).OrderBy(e => e.NumberOfBook);
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

        public static IEnumerable<SeasonDatesViewModel> CreateSeasonDaties(IEnumerable<SeasonDates> entities)
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

        public static IEnumerable<SemesterRecordViewModel> CreateSemesterRecords(IEnumerable<SemesterRecord> entities)
        {
            return entities.Select(e => CreateSemesterRecordViewModel(e)).OrderBy(e => e.Id);
        }

        public static OffsetRecordViewModel CreateOffsetRecordViewModel(OffsetRecord entity)
        {
            return new OffsetRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static IEnumerable<OffsetRecordViewModel> CreateOffsetRecords(IEnumerable<OffsetRecord> entities)
        {
            return entities.Select(e => CreateOffsetRecordViewModel(e)).OrderBy(e => e.Id);
        }
        
        public static ExaminationRecordViewModel CreateExaminationRecordViewModel(ExaminationRecord entity)
        {
            return new ExaminationRecordViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                DateExamination = entity.DateExamination,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static IEnumerable<ExaminationRecordViewModel> CreateExaminationRecords(IEnumerable<ExaminationRecord> entities)
        {
            return entities.Select(e => CreateExaminationRecordViewModel(e)).OrderBy(e => e.Id);
        }

        public static ConsultationRecordViewModel CreateConsultationRecordViewModel(ConsultationRecord entity)
        {
            return new ConsultationRecordViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static IEnumerable<ConsultationRecordViewModel> CreateConsultationRecords(IEnumerable<ConsultationRecord> entities)
        {
            return entities.Select(e => CreateConsultationRecordViewModel(e)).OrderBy(e => e.Id);
        }


        public static StreamingLessonViewModel CreateStreamingLessonViewModel(StreamingLesson entity)
        {
            return new StreamingLessonViewModel
            {
                Id = entity.Id,
                IncomingGroups = entity.IncomingGroups,
                StreamName = entity.StreamName
            };
        }

        public static IEnumerable<StreamingLessonViewModel> CreateStreamingLessons(IEnumerable<StreamingLesson> entities)
        {
            return entities.Select(e => CreateStreamingLessonViewModel(e)).OrderBy(e => e.Id);
        }

        public static ScheduleLessonTimeViewModel CreateScheduleLessonTimeViewModel(ScheduleLessonTime entity)
        {
            string text = string.Format("{0}{1}{2} - {3}", entity.Title, Environment.NewLine, entity.DateBeginLesson.ToShortTimeString(),
                entity.DateEndLesson.ToShortTimeString());
            return new ScheduleLessonTimeViewModel
            {
                Id = entity.Id,
                Text = text,
                Title = entity.Title,
                TimeBeginLesson = entity.DateBeginLesson.ToShortTimeString(),
                TimeEndLesson = entity.DateEndLesson.ToShortTimeString(),
                DateBeginLesson = entity.DateBeginLesson,
                DateEndLesson = entity.DateEndLesson
            };
        }

        public static IEnumerable<ScheduleLessonTimeViewModel> CreateScheduleLessonTimes(IEnumerable<ScheduleLessonTime> entities)
        {
            return entities.Select(e => CreateScheduleLessonTimeViewModel(e)).OrderBy(e => e.Id);
        }

        public static ScheduleStopWordViewModel CreateScheduleStopWordViewModel(ScheduleStopWord entity)
        {
            return new ScheduleStopWordViewModel
            {
                Id = entity.Id,
                StopWord = entity.StopWord,
                StopWordType = entity.StopWordType.ToString()
            };
        }

        public static IEnumerable<ScheduleStopWordViewModel> CreateScheduleStopWords(IEnumerable<ScheduleStopWord> entities)
        {
            return entities.Select(e => CreateScheduleStopWordViewModel(e)).OrderBy(e => e.StopWordType);
        }
    }
}
