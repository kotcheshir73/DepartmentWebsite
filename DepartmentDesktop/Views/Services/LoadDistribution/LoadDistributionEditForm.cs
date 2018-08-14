using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.LoadDistribution
{
    public partial class LoadDistributionEditForm : Form
    {
        private readonly IEducationalProcessService _serviceEP;

        public LoadDistributionEditForm(string academicYearId, string academicPlanRecordId, string disciplineName, string lecturerName, string LecturerId, IEducationalProcessService serviceEP)
        {
            this.Text = disciplineName + " - " + lecturerName;
            _serviceEP = serviceEP;

            InitializeComponent();




        }

        public LoadDistributionEditForm(string academicYearId, string academicPlanRecordId, string disciplineName, IEducationalProcessService serviceEP)
        {
            this.Text = disciplineName;
            _serviceEP = serviceEP;

            InitializeComponent();

            LoadGrid();

            var result = _serviceEP.GetListAPRE(new AcademicYearGetBindingModel() { Id = new Guid(academicYearId) },
                                                new AcademicPlanRecordGetBindingModel() { Id = new Guid(academicPlanRecordId) });
            if (result.Succeeded)
            {
                dataGridView.Rows.Clear();
                foreach (var elem in result.Result)
                {
                    dataGridView.Rows.Add(elem);
                }
            }
            else
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }

        }

        private void LoadGrid()
        {
            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "APRE_Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = string.Format("TimeNorm"), Title = "Название", Width = 100, Visible = true },
                new ColumnConfig { Name = string.Format("APRE_Plan"), Title = "План", Width = 40, Visible = true },
                new ColumnConfig { Name = string.Format("APRE_Fact"), Title = "Факт", Width = 40, Visible = true }
            };
            dataGridView.Columns.Clear();
            foreach (var column in columns)
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = column.Title,
                    Name = string.Format("Column_{0}", column.Name),
                    ReadOnly = true,
                    Visible = column.Visible,
                    Width = column.Width.HasValue? column.Width.Value : 0,
                    AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill
                });
            }

        }
    }
}
