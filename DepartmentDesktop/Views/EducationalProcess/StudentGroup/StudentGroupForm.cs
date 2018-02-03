﻿using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupForm : Form
    {
        private readonly IStudentGroupService _service;
    
        private readonly IStudentService _serviceS;

		private readonly IStudentMoveService _serviceSM;

		private Guid? _id;

        public StudentGroupForm(IStudentGroupService service, IStudentService serviceS, IStudentMoveService serviceSM, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
			_serviceSM = serviceSM;
			_id = id;
        }

        private void StudentGroupForm_Load(object sender, EventArgs e)
		{
			var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
			if (!resultED.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
				return;
			}

			comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();

            var resultL = _service.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return;
            }

            comboBoxCurator.ValueMember = "Value";
            comboBoxCurator.DisplayMember = "Display";
            comboBoxCurator.DataSource = resultL.Result.List
                .Select(l => new { Value = l.Id, Display = l.FullName }).ToList();

            var control = new StudentGroupStudentsControl(_service, _serviceS, _serviceSM)
            {
                Left = 0,
                Top = 0,
                Height = Height - 60,
                Width = Width - 15,
                Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)))
            };
            tabPageStudents.Controls.Add(control);

            if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			(tabPageStudents.Controls[0] as StudentGroupStudentsControl).LoadData(_id.Value);
			var result = _service.GetStudentGroup(new StudentGroupGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
			textBoxGroupName.Text = entity.GroupName;
			textBoxKurs.Text = (Math.Log(entity.Course, 2.0) + 1).ToString();
            textBoxSteward.Text = entity.StewardName;
            if (entity.CuratorId.HasValue)
            {
                comboBoxCurator.SelectedValue = entity.CuratorId;
            }
		}

		private bool CheckFill()
        {
            if (comboBoxEducationDirection.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxGroupName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxKurs.Text))
            {
                return false;
            }
            int course = 0;
            if (!int.TryParse(textBoxKurs.Text, out course))
            {
                return false;
            }
			if(course < 0 || course > 6)
			{
				return false;
			}
            return true;
		}

		private bool Save()
		{
			if (CheckFill())
			{
                Guid? curatorId = null;
                if (comboBoxCurator.SelectedValue != null)
                {
                    curatorId = new Guid(comboBoxCurator.SelectedValue.ToString());
                }
                ResultService result;
				if (!_id.HasValue)
				{
					result = _service.CreateStudentGroup(new StudentGroupRecordBindingModel
					{
						EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
						GroupName = textBoxGroupName.Text,
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxKurs.Text) - 1.0),
                        StewardName = textBoxSteward.Text,
                        CuratorId = curatorId
                    });
				}
				else
				{
					result = _service.UpdateStudentGroup(new StudentGroupRecordBindingModel
					{
						Id = _id.Value,
						EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
						GroupName = textBoxGroupName.Text,
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxKurs.Text) - 1.0),
                        StewardName = textBoxSteward.Text,
                        CuratorId = curatorId
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
