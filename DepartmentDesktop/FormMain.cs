using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DepartmentService;
using Microsoft.Practices.Unity;

namespace DepartmentDesktop
{
    public partial class FormMain : Form
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void ApplyControl(Control control)
        {
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
        }

        private void MakeTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = new Controllers.MakeTicketsUS();
            ApplyControl(control);
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = new Controllers.ListUsersUC();
            ApplyControl(control);
            control.LoadData();
        }

        private void educationDirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.EducationDirection.EducationDirectionControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void studentGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.StudentGroup.StudentGroupControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void classroomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.Classroom.ClassroomControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void seasonDatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.SeasonDates.SeasonDatesControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void streamingLessonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.StreamingLesson.StreamingLessonControl>();
            ApplyControl(control);
            control.LoadData();
        }

        #region Расписание - настройки
        /// <summary>
        /// Настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleConfigControl>();
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Стоп-слова
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleStopWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleStopWordControl>();
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Интервалы пар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleLessonTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleLessonTimeControl>();
            ApplyControl(control);
            control.LoadData();
        }
        #endregion

        #region Расписание - аудитории
        /// <summary>
        /// Расписание аудиторий на семестр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleClassroomSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleSemesterControl>();
            ApplyControl(control);
            control.LoadData(0);
        }
        /// <summary>
        /// Расписание аудиторий на зачетную неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleClassroomOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleOffsetControl>();
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Расписание аудиторий на экзаменационную сессию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleClassroomExaminationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleExaminationControl>();
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Расписание аудиторий по консультациям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleClassroomConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleConsultationControl>();
            ApplyControl(control);
            control.LoadData();
        }
        #endregion

        private void scheduleStudentGroupSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.Services.Schedule.ScheduleSemesterControl>();
            ApplyControl(control);
            control.LoadData(1);
        }
    }
}
