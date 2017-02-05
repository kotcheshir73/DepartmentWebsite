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
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleSemestrClassroomControl : UserControl
    {
        private readonly IScheduleService _service;

        private DateTime _selectDate;

        public ScheduleSemestrClassroomControl(IScheduleService service)
        {
            InitializeComponent();
            _service = service;
            _selectDate = DateTime.Now;
        }

        public void LoadData(string classroomID)
        {
            try
            {
                labelTop.Text = classroomID + " аудитория";
                bool isLoad1Week = true;
                bool isLoad2Week = true;

                //_data = new Data();
                //_semester = new ClassSemester();
                //_consultaion = new ClassConsultation();
                var dates = _service.GetCurrentDates();
                if (dates == null)
                    throw new Exception("Невозможно получить даты семестра");

                //Заполняем даты
                DateTime currentdate = _selectDate;
                var dateBeginSemester = Convert.ToDateTime(dates.DateBeginSemester);
                var dateEndSemester = Convert.ToDateTime(dates.DateEndSemester);
                if (_selectDate.Date == DateTime.Now.Date)
                {
                    currentdate = dateBeginSemester.AddDays(((DateTime.Now - dateBeginSemester).Days / 14) * 14);
                    _selectDate = currentdate;
                }
                //Определяем, нужно ли заполнять записи по первой неделе и по второй (может семестр уже закончился)
                if (currentdate < dateBeginSemester || currentdate > dateEndSemester)
                {
                    isLoad1Week = false;
                    isLoad2Week = false;
                }
                if (currentdate.Date.AddDays(8) > dateEndSemester)
                {
                    isLoad2Week = false;
                }

                var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };//дни недели
                dataGridViewFirstWeek.Rows.Clear();
                dataGridViewSecondWeek.Rows.Clear();
                for (int j = 0; j < 6; j++)
                {
                    if (isLoad1Week)
                    {
                        dataGridViewFirstWeek.Rows.Add();//добавляем строки
                        dataGridViewFirstWeek.Rows[j].Cells[0].Value = days[j] + "\r\n" + currentdate.ToShortDateString();//в первый столбец записываем день недели
                        if (currentdate.Date == DateTime.Now.Date)
                            for (int i = 0; i < 9; i++)
                                dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                    }
                    if (isLoad2Week)
                    {
                        dataGridViewSecondWeek.Rows.Add();
                        dataGridViewSecondWeek.Rows[j].Cells[0].Value = days[j] + "\r\n" + currentdate.AddDays(7).ToShortDateString();
                        if (currentdate.AddDays(7).Date == DateTime.Now.Date)
                            for (int i = 0; i < 9; i++)
                                dataGridViewSecondWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                    }

                    currentdate = currentdate.AddDays(1);
                }
                if (isLoad1Week || isLoad2Week)
                {//если можно загрузить хотя бы одну неделю
                    var list = _service.GetSemesterRecords(new ClassroomGetBindingModel {  Id = classroomID });
                    if (list == null)
                        throw new Exception("Невозможно получить список занятий в семестре");
                    foreach (var record in list)
                    {
                        if (record.Week == 0 && isLoad1Week)
                        {
                            dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Value =
                                record.LessonDiscipline + " " + record.LessonType + Environment.NewLine +
                                record.LessonTeacher + Environment.NewLine + record.GroupName;
                            dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Tag = record.Id;
                        }
                        if (record.Week == 1 && isLoad2Week)
                        {
                            dataGridViewSecondWeek.Rows[record.Day].Cells[record.Lesson + 1].Value =
                                record.LessonDiscipline + " " + record.LessonType + Environment.NewLine +
                                record.LessonTeacher + Environment.NewLine + record.GroupName;
                            dataGridViewSecondWeek.Rows[record.Day].Cells[record.Lesson + 1].Tag = record.Id;
                        }
                    }
                    var dateFinish = (isLoad2Week) ? _selectDate.AddDays(14) : _selectDate.AddDays(7);
                    //var consults = _consultaion.getListByClassroomFromSemester(_classroomID, _selectDate, dateFinish);
                    //if (consults == null)
                    //    throw new Exception(_consultaion.Error);
                    //foreach (var record in consults)
                    //{
                    //    if ((record.DateConsult.Date - _selectDate.Date).Days > 0 &&
                    //        (record.DateConsult.Date - _selectDate.Date).Days <= 5 && isLoad1Week)
                    //    {
                    //        dataGridViewFirstWeek.Rows[(record.DateConsult.Date - _selectDate.Date).Days].Cells[record.Lesson.Value + 1].Value =
                    //            record.Discipline.DisciplineShortName + " конс." + Environment.NewLine +
                    //            record.Teacher.TeacherShortName + Environment.NewLine + record.Group.GroupName;
                    //        dataGridViewFirstWeek.Rows[(record.DateConsult.Date - _selectDate.Date).Days].Cells[record.Lesson.Value + 1].Style.BackColor =
                    //            Color.Green;
                    //        dataGridViewFirstWeek.Rows[(record.DateConsult.Date - _selectDate.Date).Days].Cells[record.Lesson.Value + 1].Tag = record.Id;
                    //    }
                    //    if ((record.DateConsult.Date - _selectDate.Date).Days - 7 > 0 &&
                    //        (record.DateConsult.Date - _selectDate.Date).Days - 7 <= 5 && isLoad2Week)
                    //    {
                    //        dataGridViewSecondWeek.Rows[(record.DateConsult.Date - _selectDate.Date).Days - 7].Cells[record.Lesson.Value + 1].Value =
                    //            record.Discipline.DisciplineShortName + " конс." + Environment.NewLine +
                    //            record.Teacher.TeacherShortName + Environment.NewLine + record.Group.GroupName;
                    //        dataGridViewSecondWeek.Rows[(record.DateConsult.Date - _selectDate.Date).Days - 7].Cells[record.Lesson.Value + 1].Style.BackColor
                    //            = Color.Green;
                    //        dataGridViewSecondWeek.Rows[(record.DateConsult.Date - _selectDate.Date).Days - 7].Cells[record.Lesson.Value + 1].Tag = record.Id;
                    //    }
                    //}
                }
                for (int i = 0; i < dataGridViewFirstWeek.Rows.Count; i++)
                {
                    dataGridViewFirstWeek.Rows[i].Height = (dataGridViewFirstWeek.Height - 35) / dataGridViewFirstWeek.Rows.Count;
                }
                for (int i = 0; i < dataGridViewSecondWeek.Rows.Count; i++)
                {
                    dataGridViewSecondWeek.Rows[i].Height = (dataGridViewSecondWeek.Height - 35) / dataGridViewSecondWeek.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
