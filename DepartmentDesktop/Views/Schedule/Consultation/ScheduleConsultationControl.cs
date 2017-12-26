﻿using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Consultation
{
    public partial class ScheduleConsultationControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IConsultationRecordService _serviceCR;

        private ScheduleGetBindingModel _model;

        public ScheduleConsultationControl(IScheduleService service, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
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
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new ScheduleConsultationRecordForm(_serviceCR, _service);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRecords();
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var form = new ScheduleConsultationRecordForm(_serviceCR, _service, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadRecords();
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
                        var result = _serviceCR.DeleteConsultationRecord(new ScheduleGetBindingModel { Id = id });
                        if (result.Succeeded)
                        {
                            LoadRecords();
                        }
                        else
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
                    }
                }
            }
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }
    }
}
