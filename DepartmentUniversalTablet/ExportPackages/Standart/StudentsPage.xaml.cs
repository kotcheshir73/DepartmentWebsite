using BaseImplementations.Implementations;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
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
using LearningProgressInterfaces.ViewModels;
using LearningProgressInterfaces.Interfaces;
using LearningProgressImplementations.Implementations;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /// <summary>
    /// Страница выбора студента.
    /// </summary>
    public sealed partial class StudentsPage : Page
    {
        private IDisciplineLessonConductedStudentService _serviceDLCS;
        private ILearningProgressProcess _serviceLP;
        private DisciplineLessonConductedViewModel bindingModel;

        public StudentsPage()
        {
            this.InitializeComponent();

            _serviceDLCS = UnityConfig.Container.Resolve<DisciplineLessonConductedStudentService>();
            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bindingModel = (DisciplineLessonConductedViewModel)e.Parameter;
            var list = _serviceLP.GetDisciplineLessonConductedStudentsForFill(new LearningProgressInterfaces.BindingModels.DisciplineLessonConductedStudentsForFillBindingModel
            {
                StudentGroupId = bindingModel.StudentGroupId,
                DisciplineLessonConductedId = bindingModel.Id
            }).Result;
            Button button1;

            foreach (var item in list)
            {
                button1 = new Button();

                //https://metanit.com/sharp/wpf/11.php
                //StackPanel stackPanelInButton = new StackPanel();
                //stackPanelInButton.DataContext = item;
                //Binding binding = new Binding();
                //stackPanelInButton.Children.Add(new TextBlock { Text =  });
                //button1.Content = stackPanelInButton;

                button1.Content = item;
                button1.Click += button_Click;
                grid.Children.Add(button1);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tmp = ((DisciplineLessonConductedStudentViewModel)((Button)sender).Content);
            Frame.Navigate(typeof(DisciplineLessonConductedStudentPage), tmp);
        }
    }
}
