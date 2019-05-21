using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Unity;
using LearningProgressInterfaces.BindingModels;
using Tools;
using BaseInterfaces.ViewModels;
using LearningProgressInterfaces.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /// <summary>
    /// Страница выбора типа занятий.
    /// </summary>
    public sealed partial class TimeNormsPage : Page
    {
        private ILearningProgressProcess _serviceLP;
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public TimeNormsPage()
        {
            this.InitializeComponent();

            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
            var list = _serviceLP.GetDisciplineDetails(new LearningProcessDisciplineDetailBindingModel()
            {
                AcademicYearId = bindingModel.AcademicYearId,
                DisciplineId = bindingModel.DisciplineId,
                EducationDirectionId = bindingModel.EducationDirectionId,
                UserId = DepartmentUserManager.UserId.Value,
                Semester = bindingModel.Semester
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
            bindingModel.TimeNormId = ((LearningProcessDisciplineDetailViewModel)((Button)sender).Content).Id;
            Frame.Navigate(typeof(StudentGroupsPage), bindingModel);
        }
    }
}
