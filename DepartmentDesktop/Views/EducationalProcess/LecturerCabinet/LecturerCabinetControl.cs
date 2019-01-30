using DepartmentDesktop.Models;
using DepartmentDesktop.Views.LearningProgress.DisciplineLesson;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.LecturerCabinet
{
    public partial class LecturerCabinetControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _service;

        public LecturerCabinetControl(IDisciplineService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 50, Visible = false },
                new ColumnConfig { Name = "DisciplineName", Title = "Название", Width = 340, Visible = true },
                new ColumnConfig { Name = "DisciplineShortName", Title = "Краткое название", Width = 200, Visible = true },
                new ColumnConfig { Name = "DisciplineBlockTitle", Title = "Блок", Width = 50, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);
            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { AddDisciplineLessons(); });

        }

        public void LoadData()
        {
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplines(new DisciplineGetBindingModel { PageNumber = pageNumber, PageSize = pageSize }); // + чтобы дисциплины только этого преподавателя
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
                    res.DisciplineName,
                    res.DisciplineShortName,
                    res.DisciplineBlockTitle
                );
            }
            return result.Result.MaxCount;
        }

        public void AddDisciplineLessons()
        {
            Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
            var form = Container.Resolve<DisciplineLessonForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }
    }
}
