using DatabaseContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    /// Страница авторизации.
    /// </summary>
    public sealed partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            this.InitializeComponent();
        } 

        private async void buttonClickAsync(object sender, RoutedEventArgs e)
        {
            progressRing.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Collapsed;
            loginTextBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Visibility = Visibility.Collapsed;
            progressRing.IsActive = true;
            try
            {
                string login = loginTextBox.Text;
                string password = passwordTextBox.Password;

                await Task.Run(() =>
                {
                    DepartmentUserManager.LoginAsync(login, password);
                });

                Frame.Navigate(typeof(AcademicYearsPage));
            }
            catch(Exception ex)
            {
                ContentDialog exceptionDialog = new ContentDialog()
                {
                    Title = "Произошла ошибка",
                    Content = $"Текст ошибки:\n{ex.Message}",
                    PrimaryButtonText = "Закрыть",
                    SecondaryButtonText = "Остаться"
                };

                ContentDialogResult result = await exceptionDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    App.Current.Exit();
                }
            }
            finally
            {
                progressRing.IsActive = false;
                progressRing.Visibility = Visibility.Collapsed;
                loginButton.Visibility = Visibility.Visible;
                loginTextBox.Visibility = Visibility.Visible;
                passwordTextBox.Visibility = Visibility.Visible;
            }
        }
    }
}
