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
using Tools;
using LearningProgressInterfaces.ViewModels;
using Enums;
using BaseInterfaces.Interfaces;
using BaseImplementations.Implementations;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.Pages
{
    /// <summary>
    /// Страница выбора семестра.
    /// </summary>
    public sealed partial class SemestersPage : Page
    {
        private ILearningProgressProcess _serviceLP;
        private IDisciplineService _serviceD;
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public SemestersPage()
        {
            this.InitializeComponent();

            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
            _serviceD = UnityConfig.Container.Resolve<DisciplineService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
                var result = _serviceLP.GetSemesters(new LearningProcessSemesterBindingModel
                {
                    AcademicYearId = bindingModel.AcademicYearId,
                    EducationDirectionId = bindingModel.EducationDirectionId,
                    DisciplineId = bindingModel.DisciplineId,
                    UserId = DepartmentUserManager.UserId.Value
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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bindingModel.Semester = ((Semesters)((Button)sender).Content).ToString();
                var disciplineViewModel = _serviceD.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel { Id = bindingModel.DisciplineId });
                ImportManager importManager = new ImportManager();
                var exportModel = importManager.extCollection
                    .FirstOrDefault(x => x.Discipline == disciplineViewModel.Result.DisciplineName && x.Lecturer == DepartmentUserManager.LecturerName);//TODO: Как правильно отбирать
                Frame.Navigate(exportModel != null ? exportModel.GetUI 
                    : importManager.extCollection.FirstOrDefault(x => x.Discipline == "Standart" && x.Lecturer == "Standart").GetUI, bindingModel);
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
    }
}
