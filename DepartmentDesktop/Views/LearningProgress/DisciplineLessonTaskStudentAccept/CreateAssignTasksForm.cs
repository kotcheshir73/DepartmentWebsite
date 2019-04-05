using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonTaskStudentAccept
{
    public partial class CreateAssignTasksForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IDisciplineLessonTaskStudentAcceptService _service;

        private Guid _dlId;

        private Guid _sgId;

        public CreateAssignTasksForm(ILearningProgressProcess process, IDisciplineLessonTaskStudentAcceptService service, Guid dlId, Guid sgId)
        {
            InitializeComponent();
            _process = process;
            _service = service;
            _dlId = dlId;
            _sgId = sgId;
        }

        private void CreateAssignTasksForm_Load(object sender, EventArgs e)
        {
            var resultDLT = _service.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { DisciplineLessonId = _dlId });
            if (!resultDLT.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result.List
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedItem = null;
        }

        private void buttonForm_Click(object sender, EventArgs e)
        {
            if (comboBoxDisciplineLessonTask.SelectedValue != null)
            {
                var result = _process.GetDisciplineLessonTaskStudentAcceptForForm(new DisciplineLessonTaskStudentAcceptForFormBindingModel
                {
                    StudentGroupId = _sgId,
                    DisciplineLessonTaskId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString()),
                    DateAccept = dateTimePickerDateAccept.Value
                });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }

                for (int i = 0; i < result.Result.Count; ++i)
                {
                    dataGridViewAccepts.Rows.Add(new object[] { result.Result[i].Id, result.Result[i].Student, result.Result[i].Task, result.Result[i].Comment });
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выполнить сохранение?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var list = new List<DisciplineLessonTaskStudentAcceptUpdateBindingModel>();
                for (int i = 0; i < dataGridViewAccepts.Rows.Count; ++i)
                {
                    list.Add(new DisciplineLessonTaskStudentAcceptUpdateBindingModel
                    {
                        DisciplineLessonTaskStudentAcceptTaskId = new Guid(dataGridViewAccepts.Rows[i].Cells[0].Value.ToString()),
                        Task = dataGridViewAccepts.Rows[i].Cells[2].Value.ToString(),
                        Comment = dataGridViewAccepts.Rows[i].Cells[3].Value.ToString()
                    });
                }
                var result = _process.SetDisciplineLessonTaskStudentAccept(list);
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return;
                }
                MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
