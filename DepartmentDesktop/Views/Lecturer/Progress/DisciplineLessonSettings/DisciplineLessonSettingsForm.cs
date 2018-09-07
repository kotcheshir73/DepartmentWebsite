using DepartmentDesktop.Views.Lecturer.DisciplineLessonSettings;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.Lecturer
{
    public partial class DisciplineLessonSettingsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _serviceDL;

        private readonly IDisciplineLessonTaskService _serviceDLT;

        private Guid? _id = null;

        private Guid _dId;

        public DisciplineLessonSettingsForm(IDisciplineLessonService serviceDL, IDisciplineLessonTaskService serviceDLT, Guid dId, Guid? id = null)
        {
            InitializeComponent();
            _serviceDL = serviceDL;
            _serviceDLT = serviceDLT;

            if (id != Guid.Empty)
            {
                _id = id;
            }

            _dId = dId;

        }

        private void DisciplineLessonTaskSettingsForm_Load(object sender, EventArgs e)
        {
            comboBoxLessonType.ValueMember = "Value";
            comboBoxLessonType.DisplayMember = "Display";
            comboBoxLessonType.DataSource = Enum.GetValues(typeof(DisciplineLessonTypes));
            comboBoxLessonType.SelectedItem = LessonTypes.нд;

            if (_id.HasValue)
            {
                var result = _serviceDL.GetDisciplineLesson(new DisciplineLessonGetBindingModel { Id = _id.Value });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    Close();
                }
                var entity = result.Result;

                comboBoxLessonType.SelectedItem = entity.LessonType;
                textBoxTitle.Text = entity.Title;
                textBoxDescription.Text = entity.Description;
                textBoxCount.Text = entity.CountOfPairs.ToString();
                textBoxOrder.Text = entity.Order.ToString();
                dateTimePicker.Value = entity.Date.Value;

                LoadData();
            }
        }

        public void LoadData()
        {
            if (tabPageTasks.Controls.Count == 0)
            {
                var controlDLS = Container.Resolve<DisciplineLessonSettingsControl>();
                controlDLS.Dock = DockStyle.Fill;
                tabPageTasks.Controls.Add(controlDLS);
            }
            (tabPageTasks.Controls[0] as DisciplineLessonSettingsControl).LoadData(_id.Value);
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                return false;
            }
            if (comboBoxLessonType.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxOrder.Text))
            {
                return false;
            }
            int count = 0;
            if (!int.TryParse(textBoxCount.Text, out count))
            {
                return false;
            }
            int order = 0;
            if (!int.TryParse(textBoxOrder.Text, out order))
            {
                return false;
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
                    result = _serviceDL.CreateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        DisciplineId = _dId,
                        LessonType = comboBoxLessonType.Text,
                        Title = textBoxTitle.Text,
                        Description = textBoxDescription.Text,
                        CountOfPairs = Convert.ToInt32(textBoxCount.Text),
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        Date = dateTimePicker.Value
                    });
                }
                else
                {
                    result = _serviceDL.UpdateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        Id = _id.Value,
                        DisciplineId = _dId,
                        LessonType = comboBoxLessonType.Text,
                        Title = textBoxTitle.Text,
                        Description = textBoxDescription.Text,
                        CountOfPairs = Convert.ToInt32(textBoxCount.Text),
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        Date = dateTimePicker.Value
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
    }
}
