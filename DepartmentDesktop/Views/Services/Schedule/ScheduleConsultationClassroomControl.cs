using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DepartmentService.ViewModels;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleConsultationClassroomControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IConsultationRecordService _serviceCR;

        private string _classroomID;

        private SeasonDatesViewModel _dates;

        public ScheduleConsultationClassroomControl(IScheduleService service, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceCR = serviceCR;
        }

        public void LoadData(string classroomID)
        {
            var list = _service.GetScheduleConsultation(new ScheduleConsultationBindingModel { ClassroomId = classroomID });
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
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
                dataGridViewList.Columns[7].Width = 50;
                dataGridViewList.Columns[8].HeaderText = "Аудитория";
                dataGridViewList.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new ScheduleConsultationRecordForm(_serviceCR);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(_classroomID);
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var form = new ScheduleConsultationRecordForm(_serviceCR, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(_classroomID);
                }
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridViewList.SelectedRows.Count; ++i)
                    {
                        long id = Convert.ToInt64(dataGridViewList.SelectedRows[i].Cells[0].Value);
                        var result = _serviceCR.DeleteConsultationRecord(new ConsultationRecordGetBindingModel { Id = id });
                        if (result.Succeeded)
                        {
                            LoadData(_classroomID);
                        }
                        else
                        {
                            StringBuilder strRes = new StringBuilder();
                            foreach (var err in result.Errors)
                            {
                                strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                            }
                            MessageBox.Show("При сохранении возникла ошибка: " + strRes.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadData(_classroomID);
        }
    }
}
