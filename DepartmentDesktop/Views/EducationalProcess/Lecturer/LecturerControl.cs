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
				new ColumnConfig { Name = "DateBirth", Title = "Дата рождения", Width = 100, Visible = true },
				new ColumnConfig { Name = "Email", Title = "Почта", Width = 200, Visible = true },
				new ColumnConfig { Name = "Post", Title = "Должность", Width = 150, Visible = true },
                new ColumnConfig { Name = "LecturerPost", Title = "Должность", Width = 150, Visible = true },
                new ColumnConfig { Name = "Rank", Title = "Звание", Width = 100, Visible = true },
                new ColumnConfig { Name = "Rank2", Title = "Звание", Width = 100, Visible = true },
                new ColumnConfig { Name = "MobileNumber", Title = "Мобильный номер", Width = 150, Visible = true },
				new ColumnConfig { Name = "Address", Title = "Адрес", Width = 300, Visible = true }
			};

			List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

			standartControl.Configurate(columns, hideToolStripButtons);

			standartControl.GetPageAddEvent(LoadRecords);
			standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
			standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
			standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
			standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
			standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) => {
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
			});
		}

		public void LoadData()
		{
			standartControl.LoadPage();
		}

		private int LoadRecords(int pageNumber, int pageSize)
		{
			var result = _service.GetLecturers(new LecturerGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return - 1;
			}
			standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
				standartControl.GetDataGridViewRows.Add(
					res.Id,
					res.LastName,
					res.FirstName,
					res.Patronymic,
					res.DateBirth.ToShortDateString(),
					res.Email,
					res.Post,
                    res.LecturerPost,
                    res.Rank,
                    res.Rank2,
                    res.MobileNumber,
					res.Address
				);
			}
			return result.Result.MaxCount;
		}

		private void AddRecord()
		{
			var form = new LecturerForm(_service);
			if (form.ShowDialog() == DialogResult.OK)
			{
				standartControl.LoadPage();
			}
		}

		private void UpdRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count == 1)
			{
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = new LecturerForm(_service, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					standartControl.LoadPage();
				}
			}
		}

		private void DelRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count > 0)
			{
				if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
					{
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteLecturer(new LecturerGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					standartControl.LoadPage();
				}
			}
		}
	}
}
