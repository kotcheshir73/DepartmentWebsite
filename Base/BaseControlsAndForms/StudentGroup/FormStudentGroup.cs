using BaseControlsAndForms.Student;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.StudentGroup
{
    public partial class FormStudentGroup : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _service;

        public FormStudentGroup(IStudentGroupService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        private void FormStudentGroup_Load(object sender, EventArgs e)
		{
			var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
			if (!resultED.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
				return;
			}

			comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();
            comboBoxEducationDirection.SelectedItem = null;

            var resultL = _service.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return;
            }

            comboBoxCurator.ValueMember = "Value";
            comboBoxCurator.DisplayMember = "Display";
            comboBoxCurator.DataSource = resultL.Result.List
                .Select(l => new { Value = l.Id, Display = l.FullName }).ToList();
            comboBoxCurator.SelectedItem = null;

            StandartForm_Load(sender, e);
        }

        protected override void LoadData()
        {
            if (tabPageStudents.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlStudent>();
                control.Dock = DockStyle.Fill;
                tabPageStudents.Controls.Add(control);
            }
            (tabPageStudents.Controls[0] as ControlStudent).LoadData(null, _id.Value);
			var result = _service.GetStudentGroup(new StudentGroupGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
            if (!int.TryParse(textBoxKurs.Text, out int course))
            {
                return false;
            }
            if (course < 0 || course > 6)
			{
				return false;
			}
            return true;
		}

        protected override bool Save()
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
					result = _service.CreateStudentGroup(new StudentGroupSetBindingModel
					{
						EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
						GroupName = textBoxGroupName.Text,
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxKurs.Text) - 1.0),
                        CuratorId = curatorId
                    });
				}
				else
				{
					result = _service.UpdateStudentGroup(new StudentGroupSetBindingModel
					{
						Id = _id.Value,
						EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
						GroupName = textBoxGroupName.Text,
						Course = (int)Math.Pow(2.0, Convert.ToDouble(textBoxKurs.Text) - 1.0),
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
	}
}