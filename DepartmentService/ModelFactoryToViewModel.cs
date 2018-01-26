using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.Helpers;
using DepartmentService.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DepartmentService.ViewModels
{
    public static class ModelFactoryToViewModel
    {
        #region EducationDirection
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
            return entities.Select(e => CreateEducationDirectionViewModel(e));
        }

        public static DisciplineBlockViewModel CreateDisciplineBlockViewModel(DisciplineBlock entity)
        {
            return new DisciplineBlockViewModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static IEnumerable<DisciplineBlockViewModel> CreateDisciplineBlocks(IEnumerable<DisciplineBlock> entities)
        {
            return entities.Select(e => CreateDisciplineBlockViewModel(e));
        }

        public static LecturerPostViewModel CreateLecturerPostViewModel(LecturerPost entity)
        {
            return new LecturerPostViewModel
            {
                Id = entity.Id,
                PostTitle = entity.PostTitle,
                Hours = entity.Hours
            };
        }

        public static IEnumerable<LecturerPostViewModel> CreateLecturerPosts(IEnumerable<LecturerPost> entities)
        {
            return entities.Select(e => CreateLecturerPostViewModel(e));
        }


        public static ClassroomViewModel CreateClassroomViewModel(Classroom entity)
        {
            return new ClassroomViewModel
            {
                Id = entity.Id,
                Number = entity.Number,
                ClassroomType = entity.ClassroomType.ToString(),
                Capacity = entity.Capacity,
                NotUseInSchedule = entity.NotUseInSchedule
            };
        }

        public static IEnumerable<ClassroomViewModel> CreateClassrooms(IEnumerable<Classroom> entities)
        {
            return entities.Select(e => CreateClassroomViewModel(e));
        }

        public static DisciplineViewModel CreateDisciplineViewModel(Discipline entity)
        {
            return new DisciplineViewModel
            {
                Id = entity.Id,
                DisciplineBlockId = entity.DisciplineBlockId,
                DisciplineName = entity.DisciplineName,
                DisciplineShortName = entity.DisciplineShortName,
                DisciplineBlockTitle = entity.DisciplineBlock.Title
            };
        }

        public static IEnumerable<DisciplineViewModel> CreateDisciplines(IEnumerable<Discipline> entities)
        {
            return entities.Select(e => CreateDisciplineViewModel(e));
        }

        public static LecturerViewModel CreateLecturerViewModel(Lecturer entity)
        {
            return new LecturerViewModel
            {
                Id = entity.Id,
                LecturerPostId = entity.LecturerPostId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Abbreviation = entity.Abbreviation,
                DateBirth = entity.DateBirth,
                Post = entity.Post.ToString(),
                LecturerPost = entity.LecturerPost.PostTitle,
                Rank = entity.Rank.ToString(),
                Rank2 = entity.Rank2.ToString(),
                HomeNumber = entity.HomeNumber,
                MobileNumber = entity.MobileNumber,
                Email = entity.Email,
                Address = entity.Address,
                Description = entity.Description,
                Photo = entity.Photo != null && entity.Photo.Length > 0 ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
            };
        }

        public static IEnumerable<LecturerViewModel> CreateLecturers(IEnumerable<Lecturer> entities)
        {
            return entities.Select(e => CreateLecturerViewModel(e));
        }

        public static StudentGroupViewModel CreateStudentGroupViewModel(StudentGroup entity)
        {
            return new StudentGroupViewModel
            {
                Id = entity.Id,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionCipher = entity.EducationDirection.Cipher,
                GroupName = entity.GroupName,
                Course = (int)entity.Course,
                CountStudents = (entity.Students != null) ? entity.Students.Where(s => !s.IsDeleted).Count() : 0,
                StewardName = entity.StewardName,
                Curator = entity.CuratorId.HasValue ? entity.Curator.ToString() : "",
                CuratorId = entity.CuratorId
            };
        }

        public static IEnumerable<StudentGroupViewModel> CreateStudentGroups(IEnumerable<StudentGroup> entities)
        {
            return entities.Select(e => CreateStudentGroupViewModel(e));
        }


        public static KindOfLoadViewModel CreateKindOfLoadViewModel(KindOfLoad entity)
        {
            return new KindOfLoadViewModel
            {
                Id = entity.Id,
                KindOfLoadName = entity.KindOfLoadName,
                KindOfLoadType = entity.KindOfLoadType.ToString()
            };
        }

        public static IEnumerable<KindOfLoadViewModel> CreateKindOfLoads(IEnumerable<KindOfLoad> entities)
        {
            return entities.Select(e => CreateKindOfLoadViewModel(e));
        }

        public static TimeNormViewModel CreateTimeNormViewModel(TimeNorm entity)
        {
            return new TimeNormViewModel
            {
                Id = entity.Id,
                KindOfLoadId = entity.KindOfLoadId,
                Title = entity.Title,
                KindOfLoadName = entity.KindOfLoad.KindOfLoadName,
                Formula = entity.Formula,
                Hours = entity.Hours
            };
        }

        public static IEnumerable<TimeNormViewModel> CreateTimeNorms(IEnumerable<TimeNorm> entities)
        {
            return entities.Select(e => CreateTimeNormViewModel(e));
        }

        public static ContingentViewModel CreateContingentViewModel(Contingent entity)
        {
            return new ContingentViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionCipher = entity.EducationDirection.Cipher,
                AcademicYear = entity.AcademicYear.Title,
                Course = (int)entity.Course,
                CountStudents = entity.CountStudetns,
                CountSubgroups = entity.CountSubgroups
            };
        }

        public static IEnumerable<ContingentViewModel> CreateContingents(IEnumerable<Contingent> entities)
        {
            return entities.Select(e => CreateContingentViewModel(e));
        }


        public static AcademicPlanViewModel CreateAcademicPlanViewModel(AcademicPlan entity)
        {
            string courses = "";
            if ((entity.AcademicCourses & AcademicCourse.Course_1) == AcademicCourse.Course_1)
            {
                courses += "1";
            }
            if ((entity.AcademicCourses & AcademicCourse.Course_2) == AcademicCourse.Course_2)
            {
                courses += (courses == "" ? "" : ", ") + "2";
            }
            if ((entity.AcademicCourses & AcademicCourse.Course_3) == AcademicCourse.Course_3)
            {
                courses += (courses == "" ? "" : ", ") + "3";
            }
            if ((entity.AcademicCourses & AcademicCourse.Course_4) == AcademicCourse.Course_4)
            {
                courses += (courses == "" ? "" : ", ") + "4";
            }
            if ((entity.AcademicCourses & AcademicCourse.Course_5) == AcademicCourse.Course_5)
            {
                courses += (courses == "" ? "" : ", ") + "5";
            }
            if ((entity.AcademicCourses & AcademicCourse.Course_6) == AcademicCourse.Course_6)
            {
                courses += (courses == "" ? "" : ", ") + "6";
            }
            return new AcademicPlanViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirection = entity.EducationDirection.Cipher,
                AcademicYear = entity.AcademicYear.Title,
                AcademicLevel = entity.AcademicLevel.ToString(),
                AcademicCoursesStrings = courses,
                AcademicCourses = (int)entity.AcademicCourses
            };
        }

        public static IEnumerable<AcademicPlanViewModel> CreateAcademicPlans(IEnumerable<AcademicPlan> entities)
        {
            return entities.Select(e => CreateAcademicPlanViewModel(e));
        }

        public static AcademicPlanRecordViewModel CreateAcademicPlanRecordViewModel(AcademicPlanRecord entity)
        {
            return new AcademicPlanRecordViewModel
            {
                Id = entity.Id,
                AcademicPlanId = entity.AcademicPlanId,
                DisciplineId = entity.DisciplineId,
                Disciplne = entity.Discipline.DisciplineName,
                KindOfLoadId = entity.KindOfLoadId,
                KindOfLoad = entity.KindOfLoad.KindOfLoadName,
                Semester = entity.Semester.ToString(),
                Hours = entity.Hours
            };
        }

        public static IEnumerable<AcademicPlanRecordViewModel> CreateAcademicPlanRecords(IEnumerable<AcademicPlanRecord> entities)
        {
            return entities.Select(e => CreateAcademicPlanRecordViewModel(e));
        }

        public static AcademicYearViewModel CreateAcademicYearViewModel(AcademicYear entity)
        {
            return new AcademicYearViewModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static IEnumerable<AcademicYearViewModel> CreateAcademicYears(IEnumerable<AcademicYear> entities)
        {
            return entities.Select(e => CreateAcademicYearViewModel(e));
        }


        public static StudentViewModel CreateStudentViewModel(Student entity)
        {
            return new StudentViewModel
            {
                Id = entity.Id,
                NumberOfBook = entity.NumberOfBook,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Email = entity.Email,
                Description = entity.Description,
                Photo = entity.Photo != null ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = (entity.StudentGroup != null) ? entity.StudentGroup.GroupName : string.Empty,
                IsSteward = entity.IsSteward
            };
        }

        public static IEnumerable<StudentViewModel> CreateStudents(IEnumerable<Student> entities)
        {
            return entities.Select(e => CreateStudentViewModel(e));
        }

        public static StudentHistoryViewModel CreateStudentHistoryViewModel(StudentHistory entity)
        {
            return new StudentHistoryViewModel
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                DateCreate = entity.DateCreate.ToLongDateString(),
                TextMessage = entity.TextMessage
            };
        }

        public static IEnumerable<StudentHistoryViewModel> CreateStudentHistorys(IEnumerable<StudentHistory> entities)
        {
            return entities.Select(e => CreateStudentHistoryViewModel(e));
        }



        public static SeasonDatesViewModel CreateSeasonDatesViewModel(SeasonDates entity)
        {
            return new SeasonDatesViewModel
            {
                Id = entity.Id,
                DateBeginExamination = entity.DateBeginExamination.ToLongDateString(),
                DateBeginOffset = entity.DateBeginOffset.ToLongDateString(),
                DateBeginFirstHalfSemester = entity.DateBeginFirstHalfSemester.ToLongDateString(),
                DateBeginSecondHalfSemester = entity.DateBeginSecondHalfSemester.ToLongDateString(),
                DateEndExamination = entity.DateEndExamination.ToLongDateString(),
                DateEndOffset = entity.DateEndOffset.ToLongDateString(),
                DateEndFirstHalfSemester = entity.DateEndFirstHalfSemester.ToLongDateString(),
                DateEndSecondHalfSemester = entity.DateEndSecondHalfSemester.ToLongDateString(),
                DateBeginPractice = entity.DateBeginPractice.HasValue ? entity.DateBeginPractice.Value.ToLongDateString() : "",
                DateEndPractice = entity.DateEndPractice.HasValue ? entity.DateEndPractice.Value.ToLongDateString() : "",
                Title = entity.Title
            };
        }

        public static IEnumerable<SeasonDatesViewModel> CreateSeasonDaties(IEnumerable<SeasonDates> entities)
        {
            return entities.Select(e => CreateSeasonDatesViewModel(e));
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
            return entities.Select(e => CreateStreamingLessonViewModel(e));
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
                Order = entity.Order,
                TimeBeginLesson = entity.DateBeginLesson.ToShortTimeString(),
                TimeEndLesson = entity.DateEndLesson.ToShortTimeString(),
                DateBeginLesson = entity.DateBeginLesson,
                DateEndLesson = entity.DateEndLesson
            };
        }

        public static IEnumerable<ScheduleLessonTimeViewModel> CreateScheduleLessonTimes(IEnumerable<ScheduleLessonTime> entities)
        {
            return entities.Select(e => CreateScheduleLessonTimeViewModel(e));
        }
        #endregion

        #region LoadDistribution
        public static LoadDistributionViewModel CreateLoadDistributionViewModel(LoadDistribution entity)
        {
            return new LoadDistributionViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title
            };
        }

        public static IEnumerable<LoadDistributionViewModel> CreateLoadDistributions(IEnumerable<LoadDistribution> entities)
        {
            return entities.Select(e => CreateLoadDistributionViewModel(e));
        }

        public static LoadDistributionRecordViewModel CreateLoadDistributionRecordViewModel(LoadDistributionRecord entity)
        {
            return new LoadDistributionRecordViewModel
            {
                Id = entity.Id,
                LoadDistributionId = entity.LoadDistributionId,
                LoadDistributionAcademicYear = entity.LoadDistribution.AcademicYear.Title,
                AcademicPlanRecordId = entity.AcademicPlanRecordId,
                EducationDirectionCipher = entity.AcademicPlanRecord.AcademicPlan.EducationDirection.Cipher,
                Disciplne = entity.AcademicPlanRecord.Discipline.DisciplineName,
                DisciplineBlockTitle = entity.AcademicPlanRecord.Discipline.DisciplineBlock.Title,
                ContingentId = entity.ContingentId,
                TimeNormId = entity.TimeNormId,
                SemesterNumber = (int)entity.AcademicPlanRecord.Semester,
                Load = entity.Load
            };
        }

        public static IEnumerable<LoadDistributionRecordViewModel> CreateLoadDistributionRecords(IEnumerable<LoadDistributionRecord> entities)
        {
            return entities.Select(e => CreateLoadDistributionRecordViewModel(e));
        }

        public static LoadDistributionMissionViewModel CreateLoadDistributionMissionViewModel(LoadDistributionMission entity)
        {
            return new LoadDistributionMissionViewModel
            {
                Id = entity.Id,
                LoadDistributionRecordId = entity.LoadDistributionRecordId,
                LecturerId = entity.LecturerId,
                Hours = entity.Hours
            };
        }

        public static IEnumerable<LoadDistributionMissionViewModel> CreateLoadDistributionMissions(IEnumerable<LoadDistributionMission> entities)
        {
            return entities.Select(e => CreateLoadDistributionMissionViewModel(e));
        }
        #endregion


        public static AcademicPlanRecordForDiciplineViewModel CreateAcademicPlanRecordForDiciplineViewModel(AcademicPlanRecord entity)
        {
            return new AcademicPlanRecordForDiciplineViewModel
            {
                Id = entity.Id,
                AcademicPlanId = entity.AcademicPlanId,
                EducationDirectionCipher = entity.AcademicPlan.EducationDirection.Cipher,
                DisciplineId = entity.DisciplineId,
                Disciplne = entity.Discipline.DisciplineName,
                KindOfLoadId = entity.KindOfLoadId,
                KindOfLoad = entity.KindOfLoad.KindOfLoadName,
                Semester = entity.Semester.ToString(),
                Hours = entity.Hours
            };
        }

        public static IEnumerable<AcademicPlanRecordForDiciplineViewModel> CreateAcademicPlanRecordForDiciplines(IEnumerable<AcademicPlanRecord> entities)
        {
            return entities.Select(e => CreateAcademicPlanRecordForDiciplineViewModel(e));
        }

        #region Schedule
        public static ScheduleRecordViewModels CreateScheduleRecordViewModel(ScheduleRecord entity)
        {
            return new ScheduleRecordViewModels
            {
                Id = entity.Id,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static SemesterRecordViewModel CreateSemesterRecordViewModel(SemesterRecord entity)
        {
            return new SemesterRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                NotParseRecord = entity.NotParseRecord,
                IsStreaming = entity.IsStreaming,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                LessonType = entity.LessonType.ToString(),
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static IEnumerable<SemesterRecordViewModel> CreateSemesterRecords(IEnumerable<SemesterRecord> entities)
        {
            return entities.Select(e => CreateSemesterRecordViewModel(e));
        }

        public static SemesterRecordShortViewModel CreateSemesterRecordShortViewModel(SemesterRecord entity, string groups)
        {
            return new SemesterRecordShortViewModel
            {
                Id = entity.Id,
                Week = entity.Week,
                Day = entity.Day,
                Lesson = entity.Lesson,
                LessonType = entity.LessonType.ToString(),
                IsStreaming = entity.IsStreaming,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = groups,
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static OffsetRecordViewModel CreateOffsetRecordViewModel(OffsetRecord entity)
        {
            return new OffsetRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static OffsetRecordShortViewModel CreateOffsetRecordShortViewModel(OffsetRecord entity)
        {
            return new OffsetRecordShortViewModel
            {
                Id = entity.Id,
                Week = entity.Week,
                Day = entity.Day,
                Lesson = entity.Lesson,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static IEnumerable<OffsetRecordViewModel> CreateOffsetRecords(IEnumerable<OffsetRecord> entities)
        {
            return entities.Select(e => CreateOffsetRecordViewModel(e));
        }

        public static ExaminationRecordViewModel CreateExaminationRecordViewModel(ExaminationRecord entity)
        {
            return new ExaminationRecordViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                DateExamination = entity.DateExamination,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonConsultationClassroom = entity.LessonConsultationClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                ConsultationClassroomId = entity.ConsultationClassroomId,
                ConsultationClassroom = entity.ConsultationClassroomId != null ? entity.ConsultationClassroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static ExaminationRecordShortViewModel CreateExaminationRecordShortViewModel(ExaminationRecord entity)
        {
            return new ExaminationRecordShortViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                DateExamination = entity.DateExamination,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity),
                LessonConsultationClassroom = ScheduleHelper.GetLessonConsultationClassroom(entity)
            };
        }

        public static IEnumerable<ExaminationRecordViewModel> CreateExaminationRecords(IEnumerable<ExaminationRecord> entities)
        {
            return entities.Select(e => CreateExaminationRecordViewModel(e));
        }

        public static ConsultationRecordViewModel CreateConsultationRecordViewModel(ConsultationRecord entity)
        {
            return new ConsultationRecordViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static ConsultationRecordShortViewModel CreateConsultationRecordShortViewModel(ConsultationRecord entity, ConsultationRecordRecordBindingModel model)
        {
            return new ConsultationRecordShortViewModel
            {
                Id = entity.Id,
                Week = model.Week.Value,
                Day = model.Day.Value,
                Lesson = model.Lesson.Value,
                DateConsultation = entity.DateConsultation,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static IEnumerable<ConsultationRecordViewModel> CreateConsultationRecords(IEnumerable<ConsultationRecord> entities)
        {
            return entities.Select(e => CreateConsultationRecordViewModel(e));
        }
        #endregion

        #region Administration
        public static RoleViewModel CreateRoleViewModel(Role entity)
        {
            return new RoleViewModel
            {
                Id = entity.Id,
                RoleName = entity.RoleName
            };
        }

        public static IEnumerable<RoleViewModel> CreateRoles(IEnumerable<Role> entities)
        {
            return entities.Select(e => CreateRoleViewModel(e));
        }

        public static AccessViewModel CreateAccessViewModel(Access entity)
        {
            return new AccessViewModel
            {
                Id = entity.Id,
                RoleName = entity.Role.RoleName,
                Operation = entity.Operation.ToString(),
                AccessType = entity.AccessType.ToString()
            };
        }

        public static IEnumerable<AccessViewModel> CreateAccesses(IEnumerable<Access> entities)
        {
            return entities.Select(e => CreateAccessViewModel(e));
        }

        public static UserViewModel CreateUserViewModel(User entity)
        {
            return new UserViewModel
            {
                Id = entity.Id,
                Login = entity.Login,
                StudentId = entity.StudentId,
                LecturerId = entity.LecturerId,
                Avatar = entity.Avatar != null && entity.Avatar.Length > 0 ? Image.FromStream(new MemoryStream(entity.Avatar)) : null,
                RoleType = entity.RoleType.ToString(),
                IsBanned = entity.IsBanned,
                DateBanned = entity.DateBanned,
                DateLastVisit = entity.DateLastVisit
            };
        }

        public static IEnumerable<UserViewModel> CreateUsers(IEnumerable<User> entities)
        {
            return entities.Select(e => CreateUserViewModel(e));
        }
        #endregion
    }
}
