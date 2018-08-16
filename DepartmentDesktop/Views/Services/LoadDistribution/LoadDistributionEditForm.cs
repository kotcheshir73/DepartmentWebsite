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

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        private Guid _academicYearId;
        
        private Guid _academicPlanRecordId;
        
        private Guid _lecturerId;

        public LoadDistributionEditForm(IEducationalProcessService serviceEP, IAcademicPlanRecordElementService serviceAPRE,
                                           string academicYearId, string academicPlanRecordId, string lecturerId, string disciplineName, string lecturerName)
        {
            _serviceEP = serviceEP;
            _serviceAPRE = serviceAPRE;
            _academicYearId = new Guid(academicYearId);
            _academicPlanRecordId = new Guid(academicPlanRecordId);

            InitializeComponent();

            if(lecturerId == "")
            {
                this.Text = disciplineName;
                LoadColomnsAPRE();
                LoadDataAPRE();
            }
            else
            {
                _lecturerId = new Guid(lecturerId);
                this.Text = disciplineName + " - " + lecturerName;
                LoadColomnsAPRM();
                LoadDataAPRM();
            }
        }

        private void LoadColomnsAPRE()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Id",
                Name = "Column_APRE_Id",
                ReadOnly = true,
                Visible = false,
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                Name = "Column_TimeNorm",
                ReadOnly = true,
                Visible = true,
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "План",
                Name = "Column_APRE_Plan",
                Visible = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Факт",
                Name = "Column_APRE_Fact",
                Visible = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void LoadDataAPRE()
        {
            var result = _serviceEP.GetListAPRE(new AcademicYearGetBindingModel() { Id = _academicYearId },
                                                    new AcademicPlanRecordGetBindingModel() { Id = _academicPlanRecordId });
            if (result.Succeeded)
            {
                dataGridView.Rows.Clear();
                foreach (var elem in result.Result)
                {
                    dataGridView.Rows.Add(elem);
                }
                Height = 18 + 22 * result.Result.Count;
            }
            else
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }

        private void LoadColomnsAPRM()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Id",
                Name = "Column_APRE_Id",
                ReadOnly = true,
                Visible = false,
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Id",
                Name = "Column_APRM_Id",
                ReadOnly = true,
                Visible = false,
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                Name = "Column_TimeNorm",
                ReadOnly = true,
                Visible = true,
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "План",
                Name = "Column_APRE_Plan",
                ReadOnly = true,
                Visible = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Факт",
                Name = "Column_APRE_Fact",
                ReadOnly = true,
                Visible = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Назначено",
                Name = "Column_APRM_Hours",
                Visible = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void LoadDataAPRM()
        {
            var result = _serviceEP.GetListAPRM(new AcademicYearGetBindingModel() { Id = _academicYearId },
                                                    new AcademicPlanRecordGetBindingModel() { Id = _academicPlanRecordId },
                                                        new LecturerGetBindingModel() { Id = _lecturerId });
            if (result.Succeeded)
            {
                dataGridView.Rows.Clear();
                foreach (var elem in result.Result)
                {
                    dataGridView.Rows.Add(elem);
                }
                Height = 18 + 22 * result.Result.Count;
            }
            else
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }
    }
}
