using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace TicketViews.Views.ExaminationTemplate
{
    public partial class ExaminationTemplateControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateService _service;

        public ExaminationTemplateControl(IExaminationTemplateService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "DisciplneName", Title = "Название дисциплины", Width = 200, Visible = true },
                new ColumnConfig { Name = "EducationDirectionName", Title = "Направление", Width = 200, Visible = true },
                new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 150, Visible = true },
                new ColumnConfig { Name = "ExaminationTemplateName", Title = "Название экзамена", Width = 200, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartListControl.Configurate(columns, hideToolStripButtons);

            standartListControl.GetPageAddEvent(LoadRecords);
            standartListControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartListControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartListControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartListControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartListControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) =>
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
            });
        }

        public void LoadData()
        {
            standartListControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetExaminationTemplates(new ExaminationTemplateGetBindingModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartListControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartListControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.DisciplneName,
                    res.EducationDirectionName,
                    res.Semester,
                    res.ExaminationTemplateName
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<ExaminationTemplateForm>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<ExaminationTemplateForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartListControl.LoadPage();
            }
        }

        private void UpdRecord()
        {
            if (standartListControl.GetDataGridViewSelectedRows.Count == 1)
            {
                Guid id = new Guid(standartListControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<ExaminationTemplateForm>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<ExaminationTemplateForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartListControl.LoadPage();
                }
            }
        }

        private void DelRecord()
        {
            if (standartListControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartListControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartListControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteExaminationTemplate(new ExaminationTemplateGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartListControl.LoadPage();
                }
            }
        }
    }
}