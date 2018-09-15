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
    public partial class AttendanceControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        private readonly IStudentGroupService _serviceSG;

        public AttendanceControl(ILearningProgressProcess process, IStudentGroupService serviceSG)
        {
            InitializeComponent();
            _process = process;
            _serviceSG = serviceSG;
        }

        public void LoadData()
        {
            comboBoxGroups.Enabled = false;
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
            comboBoxGroups.Enabled = true;
            var resultG = _serviceSG.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultG.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultG.Errors);
                return;
            }

            comboBoxGroups.ValueMember = "Value";
            comboBoxGroups.DisplayMember = "Display";
            comboBoxGroups.DataSource = resultG.Result.List
                .Select(g => new { Value = g.Id, Display = g.GroupName }).ToList();
            comboBoxGroups.SelectedItem = null;

        }

        private void comboBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGroups.SelectedIndex > -1)
            {
                //foreach (var elem in Enum.GetValues(typeof(DisciplineLessonTypes)))
                //{
                //    var tabPage = tabControl.Controls.Find("tabPage" + elem, false).FirstOrDefault();
                //    if (tabPage != null)
                //    {
                //        if (tabPage.Controls.Count == 0)
                //        {
                //            var controlDL = Container.Resolve<DisciplineLessonAttendanceControl>();
                //            controlDL.Dock = DockStyle.Fill;
                //            tabPage.Controls.Add(controlDL);
                //        }
                //        (tabPage.Controls[0] as DisciplineLessonAttendanceControl).LoadData(new Guid(comboBoxDisciplines.SelectedValue.ToString()), elem.ToString());
                //    }
                //}
            }
        }
    }
}
