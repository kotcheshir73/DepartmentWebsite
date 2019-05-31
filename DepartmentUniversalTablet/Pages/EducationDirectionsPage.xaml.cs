using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Unity;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LearningProgressInterfaces.BindingModels;
using BaseInterfaces.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.Pages
{
    /// <summary>
    /// Страница выбора учебного направления.
    /// </summary>
    public sealed partial class EducationDirectionsPage : Page
    {
        private IEducationDirectionService _serviceED;
        private FullDisciplineLessonConductedBindingModel bindingModel;

        public EducationDirectionsPage()
        {
            this.InitializeComponent();

            _serviceED = UnityConfig.Container.Resolve<EducationDirectionService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            { 
                bindingModel = new FullDisciplineLessonConductedBindingModel()
                {
                    AcademicYearId = (Guid)e.Parameter
                };
                var list = _serviceED.GetEducationDirections(new BaseInterfaces.BindingModels.EducationDirectionGetBindingModel()).Result.List;

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
            bindingModel.EducationDirectionId = ((EducationDirectionViewModel)((Button)sender).Content).Id;
            Frame.Navigate(typeof(DisciplinesPage), bindingModel);
        }
    }
}
