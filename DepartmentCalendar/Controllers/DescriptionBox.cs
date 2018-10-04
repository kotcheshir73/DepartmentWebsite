using DepartmentProcessAccountingService.BindingModels;
using DepartmentProcessAccountingService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepartmentWebsite.Controllers
{
    public class DescriptionBox : UserControl
    {
        private readonly IDepartmentProcessService _serviceD;
        private TextBox textBoxHead;
        private Button buttonAddDate;
        private CheckBox checkBoxCurrentDate;
        private CheckBox checkBoxSemesterDate;
        private ComboBox comboBoxSemesterDates;
        private Label labelHead;
        private Label labelProcess;
        private Label labelDescription;
        private ComboBox comboBoxProcess;
        private TextBox textBoxDescription;
        private MonthView _monthView;

        public DescriptionBox(MonthView monthView)
        {
            this._monthView = monthView;
            InitializeComponent();
        }

        public DescriptionBox()
        {
            InitializeComponent();
            this.textBoxHead.Enabled = false;
            this.comboBoxProcess.Enabled = false;
            this.textBoxDescription.Enabled = false;

            this.buttonAddDate.Visible = false;
            this.checkBoxCurrentDate.Visible = false;
            this.checkBoxSemesterDate.Visible = false;
            this.comboBoxSemesterDates.Visible = false;
        }

        private void InitializeComponent()
        {
            this.textBoxHead = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonAddDate = new System.Windows.Forms.Button();
            this.checkBoxCurrentDate = new System.Windows.Forms.CheckBox();
            this.checkBoxSemesterDate = new System.Windows.Forms.CheckBox();
            this.comboBoxSemesterDates = new System.Windows.Forms.ComboBox();
            this.labelHead = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.comboBoxProcess = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBoxHead
            // 
            this.textBoxHead.Location = new System.Drawing.Point(25, 109);
            this.textBoxHead.Name = "textBoxHead";
            this.textBoxHead.Size = new System.Drawing.Size(304, 20);
            this.textBoxHead.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(25, 203);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(304, 127);
            this.textBoxDescription.TabIndex = 2;
            // 
            // buttonAddDate
            // 
            this.buttonAddDate.Location = new System.Drawing.Point(202, 357);
            this.buttonAddDate.Name = "buttonAddDate";
            this.buttonAddDate.Size = new System.Drawing.Size(127, 23);
            this.buttonAddDate.TabIndex = 3;
            this.buttonAddDate.Text = "Добавить дату";
            this.buttonAddDate.UseVisualStyleBackColor = true;
            this.buttonAddDate.Click += new System.EventHandler(this.buttonAddDate_Click);
            // 
            // checkBoxCurrentDate
            // 
            this.checkBoxCurrentDate.AutoSize = true;
            this.checkBoxCurrentDate.Location = new System.Drawing.Point(25, 20);
            this.checkBoxCurrentDate.Name = "checkBoxCurrentDate";
            this.checkBoxCurrentDate.Size = new System.Drawing.Size(177, 17);
            this.checkBoxCurrentDate.TabIndex = 0;
            this.checkBoxCurrentDate.Text = "Добавить для выбранных дат";
            this.checkBoxCurrentDate.UseVisualStyleBackColor = true;
            // 
            // checkBoxSemesterDate
            // 
            this.checkBoxSemesterDate.AutoSize = true;
            this.checkBoxSemesterDate.Location = new System.Drawing.Point(25, 56);
            this.checkBoxSemesterDate.Name = "checkBoxSemesterDate";
            this.checkBoxSemesterDate.Size = new System.Drawing.Size(151, 17);
            this.checkBoxSemesterDate.TabIndex = 4;
            this.checkBoxSemesterDate.Text = "Начать с даты семестра";
            this.checkBoxSemesterDate.UseVisualStyleBackColor = true;
            // 
            // comboBoxSemesterDates
            // 
            this.comboBoxSemesterDates.FormattingEnabled = true;
            this.comboBoxSemesterDates.Location = new System.Drawing.Point(186, 54);
            this.comboBoxSemesterDates.Name = "comboBoxSemesterDates";
            this.comboBoxSemesterDates.Size = new System.Drawing.Size(143, 21);
            this.comboBoxSemesterDates.TabIndex = 5;
            // 
            // labelHead
            // 
            this.labelHead.AutoSize = true;
            this.labelHead.Location = new System.Drawing.Point(22, 93);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(64, 13);
            this.labelHead.TabIndex = 6;
            this.labelHead.Text = "Заголовок:";
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(22, 144);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(44, 13);
            this.labelProcess.TabIndex = 7;
            this.labelProcess.Text = "Статус:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(22, 187);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 8;
            this.labelDescription.Text = "Описание:";
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Location = new System.Drawing.Point(82, 141);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(247, 21);
            this.comboBoxProcess.TabIndex = 9;
            // 
            // DescriptionBox
            // 
            this.Controls.Add(this.comboBoxProcess);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.labelHead);
            this.Controls.Add(this.comboBoxSemesterDates);
            this.Controls.Add(this.checkBoxSemesterDate);
            this.Controls.Add(this.checkBoxCurrentDate);
            this.Controls.Add(this.buttonAddDate);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxHead);
            this.Name = "DescriptionBox";
            this.Size = new System.Drawing.Size(364, 404);
            this.Load += new System.EventHandler(this.DescriptionBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonAddDate_Click(object sender, System.EventArgs e)
        {
            //Сохранять событие в БД
        }

        private void DescriptionBox_Load(object sender, EventArgs e)
        {
        //    var resultD = _serviceD.GetAcademicYearProcesses(new AcademicYearProcessGetBindingModel { });
        //    if (!resultD.Succeeded)
        //    {
        //        MessageBox.Show("Error");
        //        Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
        //        return;
        //    }

        //    comboBoxProcess.ValueMember = "Value";
        //    comboBoxProcess.DisplayMember = "Display";
        //    comboBoxProcess.DataSource = resultD.Result.List.Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
        //    comboBoxSemesterDates.SelectedItem = null;
        }
    }
}
