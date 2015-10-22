using AnalogGAuge;
using System.Threading;
using System.Drawing;

namespace CWExpert
{
    partial class CWExpert
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

            Exit();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CWExpert));
            this.txtCall = new System.Windows.Forms.TextBox();
            this.btnSendCall = new System.Windows.Forms.Button();
            this.btnF1 = new System.Windows.Forms.Button();
            this.btnF2 = new System.Windows.Forms.Button();
            this.btnF3 = new System.Windows.Forms.Button();
            this.btnF4 = new System.Windows.Forms.Button();
            this.btnF5 = new System.Windows.Forms.Button();
            this.btnF6 = new System.Windows.Forms.Button();
            this.btnF8 = new System.Windows.Forms.Button();
            this.btnF7 = new System.Windows.Forms.Button();
            this.btnStartMR = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rTTYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PSKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bPSK31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bPSK63ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bPSK125ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bPSK250ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK63ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK125ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK250ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lOGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.kyboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.lOGBOOKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.recorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dXCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSendRST = new System.Windows.Forms.Button();
            this.txtRST = new System.Windows.Forms.TextBox();
            this.btnSendNr = new System.Windows.Forms.Button();
            this.txtNr = new System.Windows.Forms.TextBox();
            this.lblCall = new System.Windows.Forms.Label();
            this.lblRST = new System.Windows.Forms.Label();
            this.lblNr = new System.Windows.Forms.Label();
            this.txtChannel4 = new System.Windows.Forms.TextBox();
            this.btngrab = new System.Windows.Forms.Button();
            this.btnclr = new System.Windows.Forms.Button();
            this.txtChannel5 = new System.Windows.Forms.TextBox();
            this.txtChannel6 = new System.Windows.Forms.TextBox();
            this.txtChannel7 = new System.Windows.Forms.TextBox();
            this.txtChannel11 = new System.Windows.Forms.TextBox();
            this.txtChannel10 = new System.Windows.Forms.TextBox();
            this.txtChannel9 = new System.Windows.Forms.TextBox();
            this.txtChannel8 = new System.Windows.Forms.TextBox();
            this.txtChannel15 = new System.Windows.Forms.TextBox();
            this.txtChannel14 = new System.Windows.Forms.TextBox();
            this.txtChannel13 = new System.Windows.Forms.TextBox();
            this.txtChannel12 = new System.Windows.Forms.TextBox();
            this.txtChannel19 = new System.Windows.Forms.TextBox();
            this.txtChannel18 = new System.Windows.Forms.TextBox();
            this.txtChannel17 = new System.Windows.Forms.TextBox();
            this.txtChannel16 = new System.Windows.Forms.TextBox();
            this.txtChannel3 = new System.Windows.Forms.TextBox();
            this.txtChannel2 = new System.Windows.Forms.TextBox();
            this.txtChannel20 = new System.Windows.Forms.TextBox();
            this.picWaterfall = new System.Windows.Forms.PictureBox();
            this.picWaterfallcontextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.picMonitorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.waterfallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panadapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.rTTYToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.pSK31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pSK63ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pSK125ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pSK250ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.qPSK31ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK63ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK125ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.qPSK250ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.picPanadapter = new System.Windows.Forms.PictureBox();
            this.grpDisplay = new System.Windows.Forms.GroupBox();
            this.lblPan = new System.Windows.Forms.Button();
            this.lblZoom = new System.Windows.Forms.Button();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.tbPan = new System.Windows.Forms.TrackBar();
            this.lblFilterwidth = new System.Windows.Forms.Label();
            this.txtFilterWidth = new System.Windows.Forms.Label();
            this.tbFilterWidth = new System.Windows.Forms.TrackBar();
            this.txtFreq = new System.Windows.Forms.Label();
            this.grpLogBook = new System.Windows.Forms.GroupBox();
            this.btnLogDelete = new System.Windows.Forms.Button();
            this.btnLogPrev = new System.Windows.Forms.Button();
            this.btnLogFirst = new System.Windows.Forms.Button();
            this.btnLogLast = new System.Windows.Forms.Button();
            this.btnLogNext = new System.Windows.Forms.Button();
            this.txtLogSearch = new System.Windows.Forms.TextBox();
            this.lblLOGZone = new System.Windows.Forms.Label();
            this.txtLOGZone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLOGNR = new System.Windows.Forms.TextBox();
            this.lblLOGNR = new System.Windows.Forms.Label();
            this.udLOGMyNR = new System.Windows.Forms.NumericUpDown();
            this.lblLOC = new System.Windows.Forms.Label();
            this.txtLOGLOC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLogMyRST = new System.Windows.Forms.TextBox();
            this.grpMonitor = new System.Windows.Forms.GroupBox();
            this.chkAFC = new System.Windows.Forms.CheckBox();
            this.chkSQL = new System.Windows.Forms.CheckBox();
            this.lblPSKCH2Freq = new System.Windows.Forms.Label();
            this.lblPSKDCDCh2 = new System.Windows.Forms.Label();
            this.lblDCDCh2 = new System.Windows.Forms.Label();
            this.lblPSKCH1Freq = new System.Windows.Forms.Label();
            this.lblPSKDCDCh1 = new System.Windows.Forms.Label();
            this.lblDCDCh1 = new System.Windows.Forms.Label();
            this.picMonitor = new System.Windows.Forms.PictureBox();
            this.lblRTTYMark = new System.Windows.Forms.Label();
            this.lblRTTYSpace = new System.Windows.Forms.Label();
            this.radRTTYReverse = new System.Windows.Forms.RadioButton();
            this.radRTTYNormal = new System.Windows.Forms.RadioButton();
            this.lblRTTYSpaceBox = new System.Windows.Forms.Label();
            this.lblRTTYMarkBox = new System.Windows.Forms.Label();
            this.lblLogInfo = new System.Windows.Forms.Label();
            this.txtLogInfo = new System.Windows.Forms.TextBox();
            this.lblLogQTH = new System.Windows.Forms.Label();
            this.txtLogQTH = new System.Windows.Forms.TextBox();
            this.btnLOG6 = new System.Windows.Forms.Button();
            this.btnLOG5 = new System.Windows.Forms.Button();
            this.btnLOG4 = new System.Windows.Forms.Button();
            this.btnLOG3 = new System.Windows.Forms.Button();
            this.btnLOG2 = new System.Windows.Forms.Button();
            this.btnLOG1 = new System.Windows.Forms.Button();
            this.btnLogClear = new System.Windows.Forms.Button();
            this.btnLogSearch = new System.Windows.Forms.Button();
            this.btnLOGSave = new System.Windows.Forms.Button();
            this.lblLogRST = new System.Windows.Forms.Label();
            this.txtLogRST = new System.Windows.Forms.TextBox();
            this.lblLogName = new System.Windows.Forms.Label();
            this.txtLogName = new System.Windows.Forms.TextBox();
            this.lblLogCALL = new System.Windows.Forms.Label();
            this.txtLogCall = new System.Windows.Forms.TextBox();
            this.btnLOG = new System.Windows.Forms.Button();
            this.btnAudioMute = new System.Windows.Forms.CheckBox();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnNB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInputLevel = new System.Windows.Forms.TrackBar();
            this.grpMorseRunner = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMRSquelch = new System.Windows.Forms.TrackBar();
            this.grpGenesisRadio = new System.Windows.Forms.GroupBox();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.lblPWR = new System.Windows.Forms.Label();
            this.tbG59PWR = new System.Windows.Forms.TrackBar();
            this.picSQL = new System.Windows.Forms.PictureBox();
            this.lblSQL = new System.Windows.Forms.Label();
            this.tbSQL = new System.Windows.Forms.TrackBar();
            this.grpBand = new System.Windows.Forms.GroupBox();
            this.radBand160 = new System.Windows.Forms.RadioButton();
            this.radBand6 = new System.Windows.Forms.RadioButton();
            this.radBand80 = new System.Windows.Forms.RadioButton();
            this.radBand40 = new System.Windows.Forms.RadioButton();
            this.radBand10 = new System.Windows.Forms.RadioButton();
            this.radBand30 = new System.Windows.Forms.RadioButton();
            this.radBand20 = new System.Windows.Forms.RadioButton();
            this.radBand12 = new System.Windows.Forms.RadioButton();
            this.radBand17 = new System.Windows.Forms.RadioButton();
            this.radBand15 = new System.Windows.Forms.RadioButton();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.radFilterVar = new System.Windows.Forms.RadioButton();
            this.radFilter500 = new System.Windows.Forms.RadioButton();
            this.radFilter50 = new System.Windows.Forms.RadioButton();
            this.radFilter100 = new System.Windows.Forms.RadioButton();
            this.radFilter250 = new System.Windows.Forms.RadioButton();
            this.radFilter1K = new System.Windows.Forms.RadioButton();
            this.lblUSB = new System.Windows.Forms.Label();
            this.btnATT = new System.Windows.Forms.CheckBox();
            this.lblRFGain = new System.Windows.Forms.Label();
            this.tbRFGain = new System.Windows.Forms.TrackBar();
            this.btnAF = new System.Windows.Forms.CheckBox();
            this.lblAFGain = new System.Windows.Forms.Label();
            this.btnRF = new System.Windows.Forms.CheckBox();
            this.tbAFGain = new System.Windows.Forms.TrackBar();
            this.btnMute = new System.Windows.Forms.CheckBox();
            this.grpMorseRunner2 = new System.Windows.Forms.GroupBox();
            this.grpSMeter = new System.Windows.Forms.GroupBox();
            this.txtVFOB = new System.Windows.Forms.Label();
            this.SMeter = new AnalogGAuge.AGauge();
            this.contextMenuSMeter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.pWERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reflPWRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sWRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtVFOA = new System.Windows.Forms.Label();
            this.txtLosc = new System.Windows.Forms.Label();
            this.btnTX = new System.Windows.Forms.Button();
            this.grpChannels = new System.Windows.Forms.GroupBox();
            this.btnRX2On = new System.Windows.Forms.CheckBox();
            this.btnClearCH2 = new System.Windows.Forms.Button();
            this.btnClearCH1 = new System.Windows.Forms.Button();
            this.btnCH2 = new System.Windows.Forms.Button();
            this.btnCH1 = new System.Windows.Forms.Button();
            this.rtbCH2 = new System.Windows.Forms.RichTextBox();
            this.ch2_contextQSO = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ch2CALLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2RSTMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2NameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2QTHMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2LOCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2ZoneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2NRMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch2InfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbCH1 = new System.Windows.Forms.RichTextBox();
            this.ch1_contextQSO = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ch1_cALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch1_rSTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch1_nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch1_qTHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lOCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ch1_infoTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTune = new System.Windows.Forms.Button();
            this.grpMRChannels = new System.Windows.Forms.GroupBox();
            this.btnKeyboard = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaterfall)).BeginInit();
            this.picWaterfallcontextMenu.SuspendLayout();
            this.picMonitorContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPanadapter)).BeginInit();
            this.grpDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFilterWidth)).BeginInit();
            this.grpLogBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLOGMyNR)).BeginInit();
            this.grpMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInputLevel)).BeginInit();
            this.grpMorseRunner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMRSquelch)).BeginInit();
            this.grpGenesisRadio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbG59PWR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSQL)).BeginInit();
            this.grpBand.SuspendLayout();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRFGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAFGain)).BeginInit();
            this.grpMorseRunner2.SuspendLayout();
            this.grpSMeter.SuspendLayout();
            this.contextMenuSMeter.SuspendLayout();
            this.grpChannels.SuspendLayout();
            this.ch2_contextQSO.SuspendLayout();
            this.ch1_contextQSO.SuspendLayout();
            this.grpMRChannels.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCall
            // 
            this.txtCall.BackColor = System.Drawing.Color.White;
            this.txtCall.ForeColor = System.Drawing.Color.Black;
            this.txtCall.Location = new System.Drawing.Point(181, 35);
            this.txtCall.MaxLength = 64;
            this.txtCall.Name = "txtCall";
            this.txtCall.Size = new System.Drawing.Size(103, 20);
            this.txtCall.TabIndex = 24;
            this.txtCall.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCall_KeyUp);
            // 
            // btnSendCall
            // 
            this.btnSendCall.ForeColor = System.Drawing.Color.Black;
            this.btnSendCall.Location = new System.Drawing.Point(10, 14);
            this.btnSendCall.Name = "btnSendCall";
            this.btnSendCall.Size = new System.Drawing.Size(62, 23);
            this.btnSendCall.TabIndex = 1;
            this.btnSendCall.Text = "Send";
            this.btnSendCall.UseVisualStyleBackColor = true;
            this.btnSendCall.Click += new System.EventHandler(this.btnSendCall_Click);
            // 
            // btnF1
            // 
            this.btnF1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF1.ForeColor = System.Drawing.Color.Black;
            this.btnF1.Location = new System.Drawing.Point(5, 17);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(62, 23);
            this.btnF1.TabIndex = 2;
            this.btnF1.Text = "F1";
            this.btnF1.UseVisualStyleBackColor = false;
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // btnF2
            // 
            this.btnF2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF2.ForeColor = System.Drawing.Color.Black;
            this.btnF2.Location = new System.Drawing.Point(67, 17);
            this.btnF2.Name = "btnF2";
            this.btnF2.Size = new System.Drawing.Size(62, 23);
            this.btnF2.TabIndex = 3;
            this.btnF2.Text = "F2";
            this.btnF2.UseVisualStyleBackColor = false;
            this.btnF2.Click += new System.EventHandler(this.btnF2_Click);
            // 
            // btnF3
            // 
            this.btnF3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF3.ForeColor = System.Drawing.Color.Black;
            this.btnF3.Location = new System.Drawing.Point(129, 17);
            this.btnF3.Name = "btnF3";
            this.btnF3.Size = new System.Drawing.Size(62, 23);
            this.btnF3.TabIndex = 4;
            this.btnF3.Text = "F3";
            this.btnF3.UseVisualStyleBackColor = false;
            this.btnF3.Click += new System.EventHandler(this.btnF3_Click);
            // 
            // btnF4
            // 
            this.btnF4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF4.ForeColor = System.Drawing.Color.Black;
            this.btnF4.Location = new System.Drawing.Point(191, 17);
            this.btnF4.Name = "btnF4";
            this.btnF4.Size = new System.Drawing.Size(62, 23);
            this.btnF4.TabIndex = 5;
            this.btnF4.Text = "F4";
            this.btnF4.UseVisualStyleBackColor = false;
            this.btnF4.Click += new System.EventHandler(this.btnF4_Click);
            // 
            // btnF5
            // 
            this.btnF5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF5.ForeColor = System.Drawing.Color.Black;
            this.btnF5.Location = new System.Drawing.Point(5, 51);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(62, 23);
            this.btnF5.TabIndex = 6;
            this.btnF5.Text = "F5";
            this.btnF5.UseVisualStyleBackColor = false;
            this.btnF5.Click += new System.EventHandler(this.btnF5_Click);
            // 
            // btnF6
            // 
            this.btnF6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF6.ForeColor = System.Drawing.Color.Black;
            this.btnF6.Location = new System.Drawing.Point(67, 51);
            this.btnF6.Name = "btnF6";
            this.btnF6.Size = new System.Drawing.Size(62, 23);
            this.btnF6.TabIndex = 7;
            this.btnF6.Text = "F6";
            this.btnF6.UseVisualStyleBackColor = false;
            this.btnF6.Click += new System.EventHandler(this.btnF6_Click);
            // 
            // btnF8
            // 
            this.btnF8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF8.ForeColor = System.Drawing.Color.Black;
            this.btnF8.Location = new System.Drawing.Point(191, 51);
            this.btnF8.Name = "btnF8";
            this.btnF8.Size = new System.Drawing.Size(62, 23);
            this.btnF8.TabIndex = 9;
            this.btnF8.Text = "F8";
            this.btnF8.UseVisualStyleBackColor = false;
            this.btnF8.Click += new System.EventHandler(this.btnF8_Click);
            // 
            // btnF7
            // 
            this.btnF7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnF7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnF7.ForeColor = System.Drawing.Color.Black;
            this.btnF7.Location = new System.Drawing.Point(129, 51);
            this.btnF7.Name = "btnF7";
            this.btnF7.Size = new System.Drawing.Size(62, 23);
            this.btnF7.TabIndex = 8;
            this.btnF7.Text = "F7";
            this.btnF7.UseVisualStyleBackColor = false;
            this.btnF7.Click += new System.EventHandler(this.btnF7_Click);
            // 
            // btnStartMR
            // 
            this.btnStartMR.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnStartMR.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStartMR.ForeColor = System.Drawing.Color.Black;
            this.btnStartMR.Location = new System.Drawing.Point(9, 33);
            this.btnStartMR.Name = "btnStartMR";
            this.btnStartMR.Size = new System.Drawing.Size(49, 23);
            this.btnStartMR.TabIndex = 22;
            this.btnStartMR.Text = "Start ";
            this.btnStartMR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStartMR.UseVisualStyleBackColor = true;
            this.btnStartMR.CheckedChanged += new System.EventHandler(this.btnStartMR_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupMenuItem,
            this.modeToolStripMenuItem,
            this.lOGToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setupMenuItem
            // 
            this.setupMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setupMenuItem.Name = "setupMenuItem";
            this.setupMenuItem.Size = new System.Drawing.Size(55, 21);
            this.setupMenuItem.Text = "Setup";
            this.setupMenuItem.Click += new System.EventHandler(this.setupMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cWToolStripMenuItem,
            this.rTTYToolStripMenuItem,
            this.PSKToolStripMenuItem});
            this.modeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // cWToolStripMenuItem
            // 
            this.cWToolStripMenuItem.Name = "cWToolStripMenuItem";
            this.cWToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.cWToolStripMenuItem.Text = "CW";
            this.cWToolStripMenuItem.Click += new System.EventHandler(this.cWToolStripMenuItem_Click);
            // 
            // rTTYToolStripMenuItem
            // 
            this.rTTYToolStripMenuItem.Name = "rTTYToolStripMenuItem";
            this.rTTYToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.rTTYToolStripMenuItem.Text = "RTTY";
            this.rTTYToolStripMenuItem.Click += new System.EventHandler(this.rTTYToolStripMenuItem_Click);
            // 
            // PSKToolStripMenuItem
            // 
            this.PSKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bPSK31ToolStripMenuItem,
            this.bPSK63ToolStripMenuItem,
            this.bPSK125ToolStripMenuItem,
            this.bPSK250ToolStripMenuItem,
            this.qPSK31ToolStripMenuItem,
            this.qPSK63ToolStripMenuItem,
            this.qPSK125ToolStripMenuItem,
            this.qPSK250ToolStripMenuItem});
            this.PSKToolStripMenuItem.Name = "PSKToolStripMenuItem";
            this.PSKToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.PSKToolStripMenuItem.Text = "PSK";
            // 
            // bPSK31ToolStripMenuItem
            // 
            this.bPSK31ToolStripMenuItem.Name = "bPSK31ToolStripMenuItem";
            this.bPSK31ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bPSK31ToolStripMenuItem.Text = "BPSK31";
            this.bPSK31ToolStripMenuItem.Click += new System.EventHandler(this.bPSK31ToolStripMenuItem_Click);
            // 
            // bPSK63ToolStripMenuItem
            // 
            this.bPSK63ToolStripMenuItem.Name = "bPSK63ToolStripMenuItem";
            this.bPSK63ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bPSK63ToolStripMenuItem.Text = "BPSK63";
            this.bPSK63ToolStripMenuItem.Click += new System.EventHandler(this.bPSK63ToolStripMenuItem_Click);
            // 
            // bPSK125ToolStripMenuItem
            // 
            this.bPSK125ToolStripMenuItem.Name = "bPSK125ToolStripMenuItem";
            this.bPSK125ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bPSK125ToolStripMenuItem.Text = "BPSK125";
            this.bPSK125ToolStripMenuItem.Click += new System.EventHandler(this.bPSK125ToolStripMenuItem_Click);
            // 
            // bPSK250ToolStripMenuItem
            // 
            this.bPSK250ToolStripMenuItem.Name = "bPSK250ToolStripMenuItem";
            this.bPSK250ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bPSK250ToolStripMenuItem.Text = "BPSK250";
            this.bPSK250ToolStripMenuItem.Click += new System.EventHandler(this.bPSK250ToolStripMenuItem_Click);
            // 
            // qPSK31ToolStripMenuItem
            // 
            this.qPSK31ToolStripMenuItem.Name = "qPSK31ToolStripMenuItem";
            this.qPSK31ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.qPSK31ToolStripMenuItem.Text = "QPSK31";
            this.qPSK31ToolStripMenuItem.Click += new System.EventHandler(this.qPSK31ToolStripMenuItem_Click);
            // 
            // qPSK63ToolStripMenuItem
            // 
            this.qPSK63ToolStripMenuItem.Name = "qPSK63ToolStripMenuItem";
            this.qPSK63ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.qPSK63ToolStripMenuItem.Text = "QPSK63";
            this.qPSK63ToolStripMenuItem.Click += new System.EventHandler(this.qPSK63ToolStripMenuItem_Click);
            // 
            // qPSK125ToolStripMenuItem
            // 
            this.qPSK125ToolStripMenuItem.Name = "qPSK125ToolStripMenuItem";
            this.qPSK125ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.qPSK125ToolStripMenuItem.Text = "QPSK125";
            this.qPSK125ToolStripMenuItem.Click += new System.EventHandler(this.qPSK125ToolStripMenuItem_Click);
            // 
            // qPSK250ToolStripMenuItem
            // 
            this.qPSK250ToolStripMenuItem.Name = "qPSK250ToolStripMenuItem";
            this.qPSK250ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.qPSK250ToolStripMenuItem.Text = "QPSK250";
            this.qPSK250ToolStripMenuItem.Click += new System.EventHandler(this.qPSK250ToolStripMenuItem_Click);
            // 
            // lOGToolStripMenuItem
            // 
            this.lOGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator11,
            this.kyboardToolStripMenuItem,
            this.toolStripSeparator9,
            this.lOGBOOKToolStripMenuItem,
            this.toolStripSeparator10,
            this.recorderToolStripMenuItem,
            this.dXCCToolStripMenuItem});
            this.lOGToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lOGToolStripMenuItem.Name = "lOGToolStripMenuItem";
            this.lOGToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this.lOGToolStripMenuItem.Text = "Options";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItem1.Text = "DX Cluster";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.dXClusterToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(139, 6);
            // 
            // kyboardToolStripMenuItem
            // 
            this.kyboardToolStripMenuItem.Name = "kyboardToolStripMenuItem";
            this.kyboardToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.kyboardToolStripMenuItem.Text = "Keyboard";
            this.kyboardToolStripMenuItem.Click += new System.EventHandler(this.KeyboardToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(139, 6);
            // 
            // lOGBOOKToolStripMenuItem
            // 
            this.lOGBOOKToolStripMenuItem.Name = "lOGBOOKToolStripMenuItem";
            this.lOGBOOKToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.lOGBOOKToolStripMenuItem.Text = "LOG BOOK";
            this.lOGBOOKToolStripMenuItem.Click += new System.EventHandler(this.lOGToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(139, 6);
            // 
            // recorderToolStripMenuItem
            // 
            this.recorderToolStripMenuItem.Name = "recorderToolStripMenuItem";
            this.recorderToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.recorderToolStripMenuItem.Text = "Recorder";
            this.recorderToolStripMenuItem.Click += new System.EventHandler(this.recorderToolStripMenuItem_Click);
            // 
            // dXCCToolStripMenuItem
            // 
            this.dXCCToolStripMenuItem.Name = "dXCCToolStripMenuItem";
            this.dXCCToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.dXCCToolStripMenuItem.Text = "DXCC";
            this.dXCCToolStripMenuItem.Click += new System.EventHandler(this.dXCCToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(43, 21);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnSendRST
            // 
            this.btnSendRST.ForeColor = System.Drawing.Color.Black;
            this.btnSendRST.Location = new System.Drawing.Point(10, 154);
            this.btnSendRST.Name = "btnSendRST";
            this.btnSendRST.Size = new System.Drawing.Size(62, 23);
            this.btnSendRST.TabIndex = 14;
            this.btnSendRST.Text = "Send";
            this.btnSendRST.UseVisualStyleBackColor = true;
            this.btnSendRST.Click += new System.EventHandler(this.btnSendRST_Click);
            // 
            // txtRST
            // 
            this.txtRST.BackColor = System.Drawing.Color.White;
            this.txtRST.ForeColor = System.Drawing.Color.Black;
            this.txtRST.Location = new System.Drawing.Point(10, 128);
            this.txtRST.MaxLength = 3;
            this.txtRST.Name = "txtRST";
            this.txtRST.Size = new System.Drawing.Size(62, 20);
            this.txtRST.TabIndex = 13;
            this.txtRST.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRST_KeyUp);
            // 
            // btnSendNr
            // 
            this.btnSendNr.ForeColor = System.Drawing.Color.Black;
            this.btnSendNr.Location = new System.Drawing.Point(10, 185);
            this.btnSendNr.Name = "btnSendNr";
            this.btnSendNr.Size = new System.Drawing.Size(62, 23);
            this.btnSendNr.TabIndex = 16;
            this.btnSendNr.Text = "Send";
            this.btnSendNr.UseVisualStyleBackColor = true;
            this.btnSendNr.Click += new System.EventHandler(this.btnSendNr_Click);
            // 
            // txtNr
            // 
            this.txtNr.BackColor = System.Drawing.Color.White;
            this.txtNr.ForeColor = System.Drawing.Color.Black;
            this.txtNr.Location = new System.Drawing.Point(10, 214);
            this.txtNr.MaxLength = 5;
            this.txtNr.Name = "txtNr";
            this.txtNr.Size = new System.Drawing.Size(62, 20);
            this.txtNr.TabIndex = 15;
            this.txtNr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNr_KeyUp);
            // 
            // lblCall
            // 
            this.lblCall.AutoSize = true;
            this.lblCall.ForeColor = System.Drawing.Color.White;
            this.lblCall.Location = new System.Drawing.Point(145, 39);
            this.lblCall.Name = "lblCall";
            this.lblCall.Size = new System.Drawing.Size(27, 13);
            this.lblCall.TabIndex = 17;
            this.lblCall.Text = "Msg";
            // 
            // lblRST
            // 
            this.lblRST.AutoSize = true;
            this.lblRST.Location = new System.Drawing.Point(26, 112);
            this.lblRST.Name = "lblRST";
            this.lblRST.Size = new System.Drawing.Size(29, 13);
            this.lblRST.TabIndex = 18;
            this.lblRST.Text = "RST";
            // 
            // lblNr
            // 
            this.lblNr.AutoSize = true;
            this.lblNr.Location = new System.Drawing.Point(26, 237);
            this.lblNr.Name = "lblNr";
            this.lblNr.Size = new System.Drawing.Size(30, 13);
            this.lblNr.TabIndex = 19;
            this.lblNr.Text = "NBR";
            // 
            // txtChannel4
            // 
            this.txtChannel4.Location = new System.Drawing.Point(14, 55);
            this.txtChannel4.MaxLength = 128;
            this.txtChannel4.Name = "txtChannel4";
            this.txtChannel4.ReadOnly = true;
            this.txtChannel4.Size = new System.Drawing.Size(251, 20);
            this.txtChannel4.TabIndex = 22;
            // 
            // btngrab
            // 
            this.btngrab.ForeColor = System.Drawing.Color.Black;
            this.btngrab.Location = new System.Drawing.Point(10, 49);
            this.btngrab.Name = "btngrab";
            this.btngrab.Size = new System.Drawing.Size(62, 23);
            this.btngrab.TabIndex = 23;
            this.btngrab.Text = "Grab";
            this.btngrab.UseVisualStyleBackColor = true;
            this.btngrab.Click += new System.EventHandler(this.btngrab_Click);
            // 
            // btnclr
            // 
            this.btnclr.ForeColor = System.Drawing.Color.Black;
            this.btnclr.Location = new System.Drawing.Point(10, 84);
            this.btnclr.Name = "btnclr";
            this.btnclr.Size = new System.Drawing.Size(62, 23);
            this.btnclr.TabIndex = 24;
            this.btnclr.Text = "Clear";
            this.btnclr.UseVisualStyleBackColor = true;
            this.btnclr.Click += new System.EventHandler(this.btnclr_Click);
            // 
            // txtChannel5
            // 
            this.txtChannel5.Location = new System.Drawing.Point(14, 75);
            this.txtChannel5.MaxLength = 128;
            this.txtChannel5.Name = "txtChannel5";
            this.txtChannel5.ReadOnly = true;
            this.txtChannel5.Size = new System.Drawing.Size(251, 20);
            this.txtChannel5.TabIndex = 25;
            // 
            // txtChannel6
            // 
            this.txtChannel6.Location = new System.Drawing.Point(14, 95);
            this.txtChannel6.MaxLength = 128;
            this.txtChannel6.Name = "txtChannel6";
            this.txtChannel6.ReadOnly = true;
            this.txtChannel6.Size = new System.Drawing.Size(251, 20);
            this.txtChannel6.TabIndex = 26;
            // 
            // txtChannel7
            // 
            this.txtChannel7.Location = new System.Drawing.Point(14, 115);
            this.txtChannel7.MaxLength = 128;
            this.txtChannel7.Name = "txtChannel7";
            this.txtChannel7.ReadOnly = true;
            this.txtChannel7.Size = new System.Drawing.Size(251, 20);
            this.txtChannel7.TabIndex = 27;
            // 
            // txtChannel11
            // 
            this.txtChannel11.Location = new System.Drawing.Point(14, 195);
            this.txtChannel11.MaxLength = 128;
            this.txtChannel11.Name = "txtChannel11";
            this.txtChannel11.ReadOnly = true;
            this.txtChannel11.Size = new System.Drawing.Size(251, 20);
            this.txtChannel11.TabIndex = 31;
            // 
            // txtChannel10
            // 
            this.txtChannel10.Location = new System.Drawing.Point(14, 175);
            this.txtChannel10.MaxLength = 128;
            this.txtChannel10.Name = "txtChannel10";
            this.txtChannel10.ReadOnly = true;
            this.txtChannel10.Size = new System.Drawing.Size(251, 20);
            this.txtChannel10.TabIndex = 30;
            // 
            // txtChannel9
            // 
            this.txtChannel9.Location = new System.Drawing.Point(14, 155);
            this.txtChannel9.MaxLength = 128;
            this.txtChannel9.Name = "txtChannel9";
            this.txtChannel9.ReadOnly = true;
            this.txtChannel9.Size = new System.Drawing.Size(251, 20);
            this.txtChannel9.TabIndex = 29;
            // 
            // txtChannel8
            // 
            this.txtChannel8.Location = new System.Drawing.Point(14, 135);
            this.txtChannel8.MaxLength = 128;
            this.txtChannel8.Name = "txtChannel8";
            this.txtChannel8.ReadOnly = true;
            this.txtChannel8.Size = new System.Drawing.Size(251, 20);
            this.txtChannel8.TabIndex = 28;
            // 
            // txtChannel15
            // 
            this.txtChannel15.Location = new System.Drawing.Point(14, 275);
            this.txtChannel15.MaxLength = 128;
            this.txtChannel15.Name = "txtChannel15";
            this.txtChannel15.ReadOnly = true;
            this.txtChannel15.Size = new System.Drawing.Size(251, 20);
            this.txtChannel15.TabIndex = 35;
            // 
            // txtChannel14
            // 
            this.txtChannel14.Location = new System.Drawing.Point(14, 255);
            this.txtChannel14.MaxLength = 128;
            this.txtChannel14.Name = "txtChannel14";
            this.txtChannel14.ReadOnly = true;
            this.txtChannel14.Size = new System.Drawing.Size(251, 20);
            this.txtChannel14.TabIndex = 34;
            // 
            // txtChannel13
            // 
            this.txtChannel13.Location = new System.Drawing.Point(14, 235);
            this.txtChannel13.MaxLength = 128;
            this.txtChannel13.Name = "txtChannel13";
            this.txtChannel13.ReadOnly = true;
            this.txtChannel13.Size = new System.Drawing.Size(251, 20);
            this.txtChannel13.TabIndex = 33;
            // 
            // txtChannel12
            // 
            this.txtChannel12.Location = new System.Drawing.Point(14, 215);
            this.txtChannel12.MaxLength = 128;
            this.txtChannel12.Name = "txtChannel12";
            this.txtChannel12.ReadOnly = true;
            this.txtChannel12.Size = new System.Drawing.Size(251, 20);
            this.txtChannel12.TabIndex = 32;
            // 
            // txtChannel19
            // 
            this.txtChannel19.Location = new System.Drawing.Point(14, 355);
            this.txtChannel19.MaxLength = 128;
            this.txtChannel19.Name = "txtChannel19";
            this.txtChannel19.ReadOnly = true;
            this.txtChannel19.Size = new System.Drawing.Size(251, 20);
            this.txtChannel19.TabIndex = 39;
            // 
            // txtChannel18
            // 
            this.txtChannel18.Location = new System.Drawing.Point(14, 335);
            this.txtChannel18.MaxLength = 128;
            this.txtChannel18.Name = "txtChannel18";
            this.txtChannel18.ReadOnly = true;
            this.txtChannel18.Size = new System.Drawing.Size(251, 20);
            this.txtChannel18.TabIndex = 38;
            // 
            // txtChannel17
            // 
            this.txtChannel17.Location = new System.Drawing.Point(14, 315);
            this.txtChannel17.MaxLength = 128;
            this.txtChannel17.Name = "txtChannel17";
            this.txtChannel17.ReadOnly = true;
            this.txtChannel17.Size = new System.Drawing.Size(251, 20);
            this.txtChannel17.TabIndex = 37;
            // 
            // txtChannel16
            // 
            this.txtChannel16.Location = new System.Drawing.Point(14, 295);
            this.txtChannel16.MaxLength = 128;
            this.txtChannel16.Name = "txtChannel16";
            this.txtChannel16.ReadOnly = true;
            this.txtChannel16.Size = new System.Drawing.Size(251, 20);
            this.txtChannel16.TabIndex = 36;
            // 
            // txtChannel3
            // 
            this.txtChannel3.Location = new System.Drawing.Point(14, 35);
            this.txtChannel3.MaxLength = 128;
            this.txtChannel3.Name = "txtChannel3";
            this.txtChannel3.ReadOnly = true;
            this.txtChannel3.Size = new System.Drawing.Size(251, 20);
            this.txtChannel3.TabIndex = 40;
            // 
            // txtChannel2
            // 
            this.txtChannel2.Location = new System.Drawing.Point(14, 15);
            this.txtChannel2.MaxLength = 128;
            this.txtChannel2.Name = "txtChannel2";
            this.txtChannel2.ReadOnly = true;
            this.txtChannel2.Size = new System.Drawing.Size(251, 20);
            this.txtChannel2.TabIndex = 41;
            // 
            // txtChannel20
            // 
            this.txtChannel20.Location = new System.Drawing.Point(14, 375);
            this.txtChannel20.MaxLength = 128;
            this.txtChannel20.Name = "txtChannel20";
            this.txtChannel20.ReadOnly = true;
            this.txtChannel20.Size = new System.Drawing.Size(251, 20);
            this.txtChannel20.TabIndex = 42;
            // 
            // picWaterfall
            // 
            this.picWaterfall.BackColor = System.Drawing.Color.Black;
            this.picWaterfall.ContextMenuStrip = this.picWaterfallcontextMenu;
            this.picWaterfall.Location = new System.Drawing.Point(18, 25);
            this.picWaterfall.Name = "picWaterfall";
            this.picWaterfall.Size = new System.Drawing.Size(576, 240);
            this.picWaterfall.TabIndex = 43;
            this.picWaterfall.TabStop = false;
            this.picWaterfall.MouseLeave += new System.EventHandler(this.picWaterfall_MouseLeave);
            this.picWaterfall.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picWaterfall_MouseMove);
            this.picWaterfall.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picWaterfall_MouseDown);
            this.picWaterfall.Paint += new System.Windows.Forms.PaintEventHandler(this.picWaterfall_Paint);
            this.picWaterfall.MouseEnter += new System.EventHandler(this.picWaterfall_MouseEnter);
            // 
            // picWaterfallcontextMenu
            // 
            this.picWaterfallcontextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripSeparator7,
            this.toolStripMenuItem6,
            this.toolStripSeparator1,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripSeparator2,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14});
            this.picWaterfallcontextMenu.Name = "picMonitorContextMenu";
            this.picWaterfallcontextMenu.Size = new System.Drawing.Size(122, 242);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem5.Text = "‎CW";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.cWToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(118, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem6.Text = "RTTY";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.rTTYToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem7.Text = "BPSK31";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.bPSK31ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem8.Text = "BPSK63";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.bPSK63ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem9.Text = "BPSK125";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.bPSK125ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem10.Text = "BPSK250";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.bPSK250ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(118, 6);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem11.Text = "QPSK31";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.qPSK31ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem12.Text = "QPSK63";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.qPSK63ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem13.Text = "QPSK125";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.qPSK125ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(121, 22);
            this.toolStripMenuItem14.Text = "QPSK250";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.qPSK250ToolStripMenuItem_Click);
            // 
            // picMonitorContextMenu
            // 
            this.picMonitorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waterfallToolStripMenuItem,
            this.scopeToolStripMenuItem,
            this.panadapterToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem2,
            this.toolStripSeparator6,
            this.rTTYToolStripMenuItem1,
            this.toolStripSeparator5,
            this.pSK31ToolStripMenuItem,
            this.pSK63ToolStripMenuItem,
            this.pSK125ToolStripMenuItem,
            this.pSK250ToolStripMenuItem,
            this.toolStripSeparator4,
            this.qPSK31ToolStripMenuItem1,
            this.qPSK63ToolStripMenuItem1,
            this.qPSK125ToolStripMenuItem1,
            this.qPSK250ToolStripMenuItem1});
            this.picMonitorContextMenu.Name = "picMonitorContextMenu";
            this.picMonitorContextMenu.Size = new System.Drawing.Size(135, 314);
            // 
            // waterfallToolStripMenuItem
            // 
            this.waterfallToolStripMenuItem.Name = "waterfallToolStripMenuItem";
            this.waterfallToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.waterfallToolStripMenuItem.Text = "Waterfall";
            this.waterfallToolStripMenuItem.Click += new System.EventHandler(this.waterfallToolStripMenuItem_Click);
            // 
            // scopeToolStripMenuItem
            // 
            this.scopeToolStripMenuItem.Name = "scopeToolStripMenuItem";
            this.scopeToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.scopeToolStripMenuItem.Text = "Scope";
            this.scopeToolStripMenuItem.Click += new System.EventHandler(this.scopeToolStripMenuItem_Click);
            // 
            // panadapterToolStripMenuItem
            // 
            this.panadapterToolStripMenuItem.Name = "panadapterToolStripMenuItem";
            this.panadapterToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.panadapterToolStripMenuItem.Text = "Panadapter";
            this.panadapterToolStripMenuItem.Click += new System.EventHandler(this.panadapterToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(131, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem2.Text = "‎CW";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.cWToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(131, 6);
            // 
            // rTTYToolStripMenuItem1
            // 
            this.rTTYToolStripMenuItem1.Name = "rTTYToolStripMenuItem1";
            this.rTTYToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.rTTYToolStripMenuItem1.Text = "RTTY";
            this.rTTYToolStripMenuItem1.Click += new System.EventHandler(this.rTTYToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(131, 6);
            // 
            // pSK31ToolStripMenuItem
            // 
            this.pSK31ToolStripMenuItem.Name = "pSK31ToolStripMenuItem";
            this.pSK31ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.pSK31ToolStripMenuItem.Text = "BPSK31";
            this.pSK31ToolStripMenuItem.Click += new System.EventHandler(this.bPSK31ToolStripMenuItem_Click);
            // 
            // pSK63ToolStripMenuItem
            // 
            this.pSK63ToolStripMenuItem.Name = "pSK63ToolStripMenuItem";
            this.pSK63ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.pSK63ToolStripMenuItem.Text = "BPSK63";
            this.pSK63ToolStripMenuItem.Click += new System.EventHandler(this.bPSK63ToolStripMenuItem_Click);
            // 
            // pSK125ToolStripMenuItem
            // 
            this.pSK125ToolStripMenuItem.Name = "pSK125ToolStripMenuItem";
            this.pSK125ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.pSK125ToolStripMenuItem.Text = "BPSK125";
            this.pSK125ToolStripMenuItem.Click += new System.EventHandler(this.bPSK125ToolStripMenuItem_Click);
            // 
            // pSK250ToolStripMenuItem
            // 
            this.pSK250ToolStripMenuItem.Name = "pSK250ToolStripMenuItem";
            this.pSK250ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.pSK250ToolStripMenuItem.Text = "BPSK250";
            this.pSK250ToolStripMenuItem.Click += new System.EventHandler(this.bPSK250ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(131, 6);
            // 
            // qPSK31ToolStripMenuItem1
            // 
            this.qPSK31ToolStripMenuItem1.Name = "qPSK31ToolStripMenuItem1";
            this.qPSK31ToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.qPSK31ToolStripMenuItem1.Text = "QPSK31";
            this.qPSK31ToolStripMenuItem1.Click += new System.EventHandler(this.qPSK31ToolStripMenuItem_Click);
            // 
            // qPSK63ToolStripMenuItem1
            // 
            this.qPSK63ToolStripMenuItem1.Name = "qPSK63ToolStripMenuItem1";
            this.qPSK63ToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.qPSK63ToolStripMenuItem1.Text = "QPSK63";
            this.qPSK63ToolStripMenuItem1.Click += new System.EventHandler(this.qPSK63ToolStripMenuItem_Click);
            // 
            // qPSK125ToolStripMenuItem1
            // 
            this.qPSK125ToolStripMenuItem1.Name = "qPSK125ToolStripMenuItem1";
            this.qPSK125ToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.qPSK125ToolStripMenuItem1.Text = "QPSK125";
            this.qPSK125ToolStripMenuItem1.Click += new System.EventHandler(this.qPSK125ToolStripMenuItem_Click);
            // 
            // qPSK250ToolStripMenuItem1
            // 
            this.qPSK250ToolStripMenuItem1.Name = "qPSK250ToolStripMenuItem1";
            this.qPSK250ToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.qPSK250ToolStripMenuItem1.Text = "QPSK250";
            this.qPSK250ToolStripMenuItem1.Click += new System.EventHandler(this.qPSK250ToolStripMenuItem_Click);
            // 
            // picPanadapter
            // 
            this.picPanadapter.BackColor = System.Drawing.Color.Black;
            this.picPanadapter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPanadapter.Location = new System.Drawing.Point(18, 269);
            this.picPanadapter.Name = "picPanadapter";
            this.picPanadapter.Size = new System.Drawing.Size(576, 240);
            this.picPanadapter.TabIndex = 44;
            this.picPanadapter.TabStop = false;
            this.picPanadapter.MouseLeave += new System.EventHandler(this.picPanadapter_MouseLeave);
            this.picPanadapter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPanadapter_MouseMove);
            this.picPanadapter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPanadapter_MouseDown);
            this.picPanadapter.Paint += new System.Windows.Forms.PaintEventHandler(this.picPanadapter_Paint);
            this.picPanadapter.MouseEnter += new System.EventHandler(this.picPanadapter_MouseEnter);
            // 
            // grpDisplay
            // 
            this.grpDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpDisplay.BackColor = System.Drawing.Color.Black;
            this.grpDisplay.Controls.Add(this.lblPan);
            this.grpDisplay.Controls.Add(this.lblZoom);
            this.grpDisplay.Controls.Add(this.tbZoom);
            this.grpDisplay.Controls.Add(this.tbPan);
            this.grpDisplay.Controls.Add(this.lblFilterwidth);
            this.grpDisplay.Controls.Add(this.txtFilterWidth);
            this.grpDisplay.Controls.Add(this.tbFilterWidth);
            this.grpDisplay.Controls.Add(this.txtFreq);
            this.grpDisplay.Controls.Add(this.picPanadapter);
            this.grpDisplay.Controls.Add(this.grpLogBook);
            this.grpDisplay.Controls.Add(this.picWaterfall);
            this.grpDisplay.ForeColor = System.Drawing.Color.White;
            this.grpDisplay.Location = new System.Drawing.Point(384, 12);
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.Size = new System.Drawing.Size(612, 541);
            this.grpDisplay.TabIndex = 45;
            this.grpDisplay.TabStop = false;
            this.grpDisplay.Text = "Display";
            // 
            // lblPan
            // 
            this.lblPan.BackColor = System.Drawing.Color.Black;
            this.lblPan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblPan.ForeColor = System.Drawing.Color.White;
            this.lblPan.Location = new System.Drawing.Point(470, 514);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(40, 20);
            this.lblPan.TabIndex = 58;
            this.lblPan.Text = "Pan";
            this.lblPan.UseVisualStyleBackColor = false;
            this.lblPan.Click += new System.EventHandler(this.lblPan_Click);
            // 
            // lblZoom
            // 
            this.lblZoom.BackColor = System.Drawing.Color.Black;
            this.lblZoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblZoom.ForeColor = System.Drawing.Color.White;
            this.lblZoom.Location = new System.Drawing.Point(329, 513);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(44, 20);
            this.lblZoom.TabIndex = 57;
            this.lblZoom.Text = "Zoom";
            this.lblZoom.UseVisualStyleBackColor = false;
            this.lblZoom.Click += new System.EventHandler(this.lblZoom_Click);
            // 
            // tbZoom
            // 
            this.tbZoom.AutoSize = false;
            this.tbZoom.LargeChange = 4;
            this.tbZoom.Location = new System.Drawing.Point(373, 516);
            this.tbZoom.Maximum = 128;
            this.tbZoom.Minimum = 4;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbZoom.Size = new System.Drawing.Size(98, 19);
            this.tbZoom.SmallChange = 2;
            this.tbZoom.TabIndex = 57;
            this.tbZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbZoom.Value = 4;
            this.tbZoom.MouseLeave += new System.EventHandler(this.tbZoom_MouseLeave);
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            this.tbZoom.MouseHover += new System.EventHandler(this.tbZoom_MouseHover);
            // 
            // tbPan
            // 
            this.tbPan.AutoSize = false;
            this.tbPan.Location = new System.Drawing.Point(501, 516);
            this.tbPan.Maximum = 1000;
            this.tbPan.Minimum = -1000;
            this.tbPan.Name = "tbPan";
            this.tbPan.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbPan.Size = new System.Drawing.Size(98, 19);
            this.tbPan.TabIndex = 58;
            this.tbPan.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbPan.MouseLeave += new System.EventHandler(this.tbPan_MouseLeave);
            this.tbPan.Scroll += new System.EventHandler(this.tbPan_Scroll);
            this.tbPan.MouseHover += new System.EventHandler(this.tbPan_MouseHover);
            // 
            // lblFilterwidth
            // 
            this.lblFilterwidth.AutoSize = true;
            this.lblFilterwidth.BackColor = System.Drawing.Color.Black;
            this.lblFilterwidth.Location = new System.Drawing.Point(14, 516);
            this.lblFilterwidth.Name = "lblFilterwidth";
            this.lblFilterwidth.Size = new System.Drawing.Size(57, 13);
            this.lblFilterwidth.TabIndex = 54;
            this.lblFilterwidth.Text = "Filter width";
            // 
            // txtFilterWidth
            // 
            this.txtFilterWidth.BackColor = System.Drawing.Color.Black;
            this.txtFilterWidth.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtFilterWidth.Location = new System.Drawing.Point(73, 516);
            this.txtFilterWidth.Name = "txtFilterWidth";
            this.txtFilterWidth.Size = new System.Drawing.Size(60, 13);
            this.txtFilterWidth.TabIndex = 53;
            this.txtFilterWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbFilterWidth
            // 
            this.tbFilterWidth.AutoSize = false;
            this.tbFilterWidth.Location = new System.Drawing.Point(135, 516);
            this.tbFilterWidth.Maximum = 5000;
            this.tbFilterWidth.Minimum = 31;
            this.tbFilterWidth.Name = "tbFilterWidth";
            this.tbFilterWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbFilterWidth.Size = new System.Drawing.Size(98, 19);
            this.tbFilterWidth.TabIndex = 56;
            this.tbFilterWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbFilterWidth.Value = 50;
            this.tbFilterWidth.MouseLeave += new System.EventHandler(this.tbFilterWidth_MouseLeave);
            this.tbFilterWidth.ValueChanged += new System.EventHandler(this.tbFilterWidth_Scroll);
            this.tbFilterWidth.Scroll += new System.EventHandler(this.tbFilterWidth_Scroll);
            this.tbFilterWidth.MouseHover += new System.EventHandler(this.tbFilterWidth_MouseHover);
            // 
            // txtFreq
            // 
            this.txtFreq.BackColor = System.Drawing.Color.Black;
            this.txtFreq.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtFreq.Location = new System.Drawing.Point(235, 518);
            this.txtFreq.Name = "txtFreq";
            this.txtFreq.Size = new System.Drawing.Size(100, 13);
            this.txtFreq.TabIndex = 45;
            // 
            // grpLogBook
            // 
            this.grpLogBook.Controls.Add(this.btnLogDelete);
            this.grpLogBook.Controls.Add(this.btnLogPrev);
            this.grpLogBook.Controls.Add(this.btnLogFirst);
            this.grpLogBook.Controls.Add(this.btnLogLast);
            this.grpLogBook.Controls.Add(this.btnLogNext);
            this.grpLogBook.Controls.Add(this.txtLogSearch);
            this.grpLogBook.Controls.Add(this.lblLOGZone);
            this.grpLogBook.Controls.Add(this.txtLOGZone);
            this.grpLogBook.Controls.Add(this.label1);
            this.grpLogBook.Controls.Add(this.txtLOGNR);
            this.grpLogBook.Controls.Add(this.lblLOGNR);
            this.grpLogBook.Controls.Add(this.udLOGMyNR);
            this.grpLogBook.Controls.Add(this.lblLOC);
            this.grpLogBook.Controls.Add(this.txtLOGLOC);
            this.grpLogBook.Controls.Add(this.label7);
            this.grpLogBook.Controls.Add(this.txtLogMyRST);
            this.grpLogBook.Controls.Add(this.grpMonitor);
            this.grpLogBook.Controls.Add(this.lblLogInfo);
            this.grpLogBook.Controls.Add(this.txtLogInfo);
            this.grpLogBook.Controls.Add(this.lblLogQTH);
            this.grpLogBook.Controls.Add(this.txtLogQTH);
            this.grpLogBook.Controls.Add(this.btnLOG6);
            this.grpLogBook.Controls.Add(this.btnLOG5);
            this.grpLogBook.Controls.Add(this.btnLOG4);
            this.grpLogBook.Controls.Add(this.btnLOG3);
            this.grpLogBook.Controls.Add(this.btnLOG2);
            this.grpLogBook.Controls.Add(this.btnLOG1);
            this.grpLogBook.Controls.Add(this.btnLogClear);
            this.grpLogBook.Controls.Add(this.btnLogSearch);
            this.grpLogBook.Controls.Add(this.btnLOGSave);
            this.grpLogBook.Controls.Add(this.lblLogRST);
            this.grpLogBook.Controls.Add(this.txtLogRST);
            this.grpLogBook.Controls.Add(this.lblLogName);
            this.grpLogBook.Controls.Add(this.txtLogName);
            this.grpLogBook.Controls.Add(this.lblLogCALL);
            this.grpLogBook.Controls.Add(this.txtLogCall);
            this.grpLogBook.ForeColor = System.Drawing.Color.White;
            this.grpLogBook.Location = new System.Drawing.Point(18, 12);
            this.grpLogBook.Name = "grpLogBook";
            this.grpLogBook.Size = new System.Drawing.Size(576, 255);
            this.grpLogBook.TabIndex = 60;
            this.grpLogBook.TabStop = false;
            this.grpLogBook.Text = "QSO LOG";
            // 
            // btnLogDelete
            // 
            this.btnLogDelete.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogDelete.ForeColor = System.Drawing.Color.Black;
            this.btnLogDelete.Location = new System.Drawing.Point(195, 220);
            this.btnLogDelete.Name = "btnLogDelete";
            this.btnLogDelete.Size = new System.Drawing.Size(20, 23);
            this.btnLogDelete.TabIndex = 97;
            this.btnLogDelete.Text = "-";
            this.btnLogDelete.UseVisualStyleBackColor = true;
            this.btnLogDelete.Click += new System.EventHandler(this.btnLogDelete_Click);
            // 
            // btnLogPrev
            // 
            this.btnLogPrev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogPrev.ForeColor = System.Drawing.Color.Black;
            this.btnLogPrev.Location = new System.Drawing.Point(172, 220);
            this.btnLogPrev.Name = "btnLogPrev";
            this.btnLogPrev.Size = new System.Drawing.Size(20, 23);
            this.btnLogPrev.TabIndex = 96;
            this.btnLogPrev.Text = "<";
            this.btnLogPrev.UseVisualStyleBackColor = true;
            this.btnLogPrev.Click += new System.EventHandler(this.btnLogPrev_Click);
            // 
            // btnLogFirst
            // 
            this.btnLogFirst.AutoSize = true;
            this.btnLogFirst.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogFirst.ForeColor = System.Drawing.Color.Black;
            this.btnLogFirst.Location = new System.Drawing.Point(140, 220);
            this.btnLogFirst.Name = "btnLogFirst";
            this.btnLogFirst.Size = new System.Drawing.Size(29, 23);
            this.btnLogFirst.TabIndex = 95;
            this.btnLogFirst.Text = "<<";
            this.btnLogFirst.UseVisualStyleBackColor = true;
            this.btnLogFirst.Click += new System.EventHandler(this.btnLogFirst_Click);
            // 
            // btnLogLast
            // 
            this.btnLogLast.AutoSize = true;
            this.btnLogLast.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogLast.ForeColor = System.Drawing.Color.Black;
            this.btnLogLast.Location = new System.Drawing.Point(241, 220);
            this.btnLogLast.Name = "btnLogLast";
            this.btnLogLast.Size = new System.Drawing.Size(29, 23);
            this.btnLogLast.TabIndex = 94;
            this.btnLogLast.Text = ">>";
            this.btnLogLast.UseVisualStyleBackColor = true;
            this.btnLogLast.Click += new System.EventHandler(this.btnLogLast_Click);
            // 
            // btnLogNext
            // 
            this.btnLogNext.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogNext.ForeColor = System.Drawing.Color.Black;
            this.btnLogNext.Location = new System.Drawing.Point(218, 220);
            this.btnLogNext.Name = "btnLogNext";
            this.btnLogNext.Size = new System.Drawing.Size(20, 23);
            this.btnLogNext.TabIndex = 93;
            this.btnLogNext.Text = ">";
            this.btnLogNext.UseVisualStyleBackColor = true;
            this.btnLogNext.Click += new System.EventHandler(this.btnLogNext_Click);
            // 
            // txtLogSearch
            // 
            this.txtLogSearch.BackColor = System.Drawing.Color.Black;
            this.txtLogSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogSearch.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtLogSearch.Location = new System.Drawing.Point(20, 195);
            this.txtLogSearch.MaxLength = 64;
            this.txtLogSearch.Name = "txtLogSearch";
            this.txtLogSearch.Size = new System.Drawing.Size(242, 20);
            this.txtLogSearch.TabIndex = 91;
            // 
            // lblLOGZone
            // 
            this.lblLOGZone.AutoSize = true;
            this.lblLOGZone.Location = new System.Drawing.Point(119, 121);
            this.lblLOGZone.Name = "lblLOGZone";
            this.lblLOGZone.Size = new System.Drawing.Size(32, 13);
            this.lblLOGZone.TabIndex = 90;
            this.lblLOGZone.Text = "Zone";
            // 
            // txtLOGZone
            // 
            this.txtLOGZone.Location = new System.Drawing.Point(154, 118);
            this.txtLOGZone.MaxLength = 24;
            this.txtLOGZone.Name = "txtLOGZone";
            this.txtLOGZone.Size = new System.Drawing.Size(29, 20);
            this.txtLOGZone.TabIndex = 7;
            this.txtLOGZone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "NR";
            // 
            // txtLOGNR
            // 
            this.txtLOGNR.Location = new System.Drawing.Point(58, 143);
            this.txtLOGNR.MaxLength = 24;
            this.txtLOGNR.Name = "txtLOGNR";
            this.txtLOGNR.Size = new System.Drawing.Size(30, 20);
            this.txtLOGNR.TabIndex = 8;
            this.txtLOGNR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLOGNR
            // 
            this.lblLOGNR.AutoSize = true;
            this.lblLOGNR.Location = new System.Drawing.Point(95, 146);
            this.lblLOGNR.Name = "lblLOGNR";
            this.lblLOGNR.Size = new System.Drawing.Size(40, 13);
            this.lblLOGNR.TabIndex = 86;
            this.lblLOGNR.Text = "My NR";
            // 
            // udLOGMyNR
            // 
            this.udLOGMyNR.Location = new System.Drawing.Point(136, 143);
            this.udLOGMyNR.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udLOGMyNR.Name = "udLOGMyNR";
            this.udLOGMyNR.Size = new System.Drawing.Size(47, 20);
            this.udLOGMyNR.TabIndex = 9;
            this.udLOGMyNR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblLOC
            // 
            this.lblLOC.AutoSize = true;
            this.lblLOC.Location = new System.Drawing.Point(15, 121);
            this.lblLOC.Name = "lblLOC";
            this.lblLOC.Size = new System.Drawing.Size(28, 13);
            this.lblLOC.TabIndex = 84;
            this.lblLOC.Text = "LOC";
            // 
            // txtLOGLOC
            // 
            this.txtLOGLOC.Location = new System.Drawing.Point(58, 118);
            this.txtLOGLOC.MaxLength = 24;
            this.txtLOGLOC.Name = "txtLOGLOC";
            this.txtLOGLOC.Size = new System.Drawing.Size(55, 20);
            this.txtLOGLOC.TabIndex = 6;
            this.txtLOGLOC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "SNT";
            // 
            // txtLogMyRST
            // 
            this.txtLogMyRST.Location = new System.Drawing.Point(154, 43);
            this.txtLogMyRST.Name = "txtLogMyRST";
            this.txtLogMyRST.Size = new System.Drawing.Size(29, 20);
            this.txtLogMyRST.TabIndex = 3;
            this.txtLogMyRST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpMonitor
            // 
            this.grpMonitor.Controls.Add(this.chkAFC);
            this.grpMonitor.Controls.Add(this.chkSQL);
            this.grpMonitor.Controls.Add(this.lblPSKCH2Freq);
            this.grpMonitor.Controls.Add(this.lblPSKDCDCh2);
            this.grpMonitor.Controls.Add(this.lblDCDCh2);
            this.grpMonitor.Controls.Add(this.lblPSKCH1Freq);
            this.grpMonitor.Controls.Add(this.lblPSKDCDCh1);
            this.grpMonitor.Controls.Add(this.lblDCDCh1);
            this.grpMonitor.Controls.Add(this.picMonitor);
            this.grpMonitor.Controls.Add(this.lblRTTYMark);
            this.grpMonitor.Controls.Add(this.lblRTTYSpace);
            this.grpMonitor.Controls.Add(this.radRTTYReverse);
            this.grpMonitor.Controls.Add(this.radRTTYNormal);
            this.grpMonitor.Controls.Add(this.lblRTTYSpaceBox);
            this.grpMonitor.Controls.Add(this.lblRTTYMarkBox);
            this.grpMonitor.ForeColor = System.Drawing.Color.White;
            this.grpMonitor.Location = new System.Drawing.Point(274, 10);
            this.grpMonitor.Name = "grpMonitor";
            this.grpMonitor.Size = new System.Drawing.Size(295, 235);
            this.grpMonitor.TabIndex = 80;
            this.grpMonitor.TabStop = false;
            this.grpMonitor.Text = "Monitor";
            // 
            // chkAFC
            // 
            this.chkAFC.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAFC.AutoSize = true;
            this.chkAFC.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkAFC.ForeColor = System.Drawing.Color.Black;
            this.chkAFC.Location = new System.Drawing.Point(241, 202);
            this.chkAFC.Name = "chkAFC";
            this.chkAFC.Size = new System.Drawing.Size(32, 23);
            this.chkAFC.TabIndex = 83;
            this.chkAFC.Text = "afc";
            this.chkAFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAFC.UseVisualStyleBackColor = true;
            this.chkAFC.Visible = false;
            this.chkAFC.CheckedChanged += new System.EventHandler(this.chkAFC_CheckedChanged);
            // 
            // chkSQL
            // 
            this.chkSQL.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSQL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkSQL.ForeColor = System.Drawing.Color.Black;
            this.chkSQL.Location = new System.Drawing.Point(262, 203);
            this.chkSQL.Name = "chkSQL";
            this.chkSQL.Size = new System.Drawing.Size(26, 20);
            this.chkSQL.TabIndex = 82;
            this.chkSQL.Text = "sq";
            this.chkSQL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkSQL.UseVisualStyleBackColor = true;
            this.chkSQL.CheckedChanged += new System.EventHandler(this.chkSQL_CheckedChanged);
            // 
            // lblPSKCH2Freq
            // 
            this.lblPSKCH2Freq.AutoSize = true;
            this.lblPSKCH2Freq.Location = new System.Drawing.Point(202, 207);
            this.lblPSKCH2Freq.Name = "lblPSKCH2Freq";
            this.lblPSKCH2Freq.Size = new System.Drawing.Size(28, 13);
            this.lblPSKCH2Freq.TabIndex = 81;
            this.lblPSKCH2Freq.Text = "0.01";
            // 
            // lblPSKDCDCh2
            // 
            this.lblPSKDCDCh2.AutoSize = true;
            this.lblPSKDCDCh2.BackColor = System.Drawing.Color.Red;
            this.lblPSKDCDCh2.Location = new System.Drawing.Point(190, 207);
            this.lblPSKDCDCh2.Name = "lblPSKDCDCh2";
            this.lblPSKDCDCh2.Size = new System.Drawing.Size(10, 13);
            this.lblPSKDCDCh2.TabIndex = 80;
            this.lblPSKDCDCh2.Text = " ";
            // 
            // lblDCDCh2
            // 
            this.lblDCDCh2.AutoSize = true;
            this.lblDCDCh2.Location = new System.Drawing.Point(133, 207);
            this.lblDCDCh2.Name = "lblDCDCh2";
            this.lblDCDCh2.Size = new System.Drawing.Size(54, 13);
            this.lblDCDCh2.TabIndex = 79;
            this.lblDCDCh2.Text = "CH2 DCD";
            // 
            // lblPSKCH1Freq
            // 
            this.lblPSKCH1Freq.AutoSize = true;
            this.lblPSKCH1Freq.Location = new System.Drawing.Point(90, 207);
            this.lblPSKCH1Freq.Name = "lblPSKCH1Freq";
            this.lblPSKCH1Freq.Size = new System.Drawing.Size(28, 13);
            this.lblPSKCH1Freq.TabIndex = 78;
            this.lblPSKCH1Freq.Text = "0.01";
            // 
            // lblPSKDCDCh1
            // 
            this.lblPSKDCDCh1.AutoSize = true;
            this.lblPSKDCDCh1.BackColor = System.Drawing.Color.Red;
            this.lblPSKDCDCh1.Location = new System.Drawing.Point(77, 207);
            this.lblPSKDCDCh1.Name = "lblPSKDCDCh1";
            this.lblPSKDCDCh1.Size = new System.Drawing.Size(10, 13);
            this.lblPSKDCDCh1.TabIndex = 77;
            this.lblPSKDCDCh1.Text = " ";
            // 
            // lblDCDCh1
            // 
            this.lblDCDCh1.AutoSize = true;
            this.lblDCDCh1.Location = new System.Drawing.Point(21, 207);
            this.lblDCDCh1.Name = "lblDCDCh1";
            this.lblDCDCh1.Size = new System.Drawing.Size(54, 13);
            this.lblDCDCh1.TabIndex = 76;
            this.lblDCDCh1.Text = "CH1 DCD";
            // 
            // picMonitor
            // 
            this.picMonitor.BackColor = System.Drawing.Color.DimGray;
            this.picMonitor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMonitor.ContextMenuStrip = this.picMonitorContextMenu;
            this.picMonitor.Location = new System.Drawing.Point(8, 14);
            this.picMonitor.Name = "picMonitor";
            this.picMonitor.Size = new System.Drawing.Size(279, 179);
            this.picMonitor.TabIndex = 0;
            this.picMonitor.TabStop = false;
            this.picMonitor.MouseLeave += new System.EventHandler(this.picMonitor_MouseLeave);
            this.picMonitor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMonitor_MouseMove);
            this.picMonitor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMonitor_MouseDown);
            this.picMonitor.Paint += new System.Windows.Forms.PaintEventHandler(this.picMonitor_Paint);
            this.picMonitor.MouseEnter += new System.EventHandler(this.picMonitor_MouseEnter);
            // 
            // lblRTTYMark
            // 
            this.lblRTTYMark.AutoSize = true;
            this.lblRTTYMark.Location = new System.Drawing.Point(32, 207);
            this.lblRTTYMark.Name = "lblRTTYMark";
            this.lblRTTYMark.Size = new System.Drawing.Size(31, 13);
            this.lblRTTYMark.TabIndex = 71;
            this.lblRTTYMark.Text = "Mark";
            this.lblRTTYMark.Click += new System.EventHandler(this.lblRTTYMark_Click);
            // 
            // lblRTTYSpace
            // 
            this.lblRTTYSpace.AutoSize = true;
            this.lblRTTYSpace.Location = new System.Drawing.Point(107, 207);
            this.lblRTTYSpace.Name = "lblRTTYSpace";
            this.lblRTTYSpace.Size = new System.Drawing.Size(38, 13);
            this.lblRTTYSpace.TabIndex = 72;
            this.lblRTTYSpace.Text = "Space";
            // 
            // radRTTYReverse
            // 
            this.radRTTYReverse.AutoSize = true;
            this.radRTTYReverse.Location = new System.Drawing.Point(209, 205);
            this.radRTTYReverse.Name = "radRTTYReverse";
            this.radRTTYReverse.Size = new System.Drawing.Size(45, 17);
            this.radRTTYReverse.TabIndex = 75;
            this.radRTTYReverse.Text = "Rev";
            this.radRTTYReverse.UseVisualStyleBackColor = true;
            this.radRTTYReverse.CheckedChanged += new System.EventHandler(this.radRTTYReverse_CheckedChanged);
            // 
            // radRTTYNormal
            // 
            this.radRTTYNormal.Checked = true;
            this.radRTTYNormal.Location = new System.Drawing.Point(156, 205);
            this.radRTTYNormal.Name = "radRTTYNormal";
            this.radRTTYNormal.Size = new System.Drawing.Size(50, 17);
            this.radRTTYNormal.TabIndex = 74;
            this.radRTTYNormal.TabStop = true;
            this.radRTTYNormal.Text = "Norm";
            this.radRTTYNormal.UseVisualStyleBackColor = true;
            this.radRTTYNormal.CheckedChanged += new System.EventHandler(this.radRTTYNormal_CheckedChanged);
            // 
            // lblRTTYSpaceBox
            // 
            this.lblRTTYSpaceBox.AutoSize = true;
            this.lblRTTYSpaceBox.BackColor = System.Drawing.Color.Red;
            this.lblRTTYSpaceBox.Location = new System.Drawing.Point(89, 207);
            this.lblRTTYSpaceBox.Name = "lblRTTYSpaceBox";
            this.lblRTTYSpaceBox.Size = new System.Drawing.Size(10, 13);
            this.lblRTTYSpaceBox.TabIndex = 8;
            this.lblRTTYSpaceBox.Text = " ";
            // 
            // lblRTTYMarkBox
            // 
            this.lblRTTYMarkBox.AutoSize = true;
            this.lblRTTYMarkBox.BackColor = System.Drawing.Color.Red;
            this.lblRTTYMarkBox.Location = new System.Drawing.Point(69, 207);
            this.lblRTTYMarkBox.Name = "lblRTTYMarkBox";
            this.lblRTTYMarkBox.Size = new System.Drawing.Size(10, 13);
            this.lblRTTYMarkBox.TabIndex = 7;
            this.lblRTTYMarkBox.Text = " ";
            // 
            // lblLogInfo
            // 
            this.lblLogInfo.AutoSize = true;
            this.lblLogInfo.Location = new System.Drawing.Point(15, 171);
            this.lblLogInfo.Name = "lblLogInfo";
            this.lblLogInfo.Size = new System.Drawing.Size(39, 13);
            this.lblLogInfo.TabIndex = 79;
            this.lblLogInfo.Text = "Info txt";
            // 
            // txtLogInfo
            // 
            this.txtLogInfo.Location = new System.Drawing.Point(58, 168);
            this.txtLogInfo.MaxLength = 256;
            this.txtLogInfo.Multiline = true;
            this.txtLogInfo.Name = "txtLogInfo";
            this.txtLogInfo.Size = new System.Drawing.Size(125, 20);
            this.txtLogInfo.TabIndex = 10;
            this.txtLogInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLogQTH
            // 
            this.lblLogQTH.AutoSize = true;
            this.lblLogQTH.Location = new System.Drawing.Point(15, 96);
            this.lblLogQTH.Name = "lblLogQTH";
            this.lblLogQTH.Size = new System.Drawing.Size(30, 13);
            this.lblLogQTH.TabIndex = 77;
            this.lblLogQTH.Text = "QTH";
            // 
            // txtLogQTH
            // 
            this.txtLogQTH.Location = new System.Drawing.Point(58, 93);
            this.txtLogQTH.MaxLength = 24;
            this.txtLogQTH.Name = "txtLogQTH";
            this.txtLogQTH.Size = new System.Drawing.Size(125, 20);
            this.txtLogQTH.TabIndex = 5;
            this.txtLogQTH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnLOG6
            // 
            this.btnLOG6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOG6.ForeColor = System.Drawing.Color.Black;
            this.btnLOG6.Location = new System.Drawing.Point(196, 166);
            this.btnLOG6.Name = "btnLOG6";
            this.btnLOG6.Size = new System.Drawing.Size(66, 23);
            this.btnLOG6.TabIndex = 16;
            this.btnLOG6.Text = "End QSO";
            this.btnLOG6.UseVisualStyleBackColor = true;
            this.btnLOG6.Click += new System.EventHandler(this.LOGbtn6_Click);
            // 
            // btnLOG5
            // 
            this.btnLOG5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOG5.ForeColor = System.Drawing.Color.Black;
            this.btnLOG5.Location = new System.Drawing.Point(196, 136);
            this.btnLOG5.Name = "btnLOG5";
            this.btnLOG5.Size = new System.Drawing.Size(66, 23);
            this.btnLOG5.TabIndex = 15;
            this.btnLOG5.Text = "Stn info";
            this.btnLOG5.UseVisualStyleBackColor = true;
            this.btnLOG5.Click += new System.EventHandler(this.LOGbtn5_Click);
            // 
            // btnLOG4
            // 
            this.btnLOG4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOG4.ForeColor = System.Drawing.Color.Black;
            this.btnLOG4.Location = new System.Drawing.Point(196, 106);
            this.btnLOG4.Name = "btnLOG4";
            this.btnLOG4.Size = new System.Drawing.Size(66, 23);
            this.btnLOG4.TabIndex = 14;
            this.btnLOG4.Text = "Info";
            this.btnLOG4.UseVisualStyleBackColor = true;
            this.btnLOG4.Click += new System.EventHandler(this.LOGbtn4_Click);
            // 
            // btnLOG3
            // 
            this.btnLOG3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOG3.ForeColor = System.Drawing.Color.Black;
            this.btnLOG3.Location = new System.Drawing.Point(196, 76);
            this.btnLOG3.Name = "btnLOG3";
            this.btnLOG3.Size = new System.Drawing.Size(66, 23);
            this.btnLOG3.TabIndex = 13;
            this.btnLOG3.Text = "Answer";
            this.btnLOG3.UseVisualStyleBackColor = true;
            this.btnLOG3.Click += new System.EventHandler(this.LOGbtn3_Click);
            // 
            // btnLOG2
            // 
            this.btnLOG2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOG2.ForeColor = System.Drawing.Color.Black;
            this.btnLOG2.Location = new System.Drawing.Point(196, 46);
            this.btnLOG2.Name = "btnLOG2";
            this.btnLOG2.Size = new System.Drawing.Size(66, 23);
            this.btnLOG2.TabIndex = 12;
            this.btnLOG2.Text = "QRZ?";
            this.btnLOG2.UseVisualStyleBackColor = true;
            this.btnLOG2.Click += new System.EventHandler(this.LOGbtn2_Click);
            // 
            // btnLOG1
            // 
            this.btnLOG1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOG1.ForeColor = System.Drawing.Color.Black;
            this.btnLOG1.Location = new System.Drawing.Point(196, 16);
            this.btnLOG1.Name = "btnLOG1";
            this.btnLOG1.Size = new System.Drawing.Size(66, 23);
            this.btnLOG1.TabIndex = 11;
            this.btnLOG1.Text = "CQ";
            this.btnLOG1.UseVisualStyleBackColor = true;
            this.btnLOG1.Click += new System.EventHandler(this.LOGbtn1_Click);
            // 
            // btnLogClear
            // 
            this.btnLogClear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLogClear.ForeColor = System.Drawing.Color.Black;
            this.btnLogClear.Location = new System.Drawing.Point(54, 220);
            this.btnLogClear.Name = "btnLogClear";
            this.btnLogClear.Size = new System.Drawing.Size(40, 23);
            this.btnLogClear.TabIndex = 18;
            this.btnLogClear.Text = "Clear";
            this.btnLogClear.UseVisualStyleBackColor = true;
            this.btnLogClear.Click += new System.EventHandler(this.btnLogClear_Click);
            // 
            // btnLogSearch
            // 
            this.btnLogSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLogSearch.ForeColor = System.Drawing.Color.Black;
            this.btnLogSearch.Location = new System.Drawing.Point(97, 220);
            this.btnLogSearch.Name = "btnLogSearch";
            this.btnLogSearch.Size = new System.Drawing.Size(40, 23);
            this.btnLogSearch.TabIndex = 19;
            this.btnLogSearch.Text = "Search";
            this.btnLogSearch.UseVisualStyleBackColor = true;
            this.btnLogSearch.Click += new System.EventHandler(this.btnLogSearch_Click);
            // 
            // btnLOGSave
            // 
            this.btnLOGSave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOGSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLOGSave.ForeColor = System.Drawing.Color.Black;
            this.btnLOGSave.Location = new System.Drawing.Point(11, 220);
            this.btnLOGSave.Name = "btnLOGSave";
            this.btnLOGSave.Size = new System.Drawing.Size(40, 23);
            this.btnLOGSave.TabIndex = 17;
            this.btnLOGSave.Text = "Save";
            this.btnLOGSave.UseVisualStyleBackColor = true;
            this.btnLOGSave.Click += new System.EventHandler(this.btnLogSave_Click);
            // 
            // lblLogRST
            // 
            this.lblLogRST.AutoSize = true;
            this.lblLogRST.Location = new System.Drawing.Point(15, 46);
            this.lblLogRST.Name = "lblLogRST";
            this.lblLogRST.Size = new System.Drawing.Size(29, 13);
            this.lblLogRST.TabIndex = 6;
            this.lblLogRST.Text = "RCV";
            // 
            // txtLogRST
            // 
            this.txtLogRST.Location = new System.Drawing.Point(58, 43);
            this.txtLogRST.Name = "txtLogRST";
            this.txtLogRST.Size = new System.Drawing.Size(29, 20);
            this.txtLogRST.TabIndex = 2;
            this.txtLogRST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLogName
            // 
            this.lblLogName.AutoSize = true;
            this.lblLogName.Location = new System.Drawing.Point(15, 71);
            this.lblLogName.Name = "lblLogName";
            this.lblLogName.Size = new System.Drawing.Size(35, 13);
            this.lblLogName.TabIndex = 4;
            this.lblLogName.Text = "Name";
            // 
            // txtLogName
            // 
            this.txtLogName.Location = new System.Drawing.Point(58, 68);
            this.txtLogName.MaxLength = 24;
            this.txtLogName.Name = "txtLogName";
            this.txtLogName.Size = new System.Drawing.Size(125, 20);
            this.txtLogName.TabIndex = 4;
            this.txtLogName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLogCALL
            // 
            this.lblLogCALL.AutoSize = true;
            this.lblLogCALL.Location = new System.Drawing.Point(15, 19);
            this.lblLogCALL.Name = "lblLogCALL";
            this.lblLogCALL.Size = new System.Drawing.Size(33, 13);
            this.lblLogCALL.TabIndex = 2;
            this.lblLogCALL.Text = "CALL";
            // 
            // txtLogCall
            // 
            this.txtLogCall.Location = new System.Drawing.Point(58, 18);
            this.txtLogCall.MaxLength = 12;
            this.txtLogCall.Name = "txtLogCall";
            this.txtLogCall.Size = new System.Drawing.Size(125, 20);
            this.txtLogCall.TabIndex = 1;
            this.txtLogCall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLogCall.TextChanged += new System.EventHandler(this.txtLogCall_TextChanged);
            // 
            // btnLOG
            // 
            this.btnLOG.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLOG.ForeColor = System.Drawing.Color.Black;
            this.btnLOG.Location = new System.Drawing.Point(96, 186);
            this.btnLOG.Name = "btnLOG";
            this.btnLOG.Size = new System.Drawing.Size(37, 19);
            this.btnLOG.TabIndex = 59;
            this.btnLOG.Text = "LOG";
            this.btnLOG.UseVisualStyleBackColor = true;
            this.btnLOG.Click += new System.EventHandler(this.btnLOG_Click);
            // 
            // btnAudioMute
            // 
            this.btnAudioMute.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnAudioMute.ForeColor = System.Drawing.Color.Black;
            this.btnAudioMute.Location = new System.Drawing.Point(10, 403);
            this.btnAudioMute.Name = "btnAudioMute";
            this.btnAudioMute.Size = new System.Drawing.Size(62, 23);
            this.btnAudioMute.TabIndex = 46;
            this.btnAudioMute.Text = "MUT";
            this.btnAudioMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAudioMute.UseVisualStyleBackColor = true;
            this.btnAudioMute.CheckedChanged += new System.EventHandler(this.btnAudioMute_CheckedChanged);
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(18, 427);
            this.tbVolume.Maximum = 100;
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbVolume.Size = new System.Drawing.Size(45, 73);
            this.tbVolume.TabIndex = 47;
            this.tbVolume.TickFrequency = 15;
            this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbVolume.Value = 30;
            this.tbVolume.MouseLeave += new System.EventHandler(this.tbVolume_MouseLeave);
            this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);
            this.tbVolume.MouseHover += new System.EventHandler(this.tbVolume_MouseHover);
            // 
            // lblVolume
            // 
            this.lblVolume.Location = new System.Drawing.Point(18, 504);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(42, 31);
            this.lblVolume.TabIndex = 48;
            this.lblVolume.Text = "Master Volume";
            // 
            // btnNB
            // 
            this.btnNB.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnNB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnNB.ForeColor = System.Drawing.Color.Black;
            this.btnNB.Location = new System.Drawing.Point(40, 194);
            this.btnNB.Name = "btnNB";
            this.btnNB.Size = new System.Drawing.Size(36, 23);
            this.btnNB.TabIndex = 43;
            this.btnNB.Text = "NB";
            this.btnNB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNB.UseVisualStyleBackColor = true;
            this.btnNB.CheckedChanged += new System.EventHandler(this.btnNB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 31);
            this.label2.TabIndex = 51;
            this.label2.Text = "Input Level";
            // 
            // tbInputLevel
            // 
            this.tbInputLevel.Location = new System.Drawing.Point(18, 253);
            this.tbInputLevel.Maximum = 1000;
            this.tbInputLevel.Name = "tbInputLevel";
            this.tbInputLevel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbInputLevel.Size = new System.Drawing.Size(45, 73);
            this.tbInputLevel.TabIndex = 50;
            this.tbInputLevel.TickFrequency = 150;
            this.tbInputLevel.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbInputLevel.Value = 15;
            this.tbInputLevel.MouseLeave += new System.EventHandler(this.tbInputLevel_MouseLeave);
            this.tbInputLevel.Scroll += new System.EventHandler(this.tbInputLevel_Scroll);
            this.tbInputLevel.MouseHover += new System.EventHandler(this.tbInputLevel_MouseHover);
            // 
            // grpMorseRunner
            // 
            this.grpMorseRunner.Controls.Add(this.label3);
            this.grpMorseRunner.Controls.Add(this.tbMRSquelch);
            this.grpMorseRunner.Controls.Add(this.btnSendCall);
            this.grpMorseRunner.Controls.Add(this.label2);
            this.grpMorseRunner.Controls.Add(this.txtRST);
            this.grpMorseRunner.Controls.Add(this.tbInputLevel);
            this.grpMorseRunner.Controls.Add(this.btnSendRST);
            this.grpMorseRunner.Controls.Add(this.txtNr);
            this.grpMorseRunner.Controls.Add(this.lblVolume);
            this.grpMorseRunner.Controls.Add(this.btnSendNr);
            this.grpMorseRunner.Controls.Add(this.tbVolume);
            this.grpMorseRunner.Controls.Add(this.lblRST);
            this.grpMorseRunner.Controls.Add(this.btnAudioMute);
            this.grpMorseRunner.Controls.Add(this.lblNr);
            this.grpMorseRunner.Controls.Add(this.btngrab);
            this.grpMorseRunner.Controls.Add(this.btnclr);
            this.grpMorseRunner.ForeColor = System.Drawing.Color.White;
            this.grpMorseRunner.Location = new System.Drawing.Point(294, 12);
            this.grpMorseRunner.Name = "grpMorseRunner";
            this.grpMorseRunner.Size = new System.Drawing.Size(83, 541);
            this.grpMorseRunner.TabIndex = 52;
            this.grpMorseRunner.TabStop = false;
            this.grpMorseRunner.Text = "Morse runner";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Sql";
            // 
            // tbMRSquelch
            // 
            this.tbMRSquelch.AutoSize = false;
            this.tbMRSquelch.Location = new System.Drawing.Point(5, 357);
            this.tbMRSquelch.Maximum = 1000;
            this.tbMRSquelch.Minimum = 1;
            this.tbMRSquelch.Name = "tbMRSquelch";
            this.tbMRSquelch.Size = new System.Drawing.Size(73, 20);
            this.tbMRSquelch.TabIndex = 52;
            this.tbMRSquelch.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMRSquelch.Value = 1;
            this.tbMRSquelch.Scroll += new System.EventHandler(this.tbMRSquelch_Scroll);
            // 
            // grpGenesisRadio
            // 
            this.grpGenesisRadio.Controls.Add(this.chkSplit);
            this.grpGenesisRadio.Controls.Add(this.lblPWR);
            this.grpGenesisRadio.Controls.Add(this.tbG59PWR);
            this.grpGenesisRadio.Controls.Add(this.picSQL);
            this.grpGenesisRadio.Controls.Add(this.lblSQL);
            this.grpGenesisRadio.Controls.Add(this.tbSQL);
            this.grpGenesisRadio.Controls.Add(this.grpBand);
            this.grpGenesisRadio.Controls.Add(this.grpFilter);
            this.grpGenesisRadio.Controls.Add(this.lblUSB);
            this.grpGenesisRadio.Controls.Add(this.btnATT);
            this.grpGenesisRadio.Controls.Add(this.lblRFGain);
            this.grpGenesisRadio.Controls.Add(this.tbRFGain);
            this.grpGenesisRadio.Controls.Add(this.btnNB);
            this.grpGenesisRadio.Controls.Add(this.btnAF);
            this.grpGenesisRadio.Controls.Add(this.lblAFGain);
            this.grpGenesisRadio.Controls.Add(this.btnRF);
            this.grpGenesisRadio.Controls.Add(this.tbAFGain);
            this.grpGenesisRadio.Controls.Add(this.btnMute);
            this.grpGenesisRadio.ForeColor = System.Drawing.Color.White;
            this.grpGenesisRadio.Location = new System.Drawing.Point(294, 12);
            this.grpGenesisRadio.Name = "grpGenesisRadio";
            this.grpGenesisRadio.Size = new System.Drawing.Size(83, 541);
            this.grpGenesisRadio.TabIndex = 53;
            this.grpGenesisRadio.TabStop = false;
            this.grpGenesisRadio.Text = "G59";
            // 
            // chkSplit
            // 
            this.chkSplit.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSplit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chkSplit.ForeColor = System.Drawing.Color.Black;
            this.chkSplit.Location = new System.Drawing.Point(40, 220);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(36, 23);
            this.chkSplit.TabIndex = 45;
            this.chkSplit.Text = "SP";
            this.chkSplit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSplit.UseVisualStyleBackColor = true;
            this.chkSplit.CheckedChanged += new System.EventHandler(this.chkSplit_CheckedChanged);
            // 
            // lblPWR
            // 
            this.lblPWR.Location = new System.Drawing.Point(24, 268);
            this.lblPWR.Name = "lblPWR";
            this.lblPWR.Size = new System.Drawing.Size(38, 15);
            this.lblPWR.TabIndex = 70;
            this.lblPWR.Text = "PWR";
            // 
            // tbG59PWR
            // 
            this.tbG59PWR.AutoSize = false;
            this.tbG59PWR.Location = new System.Drawing.Point(5, 248);
            this.tbG59PWR.Maximum = 100;
            this.tbG59PWR.Name = "tbG59PWR";
            this.tbG59PWR.Size = new System.Drawing.Size(73, 21);
            this.tbG59PWR.SmallChange = 10;
            this.tbG59PWR.TabIndex = 46;
            this.tbG59PWR.TickFrequency = 150;
            this.tbG59PWR.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbG59PWR.Value = 50;
            this.tbG59PWR.MouseLeave += new System.EventHandler(this.tbG59PWR_MouseLeave);
            this.tbG59PWR.Scroll += new System.EventHandler(this.tbG59PWR_Scroll);
            this.tbG59PWR.MouseHover += new System.EventHandler(this.tbG59PWR_MouseHover);
            // 
            // picSQL
            // 
            this.picSQL.Location = new System.Drawing.Point(11, 286);
            this.picSQL.Name = "picSQL";
            this.picSQL.Size = new System.Drawing.Size(60, 3);
            this.picSQL.TabIndex = 68;
            this.picSQL.TabStop = false;
            this.picSQL.Paint += new System.Windows.Forms.PaintEventHandler(this.picSQL_Paint);
            // 
            // lblSQL
            // 
            this.lblSQL.Location = new System.Drawing.Point(16, 312);
            this.lblSQL.Name = "lblSQL";
            this.lblSQL.Size = new System.Drawing.Size(47, 19);
            this.lblSQL.TabIndex = 67;
            this.lblSQL.Text = "Squelch";
            // 
            // tbSQL
            // 
            this.tbSQL.AutoSize = false;
            this.tbSQL.LargeChange = 1;
            this.tbSQL.Location = new System.Drawing.Point(4, 291);
            this.tbSQL.Maximum = 100;
            this.tbSQL.Minimum = 1;
            this.tbSQL.Name = "tbSQL";
            this.tbSQL.Size = new System.Drawing.Size(73, 21);
            this.tbSQL.TabIndex = 47;
            this.tbSQL.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSQL.Value = 20;
            this.tbSQL.MouseLeave += new System.EventHandler(this.tbSQL_MouseLeave);
            this.tbSQL.Scroll += new System.EventHandler(this.tbSQL_Scroll);
            this.tbSQL.MouseHover += new System.EventHandler(this.tbSQL_MouseHover);
            // 
            // grpBand
            // 
            this.grpBand.BackColor = System.Drawing.Color.Black;
            this.grpBand.Controls.Add(this.radBand160);
            this.grpBand.Controls.Add(this.radBand6);
            this.grpBand.Controls.Add(this.radBand80);
            this.grpBand.Controls.Add(this.radBand40);
            this.grpBand.Controls.Add(this.radBand10);
            this.grpBand.Controls.Add(this.radBand30);
            this.grpBand.Controls.Add(this.radBand20);
            this.grpBand.Controls.Add(this.radBand12);
            this.grpBand.Controls.Add(this.radBand17);
            this.grpBand.Controls.Add(this.radBand15);
            this.grpBand.ForeColor = System.Drawing.Color.White;
            this.grpBand.Location = new System.Drawing.Point(5, 11);
            this.grpBand.Name = "grpBand";
            this.grpBand.Size = new System.Drawing.Size(73, 152);
            this.grpBand.TabIndex = 65;
            this.grpBand.TabStop = false;
            this.grpBand.Text = "Band";
            // 
            // radBand160
            // 
            this.radBand160.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand160.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand160.ForeColor = System.Drawing.Color.Black;
            this.radBand160.Location = new System.Drawing.Point(3, 19);
            this.radBand160.Name = "radBand160";
            this.radBand160.Size = new System.Drawing.Size(33, 20);
            this.radBand160.TabIndex = 30;
            this.radBand160.Text = "160";
            this.radBand160.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand160.UseVisualStyleBackColor = true;
            this.radBand160.CheckedChanged += new System.EventHandler(this.radBand160_CheckedChanged);
            // 
            // radBand6
            // 
            this.radBand6.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand6.ForeColor = System.Drawing.Color.Black;
            this.radBand6.Location = new System.Drawing.Point(36, 125);
            this.radBand6.Name = "radBand6";
            this.radBand6.Size = new System.Drawing.Size(33, 20);
            this.radBand6.TabIndex = 39;
            this.radBand6.Text = "6";
            this.radBand6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand6.UseVisualStyleBackColor = true;
            this.radBand6.CheckedChanged += new System.EventHandler(this.radBand6_CheckedChanged);
            // 
            // radBand80
            // 
            this.radBand80.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand80.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand80.ForeColor = System.Drawing.Color.Black;
            this.radBand80.Location = new System.Drawing.Point(36, 19);
            this.radBand80.Name = "radBand80";
            this.radBand80.Size = new System.Drawing.Size(33, 20);
            this.radBand80.TabIndex = 31;
            this.radBand80.Text = "80";
            this.radBand80.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand80.UseVisualStyleBackColor = true;
            this.radBand80.CheckedChanged += new System.EventHandler(this.radBand80_CheckedChanged);
            // 
            // radBand40
            // 
            this.radBand40.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand40.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand40.ForeColor = System.Drawing.Color.Black;
            this.radBand40.Location = new System.Drawing.Point(3, 44);
            this.radBand40.Name = "radBand40";
            this.radBand40.Size = new System.Drawing.Size(33, 20);
            this.radBand40.TabIndex = 32;
            this.radBand40.Text = "40";
            this.radBand40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand40.UseVisualStyleBackColor = true;
            this.radBand40.CheckedChanged += new System.EventHandler(this.radBand40_CheckedChanged);
            // 
            // radBand10
            // 
            this.radBand10.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand10.ForeColor = System.Drawing.Color.Black;
            this.radBand10.Location = new System.Drawing.Point(3, 125);
            this.radBand10.Name = "radBand10";
            this.radBand10.Size = new System.Drawing.Size(33, 20);
            this.radBand10.TabIndex = 38;
            this.radBand10.Text = "10";
            this.radBand10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand10.UseVisualStyleBackColor = true;
            this.radBand10.CheckedChanged += new System.EventHandler(this.radBand10_CheckedChanged);
            // 
            // radBand30
            // 
            this.radBand30.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand30.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand30.ForeColor = System.Drawing.Color.Black;
            this.radBand30.Location = new System.Drawing.Point(36, 44);
            this.radBand30.Name = "radBand30";
            this.radBand30.Size = new System.Drawing.Size(33, 20);
            this.radBand30.TabIndex = 33;
            this.radBand30.Text = "30";
            this.radBand30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand30.UseVisualStyleBackColor = true;
            this.radBand30.CheckedChanged += new System.EventHandler(this.radBand30_CheckedChanged);
            // 
            // radBand20
            // 
            this.radBand20.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand20.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand20.ForeColor = System.Drawing.Color.Black;
            this.radBand20.Location = new System.Drawing.Point(3, 70);
            this.radBand20.Name = "radBand20";
            this.radBand20.Size = new System.Drawing.Size(33, 20);
            this.radBand20.TabIndex = 34;
            this.radBand20.Text = "20";
            this.radBand20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand20.UseVisualStyleBackColor = true;
            this.radBand20.CheckedChanged += new System.EventHandler(this.radBand20_CheckedChanged);
            // 
            // radBand12
            // 
            this.radBand12.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand12.ForeColor = System.Drawing.Color.Black;
            this.radBand12.Location = new System.Drawing.Point(36, 98);
            this.radBand12.Name = "radBand12";
            this.radBand12.Size = new System.Drawing.Size(33, 20);
            this.radBand12.TabIndex = 37;
            this.radBand12.Text = "12";
            this.radBand12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand12.UseVisualStyleBackColor = true;
            this.radBand12.CheckedChanged += new System.EventHandler(this.radBand12_CheckedChanged);
            // 
            // radBand17
            // 
            this.radBand17.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand17.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand17.ForeColor = System.Drawing.Color.Black;
            this.radBand17.Location = new System.Drawing.Point(36, 70);
            this.radBand17.Name = "radBand17";
            this.radBand17.Size = new System.Drawing.Size(33, 20);
            this.radBand17.TabIndex = 35;
            this.radBand17.Text = "17";
            this.radBand17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand17.UseVisualStyleBackColor = true;
            this.radBand17.CheckedChanged += new System.EventHandler(this.radBand17_CheckedChanged);
            // 
            // radBand15
            // 
            this.radBand15.Appearance = System.Windows.Forms.Appearance.Button;
            this.radBand15.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radBand15.ForeColor = System.Drawing.Color.Black;
            this.radBand15.Location = new System.Drawing.Point(3, 98);
            this.radBand15.Name = "radBand15";
            this.radBand15.Size = new System.Drawing.Size(33, 20);
            this.radBand15.TabIndex = 36;
            this.radBand15.Text = "15";
            this.radBand15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radBand15.UseVisualStyleBackColor = true;
            this.radBand15.CheckedChanged += new System.EventHandler(this.radBand15_CheckedChanged);
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.radFilterVar);
            this.grpFilter.Controls.Add(this.radFilter500);
            this.grpFilter.Controls.Add(this.radFilter50);
            this.grpFilter.Controls.Add(this.radFilter100);
            this.grpFilter.Controls.Add(this.radFilter250);
            this.grpFilter.Controls.Add(this.radFilter1K);
            this.grpFilter.ForeColor = System.Drawing.Color.White;
            this.grpFilter.Location = new System.Drawing.Point(5, 406);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(73, 107);
            this.grpFilter.TabIndex = 64;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter";
            // 
            // radFilterVar
            // 
            this.radFilterVar.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFilterVar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radFilterVar.ForeColor = System.Drawing.Color.Black;
            this.radFilterVar.Location = new System.Drawing.Point(36, 75);
            this.radFilterVar.Name = "radFilterVar";
            this.radFilterVar.Size = new System.Drawing.Size(33, 20);
            this.radFilterVar.TabIndex = 55;
            this.radFilterVar.Text = "Var";
            this.radFilterVar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radFilterVar.UseVisualStyleBackColor = true;
            this.radFilterVar.CheckedChanged += new System.EventHandler(this.radFilterVar_CheckedChanged);
            // 
            // radFilter500
            // 
            this.radFilter500.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFilter500.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radFilter500.ForeColor = System.Drawing.Color.Black;
            this.radFilter500.Location = new System.Drawing.Point(36, 20);
            this.radFilter500.Name = "radFilter500";
            this.radFilter500.Size = new System.Drawing.Size(33, 20);
            this.radFilter500.TabIndex = 51;
            this.radFilter500.Text = "500";
            this.radFilter500.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radFilter500.UseVisualStyleBackColor = true;
            this.radFilter500.CheckedChanged += new System.EventHandler(this.radFilter500_CheckedChanged);
            // 
            // radFilter50
            // 
            this.radFilter50.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFilter50.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radFilter50.ForeColor = System.Drawing.Color.Black;
            this.radFilter50.Location = new System.Drawing.Point(4, 75);
            this.radFilter50.Name = "radFilter50";
            this.radFilter50.Size = new System.Drawing.Size(33, 20);
            this.radFilter50.TabIndex = 54;
            this.radFilter50.Text = "50";
            this.radFilter50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radFilter50.UseVisualStyleBackColor = true;
            this.radFilter50.CheckedChanged += new System.EventHandler(this.radFilter50_CheckedChanged);
            // 
            // radFilter100
            // 
            this.radFilter100.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFilter100.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radFilter100.ForeColor = System.Drawing.Color.Black;
            this.radFilter100.Location = new System.Drawing.Point(36, 47);
            this.radFilter100.Name = "radFilter100";
            this.radFilter100.Size = new System.Drawing.Size(33, 20);
            this.radFilter100.TabIndex = 53;
            this.radFilter100.Text = "100";
            this.radFilter100.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radFilter100.UseVisualStyleBackColor = true;
            this.radFilter100.CheckedChanged += new System.EventHandler(this.radFilter100_CheckedChanged);
            // 
            // radFilter250
            // 
            this.radFilter250.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFilter250.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radFilter250.ForeColor = System.Drawing.Color.Black;
            this.radFilter250.Location = new System.Drawing.Point(4, 47);
            this.radFilter250.Name = "radFilter250";
            this.radFilter250.Size = new System.Drawing.Size(33, 20);
            this.radFilter250.TabIndex = 52;
            this.radFilter250.Text = "250";
            this.radFilter250.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radFilter250.UseVisualStyleBackColor = true;
            this.radFilter250.CheckedChanged += new System.EventHandler(this.radFilter250_CheckedChanged);
            // 
            // radFilter1K
            // 
            this.radFilter1K.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFilter1K.BackColor = System.Drawing.Color.WhiteSmoke;
            this.radFilter1K.ForeColor = System.Drawing.Color.Black;
            this.radFilter1K.Location = new System.Drawing.Point(4, 20);
            this.radFilter1K.Name = "radFilter1K";
            this.radFilter1K.Size = new System.Drawing.Size(33, 20);
            this.radFilter1K.TabIndex = 50;
            this.radFilter1K.Text = "1K";
            this.radFilter1K.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radFilter1K.UseVisualStyleBackColor = true;
            this.radFilter1K.CheckedChanged += new System.EventHandler(this.radFilter1K_CheckedChanged);
            // 
            // lblUSB
            // 
            this.lblUSB.BackColor = System.Drawing.Color.Green;
            this.lblUSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUSB.ForeColor = System.Drawing.Color.White;
            this.lblUSB.Location = new System.Drawing.Point(10, 517);
            this.lblUSB.Name = "lblUSB";
            this.lblUSB.Size = new System.Drawing.Size(62, 19);
            this.lblUSB.TabIndex = 53;
            this.lblUSB.Text = "USB";
            this.lblUSB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnATT
            // 
            this.btnATT.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnATT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnATT.ForeColor = System.Drawing.Color.Black;
            this.btnATT.Location = new System.Drawing.Point(6, 194);
            this.btnATT.Name = "btnATT";
            this.btnATT.Size = new System.Drawing.Size(36, 23);
            this.btnATT.TabIndex = 42;
            this.btnATT.Text = "ATT";
            this.btnATT.UseVisualStyleBackColor = true;
            this.btnATT.CheckedChanged += new System.EventHandler(this.btnATT_CheckedChanged);
            // 
            // lblRFGain
            // 
            this.lblRFGain.Location = new System.Drawing.Point(16, 352);
            this.lblRFGain.Name = "lblRFGain";
            this.lblRFGain.Size = new System.Drawing.Size(47, 19);
            this.lblRFGain.TabIndex = 51;
            this.lblRFGain.Text = "RF gain";
            // 
            // tbRFGain
            // 
            this.tbRFGain.AutoSize = false;
            this.tbRFGain.Location = new System.Drawing.Point(4, 331);
            this.tbRFGain.Maximum = 120;
            this.tbRFGain.Minimum = 20;
            this.tbRFGain.Name = "tbRFGain";
            this.tbRFGain.Size = new System.Drawing.Size(73, 21);
            this.tbRFGain.SmallChange = 5;
            this.tbRFGain.TabIndex = 48;
            this.tbRFGain.TickFrequency = 150;
            this.tbRFGain.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRFGain.Value = 80;
            this.tbRFGain.MouseLeave += new System.EventHandler(this.tbRFGain_MouseLeave);
            this.tbRFGain.Scroll += new System.EventHandler(this.tbRFGain_Scroll);
            this.tbRFGain.MouseHover += new System.EventHandler(this.tbRFGain_MouseHover);
            // 
            // btnAF
            // 
            this.btnAF.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnAF.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAF.ForeColor = System.Drawing.Color.Black;
            this.btnAF.Location = new System.Drawing.Point(6, 168);
            this.btnAF.Name = "btnAF";
            this.btnAF.Size = new System.Drawing.Size(36, 23);
            this.btnAF.TabIndex = 40;
            this.btnAF.Text = "AF";
            this.btnAF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAF.UseVisualStyleBackColor = true;
            this.btnAF.CheckedChanged += new System.EventHandler(this.btnAF_CheckedChanged);
            // 
            // lblAFGain
            // 
            this.lblAFGain.Location = new System.Drawing.Point(18, 392);
            this.lblAFGain.Name = "lblAFGain";
            this.lblAFGain.Size = new System.Drawing.Size(45, 16);
            this.lblAFGain.TabIndex = 48;
            this.lblAFGain.Text = "AF gain";
            // 
            // btnRF
            // 
            this.btnRF.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRF.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRF.ForeColor = System.Drawing.Color.Black;
            this.btnRF.Location = new System.Drawing.Point(40, 168);
            this.btnRF.Name = "btnRF";
            this.btnRF.Size = new System.Drawing.Size(36, 23);
            this.btnRF.TabIndex = 41;
            this.btnRF.Text = "RF";
            this.btnRF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRF.UseVisualStyleBackColor = true;
            this.btnRF.CheckedChanged += new System.EventHandler(this.btnRF_CheckedChanged);
            // 
            // tbAFGain
            // 
            this.tbAFGain.AutoSize = false;
            this.tbAFGain.Location = new System.Drawing.Point(5, 371);
            this.tbAFGain.Maximum = 100;
            this.tbAFGain.Name = "tbAFGain";
            this.tbAFGain.Size = new System.Drawing.Size(73, 21);
            this.tbAFGain.TabIndex = 49;
            this.tbAFGain.TickFrequency = 15;
            this.tbAFGain.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbAFGain.Value = 30;
            this.tbAFGain.MouseLeave += new System.EventHandler(this.tbAFGain_MouseLeave);
            this.tbAFGain.Scroll += new System.EventHandler(this.tbAFGain_Scroll);
            this.tbAFGain.MouseHover += new System.EventHandler(this.tbAFGain_MouseHover);
            // 
            // btnMute
            // 
            this.btnMute.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnMute.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMute.ForeColor = System.Drawing.Color.Black;
            this.btnMute.Location = new System.Drawing.Point(6, 220);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(36, 23);
            this.btnMute.TabIndex = 44;
            this.btnMute.Text = "MUT";
            this.btnMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMute.UseVisualStyleBackColor = true;
            this.btnMute.CheckedChanged += new System.EventHandler(this.btnMute_CheckedChanged);
            // 
            // grpMorseRunner2
            // 
            this.grpMorseRunner2.Controls.Add(this.btnF8);
            this.grpMorseRunner2.Controls.Add(this.btnF1);
            this.grpMorseRunner2.Controls.Add(this.btnF2);
            this.grpMorseRunner2.Controls.Add(this.btnF3);
            this.grpMorseRunner2.Controls.Add(this.btnF4);
            this.grpMorseRunner2.Controls.Add(this.btnF5);
            this.grpMorseRunner2.Controls.Add(this.btnF6);
            this.grpMorseRunner2.Controls.Add(this.btnF7);
            this.grpMorseRunner2.ForeColor = System.Drawing.Color.White;
            this.grpMorseRunner2.Location = new System.Drawing.Point(21, 465);
            this.grpMorseRunner2.Name = "grpMorseRunner2";
            this.grpMorseRunner2.Size = new System.Drawing.Size(259, 86);
            this.grpMorseRunner2.TabIndex = 54;
            this.grpMorseRunner2.TabStop = false;
            this.grpMorseRunner2.Text = "Morse Runner";
            // 
            // grpSMeter
            // 
            this.grpSMeter.BackColor = System.Drawing.Color.Black;
            this.grpSMeter.Controls.Add(this.txtVFOB);
            this.grpSMeter.Controls.Add(this.SMeter);
            this.grpSMeter.Controls.Add(this.txtVFOA);
            this.grpSMeter.Controls.Add(this.txtLosc);
            this.grpSMeter.ForeColor = System.Drawing.Color.White;
            this.grpSMeter.Location = new System.Drawing.Point(21, 452);
            this.grpSMeter.Name = "grpSMeter";
            this.grpSMeter.Size = new System.Drawing.Size(259, 100);
            this.grpSMeter.TabIndex = 55;
            this.grpSMeter.TabStop = false;
            this.grpSMeter.Text = "G59";
            // 
            // txtVFOB
            // 
            this.txtVFOB.BackColor = System.Drawing.Color.Black;
            this.txtVFOB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtVFOB.ForeColor = System.Drawing.Color.White;
            this.txtVFOB.Location = new System.Drawing.Point(164, 47);
            this.txtVFOB.Name = "txtVFOB";
            this.txtVFOB.Size = new System.Drawing.Size(81, 15);
            this.txtVFOB.TabIndex = 2;
            this.txtVFOB.Text = "14.040000";
            this.txtVFOB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtVFOB.MouseLeave += new System.EventHandler(this.txtVFOB_MouseLeave);
            this.txtVFOB.MouseHover += new System.EventHandler(this.txtVFOB_MouseHover);
            // 
            // SMeter
            // 
            this.SMeter.BackColor = System.Drawing.Color.White;
            this.SMeter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SMeter.BaseArcColor = System.Drawing.Color.LightGray;
            this.SMeter.BaseArcRadius = 0;
            this.SMeter.BaseArcStart = 231;
            this.SMeter.BaseArcSweep = 77;
            this.SMeter.BaseArcWidth = 4;
            this.SMeter.Cap_Idx = ((byte)(1));
            this.SMeter.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.SMeter.CapPosition = new System.Drawing.Point(50, 10);
            this.SMeter.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(50, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.SMeter.CapsText = new string[] {
        "",
        "",
        "",
        "",
        ""};
            this.SMeter.CapText = "";
            this.SMeter.Center = new System.Drawing.Point(65, 100);
            this.SMeter.ContextMenuStrip = this.contextMenuSMeter;
            this.SMeter.DirectXRenderType = AnalogGAuge.AGauge.RenderType.HARDWARE;
            this.SMeter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.SMeter.GaugeTarget = null;
            this.SMeter.Location = new System.Drawing.Point(9, 13);
            this.SMeter.MaxValue = 18F;
            this.SMeter.MinValue = 0F;
            this.SMeter.Name = "SMeter";
            this.SMeter.NeedleColor1 = AnalogGAuge.AGauge.NeedleColorEnum.Red;
            this.SMeter.NeedleColor2 = System.Drawing.Color.Red;
            this.SMeter.NeedleRadius = 90;
            this.SMeter.NeedleType = 1;
            this.SMeter.NeedleWidth = 2;
            this.SMeter.Range_Idx = ((byte)(1));
            this.SMeter.RangeColor = System.Drawing.Color.Red;
            this.SMeter.RangeEnabled = true;
            this.SMeter.RangeEndValue = 18F;
            this.SMeter.RangeInnerRadius = 1;
            this.SMeter.RangeOuterRadius = 2;
            this.SMeter.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Red,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.SMeter.RangesEnabled = new bool[] {
        false,
        true,
        false,
        false,
        false};
            this.SMeter.RangesEndValue = new float[] {
        10F,
        18F,
        0F,
        0F,
        0F};
            this.SMeter.RangesInnerRadius = new int[] {
        0,
        1,
        70,
        70,
        70};
            this.SMeter.RangesOuterRadius = new int[] {
        0,
        2,
        80,
        80,
        80};
            this.SMeter.RangesStartValue = new float[] {
        0F,
        2F,
        0F,
        0F,
        0F};
            this.SMeter.RangeStartValue = 2F;
            this.SMeter.ScaleLinesInterColor = System.Drawing.Color.DimGray;
            this.SMeter.ScaleLinesInterInnerRadius = 0;
            this.SMeter.ScaleLinesInterOuterRadius = 0;
            this.SMeter.ScaleLinesInterWidth = 2;
            this.SMeter.ScaleLinesMajorColor = System.Drawing.Color.Silver;
            this.SMeter.ScaleLinesMajorInnerRadius = 0;
            this.SMeter.ScaleLinesMajorOuterRadius = 0;
            this.SMeter.ScaleLinesMajorStepValue = 2F;
            this.SMeter.ScaleLinesMajorWidth = 2;
            this.SMeter.ScaleLinesMinorColor = System.Drawing.Color.Red;
            this.SMeter.ScaleLinesMinorInnerRadius = 0;
            this.SMeter.ScaleLinesMinorNumOf = 3;
            this.SMeter.ScaleLinesMinorOuterRadius = 0;
            this.SMeter.ScaleLinesMinorWidth = 2;
            this.SMeter.ScaleNumbersColor = System.Drawing.Color.Black;
            this.SMeter.ScaleNumbersFormat = "0";
            this.SMeter.ScaleNumbersRadius = 0;
            this.SMeter.ScaleNumbersRotation = 0;
            this.SMeter.ScaleNumbersStartScaleLine = 1;
            this.SMeter.ScaleNumbersStepScaleLines = 1;
            this.SMeter.Size = new System.Drawing.Size(130, 82);
            this.SMeter.TabIndex = 0;
            this.SMeter.Value = 0F;
            // 
            // contextMenuSMeter
            // 
            this.contextMenuSMeter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripSeparator8,
            this.pWERToolStripMenuItem,
            this.reflPWRToolStripMenuItem,
            this.sWRToolStripMenuItem});
            this.contextMenuSMeter.Name = "contextMenuSMeter";
            this.contextMenuSMeter.Size = new System.Drawing.Size(152, 98);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(151, 22);
            this.toolStripTextBox1.Text = "Transmit";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(148, 6);
            // 
            // pWERToolStripMenuItem
            // 
            this.pWERToolStripMenuItem.Name = "pWERToolStripMenuItem";
            this.pWERToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.pWERToolStripMenuItem.Text = "Direct PWR";
            this.pWERToolStripMenuItem.Click += new System.EventHandler(this.pWERToolStripMenuItem_Click);
            // 
            // reflPWRToolStripMenuItem
            // 
            this.reflPWRToolStripMenuItem.Name = "reflPWRToolStripMenuItem";
            this.reflPWRToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.reflPWRToolStripMenuItem.Text = "Reflected PWR";
            this.reflPWRToolStripMenuItem.Click += new System.EventHandler(this.reflPWRToolStripMenuItem_Click);
            // 
            // sWRToolStripMenuItem
            // 
            this.sWRToolStripMenuItem.Name = "sWRToolStripMenuItem";
            this.sWRToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.sWRToolStripMenuItem.Text = "SWR";
            this.sWRToolStripMenuItem.Click += new System.EventHandler(this.sWRToolStripMenuItem_Click);
            // 
            // txtVFOA
            // 
            this.txtVFOA.BackColor = System.Drawing.Color.Black;
            this.txtVFOA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtVFOA.ForeColor = System.Drawing.Color.White;
            this.txtVFOA.Location = new System.Drawing.Point(138, 18);
            this.txtVFOA.Name = "txtVFOA";
            this.txtVFOA.Size = new System.Drawing.Size(109, 22);
            this.txtVFOA.TabIndex = 1;
            this.txtVFOA.Text = "14.040000";
            this.txtVFOA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtVFOA.MouseLeave += new System.EventHandler(this.txtVFOA_MouseLeave);
            this.txtVFOA.MouseHover += new System.EventHandler(this.txtVFOA_MouseHover);
            // 
            // txtLosc
            // 
            this.txtLosc.BackColor = System.Drawing.Color.Black;
            this.txtLosc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtLosc.ForeColor = System.Drawing.Color.White;
            this.txtLosc.Location = new System.Drawing.Point(164, 70);
            this.txtLosc.Name = "txtLosc";
            this.txtLosc.Size = new System.Drawing.Size(81, 15);
            this.txtLosc.TabIndex = 0;
            this.txtLosc.Text = "14.040000";
            this.txtLosc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtLosc.MouseLeave += new System.EventHandler(this.txtLosc_MouseLeave);
            this.txtLosc.MouseHover += new System.EventHandler(this.txtLosc_MouseHover);
            // 
            // btnTX
            // 
            this.btnTX.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTX.ForeColor = System.Drawing.Color.Black;
            this.btnTX.Location = new System.Drawing.Point(59, 33);
            this.btnTX.Name = "btnTX";
            this.btnTX.Size = new System.Drawing.Size(32, 23);
            this.btnTX.TabIndex = 23;
            this.btnTX.Text = "TX";
            this.btnTX.UseVisualStyleBackColor = true;
            this.btnTX.Click += new System.EventHandler(this.btnTX_Click);
            // 
            // grpChannels
            // 
            this.grpChannels.Controls.Add(this.btnRX2On);
            this.grpChannels.Controls.Add(this.btnLOG);
            this.grpChannels.Controls.Add(this.btnClearCH2);
            this.grpChannels.Controls.Add(this.btnClearCH1);
            this.grpChannels.Controls.Add(this.btnCH2);
            this.grpChannels.Controls.Add(this.btnCH1);
            this.grpChannels.Controls.Add(this.rtbCH2);
            this.grpChannels.Controls.Add(this.rtbCH1);
            this.grpChannels.ForeColor = System.Drawing.Color.White;
            this.grpChannels.Location = new System.Drawing.Point(8, 61);
            this.grpChannels.Name = "grpChannels";
            this.grpChannels.Size = new System.Drawing.Size(279, 386);
            this.grpChannels.TabIndex = 76;
            this.grpChannels.TabStop = false;
            this.grpChannels.Text = "CW";
            // 
            // btnRX2On
            // 
            this.btnRX2On.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRX2On.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRX2On.ForeColor = System.Drawing.Color.Black;
            this.btnRX2On.Location = new System.Drawing.Point(138, 186);
            this.btnRX2On.Name = "btnRX2On";
            this.btnRX2On.Size = new System.Drawing.Size(44, 20);
            this.btnRX2On.TabIndex = 27;
            this.btnRX2On.Text = "RX2";
            this.btnRX2On.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRX2On.UseVisualStyleBackColor = true;
            this.btnRX2On.CheckedChanged += new System.EventHandler(this.btnRX2On_CheckedChanged);
            // 
            // btnClearCH2
            // 
            this.btnClearCH2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClearCH2.ForeColor = System.Drawing.Color.Black;
            this.btnClearCH2.Location = new System.Drawing.Point(187, 186);
            this.btnClearCH2.Name = "btnClearCH2";
            this.btnClearCH2.Size = new System.Drawing.Size(44, 20);
            this.btnClearCH2.TabIndex = 28;
            this.btnClearCH2.Text = "Clear";
            this.btnClearCH2.UseVisualStyleBackColor = true;
            this.btnClearCH2.Click += new System.EventHandler(this.btnClearCh2_Click);
            // 
            // btnClearCH1
            // 
            this.btnClearCH1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClearCH1.ForeColor = System.Drawing.Color.Black;
            this.btnClearCH1.Location = new System.Drawing.Point(47, 186);
            this.btnClearCH1.Name = "btnClearCH1";
            this.btnClearCH1.Size = new System.Drawing.Size(44, 20);
            this.btnClearCH1.TabIndex = 26;
            this.btnClearCH1.Text = "Clear";
            this.btnClearCH1.UseVisualStyleBackColor = true;
            this.btnClearCH1.Click += new System.EventHandler(this.btnClearCH1_Click);
            // 
            // btnCH2
            // 
            this.btnCH2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCH2.ForeColor = System.Drawing.Color.Black;
            this.btnCH2.Location = new System.Drawing.Point(236, 186);
            this.btnCH2.Name = "btnCH2";
            this.btnCH2.Size = new System.Drawing.Size(23, 20);
            this.btnCH2.TabIndex = 29;
            this.btnCH2.Text = "2";
            this.btnCH2.UseVisualStyleBackColor = true;
            this.btnCH2.Click += new System.EventHandler(this.btnCH2_Click);
            // 
            // btnCH1
            // 
            this.btnCH1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCH1.ForeColor = System.Drawing.Color.Black;
            this.btnCH1.Location = new System.Drawing.Point(19, 186);
            this.btnCH1.Name = "btnCH1";
            this.btnCH1.Size = new System.Drawing.Size(23, 20);
            this.btnCH1.TabIndex = 25;
            this.btnCH1.Text = "1";
            this.btnCH1.UseVisualStyleBackColor = true;
            this.btnCH1.Click += new System.EventHandler(this.btnCH1_Click);
            // 
            // rtbCH2
            // 
            this.rtbCH2.AutoWordSelection = true;
            this.rtbCH2.BackColor = System.Drawing.Color.Black;
            this.rtbCH2.ContextMenuStrip = this.ch2_contextQSO;
            this.rtbCH2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbCH2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.rtbCH2.Location = new System.Drawing.Point(11, 210);
            this.rtbCH2.MaxLength = 16384;
            this.rtbCH2.Name = "rtbCH2";
            this.rtbCH2.ReadOnly = true;
            this.rtbCH2.Size = new System.Drawing.Size(256, 168);
            this.rtbCH2.TabIndex = 21;
            this.rtbCH2.Text = "";
            this.rtbCH2.SelectionChanged += new System.EventHandler(this.rtbCH2_SelectionChanged);
            this.rtbCH2.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtCH2_LinkClicked);
            this.rtbCH2.MouseEnter += new System.EventHandler(this.rtbCH2_MouseEnter);
            this.rtbCH2.DoubleClick += new System.EventHandler(this.rtbCH2_DoubleClick);
            this.rtbCH2.MouseLeave += new System.EventHandler(this.rtbCH2_MouseLeave);
            // 
            // ch2_contextQSO
            // 
            this.ch2_contextQSO.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ch2CALLMenuItem,
            this.ch2RSTMenuItem,
            this.ch2NameMenuItem,
            this.ch2QTHMenuItem,
            this.ch2LOCMenuItem,
            this.ch2ZoneMenuItem,
            this.ch2NRMenuItem,
            this.ch2InfoMenuItem});
            this.ch2_contextQSO.Name = "ch1 contextQSO";
            this.ch2_contextQSO.Size = new System.Drawing.Size(118, 180);
            this.ch2_contextQSO.Text = "CH2 QSO log book";
            // 
            // ch2CALLMenuItem
            // 
            this.ch2CALLMenuItem.Name = "ch2CALLMenuItem";
            this.ch2CALLMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2CALLMenuItem.Text = "CALL";
            this.ch2CALLMenuItem.Click += new System.EventHandler(this.ch2CALLMenuItem_Click);
            // 
            // ch2RSTMenuItem
            // 
            this.ch2RSTMenuItem.Name = "ch2RSTMenuItem";
            this.ch2RSTMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2RSTMenuItem.Text = "RST";
            this.ch2RSTMenuItem.Click += new System.EventHandler(this.ch2RSTMenuItem_Click);
            // 
            // ch2NameMenuItem
            // 
            this.ch2NameMenuItem.Name = "ch2NameMenuItem";
            this.ch2NameMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2NameMenuItem.Text = "Name";
            this.ch2NameMenuItem.Click += new System.EventHandler(this.ch2NameMenuItem_Click);
            // 
            // ch2QTHMenuItem
            // 
            this.ch2QTHMenuItem.Name = "ch2QTHMenuItem";
            this.ch2QTHMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2QTHMenuItem.Text = "QTH";
            this.ch2QTHMenuItem.Click += new System.EventHandler(this.ch2QTHMenuItem_Click);
            // 
            // ch2LOCMenuItem
            // 
            this.ch2LOCMenuItem.Name = "ch2LOCMenuItem";
            this.ch2LOCMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2LOCMenuItem.Text = "LOC";
            this.ch2LOCMenuItem.Click += new System.EventHandler(this.ch2LOCMenuItem_Click);
            // 
            // ch2ZoneMenuItem
            // 
            this.ch2ZoneMenuItem.Name = "ch2ZoneMenuItem";
            this.ch2ZoneMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2ZoneMenuItem.Text = "Zone";
            this.ch2ZoneMenuItem.Click += new System.EventHandler(this.ch2ZoneMenuItem_Click);
            // 
            // ch2NRMenuItem
            // 
            this.ch2NRMenuItem.Name = "ch2NRMenuItem";
            this.ch2NRMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2NRMenuItem.Text = "NR";
            this.ch2NRMenuItem.Click += new System.EventHandler(this.ch2NRMenuItem_Click);
            // 
            // ch2InfoMenuItem
            // 
            this.ch2InfoMenuItem.Name = "ch2InfoMenuItem";
            this.ch2InfoMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch2InfoMenuItem.Text = "Info text";
            this.ch2InfoMenuItem.Click += new System.EventHandler(this.ch2InfoMenuItem_Click);
            // 
            // rtbCH1
            // 
            this.rtbCH1.AutoWordSelection = true;
            this.rtbCH1.BackColor = System.Drawing.Color.Black;
            this.rtbCH1.ContextMenuStrip = this.ch1_contextQSO;
            this.rtbCH1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbCH1.ForeColor = System.Drawing.Color.LawnGreen;
            this.rtbCH1.Location = new System.Drawing.Point(11, 15);
            this.rtbCH1.MaxLength = 16384;
            this.rtbCH1.Name = "rtbCH1";
            this.rtbCH1.ReadOnly = true;
            this.rtbCH1.Size = new System.Drawing.Size(256, 168);
            this.rtbCH1.TabIndex = 20;
            this.rtbCH1.Text = "";
            this.rtbCH1.SelectionChanged += new System.EventHandler(this.rtbCH1_SelectionChanged);
            this.rtbCH1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtCH1_LinkClicked);
            this.rtbCH1.MouseEnter += new System.EventHandler(this.rtbCH1_MouseEnter);
            this.rtbCH1.DoubleClick += new System.EventHandler(this.rtbCH1_DoubleClick);
            this.rtbCH1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtbCH1_MouseDown);
            this.rtbCH1.MouseLeave += new System.EventHandler(this.rtbCH1_MouseLeave);
            // 
            // ch1_contextQSO
            // 
            this.ch1_contextQSO.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ch1_cALLToolStripMenuItem,
            this.ch1_rSTToolStripMenuItem,
            this.ch1_nameToolStripMenuItem,
            this.ch1_qTHToolStripMenuItem,
            this.lOCToolStripMenuItem,
            this.ZoneToolStripMenuItem,
            this.NRToolStripMenuItem,
            this.ch1_infoTextToolStripMenuItem});
            this.ch1_contextQSO.Name = "ch1 contextQSO";
            this.ch1_contextQSO.Size = new System.Drawing.Size(118, 180);
            this.ch1_contextQSO.Text = "CH1 QSO log book";
            // 
            // ch1_cALLToolStripMenuItem
            // 
            this.ch1_cALLToolStripMenuItem.Name = "ch1_cALLToolStripMenuItem";
            this.ch1_cALLToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch1_cALLToolStripMenuItem.Text = "CALL";
            this.ch1_cALLToolStripMenuItem.Click += new System.EventHandler(this.CALLToolStripMenuItem_Click);
            // 
            // ch1_rSTToolStripMenuItem
            // 
            this.ch1_rSTToolStripMenuItem.Name = "ch1_rSTToolStripMenuItem";
            this.ch1_rSTToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch1_rSTToolStripMenuItem.Text = "RST";
            this.ch1_rSTToolStripMenuItem.Click += new System.EventHandler(this.RSTToolStripMenuItem_Click);
            // 
            // ch1_nameToolStripMenuItem
            // 
            this.ch1_nameToolStripMenuItem.Name = "ch1_nameToolStripMenuItem";
            this.ch1_nameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch1_nameToolStripMenuItem.Text = "Name";
            this.ch1_nameToolStripMenuItem.Click += new System.EventHandler(this.NameToolStripMenuItem_Click);
            // 
            // ch1_qTHToolStripMenuItem
            // 
            this.ch1_qTHToolStripMenuItem.Name = "ch1_qTHToolStripMenuItem";
            this.ch1_qTHToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch1_qTHToolStripMenuItem.Text = "QTH";
            this.ch1_qTHToolStripMenuItem.Click += new System.EventHandler(this.QTHToolStripMenuItem_Click);
            // 
            // lOCToolStripMenuItem
            // 
            this.lOCToolStripMenuItem.Name = "lOCToolStripMenuItem";
            this.lOCToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.lOCToolStripMenuItem.Text = "LOC";
            this.lOCToolStripMenuItem.Click += new System.EventHandler(this.LOCToolStripMenuItem_Click);
            // 
            // ZoneToolStripMenuItem
            // 
            this.ZoneToolStripMenuItem.Name = "ZoneToolStripMenuItem";
            this.ZoneToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ZoneToolStripMenuItem.Text = "Zone";
            this.ZoneToolStripMenuItem.Click += new System.EventHandler(this.ZoneToolStripMenuItem_Click);
            // 
            // NRToolStripMenuItem
            // 
            this.NRToolStripMenuItem.Name = "NRToolStripMenuItem";
            this.NRToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.NRToolStripMenuItem.Text = "NR";
            this.NRToolStripMenuItem.Click += new System.EventHandler(this.NRToolStripMenuItem_Click);
            // 
            // ch1_infoTextToolStripMenuItem
            // 
            this.ch1_infoTextToolStripMenuItem.Name = "ch1_infoTextToolStripMenuItem";
            this.ch1_infoTextToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ch1_infoTextToolStripMenuItem.Text = "Info text";
            this.ch1_infoTextToolStripMenuItem.Click += new System.EventHandler(this.InfoTextToolStripMenuItem_Click);
            // 
            // btnTune
            // 
            this.btnTune.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTune.ForeColor = System.Drawing.Color.Black;
            this.btnTune.Location = new System.Drawing.Point(92, 33);
            this.btnTune.Name = "btnTune";
            this.btnTune.Size = new System.Drawing.Size(48, 23);
            this.btnTune.TabIndex = 77;
            this.btnTune.Text = "Tune";
            this.btnTune.UseVisualStyleBackColor = true;
            this.btnTune.Click += new System.EventHandler(this.btnTune_Click);
            // 
            // grpMRChannels
            // 
            this.grpMRChannels.Controls.Add(this.txtChannel8);
            this.grpMRChannels.Controls.Add(this.txtChannel4);
            this.grpMRChannels.Controls.Add(this.txtChannel5);
            this.grpMRChannels.Controls.Add(this.txtChannel6);
            this.grpMRChannels.Controls.Add(this.txtChannel7);
            this.grpMRChannels.Controls.Add(this.txtChannel9);
            this.grpMRChannels.Controls.Add(this.txtChannel10);
            this.grpMRChannels.Controls.Add(this.txtChannel11);
            this.grpMRChannels.Controls.Add(this.txtChannel12);
            this.grpMRChannels.Controls.Add(this.txtChannel13);
            this.grpMRChannels.Controls.Add(this.txtChannel14);
            this.grpMRChannels.Controls.Add(this.txtChannel15);
            this.grpMRChannels.Controls.Add(this.txtChannel16);
            this.grpMRChannels.Controls.Add(this.txtChannel17);
            this.grpMRChannels.Controls.Add(this.txtChannel18);
            this.grpMRChannels.Controls.Add(this.txtChannel19);
            this.grpMRChannels.Controls.Add(this.txtChannel3);
            this.grpMRChannels.Controls.Add(this.txtChannel2);
            this.grpMRChannels.Controls.Add(this.txtChannel20);
            this.grpMRChannels.ForeColor = System.Drawing.Color.White;
            this.grpMRChannels.Location = new System.Drawing.Point(8, 59);
            this.grpMRChannels.Name = "grpMRChannels";
            this.grpMRChannels.Size = new System.Drawing.Size(279, 403);
            this.grpMRChannels.TabIndex = 78;
            this.grpMRChannels.TabStop = false;
            this.grpMRChannels.Text = "MR";
            this.grpMRChannels.Visible = false;
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnKeyboard.ForeColor = System.Drawing.Color.Black;
            this.btnKeyboard.Location = new System.Drawing.Point(141, 33);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(63, 23);
            this.btnKeyboard.TabIndex = 79;
            this.btnKeyboard.Text = "Keyboard";
            this.btnKeyboard.UseVisualStyleBackColor = true;
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // CWExpert
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.btnKeyboard);
            this.Controls.Add(this.grpMorseRunner2);
            this.Controls.Add(this.grpMorseRunner);
            this.Controls.Add(this.grpGenesisRadio);
            this.Controls.Add(this.btnTune);
            this.Controls.Add(this.btnTX);
            this.Controls.Add(this.grpSMeter);
            this.Controls.Add(this.grpDisplay);
            this.Controls.Add(this.lblCall);
            this.Controls.Add(this.btnStartMR);
            this.Controls.Add(this.txtCall);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.grpChannels);
            this.Controls.Add(this.grpMRChannels);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1920, 600);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "CWExpert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainFormMouseWheel);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CWExpert_Closing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CWExpert_KeyUp);
            this.Resize += new System.EventHandler(this.CWExpert_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CWExpert_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaterfall)).EndInit();
            this.picWaterfallcontextMenu.ResumeLayout(false);
            this.picMonitorContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPanadapter)).EndInit();
            this.grpDisplay.ResumeLayout(false);
            this.grpDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFilterWidth)).EndInit();
            this.grpLogBook.ResumeLayout(false);
            this.grpLogBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLOGMyNR)).EndInit();
            this.grpMonitor.ResumeLayout(false);
            this.grpMonitor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInputLevel)).EndInit();
            this.grpMorseRunner.ResumeLayout(false);
            this.grpMorseRunner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMRSquelch)).EndInit();
            this.grpGenesisRadio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbG59PWR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSQL)).EndInit();
            this.grpBand.ResumeLayout(false);
            this.grpFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbRFGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAFGain)).EndInit();
            this.grpMorseRunner2.ResumeLayout(false);
            this.grpSMeter.ResumeLayout(false);
            this.contextMenuSMeter.ResumeLayout(false);
            this.grpChannels.ResumeLayout(false);
            this.ch2_contextQSO.ResumeLayout(false);
            this.ch1_contextQSO.ResumeLayout(false);
            this.grpMRChannels.ResumeLayout(false);
            this.grpMRChannels.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtCall;
        private System.Windows.Forms.Button btnSendCall;
        private System.Windows.Forms.Button btnF1;
        private System.Windows.Forms.Button btnF2;
        private System.Windows.Forms.Button btnF3;
        private System.Windows.Forms.Button btnF4;
        private System.Windows.Forms.Button btnF5;
        private System.Windows.Forms.Button btnF6;
        private System.Windows.Forms.Button btnF8;
        private System.Windows.Forms.Button btnF7;
        private System.Windows.Forms.CheckBox btnStartMR;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setupMenuItem;
        private System.Windows.Forms.Button btnSendRST;
        private System.Windows.Forms.TextBox txtRST;
        private System.Windows.Forms.Button btnSendNr;
        private System.Windows.Forms.TextBox txtNr;
        public System.Windows.Forms.Label lblCall;
        private System.Windows.Forms.Label lblRST;
        private System.Windows.Forms.Label lblNr;
        private System.Windows.Forms.Button btngrab;
        private System.Windows.Forms.Button btnclr;
        public System.Windows.Forms.TextBox txtChannel4;
        public System.Windows.Forms.TextBox txtChannel5;
        public System.Windows.Forms.TextBox txtChannel6;
        public System.Windows.Forms.TextBox txtChannel7;
        public System.Windows.Forms.TextBox txtChannel11;
        public System.Windows.Forms.TextBox txtChannel10;
        public System.Windows.Forms.TextBox txtChannel9;
        public System.Windows.Forms.TextBox txtChannel8;
        public System.Windows.Forms.TextBox txtChannel15;
        public System.Windows.Forms.TextBox txtChannel14;
        public System.Windows.Forms.TextBox txtChannel13;
        public System.Windows.Forms.TextBox txtChannel12;
        public System.Windows.Forms.TextBox txtChannel19;
        public System.Windows.Forms.TextBox txtChannel18;
        public System.Windows.Forms.TextBox txtChannel17;
        public System.Windows.Forms.TextBox txtChannel16;
        public System.Windows.Forms.TextBox txtChannel3;
        public System.Windows.Forms.TextBox txtChannel2;
        public System.Windows.Forms.TextBox txtChannel20;
        public System.Windows.Forms.PictureBox picWaterfall;
        private System.Windows.Forms.GroupBox grpDisplay;
        public System.Windows.Forms.PictureBox picPanadapter;
        private System.Windows.Forms.CheckBox btnAudioMute;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.CheckBox btnNB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbInputLevel;
        private System.Windows.Forms.TrackBar tbFilterWidth;
        private System.Windows.Forms.Label txtFilterWidth;
        private System.Windows.Forms.Label lblFilterwidth;
        private System.Windows.Forms.Label lblRFGain;
        private System.Windows.Forms.CheckBox btnAF;
        private System.Windows.Forms.Label lblAFGain;
        private System.Windows.Forms.CheckBox btnRF;
        private System.Windows.Forms.CheckBox btnMute;
        private System.Windows.Forms.CheckBox btnATT;
        public System.Windows.Forms.GroupBox grpGenesisRadio;
        public System.Windows.Forms.GroupBox grpMorseRunner;
        private System.Windows.Forms.TrackBar tbRFGain;
        private System.Windows.Forms.TrackBar tbAFGain;
        public System.Windows.Forms.Label lblUSB;
        private System.Windows.Forms.RadioButton radBand160;
        private System.Windows.Forms.RadioButton radBand6;
        private System.Windows.Forms.RadioButton radBand10;
        private System.Windows.Forms.RadioButton radBand12;
        private System.Windows.Forms.RadioButton radBand15;
        private System.Windows.Forms.RadioButton radBand17;
        private System.Windows.Forms.RadioButton radBand20;
        private System.Windows.Forms.RadioButton radBand30;
        private System.Windows.Forms.RadioButton radBand40;
        private System.Windows.Forms.RadioButton radBand80;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.RadioButton radFilter100;
        private System.Windows.Forms.RadioButton radFilter250;
        private System.Windows.Forms.RadioButton radFilter1K;
        private System.Windows.Forms.Label txtVFOA;
        private System.Windows.Forms.Label txtLosc;
        public AGauge SMeter;
        private System.Windows.Forms.GroupBox grpBand;
        private System.Windows.Forms.Button lblPan;
        private System.Windows.Forms.Button lblZoom;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.TrackBar tbPan;
        private System.Windows.Forms.RadioButton radFilterVar;
        private System.Windows.Forms.RadioButton radFilter500;
        private System.Windows.Forms.RadioButton radFilter50;
        public System.Windows.Forms.GroupBox grpSMeter;
        public System.Windows.Forms.GroupBox grpMorseRunner2;
        private System.Windows.Forms.Label lblSQL;
        private System.Windows.Forms.TrackBar tbSQL;
        private System.Windows.Forms.PictureBox picSQL;
        private System.Windows.Forms.Button btnLOG;
        private System.Windows.Forms.Button btnTX;
        private System.Windows.Forms.Label txtVFOB;
        private System.Windows.Forms.Label lblPWR;
        private System.Windows.Forms.TrackBar tbG59PWR;
        private System.Windows.Forms.CheckBox chkSplit;
        public System.Windows.Forms.GroupBox grpChannels;
        private System.Windows.Forms.RichTextBox rtbCH2;
        private System.Windows.Forms.RichTextBox rtbCH1;
        private System.Windows.Forms.Button btnCH1;
        private System.Windows.Forms.Button btnClearCH1;
        private System.Windows.Forms.Button btnCH2;
        private System.Windows.Forms.Button btnClearCH2;
        private System.Windows.Forms.CheckBox btnRX2On;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rTTYToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpLogBook;
        public System.Windows.Forms.PictureBox picMonitor;
        private System.Windows.Forms.Label lblLogCALL;
        public System.Windows.Forms.TextBox txtLogCall;
        private System.Windows.Forms.Button btnLOG2;
        private System.Windows.Forms.Button btnLOG1;
        private System.Windows.Forms.Button btnLogClear;
        private System.Windows.Forms.Button btnLogSearch;
        private System.Windows.Forms.Button btnLOGSave;
        public System.Windows.Forms.Label lblRTTYSpaceBox;
        public System.Windows.Forms.Label lblRTTYMarkBox;
        private System.Windows.Forms.Label lblLogRST;
        private System.Windows.Forms.TextBox txtLogRST;
        private System.Windows.Forms.Label lblLogName;
        public System.Windows.Forms.TextBox txtLogName;
        private System.Windows.Forms.Button btnLOG6;
        private System.Windows.Forms.Button btnLOG5;
        private System.Windows.Forms.Button btnLOG4;
        private System.Windows.Forms.Button btnLOG3;
        private System.Windows.Forms.Label lblRTTYSpace;
        private System.Windows.Forms.Label lblRTTYMark;
        public System.Windows.Forms.RadioButton radRTTYNormal;
        public System.Windows.Forms.RadioButton radRTTYReverse;
        private System.Windows.Forms.Label lblLogQTH;
        private System.Windows.Forms.TextBox txtLogQTH;
        private System.Windows.Forms.Label lblLogInfo;
        private System.Windows.Forms.TextBox txtLogInfo;
        private System.Windows.Forms.GroupBox grpMonitor;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtLogMyRST;
        private System.Windows.Forms.ContextMenuStrip ch1_contextQSO;
        private System.Windows.Forms.ToolStripMenuItem ch1_cALLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch1_rSTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch1_nameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch1_qTHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch1_infoTextToolStripMenuItem;
        private System.Windows.Forms.Label lblLOC;
        private System.Windows.Forms.TextBox txtLOGLOC;
        private System.Windows.Forms.ToolStripMenuItem lOCToolStripMenuItem;
        private System.Windows.Forms.Label lblLOGNR;
        public System.Windows.Forms.NumericUpDown udLOGMyNR;
        private System.Windows.Forms.Label lblLOGZone;
        private System.Windows.Forms.TextBox txtLOGZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLOGNR;
        private System.Windows.Forms.ToolStripMenuItem NRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PSKToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ch2_contextQSO;
        private System.Windows.Forms.ToolStripMenuItem ch2CALLMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2RSTMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2NameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2QTHMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2LOCMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2ZoneMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2NRMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ch2InfoMenuItem;
        private System.Windows.Forms.Button btnTune;
        public System.Windows.Forms.Label txtFreq;
        private System.Windows.Forms.ToolStripMenuItem bPSK31ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bPSK63ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bPSK125ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bPSK250ToolStripMenuItem;
        private System.Windows.Forms.Label lblDCDCh1;
        private System.Windows.Forms.Label lblPSKCH2Freq;
        public System.Windows.Forms.Label lblPSKDCDCh2;
        private System.Windows.Forms.Label lblDCDCh2;
        private System.Windows.Forms.Label lblPSKCH1Freq;
        public System.Windows.Forms.Label lblPSKDCDCh1;
        private System.Windows.Forms.ToolStripMenuItem qPSK31ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qPSK63ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qPSK125ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qPSK250ToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkSQL;
        private System.Windows.Forms.ToolStripMenuItem lOGToolStripMenuItem;
        private System.Windows.Forms.TextBox txtLogSearch;
        public System.Windows.Forms.Button btnLogDelete;
        private System.Windows.Forms.Button btnLogPrev;
        private System.Windows.Forms.Button btnLogFirst;
        private System.Windows.Forms.Button btnLogLast;
        private System.Windows.Forms.Button btnLogNext;
        private System.Windows.Forms.CheckBox chkAFC;
        private System.Windows.Forms.ContextMenuStrip picMonitorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem waterfallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scopeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panadapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem rTTYToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pSK31ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pSK63ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pSK125ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pSK250ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qPSK31ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem qPSK63ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem qPSK125ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem qPSK250ToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip picWaterfallcontextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ContextMenuStrip contextMenuSMeter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem pWERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reflPWRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sWRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripTextBox1;
        public System.Windows.Forms.GroupBox grpMRChannels;
        private System.Windows.Forms.TrackBar tbMRSquelch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem kyboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem lOGBOOKToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem recorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dXCCToolStripMenuItem;
        private System.Windows.Forms.Button btnKeyboard;
    }
}

