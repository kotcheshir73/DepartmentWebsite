using DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    public partial class DisciplineLessonForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid? _id = null;

        private Guid? _dId = null;

        private string _type;

        public DisciplineLessonForm(IDisciplineLessonService service, Guid? dId = null, string type = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _dId = dId;
            _type = type;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonTypes)))
            {
                comboBoxLessonType.Items.Add(elem.ToString());
            }
            comboBoxLessonType.SelectedIndex = comboBoxLessonType.Items.IndexOf(_type);

            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedValue = _dId;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<DisciplineLessonTaskControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as DisciplineLessonTaskControl).LoadData(_id.Value);

            var result = _service.GetDisciplineLesson(new DisciplineLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDiscipline.SelectedValue = entity.DisciplineId;
            comboBoxLessonType.SelectedIndex = comboBoxLessonType.Items.IndexOf(entity.LessonType);
            textBoxPostTitle.Text = entity.Title;
            textBoxDiscription.Text = entity.Description;
            textBoxOrder.Text = entity.Order.ToString();
            textBoxCountOfPairs.Text = entity.CountOfPairs.ToString();
            if (entity.DisciplineLessonFile != null)
            {
                buttonGetFile.Enabled = entity.DisciplineLessonFile.Length > 0;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxLessonType.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPostTitle.Text))
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
            if (string.IsNullOrEmpty(textBoxCountOfPairs.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxCountOfPairs.Text))
            {
                int order = 0;
                if (!int.TryParse(textBoxCountOfPairs.Text, out order))
                {
                    return false;
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
                    result = _service.CreateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        LessonType = comboBoxLessonType.Text,
                        Title = textBoxPostTitle.Text,
                        Description = textBoxDiscription.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        CountOfPairs = Convert.ToInt32(textBoxCountOfPairs.Text)
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        Id = _id.Value,
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        Title = textBoxPostTitle.Text,
                        Description = textBoxDiscription.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        CountOfPairs = Convert.ToInt32(textBoxCountOfPairs.Text)
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
    }
}
