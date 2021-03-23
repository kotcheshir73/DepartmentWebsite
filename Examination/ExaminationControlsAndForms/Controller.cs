using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using ExaminationControlsAndForms.ExaminationTemplate;
using ExaminationControlsAndForms.ExaminationTemplateBlock;
using ExaminationControlsAndForms.ExaminationTemplateBlockQuestion;
using ExaminationControlsAndForms.ExaminationTemplateTicket;
using ExaminationControlsAndForms.ExaminationTemplateTicketQuestion;
using ExaminationControlsAndForms.TicketTemplate;
using ExaminationImplementations.Implementations;
using ExaminationInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;

namespace ExaminationControlsAndForms
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
            currentContainer.RegisterType<ILecturerStudyPostSerivce, LecturerStudyPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IExaminationListService, ExaminationListService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStatementService, StatementService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStatementRecordService, StatementRecordService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IExaminationTemplateService, ExaminationTemplateService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateBlockService, ExaminationTemplateBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateBlockQuestionService, ExaminationTemplateBlockQuestionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateTicketService, ExaminationTemplateTicketService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateTicketQuestionService, ExaminationTemplateTicketQuestionService>(new HierarchicalLifetimeManager());
            
            currentContainer.RegisterType<ITicketTemplateService, TicketTemplateService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateParagraphRunService, TicketTemplateParagraphRunService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateParagraphService, TicketTemplateParagraphService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateTableCellService, TicketTemplateTableCellService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateTableRowService, TicketTemplateTableRowService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateTableService, TicketTemplateTableService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateBodyService, TicketTemplateBodyService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IExaminationProcess, ExaminationProcess>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketProcess, TicketProcess>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

        public static ControlExaminationTemplate GetControlExaminationTemplate { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlExaminationTemplate>(); } }

        public static ControlExaminationTemplateBlock GetControlExaminationTemplateBlock { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlExaminationTemplateBlock>(); } }

        public static ControlExaminationTemplateBlockQuestion GetControlExaminationTemplateBlockQuestion { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlExaminationTemplateBlockQuestion>(); } }

        public static ControlExaminationTemplateTicket GetControlExaminationTemplateTicket { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlExaminationTemplateTicket>(); } }

        public static ControlExaminationTemplateTicketQuestion GetControlExaminationTemplateTicketQuestion { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlExaminationTemplateTicketQuestion>(); } }

        public static ControlTicketTemplate GetControlTicketTemplate { get { if (_container == null) _container = BuildUnityContainer(); return _container.Resolve<ControlTicketTemplate>(); } }
    }
}
