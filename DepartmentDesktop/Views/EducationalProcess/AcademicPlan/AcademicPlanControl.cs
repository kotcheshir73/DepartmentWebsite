using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Collections.Generic;
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanControl : UserControl
	{
		private readonly IAcademicPlanService _service;

		private readonly IAcademicPlanRecordService _serviceAPR;

		private readonly IEducationalProcessService _serviceEP;

		public AcademicPlanControl(IAcademicPlanService service, IAcademicPlanRecordService serviceAPR, IEducationalProcessService serviceEP)
		{
			InitializeComponent();
			_service = service;
			_serviceAPR = serviceAPR;
			_serviceEP = serviceEP;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "EducationDirection", Title = "Направление", Width = 100, Visible = true },
				new ColumnConfig { Name = "AcademicYear", Title = "Учебный год", Width = 100, Visible = true },
				new ColumnConfig { Name = "AcademicLevel", Title = "Уровень", Width = 150, Visible = true },
				new ColumnConfig { Name = "AcademicCourses", Title = "Курсы", Width = 150, Visible = true }
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
			var result = _service.GetAcademicPlans(new AcademicPlanGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
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
					res.EducationDirection,
					res.AcademicYear,
					res.AcademicLevel,
					res.AcademicCoursesStrings
				);
            }
            return result.Result.MaxCount;
        }

		private void AddRecord()
		{
			var form = new AcademicPlanForm(_service, _serviceAPR, _serviceEP);
			if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
		}

		private void UpdRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count == 1)
			{
				long id = Convert.ToInt64(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value);
				var form = new AcademicPlanForm(_service, _serviceAPR, _serviceEP, id);
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
						long id = Convert.ToInt64(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value);
						var result = _service.DeleteAcademicPlan(new AcademicPlanGetBindingModel { Id = id });
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
