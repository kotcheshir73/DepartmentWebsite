﻿using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.Contingent
{
    public partial class FormContingent : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IContingentService _service;

		private Guid? _id = null;

        private Guid _ayId;

        public FormContingent(IContingentService service, Guid ayId, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
            _ayId = ayId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void FormContingent_Load(object sender, EventArgs e)
		{
			var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
			if (!resultAY.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
				return;
			}

			var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
			if (!resultED.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultED.Errors);
				return;
			}

			comboBoxAcademicYear.ValueMember = "Value";
			comboBoxAcademicYear.DisplayMember = "Display";
			comboBoxAcademicYear.DataSource = resultAY.Result.List
				.Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
			comboBoxAcademicYear.SelectedItem = _ayId;

			comboBoxEducationDirection.ValueMember = "Value";
			comboBoxEducationDirection.DisplayMember = "Display";
			comboBoxEducationDirection.DataSource = resultED.Result.List
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
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
			comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
            textBoxContingentName.Text = entity.ContingentName;
			textBoxCourse.Text = (Math.Log(entity.Course, 2.0) + 1).ToString();
            textBoxCountGroups.Text = entity.CountGroups.ToString();
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
            if (string.IsNullOrEmpty(textBoxContingentName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxCourse.Text))
			{
				return false;
            }
            if (string.IsNullOrEmpty(textBoxCountGroups.Text))
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
            if (!int.TryParse(textBoxCountGroups.Text, out count))
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
					result = _service.CreateContingent(new ContingentSetBindingModel
					{
						AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
						EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                        ContingentName = textBoxContingentName.Text,
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxCourse.Text) - 1.0),
                        CountGroups = Convert.ToInt32(textBoxCountGroups.Text),
						CountStudents = Convert.ToInt32(textBoxCountStudents.Text),
						CountSubgroups = Convert.ToInt32(textBoxCountSubgroups.Text)
					});
				}
				else
				{
					result = _service.UpdateContingent(new ContingentSetBindingModel
					{
						Id = _id.Value,
						AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
						EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                        ContingentName = textBoxContingentName.Text,
                        Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxCourse.Text) - 1.0),
                        CountGroups = Convert.ToInt32(textBoxCountGroups.Text),
                        CountStudents = Convert.ToInt32(textBoxCountStudents.Text),
						CountSubgroups = Convert.ToInt32(textBoxCountSubgroups.Text)
					});
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is Guid)
						{
							_id = (Guid)result.Result;
						}
					}
					return true;
				}
				else
				{
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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