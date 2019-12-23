using AcademicYearInterfaces.BindingModels;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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
        private void ButtonImportSemesterFromSite_Click(object sender, EventArgs e)
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
                if(string.IsNullOrEmpty(url))
                {
                    break;
                }
                list.Add(url);
            } while (true);

            var result = _process.Import(new ImportToSemesterRecordsBindingModel
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
        /// Отчистка расписания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClearSemesterRecordClassrooms_Click(object sender, EventArgs e)
        {
            //List<string> classrooms = GetListOfClassrooms();
            //if (classrooms != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearSemesterRecords(new ScheduleGetBindingModel
            //            {
            //                ClassroomNumber = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по аудитории {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> studentGroups = GetListOfStudentGroups();
            //if (studentGroups != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in studentGroups)
            //        {
            //            var result = _process.ClearSemesterRecords(new ScheduleGetBindingModel
            //            {
            //                StudentGroupName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по группе {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> lecturers = GetListOfLecturers();
            //if (lecturers != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных преподавателей?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in lecturers)
            //        {
            //            var result = _process.ClearSemesterRecords(new ScheduleGetBindingModel
            //            {
            //                LecturerName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по преподавателю {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}
        }

        private void ButtonClearOffsetRecordClassrooms_Click(object sender, EventArgs e)
        {
            //List<string> classrooms = GetListOfClassrooms();
            //if (classrooms != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearOffsetRecords(new ScheduleGetBindingModel
            //            {
            //                ClassroomNumber = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по аудитории {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> studentGroups = GetListOfStudentGroups();
            //if (studentGroups != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearOffsetRecords(new ScheduleGetBindingModel
            //            {
            //                StudentGroupName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по группе {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> lecturers = GetListOfLecturers();
            //if (lecturers != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных преподавателей?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in lecturers)
            //        {
            //            var result = _process.ClearOffsetRecords(new ScheduleGetBindingModel
            //            {
            //                LecturerName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по преподавателю {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}
        }

        private void ButtonClearExaminationRecordClassrooms_Click(object sender, EventArgs e)
        {
            //List<string> classrooms = GetListOfClassrooms();
            //if (classrooms != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearExaminationRecords(new ScheduleGetBindingModel
            //            {
            //                ClassroomNumber = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по аудитории {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> studentGroups = GetListOfStudentGroups();
            //if (studentGroups != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearExaminationRecords(new ScheduleGetBindingModel
            //            {
            //                StudentGroupName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по группе {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> lecturers = GetListOfLecturers();
            //if (lecturers != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных преподавателей?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in lecturers)
            //        {
            //            var result = _process.ClearExaminationRecords(new ScheduleGetBindingModel
            //            {
            //                LecturerName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по преподавателю {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}
        }

        private void ButtonClearConsultationRecordClassrooms_Click(object sender, EventArgs e)
        {
            //List<string> classrooms = GetListOfClassrooms();
            //if (classrooms != null)
            //{

            //    if (MessageBox.Show("Отчистить расписание выбранных аудиторий?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearConsultationRecords(new ScheduleGetBindingModel
            //            {
            //                ClassroomNumber = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по аудитории {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> studentGroups = GetListOfStudentGroups();
            //if (studentGroups != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных групп?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in classrooms)
            //        {
            //            var result = _process.ClearConsultationRecords(new ScheduleGetBindingModel
            //            {
            //                StudentGroupName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по группе {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}

            //List<string> lecturers = GetListOfLecturers();
            //if (lecturers != null)
            //{
            //    if (MessageBox.Show("Отчистить расписание выбранных преподавателей?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        foreach (var elem in lecturers)
            //        {
            //            var result = _process.ClearConsultationRecords(new ScheduleGetBindingModel
            //            {
            //                LecturerName = elem,
            //                //IsFirstHalfSemester = checkBoxIsFirstHalfSemester.Checked
            //            });

            //            if (!result.Succeeded)
            //            {
            //                ErrorMessanger.PrintErrorMessage(string.Format("Не удалось отчистить расписание по преподавателю {0}:", elem), result.Errors);
            //            }
            //        }
            //    }
            //}
        }

        private void ButtonImportOffsetFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel-2003|*.xls|Excel-2007|*.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _process.Import(new ImportToOffsetFromExcel { FileName = dialog.FileName });
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

        private void ButtonImportExaminationFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Excel-2003|*.xls|Excel-2007|*.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _process.Import(new ImportToExaminationFromExcel { FileName = dialog.FileName });
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