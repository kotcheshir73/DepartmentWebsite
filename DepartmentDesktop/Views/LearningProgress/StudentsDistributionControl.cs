using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity.Attributes;
using Unity;
using DepartmentService.IServices;

namespace DepartmentDesktop.Views.LearningProgress
{
    public partial class StudentsDistributionControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicYearService _serviceAY;

        private readonly IEducationDirectionService _serviceED;
        
        public StudentsDistributionControl(IAcademicYearService serviceAY, IEducationDirectionService serviceED)
        {
            InitializeComponent();
            _serviceAY = serviceAY;
            _serviceED = serviceED;
        }

        public void LoadData()
        {
            var resultAY = _serviceAY.GetAcademicYears( new DepartmentService.BindingModels.AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxDisciplines.ValueMember = "Value";
            comboBoxDisciplines.DisplayMember = "Display";
            comboBoxDisciplines.DataSource = resultAY.Result.List.Select(y => new {Value = y.Id, Display = y.Title});
            comboBoxDisciplines.SelectedItem = null;

            var resultED = _serviceED.GetEducationDirections(new DepartmentService.BindingModels.EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List.Select(y => new { Value = y.Id, Display = y.Title });
            comboBoxEducationDirection.SelectedItem = null;
        }
    }
}
