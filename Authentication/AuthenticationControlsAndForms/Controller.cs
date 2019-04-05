using AuthenticationControlsAndForms.Access;
using AuthenticationControlsAndForms.Role;
using AuthenticationControlsAndForms.User;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using Implementations.Services;
using Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            currentContainer.RegisterType<IAdministrationProcess, AdministrationProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlAccess GetControlAccess { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlAccess>(); } }

        public static ControlRole GetControlRole { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlRole>(); } }

        public static ControlUser GetControlUser { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlUser>(); } }
    }
}