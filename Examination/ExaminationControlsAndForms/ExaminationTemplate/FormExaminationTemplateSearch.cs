using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;

namespace ExaminationControlsAndForms.ExaminationTemplate
{
    public partial class FormExaminationTemplateSearch : StandartSearchForm
    {
        private IExaminationTemplateService _service;

        private Guid? _disciplineId;

        public FormExaminationTemplateSearch(IExaminationTemplateService service, Guid? disciplineId = null)
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

            Configurate(columns);
        }

        protected override void LoadRecords()
        {
            var result = _service.GetExaminationTemplates(new ExaminationTemplateGetBindingModel { DisciplineId = _disciplineId });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }

            dataGridViewList.Rows.Clear();
            foreach (var res in result.Result.List)
            {
                bool isNeedFilters = false;
                bool isInclude = false;
                foreach (var elem in _searchWords)
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
                        dataGridViewList.Rows.Add(
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
                    dataGridViewList.Rows.Add(
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