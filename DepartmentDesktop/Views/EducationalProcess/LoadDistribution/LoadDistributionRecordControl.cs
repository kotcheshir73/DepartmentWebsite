using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	public partial class LoadDistributionRecordControl : UserControl
	{
		private readonly ILoadDistributionRecordService _service;

		private readonly IEducationalProcessService _serviceEP;

		private long _ldId;

		private bool _showTimeNorms = true;

		private int _countTimeNormColumns;

		public LoadDistributionRecordControl(ILoadDistributionRecordService service, IEducationalProcessService serviceEP)
		{
			InitializeComponent();
			_service = service;
			_serviceEP = serviceEP;

			LoadColumns();
		}

		private void LoadColumns()
		{
			var resultL = _service.GetLecturers(new LecturerGetBindingModel { });

			if (!resultL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке списка преподавателей возникла ошибка: ", resultL.Errors);
				return;
			}

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 50, Visible = true },
				new ColumnConfig { Name = "EducationDirection", Title = "Направление", Width = 100, Visible = true },
				new ColumnConfig { Name = "Title", Title = "Название", Width = 150, Visible = true }
			};
			if (_showTimeNorms)
			{
				var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { });
				if (!resultTN.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке списка норм времени возникла ошибка: ", resultL.Errors);
					return;
				}
				_countTimeNormColumns = resultTN.Result.List.Count;
				foreach (var timeNorm in resultTN.Result.List)
				{
					columns.Add(new ColumnConfig
					{
						Id = timeNorm.Id,
						Name = "TimeNorm" + timeNorm.Id,
						Title = timeNorm.Title,
						Width = 50,
						Visible = true
					});
				}
			}
			columns.Add(new ColumnConfig
			{
				Name = "Itog",
				Title = "Итого",
				Width = 50,
				Visible = true
			});
			foreach (var lecture in resultL.Result.List)
			{
				columns.Add(new ColumnConfig
				{
					Id = lecture.Id,
					Name = "Lecture" + lecture.Id,
					Title = lecture.Abbreviation,
					Width = 50,
					Visible = false
				});
			}
			dataGridViewList.Columns.Clear();
			foreach (var column in columns)
			{
				dataGridViewList.Columns.Add(new DataGridViewTextBoxColumn
				{
					Tag = column.Id,
					HeaderText = column.Title,
					Name = string.Format("Column{0}", column.Name),
					ReadOnly = true,
					Visible = column.Visible,
					Width = column.Width.HasValue ? column.Width.Value : 0,
					AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill
				});
			}
		}

		public void LoadData(long ldId)
		{
			_ldId = ldId;
			LoadRecords();
		}

		private void LoadRecords()
		{
			var result = _service.GetLoadDistributionRecords(new LoadDistributionRecordGetBindingModel { LoadDistributionId = _ldId });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.Rows.Clear();
			var resultDiscBlockGroups = result.Result.List.GroupBy(r => r.DisciplineBlockTitle);//.Select(grp => grp.ToList()).ToList();
			foreach (var resDiscBlockRecord in resultDiscBlockGroups)
			{
				dataGridViewList.Rows.Add();
				int index = dataGridViewList.Rows.Count - 1;
				dataGridViewList.Rows[index].Cells[2].Value = resDiscBlockRecord.Key;
				var semester = "оcень";
				var resultSemesterGroups = resDiscBlockRecord
					.Where(r => r.SemesterNumber % 2 == 1)
					.GroupBy(r => r.SemesterNumber);
				//спевра получаем нечетные семестры
				LoadSemester(semester, resultSemesterGroups);
				semester = "весна";
				resultSemesterGroups = resDiscBlockRecord
					.Where(r => r.SemesterNumber % 2 == 0)
					.GroupBy(r => r.SemesterNumber);
				//теперь получаем четные семестры
				LoadSemester(semester, resultSemesterGroups);
			}
		}

		private void LoadSemester(string semester, IEnumerable<IGrouping<int, LoadDistributionRecordViewModel>> resultSemesterGroups)
		{
			foreach (var resSemesterRecord in resultSemesterGroups)
			{
				var resultContingentGroups = resSemesterRecord.GroupBy(r => r.ContingentId);
				foreach(var resultContingentRecord in resultContingentGroups)
				{
					var resultDisciplineGroups = resultContingentRecord.GroupBy(r => r.Disciplne);
					foreach (var resDisciplineRecord in resultDisciplineGroups)
					{
						var firstRecord = resDisciplineRecord.First();
						dataGridViewList.Rows.Add();
						int index = dataGridViewList.Rows.Count - 1;
						dataGridViewList.Rows[index].Cells[0].Value = semester;
						dataGridViewList.Rows[index].Cells[1].Value = firstRecord.EducationDirectionCipher;
						dataGridViewList.Rows[index].Cells[2].Value = resDisciplineRecord.Key;
						int columnIndex = 2;
						if (_showTimeNorms)
						{
							for (int i = 3; i < _countTimeNormColumns + 3; ++i)
							{
								if (dataGridViewList.Columns[i].Tag != null)
								{
									long timeNormId = Convert.ToInt64(dataGridViewList.Columns[i].Tag);
									var recordTimeNorm = resDisciplineRecord.FirstOrDefault(r => r.TimeNormId == timeNormId);
									if (recordTimeNorm != null)
									{
										dataGridViewList.Rows[index].Cells[i].Value = recordTimeNorm.Load;
										dataGridViewList.Rows[index].Cells[i].Tag = 
											recordTimeNorm.AcademicPlanRecordId + "_" +  recordTimeNorm.Id;
									}
								}
							}
							columnIndex += _countTimeNormColumns;
						}
						dataGridViewList.Rows[index].Cells[++columnIndex].Value = resDisciplineRecord.Sum(r => r.Load);
					}
				}
			}
		}

		private void AddRecord()
		{
			//var form = new LoadDistributionRecordForm(_service, _serviceLDR);
			//if (form.ShowDialog() == DialogResult.OK)
			//{
			//	LoadRecords();
			//}
		}

		private void UpdRecord()
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
				//long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
				//var form = new LoadDistributionForm(_service, _serviceLDR, id);
				//if (form.ShowDialog() == DialogResult.OK)
				//{
				//	LoadRecords();
				//}
			}
		}

		private void DelRecord()
		{
			if (dataGridViewList.SelectedRows.Count > 0)
			{
				if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					for (int i = 0; i < dataGridViewList.SelectedRows.Count; ++i)
					{
						//long id = Convert.ToInt64(dataGridViewList.SelectedRows[i].Cells[0].Value);
						//var result = _service.DeleteLoadDistribution(new LoadDistributionGetBindingModel { Id = id });
						//if (!result.Succeeded)
						//{
						//	Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						//}
					}
					LoadRecords();
				}
			}
		}

		private void MakeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var result = _serviceEP.MakeLoadDistribution(new LoadDistributionGetBindingModel { Id = _ldId });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При формировании возникла ошибка: ", result.Errors);
				return;
			}
			else
			{
				LoadRecords();
			}
		}

		private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			for(int i = 0; i < dataGridViewList.SelectedCells.Count; ++i)
			{
				if(dataGridViewList.SelectedCells[i].Tag != null)
				{
					var apId = Convert.ToInt64(dataGridViewList.SelectedCells[i].Tag.ToString().Split('_')[0]);
					var id = Convert.ToInt64(dataGridViewList.SelectedCells[i].Tag.ToString().Split('_')[1]);
					var form = new LoadDistributionRecordForm(_service, id, apId);
					form.ShowDialog();
				}
			}
		}
	}
}
