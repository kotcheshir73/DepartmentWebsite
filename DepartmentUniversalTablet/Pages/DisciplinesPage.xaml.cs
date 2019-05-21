using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using LearningProgressInterfaces.ViewModels;
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
        private DisciplineLessonGetBindingModel getModel;

        public DisciplinesPage()
        {
            this.InitializeComponent();

            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getModel = (DisciplineLessonGetBindingModel)e.Parameter;
            var list = _serviceLP.GetDisciplines(new LearningProgressInterfaces.BindingModels.LearningProcessDisciplineBindingModel()
            {
                UserId = DepartmentUserManager.UserId.Value,
                AcademicYearId = getModel.AcademicYearId.Value,
                EducationDirectionId = getModel.EducationDirectionId.Value
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            getModel.DisciplineId = ((LearningProcessDisciplineViewModel)((Button)sender).Content).Id;

            ImportManager importManager = new ImportManager();
            var discipline = importManager.extCollection.FirstOrDefault(x => x.Discipline == ((Button)sender).Content.ToString());//TODO: Как правильно отбирать
            Frame.Navigate(discipline != null ? discipline.GetUI : importManager.extCollection.FirstOrDefault(x => x.Discipline == "Standart").GetUI, getModel);
        }

    }
}
