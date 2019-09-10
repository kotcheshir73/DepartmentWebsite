﻿using AcademicYearInterfaces.BindingModels;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using DatabaseContext;
using LearningProgressControlsAndForms.DisciplineLesson;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.Services
{
    public partial class ControlConfiguringDisciplines : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IDisciplineLessonService _serviceDL;

        public ControlConfiguringDisciplines(ILearningProgressProcess process, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _process = process;
            _serviceDL = serviceDL;
        }

        public void LoadData()
        {
            var resultAY = _serviceDL.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _process.GetCurrentAcademicYear().Result;

            var resultED = _serviceDL.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
                .Select(d => new { Value = d.Id, Display = d.ShortName }).ToList();
            comboBoxEducationDirection.SelectedItem = null;
        }

        private void GetDisciplineDetails()
        {
            if (comboBoxAcademicYear.SelectedValue != null && comboBoxEducationDirection.SelectedValue != null)
            {
                var result = _process.GetDisciplines(new LearningProcessDisciplineBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                    UserId = DepartmentUserManager.UserId.Value
                });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", result.Errors);
                    return;
                }

                comboBoxDisciplines.ValueMember = "Value";
                comboBoxDisciplines.DisplayMember = "Display";
                comboBoxDisciplines.DataSource = result.Result
                    .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            }
        }

        private void ComboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDisciplineDetails();
        }

        private void ComboBoxEducationDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDisciplineDetails();
        }

        private void ComboBoxDisciplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDisciplines.SelectedValue != null)
            {
                var result = _process.GetDisciplineDetails(new LearningProcessDisciplineDetailBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    DisciplineId = new Guid(comboBoxDisciplines.SelectedValue.ToString()),
                    EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                    UserId = DepartmentUserManager.UserId.Value
                });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке деталей дисциплины возникла ошибка: ", result.Errors);
                    return;
                }

                tabControl.Controls.Clear();
                int counter = 0;
                StringBuilder sb = new StringBuilder();
                foreach (var elem in result.Result)
                {
                    sb.Append(elem.Info);
                    var tabPage = new TabPage
                    {
                        Location = new System.Drawing.Point(4, 22),
                        Name = "tabPage" + elem.TimeNormName,
                        Size = new System.Drawing.Size(832, 326),
                        TabIndex = counter++,
                        Text = elem.TimeNormName,
                        UseVisualStyleBackColor = true
                    };
                    tabControl.Controls.Add(tabPage);

                    var controlDL = Container.Resolve<ControlDisciplineLesson>();
                    controlDL.Dock = DockStyle.Fill;
                    controlDL.LoadData(new Guid(comboBoxAcademicYear.SelectedValue.ToString()), new Guid(comboBoxDisciplines.SelectedValue.ToString()),
                        new Guid(comboBoxEducationDirection.SelectedValue.ToString()), elem.Id);
                    tabPage.Controls.Add(controlDL);
                }
                labelInfo.Text = sb.ToString();
            }
        }
    }
}