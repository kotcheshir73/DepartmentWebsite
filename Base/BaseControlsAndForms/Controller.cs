using BaseControlsAndForms.Classroom;
using BaseControlsAndForms.Discipline;
using BaseControlsAndForms.DisciplineBlock;
using BaseControlsAndForms.EducationDirection;
using BaseControlsAndForms.Lecturer;
using BaseControlsAndForms.LecturerStudyPost;
using BaseControlsAndForms.Student;
using BaseControlsAndForms.StudentGroup;
using BaseControlsAndForms.StudentOrder;
using BaseControlsAndForms.StudentOrderBlock;
using BaseControlsAndForms.StudentOrderBlockStudent;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace BaseControlsAndForms
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
            currentContainer.RegisterType<ILecturerStudyPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentOrderService, StudentOrderService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentOrderBlockService, StudentOrderBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentOrderBlockStudentService, StudentOrderBlockStudentService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IProcess, Process>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlClassroom GetControlClassroom { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlClassroom>(); } }

        public static ControlDiscipline GetControlDiscipline { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDiscipline>(); } }

        public static ControlDisciplineBlock GetControlDisciplineBlock { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlDisciplineBlock>(); } }

        public static ControlEducationDirection GetControlEducationDirection { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlEducationDirection>(); } }

        public static ControlLecturerPost GetControlLecturerPost { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlLecturerPost>(); } }

        public static ControlLecturer GetControlLecturer { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlLecturer>(); } }

        public static ControlStudent GetControlStudent { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStudent>(); } }

        public static ControlStudentGroup GetControlStudentGroup { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStudentGroup>(); } }

        public static ControlStudentOrder GetControlStudentOrder { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStudentOrder>(); } }

        public static ControlStudentOrderBlock GetControlStudentOrderBlock { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStudentOrderBlock>(); } }

        public static ControlStudentOrderBlockStudent GetControlStudentOrderBlockStudent { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlStudentOrderBlockStudent>(); } }
    }
}