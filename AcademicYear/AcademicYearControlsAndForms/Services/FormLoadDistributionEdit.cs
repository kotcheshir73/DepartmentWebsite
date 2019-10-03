using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tools;

namespace AcademicYearControlsAndForms.Services
{
    public partial class FormLoadDistributionEdit : StandartForm
    {
        private readonly IAcademicYearProcess _process;

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        private readonly IAcademicPlanRecordMissionService _serviceAPRM;

        private Guid _academicYearId;
        
        private Guid _academicPlanRecordId;
        
        private Guid? _lecturerId = null;

        private HashSet<int> listNumEditRows;

        public FormLoadDistributionEdit(IAcademicYearProcess process, IAcademicPlanRecordElementService serviceAPRE, IAcademicPlanRecordMissionService serviceAPRM,
                                           string academicYearId, string academicPlanRecordId, string lecturerId, string disciplineName, string lecturerName) : base()
        {
            _process = process;
            _serviceAPRE = serviceAPRE;
            _serviceAPRM = serviceAPRM;
            _academicYearId = new Guid(academicYearId);
            _academicPlanRecordId = new Guid(academicPlanRecordId);

            listNumEditRows = new HashSet<int>();

            InitializeComponent();

            if(lecturerId == "")
            {
                Text = disciplineName;
                LoadColomnsAPRE();
                LoadDataAPRE();
            }
            else
            {
                _lecturerId = new Guid(lecturerId);
                Text = lecturerName + " - " + disciplineName;
                LoadColomnsAPRM();
                LoadDataAPRM();
            }

            buttonAutoComplete.Visible = _lecturerId != null;
        }

        protected override void LoadData()
        {
            if (_lecturerId == null)
            {
                LoadDataAPRE();
            }
            else
            {
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
            var result = _process.GetListAPRE(new AcademicYearGetBindingModel() { Id = _academicYearId },
                                                    new AcademicPlanRecordGetBindingModel() { Id = _academicPlanRecordId });
            if (result.Succeeded)
            {
                dataGridView.Rows.Clear();
                foreach (var elem in result.Result)
                {
                    dataGridView.Rows.Add(elem);
                }
                Height = 95 + 24 * result.Result.Count;
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void LoadDataAPRM()
        {
            var result = _process.GetListAPRM(new AcademicYearGetBindingModel() { Id = _academicYearId },
                                                    new AcademicPlanRecordGetBindingModel() { Id = _academicPlanRecordId },
                                                        new LecturerGetBindingModel() { Id = _lecturerId });
            if (result.Succeeded)
            {
                dataGridView.Rows.Clear();
                foreach (var elem in result.Result)
                {
                    dataGridView.Rows.Add(elem);
                }
                Height = 95 + 24 * result.Result.Count;
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }

        private void DataGridView_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(DataGridView_KeyPress);
        }
        
        private bool CheckFillAPRE(int i)
        {
            if (dataGridView[0, i].Value == null && dataGridView[3, i].Value == null && dataGridView[4, i].Value == null)
            {
                return false;
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

        protected override bool Save()
        {
            if (_lecturerId == null)
            {
                return SaveAllAPRE();
            }
            else
            {
                return SaveAllAPRM();
            }
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
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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

        private void ButtonAutoComplete_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView[6, i].Value = dataGridView[5, i].Value;
                listNumEditRows.Add(i);
            }
        }
    }
}