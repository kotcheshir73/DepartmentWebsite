using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using Tools;
using DatabaseContext;

namespace AcademicYearControlsAndForms.Services
{
    public partial class ControlLecturerDisciplineTimeDistributions : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicYearService _serviceAY;

        private readonly IAcademicYearProcess _process;

        private bool notLoading;

        public ControlLecturerDisciplineTimeDistributions(IAcademicYearService serviceAY, IAcademicYearProcess process)
        {
            InitializeComponent();
            _serviceAY = serviceAY;
            _process = process;
        }

        public void LoadData()
        {
            var resultAY = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            notLoading = true;
            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = null;
            notLoading = false;
        }

        private void ComboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicYear.SelectedValue != null && !notLoading)
            {
                LoadControls();
            }
        }

        private void LoadControls()
        {
            Guid id = new Guid(comboBoxAcademicYear.SelectedValue.ToString());
            var lectDiscTD = _process.GetLecturerDisciplineTimeDistributions(new LecturerDisciplineTimeDistributionsBindingModel
            {
                AcademicYearId = id,
                UserId = DepartmentUserManager.UserId.Value
            });
            if (!lectDiscTD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке расчасовок возникла ошибка: ", lectDiscTD.Errors);
                return;
            }
            tabControl.TabPages.Clear();
            int counter = 0;
            foreach(var elem in lectDiscTD.Result)
            {
                var tabPage = new TabPage
                {
                    Location = new Point(4, 22),
                    Name = $"tabPage{counter}",
                    Padding = new Padding(3),
                    Size = new Size(1155, 510),
                    TabIndex = counter++,
                    Text = $"{elem.Discipline} {elem.StudentGroup} {elem.Semestr}",
                    UseVisualStyleBackColor = true
                };
                ControlLecturerDisciplineTimeDistributionElement control = new ControlLecturerDisciplineTimeDistributionElement(_process, elem)
                {
                    Dock = DockStyle.Fill
                };
                tabPage.Controls.Add(control);

                tabControl.Controls.Add(tabPage);
            }
        }
    }
}
