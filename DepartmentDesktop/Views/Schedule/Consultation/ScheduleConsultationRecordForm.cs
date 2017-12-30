using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Consultation
{
    public partial class ScheduleConsultationRecordForm : Form
    {
        private readonly IConsultationRecordService _service;

        private readonly IScheduleService _serviceS;

        private long? _id;

        private DateTime? _datetime;

        private ScheduleGetBindingModel _model;

        public ScheduleConsultationRecordForm(IConsultationRecordService service, IScheduleService serviceS, long? id = null, DateTime? datetime = null,
            ScheduleGetBindingModel model = null)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
            _id = id;
            _datetime = datetime;
            _model = model;
        }

        private void ScheduleConsultationRecordForm_Load(object sender, EventArgs e)
		{
			var resultS = _serviceS.GetClassrooms(new ClassroomGetBindingModel { });
			if (!resultS.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultS.Errors);
				return;
            }

            var resultD = _serviceS.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            var resultL = _serviceS.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return;
            }

			var resultSG = _serviceS.GetStudentGroups(new StudentGroupGetBindingModel { });
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = resultS.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Id }).ToList();
            comboBoxClassroom.SelectedItem = null;
            textBoxClassroom.Text = string.Empty;

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

            comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = resultSG.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
            comboBoxStudentGroup.SelectedItem = null;
            textBoxLessonGroup.Text = string.Empty;

            if (_datetime.HasValue)
            {
                dateTimePickerDateConsultation.Value = _datetime.Value;
            }
            if (_model != null)
            {
                if(!string.IsNullOrEmpty(_model.ClassroomId))
                {
                    comboBoxClassroom.SelectedValue = _model.ClassroomId;
                }
                if (_model.LecturerId.HasValue)
                {
                    comboBoxLecturer.SelectedValue = _model.LecturerId;
                }
                if (_model.StudentGroupId.HasValue)
                {
                    comboBoxStudentGroup.SelectedValue = _model.StudentGroupId;
                }
            }

            if (_id.HasValue)
            {
                var result = _service.GetConsultationRecord(new ScheduleGetBindingModel { Id = _id.Value });
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

                dateTimePickerDateConsultation.Value = entity.DateConsultation;

                if (!string.IsNullOrEmpty(entity.ClassroomId))
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

                dateTimePickerDateConsultation.Enabled = false;
            }
        }

        private void comboBoxDiscipline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLessonDiscipline.Text) && comboBoxDiscipline.SelectedIndex > -1)
            {
                textBoxLessonDiscipline.Text = comboBoxDiscipline.Text;
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
            if(string.IsNullOrEmpty(textBoxClassroom.Text) && comboBoxClassroom.SelectedIndex > -1)
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
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                long? disciplineId = null;
                if (comboBoxDiscipline.SelectedValue != null)
                {
                    disciplineId = Convert.ToInt64(comboBoxDiscipline.SelectedValue);
                }
                long? lecturerId = null;
                if (comboBoxLecturer.SelectedValue != null)
                {
                    lecturerId = Convert.ToInt64(comboBoxLecturer.SelectedValue);
                }
                long? studentGroupId = null;
                if (comboBoxStudentGroup.SelectedValue != null)
                {
                    studentGroupId = Convert.ToInt64(comboBoxStudentGroup.SelectedValue);
                }
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateConsultationRecord(new ConsultationRecordRecordBindingModel
                    {
                        DateConsultation = dateTimePickerDateConsultation.Value,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonGroup = textBoxLessonGroup.Text,
                        LessonClassroom = textBoxClassroom.Text,

                        ClassroomId = comboBoxClassroom.SelectedValue != null ? comboBoxClassroom.SelectedValue.ToString() : string.Empty,
                        DisciplineId = disciplineId,
                        LecturerId = lecturerId,
                        StudentGroupId = studentGroupId
                    });
                }
                else
                {
                    result = _service.UpdateConsultationRecord(new ConsultationRecordRecordBindingModel
                    {
                        Id = _id.Value,
                        DateConsultation = dateTimePickerDateConsultation.Value,

                        LessonDiscipline = textBoxLessonDiscipline.Text,
                        LessonLecturer = textBoxLessonLecturer.Text,
                        LessonGroup = textBoxLessonGroup.Text,
                        LessonClassroom = textBoxClassroom.Text,

                        ClassroomId = comboBoxClassroom.SelectedValue != null ? comboBoxClassroom.SelectedValue.ToString() : string.Empty,
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
