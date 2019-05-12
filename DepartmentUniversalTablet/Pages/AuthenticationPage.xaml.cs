using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tools;
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
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            this.InitializeComponent();
        }

        private async void buttonClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DepartmentUserManager.Login(login.Text, password.Password))
                {
                    Frame.Navigate(typeof(EducationDirectionsPage));
                }
            }
            catch(Exception ex)
            {
                ContentDialog closeDialog = new ContentDialog()
                {
                    Title = "Произошла ошибка",
                    Content = $"Текст ошибки:\n{ex.Message}",
                    PrimaryButtonText = "Закрыть",
                    SecondaryButtonText = "Остаться"
                };

                ContentDialogResult result = await closeDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    App.Current.Exit();
                }
            }

        }
    }
}
