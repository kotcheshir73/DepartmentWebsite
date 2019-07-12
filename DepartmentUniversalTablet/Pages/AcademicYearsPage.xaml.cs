using AcademicYearInterfaces.Interfaces;
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
using AcademicYearInterfaces.ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.Pages
{
    /// <summary>
    /// Страница выбора учебного года.
    /// </summary>
    public sealed partial class AcademicYearsPage : Page
    {
        private IAcademicYearService _serviceAY;

        public AcademicYearsPage()
        {
            this.InitializeComponent();

            _serviceAY = UnityConfig.Container.Resolve<IAcademicYearService>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var result = _serviceAY.GetAcademicYears(new AcademicYearInterfaces.BindingModels.AcademicYearGetBindingModel());
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
            Frame.Navigate(typeof(EducationDirectionsPage), ((AcademicYearViewModel)((Button)sender).Content).Id);
        }
    }
}
