namespace CWExpert
{
    partial class LOG_export
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
            this.rtbLOGPreview = new System.Windows.Forms.RichTextBox();
            this.btnLOGSaveAs = new System.Windows.Forms.Button();
            this.btnLOGExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboLOGformat = new System.Windows.Forms.ComboBox();
            this.lblLOGFormat = new System.Windows.Forms.Label();
            this.lblLOGPreview = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnPreviewClear = new System.Windows.Forms.Button();
            this.udFirst = new System.Windows.Forms.NumericUpDown();
            this.lblFirst = new System.Windows.Forms.Label();
            this.lblLast = new System.Windows.Forms.Label();
            this.udLast = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.udFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLast)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbLOGPreview
            // 
            this.rtbLOGPreview.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbLOGPreview.Location = new System.Drawing.Point(18, 103);
            this.rtbLOGPreview.Name = "rtbLOGPreview";
            this.rtbLOGPreview.Size = new System.Drawing.Size(548, 320);
            this.rtbLOGPreview.TabIndex = 0;
            this.rtbLOGPreview.Text = "";
            // 
            // btnLOGSaveAs
            // 
            this.btnLOGSaveAs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGSaveAs.Location = new System.Drawing.Point(160, 440);
            this.btnLOGSaveAs.Name = "btnLOGSaveAs";
            this.btnLOGSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnLOGSaveAs.TabIndex = 1;
            this.btnLOGSaveAs.Text = "Save As";
            this.btnLOGSaveAs.UseVisualStyleBackColor = false;
            this.btnLOGSaveAs.Click += new System.EventHandler(this.btnLOGSaveAs_Click);
            // 
            // btnLOGExport
            // 
            this.btnLOGExport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGExport.Location = new System.Drawing.Point(364, 33);
            this.btnLOGExport.Name = "btnLOGExport";
            this.btnLOGExport.Size = new System.Drawing.Size(75, 23);
            this.btnLOGExport.TabIndex = 2;
            this.btnLOGExport.Text = "Export";
            this.btnLOGExport.UseVisualStyleBackColor = false;
            this.btnLOGExport.Click += new System.EventHandler(this.btnLOGExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.Location = new System.Drawing.Point(350, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboLOGformat
            // 
            this.comboLOGformat.FormattingEnabled = true;
            this.comboLOGformat.Items.AddRange(new object[] {
            "ADIF",
            "Cabrillo ARI DX",
            "Cabrillo CQ-WW-RTTY",
            "BARTG RTTY Contest",
            "NA Sprint",
            "EDI",
            "TXT",
            "Custom"});
            this.comboLOGformat.Location = new System.Drawing.Point(204, 35);
            this.comboLOGformat.Name = "comboLOGformat";
            this.comboLOGformat.Size = new System.Drawing.Size(141, 21);
            this.comboLOGformat.TabIndex = 4;
            // 
            // lblLOGFormat
            // 
            this.lblLOGFormat.AutoSize = true;
            this.lblLOGFormat.Location = new System.Drawing.Point(146, 38);
            this.lblLOGFormat.Name = "lblLOGFormat";
            this.lblLOGFormat.Size = new System.Drawing.Size(39, 13);
            this.lblLOGFormat.TabIndex = 5;
            this.lblLOGFormat.Text = "Format";
            // 
            // lblLOGPreview
            // 
            this.lblLOGPreview.AutoSize = true;
            this.lblLOGPreview.Location = new System.Drawing.Point(26, 84);
            this.lblLOGPreview.Name = "lblLOGPreview";
            this.lblLOGPreview.Size = new System.Drawing.Size(45, 13);
            this.lblLOGPreview.TabIndex = 6;
            this.lblLOGPreview.Text = "Preview";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "TXT file|*.txt| EDI file |*.edi| ADIF file |*.adi| \" All files|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // btnPreviewClear
            // 
            this.btnPreviewClear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPreviewClear.Location = new System.Drawing.Point(255, 440);
            this.btnPreviewClear.Name = "btnPreviewClear";
            this.btnPreviewClear.Size = new System.Drawing.Size(75, 23);
            this.btnPreviewClear.TabIndex = 7;
            this.btnPreviewClear.Text = "Clear";
            this.btnPreviewClear.UseVisualStyleBackColor = false;
            this.btnPreviewClear.Click += new System.EventHandler(this.btnPreviewClear_Click);
            // 
            // udFirst
            // 
            this.udFirst.Location = new System.Drawing.Point(227, 70);
            this.udFirst.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udFirst.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udFirst.Name = "udFirst";
            this.udFirst.Size = new System.Drawing.Size(60, 20);
            this.udFirst.TabIndex = 8;
            this.udFirst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udFirst.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblFirst
            // 
            this.lblFirst.AutoSize = true;
            this.lblFirst.Location = new System.Drawing.Point(195, 72);
            this.lblFirst.Name = "lblFirst";
            this.lblFirst.Size = new System.Drawing.Size(26, 13);
            this.lblFirst.TabIndex = 9;
            this.lblFirst.Text = "First";
            // 
            // lblLast
            // 
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(297, 72);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(27, 13);
            this.lblLast.TabIndex = 11;
            this.lblLast.Text = "Last";
            // 
            // udLast
            // 
            this.udLast.Location = new System.Drawing.Point(330, 70);
            this.udLast.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udLast.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udLast.Name = "udLast";
            this.udLast.Size = new System.Drawing.Size(60, 20);
            this.udLast.TabIndex = 10;
            this.udLast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udLast.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // LOG_export
            // 
            this.ClientSize = new System.Drawing.Size(584, 475);
            this.Controls.Add(this.lblLast);
            this.Controls.Add(this.udLast);
            this.Controls.Add(this.lblFirst);
            this.Controls.Add(this.udFirst);
            this.Controls.Add(this.btnPreviewClear);
            this.Controls.Add(this.lblLOGPreview);
            this.Controls.Add(this.lblLOGFormat);
            this.Controls.Add(this.comboLOGformat);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLOGExport);
            this.Controls.Add(this.btnLOGSaveAs);
            this.Controls.Add(this.rtbLOGPreview);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 513);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(55, 513);
            this.Name = "LOG_export";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LOG_export";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LOGExport_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.udFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLast)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLOGPreview;
        private System.Windows.Forms.Button btnLOGSaveAs;
        private System.Windows.Forms.Button btnLOGExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboLOGformat;
        private System.Windows.Forms.Label lblLOGFormat;
        private System.Windows.Forms.Label lblLOGPreview;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnPreviewClear;
        private System.Windows.Forms.NumericUpDown udFirst;
        private System.Windows.Forms.Label lblFirst;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.NumericUpDown udLast;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}