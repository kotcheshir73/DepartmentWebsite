using DepartmentModel.Enums;
using DepartmentModel.Models;
using System;

namespace DepartmentService.BindingModels
{
	public static class ModelFacotryFromBindingModel
	{
        #region EducationDirection
        public static EducationDirection CreateEducationDirection(EducationDirectionRecordBindingModel model, EducationDirection entity = null)
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

		public static DisciplineBlock CreateDisciplineBlock(DisciplineBlockRecordBindingModel model, DisciplineBlock entity = null)
		{
			if (entity == null)
			{
				entity = new DisciplineBlock();
			}
			entity.Title = model.Title;

			return entity;
		}

        public static LecturerPost CreateLecturerPost(LecturerPostRecordBindingModel model, LecturerPost entity = null)
        {
            if (entity == null)
            {
                entity = new LecturerPost();
            }
            entity.PostTitle = model.PostTitle;
            entity.Hours = model.Hours;

            return entity;
        }
        

        public static Classroom CreateClassroom(ClassroomRecordBindingModel model, Classroom entity = null)
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

		public static Discipline CreateDiscipline(DisciplineRecordBindingModel model, Discipline entity = null)
		{
			if (entity == null)
			{
				entity = new Discipline();
			}
			entity.DisciplineBlockId = model.DisciplineBlockId;
			entity.DisciplineName = model.DisciplineName;
            entity.DisciplineShortName = model.DisciplineShortName;

            return entity;
		}

		public static Lecturer CreateLecturer(LecturerRecordBindingModel model, Lecturer entity = null)
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

		public static StudentGroup CreateStudentGroup(StudentGroupRecordBindingModel model, StudentGroup entity = null)
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


		public static KindOfLoad CreateKindOfLoad(KindOfLoadRecordBindingModel model, KindOfLoad entity = null)
		{
			if (entity == null)
			{
				entity = new KindOfLoad();
			}
			entity.KindOfLoadName = model.KindOfLoadName;
            entity.AttributeName = model.AttributeName;

            return entity;
		}

		public static TimeNorm CreateTimeNorm(TimeNormRecordBindingModel model, TimeNorm entity = null)
		{
			if (entity == null)
			{
				entity = new TimeNorm();
			}
			entity.Title = model.Title;
			entity.KindOfLoadId = model.KindOfLoadId;
            entity.AcademicYearId = model.AcademicYearId;
			entity.Hours = model.Hours;
            entity.NumKoef = model.NumKoef;
            entity.TimeNormKoef = (TimeNormKoef)Enum.Parse(typeof(TimeNormKoef), model.TimeNormKoef);
            entity.KindOfLoadType = (KindOfLoadType)Enum.Parse(typeof(KindOfLoadType), model.KindOfLoadType);

            return entity;
		}

		public static Contingent CreateContingent(ContingentRecordBindingModel model, Contingent entity = null)
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


		public static AcademicPlan CreateAcademicPlan(AcademicPlanRecordBindingModel model, AcademicPlan entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicPlan();
			}
			entity.EducationDirectionId = model.EducationDirectionId;
			entity.AcademicYearId = model.AcademicYearId;
			entity.AcademicLevel = (AcademicLevel)Enum.Parse(typeof(AcademicLevel), model.AcademicLevel);
			entity.AcademicCourses = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.AcademicCourses);

			return entity;
		}

		public static AcademicPlanRecord CreateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model, AcademicPlanRecord entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicPlanRecord();
			}
            entity.AcademicPlanId = model.AcademicPlanId;
			entity.DisciplineId = model.DisciplineId;
            entity.ContingentId = model.ContingentId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Zet = model.Zet;

            return entity;
		}

        public static AcademicPlanRecordElement CreateAcademicPlanRecordElement(AcademicPlanRecordElementRecordBindingModel model, AcademicPlanRecordElement entity = null)
        {
            if (entity == null)
            {
                entity = new AcademicPlanRecordElement();
            }
            entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
            entity.KindOfLoadId = model.KindOfLoadId;
            entity.Hours = model.Hours;
            return entity;
        }

        public static AcademicYear CreateAcademicYear(AcademicYearRecordBindingModel model, AcademicYear entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicYear();
			}
			entity.Title = model.Title;

			return entity;
		}

        public static StreamLesson CreateStreamLesson(StreamLessonRecordBindingModel model, StreamLesson entity = null)
        {
            if (entity == null)
            {
                entity = new StreamLesson();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.StreamLessonName = model.StreamLessonName;

            return entity;
        }

        public static StreamLessonRecord CreateStreamLessonRecord(StreamLessonRecordRecordBindingModel model, StreamLessonRecord entity = null)
        {
            if (entity == null)
            {
                entity = new StreamLessonRecord();
            }
            entity.StreamLessonId = model.StreamLessonId;
            entity.AcademicPlanRecordElementId = model.AcademicPlanRecordElementId;
            entity.Hours = model.Hours;
            entity.IsMain = model.IsMain;

            return entity;
        }



        public static Student CreateStudent(StudentRecordBindingModel model, Student entity = null)
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

		public static StudentHistory CreateStudentHistory(StudentHistoryRecordBindingModel model, StudentHistory entity = null)
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


		public static SeasonDates CreateSeasonDates(SeasonDatesRecordBindingModel model, SeasonDates entity = null)
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

		public static StreamingLesson CreateStreamingLesson(StreamingLessonRecordBindingModel model, StreamingLesson entity = null)
		{
			if (entity == null)
			{
				entity = new StreamingLesson();
			}
			entity.IncomingGroups = model.IncomingGroups;
			entity.StreamName = model.StreamName;

			return entity;
		}

		public static ScheduleLessonTime CreateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model, ScheduleLessonTime entity = null)
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

        #region LoadDistribution
        public static LoadDistribution CreateLoadDistribution(LoadDistributionRecordBindingModel model, LoadDistribution entity = null)
		{
			if (entity == null)
			{
				entity = new LoadDistribution();
			}
			entity.AcademicYearId = model.AcademicYearId;

			return entity;
		}

		public static LoadDistributionRecord CreateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model, LoadDistributionRecord entity = null)
		{
			if (entity == null)
			{
				entity = new LoadDistributionRecord();
			}
			entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
			entity.TimeNormId = model.TimeNormId;
			entity.Load = model.Load;

			return entity;
		}

		public static LoadDistributionMission CreateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model, LoadDistributionMission entity = null)
		{
			if (entity == null)
			{
				entity = new LoadDistributionMission();
			}
			entity.LecturerId = model.LecturerId;
			entity.Hours = model.Hours;

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
		public static Role CreateRole(RoleRecordBindingModel model, Role entity = null)
		{
			if (entity == null)
			{
				entity = new Role();
			}
			entity.RoleName = model.RoleName;

			return entity;
		}

		public static Access CreateAccess(AccessRecordBindingModel model, Access entity = null)
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

		public static User CreateUser(UserRecordBindingModel model, User entity = null)
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
        public static MaterialTechnicalValue CreateMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model, MaterialTechnicalValue entity = null)
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

        public static MaterialTechnicalValueGroup CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model, MaterialTechnicalValueGroup entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValueGroup();
            }
            entity.GroupName = model.GroupName;
            entity.Order = model.Order;

            return entity;
        }

        public static MaterialTechnicalValueRecord CreateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordRecordBindingModel model, MaterialTechnicalValueRecord entity = null)
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

        public static SoftwareRecord CreateSoftwareRecord(SoftwareRecordRecordBindingModel model, SoftwareRecord entity = null)
        {
            if (entity == null)
            {
                entity = new SoftwareRecord();
            }
            entity.DateCreate = model.DateSetup;
            entity.MaterialTechnicalValueId = model.MaterialTechnicalValueId;
            entity.SoftwareName = model.SoftwareName;
            entity.SoftwareDescription = model.SoftwareDescription;
            entity.SoftwareKey = model.SoftwareKey;
            entity.SoftwareK = model.SoftwareK;
            entity.ClaimNumber = model.ClaimNumber;

            return entity;
        }
        #endregion
    }
}
