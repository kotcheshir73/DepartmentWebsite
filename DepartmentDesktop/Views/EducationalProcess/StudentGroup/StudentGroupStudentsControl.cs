﻿using System;
using System.Text;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupStudentsControl : UserControl
    {
        private readonly IStudentService _service;

        private long _studentGroupId;

        public StudentGroupStudentsControl(IStudentService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData(long studentGroupId)
        {
            _studentGroupId = studentGroupId;
            var list = _service.GetStudents(new StudentGetBindingModel { StudentGroupId = studentGroupId });
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].HeaderText = "Номер зачетки";
                dataGridViewList.Columns[0].Width = 150;
                dataGridViewList.Columns[1].HeaderText = "Фамилия";
                dataGridViewList.Columns[1].Width = 150;
                dataGridViewList.Columns[2].HeaderText = "Имя";
                dataGridViewList.Columns[2].Width = 150;
                dataGridViewList.Columns[3].HeaderText = "Отчество";
                dataGridViewList.Columns[3].Width = 150;
                dataGridViewList.Columns[4].Visible = false;
                dataGridViewList.Columns[5].Visible = false;
                dataGridViewList.Columns[6].Visible = false;
                dataGridViewList.Columns[7].HeaderText = "Описание";
                dataGridViewList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new Student.StudentForm(_service);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(_studentGroupId);
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                string id = dataGridViewList.SelectedRows[0].Cells[0].Value.ToString();
                var form = new Student.StudentForm(_service, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(_studentGroupId);
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
                        string id = dataGridViewList.SelectedRows[0].Cells[0].Value.ToString();
                        var result = _service.DeleteStudent(new StudentGetBindingModel { NumberOfBook = id });
                        if (result.Succeeded)
                        {
                            LoadData(_studentGroupId);
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
            LoadData(_studentGroupId);
        }
    }
}
