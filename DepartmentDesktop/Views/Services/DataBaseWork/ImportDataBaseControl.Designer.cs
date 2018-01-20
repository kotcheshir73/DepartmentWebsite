namespace DepartmentDesktop.Views.Services.DataBaseWork
{
    partial class ImportDataBaseControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFolderPathJson = new System.Windows.Forms.Button();
            this.textBoxFolderPathJson = new System.Windows.Forms.TextBox();
            this.buttonSaveJson = new System.Windows.Forms.Button();
            this.groupBoxImportJson = new System.Windows.Forms.GroupBox();
            this.groupBoxCreateBackUp = new System.Windows.Forms.GroupBox();
            this.buttonFileName = new System.Windows.Forms.Button();
            this.buttonSaveBackUp = new System.Windows.Forms.Button();
            this.textBoxFileNameBackUp = new System.Windows.Forms.TextBox();
            this.groupBoxImportJson.SuspendLayout();
            this.groupBoxCreateBackUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonFolderPathJson
            // 
            this.buttonFolderPathJson.Location = new System.Drawing.Point(15, 30);
            this.buttonFolderPathJson.Name = "buttonFolderPathJson";
            this.buttonFolderPathJson.Size = new System.Drawing.Size(150, 30);
            this.buttonFolderPathJson.TabIndex = 0;
            this.buttonFolderPathJson.Text = "Путь до папки";
            this.buttonFolderPathJson.UseVisualStyleBackColor = true;
            this.buttonFolderPathJson.Click += new System.EventHandler(this.ButtonFolderPathJson_Click);
            // 
            // textBoxFolderPathJson
            // 
            this.textBoxFolderPathJson.Location = new System.Drawing.Point(171, 36);
            this.textBoxFolderPathJson.Name = "textBoxFolderPathJson";
            this.textBoxFolderPathJson.Size = new System.Drawing.Size(370, 20);
            this.textBoxFolderPathJson.TabIndex = 1;
            // 
            // buttonSaveJson
            // 
            this.buttonSaveJson.Location = new System.Drawing.Point(547, 30);
            this.buttonSaveJson.Name = "buttonSaveJson";
            this.buttonSaveJson.Size = new System.Drawing.Size(120, 30);
            this.buttonSaveJson.TabIndex = 2;
            this.buttonSaveJson.Text = "Сохранить";
            this.buttonSaveJson.UseVisualStyleBackColor = true;
            this.buttonSaveJson.Click += new System.EventHandler(this.ButtonSaveJson_Click);
            // 
            // groupBoxImportJson
            // 
            this.groupBoxImportJson.Controls.Add(this.buttonFolderPathJson);
            this.groupBoxImportJson.Controls.Add(this.buttonSaveJson);
            this.groupBoxImportJson.Controls.Add(this.textBoxFolderPathJson);
            this.groupBoxImportJson.Location = new System.Drawing.Point(0, 0);
            this.groupBoxImportJson.Name = "groupBoxImportJson";
            this.groupBoxImportJson.Size = new System.Drawing.Size(680, 80);
            this.groupBoxImportJson.TabIndex = 0;
            this.groupBoxImportJson.TabStop = false;
            this.groupBoxImportJson.Text = "Импорт в JSON";
            // 
            // groupBoxCreateBackUp
            // 
            this.groupBoxCreateBackUp.Controls.Add(this.buttonFileName);
            this.groupBoxCreateBackUp.Controls.Add(this.buttonSaveBackUp);
            this.groupBoxCreateBackUp.Controls.Add(this.textBoxFileNameBackUp);
            this.groupBoxCreateBackUp.Location = new System.Drawing.Point(0, 86);
            this.groupBoxCreateBackUp.Name = "groupBoxCreateBackUp";
            this.groupBoxCreateBackUp.Size = new System.Drawing.Size(680, 80);
            this.groupBoxCreateBackUp.TabIndex = 1;
            this.groupBoxCreateBackUp.TabStop = false;
            this.groupBoxCreateBackUp.Text = "Создать BackUp";
            // 
            // buttonFileName
            // 
            this.buttonFileName.Location = new System.Drawing.Point(15, 30);
            this.buttonFileName.Name = "buttonFileName";
            this.buttonFileName.Size = new System.Drawing.Size(150, 30);
            this.buttonFileName.TabIndex = 0;
            this.buttonFileName.Text = "Путь до файла";
            this.buttonFileName.UseVisualStyleBackColor = true;
            this.buttonFileName.Click += new System.EventHandler(this.ButtonFileName_Click);
            // 
            // buttonSaveBackUp
            // 
            this.buttonSaveBackUp.Location = new System.Drawing.Point(547, 30);
            this.buttonSaveBackUp.Name = "buttonSaveBackUp";
            this.buttonSaveBackUp.Size = new System.Drawing.Size(120, 30);
            this.buttonSaveBackUp.TabIndex = 2;
            this.buttonSaveBackUp.Text = "Сохранить";
            this.buttonSaveBackUp.UseVisualStyleBackColor = true;
            this.buttonSaveBackUp.Click += new System.EventHandler(this.ButtonSaveBackUp_Click);
            // 
            // textBoxFileNameBackUp
            // 
            this.textBoxFileNameBackUp.Location = new System.Drawing.Point(171, 36);
            this.textBoxFileNameBackUp.Name = "textBoxFileNameBackUp";
            this.textBoxFileNameBackUp.Size = new System.Drawing.Size(370, 20);
            this.textBoxFileNameBackUp.TabIndex = 1;
            // 
            // ImportDataBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCreateBackUp);
            this.Controls.Add(this.groupBoxImportJson);
            this.Name = "ImportDataBaseControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.groupBoxImportJson.ResumeLayout(false);
            this.groupBoxImportJson.PerformLayout();
            this.groupBoxCreateBackUp.ResumeLayout(false);
            this.groupBoxCreateBackUp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonFolderPathJson;
        private System.Windows.Forms.TextBox textBoxFolderPathJson;
        private System.Windows.Forms.Button buttonSaveJson;
        private System.Windows.Forms.GroupBox groupBoxImportJson;
        private System.Windows.Forms.GroupBox groupBoxCreateBackUp;
        private System.Windows.Forms.Button buttonFileName;
        private System.Windows.Forms.Button buttonSaveBackUp;
        private System.Windows.Forms.TextBox textBoxFileNameBackUp;
    }
}
