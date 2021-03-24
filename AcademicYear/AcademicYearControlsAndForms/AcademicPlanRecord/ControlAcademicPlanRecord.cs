using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using Enums;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.AcademicPlanRecord
{
    public partial class ControlAcademicPlanRecord : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordService _service;

		private readonly IAcademicYearProcess _process;

		private Guid _apId;

		public ControlAcademicPlanRecord(IAcademicPlanRecordService service, IAcademicYearProcess process)
		{
			InitializeComponent();
			_service = service;
			_process = process;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "Disciplne", Title = "Дисциплина", Width = 200, Visible = true },
                new ColumnConfig { Name = "InDepartment", Title = "Кафедральная", Width = 150, Visible = true },
                new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 150, Visible = true },
                new ColumnConfig { Name = "Contingent", Title = "Контингент", Width = 150, Visible = true },
                new ColumnConfig { Name = "Zet", Title = "Зет", Width = 100, Visible = true },
                new ColumnConfig { Name = "IsParent", Title = "Родительская", Width = 100, Visible = true },
                new ColumnConfig { Name = "IsChild", Title = "Дисц. по выб", Width = 100, Visible = true },
                new ColumnConfig { Name = "IsActiveSemester", Title = "Есть в расп.", Width = 100, Visible = true },
                new ColumnConfig { Name = "IsUseInWorkload", Title = "Участвует в расчете", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "LoadFromXMLToolStripMenuItem", "Загрузить из xml"},
                    { "LoadFromBlueAsteriskToolStripMenuItem", "Загрузить из xml (синияя звездчока)"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("LoadFromXMLToolStripMenuItem", LoadFromXMLToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("LoadFromBlueAsteriskToolStripMenuItem", LoadFromBlueAsteriskToolStripMenuItem_Click);
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
            Semesters sem = Semesters.Первый;
            switch(pageNumber + 1)
			{
                case 2: sem = Semesters.Второй; break;
                case 3: sem = Semesters.Третий; break;
                case 4: sem = Semesters.Четвертый; break;
                case 5: sem = Semesters.Пятый; break;
                case 6: sem = Semesters.Шестой; break;
                case 7: sem = Semesters.Седьмой; break;
                case 8: sem = Semesters.Восьмой; break;
                default: sem = Semesters.Первый; break;
            }
			var result = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { Semester = sem, AcademicPlanId = _apId });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return -1;
			}
            standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
                standartControl.GetDataGridViewRows.Add(
					res.Id,
					res.Disciplne,
                    res.InDepartment ? "да" : "нет",
                    res.Semester,
                    res.ContingentGroup,
                    res.Zet,
                    res.IsParent ? "да" : "нет",
                    res.IsChild ? "да" : "нет",
                    res.IsActiveSemester ? "да" : "нет",
                    res.IsUseInWorkload ? "да" : "нет"
                );
            }
            return result.Result.MaxCount;
        }

		private void AddRecord()
		{
            var form = Container.Resolve<FormAcademicPlanRecord>(
                new ParameterOverrides
                {
                    { "apId", _apId },
                    { "id", Guid.Empty }
                }
                .OnType<FormAcademicPlanRecord>());
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
                var form = Container.Resolve<FormAcademicPlanRecord>(
                    new ParameterOverrides
                    {
                        { "apId", _apId },
                        { "id", id }
                    }
                    .OnType<FormAcademicPlanRecord>());
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
                        var result = _service.DeleteAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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
				var result = _process.LoadFromXMLAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
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
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				}
			}
        }

        private void LoadFromBlueAsteriskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "plx|*.plx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _process.LoadFromBlueAsteriskAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
                {
                    Id = _apId,
                    FileName = dialog.FileName
                });
                if (result.Succeeded)
                {
                    MessageBox.Show("Загрузка прошла успешно", "Портал", MessageBoxButtons.OK);
                    LoadData(_apId);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
            }
        }
    }
}