using AcademicYearControlsAndForms.LecturerWorkload;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.Services
{
    public partial class ControlLoadDistribution : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicYearService _serviceAY;

        private readonly ITimeNormService _serviceTN;

        private readonly IAcademicYearProcess _process;

        private readonly ILecturerService _serviceL;

        // private readonly IStatementService _serviceS;
        private readonly ILecturerWorkloadService _serviceLW;

        // private readonly IIndividualPlanRecordService _serviceIPR;
        private readonly ILecturerPostSerivce _serviceLP;

        private readonly IDisciplineTimeDistributionService _serviceDTD;

        private readonly IAcademicPlanRecordMissionService _serviceAPRM;

        private bool notLoading;

        List<ColumnConfig> _columns;

        private int _lecturerFirstColumnIndex;

        private int _lecturerLastColumnIndex;

        private int _timeNormFirstColumnIndex;

        private int _timeNormLastColumnIndex;

        public ControlLoadDistribution(IAcademicYearService serviceAY, ITimeNormService serviceTN, IAcademicYearProcess process, ILecturerService serviceL,/* IStatementService serviceS, 
            IIndividualPlanRecordService serviceIPR,*/ IDisciplineTimeDistributionService serviceDTD, ILecturerWorkloadService serviceLW, ILecturerPostSerivce serviceLP,
            IAcademicPlanRecordMissionService serviceAPRM)
        {
            InitializeComponent();
            _serviceAY = serviceAY;
            _serviceTN = serviceTN;
            _process = process;
            _serviceL = serviceL;
            // _serviceS = serviceS;
            //  _serviceIPR = serviceIPR;
            _serviceDTD = serviceDTD;

            _serviceLW = serviceLW;
            _serviceLP = serviceLP;
            _serviceAPRM = serviceAPRM;
            SetDoubleBuffered(dataGridViewList, true);
        }

        public void LoadData()
        {
            var resultAY = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            notLoading = true;
            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = null;
            notLoading = false;
        }

        private void ComboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicYear.SelectedValue != null && !notLoading)
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            Guid id = new Guid(comboBoxAcademicYear.SelectedValue.ToString());
            _columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "APR_Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Semester", Title = "Сем", Width = 50, Visible = true,  },
                new ColumnConfig { Name = "EducationDirection", Title = "Код напр.", Width = 80, Visible = true },
                new ColumnConfig { Name = "Discipline", Title = "Дисциплина", Width = 200, Visible = true },
                new ColumnConfig { Name = "IsSelecetedDiscipline", Title = "Выб", Width = 30, Visible = true },
                new ColumnConfig { Name = "Cource", Title = "Курс", Width = 30, Visible = true },
                new ColumnConfig { Name = "Students", Title = "Студ", Width = 30, Visible = true },
                new ColumnConfig { Name = "Streams", Title = "Пот", Width = 30, Visible = true },
                new ColumnConfig { Name = "Groups", Title = "Гр", Width = 30, Visible = true },
                new ColumnConfig { Name = "Subgroups", Title = "Подгр", Width = 30, Visible = true }
            };
            var timeNorms = _serviceTN.GetTimeNorms(new TimeNormGetBindingModel { AcademicYearId = id });
            foreach (var tn in timeNorms.Result.List)
            {
                _columns.Add(new ColumnConfig { Name = string.Format("APRE_Id_{0}", tn.Id), Title = "Id", Width = 100, Visible = false });
                _columns.Add(new ColumnConfig { Name = string.Format("APRE_Plan_{0}", tn.Id), Title = tn.TimeNormShortName, Width = 40, Visible = true });
                _columns.Add(new ColumnConfig { Name = string.Format("APRE_Fact_{0}", tn.Id), Title = tn.TimeNormShortName, Width = 40, Visible = true });
            }
            _columns.Add(new ColumnConfig { Name = "Itog_APR", Title = "Итого (дисц.)", Width = 50, Visible = true });
            var lecturers = _serviceL.GetLecturers(new LecturerGetBindingModel());
            foreach (var lect in lecturers.Result.List)
            {
                _columns.Add(new ColumnConfig
                {
                    Name = string.Format("Lecturer_{0}_{1}", lect.Id, lect.LecturerPostId),
                    Title = lect.FullName,
                    Width = 40,
                    Visible = true
                });
            }
            _columns.Add(new ColumnConfig { Name = "Itog_Lecturer", Title = "Итого (лект.)", Width = 50, Visible = true });
            _columns.Add(new ColumnConfig { Name = "Itog", Title = "Итого", Width = 50, Visible = true });

            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataGridViewList.Columns.Clear();
            dataGridViewColumns.Rows.Clear();
            _lecturerFirstColumnIndex = -1;
            _lecturerLastColumnIndex = -1;
            _timeNormFirstColumnIndex = -1;
            _timeNormLastColumnIndex = -1;
            foreach (var column in _columns)
            {
                dataGridViewList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = column.Title,
                    Name = string.Format("Column{0}", column.Name),
                    ReadOnly = true,
                    Visible = column.Visible,
                    Width = column.Width ?? 0,
                    AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
                if(_lecturerFirstColumnIndex == -1 && column.Name.StartsWith("Lecturer") && column.Visible)
                {
                    _lecturerFirstColumnIndex = dataGridViewList.Columns.Count - 1;
                }
                if (_lecturerFirstColumnIndex != -1 && !column.Name.StartsWith("Lecturer") && column.Visible && _lecturerLastColumnIndex == -1)
                {
                    _lecturerLastColumnIndex = dataGridViewList.Columns.Count - 2;
                }
                if (column.Visible && column.Name.StartsWith("Lecturer"))
                {
                    dataGridViewColumns.Rows.Add(new object[] { column.Name, dataGridViewList.Columns.Count - 1, column.Title, column.Visible });
                }
                if (_timeNormFirstColumnIndex == -1 && column.Name.StartsWith("APRE") && column.Visible)
                {
                    _timeNormFirstColumnIndex = dataGridViewList.Columns.Count - 1;
                }
                if (_timeNormFirstColumnIndex != -1 && !column.Name.StartsWith("APRE") && column.Visible && _timeNormLastColumnIndex == -1)
                {
                    _timeNormLastColumnIndex = dataGridViewList.Columns.Count - 2;
                }
            }
            dataGridViewList.Columns[3].Frozen = true;
            Cursor.Current = Cursors.Default;

            LoadRecords();
        }

        private void LoadRecords()
        {
            Cursor.Current = Cursors.WaitCursor;
            var result = _process.GetAcademicYearLoading(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
            var workload = _serviceLW.GetLecturerWorkloads(new LecturerWorkloadGetBindingModel
            {
                AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString())
            });
            var missions = _serviceAPRM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel
            {
                AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString())
            });
            var hours = _serviceLP.GetLecturerPosts(new LecturerPostGetBindingModel() { });
            if (result.Succeeded)
            {
                dataGridViewList.Rows.Clear();

                dataGridViewList.Rows.Add();
                dataGridViewList.Rows.Add();
                dataGridViewList.Rows.Add();
                
                for (int i = 0; i < dataGridViewList.Columns.Count; ++i)
                {
                    if (dataGridViewList.Columns[i].Name.StartsWith("ColumnLecturer"))
                    {
                        Guid id = new Guid(dataGridViewList.Columns[i].Name.Split('_')[1]);

                        dataGridViewList.Rows[0].Cells[i].Value = workload.Result.List?.FirstOrDefault(x => x.LecturerId == id)?.Workload;
                        dataGridViewList.Rows[1].Cells[i].Value = (hours.Result.List?.FirstOrDefault(x => x.Id == new Guid(dataGridViewList.Columns[i].Name.Split('_')[2]))?.Hours ?? 0) *
                            (workload.Result.List?.FirstOrDefault(x => x.LecturerId == id)?.Workload ?? 0);
                        dataGridViewList.Rows[2].Cells[i].Value = missions.Result.List?.Where(x => x.LecturerId == id).Sum(x => x.Hours) ?? 0;
                    }
                }
                foreach (var elem in result.Result)
                {
                    dataGridViewList.Rows.Add(elem);
                }

                SetColumnAndRowsVisibility();

                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }

        private void SetColumnAndRowsVisibility()
        {
            for (int i = 0; i < dataGridViewColumns.Rows.Count; ++i)
            {
                int columnIndex = Convert.ToInt32(dataGridViewColumns.Rows[i].Cells[1].Value);
                dataGridViewList.Columns[columnIndex].Visible = Convert.ToBoolean(dataGridViewColumns.Rows[i].Cells[3].Value);
            }
            for (int i = _timeNormFirstColumnIndex; i <= _timeNormLastColumnIndex; ++i)
            {
                if(_columns[i].Visible)
                {
                    dataGridViewList.Columns[i].Visible = checkBoxTimeNorm.Checked;
                }
            }
            if (checkBoxLecturerLoad.Checked)
            {
                for (int i = 0; i < dataGridViewList.Rows.Count; ++i)
                {
                    bool visible = false;
                    for (int j = _lecturerFirstColumnIndex; j < _lecturerLastColumnIndex; ++j)
                    {
                        if (dataGridViewList.Columns[j].Visible)
                        {
                            if (dataGridViewList.Rows[i].Cells[j].Value != null)
                            {
                                visible = true;
                            }
                        }
                    }
                    dataGridViewList.Rows[i].Visible = visible;
                }
            }
            else
            {
                for (int i = 0; i < dataGridViewList.Rows.Count; ++i)
                {
                    dataGridViewList.Rows[i].Visible = true;
                }
            }
        }

        private void UpdRecord()
        {
            int i, j;
            if (dataGridViewList.SelectedCells.Count == 1)
            {
                i = dataGridViewList.SelectedCells[0].RowIndex;
                j = dataGridViewList.SelectedCells[0].ColumnIndex;

                Form editForm = null;
                if (i == 0)
                {
                    var lw = _serviceLW.GetLecturerWorkloads(new LecturerWorkloadGetBindingModel
                    {
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        LecturerId = new Guid(dataGridViewList.Columns[j].Name.Split('_')[1])
                    });
                    if(lw.Succeeded)
                    {
                        editForm = Container.Resolve<FormLecturerWorkload>(
                            new ParameterOverrides
                            {
                                { "ayId", new Guid(comboBoxAcademicYear.SelectedValue.ToString()) },
                                { "id", lw.Result.List.First().Id }
                            }
                            .OnType<FormLecturerWorkload>());
                    }
                    else
                    {
                        ErrorMessanger.PrintErrorMessage("При получении нагрузки возникла ошибка: ", lw.Errors);
                    }
                }
                else if (dataGridViewList[0, i].Value == null)
                {
                    return;
                }
                else if (dataGridViewList.Columns[j].Name.StartsWith("ColumnLecturer"))
                {
                    editForm = Container.Resolve<FormLoadDistributionEdit>(
                        new ParameterOverrides
                        {
                            { "academicYearId", comboBoxAcademicYear.SelectedValue.ToString() },
                            { "academicPlanRecordId", dataGridViewList[0, i].Value.ToString() },
                            { "lecturerId", dataGridViewList.Columns[j].Name.Split('_')[1] },
                            { "disciplineName", dataGridViewList[3, i].Value.ToString() },
                            { "lecturerName", dataGridViewList.Columns[j].HeaderText }
                        }
                        .OnType<FormLoadDistributionEdit>());
                }
                else if (dataGridViewList.Columns[j].Name.StartsWith("ColumnAPRE"))
                {
                    editForm = Container.Resolve<FormLoadDistributionEdit>(
                        new ParameterOverrides
                        {
                            { "academicYearId", comboBoxAcademicYear.SelectedValue.ToString() },
                            { "academicPlanRecordId", dataGridViewList[0, i].Value.ToString() },
                            { "lecturerId", "" },
                            { "disciplineName", dataGridViewList[3, i].Value.ToString() },
                            { "lecturerName", "" }
                        }
                        .OnType<FormLoadDistributionEdit>());
                }

                if(editForm != null)
                {
                    if (editForm is StandartForm)
                    {
                       // (editForm as StandartForm).AddCloseEvent(LoadRecords);
                    }
                    editForm.Show();
                }
            }
        }

        private void ButtonPanelConfig_Click(object sender, EventArgs e)
        {
            if (!checkBoxSelectAll.Visible)
            {
                checkBoxSelectAll.Visible = true;
                buttonPanelConfig.BackgroundImage = Properties.Resources.Up;
                panelConfig.Height = 300;
            }
            else
            {
                checkBoxSelectAll.Visible = false;
                SetColumnAndRowsVisibility();
                buttonPanelConfig.BackgroundImage = Properties.Resources.Down;
                panelConfig.Height = 30;
            }
        }

        private void CheckBoxTimeNorm_CheckedChanged(object sender, EventArgs e)
        {
            SetColumnAndRowsVisibility();
        }

        private void CheckBoxLecturerLoad_CheckedChanged(object sender, EventArgs e)
        {
            SetColumnAndRowsVisibility();
        }

        private void СheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewColumns.Rows.Count; ++i)
            {
                dataGridViewColumns.Rows[i].Cells[3].Value = checkBoxSelectAll.Checked;
            }
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            UpdRecord();
        }

        private void ToolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void DataGridViewList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    UpdRecord();
                    break;
            }
        }

        private void DataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdRecord();
        }

        private void DataGridViewList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Vertical text from column 0, or adjust below, if first column(s) to be skipped
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.RotateTransform(270);
                e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
                e.Graphics.ResetTransform();
                e.Handled = true;
            }
            if(e.ColumnIndex == dataGridViewList.Columns.Count - 1 && e.RowIndex > 3)
            {
                if(e.Value != null)
                {
                    double fact = Convert.ToDouble(e.Value);
                    if (fact > 0)
                    {
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else if (fact < 0)
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                    }
                }
            }
            if((e.ColumnIndex >= _lecturerFirstColumnIndex || e.ColumnIndex <= _lecturerLastColumnIndex) && e.RowIndex == 2)
            {
                if(e.Value != null && dataGridViewList.Rows[1].Cells[e.ColumnIndex].Value != null)
                {
                    double plan = Convert.ToDouble(dataGridViewList.Rows[1].Cells[e.ColumnIndex].Value);
                    double fact = Convert.ToDouble(e.Value);
                    if(plan > fact)
                    {
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else if (plan < fact)
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }

        private void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);

                MethodInfo mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic);
                if (mi != null)
                {
                    mi.Invoke(c, new object[] { ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true });
                }

                mi = typeof(Control).GetMethod("UpdateStyles", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic);
                if (mi != null)
                {
                    mi.Invoke(c, null);
                }
            }
        }

        private void ButtonCalcFactHours_Click(object sender, EventArgs e)
        {
            if (comboBoxAcademicYear.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите произвести расчет по " + comboBoxAcademicYear.Text + " году?", "Портал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Guid id = new Guid(comboBoxAcademicYear.SelectedValue.ToString());
                    var result = _process.CalcFactHoursForAcademicYear(new AcademicYearGetBindingModel { Id = id });
                    if (!result.Succeeded)
                    {
                        ErrorMessanger.PrintErrorMessage("При расчете возникла ошибка: ", result.Errors);
                    }
                    LoadGrid();
                    MessageBox.Show("Готово!");
                }
            }
            else
            {
                MessageBox.Show("Выберите нужный год");
            }
        }

        private void ButtonCreateStatement_Click(object sender, EventArgs e)
        {
            // _serviceS.CreateAllFindStatement(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
        }

        private void ButtonCreateNIRRecords_Click(object sender, EventArgs e)
        {
            //_serviceIPR.CreateAllFindIndividualPlanRecords(new AcademicYearSetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
        }

        private void ButtonCreateDisciplineTimeDistributions_Click(object sender, EventArgs e)
        {
            var result = _process.CreateDisciplineTimeDistributions(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
            if(!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При создании расчасовок возникла ошибка: ", result.Errors);
            }
            else
            {
                MessageBox.Show("Готово", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonGetLecturerWorkload_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var result = _process.ImportLecturerWorkloads(new ImportLecturerWorkloadBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    Path = fbd.SelectedPath
                });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При выгрузке возникла ошибка: ", result.Errors);
                }
                else
                {
                    MessageBox.Show("Готово", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}