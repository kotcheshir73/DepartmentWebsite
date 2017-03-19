using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.Student
{
	public partial class StudentControl : UserControl
	{
		private readonly IStudentService _service;

		private StudentState _state;

		private int _pageNumber;

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
			dataGridViewList.DataSource = result.Result;
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
	}
}
