using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask.DisciplineLessonTaskVariant
{
    public partial class DisciplineLessonTaskVariantForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskVariantService _service;

        private Guid? _id = null;

        private Guid? _dltId = null;

        public DisciplineLessonTaskVariantForm(IDisciplineLessonTaskVariantService service, Guid? dltId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _dltId = dltId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonTaskVariantForm_Load(object sender, EventArgs e)
        {
            var resultDLT = _service.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { Id = _dltId });
            if (!resultDLT.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result.List
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedValue = _dltId;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            var result = _service.GetDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxVariantNumber.Text = entity.VariantNumber;
            textBoxVariantTask.Text = entity.VariantTask;
            textBoxOrder.Text = entity.Order.ToString();
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxDisciplineLessonTask.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxVariantTask.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxVariantNumber.Text))
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

            return true;
        }

        public bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                    {
                        DisciplineLessonTaskId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString()),
                        VariantNumber = textBoxVariantNumber.Text,
                        VariantTask = textBoxVariantTask.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text)
                    });
                    if (!result.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                        return false;
                    }
                }
                else
                {
                    result = _service.UpdateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                    {
                        Id = _id.Value,
                        DisciplineLessonTaskId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString()),
                        VariantNumber = textBoxVariantNumber.Text,
                        VariantTask = textBoxVariantTask.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text)
                    });
                    if (!result.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
