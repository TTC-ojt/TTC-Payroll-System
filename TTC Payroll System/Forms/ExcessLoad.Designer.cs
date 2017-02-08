namespace TTC_Payroll_System.Forms
{
    partial class ExcessLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcessLoad));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlPrint = new System.Windows.Forms.Panel();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.pnlPrint2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCenterAdministrator = new System.Windows.Forms.Label();
            this.lblAccountingAssistant = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSummaryDate = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvLoads = new System.Windows.Forms.DataGridView();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgcTrainee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPrint.SuspendLayout();
            this.pnlPrint2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPrint
            // 
            this.pnlPrint.BackColor = System.Drawing.Color.White;
            this.pnlPrint.Controls.Add(this.btnExportToExcel);
            this.pnlPrint.Controls.Add(this.txtPeriod);
            this.pnlPrint.Controls.Add(this.label7);
            this.pnlPrint.Controls.Add(this.lblSummaryDate);
            this.pnlPrint.Controls.Add(this.pictureBox2);
            this.pnlPrint.Controls.Add(this.btnAdd);
            this.pnlPrint.Controls.Add(this.btnClear);
            this.pnlPrint.Controls.Add(this.pictureBox3);
            this.pnlPrint.Controls.Add(this.pictureBox1);
            this.pnlPrint.Controls.Add(this.label3);
            this.pnlPrint.Controls.Add(this.label2);
            this.pnlPrint.Controls.Add(this.btnPrint);
            this.pnlPrint.Controls.Add(this.label1);
            this.pnlPrint.Controls.Add(this.btnClose);
            this.pnlPrint.Controls.Add(this.dgvLoads);
            this.pnlPrint.Controls.Add(this.pnlPrint2);
            this.pnlPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrint.Location = new System.Drawing.Point(5, 5);
            this.pnlPrint.Name = "pnlPrint";
            this.pnlPrint.Size = new System.Drawing.Size(965, 551);
            this.pnlPrint.TabIndex = 0;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExportToExcel.BackColor = System.Drawing.Color.Silver;
            this.btnExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnExportToExcel.FlatAppearance.BorderSize = 2;
            this.btnExportToExcel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.Location = new System.Drawing.Point(822, 485);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(65, 65);
            this.btnExportToExcel.TabIndex = 12;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // txtPeriod
            // 
            this.txtPeriod.BackColor = System.Drawing.Color.White;
            this.txtPeriod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPeriod.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.txtPeriod.Location = new System.Drawing.Point(127, 112);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(422, 19);
            this.txtPeriod.TabIndex = 44;
            // 
            // pnlPrint2
            // 
            this.pnlPrint2.BackColor = System.Drawing.Color.White;
            this.pnlPrint2.Controls.Add(this.label5);
            this.pnlPrint2.Controls.Add(this.lblCenterAdministrator);
            this.pnlPrint2.Controls.Add(this.lblAccountingAssistant);
            this.pnlPrint2.Controls.Add(this.label6);
            this.pnlPrint2.Controls.Add(this.label4);
            this.pnlPrint2.Controls.Add(this.label8);
            this.pnlPrint2.Location = new System.Drawing.Point(4, 391);
            this.pnlPrint2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlPrint2.Name = "pnlPrint2";
            this.pnlPrint2.Size = new System.Drawing.Size(958, 93);
            this.pnlPrint2.TabIndex = 12;
            this.pnlPrint2.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(80, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "Accounting Officer";
            // 
            // lblCenterAdministrator
            // 
            this.lblCenterAdministrator.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCenterAdministrator.AutoSize = true;
            this.lblCenterAdministrator.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenterAdministrator.Location = new System.Drawing.Point(678, 60);
            this.lblCenterAdministrator.Margin = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.lblCenterAdministrator.Name = "lblCenterAdministrator";
            this.lblCenterAdministrator.Size = new System.Drawing.Size(46, 19);
            this.lblCenterAdministrator.TabIndex = 9;
            this.lblCenterAdministrator.Text = "Name";
            // 
            // lblAccountingAssistant
            // 
            this.lblAccountingAssistant.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAccountingAssistant.AutoSize = true;
            this.lblAccountingAssistant.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountingAssistant.Location = new System.Drawing.Point(80, 60);
            this.lblAccountingAssistant.Margin = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.lblAccountingAssistant.Name = "lblAccountingAssistant";
            this.lblAccountingAssistant.Size = new System.Drawing.Size(46, 19);
            this.lblAccountingAssistant.TabIndex = 9;
            this.lblAccountingAssistant.Text = "Name";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(692, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(75, 0, 0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Approved by:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(69, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(75, 0, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Prepared by:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(678, 78);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 19);
            this.label8.TabIndex = 10;
            this.label8.Text = "Admin Officer";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 19);
            this.label7.TabIndex = 43;
            this.label7.Text = "Period Covered:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSummaryDate
            // 
            this.lblSummaryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummaryDate.AutoSize = true;
            this.lblSummaryDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryDate.Location = new System.Drawing.Point(3, 93);
            this.lblSummaryDate.Name = "lblSummaryDate";
            this.lblSummaryDate.Size = new System.Drawing.Size(291, 19);
            this.lblSummaryDate.TabIndex = 43;
            this.lblSummaryDate.Text = "Payroll Summary of Trainer - Excess Load";
            this.lblSummaryDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(639, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(66, 65);
            this.pictureBox2.TabIndex = 42;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(1529, 18);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(66, 53);
            this.pictureBox3.TabIndex = 40;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(283, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 53);
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(355, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(277, 16);
            this.label3.TabIndex = 39;
            this.label3.Text = "Luisita Business Park, San Miguel, Tarlac City";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Algerian", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(393, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 18);
            this.label2.TabIndex = 38;
            this.label2.Text = "TARLAC TRAINING CENTER";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Silver;
            this.btnPrint.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(893, 487);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(68, 61);
            this.btnPrint.TabIndex = 36;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(356, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 19);
            this.label1.TabIndex = 37;
            this.label1.Text = "Jose and Demetria Cojuanco Foundation Inc.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Silver;
            this.btnClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(3, 487);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 61);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "CLOSE";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvLoads
            // 
            this.dgvLoads.AllowUserToAddRows = false;
            this.dgvLoads.AllowUserToDeleteRows = false;
            this.dgvLoads.AllowUserToResizeColumns = false;
            this.dgvLoads.AllowUserToResizeRows = false;
            this.dgvLoads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoads.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLoads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcTrainee,
            this.dgcLoad,
            this.dgcHours,
            this.dgcRate,
            this.dgcTotal,
            this.dgcTax,
            this.dgcNet});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLoads.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoads.Location = new System.Drawing.Point(3, 145);
            this.dgvLoads.Name = "dgvLoads";
            this.dgvLoads.RowHeadersVisible = false;
            this.dgvLoads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoads.Size = new System.Drawing.Size(959, 244);
            this.dgvLoads.TabIndex = 2;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAdd.BackColor = System.Drawing.Color.Silver;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.FlatAppearance.BorderSize = 2;
            this.btnAdd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(892, 395);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(69, 37);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClear.BackColor = System.Drawing.Color.Silver;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FlatAppearance.BorderSize = 2;
            this.btnClear.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(7, 395);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 37);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgcTrainee
            // 
            this.dgcTrainee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcTrainee.FillWeight = 30F;
            this.dgcTrainee.HeaderText = "Trainee";
            this.dgcTrainee.Name = "dgcTrainee";
            // 
            // dgcLoad
            // 
            this.dgcLoad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcLoad.FillWeight = 25F;
            this.dgcLoad.HeaderText = "";
            this.dgcLoad.Name = "dgcLoad";
            // 
            // dgcHours
            // 
            this.dgcHours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcHours.FillWeight = 10F;
            this.dgcHours.HeaderText = "Excess Load";
            this.dgcHours.Name = "dgcHours";
            // 
            // dgcRate
            // 
            this.dgcRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcRate.FillWeight = 10F;
            this.dgcRate.HeaderText = "Hourly Rate";
            this.dgcRate.Name = "dgcRate";
            // 
            // dgcTotal
            // 
            this.dgcTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcTotal.FillWeight = 10F;
            this.dgcTotal.HeaderText = "Total";
            this.dgcTotal.Name = "dgcTotal";
            // 
            // dgcTax
            // 
            this.dgcTax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcTax.FillWeight = 10F;
            this.dgcTax.HeaderText = "Withholding Tax";
            this.dgcTax.Name = "dgcTax";
            // 
            // dgcNet
            // 
            this.dgcNet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcNet.FillWeight = 10F;
            this.dgcNet.HeaderText = "Net Pay";
            this.dgcNet.Name = "dgcNet";
            // 
            // ExcessLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(975, 561);
            this.Controls.Add(this.pnlPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExcessLoad";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SocialObligation";
            this.Load += new System.EventHandler(this.SocialObligation_Load);
            this.pnlPrint.ResumeLayout(false);
            this.pnlPrint.PerformLayout();
            this.pnlPrint2.ResumeLayout(false);
            this.pnlPrint2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoads)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPrint;
        private System.Windows.Forms.DataGridView dgvLoads;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblSummaryDate;
        private System.Windows.Forms.Panel pnlPrint2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCenterAdministrator;
        private System.Windows.Forms.Label lblAccountingAssistant;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTrainee;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcNet;
    }
}