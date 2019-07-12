using ControlsAndForms.Messangers;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace LearningProgressControlsAndForms.Services
{
    public partial class FormFillGroup : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IDisciplineStudentRecordService _service;

        private Guid? _dId = null;

        private Guid? _sgId = null;

        private Semesters _semester = Semesters.Первый;

        public FormFillGroup(ILearningProgressProcess process, IDisciplineStudentRecordService service, Guid? dId = null, Guid? sgId = null, Semesters semester = Semesters.Первый)
        {
            InitializeComponent();
            _process = process;
            _service = service;
            _dId = dId;
            _sgId = sgId;
            _semester = semester;
        }

        private void FormFillGroup_Load(object sender, EventArgs e)
        {
            var result = _process.GetDisciplineStudentRecordsForFill(new DisciplineStudentRecordsForFillBindingModel
            {
                DisciplineId = _dId.Value,
                StudentGroupId = _sgId.Value,
                Semester = _semester
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
            }

            dataGridViewStudents.DataSource = result.Result;

            dataGridViewStudents.Columns[0].Visible = false;
            dataGridViewStudents.Columns[1].Visible = false;
            dataGridViewStudents.Columns[2].Visible = false;
            dataGridViewStudents.Columns[3].HeaderText = "Дисциплина";
            dataGridViewStudents.Columns[3].ReadOnly = true;
            dataGridViewStudents.Columns[4].HeaderText = "Группа";
            dataGridViewStudents.Columns[4].ReadOnly = true;
            dataGridViewStudents.Columns[5].HeaderText = "Семестр";
            dataGridViewStudents.Columns[5].ReadOnly = true;
            dataGridViewStudents.Columns[6].HeaderText = "Студент";
            dataGridViewStudents.Columns[6].ReadOnly = true;
            dataGridViewStudents.Columns[7].HeaderText = "Вариант";
            dataGridViewStudents.Columns[8].HeaderText = "Подгруппа";
            dataGridViewStudents.Columns[9].Visible = false;
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выполнить сохранение?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
                {
                    var result = _service.UpdateDisciplineStudentRecord(new DisciplineStudentRecordSetBindingModel
                    {
                        Id = new Guid(dataGridViewStudents.Rows[i].Cells[9].Value.ToString()),
                        DisciplineId = _dId.Value,
                        StudentId = new Guid(dataGridViewStudents.Rows[i].Cells[2].Value.ToString()),
                        Semester = _semester.ToString(),
                        Variant = dataGridViewStudents.Rows[i].Cells[7].Value.ToString(),
                        SubGroup = Convert.ToInt32(dataGridViewStudents.Rows[i].Cells[8].Value)
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