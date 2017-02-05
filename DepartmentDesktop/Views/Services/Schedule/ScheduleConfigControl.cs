using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.Services.Schedule
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
            var classrooms = _service.GetClassrooms();
            foreach(var elem in classrooms)
            {
                checkedListBoxClassrooms.Items.Add(elem.Id);
            }
        }

        private void buttonMakeLoadHTMLScheduleForClassrooms_Click(object sender, EventArgs e)
        {
            List<string> classrooms = new List<string>();
            foreach (var elem in checkedListBoxClassrooms.CheckedItems)
            {
                classrooms.Add(elem.ToString()+ "/3");
            }
            if (checkBoxClearSchedule.Checked)
            {
                if(MessageBox.Show("Отчистить расписание?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach(var elem in classrooms)
                    {
                        var res = _service.ClearSemesterRecords(new ClassroomGetBindingModel { Id = elem });
                        if(!res.Succeeded)
                        {
                            MessageBox.Show(string.Format("Не удалось отчистить расписание по аудитории {0}: {1}", elem, res.Errors["error"]), "", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            var result = _service.LoadScheduleHTMLForClassrooms(new LoadHTMLForClassroomsBindingModel { ScheduleUrl = "http://www.ulstu.ru/schedule/students/",
                Classrooms = classrooms });
            if(result.Succeeded)
            {
                MessageBox.Show("Обновление прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(string.Format("Не удалось обновить расписание: {0}", result.Errors["error"]), "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
