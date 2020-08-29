using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace BaseControlsAndForms.Services
{
    public partial class FormRecovery : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _serviceSG;

        private readonly IStudentService _serviceS;

        private readonly IProcess _process;

        private Guid? _id = null;

        public FormRecovery(IStudentGroupService serviceSG, IStudentService serviceS, IProcess process, Guid? id = null)
        {
            InitializeComponent();
            _serviceSG = serviceSG;
            _serviceS = serviceS;
            _process = process;
            if(id.HasValue)
            {
                _id = id;
            }
        }

        private void FormRecovery_Load(object sender, EventArgs e)
        {
            var resultSG = _serviceSG.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxNewStudentGroup.ValueMember = "Value";
            comboBoxNewStudentGroup.DisplayMember = "Display";
            comboBoxNewStudentGroup.DataSource = resultSG.Result.List
                .Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
            comboBoxNewStudentGroup.SelectedItem = null;

            var result = _serviceS.GetStudents(new StudentGetBindingModel
            {
                StudentStatus = StudentState.Отчислен,
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            foreach (var res in result.Result.List)
            {
                dataGridViewStudents.Rows.Add(new object[] {
                    false,
                    res.Id,
                    res.NumberOfBook,
                    res.LastName,
                    res.FirstName,
                    res.Patronymic
                });
            }

            if(_id.HasValue)
            {
                comboBoxNewStudentGroup.SelectedValue = _id;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRecoveryOrderNumber.Text))
            {
                MessageBox.Show("Введите основание перевода", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxNewStudentGroup.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var result = _process.RecoveryStudents(new StudentRecoveryBindingModel
            {
                StudnetIds = list,
                RecoveryOrderDate = dateTimePickerRecoveryDate.Value,
                RecoveryOrderNumber = textBoxRecoveryOrderNumber.Text,
                StudentGroupId = new Guid(comboBoxNewStudentGroup.SelectedValue.ToString())
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