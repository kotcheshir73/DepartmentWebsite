using Enums;
using Models.AcademicYearData;
using Models.Web;
using System;
using WebInterfaces.BindingModels;

namespace WebImplementations
{
    public static class WebModelFacotryFromBindingModel
    {
        public static News CreateNews(NewsSetBindingModel model, News entity = null)
        {
            if (entity == null)
            {
                entity = new News();

            }
            entity.DepartmentUserId = model.DepartmentUserId;
            entity.Title = model.Title;
            entity.Body = model.Body;
            entity.Tag = model.Tag;

            return entity;
        }

        public static Comment CreateComment(CommentSetBindingModel model, Comment entity = null)
        {
            if (entity == null)
            {
                entity = new Comment
                {
                    DepartmentUserId = model.DepartmentUserId.Value,
                    DisciplineId = model.DisciplineId,
                    NewsId = model.NewsId,
                    ParentId = model.ParentId
                };
            }

            entity.Content = model.Content;

            return entity;
        }

        public static AcademicYear CreateAcademicYear(WebAcademicYearSetBindingModel model, AcademicYear entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicYear();
            }
            entity.Title = model.Title;

            return entity;
        }

        public static AcademicPlan CreateAcademicPlan(WebAcademicPlanSetBindingModel model, AcademicPlan entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicPlan();
            }
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.AcademicYearId = model.AcademicYearId;
            entity.AcademicCourses = model.AcademicCourses.HasValue ? (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.AcademicCourses) : (AcademicCourse?)null;

            return entity;
        }

        public static AcademicPlanRecord CreateAcademicPlanRecord(WebAcademicPlanRecordSetBindingModel model, AcademicPlanRecord entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicPlanRecord();
            }
            entity.AcademicPlanId = model.AcademicPlanId;
            entity.DisciplineId = model.DisciplineId;
            entity.ContingentId = model.ContingentId;
            entity.Semester = string.IsNullOrEmpty(model.Semester) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Zet = model.Zet;
            entity.IsChild = model.Selectable;
            entity.IsUseInWorkload = model.IsSelected;

            return entity;
        }

        public static AcademicPlanRecordElement CreateAcademicPlanRecordElement(WebAcademicPlanRecordElementSetBindingModel model, AcademicPlanRecordElement entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicPlanRecordElement();
            }
            entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
            entity.TimeNormId = model.TimeNormId;
            entity.PlanHours = model.PlanHours;
            entity.FactHours = model.FactHours;
            return entity;
        }

        public static AcademicPlanRecordMission CreateAcademicPlanRecordMission(WebAcademicPlanRecordMissionSetBindingModel model, AcademicPlanRecordMission entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicPlanRecordMission();
            }
            entity.AcademicPlanRecordElementId = model.AcademicPlanRecordElementId;
            entity.LecturerId = model.LecturerId;
            entity.Hours = model.Hours;
            return entity;
        }

        public static StreamLesson CreateStreamLesson(WebStreamLessonSetBindingModel model, StreamLesson entity = null)
        {
            if (entity == null)
            {
                entity = new StreamLesson();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.StreamLessonName = model.StreamLessonName;
            entity.StreamLessonHours = model.StreamLessonHours;

            return entity;
        }

        public static StreamLessonRecord CreateStreamLessonRecord(WebStreamLessonRecordSetBindingModel model, StreamLessonRecord entity = null)
        {
            if (entity == null)
            {
                entity = new StreamLessonRecord();
            }
            entity.StreamLessonId = model.StreamLessonId;
            entity.AcademicPlanRecordElementId = model.AcademicPlanRecordElementId;
            entity.IsMain = model.IsMain;

            return entity;
        }

        public static TimeNorm CreateTimeNorm(WebTimeNormSetBindingModel model, TimeNorm entity = null)
        {
            if (entity == null)
            {
                entity = new TimeNorm();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.DisciplineBlockId = model.DisciplineBlockId;
            entity.TimeNormName = model.TimeNormName;
            entity.TimeNormShortName = model.TimeNormShortName;
            entity.TimeNormOrder = model.TimeNormOrder;
            entity.TimeNormEducationDirectionQualification = string.IsNullOrEmpty(model.TimeNormEducationDirectionQualification) ? (EducationDirectionQualification?)null : (EducationDirectionQualification)Enum.Parse(typeof(EducationDirectionQualification), model.TimeNormEducationDirectionQualification);
            entity.KindOfLoadName = model.KindOfLoadName;
            entity.KindOfLoadAttributeName = model.KindOfLoadAttributeName;
            entity.KindOfLoadBlueAsteriskName = model.KindOfLoadBlueAsteriskName;
            entity.KindOfLoadBlueAsteriskAttributeName = model.KindOfLoadBlueAsteriskAttributeName;
            entity.KindOfLoadBlueAsteriskPracticName = model.KindOfLoadBlueAsteriskPracticName;
            entity.KindOfLoadType = (KindOfLoadType)Enum.Parse(typeof(KindOfLoadType), model.KindOfLoadType);
            entity.Hours = model.Hours;
            entity.NumKoef = model.NumKoef;
            entity.TimeNormKoef = string.IsNullOrEmpty(model.TimeNormKoef) ? TimeNormKoef.Пусто : (TimeNormKoef)Enum.Parse(typeof(TimeNormKoef), model.TimeNormKoef);
            entity.UseInLearningProgress = model.UseInLearningProgress;
            entity.UseInSite = model.UseInSite;

            return entity;
        }

        public static Contingent CreateContingent(WebContingentSetBindingModel model, Contingent entity = null)
        {
            if (entity == null)
            {
                entity = new Contingent();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.Course = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.Course);
            entity.ContingentName = model.ContingentName;
            entity.CountGroups = model.CountGroups;
            entity.CountStudetns = model.CountStudents;
            entity.CountSubgroups = model.CountSubgroups;

            return entity;
        }

        public static LecturerWorkload CreateLecturerWorkload(WebLecturerWorkloadSetBindingModel model, LecturerWorkload entity = null)
        {
            if (entity == null)
            {
                entity = new LecturerWorkload();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.LecturerId = model.LecturerId;
            entity.Workload = model.Workload;

            return entity;
        }
    }
}