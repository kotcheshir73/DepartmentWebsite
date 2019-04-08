using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Services;
using BaseInterfaces.Interfaces;
using DepartmentService.Services;
using ScheduleControlsAndForms.Consultation;
using ScheduleControlsAndForms.Current;
using ScheduleControlsAndForms.Examination;
using ScheduleControlsAndForms.Offset;
using ScheduleControlsAndForms.ScheduleLessonTime;
using ScheduleControlsAndForms.Semester;
using ScheduleControlsAndForms.StreamingLesson;
using ScheduleImplementations.Services;
using ScheduleInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace ScheduleControlsAndForms
{
    public static class Controller
    {
        private static IUnityContainer _container;

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeasonDatesService, SeasonDatesService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IConsultationRecordService, ConsultationRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationRecordService, ExaminationRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOffsetRecordService, OffsetRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISemesterRecordService, SemesterRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IScheduleLessonTimeService, ScheduleLessonTimeService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamingLessonService, StreamingLessonService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IScheduleProcess, ScheduleProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlScheduleLessonTime GetControlScheduleLessonTime { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlScheduleLessonTime>(); } }

        public static ControlStreamingLesson GetControlStreamingLesson { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStreamingLesson>(); } }

        public static ControlScheduleConfig GetControlScheduleConfig { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlScheduleConfig>(); } }

        public static ControlCurrentClassroom GetControlCurrentClassroom { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlCurrentClassroom>(); } }

        public static ControlCurrentLecturer GetControlCurrentLecturer { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlCurrentLecturer>(); } }

        public static ControlCurrentStudentGroup GetControlCurrentStudentGroup { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlCurrentStudentGroup>(); } }

        public static ScheduleConsultationTabControl GetScheduleConsultationTabControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ScheduleConsultationTabControl>(); } }

        public static ScheduleExaminationTabControl GetScheduleExaminationTabControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ScheduleExaminationTabControl>(); } }

        public static ScheduleOffsetTabControl GetScheduleOffsetTabControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ScheduleOffsetTabControl>(); } }

        public static ScheduleSemesterTabControl GetScheduleSemesterTabControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ScheduleSemesterTabControl>(); } }
    }
}
