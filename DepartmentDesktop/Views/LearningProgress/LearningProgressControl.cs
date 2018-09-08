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
using DepartmentModel.Enums;
using DepartmentDesktop.Views.LearningProgress.DisciplineLesson;
using DepartmentService.BindingModels;

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
            var resultD = _process.GetDisciplines(new LearningProcessDisciplineBindingModel { UserId = AuthorizationService.UserId.Value });
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
