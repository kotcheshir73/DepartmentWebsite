using DepartmentContext;
using DepartmentService.IServices;
using DepartmentService.Services;
using System.Data.Entity;
using TicketServiceImplementations.Implementations;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Views.ExaminationTemplate;
using Unity;
using Unity.Lifetime;

namespace TicketViews
{
    public static class PublicViews
    {
        private static IUnityContainer container;

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, DepartmentDbContext>(new HierarchicalLifetimeManager());
            
            currentContainer.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeasonDatesService, SeasonDatesService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IExaminationTemplateService, ExaminationTemplateService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateBlockService, ExaminationTemplateBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateBlockQuestionService, ExaminationTemplateBlockQuestionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateTicketService, ExaminationTemplateTicketService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateTicketQuestionService, ExaminationTemplateTicketQuestionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateService, TicketTemplateService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketProcess, TicketProcess>(new HierarchicalLifetimeManager());
            
            return currentContainer;
        }

        public static ExaminationTemplateControl GetExaminationTemplateControl()
        {
            if (container == null)
            {
                container = BuildUnityContainer();
            }
            return container.Resolve<ExaminationTemplateControl>();
        }
    }
}