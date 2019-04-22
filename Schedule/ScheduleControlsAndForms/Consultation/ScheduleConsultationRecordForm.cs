using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;

namespace ScheduleControlsAndForms.Consultation
{
    public partial class ScheduleConsultationRecordForm : Form
    {
        private readonly IConsultationRecordService _service;

        private readonly IScheduleProcess _process;

        private Guid? _id;

        private DateTime? _datetime;

        private ScheduleGetBindingModel _model;

        public ScheduleConsultationRecordForm(IConsultationRecordService service, IScheduleProcess process, Guid? id = null, DateTime? datetime = null, ScheduleGetBindingModel model = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _id = id;
            _datetime = datetime;
            _model = model;
        }

        private void ScheduleConsultationRecordForm_Load(object sender, EventArgs e)
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

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = resultS.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.Number }).ToList();
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
                if(_model.ClassroomId.HasValue)
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
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				textBoxLessonDiscipline.Text = entity.LessonDiscipline;
                textBoxLessonGroup.Text = entity.LessonGroup;
                textBoxLessonLecturer.Text = entity.LessonLecturer;
                textBoxClassroom.Text = entity.LessonClassroom;

                dateTimePickerDateConsultation.Value = entity.DateConsultation;

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
            if (string.IsNullOrEmpty(textBoxLessonGroup.Text) && comboBoxStudentGroup.SelectedIndex > -1)
            {
                textBoxLessonGroup.Text = comboBoxStudentGroup.Text;
            }
        }
        

        private void ComboBoxClassroom_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
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
                Guid? classroomId = null;
                if (comboBoxClassroom.SelectedValue != null)
                {
                    classroomId = new Guid(comboBoxClassroom.SelectedValue.ToString());
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

                        ClassroomId = classroomId,
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