using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using LearningProgressControlsAndForms.DisciplineLesson;
using LearningProgressControlsAndForms.DisciplineLessonConducted;
using LearningProgressControlsAndForms.DisciplineLessonConductedStudent;
using LearningProgressControlsAndForms.DisciplineLessonTask;
using LearningProgressControlsAndForms.DisciplineLessonTaskStudentAccept;
using LearningProgressControlsAndForms.DisciplineLessonTaskVariant;
using LearningProgressControlsAndForms.DisciplineStudentRecord;
using LearningProgressControlsAndForms.Services;
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace LearningProgressControlsAndForms
{
    public static class Controller
    {
        private static IUnityContainer _container;

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanService, AcademicPlanService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanRecordService, AcademicPlanRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanRecordElementService, AcademicPlanRecordElementService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanRecordMissionService, AcademicPlanRecordMissionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IContingentService, ContingentService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineTimeDistributionService, DisciplineTimeDistributionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineTimeDistributionRecordService, DisciplineTimeDistributionRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineTimeDistributionClassroomService, DisciplineTimeDistributionClassroomService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerWorkloadService, LecturerWorkloadService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeasonDatesService, SeasonDatesService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamLessonService, StreamLessonService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamLessonRecordService, StreamLessonRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITimeNormService, TimeNormService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IDisciplineLessonConductedService, DisciplineLessonConductedService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonConductedStudentService, DisciplineLessonConductedStudentService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonService, DisciplineLessonService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonTaskService, DisciplineLessonTaskService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonTaskVariantService, DisciplineLessonTaskVariantService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonTaskStudentAcceptService, DisciplineLessonTaskStudentAcceptService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineStudentRecordService, DisciplineStudentRecordService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ILearningProgressProcess, LearningProgressProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlDisciplineLesson GetControlDisciplineLesson { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineLesson>(); } }

        public static ControlDisciplineLessonTask GetControlDisciplineLessonTask { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineLessonTask>(); } }

        public static ControlDisciplineLessonTaskVariant GetControlDisciplineLessonTaskVariant { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineLessonTaskVariant>(); } }

        public static ControlDisciplineLessonConducted GetControlDisciplineLessonConducted { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineLessonConducted>(); } }

        public static ControlDisciplineLessonConductedStudent GetControlDisciplineLessonConductedStudent { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineLessonConductedStudent>(); } }

        public static ControlDisciplineLessonTaskStudentAccept GetControlDisciplineLessonTaskStudentAccept { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineLessonTaskStudentAccept>(); } }

        public static ControlDisciplineStudentRecord GetControlDisciplineStudentRecord { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineStudentRecord>(); } }

        public static ControlAcceptTasks GetControlAcceptTasks { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAcceptTasks>(); } }

        public static ControlConductedLessons GetControlConductedLessons { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlConductedLessons>(); } }

        public static ControlConfiguringDisciplines GetControlConfiguringDisciplines { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlConfiguringDisciplines>(); } }

        public static ControlStudentsDistribution GetControlStudentsDistribution { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStudentsDistribution>(); } }
    }
}