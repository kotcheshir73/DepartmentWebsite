using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using LearningProgressInterfaces.BindingModels;
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
using Enums;
using BaseInterfaces.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /// <summary>
    /// Страница выбора студенческой группы.
    /// </summary>
    public sealed partial class StudentGroupsPage : Page
    {
        private IStudentGroupService _serviceSG;
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public StudentGroupsPage()
        {
            this.InitializeComponent();

            _serviceSG = UnityConfig.Container.Resolve<StudentGroupService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
                AcademicCourse course = AcademicCourse.Course_1;
                switch (bindingModel.Semester)
                {
                    case "Первый":
                    case "Второй":
                        course = AcademicCourse.Course_1;
                        break;
                    case "Третий":
                    case "Четвертый":
                        course = AcademicCourse.Course_2;
                        break;
                    case "Пятый":
                    case "Шестой":
                        course = AcademicCourse.Course_3;
                        break;
                    case "Седьмой":
                    case "Восьмой":
                        course = AcademicCourse.Course_4;
                        break;
                }
                var result = _serviceSG.GetStudentGroups(new StudentGroupGetBindingModel
                {
                    Course = course.ToString(),
                    EducationDirectionId = bindingModel.EducationDirectionId
                });
                if (!result.Succeeded)
                {
                    throw new Exception("При загрузке возникла ошибка: " + result.Errors.FirstOrDefault(x => x.Key == "Error:").Value);
                }

                Button button1;

                foreach (var item in result.Result.List)
                {
                    button1 = new Button();
                    button1.Content = item;
                    button1.Click += button_Click;
                    grid.Children.Add(button1);
                }
            }
            catch (Exception ex)
            {
                ContentDialog exceptionDialog = new ContentDialog()
                {
                    Title = "Произошла ошибка",
                    Content = $"Текст ошибки:\n{ex.Message}",
                    PrimaryButtonText = "Назад",
                    SecondaryButtonText = "Остаться"
                };

                ContentDialogResult result = await exceptionDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    if (Frame.CanGoBack)
                        Frame.GoBack();
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bindingModel.StudentGroupId = ((StudentGroupViewModel)((Button)sender).Content).Id;
            Frame.Navigate(typeof(DisciplineLessonsPage), bindingModel);
        }
    }
}
