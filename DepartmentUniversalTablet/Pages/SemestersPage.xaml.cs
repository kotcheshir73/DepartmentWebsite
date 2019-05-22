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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bindingModel = (FullDisciplineLessonConductedBindingModel)e.Parameter;
            var list = _serviceLP.GetSemesters(new LearningProcessSemesterBindingModel
            {
                AcademicYearId = bindingModel.AcademicYearId,
                EducationDirectionId = bindingModel.EducationDirectionId,
                DisciplineId = bindingModel.DisciplineId,
                UserId = DepartmentUserManager.UserId.Value
            }).Result;

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
            bindingModel.Semester = ((Semesters)((Button)sender).Content).ToString();

            var disciplineViewModel = _serviceD.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel { Id = bindingModel.DisciplineId });
            ImportManager importManager = new ImportManager();
            var exportModel = importManager.extCollection
                .FirstOrDefault(x => x.Discipline == disciplineViewModel.Result.DisciplineName && x.Lecturer == DepartmentUserManager.LecturerName);//TODO: Как правильно отбирать
            Frame.Navigate(exportModel != null ? exportModel.GetUI 
                : importManager.extCollection.FirstOrDefault(x => x.Discipline == "Standart" && x.Lecturer == "Standart").GetUI, bindingModel);
        }
    }
}
