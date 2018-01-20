namespace DepartmentDesktop.Views.Services.DataBaseWork
{
    partial class ExportDataBaseControl
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
            this.buttonLoadJson = new System.Windows.Forms.Button();
            this.textBoxFolderPathJson = new System.Windows.Forms.TextBox();
            this.buttonFolderPathJson = new System.Windows.Forms.Button();
            this.groupBoxExportJson = new System.Windows.Forms.GroupBox();
            this.groupBoxRestoreBackUp = new System.Windows.Forms.GroupBox();
            this.buttonFileName = new System.Windows.Forms.Button();
            this.buttonRestoreBackUp = new System.Windows.Forms.Button();
            this.textBoxFileNameBackUp = new System.Windows.Forms.TextBox();
            this.groupBoxExportJson.SuspendLayout();
            this.groupBoxRestoreBackUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadJson
            // 
            this.buttonLoadJson.Location = new System.Drawing.Point(547, 30);
            this.buttonLoadJson.Name = "buttonLoadJson";
            this.buttonLoadJson.Size = new System.Drawing.Size(120, 30);
            this.buttonLoadJson.TabIndex = 2;
            this.buttonLoadJson.Text = "Загрузить";
            this.buttonLoadJson.UseVisualStyleBackColor = true;
            this.buttonLoadJson.Click += new System.EventHandler(this.ButtonLoadJson_Click);
            // 
            // textBoxFolderPathJson
            // 
            this.textBoxFolderPathJson.Location = new System.Drawing.Point(171, 36);
            this.textBoxFolderPathJson.Name = "textBoxFolderPathJson";
            this.textBoxFolderPathJson.Size = new System.Drawing.Size(370, 20);
            this.textBoxFolderPathJson.TabIndex = 1;
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
            // groupBoxExportJson
            // 
            this.groupBoxExportJson.Controls.Add(this.buttonFolderPathJson);
            this.groupBoxExportJson.Controls.Add(this.buttonLoadJson);
            this.groupBoxExportJson.Controls.Add(this.textBoxFolderPathJson);
            this.groupBoxExportJson.Location = new System.Drawing.Point(0, 0);
            this.groupBoxExportJson.Name = "groupBoxExportJson";
            this.groupBoxExportJson.Size = new System.Drawing.Size(680, 80);
            this.groupBoxExportJson.TabIndex = 3;
            this.groupBoxExportJson.TabStop = false;
            this.groupBoxExportJson.Text = "Экспорт из Json";
            // 
            // groupBoxRestoreBackUp
            // 
            this.groupBoxRestoreBackUp.Controls.Add(this.buttonFileName);
            this.groupBoxRestoreBackUp.Controls.Add(this.buttonRestoreBackUp);
            this.groupBoxRestoreBackUp.Controls.Add(this.textBoxFileNameBackUp);
            this.groupBoxRestoreBackUp.Location = new System.Drawing.Point(0, 86);
            this.groupBoxRestoreBackUp.Name = "groupBoxRestoreBackUp";
            this.groupBoxRestoreBackUp.Size = new System.Drawing.Size(680, 80);
            this.groupBoxRestoreBackUp.TabIndex = 1;
            this.groupBoxRestoreBackUp.TabStop = false;
            this.groupBoxRestoreBackUp.Text = "Восстановить из BackUp";
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
            // buttonRestoreBackUp
            // 
            this.buttonRestoreBackUp.Location = new System.Drawing.Point(547, 30);
            this.buttonRestoreBackUp.Name = "buttonRestoreBackUp";
            this.buttonRestoreBackUp.Size = new System.Drawing.Size(120, 30);
            this.buttonRestoreBackUp.TabIndex = 2;
            this.buttonRestoreBackUp.Text = "Загрузить";
            this.buttonRestoreBackUp.UseVisualStyleBackColor = true;
            this.buttonRestoreBackUp.Click += new System.EventHandler(this.ButtonRestoreBackUp_Click);
            // 
            // textBoxFileNameBackUp
            // 
            this.textBoxFileNameBackUp.Location = new System.Drawing.Point(171, 36);
            this.textBoxFileNameBackUp.Name = "textBoxFileNameBackUp";
            this.textBoxFileNameBackUp.Size = new System.Drawing.Size(370, 20);
            this.textBoxFileNameBackUp.TabIndex = 1;
            // 
            // ExportDataBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxRestoreBackUp);
            this.Controls.Add(this.groupBoxExportJson);
            this.Name = "ExportDataBaseControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.groupBoxExportJson.ResumeLayout(false);
            this.groupBoxExportJson.PerformLayout();
            this.groupBoxRestoreBackUp.ResumeLayout(false);
            this.groupBoxRestoreBackUp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadJson;
        private System.Windows.Forms.TextBox textBoxFolderPathJson;
        private System.Windows.Forms.Button buttonFolderPathJson;
        private System.Windows.Forms.GroupBox groupBoxExportJson;
        private System.Windows.Forms.GroupBox groupBoxRestoreBackUp;
        private System.Windows.Forms.Button buttonFileName;
        private System.Windows.Forms.Button buttonRestoreBackUp;
        private System.Windows.Forms.TextBox textBoxFileNameBackUp;
    }
}
