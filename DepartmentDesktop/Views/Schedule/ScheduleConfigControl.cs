using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Text;
using System.Linq;

namespace DepartmentDesktop.Views.Schedule
{
    public partial class ScheduleConfigControl : UserControl
    {
        private readonly IScheduleService _service;

        public ScheduleConfigControl(IScheduleService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var resultC = _service.GetClassrooms(new ClassroomGetBindingModel { });
			if (!resultC.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке аудиторий возникла ошибка: ", resultC.Errors);
				return;
			}
			var classrooms = resultC.Result;
			foreach (var elem in classrooms.List)
            {
                checkedListBoxClassrooms.Items.Add(elem.Id, true);
            }

            var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}
			var studentGroups = resultSG.Result.List;
			foreach (var elem in studentGroups)
            {
                checkedListBoxStudentGroups.Items.Add(elem.GroupName, true);
            }

			var resultSD = _service.GetSeasonDaties(new SeasonDatesGetBindingModel { });
			if (!resultSD.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultSD.Errors);
				return;
			}
			comboBoxSeasonDates.ValueMember = "Value";
            comboBoxSeasonDates.DisplayMember = "Display";
            comboBoxSeasonDates.DataSource = resultSD.Result.List
				.Select(cd => new { Value = cd.Id, Display = cd.Title }).ToList();

			var resultCD = _service.GetCurrentDates();
			if (!resultCD.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
                return;
            }
			var _dates = resultCD.Result;
            if (_dates != null)
            {
                comboBoxSeasonDates.SelectedValue = _dates.Id;
            }
        }

        /// <summary>
        /// Получение списка выбраных аудиторий
        /// </summary>
        /// <returns></returns>
        private List<string> getListOfClassrooms()
        {
            List<string> classrooms = new List<string>();
            foreach (var elem in checkedListBoxClassrooms.CheckedItems)
            {
                classrooms.Add(elem.ToString());
            }
            if (classrooms.Count == 0)
            {
                MessageBox.Show("Список аудиторий пуст!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return classrooms;
        }

        /// <summary>
        /// Получение списка выбраных групп
        /// </summary>
        /// <returns></returns>
        private List<string> getListOfStudentGroups()
        {
            List<string> studentGroups = new List<string>();
            foreach (var elem in checkedListBoxStudentGroups.CheckedItems)
            {
                studentGroups.Add(elem.ToString());
            }
            if (studentGroups.Count == 0)
            {
                MessageBox.Show("Список групп пуст!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return studentGroups;
        }

        /// <summary>
        /// Отчистка аудиторий от пар
        /// </summary>
        /// <param name="classrooms"></param>
        private void cleaningClassrooms(List<string> classrooms)
        {
            if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var elem in classrooms)
                {
                    var result = _service.ClearSemesterRecords(new ScheduleGetBindingModel { ClassroomId = elem });

                    if (!result.Succeeded)
                    {
                        StringBuilder strRes = new StringBuilder();
                        foreach (var err in result.Errors)
                        {
                            strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                        }
                        MessageBox.Show(string.Format("Не удалось отчистить расписание по аудитории {0}: {1}", elem, strRes), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
		}

		/// <summary>
		/// Отчистка аудиторий от пар
		/// </summary>
		/// <param name="classrooms"></param>
		private void cleaningStudentGroups(List<string> studentGroups)
		{
			if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				foreach (var elem in studentGroups)
				{
					var result = _service.ClearSemesterRecords(new ScheduleGetBindingModel { StudentGroupName = elem });

					if (!result.Succeeded)
					{
						StringBuilder strRes = new StringBuilder();
						foreach (var err in result.Errors)
						{
							strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
						}
						MessageBox.Show(string.Format("Не удалось отчистить расписание по группе {0}: {1}", elem, strRes), "",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void checkRecordsIfNotComplite()
        {
            var result = _service.CheckSemesterRecordsIfNotComplite();
            if (result.Succeeded)
            {
                MessageBox.Show("Обновление прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                StringBuilder strRes = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                }
                MessageBox.Show(string.Format("Не удалось обновить расписание: {0}", strRes), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка расписания с сайта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMakeLoadHTMLScheduleForClassrooms_Click(object sender, EventArgs e)
        {
            var classrooms = getListOfClassrooms();
            var studentGroups = getListOfStudentGroups();
            if (classrooms == null && studentGroups == null)
            {
                return;
            }

            if (checkBoxClearSchedule.Checked)
            {
                cleaningClassrooms(classrooms);
				cleaningStudentGroups(studentGroups);
            }

            var result = _service.ImportHtml(new ImportToSemesterFromHTMLBindingModel
			{
                ScheduleUrl = textBoxLinkToHtml.Text,
                Classrooms = classrooms,
                StudentGroups = studentGroups
            });

            if (result.Succeeded)
            {
                if (checkBoxCheckIfNotComplite.Checked)
                {
                    checkRecordsIfNotComplite();
                }
                else
                {
                    MessageBox.Show("Обновление прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
				Program.PrintErrorMessage("Не удалось обновить расписание", result.Errors);
            }
        }

        /// <summary>
        /// Отчистка расписания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearSemesterRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms != null)
			{
				if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
						var result = _service.ClearSemesterRecords(new ScheduleGetBindingModel { ClassroomId = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по аудитории {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}


			List<string> studentGroups = getListOfStudentGroups();
			if (studentGroups != null)
			{
				if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in studentGroups)
					{
						var result = _service.ClearSemesterRecords(new ScheduleGetBindingModel { StudentGroupName = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по группе {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

        private void buttonClearOffsetRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms != null)
			{
				if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
                        var result = _service.ClearOffsetRecords(new ScheduleGetBindingModel { ClassroomId = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по аудитории {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}

			List<string> studentGroups = getListOfStudentGroups();
			if (studentGroups != null)
			{
				if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
						var result = _service.ClearOffsetRecords(new ScheduleGetBindingModel { StudentGroupName = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по группе {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

        private void buttonClearExaminationRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms != null)
            {
				if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
						var result = _service.ClearExaminationRecords(new ScheduleGetBindingModel { ClassroomId = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по аудитории {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			
			List<string> studentGroups = getListOfStudentGroups();
			if (studentGroups != null)
			{
				if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
						var result = _service.ClearExaminationRecords(new ScheduleGetBindingModel { StudentGroupName = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по группе {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

        private void buttonClearConsultationRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms != null)
			{

				if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
						var result = _service.ClearConsultationRecords(new ScheduleGetBindingModel { ClassroomId = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по аудитории {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}

			List<string> studentGroups = getListOfStudentGroups();
			if (studentGroups != null)
			{
				if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (var elem in classrooms)
					{
						var result = _service.ClearConsultationRecords(new ScheduleGetBindingModel { StudentGroupName = elem });

						if (!result.Succeeded)
						{
							StringBuilder strRes = new StringBuilder();
							foreach (var err in result.Errors)
							{
								strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
							}
							MessageBox.Show(string.Format("Не удалось отчистить расписание по группе {0}: {1}", elem, strRes), "",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

        /// <summary>
        /// Сохранение настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSeasonDatesSave_Click(object sender, EventArgs e)
        {
            if (comboBoxSeasonDates.SelectedItem != null)
            {
                if (MessageBox.Show("Сохранить изменения?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var result = _service.UpdateCurrentDates(new SeasonDatesGetBindingModel
                    {
                        Id = Convert.ToInt64(comboBoxSeasonDates.SelectedValue),
                        Title = comboBoxSeasonDates.Text
                    });

                    if (result.Succeeded)
                    {
                        MessageBox.Show("Обновление прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        StringBuilder strRes = new StringBuilder();
                        foreach (var err in result.Errors)
                        {
                            strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                        }
                        MessageBox.Show(string.Format("Не удалось обновить: {0}", strRes), "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonCheckRecordsIfNotComplite_Click(object sender, EventArgs e)
        {
            checkRecordsIfNotComplite();
        }

        private void buttonImportOffsetFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Excel-2003|*.xls|Excel-2007|*.xlsx";
			if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ImportExcel(new ImportToOffsetFromExcel { FileName = dialog.FileName });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonImportExaminationFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Excel-2003|*.xls|Excel-2007|*.xlsx";
			if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ImportExcel(new ImportToExaminationFromExcel { FileName = dialog.FileName });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Экспорт в Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportSemesterRecordExcel_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportSemesterRecordExcel(new ExportToExcelClassroomsBindingModel { FileName = dialog.FileName, Classrooms = classrooms });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonExportOffsetRecordExcel_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportOffsetRecordExcel(new ExportToExcelClassroomsBindingModel { FileName = dialog.FileName, Classrooms = classrooms });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonExportExaminationRecordExcel_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportExaminationRecordExcel(new ExportToExcelClassroomsBindingModel { FileName = dialog.FileName, Classrooms = classrooms });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Экспорт в html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportSemesterRecordHTML_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportSemesterRecordHTML(new ExportToHTMLClassroomsBindingModel { FilePath = dialog.SelectedPath, Classrooms = classrooms });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonExportOffsetRecordHTML_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportOffsetRecordHTML(new ExportToHTMLClassroomsBindingModel { FilePath = dialog.SelectedPath, Classrooms = classrooms });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonExportExaminationRecordHTML_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportExaminationRecordHTML(new ExportToHTMLClassroomsBindingModel { FilePath = dialog.SelectedPath, Classrooms = classrooms });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show(string.Format("Не удалось выгрузить: {0}", strRes), "",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
