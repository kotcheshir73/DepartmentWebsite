using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.IServices;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.Progress
{
    public partial class ProgressForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _serviceD;

        private readonly IDisciplineLessonService _serviceDL;

        private readonly IStudentGroupService _serviceSG;

        public ProgressForm(IDisciplineService serviceD, IStudentGroupService serviceSG, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _serviceD = serviceD;
            _serviceSG = serviceSG;
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

            var resultG = _serviceSG.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultG.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке студенческих групп возникла ошибка: ", resultG.Errors);
                return;
            }

            comboBoxDisciplines.ValueMember = "Value";
            comboBoxDisciplines.DisplayMember = "Display";
            comboBoxDisciplines.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName}).ToList();
            comboBoxDisciplines.SelectedItem = null;

            comboBoxStudentGroups.ValueMember = "Value";
            comboBoxStudentGroups.DisplayMember = "Display";
            comboBoxStudentGroups.DataSource = resultG.Result.List
                .Select(g => new { Value = g.Id, Display = g.GroupName}).ToList();
            comboBoxStudentGroups.SelectedItem = null;

            if (comboBoxDisciplines.SelectedItem!=null&&comboBoxStudentGroups.SelectedItem != null)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var discipline =_serviceD.GetDiscipline(new DisciplineGetBindingModel { Id = new Guid(comboBoxDisciplines.SelectedValue.ToString()) }) ;
            var lessons = _serviceDL.GetDisciplineLessons(new DisciplineLessonGetBindingModel { DisciplineId = discipline.Result.Id });
        }
    }
}
