using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using System;
using DepartmentDesktop.Models;
using System.Collections.Generic;

namespace DepartmentDesktop.Views.EducationalProcess.Student
{
	public partial class StudentControl : UserControl
	{
		private readonly IStudentService _service;

		private StudentState _state;

		public StudentControl(IStudentService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "NumberOfBook", Title = "Номер зачетки", Width = 150, Visible = true },
				new ColumnConfig { Name = "LastName", Title = "Фамилия", Width = 100, Visible = true },
				new ColumnConfig { Name = "FirstName", Title = "Имя", Width = 100, Visible = true },
				new ColumnConfig { Name = "Patronymic", Title = "Отчество", Width = 150, Visible = true },
				new ColumnConfig { Name = "StudentGroup", Title = "Группа", Width = 150, Visible = true },
				new ColumnConfig { Name = "Description", Title = "Описание", Visible = true }
			};

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd", "toolStripButtonDel", "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) => {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        UpdRecord();
                        break;
                }
            });
        }

		public void LoadData(StudentState state)
		{
			_state = state;
            standartControl.LoadPage();
        }

		private int LoadRecords(int pageNumber, int pageSize)
        {
			var result = _service.GetStudents(new StudentGetBindingModel
			{
				PageSize = pageSize,
				PageNumber = pageNumber,
				StudentStatus = _state
			});
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return -1;
			}
            standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
                standartControl.GetDataGridViewRows.Add(
					res.NumberOfBook,
					res.LastName,
					res.FirstName,
					res.Patronymic,
					res.StudentGroup,
					res.Description
				);
            }
            return result.Result.MaxCount;
        }

		private void UpdRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count == 1)
			{
				string id = Convert.ToString(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value);
				var form = new StudentForm(_service, id);
				if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
			}
		}
	}
}
