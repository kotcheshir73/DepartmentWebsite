using DepartmentDesktop.Views.LearningProgress.DisciplineLesson;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress
{
    public partial class LearningProgressControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IDisciplineLessonService _serviceDL;

        public LearningProgressControl(ILearningProgressProcess process, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _process = process;
            _serviceDL = serviceDL;
        }

        public void LoadData()
        {
            var resultAY = _serviceDL.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = null;

            int counter = 0;
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonTypes)))
            {
                var tabPage = new TabPage
                {
                    Location = new System.Drawing.Point(4, 22),
                    Name = "tabPage" + elem,
                    Size = new System.Drawing.Size(832, 326),
                    TabIndex = counter++,
                    Text = elem.ToString(),
                    UseVisualStyleBackColor = true
                };
                tabControl.Controls.Add(tabPage);
            }
        }

        private void comboBoxDisciplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDisciplines.SelectedValue != null)
            {
                foreach (var elem in Enum.GetValues(typeof(DisciplineLessonTypes)))
                {
                    var tabPage = tabControl.Controls.Find("tabPage" + elem, false).FirstOrDefault();
                    if (tabPage != null)
                    {
                        if (tabPage.Controls.Count == 0)
                        {
                            var controlDL = Container.Resolve<DisciplineLessonControl>();
                            controlDL.Dock = DockStyle.Fill;
                            tabPage.Controls.Add(controlDL);
                        }
                        (tabPage.Controls[0] as DisciplineLessonControl).LoadData(new Guid(comboBoxAcademicYear.SelectedValue.ToString()), new Guid(comboBoxDisciplines.SelectedValue.ToString()), 
                            elem.ToString());
                    }
                }
            }
        }

        private void comboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicYear.SelectedValue != null)
            {
                var resultD = _process.GetDisciplines(new LearningProcessDisciplineBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    UserId = AuthorizationService.UserId.Value
                });
                if (!resultD.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                    return;
                }

                comboBoxDisciplines.ValueMember = "Value";
                comboBoxDisciplines.DisplayMember = "Display";
                comboBoxDisciplines.DataSource = resultD.Result
                    .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
                comboBoxDisciplines.SelectedItem = null;
            }
        }
    }
}
