using AcademicYearControlsAndForms.AcademicPlan;
using AcademicYearControlsAndForms.AcademicPlanRecord;
using AcademicYearControlsAndForms.AcademicPlanRecordElement;
using AcademicYearControlsAndForms.AcademicPlanRecordMission;
using AcademicYearControlsAndForms.AcademicYear;
using AcademicYearControlsAndForms.Contingent;
using AcademicYearControlsAndForms.SeasonDates;
using AcademicYearControlsAndForms.Services.LoadDistribution;
using AcademicYearControlsAndForms.StreamLesson;
using AcademicYearControlsAndForms.StreamLessonRecord;
using AcademicYearControlsAndForms.TimeNorm;
using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace AcademicYearControlsAndForms
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

            currentContainer.RegisterType<IAcademicYearProcess, AcademicYearProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlAcademicYear GetControlAcademicYear { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAcademicYear>(); } }

        public static ControlAcademicPlan GetControlAcademicPlan { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAcademicPlan>(); } }

        public static ControlAcademicPlanRecord GetControlAcademicPlanRecord { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAcademicPlanRecord>(); } }

        public static ControlAcademicPlanRecordElement GetControlAcademicPlanRecordElement { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAcademicPlanRecordElement>(); } }

        public static ControlAcademicPlanRecordMission GetControlAcademicPlanRecordMission { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAcademicPlanRecordMission>(); } }

        public static ControlContingent GetControlContingent { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlContingent>(); } }

        public static ControlSeasonDates GetControlSeasonDates { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlSeasonDates>(); } }

        public static ControlStreamLesson GetControlStreamLesson { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStreamLesson>(); } }

        public static ControlStreamLessonRecord GetControlStreamLessonRecord { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStreamLessonRecord>(); } }

        public static ControlTimeNorm GetControlTimeNorm { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlTimeNorm>(); } }

        public static ControlLoadDistribution GetControlLoadDistribution { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlLoadDistribution>(); } }
    }
}