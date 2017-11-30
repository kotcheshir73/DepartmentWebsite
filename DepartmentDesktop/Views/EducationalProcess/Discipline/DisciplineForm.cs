using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.Discipline
{
	public partial class DisciplineForm : Form
	{
		private readonly IDisciplineService _service;

		private long? _id;

		public DisciplineForm(IDisciplineService service, long? id = null)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void DisciplineForm_Load(object sender, EventArgs e)
		{
			var resultDB = _service.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
			if (!resultDB.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке блоков дисциплин возникла ошибка: ", resultDB.Errors);
				return;
			}

			comboBoxDisciplineBlock.ValueMember = "Value";
			comboBoxDisciplineBlock.DisplayMember = "Display";
			comboBoxDisciplineBlock.DataSource = resultDB.Result.List
				.Select(d => new { Value = d.Id, Display = d.Title }).ToList();
			comboBoxDisciplineBlock.SelectedItem = null;

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetDiscipline(new DisciplineGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxTitle.Text = entity.DisciplineName;
            textBoxDisciplineShortName.Text = entity.DisciplineShortName;
			comboBoxDisciplineBlock.SelectedValue = entity.DisciplineBlockId;
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(textBoxTitle.Text))
			{
				return false;
			}
			if(comboBoxDisciplineBlock.SelectedValue == null)
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
					result = _service.CreateDiscipline(new DisciplineRecordBindingModel
					{
						DisciplineName = textBoxTitle.Text,
                        DisciplineShortName = textBoxDisciplineShortName.Text,
                        DisciplineBlockId = Convert.ToInt64(comboBoxDisciplineBlock.SelectedValue)
					});
				}
				else
				{
					result = _service.UpdateDiscipline(new DisciplineRecordBindingModel
					{
						Id = _id.Value,
						DisciplineName = textBoxTitle.Text,
                        DisciplineShortName = textBoxDisciplineShortName.Text,
						DisciplineBlockId = Convert.ToInt64(comboBoxDisciplineBlock.SelectedValue)
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
