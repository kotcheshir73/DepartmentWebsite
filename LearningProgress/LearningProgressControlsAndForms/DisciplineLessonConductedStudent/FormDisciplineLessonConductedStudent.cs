using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.DisciplineLessonConductedStudent
{
    public partial class FormDisciplineLessonConductedStudent : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedStudentService _service;

        private Guid? _dlcId = null;

        private Guid? _sgId = null;

        public FormDisciplineLessonConductedStudent(IDisciplineLessonConductedStudentService service, Guid? dlcId = null, Guid? sgId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _dlcId = dlcId;
            _sgId = sgId;
        }

        protected override bool LoadComponents()
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonStudentStatus)))
            {
                comboBoxStatus.Items.Add(elem.ToString());
            }
            comboBoxStatus.SelectedIndex = -1;

            var resultDLC = _service.GetDisciplineLessonConducteds(new DisciplineLessonConductedGetBindingModel { Id = _dlcId });
            if (!resultDLC.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке занятий возникла ошибка: ", resultDLC.Errors);
                return false;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDLC.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineLesson }).ToList();
            comboBoxDisciplineLesson.SelectedValue = _dlcId;

            var resultS = _service.GetStudents(new StudentGetBindingModel { StudentGroupId = _sgId });
            if (!resultS.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
                return false;
            }

            comboBoxStudent.ValueMember = "Value";
            comboBoxStudent.DisplayMember = "Display";
            comboBoxStudent.DataSource = resultS.Result.List
                .Select(d => new { Value = d.Id, Display = string.Format("{0} {1}", d.LastName, d.FirstName) }).ToList();
            comboBoxStudent.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDisciplineLesson.SelectedValue = entity.DisciplineLessonConductedId;
            comboBoxStudent.SelectedValue = entity.StudentId;
            comboBoxStatus.SelectedIndex = comboBoxStatus.Items.IndexOf(entity.Status.ToString());
            checkBoxBall.Checked = entity.Ball.HasValue;
            textBoxBall.Text = entity.Ball?.ToString() ?? "";
            textBoxComment.Text = entity.Comment;
        }

        protected override bool CheckFill()
        {
            if (comboBoxDisciplineLesson.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxStudent.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxStatus.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxBall.Text) && checkBoxBall.Checked)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxBall.Text))
            {
                if (!int.TryParse(textBoxBall.Text, out int ball))
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
                {
                    DisciplineLessonConductedId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                    StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                    Status = comboBoxStatus.Text,
                    Ball = checkBoxBall.Checked ? Convert.ToInt32(textBoxBall.Text) : (int?)null,
                    Comment = textBoxComment.Text
                });
            }
            else
            {
                result = _service.UpdateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
                {
                    Id = _id.Value,
                    DisciplineLessonConductedId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                    StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                    Status = comboBoxStatus.Text,
                    Ball = checkBoxBall.Checked ? Convert.ToInt32(textBoxBall.Text) : (int?)null,
                    Comment = textBoxComment.Text
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

        private void CheckBoxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxBall.Enabled = checkBoxBall.Checked;
        }
    }
}