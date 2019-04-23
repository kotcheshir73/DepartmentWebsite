using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.DisciplineLessonTaskVariant
{
    public partial class FormDisciplineLessonTaskVariant : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskVariantService _service;

        private Guid? _dltId = null;

        public FormDisciplineLessonTaskVariant(IDisciplineLessonTaskVariantService service, Guid? dltId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _dltId = dltId;
        }

        private void FormDisciplineLessonTaskVariant_Load(object sender, EventArgs e)
        {
            var resultDLT = _service.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { Id = _dltId });
            if (!resultDLT.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result.List
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedValue = _dltId;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        protected override bool Save()
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
                        ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
                        ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
    }
}