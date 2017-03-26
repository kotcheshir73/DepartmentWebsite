using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	public partial class StudentGroupDeductionForm : Form
	{
		private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private long _id = 0;

		public StudentGroupDeductionForm(IStudentGroupService service, IStudentService serviceS, long id)
		{
			InitializeComponent();
			_service = service;
			_serviceS = serviceS;
			_id = id;
		}

		private void StudentGroupDeductionForm_Load(object sender, EventArgs e)
		{
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
			if (string.IsNullOrEmpty(textBoxDeductionReason.Text))
			{
				MessageBox.Show("Введите основание отчисления", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var list = new List<StudentRecordBindingModel>();
			for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
			{
				if (Convert.ToBoolean(dataGridViewStudents.Rows[i].Cells[0].Value))
				{
					var model = new StudentRecordBindingModel
					{
						NumberOfBook = dataGridViewStudents.Rows[i].Cells[1].Value.ToString()
					};
					list.Add(model);
				}
			}
			if (list.Count == 0)
			{
				MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var result = _serviceS.DeductionStudents(new StudentDeductionBindingModel
			{
				DeductionDate = dateTimePickerDeductionDate.Value,
				DeductionReason = textBoxDeductionReason.Text,
				DeductionOrderNumber = textBoxDeductionOrderNumber.Text,
				StudentList = list,
				StudentGroupId = _id
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
