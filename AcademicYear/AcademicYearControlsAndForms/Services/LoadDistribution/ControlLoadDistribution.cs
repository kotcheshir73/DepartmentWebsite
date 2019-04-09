using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
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

namespace AcademicYearControlsAndForms.Services.LoadDistribution
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

        private bool notLoading;

		public ControlLoadDistribution(IAcademicYearService serviceAY, ITimeNormService serviceTN, IAcademicYearProcess process, ILecturerService serviceL,/* IStatementService serviceS, 
            IIndividualPlanRecordService serviceIPR,*/ IDisciplineTimeDistributionService serviceDTD, ILecturerWorkloadService serviceLW, ILecturerPostSerivce serviceLP)
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

            /*   это нужно было для создания ставок, пока жалко удалять
            
            var lecturerList = serviceL.GetLecturers(new LecturerGetBindingModel());  // 134ED8EA-5BC8-4FF1-9A52-8FF6DD8775FC
            foreach(var tmp in lecturerList.Result.List)
            {
                string name = tmp.FullName;
                serviceLW.CreateLecturerWorkload(new LecturerWorkloadSetBindingModel()
                {
                    AcademicYearId = new Guid("134ED8EA-5BC8-4FF1-9A52-8FF6DD8775FC"),
                    LecturerId = tmp.Id,
                    Workload = 1
                });
            }
            */
            setDoubleBuffered(dataGridViewList, true);
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

        private void comboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicYear.SelectedValue != null && !notLoading)
            {
                LoadGrid();
                LoadRecords();
                editHoursForAllLecturers();
            }
        }

        private void LoadGrid()
        {
            Guid id = new Guid(comboBoxAcademicYear.SelectedValue.ToString());
            List<ColumnConfig> columns = new List<ColumnConfig>
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
                columns.Add(new ColumnConfig { Name = string.Format("APRE_Id_{0}", tn.Id), Title = "Id", Width = 100, Visible = false });
                columns.Add(new ColumnConfig { Name = string.Format("APRE_Plan_{0}", tn.Id), Title = tn.TimeNormShortName, Width = 40, Visible = true });
                columns.Add(new ColumnConfig { Name = string.Format("APRE_Fact_{0}", tn.Id), Title = tn.TimeNormShortName, Width = 40, Visible = true });
            }
            columns.Add(new ColumnConfig { Name = "Itog_APR", Title = "Итого (дисц.)", Width = 40, Visible = true });
            var lecturers = _serviceL.GetLecturers(new LecturerGetBindingModel () );
            foreach (var lect in lecturers.Result.List)
            {
                // ниже костыль, нужно исправить
                var workloadThisLector = _serviceLW.GetLecturerWorkloads(new LecturerWorkloadGetBindingModel()
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    LecturerId = lect.Id
                });
                var hourThisLector = _serviceLP.GetLecturerPost(new LecturerPostGetBindingModel()
                {
                    Id = lect.LecturerPostId
                });
                columns.Add(new ColumnConfig { Name = string.Format("Lecturer_{0}", lect.Id), Title = lect.FullName + "\n" 
                    + (workloadThisLector.Result.List.Count == 0 ? "" : workloadThisLector.Result.List[0].Workload.ToString() + " - " 
                    + hourThisLector.Result.Hours * workloadThisLector.Result.List[0].Workload + " - "), Width = 50, Visible = true });
            }
            columns.Add(new ColumnConfig { Name = "Itog_Lecturer", Title = "Итого (лект.)", Width = 40, Visible = true });
            columns.Add(new ColumnConfig { Name = "Itog", Title = "Итого", Width = 40, Visible = true });
            dataGridViewList.Columns.Clear();
            foreach (var column in columns)
            {
                dataGridViewList.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = column.Title,
                    Name = string.Format("Column{0}", column.Name),
                    ReadOnly = true,
                    Visible = column.Visible,
                    Width = column.Width.HasValue ? column.Width.Value : 0,
                    AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill
                });
            }
            dataGridViewList.Columns[3].Frozen = true;

        }

        private void LoadRecords()
		{
            var result = _process.GetAcademicYearLoading(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
            if (result.Succeeded)
            {
                dataGridViewList.Rows.Clear();
                foreach (var elem in result.Result)
                {
                    dataGridViewList.Rows.Add(elem);
                }
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }

		private void AddRecord()
		{

        }

        private void UpdRecord()
		{
            int i, j;
            if(dataGridViewList.SelectedCells.Count == 1)
            {
                i = dataGridViewList.SelectedCells[0].RowIndex;
                j = dataGridViewList.SelectedCells[0].ColumnIndex;

                if (dataGridViewList[0, i].Value == null)
                {
                    return;
                }
                
                if (dataGridViewList.Columns[j].Name.StartsWith("ColumnLecturer"))
                {
                    var editForm = Container.Resolve<LoadDistributionEditForm>(
                        new ParameterOverrides
                        {
                            { "academicYearId", comboBoxAcademicYear.SelectedValue.ToString() },
                            { "academicPlanRecordId", dataGridViewList[0, i].Value.ToString() },
                            { "lecturerId", dataGridViewList.Columns[j].Name.Split('_')[1] },
                            { "disciplineName", dataGridViewList[3, i].Value.ToString() },
                            { "lecturerName", dataGridViewList.Columns[j].HeaderText }
                        }
                        .OnType<LoadDistributionEditForm>());
                    editForm.Show();
                }
                else if(dataGridViewList.Columns[j].Name.StartsWith("ColumnAPRE"))
                {
                    var editForm = Container.Resolve<LoadDistributionEditForm>(
                        new ParameterOverrides
                        {
                            { "academicYearId", comboBoxAcademicYear.SelectedValue.ToString() },
                            { "academicPlanRecordId", dataGridViewList[0, i].Value.ToString() },
                            { "lecturerId", "" },
                            { "disciplineName", dataGridViewList[3, i].Value.ToString() },
                            { "lecturerName", "" }
                        }
                        .OnType<LoadDistributionEditForm>());
                    editForm.Show();
                }
            }
		}

		private void DelRecord()
		{
			
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			AddRecord();
		}

		private void toolStripButtonUpd_Click(object sender, EventArgs e)
		{
			UpdRecord();
		}

		private void toolStripButtonDel_Click(object sender, EventArgs e)
		{
			DelRecord();
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadRecords();
            editHoursForAllLecturers();
        }

		private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
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
		}

		private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			UpdRecord();
		}

        private void dataGridViewList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
        }

        private void setDoubleBuffered(Control c, bool value)
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

        private void buttonCalcFactHours_Click(object sender, EventArgs e)
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
                    LoadRecords();
                    MessageBox.Show("Готово!");
                }
            }
            else
            {
                MessageBox.Show("Выберите нужный год");
            }
        }

        private void buttonCreatStatement_Click(object sender, EventArgs e)
        {
           // _serviceS.CreateAllFindStatement(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
        }

        private void editHoursForAllLecturers()
        {
            for (int j = 0; j < dataGridViewList.Columns.Count; j++)
            {
                if (dataGridViewList.Columns[j].Name.StartsWith("ColumnLecturer"))
                {
                    editHoursForLecturer(j);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //_serviceIPR.CreateAllFindIndividualPlanRecords(new AcademicYearSetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //_serviceDTD.CreateAllFindGrafic(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });

            //for(int j = 0; j < dataGridViewList.Columns.Count; j++)
            //{
            //    if (dataGridViewList.Columns[j].Name.StartsWith("ColumnLecturer"))
            //    {
            //        editHoursForLecturer(j);
            //    }
            //}
        }

        private void editHoursForLecturer(int j)
        {
            if (dataGridViewList.Columns[j].Name.StartsWith("ColumnLecturer"))
            {
                dataGridViewList.Columns[j].HeaderText = dataGridViewList.Columns[j].HeaderText.Split('-')[0] + "-"
                    + dataGridViewList.Columns[j].HeaderText.Split('-')[1] + "- " 
                    + (Convert.ToDouble(dataGridViewList.Columns[j].HeaderText.Split('-')[1]) - getSumHourOfLecturer(j));
            }
        }

        private double getSumHourOfLecturer(int j)
        {
            double sum = 0;
            for(int i = 0; i < dataGridViewList.Rows.Count; i++)
            {
                sum += dataGridViewList[j, i].Value == null ? 0 : Convert.ToDouble(dataGridViewList[j, i].Value);
            }
            return sum;
        }
    }
}