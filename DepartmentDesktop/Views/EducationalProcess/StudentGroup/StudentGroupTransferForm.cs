using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	public partial class StudentGroupTransferForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private readonly IStudentMoveService _serviceSM;

		private Guid? _id = null;

		public StudentGroupTransferForm(IStudentGroupService service, IStudentService serviceS, IStudentMoveService serviceSM, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
			_serviceS = serviceS;
			_serviceSM = serviceSM;
			_id = id;
		}

		private void StudentGroupTransferForm_Load(object sender, EventArgs e)
		{
			var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

			comboBoxNewStudentGroup.ValueMember = "Value";
			comboBoxNewStudentGroup.DisplayMember = "Display";
			comboBoxNewStudentGroup.DataSource = resultSG.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
			comboBoxNewStudentGroup.SelectedItem = null;

			var result = _serviceS.GetStudents(new StudentGetBindingModel { StudentGroupId = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			var list = result.Result.List;
			for (int i = 0; i < list.Count; ++i)
			{
				dataGridViewStudents.Rows.Add();
				dataGridViewStudents.Rows[i].Cells[1].Value = list[i].NumberOfBook;
				dataGridViewStudents.Rows[i].Cells[2].Value = list[i].LastName;
				dataGridViewStudents.Rows[i].Cells[3].Value = list[i].FirstName;
				dataGridViewStudents.Rows[i].Cells[4].Value = list[i].Patronymic;
			}
		}

		private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
			{
				dataGridViewStudents.Rows[i].Cells[0].Value = checkBoxSelectAll.Checked;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxTransferReason.Text))
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
			var list = new List<StudentRecordBindingModel>();
			for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
			{
				if (Convert.ToBoolean(dataGridViewStudents.Rows[i].Cells[0].Value))
				{
					var model = new StudentRecordBindingModel
					{
						NumberOfBook = dataGridViewStudents.Rows[i].Cells[1].Value.ToString(),
						StudentGroupId = newId
					};
					list.Add(model);
				}
			}
			if (list.Count == 0)
			{
				MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var result = _serviceSM.TransferStudents(new StudentTransferBindingModel
			{
				TransferDate = dateTimePickerTransferDate.Value,
				TransferReason = textBoxTransferReason.Text,
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
				Program.PrintErrorMessage("Ошибка при сохранении спсика: ", result.Errors);
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
