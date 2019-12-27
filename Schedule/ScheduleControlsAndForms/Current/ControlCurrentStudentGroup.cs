using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using Enums;
using ScheduleControlsAndForms.BaseControls;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Current
{
    public partial class ControlCurrentStudentGroup : UserControl
    {
        private readonly IScheduleProcess _process;

        private bool switchFlag;

        public ControlCurrentStudentGroup(IScheduleProcess process)
        {
            InitializeComponent();
            _process = process;
            switchFlag = true;
        }

        public void LoadData()
        {
            if (switchFlag)
            {
                var control = new ControlCurrentObjects(_process)
                {
                    Dock = DockStyle.Fill
                };
                control.LoadData(ScheduleObjectLoad.StudentGroups);

                Controls.Clear();
                Controls.Add(control);
                Controls.Add(panelTop);
            }
            else
            {
                TabControl tabControlStudentGroup = new TabControl
                {
                    Alignment = TabAlignment.Left,
                    Dock = DockStyle.Fill,
                    Location = new Point(0, 33),
                    Margin = new Padding(0),
                    Multiline = true,
                    Name = "tabControlClassroom",
                    Padding = new Point(0, 0),
                    SelectedIndex = 0,
                    Size = new Size(800, 467),
                    TabIndex = 1
                };

                tabControlStudentGroup.TabPages.Clear();

                var resultStudentGroups = _process.GetStudentGroups(new StudentGroupGetBindingModel { });
                if (!resultStudentGroups.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
                    return;
                }
                var studentGroups = resultStudentGroups.Result.List;

                if (studentGroups != null)
                {
                    for (int i = 0; i < studentGroups.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPage" + studentGroups[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = studentGroups[i].GroupName
                        };
                        tabControlStudentGroup.TabPages.Add(tabpage);
                        var control = new ControlCurrentDates(_process)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("Группа {0}.", studentGroups[i].GroupName), new LoadScheduleBindingModel { StudentGroupId = studentGroups[i].Id });
                        tabControlStudentGroup.TabPages[i].Controls.Add(control);
                    }
                }

                Controls.Clear();
                Controls.Add(tabControlStudentGroup);
                Controls.Add(panelTop);
            }
        }

        private void ButtonSwitch_Click(object sender, System.EventArgs e)
        {
            switchFlag = !switchFlag;
            LoadData();
        }
    }
}