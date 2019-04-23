using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;

namespace TicketViews.Views.ExaminationTemplate
{
    public partial class ExaminationTemplateSearchForm : Form
    {
        private IExaminationTemplateService _service;

        private Guid? _disciplineId;

        private Guid _selectedId;

        public Guid SelectedId { get { return _selectedId; } }

        public ExaminationTemplateSearchForm(IExaminationTemplateService service, Guid? disciplineId = null)
        {
            InitializeComponent();
            _service = service;
            _disciplineId = disciplineId;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Disciplne", Title = "Дисциплина", Width = 200, Visible = true },
                new ColumnConfig { Name = "EducationDirectionName", Title = "Направление", Width = 150, Visible = true },
                new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 150, Visible = true },
                new ColumnConfig { Name = "ExaminationTemplateName", Title = "Название экзамена", Width = 200, Visible = true }
            };

            standartSearchControl.Configurate(columns);
            standartSearchControl.SelectElementAddEvent((Guid Id) => { _selectedId = Id; DialogResult = DialogResult.OK; Close(); });
            standartSearchControl.GetPageAddEvent(LoadRecords);
        }

        private void LoadRecords()
        {
            var result = _service.GetExaminationTemplates(new ExaminationTemplateGetBindingModel { DisciplineId = _disciplineId });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }

            standartSearchControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                bool isNeedFilters = false;
                bool isInclude = false;
                foreach (var elem in standartSearchControl.SearchWords)
                {
                    if (elem.Value.Length > 0)
                    {
                        isNeedFilters = true;
                        switch (elem.Key)
                        {
                            case "Disciplne":
                                if (res.DisciplneName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "EducationDirectionName":
                                if (res.EducationDirectionName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "Semester":
                                if (res.Semester.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "ExaminationTemplateName":
                                if (res.ExaminationTemplateName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                        }
                    }
                }
                if (isNeedFilters)
                {
                    if (isInclude)
                    {
                        standartSearchControl.GetDataGridViewRows.Add(
                            res.Id,
                            res.DisciplneName,
                            res.EducationDirectionName,
                            res.Semester,
                            res.ExaminationTemplateName
                        );
                    }
                }
                else
                {
                    standartSearchControl.GetDataGridViewRows.Add(
                        res.Id,
                        res.DisciplneName,
                        res.EducationDirectionName,
                        res.Semester,
                        res.ExaminationTemplateName
                    );
                }
            }
        }
    }
}