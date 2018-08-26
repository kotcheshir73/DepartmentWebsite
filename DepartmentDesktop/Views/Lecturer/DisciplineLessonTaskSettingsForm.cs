using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using DepartmentService.Services.StandartServices.EducationDirection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.Lecturer
{
    public partial class DisciplineLessonTaskSettingsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _serviceDL;

        private readonly IDisciplineLessonTaskService _serviceDLT;

        private readonly DisciplineLessonTaskVariantService _serviceDLTV;

        private Guid? _id = null;

        private Guid _dId;

        public DisciplineLessonTaskSettingsForm(IDisciplineLessonService serviceDL, IDisciplineLessonTaskService serviceDLT, DisciplineLessonTaskVariantService serviceDLTV, Guid dId, Guid? id = null)
        {
            InitializeComponent();
            _serviceDL = serviceDL;
            _serviceDLT = serviceDLT;
            _serviceDLTV = serviceDLTV;

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
            comboBoxLessonType.DataSource = Enum.GetValues(typeof(LessonTypes))
                .Cast<LessonTypes>().Select(p => new
                {
                    Display = Enum.GetName(typeof(LessonTypes), p),
                    Value = (int)p
                }).ToList();
            comboBoxLessonType.SelectedItem = null;


            LoadData();
        }

        public void LoadData()
        {

        }

        private bool CheckFill()
        {
            int count = 0;
            if (!int.TryParse(textBoxCountOfPairs.Text, out count))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTask.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDLDescription.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxMaxBall.Text))
            {
                int maxBall = 0;
                if (!int.TryParse(textBoxMaxBall.Text, out maxBall))
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
                ResultService resultDL;
                ResultService resultDLT;
                if (!_id.HasValue)
                {
                    resultDL = _serviceDL.CreateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        DisciplineId = _dId,
                        CountOfPairs = Convert.ToInt32(textBoxCountOfPairs.Text),
                        Description = textBoxDLDescription.Text,
                        Title = "title"
                    });
                    if (resultDL.Succeeded)
                    {
                        if (resultDL.Result != null)
                        {
                            if (resultDL.Result is Guid)
                            {
                                _id = (Guid)resultDL.Result;
                                LessonTypes lt;
                                Enum.TryParse<LessonTypes>(comboBoxLessonType.SelectedItem.ToString(), out lt);
                                resultDLT = _serviceDLT.CreateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                                {
                                    Description = textBoxTask.Text,
                                    DisciplineLessonId = _id.Value,
                                    //LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes),/*comboBoxLessonType.SelectedValue.ToString()*/ "лек"),
                                    LessonType = lt,
                                MaxBall = Convert.ToInt32(textBoxMaxBall.Text),
                                    DisciplineLessonName = "name",
                                    Order = 1
                                });
                            }
                        }
                    }
                    else
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", resultDL.Errors);
                        return false;
                    }
                }
                else
                {
                    resultDL = _serviceDL.UpdateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        Id = _id.Value,
                        CountOfPairs = Convert.ToInt32(textBoxCountOfPairs.Text),
                        Description = textBoxDLDescription.Text,
                        Title = "title"
                    });

                    // как-то вытащить задание по этому занятию
                    var dlt = _serviceDLT.GetDisciplineLessonTask(new DisciplineLessonTaskGetBindingModel { DisciplineLessonId = _id.Value });
                    resultDLT = _serviceDLT.UpdateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                    {
                        Id = dlt.Result.Id,
                        Description = textBoxTask.Text,
                        MaxBall = Convert.ToInt32(textBoxMaxBall.Text)
                    });

                    if (!resultDLT.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", resultDLT.Errors);
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<DisciplineLessonTaskVariantSettingsForm>(new ParameterOverrides
                {
                    { "id", Guid.Empty}
                }
                .OnType<DisciplineLessonTaskVariantSettingsForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                Guid id = new Guid(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<DisciplineLessonTaskVariantSettingsForm>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<DisciplineLessonTaskVariantSettingsForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView.SelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(dataGridView.SelectedRows[i].Cells[0].Value.ToString());
                        var result = _serviceDLTV.DeleteDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    LoadData();
                }
            }
        }
    }
}
