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
using LearningProgressInterfaces.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Egov.TP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DisciplineLessonTasksPage : Page
    {
        private IDisciplineLessonTaskService _serviceDL;
        private DisciplineLessonViewModel bindingModel;

        public DisciplineLessonTasksPage()
        {
            this.InitializeComponent();

            _serviceDL = UnityConfig.Container.Resolve<DisciplineLessonTaskService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bindingModel = (DisciplineLessonViewModel)e.Parameter;
                var list = _serviceDL.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { DisciplineLessonId = bindingModel.Id}).Result.List;

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
            var tmp = ((DisciplineLessonTaskViewModel)((Button)sender).Content);
            Frame.Navigate(typeof(DisciplineLessonTaskStudentsPage), tmp);
        }
    }
}
