using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleSemesterRecordForm : Form
    {
        private readonly ISemesterRecordService _service;

        private readonly IScheduleService _serviceS;

        private long _id = 0;

        public ScheduleSemesterRecordForm(ISemesterRecordService service, IScheduleService serviceS)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
        }

        public ScheduleSemesterRecordForm(ISemesterRecordService service, IScheduleService serviceS, long id)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
            _id = id;
        }

        private void ScheduleSemesterRecordForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(LessonTypes)))
            {
                comboBoxLessonType.Items.Add(elem.ToString());
            }
            comboBoxLessonType.SelectedIndex = -1;

            //comboBoxLecturer.ValueMember = "Value";
            //comboBoxLecturer.DisplayMember = "Display";
            //comboBoxLecturer.DataSource = _service.GetEducationDirections()
            //    .Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();

            comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = _serviceS.GetStudentGroups()
                .Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
            comboBoxStudentGroup.SelectedItem = null;
            textBoxLessonGroup.Text = string.Empty;

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = _serviceS.GetClassrooms()
                .Select(ed => new { Value = ed.Id, Display = ed.Id }).ToList();
            comboBoxClassroom.SelectedItem = null;
            textBoxClassroom.Text = string.Empty;

            if (_id != 0)
            {
                var entity = _service.GetSemesterRecord(new SemesterRecordGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                textBoxLessonDiscipline.Text = entity.LessonDiscipline;
                textBoxLessonGroup.Text = entity.LessonGroup;
                textBoxLessonLecturer.Text = entity.LessonLecturer;
                textBoxClassroom.Text = entity.LessonClassroom;
                comboBoxLessonType.SelectedIndex = comboBoxLessonType.Items.IndexOf(entity.LessonType);

                comboBoxWeek.SelectedIndex = entity.Week;
                comboBoxDay.SelectedIndex = entity.Day;
                comboBoxLesson.SelectedIndex = entity.Lesson;

                comboBoxClassroom.SelectedValue = entity.ClassroomId;
                comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;

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
            if (comboBoxLessonType.SelectedIndex == -1)
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
                if (_id == 0)
                {
                    result = _service.CreateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonGroup = textBoxLessonGroup.Text,
                        LessonClassroom = textBoxClassroom.Text,
                        
                        ClassroomId = comboBoxClassroom.SelectedValue != null ? comboBoxClassroom.SelectedValue.ToString() : string.Empty,
                    });
                }
                else
                {
                    result = _service.UpdateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Id = _id,
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonGroup = textBoxLessonGroup.Text,
                        LessonClassroom = textBoxClassroom.Text,

                        ClassroomId = comboBoxClassroom.SelectedValue != null ? comboBoxClassroom.SelectedValue.ToString() : string.Empty,

                        ApplyToAnalogRecordsByTextData = radioButtonApplyToTextData.Checked,
                        ApplyToAnalogRecordsByDiscipline = checkBoxApplyToAnalogRecordsByDisipline.Checked,
                        ApplyToAnalogRecordsByLecturer = checkBoxApplyToAnalogRecordsByLecturer.Checked,
                        ApplyToAnalogRecordsByGroup = checkBoxApplyToAnalogRecordsByGroup.Checked,
                        ApplyToAnalogRecordsByClassroom = checkBoxApplyToAnalogRecordsByClassroom.Checked,
                        ApplyToAnalogRecordsByLessonType = checkBoxApplyToAnalogRecordsByLessonType.Checked
                    });
                }
                if (result.Succeeded)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show("При сохранении возникла ошибка: " + strRes.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
