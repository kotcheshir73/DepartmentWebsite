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
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	public partial class LoadDistributionRecordControl : UserControl
	{
		private readonly ILoadDistributionRecordService _service;

		public LoadDistributionRecordControl(ILoadDistributionRecordService service)
		{
			InitializeComponent();
			_service = service;

			var resultL = _service.GetLecturers();

			if(!resultL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке списка преподавателей возникла ошибка: ", resultL.Errors);
				return;
			}

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "KindOfLoadName", Title = "Запись", Width = 200, Visible = true },
				new ColumnConfig { Name = "KindOfLoadType", Title = "Тип", Width = 100, Visible = true }
			};
			foreach(var lecture in resultL.Result)
			{
				columns.Add(new ColumnConfig { Name = "Lecture" + lecture.Id, Title = lecture.LastName, Width = 50, Visible = false });
			}
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
	}
}
