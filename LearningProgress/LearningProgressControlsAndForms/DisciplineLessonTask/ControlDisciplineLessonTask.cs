﻿using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using LearningProgressControlsAndForms.Reports;
using LearningProgressControlsAndForms.Services;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace LearningProgressControlsAndForms.DisciplineLessonTask
{
    public partial class ControlDisciplineLessonTask : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskService _service;

        private Guid _dlId;

        public ControlDisciplineLessonTask(IDisciplineLessonTaskService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "DisciplineLessonTitle", Title = "Занятие", Width = 100, Visible = true },
                new ColumnConfig { Name = "Task", Title = "Задание", Width = 100, Visible = true },
                new ColumnConfig { Name = "Description", Title = "Описание", Width = 200, Visible = true },
                new ColumnConfig { Name = "IsNecessarily", Title = "Обязательность", Width = 100, Visible = true },
                new ColumnConfig { Name = "MaxBall", Title = "Макисмальный балл", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "DuplicateDisciplineLessonTasksToolStripMenuItem", "Дублировать варианты"},
                    { "FormDisciplineLessonTasksToolStripMenuItem", "Сформировать задания"},
                    { "GetDisciplineLessonTaskVariantsToolStripMenuItem", "Получить задания с вариантами"},
                    { "GetDisciplineLessonVariantTasksToolStripMenuItem", "Получить задания по вариантам"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("DuplicateDisciplineLessonTasksToolStripMenuItem", DuplicateDisciplineLessonTasksToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("FormDisciplineLessonTasksToolStripMenuItem", FormDisciplineLessonTasksToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("GetDisciplineLessonTaskVariantsToolStripMenuItem", GetDisciplineLessonTaskVariantsToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("GetDisciplineLessonVariantTasksToolStripMenuItem", GetDisciplineLessonVariantTasksToolStripMenuItem_Click);
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        AddRecord();
                        break;
                    case Keys.Enter:
                        UpdRecord();
                        break;
                    case Keys.Delete:
                        DelRecord();
                        break;
                }
            });
        }

        public void LoadData(Guid dlId)
        {
            _dlId = dlId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel { DisciplineLessonId = _dlId, PageNumber = pageNumber, PageSize = pageSize });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.DisciplineLessonTitle,
                    res.Task,
                    res.Description,
                    res.IsNecessarily ? "Да" : "Нет",
                    res.MaxBall?.ToString() ?? "Нет"
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormDisciplineLessonTask>(
                new ParameterOverrides
                {
                    { "dlId", _dlId },
                    { "id", Guid.Empty }
                }
                .OnType<FormDisciplineLessonTask>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }

        private void UpdRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<FormDisciplineLessonTask>(
                    new ParameterOverrides
                    {
                        { "dlId", _dlId },
                        { "id", id }
                    }
                    .OnType<FormDisciplineLessonTask>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }

        private void DelRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteDisciplineLessonTask(new DisciplineLessonTaskGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void DuplicateDisciplineLessonTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<FormDuplicateDisciplineLessonTask>(
                new ParameterOverrides
                {
                    { "dltId", id }
                }
                .OnType<FormDuplicateDisciplineLessonTask>());
                form.Show();
            }
        }

        private void FormDisciplineLessonTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDisciplineLessonTasks>(
                new ParameterOverrides
                {
                    { "dlId", _dlId }
                }
                .OnType<FormDisciplineLessonTasks>());
            form.Show();
        }

        private void GetDisciplineLessonTaskVariantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportDisciplineLessonTasks>(
                new ParameterOverrides
                {
                    { "dlId", _dlId }
                }
                .OnType<ReportDisciplineLessonTasks>());
            form.Show();
        }

        private void GetDisciplineLessonVariantTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportDisciplineLessonVariants>(
                new ParameterOverrides
                {
                    { "dlId", _dlId }
                }
                .OnType<ReportDisciplineLessonVariants>());
            form.Show();
        }
    }
}