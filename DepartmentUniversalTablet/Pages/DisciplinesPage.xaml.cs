using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
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
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public DisciplinesPage()
        {
            this.InitializeComponent();

            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
                var result = _serviceLP.GetDisciplines(new LearningProgressInterfaces.BindingModels.LearningProcessDisciplineBindingModel()
                {
                    UserId = DepartmentUserManager.UserId.Value,
                    AcademicYearId = bindingModel.AcademicYearId,
                    EducationDirectionId = bindingModel.EducationDirectionId
                });

                if (!result.Succeeded)
                {
                    throw new Exception("При загрузке возникла ошибка: " + result.Errors.FirstOrDefault(x => x.Key == "Error:").Value);
                }

                Button button1;

                foreach (var item in result.Result)
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
            bindingModel.DisciplineId = ((LearningProcessDisciplineViewModel)((Button)sender).Content).Id;

            Frame.Navigate(typeof(SemestersPage), bindingModel);
        }

    }
}
