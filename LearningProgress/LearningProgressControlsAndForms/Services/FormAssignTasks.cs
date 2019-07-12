﻿using ControlsAndForms.Messangers;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace LearningProgressControlsAndForms.Services
{
    public partial class FormAssignTasks : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IDisciplineLessonTaskStudentAcceptService _service;

        private Guid _dlId;

        private Guid _sgId;

        public FormAssignTasks(ILearningProgressProcess process, IDisciplineLessonTaskStudentAcceptService service, Guid dlId, Guid sgId)
        {
            InitializeComponent();
            _process = process;
            _service = service;
            _dlId = dlId;
            _sgId = sgId;
        }

        private void FormAssignTasks_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonTaskStudentResult)))
            {
                ColumnResult.Items.Add(elem.ToString());
            }

            var resultDLT = _service.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { DisciplineLessonId = _dlId });
            if (!resultDLT.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result.List
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedItem = null;
        }

        private void ButtonGet_Click(object sender, EventArgs e)
        {
            if (comboBoxDisciplineLessonTask.SelectedValue != null)
            {
                var result = _process.GetDisciplineLessonTaskStudentAcceptForFill(new DisciplineLessonTaskStudentAcceptForFillBindingModel
                {
                    StudentGroupId = _sgId,
                    DisciplineLessonTaskId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString())
                });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }

                for (int i = 0; i < result.Result.Count; ++i)
                {
                    dataGridViewAccepts.Rows.Add(new object[] {
                        result.Result[i].Id,
                        result.Result[i].DisciplineLessonTaskId,
                        result.Result[i].StudentId,
                        result.Result[i].Student,
                        result.Result[i].Task,
                        result.Result[i].Result.ToString(),
                        result.Result[i].Score,
                        result.Result[i].Comment
                    });
                }
            }
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выполнить сохранение?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridViewAccepts.Rows.Count; ++i)
                {
                    double ball = 0;
                    if (dataGridViewAccepts.Rows[i].Cells[6].Value != null)
                    {
                        string tempVal = dataGridViewAccepts.Rows[i].Cells[6].Value.ToString();
                        if (double.TryParse(tempVal, out double temp))
                        {
                            ball = temp;
                        }
                        else if (tempVal.Contains(","))
                        {
                            tempVal = tempVal.Replace(",", ".");
                            if (double.TryParse(tempVal, out temp))
                            {
                                ball = temp;
                            }
                        }
                        else if (tempVal.Contains("."))
                        {
                            tempVal = tempVal.Replace(".", ",");
                            if (double.TryParse(tempVal, out temp))
                            {
                                ball = temp;
                            }
                        }
                    }
                    var result = _service.UpdateDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptSetBindingModel
                    {
                        Id = new Guid(dataGridViewAccepts.Rows[i].Cells[0].Value.ToString()),
                        DisciplineLessonTaskId = new Guid(dataGridViewAccepts.Rows[i].Cells[1].Value.ToString()),
                        StudentId = new Guid(dataGridViewAccepts.Rows[i].Cells[2].Value.ToString()),
                        DateAccept = dateTimePickerDateAccept.Value,
                        Task = dataGridViewAccepts.Rows[i].Cells[4].Value.ToString(),
                        Result = dataGridViewAccepts.Rows[i].Cells[5].Value.ToString(),
                        Score = ball,
                        Comment = dataGridViewAccepts.Rows[i].Cells[7].Value?.ToString() ?? ""
                    });
                    if (!result.Succeeded)
                    {
                        ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                        return;
                    }
                }
                MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}