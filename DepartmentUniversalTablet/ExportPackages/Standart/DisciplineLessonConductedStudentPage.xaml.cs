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
using LearningProgressInterfaces.Interfaces;
using LearningProgressInterfaces.BindingModels;
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.ViewModels;
using Enums;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /// <summary>
    /// Страница выставления оценок для студента.
    /// </summary>
    public sealed partial class DisciplineLessonConductedStudentPage : Page
    {
        private IDisciplineLessonConductedStudentService _serviceDLCS;
        private DisciplineLessonConductedStudentViewModel dataContext;

        public DisciplineLessonConductedStudentPage()
        {
            this.InitializeComponent();

            _serviceDLCS = UnityConfig.Container.Resolve<DisciplineLessonConductedStudentService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dataContext = (DisciplineLessonConductedStudentViewModel)e.Parameter;

            this.DataContext = dataContext;

            switch (dataContext.Status)
            {
                case DisciplineLessonStudentStatus.НеЯвка:
                    NotComeRadioButton.IsChecked = true;
                    break;
                case DisciplineLessonStudentStatus.Явка:
                    ComeRadioButton.IsChecked = true;
                    break;
                case DisciplineLessonStudentStatus.Пропуск:
                    PassRadioButton.IsChecked = true;
                    break;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            _serviceDLCS.UpdateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
            {
                Ball = dataContext.Ball,
                Comment = dataContext.Comment,
                DisciplineLessonConductedId = dataContext.DisciplineLessonConductedId,
                Id = dataContext.Id,
                Status = dataContext.Status.ToString(),
                StudentId = dataContext.StudentId
            });
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            string content = (string)pressed.Content == "Не явка" ? "НеЯвка" : (string)pressed.Content; 
            dataContext.Status = (DisciplineLessonStudentStatus)Enum.Parse(typeof(DisciplineLessonStudentStatus), content);
        }

    }
}
