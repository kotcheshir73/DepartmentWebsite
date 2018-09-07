using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentModel.Models.BaseEnities;
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

        public static AcademicYear CreateAcademicYear(AcademicYearSetBindingModel model, AcademicYear entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicYear();
			}
			entity.Title = model.Title;

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

		public static ScheduleLessonTime CreateScheduleLessonTime(ScheduleLessonTimeSetBindingModel model, ScheduleLessonTime entity = null)
		{
			if (entity == null)
			{
				entity = new ScheduleLessonTime();
			}
			entity.Title = model.Title;
            entity.Order = model.Order;
			entity.DateBeginLesson = model.DateBeginLesson;
			entity.DateEndLesson = model.DateEndLesson;

			return entity;
		}
        #endregion

        #region Schedule
        private static void CreateScheduleRecord(ScheduleRecordBindingModel model, ScheduleRecord entity = null)
        {
            if (!string.IsNullOrEmpty(model.LessonClassroom))
            {
                entity.LessonClassroom = model.LessonClassroom;
            }
            if (!string.IsNullOrEmpty(model.LessonDiscipline))
            {
                entity.LessonDiscipline = model.LessonDiscipline;
            }
            if (!string.IsNullOrEmpty(model.LessonLecturer))
            {
                entity.LessonLecturer = model.LessonLecturer;
            }
            if (!string.IsNullOrEmpty(model.LessonGroup))
            {
                entity.LessonGroup = model.LessonGroup;
            }
            if (model.ClassroomId.HasValue)
            {
                entity.ClassroomId = model.ClassroomId;
            }
            if (model.DisciplineId.HasValue)
            {
                entity.DisciplineId = model.DisciplineId;
            }
            if (model.LecturerId.HasValue)
            {
                entity.LecturerId = model.LecturerId;
            }
            if (model.StudentGroupId.HasValue)
            {
                entity.StudentGroupId = model.StudentGroupId;
            }
        }

        public static SemesterRecord CreateSemesterRecord(SemesterRecordRecordBindingModel model, SemesterRecord entity = null, SeasonDates seasonDate = null)
        {
            if (entity == null)
            {
                entity = new SemesterRecord()
                {
                    Week = model.Week,
                    Day = model.Day,
                    Lesson = model.Lesson,
                    SeasonDatesId = seasonDate.Id
                };
            }
            if (model.LessonType != LessonTypes.нд.ToString())
            {
                entity.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
            }
            entity.IsFirstHalfSemester = model.IsFirstHalfSemester;
            entity.IsStreaming = model.IsStreaming;
            entity.IsSubgroup = model.IsSubgroup;
            entity.NotParseRecord = model.NotParseRecord;
            CreateScheduleRecord(model, entity);

            return entity;
		}

		public static OffsetRecord CreateOffsetRecord(OffsetRecordRecordBindingModel model, OffsetRecord entity = null, SeasonDates seasonDate = null)
		{
			if (entity == null)
			{
				entity = new OffsetRecord()
				{
					Week = model.Week,
					Day = model.Day,
					Lesson = model.Lesson,
					NotParseRecord = model.NotParseRecord,
					SeasonDatesId = seasonDate.Id
				};
            }
            CreateScheduleRecord(model, entity);

            return entity;
		}

		public static ExaminationRecord CreateExaminationRecord(ExaminationRecordRecordBindingModel model, ExaminationRecord entity = null, SeasonDates seasonDate = null)
		{
			if (entity == null)
			{
				entity = new ExaminationRecord()
				{
					DateConsultation = model.DateConsultation,
					DateExamination = model.DateExamination,
					NotParseRecord = model.NotParseRecord,
					SeasonDatesId = seasonDate.Id,
                    ConsultationClassroomId = model.ConsultationClassroomId,
                    LessonConsultationClassroom = model.LessonConsultationClassroom
				};
			}
            CreateScheduleRecord(model, entity);

            return entity;
		}

		public static ConsultationRecord CreateConsultationRecord(ConsultationRecordRecordBindingModel model, ConsultationRecord entity = null, SeasonDates seasonDate = null)
		{
			if (entity == null)
			{
				entity = new ConsultationRecord()
				{
					DateConsultation = model.DateConsultation,
					NotParseRecord = model.NotParseRecord,
					SeasonDatesId = seasonDate.Id
				};
            }
            CreateScheduleRecord(model, entity);

            return entity;
		}
		#endregion

		#region Administration
		public static Role CreateRole(RoleSetBindingModel model, Role entity = null)
		{
			if (entity == null)
			{
				entity = new Role();
			}
			entity.RoleName = model.RoleName;

			return entity;
		}

		public static Access CreateAccess(AccessSetBindingModel model, Access entity = null)
		{
			if (entity == null)
			{
				entity = new Access();
			}
			entity.RoleId = model.RoleId;
			entity.Operation = (AccessOperation)Enum.Parse(typeof(AccessOperation), model.Operation);
			entity.AccessType = (AccessType)Enum.Parse(typeof(AccessType), model.AccessType);

			return entity;
		}

		public static User CreateUser(UserSetBindingModel model, User entity = null)
		{
			if (entity == null)
			{
				entity = new User
				{
					Password = AccessCheckService.GetPasswordHash(model.Password)
				};
			}
			entity.Login = model.Login;
			entity.Avatar = model.Avatar;
			entity.LecturerId = model.LecturerId;
			entity.StudentId = model.StudentId;
            if (entity.IsBanned != model.IsBanned)
			{
				entity.IsBanned = model.IsBanned;
				if (model.IsBanned)
				{
					entity.DateBanned = DateTime.Now;
				}
			}
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
            entity.DisciplineId = model.DisciplineId;
            entity.LessonType = (DisciplineLessonTypes)Enum.Parse(typeof(DisciplineLessonTypes), model.LessonType);
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

            return entity;
        }
        #endregion
    }
}
