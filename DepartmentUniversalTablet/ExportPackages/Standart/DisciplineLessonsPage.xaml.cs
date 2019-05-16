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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DisciplineLessonsPage : Page
    {
        private IDisciplineLessonService _serviceDL;
        private DisciplineLessonGetBindingModel getModel;

        public DisciplineLessonsPage()
        {
            this.InitializeComponent();

            _serviceDL = UnityConfig.Container.Resolve<DisciplineLessonService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getModel = (DisciplineLessonGetBindingModel)e.Parameter;
            var list = _serviceDL.GetDisciplineLessons(getModel).Result.List;

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
            //getModel.EducationDirectionId = ((EducationDirectionViewModel)((Button)sender).Content).Id;
            //Frame.Navigate(typeof(DisciplineLessonsPage), getModel);
        }


    }
}
