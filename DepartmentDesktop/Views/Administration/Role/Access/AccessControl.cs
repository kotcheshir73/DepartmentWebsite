using AuthenticationServiceInterfaces.BindingModels;
using AuthenticationServiceInterfaces.Interfaces;
using DepartmentDesktop.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DepartmentDesktop.Views.Administration.Access
{
    public partial class AccessControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAccessService _service;

        private Guid _roleId;

        public AccessControl(IAccessService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Operation", Title = "Операция", Width = 300, Visible = true },
                new ColumnConfig { Name = "AccessType", Title = "Доступ", Width = 100, Visible = true }
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

        public void LoadData(Guid roleId)
        {
            _roleId = roleId;
            LoadRecords();
        }

        private void LoadRecords()
        {
            var result = _service.GetAccesses(new AccessGetBindingModel { RoleId = _roleId });
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
                    res.Operation,
                    res.AccessType
                });
            }
        }

        private void AddRecord()
        {
            var form = Container.Resolve<AccessForm>(
                new ParameterOverrides
                {
                    { "roleId", _roleId },
                    { "id", Guid.Empty }
                }
                .OnType<AccessForm>());
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
                var form = Container.Resolve<AccessForm>(
                    new ParameterOverrides
                    {
                        { "roleId", _roleId },
                        { "id", id }
                    }
                    .OnType<AccessForm>());
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
                        var result = _service.DeleteAccess(new AccessGetBindingModel { Id = id });
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
