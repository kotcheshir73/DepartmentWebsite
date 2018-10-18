using DepartmentProcessAccountingService.BindingModels;
using DepartmentProcessAccountingService.IServices;
using DepartmentProcessAccountingService.ViewModels;
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
        #region Переменные
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
        private Button buttonBackward;
        private Button buttonForward;
        private Button buttonDeleteProcess;
        private MonthView _monthView;

        private readonly IDepartmentProcessService service;

        public List<Guid> EventsIds { set { eventsIds = value; } }
        private List<Guid> eventsIds;

        public Guid EvenvtId { set { eventId = value; } }
        private Guid? eventId;
        #endregion

        public DescriptionBox(MonthView monthView)
        {
            this._monthView = monthView;
            InitializeComponent();
            //Добавление дат семестра в comboBox
            comboBoxSemesterDates.DataSource = Enum.GetValues(typeof(DepartmentModel.Enums.SemesterDates));
            comboBoxSemesterDates.SelectedItem = DepartmentModel.Enums.SemesterDates.НПП;

            //Добавление статусов в comboBox
            comboBoxProcess.DataSource = Enum.GetValues(typeof(DepartmentModel.Enums.ProcessStatus));
            comboBoxProcess.SelectedItem = DepartmentModel.Enums.ProcessStatus.запущен;

            this.buttonBackward.Visible = false;
            this.buttonForward.Visible = false;
            this.buttonDeleteProcess.Visible = false;
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
            this.buttonBackward = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonDeleteProcess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxHead
            // 
            this.textBoxHead.Location = new System.Drawing.Point(25, 102);
            this.textBoxHead.Name = "textBoxHead";
            this.textBoxHead.Size = new System.Drawing.Size(304, 20);
            this.textBoxHead.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(25, 181);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(304, 104);
            this.textBoxDescription.TabIndex = 2;
            // 
            // buttonAddDate
            // 
            this.buttonAddDate.Location = new System.Drawing.Point(202, 329);
            this.buttonAddDate.Name = "buttonAddDate";
            this.buttonAddDate.Size = new System.Drawing.Size(127, 23);
            this.buttonAddDate.TabIndex = 3;
            this.buttonAddDate.Text = "Добавить событие";
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
            this.comboBoxSemesterDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemesterDates.FormattingEnabled = true;
            this.comboBoxSemesterDates.Location = new System.Drawing.Point(186, 54);
            this.comboBoxSemesterDates.Name = "comboBoxSemesterDates";
            this.comboBoxSemesterDates.Size = new System.Drawing.Size(143, 21);
            this.comboBoxSemesterDates.TabIndex = 5;
            // 
            // labelHead
            // 
            this.labelHead.AutoSize = true;
            this.labelHead.Location = new System.Drawing.Point(22, 86);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(64, 13);
            this.labelHead.TabIndex = 6;
            this.labelHead.Text = "Заголовок:";
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(22, 140);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(44, 13);
            this.labelProcess.TabIndex = 7;
            this.labelProcess.Text = "Статус:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(22, 165);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 8;
            this.labelDescription.Text = "Описание:";
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Location = new System.Drawing.Point(82, 137);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(247, 21);
            this.comboBoxProcess.TabIndex = 9;
            // 
            // buttonBackward
            // 
            this.buttonBackward.Location = new System.Drawing.Point(25, 300);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(75, 23);
            this.buttonBackward.TabIndex = 10;
            this.buttonBackward.Text = "<";
            this.buttonBackward.UseVisualStyleBackColor = true;
            this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(254, 300);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(75, 23);
            this.buttonForward.TabIndex = 11;
            this.buttonForward.Text = ">";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonDeleteProcess
            // 
            this.buttonDeleteProcess.Location = new System.Drawing.Point(202, 358);
            this.buttonDeleteProcess.Name = "buttonDeleteProcess";
            this.buttonDeleteProcess.Size = new System.Drawing.Size(127, 23);
            this.buttonDeleteProcess.TabIndex = 12;
            this.buttonDeleteProcess.Text = "Удалить событие";
            this.buttonDeleteProcess.UseVisualStyleBackColor = true;
            this.buttonDeleteProcess.Click += new System.EventHandler(this.buttonDeleteProcess_Click);
            // 
            // DescriptionBox
            // 
            this.Controls.Add(this.buttonDeleteProcess);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.buttonBackward);
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
            this.Size = new System.Drawing.Size(364, 455);
            this.Load += new System.EventHandler(this.DescriptionBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonAddDate_Click(object sender, EventArgs e)
        {
            //Сохранять событие в БД
            if (!checkBoxCurrentDate.Checked || !checkBoxSemesterDate.Checked)
            {
                MessageBox.Show("Параметр даты не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBoxSemesterDate.Checked && comboBoxSemesterDates.SelectedItem == null)
            {
                MessageBox.Show("Дата семестра не выбрана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxHead.Text))
            {
                MessageBox.Show("Не введен заголовок события", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (eventId.HasValue)
                {
                    service.UpdateDepartmentProcess(new DepartmentProcessRecordBindingModel
                    {
                        Id = eventId.Value,
                        Description = textBoxDescription.Text
                    }
                    );
                }
                else
                {
                    service.CreateDepartmentProcess(new DepartmentProcessRecordBindingModel
                    {
                        Description = textBoxDescription.Text,
                        Title = textBoxHead.Text
                    }
                    );
                }
                MessageBox.Show("Добавлено новое блюдо", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DescriptionBox_Load(object sender, EventArgs e)
        {
            if (eventId.HasValue)
            {
                try
                {
                    var dpgbm = new DepartmentProcessGetBindingModel();
                    dpgbm.Id = eventId.Value;
                    var result = service.GetDepartmentProcess(dpgbm);
                    if (result.Succeeded)
                    {
                        DepartmentProcessViewModel view = result.Result;
                        if (view != null)
                        {
                            textBoxHead.Text = view.Title;
                            //comboBoxProcess.SelectedItem = view.P;
                            textBoxDescription.Text = view.Description;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить процесс", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonDeleteProcess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить процесс?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    service.DeleteDepartmentProcess(new DepartmentProcessGetBindingModel
                    {
                        Id = eventId.Value
                    }
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            if (eventId.HasValue)
            {
                int index = eventsIds.IndexOf(eventId.Value);
                if (0 < index)
                {
                    eventId = eventsIds[index - 1];
                }
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (eventId.HasValue)
            {
                int index = eventsIds.IndexOf(eventId.Value);
                if (index < eventsIds.Count - 1)
                {
                    eventId = eventsIds[index + 1];
                }
            }
        }
    }
}
