using DepartmentDesktop.Models;
using DepartmentModel;
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

        private readonly IAcademicPlanRecordMissionService _serviceAPRM;

        private Guid _academicYearId;
        
        private Guid _academicPlanRecordId;
        
        private Guid? _lecturerId = null;

        private HashSet<int> listNumEditRows;

        public LoadDistributionEditForm(IEducationalProcessService serviceEP, IAcademicPlanRecordElementService serviceAPRE, IAcademicPlanRecordMissionService serviceAPRM,
                                           string academicYearId, string academicPlanRecordId, string lecturerId, string disciplineName, string lecturerName)
        {
            _serviceEP = serviceEP;
            _serviceAPRE = serviceAPRE;
            _serviceAPRM = serviceAPRM;
            _academicYearId = new Guid(academicYearId);
            _academicPlanRecordId = new Guid(academicPlanRecordId);

            listNumEditRows = new HashSet<int>();

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
                this.Text = lecturerName + " - " + disciplineName;
                LoadColomnsAPRM();
                LoadDataAPRM();
            }
            
            this.buttonAutoComplete.Visible = this._lecturerId != null;
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
                HeaderText = "Id",
                Name = "Column_TimeNorm_Id",
                ReadOnly = true,
                Visible = false,
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                Name = "Column_TimeNorm_Name",
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
                Height = 95 + 22 * result.Result.Count;
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
                HeaderText = "Id",
                Name = "Column_TimeNorm_Id",
                ReadOnly = true,
                Visible = false,
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Название",
                Name = "Column_TimeNorm_Name",
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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                //ValueType = typeof(double)
                
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
                Height = 95 + 22 * result.Result.Count;
            }
            else
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }

        private void dataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
            else
            {
                listNumEditRows.Add(dataGridView.CurrentCell.RowIndex);
            }
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(dataGridView_KeyPress);
        }
        
        private bool CheckFillAPRE(int i)
        {
            if (dataGridView[0, i].Value == null && dataGridView[3, i].Value == null && dataGridView[4, i].Value == null)
            {
                return false;
            }
            return true;
        }

        private bool SaveAPRE(int i)
        {
            if (CheckFillAPRE(i))
            {
                ResultService result;
                if (dataGridView[0, i].Value == null)
                {
                    result = _serviceAPRE.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                    {
                        AcademicPlanRecordId = _academicPlanRecordId,
                        TimeNormId = new Guid(dataGridView[1, i].Value.ToString()),
                        PlanHours = Convert.ToDecimal(dataGridView[3, i].Value),
                        FactHours = Convert.ToDecimal(dataGridView[4, i].Value)
                    });
                }
                else
                {
                    result = _serviceAPRE.UpdateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                    {
                        Id = new Guid(dataGridView[0, i].Value.ToString()),
                        AcademicPlanRecordId = _academicPlanRecordId,
                        TimeNormId = new Guid(dataGridView[1, i].Value.ToString()),
                        PlanHours = Convert.ToDecimal(dataGridView[3, i].Value),
                        FactHours = Convert.ToDecimal(dataGridView[4, i].Value)
                    });
                }
                if (result.Succeeded)
                {
                    if (result.Result != null)
                    {
                        if (result.Result is Guid)
                        {
                            dataGridView[0, i].Value = ((Guid)result.Result).ToString();
                        }
                    }
                    return true;
                }
                else
                {
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool SaveAllAPRE()
        {
            foreach(var tmp in listNumEditRows)
            {
                if (!SaveAPRE(tmp))
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckFillAPRM(int i)
        {
            if (dataGridView[1, i].Value == null && dataGridView[6, i].Value == null)
            {
                return false;
            }
            return true;
        }

        private bool SaveAPRM(int i)
        {
            if (CheckFillAPRM(i))
            {
                ResultService result;
                if (dataGridView[1, i].Value == null)
                {
                    result = _serviceAPRM.CreateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                    {
                        AcademicPlanRecordElementId = new Guid(dataGridView[0, i].Value.ToString()),
                        LecturerId = (Guid)_lecturerId,
                        Hours = Convert.ToDecimal(dataGridView[6, i].Value)
                    });
                }
                else
                {
                    result = _serviceAPRM.UpdateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                    {
                        Id = new Guid(dataGridView[1, i].Value.ToString()),
                        AcademicPlanRecordElementId = new Guid(dataGridView[0, i].Value.ToString()),
                        LecturerId = (Guid)_lecturerId,
                        Hours = Convert.ToDecimal(dataGridView[6, i].Value)
                    });
                }
                if (result.Succeeded)
                {
                    if (result.Result != null)
                    {
                        if (result.Result is Guid)
                        {
                            dataGridView[1, i].Value = ((Guid)result.Result).ToString();
                        }
                    }
                    return true;
                }
                else
                {
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool SaveAllAPRM()
        {
            foreach (var tmp in listNumEditRows)
            {
                if (!SaveAPRM(tmp))
                {
                    return false;
                }
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_lecturerId == null)
            {
                if (SaveAllAPRE())
                {
                    MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadDataAPRE();
            }
            else
            {
                if (SaveAllAPRM())
                {
                    MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                LoadDataAPRM();
            }
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if ( _lecturerId == null ? SaveAllAPRE() : SaveAllAPRM())
            {
                Close();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAutoComplete_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView.Rows)
            {
                row.Cells[6].Value = row.Cells[5].Value;
            }
        }
    }
}
