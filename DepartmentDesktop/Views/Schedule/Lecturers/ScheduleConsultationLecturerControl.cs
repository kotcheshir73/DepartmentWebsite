using DepartmentDesktop.Views.Schedule.Consultation;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Lecturers
{
    public partial class ScheduleConsultationLecturerControl : UserControl
	{
		private readonly IScheduleService _service;

		private readonly IConsultationRecordService _serviceCR;

		private long _lecturerID;

		private string _lectureFullName;

		public ScheduleConsultationLecturerControl(IScheduleService service, IConsultationRecordService serviceCR)
		{
			InitializeComponent();
			_service = service;
			_serviceCR = serviceCR;
		}

		public void LoadData(long lecturerID, string lectureFullName)
		{
			_lecturerID = lecturerID;
			_lectureFullName = lectureFullName;
			var result = _serviceCR.GetConsultationSchedule(new ScheduleGetBindingModel { LecturerId = _lecturerID });
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
				dataGridViewList.Columns[7].Visible = false;
				dataGridViewList.Columns[8].HeaderText = "Аудитория";
				dataGridViewList.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			var form = new ScheduleConsultationRecordForm(_serviceCR, _service);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_lecturerID, _lectureFullName);
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
					LoadData(_lecturerID, _lectureFullName);
				}
			}
		}

		private void toolStripButtonDel_Click(object sender, EventArgs e)
		{
			//TODO
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadData(_lecturerID, _lectureFullName);
		}
	}
}
