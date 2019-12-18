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
    public partial class ControlCurrentClassroom : UserControl
    {
        private readonly IScheduleProcess _process;

        private bool switchFlag;

        public ControlCurrentClassroom(IScheduleProcess process)
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
                control.LoadData(ScheduleObjectLoad.Classrooms);

                Controls.Clear();
                Controls.Add(control);
                Controls.Add(panelTop);
            }
            else
            {
                TabControl tabControlClassroom = new TabControl
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

                tabControlClassroom.TabPages.Clear();

                var resultClassrooms = _process.GetClassrooms(new ClassroomGetBindingModel { });
                if (!resultClassrooms.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
                    return;
                }
                var classrooms = resultClassrooms.Result.List;

                if (classrooms != null)
                {
                    for (int i = 0; i < classrooms.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPage" + classrooms[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = "Аудитория " + classrooms[i].Number
                        };
                        tabControlClassroom.TabPages.Add(tabpage);
                        var control = new ControlCurrentDates(_process)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0} аудитория.", classrooms[i].Number), new LoadScheduleBindingModel { ClassroomId = classrooms[i].Id });

                        tabControlClassroom.TabPages[i].Controls.Add(control);
                    }
                }

                Controls.Clear();
                Controls.Add(tabControlClassroom);
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