using ControlsAndForms.Messangers;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace LearningProgressControlsAndForms.Services
{
    public partial class FormFillStudents : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedStudentService _service;

        private readonly ILearningProgressProcess _process;

        private Guid? _dlcId = null;

        private Guid? _sgId = null;

        public FormFillStudents(IDisciplineLessonConductedStudentService service, ILearningProgressProcess process, Guid? dlcId = null, Guid? sgId = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _dlcId = dlcId;
            _sgId = sgId;
        }

        private void FormFillStudents_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonStudentStatus)))
            {
                ColumnStatus.Items.Add(elem.ToString());
            }

            var result = _process.GetDisciplineLessonConductedStudentsForFill(new DisciplineLessonConductedStudentsForFillBindingModel
            {
                DisciplineLessonConductedId = _dlcId.Value,
                StudentGroupId = _sgId.Value
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
            }

            dataGridViewStudents.Rows.Clear();

            for (int i = 0; i < result.Result.Count; ++i)
            {
                dataGridViewStudents.Rows.Add(new object[] {
                    result.Result[i].Id,
                    result.Result[i].DisciplineLessonConductedId,
                    result.Result[i].StudentId,
                    result.Result[i].Student,
                    result.Result[i].Status.ToString(),
                    result.Result[i].Ball,
                    result.Result[i].Comment
                });
            }
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выполнить сохранение?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
                {
                    double? ball = null;
                    if (dataGridViewStudents.Rows[i].Cells[5].Value != null)
                    {
                        string tempVal = dataGridViewStudents.Rows[i].Cells[5].Value.ToString();
                        if (double.TryParse(tempVal, out double temp))
                        {
                            ball = temp;
                        }
                        else if (tempVal.Contains(","))
                        {
                            tempVal = tempVal.Replace(",", ".");
                            if (double.TryParse(tempVal, out temp))
                            {
                                ball = temp;
                            }
                        }
                        else if (tempVal.Contains("."))
                        {
                            tempVal = tempVal.Replace(".", ",");
                            if (double.TryParse(tempVal, out temp))
                            {
                                ball = temp;
                            }
                        }
                    }
                    var result = _service.UpdateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
                    {
                        Id = new Guid(dataGridViewStudents.Rows[i].Cells[0].Value.ToString()),
                        DisciplineLessonConductedId = _dlcId.Value,
                        StudentId = new Guid(dataGridViewStudents.Rows[i].Cells[2].Value.ToString()),
                        Status = dataGridViewStudents.Rows[i].Cells[4].Value.ToString(),
                        Ball = dataGridViewStudents.Rows[i].Cells[5].Value == null ? null : ball,
                        Comment = dataGridViewStudents.Rows[i].Cells[6].Value?.ToString() ?? ""
                    });
                    if (!result.Succeeded)
                    {
                        ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                        return;
                    }
                }
                MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}