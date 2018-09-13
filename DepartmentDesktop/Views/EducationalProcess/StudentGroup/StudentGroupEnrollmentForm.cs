using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupEnrollmentForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _service;

		private readonly IStudentMoveService _serviceSM;

		private Guid? _id = null;

		public StudentGroupEnrollmentForm(IStudentGroupService service, IStudentMoveService serviceSM, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
			_serviceSM = serviceSM;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void buttonLoadFromFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "doc|*.doc|docx|*.docx";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				var result = _serviceSM.LoadStudentsFromFile(new StudentLoadDocBindingModel { Id = _id.Value, FileName = dialog.FileName });
				if (result.Succeeded)
				{
					var list = result.Result.List;
					for (int i = 0; i < list.Count; ++i)
					{
						dataGridViewStudents.Rows.Add();
						int index = dataGridViewStudents.Rows.Count - 2;
						dataGridViewStudents.Rows[index].Cells[0].Value = list[i].NumberOfBook;
						dataGridViewStudents.Rows[index].Cells[1].Value = list[i].LastName;
						dataGridViewStudents.Rows[index].Cells[2].Value = list[i].FirstName;
						dataGridViewStudents.Rows[index].Cells[3].Value = list[i].Patronymic;
						dataGridViewStudents.Rows[index].Cells[5].Value = list[i].Description;
					}
				}
				else
				{
					Program.PrintErrorMessage("При загрузки возникла ошибка: ", result.Errors);
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxOrderNumber.Text))
			{
				MessageBox.Show("Введите номер приказа", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (dataGridViewStudents.Rows.Count == 1)
			{
				MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var list = new List<StudentSetBindingModel>();
			for (int i = 0; i < dataGridViewStudents.Rows.Count - 1; ++i)
			{
				var model = new StudentSetBindingModel
				{
					NumberOfBook = dataGridViewStudents.Rows[i].Cells[0].Value.ToString(),
					LastName = dataGridViewStudents.Rows[i].Cells[1].Value.ToString(),
					FirstName = dataGridViewStudents.Rows[i].Cells[2].Value.ToString(),
					Patronymic = dataGridViewStudents.Rows[i].Cells[3].Value.ToString(),
					Description = dataGridViewStudents.Rows[i].Cells[5].Value.ToString(),
					StudentGroupId = _id.Value,
                    Email = "no"
				};
				list.Add(model);
			}
			var result = _serviceSM.EnrollmentStudents(new StudentEnrollmentBindingModel
			{
				OrderNumber = textBoxOrderNumber.Text,
				OrderDate = dateTimePickerEnrollmentDate.Value,
				StudentList = list
			});
			if(result.Succeeded)
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
