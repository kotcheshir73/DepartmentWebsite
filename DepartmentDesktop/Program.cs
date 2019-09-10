using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            DepartmentUserManager.CheckExsistData();
            DepartmentUserManager.Login("admin", "qwerty");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer
        .RegisterType<FormMain>()
        .RegisterInstance<IUnityContainer>(currentContainer);
            return currentContainer;
        }

		public static void PrintErrorMessage(string text, List<KeyValuePair<string, string>> result)
		{
			FormError form = new FormError();
			form.LoadData(text, result);
		}
    }
}