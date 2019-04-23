using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AuthenticationControlsAndForms.User
{
    public partial class ControlUser : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IUserService _service;

		public ControlUser(IUserService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "Login", Title = "Логин", Width = 200, Visible = true },
                new ColumnConfig { Name = "DateVisit", Title = "Дата посещения", Width = 300, Visible = true },
				new ColumnConfig { Name = "IsBanned", Title = "Забаннен", Width = 100, Visible = true }
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
			var result = _service.GetUsers(new UserGetBindingModel { });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return -1;
			}
            standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
                standartControl.GetDataGridViewRows.Add(new object[]
				{
					res.Id,
					res.Login,
					res.DateLastVisit?.ToShortDateString() ?? "",
					res.IsBanned ? "Да" : "Нет"
				});
            }
            return result.Result.MaxCount;
        }

		private void AddRecord()
        {
            var form = Container.Resolve<FormUser>(
                    new ParameterOverrides
                    {
                        { "id", Guid.Empty }
                    }
                    .OnType<FormUser>());
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
                var form = Container.Resolve<FormUser>(
                        new ParameterOverrides
                        {
                            { "id", id }
                        }
                        .OnType<FormUser>());
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
                        var result = _service.DeleteUser(new UserGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
                    }
                    standartControl.LoadPage();
                }
			}
		}
	}
}