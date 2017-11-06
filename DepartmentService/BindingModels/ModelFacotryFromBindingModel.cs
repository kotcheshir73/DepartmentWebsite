using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using System;

namespace DepartmentService.BindingModels
{
	public static class ModelFacotryFromBindingModel
	{
		public static EducationDirection CreateEducationDirection(EducationDirectionRecordBindingModel model, EducationDirection entity = null)
		{
			if (entity == null)
			{
				entity = new EducationDirection
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.Cipher = model.Cipher;
			entity.Description = model.Description;
			entity.Title = model.Title;

			return entity;
		}

		public static KindOfLoad CreateKindOfLoad(KindOfLoadRecordBindingModel model, KindOfLoad entity = null)
		{
			if (entity == null)
			{
				entity = new KindOfLoad
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.KindOfLoadName = model.KindOfLoadName;
			entity.KindOfLoadType = (KindOfLoadType)Enum.Parse(typeof(KindOfLoadType), model.KindOfLoadType);

			return entity;
		}

		public static TimeNorm CreateTimeNorm(TimeNormRecordBindingModel model, TimeNorm entity = null)
		{
			if (entity == null)
			{
				entity = new TimeNorm
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.Title = model.Title;
			entity.KindOfLoadId = model.KindOfLoadId;
			entity.Formula = model.Formula;
			entity.Hours = model.Hours;

			return entity;
		}

		public static AcademicPlan CreateAcademicPlan(AcademicPlanRecordBindingModel model, AcademicPlan entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicPlan
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
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
				entity = new AcademicPlanRecord
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.DisciplineId = model.DisciplineId;
			entity.KindOfLoadId = model.KindOfLoadId;
			entity.Semester = (Semesters)Enum.ToObject(typeof(Semesters), model.Semester);
			entity.Hours = model.Hours;

			return entity;
		}

		public static AcademicYear CreateAcademicYear(AcademicYearRecordBindingModel model, AcademicYear entity = null)
		{
			if (entity == null)
			{
				entity = new AcademicYear
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.Title = model.Title;

			return entity;
		}

		public static StudentGroup CreateStudentGroup(StudentGroupRecordBindingModel model, StudentGroup entity = null)
		{
			if (entity == null)
			{
				entity = new StudentGroup
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.EducationDirectionId = model.EducationDirectionId;
			entity.GroupName = model.GroupName;
			entity.Course = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.Course);
			if (!string.IsNullOrEmpty(model.StewardId))
			{
				entity.StewardId = model.StewardId;
			}

			return entity;
		}

		public static Contingent CreateContingent(ContingentRecordBindingModel model, Contingent entity = null)
		{
			if (entity == null)
			{
				entity = new Contingent
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.AcademicYearId = model.AcademicYearId;
			entity.EducationDirectionId = model.EducationDirectionId;
			entity.Course = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.Course);
			entity.CountStudetns = model.CountStudents;
			entity.CountSubgroups = model.CountSubgroups;

			return entity;
		}

		public static LoadDistribution CreateLoadDistribution(LoadDistributionRecordBindingModel model, LoadDistribution entity = null)
		{
			if (entity == null)
			{
				entity = new LoadDistribution
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.AcademicYearId = model.AcademicYearId;

			return entity;
		}

		public static LoadDistributionRecord CreateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model, LoadDistributionRecord entity = null)
		{
			if (entity == null)
			{
				entity = new LoadDistributionRecord
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
			entity.ContingentId = model.ContingentId;
			entity.TimeNormId = model.TimeNormId;
			entity.Load = model.Load;

			return entity;
		}

		public static LoadDistributionMission CreateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model, LoadDistributionMission entity = null)
		{
			if (entity == null)
			{
				entity = new LoadDistributionMission
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.LecturerId = model.LecturerId;
			entity.Hours = model.Hours;

			return entity;
		}

		public static Classroom CreateClassroom(ClassroomRecordBindingModel model, Classroom entity = null)
		{
			if (entity == null)
			{
				entity = new Classroom
				{
					IsDeleted = false
				};
			}
			entity.Capacity = model.Capacity;
			entity.ClassroomType = (ClassroomTypes)Enum.Parse(typeof(ClassroomTypes), model.ClassroomType);

			return entity;
		}

		public static DisciplineBlock CreateDisciplineBlock(DisciplineBlockRecordBindingModel model, DisciplineBlock entity = null)
		{
			if (entity == null)
			{
				entity = new DisciplineBlock
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.Title = model.Title;

			return entity;
		}

		public static Discipline CreateDiscipline(DisciplineRecordBindingModel model, Discipline entity = null)
		{
			if (entity == null)
			{
				entity = new Discipline
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.DisciplineBlockId = model.DisciplineBlockId;
			entity.DisciplineName = model.DisciplineName;

			return entity;
		}

		public static Lecturer CreateLecturer(LecturerRecordBindingModel model, Lecturer entity = null)
		{
			if (entity == null)
			{
				entity = new Lecturer
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.FirstName = model.FirstName;
			entity.LastName = model.LastName;
			entity.Patronymic = model.Patronymic;
			entity.Abbreviation = model.Abbreviation;
			entity.DateBirth = model.DateBirth;
			entity.Post = model.Post;
			entity.Rank = model.Rank;
			entity.Address = model.Address;
			entity.HomeNumber = model.HomeNumber;
			entity.MobileNumber = model.MobileNumber;
			entity.Email = model.Email;
			entity.Description = model.Description;
			entity.Photo = model.Photo;

			return entity;
		}

		public static Student CreateStudent(StudentRecordBindingModel model, Student entity = null)
		{
			if (entity == null)
			{
				entity = new Student
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.LastName = model.LastName;
			entity.FirstName = model.FirstName;
			entity.Patronymic = model.Patronymic;
			entity.Email = model.Email;
			entity.Description = model.Description;
			entity.Photo = model.Photo;

			return entity;
		}

		public static StudentHistory CreateStudentHistory(StudentHistoryRecordBindingModel model, StudentHistory entity = null)
		{
			if (entity == null)
			{
				entity = new StudentHistory
				{
					StudentId = model.NumberOfBook,
					DateCreate = DateTime.Now
				};
			}
			entity.DateCreate = model.DateCreate;
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
			entity.DateBeginExamination = model.DateBeginExamination;
			entity.DateBeginOffset = model.DateBeginOffset;
			entity.DateBeginPractice = model.DateBeginPractice;
			entity.DateBeginSemester = model.DateBeginSemester;
			entity.DateEndExamination = model.DateEndExamination;
			entity.DateEndOffset = model.DateEndOffset;
			entity.DateEndPractice = model.DateEndPractice;
			entity.DateEndSemester = model.DateEndSemester;

			return entity;
		}

		#region Schedule

		public static SemesterRecord CreateSemesterRecord(SemesterRecordRecordBindingModel model, SemesterRecord entity = null, SeasonDates seasonDate = null)
		{
			if (entity == null)
			{
				entity = new SemesterRecord()
				{
					Week = model.Week,
					Day = model.Day,
					Lesson = model.Lesson,
					NotParseRecord = model.NotParseRecord,
					SeasonDatesId = seasonDate.Id,
					IsStreaming = model.IsStreaming
				};
			}
			entity.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
			entity.LessonDiscipline = model.LessonDiscipline;
			entity.LessonGroup = model.LessonGroup;
			entity.LessonLecturer = model.LessonLecturer;
			entity.LessonClassroom = model.LessonClassroom;
			if (!string.IsNullOrEmpty(model.ClassroomId))
			{
				entity.ClassroomId = model.ClassroomId;
			}
			entity.LecturerId = model.LecturerId;
			entity.StudentGroupId = model.StudentGroupId;

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
			entity.LessonDiscipline = model.LessonDiscipline;
			entity.LessonGroup = model.LessonGroup;
			entity.LessonLecturer = model.LessonLecturer;
			entity.LessonClassroom = model.LessonClassroom;
			if (!string.IsNullOrEmpty(model.ClassroomId))
			{
				entity.ClassroomId = model.ClassroomId;
			}
			entity.LecturerId = model.LecturerId;
			entity.StudentGroupId = model.StudentGroupId;

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
					SeasonDatesId = seasonDate.Id
				};
			}
			entity.LessonDiscipline = model.LessonDiscipline;
			entity.LessonGroup = model.LessonGroup;
			entity.LessonLecturer = model.LessonLecturer;
			entity.LessonClassroom = model.LessonClassroom;
			if (!string.IsNullOrEmpty(model.ClassroomId))
			{
				entity.ClassroomId = model.ClassroomId;
			}
			entity.LecturerId = model.LecturerId;
			entity.StudentGroupId = model.StudentGroupId;

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
			entity.LessonDiscipline = model.LessonDiscipline;
			entity.LessonGroup = model.LessonGroup;
			entity.LessonLecturer = model.LessonLecturer;
			entity.LessonClassroom = model.LessonClassroom;
			if (!string.IsNullOrEmpty(model.ClassroomId))
			{
				entity.ClassroomId = model.ClassroomId;
			}
			entity.LecturerId = model.LecturerId;
			entity.StudentGroupId = model.StudentGroupId;

			return entity;
		}

		public static StreamingLesson CreateStreamingLesson(StreamingLessonRecordBindingModel model, StreamingLesson entity = null)
		{
			if (entity == null)
			{
				entity = new StreamingLesson
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
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
			entity.DateBeginLesson = model.DateBeginLesson;
			entity.DateEndLesson = model.DateEndLesson;

			return entity;
		}
		#endregion

		#region Administration
		public static Role CreateRole(RoleRecordBindingModel model, Role entity = null)
		{
			if (entity == null)
			{
				entity = new Role
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.RoleName = model.RoleName;

			return entity;
		}

		public static Access CreateAccess(AccessRecordBindingModel model, Access entity = null)
		{
			if (entity == null)
			{
				entity = new Access
				{
					DateCreate = DateTime.Now,
					IsDeleted = false
				};
			}
			entity.RoleId = model.RoleId;
			entity.Operation = model.Operation;
			entity.AccessType = (AccessType)Enum.ToObject(typeof(AccessType), model.AccessType);

			return entity;
		}
		#endregion
	}
}
