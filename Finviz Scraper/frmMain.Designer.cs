namespace Finviz_Scraper
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnScanFiltersOptions = new System.Windows.Forms.Button();
            this.lblTotalFilters = new System.Windows.Forms.Label();
            this.lblTotalOptions = new System.Windows.Forms.Label();
            this.chkExcludePrice = new System.Windows.Forms.CheckBox();
            this.grpExclude = new System.Windows.Forms.GroupBox();
            this.chkExcludeAverageVolume = new System.Windows.Forms.CheckBox();
            this.chkExcludeIndustry = new System.Windows.Forms.CheckBox();
            this.chkExcludeSector = new System.Windows.Forms.CheckBox();
            this.lblPageUpdate = new System.Windows.Forms.Label();
            this.prgPageUpdate = new System.Windows.Forms.ProgressBar();
            this.btnPerformScan = new System.Windows.Forms.Button();
            this.grpBaseFilterSettings = new System.Windows.Forms.GroupBox();
            this.chkScanFloatUnder50m = new System.Windows.Forms.CheckBox();
            this.chkScanExcludeFunds = new System.Windows.Forms.CheckBox();
            this.chkScanVolOver400k = new System.Windows.Forms.CheckBox();
            this.chkScanPriceUnder20 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.grpScanType = new System.Windows.Forms.GroupBox();
            this.chkTestMode = new System.Windows.Forms.CheckBox();
            this.rdoScanAllInfo = new System.Windows.Forms.RadioButton();
            this.rdoScanBaseInfo = new System.Windows.Forms.RadioButton();
            this.rdoScanAllFilters = new System.Windows.Forms.RadioButton();
            this.lblFilterUpdate = new System.Windows.Forms.Label();
            this.prgFilterUpdate = new System.Windows.Forms.ProgressBar();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.txtScanElapsedTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadFilterScanResults = new System.Windows.Forms.Button();
            this.lblTotalFilterResults = new System.Windows.Forms.Label();
            this.lblLastRecordDate = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNextScan = new System.Windows.Forms.TextBox();
            this.chkAutoScan = new System.Windows.Forms.CheckBox();
            this.lblNextScan = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpExclude.SuspendLayout();
            this.grpBaseFilterSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpScanType.SuspendLayout();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScanFiltersOptions
            // 
            this.btnScanFiltersOptions.Location = new System.Drawing.Point(15, 22);
            this.btnScanFiltersOptions.Name = "btnScanFiltersOptions";
            this.btnScanFiltersOptions.Size = new System.Drawing.Size(128, 23);
            this.btnScanFiltersOptions.TabIndex = 0;
            this.btnScanFiltersOptions.Text = "Update Finviz Filters";
            this.btnScanFiltersOptions.UseVisualStyleBackColor = true;
            this.btnScanFiltersOptions.Click += new System.EventHandler(this.BtnScanFiltersOptions_Click);
            // 
            // lblTotalFilters
            // 
            this.lblTotalFilters.Location = new System.Drawing.Point(149, 27);
            this.lblTotalFilters.Name = "lblTotalFilters";
            this.lblTotalFilters.Size = new System.Drawing.Size(108, 13);
            this.lblTotalFilters.TabIndex = 2;
            this.lblTotalFilters.Text = "Total Filters :";
            // 
            // lblTotalOptions
            // 
            this.lblTotalOptions.Location = new System.Drawing.Point(261, 27);
            this.lblTotalOptions.Name = "lblTotalOptions";
            this.lblTotalOptions.Size = new System.Drawing.Size(108, 13);
            this.lblTotalOptions.TabIndex = 3;
            this.lblTotalOptions.Text = "Total Options :";
            // 
            // chkExcludePrice
            // 
            this.chkExcludePrice.AutoSize = true;
            this.chkExcludePrice.Checked = true;
            this.chkExcludePrice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludePrice.Enabled = false;
            this.chkExcludePrice.Location = new System.Drawing.Point(10, 17);
            this.chkExcludePrice.Name = "chkExcludePrice";
            this.chkExcludePrice.Size = new System.Drawing.Size(50, 17);
            this.chkExcludePrice.TabIndex = 4;
            this.chkExcludePrice.Text = "Price";
            this.chkExcludePrice.UseVisualStyleBackColor = true;
            // 
            // grpExclude
            // 
            this.grpExclude.Controls.Add(this.chkExcludeAverageVolume);
            this.grpExclude.Controls.Add(this.chkExcludeIndustry);
            this.grpExclude.Controls.Add(this.chkExcludeSector);
            this.grpExclude.Controls.Add(this.chkExcludePrice);
            this.grpExclude.Location = new System.Drawing.Point(189, 11);
            this.grpExclude.Name = "grpExclude";
            this.grpExclude.Size = new System.Drawing.Size(182, 54);
            this.grpExclude.TabIndex = 5;
            this.grpExclude.TabStop = false;
            this.grpExclude.Text = "Exclude Filters";
            this.grpExclude.Visible = false;
            // 
            // chkExcludeAverageVolume
            // 
            this.chkExcludeAverageVolume.AutoSize = true;
            this.chkExcludeAverageVolume.Checked = true;
            this.chkExcludeAverageVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludeAverageVolume.Location = new System.Drawing.Point(73, 33);
            this.chkExcludeAverageVolume.Name = "chkExcludeAverageVolume";
            this.chkExcludeAverageVolume.Size = new System.Drawing.Size(104, 17);
            this.chkExcludeAverageVolume.TabIndex = 7;
            this.chkExcludeAverageVolume.Text = "Average Volume";
            this.chkExcludeAverageVolume.UseVisualStyleBackColor = true;
            // 
            // chkExcludeIndustry
            // 
            this.chkExcludeIndustry.AutoSize = true;
            this.chkExcludeIndustry.Checked = true;
            this.chkExcludeIndustry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludeIndustry.Location = new System.Drawing.Point(73, 17);
            this.chkExcludeIndustry.Name = "chkExcludeIndustry";
            this.chkExcludeIndustry.Size = new System.Drawing.Size(63, 17);
            this.chkExcludeIndustry.TabIndex = 6;
            this.chkExcludeIndustry.Text = "Industry";
            this.chkExcludeIndustry.UseVisualStyleBackColor = true;
            // 
            // chkExcludeSector
            // 
            this.chkExcludeSector.AutoSize = true;
            this.chkExcludeSector.Checked = true;
            this.chkExcludeSector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludeSector.Location = new System.Drawing.Point(10, 33);
            this.chkExcludeSector.Name = "chkExcludeSector";
            this.chkExcludeSector.Size = new System.Drawing.Size(57, 17);
            this.chkExcludeSector.TabIndex = 5;
            this.chkExcludeSector.Text = "Sector";
            this.chkExcludeSector.UseVisualStyleBackColor = true;
            // 
            // lblPageUpdate
            // 
            this.lblPageUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageUpdate.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPageUpdate.Location = new System.Drawing.Point(9, 19);
            this.lblPageUpdate.Name = "lblPageUpdate";
            this.lblPageUpdate.Size = new System.Drawing.Size(163, 18);
            this.lblPageUpdate.TabIndex = 10;
            this.lblPageUpdate.Text = "Page Scan Progress";
            // 
            // prgPageUpdate
            // 
            this.prgPageUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgPageUpdate.Location = new System.Drawing.Point(12, 35);
            this.prgPageUpdate.Name = "prgPageUpdate";
            this.prgPageUpdate.Size = new System.Drawing.Size(160, 18);
            this.prgPageUpdate.Step = 1;
            this.prgPageUpdate.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgPageUpdate.TabIndex = 11;
            // 
            // btnPerformScan
            // 
            this.btnPerformScan.Location = new System.Drawing.Point(197, 19);
            this.btnPerformScan.Name = "btnPerformScan";
            this.btnPerformScan.Size = new System.Drawing.Size(139, 43);
            this.btnPerformScan.TabIndex = 12;
            this.btnPerformScan.Text = "Perform Scan";
            this.btnPerformScan.UseVisualStyleBackColor = true;
            this.btnPerformScan.Click += new System.EventHandler(this.BtnPerformScan_Click);
            // 
            // grpBaseFilterSettings
            // 
            this.grpBaseFilterSettings.Controls.Add(this.chkScanFloatUnder50m);
            this.grpBaseFilterSettings.Controls.Add(this.chkScanExcludeFunds);
            this.grpBaseFilterSettings.Controls.Add(this.chkScanVolOver400k);
            this.grpBaseFilterSettings.Controls.Add(this.chkScanPriceUnder20);
            this.grpBaseFilterSettings.Location = new System.Drawing.Point(12, 323);
            this.grpBaseFilterSettings.Name = "grpBaseFilterSettings";
            this.grpBaseFilterSettings.Size = new System.Drawing.Size(350, 78);
            this.grpBaseFilterSettings.TabIndex = 8;
            this.grpBaseFilterSettings.TabStop = false;
            this.grpBaseFilterSettings.Text = "Scan For Only Stocks With... ( locks && excludes filter in scan )";
            // 
            // chkScanFloatUnder50m
            // 
            this.chkScanFloatUnder50m.AutoSize = true;
            this.chkScanFloatUnder50m.Location = new System.Drawing.Point(192, 24);
            this.chkScanFloatUnder50m.Name = "chkScanFloatUnder50m";
            this.chkScanFloatUnder50m.Size = new System.Drawing.Size(140, 17);
            this.chkScanFloatUnder50m.TabIndex = 7;
            this.chkScanFloatUnder50m.Text = "Float Under 50M ( float )";
            this.chkScanFloatUnder50m.UseVisualStyleBackColor = true;
            // 
            // chkScanExcludeFunds
            // 
            this.chkScanExcludeFunds.AutoSize = true;
            this.chkScanExcludeFunds.Checked = true;
            this.chkScanExcludeFunds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScanExcludeFunds.Location = new System.Drawing.Point(192, 49);
            this.chkScanExcludeFunds.Name = "chkScanExcludeFunds";
            this.chkScanExcludeFunds.Size = new System.Drawing.Size(147, 17);
            this.chkScanExcludeFunds.TabIndex = 6;
            this.chkScanExcludeFunds.Text = "Exclude Funds ( industry )";
            this.chkScanExcludeFunds.UseVisualStyleBackColor = true;
            // 
            // chkScanVolOver400k
            // 
            this.chkScanVolOver400k.AutoSize = true;
            this.chkScanVolOver400k.Checked = true;
            this.chkScanVolOver400k.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScanVolOver400k.Location = new System.Drawing.Point(16, 49);
            this.chkScanVolOver400k.Name = "chkScanVolOver400k";
            this.chkScanVolOver400k.Size = new System.Drawing.Size(166, 17);
            this.chkScanVolOver400k.TabIndex = 5;
            this.chkScanVolOver400k.Text = "Avg Vol Over 400k ( avg vol )";
            this.chkScanVolOver400k.UseVisualStyleBackColor = true;
            // 
            // chkScanPriceUnder20
            // 
            this.chkScanPriceUnder20.AutoSize = true;
            this.chkScanPriceUnder20.Checked = true;
            this.chkScanPriceUnder20.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScanPriceUnder20.Location = new System.Drawing.Point(16, 24);
            this.chkScanPriceUnder20.Name = "chkScanPriceUnder20";
            this.chkScanPriceUnder20.Size = new System.Drawing.Size(141, 17);
            this.chkScanPriceUnder20.TabIndex = 4;
            this.chkScanPriceUnder20.Text = "Price Under $20 ( price )";
            this.chkScanPriceUnder20.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnScanFiltersOptions);
            this.groupBox1.Controls.Add(this.lblTotalFilters);
            this.groupBox1.Controls.Add(this.lblTotalOptions);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(376, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 219);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters && Options";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(15, 76);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(356, 128);
            this.treeView1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Filters && Options in Database";
            // 
            // grpScanType
            // 
            this.grpScanType.Controls.Add(this.chkTestMode);
            this.grpScanType.Controls.Add(this.rdoScanAllInfo);
            this.grpScanType.Controls.Add(this.rdoScanBaseInfo);
            this.grpScanType.Controls.Add(this.rdoScanAllFilters);
            this.grpScanType.Controls.Add(this.btnPerformScan);
            this.grpScanType.Location = new System.Drawing.Point(12, 408);
            this.grpScanType.Name = "grpScanType";
            this.grpScanType.Size = new System.Drawing.Size(350, 96);
            this.grpScanType.TabIndex = 14;
            this.grpScanType.TabStop = false;
            this.grpScanType.Text = "Scan Type";
            // 
            // chkTestMode
            // 
            this.chkTestMode.AutoSize = true;
            this.chkTestMode.Location = new System.Drawing.Point(198, 68);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(142, 17);
            this.chkTestMode.TabIndex = 21;
            this.chkTestMode.Text = "Test Mode ( few results )";
            this.chkTestMode.UseVisualStyleBackColor = true;
            // 
            // rdoScanAllInfo
            // 
            this.rdoScanAllInfo.AutoSize = true;
            this.rdoScanAllInfo.Enabled = false;
            this.rdoScanAllInfo.Location = new System.Drawing.Point(20, 67);
            this.rdoScanAllInfo.Name = "rdoScanAllInfo";
            this.rdoScanAllInfo.Size = new System.Drawing.Size(170, 17);
            this.rdoScanAllInfo.TabIndex = 15;
            this.rdoScanAllInfo.Text = "All Stocks -> All Info ( 30 mins )";
            this.rdoScanAllInfo.UseVisualStyleBackColor = true;
            // 
            // rdoScanBaseInfo
            // 
            this.rdoScanBaseInfo.AutoSize = true;
            this.rdoScanBaseInfo.Checked = true;
            this.rdoScanBaseInfo.Location = new System.Drawing.Point(20, 45);
            this.rdoScanBaseInfo.Name = "rdoScanBaseInfo";
            this.rdoScanBaseInfo.Size = new System.Drawing.Size(172, 17);
            this.rdoScanBaseInfo.TabIndex = 14;
            this.rdoScanBaseInfo.TabStop = true;
            this.rdoScanBaseInfo.Text = "All Stocks -> Base Info ( 1 min )";
            this.rdoScanBaseInfo.UseVisualStyleBackColor = true;
            // 
            // rdoScanAllFilters
            // 
            this.rdoScanAllFilters.AutoSize = true;
            this.rdoScanAllFilters.Location = new System.Drawing.Point(20, 23);
            this.rdoScanAllFilters.Name = "rdoScanAllFilters";
            this.rdoScanAllFilters.Size = new System.Drawing.Size(165, 17);
            this.rdoScanAllFilters.TabIndex = 13;
            this.rdoScanAllFilters.Text = "All Filters -> Stocks ( 90 mins )";
            this.rdoScanAllFilters.UseVisualStyleBackColor = true;
            // 
            // lblFilterUpdate
            // 
            this.lblFilterUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterUpdate.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblFilterUpdate.Location = new System.Drawing.Point(178, 19);
            this.lblFilterUpdate.Name = "lblFilterUpdate";
            this.lblFilterUpdate.Size = new System.Drawing.Size(163, 18);
            this.lblFilterUpdate.TabIndex = 17;
            this.lblFilterUpdate.Text = "Filter Scan Progress";
            // 
            // prgFilterUpdate
            // 
            this.prgFilterUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgFilterUpdate.Location = new System.Drawing.Point(181, 35);
            this.prgFilterUpdate.Name = "prgFilterUpdate";
            this.prgFilterUpdate.Size = new System.Drawing.Size(160, 18);
            this.prgFilterUpdate.Step = 1;
            this.prgFilterUpdate.TabIndex = 18;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.txtScanElapsedTime);
            this.grpProgress.Controls.Add(this.label1);
            this.grpProgress.Controls.Add(this.prgPageUpdate);
            this.grpProgress.Controls.Add(this.prgFilterUpdate);
            this.grpProgress.Controls.Add(this.lblFilterUpdate);
            this.grpProgress.Controls.Add(this.lblPageUpdate);
            this.grpProgress.Location = new System.Drawing.Point(12, 510);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(350, 91);
            this.grpProgress.TabIndex = 19;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = "Scan Progress";
            // 
            // txtScanElapsedTime
            // 
            this.txtScanElapsedTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScanElapsedTime.Location = new System.Drawing.Point(95, 63);
            this.txtScanElapsedTime.Multiline = true;
            this.txtScanElapsedTime.Name = "txtScanElapsedTime";
            this.txtScanElapsedTime.ReadOnly = true;
            this.txtScanElapsedTime.Size = new System.Drawing.Size(62, 18);
            this.txtScanElapsedTime.TabIndex = 20;
            this.txtScanElapsedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(9, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Time Elapsed:";
            // 
            // btnLoadFilterScanResults
            // 
            this.btnLoadFilterScanResults.Location = new System.Drawing.Point(12, 272);
            this.btnLoadFilterScanResults.Name = "btnLoadFilterScanResults";
            this.btnLoadFilterScanResults.Size = new System.Drawing.Size(84, 39);
            this.btnLoadFilterScanResults.TabIndex = 20;
            this.btnLoadFilterScanResults.Text = "Load Filter Scan Results";
            this.btnLoadFilterScanResults.UseVisualStyleBackColor = true;
            this.btnLoadFilterScanResults.Click += new System.EventHandler(this.BtnLoadFilterScanResults_Click);
            // 
            // lblTotalFilterResults
            // 
            this.lblTotalFilterResults.Location = new System.Drawing.Point(102, 276);
            this.lblTotalFilterResults.Name = "lblTotalFilterResults";
            this.lblTotalFilterResults.Size = new System.Drawing.Size(246, 15);
            this.lblTotalFilterResults.TabIndex = 4;
            this.lblTotalFilterResults.Text = "Total Filter Results :";
            // 
            // lblLastRecordDate
            // 
            this.lblLastRecordDate.Location = new System.Drawing.Point(102, 295);
            this.lblLastRecordDate.Name = "lblLastRecordDate";
            this.lblLastRecordDate.Size = new System.Drawing.Size(246, 15);
            this.lblLastRecordDate.TabIndex = 21;
            this.lblLastRecordDate.Text = "Last Record Date :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Finviz_Scraper.Properties.Resources.iMICA_Logo_TEXT_Only;
            this.pictureBox1.Location = new System.Drawing.Point(11, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "iMICA";
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNextScan);
            this.groupBox2.Controls.Add(this.chkAutoScan);
            this.groupBox2.Controls.Add(this.lblNextScan);
            this.groupBox2.Controls.Add(this.grpExclude);
            this.groupBox2.Location = new System.Drawing.Point(376, 498);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 103);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scan Scheduling";
            // 
            // txtNextScan
            // 
            this.txtNextScan.BackColor = System.Drawing.SystemColors.ControlText;
            this.txtNextScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNextScan.Font = new System.Drawing.Font("OCR A Extended", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNextScan.ForeColor = System.Drawing.Color.Lime;
            this.txtNextScan.Location = new System.Drawing.Point(15, 71);
            this.txtNextScan.Multiline = true;
            this.txtNextScan.Name = "txtNextScan";
            this.txtNextScan.ReadOnly = true;
            this.txtNextScan.Size = new System.Drawing.Size(356, 22);
            this.txtNextScan.TabIndex = 21;
            this.txtNextScan.Text = "No scheduled scan found.";
            this.txtNextScan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkAutoScan
            // 
            this.chkAutoScan.AutoSize = true;
            this.chkAutoScan.Checked = true;
            this.chkAutoScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScan.Location = new System.Drawing.Point(15, 26);
            this.chkAutoScan.Name = "chkAutoScan";
            this.chkAutoScan.Size = new System.Drawing.Size(132, 17);
            this.chkAutoScan.TabIndex = 22;
            this.chkAutoScan.Text = "Enable Auto Scanning";
            this.chkAutoScan.UseVisualStyleBackColor = true;
            this.chkAutoScan.CheckedChanged += new System.EventHandler(this.ChkAutoScan_CheckedChanged);
            // 
            // lblNextScan
            // 
            this.lblNextScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextScan.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNextScan.Location = new System.Drawing.Point(12, 50);
            this.lblNextScan.Name = "lblNextScan";
            this.lblNextScan.Size = new System.Drawing.Size(134, 18);
            this.lblNextScan.TabIndex = 21;
            this.lblNextScan.Text = "Next Scheduled Scan: ";
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.rtbLog.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.ForeColor = System.Drawing.Color.Lime;
            this.rtbLog.Location = new System.Drawing.Point(11, 90);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(752, 172);
            this.rtbLog.TabIndex = 25;
            this.rtbLog.Text = "";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("OCR A Extended", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(206, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(570, 33);
            this.label3.TabIndex = 28;
            this.label3.Text = "Intelligent Market Investment";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("OCR A Extended", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(242, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(533, 37);
            this.label4.TabIndex = 29;
            this.label4.Text = "Curating Automatron";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 609);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblLastRecordDate);
            this.Controls.Add(this.lblTotalFilterResults);
            this.Controls.Add(this.btnLoadFilterScanResults);
            this.Controls.Add(this.grpProgress);
            this.Controls.Add(this.grpScanType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBaseFilterSettings);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iMICA | v. 0.2.1";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.grpExclude.ResumeLayout(false);
            this.grpExclude.PerformLayout();
            this.grpBaseFilterSettings.ResumeLayout(false);
            this.grpBaseFilterSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.grpScanType.ResumeLayout(false);
            this.grpScanType.PerformLayout();
            this.grpProgress.ResumeLayout(false);
            this.grpProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScanFiltersOptions;
        private System.Windows.Forms.Label lblTotalFilters;
        private System.Windows.Forms.Label lblTotalOptions;
        private System.Windows.Forms.CheckBox chkExcludePrice;
        private System.Windows.Forms.GroupBox grpExclude;
        private System.Windows.Forms.CheckBox chkExcludeAverageVolume;
        private System.Windows.Forms.CheckBox chkExcludeIndustry;
        private System.Windows.Forms.CheckBox chkExcludeSector;
        private System.Windows.Forms.Label lblPageUpdate;
        private System.Windows.Forms.ProgressBar prgPageUpdate;
        private System.Windows.Forms.Button btnPerformScan;
        private System.Windows.Forms.GroupBox grpBaseFilterSettings;
        private System.Windows.Forms.CheckBox chkScanFloatUnder50m;
        private System.Windows.Forms.CheckBox chkScanExcludeFunds;
        private System.Windows.Forms.CheckBox chkScanVolOver400k;
        private System.Windows.Forms.CheckBox chkScanPriceUnder20;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpScanType;
        private System.Windows.Forms.RadioButton rdoScanAllInfo;
        private System.Windows.Forms.RadioButton rdoScanBaseInfo;
        private System.Windows.Forms.RadioButton rdoScanAllFilters;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFilterUpdate;
        private System.Windows.Forms.ProgressBar prgFilterUpdate;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.TextBox txtScanElapsedTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadFilterScanResults;
        private System.Windows.Forms.Label lblTotalFilterResults;
        private System.Windows.Forms.Label lblLastRecordDate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkTestMode;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNextScan;
        private System.Windows.Forms.CheckBox chkAutoScan;
        private System.Windows.Forms.Label lblNextScan;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

