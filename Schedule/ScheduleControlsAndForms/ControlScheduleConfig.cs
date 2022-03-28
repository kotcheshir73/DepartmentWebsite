using ControlsAndForms.Messangers;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace ScheduleControlsAndForms
{
    public partial class ControlScheduleConfig : UserControl
    {
        private readonly IScheduleProcess _process;

        public ControlScheduleConfig(IScheduleProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        public void LoadData()
        {
            foreach (var date in ScheduleHelper.GetSemesterDates())
            {
                comboBoxStartPeriodDate.Items.Add(date.ToLongDateString());
            }
            comboBoxStartPeriodDate.SelectedIndex = -1;
        }

        /// <summary>
        /// Загрузка расписания с сайта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonImportSemesterFromSite_Click(object sender, EventArgs e)
        {
            if (comboBoxStartPeriodDate.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите дату начала периода", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var list = new List<string>();

            int counter = 1;
            string url;
            do
            {
                url = ConfigurationManager.AppSettings[$"ScheduleUrl{counter++}"];
                if (string.IsNullOrEmpty(url))
                {
                    break;
                }
                list.Add(url);
            } while (true);

            var result = await _process.Import(new ImportToSemesterRecordsBindingModel
            {
                ScheduleDate = Convert.ToDateTime(comboBoxStartPeriodDate.Text),
                ScheduleUrls = list
            });

            if (result.Succeeded)
            {
                MessageBox.Show("Обновление прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("Не удалось обновить расписание", result.Errors);
            }
        }

        /// <summary>
        /// Загрузка расписания зачетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonImportOffsetFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel|*.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _process.Import(new ImportToOffsetFromExcel
                {
                    ScheduleDate = dateTimePickerOffsetStart.Value,
                    FileName = dialog.FileName
                });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
            }
        }

        /// <summary>
        /// Загрузка расписания экзаменов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonImportExaminationFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel-2007|*.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _process.Import(new ImportToExaminationFromExcel
                {
                    ScheduleDate = dateTimePickerExamStart.Value,
                    FileName = dialog.FileName
                });
                if (result.Succeeded)
                {
                    MessageBox.Show("Выгружено", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                }
            }
        }

        /// <summary>
        /// Отчистка расписания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClearSemesterRecord_Click(object sender, EventArgs e)
        {
            var result = _process.ClearSemesterRecords(new ScheduleGetBindingModel
            {
                DateBegin = dateTimePickerFromClear.Value,
                DateEnd = dateTimePickerToClear.Value
            });

            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("Не удалось отчистить расписание по семестру", result.Errors);
            }
        }

        private void ButtonClearOffsetRecord_Click(object sender, EventArgs e)
        {
            var result = _process.ClearOffsetRecords(new ScheduleGetBindingModel
            {
                DateBegin = dateTimePickerFromClear.Value,
                DateEnd = dateTimePickerToClear.Value
            });

            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("Не удалось отчистить расписание по зачетам", result.Errors);
            }
        }

        private void ButtonClearExaminationRecord_Click(object sender, EventArgs e)
        {
            var result = _process.ClearExaminationRecords(new ScheduleGetBindingModel
            {
                DateBegin = dateTimePickerFromClear.Value,
                DateEnd = dateTimePickerToClear.Value
            });

            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("Не удалось отчистить расписание по экзаменам", result.Errors);
            }
        }

        private void ButtonClearConsultationRecord_Click(object sender, EventArgs e)
        {
            var result = _process.ClearConsultationRecords(new ScheduleGetBindingModel
            {
                DateBegin = dateTimePickerFromClear.Value,
                DateEnd = dateTimePickerToClear.Value
            });

            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("Не удалось отчистить расписание по консультациям", result.Errors);
            }
        }
    }
}