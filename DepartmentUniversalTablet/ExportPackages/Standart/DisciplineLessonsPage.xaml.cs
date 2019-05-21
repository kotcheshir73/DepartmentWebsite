using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.BindingModels;
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
using BaseInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Enums;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /// <summary>
    /// Страница для отображения списка занятий одного типа отбираемого из таблицы DiscilineLessonConducted.
    /// </summary>
    public sealed partial class DisciplineLessonsPage : Page
    {
        private ILearningProgressProcess _serviceLP;
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public DisciplineLessonsPage()
        {
            this.InitializeComponent();

            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
            var list = _serviceLP.GetFullDisciplineLessonConducteds(bindingModel).Result;

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
            var tmpModel = ((DisciplineLessonConductedViewModel)((Button)sender).Content);
            Frame.Navigate(typeof(StudentsPage), tmpModel);
        }


    }
}
