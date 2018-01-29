using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Collections.Generic;
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear
{
    public partial class AcademicYearControl : UserControl
    {
        private readonly IAcademicYearService _service;

        private readonly IAcademicPlanService _serviceAP;

        private readonly ITimeNormService _serviceTM;

        private readonly IContingentService _serviceC;

        private readonly ISeasonDatesService _serviceSD;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IEducationalProcessService _serviceEP;

        public AcademicYearControl(IAcademicYearService service, IAcademicPlanService serviceAP, ITimeNormService serviceTM, IContingentService serviceC, 
            ISeasonDatesService serviceSD, IAcademicPlanRecordService serviceAPR, IEducationalProcessService serviceEP)
        {
            InitializeComponent();
            _service = service;
            _serviceAP = serviceAP;
            _serviceTM = serviceTM;
            _serviceC = serviceC;
            _serviceSD = serviceSD;
            _serviceAPR = serviceAPR;
            _serviceEP = serviceEP;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Title", Title = "Название", Width = 200, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) =>
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
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetAcademicYears(new AcademicYearGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.Title
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = new AcademicYearForm(_service, _serviceAP, _serviceTM, _serviceC, _serviceSD, _serviceAPR, _serviceEP);
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
                var form = new AcademicYearForm(_service, _serviceAP, _serviceTM, _serviceC, _serviceSD, _serviceAPR, _serviceEP, id);
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
                        var result = _service.DeleteAcademicYear(new AcademicYearGetBindingModel { Id = id });
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
