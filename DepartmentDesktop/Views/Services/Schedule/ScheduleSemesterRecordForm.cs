using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleSemesterRecordForm : Form
    {
        private readonly ISemesterRecordService _service;

        private long _id = 0;

        public ScheduleSemesterRecordForm(ISemesterRecordService service)
        {
            InitializeComponent();
            _service = service;
        }

        public ScheduleSemesterRecordForm(ISemesterRecordService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void ScheduleSemesterRecordForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(LessonTypes)))
            {
                comboBoxLessonType.Items.Add(elem.ToString());
            }
            comboBoxLessonType.SelectedIndex = -1;
            if (_id != 0)
            {
                var entity = _service.GetSemesterRecord(new SemesterRecordGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                textBoxLessonDiscipline.Text = entity.LessonDiscipline;
                textBoxLessonGroupName.Text = entity.GroupName;
                textBoxLessonTeacher.Text = entity.LessonTeacher;
                textBoxClassroomId.Text = entity.ClassroomNumber;
                comboBoxLessonType.SelectedIndex = comboBoxLessonType.Items.IndexOf(entity.LessonType);
                comboBoxWeek.SelectedIndex = entity.Week;
                comboBoxDay.SelectedIndex = entity.Day;
                comboBoxLesson.SelectedIndex = entity.Lesson;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxLessonDiscipline.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonGroupName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonTeacher.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxClassroomId.Text))
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
                if (_id == 0)
                {
                    var res = _service.CreateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,
                        ClassroomId = textBoxClassroomId.Text,
                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonGroupName = textBoxLessonGroupName.Text,
                        LessonTeacher = textBoxLessonTeacher.Text,
                        ApplyToAnalogRecords = checkBoxApplyToAnalogRecords.Checked
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var res = _service.UpdateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Id = _id,
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,
                        ClassroomId = textBoxClassroomId.Text,
                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonGroupName = textBoxLessonGroupName.Text,
                        LessonTeacher = textBoxLessonTeacher.Text,
                        ApplyToAnalogRecords = checkBoxApplyToAnalogRecords.Checked
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
