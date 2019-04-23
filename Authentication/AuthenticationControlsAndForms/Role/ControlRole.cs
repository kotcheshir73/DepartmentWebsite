using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AuthenticationControlsAndForms.Role
{
    public partial class ControlRole : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IRoleService _service;

		public ControlRole(IRoleService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "RoleName", Title = "Название", Width = 300, Visible = true }
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
			var result = _service.GetRoles(new RoleGetBindingModel { });
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
					res.RoleName
				});
            }
            return result.Result.MaxCount;
        }

		private void AddRecord()
		{
			var form = Container.Resolve<FormRole>(
                    new ParameterOverrides
                    {
                        { "id", Guid.Empty }
                    }
                    .OnType<FormRole>());
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
                var form = Container.Resolve<FormRole>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<FormRole>());
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
						var result = _service.DeleteRole(new RoleGetBindingModel { Id = id });
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