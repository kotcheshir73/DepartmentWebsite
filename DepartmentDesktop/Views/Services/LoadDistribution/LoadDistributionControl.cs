using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.LoadDistribution
{
	public partial class LoadDistributionControl : UserControl
	{
        private readonly IAcademicYearService _serviceAY;

        private readonly ITimeNormService _serviceTN;

		private readonly IEducationalProcessService _serviceEP;

        private bool notLoading;

		public LoadDistributionControl(IAcademicYearService serviceAY, ITimeNormService serviceTN, IEducationalProcessService serviceEP)
		{
			InitializeComponent();
            _serviceAY = serviceAY;
            _serviceTN = serviceTN;
            _serviceEP = serviceEP;

            setDoubleBuffered(dataGridViewList, true);
		}

		public void LoadData()
        {
            var resultAY = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
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
            }
        }

        private void LoadGrid()
        {
            Guid id = new Guid(comboBoxAcademicYear.SelectedValue.ToString());
            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "APR_Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Semester", Title = "Сем", Width = 50, Visible = true },
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
                columns.Add(new ColumnConfig { Name = string.Format("APRE_Id{0}", tn.Id), Title = "Id", Width = 100, Visible = false });
                columns.Add(new ColumnConfig { Name = string.Format("APRE_Plan{0}", tn.Id), Title = tn.TimeNormShortName, Width = 40, Visible = true });
                columns.Add(new ColumnConfig { Name = string.Format("APRE_Fact{0}", tn.Id), Title = tn.TimeNormShortName, Width = 40, Visible = true });
            }
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
        }

        private void LoadRecords()
		{
            var result = _serviceEP.GetAcademicYearLoading(new AcademicYearGetBindingModel { Id = new Guid(comboBoxAcademicYear.SelectedValue.ToString()) });
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
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
        }

		private void AddRecord()
		{
			
		}

		private void UpdRecord()
		{
			
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
                    var result = _serviceEP.CalcFactHoursForAcademicYear(new AcademicYearGetBindingModel { Id = id });
                    if (!result.Succeeded)
                    {
                        Program.PrintErrorMessage("При расчете возникла ошибка: ", result.Errors);
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
    }
}
