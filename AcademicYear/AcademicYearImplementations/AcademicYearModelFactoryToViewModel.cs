using AcademicYearInterfaces.ViewModels;
using Enums;
using Models.AcademicYearData;

namespace AcademicYearImplementations
{
    public static class AcademicYearModelFactoryToViewModel
    {
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

        public static AcademicPlanRecordMissionViewModel CreateAcademicPlanRecordMissionViewModel(AcademicPlanRecordMission entity)
        {
            return new AcademicPlanRecordMissionViewModel
            {
                Id = entity.Id,
                AcademicPlanRecordElementId = entity.AcademicPlanRecordElementId,
                LecturerId = entity.LecturerId,
                AcademicPlanRecordElementTitle = entity.AcademicPlanRecordElement.TimeNorm.TimeNormName,
                LecturerName = entity.Lecturer.ToString(),
                Hours = entity.Hours
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

        public static LecturerWorkloadViewModel CreateLecturerWorkloadViewModel(LecturerWorkload entity)
        {
            return new LecturerWorkloadViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                LecturerId = entity.LecturerId,
                AcademicYear = entity.AcademicYear.Title,
                Lecturer = entity.Lecturer.ToString(),
                Workload = entity.Workload
            };
        }
        
        public static DisciplineTimeDistributionViewModel CreateDisciplineTimeDistributionViewModel(DisciplineTimeDistribution entity)
        {
            return new DisciplineTimeDistributionViewModel
            {
                Id = entity.Id,
                AcademicPlanRecordId = entity.AcademicPlanRecordId,
                StudentGroupId = entity.StudentGroupId,
                Comment = entity.Comment.ToString(),
                CommentWishesOfTeacher = entity.CommentWishesOfTeacher.ToString(),
                Semester = entity.AcademicPlanRecord.Semester.ToString(),
                DisciplineName = entity.AcademicPlanRecord.Discipline.DisciplineName.ToString(),
                StudentGroupName = entity.StudentGroup.GroupName.ToString()
            };
        }

        public static DisciplineTimeDistributionRecordViewModel CreateDisciplineTimeDistributionRecordViewModel(DisciplineTimeDistributionRecord entity)
        {
            return new DisciplineTimeDistributionRecordViewModel
            {
                Id = entity.Id,
                DisciplineTimeDistributionId = entity.DisciplineTimeDistributionId,
                TimeNormId = entity.TimeNormId,
                WeekNumber = entity.WeekNumber,
                Hours = entity.Hours,
                TimeNormName = entity.TimeNorm.TimeNormName,
                TimeNormHours = entity.TimeNorm.Hours.ToString()
            };
        }

        public static DisciplineTimeDistributionClassroomViewModel CreateDisciplineTimeDistributionClassroomViewModel(DisciplineTimeDistributionClassroom entity)
        {
            return new DisciplineTimeDistributionClassroomViewModel
            {
                Id = entity.Id,
                DisciplineTimeDistributionId = entity.DisciplineTimeDistributionId,
                TimeNormId = entity.TimeNormId,
                ClassroomDescription = entity.ClassroomDescription
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
                TimeNormKoef = entity.TimeNormKoef.ToString(),
                UseInLearningProgress = entity.UseInLearningProgress
            };
        }
        
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
    }
}