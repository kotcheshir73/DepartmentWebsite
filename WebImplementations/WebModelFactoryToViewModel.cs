using Enums;
using Models.AcademicYearData;
using Models.Base;
using Models.Web;
using System;
using System.Linq;
using WebInterfaces.ViewModels;

namespace WebImplementations
{
    public static class WebModelFactoryToViewModel
    {
        public static WebEducationDirectionViewModel CreateWebEducationDirectionViewModel(IGrouping<EducationDirection, Contingent> entity)
        {
            return new WebEducationDirectionViewModel
            {
                Id = entity.Key.Id,
                Cipher = entity.Key.Cipher,
                Title = entity.Key.Title,
                ShortName = entity.Key.ShortName,
                Qualification = entity.Key.Qualification.ToString(),
                Profile = entity.Key.Profile,
                Description = entity.Key.Description,
                Courses = entity.Select(x => new Tuple<Guid, string>(x.Id, $"Курс {Math.Log((double)x.Course, 2) + 1}")).OrderBy(x => x.Item2).ToList()
            };
        }

        public static NewsViewModel CreateNewsViewModel(News entity)
        {
            return new NewsViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                DepartmentUserId = entity.DepartmentUserId,
                DepartmentUser = entity.DepartmentUser.UserName,
                Body = entity.Body,
                Date = entity.DateCreate,
                Tag = entity.Tag
            };
        }

        public static CommentViewModel CreateCommentViewModel(Comment entity)
        {
            return new CommentViewModel
            {
                Id = entity.Id,
                DepartmentUserId = entity.DepartmentUserId,
                DepartmentUser = entity.DepartmentUser.UserName,
                DisciplineId = entity.DisciplineId,
                NewsId = entity.NewsId,
                ParentId = entity.ParentId,
                Content = entity.Content,
                Date = entity.DateCreate,
                CountChilds = entity.Childs?.Where(x => !x.IsDeleted).Count() ?? 0
            };
        }

        public static WebAcademicYearViewModel CreateWebAcademicYearViewModel(AcademicYear entity)
        {
            return new WebAcademicYearViewModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static WebAcademicPlanViewModel CreateWebAcademicPlanViewModel(AcademicPlan entity)
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

            return new WebAcademicPlanViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirection = entity.EducationDirectionId.HasValue ? string.Format("{0} {1} ({2})", entity.EducationDirection.Cipher, entity.EducationDirection.ShortName, entity.EducationDirection.Profile) : string.Empty,
                AcademicCoursesStrings = courses,
                AcademicCourses = entity.AcademicCourses.HasValue ? (int)entity.AcademicCourses : (int?)null
            };
        }

        public static WebAcademicPlanRecordViewModel CreateWebAcademicPlanRecordViewModel(AcademicPlanRecord entity)
        {
            return new WebAcademicPlanRecordViewModel
            {
                Id = entity.Id,
                AcademicPlanId = entity.AcademicPlanId,
                DisciplineId = entity.DisciplineId,
                ContingentId = entity.ContingentId,
                Disciplne = entity.Discipline.DisciplineName,
                Semester = entity.Semester.HasValue ? entity.Semester.ToString() : string.Empty,
                ContingentGroup = entity.ContingentId.HasValue ? entity.Contingent.ContingentName : string.Empty,
                Zet = entity.Zet,
                AcademicPlanRecordParentId = entity.AcademicPlanRecordParentId,
                IsParent = entity.IsParent,
                IsSelected = entity.IsSelected,
                Selectable = entity.Selectable
            };
        }

        public static WebAcademicPlanRecordElementViewModel CreateWebAcademicPlanRecordElementViewModel(AcademicPlanRecordElement entity)
        {
            return new WebAcademicPlanRecordElementViewModel
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

        public static WebAcademicPlanRecordMissionViewModel CreateWebAcademicPlanRecordMissionViewModel(AcademicPlanRecordMission entity)
        {
            return new WebAcademicPlanRecordMissionViewModel
            {
                Id = entity.Id,
                AcademicPlanRecordElementId = entity.AcademicPlanRecordElementId,
                LecturerId = entity.LecturerId,
                AcademicPlanRecordElementTitle = entity.AcademicPlanRecordElement.TimeNorm.TimeNormName,
                LecturerName = entity.Lecturer.ToString(),
                Hours = entity.Hours
            };
        }

        public static WebStreamLessonViewModel CreateWebStreamLessonViewModel(StreamLesson entity)
        {
            return new WebStreamLessonViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title,
                StreamLessonName = entity.StreamLessonName,
                StreamLessonHours = entity.StreamLessonHours,
                Semester = entity.Semester.ToString(),
                Records = new WebStreamLessonRecordPageViewModel { }
            };
        }

        public static WebStreamLessonRecordViewModel CreateWebStreamLessonRecordViewModel(StreamLessonRecord entity)
        {
            return new WebStreamLessonRecordViewModel
            {
                Id = entity.Id,
                StreamLessonId = entity.StreamLessonId,
                AcademicPlanRecordElementId = entity.AcademicPlanRecordElementId,
                AcademicPlanRecordId = entity.AcademicPlanRecordElement.AcademicPlanRecordId,
                AcademicPlanId = entity.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlanId,
                StreamLessonName = entity.StreamLesson.StreamLessonName,
                AcademicPlanRecordElementText = string.Format("{0} {1} {2}",
                    entity.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection.ShortName,
                    entity.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName,
                    entity.AcademicPlanRecordElement.TimeNorm.KindOfLoadName),
                IsMain = entity.IsMain
            };
        }

        public static WebTimeNormViewModel CreateWebTimeNormViewModel(TimeNorm entity)
        {
            return new WebTimeNormViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title,
                DisciplineBlockId = entity.DisciplineBlockId,
                DisciplineBlockName = entity.DisciplineBlock.Title,
                TimeNormName = entity.TimeNormName,
                TimeNormShortName = entity.TimeNormShortName,
                TimeNormOrder = entity.TimeNormOrder,
                TimeNormEducationDirectionQualification = entity.TimeNormEducationDirectionQualification.HasValue ? entity.TimeNormEducationDirectionQualification.ToString() : null,
                KindOfLoadName = entity.KindOfLoadName,
                KindOfLoadAttributeName = entity.KindOfLoadAttributeName,
                KindOfLoadBlueAsteriskName = entity.KindOfLoadBlueAsteriskName,
                KindOfLoadBlueAsteriskAttributeName = entity.KindOfLoadBlueAsteriskAttributeName,
                KindOfLoadBlueAsteriskPracticName = entity.KindOfLoadBlueAsteriskPracticName,
                KindOfLoadType = entity.KindOfLoadType.ToString(),
                Hours = entity.Hours,
                NumKoef = entity.NumKoef,
                TimeNormKoef = entity.TimeNormKoef.ToString(),
                UseInLearningProgress = entity.UseInLearningProgress,
                UseInSite = entity.UseInSite
            };
        }

        public static WebContingentViewModel CreateWebContingentViewModel(Contingent entity)
        {
            return new WebContingentViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirection = entity.EducationDirection.ShortName,
                ContingentName = entity.ContingentName,
                Course = (Math.Log((int)entity.Course, 2.0) + 1).ToString(),
                CountGroups = entity.CountGroups,
                CountStudents = entity.CountStudetns,
                CountSubgroups = entity.CountSubgroups
            };
        }

        public static WebLecturerWorkloadViewModel CreateWebLecturerWorkloadViewModel(LecturerWorkload entity)
        {
            return new WebLecturerWorkloadViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                AcademicYear = entity.AcademicYear.Title,
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer.ToString(),
                Workload = entity.Workload
            };
        }
    }
}