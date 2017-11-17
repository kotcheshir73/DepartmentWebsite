using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleOffsetRecordForm : Form
    {
        private readonly IOffsetRecordService _service;

        private readonly IScheduleService _serviceS;

        private long? _id;

        public ScheduleOffsetRecordForm(IOffsetRecordService service, IScheduleService serviceS, long? id = null)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
            _id = id;
        }

        private void ScheduleOffsetRecordForm_Load(object sender, EventArgs e)
		{
			var resultSG = _serviceS.GetStudentGroups();
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

			var resultS = _serviceS.GetClassrooms(new ClassroomGetBindingModel { });
			if (!resultS.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultS.Errors);
				return;
			}

			//comboBoxLecturer.ValueMember = "Value";
			//comboBoxLecturer.DisplayMember = "Display";
			//comboBoxLecturer.DataSource = _service.GetEducationDirections()
			//    .Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();

			comboBoxStudentGroup.ValueMember = "Value";
			comboBoxStudentGroup.DisplayMember = "Display";
			comboBoxStudentGroup.DataSource = resultSG.Result
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
			comboBoxStudentGroup.SelectedItem = null;
			textBoxLessonGroup.Text = string.Empty;

			comboBoxClassroom.ValueMember = "Value";
			comboBoxClassroom.DisplayMember = "Display";
			comboBoxClassroom.DataSource = resultS.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Id }).ToList();
			comboBoxClassroom.SelectedItem = null;
			textBoxClassroom.Text = string.Empty;

			if (_id.HasValue)
            {
                var result = _service.GetOffsetRecord(new OffsetRecordGetBindingModel { Id = _id.Value });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				textBoxLessonDiscipline.Text = entity.LessonDiscipline;
                textBoxLessonGroup.Text = entity.LessonGroup;
                textBoxLessonLecturer.Text = entity.LessonLecturer;
                textBoxClassroom.Text = entity.LessonClassroom;

                comboBoxWeek.SelectedIndex = entity.Week;
                comboBoxDay.SelectedIndex = entity.Day;
                comboBoxLesson.SelectedIndex = entity.Lesson;

                if (!string.IsNullOrEmpty(entity.ClassroomId))
                {
                    comboBoxClassroom.SelectedValue = entity.ClassroomId;
                }
                if (entity.StudentGroupId.HasValue)
                {
                    comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
                }

                panelDateTime.Enabled = false;
            }
        }

        private void comboBoxLecturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonLecturer.Text) && comboBoxLecturer.SelectedIndex > -1)
            {
                textBoxLessonLecturer.Text = comboBoxLecturer.Text;
            }
        }

        private void comboBoxStudentGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonGroup.Text) && comboBoxStudentGroup.SelectedIndex > -1)
            {
                textBoxLessonGroup.Text = comboBoxStudentGroup.Text;
            }
        }

        private void comboBoxClassroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxClassroom.Text) && comboBoxClassroom.SelectedIndex > -1)
            {
                textBoxClassroom.Text = comboBoxClassroom.Text;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxLessonDiscipline.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonGroup.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonLecturer.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxClassroom.Text))
            {
                return false;
            }
            if (comboBoxWeek.SelectedIndex == -1)
            {
                return false;
            }
            if (comboBoxDay.SelectedIndex == -1)
            {
                return false;
            }
            if (comboBoxLesson.SelectedIndex == -1)
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
                if (!_id.HasValue)
                {
                    result = _service.CreateOffsetRecord(new OffsetRecordRecordBindingModel
                    {
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonGroup = textBoxLessonGroup.Text,
                        LessonClassroom = textBoxClassroom.Text,

                        ClassroomId = comboBoxClassroom.SelectedValue != null ? comboBoxClassroom.SelectedValue.ToString() : string.Empty,
                    });
                }
                else
                {
                    result = _service.UpdateOffsetRecord(new OffsetRecordRecordBindingModel
                    {
                        Id = _id.Value,
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonGroup = textBoxLessonGroup.Text,
                        LessonClassroom = textBoxClassroom.Text,

                        ClassroomId = comboBoxClassroom.SelectedValue != null ? comboBoxClassroom.SelectedValue.ToString() : string.Empty
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
