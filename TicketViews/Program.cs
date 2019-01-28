using DepartmentContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows.Forms;
using TicketServiceImplementations.Implementations;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Views.ExaminationTemplate;
using Unity;
using Unity.Lifetime;

namespace TicketViews
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ExaminationTemplateTicketSearchForm());
        }

        public static void PrintErrorMessage(string text, List<KeyValuePair<string, string>> result)
        {
            if(result.Count == 1)
            {
                MessageBox.Show(result[0].Value, result[0].Key, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FormError form = new FormError();
                form.LoadData(text, result);
            }
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, DepartmentDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateService, ExaminationTemplateService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateBlockService, ExaminationTemplateBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateBlockQuestionService, ExaminationTemplateBlockQuestionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateTicketService, ExaminationTemplateTicketService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationTemplateTicketQuestionService, ExaminationTemplateTicketQuestionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketTemplateService, TicketTemplateService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketProcess, TicketProcess>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ExaminationTemplateControl>().RegisterInstance<IUnityContainer>(currentContainer);
            return currentContainer;
        }
    }
}