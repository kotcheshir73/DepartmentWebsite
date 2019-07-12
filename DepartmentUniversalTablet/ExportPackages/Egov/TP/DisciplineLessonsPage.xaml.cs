using LearningProgressInterfaces.Interfaces;
using LearningProgressInterfaces.ViewModels;
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
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.BindingModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Egov.TP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DisciplineLessonsPage : Page
    {
        private IDisciplineLessonService _serviceDL;
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public DisciplineLessonsPage()
        {
            this.InitializeComponent();

            _serviceDL = UnityConfig.Container.Resolve<DisciplineLessonService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
                var list = _serviceDL.GetDisciplineLessons(new DisciplineLessonGetBindingModel
                {
                    AcademicYearId = bindingModel.AcademicYearId,
                    DisciplineId = bindingModel.DisciplineId,
                    EducationDirectionId = bindingModel.EducationDirectionId,
                    TimeNormId = bindingModel.TimeNormId,
                    Semester = bindingModel.Semester
                }).Result.List;
                
                Button button1;

                foreach (var item in list)
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
            var tmp = ((DisciplineLessonViewModel)((Button)sender).Content);
            Frame.Navigate(typeof(DisciplineLessonTasksPage), tmp);
        }
    }
}
