using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using DepartmentService.Services;
using LaboratoryHeadControlsAndForms.MaterialTechnicalValue;
using LaboratoryHeadControlsAndForms.MaterialTechnicalValueGroup;
using LaboratoryHeadControlsAndForms.MaterialTechnicalValueRecord;
using LaboratoryHeadControlsAndForms.Software;
using LaboratoryHeadControlsAndForms.SoftwareRecord;
using LaboratoryHeadInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace LaboratoryHeadControlsAndForms
{
    public static class Controller
    {
        private static IUnityContainer _container;

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMaterialTechnicalValueGroupService, MaterialTechnicalValueGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialTechnicalValueRecordService, MaterialTechnicalValueRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialTechnicalValueService, MaterialTechnicalValueService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISoftwareRecordService, SoftwareRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISoftwareService, SoftwareService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ILaboratoryProcess, LaboratoryProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlMaterialTechnicalValue GetControlMaterialTechnicalValue { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlMaterialTechnicalValue>(); } }

        public static ControlMaterialTechnicalValueGroup GetControlMaterialTechnicalValueGroup { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlMaterialTechnicalValueGroup>(); } }

        public static ControlMaterialTechnicalValueRecord GetControlMaterialTechnicalValueRecord { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlMaterialTechnicalValueRecord>(); } }

        public static ControlSoftware GetControlSoftware { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlSoftware>(); } }

        public static ControlSoftwareRecord GetControlSoftwareRecord { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlSoftwareRecord>(); } }
    }
}