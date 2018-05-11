using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.Administration.User
{
    public partial class UsersControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IUserService _service;

		public UsersControl(IUserService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "Login", Title = "Логин", Width = 200, Visible = true },
                new ColumnConfig { Name = "RoleType", Title = "Тип роли", Width = 200, Visible = true },
                new ColumnConfig { Name = "DateVisit", Title = "Дата посещения", Width = 300, Visible = true },
				new ColumnConfig { Name = "IsBanned", Title = "Забаннен", Width = 100, Visible = true }
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
					Width = column.Width ?? 0,
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
			var result = _service.GetUsers(new UserGetBindingModel { });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.Rows.Clear();
			foreach (var res in result.Result.List)
			{
				dataGridViewList.Rows.Add(new object[]
				{
					res.Id,
					res.Login,
                    res.RoleType,
					res.DateLastVisit?.ToShortDateString() ?? "",
					res.IsBanned ? "Да" : "Нет"
				});
			}
		}

		private void AddRecord()
        {
            var form = Container.Resolve<UserForm>(
                    new ParameterOverrides
                    {
                        { "id", Guid.Empty }
                    }
                    .OnType<UserForm>());
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadRecords();
			}
		}

		private void UpdRecord()
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
                Guid id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<UserForm>(
                        new ParameterOverrides
                        {
                            { "id", id }
                        }
                        .OnType<UserForm>());
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
                        Guid id = new Guid(dataGridViewList.SelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteUser(new UserGetBindingModel { Id = id });
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
