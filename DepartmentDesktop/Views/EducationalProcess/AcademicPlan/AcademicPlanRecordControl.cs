using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Collections.Generic;
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanRecordControl : UserControl
	{
		private readonly IAcademicPlanRecordService _service;

		private readonly IEducationalProcessService _serviceEP;

		private Guid _apId;

		public AcademicPlanRecordControl(IAcademicPlanRecordService service, IEducationalProcessService serviceEP)
		{
			InitializeComponent();
			_service = service;
			_serviceEP = serviceEP;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "Disciplne", Title = "Дисциплина", Width = 200, Visible = true },
				new ColumnConfig { Name = "KindOfLoad", Title = "Вид нагрузки", Width = 150, Visible = true },
				new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 150, Visible = true },
				new ColumnConfig { Name = "Hours", Title = "Часы", Width = 100, Visible = true }
			};

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "LoadFromXMLToolStripMenuItem", "Загрузить из xml"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("LoadFromXMLToolStripMenuItem", LoadFromXMLToolStripMenuItem_Click);
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

		public void LoadData(Guid apId)
		{
			_apId = apId;
            standartControl.LoadPage();
        }

		private int LoadRecords(int pageNumber, int pageSize)
        {
			var result = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { PageNumber = pageNumber, PageSize = pageSize, AcademicPlanId = _apId });
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
					res.Disciplne,
					res.KindOfLoad,
					res.Semester,
					res.Hours
				);
            }
            return result.Result.MaxCount;
        }

		private void AddRecord()
		{
			var form = new AcademicPlanRecordForm(_service, _apId);
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
				var form = new AcademicPlanRecordForm(_service, _apId, id);
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
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[i].Value.ToString());
                        var result = _service.DeleteAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
                    }
                    standartControl.LoadPage();
                }
			}
		}

		private void LoadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
			{
				var result = _serviceEP.LoadFromXMLAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
				{
					Id = _apId,
					FileName = dialog.FileName
				});
				if (result.Succeeded)
				{
					LoadData(_apId);
				}
				else
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				}
			}
		}
	}
}
