﻿namespace TicketViews.Views.ExaminationTemplateTicket
{
    partial class ExaminationTemplateTicketSearchForm
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
            this.standartSearchControl = new TicketViews.Controls.StandartSearchControl();
            this.SuspendLayout();
            // 
            // standartSearchControl
            // 
            this.standartSearchControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.standartSearchControl.Location = new System.Drawing.Point(0, 0);
            this.standartSearchControl.Name = "standartSearchControl";
            this.standartSearchControl.Size = new System.Drawing.Size(684, 461);
            this.standartSearchControl.TabIndex = 0;
            // 
            // ExaminationTemplateTicketSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.standartSearchControl);
            this.Name = "ExaminationTemplateTicketSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор билета";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.StandartSearchControl standartSearchControl;
    }
}