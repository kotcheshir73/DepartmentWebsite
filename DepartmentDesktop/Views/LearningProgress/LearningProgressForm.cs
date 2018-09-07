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
    public partial class LearningProgressForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _serviceD;

        private readonly IDisciplineLessonService _serviceDL;

        public LearningProgressForm(IDisciplineService serviceD, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _serviceD = serviceD;
            _serviceDL = serviceDL;
        }

        private void LearningProgressForm_Load(object sender, EventArgs e)
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
            if (comboBoxDisciplines.SelectedIndex > 0)
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
                        (tabPage.Controls[0] as DisciplineLessonControl).LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()), elem.ToString());
                    }
                }
            }
        }
    }
}
