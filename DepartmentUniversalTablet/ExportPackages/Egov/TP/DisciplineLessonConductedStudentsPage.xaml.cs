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
using Enums;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Egov.TP
{
    /// <summary>
    /// Страница выбора студента.
    /// </summary>
    public sealed partial class DisciplineLessonConductedStudentsPage : Page
    {
        private IDisciplineLessonConductedStudentService _serviceDLCS;
        private ILearningProgressProcess _serviceLP;
        private DisciplineLessonConductedViewModel bindingModel;
        private List<DisciplineLessonConductedStudentViewModel> dataContext;

        public DisciplineLessonConductedStudentsPage()
        {
            this.InitializeComponent();

            _serviceDLCS = UnityConfig.Container.Resolve<DisciplineLessonConductedStudentService>();
            _serviceLP = UnityConfig.Container.Resolve<LearningProgressProcess>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bindingModel = (DisciplineLessonConductedViewModel)e.Parameter;
                dataContext = _serviceLP.GetDisciplineLessonConductedStudentsForFill(new LearningProgressInterfaces.BindingModels.DisciplineLessonConductedStudentsForFillBindingModel
                {
                    StudentGroupId = bindingModel.StudentGroupId,
                    DisciplineLessonConductedId = bindingModel.Id
                }).Result;
                Button button1;

                foreach (var item in dataContext)
                {
                    button1 = new Button();
                
                    var ComeRadioButton = new RadioButton { GroupName = "Status" + item.Id, Content = "Явка", Margin = new Thickness(5), Tag = item.Id };
                    ComeRadioButton.Checked += radioButton_Checked;
                    var NotComeRadioButton = new RadioButton { GroupName = "Status" + item.Id, Content = "Не явка", Margin = new Thickness(5), Tag = item.Id };
                    NotComeRadioButton.Checked += radioButton_Checked;
                    var PassRadioButton = new RadioButton { GroupName = "Status" + item.Id, Content = "Пропуск", Margin = new Thickness(5), Tag = item.Id };
                    PassRadioButton.Checked += radioButton_Checked;

                    switch (item.Status)
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

                    var StackPanelRadioButton = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(5) };
                    StackPanelRadioButton.Children.Add(ComeRadioButton);
                    StackPanelRadioButton.Children.Add(NotComeRadioButton);
                    StackPanelRadioButton.Children.Add(PassRadioButton);

                    button1.Content = item;
                    button1.Click += button_Click;

                    var StackPanel = new StackPanel { Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Stretch };
                    StackPanel.Children.Add(button1);
                    StackPanel.Children.Add(StackPanelRadioButton);

                    grid.Children.Add(StackPanel);
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

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            foreach(var item in dataContext)
            {
                _serviceDLCS.UpdateDisciplineLessonConductedStudent(new LearningProgressInterfaces.BindingModels.DisciplineLessonConductedStudentSetBindingModel
                {
                    Id = item.Id,
                    Ball = item.Ball,
                    Comment = item.Comment,
                    DisciplineLessonConductedId = item.DisciplineLessonConductedId,
                    Status = item.Status.ToString(),
                    StudentId = item.StudentId
                });
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var tmp = ((DisciplineLessonConductedStudentViewModel)((Button)sender).Content);
            Frame.Navigate(typeof(DisciplineLessonConductedStudentPage), tmp);
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            string content = (string)pressed.Content == "Не явка" ? "НеЯвка" : (string)pressed.Content;

            dataContext[dataContext.FindIndex(x => x.Id == ((Guid)pressed.Tag))].Status 
                = (DisciplineLessonStudentStatus)Enum.Parse(typeof(DisciplineLessonStudentStatus), content);
        }
    }
}
