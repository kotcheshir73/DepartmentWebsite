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
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AcademicYearsPage : Page
    {
        private IAcademicYearService _serviceAY;

        public AcademicYearsPage()
        {
            this.InitializeComponent();

            _serviceAY = UnityConfig.Container.Resolve<IAcademicYearService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var list = _serviceAY.GetAcademicYears(new AcademicYearInterfaces.BindingModels.AcademicYearGetBindingModel()).Result.List;

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
            Frame.Navigate(typeof(EducationDirectionsPage), ((AcademicYearViewModel)((Button)sender).Content).Id);
        }
    }
}
