using AcademicYearInterfaces.BindingModels;
using Enums;
using Models.AcademicYearData;
using System;

namespace AcademicYearImplementations
{
    public static class AcademicYearModelFacotryFromBindingModel
	{
		public static AcademicPlan CreateAcademicPlan(AcademicPlanSetBindingModel model, AcademicPlan entity = null)
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

		public static AcademicPlanRecord CreateAcademicPlanRecord(AcademicPlanRecordSetBindingModel model, AcademicPlanRecord entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicPlanRecord();
			}
            entity.AcademicPlanId = model.AcademicPlanId;
			entity.DisciplineId = model.DisciplineId;
            entity.ContingentId = model.ContingentId;
            entity.InDepartment = model.InDepartment;
            entity.Semester = string.IsNullOrEmpty(model.Semester) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Zet = model.Zet;
            entity.IsChild = model.IsChild;
            entity.IsUseInWorkload = model.IsUseInWorkload;
            entity.IsActiveSemester = model.IsActiveSemester;

            return entity;
		}

        public static AcademicPlanRecordElement CreateAcademicPlanRecordElement(AcademicPlanRecordElementSetBindingModel model, AcademicPlanRecordElement entity = null)
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

        public static AcademicPlanRecordMission CreateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model, AcademicPlanRecordMission entity = null)
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

        public static AcademicYear CreateAcademicYear(AcademicYearSetBindingModel model, AcademicYear entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicYear();
			}
			entity.Title = model.Title;

			return entity;
		}

		public static Contingent CreateContingent(ContingentSetBindingModel model, Contingent entity = null)
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

        public static DisciplineTimeDistribution CreateDisciplineTimeDistribution(DisciplineTimeDistributionSetBindingModel model, DisciplineTimeDistribution entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineTimeDistribution();
            }
            entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.Comment = model.Comment;
            entity.CommentWishesOfTeacher = model.CommentWishesOfTeacher;
            return entity;
        }

        public static DisciplineTimeDistributionRecord CreateDisciplineTimeDistributionRecord(DisciplineTimeDistributionRecordSetBindingModel model, DisciplineTimeDistributionRecord entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineTimeDistributionRecord();
            }
            entity.DisciplineTimeDistributionId = model.DisciplineTimeDistributionId;
            entity.TimeNormId = model.TimeNormId;
            entity.WeekNumber = model.WeekNumber;
            entity.Hours = model.Hours;
            return entity;
        }

        public static DisciplineTimeDistributionClassroom CreateDisciplineTimeDistributionClassroom(DisciplineTimeDistributionClassroomSetBindingModel model, DisciplineTimeDistributionClassroom entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineTimeDistributionClassroom();
            }
            entity.DisciplineTimeDistributionId = model.DisciplineTimeDistributionId;
            entity.TimeNormId = model.TimeNormId;
            entity.ClassroomDescription = model.ClassroomDescription;
            return entity;
        }
        
        public static LecturerWorkload CreateLecturerWorkload(LecturerWorkloadSetBindingModel model, LecturerWorkload entity = null)
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

		public static SeasonDates CreateSeasonDates(SeasonDatesSetBindingModel model, SeasonDates entity = null)
		{
			if (entity == null)
			{
				entity = new SeasonDates();
			}
			entity.Title = model.Title;
            entity.AcademicYearId = model.AcademicYearId;
			entity.DateBeginExamination = model.DateBeginExamination;
			entity.DateBeginOffset = model.DateBeginOffset;
			entity.DateBeginPractice = model.DateBeginPractice;
			entity.DateBeginFirstHalfSemester = model.DateBeginFirstHalfSemester;
            entity.DateBeginSecondHalfSemester = model.DateBeginSecondHalfSemester;
            entity.DateEndExamination = model.DateEndExamination;
			entity.DateEndOffset = model.DateEndOffset;
			entity.DateEndPractice = model.DateEndPractice;
			entity.DateEndFirstHalfSemester = model.DateEndFirstHalfSemester;
            entity.DateEndSecondHalfSemester = model.DateEndSecondHalfSemester;

            return entity;
		}

        public static StreamLesson CreateStreamLesson(StreamLessonSetBindingModel model, StreamLesson entity = null)
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

        public static StreamLessonRecord CreateStreamLessonRecord(StreamLessonRecordSetBindingModel model, StreamLessonRecord entity = null)
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
        
		public static TimeNorm CreateTimeNorm(TimeNormSetBindingModel model, TimeNorm entity = null)
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

        public static IndividualPlanTitle CreateIndividualPlanTitle(IndividualPlanTitleSetBindingModel model, IndividualPlanTitle entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanTitle();
            }

            entity.Title = model.Title;
            entity.Order = model.Order;
            return entity;
        }

        public static IndividualPlanKindOfWork CreateIndividualPlanKindOfWork(IndividualPlanKindOfWorkSetBindingModel model, IndividualPlanKindOfWork entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanKindOfWork();
            }
            entity.IndividualPlanTitleId = model.IndividualPlanTitleId;
            entity.Name = model.Name;
            entity.TimeNormDescription = model.TimeNormDescription;
            entity.Order = model.Order;
            return entity;
        }

        public static IndividualPlan CreateIndividualPlan(IndividualPlanSetBindingModel model, IndividualPlan entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlan();
            }
            entity.LecturerId = model.LecturerId;
            entity.AcademicYearId = model.AcademicYearId;
            return entity;
        }

        public static IndividualPlanRecord CreateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model, IndividualPlanRecord entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanRecord();
            }
            entity.IndividualPlanKindOfWorkId = model.IndividualPlanKindOfWorkId;
            entity.IndividualPlanId = model.IndividualPlanId;
            entity.PlanAutumn = model.PlanAutumn;
            entity.FactAutumn = model.FactAutumn;
            entity.PlanSpring = model.PlanSpring;
            entity.FactSpring = model.FactSpring;
            return entity;
        }

        public static IndividualPlanNIRScientificArticle CreateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model, IndividualPlanNIRScientificArticle entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanNIRScientificArticle();
            }
            entity.IndividualPlanId = model.IndividualPlanId;
            entity.Name = model.Name;
            entity.Order = model.Order;
            entity.Publishing = model.Publishing;
            entity.Status = model.Status;
            entity.TypeOfPublication = model.TypeOfPublication;
            entity.Volume = model.Volume;
            entity.Year = model.Year;
            return entity;
        }

        public static IndividualPlanNIRContractualWork CreateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model, IndividualPlanNIRContractualWork entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanNIRContractualWork();
            }
            entity.IndividualPlanId = model.IndividualPlanId;
            entity.JobContent = model.JobContent;
            entity.PlannedTerm = model.PlannedTerm;
            entity.Post = model.Post;
            entity.ReadyMark = model.ReadyMark;
            return entity;
        }
    }
}