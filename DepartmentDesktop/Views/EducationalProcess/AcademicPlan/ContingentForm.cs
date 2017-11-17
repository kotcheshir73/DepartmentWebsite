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

		private long? _id = 0;

		public ContingentForm(IContingentService service, long? id = null)
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

			var resultED = _service.GetEducationDirections();
			if (!resultED.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultED.Errors);
				return;
			}

			comboBoxAcademicYear.ValueMember = "Value";
			comboBoxAcademicYear.DisplayMember = "Display";
			comboBoxAcademicYear.DataSource = resultAY.Result
				.Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
			comboBoxAcademicYear.SelectedItem = null;

			comboBoxEducationDirection.ValueMember = "Value";
			comboBoxEducationDirection.DisplayMember = "Display";
			comboBoxEducationDirection.DataSource = resultED.Result
				.Select(ed => new { Value = ed.Id, Display = ed.Cipher }).ToList();
			comboBoxEducationDirection.SelectedItem = null;

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetContingent(new ContingentGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
			comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
			textBoxCourse.Text = (Math.Log(entity.Course, 2.0) + 1).ToString();
			textBoxCountStudents.Text = entity.CountStudents.ToString();
			textBoxCountSubgroups.Text = entity.CountSubgroups.ToString();
		}

		private bool CheckFill()
		{
			if (comboBoxAcademicYear.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxEducationDirection.SelectedValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxCourse.Text))
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
			if (!int.TryParse(textBoxCourse.Text, out count))
			{
				return false;
			}
			if (!int.TryParse(textBoxCountStudents.Text, out count))
			{
				return false;
			}
			if (!int.TryParse(textBoxCountSubgroups.Text, out count))
			{
				return false;
			}
			return true;
		}

		private bool Save()
		{
			if (CheckFill())
			{
				ResultService result;
				if (!_id.HasValue)
				{
					result = _service.CreateContingent(new ContingentRecordBindingModel
					{
						AcademicYearId = Convert.ToInt64(comboBoxAcademicYear.SelectedValue),
						EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxCourse.Text) - 1.0),
						CountStudents = Convert.ToInt32(textBoxCountStudents.Text),
						CountSubgroups = Convert.ToInt32(textBoxCountSubgroups.Text)
					});
				}
				else
				{
					result = _service.UpdateContingent(new ContingentRecordBindingModel
					{
						Id = _id.Value,
						AcademicYearId = Convert.ToInt64(comboBoxAcademicYear.SelectedValue),
						EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxCourse.Text) - 1.0),
						CountStudents = Convert.ToInt32(textBoxCountStudents.Text),
						CountSubgroups = Convert.ToInt32(textBoxCountSubgroups.Text)
					});
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is long)
						{
							_id = (long)result.Result;
						}
					}
					return true;
				}
				else
				{
					Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
					return false;
				}
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				LoadData();
			}
		}

		private void buttonSaveAndClose_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
