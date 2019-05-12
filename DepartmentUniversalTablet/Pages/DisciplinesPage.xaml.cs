using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tools;
using Unity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.Pages
{
    /// <summary>
    /// Страница выбора из назначенных дисциплин.
    /// </summary>
    public sealed partial class DisciplinesPage : Page
    {
        private ILearningProgressProcess _serviceLP;

        public DisciplinesPage()
        {
            this.InitializeComponent();

            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var educationDirection = (EducationDirectionViewModel)e.Parameter;
                var list = _serviceLP.GetDisciplines(new LearningProgressInterfaces.BindingModels.LearningProcessDisciplineBindingModel()
                {
                    UserId = DepartmentUserManager.UserId.Value,
                    AcademicYearId = _serviceLP.GetCurrentAcademicYear().Result,
                    EducationDirectionId = educationDirection.Id
                }).Result;

                Button button1;

                foreach (var item in list)
                {
                    button1 = new Button();
                    button1.Content = item;
                    button1.Click += button_Click;
                    grid.Children.Add(button1);
                }
            }
                
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ImportManager importManager = new ImportManager();
            var discipline = importManager.extCollection.FirstOrDefault(x => x.Discipline == ((Button)sender).Content.ToString());
            Frame.Navigate(discipline != null ? discipline.GetUI : importManager.extCollection.FirstOrDefault(x => x.Discipline == "Standart").GetUI, sender);
        }

    }
}
