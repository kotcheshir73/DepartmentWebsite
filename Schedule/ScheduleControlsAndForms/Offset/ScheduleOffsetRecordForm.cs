﻿using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;

namespace ScheduleControlsAndForms.Offset
{
    public partial class ScheduleOffsetRecordForm : Form
    {
        private readonly IOffsetRecordService _service;

        private readonly IScheduleProcess _process;

        private Guid? _id;

        public ScheduleOffsetRecordForm(IOffsetRecordService service, IScheduleProcess process, Guid? id = null, DateTime? scheduleDate = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _id = id;

            if (scheduleDate.HasValue)
            {
                dateTimePickerDateOffset.Value = scheduleDate.Value;
            }
        }

        private void ScheduleOffsetRecordForm_Load(object sender, EventArgs e)
		{
			var resultS = _process.GetClassrooms(new ClassroomGetBindingModel { });
			if (!resultS.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultS.Errors);
				return;
            }

            var resultD = _process.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            var resultL = _process.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return;
            }

            var resultSG = _process.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxStudentGroup.ValueMember = "Value";
			comboBoxStudentGroup.DisplayMember = "Display";
			comboBoxStudentGroup.DataSource = resultSG.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
			comboBoxStudentGroup.SelectedItem = null;
            textBoxLessonStudentGroup.Text = string.Empty;

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(ed => new { Value = ed.Id, Display = ed.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedItem = null;
            textBoxLessonDiscipline.Text = string.Empty;

            comboBoxLecturer.ValueMember = "Value";
            comboBoxLecturer.DisplayMember = "Display";
            comboBoxLecturer.DataSource = resultL.Result.List
                .Select(ed => new { Value = ed.Id, Display = ed.FullName }).ToList();
            comboBoxLecturer.SelectedItem = null;
            textBoxLessonLecturer.Text = string.Empty;

            comboBoxClassroom.ValueMember = "Value";
			comboBoxClassroom.DisplayMember = "Display";
			comboBoxClassroom.DataSource = resultS.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Number }).ToList();
			comboBoxClassroom.SelectedItem = null;
            textBoxLessonClassroom.Text = string.Empty;

            if (_id.HasValue)
            {
                var result = _service.GetOffsetRecord(new ScheduleGetBindingModel { Id = _id.Value });
				if (!result.Succeeded)
				{
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				textBoxLessonDiscipline.Text = entity.LessonDiscipline;
                textBoxLessonStudentGroup.Text = entity.LessonStudentGroup;
                textBoxLessonLecturer.Text = entity.LessonLecturer;
                textBoxLessonClassroom.Text = entity.LessonClassroom;

                dateTimePickerDateOffset.Value = entity.ScheduleDate;
                comboBoxLesson.SelectedIndex = entity.Lesson;

                if (entity.ClassroomId.HasValue)
                {
                    comboBoxClassroom.SelectedValue = entity.ClassroomId;
                }
                if (entity.DisciplineId.HasValue)
                {
                    comboBoxDiscipline.SelectedValue = entity.DisciplineId;
                }
                if (entity.LecturerId.HasValue)
                {
                    comboBoxLecturer.SelectedValue = entity.LecturerId;
                }
                if (entity.StudentGroupId.HasValue)
                {
                    comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
                }

                panelDateTime.Enabled = false;
            }
        }

        private void ComboBoxClassroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonClassroom.Text) && comboBoxClassroom.SelectedIndex > -1)
            {
                textBoxLessonClassroom.Text = comboBoxClassroom.Text;
            }
        }

        private void ComboBoxDiscipline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonDiscipline.Text) && comboBoxDiscipline.SelectedIndex > -1)
            {
                textBoxLessonDiscipline.Text = comboBoxDiscipline.Text;
            }
        }

        private void ComboBoxLecturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonLecturer.Text) && comboBoxLecturer.SelectedIndex > -1)
            {
                textBoxLessonLecturer.Text = comboBoxLecturer.Text;
            }
        }

        private void ComboBoxStudentGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonStudentGroup.Text) && comboBoxStudentGroup.SelectedIndex > -1)
            {
                textBoxLessonStudentGroup.Text = comboBoxStudentGroup.Text;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxLessonClassroom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonDiscipline.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonLecturer.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLessonStudentGroup.Text))
            {
                return false;
            }
            if (comboBoxLesson.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                Guid? classroomId = null;
                if (comboBoxClassroom.SelectedValue != null)
                {
                    classroomId = new Guid(comboBoxClassroom.SelectedValue.ToString());
                }
                Guid? disciplineId = null;
                if (comboBoxDiscipline.SelectedValue != null)
                {
                    disciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString());
                }
                Guid? lecturerId = null;
                if (comboBoxLecturer.SelectedValue != null)
                {
                    lecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString());
                }
                Guid? studentGroupId = null;
                if (comboBoxStudentGroup.SelectedValue != null)
                {
                    studentGroupId = new Guid(comboBoxStudentGroup.SelectedValue.ToString());
                }
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateOffsetRecord(new OffsetRecordSetBindingModel
                    {
                        ScheduleDate = dateTimePickerDateOffset.Value,
                        Lesson = comboBoxLesson.SelectedIndex,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonStudentGroup = textBoxLessonStudentGroup.Text,
                        LessonClassroom = textBoxLessonClassroom.Text,

                        ClassroomId = classroomId,
                        DisciplineId = disciplineId,
                        LecturerId = lecturerId,
                        StudentGroupId = studentGroupId
                    });
                }
                else
                {
                    result = _service.UpdateOffsetRecord(new OffsetRecordSetBindingModel
                    {
                        Id = _id.Value,
                        ScheduleDate = dateTimePickerDateOffset.Value,
                        Lesson = comboBoxLesson.SelectedIndex,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonStudentGroup = textBoxLessonStudentGroup.Text,
                        LessonClassroom = textBoxLessonClassroom.Text,

                        ClassroomId = classroomId,
                        DisciplineId = disciplineId,
                        LecturerId = lecturerId,
                        StudentGroupId = studentGroupId
                    });
                }
                if (result.Succeeded)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
				{
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
				}
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}