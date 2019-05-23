using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using LearningProgressControlsAndForms.DisciplineLessonTaskVariant;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.DisciplineLessonTask
{
    public partial class FormDisciplineLessonTask : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskService _service;

        private Guid? _dlId = null;

        public FormDisciplineLessonTask(IDisciplineLessonTaskService service, Guid? dlId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _dlId = dlId;
        }

        protected override bool LoadComponents()
        {
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

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlDisciplineLessonTaskVariant>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlDisciplineLessonTaskVariant).LoadData(_id.Value);

            var result = _service.GetDisciplineLessonTask(new DisciplineLessonTaskGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDisciplineLesson.SelectedValue = entity.DisciplineLessonId;
            textBoxTask.Text = entity.Task;
            textBoxDiscription.Text = entity.Description;
            checkBoxIsNecessarily.Checked = entity.IsNecessarily;
            textBoxOrder.Text = entity.Order.ToString();
            checkBoxMaxBall.Checked = entity.MaxBall.HasValue;
            if (entity.MaxBall.HasValue)
            {
                textBoxMaxBall.Text = entity.MaxBall.Value.ToString();
            }
            if (entity.Image != null)
            {
                buttonGetFile.Enabled = entity.Image.Length > 0;
            }
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxDisciplineLesson.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTask.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDiscription.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxOrder.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxOrder.Text))
            {
                if (!int.TryParse(textBoxOrder.Text, out int order))
                {
                    return false;
                }
            }
            if (checkBoxMaxBall.Checked)
            {
                if (string.IsNullOrEmpty(textBoxMaxBall.Text))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(textBoxMaxBall.Text))
                {
                    if (!double.TryParse(textBoxMaxBall.Text, out double maxBall))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                {
                    DisciplineLessonId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                    Task = textBoxTask.Text,
                    Description = textBoxDiscription.Text,
                    IsNecessarily = checkBoxIsNecessarily.Checked,
                    Order = Convert.ToInt32(textBoxOrder.Text),
                    MaxBall = checkBoxMaxBall.Checked ? Convert.ToDouble(textBoxMaxBall.Text) : (double?)null
                });
            }
            else
            {
                result = _service.UpdateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                {
                    Id = _id.Value,
                    DisciplineLessonId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                    Task = textBoxTask.Text,
                    Description = textBoxDiscription.Text,
                    IsNecessarily = checkBoxIsNecessarily.Checked,
                    Order = Convert.ToInt32(textBoxOrder.Text),
                    MaxBall = checkBoxMaxBall.Checked ? Convert.ToDouble(textBoxMaxBall.Text) : (double?)null
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

        private void CheckBoxMaxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMaxBall.Enabled = checkBoxMaxBall.Checked;
        }
    }
}
