using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace BaseControlsAndForms.Services
{
    public partial class FormFinishEducation : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentService _serviceS;

        private readonly IProcess _process;

        private Guid? _id = null;

        public FormFinishEducation(IStudentService serviceS, IProcess process, Guid? id = null)
        {
            InitializeComponent();
            _serviceS = serviceS;
            _process = process;
            _id = id;
        }

        private void FormFinishEducation_Load(object sender, EventArgs e)
        {
            var result = _serviceS.GetStudents(new StudentGetBindingModel { StudentGroupId = _id, StudentStatus = StudentState.Учится });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            var list = result.Result.List;
            for (int i = 0; i < list.Count; ++i)
            {
                dataGridViewStudents.Rows.Add(new object[]
                {
                    false,
                    list[i].Id,
                    list[i].NumberOfBook,
                    list[i].LastName,
                    list[i].FirstName,
                    list[i].Patronymic
                });
            }
        }

        private void CheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
            {
                dataGridViewStudents.Rows[i].Cells[0].Value = checkBoxSelectAll.Checked;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFinishEducationOrderNumber.Text))
            {
                MessageBox.Show("Введите основание отчисления", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var list = new List<Guid>();
            for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
            {
                if (Convert.ToBoolean(dataGridViewStudents.Rows[i].Cells[0].Value))
                {
                    list.Add(new Guid(dataGridViewStudents.Rows[i].Cells[1].Value.ToString()));
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = _process.FinishEducation(new FinishEducationBindingModel
            {
                FinishEducationOrderDate = dateTimePickerFinishEducationOrderDate.Value,
                FinishEducationOrderNumber = textBoxFinishEducationOrderNumber.Text,
                StudnetIds = list
            });
            if (result.Succeeded)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("Ошибка при сохранении спсика: ", result.Errors);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}