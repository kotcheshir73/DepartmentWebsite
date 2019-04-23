using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.StreamLesson
{
    public partial class ControlStreamLesson : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamLessonService _service;

        private readonly IAcademicYearProcess _process;

        private Guid _ayId;

        public ControlStreamLesson(IStreamLessonService service, IAcademicYearProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "StreamLessonName", Title = "Название потока", Width = 250, Visible = true },
                new ColumnConfig { Name = "StreamLessonHours", Title = "Часы", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "CreateStreamsToolStripMenuItem", "Создать потоки"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("CreateStreamsToolStripMenuItem", CreateStreamsToolStripMenuItem_Click);
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

        public void LoadData(Guid ayId)
        {
            _ayId = ayId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetStreamLessons(new StreamLessonGetBindingModel { AcademicYearId = _ayId, PageNumber = pageNumber, PageSize = pageSize });
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
                    res.StreamLessonName,
                    res.StreamLessonHours
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormStreamLesson>(
                new ParameterOverrides
                {
                    { "ayId", _ayId },
                    { "id", Guid.Empty }
                }
                .OnType<FormStreamLesson>());
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
                var form = Container.Resolve<FormStreamLesson>(
                    new ParameterOverrides
                    {
                        { "ayId", _ayId },
                        { "id", id }
                    }
                    .OnType<FormStreamLesson>());
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
                        var result = _service.DeleteStreamLesson(new StreamLessonGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void CreateStreamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите создать или обновить потоки?", "Портал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var result = _process.CreateStreamsForAcademicYear(new EducationalProcessCreateStreams { AcademicYearId = _ayId });
                if (result.Succeeded)
                {
                    MessageBox.Show("Операция успешно выполнена", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                }
                standartControl.LoadPage();
            }
        }
    }
}