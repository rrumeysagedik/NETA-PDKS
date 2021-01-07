namespace deneme2
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dateBaslama = new System.Windows.Forms.DateTimePicker();
            this.dateBitis = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtCmdKey = new System.Windows.Forms.TextBox();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDurum = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSaat = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstLogs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGeneralAttData = new System.Windows.Forms.Button();
            this.BtnExportExcel = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnAddDevice = new System.Windows.Forms.Button();
            this.dgvDevices = new System.Windows.Forms.DataGridView();
            this.chcbx = new System.Windows.Forms.CheckBox();
            this.btnExportMultiLineExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).BeginInit();
            this.SuspendLayout();
            // 
            // dateBaslama
            // 
            this.dateBaslama.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateBaslama.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateBaslama.Location = new System.Drawing.Point(100, 11);
            this.dateBaslama.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateBaslama.Name = "dateBaslama";
            this.dateBaslama.Size = new System.Drawing.Size(268, 22);
            this.dateBaslama.TabIndex = 0;
            // 
            // dateBitis
            // 
            this.dateBitis.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateBitis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateBitis.Location = new System.Drawing.Point(100, 39);
            this.dateBitis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Size = new System.Drawing.Size(268, 22);
            this.dateBitis.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(-4, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "BAŞLAMA:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(36, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "BİTİŞ:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(472, 11);
            this.txtIp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(132, 22);
            this.txtIp.TabIndex = 4;
            this.txtIp.Text = "192.168.1.3";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(472, 33);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(132, 22);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "4370";
            // 
            // txtCmdKey
            // 
            this.txtCmdKey.Location = new System.Drawing.Point(472, 57);
            this.txtCmdKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCmdKey.Name = "txtCmdKey";
            this.txtCmdKey.Size = new System.Drawing.Size(132, 22);
            this.txtCmdKey.TabIndex = 6;
            // 
            // txtMac
            // 
            this.txtMac.Location = new System.Drawing.Point(472, 79);
            this.txtMac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(132, 22);
            this.txtMac.TabIndex = 7;
            this.txtMac.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(433, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "IP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(407, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 21);
            this.label4.TabIndex = 9;
            this.label4.Text = "PORT:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(407, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "ŞİFRE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(372, 85);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "CİHAZ NO:";
            // 
            // dgvLogs
            // 
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column14,
            this.Column15});
            this.dgvLogs.Location = new System.Drawing.Point(16, 130);
            this.dgvLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowHeadersWidth = 51;
            this.dgvLogs.Size = new System.Drawing.Size(896, 418);
            this.dgvLogs.TabIndex = 13;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "UserID";
            this.Column6.HeaderText = "PERSONEL KOD";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "UserName";
            this.Column7.HeaderText = "AD-SOYAD";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Date";
            this.Column8.HeaderText = "TARİH";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Times";
            this.Column9.HeaderText = "SAATLER";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.Width = 125;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "Elapsed";
            this.Column14.HeaderText = "GÜNLÜK TOPLAM SAAT";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.Width = 125;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "WeekElapsed";
            this.Column15.HeaderText = "HAFTALIK TOPLAM SAAT";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.Width = 125;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDurum,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.lblSaat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 603);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1669, 26);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "Durum";
            // 
            // lblDurum
            // 
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(54, 20);
            this.lblDurum.Text = "Durum";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // lblSaat
            // 
            this.lblSaat.Name = "lblSaat";
            this.lblSaat.Size = new System.Drawing.Size(0, 20);
            // 
            // lstLogs
            // 
            this.lstLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstLogs.HideSelection = false;
            this.lstLogs.Location = new System.Drawing.Point(1223, 11);
            this.lstLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstLogs.Name = "lstLogs";
            this.lstLogs.Size = new System.Drawing.Size(446, 111);
            this.lstLogs.TabIndex = 16;
            this.lstLogs.UseCompatibleStateImageBehavior = false;
            this.lstLogs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "DURUM";
            this.columnHeader1.Width = 171;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "DEĞER";
            this.columnHeader2.Width = 248;
            // 
            // btnGeneralAttData
            // 
            this.btnGeneralAttData.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGeneralAttData.Location = new System.Drawing.Point(613, 57);
            this.btnGeneralAttData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGeneralAttData.Name = "btnGeneralAttData";
            this.btnGeneralAttData.Size = new System.Drawing.Size(180, 46);
            this.btnGeneralAttData.TabIndex = 19;
            this.btnGeneralAttData.Text = "VERİ ÇEK";
            this.btnGeneralAttData.UseVisualStyleBackColor = true;
            this.btnGeneralAttData.Click += new System.EventHandler(this.BtnGeneralAttData_Click);
            // 
            // BtnExportExcel
            // 
            this.BtnExportExcel.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnExportExcel.Location = new System.Drawing.Point(16, 560);
            this.BtnExportExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnExportExcel.Name = "BtnExportExcel";
            this.BtnExportExcel.Size = new System.Drawing.Size(209, 34);
            this.BtnExportExcel.TabIndex = 24;
            this.BtnExportExcel.Text = "HAFTALIK RAPOR ";
            this.BtnExportExcel.UseVisualStyleBackColor = true;
            this.BtnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // dgvUsers
            // 
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvUsers.Location = new System.Drawing.Point(920, 130);
            this.dgvUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.Size = new System.Drawing.Size(1045, 418);
            this.dgvUsers.TabIndex = 26;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "UserID";
            this.Column1.HeaderText = "PERSONEL KOD";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "UserName";
            this.Column2.HeaderText = "AD-SOYAD";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Mail";
            this.Column3.HeaderText = "E-POSTA";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Izin";
            this.Column4.HeaderText = "İZİN";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TotalShift";
            this.Column5.HeaderText = "TOPLAM SAAT";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 125;
            // 
            // BtnAddDevice
            // 
            this.BtnAddDevice.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnAddDevice.Location = new System.Drawing.Point(613, 11);
            this.BtnAddDevice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnAddDevice.Name = "BtnAddDevice";
            this.BtnAddDevice.Size = new System.Drawing.Size(180, 43);
            this.BtnAddDevice.TabIndex = 27;
            this.BtnAddDevice.Text = "CİHAZ EKLE";
            this.BtnAddDevice.UseVisualStyleBackColor = true;
            this.BtnAddDevice.Click += new System.EventHandler(this.BtnAddDevice_Click);
            // 
            // dgvDevices
            // 
            this.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevices.Location = new System.Drawing.Point(801, 11);
            this.dgvDevices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDevices.Name = "dgvDevices";
            this.dgvDevices.RowHeadersWidth = 51;
            this.dgvDevices.Size = new System.Drawing.Size(413, 112);
            this.dgvDevices.TabIndex = 28;
            // 
            // chcbx
            // 
            this.chcbx.AutoSize = true;
            this.chcbx.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chcbx.Location = new System.Drawing.Point(1489, 560);
            this.chcbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chcbx.Name = "chcbx";
            this.chcbx.Size = new System.Drawing.Size(138, 28);
            this.chcbx.TabIndex = 29;
            this.chcbx.Text = "Tümünü Seç";
            this.chcbx.UseVisualStyleBackColor = true;
            this.chcbx.CheckStateChanged += new System.EventHandler(this.Chcbx_CheckStateChanged);
            // 
            // btnExportMultiLineExcel
            // 
            this.btnExportMultiLineExcel.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnExportMultiLineExcel.Location = new System.Drawing.Point(233, 560);
            this.btnExportMultiLineExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportMultiLineExcel.Name = "btnExportMultiLineExcel";
            this.btnExportMultiLineExcel.Size = new System.Drawing.Size(173, 34);
            this.btnExportMultiLineExcel.TabIndex = 24;
            this.btnExportMultiLineExcel.Text = "AYLIK RAPOR";
            this.btnExportMultiLineExcel.UseVisualStyleBackColor = true;
            this.btnExportMultiLineExcel.Click += new System.EventHandler(this.BtnExportMultiLineExcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1669, 629);
            this.Controls.Add(this.chcbx);
            this.Controls.Add(this.dgvDevices);
            this.Controls.Add(this.BtnAddDevice);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.btnExportMultiLineExcel);
            this.Controls.Add(this.BtnExportExcel);
            this.Controls.Add(this.btnGeneralAttData);
            this.Controls.Add(this.lstLogs);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMac);
            this.Controls.Add(this.txtCmdKey);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateBitis);
            this.Controls.Add(this.dateBaslama);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "NETA PDKS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateBaslama;
        private System.Windows.Forms.DateTimePicker dateBitis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtCmdKey;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblDurum;
        private System.Windows.Forms.ListView lstLogs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnGeneralAttData;
        private System.Windows.Forms.Button BtnExportExcel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.ToolStripStatusLabel lblSaat;
        private System.Windows.Forms.Button BtnAddDevice;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.CheckBox chcbx;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.Button btnExportMultiLineExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}

