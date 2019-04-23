using AuthenticationControlsAndForms.Access;
using AuthenticationControlsAndForms.Role;
using AuthenticationControlsAndForms.Services.DataBaseWork;
using AuthenticationControlsAndForms.Services.Synchronization;
using AuthenticationControlsAndForms.User;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace AuthenticationControlsAndForms
{
    public static class Controller
    {
        private static IUnityContainer _container;

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAccessService, AccessService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IAuthenticationProcess, AuthenticationProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlAccess GetControlAccess { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAccess>(); } }

        public static ControlRole GetControlRole { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlRole>(); } }

        public static ControlUser GetControlUser { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlUser>(); } }

        public static ExportDataBaseControl GetExportDataBaseControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ExportDataBaseControl>(); } }

        public static ImportDataBaseControl GetImportDataBaseControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ImportDataBaseControl>(); } }

        public static SynchronizationRolesControl GetSynchronizationRolesControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<SynchronizationRolesControl>(); } }

        public static SynchronizationUsersControl GetSynchronizationUsersControl { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<SynchronizationUsersControl>(); } }
    }
}