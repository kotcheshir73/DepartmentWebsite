using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Consultation
{
    public partial class ScheduleConsultationControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IConsultationRecordService _serviceCR;

        private ScheduleGetBindingModel _model;

        public ScheduleConsultationControl(IScheduleProcess process, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _process = process;
            _serviceCR = serviceCR;
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            _model = model;
        }

        private void LoadRecords()
        {
            var result = _serviceCR.GetConsultationSchedule(_model);
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            dataGridViewList.DataSource = result.Result;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].HeaderText = "Неделя";
                dataGridViewList.Columns[1].Width = 50;
                dataGridViewList.Columns[2].HeaderText = "День";
                dataGridViewList.Columns[2].Width = 50;
                dataGridViewList.Columns[3].HeaderText = "Пара";
                dataGridViewList.Columns[3].Width = 50;
                dataGridViewList.Columns[4].HeaderText = "Дата";
                dataGridViewList.Columns[4].Width = 100;
                dataGridViewList.Columns[5].HeaderText = "Дисциплина";
                dataGridViewList.Columns[5].Width = 150;
                dataGridViewList.Columns[6].HeaderText = "Преподаватель";
                dataGridViewList.Columns[6].Width = 150;
                dataGridViewList.Columns[7].HeaderText = "Группа";
                dataGridViewList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewList.Columns[8].HeaderText = "Аудитория";
                dataGridViewList.Columns[8].Width = 150;
            }
        }

        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new ScheduleConsultationRecordForm(_serviceCR, _process);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRecords();
            }
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                Guid id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                var form = new ScheduleConsultationRecordForm(_serviceCR, _process, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadRecords();
                }
            }
        }

        private void ToolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridViewList.SelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(dataGridViewList.SelectedRows[i].Cells[0].Value.ToString());
                        var result = _serviceCR.DeleteConsultationRecord(new ScheduleGetBindingModel { Id = id });
                        if (result.Succeeded)
                        {
                            LoadRecords();
                        }
                        else
						{
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
                    }
                }
            }
        }

        private void ToolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }
    }
}