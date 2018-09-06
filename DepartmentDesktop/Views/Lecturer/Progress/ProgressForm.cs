using DepartmentDesktop.Models;
using DepartmentDesktop.Views.Lecturer;
using DepartmentDesktop.Views.Lecturer.Progress;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.IServices;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.EducationalProcess.Progress
{
    public partial class ProgressForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _serviceD;

        private readonly IDisciplineLessonService _serviceDL;

        public ProgressForm(IDisciplineService serviceD, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _serviceD = serviceD;
            _serviceDL = serviceDL;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            var resultD = _serviceD.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            comboBoxDisciplines.ValueMember = "Value";
            comboBoxDisciplines.DisplayMember = "Display";
            comboBoxDisciplines.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDisciplines.SelectedIndex = 0;
        }

        private void LoadData()
        {
            if (tabPageLectures.Controls.Count == 0)
            {
                var controlPL = Container.Resolve<ProgressLecturesControl>();
                controlPL.Dock = DockStyle.Fill;
                tabPageLectures.Controls.Add(controlPL);
            }
                (tabPageLectures.Controls[0] as ProgressLecturesControl).LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()));

            if (tabPageLabs.Controls.Count == 0)
            {
                var controlPL = Container.Resolve<ProgressLabsControl>();
                controlPL.Dock = DockStyle.Fill;
                tabPageLabs.Controls.Add(controlPL);
            }
                (tabPageLabs.Controls[0] as ProgressLabsControl).LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()));

            if (tabPagePractices.Controls.Count == 0)
            {
                var controlPP = Container.Resolve<ProgressPracticsControl>();
                controlPP.Dock = DockStyle.Fill;
                tabPagePractices.Controls.Add(controlPP);
            }
                (tabPagePractices.Controls[0] as ProgressPracticsControl).LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()));

            if (tabPageCourseWorks.Controls.Count == 0)
            {
                var controlC = Container.Resolve<ProgressCursControl>();
                controlC.Dock = DockStyle.Fill;
                tabPageCourseWorks.Controls.Add(controlC);
            }
                (tabPageCourseWorks.Controls[0] as ProgressCursControl).LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()));

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxDisciplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
