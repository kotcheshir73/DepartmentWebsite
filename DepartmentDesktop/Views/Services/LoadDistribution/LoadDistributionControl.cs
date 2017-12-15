using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	public partial class LoadDistributionControl : UserControl
	{
		private readonly ILoadDistributionService _service;

		private readonly ILoadDistributionRecordService _serviceLDR;

		private readonly IEducationalProcessService _serviceEP;

		public LoadDistributionControl(ILoadDistributionService service, ILoadDistributionRecordService serviceLDR, IEducationalProcessService serviceEP)
		{
			InitializeComponent();
			_service = service;
			_serviceLDR = serviceLDR;
			_serviceEP = serviceEP;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "AcademicYear", Title = "Учебный год", Width = 100, Visible = true }
			};
			dataGridViewList.Columns.Clear();
			foreach (var column in columns)
			{
				dataGridViewList.Columns.Add(new DataGridViewTextBoxColumn
				{
					HeaderText = column.Title,
					Name = string.Format("Column{0}", column.Name),
					ReadOnly = true,
					Visible = column.Visible,
					Width = column.Width.HasValue ? column.Width.Value : 0,
					AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill
				});
			}
		}

		public void LoadData()
		{
			LoadRecords();
		}

		private void LoadRecords()
		{
			var result = _service.GetLoadDistributions(new LoadDistributionGetBindingModel { });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.Rows.Clear();
			foreach (var res in result.Result.List)
			{
				dataGridViewList.Rows.Add(
					res.Id,
					res.AcademicYear
				);
			}
		}

		private void AddRecord()
		{
			var form = new LoadDistributionForm(_service, _serviceLDR, _serviceEP);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadRecords();
			}
		}

		private void UpdRecord()
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
				long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
				var form = new LoadDistributionForm(_service, _serviceLDR, _serviceEP, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadRecords();
				}
			}
		}

		private void DelRecord()
		{
			if (dataGridViewList.SelectedRows.Count > 0)
			{
				if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					for (int i = 0; i < dataGridViewList.SelectedRows.Count; ++i)
					{
						long id = Convert.ToInt64(dataGridViewList.SelectedRows[i].Cells[0].Value);
						var result = _service.DeleteLoadDistribution(new LoadDistributionGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					LoadRecords();
				}
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			AddRecord();
		}

		private void toolStripButtonUpd_Click(object sender, EventArgs e)
		{
			UpdRecord();
		}

		private void toolStripButtonDel_Click(object sender, EventArgs e)
		{
			DelRecord();
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadRecords();
		}

		private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Insert:
					AddRecord();
					break;
				case Keys.Enter:
					UpdRecord();
					break;
				case Keys.Delete:
					DelRecord();
					break;
			}
		}

		private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			UpdRecord();
		}
	}
}
