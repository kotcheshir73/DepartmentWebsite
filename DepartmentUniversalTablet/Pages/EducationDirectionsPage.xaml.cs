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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class EducationDirectionsPage : Page
    {
        private IEducationDirectionService _serviceED;

        public EducationDirectionsPage()
        {
            this.InitializeComponent();

            _serviceED = UnityConfig.Container.Resolve<EducationDirectionService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DisciplinesPage), ((Button)sender).Content);
        }
    }
}
