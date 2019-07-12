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

namespace LearningProgressControlsAndForms.DisciplineLessonTaskStudentAccept
{
    public partial class FormDisciplineLessonTaskStudentAccept : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskStudentAcceptService _service;

        private Guid? _dlId = null;

        private Guid? _sgId = null;

        public FormDisciplineLessonTaskStudentAccept(IDisciplineLessonTaskStudentAcceptService service, Guid? dlId = null, Guid? sgId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _dlId = dlId;
            _sgId = sgId;
        }

        protected override bool LoadComponents()
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonTaskStudentResult)))
            {
                comboBoxResult.Items.Add(elem.ToString());
            }

            var resultDL = _service.GetDisciplineLessons(new DisciplineLessonGetBindingModel { Id = _dlId });
            if (!resultDL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке занятий возникла ошибка: ", resultDL.Errors);
                return false;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDL.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxDisciplineLesson.SelectedValue = _dlId;

            var resultDLT = _service.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { DisciplineLessonId = _dlId });
            if (!resultDLT.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return false;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result.List
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedItem = null;

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
            comboBoxStudent.SelectedValue = _dlId;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDisciplineLessonTask.SelectedValue = entity.DisciplineLessonTaskId;
            comboBoxStudent.SelectedValue = entity.StudentId;
            comboBoxResult.SelectedIndex = comboBoxResult.Items.IndexOf(entity.Result.ToString());
            dateTimePickerDateAccept.Value = entity.DateAccept;
            textBoxTask.Text = entity.Task;
            textBoxScore.Text = entity.Score.ToString();
            textBoxComment.Text = entity.Comment;
            richTextBoxLog.Text = entity.Log;
        }

        protected override bool CheckFill()
        {
            if (comboBoxDisciplineLessonTask.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxStudent.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxResult.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTask.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxScore.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxScore.Text))
            {
                if (!decimal.TryParse(textBoxScore.Text, out decimal order))
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
                result = _service.CreateDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptSetBindingModel
                {
                    DisciplineLessonTaskId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString()),
                    StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                    Result = comboBoxResult.Text,
                    Task = textBoxTask.Text,
                    DateAccept = dateTimePickerDateAccept.Value,
                    Score = Convert.ToDouble(textBoxScore.Text),
                    Comment = textBoxComment.Text
                });
            }
            else
            {
                result = _service.UpdateDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptSetBindingModel
                {
                    Id = _id.Value,
                    DisciplineLessonTaskId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString()),
                    StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                    Result = comboBoxResult.Text,
                    Task = textBoxTask.Text,
                    DateAccept = dateTimePickerDateAccept.Value,
                    Score = Convert.ToDouble(textBoxScore.Text),
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
    }
}