using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Text;
using System.Linq;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleConfigControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IScheduleStopWordService _serviceSW;

        public ScheduleConfigControl(IScheduleService service, IScheduleStopWordService serviceSW)
        {
            InitializeComponent();
            _service = service;
            _serviceSW = serviceSW;
        }

        public void LoadData()
        {
            var classrooms = _service.GetClassrooms();
            foreach (var elem in classrooms)
            {
                checkedListBoxClassrooms.Items.Add(elem.Id);
            }
            comboBoxSeasonDates.ValueMember = "Value";
            comboBoxSeasonDates.DisplayMember = "Display";
            comboBoxSeasonDates.DataSource = _service.GetSeasonDaties()
                .Select(cd => new { Value = cd.Id, Display = cd.Title }).ToList();
            var record = _service.GetCurrentDates();
            if (record != null)
            {
                comboBoxSeasonDates.SelectedValue = record.Id;
            }
            else
            {
                comboBoxSeasonDates.SelectedValue = null;
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
        /// Отчистка аудиторий от пар
        /// </summary>
        /// <param name="classrooms"></param>
        private void cleaningClassrooms(List<string> classrooms)
        {
            if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var elem in classrooms)
                {
                    var result = _service.ClearSemesterRecords(new ClassroomGetBindingModel { Id = elem });

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
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            if (checkBoxClearSchedule.Checked)
            {
                cleaningClassrooms(classrooms);
            }

            var result = _service.LoadScheduleHTMLForClassrooms(new LoadHTMLForClassroomsBindingModel
            {
                ScheduleUrl = textBoxLinkToHtml.Text,
                Classrooms = classrooms
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
        /// Отчистка аудиторий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearSemesterRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var elem in classrooms)
                {
                    var result = _service.ClearSemesterRecords(new ClassroomGetBindingModel { Id = elem });

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

        private void buttonClearOffsetRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var elem in classrooms)
                {
                    var result = _service.ClearOffsetRecords(new ClassroomGetBindingModel { Id = elem });

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

        private void buttonClearExaminationRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var elem in classrooms)
                {
                    var result = _service.ClearExaminationRecords(new ClassroomGetBindingModel { Id = elem });

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

        private void buttonClearConsultationRecordClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var elem in classrooms)
                {
                    var result = _service.ClearConsultationRecords(new ClassroomGetBindingModel { Id = elem });

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
        /// Экспорт в html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportHTML_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportHTML(new ExportToHTMLClassroomsBindingModel { FilePath = dialog.SelectedPath, Classrooms = classrooms });
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
        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            List<string> classrooms = getListOfClassrooms();
            if (classrooms == null)
            {
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _service.ExportExcel(new ExportToExcelClassroomsBindingModel { FileName = dialog.FileName, Classrooms = classrooms });
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
    }
}
