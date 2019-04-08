﻿using DepartmentModel.Enums;
using System;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop
{
    public partial class FormMain : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }

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
			control.Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right);
			while (Controls.Count > 1)
			{
				Controls.RemoveAt(Controls.Count - 1);
			}
			Controls.Add(control);
		}

        #region Сервис
        /// <summary>
        /// Генерация билетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MakeTicketsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = new Controllers.MakeTicketsUS();
			ApplyControl(control);
		}
        /// <summary>
        /// Расчет штатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void LoadDistributionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = Container.Resolve<Views.EducationalProcess.LoadDistribution.LoadDistributionControl>();
			ApplyControl(control);
			control.LoadData();
		}

        /// <summary>
        /// Синхронизация ролей и доступов по ним
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SynchronizationRolesAndAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = AuthenticationControlsAndForms.Controller.GetSynchronizationRolesControl;
            ApplyControl(control);
        }
        /// <summary>
        /// Успеваемость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void progressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LearningProgress.ConfiguringDisciplinesControl>();
            ApplyControl(control);
            control.LoadData();
        }

        /// <summary>
        /// Синхронизация пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SynchronizationUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = AuthenticationControlsAndForms.Controller.GetSynchronizationUsersControl;
            ApplyControl(control);
        }
        #endregion

        #region Работа с БД
        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var control = Container.Resolve<Views.Services.DataBaseWork.ImportDataBaseControl>();
            var control = AuthenticationControlsAndForms.Controller.GetImportDataBaseControl;
            ApplyControl(control);
        }
        /// <summary>
        /// Загрузить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var control = Container.Resolve<Views.Services.DataBaseWork.ExportDataBaseControl>();
            var control = AuthenticationControlsAndForms.Controller.GetExportDataBaseControl;
            ApplyControl(control);
        }
        #endregion

        #region Администрирование
        /// <summary>
        /// Пользователи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = AuthenticationControlsAndForms.Controller.GetControlUser;
			ApplyControl(control);
			control.LoadData();
		}
        /// <summary>
        /// Роли
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = AuthenticationControlsAndForms.Controller.GetControlRole;
            ApplyControl(control);
            control.LoadData();
        }
        #endregion

        #region Учебный процесс
        /// <summary>
        /// Направления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EducationDirectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlEducationDirection;
			ApplyControl(control);
			control.LoadData();
		}
        /// <summary>
        /// Должности преподавателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LecturerPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = BaseControlsAndForms.Controller.GetControlLecturerPost;
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Преподаватели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void LecturerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlLecturer;
            ApplyControl(control);
			control.LoadData();
		}
        /// <summary>
        /// Блоки дисциплин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void DisciplineBlockToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlDisciplineBlock;
            ApplyControl(control);
			control.LoadData();
		}
        /// <summary>
        /// Дисциплины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void DisciplineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlDiscipline;
            ApplyControl(control);
			control.LoadData();
		}
        /// <summary>
        /// Группы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void StudentGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlStudentGroup;
            ApplyControl(control);
			control.LoadData();
		}

        #region Студенты
        /// <summary>
        /// Учащиеся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StudentsStudentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlStudent;
            ApplyControl(control);
			control.LoadData(Enums.StudentState.Учится, null);
		}
        /// <summary>
        /// Завершившие обучение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void StudentsGraduateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = BaseControlsAndForms.Controller.GetControlStudent;
            ApplyControl(control);
			control.LoadData(Enums.StudentState.Завершил, null);
		}
        /// <summary>
        /// В академическом отпуске
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void StudentsAcademToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = BaseControlsAndForms.Controller.GetControlStudent;
            ApplyControl(control);
			control.LoadData(Enums.StudentState.Академ, null);
		}
        /// <summary>
        /// Отчисленные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void StudentsDeductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = BaseControlsAndForms.Controller.GetControlStudent;
            ApplyControl(control);
			control.LoadData(Enums.StudentState.Отчислен, null);
		}
        #endregion

        /// <summary>
        /// Аудитории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClassroomToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = BaseControlsAndForms.Controller.GetControlClassroom;
            ApplyControl(control);
			control.LoadData();
		}

        #region Учебные планы
        /// <summary>
        /// Учебные года
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void AcademicYearsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = Container.Resolve<Views.EducationalProcess.AcademicYear.AcademicYearControl>();
			ApplyControl(control);
			control.LoadData();
		}
        #endregion

        #endregion

        #region Расписание

        #region Расписание текущие
        /// <summary>
        /// Расписание аудитории по текущей дате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCurrentClassroomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetControlCurrentClassroom;
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Расписание групп по текущей дате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCurrentStudentGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetControlCurrentStudentGroup;
            ApplyControl(control);
            control.LoadData();
        }
        /// <summary>
        /// Расписание преподавателей по текущей дате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCurrentLecturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetControlCurrentLecturer;
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
        private void ScheduleClassroomSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleSemesterTabControl;
            ApplyControl(control);
            control.LoadData(0);
        }
        /// <summary>
        /// Расписание аудиторий на зачетную неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleClassroomOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleOffsetTabControl;
            ApplyControl(control);
            control.LoadData(0);
        }
        /// <summary>
        /// Расписание аудиторий на экзаменационную сессию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleClassroomExaminationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleExaminationTabControl;
            ApplyControl(control);
            control.LoadData(0);
        }
        /// <summary>
        /// Расписание аудиторий по консультациям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleClassroomConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleConsultationTabControl;
            ApplyControl(control);
            control.LoadData(0);
        }
        #endregion

        #region Расписание - группы
        /// <summary>
        /// Расписание групп на семестр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleStudentGroupSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleSemesterTabControl;
            ApplyControl(control);
            control.LoadData(1);
        }
        /// <summary>
        /// Расписание групп на зачетную неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleStudentGroupOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleOffsetTabControl;
            ApplyControl(control);
            control.LoadData(1);
        }
        /// <summary>
        /// Расписание групп на экзаменационную сессию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleStudentGroupExaminationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleExaminationTabControl;
            ApplyControl(control);
            control.LoadData(1);
        }
        /// <summary>
        /// Расписание групп по консультациям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleStudentGroupConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleConsultationTabControl;
            ApplyControl(control);
            control.LoadData(1);
        }
        #endregion

        #region Расписание - преподаватели
        /// <summary>
        /// Расписание преподавателей на семестр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleLecturerSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleSemesterTabControl;
            ApplyControl(control);
            control.LoadData(2);
        }
        /// <summary>
        /// Расписание преподавателей на зачетную неделю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleLecturerOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleOffsetTabControl;
            ApplyControl(control);
            control.LoadData(2);
        }
        /// <summary>
        /// Расписание преподавателей на экзаменационную сессию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleLecturerExaminationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleExaminationTabControl;
            ApplyControl(control);
            control.LoadData(2);
        }
        /// <summary>
        /// Расписание преподавателей по консультациям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scheduleLecturerConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = ScheduleControlsAndForms.Controller.GetScheduleConsultationTabControl;
            ApplyControl(control);
            control.LoadData(2);
        }
        #endregion

        /// <summary>
        /// Настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = ScheduleControlsAndForms.Controller.GetControlScheduleConfig;
            ApplyControl(control);
			control.LoadData();
		}

        /// <summary>
        /// Потоки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void StreamingLessonsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = ScheduleControlsAndForms.Controller.GetControlStreamingLesson;
            ApplyControl(control);
			control.LoadData();
		}

		/// <summary>
		/// Интервалы пар
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScheduleLessonTimeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = ScheduleControlsAndForms.Controller.GetControlScheduleLessonTime;
            ApplyControl(control);
			control.LoadData();
		}
        #endregion

        #region Зав. лабораторией
        private void materialTechnicalValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LaboratoryHead.MaterialTechnicalValue.MaterialTechnicalValueControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void materialTechnicalValueGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LaboratoryHead.MaterialTechnicalValueGroup.MaterialTechnicalValueGroupControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void softwaresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LaboratoryHead.Software.SoftwareControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void softwareRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LaboratoryHead.SoftwareRecord.SoftwareRecordControl>();
            ApplyControl(control);
            control.LoadData();
        }
        #endregion

        #region Преподаватель
        private void configuringDisciplinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LearningProgress.ConfiguringDisciplinesControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void studentsDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LearningProgress.StudentsDistributionControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void посещаемостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LearningProgress.ConductedLessonsControl>();
            ApplyControl(control);
            control.LoadData();
        }

        private void успеваемостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.LearningProgress.AcceptTasksControl>();
            ApplyControl(control);
            control.LoadData();
        }
        #endregion

        #region
        private void examinationTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = TicketViews.PublicViews.GetExaminationTemplateControl();
            ApplyControl(control);
            control.LoadData();
        }
        #endregion
    }
}
