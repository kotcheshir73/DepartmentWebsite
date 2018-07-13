using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.DisciplineLesson
{
    public partial class DisciplineLessonForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid? _id = null;

        public DisciplineLessonForm(IDisciplineLessonService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void LoadData()
        {
            var result = _service.GetDisciplineLesson(new DisciplineLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxPostTitle.Text = entity.Title;
            textBoxDiscription.Text = entity.Description;
            comboBoxDiscipline.SelectedValue = entity.DisciplineId;
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxPostTitle.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDiscription.Text))
            {
                return false;
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
                    result = _service.CreateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        Title = textBoxPostTitle.Text,
                        Description = textBoxDiscription.Text,
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString())
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        Id = _id.Value,
                        Title = textBoxPostTitle.Text,
                        Description = textBoxDiscription.Text,
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

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
        }

        private void DisciplineLessonForm_Load(object sender, EventArgs e)
        {
            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { });

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }
    }
}
