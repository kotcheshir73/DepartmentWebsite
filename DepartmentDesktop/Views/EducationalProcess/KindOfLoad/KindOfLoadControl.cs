using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.KindOfLoad
{
	public partial class KindOfLoadControl : UserControl
	{
		private readonly IKindOfLoadService _service;

		public KindOfLoadControl(IKindOfLoadService service)
		{
			InitializeComponent();
			_service = service;
		}

		public void LoadData()
		{
			var result = _service.GetKindOfLoads();
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.DataSource = result.Result;
			if (dataGridViewList.Columns.Count > 0)
			{
				dataGridViewList.Columns[0].Visible = false;
				dataGridViewList.Columns[1].HeaderText = "Название";
				dataGridViewList.Columns[1].Width = 150;
				dataGridViewList.Columns[2].HeaderText = "Тип";
				dataGridViewList.Columns[2].Width = 200;
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			var form = new KindOfLoadForm(_service);
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
				var form = new KindOfLoadForm(_service, id);
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
						var result = _service.DeleteKindOfLoad(new KindOfLoadGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					LoadData();
				}
			}
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}
	}
}
