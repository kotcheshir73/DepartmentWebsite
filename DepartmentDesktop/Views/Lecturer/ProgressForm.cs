using DepartmentDesktop.Views.Lecturer;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.IServices;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.EducationalProcess.Progress
{
    public partial class ProgressForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _serviceD;

        private readonly IDisciplineLessonService _serviceDL;

        public ProgressForm(IDisciplineService serviceD, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _serviceD = serviceD;
            _serviceDL = serviceDL;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            var resultD = _serviceD.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            comboBoxDisciplines.ValueMember = "Value";
            comboBoxDisciplines.DisplayMember = "Display";
            comboBoxDisciplines.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDisciplines.SelectedItem = null;
        }

        private void LoadData()
        {
            var discipline = _serviceD.GetDiscipline(new DisciplineGetBindingModel { Id = new Guid(comboBoxDisciplines.SelectedValue.ToString()) });

            if (!discipline.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплины возникла ошибка: ", discipline.Errors);
                return;
            }

            var lessons = _serviceDL.GetDisciplineLessons(new DisciplineLessonGetBindingModel { DisciplineId = discipline.Result.Id });

            if (!lessons.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке занятий возникла ошибка: ", lessons.Errors);
                return;
            }

            dataGridViewLectures.Rows.Clear();
            dataGridViewCourseWorks.Rows.Clear();
            dataGridViewLabs.Rows.Clear();
            dataGridViewPractices.Rows.Clear();

            foreach (var les in lessons.Result.List)
            {
                if (les.LessonType == (LessonTypes)Enum.Parse(typeof(LessonTypes), "1"))
                {
                    // забрать лист заданий для этого занятия??
                    dataGridViewLectures.Rows.Add(
                        null,
                        les.Title,
                        les.Date,
                        les.CountOfPairs,
                        null
                        );
                }
                if (les.LessonType == (LessonTypes)Enum.Parse(typeof(LessonTypes), "3"))
                {
                    // забрать лист заданий для этого занятия??
                    dataGridViewLabs.Rows.Add(
                        null,
                        les.Title,
                        les.Date,
                        les.CountOfPairs,
                        null
                        );
                }
                if (les.LessonType == (LessonTypes)Enum.Parse(typeof(LessonTypes), "2"))
                {
                    // забрать лист заданий для этого занятия??
                    dataGridViewPractices.Rows.Add(
                        null,
                        les.Title,
                        les.Date,
                        les.CountOfPairs,
                        null
                        );
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<DisciplineLessonTaskSettingsForm>(new ParameterOverrides
                {
                    {"id", Guid.Empty},
                    {"dId", new Guid(comboBoxDisciplines.SelectedValue.ToString()) }
                }
                .OnType<DisciplineLessonTaskSettingsForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourseWorks.SelectedRows.Count == 1)
            {
                Guid id = new Guid(dataGridViewCourseWorks.SelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<DisciplineLessonTaskSettingsForm>(
                    new ParameterOverrides
                    {
                        { "id", id },
                        {"dId", new Guid(comboBoxDisciplines.SelectedValue.ToString()) }
                    }
                    .OnType<DisciplineLessonTaskSettingsForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourseWorks.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridViewCourseWorks.SelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(dataGridViewCourseWorks.SelectedRows[i].Cells[0].Value.ToString());
                        var result = _serviceDL.DeleteDisciplineLesson(new DisciplineLessonGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    LoadData();
                }
            }
        }

        private bool Save()
        {
            return false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxDisciplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDisciplines.SelectedItem != null)
            {
                LoadData();
            }
        }
    }
}
