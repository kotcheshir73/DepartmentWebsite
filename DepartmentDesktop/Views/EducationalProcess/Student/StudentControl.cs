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

		private int _pageNumber;

		private int _maxPage;

		public StudentControl(IStudentService service)
		{
			InitializeComponent();
			_service = service;
			_pageNumber = 1;
			textBoxPageNumber.Text = _pageNumber.ToString();

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "NumberOfBook", Title = "Номер зачетки", Width = 150, Visible = true },
				new ColumnConfig { Name = "LastName", Title = "Фамилия", Width = 100, Visible = true },
				new ColumnConfig { Name = "FirstName", Title = "Имя", Width = 100, Visible = true },
				new ColumnConfig { Name = "Patronymic", Title = "Отчество", Width = 150, Visible = true },
				new ColumnConfig { Name = "StudentGroup", Title = "Группа", Width = 150, Visible = true },
				new ColumnConfig { Name = "Description", Title = "Описание", Visible = true }
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
					Width = column.Width.HasValue ? column.Width.Value : 0,
					AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill
				});
			}
		}

		public void LoadData(StudentState state)
		{
			_state = state;
			LoadRecords();
		}

		private void LoadRecords()
		{
			var result = _service.GetStudents(new StudentGetBindingModel
			{
				PageSize = 50,
				PageNumber = _pageNumber - 1,
				StudentStatus = _state
			});
			_maxPage = result.Result.MaxCount;
			labelFromCountPages.Text = string.Format("из {0}", _maxPage);
			textBoxPageNumber.Text = _pageNumber.ToString();
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.Rows.Clear();
			foreach (var res in result.Result.List)
			{
				dataGridViewList.Rows.Add(
					res.NumberOfBook,
					res.LastName,
					res.FirstName,
					res.Patronymic,
					res.StudentGroup,
					res.Description
				);
			}
		}

		private void UpdRecord()
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
				string id = Convert.ToString(dataGridViewList.SelectedRows[0].Cells[0].Value);
				var form = new StudentForm(_service, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadRecords();
				}
			}
		}

		private void buttonPrev_Click(object sender, System.EventArgs e)
		{
			if(_pageNumber > 1)
			{
				_pageNumber--;
				LoadRecords();
			}
		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			if (_pageNumber < _maxPage)
			{
				_pageNumber++;
				LoadRecords();
			}
		}

		private void textBoxPageNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
			{
				try
				{
					int number = Convert.ToInt32(textBoxPageNumber.Text);
					if (0 < number && number < _maxPage + 1)
					{
						_pageNumber = number;
					}
					else if (0 < number)
					{
						number = 1;
					}
					else
					{
						number = _maxPage;
					}
					LoadRecords();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void toolStripButtonUpd_Click(object sender, EventArgs e)
		{
			UpdRecord();
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadRecords();
		}

		private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					UpdRecord();
					break;
			}
		}

		private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			UpdRecord();
		}
	}
}
