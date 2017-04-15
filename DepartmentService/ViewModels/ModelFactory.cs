using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DepartmentService.ViewModels
{
	public static class ModelFactory
	{
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
			return entities.Select(e => CreateEducationDirectionViewModel(e)).OrderBy(e => e.Cipher);
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
				ParentTimeNormId = entity.ParentTimeNormId,
				ParentTimeNormTitle = entity.ParentTimeNormId.HasValue ? entity.ParentTimeNorm.Title : string.Empty,
				Hours = entity.Hours
			};
		}

		public static IEnumerable<TimeNormViewModel> CreateTimeNorms(IEnumerable<TimeNorm> entities)
		{
			return entities.Select(e => CreateTimeNormViewModel(e));
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

		public static StudentGroupViewModel CreateStudentGroupViewModel(StudentGroup entity)
		{
			return new StudentGroupViewModel
			{
				Id = entity.Id,
				EducationDirectionId = entity.EducationDirectionId,
				EducationDirectionCipher = entity.EducationDirection.Cipher,
				GroupName = entity.GroupName,
				Kurs = entity.Kurs,
				CountStudents = (entity.Students != null) ? entity.Students.Where(s => !s.IsDeleted).Count() : 0,
				StewardId = entity.StewardId,
				Steward = string.IsNullOrEmpty(entity.StewardId) ? string.Empty :
							string.Format("{0} {1} {2}", entity.Steward.LastName, entity.Steward.FirstName, entity.Steward.Patronymic)
			};
		}

		public static IEnumerable<StudentGroupViewModel> CreateStudentGroups(IEnumerable<StudentGroup> entities)
		{
			return entities.Select(e => CreateStudentGroupViewModel(e)).OrderBy(e => e.Kurs).ThenBy(e => e.EducationDirectionId);
		}

		public static ContingentViewModel CreateContingentViewModel(Contingent entity)
		{
			return new ContingentViewModel
			{
				Id = entity.Id,
				AcademicYearId = entity.AcademicYearId,
				StudentGroupId = entity.StudentGroupId,
				AcademicYear = entity.AcademicYear.Title,
				StudentGroupName = entity.StudentGroup.GroupName,
				Course = entity.StudentGroup.Kurs,
				CountStudents = entity.CountStudetns,
				CountSubgroups = entity.CountSubgroups
			};
		}

		public static IEnumerable<ContingentViewModel> CreateContingents(IEnumerable<Contingent> entities)
		{
			return entities.Select(e => CreateContingentViewModel(e)).OrderBy(e => e.AcademicYearId).ThenBy(e => e.StudentGroupId);
		}

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
			return entities.Select(e => CreateLoadDistributionViewModel(e)).OrderBy(e => e.AcademicYearId);
		}

		public static LoadDistributionRecordViewModel CreateLoadDistributionRecordViewModel(LoadDistributionRecord entity)
		{
			return new LoadDistributionRecordViewModel
			{
				Id = entity.Id,
				LoadDistributionId = entity.LoadDistributionId,
				LoadDistributionAcademicYear = entity.LoadDistribution.AcademicYear.Title,
				AcademicPlanRecordId = entity.AcademicPlanRecordId,
				AcademicPlanRecordViewModel = CreateAcademicPlanRecordViewModel(entity.AcademicPlanRecord),
				ContingentId = entity.ContingentId,
				ContingentViewModel = CreateContingentViewModel(entity.Contingent),
				TimeNormId = entity.TimeNormId,
				TimeNormViewModel = CreateTimeNormViewModel(entity.TimeNorm)
			};
		}

		public static IEnumerable<LoadDistributionRecordViewModel> CreateLoadDistributionRecords(IEnumerable<LoadDistributionRecord> entities)
		{
			return entities.Select(e => CreateLoadDistributionRecordViewModel(e)).OrderBy(e => e.AcademicPlanRecordViewModel.Semester).ThenBy(e => e.ContingentViewModel.StudentGroupName)
				.ThenBy(e => e.AcademicPlanRecordViewModel.Disciplne).ThenBy(e => e.TimeNormId);
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


		public static ClassroomViewModel CreateClassroomViewModel(Classroom entity)
		{
			return new ClassroomViewModel
			{
				Id = entity.Id,
				ClassroomType = entity.ClassroomType.ToString(),
				Capacity = entity.Capacity
			};
		}

		public static IEnumerable<ClassroomViewModel> CreateClassrooms(IEnumerable<Classroom> entities)
		{
			return entities.Select(e => CreateClassroomViewModel(e)).OrderBy(e => e.Id);
		}


		public static DisciplineViewModel CreateDisciplineViewModel(Discipline entity)
		{
			return new DisciplineViewModel
			{
				Id = entity.Id,
				DisciplineName = entity.DisciplineName
			};
		}

		public static IEnumerable<DisciplineViewModel> CreateDisciplines(IEnumerable<Discipline> entities)
		{
			return entities.Select(e => CreateDisciplineViewModel(e)).OrderBy(e => e.Id);
		}


		public static LecturerViewModel CreateLecturerViewModel(Lecturer entity)
		{
			return new LecturerViewModel
			{
				Id = entity.Id,
				LastName = entity.LastName,
				FirstName = entity.FirstName,
				Patronymic = entity.Patronymic,
				Abbreviation = entity.Abbreviation,
				DateBirth = entity.DateBirth,
				Post = entity.Post,
				Rank = entity.Rank,
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
			return entities.Select(e => CreateLecturerViewModel(e)).OrderBy(e => e.Id);
		}


		public static StudentViewModel CreateStudentViewModel(Student entity)
		{
			return new StudentViewModel
			{
				NumberOfBook = entity.NumberOfBook,
				LastName = entity.LastName,
				FirstName = entity.FirstName,
				Patronymic = entity.Patronymic,
				Email = entity.Email,
				Description = entity.Description,
				Photo = entity.Photo != null ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
				StudentGroupId = entity.StudentGroupId,
				StudentGroup = (entity.StudentGroup != null) ? entity.StudentGroup.GroupName : string.Empty
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
				NumberOfBook = entity.StudentId,
				DateCreate = entity.DateCreate.ToLongDateString(),
				TextMessage = entity.TextMessage
			};
		}

		public static IEnumerable<StudentHistoryViewModel> CreateStudentHistorys(IEnumerable<StudentHistory> entities)
		{
			return entities.Select(e => CreateStudentHistoryViewModel(e)).OrderBy(e => e.DateCreate);
		}


		public static SeasonDatesViewModel CreateSeasonDatesViewModel(SeasonDates entity)
		{
			return new SeasonDatesViewModel
			{
				Id = entity.Id,
				DateBeginExamination = entity.DateBeginExamination.ToLongDateString(),
				DateBeginOffset = entity.DateBeginOffset.ToLongDateString(),
				DateBeginSemester = entity.DateBeginSemester.ToLongDateString(),
				DateEndExamination = entity.DateEndExamination.ToLongDateString(),
				DateEndOffset = entity.DateEndOffset.ToLongDateString(),
				DateEndSemester = entity.DateEndSemester.ToLongDateString(),
				DateBeginPractice = entity.DateBeginPractice.HasValue ? entity.DateBeginPractice.Value.ToLongDateString() : "",
				DateEndPractice = entity.DateEndPractice.HasValue ? entity.DateEndPractice.Value.ToLongDateString() : "",
				Title = entity.Title
			};
		}

		public static IEnumerable<SeasonDatesViewModel> CreateSeasonDaties(IEnumerable<SeasonDates> entities)
		{
			return entities.Select(e => CreateSeasonDatesViewModel(e)).OrderBy(e => e.Id);
		}

		#region Schedule
		public static SemesterRecordViewModel CreateSemesterRecordViewModel(SemesterRecord entity)
		{
			return new SemesterRecordViewModel
			{
				Id = entity.Id,
				Day = entity.Day,
				Week = entity.Week,
				Lesson = entity.Lesson,
				IsStreaming = entity.IsStreaming,
				LessonClassroom = entity.LessonClassroom,
				LessonGroup = entity.LessonGroup,
				LessonDiscipline = entity.LessonDiscipline,
				LessonLecturer = entity.LessonLecturer,
				LessonType = entity.LessonType.ToString(),
				ClassroomId = entity.ClassroomId,
				Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
				LecturerId = entity.LecturerId,
				Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
				StudentGroupId = entity.StudentGroupId,
				StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
			};
		}

		public static IEnumerable<SemesterRecordViewModel> CreateSemesterRecords(IEnumerable<SemesterRecord> entities)
		{
			return entities.Select(e => CreateSemesterRecordViewModel(e)).OrderBy(e => e.Id);
		}

		public static OffsetRecordViewModel CreateOffsetRecordViewModel(OffsetRecord entity)
		{
			return new OffsetRecordViewModel
			{
				Id = entity.Id,
				Day = entity.Day,
				Week = entity.Week,
				Lesson = entity.Lesson,
				LessonClassroom = entity.LessonClassroom,
				LessonGroup = entity.LessonGroup,
				LessonDiscipline = entity.LessonDiscipline,
				LessonLecturer = entity.LessonLecturer,
				ClassroomId = entity.ClassroomId,
				Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
				LecturerId = entity.LecturerId,
				Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
				StudentGroupId = entity.StudentGroupId,
				StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
			};
		}

		public static IEnumerable<OffsetRecordViewModel> CreateOffsetRecords(IEnumerable<OffsetRecord> entities)
		{
			return entities.Select(e => CreateOffsetRecordViewModel(e)).OrderBy(e => e.Id);
		}

		public static ExaminationRecordViewModel CreateExaminationRecordViewModel(ExaminationRecord entity)
		{
			return new ExaminationRecordViewModel
			{
				Id = entity.Id,
				DateConsultation = entity.DateConsultation,
				DateExamination = entity.DateExamination,
				LessonClassroom = entity.LessonClassroom,
				LessonGroup = entity.LessonGroup,
				LessonDiscipline = entity.LessonDiscipline,
				LessonLecturer = entity.LessonLecturer,
				ClassroomId = entity.ClassroomId,
				Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
				LecturerId = entity.LecturerId,
				Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
				StudentGroupId = entity.StudentGroupId,
				StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
			};
		}

		public static IEnumerable<ExaminationRecordViewModel> CreateExaminationRecords(IEnumerable<ExaminationRecord> entities)
		{
			return entities.Select(e => CreateExaminationRecordViewModel(e)).OrderBy(e => e.Id);
		}

		public static ConsultationRecordViewModel CreateConsultationRecordViewModel(ConsultationRecord entity)
		{
			return new ConsultationRecordViewModel
			{
				Id = entity.Id,
				DateConsultation = entity.DateConsultation,
				LessonClassroom = entity.LessonClassroom,
				LessonGroup = entity.LessonGroup,
				LessonDiscipline = entity.LessonDiscipline,
				LessonLecturer = entity.LessonLecturer,
				ClassroomId = entity.ClassroomId,
				Classroom = entity.Classroom != null ? entity.Classroom.Id : "",
				LecturerId = entity.LecturerId,
				Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
				StudentGroupId = entity.StudentGroupId,
				StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
			};
		}

		public static IEnumerable<ConsultationRecordViewModel> CreateConsultationRecords(IEnumerable<ConsultationRecord> entities)
		{
			return entities.Select(e => CreateConsultationRecordViewModel(e)).OrderBy(e => e.Id);
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
			return entities.Select(e => CreateStreamingLessonViewModel(e)).OrderBy(e => e.Id);
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
				TimeBeginLesson = entity.DateBeginLesson.ToShortTimeString(),
				TimeEndLesson = entity.DateEndLesson.ToShortTimeString(),
				DateBeginLesson = entity.DateBeginLesson,
				DateEndLesson = entity.DateEndLesson
			};
		}

		public static IEnumerable<ScheduleLessonTimeViewModel> CreateScheduleLessonTimes(IEnumerable<ScheduleLessonTime> entities)
		{
			return entities.Select(e => CreateScheduleLessonTimeViewModel(e)).OrderBy(e => e.Id);
		}

		public static ScheduleStopWordViewModel CreateScheduleStopWordViewModel(ScheduleStopWord entity)
		{
			return new ScheduleStopWordViewModel
			{
				Id = entity.Id,
				StopWord = entity.StopWord,
				StopWordType = entity.StopWordType.ToString()
			};
		}

		public static IEnumerable<ScheduleStopWordViewModel> CreateScheduleStopWords(IEnumerable<ScheduleStopWord> entities)
		{
			return entities.Select(e => CreateScheduleStopWordViewModel(e)).OrderBy(e => e.StopWordType);
		}
		#endregion
	}
}
