using AuthenticationModels.Models;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using System;

namespace DepartmentService.BindingModels
{
    public static class ModelFacotryFromBindingModel
	{
        #region EducationDirection
        public static EducationDirection CreateEducationDirection(EducationDirectionSetBindingModel model, EducationDirection entity = null)
		{
			if (entity == null)
			{
                entity = new EducationDirection();
			}
			entity.Cipher = model.Cipher;
            entity.ShortName = model.ShortName;
			entity.Description = model.Description;
			entity.Title = model.Title;

			return entity;
		}

		public static DisciplineBlock CreateDisciplineBlock(DisciplineBlockSetBindingModel model, DisciplineBlock entity = null)
		{
			if (entity == null)
			{
				entity = new DisciplineBlock();
			}
			entity.Title = model.Title;
            entity.DisciplineBlockBlueAsteriskName = model.DisciplineBlockBlueAsteriskName;
            entity.DisciplineBlockUseForGrouping = model.DisciplineBlockUseForGrouping;
            entity.DisciplineBlockOrder = model.DisciplineBlockOrder;

			return entity;
        }

        public static LecturerPost CreateLecturerPost(LecturerPostSetBindingModel model, LecturerPost entity = null)
        {
            if (entity == null)
            {
                entity = new LecturerPost();
            }
            entity.PostTitle = model.PostTitle;
            entity.Hours = model.Hours;

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


        public static Classroom CreateClassroom(ClassroomSetBindingModel model, Classroom entity = null)
		{
			if (entity == null)
			{
				entity = new Classroom();
			}
            entity.Number = model.Number;
			entity.Capacity = model.Capacity;
			entity.ClassroomType = (ClassroomTypes)Enum.Parse(typeof(ClassroomTypes), model.ClassroomType);
            entity.NotUseInSchedule = model.NotUseInSchedule;

			return entity;
		}

		public static Discipline CreateDiscipline(DisciplineSetBindingModel model, Discipline entity = null)
		{
			if (entity == null)
			{
				entity = new Discipline();
			}
			entity.DisciplineBlockId = model.DisciplineBlockId;
			entity.DisciplineName = model.DisciplineName;
            entity.DisciplineShortName = model.DisciplineShortName;
            entity.DisciplineBlueAsteriskName = model.DisciplineBlueAsteriskName;

            return entity;
		}

        public static Lecturer CreateLecturer(LecturerSetBindingModel model, Lecturer entity = null)
		{
			if (entity == null)
			{
				entity = new Lecturer();
			}
            entity.LecturerPostId = model.LecturerPostId;
			entity.FirstName = model.FirstName;
			entity.LastName = model.LastName;
			entity.Patronymic = model.Patronymic;
			entity.Abbreviation = model.Abbreviation;
			entity.DateBirth = model.DateBirth;
			entity.Post = (Post)Enum.Parse(typeof(Post), model.Post);
			entity.Rank = (Rank)Enum.Parse(typeof(Rank), model.Rank);
            entity.Rank2 = (Rank2)Enum.Parse(typeof(Rank2), model.Rank2);
            entity.Address = model.Address;
			entity.HomeNumber = model.HomeNumber;
			entity.MobileNumber = model.MobileNumber;
			entity.Email = model.Email;
			entity.Description = model.Description;
			entity.Photo = model.Photo;

			return entity;
		}

		public static StudentGroup CreateStudentGroup(StudentGroupSetBindingModel model, StudentGroup entity = null)
		{
			if (entity == null)
			{
				entity = new StudentGroup();
			}
			entity.EducationDirectionId = model.EducationDirectionId;
			entity.GroupName = model.GroupName;
			entity.Course = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.Course);
            entity.StewardName = model.StewardName;
            entity.CuratorId = model.CuratorId;

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
            entity.TimeNormAcademicLevel = string.IsNullOrEmpty(model.TimeNormAcademicLevel) ? (AcademicLevel?)null : (AcademicLevel)Enum.Parse(typeof(AcademicLevel), model.TimeNormAcademicLevel);
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


		public static AcademicPlan CreateAcademicPlan(AcademicPlanSetBindingModel model, AcademicPlan entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicPlan();
			}
			entity.EducationDirectionId = model.EducationDirectionId;
			entity.AcademicYearId = model.AcademicYearId;
			entity.AcademicLevel = (AcademicLevel)Enum.Parse(typeof(AcademicLevel), model.AcademicLevel);
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
            entity.Semester = string.IsNullOrEmpty(model.Semester) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Zet = model.Zet;

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

        public static Statement CreateStatement(StatementSetBindingModel model, Statement entity = null)
        {
            if (entity == null)
            {
                entity = new Statement();
            }
            entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
            entity.LecturerId = model.LecturerId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.Course = (AcademicCourse)Enum.Parse(typeof(AcademicCourse), model.Course); //TODO: тут нужно проверить правильность перечислений
            entity.Semester = string.IsNullOrEmpty(model.Semester) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.TypeOfTest = (TypeOfTest)Enum.Parse(typeof(TypeOfTest), model.TypeOfTest.Replace(' ', '_'));
            entity.Date = model.Date;
            return entity;
        }

        public static StatementRecord CreateStatementRecord(StatementRecordSetBindingModel model, StatementRecord entity = null)
        {
            if (entity == null)
            {
                entity = new StatementRecord();
            }
            entity.StatementId = model.StatementId;
            entity.StudentId = model.StudentId;
            entity.Score = model.Score;
            return entity;
        }

        public static StatementRecordExtended CreateStatementRecordExtended(StatementRecordExtendedSetBindingModel model, StatementRecordExtended entity = null)
        {
            if (entity == null)
            {
                entity = new StatementRecordExtended();
            }
            entity.StatementRecordId = model.StatementRecordId;
            entity.Name = model.Name;
            return entity;
        }

        public static StreamLesson CreateStreamLesson(StreamLessonSetBindingModel model, StreamLesson entity = null)
        {
            if (entity == null)
            {
                entity = new StreamLesson();
            }
            entity.AcademicYearId = model.AcademicYearId;
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



        public static Student CreateStudent(StudentSetBindingModel model, Student entity = null)
		{
			if (entity == null)
			{
				entity = new Student();
			}
            entity.StudentGroupId = model.StudentGroupId;
            entity.NumberOfBook = model.NumberOfBook;
			entity.LastName = model.LastName;
			entity.FirstName = model.FirstName;
			entity.Patronymic = model.Patronymic;
			entity.Email = model.Email;
			entity.Description = model.Description;
			entity.Photo = model.Photo;
            entity.IsSteward = model.IsSteward;

			return entity;
		}

		public static StudentHistory CreateStudentHistory(StudentHistorySetBindingModel model, StudentHistory entity = null)
		{
			if (entity == null)
			{
				entity = new StudentHistory
				{
					StudentId = model.StudetnId
				};
			}
			entity.TextMessage = model.TextMessage;

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

		public static StreamingLesson CreateStreamingLesson(StreamingLessonSetBindingModel model, StreamingLesson entity = null)
		{
			if (entity == null)
			{
				entity = new StreamingLesson();
			}
			entity.IncomingGroups = model.IncomingGroups;
			entity.StreamName = model.StreamName;

			return entity;
		}

        public static IndividualPlanTitle CreateIndividualPlanTitle(IndividualPlanTitleSetBindingModel model, IndividualPlanTitle entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanTitle();
            }

            entity.Title = model.Title;
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
            return entity;
        }

        public static IndividualPlanRecord CreateIndividualPlanRecord(IndividualPlanRecordSetBindingModel model, IndividualPlanRecord entity = null)
        {
            if (entity == null)
            {
                entity = new IndividualPlanRecord();
            }
            entity.IndividualPlanKindOfWorkId = model.IndividualPlanKindOfWorkId;
            entity.LecturerId = model.LecturerId;
            entity.AcademicYearId = model.AcademicYearId;
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
            entity.LecturerId = model.LecturerId;
            entity.Name = model.Name;
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
            entity.LecturerId = model.LecturerId;
            entity.JobContent = model.JobContent;
            entity.PlannedTerm = model.PlannedTerm;
            entity.Post = model.Post;
            entity.ReadyMark = model.ReadyMark;
            return entity;
        }

        public static Grafic CreateGrafic(GraficSetBindingModel model, Grafic entity = null)
        {
            if (entity == null)
            {
                entity = new Grafic();
            }
            entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.Comment = model.Comment;
            entity.CommentWishesOfTeacher = model.CommentWishesOfTeacher;
            return entity;
        }

        public static GraficRecord CreateGraficRecord(GraficRecordSetBindingModel model, GraficRecord entity = null)
        {
            if (entity == null)
            {
                entity = new GraficRecord();
            }
            entity.GraficId = model.GraficId;
            entity.TimeNormId = model.TimeNormId;
            entity.WeekNumber = model.WeekNumber;
            entity.Hours = model.Hours;
            return entity;
        }

        public static GraficClassroom CreateGraficClassroom(GraficClassroomSetBindingModel model, GraficClassroom entity = null)
        {
            if (entity == null)
            {
                entity = new GraficClassroom();
            }
            entity.GraficId = model.GraficId;
            entity.TimeNormId = model.TimeNormId;
            entity.ClassroomDescription = model.ClassroomDescription;
            return entity;
        }

        #endregion

        #region LaboratoryHead
        public static MaterialTechnicalValue CreateMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model, MaterialTechnicalValue entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValue();
            }
            entity.DateCreate = model.DateInclude;
            entity.ClassroomId = model.ClassroomId;
            entity.InventoryNumber = model.InventoryNumber;
            entity.FullName = model.FullName;
            entity.Description = model.Description;
            entity.Location = model.Location;
            entity.Cost = model.Cost;
            entity.DeleteReason = model.DeleteReason;

            return entity;
        }

        public static MaterialTechnicalValueGroup CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model, MaterialTechnicalValueGroup entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValueGroup();
            }
            entity.GroupName = model.GroupName;
            entity.Order = model.Order;

            return entity;
        }

        public static MaterialTechnicalValueRecord CreateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model, MaterialTechnicalValueRecord entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValueRecord();
            }
            entity.MaterialTechnicalValueId = model.MaterialTechnicalValueId;
            entity.MaterialTechnicalValueGroupId = model.MaterialTechnicalValueGroupId;
            entity.FieldName = model.FieldName;
            entity.FieldValue = model.FieldValue;
            entity.Order = model.Order;

            return entity;
        }

        public static Software CreateSoftware(SoftwareSetBindingModel model, Software entity = null)
        {
            if (entity == null)
            {
                entity = new Software();
            }
            entity.SoftwareName = model.SoftwareName;
            entity.SoftwareDescription = model.SoftwareDescription;
            entity.SoftwareKey = model.SoftwareKey;
            entity.SoftwareK = model.SoftwareK;

            return entity;
        }

        public static SoftwareRecord CreateSoftwareRecord(SoftwareRecordSetBindingModel model, SoftwareRecord entity = null)
        {
            if (entity == null)
            {
                entity = new SoftwareRecord();
            }
            entity.DateCreate = model.DateSetup;
            entity.MaterialTechnicalValueId = model.MaterialTechnicalValueId;
            entity.SoftwareId = model.SoftwareId;
            entity.SetupDescription = model.SetupDescription;
            entity.ClaimNumber = model.ClaimNumber;

            return entity;
        }
        #endregion

        #region LearningProgress
        public static DisciplineLesson CreateDisciplineLesson(DisciplineLessonRecordBindingModel model, DisciplineLesson entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLesson();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.DisciplineId = model.DisciplineId;
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.TimeNormId = model.TimeNormId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Order = model.Order;
            entity.DisciplineLessonFile = model.DisciplineLessonFile;
            entity.Date = model.Date;
            entity.CountOfPairs = model.CountOfPairs;

            return entity;
        }

        public static DisciplineLessonTask CreateDisciplineLessonTask(DisciplineLessonTaskRecordBindingModel model, DisciplineLessonTask entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonTask();
            }
            entity.DisciplineLessonId = model.DisciplineLessonId;
            entity.Order = model.Order;
            entity.Description = model.Description;
            entity.Image = model.Image;
            entity.IsNecessarily = model.IsNecessarily;
            entity.MaxBall = model.MaxBall;
            entity.Task = model.Task;

            return entity;
        }

        public static DisciplineLessonTaskVariant CreateDisciplineLessonTaskVariant(DisciplineLessonTaskVariantRecordBindingModel model, DisciplineLessonTaskVariant entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonTaskVariant();
            }
            entity.DisciplineLessonTaskId = model.DisciplineLessonTaskId;
            entity.VariantNumber = model.VariantNumber;
            entity.VariantTask = model.VariantTask;
            entity.Order = model.Order;

            return entity;
        }

        public static DisciplineStudentRecord CreateDisciplineStudentRecord(DisciplineStudentRecordSetBindingModel model, DisciplineStudentRecord entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineStudentRecord();
            }
            entity.DisciplineId = model.DisciplineId;
            entity.StudentId = model.StudentId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Variant = model.Variant;
            entity.SubGroup = model.SubGroup;

            return entity;
        }

        public static DisciplineLessonConducted CreateDisciplineLessonConducted(DisciplineLessonConductedSetBindingModel model, DisciplineLessonConducted entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonConducted();
            }
            entity.DisciplineLessonId = model.DisciplineLessonId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.DateCreate = model.Date;
            entity.Subgroup = model.Subgroup;

            return entity;
        }

        public static DisciplineLessonConductedStudent CreateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model, DisciplineLessonConductedStudent entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonConductedStudent();
            }
            entity.DisciplineLessonConductedId = model.DisciplineLessonConductedId;
            entity.StudentId = model.StudentId;
            entity.Status = (DisciplineLessonStudentStatus)Enum.Parse(typeof(DisciplineLessonStudentStatus), model.Status);
            entity.Comment = model.Comment;
            entity.Ball = model.Ball;

            return entity;
        }

        public static DisciplineLessonTaskStudentAccept CreateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model, DisciplineLessonTaskStudentAccept entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonTaskStudentAccept();
            }
            entity.DisciplineLessonTaskId = model.DisciplineLessonTaskId;
            entity.StudentId = model.StudentId;
            entity.Result = (DisciplineLessonTaskStudentResult)Enum.Parse(typeof(DisciplineLessonTaskStudentResult), model.Result);
            entity.Task = model.Task;
            entity.DateAccept = model.DateAccept;
            entity.Score = model.Score;
            entity.Comment = model.Comment;
            entity.Log = model.Log;

            return entity;
        }
        #endregion
    }
}