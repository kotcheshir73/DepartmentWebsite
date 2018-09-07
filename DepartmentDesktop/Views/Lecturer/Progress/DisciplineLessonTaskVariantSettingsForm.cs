using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.Services;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Lecturer
{
    public partial class DisciplineLessonTaskVariantSettingsForm : Form
    {
        private readonly IDisciplineLessonTaskService _serviceDLT;
        private readonly DisciplineLessonTaskVariantService _serviceDLTV;

        private Guid? _id = null;

        private Guid _taskId;

        public DisciplineLessonTaskVariantSettingsForm(IDisciplineLessonTaskService serviceDLT, DisciplineLessonTaskVariantService serviceDLTV, Guid taskId, Guid? id = null)
        {
            InitializeComponent();
            _serviceDLT = serviceDLT;
            _serviceDLTV = serviceDLTV;

            if (id != Guid.Empty)
            {
                _id = id;
            }

            _taskId = taskId;
        }

        private void DisciplineLessonTaskVariantSettingsForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var task =_serviceDLTV.GetDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = _id });
                textBoxVariantNumber.Text = task.Result.VariantNumber;
                textBoxTask.Text = task.Result.VariantTask;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTask.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxVariantNumber.Text))
            {
                return false;
            }

            return true;
        }

        public bool Save()
        {
            if (CheckFill())
            {
                ResultService resultDLTV;
                if (!_id.HasValue)
                {
                    resultDLTV = _serviceDLTV.CreateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                    {
                        DisciplineLessonTaskId = _taskId,
                        VariantNumber = textBoxVariantNumber.Text,
                        VariantTask = textBoxTask.Text
                    });
                    if (!resultDLTV.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", resultDLTV.Errors);
                        return false;
                    }
                }
                else
                {
                    resultDLTV = _serviceDLTV.UpdateDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantRecordBindingModel
                    {
                        Id=_id.Value,
                        DisciplineLessonTaskId = _taskId,
                        VariantNumber = textBoxVariantNumber.Text,
                        VariantTask = textBoxTask.Text
                    });
                    if (!resultDLTV.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", resultDLTV.Errors);
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
