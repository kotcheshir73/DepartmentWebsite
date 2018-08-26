namespace DepartmentDesktop.Views.EducationalProcess.Progress
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDisciplines = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLectures = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewLectures = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonLecturesAdd = new System.Windows.Forms.Button();
            this.buttonLecturesDelete = new System.Windows.Forms.Button();
            this.buttonLecturesUpdate = new System.Windows.Forms.Button();
            this.tabPageLabs = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewLabs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonLabsAdd = new System.Windows.Forms.Button();
            this.buttonLabsDelete = new System.Windows.Forms.Button();
            this.buttonLabsUpdate = new System.Windows.Forms.Button();
            this.tabPagePractices = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewPractices = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonPracticesAdd = new System.Windows.Forms.Button();
            this.buttonPracticesDelete = new System.Windows.Forms.Button();
            this.buttonPracticesUpdate = new System.Windows.Forms.Button();
            this.tabPageCourseWorks = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewCourseWorks = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonCourseWorksAdd = new System.Windows.Forms.Button();
            this.buttonCourseWorksDelete = new System.Windows.Forms.Button();
            this.buttonCourseWorksUpdate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageLectures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLectures)).BeginInit();
            this.tabPageLabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLabs)).BeginInit();
            this.tabPagePractices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPractices)).BeginInit();
            this.tabPageCourseWorks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCourseWorks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дисциплина";
            // 
            // comboBoxDisciplines
            // 
            this.comboBoxDisciplines.FormattingEnabled = true;
            this.comboBoxDisciplines.Location = new System.Drawing.Point(98, 13);
            this.comboBoxDisciplines.Name = "comboBoxDisciplines";
            this.comboBoxDisciplines.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDisciplines.TabIndex = 2;
            this.comboBoxDisciplines.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplines_SelectedIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLectures);
            this.tabControl.Controls.Add(this.tabPageLabs);
            this.tabControl.Controls.Add(this.tabPagePractices);
            this.tabControl.Controls.Add(this.tabPageCourseWorks);
            this.tabControl.Location = new System.Drawing.Point(12, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(816, 312);
            this.tabControl.TabIndex = 4;
            this.tabControl.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // tabPageLectures
            // 
            this.tabPageLectures.Controls.Add(this.label2);
            this.tabPageLectures.Controls.Add(this.dataGridViewLectures);
            this.tabPageLectures.Controls.Add(this.buttonLecturesAdd);
            this.tabPageLectures.Controls.Add(this.buttonLecturesDelete);
            this.tabPageLectures.Controls.Add(this.buttonLecturesUpdate);
            this.tabPageLectures.Location = new System.Drawing.Point(4, 22);
            this.tabPageLectures.Name = "tabPageLectures";
            this.tabPageLectures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLectures.Size = new System.Drawing.Size(808, 286);
            this.tabPageLectures.TabIndex = 0;
            this.tabPageLectures.Text = "Лекции";
            this.tabPageLectures.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Занятия";
            // 
            // dataGridViewLectures
            // 
            this.dataGridViewLectures.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewLectures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLectures.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.dataGridViewLectures.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewLectures.Location = new System.Drawing.Point(8, 23);
            this.dataGridViewLectures.Name = "dataGridViewLectures";
            this.dataGridViewLectures.Size = new System.Drawing.Size(678, 256);
            this.dataGridViewLectures.TabIndex = 13;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Тема";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 270;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Дата проведения";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 120;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "Кол-во пар";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Width = 120;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "Кол-во заданий";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Width = 120;
            // 
            // buttonLecturesAdd
            // 
            this.buttonLecturesAdd.Location = new System.Drawing.Point(692, 23);
            this.buttonLecturesAdd.Name = "buttonLecturesAdd";
            this.buttonLecturesAdd.Size = new System.Drawing.Size(111, 23);
            this.buttonLecturesAdd.TabIndex = 15;
            this.buttonLecturesAdd.Text = "Добавить";
            this.buttonLecturesAdd.UseVisualStyleBackColor = true;
            this.buttonLecturesAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonLecturesDelete
            // 
            this.buttonLecturesDelete.Location = new System.Drawing.Point(692, 81);
            this.buttonLecturesDelete.Name = "buttonLecturesDelete";
            this.buttonLecturesDelete.Size = new System.Drawing.Size(111, 23);
            this.buttonLecturesDelete.TabIndex = 17;
            this.buttonLecturesDelete.Text = "Удалить";
            this.buttonLecturesDelete.UseVisualStyleBackColor = true;
            this.buttonLecturesDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonLecturesUpdate
            // 
            this.buttonLecturesUpdate.Location = new System.Drawing.Point(692, 52);
            this.buttonLecturesUpdate.Name = "buttonLecturesUpdate";
            this.buttonLecturesUpdate.Size = new System.Drawing.Size(111, 23);
            this.buttonLecturesUpdate.TabIndex = 16;
            this.buttonLecturesUpdate.Text = "Редактировать";
            this.buttonLecturesUpdate.UseVisualStyleBackColor = true;
            // 
            // tabPageLabs
            // 
            this.tabPageLabs.Controls.Add(this.label3);
            this.tabPageLabs.Controls.Add(this.dataGridViewLabs);
            this.tabPageLabs.Controls.Add(this.buttonLabsAdd);
            this.tabPageLabs.Controls.Add(this.buttonLabsDelete);
            this.tabPageLabs.Controls.Add(this.buttonLabsUpdate);
            this.tabPageLabs.Location = new System.Drawing.Point(4, 22);
            this.tabPageLabs.Name = "tabPageLabs";
            this.tabPageLabs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLabs.Size = new System.Drawing.Size(808, 286);
            this.tabPageLabs.TabIndex = 1;
            this.tabPageLabs.Text = "Лабораторные";
            this.tabPageLabs.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Занятия";
            // 
            // dataGridViewLabs
            // 
            this.dataGridViewLabs.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewLabs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLabs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridViewLabs.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewLabs.Location = new System.Drawing.Point(8, 23);
            this.dataGridViewLabs.Name = "dataGridViewLabs";
            this.dataGridViewLabs.Size = new System.Drawing.Size(678, 256);
            this.dataGridViewLabs.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Тема";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 270;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Дата проведения";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Кол-во пар";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Кол-во заданий";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // buttonLabsAdd
            // 
            this.buttonLabsAdd.Location = new System.Drawing.Point(692, 23);
            this.buttonLabsAdd.Name = "buttonLabsAdd";
            this.buttonLabsAdd.Size = new System.Drawing.Size(111, 23);
            this.buttonLabsAdd.TabIndex = 10;
            this.buttonLabsAdd.Text = "Добавить";
            this.buttonLabsAdd.UseVisualStyleBackColor = true;
            this.buttonLabsAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonLabsDelete
            // 
            this.buttonLabsDelete.Location = new System.Drawing.Point(692, 81);
            this.buttonLabsDelete.Name = "buttonLabsDelete";
            this.buttonLabsDelete.Size = new System.Drawing.Size(111, 23);
            this.buttonLabsDelete.TabIndex = 12;
            this.buttonLabsDelete.Text = "Удалить";
            this.buttonLabsDelete.UseVisualStyleBackColor = true;
            this.buttonLabsDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonLabsUpdate
            // 
            this.buttonLabsUpdate.Location = new System.Drawing.Point(692, 52);
            this.buttonLabsUpdate.Name = "buttonLabsUpdate";
            this.buttonLabsUpdate.Size = new System.Drawing.Size(111, 23);
            this.buttonLabsUpdate.TabIndex = 11;
            this.buttonLabsUpdate.Text = "Редактировать";
            this.buttonLabsUpdate.UseVisualStyleBackColor = true;
            this.buttonLabsUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // tabPagePractices
            // 
            this.tabPagePractices.Controls.Add(this.label4);
            this.tabPagePractices.Controls.Add(this.dataGridViewPractices);
            this.tabPagePractices.Controls.Add(this.buttonPracticesAdd);
            this.tabPagePractices.Controls.Add(this.buttonPracticesDelete);
            this.tabPagePractices.Controls.Add(this.buttonPracticesUpdate);
            this.tabPagePractices.Location = new System.Drawing.Point(4, 22);
            this.tabPagePractices.Name = "tabPagePractices";
            this.tabPagePractices.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePractices.Size = new System.Drawing.Size(808, 286);
            this.tabPagePractices.TabIndex = 2;
            this.tabPagePractices.Text = "Практики";
            this.tabPagePractices.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Занятия";
            // 
            // dataGridViewPractices
            // 
            this.dataGridViewPractices.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewPractices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPractices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridViewPractices.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewPractices.Location = new System.Drawing.Point(8, 23);
            this.dataGridViewPractices.Name = "dataGridViewPractices";
            this.dataGridViewPractices.Size = new System.Drawing.Size(678, 256);
            this.dataGridViewPractices.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Тема";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 270;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Дата проведения";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Кол-во пар";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Кол-во заданий";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // buttonPracticesAdd
            // 
            this.buttonPracticesAdd.Location = new System.Drawing.Point(692, 23);
            this.buttonPracticesAdd.Name = "buttonPracticesAdd";
            this.buttonPracticesAdd.Size = new System.Drawing.Size(111, 23);
            this.buttonPracticesAdd.TabIndex = 10;
            this.buttonPracticesAdd.Text = "Добавить";
            this.buttonPracticesAdd.UseVisualStyleBackColor = true;
            this.buttonPracticesAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonPracticesDelete
            // 
            this.buttonPracticesDelete.Location = new System.Drawing.Point(692, 81);
            this.buttonPracticesDelete.Name = "buttonPracticesDelete";
            this.buttonPracticesDelete.Size = new System.Drawing.Size(111, 23);
            this.buttonPracticesDelete.TabIndex = 12;
            this.buttonPracticesDelete.Text = "Удалить";
            this.buttonPracticesDelete.UseVisualStyleBackColor = true;
            this.buttonPracticesDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonPracticesUpdate
            // 
            this.buttonPracticesUpdate.Location = new System.Drawing.Point(692, 52);
            this.buttonPracticesUpdate.Name = "buttonPracticesUpdate";
            this.buttonPracticesUpdate.Size = new System.Drawing.Size(111, 23);
            this.buttonPracticesUpdate.TabIndex = 11;
            this.buttonPracticesUpdate.Text = "Редактировать";
            this.buttonPracticesUpdate.UseVisualStyleBackColor = true;
            this.buttonPracticesUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // tabPageCourseWorks
            // 
            this.tabPageCourseWorks.Controls.Add(this.label5);
            this.tabPageCourseWorks.Controls.Add(this.dataGridViewCourseWorks);
            this.tabPageCourseWorks.Controls.Add(this.buttonCourseWorksAdd);
            this.tabPageCourseWorks.Controls.Add(this.buttonCourseWorksDelete);
            this.tabPageCourseWorks.Controls.Add(this.buttonCourseWorksUpdate);
            this.tabPageCourseWorks.Location = new System.Drawing.Point(4, 22);
            this.tabPageCourseWorks.Name = "tabPageCourseWorks";
            this.tabPageCourseWorks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCourseWorks.Size = new System.Drawing.Size(808, 286);
            this.tabPageCourseWorks.TabIndex = 3;
            this.tabPageCourseWorks.Text = "Курсовые р/п";
            this.tabPageCourseWorks.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Занятия";
            // 
            // dataGridViewCourseWorks
            // 
            this.dataGridViewCourseWorks.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewCourseWorks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCourseWorks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.dataGridViewCourseWorks.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewCourseWorks.Location = new System.Drawing.Point(8, 23);
            this.dataGridViewCourseWorks.Name = "dataGridViewCourseWorks";
            this.dataGridViewCourseWorks.Size = new System.Drawing.Size(678, 256);
            this.dataGridViewCourseWorks.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Тема";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 270;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Дата проведения";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 120;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Кол-во пар";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 120;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Кол-во заданий";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 120;
            // 
            // buttonCourseWorksAdd
            // 
            this.buttonCourseWorksAdd.Location = new System.Drawing.Point(692, 23);
            this.buttonCourseWorksAdd.Name = "buttonCourseWorksAdd";
            this.buttonCourseWorksAdd.Size = new System.Drawing.Size(111, 23);
            this.buttonCourseWorksAdd.TabIndex = 10;
            this.buttonCourseWorksAdd.Text = "Добавить";
            this.buttonCourseWorksAdd.UseVisualStyleBackColor = true;
            this.buttonCourseWorksAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCourseWorksDelete
            // 
            this.buttonCourseWorksDelete.Location = new System.Drawing.Point(692, 81);
            this.buttonCourseWorksDelete.Name = "buttonCourseWorksDelete";
            this.buttonCourseWorksDelete.Size = new System.Drawing.Size(111, 23);
            this.buttonCourseWorksDelete.TabIndex = 12;
            this.buttonCourseWorksDelete.Text = "Удалить";
            this.buttonCourseWorksDelete.UseVisualStyleBackColor = true;
            this.buttonCourseWorksDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonCourseWorksUpdate
            // 
            this.buttonCourseWorksUpdate.Location = new System.Drawing.Point(692, 52);
            this.buttonCourseWorksUpdate.Name = "buttonCourseWorksUpdate";
            this.buttonCourseWorksUpdate.Size = new System.Drawing.Size(111, 23);
            this.buttonCourseWorksUpdate.TabIndex = 11;
            this.buttonCourseWorksUpdate.Text = "Редактировать";
            this.buttonCourseWorksUpdate.UseVisualStyleBackColor = true;
            this.buttonCourseWorksUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(749, 358);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 34;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(668, 358);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 32;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 391);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comboBoxDisciplines);
            this.Controls.Add(this.label1);
            this.Name = "ProgressForm";
            this.Text = "Успеваемость";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageLectures.ResumeLayout(false);
            this.tabPageLectures.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLectures)).EndInit();
            this.tabPageLabs.ResumeLayout(false);
            this.tabPageLabs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLabs)).EndInit();
            this.tabPagePractices.ResumeLayout(false);
            this.tabPagePractices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPractices)).EndInit();
            this.tabPageCourseWorks.ResumeLayout(false);
            this.tabPageCourseWorks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCourseWorks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDisciplines;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLectures;
        private System.Windows.Forms.TabPage tabPageLabs;
        private System.Windows.Forms.TabPage tabPagePractices;
        private System.Windows.Forms.TabPage tabPageCourseWorks;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewLabs;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button buttonLabsAdd;
        private System.Windows.Forms.Button buttonLabsDelete;
        private System.Windows.Forms.Button buttonLabsUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewPractices;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button buttonPracticesAdd;
        private System.Windows.Forms.Button buttonPracticesDelete;
        private System.Windows.Forms.Button buttonPracticesUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewCourseWorks;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Button buttonCourseWorksAdd;
        private System.Windows.Forms.Button buttonCourseWorksDelete;
        private System.Windows.Forms.Button buttonCourseWorksUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewLectures;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.Button buttonLecturesAdd;
        private System.Windows.Forms.Button buttonLecturesDelete;
        private System.Windows.Forms.Button buttonLecturesUpdate;
    }
}