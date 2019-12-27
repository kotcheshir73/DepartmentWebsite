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
    public partial class ControlCurrentDiscipline : UserControl
    {
        private readonly IScheduleProcess _process;

        private bool switchFlag;

        public ControlCurrentDiscipline(IScheduleProcess process)
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
                control.LoadData(ScheduleObjectLoad.Disciplines);

                Controls.Clear();
                Controls.Add(control);
                Controls.Add(panelTop);
            }
            else
            {
                TabControl tabControlDiscipline = new TabControl
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

                tabControlDiscipline.TabPages.Clear();

                var resultDisciplines = _process.GetDisciplines(new DisciplineGetBindingModel { });
                if (!resultDisciplines.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultDisciplines.Errors);
                    return;
                }
                var disciplines = resultDisciplines.Result.List;

                if (disciplines != null)
                {
                    for (int i = 0; i < disciplines.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPage" + disciplines[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = "Дисциплина " + disciplines[i].DisciplineShortName
                        };
                        tabControlDiscipline.TabPages.Add(tabpage);
                        var control = new ControlCurrentDates(_process)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0}.", disciplines[i].DisciplineName), new LoadScheduleBindingModel { DisciplineId = disciplines[i].Id });
                        tabControlDiscipline.TabPages[i].Controls.Add(control);
                    }
                }

                Controls.Clear();
                Controls.Add(tabControlDiscipline);
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