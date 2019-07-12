using Enums;
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.BindingModels;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Egov.TP
{
    /// <summary>
    /// Страница для оценки задания.
    /// </summary>
    public sealed partial class DisciplineLessonTaskStudentPage : Page
    {
        private IDisciplineLessonTaskStudentAcceptService _serviceDLTSA;
        private DisciplineLessonTaskStudentAcceptViewModel dataContext;

        public DisciplineLessonTaskStudentPage()
        {
            this.InitializeComponent();

            _serviceDLTSA = UnityConfig.Container.Resolve<DisciplineLessonTaskStudentAcceptService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dataContext = (DisciplineLessonTaskStudentAcceptViewModel)e.Parameter;

            textBoxComment.Text = dataContext.Comment;
            calendar.Date = dataContext.DateAccept;

            var _enumval = Enum.GetValues(typeof(DisciplineLessonTaskStudentResult)).Cast<DisciplineLessonTaskStudentResult>();
            result.ItemsSource = _enumval.ToList();

            grid.DataContext = dataContext;
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                _serviceDLTSA.UpdateDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptSetBindingModel
                {
                    Comment = dataContext.Comment,
                    DateAccept = dataContext.DateAccept,
                    DisciplineLessonTaskId = dataContext.DisciplineLessonTaskId,
                    Id = dataContext.Id,
                    Log = dataContext.Log,
                    Result = dataContext.Result.ToString(),
                    Score = dataContext.Score,
                    StudentId = dataContext.StudentId,
                    Task = dataContext.Task
                });
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

        private void Calendar_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (calendar.Date != null)
            {
                dataContext.DateAccept = calendar.Date.Value.DateTime;
            }
        }

        private void TextBoxComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataContext.Comment = textBoxComment.Text;
        }
    }
}
