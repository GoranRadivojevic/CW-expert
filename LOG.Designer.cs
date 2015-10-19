namespace CWExpert
{
    partial class LOG
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DXClusterMenuClick = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogImport = new System.Windows.Forms.Button();
            this.btnLogExport = new System.Windows.Forms.Button();
            this.btnLOGEraseRow = new System.Windows.Forms.Button();
            this.btnLogSearch = new System.Windows.Forms.Button();
            this.txtCALL = new System.Windows.Forms.TextBox();
            this.lblLogSearch = new System.Windows.Forms.Label();
            this.grpLOG = new System.Windows.Forms.GroupBox();
            this.btnLogOpen = new System.Windows.Forms.Button();
            this.txtSearchCount = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.GroupBox();
            this.txtQPSK250 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPSK250 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSSB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtQPSK125 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQPSK63 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQPSK31 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPSK125 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPSK63 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPSK31 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRTTY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCW = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchFirst = new System.Windows.Forms.Button();
            this.btnSearchLast = new System.Windows.Forms.Button();
            this.btnLOGLast = new System.Windows.Forms.Button();
            this.btnLOGFirst = new System.Windows.Forms.Button();
            this.btnLOGPrev = new System.Windows.Forms.Button();
            this.btnLOGNext = new System.Windows.Forms.Button();
            this.btnLOGNew = new System.Windows.Forms.Button();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.btnSearchPrev = new System.Windows.Forms.Button();
            this.btnLOGSave = new System.Windows.Forms.Button();
            this.btnLOGAddRow = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.NewLOGFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dataGridQSOLog = new DBDataGridView();
            this.contextMenuStrip1.SuspendLayout();
            this.grpLOG.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQSOLog)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DXClusterMenuClick});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 26);
            // 
            // DXClusterMenuClick
            // 
            this.DXClusterMenuClick.Name = "DXClusterMenuClick";
            this.DXClusterMenuClick.Size = new System.Drawing.Size(172, 22);
            this.DXClusterMenuClick.Text = "Send to DX Cluster";
            this.DXClusterMenuClick.Click += new System.EventHandler(this.DXClusterMenuClick_Click);
            // 
            // btnLogImport
            // 
            this.btnLogImport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogImport.Location = new System.Drawing.Point(418, 59);
            this.btnLogImport.Name = "btnLogImport";
            this.btnLogImport.Size = new System.Drawing.Size(65, 23);
            this.btnLogImport.TabIndex = 1;
            this.btnLogImport.Text = "Import LOG";
            this.btnLogImport.UseVisualStyleBackColor = false;
            this.btnLogImport.Click += new System.EventHandler(this.btnLogImport_Click);
            // 
            // btnLogExport
            // 
            this.btnLogExport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogExport.Location = new System.Drawing.Point(484, 59);
            this.btnLogExport.Name = "btnLogExport";
            this.btnLogExport.Size = new System.Drawing.Size(65, 23);
            this.btnLogExport.TabIndex = 2;
            this.btnLogExport.Text = "Export LOG";
            this.btnLogExport.UseVisualStyleBackColor = false;
            this.btnLogExport.Click += new System.EventHandler(this.btnLogExport_Click);
            // 
            // btnLOGEraseRow
            // 
            this.btnLOGEraseRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGEraseRow.Location = new System.Drawing.Point(110, 59);
            this.btnLOGEraseRow.Name = "btnLOGEraseRow";
            this.btnLOGEraseRow.Size = new System.Drawing.Size(27, 23);
            this.btnLOGEraseRow.TabIndex = 3;
            this.btnLOGEraseRow.Text = "-";
            this.btnLOGEraseRow.UseVisualStyleBackColor = false;
            this.btnLOGEraseRow.Click += new System.EventHandler(this.btnLOGEraseRow_Click);
            // 
            // btnLogSearch
            // 
            this.btnLogSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogSearch.Location = new System.Drawing.Point(231, 22);
            this.btnLogSearch.Name = "btnLogSearch";
            this.btnLogSearch.Size = new System.Drawing.Size(75, 23);
            this.btnLogSearch.TabIndex = 4;
            this.btnLogSearch.Text = "Search";
            this.btnLogSearch.UseVisualStyleBackColor = false;
            this.btnLogSearch.Click += new System.EventHandler(this.btnLogSearch_Click);
            // 
            // txtCALL
            // 
            this.txtCALL.Location = new System.Drawing.Point(109, 22);
            this.txtCALL.MaxLength = 16;
            this.txtCALL.Name = "txtCALL";
            this.txtCALL.Size = new System.Drawing.Size(112, 20);
            this.txtCALL.TabIndex = 5;
            this.txtCALL.TextChanged += new System.EventHandler(this.txtCALL_TextChanged);
            // 
            // lblLogSearch
            // 
            this.lblLogSearch.AutoSize = true;
            this.lblLogSearch.Location = new System.Drawing.Point(66, 25);
            this.lblLogSearch.Name = "lblLogSearch";
            this.lblLogSearch.Size = new System.Drawing.Size(33, 13);
            this.lblLogSearch.TabIndex = 6;
            this.lblLogSearch.Text = "CALL";
            // 
            // grpLOG
            // 
            this.grpLOG.Controls.Add(this.btnLogOpen);
            this.grpLOG.Controls.Add(this.txtSearchCount);
            this.grpLOG.Controls.Add(this.panel1);
            this.grpLOG.Controls.Add(this.btnSearchFirst);
            this.grpLOG.Controls.Add(this.btnSearchLast);
            this.grpLOG.Controls.Add(this.btnLOGLast);
            this.grpLOG.Controls.Add(this.btnLOGFirst);
            this.grpLOG.Controls.Add(this.btnLOGPrev);
            this.grpLOG.Controls.Add(this.btnLOGNext);
            this.grpLOG.Controls.Add(this.btnLOGNew);
            this.grpLOG.Controls.Add(this.btnSearchNext);
            this.grpLOG.Controls.Add(this.btnSearchPrev);
            this.grpLOG.Controls.Add(this.btnLOGSave);
            this.grpLOG.Controls.Add(this.btnLOGAddRow);
            this.grpLOG.Controls.Add(this.btnLOGEraseRow);
            this.grpLOG.Controls.Add(this.btnLogImport);
            this.grpLOG.Controls.Add(this.lblLogSearch);
            this.grpLOG.Controls.Add(this.btnLogExport);
            this.grpLOG.Controls.Add(this.txtCALL);
            this.grpLOG.Controls.Add(this.btnLogSearch);
            this.grpLOG.Location = new System.Drawing.Point(9, 431);
            this.grpLOG.Name = "grpLOG";
            this.grpLOG.Size = new System.Drawing.Size(991, 99);
            this.grpLOG.TabIndex = 7;
            this.grpLOG.TabStop = false;
            this.grpLOG.Text = "LOG";
            // 
            // btnLogOpen
            // 
            this.btnLogOpen.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogOpen.Location = new System.Drawing.Point(286, 59);
            this.btnLogOpen.Name = "btnLogOpen";
            this.btnLogOpen.Size = new System.Drawing.Size(65, 23);
            this.btnLogOpen.TabIndex = 19;
            this.btnLogOpen.Text = "Open";
            this.btnLogOpen.UseVisualStyleBackColor = false;
            this.btnLogOpen.Click += new System.EventHandler(this.btnLogOpen_Click);
            // 
            // txtSearchCount
            // 
            this.txtSearchCount.Location = new System.Drawing.Point(390, 23);
            this.txtSearchCount.MaxLength = 4;
            this.txtSearchCount.Name = "txtSearchCount";
            this.txtSearchCount.Size = new System.Drawing.Size(30, 20);
            this.txtSearchCount.TabIndex = 18;
            this.txtSearchCount.Text = "0";
            this.txtSearchCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtQPSK250);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtPSK250);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtSSB);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtQPSK125);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtQPSK63);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtQPSK31);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtPSK125);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtPSK63);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPSK31);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtRTTY);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCW);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(567, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 88);
            this.panel1.TabIndex = 20;
            this.panel1.TabStop = false;
            // 
            // txtQPSK250
            // 
            this.txtQPSK250.Location = new System.Drawing.Point(357, 38);
            this.txtQPSK250.MaxLength = 16;
            this.txtQPSK250.Name = "txtQPSK250";
            this.txtQPSK250.Size = new System.Drawing.Size(40, 20);
            this.txtQPSK250.TabIndex = 40;
            this.txtQPSK250.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(304, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "QPSK250";
            // 
            // txtPSK250
            // 
            this.txtPSK250.Location = new System.Drawing.Point(251, 12);
            this.txtPSK250.MaxLength = 16;
            this.txtPSK250.Name = "txtPSK250";
            this.txtPSK250.Size = new System.Drawing.Size(40, 20);
            this.txtPSK250.TabIndex = 38;
            this.txtPSK250.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(199, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "PSK250";
            // 
            // txtSSB
            // 
            this.txtSSB.Location = new System.Drawing.Point(57, 64);
            this.txtSSB.MaxLength = 16;
            this.txtSSB.Name = "txtSSB";
            this.txtSSB.Size = new System.Drawing.Size(40, 20);
            this.txtSSB.TabIndex = 36;
            this.txtSSB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "SSB";
            // 
            // txtQPSK125
            // 
            this.txtQPSK125.Location = new System.Drawing.Point(357, 12);
            this.txtQPSK125.MaxLength = 16;
            this.txtQPSK125.Name = "txtQPSK125";
            this.txtQPSK125.Size = new System.Drawing.Size(40, 20);
            this.txtQPSK125.TabIndex = 34;
            this.txtQPSK125.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "QPSK125";
            // 
            // txtQPSK63
            // 
            this.txtQPSK63.Location = new System.Drawing.Point(251, 64);
            this.txtQPSK63.MaxLength = 16;
            this.txtQPSK63.Name = "txtQPSK63";
            this.txtQPSK63.Size = new System.Drawing.Size(40, 20);
            this.txtQPSK63.TabIndex = 32;
            this.txtQPSK63.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "QPSK63";
            // 
            // txtQPSK31
            // 
            this.txtQPSK31.Location = new System.Drawing.Point(251, 38);
            this.txtQPSK31.MaxLength = 16;
            this.txtQPSK31.Name = "txtQPSK31";
            this.txtQPSK31.Size = new System.Drawing.Size(40, 20);
            this.txtQPSK31.TabIndex = 30;
            this.txtQPSK31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "QPSK31";
            // 
            // txtPSK125
            // 
            this.txtPSK125.Location = new System.Drawing.Point(153, 64);
            this.txtPSK125.MaxLength = 16;
            this.txtPSK125.Name = "txtPSK125";
            this.txtPSK125.Size = new System.Drawing.Size(40, 20);
            this.txtPSK125.TabIndex = 28;
            this.txtPSK125.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "PSK125";
            // 
            // txtPSK63
            // 
            this.txtPSK63.Location = new System.Drawing.Point(153, 38);
            this.txtPSK63.MaxLength = 16;
            this.txtPSK63.Name = "txtPSK63";
            this.txtPSK63.Size = new System.Drawing.Size(40, 20);
            this.txtPSK63.TabIndex = 26;
            this.txtPSK63.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "PSK63";
            // 
            // txtPSK31
            // 
            this.txtPSK31.Location = new System.Drawing.Point(153, 12);
            this.txtPSK31.MaxLength = 16;
            this.txtPSK31.Name = "txtPSK31";
            this.txtPSK31.Size = new System.Drawing.Size(40, 20);
            this.txtPSK31.TabIndex = 24;
            this.txtPSK31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "PSK31";
            // 
            // txtRTTY
            // 
            this.txtRTTY.Location = new System.Drawing.Point(57, 38);
            this.txtRTTY.MaxLength = 16;
            this.txtRTTY.Name = "txtRTTY";
            this.txtRTTY.Size = new System.Drawing.Size(40, 20);
            this.txtRTTY.TabIndex = 22;
            this.txtRTTY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "RTTY";
            // 
            // txtCW
            // 
            this.txtCW.Location = new System.Drawing.Point(57, 12);
            this.txtCW.MaxLength = 16;
            this.txtCW.Name = "txtCW";
            this.txtCW.Size = new System.Drawing.Size(40, 20);
            this.txtCW.TabIndex = 20;
            this.txtCW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "CW";
            // 
            // btnSearchFirst
            // 
            this.btnSearchFirst.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchFirst.Location = new System.Drawing.Point(316, 22);
            this.btnSearchFirst.Name = "btnSearchFirst";
            this.btnSearchFirst.Size = new System.Drawing.Size(27, 23);
            this.btnSearchFirst.TabIndex = 17;
            this.btnSearchFirst.Text = "<<";
            this.btnSearchFirst.UseVisualStyleBackColor = false;
            this.btnSearchFirst.Click += new System.EventHandler(this.btnSearchFirst_Click);
            // 
            // btnSearchLast
            // 
            this.btnSearchLast.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchLast.Location = new System.Drawing.Point(467, 22);
            this.btnSearchLast.Name = "btnSearchLast";
            this.btnSearchLast.Size = new System.Drawing.Size(27, 23);
            this.btnSearchLast.TabIndex = 16;
            this.btnSearchLast.Text = ">>";
            this.btnSearchLast.UseVisualStyleBackColor = false;
            this.btnSearchLast.Click += new System.EventHandler(this.btnSearchLast_Click);
            // 
            // btnLOGLast
            // 
            this.btnLOGLast.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGLast.Location = new System.Drawing.Point(176, 59);
            this.btnLOGLast.Name = "btnLOGLast";
            this.btnLOGLast.Size = new System.Drawing.Size(27, 23);
            this.btnLOGLast.TabIndex = 15;
            this.btnLOGLast.Text = ">>";
            this.btnLOGLast.UseVisualStyleBackColor = false;
            this.btnLOGLast.Click += new System.EventHandler(this.btnLOGLast_Click);
            // 
            // btnLOGFirst
            // 
            this.btnLOGFirst.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGFirst.Location = new System.Drawing.Point(11, 59);
            this.btnLOGFirst.Name = "btnLOGFirst";
            this.btnLOGFirst.Size = new System.Drawing.Size(27, 23);
            this.btnLOGFirst.TabIndex = 14;
            this.btnLOGFirst.Text = "<<";
            this.btnLOGFirst.UseVisualStyleBackColor = false;
            this.btnLOGFirst.Click += new System.EventHandler(this.btnLOGFirst_Click);
            // 
            // btnLOGPrev
            // 
            this.btnLOGPrev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGPrev.Location = new System.Drawing.Point(44, 59);
            this.btnLOGPrev.Name = "btnLOGPrev";
            this.btnLOGPrev.Size = new System.Drawing.Size(27, 23);
            this.btnLOGPrev.TabIndex = 13;
            this.btnLOGPrev.Text = "<";
            this.btnLOGPrev.UseVisualStyleBackColor = false;
            this.btnLOGPrev.Click += new System.EventHandler(this.btnLOGPrev_Click);
            // 
            // btnLOGNext
            // 
            this.btnLOGNext.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGNext.Location = new System.Drawing.Point(143, 59);
            this.btnLOGNext.Name = "btnLOGNext";
            this.btnLOGNext.Size = new System.Drawing.Size(27, 23);
            this.btnLOGNext.TabIndex = 12;
            this.btnLOGNext.Text = ">";
            this.btnLOGNext.UseVisualStyleBackColor = false;
            this.btnLOGNext.Click += new System.EventHandler(this.btnLOGNext_Click);
            // 
            // btnLOGNew
            // 
            this.btnLOGNew.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGNew.Location = new System.Drawing.Point(352, 59);
            this.btnLOGNew.Name = "btnLOGNew";
            this.btnLOGNew.Size = new System.Drawing.Size(65, 23);
            this.btnLOGNew.TabIndex = 11;
            this.btnLOGNew.Text = "New LOG";
            this.btnLOGNew.UseVisualStyleBackColor = false;
            this.btnLOGNew.Click += new System.EventHandler(this.btnLOGNew_Click);
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchNext.Location = new System.Drawing.Point(430, 22);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(27, 23);
            this.btnSearchNext.TabIndex = 10;
            this.btnSearchNext.Text = ">";
            this.btnSearchNext.UseVisualStyleBackColor = false;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // btnSearchPrev
            // 
            this.btnSearchPrev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchPrev.Location = new System.Drawing.Point(353, 22);
            this.btnSearchPrev.Name = "btnSearchPrev";
            this.btnSearchPrev.Size = new System.Drawing.Size(27, 23);
            this.btnSearchPrev.TabIndex = 9;
            this.btnSearchPrev.Text = "<";
            this.btnSearchPrev.UseVisualStyleBackColor = false;
            this.btnSearchPrev.Click += new System.EventHandler(this.btnSearchPrev_Click);
            // 
            // btnLOGSave
            // 
            this.btnLOGSave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGSave.Location = new System.Drawing.Point(220, 59);
            this.btnLOGSave.Name = "btnLOGSave";
            this.btnLOGSave.Size = new System.Drawing.Size(65, 23);
            this.btnLOGSave.TabIndex = 8;
            this.btnLOGSave.Text = "Save";
            this.btnLOGSave.UseVisualStyleBackColor = false;
            this.btnLOGSave.Click += new System.EventHandler(this.btnLOGSave_Click);
            // 
            // btnLOGAddRow
            // 
            this.btnLOGAddRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGAddRow.Location = new System.Drawing.Point(77, 59);
            this.btnLOGAddRow.Name = "btnLOGAddRow";
            this.btnLOGAddRow.Size = new System.Drawing.Size(27, 23);
            this.btnLOGAddRow.TabIndex = 7;
            this.btnLOGAddRow.Text = "+";
            this.btnLOGAddRow.UseVisualStyleBackColor = false;
            this.btnLOGAddRow.Click += new System.EventHandler(this.btnLOGAddRow_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "\"LOG file|*.xml|All files|*.*\"";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // NewLOGFileDialog
            // 
            this.NewLOGFileDialog.Filter = "\"LOG file|*.xml|All files|*.*\"";
            this.NewLOGFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.NewLOGFileDialog_FileOk);
            // 
            // dataGridQSOLog
            // 
            this.dataGridQSOLog.AllowUserToOrderColumns = true;
            this.dataGridQSOLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridQSOLog.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridQSOLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridQSOLog.Location = new System.Drawing.Point(12, 12);
            this.dataGridQSOLog.Name = "dataGridQSOLog";
            this.dataGridQSOLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridQSOLog.RowHeadersWidth = 4;
            this.dataGridQSOLog.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridQSOLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridQSOLog.Size = new System.Drawing.Size(984, 413);
            this.dataGridQSOLog.TabIndex = 0;
            // 
            // LOG
            // 
            this.ClientSize = new System.Drawing.Size(1008, 534);
            this.Controls.Add(this.dataGridQSOLog);
            this.Controls.Add(this.grpLOG);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "LOG";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LOG Book";
            this.Load += new System.EventHandler(this.LOG_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LOG_Closing);
            this.Resize += new System.EventHandler(this.LOG_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.grpLOG.ResumeLayout(false);
            this.grpLOG.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQSOLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogImport;
        private System.Windows.Forms.Button btnLogExport;
        private System.Windows.Forms.Button btnLOGEraseRow;
        private System.Windows.Forms.Button btnLogSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DXClusterMenuClick;
        private System.Windows.Forms.TextBox txtCALL;
        private System.Windows.Forms.Label lblLogSearch;
        private System.Windows.Forms.GroupBox grpLOG;
        private System.Windows.Forms.Button btnLOGSave;
        private System.Windows.Forms.Button btnLOGAddRow;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLOGPrev;
        private System.Windows.Forms.Button btnLOGNext;
        private System.Windows.Forms.Button btnLOGNew;
        private System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.Button btnSearchPrev;
        private System.Windows.Forms.Button btnLOGFirst;
        private System.Windows.Forms.Button btnLOGLast;

        private void LOG_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private System.Windows.Forms.Button btnSearchFirst;
        private System.Windows.Forms.Button btnSearchLast;
        private System.Windows.Forms.TextBox txtSearchCount;
        private System.Windows.Forms.SaveFileDialog NewLOGFileDialog;
        private System.Windows.Forms.Button btnLogOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtCW;
        public System.Windows.Forms.TextBox txtPSK63;
        public System.Windows.Forms.TextBox txtPSK31;
        public System.Windows.Forms.TextBox txtRTTY;
        public System.Windows.Forms.TextBox txtSSB;
        public System.Windows.Forms.TextBox txtQPSK125;
        public System.Windows.Forms.TextBox txtQPSK63;
        public System.Windows.Forms.TextBox txtQPSK31;
        public System.Windows.Forms.TextBox txtPSK125;
        public System.Windows.Forms.TextBox txtQPSK250;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtPSK250;
        private System.Windows.Forms.Label label10;
        private DBDataGridView dataGridQSOLog;
    }

    class DBDataGridView : System.Windows.Forms.DataGridView
    {
        public DBDataGridView() { DoubleBuffered = true; }    // for render speed
    }
}