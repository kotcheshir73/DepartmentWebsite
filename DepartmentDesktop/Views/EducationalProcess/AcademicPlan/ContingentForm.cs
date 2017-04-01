using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class ContingentForm : Form
	{
		private readonly IContingentService _service;

		private long _id = 0;

		public ContingentForm(IContingentService service)
		{
			InitializeComponent();
			_service = service;
		}

		public ContingentForm(IContingentService service, long id)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void ContingentForm_Load(object sender, EventArgs e)
		{
			var resultAY = _service.GetAcademicYears();
			if (!resultAY.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
				return;
			}

			var resultSG = _service.GetStudentGroups();
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

			comboBoxAcademicYear.ValueMember = "Value";
			comboBoxAcademicYear.DisplayMember = "Display";
			comboBoxAcademicYear.DataSource = resultAY.Result
				.Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
			comboBoxAcademicYear.SelectedItem = null;

			comboBoxStudentGroup.ValueMember = "Value";
			comboBoxStudentGroup.DisplayMember = "Display";
			comboBoxStudentGroup.DataSource = resultSG.Result
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
			comboBoxStudentGroup.SelectedItem = null;

			if (_id != 0)
			{
				var result = _service.GetContingent(new ContingentGetBindingModel { Id = _id });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
				comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
				textBoxCountStudents.Text = entity.CountStudents.ToString();
				textBoxCountSubgroups.Text = entity.CountSubgroups.ToString();
			}
		}

		private bool CheckFill()
		{
			if (comboBoxAcademicYear.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxStudentGroup.SelectedValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxCountStudents.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxCountSubgroups.Text))
			{
				return false;
			}
			int count = 0;
			if(!int.TryParse(textBoxCountStudents.Text, out count))
			{
				return false;
			}
			if (!int.TryParse(textBoxCountSubgroups.Text, out count))
			{
				return false;
			}
			return true;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (CheckFill())
			{
				ResultService result;
				if (_id == 0)
				{
					result = _service.CreateContingent(new ContingentRecordBindingModel
					{
						AcademicYearId = Convert.ToInt64(comboBoxAcademicYear.SelectedValue),
						StudentGroupId = Convert.ToInt64(comboBoxStudentGroup.SelectedValue),
						CountStudents = Convert.ToInt32(textBoxCountStudents.Text),
						CountSubgroups = Convert.ToInt32(textBoxCountSubgroups.Text)
					});
				}
				else
				{
					result = _service.UpdateContingent(new ContingentRecordBindingModel
					{
						Id = _id,
						AcademicYearId = Convert.ToInt64(comboBoxAcademicYear.SelectedValue),
						StudentGroupId = Convert.ToInt64(comboBoxStudentGroup.SelectedValue),
						CountStudents = Convert.ToInt32(textBoxCountStudents.Text),
						CountSubgroups = Convert.ToInt32(textBoxCountSubgroups.Text)
					});
				}
				if (result.Succeeded)
				{
					DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
				}
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
