using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanControl : UserControl
	{
		private readonly IAcademicPlanService _service;

		private readonly IAcademicPlanRecordService _serviceAPR;

		public AcademicPlanControl(IAcademicPlanService service, IAcademicPlanRecordService serviceAPR)
		{
			InitializeComponent();
			_service = service;
			_serviceAPR = serviceAPR;
		}

		public void LoadData()
		{
			var result = _service.GetAcademicPlans();
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.DataSource = result.Result;
			if (dataGridViewList.Columns.Count > 0)
			{
				dataGridViewList.Columns[0].Visible = false;
				dataGridViewList.Columns[1].Visible = false;
				dataGridViewList.Columns[2].HeaderText = "Направление";
				dataGridViewList.Columns[2].Width = 100;
				dataGridViewList.Columns[3].HeaderText = "Учебный год";
				dataGridViewList.Columns[3].Width = 100;
				dataGridViewList.Columns[4].HeaderText = "Урвень";
				dataGridViewList.Columns[4].Width = 150;
				dataGridViewList.Columns[5].Visible = false;
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			var form = new AcademicPlanForm(_service, _serviceAPR);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void toolStripButtonUpd_Click(object sender, EventArgs e)
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
				long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
				var form = new AcademicPlanForm(_service, _serviceAPR, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadData();
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
						var result = _service.DeleteAcademicPlan(new AcademicPlanGetBindingModel { Id = id });
						if (result.Succeeded)
						{
							LoadData();
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
			LoadData();
		}
	}
}
