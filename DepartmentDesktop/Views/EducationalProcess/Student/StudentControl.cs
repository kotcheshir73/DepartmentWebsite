using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using System;

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
		}

		public void LoadData(StudentState state)
		{
			_state = state;
			var result = _service.GetStudents(new StudentGetBindingModel
			{
				PageSize = 50,
				PageNumber = _pageNumber - 1,
				StudentStatus = _state
			});
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			_maxPage = result.Result.MaxCount;
			labelFromCountPages.Text = string.Format("из {0}", _maxPage);
			textBoxPageNumber.Text = _pageNumber.ToString();
			dataGridViewList.DataSource = result.Result.List;
			if (dataGridViewList.Columns.Count > 0)
			{
				dataGridViewList.Columns[0].HeaderText = "Номер зачетки";
				dataGridViewList.Columns[0].Width = 150;
				dataGridViewList.Columns[1].HeaderText = "Фамилия";
				dataGridViewList.Columns[1].Width = 150;
				dataGridViewList.Columns[2].HeaderText = "Имя";
				dataGridViewList.Columns[2].Width = 150;
				dataGridViewList.Columns[3].HeaderText = "Отчество";
				dataGridViewList.Columns[3].Width = 150;
				dataGridViewList.Columns[4].Visible = false;
				dataGridViewList.Columns[5].Visible = false;
				dataGridViewList.Columns[6].HeaderText = "Группа";
				dataGridViewList.Columns[6].Width = 150;
				dataGridViewList.Columns[7].HeaderText = "Описание";
				dataGridViewList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
		}

		private void buttonPrev_Click(object sender, System.EventArgs e)
		{
			if(_pageNumber > 1)
			{
				_pageNumber--;
				LoadData(_state);
			}
		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			if (_pageNumber < _maxPage)
			{
				_pageNumber++;
				LoadData(_state);
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
						LoadData(_state);
					}
					else if (0 < number)
					{
						number = 1;
					}
					else
					{
						number = _maxPage;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
