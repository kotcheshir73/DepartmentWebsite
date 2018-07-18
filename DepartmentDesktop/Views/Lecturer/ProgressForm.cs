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
                .Select(d => new { Value = d.Id, Display = d.DisciplineName}).ToList();
            comboBoxDisciplines.SelectedItem = null;

            if (comboBoxDisciplines.SelectedItem!=null)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var discipline =_serviceD.GetDiscipline(new DisciplineGetBindingModel { Id = new Guid(comboBoxDisciplines.SelectedValue.ToString()) }) ;
            var lessons = _serviceDL.GetDisciplineLessons(new DisciplineLessonGetBindingModel { DisciplineId = discipline.Result.Id });
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

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
    }
}
