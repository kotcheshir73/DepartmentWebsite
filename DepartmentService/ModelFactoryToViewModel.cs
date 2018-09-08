using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentModel.Models.BaseEnities;
using DepartmentService.BindingModels;
using DepartmentService.Helpers;
using System;
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
                ShortName = entity.ShortName,
                Description = entity.Description,
                Title = entity.Title
            };
        }

        public static DisciplineBlockViewModel CreateDisciplineBlockViewModel(DisciplineBlock entity)
        {
            return new DisciplineBlockViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                DisciplineBlockBlueAsteriskName = entity.DisciplineBlockBlueAsteriskName,
                DisciplineBlockUseForGrouping = entity.DisciplineBlockUseForGrouping,
                DisciplineBlockOrder = entity.DisciplineBlockOrder
            };
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

        public static DisciplineViewModel CreateDisciplineViewModel(Discipline entity)
        {
            return new DisciplineViewModel
            {
                Id = entity.Id,
                DisciplineBlockId = entity.DisciplineBlockId,
                DisciplineName = entity.DisciplineName,
                DisciplineShortName = entity.DisciplineShortName,
                DisciplineBlockTitle = entity.DisciplineBlock.Title,
                DisciplineBlueAsteriskName = entity.DisciplineBlueAsteriskName
            };
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


        public static TimeNormViewModel CreateTimeNormViewModel(TimeNorm entity)
        {
            return new TimeNormViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                DisciplineBlockId = entity.DisciplineBlockId,
                AcademicYear = entity.AcademicYear.Title,
                DisciplineBlockName = entity.DisciplineBlock.Title,
                TimeNormName = entity.TimeNormName,
                TimeNormShortName = entity.TimeNormShortName,
                TimeNormOrder = entity.TimeNormOrder,
                TimeNormAcademicLevel = entity.TimeNormAcademicLevel.HasValue ? entity.TimeNormAcademicLevel.ToString() : null,
                KindOfLoadName = entity.KindOfLoadName,
                KindOfLoadAttributeName = entity.KindOfLoadAttributeName,
                KindOfLoadBlueAsteriskName = entity.KindOfLoadBlueAsteriskName,
                KindOfLoadBlueAsteriskAttributeName = entity.KindOfLoadBlueAsteriskAttributeName,
                KindOfLoadBlueAsteriskPracticName = entity.KindOfLoadBlueAsteriskPracticName,
                KindOfLoadType = entity.KindOfLoadType.ToString(),
                Hours = entity.Hours,
                NumKoef = entity.NumKoef,
                TimeNormKoef = entity.TimeNormKoef.ToString()
            };
        }

        public static ContingentViewModel CreateContingentViewModel(Contingent entity)
        {
            return new ContingentViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionShortName = entity.EducationDirection.ShortName,
                AcademicYear = entity.AcademicYear.Title,
                ContingentName = entity.ContingentName,
                Course = (int)entity.Course,
                CountGroups = entity.CountGroups,
                CountStudents = entity.CountStudetns,
                CountSubgroups = entity.CountSubgroups
            };
        }


        public static AcademicPlanViewModel CreateAcademicPlanViewModel(AcademicPlan entity)
        {
            string courses = string.Empty;
            if (entity.AcademicCourses.HasValue)
            {
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
            }
            return new AcademicPlanViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirection = entity.EducationDirectionId.HasValue ? string.Format("{0} {1}", entity.EducationDirection.Cipher, entity.EducationDirection.ShortName) : string.Empty,
                AcademicYear = entity.AcademicYear.Title,
                AcademicLevel = entity.AcademicLevel.ToString(),
                AcademicCoursesStrings = courses,
                AcademicCourses = entity.AcademicCourses.HasValue ? (int)entity.AcademicCourses : (int?)null
            };
        }

        public static AcademicPlanRecordViewModel CreateAcademicPlanRecordViewModel(AcademicPlanRecord entity)
        {
            return new AcademicPlanRecordViewModel
            {
                Id = entity.Id,
                AcademicPlanId = entity.AcademicPlanId,
                DisciplineId = entity.DisciplineId,
                ContingentId = entity.ContingentId,
                Disciplne = entity.Discipline.DisciplineName,
                Semester = entity.Semester.HasValue ? entity.Semester.ToString() : string.Empty,
                ContingentGroup = entity.ContingentId.HasValue ? entity.Contingent.ContingentName : string.Empty,
                Zet = entity.Zet
            };
        }

        public static AcademicPlanRecordElementViewModel CreateAcademicPlanRecordElementViewModel(AcademicPlanRecordElement entity)
        {
            return new AcademicPlanRecordElementViewModel
            {
                Id = entity.Id,
                AcademicPlanRecordId = entity.AcademicPlanRecordId,
                TimeNormId = entity.TimeNormId,
                Disciplne = entity.AcademicPlanRecord.Discipline.DisciplineName,
                KindOfLoadName = entity.TimeNorm.KindOfLoadName,
                PlanHours = entity.PlanHours,
                FactHours = entity.FactHours
            };
        }

        public static AcademicYearViewModel CreateAcademicYearViewModel(AcademicYear entity)
        {
            return new AcademicYearViewModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static StreamLessonViewModel CreateStreamLessonViewModel(StreamLesson entity)
        {
            return new StreamLessonViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title,
                StreamLessonName = entity.StreamLessonName,
                StreamLessonHours = entity.StreamLessonHours
            };
        }

        public static StreamLessonRecordViewModel CreateStreamLessonRecordViewModel(StreamLessonRecord entity)
        {
            return new StreamLessonRecordViewModel
            {
                Id = entity.Id,
                StreamLessonId = entity.StreamLessonId,
                AcademicPlanRecordElementId = entity.AcademicPlanRecordElementId,
                StreamLessonName = entity.StreamLesson.StreamLessonName,
                AcademicPlanRecordElementText = string.Format("{0} {1} {2}",
                    entity.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection.ShortName,
                    entity.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName,
                    entity.AcademicPlanRecordElement.TimeNorm.KindOfLoadName),
                IsMain = entity.IsMain
            };
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

        public static StreamingLessonViewModel CreateStreamingLessonViewModel(StreamingLesson entity)
        {
            return new StreamingLessonViewModel
            {
                Id = entity.Id,
                IncomingGroups = entity.IncomingGroups,
                StreamName = entity.StreamName
            };
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
        #endregion


        public static AcademicPlanRecordForDiciplineViewModel CreateAcademicPlanRecordForDiciplineViewModel(AcademicPlanRecord entity)
        {
            return new AcademicPlanRecordForDiciplineViewModel
            {
                Id = entity.Id,
                AcademicPlanId = entity.AcademicPlanId,
                EducationDirectionShortName = entity.AcademicPlan.EducationDirection.ShortName,
                DisciplineId = entity.DisciplineId,
                Disciplne = entity.Discipline.DisciplineName,
                Semester = entity.Semester.ToString()
            };
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
                IsFirstHalfSemester = entity.IsFirstHalfSemester,
                IsStreaming = entity.IsStreaming,
                IsSubgroup = entity.IsSubgroup,
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
                IsSubgroup = entity.IsSubgroup,
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

        public static UserViewModel CreateUserViewModel(User entity)
        {
            return new UserViewModel
            {
                Id = entity.Id,
                Login = entity.Login,
                StudentId = entity.StudentId,
                LecturerId = entity.LecturerId,
                Avatar = entity.Avatar != null && entity.Avatar.Length > 0 ? Image.FromStream(new MemoryStream(entity.Avatar)) : null,
                IsBanned = entity.IsBanned,
                DateBanned = entity.DateBanned,
                DateLastVisit = entity.DateLastVisit
            };
        }
        #endregion

        #region LaboratoryHead
        public static MaterialTechnicalValueViewModel CreateMaterialTechnicalValueViewModel(MaterialTechnicalValue entity)
        {
            return new MaterialTechnicalValueViewModel
            {
                Id = entity.Id,
                DateInclude = entity.DateCreate,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom.Number,
                InventoryNumber = entity.InventoryNumber,
                FullName = entity.FullName,
                Description = entity.Description,
                Location = entity.Location,
                Cost = entity.Cost,
                DateDelete = entity.DateDelete,
                DeleteReason = entity.DeleteReason
            };
        }

        public static MaterialTechnicalValueGroupViewModel CreateMaterialTechnicalValueGroupViewModel(MaterialTechnicalValueGroup entity)
        {
            return new MaterialTechnicalValueGroupViewModel
            {
                Id = entity.Id,
                GroupName = entity.GroupName,
                Order = entity.Order
            };
        }

        public static MaterialTechnicalValueRecordViewModel CreateMaterialTechnicalValueRecordViewModel(MaterialTechnicalValueRecord entity)
        {
            return new MaterialTechnicalValueRecordViewModel
            {
                Id = entity.Id,
                MaterialTechnicalValueId = entity.MaterialTechnicalValueId,
                InventoryNumber = entity.MaterialTechnicalValue.InventoryNumber,
                MaterialTechnicalValueGroupId = entity.MaterialTechnicalValueGroupId,
                GroupName = entity.MaterialTechnicalValueGroup.GroupName,
                GroupOrder = entity.MaterialTechnicalValueGroup.Order,
                FieldName = entity.FieldName,
                FieldValue = entity.FieldValue,
                Order = entity.Order
            };
        }

        public static SoftwareViewModel CreateSoftwareViewModel(Software entity)
        {
            return new SoftwareViewModel
            {
                Id = entity.Id,
                SoftwareName = entity.SoftwareName,
                SoftwareDescription = entity.SoftwareDescription,
                SoftwareKey = entity.SoftwareKey,
                SoftwareK = entity.SoftwareK
            };
        }

        public static SoftwareRecordViewModel CreateSoftwareRecordViewModel(SoftwareRecord entity)
        {
            return new SoftwareRecordViewModel
            {
                Id = entity.Id,
                DateSetup = entity.DateCreate,
                MaterialTechnicalValueId = entity.MaterialTechnicalValueId,
                SoftwareId = entity.SoftwareId,
                InventoryNumber = entity.MaterialTechnicalValue.InventoryNumber,
                SoftwareName = entity.Software.SoftwareName,
                SoftwareKey = entity.Software.SoftwareKey,
                SetupDescription = entity.SetupDescription,
                ClaimNumber = entity.ClaimNumber
            };
        }
        #endregion

        #region LearningProgress
        public static DisciplineLessonViewModel CreateDisciplineLessonViewModel(DisciplineLesson entity)
        {
            return new DisciplineLessonViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                DisciplineId = entity.DisciplineId,
                AcademicYear = entity.AcademicYear.Title,
                Discipline = entity.Discipline.DisciplineName,
                Semester = entity.Semester,
                LessonType = entity.LessonType,
                Title = entity.Title,
                Description = entity.Description,
                Order = entity.Order,
                DisciplineLessonFile = entity.DisciplineLessonFile,
                CountOfPairs = entity.CountOfPairs,
                CountTasks = entity.DisciplineLessonTasks.Count,
                Date = entity.Date
            };
        }

        public static DisciplineLessonTaskViewModel CreateDisciplineLessonTaskViewModel(DisciplineLessonTask entity)
        {
            return new DisciplineLessonTaskViewModel
            {
                Id = entity.Id,
                DisciplineLessonId = entity.DisciplineLessonId,
                DisciplineLessonTitle = entity.DisciplineLesson.Title,
                Task = entity.Task,
                Description = entity.Description,
                MaxBall = entity.MaxBall,
                Order = entity.Order,
                Image = entity.Image,
                IsNecessarily = entity.IsNecessarily
            };
        }

        public static DisciplineLessonTaskVariantViewModel CreateDisciplineLessonTaskVariantViewModel(DisciplineLessonTaskVariant entity)
        {
            return new DisciplineLessonTaskVariantViewModel
            {
                Id = entity.Id,
                DisciplineLessonTaskId = entity.DisciplineLessonTaskId,
                DisciplineLessonTaskTask = entity.DisciplineLessonTask.Task,
                VariantNumber = entity.VariantNumber,
                VariantTask = entity.VariantTask,
                Order = entity.Order
            };
        }
        #endregion
    }
}
