using DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask.DisciplineLessonTaskVariant;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask
{
    public partial class DisciplineLessonTaskForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskService _service;

        private Guid? _id = null;

        private Guid? _dlId = null;

        public DisciplineLessonTaskForm(IDisciplineLessonTaskService service, Guid? dlId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _dlId = dlId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonTaskForm_Load(object sender, EventArgs e)
        {
            var resultDL = _service.GetDisciplineLessons(new DisciplineLessonGetBindingModel { Id = _dlId });
            if (!resultDL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке занятий возникла ошибка: ", resultDL.Errors);
                return;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDL.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxDisciplineLesson.SelectedValue = _dlId;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<DisciplineLessonTaskVariantControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as DisciplineLessonTaskVariantControl).LoadData(_id.Value);

            var result = _service.GetDisciplineLessonTask(new DisciplineLessonTaskGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        private bool CheckFill()
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
                int order = 0;
                if (!int.TryParse(textBoxOrder.Text, out order))
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
                    decimal maxBall = 0;
                    if (!decimal.TryParse(textBoxMaxBall.Text, out maxBall))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Save()
        {
            if (CheckFill())
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
                        MaxBall = checkBoxMaxBall.Checked ? Convert.ToDecimal(textBoxMaxBall.Text) : (decimal?)null
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
                        MaxBall = checkBoxMaxBall.Checked ? Convert.ToDecimal(textBoxMaxBall.Text) : (decimal?)null
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
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void checkBoxMaxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMaxBall.Enabled = checkBoxMaxBall.Checked;
        }
    }
}
