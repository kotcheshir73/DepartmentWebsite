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
    public partial class ControlCurrentLecturer : UserControl
    {
        private readonly IScheduleProcess _process;

        private bool switchFlag;

        public ControlCurrentLecturer(IScheduleProcess process)
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
                control.LoadData(ScheduleObjectLoad.Lecturers);

                Controls.Clear();
                Controls.Add(control);
                Controls.Add(panelTop);
            }
            else
            {
                TabControl tabControlLecturer = new TabControl
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

                tabControlLecturer.TabPages.Clear();

                var resultLecturers = _process.GetLecturers(new LecturerGetBindingModel { });
                if (!resultLecturers.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
                    return;
                }
                var lecturers = resultLecturers.Result.List;

                if (lecturers != null)
                {
                    for (int i = 0; i < lecturers.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPage" + lecturers[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = lecturers[i].FullName
                        };
                        tabControlLecturer.TabPages.Add(tabpage);
                        var control = new ControlCurrentDates(_process)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0}.", lecturers[i].FullName), new LoadScheduleBindingModel { LecturerId = lecturers[i].Id });
                        tabControlLecturer.TabPages[i].Controls.Add(control);
                    }
                }

                Controls.Clear();
                Controls.Add(tabControlLecturer);
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