using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanForm : Form
	{
		private readonly IAcademicPlanService _service;

		private readonly IAcademicPlanRecordService _serviceAPR;

		private long _id = 0;

		public AcademicPlanForm(IAcademicPlanService service, IAcademicPlanRecordService serviceAPR)
		{
			InitializeComponent();
			_service = service;
			_serviceAPR = serviceAPR;
		}

		public AcademicPlanForm(IAcademicPlanService service, IAcademicPlanRecordService serviceAPR, long id)
		{
			InitializeComponent();
			_service = service;
			_serviceAPR = serviceAPR;
			_id = id;
		}

		private void AcademicPlanForm_Load(object sender, EventArgs e)
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
				Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
				return;
			}

			foreach (var elem in Enum.GetValues(typeof(AcademicLevel)))
			{
				comboBoxAcademicLevel.Items.Add(elem.ToString());
			}

			comboBoxAcademicYear.ValueMember = "Value";
			comboBoxAcademicYear.DisplayMember = "Display";
			comboBoxAcademicYear.DataSource = resultAY.Result
				.Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
			comboBoxAcademicYear.SelectedItem = null;

			comboBoxEducationDirection.ValueMember = "Value";
			comboBoxEducationDirection.DisplayMember = "Display";
			comboBoxEducationDirection.DataSource = resultED.Result
				.Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();
			comboBoxEducationDirection.SelectedItem = null;

			var control = new AcademicPlanRecordControl(_serviceAPR);
			control.Left = 0;
			control.Top = 0;
			control.Height = Height - 60;
			control.Width = Width - 15;
			control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
						| System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			tabPageRecords.Controls.Add(control);

			if (_id != 0)
			{
				control.LoadData(_id);
				var result = _service.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = _id });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
				comboBoxEducationDirection.Enabled = false;
				comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
				comboBoxAcademicLevel.SelectedIndex = comboBoxAcademicLevel.Items.IndexOf(entity.AcademicLevel);
				var courses = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), entity.AcademicCourses);
				checkBox1.Checked = (courses & AcademicCourse.Course_1) == AcademicCourse.Course_1;
				checkBox2.Checked = (courses & AcademicCourse.Course_2) == AcademicCourse.Course_2;
				checkBox3.Checked = (courses & AcademicCourse.Course_3) == AcademicCourse.Course_3;
				checkBox4.Checked = (courses & AcademicCourse.Course_4) == AcademicCourse.Course_4;
				checkBox5.Checked = (courses & AcademicCourse.Course_5) == AcademicCourse.Course_5;
				checkBox6.Checked = (courses & AcademicCourse.Course_6) == AcademicCourse.Course_6;
			}
		}

		private bool CheckFill()
		{
			if (comboBoxEducationDirection.SelectedValue == null)
			{
				return false;
			}
			if (comboBoxAcademicYear.SelectedValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(comboBoxAcademicLevel.Text))
			{
				return false;
			}
			if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked &&
				!checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked)
			{
				return false;
			}
			return true;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (CheckFill())
			{
				AcademicCourse courses = new AcademicCourse();
				if(checkBox1.Checked)
				{
					courses = courses | AcademicCourse.Course_1;
				}
				if (checkBox2.Checked)
				{
					courses = courses | AcademicCourse.Course_2;
				}
				if (checkBox3.Checked)
				{
					courses = courses | AcademicCourse.Course_3;
				}
				if (checkBox4.Checked)
				{
					courses = courses | AcademicCourse.Course_4;
				}
				if (checkBox5.Checked)
				{
					courses = courses | AcademicCourse.Course_5;
				}
				if (checkBox6.Checked)
				{
					courses = courses | AcademicCourse.Course_6;
				}
				ResultService result;
				if (_id == 0)
				{
					result = _service.CreateAcademicPlan(new AcademicPlanRecordBindingModel
					{
						EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
						AcademicYearId = Convert.ToInt64(comboBoxAcademicYear.SelectedValue),
						AcademicLevel = comboBoxAcademicLevel.Text,
						AcademicCourses = Convert.ToInt32(courses)
					});
				}
				else
				{
					result = _service.UpdateAcademicPlan(new AcademicPlanRecordBindingModel
					{
						Id = _id,
						EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
						AcademicYearId = Convert.ToInt64(comboBoxAcademicYear.SelectedValue),
						AcademicLevel = comboBoxAcademicLevel.Text,
						AcademicCourses = Convert.ToInt32(courses)
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
