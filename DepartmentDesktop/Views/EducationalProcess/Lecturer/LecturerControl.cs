using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentDesktop.Models;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.Lecturer
{
	public partial class LecturerControl : UserControl
	{
		private readonly ILecturerService _service;

		public LecturerControl(ILecturerService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "LastName", Title = "Фамилия", Width = 100, Visible = true },
				new ColumnConfig { Name = "FirstName", Title = "Имя", Width = 100, Visible = true },
				new ColumnConfig { Name = "Patronymic", Title = "Отчество", Width = 100, Visible = true },
				new ColumnConfig { Name = "DateBirth", Title = "Дата рождения", Width = 150, Visible = true },
				new ColumnConfig { Name = "Email", Title = "Почта", Width = 100, Visible = true },
				new ColumnConfig { Name = "Post", Title = "Должность", Width = 100, Visible = true },
				new ColumnConfig { Name = "Rank", Title = "Звание", Width = 100, Visible = true },
				new ColumnConfig { Name = "MobileNumber", Title = "Мобильный номер", Width = 150, Visible = true },
				new ColumnConfig { Name = "Address", Title = "Адрес", Width = 100, Visible = true }
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
			var result = _service.GetLecturers(new LecturerGetBindingModel { });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.Rows.Clear();
			foreach (var res in result.Result)
			{
				dataGridViewList.Rows.Add(
					res.Id,
					res.LastName,
					res.FirstName,
					res.Patronymic,
					res.DateBirth.ToShortDateString(),
					res.Email,
					res.Post,
					res.Rank,
					res.MobileNumber,
					res.Address
				);
			}
		}

		private void AddRecord()
		{
			var form = new LecturerForm(_service);
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
				var form = new LecturerForm(_service, id);
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
						var result = _service.DeleteLecturer(new LecturerGetBindingModel { Id = id });
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
