using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask.DisciplineLessonTaskVariant
{
    public partial class DisciplineLessonTaskVariantControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskVariantService _service;

        private Guid _dltId;

        public DisciplineLessonTaskVariantControl(IDisciplineLessonTaskVariantService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "DisciplineLessonTaskTask", Title = "Задание", Width = 200, Visible = true },
                new ColumnConfig { Name = "VariantNumber", Title = "Вариант", Width = 150, Visible = true },
                new ColumnConfig { Name = "VariantTask", Title = "Задание по варианту", Width = 500, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "FormDisciplineLessonTaskVariantsToolStripMenuItem", "Сформировать варианты"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("FormDisciplineLessonTaskVariantsToolStripMenuItem", FormDisciplineLessonTaskVariantsToolStripMenuItem_Click);
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

        public void LoadData(Guid dltId)
        {
            _dltId = dltId;
            standartControl.LoadPage();
        }

        public int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessonTaskVariants(new DisciplineLessonTaskVariantGetBindingModel
            {
                DisciplineLessonTaskId = _dltId,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControl.GetDataGridViewRows.Add(
                     res.Id,
                     res.DisciplineLessonTaskTask,
                     res.VariantNumber,
                     res.VariantTask
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonTaskVariantForm>(new ParameterOverrides
                {
                    { "dltId", _dltId },
                    { "id", Guid.Empty}
                }
                .OnType<DisciplineLessonTaskVariantForm>());
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
                var form = Container.Resolve<DisciplineLessonTaskVariantForm>(
                    new ParameterOverrides
                    {
                        { "dltId", _dltId },
                        { "id", id }
                    }
                    .OnType<DisciplineLessonTaskVariantForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }

        private void DelRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void FormDisciplineLessonTaskVariantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDisciplineLessonTaskVariantsForm>(
                new ParameterOverrides
                {
                    { "dltId", _dltId }
                }
                .OnType<FormDisciplineLessonTaskVariantsForm>());
            form.Show();
        }
    }
}
