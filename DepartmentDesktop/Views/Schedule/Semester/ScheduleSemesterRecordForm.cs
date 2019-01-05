using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Semester
{
    public partial class ScheduleSemesterRecordForm : Form
    {
        private readonly ISemesterRecordService _service;

        private readonly IScheduleProcess _process;

        private Guid? _id;

        private int? _lesson;

        private bool _isFirstHalfSemester;

        public ScheduleSemesterRecordForm(ISemesterRecordService service, IScheduleProcess process, bool isFirstHalfSemester, Guid? id = null, int? lesson = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _id = id;
            _lesson = lesson;
            _isFirstHalfSemester = isFirstHalfSemester;
        }

        private void ScheduleSemesterRecordForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(LessonTypes)))
            {
                comboBoxLessonType.Items.Add(elem.ToString());
            }
            comboBoxLessonType.SelectedIndex = -1;

            var resultC = _process.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultC.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultC.Errors);
                return;
            }

            var resultD = _process.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            var resultL = _process.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return;
            }

            var resultSG = _process.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultSG.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxClassroom.ValueMember = "Value";
            comboBoxClassroom.DisplayMember = "Display";
            comboBoxClassroom.DataSource = resultC.Result.List
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

            if (_lesson.HasValue)
            {
                comboBoxWeek.SelectedIndex = _lesson.Value / 100;
                comboBoxDay.SelectedIndex = (_lesson.Value % 100) / 10;
                comboBoxLesson.SelectedIndex = _lesson.Value % 10 - 1;
            }

            if (_id.HasValue)
            {
                var result = _service.GetSemesterRecord(new ScheduleGetBindingModel { Id = _id.Value });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    Close();
                }
                var entity = result.Result;

                textBoxNotParseRecord.Text = entity.NotParseRecord;

                textBoxLessonDiscipline.Text = entity.LessonDiscipline;
                textBoxLessonGroup.Text = entity.LessonGroup;
                textBoxLessonLecturer.Text = entity.LessonLecturer;
                textBoxClassroom.Text = entity.LessonClassroom;
                comboBoxLessonType.SelectedIndex = comboBoxLessonType.Items.IndexOf(entity.LessonType);

                comboBoxWeek.SelectedIndex = entity.Week;
                comboBoxDay.SelectedIndex = entity.Day;
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

                _isFirstHalfSemester = entity.IsFirstHalfSemester;

                panelDateTime.Enabled = false;
            }
            else
            {

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

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            var model = new ScheduleGetBindingModel();
            if (checkBoxClassroom.Checked)
            {
                if (comboBoxClassroom.SelectedValue != null)
                {
                    model.ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(textBoxClassroom.Text))
                {
                    model.ClassroomNumber = textBoxClassroom.Text;
                }
            }
            if (checkBoxDiscipline.Checked)
            {
                if (comboBoxDiscipline.SelectedValue != null)
                {
                    model.DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(textBoxLessonDiscipline.Text))
                {
                    model.DisciplineName = textBoxLessonDiscipline.Text;
                }
            }
            if (checkBoxGroupName.Checked)
            {
                if (comboBoxStudentGroup.SelectedValue != null)
                {
                    model.StudentGroupId = new Guid(comboBoxDiscipline.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(textBoxLessonGroup.Text))
                {
                    model.StudentGroupName = textBoxLessonGroup.Text;
                }
            }
            if (checkBoxLecturer.Checked)
            {
                if (comboBoxLecturer.SelectedValue != null)
                {
                    model.LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString());
                }
                if (!string.IsNullOrEmpty(textBoxLessonLecturer.Text))
                {
                    model.LecturerName = textBoxLessonLecturer.Text;
                }
            }

            var result = _service.GetSemesterSchedule(model);

            dataGridViewRecords.Rows.Clear();

            if (result.Succeeded)
            {
                var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };//дни недели
                for (int i = 0; i < result.Result.Count; ++i)
                {
                    dataGridViewRecords.Rows.Add(result.Result[i].Id, false, string.Format("{0} {1} {2} {3} {4} {5} {6}", result.Result[i].Week + 1, days[result.Result[i].Day],
                        result.Result[i].Lesson + 1, result.Result[i].LessonDiscipline, result.Result[i].LessonGroup, result.Result[i].LessonLecturer,
                        result.Result[i].LessonClassroom));
                }
            }
        }

        private void ButtonSaveOther_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewRecords.Rows.Count; ++i)
            {
                if (Convert.ToBoolean(dataGridViewRecords.Rows[i].Cells[1].Value))
                {
                    var model = new SemesterRecordRecordBindingModel
                    {
                        Id = new Guid(dataGridViewRecords.Rows[i].Cells[0].Value.ToString()),
                        LessonType = comboBoxLessonType.Text,
                        IsFirstHalfSemester = _isFirstHalfSemester
                    };
                    if (checkBoxClassroom.Checked)
                    {
                        if (comboBoxClassroom.SelectedValue != null)
                        {
                            model.ClassroomId = new Guid(comboBoxClassroom.SelectedValue.ToString());
                        }
                        if (!string.IsNullOrEmpty(textBoxClassroom.Text))
                        {
                            model.LessonClassroom = textBoxClassroom.Text;
                        }
                    }
                    if (checkBoxDiscipline.Checked)
                    {
                        if (comboBoxDiscipline.SelectedValue != null)
                        {
                            model.DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString());
                        }
                        if (!string.IsNullOrEmpty(textBoxLessonDiscipline.Text))
                        {
                            model.LessonDiscipline = textBoxLessonDiscipline.Text;
                        }
                    }
                    if (checkBoxGroupName.Checked)
                    {
                        if (comboBoxStudentGroup.SelectedValue != null)
                        {
                            model.StudentGroupId = new Guid(comboBoxDiscipline.SelectedValue.ToString());
                        }
                        if (!string.IsNullOrEmpty(textBoxLessonGroup.Text))
                        {
                            model.LessonGroup = textBoxLessonGroup.Text;
                        }
                    }
                    if (checkBoxLecturer.Checked)
                    {
                        if (comboBoxLecturer.SelectedValue != null)
                        {
                            model.LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString());
                        }
                        if (!string.IsNullOrEmpty(textBoxLessonLecturer.Text))
                        {
                            model.LessonLecturer = textBoxLessonLecturer.Text;
                        }
                    }
                    var result = _service.UpdateSemesterRecord(model);
                    if (!result.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                        return;
                    }
                }
            }
            MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
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
                    result = _service.CreateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,
                        NotParseRecord = textBoxNotParseRecord.Text,
                        IsFirstHalfSemester = _isFirstHalfSemester,

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
                    MessageBox.Show("Запись имеет идентификатор", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (result.Succeeded)
                {
                    MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    result = _service.CreateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,
                        NotParseRecord = textBoxNotParseRecord.Text,
                        IsFirstHalfSemester = _isFirstHalfSemester,

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
                    result = _service.UpdateSemesterRecord(new SemesterRecordRecordBindingModel
                    {
                        Id = _id.Value,
                        Week = comboBoxWeek.SelectedIndex,
                        Day = comboBoxDay.SelectedIndex,
                        Lesson = comboBoxLesson.SelectedIndex,
                        LessonType = comboBoxLessonType.Text,
                        NotParseRecord = textBoxNotParseRecord.Text,
                        IsFirstHalfSemester = _isFirstHalfSemester,

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
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
