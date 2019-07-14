namespace AcademicYearControlsAndForms.TimeNorm
{
	partial class FormTimeNorm
	{
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
            this.labelTimeNormName = new System.Windows.Forms.Label();
            this.textBoxTimeNormName = new System.Windows.Forms.TextBox();
            this.labelSelectKindOfLoadType = new System.Windows.Forms.Label();
            this.comboBoxSelectKindOfLoadType = new System.Windows.Forms.ComboBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.textBoxHours = new System.Windows.Forms.TextBox();
            this.comboBoxAcademicYear = new System.Windows.Forms.ComboBox();
            this.labelAcademicYear = new System.Windows.Forms.Label();
            this.comboBoxTimeNormKoef = new System.Windows.Forms.ComboBox();
            this.labelTimeNormKoef = new System.Windows.Forms.Label();
            this.textBoxNumKoef = new System.Windows.Forms.TextBox();
            this.labelNumKoef = new System.Windows.Forms.Label();
            this.groupBoxMult1 = new System.Windows.Forms.GroupBox();
            this.groupBoxMult2 = new System.Windows.Forms.GroupBox();
            this.groupBoxMult3 = new System.Windows.Forms.GroupBox();
            this.textBoxTimeNormShortName = new System.Windows.Forms.TextBox();
            this.labelTimeNormShortName = new System.Windows.Forms.Label();
            this.textBoxTimeNormOrder = new System.Windows.Forms.TextBox();
            this.labelTimeNormOrder = new System.Windows.Forms.Label();
            this.textBoxKindOfLoadName = new System.Windows.Forms.TextBox();
            this.labelKindOfLoadName = new System.Windows.Forms.Label();
            this.groupBoxKindOfLoad = new System.Windows.Forms.GroupBox();
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip = new System.Windows.Forms.Label();
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip = new System.Windows.Forms.Label();
            this.labelKindOfLoadBlueAsteriskNameToolTip = new System.Windows.Forms.Label();
            this.labelKindOfLoadAttributeNameToolTip = new System.Windows.Forms.Label();
            this.textBoxKindOfLoadBlueAsteriskPracticName = new System.Windows.Forms.TextBox();
            this.labelKindOfLoadBlueAsteriskPracticName = new System.Windows.Forms.Label();
            this.textBoxKindOfLoadBlueAsteriskAttributeName = new System.Windows.Forms.TextBox();
            this.labelKindOfLoadBlueAsteriskAttributeName = new System.Windows.Forms.Label();
            this.textBoxKindOfLoadBlueAsteriskName = new System.Windows.Forms.TextBox();
            this.labelKindOfLoadBlueAsteriskName = new System.Windows.Forms.Label();
            this.textBoxKindOfLoadAttributeName = new System.Windows.Forms.TextBox();
            this.labelKindOfLoadAttributeName = new System.Windows.Forms.Label();
            this.comboBoxDisciplineBlock = new System.Windows.Forms.ComboBox();
            this.labelDisciplineBlock = new System.Windows.Forms.Label();
            this.comboBoxEducationDirectionQualification = new System.Windows.Forms.ComboBox();
            this.labelEducationDirectionQualification = new System.Windows.Forms.Label();
            this.checkBoxUseInLearningProgress = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.groupBoxMult1.SuspendLayout();
            this.groupBoxMult2.SuspendLayout();
            this.groupBoxMult3.SuspendLayout();
            this.groupBoxKindOfLoad.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.checkBoxUseInLearningProgress);
            this.panelMain.Controls.Add(this.labelAcademicYear);
            this.panelMain.Controls.Add(this.comboBoxEducationDirectionQualification);
            this.panelMain.Controls.Add(this.labelTimeNormName);
            this.panelMain.Controls.Add(this.labelEducationDirectionQualification);
            this.panelMain.Controls.Add(this.textBoxTimeNormName);
            this.panelMain.Controls.Add(this.comboBoxDisciplineBlock);
            this.panelMain.Controls.Add(this.comboBoxAcademicYear);
            this.panelMain.Controls.Add(this.labelDisciplineBlock);
            this.panelMain.Controls.Add(this.groupBoxMult1);
            this.panelMain.Controls.Add(this.groupBoxKindOfLoad);
            this.panelMain.Controls.Add(this.groupBoxMult2);
            this.panelMain.Controls.Add(this.textBoxTimeNormOrder);
            this.panelMain.Controls.Add(this.groupBoxMult3);
            this.panelMain.Controls.Add(this.labelTimeNormOrder);
            this.panelMain.Controls.Add(this.labelTimeNormShortName);
            this.panelMain.Controls.Add(this.textBoxTimeNormShortName);
            this.panelMain.Size = new System.Drawing.Size(834, 365);
            // 
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(834, 36);
            // 
            // labelTimeNormName
            // 
            this.labelTimeNormName.AutoSize = true;
            this.labelTimeNormName.Location = new System.Drawing.Point(12, 68);
            this.labelTimeNormName.Name = "labelTimeNormName";
            this.labelTimeNormName.Size = new System.Drawing.Size(64, 13);
            this.labelTimeNormName.TabIndex = 4;
            this.labelTimeNormName.Text = "Название*:";
            // 
            // textBoxTimeNormName
            // 
            this.textBoxTimeNormName.Location = new System.Drawing.Point(176, 65);
            this.textBoxTimeNormName.Name = "textBoxTimeNormName";
            this.textBoxTimeNormName.Size = new System.Drawing.Size(220, 20);
            this.textBoxTimeNormName.TabIndex = 5;
            // 
            // labelSelectKindOfLoadType
            // 
            this.labelSelectKindOfLoadType.AutoSize = true;
            this.labelSelectKindOfLoadType.Location = new System.Drawing.Point(9, 22);
            this.labelSelectKindOfLoadType.Name = "labelSelectKindOfLoadType";
            this.labelSelectKindOfLoadType.Size = new System.Drawing.Size(82, 13);
            this.labelSelectKindOfLoadType.TabIndex = 0;
            this.labelSelectKindOfLoadType.Text = "Тип нагрузки*:";
            // 
            // comboBoxSelectKindOfLoadType
            // 
            this.comboBoxSelectKindOfLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectKindOfLoadType.FormattingEnabled = true;
            this.comboBoxSelectKindOfLoadType.Location = new System.Drawing.Point(172, 19);
            this.comboBoxSelectKindOfLoadType.Name = "comboBoxSelectKindOfLoadType";
            this.comboBoxSelectKindOfLoadType.Size = new System.Drawing.Size(220, 21);
            this.comboBoxSelectKindOfLoadType.TabIndex = 1;
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(8, 18);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(38, 13);
            this.labelHours.TabIndex = 0;
            this.labelHours.Text = "Часы:";
            // 
            // textBoxHours
            // 
            this.textBoxHours.Location = new System.Drawing.Point(172, 15);
            this.textBoxHours.Name = "textBoxHours";
            this.textBoxHours.Size = new System.Drawing.Size(220, 20);
            this.textBoxHours.TabIndex = 1;
            // 
            // comboBoxAcademicYear
            // 
            this.comboBoxAcademicYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcademicYear.Enabled = false;
            this.comboBoxAcademicYear.FormattingEnabled = true;
            this.comboBoxAcademicYear.Location = new System.Drawing.Point(176, 11);
            this.comboBoxAcademicYear.Name = "comboBoxAcademicYear";
            this.comboBoxAcademicYear.Size = new System.Drawing.Size(220, 21);
            this.comboBoxAcademicYear.TabIndex = 1;
            // 
            // labelAcademicYear
            // 
            this.labelAcademicYear.AutoSize = true;
            this.labelAcademicYear.Location = new System.Drawing.Point(12, 14);
            this.labelAcademicYear.Name = "labelAcademicYear";
            this.labelAcademicYear.Size = new System.Drawing.Size(79, 13);
            this.labelAcademicYear.TabIndex = 0;
            this.labelAcademicYear.Text = "Учебный год*:";
            // 
            // comboBoxTimeNormKoef
            // 
            this.comboBoxTimeNormKoef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNormKoef.FormattingEnabled = true;
            this.comboBoxTimeNormKoef.Location = new System.Drawing.Point(172, 45);
            this.comboBoxTimeNormKoef.Name = "comboBoxTimeNormKoef";
            this.comboBoxTimeNormKoef.Size = new System.Drawing.Size(220, 21);
            this.comboBoxTimeNormKoef.TabIndex = 3;
            // 
            // labelTimeNormKoef
            // 
            this.labelTimeNormKoef.AutoSize = true;
            this.labelTimeNormKoef.Location = new System.Drawing.Point(8, 48);
            this.labelTimeNormKoef.Name = "labelTimeNormKoef";
            this.labelTimeNormKoef.Size = new System.Drawing.Size(160, 13);
            this.labelTimeNormKoef.TabIndex = 2;
            this.labelTimeNormKoef.Text = "Коэффициент норм времени*:";
            // 
            // textBoxNumKoef
            // 
            this.textBoxNumKoef.Location = new System.Drawing.Point(172, 19);
            this.textBoxNumKoef.Name = "textBoxNumKoef";
            this.textBoxNumKoef.Size = new System.Drawing.Size(220, 20);
            this.textBoxNumKoef.TabIndex = 1;
            // 
            // labelNumKoef
            // 
            this.labelNumKoef.AutoSize = true;
            this.labelNumKoef.Location = new System.Drawing.Point(8, 22);
            this.labelNumKoef.Name = "labelNumKoef";
            this.labelNumKoef.Size = new System.Drawing.Size(132, 13);
            this.labelNumKoef.TabIndex = 0;
            this.labelNumKoef.Text = "Числовой коэффициент:";
            // 
            // groupBoxMult1
            // 
            this.groupBoxMult1.Controls.Add(this.comboBoxSelectKindOfLoadType);
            this.groupBoxMult1.Controls.Add(this.labelSelectKindOfLoadType);
            this.groupBoxMult1.Location = new System.Drawing.Point(4, 178);
            this.groupBoxMult1.Name = "groupBoxMult1";
            this.groupBoxMult1.Size = new System.Drawing.Size(401, 50);
            this.groupBoxMult1.TabIndex = 10;
            this.groupBoxMult1.TabStop = false;
            this.groupBoxMult1.Text = "Множитель 1 - кол-во объектов";
            // 
            // groupBoxMult2
            // 
            this.groupBoxMult2.Controls.Add(this.textBoxHours);
            this.groupBoxMult2.Controls.Add(this.labelHours);
            this.groupBoxMult2.Location = new System.Drawing.Point(4, 234);
            this.groupBoxMult2.Name = "groupBoxMult2";
            this.groupBoxMult2.Size = new System.Drawing.Size(401, 41);
            this.groupBoxMult2.TabIndex = 11;
            this.groupBoxMult2.TabStop = false;
            this.groupBoxMult2.Text = "Множитель 2 - Кол-во часов (пусто - значит из УП)";
            // 
            // groupBoxMult3
            // 
            this.groupBoxMult3.Controls.Add(this.textBoxNumKoef);
            this.groupBoxMult3.Controls.Add(this.labelNumKoef);
            this.groupBoxMult3.Controls.Add(this.labelTimeNormKoef);
            this.groupBoxMult3.Controls.Add(this.comboBoxTimeNormKoef);
            this.groupBoxMult3.Location = new System.Drawing.Point(4, 281);
            this.groupBoxMult3.Name = "groupBoxMult3";
            this.groupBoxMult3.Size = new System.Drawing.Size(401, 78);
            this.groupBoxMult3.TabIndex = 12;
            this.groupBoxMult3.TabStop = false;
            this.groupBoxMult3.Text = "Множитель 3 - Коэф (пусто - значит нет)";
            // 
            // textBoxTimeNormShortName
            // 
            this.textBoxTimeNormShortName.Location = new System.Drawing.Point(176, 91);
            this.textBoxTimeNormShortName.MaxLength = 5;
            this.textBoxTimeNormShortName.Name = "textBoxTimeNormShortName";
            this.textBoxTimeNormShortName.Size = new System.Drawing.Size(220, 20);
            this.textBoxTimeNormShortName.TabIndex = 7;
            // 
            // labelTimeNormShortName
            // 
            this.labelTimeNormShortName.AutoSize = true;
            this.labelTimeNormShortName.Location = new System.Drawing.Point(12, 94);
            this.labelTimeNormShortName.Name = "labelTimeNormShortName";
            this.labelTimeNormShortName.Size = new System.Drawing.Size(107, 13);
            this.labelTimeNormShortName.TabIndex = 6;
            this.labelTimeNormShortName.Text = "Краткое название*:";
            // 
            // textBoxTimeNormOrder
            // 
            this.textBoxTimeNormOrder.Location = new System.Drawing.Point(176, 117);
            this.textBoxTimeNormOrder.MaxLength = 100;
            this.textBoxTimeNormOrder.Name = "textBoxTimeNormOrder";
            this.textBoxTimeNormOrder.Size = new System.Drawing.Size(220, 20);
            this.textBoxTimeNormOrder.TabIndex = 9;
            // 
            // labelTimeNormOrder
            // 
            this.labelTimeNormOrder.AutoSize = true;
            this.labelTimeNormOrder.Location = new System.Drawing.Point(12, 120);
            this.labelTimeNormOrder.Name = "labelTimeNormOrder";
            this.labelTimeNormOrder.Size = new System.Drawing.Size(58, 13);
            this.labelTimeNormOrder.TabIndex = 8;
            this.labelTimeNormOrder.Text = "Порядок*:";
            // 
            // textBoxKindOfLoadName
            // 
            this.textBoxKindOfLoadName.Location = new System.Drawing.Point(185, 20);
            this.textBoxKindOfLoadName.MaxLength = 100;
            this.textBoxKindOfLoadName.Name = "textBoxKindOfLoadName";
            this.textBoxKindOfLoadName.Size = new System.Drawing.Size(220, 20);
            this.textBoxKindOfLoadName.TabIndex = 1;
            // 
            // labelKindOfLoadName
            // 
            this.labelKindOfLoadName.AutoSize = true;
            this.labelKindOfLoadName.Location = new System.Drawing.Point(8, 23);
            this.labelKindOfLoadName.Name = "labelKindOfLoadName";
            this.labelKindOfLoadName.Size = new System.Drawing.Size(140, 13);
            this.labelKindOfLoadName.TabIndex = 0;
            this.labelKindOfLoadName.Text = "Название вида нагрузки*:";
            // 
            // groupBoxKindOfLoad
            // 
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadBlueAsteriskPracticNameToolTip);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadBlueAsteriskAttributeNameToolTip);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadBlueAsteriskNameToolTip);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadAttributeNameToolTip);
            this.groupBoxKindOfLoad.Controls.Add(this.textBoxKindOfLoadBlueAsteriskPracticName);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadBlueAsteriskPracticName);
            this.groupBoxKindOfLoad.Controls.Add(this.textBoxKindOfLoadBlueAsteriskAttributeName);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadBlueAsteriskAttributeName);
            this.groupBoxKindOfLoad.Controls.Add(this.textBoxKindOfLoadBlueAsteriskName);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadBlueAsteriskName);
            this.groupBoxKindOfLoad.Controls.Add(this.textBoxKindOfLoadAttributeName);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadAttributeName);
            this.groupBoxKindOfLoad.Controls.Add(this.textBoxKindOfLoadName);
            this.groupBoxKindOfLoad.Controls.Add(this.labelKindOfLoadName);
            this.groupBoxKindOfLoad.Location = new System.Drawing.Point(411, 11);
            this.groupBoxKindOfLoad.Name = "groupBoxKindOfLoad";
            this.groupBoxKindOfLoad.Size = new System.Drawing.Size(419, 274);
            this.groupBoxKindOfLoad.TabIndex = 13;
            this.groupBoxKindOfLoad.TabStop = false;
            this.groupBoxKindOfLoad.Text = "Тип нагрузки";
            // 
            // labelKindOfLoadBlueAsteriskPracticNameToolTip
            // 
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.AutoSize = true;
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.Location = new System.Drawing.Point(104, 231);
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.Name = "labelKindOfLoadBlueAsteriskPracticNameToolTip";
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.Size = new System.Drawing.Size(301, 26);
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.TabIndex = 13;
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.Text = "Название практики для извлечения из вида практик \r\n(указывать только для нагрузок" +
    ", связанных с практикой)";
            this.labelKindOfLoadBlueAsteriskPracticNameToolTip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKindOfLoadBlueAsteriskAttributeNameToolTip
            // 
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.AutoSize = true;
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.Location = new System.Drawing.Point(153, 167);
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.Name = "labelKindOfLoadBlueAsteriskAttributeNameToolTip";
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.Size = new System.Drawing.Size(252, 26);
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.TabIndex = 12;
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.Text = "Название атрибута по которму извлекать часы \r\nдля расчетов из строк Плана";
            this.labelKindOfLoadBlueAsteriskAttributeNameToolTip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKindOfLoadBlueAsteriskNameToolTip
            // 
            this.labelKindOfLoadBlueAsteriskNameToolTip.AutoSize = true;
            this.labelKindOfLoadBlueAsteriskNameToolTip.Location = new System.Drawing.Point(37, 117);
            this.labelKindOfLoadBlueAsteriskNameToolTip.Name = "labelKindOfLoadBlueAsteriskNameToolTip";
            this.labelKindOfLoadBlueAsteriskNameToolTip.Size = new System.Drawing.Size(368, 13);
            this.labelKindOfLoadBlueAsteriskNameToolTip.TabIndex = 11;
            this.labelKindOfLoadBlueAsteriskNameToolTip.Text = "Название нагрузки в справочнике видов работ в новой версии планов";
            // 
            // labelKindOfLoadAttributeNameToolTip
            // 
            this.labelKindOfLoadAttributeNameToolTip.AutoSize = true;
            this.labelKindOfLoadAttributeNameToolTip.Location = new System.Drawing.Point(124, 69);
            this.labelKindOfLoadAttributeNameToolTip.Name = "labelKindOfLoadAttributeNameToolTip";
            this.labelKindOfLoadAttributeNameToolTip.Size = new System.Drawing.Size(281, 13);
            this.labelKindOfLoadAttributeNameToolTip.TabIndex = 10;
            this.labelKindOfLoadAttributeNameToolTip.Text = "Атрибут для поиска нагрузки в старой версии планов";
            // 
            // textBoxKindOfLoadBlueAsteriskPracticName
            // 
            this.textBoxKindOfLoadBlueAsteriskPracticName.Location = new System.Drawing.Point(185, 208);
            this.textBoxKindOfLoadBlueAsteriskPracticName.MaxLength = 100;
            this.textBoxKindOfLoadBlueAsteriskPracticName.Name = "textBoxKindOfLoadBlueAsteriskPracticName";
            this.textBoxKindOfLoadBlueAsteriskPracticName.Size = new System.Drawing.Size(220, 20);
            this.textBoxKindOfLoadBlueAsteriskPracticName.TabIndex = 9;
            // 
            // labelKindOfLoadBlueAsteriskPracticName
            // 
            this.labelKindOfLoadBlueAsteriskPracticName.AutoSize = true;
            this.labelKindOfLoadBlueAsteriskPracticName.Location = new System.Drawing.Point(8, 211);
            this.labelKindOfLoadBlueAsteriskPracticName.Name = "labelKindOfLoadBlueAsteriskPracticName";
            this.labelKindOfLoadBlueAsteriskPracticName.Size = new System.Drawing.Size(126, 13);
            this.labelKindOfLoadBlueAsteriskPracticName.TabIndex = 8;
            this.labelKindOfLoadBlueAsteriskPracticName.Text = "Синоним для практики:";
            // 
            // textBoxKindOfLoadBlueAsteriskAttributeName
            // 
            this.textBoxKindOfLoadBlueAsteriskAttributeName.Location = new System.Drawing.Point(185, 144);
            this.textBoxKindOfLoadBlueAsteriskAttributeName.MaxLength = 100;
            this.textBoxKindOfLoadBlueAsteriskAttributeName.Name = "textBoxKindOfLoadBlueAsteriskAttributeName";
            this.textBoxKindOfLoadBlueAsteriskAttributeName.Size = new System.Drawing.Size(220, 20);
            this.textBoxKindOfLoadBlueAsteriskAttributeName.TabIndex = 7;
            // 
            // labelKindOfLoadBlueAsteriskAttributeName
            // 
            this.labelKindOfLoadBlueAsteriskAttributeName.AutoSize = true;
            this.labelKindOfLoadBlueAsteriskAttributeName.Location = new System.Drawing.Point(8, 147);
            this.labelKindOfLoadBlueAsteriskAttributeName.Name = "labelKindOfLoadBlueAsteriskAttributeName";
            this.labelKindOfLoadBlueAsteriskAttributeName.Size = new System.Drawing.Size(158, 13);
            this.labelKindOfLoadBlueAsteriskAttributeName.TabIndex = 6;
            this.labelKindOfLoadBlueAsteriskAttributeName.Text = "Атрибут для получения часов:";
            // 
            // textBoxKindOfLoadBlueAsteriskName
            // 
            this.textBoxKindOfLoadBlueAsteriskName.Location = new System.Drawing.Point(185, 94);
            this.textBoxKindOfLoadBlueAsteriskName.MaxLength = 100;
            this.textBoxKindOfLoadBlueAsteriskName.Name = "textBoxKindOfLoadBlueAsteriskName";
            this.textBoxKindOfLoadBlueAsteriskName.Size = new System.Drawing.Size(220, 20);
            this.textBoxKindOfLoadBlueAsteriskName.TabIndex = 5;
            // 
            // labelKindOfLoadBlueAsteriskName
            // 
            this.labelKindOfLoadBlueAsteriskName.AutoSize = true;
            this.labelKindOfLoadBlueAsteriskName.Location = new System.Drawing.Point(8, 97);
            this.labelKindOfLoadBlueAsteriskName.Name = "labelKindOfLoadBlueAsteriskName";
            this.labelKindOfLoadBlueAsteriskName.Size = new System.Drawing.Size(171, 13);
            this.labelKindOfLoadBlueAsteriskName.TabIndex = 4;
            this.labelKindOfLoadBlueAsteriskName.Text = "Синоноим для синей звездочки:";
            // 
            // textBoxKindOfLoadAttributeName
            // 
            this.textBoxKindOfLoadAttributeName.Location = new System.Drawing.Point(185, 46);
            this.textBoxKindOfLoadAttributeName.MaxLength = 100;
            this.textBoxKindOfLoadAttributeName.Name = "textBoxKindOfLoadAttributeName";
            this.textBoxKindOfLoadAttributeName.Size = new System.Drawing.Size(220, 20);
            this.textBoxKindOfLoadAttributeName.TabIndex = 3;
            // 
            // labelKindOfLoadAttributeName
            // 
            this.labelKindOfLoadAttributeName.AutoSize = true;
            this.labelKindOfLoadAttributeName.Location = new System.Drawing.Point(8, 48);
            this.labelKindOfLoadAttributeName.Name = "labelKindOfLoadAttributeName";
            this.labelKindOfLoadAttributeName.Size = new System.Drawing.Size(80, 13);
            this.labelKindOfLoadAttributeName.TabIndex = 2;
            this.labelKindOfLoadAttributeName.Text = "Имя атрибута:";
            // 
            // comboBoxDisciplineBlock
            // 
            this.comboBoxDisciplineBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDisciplineBlock.FormattingEnabled = true;
            this.comboBoxDisciplineBlock.Location = new System.Drawing.Point(176, 38);
            this.comboBoxDisciplineBlock.Name = "comboBoxDisciplineBlock";
            this.comboBoxDisciplineBlock.Size = new System.Drawing.Size(220, 21);
            this.comboBoxDisciplineBlock.TabIndex = 3;
            // 
            // labelDisciplineBlock
            // 
            this.labelDisciplineBlock.AutoSize = true;
            this.labelDisciplineBlock.Location = new System.Drawing.Point(12, 41);
            this.labelDisciplineBlock.Name = "labelDisciplineBlock";
            this.labelDisciplineBlock.Size = new System.Drawing.Size(96, 13);
            this.labelDisciplineBlock.TabIndex = 2;
            this.labelDisciplineBlock.Text = "Блок дисциплин*:";
            // 
            // comboBoxEducationDirectionQualification
            // 
            this.comboBoxEducationDirectionQualification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEducationDirectionQualification.FormattingEnabled = true;
            this.comboBoxEducationDirectionQualification.Location = new System.Drawing.Point(176, 143);
            this.comboBoxEducationDirectionQualification.Name = "comboBoxEducationDirectionQualification";
            this.comboBoxEducationDirectionQualification.Size = new System.Drawing.Size(220, 21);
            this.comboBoxEducationDirectionQualification.TabIndex = 18;
            // 
            // labelEducationDirectionQualification
            // 
            this.labelEducationDirectionQualification.AutoSize = true;
            this.labelEducationDirectionQualification.Location = new System.Drawing.Point(12, 146);
            this.labelEducationDirectionQualification.Name = "labelEducationDirectionQualification";
            this.labelEducationDirectionQualification.Size = new System.Drawing.Size(103, 13);
            this.labelEducationDirectionQualification.TabIndex = 17;
            this.labelEducationDirectionQualification.Text = "Уровень обучения:";
            // 
            // checkBoxUseInLearningProgress
            // 
            this.checkBoxUseInLearningProgress.AutoSize = true;
            this.checkBoxUseInLearningProgress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUseInLearningProgress.Location = new System.Drawing.Point(422, 299);
            this.checkBoxUseInLearningProgress.Name = "checkBoxUseInLearningProgress";
            this.checkBoxUseInLearningProgress.Size = new System.Drawing.Size(218, 17);
            this.checkBoxUseInLearningProgress.TabIndex = 14;
            this.checkBoxUseInLearningProgress.Text = "Выводить при настройки дисциплины";
            this.checkBoxUseInLearningProgress.UseVisualStyleBackColor = true;
            // 
            // FormTimeNorm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 401);
            this.Name = "FormTimeNorm";
            this.Text = "Норма времени";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.groupBoxMult1.ResumeLayout(false);
            this.groupBoxMult1.PerformLayout();
            this.groupBoxMult2.ResumeLayout(false);
            this.groupBoxMult2.PerformLayout();
            this.groupBoxMult3.ResumeLayout(false);
            this.groupBoxMult3.PerformLayout();
            this.groupBoxKindOfLoad.ResumeLayout(false);
            this.groupBoxKindOfLoad.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelTimeNormName;
		private System.Windows.Forms.TextBox textBoxTimeNormName;
		private System.Windows.Forms.Label labelSelectKindOfLoadType;
		private System.Windows.Forms.ComboBox comboBoxSelectKindOfLoadType;
		private System.Windows.Forms.Label labelHours;
		private System.Windows.Forms.TextBox textBoxHours;
        private System.Windows.Forms.ComboBox comboBoxAcademicYear;
        private System.Windows.Forms.Label labelAcademicYear;
        private System.Windows.Forms.ComboBox comboBoxTimeNormKoef;
        private System.Windows.Forms.Label labelTimeNormKoef;
        private System.Windows.Forms.TextBox textBoxNumKoef;
        private System.Windows.Forms.Label labelNumKoef;
        private System.Windows.Forms.GroupBox groupBoxMult1;
        private System.Windows.Forms.GroupBox groupBoxMult2;
        private System.Windows.Forms.GroupBox groupBoxMult3;
        private System.Windows.Forms.TextBox textBoxTimeNormShortName;
        private System.Windows.Forms.Label labelTimeNormShortName;
        private System.Windows.Forms.TextBox textBoxTimeNormOrder;
        private System.Windows.Forms.Label labelTimeNormOrder;
        private System.Windows.Forms.TextBox textBoxKindOfLoadName;
        private System.Windows.Forms.Label labelKindOfLoadName;
        private System.Windows.Forms.GroupBox groupBoxKindOfLoad;
        private System.Windows.Forms.TextBox textBoxKindOfLoadBlueAsteriskPracticName;
        private System.Windows.Forms.Label labelKindOfLoadBlueAsteriskPracticName;
        private System.Windows.Forms.TextBox textBoxKindOfLoadBlueAsteriskAttributeName;
        private System.Windows.Forms.Label labelKindOfLoadBlueAsteriskAttributeName;
        private System.Windows.Forms.TextBox textBoxKindOfLoadBlueAsteriskName;
        private System.Windows.Forms.Label labelKindOfLoadBlueAsteriskName;
        private System.Windows.Forms.TextBox textBoxKindOfLoadAttributeName;
        private System.Windows.Forms.Label labelKindOfLoadAttributeName;
        private System.Windows.Forms.Label labelKindOfLoadAttributeNameToolTip;
        private System.Windows.Forms.Label labelKindOfLoadBlueAsteriskNameToolTip;
        private System.Windows.Forms.Label labelKindOfLoadBlueAsteriskAttributeNameToolTip;
        private System.Windows.Forms.Label labelKindOfLoadBlueAsteriskPracticNameToolTip;
        private System.Windows.Forms.ComboBox comboBoxDisciplineBlock;
        private System.Windows.Forms.Label labelDisciplineBlock;
        private System.Windows.Forms.ComboBox comboBoxEducationDirectionQualification;
        private System.Windows.Forms.Label labelEducationDirectionQualification;
        private System.Windows.Forms.CheckBox checkBoxUseInLearningProgress;
    }
}