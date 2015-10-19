//=================================================================
// SetupDesigner.cs
//=================================================================
// Copyright (C) 2011 S56A YT7PWR
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//=================================================================

namespace CWExpert
{
    partial class Setup
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkQSK = new System.Windows.Forms.CheckBox();
            this.chkTXSwap = new System.Windows.Forms.CheckBox();
            this.chkRXSwap = new System.Windows.Forms.CheckBox();
            this.lblMonitorDriver = new System.Windows.Forms.Label();
            this.comboMonitorDriver = new System.Windows.Forms.ComboBox();
            this.lblMonitorFreq = new System.Windows.Forms.Label();
            this.udMonitorFrequncy = new System.Windows.Forms.NumericUpDown();
            this.comboAudioMonitor = new System.Windows.Forms.ComboBox();
            this.lblAudioMonitor = new System.Windows.Forms.Label();
            this.lblAudioVolts = new System.Windows.Forms.Label();
            this.udAudioOutputVoltage = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAudioBuffer = new System.Windows.Forms.ComboBox();
            this.comboAudioOutput = new System.Windows.Forms.ComboBox();
            this.lblAudioOutput = new System.Windows.Forms.Label();
            this.lblAudioDriver = new System.Windows.Forms.Label();
            this.lblAudioLatency = new System.Windows.Forms.Label();
            this.comboAudioDriver = new System.Windows.Forms.ComboBox();
            this.udLatency = new System.Windows.Forms.NumericUpDown();
            this.comboAudioInput = new System.Windows.Forms.ComboBox();
            this.lblAudioSampleRate = new System.Windows.Forms.Label();
            this.lblAudioInput = new System.Windows.Forms.Label();
            this.comboAudioSampleRate = new System.Windows.Forms.ComboBox();
            this.tbSetup = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblMonitorMode = new System.Windows.Forms.Label();
            this.comboMonitorMode = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radDirectXSW = new System.Windows.Forms.RadioButton();
            this.radDirectXHW = new System.Windows.Forms.RadioButton();
            this.grpDisplayDriver = new System.Windows.Forms.GroupBox();
            this.radDisplayDirectX = new System.Windows.Forms.RadioButton();
            this.radDisplayGDI = new System.Windows.Forms.RadioButton();
            this.grpMisc = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.chkFillPanadapter = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.btnFillColor = new System.Windows.Forms.Button();
            this.btnLineColor = new System.Windows.Forms.Button();
            this.lblFillPanadapter = new System.Windows.Forms.Label();
            this.udDisplayRefresh = new System.Windows.Forms.NumericUpDown();
            this.lblDisplayRefresh = new System.Windows.Forms.Label();
            this.udScopeTime = new System.Windows.Forms.NumericUpDown();
            this.lblScopeTime = new System.Windows.Forms.Label();
            this.chkWaterfallReverse = new System.Windows.Forms.CheckBox();
            this.lblWaterfallReverse = new System.Windows.Forms.Label();
            this.lblDisplayMode = new System.Windows.Forms.Label();
            this.comboDisplayMode = new System.Windows.Forms.ComboBox();
            this.udDisplayCalOffset = new System.Windows.Forms.NumericUpDown();
            this.lblCalOffset = new System.Windows.Forms.Label();
            this.chkDisplayAveraging = new System.Windows.Forms.CheckBox();
            this.udDisplayHigh = new System.Windows.Forms.NumericUpDown();
            this.lblDisplayHigh = new System.Windows.Forms.Label();
            this.udDisplayLow = new System.Windows.Forms.NumericUpDown();
            this.lblDisplayLow = new System.Windows.Forms.Label();
            this.udAveraging = new System.Windows.Forms.NumericUpDown();
            this.lblAveraging = new System.Windows.Forms.Label();
            this.tabG59 = new System.Windows.Forms.TabPage();
            this.tabRadio = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.udTXSwicthTime = new System.Windows.Forms.NumericUpDown();
            this.lblTXSwichTime = new System.Windows.Forms.Label();
            this.lblRadioModel = new System.Windows.Forms.Label();
            this.comboRadioModel = new System.Windows.Forms.ComboBox();
            this.grpGenesisSi570 = new System.Windows.Forms.GroupBox();
            this.lblSi570I2CAddress = new System.Windows.Forms.Label();
            this.udSi570I2CAddress = new System.Windows.Forms.NumericUpDown();
            this.btnSi570Test = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.udSi570_3 = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.udSi570_2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.udSi570_1 = new System.Windows.Forms.NumericUpDown();
            this.udMsgRptTime = new System.Windows.Forms.NumericUpDown();
            this.lblMsgRpt = new System.Windows.Forms.Label();
            this.chkPA10 = new System.Windows.Forms.CheckBox();
            this.udTXOffDelay = new System.Windows.Forms.NumericUpDown();
            this.chkPTTinv = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkRX2 = new System.Windows.Forms.CheckBox();
            this.udTXIfShift = new System.Windows.Forms.NumericUpDown();
            this.lblSmeterCalOffset = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.udSmeterCalOffset = new System.Windows.Forms.NumericUpDown();
            this.udNBThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblNBThresholds = new System.Windows.Forms.Label();
            this.tabPA10 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udPAGain10m = new System.Windows.Forms.NumericUpDown();
            this.udPAGain6m = new System.Windows.Forms.NumericUpDown();
            this.lblBand160m = new System.Windows.Forms.Label();
            this.lblBand6m = new System.Windows.Forms.Label();
            this.udPAGain160m = new System.Windows.Forms.NumericUpDown();
            this.lblBand80m = new System.Windows.Forms.Label();
            this.lblBand10m = new System.Windows.Forms.Label();
            this.udPAGain80m = new System.Windows.Forms.NumericUpDown();
            this.udPAGain12m = new System.Windows.Forms.NumericUpDown();
            this.lblBand40m = new System.Windows.Forms.Label();
            this.lblBand12m = new System.Windows.Forms.Label();
            this.udPAGain40m = new System.Windows.Forms.NumericUpDown();
            this.udPAGain15m = new System.Windows.Forms.NumericUpDown();
            this.lblBand30m = new System.Windows.Forms.Label();
            this.lblBand15m = new System.Windows.Forms.Label();
            this.udPAGain30m = new System.Windows.Forms.NumericUpDown();
            this.udPAGain17m = new System.Windows.Forms.NumericUpDown();
            this.lblBand20m = new System.Windows.Forms.Label();
            this.lblBand17m = new System.Windows.Forms.Label();
            this.udPAGain20m = new System.Windows.Forms.NumericUpDown();
            this.tabDSP = new System.Windows.Forms.TabPage();
            this.label52 = new System.Windows.Forms.Label();
            this.grpDSPRXImageReject = new System.Windows.Forms.GroupBox();
            this.radIQWBIR = new System.Windows.Forms.RadioButton();
            this.radIQFixed = new System.Windows.Forms.RadioButton();
            this.bttnRXClearAll = new System.Windows.Forms.Button();
            this.bttnRXClearBand = new System.Windows.Forms.Button();
            this.bttnRXCallAll = new System.Windows.Forms.Button();
            this.bttnRXCalBand = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.udRXGain = new System.Windows.Forms.NumericUpDown();
            this.tbRXGain = new System.Windows.Forms.TrackBar();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.udRXPhase = new System.Windows.Forms.NumericUpDown();
            this.tbRXPhase = new System.Windows.Forms.TrackBar();
            this.label47 = new System.Windows.Forms.Label();
            this.grpDSPTXImageReject = new System.Windows.Forms.GroupBox();
            this.bttnTXClearAll = new System.Windows.Forms.Button();
            this.bttnTXClearBand = new System.Windows.Forms.Button();
            this.bttnTXCalAll = new System.Windows.Forms.Button();
            this.bttnTXCalBand = new System.Windows.Forms.Button();
            this.lblGainValue = new System.Windows.Forms.Label();
            this.udTXGain = new System.Windows.Forms.NumericUpDown();
            this.tbTXGain = new System.Windows.Forms.TrackBar();
            this.lblGain = new System.Windows.Forms.Label();
            this.lblPhaseValue = new System.Windows.Forms.Label();
            this.udTXPhase = new System.Windows.Forms.NumericUpDown();
            this.tbTXPhase = new System.Windows.Forms.TrackBar();
            this.lblPhase = new System.Windows.Forms.Label();
            this.comboCWAGC = new System.Windows.Forms.ComboBox();
            this.tabG11 = new System.Windows.Forms.TabPage();
            this.chkG11MultiBand = new System.Windows.Forms.CheckBox();
            this.grpG11BandFilters = new System.Windows.Forms.GroupBox();
            this.chkG11B4030_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B4030_CH2 = new System.Windows.Forms.CheckBox();
            this.label53 = new System.Windows.Forms.Label();
            this.chkG11B6_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B6_CH2 = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.chkG11B151210_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B1210_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B201715_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B1715_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B2017_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B3020_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B6040_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B80_CH1 = new System.Windows.Forms.CheckBox();
            this.chkG11B151210_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B1210_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B201715_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B1715_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B2017_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B3020_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B6040_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B80_CH2 = new System.Windows.Forms.CheckBox();
            this.chkG11B160_CH2 = new System.Windows.Forms.CheckBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCWSettings = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.udCWDebounce = new System.Windows.Forms.NumericUpDown();
            this.label49 = new System.Windows.Forms.Label();
            this.udCWWeight = new System.Windows.Forms.NumericUpDown();
            this.label48 = new System.Windows.Forms.Label();
            this.udCWFall = new System.Windows.Forms.NumericUpDown();
            this.lblCWFall = new System.Windows.Forms.Label();
            this.udCWRise = new System.Windows.Forms.NumericUpDown();
            this.lblCWRise = new System.Windows.Forms.Label();
            this.udG59DASHDOTRatio = new System.Windows.Forms.NumericUpDown();
            this.lblG59dashDotRatio = new System.Windows.Forms.Label();
            this.chkG59IambicRev = new System.Windows.Forms.CheckBox();
            this.chkG59IambicBmode = new System.Windows.Forms.CheckBox();
            this.chkG59Iambic = new System.Windows.Forms.CheckBox();
            this.udCWSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblCWSpeed = new System.Windows.Forms.Label();
            this.udCWPitch = new System.Windows.Forms.NumericUpDown();
            this.lblCWPitch = new System.Windows.Forms.Label();
            this.tabRTTYSettings = new System.Windows.Forms.TabPage();
            this.chkMarkOnly = new System.Windows.Forms.CheckBox();
            this.lblRTTYfreq = new System.Windows.Forms.Label();
            this.udRTTYfreq = new System.Windows.Forms.NumericUpDown();
            this.lblRTTYTXPreamble = new System.Windows.Forms.Label();
            this.udRTTYTXPreamble = new System.Windows.Forms.NumericUpDown();
            this.lblRTTYParity = new System.Windows.Forms.Label();
            this.comboRTTYParity = new System.Windows.Forms.ComboBox();
            this.lblRTTYStopBits = new System.Windows.Forms.Label();
            this.comboRTTYStopBits = new System.Windows.Forms.ComboBox();
            this.lblRTTYshift = new System.Windows.Forms.Label();
            this.tbRTTYCarrierShift = new System.Windows.Forms.TrackBar();
            this.lblBits = new System.Windows.Forms.Label();
            this.comboRTTYBits = new System.Windows.Forms.ComboBox();
            this.lblRTTYCarrierShift = new System.Windows.Forms.Label();
            this.comboRTTYCarrierShift = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.comboRTTYBaudRate = new System.Windows.Forms.ComboBox();
            this.tabPSKsettings = new System.Windows.Forms.TabPage();
            this.lblPSKPreamble = new System.Windows.Forms.Label();
            this.udPSKPreamble = new System.Windows.Forms.NumericUpDown();
            this.udPSKpitch = new System.Windows.Forms.NumericUpDown();
            this.lblPSKpitch = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.txtCWbtn6 = new System.Windows.Forms.TextBox();
            this.txtCWbtn4 = new System.Windows.Forms.TextBox();
            this.txtCWbtn2 = new System.Windows.Forms.TextBox();
            this.txtCWbtn5 = new System.Windows.Forms.TextBox();
            this.txtCWbtn3 = new System.Windows.Forms.TextBox();
            this.txtCWbtn1 = new System.Windows.Forms.TextBox();
            this.lblCWMsg6 = new System.Windows.Forms.Label();
            this.txtCWMsg5 = new System.Windows.Forms.TextBox();
            this.lblCWMsg5 = new System.Windows.Forms.Label();
            this.txtCWMsg6 = new System.Windows.Forms.TextBox();
            this.lblCWMsg4 = new System.Windows.Forms.Label();
            this.txtCWMsg3 = new System.Windows.Forms.TextBox();
            this.lblCWMsg3 = new System.Windows.Forms.Label();
            this.txtCWMsg4 = new System.Windows.Forms.TextBox();
            this.lblCWMsg2 = new System.Windows.Forms.Label();
            this.txtCWMsg1 = new System.Windows.Forms.TextBox();
            this.lblCWMsg1 = new System.Windows.Forms.Label();
            this.txtCWMsg2 = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.txtRTTYbtn6 = new System.Windows.Forms.TextBox();
            this.txtRTTYbtn4 = new System.Windows.Forms.TextBox();
            this.txtRTTYbtn2 = new System.Windows.Forms.TextBox();
            this.txtRTTYbtn5 = new System.Windows.Forms.TextBox();
            this.txtRTTYbtn3 = new System.Windows.Forms.TextBox();
            this.txtRTTYbtn1 = new System.Windows.Forms.TextBox();
            this.lblRTTYMsg6 = new System.Windows.Forms.Label();
            this.txtRTTYMsg5 = new System.Windows.Forms.TextBox();
            this.lblRTTYMsg5 = new System.Windows.Forms.Label();
            this.txtRTTYMsg6 = new System.Windows.Forms.TextBox();
            this.lblRTTYMsg4 = new System.Windows.Forms.Label();
            this.txtRTTYMsg3 = new System.Windows.Forms.TextBox();
            this.lblRTTYMsg3 = new System.Windows.Forms.Label();
            this.txtRTTYMsg4 = new System.Windows.Forms.TextBox();
            this.lblRTTYMsg2 = new System.Windows.Forms.Label();
            this.txtRTTYMsg1 = new System.Windows.Forms.TextBox();
            this.lblRTTYMsg1 = new System.Windows.Forms.Label();
            this.txtRTTYMsg2 = new System.Windows.Forms.TextBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.txtPSKbtn6 = new System.Windows.Forms.TextBox();
            this.txtPSKbtn4 = new System.Windows.Forms.TextBox();
            this.txtPSKbtn2 = new System.Windows.Forms.TextBox();
            this.txtPSKbtn5 = new System.Windows.Forms.TextBox();
            this.txtPSKbtn3 = new System.Windows.Forms.TextBox();
            this.txtPSKbtn1 = new System.Windows.Forms.TextBox();
            this.lblPSKMsg6 = new System.Windows.Forms.Label();
            this.txtPSKMsg5 = new System.Windows.Forms.TextBox();
            this.lblPSKMsg5 = new System.Windows.Forms.Label();
            this.txtPSKMsg6 = new System.Windows.Forms.TextBox();
            this.lblPSKMsg4 = new System.Windows.Forms.Label();
            this.txtPSKMsg3 = new System.Windows.Forms.TextBox();
            this.lblPSKMsg3 = new System.Windows.Forms.Label();
            this.txtPSKMsg4 = new System.Windows.Forms.TextBox();
            this.lblPSKMsg2 = new System.Windows.Forms.Label();
            this.txtPSKMsg1 = new System.Windows.Forms.TextBox();
            this.lblPSKMsg1 = new System.Windows.Forms.Label();
            this.txtPSKMsg2 = new System.Windows.Forms.TextBox();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txtStnZone = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblStnLOC = new System.Windows.Forms.Label();
            this.txtStnLOC = new System.Windows.Forms.TextBox();
            this.lblStnInfoTxt = new System.Windows.Forms.Label();
            this.txtStnCALL = new System.Windows.Forms.TextBox();
            this.lblStnCallSign = new System.Windows.Forms.Label();
            this.txtStnInfoTxt = new System.Windows.Forms.TextBox();
            this.lblStnQTH = new System.Windows.Forms.Label();
            this.txtStnQTH = new System.Windows.Forms.TextBox();
            this.lblStnInfoName = new System.Windows.Forms.Label();
            this.txtStnName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkMRLog = new System.Windows.Forms.CheckBox();
            this.chkMRMedian = new System.Windows.Forms.CheckBox();
            this.chkSDRmode = new System.Windows.Forms.CheckBox();
            this.udTelnetServerPort = new System.Windows.Forms.NumericUpDown();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.chkIPV6 = new System.Windows.Forms.CheckBox();
            this.chkRobot = new System.Windows.Forms.CheckBox();
            this.lblTelnetHostAddress = new System.Windows.Forms.Label();
            this.txtTelnetHostAddress = new System.Windows.Forms.TextBox();
            this.chkTelnet = new System.Windows.Forms.CheckBox();
            this.chkRXOnly = new System.Windows.Forms.CheckBox();
            this.grpAudioTests = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAudioStreamOutputLatencyValuelabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblAudioStreamInputLatencyValue = new System.Windows.Forms.Label();
            this.lblAudioStreamSampleRateValue = new System.Windows.Forms.Label();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label41 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label42 = new System.Windows.Forms.Label();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMonitorFrequncy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAudioOutputVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLatency)).BeginInit();
            this.tbSetup.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpDisplayDriver.SuspendLayout();
            this.grpMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScopeTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayCalOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAveraging)).BeginInit();
            this.tabG59.SuspendLayout();
            this.tabRadio.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTXSwicthTime)).BeginInit();
            this.grpGenesisSi570.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570I2CAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMsgRptTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTXOffDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTXIfShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSmeterCalOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udNBThreshold)).BeginInit();
            this.tabPA10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain10m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain6m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain160m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain80m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain12m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain40m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain15m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain30m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain17m)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain20m)).BeginInit();
            this.tabDSP.SuspendLayout();
            this.grpDSPRXImageReject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRXGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRXGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRXPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRXPhase)).BeginInit();
            this.grpDSPTXImageReject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTXGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTXGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTXPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTXPhase)).BeginInit();
            this.tabG11.SuspendLayout();
            this.grpG11BandFilters.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCWSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCWDebounce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWFall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWRise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udG59DASHDOTRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWPitch)).BeginInit();
            this.tabRTTYSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRTTYfreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRTTYTXPreamble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRTTYCarrierShift)).BeginInit();
            this.tabPSKsettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPSKPreamble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPSKpitch)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTelnetServerPort)).BeginInit();
            this.grpAudioTests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(101, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(228, 350);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.chkQSK);
            this.tabPage1.Controls.Add(this.chkTXSwap);
            this.tabPage1.Controls.Add(this.chkRXSwap);
            this.tabPage1.Controls.Add(this.lblMonitorDriver);
            this.tabPage1.Controls.Add(this.comboMonitorDriver);
            this.tabPage1.Controls.Add(this.lblMonitorFreq);
            this.tabPage1.Controls.Add(this.udMonitorFrequncy);
            this.tabPage1.Controls.Add(this.comboAudioMonitor);
            this.tabPage1.Controls.Add(this.lblAudioMonitor);
            this.tabPage1.Controls.Add(this.lblAudioVolts);
            this.tabPage1.Controls.Add(this.udAudioOutputVoltage);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboAudioBuffer);
            this.tabPage1.Controls.Add(this.comboAudioOutput);
            this.tabPage1.Controls.Add(this.lblAudioOutput);
            this.tabPage1.Controls.Add(this.lblAudioDriver);
            this.tabPage1.Controls.Add(this.lblAudioLatency);
            this.tabPage1.Controls.Add(this.comboAudioDriver);
            this.tabPage1.Controls.Add(this.udLatency);
            this.tabPage1.Controls.Add(this.comboAudioInput);
            this.tabPage1.Controls.Add(this.lblAudioSampleRate);
            this.tabPage1.Controls.Add(this.lblAudioInput);
            this.tabPage1.Controls.Add(this.comboAudioSampleRate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(347, 296);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Audio";
            // 
            // chkQSK
            // 
            this.chkQSK.AutoSize = true;
            this.chkQSK.Location = new System.Drawing.Point(243, 266);
            this.chkQSK.Name = "chkQSK";
            this.chkQSK.Size = new System.Drawing.Size(48, 17);
            this.chkQSK.TabIndex = 24;
            this.chkQSK.Text = "QSK";
            this.chkQSK.UseVisualStyleBackColor = true;
            this.chkQSK.CheckedChanged += new System.EventHandler(this.chkQSK_CheckedChanged);
            // 
            // chkTXSwap
            // 
            this.chkTXSwap.AutoSize = true;
            this.chkTXSwap.Location = new System.Drawing.Point(150, 266);
            this.chkTXSwap.Name = "chkTXSwap";
            this.chkTXSwap.Size = new System.Drawing.Size(87, 17);
            this.chkTXSwap.TabIndex = 23;
            this.chkTXSwap.Text = "TX swap I/Q";
            this.chkTXSwap.UseVisualStyleBackColor = true;
            this.chkTXSwap.CheckedChanged += new System.EventHandler(this.chkTXSwap_CheckedChanged);
            // 
            // chkRXSwap
            // 
            this.chkRXSwap.AutoSize = true;
            this.chkRXSwap.Location = new System.Drawing.Point(56, 266);
            this.chkRXSwap.Name = "chkRXSwap";
            this.chkRXSwap.Size = new System.Drawing.Size(88, 17);
            this.chkRXSwap.TabIndex = 22;
            this.chkRXSwap.Text = "RX swap I/Q";
            this.chkRXSwap.UseVisualStyleBackColor = true;
            this.chkRXSwap.CheckedChanged += new System.EventHandler(this.chkRXSwap_CheckedChanged);
            // 
            // lblMonitorDriver
            // 
            this.lblMonitorDriver.AutoSize = true;
            this.lblMonitorDriver.Location = new System.Drawing.Point(50, 95);
            this.lblMonitorDriver.Name = "lblMonitorDriver";
            this.lblMonitorDriver.Size = new System.Drawing.Size(70, 13);
            this.lblMonitorDriver.TabIndex = 21;
            this.lblMonitorDriver.Text = "MonitorDriver";
            // 
            // comboMonitorDriver
            // 
            this.comboMonitorDriver.FormattingEnabled = true;
            this.comboMonitorDriver.Location = new System.Drawing.Point(130, 92);
            this.comboMonitorDriver.Name = "comboMonitorDriver";
            this.comboMonitorDriver.Size = new System.Drawing.Size(167, 21);
            this.comboMonitorDriver.TabIndex = 20;
            this.comboMonitorDriver.SelectedIndexChanged += new System.EventHandler(this.comboMonitorDriver_SelectedIndexChanged);
            // 
            // lblMonitorFreq
            // 
            this.lblMonitorFreq.AutoSize = true;
            this.lblMonitorFreq.Location = new System.Drawing.Point(50, 144);
            this.lblMonitorFreq.Name = "lblMonitorFreq";
            this.lblMonitorFreq.Size = new System.Drawing.Size(92, 13);
            this.lblMonitorFreq.TabIndex = 19;
            this.lblMonitorFreq.Text = "Monitor frequency";
            // 
            // udMonitorFrequncy
            // 
            this.udMonitorFrequncy.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udMonitorFrequncy.Location = new System.Drawing.Point(195, 142);
            this.udMonitorFrequncy.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.udMonitorFrequncy.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udMonitorFrequncy.Name = "udMonitorFrequncy";
            this.udMonitorFrequncy.Size = new System.Drawing.Size(56, 20);
            this.udMonitorFrequncy.TabIndex = 18;
            this.udMonitorFrequncy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udMonitorFrequncy.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.udMonitorFrequncy.ValueChanged += new System.EventHandler(this.udMonitorFrequncy_ValueChanged);
            // 
            // comboAudioMonitor
            // 
            this.comboAudioMonitor.FormattingEnabled = true;
            this.comboAudioMonitor.Location = new System.Drawing.Point(130, 117);
            this.comboAudioMonitor.Name = "comboAudioMonitor";
            this.comboAudioMonitor.Size = new System.Drawing.Size(167, 21);
            this.comboAudioMonitor.TabIndex = 16;
            this.comboAudioMonitor.SelectedIndexChanged += new System.EventHandler(this.comboAudioMonitor_SelectedIndexChanged);
            // 
            // lblAudioMonitor
            // 
            this.lblAudioMonitor.AutoSize = true;
            this.lblAudioMonitor.Location = new System.Drawing.Point(50, 120);
            this.lblAudioMonitor.Name = "lblAudioMonitor";
            this.lblAudioMonitor.Size = new System.Drawing.Size(42, 13);
            this.lblAudioMonitor.TabIndex = 17;
            this.lblAudioMonitor.Text = "Monitor";
            // 
            // lblAudioVolts
            // 
            this.lblAudioVolts.AutoSize = true;
            this.lblAudioVolts.Location = new System.Drawing.Point(50, 242);
            this.lblAudioVolts.Name = "lblAudioVolts";
            this.lblAudioVolts.Size = new System.Drawing.Size(105, 13);
            this.lblAudioVolts.TabIndex = 15;
            this.lblAudioVolts.Text = "Audio output voltage";
            // 
            // udAudioOutputVoltage
            // 
            this.udAudioOutputVoltage.DecimalPlaces = 2;
            this.udAudioOutputVoltage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.udAudioOutputVoltage.Location = new System.Drawing.Point(195, 240);
            this.udAudioOutputVoltage.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udAudioOutputVoltage.Name = "udAudioOutputVoltage";
            this.udAudioOutputVoltage.Size = new System.Drawing.Size(56, 20);
            this.udAudioOutputVoltage.TabIndex = 14;
            this.udAudioOutputVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udAudioOutputVoltage.Value = new decimal(new int[] {
            223,
            0,
            0,
            131072});
            this.udAudioOutputVoltage.ValueChanged += new System.EventHandler(this.udAudioOutputVoltage_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Buffer size";
            // 
            // comboAudioBuffer
            // 
            this.comboAudioBuffer.FormattingEnabled = true;
            this.comboAudioBuffer.Items.AddRange(new object[] {
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.comboAudioBuffer.Location = new System.Drawing.Point(172, 191);
            this.comboAudioBuffer.Name = "comboAudioBuffer";
            this.comboAudioBuffer.Size = new System.Drawing.Size(79, 21);
            this.comboAudioBuffer.TabIndex = 12;
            this.comboAudioBuffer.SelectedIndexChanged += new System.EventHandler(this.comboAudioBuffer_SelectedIndexChanged);
            // 
            // comboAudioOutput
            // 
            this.comboAudioOutput.FormattingEnabled = true;
            this.comboAudioOutput.Location = new System.Drawing.Point(130, 67);
            this.comboAudioOutput.Name = "comboAudioOutput";
            this.comboAudioOutput.Size = new System.Drawing.Size(167, 21);
            this.comboAudioOutput.TabIndex = 10;
            this.comboAudioOutput.SelectedIndexChanged += new System.EventHandler(this.comboAudioOutput_SelectedIndexChanged);
            // 
            // lblAudioOutput
            // 
            this.lblAudioOutput.AutoSize = true;
            this.lblAudioOutput.Location = new System.Drawing.Point(50, 70);
            this.lblAudioOutput.Name = "lblAudioOutput";
            this.lblAudioOutput.Size = new System.Drawing.Size(39, 13);
            this.lblAudioOutput.TabIndex = 11;
            this.lblAudioOutput.Text = "Output";
            // 
            // lblAudioDriver
            // 
            this.lblAudioDriver.AutoSize = true;
            this.lblAudioDriver.Location = new System.Drawing.Point(50, 20);
            this.lblAudioDriver.Name = "lblAudioDriver";
            this.lblAudioDriver.Size = new System.Drawing.Size(62, 13);
            this.lblAudioDriver.TabIndex = 4;
            this.lblAudioDriver.Text = "AudioDriver";
            // 
            // lblAudioLatency
            // 
            this.lblAudioLatency.AutoSize = true;
            this.lblAudioLatency.Location = new System.Drawing.Point(50, 218);
            this.lblAudioLatency.Name = "lblAudioLatency";
            this.lblAudioLatency.Size = new System.Drawing.Size(45, 13);
            this.lblAudioLatency.TabIndex = 9;
            this.lblAudioLatency.Text = "Latency";
            // 
            // comboAudioDriver
            // 
            this.comboAudioDriver.FormattingEnabled = true;
            this.comboAudioDriver.Location = new System.Drawing.Point(130, 17);
            this.comboAudioDriver.Name = "comboAudioDriver";
            this.comboAudioDriver.Size = new System.Drawing.Size(167, 21);
            this.comboAudioDriver.TabIndex = 2;
            this.comboAudioDriver.SelectedIndexChanged += new System.EventHandler(this.comboAudioDriver_SelectedIndexChanged);
            // 
            // udLatency
            // 
            this.udLatency.Location = new System.Drawing.Point(195, 216);
            this.udLatency.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udLatency.Name = "udLatency";
            this.udLatency.Size = new System.Drawing.Size(56, 20);
            this.udLatency.TabIndex = 8;
            this.udLatency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udLatency.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udLatency.ValueChanged += new System.EventHandler(this.udLatency_ValueChanged);
            // 
            // comboAudioInput
            // 
            this.comboAudioInput.FormattingEnabled = true;
            this.comboAudioInput.Location = new System.Drawing.Point(130, 42);
            this.comboAudioInput.Name = "comboAudioInput";
            this.comboAudioInput.Size = new System.Drawing.Size(167, 21);
            this.comboAudioInput.TabIndex = 3;
            this.comboAudioInput.SelectedIndexChanged += new System.EventHandler(this.comboAudioInput_SelectedIndexChanged);
            // 
            // lblAudioSampleRate
            // 
            this.lblAudioSampleRate.AutoSize = true;
            this.lblAudioSampleRate.Location = new System.Drawing.Point(50, 169);
            this.lblAudioSampleRate.Name = "lblAudioSampleRate";
            this.lblAudioSampleRate.Size = new System.Drawing.Size(63, 13);
            this.lblAudioSampleRate.TabIndex = 7;
            this.lblAudioSampleRate.Text = "Sample rate";
            // 
            // lblAudioInput
            // 
            this.lblAudioInput.AutoSize = true;
            this.lblAudioInput.Location = new System.Drawing.Point(50, 45);
            this.lblAudioInput.Name = "lblAudioInput";
            this.lblAudioInput.Size = new System.Drawing.Size(31, 13);
            this.lblAudioInput.TabIndex = 5;
            this.lblAudioInput.Text = "Input";
            // 
            // comboAudioSampleRate
            // 
            this.comboAudioSampleRate.FormattingEnabled = true;
            this.comboAudioSampleRate.Items.AddRange(new object[] {
            "6000",
            "8000",
            "11025",
            "12000",
            "22050",
            "24000",
            "44100",
            "48000",
            "96000",
            "192000"});
            this.comboAudioSampleRate.Location = new System.Drawing.Point(172, 166);
            this.comboAudioSampleRate.Name = "comboAudioSampleRate";
            this.comboAudioSampleRate.Size = new System.Drawing.Size(79, 21);
            this.comboAudioSampleRate.TabIndex = 6;
            this.comboAudioSampleRate.SelectedIndexChanged += new System.EventHandler(this.comboAudioSampleRate_SelectedIndexChanged);
            // 
            // tbSetup
            // 
            this.tbSetup.Controls.Add(this.tabPage1);
            this.tbSetup.Controls.Add(this.tabPage3);
            this.tbSetup.Controls.Add(this.tabG59);
            this.tbSetup.Controls.Add(this.tabPage5);
            this.tbSetup.Controls.Add(this.tabPage6);
            this.tbSetup.Controls.Add(this.tabPage2);
            this.tbSetup.Location = new System.Drawing.Point(25, 12);
            this.tbSetup.Name = "tbSetup";
            this.tbSetup.SelectedIndex = 0;
            this.tbSetup.Size = new System.Drawing.Size(355, 322);
            this.tbSetup.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.grpDisplayDriver);
            this.tabPage3.Controls.Add(this.grpMisc);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(347, 296);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Display";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lblMonitorMode);
            this.groupBox7.Controls.Add(this.comboMonitorMode);
            this.groupBox7.Location = new System.Drawing.Point(196, 230);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(145, 52);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Monitor";
            // 
            // lblMonitorMode
            // 
            this.lblMonitorMode.AutoSize = true;
            this.lblMonitorMode.Location = new System.Drawing.Point(6, 24);
            this.lblMonitorMode.Name = "lblMonitorMode";
            this.lblMonitorMode.Size = new System.Drawing.Size(34, 13);
            this.lblMonitorMode.TabIndex = 30;
            this.lblMonitorMode.Text = "Mode";
            // 
            // comboMonitorMode
            // 
            this.comboMonitorMode.FormattingEnabled = true;
            this.comboMonitorMode.Items.AddRange(new object[] {
            "Waterfall",
            "Scope",
            "Panadapter"});
            this.comboMonitorMode.Location = new System.Drawing.Point(46, 21);
            this.comboMonitorMode.Name = "comboMonitorMode";
            this.comboMonitorMode.Size = new System.Drawing.Size(93, 21);
            this.comboMonitorMode.TabIndex = 29;
            this.comboMonitorMode.SelectedIndexChanged += new System.EventHandler(this.comboMonitorMode_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radDirectXSW);
            this.groupBox3.Controls.Add(this.radDirectXHW);
            this.groupBox3.Location = new System.Drawing.Point(222, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(108, 102);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DirectX type";
            // 
            // radDirectXSW
            // 
            this.radDirectXSW.AutoSize = true;
            this.radDirectXSW.Location = new System.Drawing.Point(19, 68);
            this.radDirectXSW.Name = "radDirectXSW";
            this.radDirectXSW.Size = new System.Drawing.Size(67, 17);
            this.radDirectXSW.TabIndex = 1;
            this.radDirectXSW.Text = "Software";
            this.radDirectXSW.UseVisualStyleBackColor = true;
            this.radDirectXSW.CheckedChanged += new System.EventHandler(this.radDirectXSW_CheckedChanged);
            // 
            // radDirectXHW
            // 
            this.radDirectXHW.AutoSize = true;
            this.radDirectXHW.Checked = true;
            this.radDirectXHW.Location = new System.Drawing.Point(19, 26);
            this.radDirectXHW.Name = "radDirectXHW";
            this.radDirectXHW.Size = new System.Drawing.Size(71, 17);
            this.radDirectXHW.TabIndex = 0;
            this.radDirectXHW.TabStop = true;
            this.radDirectXHW.Text = "Hardware";
            this.radDirectXHW.UseVisualStyleBackColor = true;
            this.radDirectXHW.CheckedChanged += new System.EventHandler(this.radDirectXHW_CheckedChanged);
            // 
            // grpDisplayDriver
            // 
            this.grpDisplayDriver.Controls.Add(this.radDisplayDirectX);
            this.grpDisplayDriver.Controls.Add(this.radDisplayGDI);
            this.grpDisplayDriver.Location = new System.Drawing.Point(218, 21);
            this.grpDisplayDriver.Name = "grpDisplayDriver";
            this.grpDisplayDriver.Size = new System.Drawing.Size(113, 82);
            this.grpDisplayDriver.TabIndex = 11;
            this.grpDisplayDriver.TabStop = false;
            this.grpDisplayDriver.Text = "Driver";
            // 
            // radDisplayDirectX
            // 
            this.radDisplayDirectX.AutoSize = true;
            this.radDisplayDirectX.Location = new System.Drawing.Point(26, 47);
            this.radDisplayDirectX.Name = "radDisplayDirectX";
            this.radDisplayDirectX.Size = new System.Drawing.Size(60, 17);
            this.radDisplayDirectX.TabIndex = 1;
            this.radDisplayDirectX.TabStop = true;
            this.radDisplayDirectX.Text = "DirectX";
            this.radDisplayDirectX.UseVisualStyleBackColor = true;
            this.radDisplayDirectX.CheckedChanged += new System.EventHandler(this.radDisplayDirectX_CheckedChanged);
            // 
            // radDisplayGDI
            // 
            this.radDisplayGDI.AutoSize = true;
            this.radDisplayGDI.Checked = true;
            this.radDisplayGDI.Location = new System.Drawing.Point(26, 23);
            this.radDisplayGDI.Name = "radDisplayGDI";
            this.radDisplayGDI.Size = new System.Drawing.Size(50, 17);
            this.radDisplayGDI.TabIndex = 0;
            this.radDisplayGDI.TabStop = true;
            this.radDisplayGDI.Text = "GDI+";
            this.radDisplayGDI.UseVisualStyleBackColor = true;
            this.radDisplayGDI.CheckedChanged += new System.EventHandler(this.radDisplayGDI_CheckedChanged);
            // 
            // grpMisc
            // 
            this.grpMisc.Controls.Add(this.label43);
            this.grpMisc.Controls.Add(this.chkFillPanadapter);
            this.grpMisc.Controls.Add(this.label34);
            this.grpMisc.Controls.Add(this.btnFillColor);
            this.grpMisc.Controls.Add(this.btnLineColor);
            this.grpMisc.Controls.Add(this.lblFillPanadapter);
            this.grpMisc.Controls.Add(this.udDisplayRefresh);
            this.grpMisc.Controls.Add(this.lblDisplayRefresh);
            this.grpMisc.Controls.Add(this.udScopeTime);
            this.grpMisc.Controls.Add(this.lblScopeTime);
            this.grpMisc.Controls.Add(this.chkWaterfallReverse);
            this.grpMisc.Controls.Add(this.lblWaterfallReverse);
            this.grpMisc.Controls.Add(this.lblDisplayMode);
            this.grpMisc.Controls.Add(this.comboDisplayMode);
            this.grpMisc.Controls.Add(this.udDisplayCalOffset);
            this.grpMisc.Controls.Add(this.lblCalOffset);
            this.grpMisc.Controls.Add(this.chkDisplayAveraging);
            this.grpMisc.Controls.Add(this.udDisplayHigh);
            this.grpMisc.Controls.Add(this.lblDisplayHigh);
            this.grpMisc.Controls.Add(this.udDisplayLow);
            this.grpMisc.Controls.Add(this.lblDisplayLow);
            this.grpMisc.Controls.Add(this.udAveraging);
            this.grpMisc.Controls.Add(this.lblAveraging);
            this.grpMisc.Location = new System.Drawing.Point(15, 6);
            this.grpMisc.Name = "grpMisc";
            this.grpMisc.Size = new System.Drawing.Size(175, 284);
            this.grpMisc.TabIndex = 10;
            this.grpMisc.TabStop = false;
            this.grpMisc.Text = "Display settings";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(16, 241);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(48, 13);
            this.label43.TabIndex = 28;
            this.label43.Text = "Fill color:";
            // 
            // chkFillPanadapter
            // 
            this.chkFillPanadapter.AutoSize = true;
            this.chkFillPanadapter.Location = new System.Drawing.Point(117, 263);
            this.chkFillPanadapter.Name = "chkFillPanadapter";
            this.chkFillPanadapter.Size = new System.Drawing.Size(15, 14);
            this.chkFillPanadapter.TabIndex = 26;
            this.chkFillPanadapter.UseVisualStyleBackColor = true;
            this.chkFillPanadapter.CheckedChanged += new System.EventHandler(this.chkFillPanadapter_CheckedChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(16, 219);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(56, 13);
            this.label34.TabIndex = 24;
            this.label34.Text = "Line color:";
            // 
            // btnFillColor
            // 
            this.btnFillColor.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnFillColor.Location = new System.Drawing.Point(109, 239);
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.Size = new System.Drawing.Size(38, 20);
            this.btnFillColor.TabIndex = 27;
            this.btnFillColor.UseVisualStyleBackColor = false;
            this.btnFillColor.Click += new System.EventHandler(this.btnFillColor_Click);
            // 
            // btnLineColor
            // 
            this.btnLineColor.BackColor = System.Drawing.Color.White;
            this.btnLineColor.Location = new System.Drawing.Point(109, 216);
            this.btnLineColor.Name = "btnLineColor";
            this.btnLineColor.Size = new System.Drawing.Size(38, 20);
            this.btnLineColor.TabIndex = 13;
            this.btnLineColor.UseVisualStyleBackColor = false;
            this.btnLineColor.Click += new System.EventHandler(this.btnLineColor_Click);
            // 
            // lblFillPanadapter
            // 
            this.lblFillPanadapter.AutoSize = true;
            this.lblFillPanadapter.Location = new System.Drawing.Point(16, 263);
            this.lblFillPanadapter.Name = "lblFillPanadapter";
            this.lblFillPanadapter.Size = new System.Drawing.Size(77, 13);
            this.lblFillPanadapter.TabIndex = 25;
            this.lblFillPanadapter.Text = "Fill Panadapter";
            // 
            // udDisplayRefresh
            // 
            this.udDisplayRefresh.DecimalPlaces = 1;
            this.udDisplayRefresh.Location = new System.Drawing.Point(103, 190);
            this.udDisplayRefresh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udDisplayRefresh.Name = "udDisplayRefresh";
            this.udDisplayRefresh.Size = new System.Drawing.Size(51, 20);
            this.udDisplayRefresh.TabIndex = 22;
            this.udDisplayRefresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udDisplayRefresh.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udDisplayRefresh.ValueChanged += new System.EventHandler(this.udDisplayRefresh_ValueChanged);
            // 
            // lblDisplayRefresh
            // 
            this.lblDisplayRefresh.AutoSize = true;
            this.lblDisplayRefresh.Location = new System.Drawing.Point(16, 192);
            this.lblDisplayRefresh.Name = "lblDisplayRefresh";
            this.lblDisplayRefresh.Size = new System.Drawing.Size(71, 13);
            this.lblDisplayRefresh.TabIndex = 23;
            this.lblDisplayRefresh.Text = "Refresh (mS):";
            // 
            // udScopeTime
            // 
            this.udScopeTime.DecimalPlaces = 1;
            this.udScopeTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udScopeTime.Location = new System.Drawing.Point(103, 164);
            this.udScopeTime.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.udScopeTime.Name = "udScopeTime";
            this.udScopeTime.Size = new System.Drawing.Size(51, 20);
            this.udScopeTime.TabIndex = 20;
            this.udScopeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udScopeTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udScopeTime.ValueChanged += new System.EventHandler(this.udScopeTime_ValueChanged);
            // 
            // lblScopeTime
            // 
            this.lblScopeTime.AutoSize = true;
            this.lblScopeTime.Location = new System.Drawing.Point(16, 166);
            this.lblScopeTime.Name = "lblScopeTime";
            this.lblScopeTime.Size = new System.Drawing.Size(63, 13);
            this.lblScopeTime.TabIndex = 21;
            this.lblScopeTime.Text = "Scope time:";
            // 
            // chkWaterfallReverse
            // 
            this.chkWaterfallReverse.AutoSize = true;
            this.chkWaterfallReverse.Location = new System.Drawing.Point(117, 144);
            this.chkWaterfallReverse.Name = "chkWaterfallReverse";
            this.chkWaterfallReverse.Size = new System.Drawing.Size(15, 14);
            this.chkWaterfallReverse.TabIndex = 19;
            this.chkWaterfallReverse.UseVisualStyleBackColor = true;
            this.chkWaterfallReverse.CheckedChanged += new System.EventHandler(this.chkWaterfallReverse_CheckedChanged);
            // 
            // lblWaterfallReverse
            // 
            this.lblWaterfallReverse.AutoSize = true;
            this.lblWaterfallReverse.Location = new System.Drawing.Point(16, 144);
            this.lblWaterfallReverse.Name = "lblWaterfallReverse";
            this.lblWaterfallReverse.Size = new System.Drawing.Size(87, 13);
            this.lblWaterfallReverse.TabIndex = 18;
            this.lblWaterfallReverse.Text = "Waterfall reverse";
            // 
            // lblDisplayMode
            // 
            this.lblDisplayMode.AutoSize = true;
            this.lblDisplayMode.Location = new System.Drawing.Point(16, 120);
            this.lblDisplayMode.Name = "lblDisplayMode";
            this.lblDisplayMode.Size = new System.Drawing.Size(34, 13);
            this.lblDisplayMode.TabIndex = 17;
            this.lblDisplayMode.Text = "Mode";
            // 
            // comboDisplayMode
            // 
            this.comboDisplayMode.FormattingEnabled = true;
            this.comboDisplayMode.Items.AddRange(new object[] {
            "Panafall",
            "Panafall_inv",
            "Panascope",
            "Panascope_inv"});
            this.comboDisplayMode.Location = new System.Drawing.Point(61, 117);
            this.comboDisplayMode.Name = "comboDisplayMode";
            this.comboDisplayMode.Size = new System.Drawing.Size(93, 21);
            this.comboDisplayMode.TabIndex = 16;
            this.comboDisplayMode.SelectedIndexChanged += new System.EventHandler(this.comboDisplayMode_SelectedIndexChanged);
            // 
            // udDisplayCalOffset
            // 
            this.udDisplayCalOffset.DecimalPlaces = 1;
            this.udDisplayCalOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udDisplayCalOffset.Location = new System.Drawing.Point(103, 91);
            this.udDisplayCalOffset.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udDisplayCalOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udDisplayCalOffset.Name = "udDisplayCalOffset";
            this.udDisplayCalOffset.Size = new System.Drawing.Size(51, 20);
            this.udDisplayCalOffset.TabIndex = 14;
            this.udDisplayCalOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udDisplayCalOffset.Value = new decimal(new int[] {
            351,
            0,
            0,
            -2147418112});
            this.udDisplayCalOffset.ValueChanged += new System.EventHandler(this.udDisplayCalOffset_ValueChanged);
            // 
            // lblCalOffset
            // 
            this.lblCalOffset.AutoSize = true;
            this.lblCalOffset.Location = new System.Drawing.Point(15, 93);
            this.lblCalOffset.Name = "lblCalOffset";
            this.lblCalOffset.Size = new System.Drawing.Size(54, 13);
            this.lblCalOffset.TabIndex = 15;
            this.lblCalOffset.Text = "Cal. offset";
            // 
            // chkDisplayAveraging
            // 
            this.chkDisplayAveraging.AutoSize = true;
            this.chkDisplayAveraging.Location = new System.Drawing.Point(77, 67);
            this.chkDisplayAveraging.Name = "chkDisplayAveraging";
            this.chkDisplayAveraging.Size = new System.Drawing.Size(15, 14);
            this.chkDisplayAveraging.TabIndex = 13;
            this.chkDisplayAveraging.UseVisualStyleBackColor = true;
            this.chkDisplayAveraging.CheckedChanged += new System.EventHandler(this.chkDisplayAveraging_CheckedChanged);
            // 
            // udDisplayHigh
            // 
            this.udDisplayHigh.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udDisplayHigh.Location = new System.Drawing.Point(103, 17);
            this.udDisplayHigh.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.udDisplayHigh.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.udDisplayHigh.Name = "udDisplayHigh";
            this.udDisplayHigh.Size = new System.Drawing.Size(51, 20);
            this.udDisplayHigh.TabIndex = 11;
            this.udDisplayHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udDisplayHigh.Value = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.udDisplayHigh.ValueChanged += new System.EventHandler(this.udDisplayHigh_ValueChanged);
            // 
            // lblDisplayHigh
            // 
            this.lblDisplayHigh.AutoSize = true;
            this.lblDisplayHigh.Location = new System.Drawing.Point(16, 19);
            this.lblDisplayHigh.Name = "lblDisplayHigh";
            this.lblDisplayHigh.Size = new System.Drawing.Size(54, 13);
            this.lblDisplayHigh.TabIndex = 12;
            this.lblDisplayHigh.Text = "High level";
            // 
            // udDisplayLow
            // 
            this.udDisplayLow.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udDisplayLow.Location = new System.Drawing.Point(103, 40);
            this.udDisplayLow.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.udDisplayLow.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.udDisplayLow.Name = "udDisplayLow";
            this.udDisplayLow.Size = new System.Drawing.Size(51, 20);
            this.udDisplayLow.TabIndex = 9;
            this.udDisplayLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udDisplayLow.Value = new decimal(new int[] {
            150,
            0,
            0,
            -2147483648});
            this.udDisplayLow.ValueChanged += new System.EventHandler(this.udDisplayLow_ValueChanged);
            // 
            // lblDisplayLow
            // 
            this.lblDisplayLow.AutoSize = true;
            this.lblDisplayLow.Location = new System.Drawing.Point(15, 42);
            this.lblDisplayLow.Name = "lblDisplayLow";
            this.lblDisplayLow.Size = new System.Drawing.Size(52, 13);
            this.lblDisplayLow.TabIndex = 10;
            this.lblDisplayLow.Text = "Low level";
            // 
            // udAveraging
            // 
            this.udAveraging.Location = new System.Drawing.Point(103, 65);
            this.udAveraging.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udAveraging.Name = "udAveraging";
            this.udAveraging.Size = new System.Drawing.Size(51, 20);
            this.udAveraging.TabIndex = 7;
            this.udAveraging.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udAveraging.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udAveraging.ValueChanged += new System.EventHandler(this.udAveraging_ValueChanged);
            // 
            // lblAveraging
            // 
            this.lblAveraging.AutoSize = true;
            this.lblAveraging.Location = new System.Drawing.Point(16, 67);
            this.lblAveraging.Name = "lblAveraging";
            this.lblAveraging.Size = new System.Drawing.Size(55, 13);
            this.lblAveraging.TabIndex = 8;
            this.lblAveraging.Text = "Averaging";
            // 
            // tabG59
            // 
            this.tabG59.BackColor = System.Drawing.SystemColors.Control;
            this.tabG59.Controls.Add(this.tabRadio);
            this.tabG59.Location = new System.Drawing.Point(4, 22);
            this.tabG59.Name = "tabG59";
            this.tabG59.Padding = new System.Windows.Forms.Padding(3);
            this.tabG59.Size = new System.Drawing.Size(347, 296);
            this.tabG59.TabIndex = 3;
            this.tabG59.Text = "Radio";
            // 
            // tabRadio
            // 
            this.tabRadio.Controls.Add(this.tabPage4);
            this.tabRadio.Controls.Add(this.tabPA10);
            this.tabRadio.Controls.Add(this.tabDSP);
            this.tabRadio.Controls.Add(this.tabG11);
            this.tabRadio.Location = new System.Drawing.Point(0, 3);
            this.tabRadio.Name = "tabRadio";
            this.tabRadio.SelectedIndex = 0;
            this.tabRadio.Size = new System.Drawing.Size(347, 290);
            this.tabRadio.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.udTXSwicthTime);
            this.tabPage4.Controls.Add(this.lblTXSwichTime);
            this.tabPage4.Controls.Add(this.lblRadioModel);
            this.tabPage4.Controls.Add(this.comboRadioModel);
            this.tabPage4.Controls.Add(this.grpGenesisSi570);
            this.tabPage4.Controls.Add(this.udMsgRptTime);
            this.tabPage4.Controls.Add(this.lblMsgRpt);
            this.tabPage4.Controls.Add(this.chkPA10);
            this.tabPage4.Controls.Add(this.udTXOffDelay);
            this.tabPage4.Controls.Add(this.chkPTTinv);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.chkRX2);
            this.tabPage4.Controls.Add(this.udTXIfShift);
            this.tabPage4.Controls.Add(this.lblSmeterCalOffset);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.udSmeterCalOffset);
            this.tabPage4.Controls.Add(this.udNBThreshold);
            this.tabPage4.Controls.Add(this.lblNBThresholds);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(339, 264);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Genesis";
            // 
            // udTXSwicthTime
            // 
            this.udTXSwicthTime.Increment = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.udTXSwicthTime.Location = new System.Drawing.Point(97, 233);
            this.udTXSwicthTime.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udTXSwicthTime.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.udTXSwicthTime.Name = "udTXSwicthTime";
            this.udTXSwicthTime.Size = new System.Drawing.Size(57, 20);
            this.udTXSwicthTime.TabIndex = 34;
            this.udTXSwicthTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udTXSwicthTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udTXSwicthTime.ValueChanged += new System.EventHandler(this.udTXSwicthTime_ValueChanged);
            // 
            // lblTXSwichTime
            // 
            this.lblTXSwichTime.AutoSize = true;
            this.lblTXSwichTime.Location = new System.Drawing.Point(8, 236);
            this.lblTXSwichTime.Name = "lblTXSwichTime";
            this.lblTXSwichTime.Size = new System.Drawing.Size(76, 13);
            this.lblTXSwichTime.TabIndex = 35;
            this.lblTXSwichTime.Text = "TX swicth time";
            // 
            // lblRadioModel
            // 
            this.lblRadioModel.AutoSize = true;
            this.lblRadioModel.Location = new System.Drawing.Point(67, 11);
            this.lblRadioModel.Name = "lblRadioModel";
            this.lblRadioModel.Size = new System.Drawing.Size(67, 13);
            this.lblRadioModel.TabIndex = 33;
            this.lblRadioModel.Text = "Radio Model";
            // 
            // comboRadioModel
            // 
            this.comboRadioModel.FormattingEnabled = true;
            this.comboRadioModel.Items.AddRange(new object[] {
            "Genesis G59",
            "Genesis G11"});
            this.comboRadioModel.Location = new System.Drawing.Point(150, 8);
            this.comboRadioModel.Name = "comboRadioModel";
            this.comboRadioModel.Size = new System.Drawing.Size(121, 21);
            this.comboRadioModel.TabIndex = 32;
            this.comboRadioModel.SelectedIndexChanged += new System.EventHandler(this.comboRadioModel_SelectedIndexChanged);
            // 
            // grpGenesisSi570
            // 
            this.grpGenesisSi570.Controls.Add(this.lblSi570I2CAddress);
            this.grpGenesisSi570.Controls.Add(this.udSi570I2CAddress);
            this.grpGenesisSi570.Controls.Add(this.btnSi570Test);
            this.grpGenesisSi570.Controls.Add(this.label15);
            this.grpGenesisSi570.Controls.Add(this.udSi570_3);
            this.grpGenesisSi570.Controls.Add(this.label14);
            this.grpGenesisSi570.Controls.Add(this.udSi570_2);
            this.grpGenesisSi570.Controls.Add(this.label8);
            this.grpGenesisSi570.Controls.Add(this.udSi570_1);
            this.grpGenesisSi570.Location = new System.Drawing.Point(160, 41);
            this.grpGenesisSi570.Name = "grpGenesisSi570";
            this.grpGenesisSi570.Size = new System.Drawing.Size(173, 212);
            this.grpGenesisSi570.TabIndex = 31;
            this.grpGenesisSi570.TabStop = false;
            this.grpGenesisSi570.Text = "Si570 settings";
            // 
            // lblSi570I2CAddress
            // 
            this.lblSi570I2CAddress.AutoSize = true;
            this.lblSi570I2CAddress.Location = new System.Drawing.Point(7, 124);
            this.lblSi570I2CAddress.Name = "lblSi570I2CAddress";
            this.lblSi570I2CAddress.Size = new System.Drawing.Size(63, 13);
            this.lblSi570I2CAddress.TabIndex = 10;
            this.lblSi570I2CAddress.Text = "I2C address";
            // 
            // udSi570I2CAddress
            // 
            this.udSi570I2CAddress.Location = new System.Drawing.Point(121, 122);
            this.udSi570I2CAddress.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udSi570I2CAddress.Name = "udSi570I2CAddress";
            this.udSi570I2CAddress.Size = new System.Drawing.Size(44, 20);
            this.udSi570I2CAddress.TabIndex = 9;
            this.udSi570I2CAddress.Value = new decimal(new int[] {
            170,
            0,
            0,
            0});
            this.udSi570I2CAddress.ValueChanged += new System.EventHandler(this.udSi570I2CAddress_ValueChanged);
            // 
            // btnSi570Test
            // 
            this.btnSi570Test.Location = new System.Drawing.Point(49, 173);
            this.btnSi570Test.Name = "btnSi570Test";
            this.btnSi570Test.Size = new System.Drawing.Size(75, 23);
            this.btnSi570Test.TabIndex = 8;
            this.btnSi570Test.Text = "Test";
            this.btnSi570Test.UseVisualStyleBackColor = true;
            this.btnSi570Test.Click += new System.EventHandler(this.btnSi570Test_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "30-50MHz";
            // 
            // udSi570_3
            // 
            this.udSi570_3.DecimalPlaces = 1;
            this.udSi570_3.Location = new System.Drawing.Point(71, 96);
            this.udSi570_3.Maximum = new decimal(new int[] {
            120000000,
            0,
            0,
            0});
            this.udSi570_3.Minimum = new decimal(new int[] {
            110000000,
            0,
            0,
            0});
            this.udSi570_3.Name = "udSi570_3";
            this.udSi570_3.Size = new System.Drawing.Size(94, 20);
            this.udSi570_3.TabIndex = 6;
            this.udSi570_3.Value = new decimal(new int[] {
            114285000,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "17-30MHz";
            // 
            // udSi570_2
            // 
            this.udSi570_2.DecimalPlaces = 1;
            this.udSi570_2.Location = new System.Drawing.Point(71, 69);
            this.udSi570_2.Maximum = new decimal(new int[] {
            120000000,
            0,
            0,
            0});
            this.udSi570_2.Minimum = new decimal(new int[] {
            110000000,
            0,
            0,
            0});
            this.udSi570_2.Name = "udSi570_2";
            this.udSi570_2.Size = new System.Drawing.Size(94, 20);
            this.udSi570_2.TabIndex = 4;
            this.udSi570_2.Value = new decimal(new int[] {
            114285000,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "1-17MHz";
            // 
            // udSi570_1
            // 
            this.udSi570_1.DecimalPlaces = 1;
            this.udSi570_1.Location = new System.Drawing.Point(71, 41);
            this.udSi570_1.Maximum = new decimal(new int[] {
            120000000,
            0,
            0,
            0});
            this.udSi570_1.Minimum = new decimal(new int[] {
            110000000,
            0,
            0,
            0});
            this.udSi570_1.Name = "udSi570_1";
            this.udSi570_1.Size = new System.Drawing.Size(94, 20);
            this.udSi570_1.TabIndex = 0;
            this.udSi570_1.Value = new decimal(new int[] {
            114285000,
            0,
            0,
            0});
            // 
            // udMsgRptTime
            // 
            this.udMsgRptTime.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.udMsgRptTime.Location = new System.Drawing.Point(97, 206);
            this.udMsgRptTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udMsgRptTime.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udMsgRptTime.Name = "udMsgRptTime";
            this.udMsgRptTime.Size = new System.Drawing.Size(57, 20);
            this.udMsgRptTime.TabIndex = 29;
            this.udMsgRptTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udMsgRptTime.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.udMsgRptTime.ValueChanged += new System.EventHandler(this.udMsgRptTime_ValueChanged);
            // 
            // lblMsgRpt
            // 
            this.lblMsgRpt.AutoSize = true;
            this.lblMsgRpt.Location = new System.Drawing.Point(8, 209);
            this.lblMsgRpt.Name = "lblMsgRpt";
            this.lblMsgRpt.Size = new System.Drawing.Size(88, 13);
            this.lblMsgRpt.TabIndex = 30;
            this.lblMsgRpt.Text = "Repeat time (mS)";
            // 
            // chkPA10
            // 
            this.chkPA10.AutoSize = true;
            this.chkPA10.Location = new System.Drawing.Point(37, 40);
            this.chkPA10.Name = "chkPA10";
            this.chkPA10.Size = new System.Drawing.Size(52, 17);
            this.chkPA10.TabIndex = 0;
            this.chkPA10.Text = "PA10";
            this.chkPA10.UseVisualStyleBackColor = true;
            this.chkPA10.CheckedChanged += new System.EventHandler(this.chkPA10_CheckedChanged);
            // 
            // udTXOffDelay
            // 
            this.udTXOffDelay.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udTXOffDelay.Location = new System.Drawing.Point(97, 181);
            this.udTXOffDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.udTXOffDelay.Name = "udTXOffDelay";
            this.udTXOffDelay.Size = new System.Drawing.Size(57, 20);
            this.udTXOffDelay.TabIndex = 27;
            this.udTXOffDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udTXOffDelay.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.udTXOffDelay.ValueChanged += new System.EventHandler(this.udTXOffDelay_ValueChanged);
            // 
            // chkPTTinv
            // 
            this.chkPTTinv.AutoSize = true;
            this.chkPTTinv.Location = new System.Drawing.Point(37, 60);
            this.chkPTTinv.Name = "chkPTTinv";
            this.chkPTTinv.Size = new System.Drawing.Size(88, 17);
            this.chkPTTinv.TabIndex = 1;
            this.chkPTTinv.Text = "PTT inverted";
            this.chkPTTinv.UseVisualStyleBackColor = true;
            this.chkPTTinv.CheckedChanged += new System.EventHandler(this.chkPTTinv_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "TX off delay";
            // 
            // chkRX2
            // 
            this.chkRX2.AutoSize = true;
            this.chkRX2.Location = new System.Drawing.Point(37, 80);
            this.chkRX2.Name = "chkRX2";
            this.chkRX2.Size = new System.Drawing.Size(107, 17);
            this.chkRX2.TabIndex = 2;
            this.chkRX2.Text = "Second RX input";
            this.chkRX2.UseVisualStyleBackColor = true;
            this.chkRX2.CheckedChanged += new System.EventHandler(this.chkRX2_CheckedChanged);
            // 
            // udTXIfShift
            // 
            this.udTXIfShift.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udTXIfShift.Location = new System.Drawing.Point(97, 156);
            this.udTXIfShift.Maximum = new decimal(new int[] {
            192000,
            0,
            0,
            0});
            this.udTXIfShift.Name = "udTXIfShift";
            this.udTXIfShift.Size = new System.Drawing.Size(57, 20);
            this.udTXIfShift.TabIndex = 25;
            this.udTXIfShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udTXIfShift.Value = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.udTXIfShift.ValueChanged += new System.EventHandler(this.udTXIfShift_ValueChanged);
            // 
            // lblSmeterCalOffset
            // 
            this.lblSmeterCalOffset.AutoSize = true;
            this.lblSmeterCalOffset.Location = new System.Drawing.Point(8, 108);
            this.lblSmeterCalOffset.Name = "lblSmeterCalOffset";
            this.lblSmeterCalOffset.Size = new System.Drawing.Size(89, 13);
            this.lblSmeterCalOffset.TabIndex = 17;
            this.lblSmeterCalOffset.Text = "Smeter cal. offset";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "TX IF frequency";
            // 
            // udSmeterCalOffset
            // 
            this.udSmeterCalOffset.DecimalPlaces = 1;
            this.udSmeterCalOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udSmeterCalOffset.Location = new System.Drawing.Point(103, 106);
            this.udSmeterCalOffset.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udSmeterCalOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.udSmeterCalOffset.Name = "udSmeterCalOffset";
            this.udSmeterCalOffset.Size = new System.Drawing.Size(51, 20);
            this.udSmeterCalOffset.TabIndex = 16;
            this.udSmeterCalOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udSmeterCalOffset.Value = new decimal(new int[] {
            351,
            0,
            0,
            -2147418112});
            this.udSmeterCalOffset.ValueChanged += new System.EventHandler(this.udSmeterCalOffset_ValueChanged);
            // 
            // udNBThreshold
            // 
            this.udNBThreshold.Location = new System.Drawing.Point(103, 131);
            this.udNBThreshold.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.udNBThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udNBThreshold.Name = "udNBThreshold";
            this.udNBThreshold.Size = new System.Drawing.Size(51, 20);
            this.udNBThreshold.TabIndex = 20;
            this.udNBThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udNBThreshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udNBThreshold.ValueChanged += new System.EventHandler(this.udNBTreshold_ValueChanged);
            // 
            // lblNBThresholds
            // 
            this.lblNBThresholds.AutoSize = true;
            this.lblNBThresholds.Location = new System.Drawing.Point(8, 133);
            this.lblNBThresholds.Name = "lblNBThresholds";
            this.lblNBThresholds.Size = new System.Drawing.Size(68, 13);
            this.lblNBThresholds.TabIndex = 21;
            this.lblNBThresholds.Text = "NB threshold";
            // 
            // tabPA10
            // 
            this.tabPA10.BackColor = System.Drawing.SystemColors.Control;
            this.tabPA10.Controls.Add(this.groupBox1);
            this.tabPA10.Location = new System.Drawing.Point(4, 22);
            this.tabPA10.Name = "tabPA10";
            this.tabPA10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPA10.Size = new System.Drawing.Size(339, 264);
            this.tabPA10.TabIndex = 1;
            this.tabPA10.Text = "PA10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udPAGain10m);
            this.groupBox1.Controls.Add(this.udPAGain6m);
            this.groupBox1.Controls.Add(this.lblBand160m);
            this.groupBox1.Controls.Add(this.lblBand6m);
            this.groupBox1.Controls.Add(this.udPAGain160m);
            this.groupBox1.Controls.Add(this.lblBand80m);
            this.groupBox1.Controls.Add(this.lblBand10m);
            this.groupBox1.Controls.Add(this.udPAGain80m);
            this.groupBox1.Controls.Add(this.udPAGain12m);
            this.groupBox1.Controls.Add(this.lblBand40m);
            this.groupBox1.Controls.Add(this.lblBand12m);
            this.groupBox1.Controls.Add(this.udPAGain40m);
            this.groupBox1.Controls.Add(this.udPAGain15m);
            this.groupBox1.Controls.Add(this.lblBand30m);
            this.groupBox1.Controls.Add(this.lblBand15m);
            this.groupBox1.Controls.Add(this.udPAGain30m);
            this.groupBox1.Controls.Add(this.udPAGain17m);
            this.groupBox1.Controls.Add(this.lblBand20m);
            this.groupBox1.Controls.Add(this.lblBand17m);
            this.groupBox1.Controls.Add(this.udPAGain20m);
            this.groupBox1.Location = new System.Drawing.Point(32, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 171);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gain by band (dB)";
            // 
            // udPAGain10m
            // 
            this.udPAGain10m.DecimalPlaces = 1;
            this.udPAGain10m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain10m.Location = new System.Drawing.Point(212, 102);
            this.udPAGain10m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain10m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain10m.Name = "udPAGain10m";
            this.udPAGain10m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain10m.TabIndex = 17;
            this.udPAGain10m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain10m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // udPAGain6m
            // 
            this.udPAGain6m.DecimalPlaces = 1;
            this.udPAGain6m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain6m.Location = new System.Drawing.Point(212, 128);
            this.udPAGain6m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain6m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain6m.Name = "udPAGain6m";
            this.udPAGain6m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain6m.TabIndex = 19;
            this.udPAGain6m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain6m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // lblBand160m
            // 
            this.lblBand160m.AutoSize = true;
            this.lblBand160m.Location = new System.Drawing.Point(19, 24);
            this.lblBand160m.Name = "lblBand160m";
            this.lblBand160m.Size = new System.Drawing.Size(36, 13);
            this.lblBand160m.TabIndex = 0;
            this.lblBand160m.Text = "160m:";
            // 
            // lblBand6m
            // 
            this.lblBand6m.AutoSize = true;
            this.lblBand6m.Location = new System.Drawing.Point(152, 130);
            this.lblBand6m.Name = "lblBand6m";
            this.lblBand6m.Size = new System.Drawing.Size(24, 13);
            this.lblBand6m.TabIndex = 18;
            this.lblBand6m.Text = "6m:";
            // 
            // udPAGain160m
            // 
            this.udPAGain160m.DecimalPlaces = 1;
            this.udPAGain160m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain160m.Location = new System.Drawing.Point(79, 22);
            this.udPAGain160m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain160m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain160m.Name = "udPAGain160m";
            this.udPAGain160m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain160m.TabIndex = 1;
            this.udPAGain160m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain160m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // lblBand80m
            // 
            this.lblBand80m.AutoSize = true;
            this.lblBand80m.Location = new System.Drawing.Point(19, 50);
            this.lblBand80m.Name = "lblBand80m";
            this.lblBand80m.Size = new System.Drawing.Size(30, 13);
            this.lblBand80m.TabIndex = 2;
            this.lblBand80m.Text = "80m:";
            // 
            // lblBand10m
            // 
            this.lblBand10m.AutoSize = true;
            this.lblBand10m.Location = new System.Drawing.Point(152, 104);
            this.lblBand10m.Name = "lblBand10m";
            this.lblBand10m.Size = new System.Drawing.Size(30, 13);
            this.lblBand10m.TabIndex = 16;
            this.lblBand10m.Text = "10m:";
            // 
            // udPAGain80m
            // 
            this.udPAGain80m.DecimalPlaces = 1;
            this.udPAGain80m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain80m.Location = new System.Drawing.Point(79, 48);
            this.udPAGain80m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain80m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain80m.Name = "udPAGain80m";
            this.udPAGain80m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain80m.TabIndex = 3;
            this.udPAGain80m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain80m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // udPAGain12m
            // 
            this.udPAGain12m.DecimalPlaces = 1;
            this.udPAGain12m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain12m.Location = new System.Drawing.Point(212, 76);
            this.udPAGain12m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain12m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain12m.Name = "udPAGain12m";
            this.udPAGain12m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain12m.TabIndex = 15;
            this.udPAGain12m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain12m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // lblBand40m
            // 
            this.lblBand40m.AutoSize = true;
            this.lblBand40m.Location = new System.Drawing.Point(19, 76);
            this.lblBand40m.Name = "lblBand40m";
            this.lblBand40m.Size = new System.Drawing.Size(30, 13);
            this.lblBand40m.TabIndex = 4;
            this.lblBand40m.Text = "40m:";
            // 
            // lblBand12m
            // 
            this.lblBand12m.AutoSize = true;
            this.lblBand12m.Location = new System.Drawing.Point(152, 78);
            this.lblBand12m.Name = "lblBand12m";
            this.lblBand12m.Size = new System.Drawing.Size(30, 13);
            this.lblBand12m.TabIndex = 14;
            this.lblBand12m.Text = "12m:";
            // 
            // udPAGain40m
            // 
            this.udPAGain40m.DecimalPlaces = 1;
            this.udPAGain40m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain40m.Location = new System.Drawing.Point(79, 74);
            this.udPAGain40m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain40m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain40m.Name = "udPAGain40m";
            this.udPAGain40m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain40m.TabIndex = 5;
            this.udPAGain40m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain40m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // udPAGain15m
            // 
            this.udPAGain15m.DecimalPlaces = 1;
            this.udPAGain15m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain15m.Location = new System.Drawing.Point(212, 50);
            this.udPAGain15m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain15m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain15m.Name = "udPAGain15m";
            this.udPAGain15m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain15m.TabIndex = 13;
            this.udPAGain15m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain15m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // lblBand30m
            // 
            this.lblBand30m.AutoSize = true;
            this.lblBand30m.Location = new System.Drawing.Point(19, 102);
            this.lblBand30m.Name = "lblBand30m";
            this.lblBand30m.Size = new System.Drawing.Size(30, 13);
            this.lblBand30m.TabIndex = 6;
            this.lblBand30m.Text = "30m:";
            // 
            // lblBand15m
            // 
            this.lblBand15m.AutoSize = true;
            this.lblBand15m.Location = new System.Drawing.Point(152, 52);
            this.lblBand15m.Name = "lblBand15m";
            this.lblBand15m.Size = new System.Drawing.Size(30, 13);
            this.lblBand15m.TabIndex = 12;
            this.lblBand15m.Text = "15m:";
            // 
            // udPAGain30m
            // 
            this.udPAGain30m.DecimalPlaces = 1;
            this.udPAGain30m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain30m.Location = new System.Drawing.Point(79, 100);
            this.udPAGain30m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain30m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain30m.Name = "udPAGain30m";
            this.udPAGain30m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain30m.TabIndex = 7;
            this.udPAGain30m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain30m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // udPAGain17m
            // 
            this.udPAGain17m.DecimalPlaces = 1;
            this.udPAGain17m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain17m.Location = new System.Drawing.Point(212, 24);
            this.udPAGain17m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain17m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain17m.Name = "udPAGain17m";
            this.udPAGain17m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain17m.TabIndex = 11;
            this.udPAGain17m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain17m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // lblBand20m
            // 
            this.lblBand20m.AutoSize = true;
            this.lblBand20m.Location = new System.Drawing.Point(19, 128);
            this.lblBand20m.Name = "lblBand20m";
            this.lblBand20m.Size = new System.Drawing.Size(30, 13);
            this.lblBand20m.TabIndex = 8;
            this.lblBand20m.Text = "20m:";
            // 
            // lblBand17m
            // 
            this.lblBand17m.AutoSize = true;
            this.lblBand17m.Location = new System.Drawing.Point(152, 26);
            this.lblBand17m.Name = "lblBand17m";
            this.lblBand17m.Size = new System.Drawing.Size(30, 13);
            this.lblBand17m.TabIndex = 10;
            this.lblBand17m.Text = "17m:";
            // 
            // udPAGain20m
            // 
            this.udPAGain20m.DecimalPlaces = 1;
            this.udPAGain20m.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udPAGain20m.Location = new System.Drawing.Point(79, 126);
            this.udPAGain20m.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udPAGain20m.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.udPAGain20m.Name = "udPAGain20m";
            this.udPAGain20m.Size = new System.Drawing.Size(43, 20);
            this.udPAGain20m.TabIndex = 9;
            this.udPAGain20m.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.udPAGain20m.ValueChanged += new System.EventHandler(this.udPAGain_ValueChanged);
            // 
            // tabDSP
            // 
            this.tabDSP.BackColor = System.Drawing.SystemColors.Control;
            this.tabDSP.Controls.Add(this.label52);
            this.tabDSP.Controls.Add(this.grpDSPRXImageReject);
            this.tabDSP.Controls.Add(this.grpDSPTXImageReject);
            this.tabDSP.Controls.Add(this.comboCWAGC);
            this.tabDSP.Location = new System.Drawing.Point(4, 22);
            this.tabDSP.Name = "tabDSP";
            this.tabDSP.Padding = new System.Windows.Forms.Padding(3);
            this.tabDSP.Size = new System.Drawing.Size(339, 264);
            this.tabDSP.TabIndex = 2;
            this.tabDSP.Text = "DSP";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(20, 240);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(29, 13);
            this.label52.TabIndex = 26;
            this.label52.Text = "AGC";
            // 
            // grpDSPRXImageReject
            // 
            this.grpDSPRXImageReject.Controls.Add(this.radIQWBIR);
            this.grpDSPRXImageReject.Controls.Add(this.radIQFixed);
            this.grpDSPRXImageReject.Controls.Add(this.bttnRXClearAll);
            this.grpDSPRXImageReject.Controls.Add(this.bttnRXClearBand);
            this.grpDSPRXImageReject.Controls.Add(this.bttnRXCallAll);
            this.grpDSPRXImageReject.Controls.Add(this.bttnRXCalBand);
            this.grpDSPRXImageReject.Controls.Add(this.label44);
            this.grpDSPRXImageReject.Controls.Add(this.udRXGain);
            this.grpDSPRXImageReject.Controls.Add(this.tbRXGain);
            this.grpDSPRXImageReject.Controls.Add(this.label45);
            this.grpDSPRXImageReject.Controls.Add(this.label46);
            this.grpDSPRXImageReject.Controls.Add(this.udRXPhase);
            this.grpDSPRXImageReject.Controls.Add(this.tbRXPhase);
            this.grpDSPRXImageReject.Controls.Add(this.label47);
            this.grpDSPRXImageReject.Location = new System.Drawing.Point(170, 13);
            this.grpDSPRXImageReject.Name = "grpDSPRXImageReject";
            this.grpDSPRXImageReject.Size = new System.Drawing.Size(167, 222);
            this.grpDSPRXImageReject.TabIndex = 36;
            this.grpDSPRXImageReject.TabStop = false;
            this.grpDSPRXImageReject.Text = "RX Image reject";
            // 
            // radIQWBIR
            // 
            this.radIQWBIR.AutoSize = true;
            this.radIQWBIR.Location = new System.Drawing.Point(48, 108);
            this.radIQWBIR.Name = "radIQWBIR";
            this.radIQWBIR.Size = new System.Drawing.Size(54, 17);
            this.radIQWBIR.TabIndex = 38;
            this.radIQWBIR.Text = "WBIR";
            this.radIQWBIR.UseVisualStyleBackColor = true;
            this.radIQWBIR.CheckedChanged += new System.EventHandler(this.radIQWBIR_CheckedChanged);
            // 
            // radIQFixed
            // 
            this.radIQFixed.AutoSize = true;
            this.radIQFixed.Checked = true;
            this.radIQFixed.Location = new System.Drawing.Point(48, 136);
            this.radIQFixed.Name = "radIQFixed";
            this.radIQFixed.Size = new System.Drawing.Size(50, 17);
            this.radIQFixed.TabIndex = 36;
            this.radIQFixed.TabStop = true;
            this.radIQFixed.Text = "Fixed";
            this.radIQFixed.UseVisualStyleBackColor = true;
            this.radIQFixed.CheckedChanged += new System.EventHandler(this.radIQFixed_CheckedChanged);
            // 
            // bttnRXClearAll
            // 
            this.bttnRXClearAll.Location = new System.Drawing.Point(85, 194);
            this.bttnRXClearAll.Name = "bttnRXClearAll";
            this.bttnRXClearAll.Size = new System.Drawing.Size(75, 23);
            this.bttnRXClearAll.TabIndex = 35;
            this.bttnRXClearAll.Text = "Clear All";
            this.bttnRXClearAll.UseVisualStyleBackColor = true;
            this.bttnRXClearAll.Click += new System.EventHandler(this.bttnRXClearAll_Click);
            // 
            // bttnRXClearBand
            // 
            this.bttnRXClearBand.Location = new System.Drawing.Point(6, 194);
            this.bttnRXClearBand.Name = "bttnRXClearBand";
            this.bttnRXClearBand.Size = new System.Drawing.Size(75, 23);
            this.bttnRXClearBand.TabIndex = 34;
            this.bttnRXClearBand.Text = "Clear Band";
            this.bttnRXClearBand.UseVisualStyleBackColor = true;
            this.bttnRXClearBand.Click += new System.EventHandler(this.bttnRXClearBand_Click);
            // 
            // bttnRXCallAll
            // 
            this.bttnRXCallAll.Location = new System.Drawing.Point(85, 166);
            this.bttnRXCallAll.Name = "bttnRXCallAll";
            this.bttnRXCallAll.Size = new System.Drawing.Size(75, 23);
            this.bttnRXCallAll.TabIndex = 33;
            this.bttnRXCallAll.Text = "Save All";
            this.bttnRXCallAll.UseVisualStyleBackColor = true;
            this.bttnRXCallAll.Click += new System.EventHandler(this.bttnRXCallAll_Click);
            // 
            // bttnRXCalBand
            // 
            this.bttnRXCalBand.Location = new System.Drawing.Point(6, 166);
            this.bttnRXCalBand.Name = "bttnRXCalBand";
            this.bttnRXCalBand.Size = new System.Drawing.Size(75, 23);
            this.bttnRXCalBand.TabIndex = 32;
            this.bttnRXCalBand.Text = "Save Band";
            this.bttnRXCalBand.UseVisualStyleBackColor = true;
            this.bttnRXCalBand.Click += new System.EventHandler(this.bttnRXCalBand_Click);
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(63, 84);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(100, 13);
            this.label44.TabIndex = 31;
            this.label44.Text = "-400-200 0 200 400";
            // 
            // udRXGain
            // 
            this.udRXGain.DecimalPlaces = 2;
            this.udRXGain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.udRXGain.Location = new System.Drawing.Point(6, 75);
            this.udRXGain.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.udRXGain.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            -2147483648});
            this.udRXGain.Name = "udRXGain";
            this.udRXGain.Size = new System.Drawing.Size(54, 20);
            this.udRXGain.TabIndex = 30;
            this.udRXGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udRXGain.ValueChanged += new System.EventHandler(this.udRXGain_ValueChanged);
            // 
            // tbRXGain
            // 
            this.tbRXGain.AutoSize = false;
            this.tbRXGain.LargeChange = 1;
            this.tbRXGain.Location = new System.Drawing.Point(62, 59);
            this.tbRXGain.Maximum = 400;
            this.tbRXGain.Minimum = -400;
            this.tbRXGain.Name = "tbRXGain";
            this.tbRXGain.Size = new System.Drawing.Size(100, 23);
            this.tbRXGain.TabIndex = 28;
            this.tbRXGain.TickFrequency = 50;
            this.tbRXGain.ValueChanged += new System.EventHandler(this.tbRXGain_ValueChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(14, 59);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(29, 13);
            this.label45.TabIndex = 29;
            this.label45.Text = "Gain";
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(63, 43);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(100, 13);
            this.label46.TabIndex = 27;
            this.label46.Text = "-400-200 0 200 400";
            // 
            // udRXPhase
            // 
            this.udRXPhase.DecimalPlaces = 2;
            this.udRXPhase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.udRXPhase.Location = new System.Drawing.Point(6, 30);
            this.udRXPhase.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.udRXPhase.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            -2147483648});
            this.udRXPhase.Name = "udRXPhase";
            this.udRXPhase.Size = new System.Drawing.Size(54, 20);
            this.udRXPhase.TabIndex = 26;
            this.udRXPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udRXPhase.ValueChanged += new System.EventHandler(this.udRXPhase_ValueChanged);
            // 
            // tbRXPhase
            // 
            this.tbRXPhase.AutoSize = false;
            this.tbRXPhase.LargeChange = 1;
            this.tbRXPhase.Location = new System.Drawing.Point(62, 14);
            this.tbRXPhase.Maximum = 400;
            this.tbRXPhase.Minimum = -400;
            this.tbRXPhase.Name = "tbRXPhase";
            this.tbRXPhase.Size = new System.Drawing.Size(100, 23);
            this.tbRXPhase.TabIndex = 24;
            this.tbRXPhase.TickFrequency = 50;
            this.tbRXPhase.ValueChanged += new System.EventHandler(this.tbRXPhase_ValueChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(13, 14);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(37, 13);
            this.label47.TabIndex = 25;
            this.label47.Text = "Phase";
            // 
            // grpDSPTXImageReject
            // 
            this.grpDSPTXImageReject.Controls.Add(this.bttnTXClearAll);
            this.grpDSPTXImageReject.Controls.Add(this.bttnTXClearBand);
            this.grpDSPTXImageReject.Controls.Add(this.bttnTXCalAll);
            this.grpDSPTXImageReject.Controls.Add(this.bttnTXCalBand);
            this.grpDSPTXImageReject.Controls.Add(this.lblGainValue);
            this.grpDSPTXImageReject.Controls.Add(this.udTXGain);
            this.grpDSPTXImageReject.Controls.Add(this.tbTXGain);
            this.grpDSPTXImageReject.Controls.Add(this.lblGain);
            this.grpDSPTXImageReject.Controls.Add(this.lblPhaseValue);
            this.grpDSPTXImageReject.Controls.Add(this.udTXPhase);
            this.grpDSPTXImageReject.Controls.Add(this.tbTXPhase);
            this.grpDSPTXImageReject.Controls.Add(this.lblPhase);
            this.grpDSPTXImageReject.Location = new System.Drawing.Point(1, 13);
            this.grpDSPTXImageReject.Name = "grpDSPTXImageReject";
            this.grpDSPTXImageReject.Size = new System.Drawing.Size(167, 222);
            this.grpDSPTXImageReject.TabIndex = 26;
            this.grpDSPTXImageReject.TabStop = false;
            this.grpDSPTXImageReject.Text = "TX Image reject";
            // 
            // bttnTXClearAll
            // 
            this.bttnTXClearAll.Location = new System.Drawing.Point(85, 191);
            this.bttnTXClearAll.Name = "bttnTXClearAll";
            this.bttnTXClearAll.Size = new System.Drawing.Size(75, 23);
            this.bttnTXClearAll.TabIndex = 35;
            this.bttnTXClearAll.Text = "Clear All";
            this.bttnTXClearAll.UseVisualStyleBackColor = true;
            this.bttnTXClearAll.Click += new System.EventHandler(this.bttnTXClearAll_Click);
            // 
            // bttnTXClearBand
            // 
            this.bttnTXClearBand.Location = new System.Drawing.Point(6, 191);
            this.bttnTXClearBand.Name = "bttnTXClearBand";
            this.bttnTXClearBand.Size = new System.Drawing.Size(75, 23);
            this.bttnTXClearBand.TabIndex = 34;
            this.bttnTXClearBand.Text = "Clear Band";
            this.bttnTXClearBand.UseVisualStyleBackColor = true;
            this.bttnTXClearBand.Click += new System.EventHandler(this.bttnTXClearBand_Click);
            // 
            // bttnTXCalAll
            // 
            this.bttnTXCalAll.Location = new System.Drawing.Point(85, 147);
            this.bttnTXCalAll.Name = "bttnTXCalAll";
            this.bttnTXCalAll.Size = new System.Drawing.Size(75, 23);
            this.bttnTXCalAll.TabIndex = 33;
            this.bttnTXCalAll.Text = "Save All";
            this.bttnTXCalAll.UseVisualStyleBackColor = true;
            this.bttnTXCalAll.Click += new System.EventHandler(this.bttnTXCalAll_Click);
            // 
            // bttnTXCalBand
            // 
            this.bttnTXCalBand.Location = new System.Drawing.Point(6, 147);
            this.bttnTXCalBand.Name = "bttnTXCalBand";
            this.bttnTXCalBand.Size = new System.Drawing.Size(75, 23);
            this.bttnTXCalBand.TabIndex = 32;
            this.bttnTXCalBand.Text = "Save Band";
            this.bttnTXCalBand.UseVisualStyleBackColor = true;
            this.bttnTXCalBand.Click += new System.EventHandler(this.bttnTXCalBand_Click);
            // 
            // lblGainValue
            // 
            this.lblGainValue.Location = new System.Drawing.Point(63, 107);
            this.lblGainValue.Name = "lblGainValue";
            this.lblGainValue.Size = new System.Drawing.Size(100, 13);
            this.lblGainValue.TabIndex = 31;
            this.lblGainValue.Text = "-400-200 0 200 400";
            // 
            // udTXGain
            // 
            this.udTXGain.DecimalPlaces = 2;
            this.udTXGain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.udTXGain.Location = new System.Drawing.Point(6, 95);
            this.udTXGain.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.udTXGain.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            -2147483648});
            this.udTXGain.Name = "udTXGain";
            this.udTXGain.Size = new System.Drawing.Size(54, 20);
            this.udTXGain.TabIndex = 30;
            this.udTXGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udTXGain.ValueChanged += new System.EventHandler(this.udGain_ValueChanged);
            // 
            // tbTXGain
            // 
            this.tbTXGain.AutoSize = false;
            this.tbTXGain.LargeChange = 1;
            this.tbTXGain.Location = new System.Drawing.Point(62, 79);
            this.tbTXGain.Maximum = 400;
            this.tbTXGain.Minimum = -400;
            this.tbTXGain.Name = "tbTXGain";
            this.tbTXGain.Size = new System.Drawing.Size(100, 23);
            this.tbTXGain.TabIndex = 28;
            this.tbTXGain.TickFrequency = 50;
            this.tbTXGain.ValueChanged += new System.EventHandler(this.tbGain_ValueChanged);
            // 
            // lblGain
            // 
            this.lblGain.AutoSize = true;
            this.lblGain.Location = new System.Drawing.Point(14, 79);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(29, 13);
            this.lblGain.TabIndex = 29;
            this.lblGain.Text = "Gain";
            // 
            // lblPhaseValue
            // 
            this.lblPhaseValue.Location = new System.Drawing.Point(63, 47);
            this.lblPhaseValue.Name = "lblPhaseValue";
            this.lblPhaseValue.Size = new System.Drawing.Size(100, 13);
            this.lblPhaseValue.TabIndex = 27;
            this.lblPhaseValue.Text = "-400-200 0 200 400";
            // 
            // udTXPhase
            // 
            this.udTXPhase.DecimalPlaces = 2;
            this.udTXPhase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.udTXPhase.Location = new System.Drawing.Point(6, 35);
            this.udTXPhase.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.udTXPhase.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            -2147483648});
            this.udTXPhase.Name = "udTXPhase";
            this.udTXPhase.Size = new System.Drawing.Size(54, 20);
            this.udTXPhase.TabIndex = 26;
            this.udTXPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udTXPhase.ValueChanged += new System.EventHandler(this.udPhase_ValueChanged);
            // 
            // tbTXPhase
            // 
            this.tbTXPhase.AutoSize = false;
            this.tbTXPhase.LargeChange = 1;
            this.tbTXPhase.Location = new System.Drawing.Point(62, 19);
            this.tbTXPhase.Maximum = 400;
            this.tbTXPhase.Minimum = -400;
            this.tbTXPhase.Name = "tbTXPhase";
            this.tbTXPhase.Size = new System.Drawing.Size(100, 23);
            this.tbTXPhase.TabIndex = 24;
            this.tbTXPhase.TickFrequency = 50;
            this.tbTXPhase.ValueChanged += new System.EventHandler(this.tbTXPhase_ValueChanged);
            // 
            // lblPhase
            // 
            this.lblPhase.AutoSize = true;
            this.lblPhase.Location = new System.Drawing.Point(13, 19);
            this.lblPhase.Name = "lblPhase";
            this.lblPhase.Size = new System.Drawing.Size(37, 13);
            this.lblPhase.TabIndex = 25;
            this.lblPhase.Text = "Phase";
            // 
            // comboCWAGC
            // 
            this.comboCWAGC.FormattingEnabled = true;
            this.comboCWAGC.Items.AddRange(new object[] {
            "OFF",
            "LONG",
            "SLOW",
            "MED",
            "FAST"});
            this.comboCWAGC.Location = new System.Drawing.Point(68, 237);
            this.comboCWAGC.Name = "comboCWAGC";
            this.comboCWAGC.Size = new System.Drawing.Size(67, 21);
            this.comboCWAGC.TabIndex = 25;
            this.comboCWAGC.Text = "LONG";
            this.comboCWAGC.SelectedIndexChanged += new System.EventHandler(this.comboCWAGC_SelectedIndexChanged);
            // 
            // tabG11
            // 
            this.tabG11.BackColor = System.Drawing.SystemColors.Control;
            this.tabG11.Controls.Add(this.chkG11MultiBand);
            this.tabG11.Controls.Add(this.grpG11BandFilters);
            this.tabG11.Location = new System.Drawing.Point(4, 22);
            this.tabG11.Name = "tabG11";
            this.tabG11.Padding = new System.Windows.Forms.Padding(3);
            this.tabG11.Size = new System.Drawing.Size(339, 264);
            this.tabG11.TabIndex = 3;
            this.tabG11.Text = "G11";
            // 
            // chkG11MultiBand
            // 
            this.chkG11MultiBand.AutoSize = true;
            this.chkG11MultiBand.Location = new System.Drawing.Point(16, 25);
            this.chkG11MultiBand.Name = "chkG11MultiBand";
            this.chkG11MultiBand.Size = new System.Drawing.Size(73, 17);
            this.chkG11MultiBand.TabIndex = 1;
            this.chkG11MultiBand.Text = "MultiBand";
            this.chkG11MultiBand.UseVisualStyleBackColor = true;
            this.chkG11MultiBand.CheckedChanged += new System.EventHandler(this.chkG11MultiBand_CheckedChanged);
            // 
            // grpG11BandFilters
            // 
            this.grpG11BandFilters.Controls.Add(this.chkG11B4030_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B4030_CH2);
            this.grpG11BandFilters.Controls.Add(this.label53);
            this.grpG11BandFilters.Controls.Add(this.chkG11B6_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B6_CH2);
            this.grpG11BandFilters.Controls.Add(this.label31);
            this.grpG11BandFilters.Controls.Add(this.chkG11B151210_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B1210_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B201715_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B1715_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B2017_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B3020_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B6040_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B80_CH1);
            this.grpG11BandFilters.Controls.Add(this.chkG11B151210_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B1210_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B201715_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B1715_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B2017_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B3020_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B6040_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B80_CH2);
            this.grpG11BandFilters.Controls.Add(this.chkG11B160_CH2);
            this.grpG11BandFilters.Controls.Add(this.label32);
            this.grpG11BandFilters.Controls.Add(this.label29);
            this.grpG11BandFilters.Controls.Add(this.label30);
            this.grpG11BandFilters.Controls.Add(this.label27);
            this.grpG11BandFilters.Controls.Add(this.label28);
            this.grpG11BandFilters.Controls.Add(this.label25);
            this.grpG11BandFilters.Controls.Add(this.label26);
            this.grpG11BandFilters.Controls.Add(this.label24);
            this.grpG11BandFilters.Controls.Add(this.label23);
            this.grpG11BandFilters.Controls.Add(this.label22);
            this.grpG11BandFilters.Controls.Add(this.label1);
            this.grpG11BandFilters.Location = new System.Drawing.Point(99, 19);
            this.grpG11BandFilters.Name = "grpG11BandFilters";
            this.grpG11BandFilters.Size = new System.Drawing.Size(234, 227);
            this.grpG11BandFilters.TabIndex = 0;
            this.grpG11BandFilters.TabStop = false;
            this.grpG11BandFilters.Text = "Band filters";
            // 
            // chkG11B4030_CH1
            // 
            this.chkG11B4030_CH1.AutoSize = true;
            this.chkG11B4030_CH1.Location = new System.Drawing.Point(119, 80);
            this.chkG11B4030_CH1.Name = "chkG11B4030_CH1";
            this.chkG11B4030_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B4030_CH1.TabIndex = 36;
            this.chkG11B4030_CH1.UseVisualStyleBackColor = true;
            this.chkG11B4030_CH1.CheckedChanged += new System.EventHandler(this.chkG11B4030_CH1_CheckedChanged);
            // 
            // chkG11B4030_CH2
            // 
            this.chkG11B4030_CH2.AutoSize = true;
            this.chkG11B4030_CH2.Location = new System.Drawing.Point(178, 80);
            this.chkG11B4030_CH2.Name = "chkG11B4030_CH2";
            this.chkG11B4030_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B4030_CH2.TabIndex = 35;
            this.chkG11B4030_CH2.UseVisualStyleBackColor = true;
            this.chkG11B4030_CH2.CheckedChanged += new System.EventHandler(this.chkG11B4030_CH2_CheckedChanged);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(24, 80);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(42, 13);
            this.label53.TabIndex = 34;
            this.label53.Text = "40-30m";
            // 
            // chkG11B6_CH1
            // 
            this.chkG11B6_CH1.AutoSize = true;
            this.chkG11B6_CH1.Location = new System.Drawing.Point(119, 194);
            this.chkG11B6_CH1.Name = "chkG11B6_CH1";
            this.chkG11B6_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B6_CH1.TabIndex = 33;
            this.chkG11B6_CH1.UseVisualStyleBackColor = true;
            this.chkG11B6_CH1.CheckedChanged += new System.EventHandler(this.chkG11B6_CH1_CheckedChanged);
            // 
            // chkG11B6_CH2
            // 
            this.chkG11B6_CH2.AutoSize = true;
            this.chkG11B6_CH2.Location = new System.Drawing.Point(178, 194);
            this.chkG11B6_CH2.Name = "chkG11B6_CH2";
            this.chkG11B6_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B6_CH2.TabIndex = 32;
            this.chkG11B6_CH2.UseVisualStyleBackColor = true;
            this.chkG11B6_CH2.CheckedChanged += new System.EventHandler(this.chkG11B6_CH2_CheckedChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(24, 194);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(21, 13);
            this.label31.TabIndex = 31;
            this.label31.Text = "6m";
            // 
            // chkG11B151210_CH1
            // 
            this.chkG11B151210_CH1.AutoSize = true;
            this.chkG11B151210_CH1.Location = new System.Drawing.Point(119, 177);
            this.chkG11B151210_CH1.Name = "chkG11B151210_CH1";
            this.chkG11B151210_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B151210_CH1.TabIndex = 30;
            this.chkG11B151210_CH1.UseVisualStyleBackColor = true;
            this.chkG11B151210_CH1.CheckedChanged += new System.EventHandler(this.chkG11B151210_CH1_CheckedChanged);
            // 
            // chkG11B1210_CH1
            // 
            this.chkG11B1210_CH1.AutoSize = true;
            this.chkG11B1210_CH1.Location = new System.Drawing.Point(119, 161);
            this.chkG11B1210_CH1.Name = "chkG11B1210_CH1";
            this.chkG11B1210_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B1210_CH1.TabIndex = 29;
            this.chkG11B1210_CH1.UseVisualStyleBackColor = true;
            this.chkG11B1210_CH1.CheckedChanged += new System.EventHandler(this.chkG11B1210_CH1_CheckedChanged);
            // 
            // chkG11B201715_CH1
            // 
            this.chkG11B201715_CH1.AutoSize = true;
            this.chkG11B201715_CH1.Location = new System.Drawing.Point(119, 145);
            this.chkG11B201715_CH1.Name = "chkG11B201715_CH1";
            this.chkG11B201715_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B201715_CH1.TabIndex = 28;
            this.chkG11B201715_CH1.UseVisualStyleBackColor = true;
            this.chkG11B201715_CH1.CheckedChanged += new System.EventHandler(this.chkG11B201715_CH1_CheckedChanged);
            // 
            // chkG11B1715_CH1
            // 
            this.chkG11B1715_CH1.AutoSize = true;
            this.chkG11B1715_CH1.Location = new System.Drawing.Point(119, 129);
            this.chkG11B1715_CH1.Name = "chkG11B1715_CH1";
            this.chkG11B1715_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B1715_CH1.TabIndex = 27;
            this.chkG11B1715_CH1.UseVisualStyleBackColor = true;
            this.chkG11B1715_CH1.CheckedChanged += new System.EventHandler(this.chkG11B1715_CH1_CheckedChanged);
            // 
            // chkG11B2017_CH1
            // 
            this.chkG11B2017_CH1.AutoSize = true;
            this.chkG11B2017_CH1.Location = new System.Drawing.Point(119, 113);
            this.chkG11B2017_CH1.Name = "chkG11B2017_CH1";
            this.chkG11B2017_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B2017_CH1.TabIndex = 26;
            this.chkG11B2017_CH1.UseVisualStyleBackColor = true;
            this.chkG11B2017_CH1.CheckedChanged += new System.EventHandler(this.chkG11B2017_CH1_CheckedChanged);
            // 
            // chkG11B3020_CH1
            // 
            this.chkG11B3020_CH1.AutoSize = true;
            this.chkG11B3020_CH1.Location = new System.Drawing.Point(119, 97);
            this.chkG11B3020_CH1.Name = "chkG11B3020_CH1";
            this.chkG11B3020_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B3020_CH1.TabIndex = 25;
            this.chkG11B3020_CH1.UseVisualStyleBackColor = true;
            this.chkG11B3020_CH1.CheckedChanged += new System.EventHandler(this.chkG11B3020_CH1_CheckedChanged);
            // 
            // chkG11B6040_CH1
            // 
            this.chkG11B6040_CH1.AutoSize = true;
            this.chkG11B6040_CH1.Location = new System.Drawing.Point(119, 63);
            this.chkG11B6040_CH1.Name = "chkG11B6040_CH1";
            this.chkG11B6040_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B6040_CH1.TabIndex = 23;
            this.chkG11B6040_CH1.UseVisualStyleBackColor = true;
            this.chkG11B6040_CH1.CheckedChanged += new System.EventHandler(this.chkG11B6040_CH1_CheckedChanged);
            // 
            // chkG11B80_CH1
            // 
            this.chkG11B80_CH1.AutoSize = true;
            this.chkG11B80_CH1.Location = new System.Drawing.Point(119, 47);
            this.chkG11B80_CH1.Name = "chkG11B80_CH1";
            this.chkG11B80_CH1.Size = new System.Drawing.Size(15, 14);
            this.chkG11B80_CH1.TabIndex = 22;
            this.chkG11B80_CH1.UseVisualStyleBackColor = true;
            this.chkG11B80_CH1.CheckedChanged += new System.EventHandler(this.chkG11B80_CH1_CheckedChanged);
            // 
            // chkG11B151210_CH2
            // 
            this.chkG11B151210_CH2.AutoSize = true;
            this.chkG11B151210_CH2.Location = new System.Drawing.Point(178, 177);
            this.chkG11B151210_CH2.Name = "chkG11B151210_CH2";
            this.chkG11B151210_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B151210_CH2.TabIndex = 20;
            this.chkG11B151210_CH2.UseVisualStyleBackColor = true;
            this.chkG11B151210_CH2.CheckedChanged += new System.EventHandler(this.chkG11B151210_CH2_CheckedChanged);
            // 
            // chkG11B1210_CH2
            // 
            this.chkG11B1210_CH2.AutoSize = true;
            this.chkG11B1210_CH2.Location = new System.Drawing.Point(178, 161);
            this.chkG11B1210_CH2.Name = "chkG11B1210_CH2";
            this.chkG11B1210_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B1210_CH2.TabIndex = 19;
            this.chkG11B1210_CH2.UseVisualStyleBackColor = true;
            this.chkG11B1210_CH2.CheckedChanged += new System.EventHandler(this.chkG11B1210_CH2_CheckedChanged);
            // 
            // chkG11B201715_CH2
            // 
            this.chkG11B201715_CH2.AutoSize = true;
            this.chkG11B201715_CH2.Location = new System.Drawing.Point(178, 145);
            this.chkG11B201715_CH2.Name = "chkG11B201715_CH2";
            this.chkG11B201715_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B201715_CH2.TabIndex = 18;
            this.chkG11B201715_CH2.UseVisualStyleBackColor = true;
            this.chkG11B201715_CH2.CheckedChanged += new System.EventHandler(this.chkG11B201715_CH2_CheckedChanged);
            // 
            // chkG11B1715_CH2
            // 
            this.chkG11B1715_CH2.AutoSize = true;
            this.chkG11B1715_CH2.Location = new System.Drawing.Point(178, 129);
            this.chkG11B1715_CH2.Name = "chkG11B1715_CH2";
            this.chkG11B1715_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B1715_CH2.TabIndex = 17;
            this.chkG11B1715_CH2.UseVisualStyleBackColor = true;
            this.chkG11B1715_CH2.CheckedChanged += new System.EventHandler(this.chkG11B1715_CH2_CheckedChanged);
            // 
            // chkG11B2017_CH2
            // 
            this.chkG11B2017_CH2.AutoSize = true;
            this.chkG11B2017_CH2.Location = new System.Drawing.Point(178, 113);
            this.chkG11B2017_CH2.Name = "chkG11B2017_CH2";
            this.chkG11B2017_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B2017_CH2.TabIndex = 16;
            this.chkG11B2017_CH2.UseVisualStyleBackColor = true;
            this.chkG11B2017_CH2.CheckedChanged += new System.EventHandler(this.chkG11B2017_CH2_CheckedChanged);
            // 
            // chkG11B3020_CH2
            // 
            this.chkG11B3020_CH2.AutoSize = true;
            this.chkG11B3020_CH2.Location = new System.Drawing.Point(178, 97);
            this.chkG11B3020_CH2.Name = "chkG11B3020_CH2";
            this.chkG11B3020_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B3020_CH2.TabIndex = 15;
            this.chkG11B3020_CH2.UseVisualStyleBackColor = true;
            this.chkG11B3020_CH2.CheckedChanged += new System.EventHandler(this.chkG11B3020_CH2_CheckedChanged);
            // 
            // chkG11B6040_CH2
            // 
            this.chkG11B6040_CH2.AutoSize = true;
            this.chkG11B6040_CH2.Location = new System.Drawing.Point(178, 63);
            this.chkG11B6040_CH2.Name = "chkG11B6040_CH2";
            this.chkG11B6040_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B6040_CH2.TabIndex = 13;
            this.chkG11B6040_CH2.UseVisualStyleBackColor = true;
            this.chkG11B6040_CH2.CheckedChanged += new System.EventHandler(this.chkG11B6040_CH2_CheckedChanged);
            // 
            // chkG11B80_CH2
            // 
            this.chkG11B80_CH2.AutoSize = true;
            this.chkG11B80_CH2.Location = new System.Drawing.Point(178, 47);
            this.chkG11B80_CH2.Name = "chkG11B80_CH2";
            this.chkG11B80_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B80_CH2.TabIndex = 12;
            this.chkG11B80_CH2.UseVisualStyleBackColor = true;
            this.chkG11B80_CH2.CheckedChanged += new System.EventHandler(this.chkG11B80_CH2_CheckedChanged);
            // 
            // chkG11B160_CH2
            // 
            this.chkG11B160_CH2.AutoSize = true;
            this.chkG11B160_CH2.Location = new System.Drawing.Point(178, 31);
            this.chkG11B160_CH2.Name = "chkG11B160_CH2";
            this.chkG11B160_CH2.Size = new System.Drawing.Size(15, 14);
            this.chkG11B160_CH2.TabIndex = 11;
            this.chkG11B160_CH2.UseVisualStyleBackColor = true;
            this.chkG11B160_CH2.CheckedChanged += new System.EventHandler(this.chkG11B160_CH2_CheckedChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(24, 177);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(57, 13);
            this.label32.TabIndex = 10;
            this.label32.Text = "15-12-10m";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(24, 161);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(42, 13);
            this.label29.TabIndex = 9;
            this.label29.Text = "12-10m";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(24, 145);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(57, 13);
            this.label30.TabIndex = 8;
            this.label30.Text = "20-17-15m";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(24, 129);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(42, 13);
            this.label27.TabIndex = 7;
            this.label27.Text = "17-15m";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(24, 113);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(42, 13);
            this.label28.TabIndex = 6;
            this.label28.Text = "20-17m";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(24, 97);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(42, 13);
            this.label25.TabIndex = 5;
            this.label25.Text = "30-20m";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(24, 63);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "60-40m";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(24, 47);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(27, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "80m";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(24, 31);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(33, 13);
            this.label23.TabIndex = 2;
            this.label23.Text = "160m";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(175, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(26, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Ch2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ch1";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.tabControl1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(347, 296);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Modem";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCWSettings);
            this.tabControl1.Controls.Add(this.tabRTTYSettings);
            this.tabControl1.Controls.Add(this.tabPSKsettings);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 293);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCWSettings
            // 
            this.tabCWSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabCWSettings.Controls.Add(this.groupBox2);
            this.tabCWSettings.Controls.Add(this.udCWPitch);
            this.tabCWSettings.Controls.Add(this.lblCWPitch);
            this.tabCWSettings.Location = new System.Drawing.Point(4, 22);
            this.tabCWSettings.Name = "tabCWSettings";
            this.tabCWSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabCWSettings.Size = new System.Drawing.Size(336, 267);
            this.tabCWSettings.TabIndex = 0;
            this.tabCWSettings.Text = "CW";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.udCWDebounce);
            this.groupBox2.Controls.Add(this.label49);
            this.groupBox2.Controls.Add(this.udCWWeight);
            this.groupBox2.Controls.Add(this.label48);
            this.groupBox2.Controls.Add(this.udCWFall);
            this.groupBox2.Controls.Add(this.lblCWFall);
            this.groupBox2.Controls.Add(this.udCWRise);
            this.groupBox2.Controls.Add(this.lblCWRise);
            this.groupBox2.Controls.Add(this.udG59DASHDOTRatio);
            this.groupBox2.Controls.Add(this.lblG59dashDotRatio);
            this.groupBox2.Controls.Add(this.chkG59IambicRev);
            this.groupBox2.Controls.Add(this.chkG59IambicBmode);
            this.groupBox2.Controls.Add(this.chkG59Iambic);
            this.groupBox2.Controls.Add(this.udCWSpeed);
            this.groupBox2.Controls.Add(this.lblCWSpeed);
            this.groupBox2.Location = new System.Drawing.Point(26, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 239);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keyer";
            // 
            // udCWDebounce
            // 
            this.udCWDebounce.Location = new System.Drawing.Point(94, 212);
            this.udCWDebounce.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udCWDebounce.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.udCWDebounce.Name = "udCWDebounce";
            this.udCWDebounce.Size = new System.Drawing.Size(51, 20);
            this.udCWDebounce.TabIndex = 35;
            this.udCWDebounce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udCWDebounce.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udCWDebounce.ValueChanged += new System.EventHandler(this.udCWDebounce_ValueChanged);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(9, 214);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(57, 13);
            this.label49.TabIndex = 36;
            this.label49.Text = "Debaunce";
            // 
            // udCWWeight
            // 
            this.udCWWeight.Location = new System.Drawing.Point(94, 188);
            this.udCWWeight.Maximum = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.udCWWeight.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.udCWWeight.Name = "udCWWeight";
            this.udCWWeight.Size = new System.Drawing.Size(51, 20);
            this.udCWWeight.TabIndex = 33;
            this.udCWWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udCWWeight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udCWWeight.ValueChanged += new System.EventHandler(this.udCWWeight_ValueChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(9, 190);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(41, 13);
            this.label48.TabIndex = 34;
            this.label48.Text = "Weight";
            // 
            // udCWFall
            // 
            this.udCWFall.DecimalPlaces = 1;
            this.udCWFall.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udCWFall.Location = new System.Drawing.Point(94, 112);
            this.udCWFall.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udCWFall.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udCWFall.Name = "udCWFall";
            this.udCWFall.Size = new System.Drawing.Size(51, 20);
            this.udCWFall.TabIndex = 31;
            this.udCWFall.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udCWFall.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.udCWFall.ValueChanged += new System.EventHandler(this.udCWFall_ValueChanged);
            // 
            // lblCWFall
            // 
            this.lblCWFall.AutoSize = true;
            this.lblCWFall.Location = new System.Drawing.Point(9, 114);
            this.lblCWFall.Name = "lblCWFall";
            this.lblCWFall.Size = new System.Drawing.Size(45, 13);
            this.lblCWFall.TabIndex = 32;
            this.lblCWFall.Text = "Fall time";
            // 
            // udCWRise
            // 
            this.udCWRise.DecimalPlaces = 1;
            this.udCWRise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udCWRise.Location = new System.Drawing.Point(94, 87);
            this.udCWRise.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udCWRise.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udCWRise.Name = "udCWRise";
            this.udCWRise.Size = new System.Drawing.Size(51, 20);
            this.udCWRise.TabIndex = 29;
            this.udCWRise.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udCWRise.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.udCWRise.ValueChanged += new System.EventHandler(this.udCWRise_ValueChanged);
            // 
            // lblCWRise
            // 
            this.lblCWRise.AutoSize = true;
            this.lblCWRise.Location = new System.Drawing.Point(9, 91);
            this.lblCWRise.Name = "lblCWRise";
            this.lblCWRise.Size = new System.Drawing.Size(50, 13);
            this.lblCWRise.TabIndex = 30;
            this.lblCWRise.Text = "Rise time";
            // 
            // udG59DASHDOTRatio
            // 
            this.udG59DASHDOTRatio.DecimalPlaces = 1;
            this.udG59DASHDOTRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udG59DASHDOTRatio.Location = new System.Drawing.Point(94, 162);
            this.udG59DASHDOTRatio.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.udG59DASHDOTRatio.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.udG59DASHDOTRatio.Name = "udG59DASHDOTRatio";
            this.udG59DASHDOTRatio.Size = new System.Drawing.Size(51, 20);
            this.udG59DASHDOTRatio.TabIndex = 27;
            this.udG59DASHDOTRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udG59DASHDOTRatio.Value = new decimal(new int[] {
            30,
            0,
            0,
            65536});
            this.udG59DASHDOTRatio.ValueChanged += new System.EventHandler(this.udG59DASHDOTRatio_ValueChanged);
            // 
            // lblG59dashDotRatio
            // 
            this.lblG59dashDotRatio.AutoSize = true;
            this.lblG59dashDotRatio.Location = new System.Drawing.Point(9, 164);
            this.lblG59dashDotRatio.Name = "lblG59dashDotRatio";
            this.lblG59dashDotRatio.Size = new System.Drawing.Size(65, 13);
            this.lblG59dashDotRatio.TabIndex = 28;
            this.lblG59dashDotRatio.Text = "DASH/DOT";
            // 
            // chkG59IambicRev
            // 
            this.chkG59IambicRev.AutoSize = true;
            this.chkG59IambicRev.Location = new System.Drawing.Point(21, 65);
            this.chkG59IambicRev.Name = "chkG59IambicRev";
            this.chkG59IambicRev.Size = new System.Drawing.Size(85, 17);
            this.chkG59IambicRev.TabIndex = 26;
            this.chkG59IambicRev.Text = "Rev. Paddle";
            this.chkG59IambicRev.UseVisualStyleBackColor = true;
            this.chkG59IambicRev.CheckedChanged += new System.EventHandler(this.chkG59IambicRev_CheckedChanged);
            // 
            // chkG59IambicBmode
            // 
            this.chkG59IambicBmode.AutoSize = true;
            this.chkG59IambicBmode.Location = new System.Drawing.Point(21, 41);
            this.chkG59IambicBmode.Name = "chkG59IambicBmode";
            this.chkG59IambicBmode.Size = new System.Drawing.Size(62, 17);
            this.chkG59IambicBmode.TabIndex = 25;
            this.chkG59IambicBmode.Text = "B mode";
            this.chkG59IambicBmode.UseVisualStyleBackColor = true;
            this.chkG59IambicBmode.CheckedChanged += new System.EventHandler(this.chkG59IambicBmode_CheckedChanged);
            // 
            // chkG59Iambic
            // 
            this.chkG59Iambic.AutoSize = true;
            this.chkG59Iambic.Location = new System.Drawing.Point(21, 17);
            this.chkG59Iambic.Name = "chkG59Iambic";
            this.chkG59Iambic.Size = new System.Drawing.Size(57, 17);
            this.chkG59Iambic.TabIndex = 24;
            this.chkG59Iambic.Text = "Iambic";
            this.chkG59Iambic.UseVisualStyleBackColor = true;
            this.chkG59Iambic.CheckedChanged += new System.EventHandler(this.chkG59Iambic_CheckedChanged);
            // 
            // udCWSpeed
            // 
            this.udCWSpeed.Location = new System.Drawing.Point(94, 137);
            this.udCWSpeed.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.udCWSpeed.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udCWSpeed.Name = "udCWSpeed";
            this.udCWSpeed.Size = new System.Drawing.Size(51, 20);
            this.udCWSpeed.TabIndex = 22;
            this.udCWSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udCWSpeed.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.udCWSpeed.ValueChanged += new System.EventHandler(this.udCWSpeed_ValueChanged);
            // 
            // lblCWSpeed
            // 
            this.lblCWSpeed.AutoSize = true;
            this.lblCWSpeed.Location = new System.Drawing.Point(9, 139);
            this.lblCWSpeed.Name = "lblCWSpeed";
            this.lblCWSpeed.Size = new System.Drawing.Size(66, 13);
            this.lblCWSpeed.TabIndex = 23;
            this.lblCWSpeed.Text = "Keyer speed";
            // 
            // udCWPitch
            // 
            this.udCWPitch.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udCWPitch.Location = new System.Drawing.Point(270, 53);
            this.udCWPitch.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udCWPitch.Name = "udCWPitch";
            this.udCWPitch.Size = new System.Drawing.Size(51, 20);
            this.udCWPitch.TabIndex = 18;
            this.udCWPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udCWPitch.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.udCWPitch.ValueChanged += new System.EventHandler(this.udCWPitch_ValueChanged);
            // 
            // lblCWPitch
            // 
            this.lblCWPitch.AutoSize = true;
            this.lblCWPitch.Location = new System.Drawing.Point(192, 55);
            this.lblCWPitch.Name = "lblCWPitch";
            this.lblCWPitch.Size = new System.Drawing.Size(52, 13);
            this.lblCWPitch.TabIndex = 19;
            this.lblCWPitch.Text = "CW Pitch";
            // 
            // tabRTTYSettings
            // 
            this.tabRTTYSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabRTTYSettings.Controls.Add(this.chkMarkOnly);
            this.tabRTTYSettings.Controls.Add(this.lblRTTYfreq);
            this.tabRTTYSettings.Controls.Add(this.udRTTYfreq);
            this.tabRTTYSettings.Controls.Add(this.lblRTTYTXPreamble);
            this.tabRTTYSettings.Controls.Add(this.udRTTYTXPreamble);
            this.tabRTTYSettings.Controls.Add(this.lblRTTYParity);
            this.tabRTTYSettings.Controls.Add(this.comboRTTYParity);
            this.tabRTTYSettings.Controls.Add(this.lblRTTYStopBits);
            this.tabRTTYSettings.Controls.Add(this.comboRTTYStopBits);
            this.tabRTTYSettings.Controls.Add(this.lblRTTYshift);
            this.tabRTTYSettings.Controls.Add(this.tbRTTYCarrierShift);
            this.tabRTTYSettings.Controls.Add(this.lblBits);
            this.tabRTTYSettings.Controls.Add(this.comboRTTYBits);
            this.tabRTTYSettings.Controls.Add(this.lblRTTYCarrierShift);
            this.tabRTTYSettings.Controls.Add(this.comboRTTYCarrierShift);
            this.tabRTTYSettings.Controls.Add(this.lblBaudRate);
            this.tabRTTYSettings.Controls.Add(this.comboRTTYBaudRate);
            this.tabRTTYSettings.Location = new System.Drawing.Point(4, 22);
            this.tabRTTYSettings.Name = "tabRTTYSettings";
            this.tabRTTYSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabRTTYSettings.Size = new System.Drawing.Size(336, 267);
            this.tabRTTYSettings.TabIndex = 1;
            this.tabRTTYSettings.Text = "RTTY";
            // 
            // chkMarkOnly
            // 
            this.chkMarkOnly.AutoSize = true;
            this.chkMarkOnly.Location = new System.Drawing.Point(195, 80);
            this.chkMarkOnly.Name = "chkMarkOnly";
            this.chkMarkOnly.Size = new System.Drawing.Size(121, 17);
            this.chkMarkOnly.TabIndex = 22;
            this.chkMarkOnly.Text = "MARK only decoder";
            this.chkMarkOnly.UseVisualStyleBackColor = true;
            this.chkMarkOnly.CheckedChanged += new System.EventHandler(this.chkMarkOnly_CheckedChanged);
            // 
            // lblRTTYfreq
            // 
            this.lblRTTYfreq.AutoSize = true;
            this.lblRTTYfreq.Location = new System.Drawing.Point(192, 43);
            this.lblRTTYfreq.Name = "lblRTTYfreq";
            this.lblRTTYfreq.Size = new System.Drawing.Size(63, 13);
            this.lblRTTYfreq.TabIndex = 21;
            this.lblRTTYfreq.Text = "RTTY Pitch";
            // 
            // udRTTYfreq
            // 
            this.udRTTYfreq.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udRTTYfreq.Location = new System.Drawing.Point(268, 40);
            this.udRTTYfreq.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.udRTTYfreq.Name = "udRTTYfreq";
            this.udRTTYfreq.Size = new System.Drawing.Size(51, 20);
            this.udRTTYfreq.TabIndex = 20;
            this.udRTTYfreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udRTTYfreq.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.udRTTYfreq.ValueChanged += new System.EventHandler(this.udRTTYfreq_ValueChanged);
            // 
            // lblRTTYTXPreamble
            // 
            this.lblRTTYTXPreamble.AutoSize = true;
            this.lblRTTYTXPreamble.Location = new System.Drawing.Point(17, 206);
            this.lblRTTYTXPreamble.Name = "lblRTTYTXPreamble";
            this.lblRTTYTXPreamble.Size = new System.Drawing.Size(67, 13);
            this.lblRTTYTXPreamble.TabIndex = 13;
            this.lblRTTYTXPreamble.Text = "TX preamble";
            // 
            // udRTTYTXPreamble
            // 
            this.udRTTYTXPreamble.Location = new System.Drawing.Point(106, 204);
            this.udRTTYTXPreamble.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udRTTYTXPreamble.Name = "udRTTYTXPreamble";
            this.udRTTYTXPreamble.Size = new System.Drawing.Size(49, 20);
            this.udRTTYTXPreamble.TabIndex = 12;
            this.udRTTYTXPreamble.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udRTTYTXPreamble.ValueChanged += new System.EventHandler(this.udRTTYTXPreamble_ValueChanged);
            // 
            // lblRTTYParity
            // 
            this.lblRTTYParity.AutoSize = true;
            this.lblRTTYParity.Location = new System.Drawing.Point(17, 177);
            this.lblRTTYParity.Name = "lblRTTYParity";
            this.lblRTTYParity.Size = new System.Drawing.Size(33, 13);
            this.lblRTTYParity.TabIndex = 11;
            this.lblRTTYParity.Text = "Parity";
            // 
            // comboRTTYParity
            // 
            this.comboRTTYParity.FormattingEnabled = true;
            this.comboRTTYParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd",
            "Zero",
            "One"});
            this.comboRTTYParity.Location = new System.Drawing.Point(97, 174);
            this.comboRTTYParity.Name = "comboRTTYParity";
            this.comboRTTYParity.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboRTTYParity.Size = new System.Drawing.Size(58, 21);
            this.comboRTTYParity.TabIndex = 10;
            this.comboRTTYParity.Text = "None";
            this.comboRTTYParity.SelectedIndexChanged += new System.EventHandler(this.comboRTTYParity_SelectedIndexChanged);
            // 
            // lblRTTYStopBits
            // 
            this.lblRTTYStopBits.AutoSize = true;
            this.lblRTTYStopBits.Location = new System.Drawing.Point(17, 150);
            this.lblRTTYStopBits.Name = "lblRTTYStopBits";
            this.lblRTTYStopBits.Size = new System.Drawing.Size(48, 13);
            this.lblRTTYStopBits.TabIndex = 9;
            this.lblRTTYStopBits.Text = "Stop bits";
            // 
            // comboRTTYStopBits
            // 
            this.comboRTTYStopBits.FormattingEnabled = true;
            this.comboRTTYStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.comboRTTYStopBits.Location = new System.Drawing.Point(109, 147);
            this.comboRTTYStopBits.Name = "comboRTTYStopBits";
            this.comboRTTYStopBits.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboRTTYStopBits.Size = new System.Drawing.Size(46, 21);
            this.comboRTTYStopBits.TabIndex = 8;
            this.comboRTTYStopBits.Text = "1";
            this.comboRTTYStopBits.SelectedIndexChanged += new System.EventHandler(this.comboRTTYStopBits_SelectedIndexChanged);
            // 
            // lblRTTYshift
            // 
            this.lblRTTYshift.AutoSize = true;
            this.lblRTTYshift.Location = new System.Drawing.Point(17, 96);
            this.lblRTTYshift.Name = "lblRTTYshift";
            this.lblRTTYshift.Size = new System.Drawing.Size(59, 13);
            this.lblRTTYshift.TabIndex = 7;
            this.lblRTTYshift.Text = "Carrier shift";
            // 
            // tbRTTYCarrierShift
            // 
            this.tbRTTYCarrierShift.AutoSize = false;
            this.tbRTTYCarrierShift.Location = new System.Drawing.Point(78, 94);
            this.tbRTTYCarrierShift.Maximum = 10000;
            this.tbRTTYCarrierShift.Name = "tbRTTYCarrierShift";
            this.tbRTTYCarrierShift.Size = new System.Drawing.Size(77, 21);
            this.tbRTTYCarrierShift.TabIndex = 6;
            this.tbRTTYCarrierShift.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRTTYCarrierShift.Scroll += new System.EventHandler(this.tbRTTYCarrierShift_Scroll);
            // 
            // lblBits
            // 
            this.lblBits.AutoSize = true;
            this.lblBits.Location = new System.Drawing.Point(17, 123);
            this.lblBits.Name = "lblBits";
            this.lblBits.Size = new System.Drawing.Size(24, 13);
            this.lblBits.TabIndex = 5;
            this.lblBits.Text = "Bits";
            // 
            // comboRTTYBits
            // 
            this.comboRTTYBits.FormattingEnabled = true;
            this.comboRTTYBits.Items.AddRange(new object[] {
            "5",
            "7",
            "8"});
            this.comboRTTYBits.Location = new System.Drawing.Point(120, 120);
            this.comboRTTYBits.Name = "comboRTTYBits";
            this.comboRTTYBits.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboRTTYBits.Size = new System.Drawing.Size(35, 21);
            this.comboRTTYBits.TabIndex = 4;
            this.comboRTTYBits.Text = "5";
            this.comboRTTYBits.SelectedIndexChanged += new System.EventHandler(this.comboRTTYBits_SelectedIndexChanged);
            // 
            // lblRTTYCarrierShift
            // 
            this.lblRTTYCarrierShift.AutoSize = true;
            this.lblRTTYCarrierShift.Location = new System.Drawing.Point(17, 70);
            this.lblRTTYCarrierShift.Name = "lblRTTYCarrierShift";
            this.lblRTTYCarrierShift.Size = new System.Drawing.Size(59, 13);
            this.lblRTTYCarrierShift.TabIndex = 3;
            this.lblRTTYCarrierShift.Text = "Carrier shift";
            // 
            // comboRTTYCarrierShift
            // 
            this.comboRTTYCarrierShift.FormattingEnabled = true;
            this.comboRTTYCarrierShift.Items.AddRange(new object[] {
            "85",
            "160",
            "170",
            "182",
            "200",
            "240",
            "350",
            "425",
            "850",
            "Custom"});
            this.comboRTTYCarrierShift.Location = new System.Drawing.Point(109, 67);
            this.comboRTTYCarrierShift.Name = "comboRTTYCarrierShift";
            this.comboRTTYCarrierShift.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboRTTYCarrierShift.Size = new System.Drawing.Size(46, 21);
            this.comboRTTYCarrierShift.TabIndex = 2;
            this.comboRTTYCarrierShift.Text = "170";
            this.comboRTTYCarrierShift.SelectedIndexChanged += new System.EventHandler(this.udRTTYCarrierShift_SelectedIndexChanged);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(17, 43);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(55, 13);
            this.lblBaudRate.TabIndex = 1;
            this.lblBaudRate.Text = "BaudRate";
            // 
            // comboRTTYBaudRate
            // 
            this.comboRTTYBaudRate.FormattingEnabled = true;
            this.comboRTTYBaudRate.Items.AddRange(new object[] {
            "45",
            "45.45",
            "50",
            "56",
            "75",
            "100",
            "110",
            "150",
            "200",
            "300"});
            this.comboRTTYBaudRate.Location = new System.Drawing.Point(97, 40);
            this.comboRTTYBaudRate.Name = "comboRTTYBaudRate";
            this.comboRTTYBaudRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboRTTYBaudRate.Size = new System.Drawing.Size(58, 21);
            this.comboRTTYBaudRate.TabIndex = 0;
            this.comboRTTYBaudRate.Text = "45";
            this.comboRTTYBaudRate.SelectedIndexChanged += new System.EventHandler(this.comboRTTYBaudRate_SelectedIndexChanged);
            // 
            // tabPSKsettings
            // 
            this.tabPSKsettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPSKsettings.Controls.Add(this.lblPSKPreamble);
            this.tabPSKsettings.Controls.Add(this.udPSKPreamble);
            this.tabPSKsettings.Controls.Add(this.udPSKpitch);
            this.tabPSKsettings.Controls.Add(this.lblPSKpitch);
            this.tabPSKsettings.Location = new System.Drawing.Point(4, 22);
            this.tabPSKsettings.Name = "tabPSKsettings";
            this.tabPSKsettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPSKsettings.Size = new System.Drawing.Size(336, 267);
            this.tabPSKsettings.TabIndex = 2;
            this.tabPSKsettings.Text = "PSK";
            // 
            // lblPSKPreamble
            // 
            this.lblPSKPreamble.AutoSize = true;
            this.lblPSKPreamble.Location = new System.Drawing.Point(39, 101);
            this.lblPSKPreamble.Name = "lblPSKPreamble";
            this.lblPSKPreamble.Size = new System.Drawing.Size(67, 13);
            this.lblPSKPreamble.TabIndex = 15;
            this.lblPSKPreamble.Text = "TX preamble";
            // 
            // udPSKPreamble
            // 
            this.udPSKPreamble.Location = new System.Drawing.Point(122, 99);
            this.udPSKPreamble.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udPSKPreamble.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPSKPreamble.Name = "udPSKPreamble";
            this.udPSKPreamble.Size = new System.Drawing.Size(55, 20);
            this.udPSKPreamble.TabIndex = 14;
            this.udPSKPreamble.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udPSKPreamble.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udPSKPreamble.ValueChanged += new System.EventHandler(this.udPSKPreamble_ValueChanged);
            // 
            // udPSKpitch
            // 
            this.udPSKpitch.Location = new System.Drawing.Point(122, 61);
            this.udPSKpitch.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.udPSKpitch.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udPSKpitch.Name = "udPSKpitch";
            this.udPSKpitch.Size = new System.Drawing.Size(55, 20);
            this.udPSKpitch.TabIndex = 1;
            this.udPSKpitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udPSKpitch.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.udPSKpitch.ValueChanged += new System.EventHandler(this.udPSKpitch_ValueChanged);
            // 
            // lblPSKpitch
            // 
            this.lblPSKpitch.AutoSize = true;
            this.lblPSKpitch.Location = new System.Drawing.Point(39, 63);
            this.lblPSKpitch.Name = "lblPSKpitch";
            this.lblPSKpitch.Size = new System.Drawing.Size(54, 13);
            this.lblPSKpitch.TabIndex = 0;
            this.lblPSKpitch.Text = "PSK pitch";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.tabControl2);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(347, 296);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "LOG settings";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Location = new System.Drawing.Point(3, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(341, 287);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.txtCWbtn6);
            this.tabPage7.Controls.Add(this.txtCWbtn4);
            this.tabPage7.Controls.Add(this.txtCWbtn2);
            this.tabPage7.Controls.Add(this.txtCWbtn5);
            this.tabPage7.Controls.Add(this.txtCWbtn3);
            this.tabPage7.Controls.Add(this.txtCWbtn1);
            this.tabPage7.Controls.Add(this.lblCWMsg6);
            this.tabPage7.Controls.Add(this.txtCWMsg5);
            this.tabPage7.Controls.Add(this.lblCWMsg5);
            this.tabPage7.Controls.Add(this.txtCWMsg6);
            this.tabPage7.Controls.Add(this.lblCWMsg4);
            this.tabPage7.Controls.Add(this.txtCWMsg3);
            this.tabPage7.Controls.Add(this.lblCWMsg3);
            this.tabPage7.Controls.Add(this.txtCWMsg4);
            this.tabPage7.Controls.Add(this.lblCWMsg2);
            this.tabPage7.Controls.Add(this.txtCWMsg1);
            this.tabPage7.Controls.Add(this.lblCWMsg1);
            this.tabPage7.Controls.Add(this.txtCWMsg2);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(333, 261);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "CW";
            // 
            // txtCWbtn6
            // 
            this.txtCWbtn6.Location = new System.Drawing.Point(227, 170);
            this.txtCWbtn6.Name = "txtCWbtn6";
            this.txtCWbtn6.Size = new System.Drawing.Size(79, 20);
            this.txtCWbtn6.TabIndex = 48;
            this.txtCWbtn6.Text = "End QSO";
            // 
            // txtCWbtn4
            // 
            this.txtCWbtn4.Location = new System.Drawing.Point(227, 90);
            this.txtCWbtn4.Name = "txtCWbtn4";
            this.txtCWbtn4.Size = new System.Drawing.Size(79, 20);
            this.txtCWbtn4.TabIndex = 44;
            this.txtCWbtn4.Text = "Short Info";
            // 
            // txtCWbtn2
            // 
            this.txtCWbtn2.Location = new System.Drawing.Point(227, 10);
            this.txtCWbtn2.Name = "txtCWbtn2";
            this.txtCWbtn2.Size = new System.Drawing.Size(79, 20);
            this.txtCWbtn2.TabIndex = 47;
            this.txtCWbtn2.Text = "QRZ?";
            // 
            // txtCWbtn5
            // 
            this.txtCWbtn5.Location = new System.Drawing.Point(77, 170);
            this.txtCWbtn5.Name = "txtCWbtn5";
            this.txtCWbtn5.Size = new System.Drawing.Size(79, 20);
            this.txtCWbtn5.TabIndex = 46;
            this.txtCWbtn5.Text = "Long Info";
            // 
            // txtCWbtn3
            // 
            this.txtCWbtn3.Location = new System.Drawing.Point(77, 90);
            this.txtCWbtn3.Name = "txtCWbtn3";
            this.txtCWbtn3.Size = new System.Drawing.Size(79, 20);
            this.txtCWbtn3.TabIndex = 45;
            this.txtCWbtn3.Text = "Answer";
            // 
            // txtCWbtn1
            // 
            this.txtCWbtn1.Location = new System.Drawing.Point(77, 10);
            this.txtCWbtn1.Name = "txtCWbtn1";
            this.txtCWbtn1.Size = new System.Drawing.Size(79, 20);
            this.txtCWbtn1.TabIndex = 43;
            this.txtCWbtn1.Text = "CQ";
            // 
            // lblCWMsg6
            // 
            this.lblCWMsg6.AutoSize = true;
            this.lblCWMsg6.Location = new System.Drawing.Point(174, 173);
            this.lblCWMsg6.Name = "lblCWMsg6";
            this.lblCWMsg6.Size = new System.Drawing.Size(47, 13);
            this.lblCWMsg6.TabIndex = 42;
            this.lblCWMsg6.Text = "Button 6";
            // 
            // txtCWMsg5
            // 
            this.txtCWMsg5.Location = new System.Drawing.Point(27, 190);
            this.txtCWMsg5.MaxLength = 256;
            this.txtCWMsg5.Multiline = true;
            this.txtCWMsg5.Name = "txtCWMsg5";
            this.txtCWMsg5.Size = new System.Drawing.Size(129, 59);
            this.txtCWMsg5.TabIndex = 39;
            // 
            // lblCWMsg5
            // 
            this.lblCWMsg5.AutoSize = true;
            this.lblCWMsg5.Location = new System.Drawing.Point(24, 173);
            this.lblCWMsg5.Name = "lblCWMsg5";
            this.lblCWMsg5.Size = new System.Drawing.Size(47, 13);
            this.lblCWMsg5.TabIndex = 40;
            this.lblCWMsg5.Text = "Button 5";
            // 
            // txtCWMsg6
            // 
            this.txtCWMsg6.Location = new System.Drawing.Point(177, 190);
            this.txtCWMsg6.MaxLength = 256;
            this.txtCWMsg6.Multiline = true;
            this.txtCWMsg6.Name = "txtCWMsg6";
            this.txtCWMsg6.Size = new System.Drawing.Size(129, 59);
            this.txtCWMsg6.TabIndex = 41;
            // 
            // lblCWMsg4
            // 
            this.lblCWMsg4.AutoSize = true;
            this.lblCWMsg4.Location = new System.Drawing.Point(174, 93);
            this.lblCWMsg4.Name = "lblCWMsg4";
            this.lblCWMsg4.Size = new System.Drawing.Size(47, 13);
            this.lblCWMsg4.TabIndex = 38;
            this.lblCWMsg4.Text = "Button 4";
            // 
            // txtCWMsg3
            // 
            this.txtCWMsg3.Location = new System.Drawing.Point(27, 110);
            this.txtCWMsg3.MaxLength = 256;
            this.txtCWMsg3.Multiline = true;
            this.txtCWMsg3.Name = "txtCWMsg3";
            this.txtCWMsg3.Size = new System.Drawing.Size(129, 59);
            this.txtCWMsg3.TabIndex = 35;
            // 
            // lblCWMsg3
            // 
            this.lblCWMsg3.AutoSize = true;
            this.lblCWMsg3.Location = new System.Drawing.Point(24, 93);
            this.lblCWMsg3.Name = "lblCWMsg3";
            this.lblCWMsg3.Size = new System.Drawing.Size(47, 13);
            this.lblCWMsg3.TabIndex = 36;
            this.lblCWMsg3.Text = "Button 3";
            // 
            // txtCWMsg4
            // 
            this.txtCWMsg4.Location = new System.Drawing.Point(177, 110);
            this.txtCWMsg4.MaxLength = 256;
            this.txtCWMsg4.Multiline = true;
            this.txtCWMsg4.Name = "txtCWMsg4";
            this.txtCWMsg4.Size = new System.Drawing.Size(129, 59);
            this.txtCWMsg4.TabIndex = 37;
            // 
            // lblCWMsg2
            // 
            this.lblCWMsg2.AutoSize = true;
            this.lblCWMsg2.Location = new System.Drawing.Point(174, 13);
            this.lblCWMsg2.Name = "lblCWMsg2";
            this.lblCWMsg2.Size = new System.Drawing.Size(47, 13);
            this.lblCWMsg2.TabIndex = 34;
            this.lblCWMsg2.Text = "Button 2";
            // 
            // txtCWMsg1
            // 
            this.txtCWMsg1.Location = new System.Drawing.Point(27, 30);
            this.txtCWMsg1.MaxLength = 256;
            this.txtCWMsg1.Multiline = true;
            this.txtCWMsg1.Name = "txtCWMsg1";
            this.txtCWMsg1.Size = new System.Drawing.Size(129, 59);
            this.txtCWMsg1.TabIndex = 31;
            // 
            // lblCWMsg1
            // 
            this.lblCWMsg1.AutoSize = true;
            this.lblCWMsg1.Location = new System.Drawing.Point(24, 13);
            this.lblCWMsg1.Name = "lblCWMsg1";
            this.lblCWMsg1.Size = new System.Drawing.Size(47, 13);
            this.lblCWMsg1.TabIndex = 32;
            this.lblCWMsg1.Text = "Button 1";
            // 
            // txtCWMsg2
            // 
            this.txtCWMsg2.Location = new System.Drawing.Point(177, 30);
            this.txtCWMsg2.MaxLength = 256;
            this.txtCWMsg2.Multiline = true;
            this.txtCWMsg2.Name = "txtCWMsg2";
            this.txtCWMsg2.Size = new System.Drawing.Size(129, 59);
            this.txtCWMsg2.TabIndex = 33;
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage8.Controls.Add(this.txtRTTYbtn6);
            this.tabPage8.Controls.Add(this.txtRTTYbtn4);
            this.tabPage8.Controls.Add(this.txtRTTYbtn2);
            this.tabPage8.Controls.Add(this.txtRTTYbtn5);
            this.tabPage8.Controls.Add(this.txtRTTYbtn3);
            this.tabPage8.Controls.Add(this.txtRTTYbtn1);
            this.tabPage8.Controls.Add(this.lblRTTYMsg6);
            this.tabPage8.Controls.Add(this.txtRTTYMsg5);
            this.tabPage8.Controls.Add(this.lblRTTYMsg5);
            this.tabPage8.Controls.Add(this.txtRTTYMsg6);
            this.tabPage8.Controls.Add(this.lblRTTYMsg4);
            this.tabPage8.Controls.Add(this.txtRTTYMsg3);
            this.tabPage8.Controls.Add(this.lblRTTYMsg3);
            this.tabPage8.Controls.Add(this.txtRTTYMsg4);
            this.tabPage8.Controls.Add(this.lblRTTYMsg2);
            this.tabPage8.Controls.Add(this.txtRTTYMsg1);
            this.tabPage8.Controls.Add(this.lblRTTYMsg1);
            this.tabPage8.Controls.Add(this.txtRTTYMsg2);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(333, 261);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "RTTY";
            // 
            // txtRTTYbtn6
            // 
            this.txtRTTYbtn6.Location = new System.Drawing.Point(227, 170);
            this.txtRTTYbtn6.Name = "txtRTTYbtn6";
            this.txtRTTYbtn6.Size = new System.Drawing.Size(79, 20);
            this.txtRTTYbtn6.TabIndex = 48;
            this.txtRTTYbtn6.Text = "End QSO";
            // 
            // txtRTTYbtn4
            // 
            this.txtRTTYbtn4.Location = new System.Drawing.Point(227, 90);
            this.txtRTTYbtn4.Name = "txtRTTYbtn4";
            this.txtRTTYbtn4.Size = new System.Drawing.Size(79, 20);
            this.txtRTTYbtn4.TabIndex = 44;
            this.txtRTTYbtn4.Text = "Short Info";
            // 
            // txtRTTYbtn2
            // 
            this.txtRTTYbtn2.Location = new System.Drawing.Point(227, 10);
            this.txtRTTYbtn2.Name = "txtRTTYbtn2";
            this.txtRTTYbtn2.Size = new System.Drawing.Size(79, 20);
            this.txtRTTYbtn2.TabIndex = 47;
            this.txtRTTYbtn2.Text = "QRZ?";
            // 
            // txtRTTYbtn5
            // 
            this.txtRTTYbtn5.Location = new System.Drawing.Point(77, 170);
            this.txtRTTYbtn5.Name = "txtRTTYbtn5";
            this.txtRTTYbtn5.Size = new System.Drawing.Size(79, 20);
            this.txtRTTYbtn5.TabIndex = 46;
            this.txtRTTYbtn5.Text = "Long Info";
            // 
            // txtRTTYbtn3
            // 
            this.txtRTTYbtn3.Location = new System.Drawing.Point(77, 90);
            this.txtRTTYbtn3.Name = "txtRTTYbtn3";
            this.txtRTTYbtn3.Size = new System.Drawing.Size(79, 20);
            this.txtRTTYbtn3.TabIndex = 45;
            this.txtRTTYbtn3.Text = "Answer";
            // 
            // txtRTTYbtn1
            // 
            this.txtRTTYbtn1.Location = new System.Drawing.Point(77, 10);
            this.txtRTTYbtn1.Name = "txtRTTYbtn1";
            this.txtRTTYbtn1.Size = new System.Drawing.Size(79, 20);
            this.txtRTTYbtn1.TabIndex = 43;
            this.txtRTTYbtn1.Text = "CQ";
            // 
            // lblRTTYMsg6
            // 
            this.lblRTTYMsg6.AutoSize = true;
            this.lblRTTYMsg6.Location = new System.Drawing.Point(174, 173);
            this.lblRTTYMsg6.Name = "lblRTTYMsg6";
            this.lblRTTYMsg6.Size = new System.Drawing.Size(47, 13);
            this.lblRTTYMsg6.TabIndex = 42;
            this.lblRTTYMsg6.Text = "Button 6";
            // 
            // txtRTTYMsg5
            // 
            this.txtRTTYMsg5.Location = new System.Drawing.Point(27, 190);
            this.txtRTTYMsg5.MaxLength = 256;
            this.txtRTTYMsg5.Multiline = true;
            this.txtRTTYMsg5.Name = "txtRTTYMsg5";
            this.txtRTTYMsg5.Size = new System.Drawing.Size(129, 59);
            this.txtRTTYMsg5.TabIndex = 39;
            // 
            // lblRTTYMsg5
            // 
            this.lblRTTYMsg5.AutoSize = true;
            this.lblRTTYMsg5.Location = new System.Drawing.Point(24, 173);
            this.lblRTTYMsg5.Name = "lblRTTYMsg5";
            this.lblRTTYMsg5.Size = new System.Drawing.Size(47, 13);
            this.lblRTTYMsg5.TabIndex = 40;
            this.lblRTTYMsg5.Text = "Button 5";
            // 
            // txtRTTYMsg6
            // 
            this.txtRTTYMsg6.Location = new System.Drawing.Point(177, 190);
            this.txtRTTYMsg6.MaxLength = 256;
            this.txtRTTYMsg6.Multiline = true;
            this.txtRTTYMsg6.Name = "txtRTTYMsg6";
            this.txtRTTYMsg6.Size = new System.Drawing.Size(129, 59);
            this.txtRTTYMsg6.TabIndex = 41;
            // 
            // lblRTTYMsg4
            // 
            this.lblRTTYMsg4.AutoSize = true;
            this.lblRTTYMsg4.Location = new System.Drawing.Point(174, 93);
            this.lblRTTYMsg4.Name = "lblRTTYMsg4";
            this.lblRTTYMsg4.Size = new System.Drawing.Size(47, 13);
            this.lblRTTYMsg4.TabIndex = 38;
            this.lblRTTYMsg4.Text = "Button 4";
            // 
            // txtRTTYMsg3
            // 
            this.txtRTTYMsg3.Location = new System.Drawing.Point(27, 110);
            this.txtRTTYMsg3.MaxLength = 256;
            this.txtRTTYMsg3.Multiline = true;
            this.txtRTTYMsg3.Name = "txtRTTYMsg3";
            this.txtRTTYMsg3.Size = new System.Drawing.Size(129, 59);
            this.txtRTTYMsg3.TabIndex = 35;
            // 
            // lblRTTYMsg3
            // 
            this.lblRTTYMsg3.AutoSize = true;
            this.lblRTTYMsg3.Location = new System.Drawing.Point(24, 93);
            this.lblRTTYMsg3.Name = "lblRTTYMsg3";
            this.lblRTTYMsg3.Size = new System.Drawing.Size(47, 13);
            this.lblRTTYMsg3.TabIndex = 36;
            this.lblRTTYMsg3.Text = "Button 3";
            // 
            // txtRTTYMsg4
            // 
            this.txtRTTYMsg4.Location = new System.Drawing.Point(177, 110);
            this.txtRTTYMsg4.MaxLength = 256;
            this.txtRTTYMsg4.Multiline = true;
            this.txtRTTYMsg4.Name = "txtRTTYMsg4";
            this.txtRTTYMsg4.Size = new System.Drawing.Size(129, 59);
            this.txtRTTYMsg4.TabIndex = 37;
            // 
            // lblRTTYMsg2
            // 
            this.lblRTTYMsg2.AutoSize = true;
            this.lblRTTYMsg2.Location = new System.Drawing.Point(174, 13);
            this.lblRTTYMsg2.Name = "lblRTTYMsg2";
            this.lblRTTYMsg2.Size = new System.Drawing.Size(47, 13);
            this.lblRTTYMsg2.TabIndex = 34;
            this.lblRTTYMsg2.Text = "Button 2";
            // 
            // txtRTTYMsg1
            // 
            this.txtRTTYMsg1.Location = new System.Drawing.Point(27, 30);
            this.txtRTTYMsg1.MaxLength = 256;
            this.txtRTTYMsg1.Multiline = true;
            this.txtRTTYMsg1.Name = "txtRTTYMsg1";
            this.txtRTTYMsg1.Size = new System.Drawing.Size(129, 59);
            this.txtRTTYMsg1.TabIndex = 31;
            // 
            // lblRTTYMsg1
            // 
            this.lblRTTYMsg1.AutoSize = true;
            this.lblRTTYMsg1.Location = new System.Drawing.Point(24, 13);
            this.lblRTTYMsg1.Name = "lblRTTYMsg1";
            this.lblRTTYMsg1.Size = new System.Drawing.Size(47, 13);
            this.lblRTTYMsg1.TabIndex = 32;
            this.lblRTTYMsg1.Text = "Button 1";
            // 
            // txtRTTYMsg2
            // 
            this.txtRTTYMsg2.Location = new System.Drawing.Point(177, 30);
            this.txtRTTYMsg2.MaxLength = 256;
            this.txtRTTYMsg2.Multiline = true;
            this.txtRTTYMsg2.Name = "txtRTTYMsg2";
            this.txtRTTYMsg2.Size = new System.Drawing.Size(129, 59);
            this.txtRTTYMsg2.TabIndex = 33;
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage9.Controls.Add(this.txtPSKbtn6);
            this.tabPage9.Controls.Add(this.txtPSKbtn4);
            this.tabPage9.Controls.Add(this.txtPSKbtn2);
            this.tabPage9.Controls.Add(this.txtPSKbtn5);
            this.tabPage9.Controls.Add(this.txtPSKbtn3);
            this.tabPage9.Controls.Add(this.txtPSKbtn1);
            this.tabPage9.Controls.Add(this.lblPSKMsg6);
            this.tabPage9.Controls.Add(this.txtPSKMsg5);
            this.tabPage9.Controls.Add(this.lblPSKMsg5);
            this.tabPage9.Controls.Add(this.txtPSKMsg6);
            this.tabPage9.Controls.Add(this.lblPSKMsg4);
            this.tabPage9.Controls.Add(this.txtPSKMsg3);
            this.tabPage9.Controls.Add(this.lblPSKMsg3);
            this.tabPage9.Controls.Add(this.txtPSKMsg4);
            this.tabPage9.Controls.Add(this.lblPSKMsg2);
            this.tabPage9.Controls.Add(this.txtPSKMsg1);
            this.tabPage9.Controls.Add(this.lblPSKMsg1);
            this.tabPage9.Controls.Add(this.txtPSKMsg2);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(333, 261);
            this.tabPage9.TabIndex = 2;
            this.tabPage9.Text = "PSK";
            // 
            // txtPSKbtn6
            // 
            this.txtPSKbtn6.Location = new System.Drawing.Point(227, 170);
            this.txtPSKbtn6.Name = "txtPSKbtn6";
            this.txtPSKbtn6.Size = new System.Drawing.Size(79, 20);
            this.txtPSKbtn6.TabIndex = 48;
            this.txtPSKbtn6.Text = "End QSO";
            // 
            // txtPSKbtn4
            // 
            this.txtPSKbtn4.Location = new System.Drawing.Point(227, 90);
            this.txtPSKbtn4.Name = "txtPSKbtn4";
            this.txtPSKbtn4.Size = new System.Drawing.Size(79, 20);
            this.txtPSKbtn4.TabIndex = 44;
            this.txtPSKbtn4.Text = "Short Info";
            // 
            // txtPSKbtn2
            // 
            this.txtPSKbtn2.Location = new System.Drawing.Point(227, 10);
            this.txtPSKbtn2.Name = "txtPSKbtn2";
            this.txtPSKbtn2.Size = new System.Drawing.Size(79, 20);
            this.txtPSKbtn2.TabIndex = 47;
            this.txtPSKbtn2.Text = "QRZ?";
            // 
            // txtPSKbtn5
            // 
            this.txtPSKbtn5.Location = new System.Drawing.Point(77, 170);
            this.txtPSKbtn5.Name = "txtPSKbtn5";
            this.txtPSKbtn5.Size = new System.Drawing.Size(79, 20);
            this.txtPSKbtn5.TabIndex = 46;
            this.txtPSKbtn5.Text = "Long Info";
            // 
            // txtPSKbtn3
            // 
            this.txtPSKbtn3.Location = new System.Drawing.Point(77, 90);
            this.txtPSKbtn3.Name = "txtPSKbtn3";
            this.txtPSKbtn3.Size = new System.Drawing.Size(79, 20);
            this.txtPSKbtn3.TabIndex = 45;
            this.txtPSKbtn3.Text = "Answer";
            // 
            // txtPSKbtn1
            // 
            this.txtPSKbtn1.Location = new System.Drawing.Point(77, 10);
            this.txtPSKbtn1.Name = "txtPSKbtn1";
            this.txtPSKbtn1.Size = new System.Drawing.Size(79, 20);
            this.txtPSKbtn1.TabIndex = 43;
            this.txtPSKbtn1.Text = "CQ";
            // 
            // lblPSKMsg6
            // 
            this.lblPSKMsg6.AutoSize = true;
            this.lblPSKMsg6.Location = new System.Drawing.Point(174, 173);
            this.lblPSKMsg6.Name = "lblPSKMsg6";
            this.lblPSKMsg6.Size = new System.Drawing.Size(47, 13);
            this.lblPSKMsg6.TabIndex = 42;
            this.lblPSKMsg6.Text = "Button 6";
            // 
            // txtPSKMsg5
            // 
            this.txtPSKMsg5.Location = new System.Drawing.Point(27, 190);
            this.txtPSKMsg5.MaxLength = 256;
            this.txtPSKMsg5.Multiline = true;
            this.txtPSKMsg5.Name = "txtPSKMsg5";
            this.txtPSKMsg5.Size = new System.Drawing.Size(129, 59);
            this.txtPSKMsg5.TabIndex = 39;
            // 
            // lblPSKMsg5
            // 
            this.lblPSKMsg5.AutoSize = true;
            this.lblPSKMsg5.Location = new System.Drawing.Point(24, 173);
            this.lblPSKMsg5.Name = "lblPSKMsg5";
            this.lblPSKMsg5.Size = new System.Drawing.Size(47, 13);
            this.lblPSKMsg5.TabIndex = 40;
            this.lblPSKMsg5.Text = "Button 5";
            // 
            // txtPSKMsg6
            // 
            this.txtPSKMsg6.Location = new System.Drawing.Point(177, 190);
            this.txtPSKMsg6.MaxLength = 256;
            this.txtPSKMsg6.Multiline = true;
            this.txtPSKMsg6.Name = "txtPSKMsg6";
            this.txtPSKMsg6.Size = new System.Drawing.Size(129, 59);
            this.txtPSKMsg6.TabIndex = 41;
            // 
            // lblPSKMsg4
            // 
            this.lblPSKMsg4.AutoSize = true;
            this.lblPSKMsg4.Location = new System.Drawing.Point(174, 93);
            this.lblPSKMsg4.Name = "lblPSKMsg4";
            this.lblPSKMsg4.Size = new System.Drawing.Size(47, 13);
            this.lblPSKMsg4.TabIndex = 38;
            this.lblPSKMsg4.Text = "Button 4";
            // 
            // txtPSKMsg3
            // 
            this.txtPSKMsg3.Location = new System.Drawing.Point(27, 110);
            this.txtPSKMsg3.MaxLength = 256;
            this.txtPSKMsg3.Multiline = true;
            this.txtPSKMsg3.Name = "txtPSKMsg3";
            this.txtPSKMsg3.Size = new System.Drawing.Size(129, 59);
            this.txtPSKMsg3.TabIndex = 35;
            // 
            // lblPSKMsg3
            // 
            this.lblPSKMsg3.AutoSize = true;
            this.lblPSKMsg3.Location = new System.Drawing.Point(24, 93);
            this.lblPSKMsg3.Name = "lblPSKMsg3";
            this.lblPSKMsg3.Size = new System.Drawing.Size(47, 13);
            this.lblPSKMsg3.TabIndex = 36;
            this.lblPSKMsg3.Text = "Button 3";
            // 
            // txtPSKMsg4
            // 
            this.txtPSKMsg4.Location = new System.Drawing.Point(177, 110);
            this.txtPSKMsg4.MaxLength = 256;
            this.txtPSKMsg4.Multiline = true;
            this.txtPSKMsg4.Name = "txtPSKMsg4";
            this.txtPSKMsg4.Size = new System.Drawing.Size(129, 59);
            this.txtPSKMsg4.TabIndex = 37;
            // 
            // lblPSKMsg2
            // 
            this.lblPSKMsg2.AutoSize = true;
            this.lblPSKMsg2.Location = new System.Drawing.Point(174, 13);
            this.lblPSKMsg2.Name = "lblPSKMsg2";
            this.lblPSKMsg2.Size = new System.Drawing.Size(47, 13);
            this.lblPSKMsg2.TabIndex = 34;
            this.lblPSKMsg2.Text = "Button 2";
            // 
            // txtPSKMsg1
            // 
            this.txtPSKMsg1.Location = new System.Drawing.Point(27, 30);
            this.txtPSKMsg1.MaxLength = 256;
            this.txtPSKMsg1.Multiline = true;
            this.txtPSKMsg1.Name = "txtPSKMsg1";
            this.txtPSKMsg1.Size = new System.Drawing.Size(129, 59);
            this.txtPSKMsg1.TabIndex = 31;
            // 
            // lblPSKMsg1
            // 
            this.lblPSKMsg1.AutoSize = true;
            this.lblPSKMsg1.Location = new System.Drawing.Point(24, 13);
            this.lblPSKMsg1.Name = "lblPSKMsg1";
            this.lblPSKMsg1.Size = new System.Drawing.Size(47, 13);
            this.lblPSKMsg1.TabIndex = 32;
            this.lblPSKMsg1.Text = "Button 1";
            // 
            // txtPSKMsg2
            // 
            this.txtPSKMsg2.Location = new System.Drawing.Point(177, 30);
            this.txtPSKMsg2.MaxLength = 256;
            this.txtPSKMsg2.Multiline = true;
            this.txtPSKMsg2.Name = "txtPSKMsg2";
            this.txtPSKMsg2.Size = new System.Drawing.Size(129, 59);
            this.txtPSKMsg2.TabIndex = 33;
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage10.Controls.Add(this.label50);
            this.tabPage10.Controls.Add(this.label51);
            this.tabPage10.Controls.Add(this.txtStnZone);
            this.tabPage10.Controls.Add(this.label21);
            this.tabPage10.Controls.Add(this.label20);
            this.tabPage10.Controls.Add(this.label19);
            this.tabPage10.Controls.Add(this.label18);
            this.tabPage10.Controls.Add(this.label17);
            this.tabPage10.Controls.Add(this.label16);
            this.tabPage10.Controls.Add(this.lblStnLOC);
            this.tabPage10.Controls.Add(this.txtStnLOC);
            this.tabPage10.Controls.Add(this.lblStnInfoTxt);
            this.tabPage10.Controls.Add(this.txtStnCALL);
            this.tabPage10.Controls.Add(this.lblStnCallSign);
            this.tabPage10.Controls.Add(this.txtStnInfoTxt);
            this.tabPage10.Controls.Add(this.lblStnQTH);
            this.tabPage10.Controls.Add(this.txtStnQTH);
            this.tabPage10.Controls.Add(this.lblStnInfoName);
            this.tabPage10.Controls.Add(this.txtStnName);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(333, 261);
            this.tabPage10.TabIndex = 3;
            this.tabPage10.Text = "Stn Info";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(213, 175);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(44, 13);
            this.label50.TabIndex = 59;
            this.label50.Text = "<Zone>";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(30, 157);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(32, 13);
            this.label51.TabIndex = 58;
            this.label51.Text = "Zone";
            // 
            // txtStnZone
            // 
            this.txtStnZone.Location = new System.Drawing.Point(33, 172);
            this.txtStnZone.MaxLength = 256;
            this.txtStnZone.Multiline = true;
            this.txtStnZone.Name = "txtStnZone";
            this.txtStnZone.Size = new System.Drawing.Size(129, 20);
            this.txtStnZone.TabIndex = 57;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(213, 216);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 56;
            this.label21.Text = "<My Info>";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(213, 139);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 13);
            this.label20.TabIndex = 55;
            this.label20.Text = "<My LOC>";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(213, 101);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 13);
            this.label19.TabIndex = 54;
            this.label19.Text = "<My QTH>";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(213, 63);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 13);
            this.label18.TabIndex = 53;
            this.label18.Text = "<My Name>";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(213, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 13);
            this.label17.TabIndex = 52;
            this.label17.Text = "<My CALL>";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(205, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 13);
            this.label16.TabIndex = 51;
            this.label16.Text = "Replace Macro:";
            // 
            // lblStnLOC
            // 
            this.lblStnLOC.AutoSize = true;
            this.lblStnLOC.Location = new System.Drawing.Point(30, 121);
            this.lblStnLOC.Name = "lblStnLOC";
            this.lblStnLOC.Size = new System.Drawing.Size(43, 13);
            this.lblStnLOC.TabIndex = 50;
            this.lblStnLOC.Text = "Locator";
            // 
            // txtStnLOC
            // 
            this.txtStnLOC.Location = new System.Drawing.Point(33, 136);
            this.txtStnLOC.MaxLength = 256;
            this.txtStnLOC.Multiline = true;
            this.txtStnLOC.Name = "txtStnLOC";
            this.txtStnLOC.Size = new System.Drawing.Size(129, 20);
            this.txtStnLOC.TabIndex = 49;
            // 
            // lblStnInfoTxt
            // 
            this.lblStnInfoTxt.AutoSize = true;
            this.lblStnInfoTxt.Location = new System.Drawing.Point(30, 194);
            this.lblStnInfoTxt.Name = "lblStnInfoTxt";
            this.lblStnInfoTxt.Size = new System.Drawing.Size(45, 13);
            this.lblStnInfoTxt.TabIndex = 48;
            this.lblStnInfoTxt.Text = "Info text";
            // 
            // txtStnCALL
            // 
            this.txtStnCALL.Location = new System.Drawing.Point(33, 24);
            this.txtStnCALL.MaxLength = 10;
            this.txtStnCALL.Name = "txtStnCALL";
            this.txtStnCALL.Size = new System.Drawing.Size(129, 20);
            this.txtStnCALL.TabIndex = 0;
            // 
            // lblStnCallSign
            // 
            this.lblStnCallSign.AutoSize = true;
            this.lblStnCallSign.Location = new System.Drawing.Point(30, 8);
            this.lblStnCallSign.Name = "lblStnCallSign";
            this.lblStnCallSign.Size = new System.Drawing.Size(46, 13);
            this.lblStnCallSign.TabIndex = 1;
            this.lblStnCallSign.Text = "Call sign";
            // 
            // txtStnInfoTxt
            // 
            this.txtStnInfoTxt.Location = new System.Drawing.Point(33, 210);
            this.txtStnInfoTxt.MaxLength = 256;
            this.txtStnInfoTxt.Multiline = true;
            this.txtStnInfoTxt.Name = "txtStnInfoTxt";
            this.txtStnInfoTxt.Size = new System.Drawing.Size(129, 48);
            this.txtStnInfoTxt.TabIndex = 47;
            // 
            // lblStnQTH
            // 
            this.lblStnQTH.AutoSize = true;
            this.lblStnQTH.Location = new System.Drawing.Point(30, 83);
            this.lblStnQTH.Name = "lblStnQTH";
            this.lblStnQTH.Size = new System.Drawing.Size(30, 13);
            this.lblStnQTH.TabIndex = 46;
            this.lblStnQTH.Text = "QTH";
            // 
            // txtStnQTH
            // 
            this.txtStnQTH.Location = new System.Drawing.Point(33, 98);
            this.txtStnQTH.MaxLength = 256;
            this.txtStnQTH.Multiline = true;
            this.txtStnQTH.Name = "txtStnQTH";
            this.txtStnQTH.Size = new System.Drawing.Size(129, 20);
            this.txtStnQTH.TabIndex = 45;
            // 
            // lblStnInfoName
            // 
            this.lblStnInfoName.AutoSize = true;
            this.lblStnInfoName.Location = new System.Drawing.Point(30, 45);
            this.lblStnInfoName.Name = "lblStnInfoName";
            this.lblStnInfoName.Size = new System.Drawing.Size(35, 13);
            this.lblStnInfoName.TabIndex = 44;
            this.lblStnInfoName.Text = "Name";
            // 
            // txtStnName
            // 
            this.txtStnName.Location = new System.Drawing.Point(33, 60);
            this.txtStnName.MaxLength = 256;
            this.txtStnName.Multiline = true;
            this.txtStnName.Name = "txtStnName";
            this.txtStnName.Size = new System.Drawing.Size(129, 20);
            this.txtStnName.TabIndex = 43;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.udTelnetServerPort);
            this.tabPage2.Controls.Add(this.lblServerPort);
            this.tabPage2.Controls.Add(this.chkIPV6);
            this.tabPage2.Controls.Add(this.chkRobot);
            this.tabPage2.Controls.Add(this.lblTelnetHostAddress);
            this.tabPage2.Controls.Add(this.txtTelnetHostAddress);
            this.tabPage2.Controls.Add(this.chkTelnet);
            this.tabPage2.Controls.Add(this.chkRXOnly);
            this.tabPage2.Controls.Add(this.grpAudioTests);
            this.tabPage2.Controls.Add(this.chkAlwaysOnTop);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(347, 296);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Misc";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chkMRLog);
            this.groupBox8.Controls.Add(this.chkMRMedian);
            this.groupBox8.Controls.Add(this.chkSDRmode);
            this.groupBox8.Location = new System.Drawing.Point(175, 20);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(158, 100);
            this.groupBox8.TabIndex = 22;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Morse Runner mode";
            // 
            // chkMRLog
            // 
            this.chkMRLog.AutoSize = true;
            this.chkMRLog.Location = new System.Drawing.Point(48, 72);
            this.chkMRLog.Name = "chkMRLog";
            this.chkMRLog.Size = new System.Drawing.Size(63, 17);
            this.chkMRLog.TabIndex = 15;
            this.chkMRLog.Text = "Log -dB";
            this.chkMRLog.UseVisualStyleBackColor = true;
            this.chkMRLog.CheckedChanged += new System.EventHandler(this.chkMRLog_CheckedChanged);
            // 
            // chkMRMedian
            // 
            this.chkMRMedian.AutoSize = true;
            this.chkMRMedian.Location = new System.Drawing.Point(48, 47);
            this.chkMRMedian.Name = "chkMRMedian";
            this.chkMRMedian.Size = new System.Drawing.Size(61, 17);
            this.chkMRMedian.TabIndex = 14;
            this.chkMRMedian.Text = "Median";
            this.chkMRMedian.UseVisualStyleBackColor = true;
            this.chkMRMedian.CheckedChanged += new System.EventHandler(this.chkMRMedian_CheckedChanged);
            // 
            // chkSDRmode
            // 
            this.chkSDRmode.AutoSize = true;
            this.chkSDRmode.Location = new System.Drawing.Point(49, 22);
            this.chkSDRmode.Name = "chkSDRmode";
            this.chkSDRmode.Size = new System.Drawing.Size(59, 17);
            this.chkSDRmode.TabIndex = 13;
            this.chkSDRmode.Text = "Enable";
            this.chkSDRmode.UseVisualStyleBackColor = true;
            this.chkSDRmode.CheckedChanged += new System.EventHandler(this.chkSDRmode_CheckedChanged);
            // 
            // udTelnetServerPort
            // 
            this.udTelnetServerPort.Location = new System.Drawing.Point(105, 239);
            this.udTelnetServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.udTelnetServerPort.Name = "udTelnetServerPort";
            this.udTelnetServerPort.Size = new System.Drawing.Size(64, 20);
            this.udTelnetServerPort.TabIndex = 21;
            this.udTelnetServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udTelnetServerPort.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(17, 239);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(59, 13);
            this.lblServerPort.TabIndex = 20;
            this.lblServerPort.Text = "Server port";
            // 
            // chkIPV6
            // 
            this.chkIPV6.AutoSize = true;
            this.chkIPV6.Location = new System.Drawing.Point(20, 163);
            this.chkIPV6.Name = "chkIPV6";
            this.chkIPV6.Size = new System.Drawing.Size(49, 17);
            this.chkIPV6.TabIndex = 18;
            this.chkIPV6.Text = "IPV6";
            this.chkIPV6.UseVisualStyleBackColor = true;
            this.chkIPV6.CheckedChanged += new System.EventHandler(this.chkIPV6_CheckedChanged);
            // 
            // chkRobot
            // 
            this.chkRobot.AutoSize = true;
            this.chkRobot.Location = new System.Drawing.Point(20, 98);
            this.chkRobot.Name = "chkRobot";
            this.chkRobot.Size = new System.Drawing.Size(55, 17);
            this.chkRobot.TabIndex = 17;
            this.chkRobot.Text = "Robot";
            this.chkRobot.UseVisualStyleBackColor = true;
            this.chkRobot.CheckedChanged += new System.EventHandler(this.chkRobot_CheckedChanged);
            // 
            // lblTelnetHostAddress
            // 
            this.lblTelnetHostAddress.AutoSize = true;
            this.lblTelnetHostAddress.Location = new System.Drawing.Point(17, 204);
            this.lblTelnetHostAddress.Name = "lblTelnetHostAddress";
            this.lblTelnetHostAddress.Size = new System.Drawing.Size(78, 13);
            this.lblTelnetHostAddress.TabIndex = 16;
            this.lblTelnetHostAddress.Text = "Server address";
            // 
            // txtTelnetHostAddress
            // 
            this.txtTelnetHostAddress.Location = new System.Drawing.Point(97, 201);
            this.txtTelnetHostAddress.MaxLength = 16;
            this.txtTelnetHostAddress.Name = "txtTelnetHostAddress";
            this.txtTelnetHostAddress.Size = new System.Drawing.Size(72, 20);
            this.txtTelnetHostAddress.TabIndex = 15;
            this.txtTelnetHostAddress.Text = "127.0.0.1";
            this.txtTelnetHostAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTelnet
            // 
            this.chkTelnet.AutoSize = true;
            this.chkTelnet.Location = new System.Drawing.Point(20, 129);
            this.chkTelnet.Name = "chkTelnet";
            this.chkTelnet.Size = new System.Drawing.Size(90, 17);
            this.chkTelnet.TabIndex = 14;
            this.chkTelnet.Text = "Telnet Server";
            this.chkTelnet.UseVisualStyleBackColor = true;
            this.chkTelnet.CheckedChanged += new System.EventHandler(this.chkTelnet_CheckedChanged);
            // 
            // chkRXOnly
            // 
            this.chkRXOnly.AutoSize = true;
            this.chkRXOnly.Location = new System.Drawing.Point(20, 67);
            this.chkRXOnly.Name = "chkRXOnly";
            this.chkRXOnly.Size = new System.Drawing.Size(63, 17);
            this.chkRXOnly.TabIndex = 11;
            this.chkRXOnly.Text = "RX only";
            this.chkRXOnly.UseVisualStyleBackColor = true;
            this.chkRXOnly.CheckedChanged += new System.EventHandler(this.chkRXOnly_CheckedChanged);
            // 
            // grpAudioTests
            // 
            this.grpAudioTests.Controls.Add(this.label3);
            this.grpAudioTests.Controls.Add(this.label4);
            this.grpAudioTests.Controls.Add(this.label5);
            this.grpAudioTests.Controls.Add(this.lblAudioStreamOutputLatencyValuelabel);
            this.grpAudioTests.Controls.Add(this.button1);
            this.grpAudioTests.Controls.Add(this.lblAudioStreamInputLatencyValue);
            this.grpAudioTests.Controls.Add(this.lblAudioStreamSampleRateValue);
            this.grpAudioTests.Location = new System.Drawing.Point(175, 152);
            this.grpAudioTests.Name = "grpAudioTests";
            this.grpAudioTests.Size = new System.Drawing.Size(158, 112);
            this.grpAudioTests.TabIndex = 9;
            this.grpAudioTests.TabStop = false;
            this.grpAudioTests.Text = "Audio test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output latency";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Input latency";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sample rate";
            // 
            // lblAudioStreamOutputLatencyValuelabel
            // 
            this.lblAudioStreamOutputLatencyValuelabel.AutoSize = true;
            this.lblAudioStreamOutputLatencyValuelabel.Location = new System.Drawing.Point(107, 41);
            this.lblAudioStreamOutputLatencyValuelabel.Name = "lblAudioStreamOutputLatencyValuelabel";
            this.lblAudioStreamOutputLatencyValuelabel.Size = new System.Drawing.Size(28, 13);
            this.lblAudioStreamOutputLatencyValuelabel.TabIndex = 4;
            this.lblAudioStreamOutputLatencyValuelabel.Text = "0mS";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAudioStreamInfo_Click);
            // 
            // lblAudioStreamInputLatencyValue
            // 
            this.lblAudioStreamInputLatencyValue.AutoSize = true;
            this.lblAudioStreamInputLatencyValue.Location = new System.Drawing.Point(107, 17);
            this.lblAudioStreamInputLatencyValue.Name = "lblAudioStreamInputLatencyValue";
            this.lblAudioStreamInputLatencyValue.Size = new System.Drawing.Size(28, 13);
            this.lblAudioStreamInputLatencyValue.TabIndex = 3;
            this.lblAudioStreamInputLatencyValue.Text = "0mS";
            // 
            // lblAudioStreamSampleRateValue
            // 
            this.lblAudioStreamSampleRateValue.AutoSize = true;
            this.lblAudioStreamSampleRateValue.Location = new System.Drawing.Point(107, 65);
            this.lblAudioStreamSampleRateValue.Name = "lblAudioStreamSampleRateValue";
            this.lblAudioStreamSampleRateValue.Size = new System.Drawing.Size(37, 13);
            this.lblAudioStreamSampleRateValue.TabIndex = 5;
            this.lblAudioStreamSampleRateValue.Text = "48000";
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(20, 35);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(92, 17);
            this.chkAlwaysOnTop.TabIndex = 6;
            this.chkAlwaysOnTop.Text = "Always on top";
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            this.chkAlwaysOnTop.CheckedChanged += new System.EventHandler(this.chkAlwaysOnTop_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Stop bits";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.comboBox1.Location = new System.Drawing.Point(120, 137);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox1.Size = new System.Drawing.Size(35, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Carrier shift";
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(78, 84);
            this.trackBar1.Maximum = 10000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(77, 21);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Bits";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "5(baudot)",
            "7(ascii)",
            "8(ascii)"});
            this.comboBox2.Location = new System.Drawing.Point(87, 110);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox2.Size = new System.Drawing.Size(68, 21);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.Text = "5(baudot)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Carrier shift";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "85",
            "160",
            "170",
            "182",
            "200",
            "240",
            "350",
            "425",
            "850",
            "Custom",
            ""});
            this.comboBox3.Location = new System.Drawing.Point(97, 57);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox3.Size = new System.Drawing.Size(58, 21);
            this.comboBox3.TabIndex = 2;
            this.comboBox3.Text = "170";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "BaudRate";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "45",
            "45.45",
            "50",
            "56",
            "75",
            "100",
            "110",
            "150",
            "200",
            "300"});
            this.comboBox4.Location = new System.Drawing.Point(97, 30);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox4.Size = new System.Drawing.Size(58, 21);
            this.comboBox4.TabIndex = 0;
            this.comboBox4.Text = "45";
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.White;
            this.colorDialog1.FullOpen = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Location = new System.Drawing.Point(222, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(108, 102);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DirectX type";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(19, 68);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(67, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Software";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(19, 26);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Hardware";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton3);
            this.groupBox5.Controls.Add(this.radioButton4);
            this.groupBox5.Location = new System.Drawing.Point(218, 21);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(113, 82);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Driver";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(26, 47);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(60, 17);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "DirectX";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(26, 23);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(50, 17);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "GDI+";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label33);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.numericUpDown1);
            this.groupBox6.Controls.Add(this.label35);
            this.groupBox6.Controls.Add(this.numericUpDown2);
            this.groupBox6.Controls.Add(this.label36);
            this.groupBox6.Controls.Add(this.checkBox1);
            this.groupBox6.Controls.Add(this.label37);
            this.groupBox6.Controls.Add(this.label38);
            this.groupBox6.Controls.Add(this.comboBox5);
            this.groupBox6.Controls.Add(this.numericUpDown3);
            this.groupBox6.Controls.Add(this.label39);
            this.groupBox6.Controls.Add(this.checkBox2);
            this.groupBox6.Controls.Add(this.numericUpDown4);
            this.groupBox6.Controls.Add(this.label40);
            this.groupBox6.Controls.Add(this.numericUpDown5);
            this.groupBox6.Controls.Add(this.label41);
            this.groupBox6.Controls.Add(this.numericUpDown6);
            this.groupBox6.Controls.Add(this.label42);
            this.groupBox6.Location = new System.Drawing.Point(15, 21);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(175, 269);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Display settings";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(16, 224);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(56, 13);
            this.label33.TabIndex = 24;
            this.label33.Text = "Line color:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(109, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 20);
            this.button2.TabIndex = 13;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Location = new System.Drawing.Point(103, 194);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(16, 196);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(71, 13);
            this.label35.TabIndex = 23;
            this.label35.Text = "Refresh (mS):";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 1;
            this.numericUpDown2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(103, 168);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown2.TabIndex = 20;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(16, 170);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(63, 13);
            this.label36.TabIndex = 21;
            this.label36.Text = "Scope time:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(117, 148);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(16, 148);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(80, 13);
            this.label37.TabIndex = 18;
            this.label37.Text = "Monitor reverse";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(16, 124);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(34, 13);
            this.label38.TabIndex = 17;
            this.label38.Text = "Mode";
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "Panafall",
            "Panafall_inv",
            "Panascope",
            "Panascope_inv"});
            this.comboBox5.Location = new System.Drawing.Point(61, 121);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(93, 21);
            this.comboBox5.TabIndex = 16;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 1;
            this.numericUpDown3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown3.Location = new System.Drawing.Point(103, 95);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown3.TabIndex = 14;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown3.Value = new decimal(new int[] {
            351,
            0,
            0,
            -2147418112});
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(15, 97);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(54, 13);
            this.label39.TabIndex = 15;
            this.label39.Text = "Cal. offset";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(77, 71);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown4.Location = new System.Drawing.Point(103, 21);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown4.TabIndex = 11;
            this.numericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown4.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(16, 23);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(54, 13);
            this.label40.TabIndex = 12;
            this.label40.Text = "High level";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown5.Location = new System.Drawing.Point(103, 44);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown5.TabIndex = 9;
            this.numericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown5.Value = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(15, 46);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(52, 13);
            this.label41.TabIndex = 10;
            this.label41.Text = "Low level";
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(103, 69);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown6.TabIndex = 7;
            this.numericUpDown6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown6.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(16, 71);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(55, 13);
            this.label42.TabIndex = 8;
            this.label42.Text = "Averaging";
            // 
            // Setup
            // 
            this.ClientSize = new System.Drawing.Size(397, 388);
            this.Controls.Add(this.tbSetup);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(413, 426);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(413, 426);
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CWExpert Setup";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Setup_Closing);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMonitorFrequncy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAudioOutputVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLatency)).EndInit();
            this.tbSetup.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpDisplayDriver.ResumeLayout(false);
            this.grpDisplayDriver.PerformLayout();
            this.grpMisc.ResumeLayout(false);
            this.grpMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScopeTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayCalOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDisplayLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAveraging)).EndInit();
            this.tabG59.ResumeLayout(false);
            this.tabRadio.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTXSwicthTime)).EndInit();
            this.grpGenesisSi570.ResumeLayout(false);
            this.grpGenesisSi570.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570I2CAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSi570_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMsgRptTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTXOffDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTXIfShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSmeterCalOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udNBThreshold)).EndInit();
            this.tabPA10.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain10m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain6m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain160m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain80m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain12m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain40m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain15m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain30m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain17m)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPAGain20m)).EndInit();
            this.tabDSP.ResumeLayout(false);
            this.tabDSP.PerformLayout();
            this.grpDSPRXImageReject.ResumeLayout(false);
            this.grpDSPRXImageReject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRXGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRXGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRXPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRXPhase)).EndInit();
            this.grpDSPTXImageReject.ResumeLayout(false);
            this.grpDSPTXImageReject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTXGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTXGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTXPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTXPhase)).EndInit();
            this.tabG11.ResumeLayout(false);
            this.tabG11.PerformLayout();
            this.grpG11BandFilters.ResumeLayout(false);
            this.grpG11BandFilters.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabCWSettings.ResumeLayout(false);
            this.tabCWSettings.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCWDebounce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWFall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWRise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udG59DASHDOTRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udCWPitch)).EndInit();
            this.tabRTTYSettings.ResumeLayout(false);
            this.tabRTTYSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRTTYfreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRTTYTXPreamble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRTTYCarrierShift)).EndInit();
            this.tabPSKsettings.ResumeLayout(false);
            this.tabPSKsettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPSKPreamble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPSKpitch)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTelnetServerPort)).EndInit();
            this.grpAudioTests.ResumeLayout(false);
            this.grpAudioTests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblAudioDriver;
        private System.Windows.Forms.Label lblAudioLatency;
        private System.Windows.Forms.ComboBox comboAudioDriver;
        private System.Windows.Forms.NumericUpDown udLatency;
        private System.Windows.Forms.ComboBox comboAudioInput;
        private System.Windows.Forms.Label lblAudioSampleRate;
        private System.Windows.Forms.Label lblAudioInput;
        private System.Windows.Forms.ComboBox comboAudioSampleRate;
        private System.Windows.Forms.TabControl tbSetup;
        private System.Windows.Forms.ComboBox comboAudioOutput;
        private System.Windows.Forms.Label lblAudioOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAudioBuffer;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblStnCallSign;
        public System.Windows.Forms.TextBox txtStnCALL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.Label lblAveraging;
        private System.Windows.Forms.NumericUpDown udAveraging;
        private System.Windows.Forms.GroupBox grpMisc;
        private System.Windows.Forms.NumericUpDown udDisplayHigh;
        private System.Windows.Forms.Label lblDisplayHigh;
        private System.Windows.Forms.NumericUpDown udDisplayLow;
        private System.Windows.Forms.Label lblDisplayLow;
        private System.Windows.Forms.GroupBox grpAudioTests;
        public System.Windows.Forms.CheckBox chkRXOnly;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox grpDisplayDriver;
        private System.Windows.Forms.RadioButton radDisplayDirectX;
        private System.Windows.Forms.RadioButton radDisplayGDI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAudioStreamOutputLatencyValuelabel;
        private System.Windows.Forms.Label lblAudioStreamInputLatencyValue;
        private System.Windows.Forms.Label lblAudioStreamSampleRateValue;
        private System.Windows.Forms.CheckBox chkDisplayAveraging;
        private System.Windows.Forms.CheckBox chkSDRmode;
        private System.Windows.Forms.TabPage tabG59;
        private System.Windows.Forms.NumericUpDown udDisplayCalOffset;
        private System.Windows.Forms.Label lblCalOffset;
        private System.Windows.Forms.TabControl tabRadio;
        private System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.NumericUpDown udNBThreshold;
        private System.Windows.Forms.Label lblNBThresholds;
        private System.Windows.Forms.NumericUpDown udCWPitch;
        private System.Windows.Forms.Label lblCWPitch;
        private System.Windows.Forms.NumericUpDown udSmeterCalOffset;
        private System.Windows.Forms.Label lblSmeterCalOffset;
        private System.Windows.Forms.CheckBox chkPA10;
        private System.Windows.Forms.CheckBox chkRX2;
        private System.Windows.Forms.CheckBox chkPTTinv;
        private System.Windows.Forms.ComboBox comboDisplayMode;
        private System.Windows.Forms.Label lblDisplayMode;
        public System.Windows.Forms.NumericUpDown udCWSpeed;
        private System.Windows.Forms.Label lblCWSpeed;
        private System.Windows.Forms.Label lblPhase;
        public System.Windows.Forms.TrackBar tbTXPhase;
        private System.Windows.Forms.TabPage tabDSP;
        private System.Windows.Forms.GroupBox grpDSPTXImageReject;
        public System.Windows.Forms.NumericUpDown udTXPhase;
        private System.Windows.Forms.Label lblGainValue;
        public System.Windows.Forms.NumericUpDown udTXGain;
        public System.Windows.Forms.TrackBar tbTXGain;
        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Label lblPhaseValue;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown udG59DASHDOTRatio;
        private System.Windows.Forms.Label lblG59dashDotRatio;
        public System.Windows.Forms.CheckBox chkG59IambicRev;
        public System.Windows.Forms.CheckBox chkG59IambicBmode;
        public System.Windows.Forms.CheckBox chkG59Iambic;
        public System.Windows.Forms.NumericUpDown udCWFall;
        private System.Windows.Forms.Label lblCWFall;
        public System.Windows.Forms.NumericUpDown udCWRise;
        private System.Windows.Forms.Label lblCWRise;
        public System.Windows.Forms.NumericUpDown udTXIfShift;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown udTXOffDelay;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown udMsgRptTime;
        private System.Windows.Forms.Label lblMsgRpt;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCWSettings;
        private System.Windows.Forms.TabPage tabRTTYSettings;
        private System.Windows.Forms.Label lblBaudRate;
        public System.Windows.Forms.ComboBox comboRTTYBaudRate;
        private System.Windows.Forms.Label lblBits;
        private System.Windows.Forms.ComboBox comboRTTYBits;
        private System.Windows.Forms.Label lblRTTYCarrierShift;
        public System.Windows.Forms.ComboBox comboRTTYCarrierShift;
        private System.Windows.Forms.Label lblRTTYshift;
        private System.Windows.Forms.TrackBar tbRTTYCarrierShift;
        private System.Windows.Forms.Label lblRTTYStopBits;
        private System.Windows.Forms.ComboBox comboRTTYStopBits;
        private System.Windows.Forms.Label lblRTTYParity;
        private System.Windows.Forms.ComboBox comboRTTYParity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label lblRTTYTXPreamble;
        private System.Windows.Forms.NumericUpDown udRTTYTXPreamble;
        private System.Windows.Forms.GroupBox grpGenesisSi570;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSi570Test;
        public System.Windows.Forms.NumericUpDown udSi570_3;
        public System.Windows.Forms.NumericUpDown udSi570_2;
        public System.Windows.Forms.NumericUpDown udSi570_1;
        private System.Windows.Forms.Label lblSi570I2CAddress;
        public System.Windows.Forms.NumericUpDown udSi570I2CAddress;
        private System.Windows.Forms.Label lblRadioModel;
        private System.Windows.Forms.ComboBox comboRadioModel;
        private System.Windows.Forms.Label lblAudioVolts;
        private System.Windows.Forms.NumericUpDown udAudioOutputVoltage;
        private System.Windows.Forms.TabPage tabPA10;
        private System.Windows.Forms.NumericUpDown udPAGain160m;
        private System.Windows.Forms.Label lblBand160m;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown udPAGain10m;
        private System.Windows.Forms.NumericUpDown udPAGain6m;
        private System.Windows.Forms.Label lblBand6m;
        private System.Windows.Forms.Label lblBand80m;
        private System.Windows.Forms.Label lblBand10m;
        private System.Windows.Forms.NumericUpDown udPAGain80m;
        private System.Windows.Forms.NumericUpDown udPAGain12m;
        private System.Windows.Forms.Label lblBand40m;
        private System.Windows.Forms.Label lblBand12m;
        private System.Windows.Forms.NumericUpDown udPAGain40m;
        private System.Windows.Forms.NumericUpDown udPAGain15m;
        private System.Windows.Forms.Label lblBand30m;
        private System.Windows.Forms.Label lblBand15m;
        private System.Windows.Forms.NumericUpDown udPAGain30m;
        private System.Windows.Forms.NumericUpDown udPAGain17m;
        private System.Windows.Forms.Label lblBand20m;
        private System.Windows.Forms.Label lblBand17m;
        private System.Windows.Forms.NumericUpDown udPAGain20m;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        public System.Windows.Forms.TextBox txtCWbtn6;
        public System.Windows.Forms.TextBox txtCWbtn4;
        public System.Windows.Forms.TextBox txtCWbtn2;
        public System.Windows.Forms.TextBox txtCWbtn5;
        public System.Windows.Forms.TextBox txtCWbtn3;
        public System.Windows.Forms.TextBox txtCWbtn1;
        private System.Windows.Forms.Label lblCWMsg6;
        public System.Windows.Forms.TextBox txtCWMsg5;
        private System.Windows.Forms.Label lblCWMsg5;
        public System.Windows.Forms.TextBox txtCWMsg6;
        private System.Windows.Forms.Label lblCWMsg4;
        public System.Windows.Forms.TextBox txtCWMsg3;
        private System.Windows.Forms.Label lblCWMsg3;
        public System.Windows.Forms.TextBox txtCWMsg4;
        private System.Windows.Forms.Label lblCWMsg2;
        public System.Windows.Forms.TextBox txtCWMsg1;
        private System.Windows.Forms.Label lblCWMsg1;
        public System.Windows.Forms.TextBox txtCWMsg2;
        public System.Windows.Forms.TextBox txtRTTYbtn6;
        public System.Windows.Forms.TextBox txtRTTYbtn4;
        public System.Windows.Forms.TextBox txtRTTYbtn2;
        public System.Windows.Forms.TextBox txtRTTYbtn5;
        public System.Windows.Forms.TextBox txtRTTYbtn3;
        public System.Windows.Forms.TextBox txtRTTYbtn1;
        private System.Windows.Forms.Label lblRTTYMsg6;
        public System.Windows.Forms.TextBox txtRTTYMsg5;
        private System.Windows.Forms.Label lblRTTYMsg5;
        public System.Windows.Forms.TextBox txtRTTYMsg6;
        private System.Windows.Forms.Label lblRTTYMsg4;
        public System.Windows.Forms.TextBox txtRTTYMsg3;
        private System.Windows.Forms.Label lblRTTYMsg3;
        public System.Windows.Forms.TextBox txtRTTYMsg4;
        private System.Windows.Forms.Label lblRTTYMsg2;
        public System.Windows.Forms.TextBox txtRTTYMsg1;
        private System.Windows.Forms.Label lblRTTYMsg1;
        public System.Windows.Forms.TextBox txtRTTYMsg2;
        public System.Windows.Forms.TextBox txtPSKbtn6;
        public System.Windows.Forms.TextBox txtPSKbtn4;
        public System.Windows.Forms.TextBox txtPSKbtn2;
        public System.Windows.Forms.TextBox txtPSKbtn5;
        public System.Windows.Forms.TextBox txtPSKbtn3;
        public System.Windows.Forms.TextBox txtPSKbtn1;
        private System.Windows.Forms.Label lblPSKMsg6;
        public System.Windows.Forms.TextBox txtPSKMsg5;
        private System.Windows.Forms.Label lblPSKMsg5;
        public System.Windows.Forms.TextBox txtPSKMsg6;
        private System.Windows.Forms.Label lblPSKMsg4;
        public System.Windows.Forms.TextBox txtPSKMsg3;
        private System.Windows.Forms.Label lblPSKMsg3;
        public System.Windows.Forms.TextBox txtPSKMsg4;
        private System.Windows.Forms.Label lblPSKMsg2;
        public System.Windows.Forms.TextBox txtPSKMsg1;
        private System.Windows.Forms.Label lblPSKMsg1;
        public System.Windows.Forms.TextBox txtPSKMsg2;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.Label lblStnInfoTxt;
        public System.Windows.Forms.TextBox txtStnInfoTxt;
        private System.Windows.Forms.Label lblStnQTH;
        public System.Windows.Forms.TextBox txtStnQTH;
        private System.Windows.Forms.Label lblStnInfoName;
        public System.Windows.Forms.TextBox txtStnName;
        private System.Windows.Forms.Label lblStnLOC;
        public System.Windows.Forms.TextBox txtStnLOC;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboAudioMonitor;
        private System.Windows.Forms.Label lblAudioMonitor;
        private System.Windows.Forms.Label lblMonitorFreq;
        public System.Windows.Forms.NumericUpDown udMonitorFrequncy;
        private System.Windows.Forms.CheckBox chkWaterfallReverse;
        private System.Windows.Forms.Label lblWaterfallReverse;
        private System.Windows.Forms.Label lblMonitorDriver;
        private System.Windows.Forms.ComboBox comboMonitorDriver;
        private System.Windows.Forms.CheckBox chkTXSwap;
        private System.Windows.Forms.CheckBox chkRXSwap;
        private System.Windows.Forms.Button bttnTXClearAll;
        private System.Windows.Forms.Button bttnTXClearBand;
        private System.Windows.Forms.Button bttnTXCalAll;
        private System.Windows.Forms.Button bttnTXCalBand;
        private System.Windows.Forms.Label lblRTTYfreq;
        private System.Windows.Forms.NumericUpDown udRTTYfreq;
        private System.Windows.Forms.TabPage tabPSKsettings;
        private System.Windows.Forms.NumericUpDown udPSKpitch;
        private System.Windows.Forms.Label lblPSKpitch;
        private System.Windows.Forms.NumericUpDown udScopeTime;
        private System.Windows.Forms.Label lblScopeTime;
        private System.Windows.Forms.CheckBox chkTelnet;
        private System.Windows.Forms.Label lblTelnetHostAddress;
        public System.Windows.Forms.TextBox txtTelnetHostAddress;
        private System.Windows.Forms.CheckBox chkQSK;
        public System.Windows.Forms.NumericUpDown udTXSwicthTime;
        private System.Windows.Forms.Label lblTXSwichTime;
        private System.Windows.Forms.TabPage tabG11;
        private System.Windows.Forms.GroupBox grpG11BandFilters;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkG11B6040_CH2;
        private System.Windows.Forms.CheckBox chkG11B80_CH2;
        private System.Windows.Forms.CheckBox chkG11B160_CH2;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox chkG11B151210_CH1;
        private System.Windows.Forms.CheckBox chkG11B1210_CH1;
        private System.Windows.Forms.CheckBox chkG11B201715_CH1;
        private System.Windows.Forms.CheckBox chkG11B1715_CH1;
        private System.Windows.Forms.CheckBox chkG11B2017_CH1;
        private System.Windows.Forms.CheckBox chkG11B3020_CH1;
        private System.Windows.Forms.CheckBox chkG11B6040_CH1;
        private System.Windows.Forms.CheckBox chkG11B80_CH1;
        private System.Windows.Forms.CheckBox chkG11B151210_CH2;
        private System.Windows.Forms.CheckBox chkG11B1210_CH2;
        private System.Windows.Forms.CheckBox chkG11B201715_CH2;
        private System.Windows.Forms.CheckBox chkG11B1715_CH2;
        private System.Windows.Forms.CheckBox chkG11B2017_CH2;
        private System.Windows.Forms.CheckBox chkG11B3020_CH2;
        private System.Windows.Forms.CheckBox chkG11B6_CH1;
        private System.Windows.Forms.CheckBox chkG11B6_CH2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radDirectXSW;
        private System.Windows.Forms.RadioButton radDirectXHW;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnLineColor;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.NumericUpDown udDisplayRefresh;
        private System.Windows.Forms.Label lblDisplayRefresh;
        private System.Windows.Forms.CheckBox chkFillPanadapter;
        private System.Windows.Forms.Label lblFillPanadapter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.GroupBox grpDSPRXImageReject;
        private System.Windows.Forms.Button bttnRXClearAll;
        private System.Windows.Forms.Button bttnRXClearBand;
        private System.Windows.Forms.Button bttnRXCallAll;
        private System.Windows.Forms.Button bttnRXCalBand;
        private System.Windows.Forms.Label label44;
        public System.Windows.Forms.NumericUpDown udRXGain;
        public System.Windows.Forms.TrackBar tbRXGain;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        public System.Windows.Forms.NumericUpDown udRXPhase;
        public System.Windows.Forms.TrackBar tbRXPhase;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.RadioButton radIQFixed;
        public System.Windows.Forms.CheckBox chkMarkOnly;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lblMonitorMode;
        private System.Windows.Forms.ComboBox comboMonitorMode;
        public System.Windows.Forms.CheckBox chkRobot;
        public System.Windows.Forms.CheckBox chkIPV6;
        private System.Windows.Forms.Label lblServerPort;
        public System.Windows.Forms.NumericUpDown udTelnetServerPort;
        public System.Windows.Forms.NumericUpDown udCWDebounce;
        private System.Windows.Forms.Label label49;
        public System.Windows.Forms.NumericUpDown udCWWeight;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label lblPSKPreamble;
        private System.Windows.Forms.NumericUpDown udPSKPreamble;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        public System.Windows.Forms.TextBox txtStnZone;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ComboBox comboCWAGC;
        private System.Windows.Forms.CheckBox chkG11B4030_CH1;
        private System.Windows.Forms.CheckBox chkG11B4030_CH2;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkMRLog;
        private System.Windows.Forms.CheckBox chkMRMedian;
        private System.Windows.Forms.RadioButton radIQWBIR;
        private System.Windows.Forms.CheckBox chkG11MultiBand;
    }
}