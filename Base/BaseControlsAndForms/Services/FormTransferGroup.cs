using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace BaseControlsAndForms.Services
{
    public partial class FormTransferGroup : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _serviceSG;

        private readonly IStudentService _serviceS;

        private readonly IProcess _process;

        private Guid? _id = null;

        private List<Guid> _ids = null;

        public FormTransferGroup(IStudentGroupService serviceSG, IStudentService serviceS, IProcess process, Guid? id, List<Guid> ids)
        {
            InitializeComponent();
            _serviceSG = serviceSG;
            _serviceS = serviceS;
            _process = process;
            _id = id;
            _ids = ids;
        }

        private void FormTransferGroup_Load(object sender, EventArgs e)
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

            for (int i = 0; i < _ids.Count; ++i)
            {
                var result = _serviceS.GetStudent(new StudentGetBindingModel { Id = _ids[i] });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    return;
                }
                dataGridViewStudents.Rows.Add(new object[] {
                    result.Result.Id,
                    result.Result.NumberOfBook,
                    result.Result.LastName,
                    result.Result.FirstName,
                    result.Result.Patronymic
                });
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTransferOrderNumber.Text))
            {
                MessageBox.Show("Введите основание перевода", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxNewStudentGroup.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу перевода", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Guid newId = new Guid(comboBoxNewStudentGroup.SelectedValue.ToString());
            var list = new List<Tuple<Guid, bool>>();
            for (int i = 0; i < _ids.Count; ++i)
            {
                list.Add(new Tuple<Guid, bool>(_ids[i], false));
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = _process.TransferGroup(new StudentTransferBindingModel
            {
                TransferOrderDate = dateTimePickerTransferDate.Value,
                TransferOrderNumber = textBoxTransferOrderNumber.Text,
                NewStudentGroupId = newId,
                OldStudentGroupId = _id.Value,
                StudentList = list
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