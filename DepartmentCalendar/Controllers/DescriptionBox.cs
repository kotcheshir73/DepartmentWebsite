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
using Unity;
using Unity.Attributes;

namespace DepartmentWebsite.Controllers
{
    public class DescriptionBox : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        #region Переменные
        private TextBox textBoxHead;
        private Button buttonAddDate;
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

        private List<DepartmentProcessViewModel> processes = null;
        private Guid currentProcessId = Guid.Empty;
        private DateTimePicker dateTimePicker;
        private Label label1;
        private ComboBox comboBoxExecutor;
        private Label label2;
        private CheckBox checkBoxCon;
        private bool add = false;
        #endregion

        public DescriptionBox(MonthView monthView, IDepartmentProcessService service, bool add)
        {
            this.service = service;

            this._monthView = monthView;
            this.add = add;
            InitializeComponent();
            //Добавление дат семестра в comboBox
            comboBoxSemesterDates.DataSource = Enum.GetValues(typeof(DepartmentModel.Enums.SemesterDates));
            comboBoxSemesterDates.SelectedItem = DepartmentModel.Enums.SemesterDates.НПП;

            //Добавление статусов в comboBox
            comboBoxProcess.DataSource = Enum.GetValues(typeof(DepartmentModel.Enums.ProcessStatus));
            comboBoxProcess.SelectedItem = DepartmentModel.Enums.ProcessStatus.запущен;
            
            comboBoxExecutor.DataSource = Enum.GetValues(typeof(DepartmentModel.Enums.Post));
            comboBoxExecutor.SelectedItem = DepartmentModel.Enums.Post.ЗаведующийКафедрой;

            if (add)
            {
                this.buttonBackward.Visible = false;
                this.buttonForward.Visible = false;
                this.buttonDeleteProcess.Visible = false;
                dateTimePicker.Value = _monthView.SelectionStart;
            }
            else
            {
                this.textBoxHead.Enabled = false;
                this.comboBoxProcess.Enabled = false;
                this.textBoxDescription.Enabled = false;
                comboBoxExecutor.Enabled = false;
                dateTimePicker.Enabled = false;
                checkBoxCon.Enabled = false;

                this.buttonAddDate.Visible = false;
                this.checkBoxSemesterDate.Visible = false;
                this.comboBoxSemesterDates.Visible = false;
            }

        }

        private void InitializeComponent()
        {
            this.textBoxHead = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonAddDate = new System.Windows.Forms.Button();
            this.checkBoxSemesterDate = new System.Windows.Forms.CheckBox();
            this.comboBoxSemesterDates = new System.Windows.Forms.ComboBox();
            this.labelHead = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.comboBoxProcess = new System.Windows.Forms.ComboBox();
            this.buttonBackward = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonDeleteProcess = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxExecutor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxCon = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxHead
            // 
            this.textBoxHead.Location = new System.Drawing.Point(24, 135);
            this.textBoxHead.Name = "textBoxHead";
            this.textBoxHead.Size = new System.Drawing.Size(304, 22);
            this.textBoxHead.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(24, 214);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(304, 104);
            this.textBoxDescription.TabIndex = 2;
            // 
            // buttonAddDate
            // 
            this.buttonAddDate.Location = new System.Drawing.Point(201, 362);
            this.buttonAddDate.Name = "buttonAddDate";
            this.buttonAddDate.Size = new System.Drawing.Size(127, 23);
            this.buttonAddDate.TabIndex = 3;
            this.buttonAddDate.Text = "Добавить событие";
            this.buttonAddDate.UseVisualStyleBackColor = true;
            this.buttonAddDate.Click += new System.EventHandler(this.buttonAddDate_Click);
            // 
            // checkBoxSemesterDate
            // 
            this.checkBoxSemesterDate.AutoSize = true;
            this.checkBoxSemesterDate.Location = new System.Drawing.Point(24, 89);
            this.checkBoxSemesterDate.Name = "checkBoxSemesterDate";
            this.checkBoxSemesterDate.Size = new System.Drawing.Size(192, 21);
            this.checkBoxSemesterDate.TabIndex = 4;
            this.checkBoxSemesterDate.Text = "Начать с даты семестра";
            this.checkBoxSemesterDate.UseVisualStyleBackColor = true;
            this.checkBoxSemesterDate.CheckedChanged += new System.EventHandler(this.checkBoxSemesterDate_CheckedChanged);
            // 
            // comboBoxSemesterDates
            // 
            this.comboBoxSemesterDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSemesterDates.FormattingEnabled = true;
            this.comboBoxSemesterDates.Location = new System.Drawing.Point(185, 87);
            this.comboBoxSemesterDates.Name = "comboBoxSemesterDates";
            this.comboBoxSemesterDates.Size = new System.Drawing.Size(143, 24);
            this.comboBoxSemesterDates.TabIndex = 5;
            // 
            // labelHead
            // 
            this.labelHead.AutoSize = true;
            this.labelHead.Location = new System.Drawing.Point(21, 119);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(80, 17);
            this.labelHead.TabIndex = 6;
            this.labelHead.Text = "Заголовок:";
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(21, 173);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(57, 17);
            this.labelProcess.TabIndex = 7;
            this.labelProcess.Text = "Статус:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(21, 198);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(78, 17);
            this.labelDescription.TabIndex = 8;
            this.labelDescription.Text = "Описание:";
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Location = new System.Drawing.Point(81, 170);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(247, 24);
            this.comboBoxProcess.TabIndex = 9;
            // 
            // buttonBackward
            // 
            this.buttonBackward.Location = new System.Drawing.Point(24, 333);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(75, 23);
            this.buttonBackward.TabIndex = 10;
            this.buttonBackward.Text = "<";
            this.buttonBackward.UseVisualStyleBackColor = true;
            this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(253, 333);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(75, 23);
            this.buttonForward.TabIndex = 11;
            this.buttonForward.Text = ">";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonDeleteProcess
            // 
            this.buttonDeleteProcess.Location = new System.Drawing.Point(201, 391);
            this.buttonDeleteProcess.Name = "buttonDeleteProcess";
            this.buttonDeleteProcess.Size = new System.Drawing.Size(127, 23);
            this.buttonDeleteProcess.TabIndex = 12;
            this.buttonDeleteProcess.Text = "Удалить событие";
            this.buttonDeleteProcess.UseVisualStyleBackColor = true;
            this.buttonDeleteProcess.Click += new System.EventHandler(this.buttonDeleteProcess_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(128, 16);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Дата начала:";
            // 
            // comboBoxExecutor
            // 
            this.comboBoxExecutor.FormattingEnabled = true;
            this.comboBoxExecutor.Location = new System.Drawing.Point(128, 51);
            this.comboBoxExecutor.Name = "comboBoxExecutor";
            this.comboBoxExecutor.Size = new System.Drawing.Size(121, 24);
            this.comboBoxExecutor.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Исполнитель:";
            // 
            // checkBoxCon
            // 
            this.checkBoxCon.AutoSize = true;
            this.checkBoxCon.Location = new System.Drawing.Point(24, 364);
            this.checkBoxCon.Name = "checkBoxCon";
            this.checkBoxCon.Size = new System.Drawing.Size(146, 21);
            this.checkBoxCon.TabIndex = 17;
            this.checkBoxCon.Text = "Подтверждаемый";
            this.checkBoxCon.UseVisualStyleBackColor = true;
            // 
            // DescriptionBox
            // 
            this.Controls.Add(this.checkBoxCon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxExecutor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.buttonDeleteProcess);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.buttonBackward);
            this.Controls.Add(this.comboBoxProcess);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.labelHead);
            this.Controls.Add(this.comboBoxSemesterDates);
            this.Controls.Add(this.checkBoxSemesterDate);
            this.Controls.Add(this.buttonAddDate);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxHead);
            this.Name = "DescriptionBox";
            this.Size = new System.Drawing.Size(342, 437);
            this.Load += new System.EventHandler(this.DescriptionBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonAddDate_Click(object sender, EventArgs e)
        {
            //Сохранять событие в БД
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
                if (add && eventId.HasValue)
                {
                    Enum.TryParse<DepartmentModel.Enums.Post>(comboBoxExecutor.SelectedValue.ToString(), out DepartmentModel.Enums.Post executor);
                    service.UpdateDepartmentProcess(new DepartmentProcessRecordBindingModel
                    {
                        Id = eventId.Value,
                        Description = textBoxDescription.Text,
                        Confirmability = checkBoxCon.Checked,
                        Executor = executor,
                        Title = textBoxHead.Text,
                        DateStart = dateTimePicker.Value,
                        DateFinish = _monthView.SelectionEnd
                    }
                    );
                }
                else
                {
                    Enum.TryParse<DepartmentModel.Enums.Post>(comboBoxExecutor.SelectedValue.ToString(), out DepartmentModel.Enums.Post executor);
                    service.CreateDepartmentProcess(new DepartmentProcessRecordBindingModel
                    {
                        Confirmability = checkBoxCon.Checked,
                        Executor = executor,
                        Description = textBoxDescription.Text,
                        Title = textBoxHead.Text,
                        DateStart = dateTimePicker.Value,
                        DateFinish = _monthView.SelectionEnd
                    }
                    );
                }
                MessageBox.Show("Добавлено новый процесс", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    var result = service.GetDepartmentProcess(new DepartmentProcessGetBindingModel { Id = eventId });
                    if (result.Succeeded)
                    {
                        DepartmentProcessViewModel view = result.Result;
                        if (view != null)
                        {
                            textBoxHead.Text = view.Title;
                            textBoxDescription.Text = view.Description;
                            comboBoxExecutor.SelectedValue = view.Executor;
                            dateTimePicker.Value = view.DateStart.Value;
                            checkBoxCon.Checked = view.Confirmability;
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
            else
            {
                if (!add)
                {
                    var res = service.GetDepartmentProcesses(new DepartmentProcessGetBindingModel { }).Result;
                    if (res != null)
                    {
                        processes = res.List;
                        if (processes.Count > 0)
                            loadProcess(processes[0].Id);
                    }
                }
            }
        }

        private void loadProcess(Guid id)
        {
            var res = service.GetDepartmentProcess(new DepartmentProcessGetBindingModel { Id = id });
            if (res.Result != null)
            {
                textBoxHead.Text = res.Result.Title;
                textBoxDescription.Text = res.Result.Description;
                comboBoxExecutor.SelectedItem = res.Result.Executor;
                dateTimePicker.Value = res.Result.DateStart.Value;
                checkBoxCon.Checked = res.Result.Confirmability;
            }
            currentProcessId = res.Result.Id;
            this.Refresh();
        }

        private void buttonDeleteProcess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить процесс?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (currentProcessId != Guid.Empty)
                {
                    try
                    {
                        service.DeleteDepartmentProcess(new DepartmentProcessGetBindingModel
                        {
                            Id = currentProcessId
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Удалено", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    processes = service.GetDepartmentProcesses(new DepartmentProcessGetBindingModel { }).Result.List;
                    if (processes!=null&&processes.Count > 0)
                    {
                        currentProcessId = processes[0].Id;
                        loadProcess(currentProcessId);
                    }
                }
            }
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            if (currentProcessId != Guid.Empty)
            {
                var res = service.GetDepartmentProcess(new DepartmentProcessGetBindingModel { Id = currentProcessId }).Result;
                var flag = processes.FirstOrDefault(x => x.Id == res.Id);
                int index = 0;
                if (flag != null)
                {
                    for (int i = 0; i < processes.Count; i++)
                    {
                        if (processes[i].Id == res.Id) break;
                        index++;
                    }
                }
                if (0 < index)
                {
                    currentProcessId = processes[index - 1].Id;
                    loadProcess(currentProcessId);
                }
            }
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (currentProcessId != Guid.Empty)
            {
                var res = service.GetDepartmentProcess(new DepartmentProcessGetBindingModel { Id = currentProcessId }).Result;
                if (res == null) return;
                var flag = processes.FirstOrDefault(x => x.Id == res.Id);
                int index = 0;
                if (flag != null)
                {
                    for (int i = 0; i < processes.Count; i++)
                    {
                        if (processes[i].Id == res.Id) break;
                        index++;
                    }
                }
                if (index < processes.Count - 1)
                {
                    currentProcessId = processes[index + 1].Id;
                    loadProcess(currentProcessId);
                }
            }
        }

        private void checkBoxSemesterDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSemesterDate.Checked)
                dateTimePicker.Enabled = false;
            else dateTimePicker.Enabled = true;
        }
    }
}
