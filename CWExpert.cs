//=================================================================
// CWExpert.cs
//=================================================================
// Copyright (C) 2011,2012 S56A YT7PWR
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.IO;
using GenesisG59;
using AnalogGAuge;
using System.Drawing.Imaging;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;


namespace CWExpert
{
    #region enum

    public enum IQ_correction
    {
        FIXED = 0,
        WBIR,
    }

    public enum RenderType
    {
        HARDWARE = 0,
        SOFTWARE,
        NONE,
    }

    public enum AGCMode
    {
        FIRST = -1,
        OFF,
        LONG,
        SLOW,
        MED,
        FAST,
        CUSTOM,
        LAST,
    }

    public enum MeterType
    {
        SIGNAL_STRENGTH = 0,
        DIR_PWR,
        REFL_PWR,
        SWR,
    }

    public enum Model
    {
        GENESIS_G59USB = 1,
        GENESIS_G11,
    }

    public enum Mode
    {
        CW = 0,
        MFSK16,
        MFSK8,
        RTTY,
        THROB1,
        THROB2,
        THROB4,
        BPSK31,
        BPSK63,
        BPSK125,
        BPSK250,
        QPSK31,
        QPSK63,
        QPSK125,
        QPSK250,
        MT63,
        FELDHELL,
        FMHELL
    }

    public enum Keyer_mode
    {
        ZERO = 0,
        HandKey,
        Iambic,
        IambicReverse,
        PHONE,
        Iambic_B_Mode,
        Iambic_Reverse_B_Mode,
        TUNE,
        CWX,
    };

    public enum KeyerState
    {
        Dot,
        Dash,
        Silence,
    }

    public enum TuneMode
    {
        Off,
        VFOA,
        VFOB,
    }

    public enum WBIR_State
    {
        FastAdapt,
        SlowAdapt,
        NoAdapt,
        StopAdapt,
        DelayAdapt,
        MOXAdapt,
    }

    public enum BandFilter
    {
        B160M = 1,
        B80M = 2,
        B60M = 3,
        B40M = 3,
        B30M = 4,
        B20M = 4,
        B17M = 5,
        B15M = 5,
        B12M = 6,
        B10M = 6,
        B6M = 7,
    }

    public enum Band
    {
        FIRST = 0,
        B160M,
        B80M,
        B40M,
        B30M,
        B20M,
        B17M,
        B15M,
        B12M,
        B10M,
        B6M,
        LAST,
    }

    public enum DisplayDriver
    {
        FIRST = -1,
        GDI,
        DIRECTX,
        LAST,
    }

    public enum DisplayMode
    {
        PANADAPTER,
        WATERFALL,
        PANAFALL,
        PANASCOPE,
        PANAFALL_INV,
        PANASCOPE_INV,
        SCOPE,
        OFF,
    }

    #endregion

    #region structures

    [StructLayout(LayoutKind.Sequential)]
    public struct TextEdit
    {
        public int hWnd;
        public int Xpos;
        public int Width;
    }

    #endregion

    unsafe public partial class CWExpert : Form
    {
        #region DLL imports

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("msvcrt.dll", EntryPoint = "memcpy")]
        public static extern void memcpy(void* destptr, void* srcptr, int n);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr window, int message, int wparam, int lparam);

        [DllImport("Receiver.dll", EntryPoint = "SetRXOsc")]
        unsafe public static extern int SetRXOsc(uint thread, uint subrx, double freq);

        [DllImport("Receiver.dll", EntryPoint = "SetRXFilter")]
        public static extern int SetRXFilter(uint thread, uint subrx, double low, double high);

        [DllImport("Receiver.dll", EntryPoint = "Process_Panadapter")]
        unsafe static public extern void GetPanadapter(uint thread, float* results);

        [DllImport("Receiver.dll", EntryPoint = "SetTRX")]
        unsafe static public extern void SetTRX(uint thread, bool trx_on);

        [DllImport("Receiver.dll", EntryPoint = "CalculateRXMeter")]
        public static extern float CalculateRXMeter(uint thread, uint subrx, MeterType MT);

        [DllImport("Receiver.dll", EntryPoint = "Destroy_SDR")]
        public static extern void Exit();

        [DllImport("Receiver.dll", EntryPoint = "SetSampleRate")]
        public static extern int SetSampleRate(double sampleRate);

        [DllImport("Receiver.dll", EntryPoint = "Setup_SDR")]
        public static extern void SetupSDR(string data_path);

        [DllImport("Receiver.dll", EntryPoint = "Release_Update")]
        unsafe static public extern void ReleaseUpdate();

        [DllImport("Receiver.dll", EntryPoint = "SetDSPBuflen")]
        public static extern void ResizeSDR(uint thread, uint DSPsize);

        [DllImport("Receiver.dll", EntryPoint = "SetCorrectRXIQw")]
        public static extern void SetCorrectRXIQw(uint thread, uint subrx, float real, float imag, uint index);

        [DllImport("Receiver.dll", EntryPoint = "SetCorrectRXIQMu")]
        public static extern void SetCorrectIQMu(uint thread, uint subrx, double setit);

        [DllImport("Receiver.dll", EntryPoint = "GetCorrectRXIQMu")]
        public static extern float GetCorrectIQMu(uint thread, uint subrx);

        [DllImport("Receiver.dll", EntryPoint = "SetCorrectIQEnable")]
        public static extern void SetCorrectIQEnable(uint thread, uint setit);

        [DllImport("Receiver.dll", EntryPoint = "SetNB")]
        public static extern void SetNB(uint thread, uint subrx, bool setit);

        [DllImport("Receiver.dll", EntryPoint = "SetNBvals")]
        public static extern void SetNBvals(uint thread, uint subrx, double threshold);

        [DllImport("Receiver.dll", EntryPoint = "SetSubRXSt")]
        unsafe public static extern void SetRXOn(uint thread, uint subrx, bool setit);

        [DllImport("Receiver.dll", EntryPoint = "SetRXAGC")]
        public static extern void SetRXAGC(uint thread, uint subrx, AGCMode setit);

        [DllImport("Receiver.dll", EntryPoint = "SetRXAGCTop")]
        public static extern void SetRXAGCMaxGain(uint thread, uint subrx, double max_agc);

        [DllImport("Receiver.dll", EntryPoint = "SetIQFixed")]
        public static extern void SetIQFixed(uint thread, uint subrx, uint setit, float gain, float phase);

        [DllImport("Receiver.dll", EntryPoint = "SetIQBalanced")]
        public static extern void SetIQBalanced(uint thread, bool setit);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        #endregion

        #region structures

        [StructLayout(LayoutKind.Sequential)]
        public struct BandStack
        {
            public double vfoA;
            public double vfoB;
            public double losc;
            public int filter;
            public int zoom;
            public int pan;
        }

        #endregion

        #region variable definition

        delegate void CrossThreadCallback(string command, string data);
        delegate void KeyboardThreadCommand(string command, int param_1, string param_2);

        const int WM_VSCROLL = 0x115;
        const int SB_BOTTOM = 7;
        public bool booting = false;
        private int WM_LBUTTONDOWN = 0x0201;
        private int WM_LBUTTONUP = 0x0202;
        private int WM_KEYDOWN = 0x0100;
        private int WM_KEYUP = 0x0101;
        private int VK_F1 = 0x70;
        private int VK_F2 = 0x71;
        private int VK_F3 = 0x72;
        private int VK_F4 = 0x73;
        private int VK_F5 = 0x74;
        private int VK_F6 = 0x75;
        private int VK_F7 = 0x76;
        private int VK_F8 = 0x77;
        private int VK_RETURN = 0x0D;
        private int WM_APP = 0x8000;
        private int topWindow = 0;
        private int editPanel = 0;
        MessageHelper msg;
        public Setup SetupForm;
        TextEdit[] edits;
        public CWDecode cwDecoder;
        public CWEncode cwEncoder;
        public RTTY rtty;
        public PSK psk;
        public bool pause_DisplayThread = false;
        public float[] display_buffer;
        private Thread display_thread;
        private Thread Smeter_thread;
        //        private Thread wbir_thread;
        public AutoResetEvent display_event;
        public bool standalone = true;
        private bool runDisplay = false;
        public GenesisG59.G59 genesis;
        public float Smeter_cal_offset = 0f;
        private float Smeter_avg = -999.999f;
        private float pwr_avg = 0.0f;
        private TuneMode tuning_mode = TuneMode.Off;
        private bool cw_message_repeat = false;
        private bool rtty_message_repeat = false;
        private bool psk_message_repeat = false;
        private Thread cw_message_thread;
        private Thread rtty_message_thread;
        private Thread psk_message_thread;
        private AutoResetEvent cw_message_event = new AutoResetEvent(false);
        private AutoResetEvent rtty_message_event = new AutoResetEvent(false);
        private AutoResetEvent psk_message_event = new AutoResetEvent(false);
        public delegate void ReadCallbackFunction(int type, int data);
        private static Bitmap picMonitor_bmp;
        private float[] picMonitor_buffer = new float[4096];
        public RingBuffer output_ring_buf;
        public RingBuffer mon_ring_buf;
        public float[] tx_image_phase_table;
        public float[] tx_image_gain_table;
        private Thread[] audio_process_thread;
        private bool once = false;
        private bool get_state = false;
        private bool freq_mult = false;
        public TelnetServer telnet_server;
        const int MAXRX = 4;
        public DXClusterClient DXClusterForm;
        public bool[] G11BandFiltersCH1 = new bool[20];
        public bool[] G11BandFiltersCH2 = new bool[20];
        private byte[] waterfall_memory;
        private int waterfall_bmp_size;
        private int waterfall_bmp_stride;
        private DataStream waterfall_data_stream;
        private Rectangle waterfall_rect;
        private Surface backbuf;
        private static VertexBuffer PanLine_vb = null;
        private static VertexBuffer PanLine_vb_fill = null;
        private Texture WaterfallTexture = null;
        private Texture PanadapterTexture = null;
        public Device waterfall_dx_device = null;
        private Device panadapter_dx_device = null;
        private Sprite Waterfall_Sprite = null;
        private Sprite Panadapter_Sprite = null;
        private Rectangle Waterfall_texture_size;
        private static Rectangle Panadapter_texture_size;
        private Control monitor_target = null;
        private static Vertex[] PanLine_verts = null;
        private static Vertex[] PanLine_verts_fill = null;
        private static Vertex[] ScopeLine_verts = null;
        private static VertexBuffer ScopeLine_vb = null;
        private static float[] panadapterX_data = null;
        private static float[] panadapterX_scope_data = null;
        private static float[] panadapterX_scope_data_mark = null;
        private static float[] panadapterX_scope_data_space = null;
        private Bitmap waterfall_bmp = null;
        public bool LogVisible = true;
        public float[] rx_image_phase_table;
        public float[] rx_image_gain_table;
        public LOG log_book;
        public string log_file_path = Application.StartupPath + "\\LogBook.xml";
        private bool rtbCH1_scroll_down = true;
        private bool rtbCH2_scroll_down = true;
        double psk_vfoa_max = 0.0;
        double psk_vfoa_min = 0.0;
        double psk_vfob_max = 0.0;
        double psk_vfob_min = 0.0;
        bool tune_psk = false;
        public static float[] average_buffer;					// Averaged display data buffer
        public Keyboard keyboard;
        public bool ROBOT = false;
        private MeterType TX_meter_type = MeterType.DIR_PWR;
        public bool hst = false;
        public Recorder recorder;
        public bool g11_multiband = false;
        public DXCC dxcc;

        #endregion

        #region properites

        private IQ_correction iq_correction = IQ_correction.FIXED;
        public IQ_correction IQcorrection
        {
            get { return iq_correction; }
            set { iq_correction = value; }
        }

        private bool tx_split = false;
        public bool TXSplit
        {
            get { return tx_split; }
            set { tx_split = value; }
        }

        private bool reverse_waterfall = false;
        public bool ReverseWaterfall
        {
            get { return reverse_waterfall; }
            set { reverse_waterfall = value; }
        }

        #region G59/G11

        private double tx_pwr = 0.0;
        public double TXPWR
        {
            get { return tx_pwr; }
            set { tx_pwr = value; }
        }

        private int tx_switch_time = 60;
        public int TXSwitchTime
        {
            set
            {
                tx_switch_time = value;
            }
        }

        private bool use_telnet = false;
        public bool UseTelnet
        {
            set
            {
                use_telnet = value;

                if (!booting)
                {
                    if (use_telnet)
                        telnet_server = new TelnetServer(SetupForm.txtTelnetHostAddress.Text.ToString(),
                            (int)SetupForm.udTelnetServerPort.Value, SetupForm.chkIPV6.Checked);
                    else if (telnet_server != null)
                        telnet_server.Close();
                }
            }
        }

        private double nb_vals = 20.0;
        public double NBvals
        {
            set
            {
                nb_vals = value;

                if (!booting)
                {
                    SetNBvals(0, 0, nb_vals);
                    SetNBvals(0, 1, nb_vals);
                }
            }
        }

        public bool pa10_present = false;
        public bool PA10_present
        {
            get { return pa10_present; }
            set
            {
                pa10_present = value;

                if (genesis != null)
                {
                    if (value)
                        genesis.WriteToDevice(21, 1);
                    else
                        genesis.WriteToDevice(21, 0);
                }
            }
        }

        private Model current_model = Model.GENESIS_G59USB;
        public Model CurrentModel
        {
            get { return current_model; }
            set
            {
                bool pwr = PWR;
                current_model = value;

                if (current_model == Model.GENESIS_G59USB)
                {
                    grpGenesisRadio.Text = "G59";
                    grpSMeter.Text = "G59";
                }
                else if (current_model == Model.GENESIS_G11)
                {
                    grpGenesisRadio.Text = "G11";
                    grpSMeter.Text = "G11";
                }

                if (PWR)
                {
                    PWR = false;

                    Thread.Sleep(100);
                    PWR = pwr;
                }
                else
                {
                    if (genesis != null)
                    {
                        genesis.booting = false;
                        G59Init();
                        genesis.si570_i2c_address = 170;
                        genesis.si570_fxtal = 114272200.0;
                        genesis.HSDiv = 11;
                        bool conn = false;
                        conn = genesis.Connected;

                        if (conn)
                        {
                            lblUSB.BackColor = Color.Green;
                        }
                        else
                        {
                            lblUSB.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private bool genesis_sec_rx_ant = false;
        public bool GenesisSecRXAnt
        {
            get { return genesis_sec_rx_ant; }
            set
            {
                if (current_model == Model.GENESIS_G59USB ||
                    current_model == Model.GENESIS_G11)
                {
                    genesis_sec_rx_ant = value;

                    if (!booting)
                    {
                        if (value == true)
                            genesis.WriteToDevice(23, 1);
                        else
                            genesis.WriteToDevice(23, 0);
                    }
                }
            }
        }

        public short g59_ptt_inverted = 0;
        public short G59PTT_Inverted
        {
            get { return g59_ptt_inverted; }
            set
            {
                g59_ptt_inverted = value;
                genesis.WriteToDevice(28, value);
            }
        }

        private int filter_width_vfoA = 64;
        public int FilterWidthVFOA
        {
            get { return filter_width_vfoA; }
            set
            {
                filter_width_vfoA = value;

                if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                {
                    switch (filter_width_vfoA)
                    {
                        case 1000:
                            radFilter1K.Checked = true;
                            break;
                        case 500:
                            radFilter500.Checked = true;
                            break;
                        case 250:
                            radFilter250.Checked = true;
                            break;
                        case 100:
                            radFilter100.Checked = true;
                            break;
                        case 50:
                            radFilter50.Checked = true;
                            break;
                        default:
                            radFilterVar.Checked = true;
                            tbFilterWidth.Value = filter_width_vfoA;
                            break;
                    }
                }
            }
        }

        private int filter_width_vfoB = 64;
        public int FilterWidthVFOB
        {
            get { return filter_width_vfoB; }
            set
            {
                filter_width_vfoB = value;

                if (tuning_mode == TuneMode.VFOB)
                {
                    switch (filter_width_vfoB)
                    {
                        case 1000:
                            radFilter1K.Checked = true;
                            break;
                        case 500:
                            radFilter500.Checked = true;
                            break;
                        case 250:
                            radFilter250.Checked = true;
                            break;
                        case 100:
                            radFilter100.Checked = true;
                            break;
                        case 50:
                            radFilter50.Checked = true;
                            break;
                        default:
                            radFilterVar.Checked = true;
                            tbFilterWidth.Value = filter_width_vfoB;
                            break;
                    }
                }
            }
        }

        private Band current_band = Band.B20M;
        public Band CurrentBand
        {
            get { return current_band; }
            set
            {
                current_band = value;
                Audio.iq_balancer_reset = true;
                SetupForm.udTXGain.Value = (decimal)tx_image_gain_table[(int)current_band];
                SetupForm.udTXPhase.Value = (decimal)tx_image_phase_table[(int)current_band];
                SetupForm.udRXGain.Value = (decimal)rx_image_gain_table[(int)current_band];
                SetupForm.udRXPhase.Value = (decimal)rx_image_phase_table[(int)current_band];
                Audio.iq_balancer_reset = true;
            }
        }

        private bool tune = false;
        public bool TUNE
        {
            set
            {
                tune = value;
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                if (!tune)
                {
                    btnTune.BackColor = Color.WhiteSmoke;

                    switch (new_mode)
                    {
                        case Mode.CW:
                            {
                                if (cwEncoder != null)
                                    cwEncoder.TUN = false;
                            }
                            break;

                        case Mode.RTTY:
                            {
                                if (rtty != null)
                                    rtty.trx.tune = false;
                            }
                            break;

                        case Mode.BPSK31:
                        case Mode.BPSK63:
                        case Mode.BPSK125:
                        case Mode.BPSK250:
                        case Mode.QPSK31:
                        case Mode.QPSK63:
                        case Mode.QPSK125:
                        case Mode.QPSK250:
                            {
                                if (psk != null)
                                    psk.trx.tune = false;
                            }
                            break;
                    }
                }
                else
                {
                    if (btnStartMR.Checked)
                    {
                        genesis.WriteToDevice(18, (long)Keyer_mode.TUNE);

                        btnTune.BackColor = Color.Red;

                        switch (new_mode)
                        {
                            case Mode.CW:
                                {
                                    if (cwEncoder != null)
                                        cwEncoder.TUN = true;
                                }
                                break;

                            case Mode.RTTY:
                                {
                                    if (rtty != null)
                                        rtty.trx.tune = true;
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                {
                                    if (psk != null)
                                        psk.trx.tune = true;
                                }
                                break;
                        }

                    }
                }
            }
        }

        private bool mox = false;
        public bool MOX
        {
            get { return mox; }
            set
            {
                mox = value;
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                if (mox)
                {
                    btnTX.BackColor = Color.Red;

                    if (Audio.SDRmode)
                    {
                        Audio.mox_switch_time = 0;
                        Audio.MOX = true;
                        genesis.WriteToDevice(13, tx_switch_time / 3);
                        Thread.Sleep(1);

                        switch (op_mode_vfoB)
                        {
                            case Mode.CW:
                                {
                                    if (tx_split)
                                        genesis.Set_frequency((long)(vfob * 1e6 - cwEncoder.TXIfShift), true);
                                    else
                                        genesis.Set_frequency((long)(Math.Round(vfoa * 1e6, 6) - cwEncoder.TXIfShift), true);
                                }
                                break;

                            case Mode.RTTY:
                                {
                                    if (tx_split)
                                        genesis.Set_frequency((long)(vfob * 1e6 - rtty.TXIfShift), true);
                                    else
                                        genesis.Set_frequency((long)(Math.Round(vfoa * 1e6, 6) - rtty.TXIfShift), true);
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                {
                                    if (tx_split)
                                        genesis.Set_frequency((long)(vfob * 1e6 - psk.TXIfShift), true);
                                    else
                                        genesis.Set_frequency((long)(vfoa * 1e6 - psk.TXIfShift), true);
                                }
                                break;
                        }

                        Thread.Sleep(1);
                    }
                }
                else
                {
                    btnTX.BackColor = Color.WhiteSmoke;

                    if (Audio.SDRmode)
                    {
                        Audio.mox_switch_time = 0;
                        Audio.MOX = false;
                        genesis.WriteToDevice(14, 0);
                        Thread.Sleep(1);
                        genesis.Set_frequency((long)(losc * 1e6), true);
                        Thread.Sleep(1);
                        G59_set_keyer();
                        //TUNE = false;

                        cwDecoder.reset_after_mox = true;
                        cwEncoder.local_mox = false;
                        cwEncoder.run_transmiter = false;
                        rtty.reset_after_mox = true;
                        rtty.MOX = false;
                        rtty.run_transmiter = false;
                        rtty.RBmessage.Restart();
                        psk.reset_after_mox = true;
                        psk.MOX = false;
                        psk.run_transmiter = false;
                        psk.RBmessage.Restart();
                        //cwEncoder.reset_after_mox = true;
                        cwEncoder.MOX = false;
                        cwEncoder.run_transmiter = false;
                        cwEncoder.RBmessage.Restart();

                        output_ring_buf.Restart();
                        mon_ring_buf.Restart();
                        Audio.iq_balancer_reset = true;
                    }

                    if (keyboard != null && keyboard.MOX)
                        keyboard.MOX = false;
                }
            }
        }

        private double vfoa = 14.0;
        public double VFOA
        {
            get { return vfoa; }
            set
            {
                try
                {
                    vfoa = Math.Round(value, 6);

                    if (vfoa > (losc * 1e6 + Audio.SampleRate / 2) / 1e6)
                        vfoa = (losc * 1e6 + Audio.SampleRate / 2) / 1e6;
                    else if (vfoa < (losc * 1e6 - Audio.SampleRate / 2) / 1e6)
                        vfoa = (losc * 1e6 - Audio.SampleRate / 2) / 1e6;

                    txtVFOA.Text = vfoa.ToString("f6");

                    if (iq_correction == IQ_correction.FIXED)
                    {
                        SetRXIQGainPhase(1, rx_image_gain_table[(int)current_band],
                            rx_image_phase_table[(int)current_band]);
                        RX_phase_gain();
                    }

                    switch (op_mode_vfoA)
                    {
                        case Mode.CW:
                            {
#if(DirectX)
                                DX.VFOA = vfoa;
#endif
                                Display_GDI.VFOA = vfoa;

                                SetRXOsc(0, 0, (losc - vfoa) * 1e6 + cw_pitch);
                                SetRXFilter(0, 0, cw_pitch - filter_width_vfoA / 2, cw_pitch + filter_width_vfoA / 2);
                            }
                            break;

                        case Mode.RTTY:
                            {
#if(DirectX)
                                DX.VFOA = vfoa;
#endif
                                Display_GDI.VFOA = vfoa;


                                SetRXOsc(0, 0, ((losc - vfoa) * 1e6 + rtty_pitch + rtty.trx.modem[0].shift / 2));
                                SetRXOsc(0, 1, ((losc - vfoa) * 1e6 + rtty_pitch - rtty.trx.modem[0].shift / 2));
                                SetRXFilter(0, 0, rtty_pitch - filter_width_vfoA / 2, rtty_pitch + filter_width_vfoA / 2);
                                SetRXFilter(0, 1, rtty_pitch - filter_width_vfoA / 2, rtty_pitch + filter_width_vfoA / 2);

                                rtty.trx.modem[0].rx_frequency = rtty_pitch;
                                rtty.trx.modem[0].tx_frequency = rtty.TXIfShift;

                                if (radRTTYNormal.Checked)
                                {
#if(DirectX)
                                    DX.VFOA_MARK = vfoa + (rtty.trx.modem[0].shift / 2) / 1e6;
                                    DX.VFOA_SPACE = vfoa - (rtty.trx.modem[0].shift / 2) / 1e6;
#endif

                                    Display_GDI.VFOA_MARK = vfoa + (rtty.trx.modem[0].shift / 2) / 1e6;
                                    Display_GDI.VFOA_SPACE = vfoa - (rtty.trx.modem[0].shift / 2) / 1e6;
                                }
                                else
                                {
#if(DirectX)
                                    DX.VFOA_MARK = vfoa - (rtty.trx.modem[0].shift / 2) / 1e6;
                                    DX.VFOA_SPACE = vfoa + (rtty.trx.modem[0].shift / 2) / 1e6;
#endif

                                    Display_GDI.VFOA_MARK = vfoa - (rtty.trx.modem[0].shift / 2) / 1e6;
                                    Display_GDI.VFOA_SPACE = vfoa + (rtty.trx.modem[0].shift / 2) / 1e6;
                                }
                            }
                            break;

                        case Mode.BPSK31:
                        case Mode.BPSK63:
                        case Mode.BPSK125:
                        case Mode.BPSK250:
                        case Mode.QPSK31:
                        case Mode.QPSK63:
                        case Mode.QPSK125:
                        case Mode.QPSK250:
                            {
                                if (chkAFC.Checked && tune_psk)
                                {
                                    if (vfoa > psk_vfoa_max)
                                        vfoa = psk_vfoa_max;
                                    else if (vfoa < psk_vfoa_min)
                                        vfoa = psk_vfoa_min;
                                }

#if(DirectX)
                                DX.VFOA = vfoa;
#endif

                                Display_GDI.VFOA = vfoa;
                                SetRXOsc(0, 0, ((losc - vfoa) * 1e6 + psk_pitch));
                                //int result = SetRXFilter(0, 0, psk_pitch - filter_width / 2, psk_pitch + filter_width / 2);

                                if (tune_psk || !chkAFC.Checked)
                                {
                                    if (!chkAFC.Checked)
                                    {
                                        psk_vfoa_max = vfoa + 0.000015;
                                        psk_vfoa_min = vfoa - 0.000015;
                                        psk.trx.modem[0].rx_frequency = psk_pitch;
                                        psk.trx.modem[0].tx_frequency = psk.TXIfShift;
                                    }
                                }
                                else
                                {
                                    psk_vfoa_max = vfoa + 0.000015;
                                    psk_vfoa_min = vfoa - 0.000015;
                                    psk.trx.modem[0].rx_frequency = psk_pitch;
                                    psk.trx.modem[0].tx_frequency = psk.TXIfShift;
                                }
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
        }

        private double vfob = 14.0;
        public double VFOB
        {
            get { return vfob; }
            set
            {
                try
                {
                    vfob = value;

                    if (vfoa > (losc * 1e6 + Audio.SampleRate / 2) / 1e6)
                        vfoa = (losc * 1e6 + Audio.SampleRate / 2) / 1e6;
                    else if (vfoa < (losc * 1e6 - Audio.SampleRate / 2) / 1e6)
                        vfoa = (losc * 1e6 - Audio.SampleRate / 2) / 1e6;

                    txtVFOB.Text = vfob.ToString("f6");

                    switch (op_mode_vfoB)
                    {
                        case Mode.CW:
                            {
                                switch (op_mode_vfoA)
                                {
                                    case Mode.CW:
                                        SetRXOsc(0, 1, (losc - vfob) * 1e6 + cw_pitch);
                                        SetRXFilter(0, 1, cw_pitch - filter_width_vfoB / 2, cw_pitch + filter_width_vfoB / 2);
                                        break;

                                    case Mode.BPSK31:
                                    case Mode.BPSK63:
                                    case Mode.BPSK125:
                                    case Mode.BPSK250:
                                    case Mode.QPSK31:
                                    case Mode.QPSK63:
                                    case Mode.QPSK125:
                                    case Mode.QPSK250:
                                        SetRXOsc(0, 1, (losc - vfob) * 1e6 + psk_pitch);
                                        SetRXFilter(0, 1, psk_pitch - filter_width_vfoB / 2, psk_pitch + filter_width_vfoB / 2);
                                        break;

                                    case Mode.RTTY:
                                        SetRXOsc(0, 2, (losc - vfob) * 1e6 + rtty_pitch);
                                        SetRXFilter(0, 2, rtty_pitch - filter_width_vfoB / 2, rtty_pitch + filter_width_vfoB / 2);
                                        break;
                                }

                                //SetRXOsc(0, 1, (losc - vfob) * 1e6 + cw_pitch);
                                //SetRXFilter(0, 1, cw_pitch - filter_width_vfoB / 2, cw_pitch + filter_width_vfoB / 2);
#if(DirectX)
                                DX.VFOB = vfob;
#endif
                                Display_GDI.VFOB = vfob;
                            }
                            break;

                        case Mode.RTTY:
                            {
#if(DirectX)
                                DX.VFOB = vfob;
#endif
                                Display_GDI.VFOB = vfob;

                                SetRXOsc(0, 2, (losc - vfob) * 1e6 + rtty_pitch + rtty.trx.modem[1].shift / 2);
                                SetRXOsc(0, 3, (losc - vfob) * 1e6 + rtty_pitch - rtty.trx.modem[1].shift / 2);
                                SetRXFilter(0, 2, rtty_pitch - filter_width_vfoB / 2, rtty_pitch + filter_width_vfoB / 2);
                                SetRXFilter(0, 3, rtty_pitch - filter_width_vfoB / 2, rtty_pitch + filter_width_vfoB / 2);

                                if (radRTTYNormal.Checked)
                                {
#if(DirectX)
                                    DX.VFOB_MARK = vfob + (rtty.trx.modem[1].shift / 2) / 1e6;
                                    DX.VFOB_SPACE = vfob - (rtty.trx.modem[1].shift / 2) / 1e6;
#endif

                                    Display_GDI.VFOB_MARK = vfob + (rtty.trx.modem[1].shift / 2) / 1e6;
                                    Display_GDI.VFOB_SPACE = vfob - (rtty.trx.modem[1].shift / 2) / 1e6;
                                }
                                else
                                {
#if(DirectX)
                                    DX.VFOB_MARK = vfob - (rtty.trx.modem[1].shift / 2) / 1e6;
                                    DX.VFOB_SPACE = vfob + (rtty.trx.modem[1].shift / 2) / 1e6;
#endif

                                    Display_GDI.VFOB_MARK = vfob - (rtty.trx.modem[1].shift / 2) / 1e6;
                                    Display_GDI.VFOB_SPACE = vfob + (rtty.trx.modem[1].shift / 2) / 1e6;
                                }
                            }
                            break;

                        case Mode.BPSK31:
                        case Mode.BPSK63:
                        case Mode.BPSK125:
                        case Mode.BPSK250:
                        case Mode.QPSK31:
                        case Mode.QPSK63:
                        case Mode.QPSK125:
                        case Mode.QPSK250:
                            {
                                vfob = value;

                                if (vfob > (losc * 1e6 + Audio.SampleRate / 2) / 1e6)
                                    vfob = (losc * 1e6 + Audio.SampleRate / 2) / 1e6;
                                else if (vfob < (losc * 1e6 - Audio.SampleRate / 2) / 1e6)
                                    vfob = (losc * 1e6 - Audio.SampleRate / 2) / 1e6;

                                txtVFOB.Text = vfob.ToString("f6");

                                if (chkAFC.Checked && tune_psk)
                                {
                                    if (vfob > psk_vfob_max)
                                        vfob = psk_vfob_max;
                                    else if (vfob < psk_vfob_min)
                                        vfob = psk_vfob_min;
                                }

                                switch (op_mode_vfoA)
                                {
                                    case Mode.CW:
                                        SetRXOsc(0, 1, (losc - vfob) * 1e6 + cw_pitch);
                                        SetRXFilter(0, 1, cw_pitch - filter_width_vfoB / 2, cw_pitch + filter_width_vfoB / 2);
                                        break;

                                    case Mode.BPSK31:
                                    case Mode.BPSK63:
                                    case Mode.BPSK125:
                                    case Mode.BPSK250:
                                    case Mode.QPSK31:
                                    case Mode.QPSK63:
                                    case Mode.QPSK125:
                                    case Mode.QPSK250:
                                        SetRXOsc(0, 1, (losc - vfob) * 1e6 + psk_pitch);
                                        SetRXFilter(0, 1, psk_pitch - filter_width_vfoB / 2, psk_pitch + filter_width_vfoB / 2);
                                        break;

                                    case Mode.RTTY:
                                        SetRXOsc(0, 2, (losc - vfob) * 1e6 + psk_pitch);
                                        SetRXFilter(0, 2, psk_pitch - filter_width_vfoB / 2, psk_pitch + filter_width_vfoB / 2);
                                        break;
                                }

                                //SetRXOsc(0, 1, (losc - vfob) * 1e6 + psk_pitch);
                                //SetRXFilter(0, 1, psk_pitch - filter_width / 2, psk_pitch + filter_width / 2);

                                if (tune_psk || !chkAFC.Checked)
                                {
                                    if (!chkAFC.Checked)
                                    {
                                        psk_vfob_max = vfob + 0.000015;
                                        psk_vfob_min = vfob - 0.000015;
                                        psk.trx.modem[1].rx_frequency = psk_pitch;
                                        psk.trx.modem[1].tx_frequency = psk.TXIfShift;
                                    }
                                }
                                else
                                {
                                    psk_vfob_max = vfob + 0.000015;
                                    psk_vfob_min = vfob - 0.000015;
                                    psk.trx.modem[1].rx_frequency = psk_pitch;
                                    psk.trx.modem[1].tx_frequency = psk.TXIfShift;
                                }

#if(DirectX)
                                DX.VFOB = vfob;
#endif
                                Display_GDI.VFOB = vfob;
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
        }

        private double losc = 14.0;
        public double LOSC
        {
            get { return losc; }
            set
            {
                losc = Math.Round(value, 6);
                txtLosc.Text = value.ToString("f6");

                if (losc > 1.0 && losc < 17.0)
                    genesis.si570_fxtal = (double)SetupForm.udSi570_1.Value;
                else if (losc > 17.0 && losc < 30.0)
                    genesis.si570_fxtal = (double)SetupForm.udSi570_2.Value;
                else if (losc > 30.0 && losc < 60.0)
                    genesis.si570_fxtal = (double)SetupForm.udSi570_3.Value;

                genesis.Set_frequency((long)(losc * 1e6), false);
                Thread.Sleep(1);
                Display_GDI.LOSC = losc;
#if(DirectX)
                DX.LOSC = losc;
                DX.RefreshPanadapterGrid = true;
#endif
                VFOA = vfoa;    // force refresh
                VFOB = vfob;

                if (!MOX)
                    WBIR_state = WBIR_State.DelayAdapt;

                Audio.iq_balancer_reset = true;
            }
        }

        private int cw_pitch = 700;
        public int CWPitch
        {
            get { return cw_pitch; }
            set
            {
                cw_pitch = value;

                if (!booting)
                {
                    VFOA = vfoa;
                    VFOB = vfob;
                }
            }
        }

        private int rtty_pitch = 700;
        public int RTTYPitch
        {
            get { return rtty_pitch; }
            set
            {
                rtty_pitch = value;

                if (!booting)
                {
                    VFOA = vfoa;
                    VFOB = vfob;
                }

            }
        }

        private int psk_pitch = 700;
        public int PSKPitch
        {
            get { return psk_pitch; }
            set
            {
                psk_pitch = value;

                if (!booting)
                {
                    VFOA = vfoa;
                    VFOB = vfob;
                }
            }
        }

        private int psk_preamble = 50;
        public int PSKPreamble
        {
            get { return psk_preamble; }
            set
            {
                psk_preamble = value;

                if (!booting)
                {
                    psk.TXPreamble = value;
                }
            }
        }

        #endregion

        private Mode op_mode_vfoA = Mode.CW;    // default
        public Mode OpModeVFOA
        {
            get { return op_mode_vfoA; }
            set
            {
                op_mode_vfoA = value;

                switch (value)
                {
                    case Mode.CW:
                    case Mode.RTTY:
                        FilterWidthVFOA = 50;
                        break;

                    case Mode.BPSK31:
                    case Mode.QPSK31:
                        FilterWidthVFOA = 31;
                        break;

                    case Mode.BPSK63:
                    case Mode.QPSK63:
                        FilterWidthVFOA = 63;
                        break;

                    case Mode.QPSK125:
                        FilterWidthVFOA = 150;
                        break;
                    case Mode.BPSK125:
                        FilterWidthVFOA = 125;
                        break;

                    case Mode.BPSK250:
                        FilterWidthVFOA = 300;
                        break;
                    case Mode.QPSK250:
                        FilterWidthVFOA = 250;
                        break;
                }
            }
        }

        private Mode op_mode_vfoB = Mode.CW;    // default
        public Mode OpModeVFOB
        {
            get { return op_mode_vfoB; }
            set
            {
                op_mode_vfoB = value;

                switch (value)
                {
                    case Mode.CW:
                    case Mode.RTTY:
                        FilterWidthVFOB = 50;
                        break;

                    case Mode.BPSK31:
                    case Mode.QPSK31:
                        FilterWidthVFOB = 31;
                        break;

                    case Mode.BPSK63:
                    case Mode.QPSK63:
                        FilterWidthVFOB = 63;
                        break;

                    case Mode.QPSK125:
                        FilterWidthVFOB = 150;
                        break;
                    case Mode.BPSK125:
                        FilterWidthVFOB = 125;
                        break;

                    case Mode.BPSK250:
                        FilterWidthVFOB = 300;
                        break;
                    case Mode.QPSK250:
                        FilterWidthVFOB = 250;
                        break;
                }
            }
        }

        private int msg_rpt_time = 5000;
        public int MsgRptTime
        {
            get { return msg_rpt_time; }
            set { msg_rpt_time = value; }
        }

        private DisplayMode display_mode = DisplayMode.PANAFALL;
        public DisplayMode DisplayMode
        {
            get { return display_mode; }
            set
            {
                display_mode = value;
                Display_GDI.DisplayMode = value;
                bool pwr = btnStartMR.Checked;

                if (btnStartMR.Checked)
                {
                    btnStartMR.Checked = false;
                }

                Thread.Sleep(100);

                if (display_mode == DisplayMode.PANADAPTER ||
                     display_mode == DisplayMode.PANAFALL ||
                     display_mode == DisplayMode.PANASCOPE ||
                     display_mode == DisplayMode.PANASCOPE_INV)
                    Display_GDI.Target = picPanadapter;
                else
                    Display_GDI.Target = picWaterfall;
#if(DirectX)
                DX.DisplayMode = value;
                if (video_driver == DisplayDriver.DIRECTX)
                {
                    DX.DirectXRelease();
                    Thread.Sleep(100);
                    DX.DirectXInit();
                }
#endif

                btnStartMR.Checked = pwr;
            }
        }

        private DisplayMode monitor_mode = DisplayMode.WATERFALL;
        public DisplayMode MonitorMode
        {
            get { return monitor_mode; }
            set
            { monitor_mode = value; }
        }

        private bool pwr = false;
        public bool PWR
        {
            get { return pwr; }
            set { btnStartMR.Checked = value; }
        }

        private DisplayDriver video_driver = DisplayDriver.GDI;
        public DisplayDriver VideoDriver
        {
            get { return video_driver; }
            set
            {
                if (!booting)
                {
                    bool mrRuning = btnStartMR.Checked;

                    if (mrRuning)
                        btnStartMR.Checked = false;

                    Thread.Sleep(100);

                    if (value != video_driver)
                    {
                        if (value == DisplayDriver.GDI)
                        {
                            SMeter.displayEngine = AnalogGAuge.AGauge.DisplayDriver.GDI;
#if(DirectX)
                            DX.DirectXRelease();
#endif
                            Thread.Sleep(100);
                            Display_GDI.Target = picPanadapter;
                            Display_GDI.MainForm = this;
                            Display_GDI.Init(this);

                            if (File.Exists(".\\picDisplay.png"))
                            {
                                picPanadapter.BackgroundImage = Image.FromFile(".\\picDisplay.png");
                                picWaterfall.BackgroundImage = Image.FromFile(".\\picDisplay.png");
                            }
                        }
                        else
                        {
#if(DirectX)
                            SMeter.displayEngine = AnalogGAuge.AGauge.DisplayDriver.DIRECTX;
                            Display_GDI.Close();
                            Thread.Sleep(100);
                            DX.MainForm = this;
                            DX.PanadapterTarget = picPanadapter;
                            DX.WaterfallTarget = picWaterfall;
                            DX.DirectXInit();
                            SMeter.GaugeTarget = SMeter;
                            SMeter.DirectX_Init(Application.StartupPath + "\\SMeter.jpg");
#endif
                        }
                    }

                    btnStartMR.Checked = mrRuning;
                }

                video_driver = value;
            }
        }

        private int refresh_time = 100;
        public int RefreshTime
        {
            set { refresh_time = value; }
        }

        private bool always_on_top = false; // yt7pwr
        public bool AlwaysOnTop
        {
            get { return always_on_top; }
            set
            {
                always_on_top = value;

                if (value)
                {
                    SetWindowPos(this.Handle.ToInt32(),
                        -1, this.Left, this.Top, this.Width, this.Height, 0);
                }
                else
                {
                    SetWindowPos(this.Handle.ToInt32(),
                        -2, this.Left, this.Top, this.Width, this.Height, 0);
                }
            }
        }

        public string txtCALL
        {
            set { this.txtCall.Text = value; }
        }

        public string txtNR
        {
            set { txtNr.Text = value; }
        }

        public string txtRst
        {
            set { txtRST.Text = value; }
        }


        private bool mrIsRunning = false;

        public bool MRIsRunning
        {
            get { return mrIsRunning; }
            set
            {
                if (!value)
                    btnStartMR.Checked = false;
                else
                    btnStartMR.Checked = true;

                Thread.Sleep(100);
                mrIsRunning = value;
            }
        }

        #endregion

        #region constructor/destructor

        public CWExpert()
        {
            booting = true;
            InitializeComponent();

            float dpi = this.CreateGraphics().DpiX;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            SizeF d = this.AutoScaleDimensions;
            d.Height = dpi;
            d.Width = dpi;
            this.AutoScaleDimensions = d;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = (float)(8.25 / ratio);
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;
            font_name = txtVFOA.Font.Name;
            size = 14.25f / ratio;
            new_font = new System.Drawing.Font(font_name, size);
            txtVFOA.Font = new_font;
            size = 9.75f / ratio;
            new_font = new System.Drawing.Font(font_name, size);
            txtVFOB.Font = new_font;
            txtLosc.Font = new_font;
            font_name = this.menuStrip1.Font.Name;
            this.menuStrip1.Font = new System.Drawing.Font(font_name, size);
            this.menuStrip1.Size = new Size(this.menuStrip1.Width, (int)(25 / ratio));
            this.PerformAutoScale();
            this.PerformLayout();

            SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            tx_image_phase_table = new float[(int)Band.LAST];
            tx_image_gain_table = new float[(int)Band.LAST];
            rx_image_phase_table = new float[(int)Band.LAST];
            rx_image_gain_table = new float[(int)Band.LAST];
            msg = new MessageHelper();
            edits = new TextEdit[3];
            DB.AppDataPath = Application.StartupPath;
            DB.Init();
            Audio.MainForm = this;
            PA19.PA_Initialize();
#if DirectX
            DX.MainForm = this;
#endif
            Display_GDI.MainForm = this;
            SetupForm = new Setup(this);
            booting = false;
            DXClusterForm = new DXClusterClient(SetupForm.txtTelnetHostAddress.Text.ToString(),
                SetupForm.txtStnCALL.Text.ToString(), SetupForm.txtStnName.Text.ToString(),
                SetupForm.txtStnQTH.Text.ToString());
            SetupForm.chkIPV6_CheckedChanged(this, EventArgs.Empty);
            display_event = new AutoResetEvent(false);
            output_ring_buf = new RingBuffer(32768);
            mon_ring_buf = new RingBuffer(32768);
            tbFilterWidth.Visible = false;
            picMonitor_bmp = new Bitmap(picMonitor.Width, picMonitor.Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

#if(DirectX)
            if (VideoDriver == DisplayDriver.DIRECTX)
            {
                DirectXInit();
            }
            else
#endif
            {
                SMeter.displayEngine = AnalogGAuge.AGauge.DisplayDriver.GDI;
                Display_GDI.Target = picPanadapter;
                Display_GDI.Init(this);

                if (File.Exists(".\\picDisplay.png"))
                {
                    picPanadapter.BackgroundImage = Image.FromFile(".\\picDisplay.png");
                    picWaterfall.BackgroundImage = Image.FromFile(".\\picDisplay.png");
                }
            }

            display_buffer = new float[4096];
            AlwaysOnTop = always_on_top;
            txtFilterWidth.Visible = false;
            tbFilterWidth.Visible = false;
            lblFilterwidth.Visible = false;
            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            tbAFGain_Scroll(this, EventArgs.Empty);
            genesis = new G59(this.Handle);
            btnCH1.BackColor = Color.LimeGreen;
            btnCH2.BackColor = Color.WhiteSmoke;

            if (Audio.SDRmode)
            {
                genesis.booting = false;
                G59Init();
                genesis.si570_i2c_address = 170;
                genesis.si570_fxtal = 114272200.0;
                genesis.HSDiv = 11;
                bool conn = genesis.Connected;

                if (conn)
                {
                    lblUSB.BackColor = Color.Green;
                }
                else
                {
                    lblUSB.BackColor = Color.Red;
                }
            }

            cwDecoder = new CWDecode(this);
            cwEncoder = new CWEncode(this);
            rtty = new RTTY(this);
            psk = new PSK(this);
            cwEncoder.CWSpedd = (int)SetupForm.udCWSpeed.Value;
            cwDecoder.rx_only = SetupForm.chkRXOnly.Checked;
            cwEncoder.TXPhase = (float)SetupForm.udTXPhase.Value;
            cwEncoder.TXGain = (float)SetupForm.udTXGain.Value;
            cwEncoder.TXIfShift = (float)SetupForm.udTXIfShift.Value;
            cwEncoder.TXOffDelay = (int)SetupForm.udTXOffDelay.Value;
            cwEncoder.SetKeyerSpeed((float)SetupForm.udCWSpeed.Value);
            cwEncoder.SetKeyerWeight((int)SetupForm.udCWWeight.Value);
            cwEncoder.SetKeyerIambic(SetupForm.chkG59Iambic.Checked);
            chkAFC_CheckedChanged(this, EventArgs.Empty);
            psk.TXPreamble = psk_preamble;

            if (SetupForm.chkG59IambicBmode.Checked)
                cwEncoder.SetKeyerMode(1);
            else
                cwEncoder.SetKeyerMode(0);

            rtty.TXPhase = (float)SetupForm.udTXPhase.Value;
            rtty.TXGain = (float)SetupForm.udTXGain.Value;

            if (!once)
            {
                SetupSDR(Application.StartupPath.ToString() + "\\wisdom");
                ReleaseUpdate();
                SetSampleRate(Audio.SampleRate);
                ResizeSDR(0, 4096);
                SetAGC(agc_mode);

                ProcessSampleThreadController[] pstc = new ProcessSampleThreadController[1];
                audio_process_thread = new Thread[1];

                for (uint proc_thread = 0; proc_thread < 1; proc_thread++)
                {
                    pstc[proc_thread] = new ProcessSampleThreadController(proc_thread);
                    audio_process_thread[proc_thread] = new Thread(new ThreadStart(pstc[proc_thread].ProcessSampleThread));
                    audio_process_thread[proc_thread].Name = "Audio Process Thread " + proc_thread.ToString();
                    audio_process_thread[proc_thread].Priority = ThreadPriority.Highest;
                    audio_process_thread[proc_thread].IsBackground = true;
                    audio_process_thread[proc_thread].Start();
                }

                once = true;
            }

            get_state = true;
            GetState();
            get_state = false;
            DB.LOGFilePath = log_file_path;
            DB.LOG_Init();
            InitLOG();
            keyboard = new Keyboard(this);
            recorder = new Recorder(this);

            double vfoa_freq, vfob_freq, losc_freq;
            int filter_vfoA, filter_vfoB, zoom, pan;
            DB.GetBandStack(current_band.ToString(), out vfoa_freq, out vfob_freq, out losc_freq, out filter_vfoA,
                out filter_vfoB, out zoom, out pan);
            LOSC = losc_freq;
            VFOA = vfoa_freq;
            VFOB = vfob_freq;
            FilterWidthVFOA = filter_vfoA;
            FilterWidthVFOB = filter_vfoB;
            tbZoom.Value = zoom;
            tbPan.Value = pan;
            lblPan_Click(null, null);

            switch (op_mode_vfoA)
            {
                case Mode.BPSK31:
                case Mode.BPSK63:
                case Mode.BPSK125:
                case Mode.BPSK250:
                case Mode.QPSK31:
                case Mode.QPSK63:
                case Mode.QPSK125:
                case Mode.QPSK250:
                    this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                    op_mode_vfoB.ToString() + "     ";
                    grpMonitor.Text = "Mode PSK";
                    break;
            }

            if (iq_correction == IQ_correction.FIXED)
            {
                SetRXIQGainPhase(1, rx_image_gain_table[(int)current_band],
                    rx_image_phase_table[(int)current_band]);
                RX_phase_gain();
            }
            else if (iq_correction == IQ_correction.WBIR)
            {
                SetRXIQGainPhase(2, 0.0f, 0.0f);
                RX_phase_gain();
            }

            dxcc = new DXCC();
            dxcc.Init(SetupForm.txtStnLOC.Text.ToString());

            try
            {
                if (use_telnet)
                    telnet_server = new TelnetServer(SetupForm.txtTelnetHostAddress.Text.ToString(),
                        (int)SetupForm.udTelnetServerPort.Value, SetupForm.chkIPV6.Checked);

                SetupForm.chkIPV6_CheckedChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        ~CWExpert()
        {

        }

        private void CWExpert_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (btnStartMR.Checked)
                    btnStartMR.Checked = false;

                if (telnet_server != null)
                    telnet_server.Close();

                cwEncoder.runKeyer = false;
                Thread.Sleep(100);

                if (SetupForm != null)
                    SetupForm.SaveOptions();

                if (log_book != null)
                    log_book.SaveState();

                if (DXClusterForm != null)
                    DXClusterForm.SaveOptions();

                if (keyboard != null)
                    keyboard.SaveOptions();

                if (dxcc != null)
                    dxcc.SaveOptions();

                if (recorder != null)
                    recorder.SaveOptions();

                SaveState();
                DB.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in CWExpert closing!\n" + ex.ToString());
            }
        }

        #endregion

        #region misc function

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (!booting)
                {
                    // The OnDeviceChange routine processes WM_DEVICECHANGE messages.
                    if (m.WParam.ToInt32() == 0x0007 && !genesis.Connected) // device arrival
                    {
                        genesis.OnDeviceChange(m);
                        Thread.Sleep(300);
                        G59Init();

                        if (genesis.Connected)
                        {
                            lblUSB.BackColor = Color.Green;
                        }
                        else
                            lblUSB.BackColor = Color.Red;
                    }
                    else if (m.WParam.ToInt32() == 0x8004)  // removal
                    {
                        if (!genesis.OnDeviceChange(m))
                            lblUSB.BackColor = Color.Red;
                        else
                            lblUSB.BackColor = Color.Green;
                    }
                }

                // Let the base form process the message.
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Debug.Write("WndProc error!\n\n" + ex.StackTrace.ToString());
            }
        }

        private bool EnsureMRWindow()
        {
            try
            {
                topWindow = msg.getWindowId("TMainForm", "Morse Runner");
                if (topWindow != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Morse Runner not found!\n" + ex.ToString());
                return false;
            }
        }

        private bool EnsureMREditWindows()
        {
            try
            {
                int childAfter = 0;
                int subPanel = 0;
                int tmpEdit = 0;
                WINDOWINFO wInfo = new WINDOWINFO();
                TextEdit[] tmpEdits = new TextEdit[3];

                if (EnsureMRWindow())
                {
                    while (true)
                    {
                        editPanel = msg.getWindowIdEx(topWindow, childAfter, "TPanel", null);
                        if (editPanel == 0)
                            break;
                        childAfter = editPanel;
                        subPanel = msg.getWindowIdEx(editPanel, 0, "TPanel", "0");
                        if (subPanel != 0)
                            break;
                    }
                    int i = 0;
                    while (true)
                    {
                        tmpEdit = msg.getWindowIdEx(editPanel, tmpEdit, "TEdit", null);
                        if (tmpEdit == 0)
                            break;
                        else
                        {
                            wInfo = msg.getWindowInfo(tmpEdit);
                            {
                                tmpEdits[i].hWnd = tmpEdit;
                                tmpEdits[i].Xpos = wInfo.rcClient.Left;
                                i++;
                            }
                        }
                    }

                    if (tmpEdits[0].Xpos < tmpEdits[1].Xpos)
                    {
                        edits[0] = tmpEdits[0];
                        edits[1] = tmpEdits[1];
                        edits[2] = tmpEdits[2];
                    }
                    else
                    {
                        edits[0] = tmpEdits[2];
                        edits[1] = tmpEdits[1];
                        edits[2] = tmpEdits[0];
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Morse Runner not running?\n" + ex.ToString());
                return false;
            }
        }

        #endregion

        #region MR Functions keys

        public void btnF1_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F1, (1 + (59 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F1, (1 + (59 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F1, (1 + (59 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F1, (1 + (59 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF2_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F2, (1 + (60 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F2, (1 + (60 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F2, (1 + (60 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F2, (1 + (60 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF3_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F3, (1 + (61 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F3, (1 + (61 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F3, (1 + (61 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F3, (1 + (61 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF4_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F4, (1 + (62 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F4, (1 + (62 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F4, (1 + (62 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F4, (1 + (62 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF5_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F5, (1 + (63 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F5, (1 + (63 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F5, (1 + (63 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F5, (1 + (63 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnF6_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F6, (1 + (64 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F6, (1 + (64 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F6, (1 + (64 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F6, (1 + (64 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnF7_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F7, (1 + (65 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F7, (1 + (65 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F7, (1 + (65 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F7, (1 + (65 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnF8_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F8, (1 + (66 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F8, (1 + (66 << 16)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F8, (1 + (66 << 16) + (3 << 30)));
                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F8, (1 + (66 << 16) + (3 << 30)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region MorseRunner keys

        private void btnStartMR_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnStartMR.Checked)
                {
                    btnStartMR.BackColor = Color.LimeGreen;
                    txtChannelClear(0); // all

                    if (Audio.SDRmode)
                    {
                        if (ROBOT)
                        {
                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                            rtbCH1.SelectionColor = Color.LawnGreen;
                            detect_call = false;
                            detect_loc = false;
                            detect_name = false;
                            detect_qth = false;
                            detect_rst = false;
                            detect_info = false;
                            detection = false;
                        }

                        G59Init();
                        CurrentBand = current_band;
                        Audio.callback_return = 0;
                        tx_pwr = tbG59PWR.Value;
                        OutputPowerUpdate();

                        if (cwDecoder == null)
                            cwDecoder = new CWDecode(this);

                        if (cwDecoder.AudioEvent1 == null)
                            cwDecoder.AudioEvent1 = new AutoResetEvent(false);

                        SetRXOn(0, 2, false);
                        SetRXOn(0, 3, false);
                        cwDecoder.CWdecodeStart();

                        if (rtty == null)
                            rtty = new RTTY(this);

                        if (rtty.AudioEventRX1 == null)
                            rtty.AudioEventRX1 = new AutoResetEvent(false);

                        rtty.RTTYStart();

                        if (op_mode_vfoA == Mode.RTTY)
                            SetRXOn(0, 1, true);

                        if (psk == null)
                            psk = new PSK(this);

                        if (psk.AudioEvent1 == null)
                            psk.AudioEvent1 = new AutoResetEvent(false);

                        psk.PSKStart();
                        VFOA = vfoa;
                        VFOB = vfob;
                        tbFilterWidth_Scroll(this, EventArgs.Empty);
                        tbSQL_Scroll(this, EventArgs.Empty);
                        lblPan_Click(this, EventArgs.Empty);
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);

                        if (!btnMute.Checked)
                            Audio.Volume = tbAFGain.Value;

                        Audio.ScopeLevel = tbAFGain.Value * 5;
                        SetTRX(0, false);   // RX

                        if (!Audio.Start())
                        {
                            btnStartMR.Checked = false;
                            return;
                        }

                        SetAGC(agc_mode);
                        tbRFGain_Scroll(this, EventArgs.Empty);
                        NBvals = nb_vals;
                        SetNB(0, 0, btnNB.Checked);
                        SetNB(0, 1, btnNB.Checked);
                        VFOA = vfoa;    // refresh
                        VFOB = vfob;
                        btnStartMR.Text = "Stop";

                        if (Audio.SDRmode)
                        {
                            txtFilterWidth.Visible = true;
                            lblFilterwidth.Visible = true;
                            txtFilterWidth.Text = tbFilterWidth.Value.ToString() + "Hz";
                        }
                        else
                        {
                            txtFilterWidth.Visible = false;
                            tbFilterWidth.Visible = false;
                            lblFilterwidth.Visible = false;
                        }

                        runDisplay = true;
                        display_thread = new Thread(new ThreadStart(RunDisplay));
                        display_thread.Name = "Display Thread";
                        display_thread.Priority = ThreadPriority.Normal;
                        display_thread.IsBackground = true;
                        display_thread.Start();

                        if (Audio.SDRmode)
                        {
                            Smeter_thread = new Thread(new ThreadStart(Smeter_thread_function));
                            Smeter_thread.Name = "Smeter Thread";
                            Smeter_thread.Priority = ThreadPriority.Normal;
                            Smeter_thread.IsBackground = true;
                            Smeter_thread.Start();

                            /*wbir_run = true;

                            if (wbir_thread == null || !wbir_thread.IsAlive)
                            {
                                wbir_thread = new Thread(new ThreadStart(WBIR_thread));
                                wbir_thread.Name = "WBIR Thread";
                                wbir_thread.Priority = ThreadPriority.Normal;
                                wbir_thread.IsBackground = true;
                            }

                            WBIR_state = WBIR_State.DelayAdapt;
                            wbir_tuned = true;
                            wbir_thread.Start();*/
                        }
                    }
                    else
                    {
                        if (!mrIsRunning)
                        {
                            mrIsRunning = true;

                            if (!btnMute.Checked)
                                Audio.Volume = tbVolume.Value * 10;

                            Audio.ScopeLevel = tbVolume.Value;
                            Audio.InputLevel = (double)tbInputLevel.Value; ;
                            Audio.callback_return = 0;

                            if (cwDecoder == null)
                                cwDecoder = new CWDecode(this);

                            if (cwDecoder.AudioEvent1 == null)
                                cwDecoder.AudioEvent1 = new AutoResetEvent(false);

                            cwDecoder.SQL = (double)(tbMRSquelch.Value / 20000.0);

                            EnsureMRWindow();

                            if (topWindow == 0)
                            {
                                btnStartMR.Checked = false;
                                return;
                            }

                            cwDecoder.CWdecodeStart();

                            Audio.Start();

                            int runButton = 0;
                            int panel = 0;
                            int subPanel = 0;

                            while (true)
                            {
                                panel = msg.getWindowIdEx(topWindow, panel, "TPanel", null);
                                if (panel == 0)
                                    break;
                                // Test if it has panel with subpanel with empty caption
                                subPanel = msg.getWindowIdEx(panel, 0, "TPanel", "");
                                if (subPanel == 0)
                                    break;

                                // Test if that sub-panel has toolbar
                                runButton = msg.getWindowIdEx(subPanel, 0, "TToolBar", null);
                                if (runButton == 0)
                                    break;
                            }

                            if (runButton == 0)
                            {
                                btnStartMR.Checked = false;
                                return;
                            }
                            else
                            {
                                msg.sendWindowsMessage(runButton, WM_LBUTTONDOWN, 0, (10 << 16) + 10);
                                msg.sendWindowsMessage(runButton, WM_LBUTTONUP, 0, (10 << 16) + 10);
                                udLOGMyNR.Value = 1;
                                mrIsRunning = true;
                                btnStartMR.Text = "Stop";
                                runDisplay = true;
                                display_thread = new Thread(new ThreadStart(RunDisplay));
                                display_thread.Name = "Display Thread";
                                display_thread.Priority = ThreadPriority.Normal;
                                display_thread.IsBackground = true;
                                display_thread.Start();
                            }
                        }
                    }
                }
                else
                {
                    btnStartMR.BackColor = Color.WhiteSmoke;

                    if (Audio.SDRmode)
                    {
                        MOX = false;
                        TUNE = false;
                        wbir_run = false;
                        Thread.Sleep(100);
                        runDisplay = false;

                        switch (op_mode_vfoA)
                        {
                            case Mode.CW:
                                cwDecoder.CWdecodeStop();
                                break;

                            case Mode.RTTY:
                                rtty.RTTYStop();
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                psk.PSKStop();
                                break;
                        }

                        Thread.Sleep(100);
                        Audio.callback_return = 2;
                        Thread.Sleep(100);
                        Audio.StopAudio();
                        Thread.Sleep(100);
                        btnStartMR.Text = "Start";
                    }
                    else
                    {
                        if (mrIsRunning)
                        {
                            runDisplay = false;
                            mrIsRunning = false;
                            Audio.callback_return = 2;
                            Thread.Sleep(100);
                            Audio.StopAudio();
                            Thread.Sleep(100);
                            cwDecoder.CWdecodeStop();
                            Thread.Sleep(100);
                            cwDecoder.CWDecodeClose();
                            cwDecoder = null;

                            EnsureMRWindow();

                            if (topWindow == 0)
                                return;

                            int runButton = 0;
                            int panel = 0;
                            int subPanel = 0;

                            while (true)
                            {
                                panel = msg.getWindowIdEx(topWindow, panel, "TPanel", null);
                                if (panel == 0)
                                    break;
                                // Test if it has panel with subpanel with empty caption
                                subPanel = msg.getWindowIdEx(panel, 0, "TPanel", "");
                                if (subPanel == 0)
                                    break;

                                // Test if that sub-panel has toolbar
                                runButton = msg.getWindowIdEx(subPanel, 0, "TToolBar", null);
                                if (runButton == 0)
                                    break;
                            }

                            if (runButton == 0)
                                return;
                            else
                            {
                                msg.sendWindowsMessage(runButton, WM_LBUTTONDOWN, 0, (10 << 16) + 10);
                                msg.sendWindowsMessage(runButton, WM_LBUTTONUP, 0, (10 << 16) + 10);
                                mrIsRunning = false;
                                btnStartMR.Text = "Start";
                            }
                        }
                    }
                }

                if (btnStartMR.Checked)
                    pwr = true;
                else
                    pwr = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Morse Runner not running?\n" + ex.ToString());
                btnStartMR.Text = "Start";
            }
        }

        public void btnSendCall_Click(object sender, EventArgs e)      // button call
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsStringMessage(edits[0].hWnd, 0, txtCall.Text);
                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_RETURN, 1 + (13 << 16));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnSendRST_Click(object sender, EventArgs e)       // button RST
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsStringMessage(edits[1].hWnd, 0, txtRST.Text);
                    //  msg.sendWindowsMessage(edits[1].hWnd, WM_KEYDOWN, VK_RETURN, 1 + (13 << 16));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void btnSendNr_Click(object sender, EventArgs e)        // button Nr
        {
            try
            {
                EnsureMREditWindows();

                if (topWindow != 0)
                {
                    msg.sendWindowsStringMessage(edits[2].hWnd, 0, txtNr.Text);
                    msg.sendWindowsMessage(edits[2].hWnd, WM_KEYDOWN, VK_RETURN, 1 + (13 << 16));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtNr_KeyUp(object sender, KeyEventArgs e)     // Nr + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnSendNr_Click(null, null);
        }

        private void txtRST_KeyUp(object sender, KeyEventArgs e)    // RST + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnSendRST_Click(null, null);
        }

        private void txtCall_KeyUp(object sender, KeyEventArgs e)   // Call + enter
        {
            if (e.KeyCode == Keys.Enter)
                btnSendCall_Click(null, null);
        }

        public void CWExpert_KeyUp(object sender, KeyEventArgs e)   // function keys F1...F12
        {

            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnF1_Click(null, null);
                    break;
                case Keys.F2:
                    btnF2_Click(null, null);
                    break;
                case Keys.F3:
                    btnF3_Click(null, null);
                    break;
                case Keys.F4:
                    btnF4_Click(null, null);
                    break;
                case Keys.F5:
                    btnF5_Click(null, null);
                    break;
                case Keys.F6:
                    btnF6_Click(null, null);
                    break;
                case Keys.F7:
                    btnF7_Click(null, null);
                    break;
                case Keys.F8:
                    btnF8_Click(null, null);
                    break;
            }

            freq_mult = false;
        }

        private void btngrab_Click(object sender, EventArgs e)
        {
            //txtCALL = Callers.SelectedItem.ToString();
        }

        public void txtChannelClear(int channel)
        {
            try
            {
                switch (channel)
                {
                    case 2:
                        txtChannel2.Clear();
                        break;
                    case 3:
                        txtChannel3.Clear();
                        break;
                    case 4:
                        txtChannel4.Clear();
                        break;
                    case 5:
                        txtChannel5.Clear();
                        break;
                    case 6:
                        txtChannel6.Clear();
                        break;
                    case 7:
                        txtChannel7.Clear();
                        break;
                    case 8:
                        txtChannel8.Clear();
                        break;
                    case 9:
                        txtChannel9.Clear();
                        break;
                    case 10:
                        txtChannel10.Clear();
                        break;
                    case 11:
                        txtChannel11.Clear();
                        break;
                    case 12:
                        txtChannel12.Clear();
                        break;
                    case 13:
                        txtChannel13.Clear();
                        break;
                    case 14:
                        txtChannel14.Clear();
                        break;
                    case 15:
                        txtChannel15.Clear();
                        break;
                    case 16:
                        txtChannel16.Clear();
                        break;
                    case 17:
                        txtChannel17.Clear();
                        break;
                    case 18:
                        txtChannel18.Clear();
                        break;
                    case 19:
                        txtChannel19.Clear();
                        break;
                    case 20:
                        txtChannel20.Clear();
                        break;
                    case 0:
                        {
                            txtChannel2.Clear();
                            txtChannel3.Clear();
                            txtChannel4.Clear();
                            txtChannel5.Clear();
                            txtChannel6.Clear();
                            txtChannel7.Clear();
                            txtChannel8.Clear();
                            txtChannel9.Clear();
                            txtChannel10.Clear();
                            txtChannel11.Clear();
                            txtChannel12.Clear();
                            txtChannel13.Clear();
                            txtChannel14.Clear();
                            txtChannel15.Clear();
                            txtChannel16.Clear();
                            txtChannel17.Clear();
                            txtChannel18.Clear();
                            txtChannel19.Clear();
                            txtChannel20.Clear();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnclr_Click(object sender, EventArgs e)
        {
            try
            {
                txtChannelClear(0); // all
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbMRSquelch_Scroll(object sender, EventArgs e)
        {
            if (cwDecoder != null)
                cwDecoder.SQL = (double)(tbMRSquelch.Value / 20000.0);
        }

        #endregion

        #region Display functions

        private void picWaterfall_Paint(object sender, PaintEventArgs e)
        {
            if (video_driver == DisplayDriver.GDI)
            {
                if (display_mode == DisplayMode.PANAFALL_INV)
                {
                    if (!Display_GDI.RenderPanadapter(ref e))
                    {
                        if (Display_GDI.IsInitialized)
                        {
                            Display_GDI.Close();
                            Thread.Sleep(100);
                            if (!Display_GDI.Init(this))
                                btnStartMR.Checked = false;
                        }
                    }
                }
                else
                {
                    if (!Display_GDI.RenderWaterfall(ref e))
                    {
                        if (Display_GDI.IsInitialized)
                        {
                            Display_GDI.Close();
                            Thread.Sleep(100);
                            if (!Display_GDI.Init(this))
                                btnStartMR.Checked = false;
                        }
                    }
                }
            }
        }

        private void picPanadapter_Paint(object sender, PaintEventArgs e)
        {
            if (display_mode == DisplayMode.PANADAPTER || display_mode == DisplayMode.PANAFALL ||
                display_mode == DisplayMode.PANASCOPE || display_mode == DisplayMode.PANASCOPE_INV)
            {
#if(DirectX)
                if (VideoDriver == DisplayDriver.DIRECTX && !runDisplay)
                    DX.RenderDirectX();  //RenderWaterfall(e.Graphics, picWaterfall.Width, picWaterfall.Height);
                else
#endif
                    if (video_driver == DisplayDriver.GDI)
                    {
                        if (!Display_GDI.RenderPanadapter(ref e))
                        {
                            if (Display_GDI.IsInitialized)
                            {
                                Display_GDI.Close();
                                Thread.Sleep(100);
                                if (!Display_GDI.Init(this))
                                    btnStartMR.Checked = false;
                            }
                        }
                    }
            }
            else if (display_mode == DisplayMode.PANAFALL_INV)
            {
                if (video_driver == DisplayDriver.GDI)
                {
                    if (!Display_GDI.RenderWaterfall(ref e))
                    {
                        if (Display_GDI.IsInitialized)
                        {
                            Display_GDI.Close();
                            Thread.Sleep(100);
                            if (!Display_GDI.Init(this))
                                btnStartMR.Checked = false;
                        }
                    }
                }
            }
        }

        private Fourier fft;
        public bool data_ready = false;
        unsafe private void RunDisplay()
        {
            try
            {
                float[] display_data = new float[4096];
                Thread.Sleep(100);
                fft = new Fourier();

                while (runDisplay)
                {
                    Thread.Sleep(refresh_time);

                    if (this.WindowState != FormWindowState.Minimized)
                    {
#if(DirectX)
                        if (VideoDriver == DisplayDriver.DIRECTX)
                        {
                            fixed (float* ptr = &DX.new_display_data[0])
                                GetPanadapter(0, ptr);
                            Array.Copy(DX.new_display_data, picMonitor_buffer, 4096);
                            Array.Copy(DX.new_display_data, DX.new_waterfall_data, 4096);

                            DX.DataReady = true;

                            if (!DX.RenderDirectX())
                            {
                                this.Invoke(new CrossThreadCallback(CrossThreadCommand), "Reinit DirectX", "");

                                Thread.Sleep(100);
                            }

                            if (display_mode == DisplayMode.PANAFALL ||
                                display_mode == DisplayMode.PANAFALL_INV)
                            {
                                DX.WaterfallDataReady = true;
                            }

                            if (grpLogBook.Visible)
                                picMonitorRender();
                        }
                        else
#endif
                        {
                            if (Display_GDI.IsInitialized)
                            {
                                fixed (float* ptr = &Display_GDI.new_display_data[0])
                                    GetPanadapter(0, ptr);
                                Array.Copy(Display_GDI.new_display_data, picMonitor_buffer, 4096);
                                Array.Copy(Display_GDI.new_display_data, Display_GDI.new_waterfall_data, 4096);

                                if (grpLogBook.Visible && !mox)
                                    picMonitor.Invalidate();

                                Display_GDI.DataReady = true;
                                picPanadapter.Invalidate();
                                picWaterfall.Invalidate();
                                Display_GDI.WaterfallDataReady = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void picPanadapter_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                double freq = 0.0;
                picPanadapter.Focus();
                Display_GDI.DisplayCursorX = e.X;
#if(DirectX)
                DX.DisplayCursorX = e.X;
                DX.DisplayCursorY = e.Y;
                DX.waterfall_target_focused = false;
                DX.panadapter_target_focused = true;
#endif
                Display_GDI.DisplayCursorY = e.Y;
                float x = PixelToHz(e.X);

                if (Audio.SDRmode)
                {
                    freq = x / 1e6;
                    txtFreq.Text = freq.ToString("f6") + "Hz";

                    if ((op_mode_vfoA == Mode.RTTY || op_mode_vfoB == Mode.RTTY) && rtty != null)
                    {
#if(DirectX)
                        DX.RTTYCursorX1 = (int)HzToPixel((float)((freq - losc - (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                        DX.RTTYCursorX2 = (int)HzToPixel((float)((freq - losc + (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
#endif
                        Display_GDI.RTTYCursorX1 = (int)HzToPixel((float)((freq - losc - (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                        Display_GDI.RTTYCursorX2 = (int)HzToPixel((float)((freq - losc + (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                    }
                }
                else
                    txtFreq.Text = Math.Round(x, 0).ToString() + "Hz";
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private float PixelToHz(float x)
        {
            double low, high;

            if (Audio.SDRmode)
            {
#if(DirectX)
                if (video_driver == DisplayDriver.DIRECTX)
                {
                    low = losc * 1e6 + DX.RXDisplayLow;
                    high = losc * 1e6 + DX.RXDisplayHigh;
                }
                else
#endif
                {
                    low = losc * 1e6 + Display_GDI.RXDisplayLow;
                    high = losc * 1e6 + Display_GDI.RXDisplayHigh;
                }

                double width = high - low;
                return (float)(low + ((double)x / (double)picPanadapter.Width * (double)width));
            }
            else
            {
#if(DirectX)
                if (video_driver == DisplayDriver.DIRECTX)
                {
                    low = DX.RXDisplayLow;
                    high = DX.RXDisplayHigh;
                }
                else
#endif
                {
                    low = Display_GDI.RXDisplayLow;
                    high = Display_GDI.RXDisplayHigh;
                }

                double width = high - low;
                return (float)(low + (double)x / (double)picPanadapter.Width * (double)width);
            }
        }

        private double HzToPixel(float freq)
        {
            double low = 0, high = 0;

#if(DirectX)
            if (video_driver == DisplayDriver.DIRECTX)
            {
                low = DX.RXDisplayLow;
                high = DX.RXDisplayHigh;
            }
            else
#endif
                if (video_driver == DisplayDriver.GDI)
                {
                    low = Display_GDI.RXDisplayLow;
                    high = Display_GDI.RXDisplayHigh;
                }

            double width = high - low;
            return (freq - low) / width * picPanadapter.Width;
        }

        private void picPanadapter_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
        }

        private void picPanadapter_MouseEnter(object sender, EventArgs e)
        {
            picPanadapter.Focus();
        }

        private void picPanadapter_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                picPanadapter.Focus();

#if(DirectX)
                if (VideoDriver == DisplayDriver.DIRECTX)
                {
                    if (e.Button == MouseButtons.Left && (tuning_mode == TuneMode.Off ||
                        tuning_mode == TuneMode.VFOA))
                    {
                        float x = PixelToHz(e.X);
                        VFOA = x / 1e6;
                        tbPan.Value = 0;
                    }
                    else if (e.Button == MouseButtons.Left && tuning_mode == TuneMode.VFOB)
                    {
                        float x = PixelToHz(e.X);
                        VFOB = x / 1e6;
                        tbPan.Value = 0;
                    }
                    else if (e.Button == MouseButtons.Right && tuning_mode == TuneMode.Off)
                    {
                        tuning_mode = TuneMode.VFOA;
                        DX.tuning_mode = TuneMode.VFOA;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                    else if (e.Button == MouseButtons.Right && tuning_mode == TuneMode.VFOA && DX.RX2Enabled)
                    {
                        tuning_mode = TuneMode.VFOB;
                        DX.tuning_mode = TuneMode.VFOB;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                    else if (e.Button == MouseButtons.Right && tuning_mode == TuneMode.VFOA && !DX.RX2Enabled)
                    {
                        tuning_mode = TuneMode.Off;
                        DX.tuning_mode = TuneMode.Off;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                    else if (e.Button == MouseButtons.Right && tuning_mode == TuneMode.VFOB)
                    {
                        tuning_mode = TuneMode.Off;
                        DX.tuning_mode = TuneMode.Off;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                }
                else
#endif
                {
                    if (e.Button == MouseButtons.Left && (tuning_mode == TuneMode.Off ||
                        tuning_mode == TuneMode.VFOA))
                    {
                        float x = PixelToHz(e.X);
                        VFOA = x / 1e6;
                        tbPan.Value = 0;
                    }
                    else if (e.Button == MouseButtons.Left && tuning_mode == TuneMode.VFOB)
                    {
                        float x = PixelToHz(e.X);
                        VFOB = x / 1e6;
                        tbPan.Value = 0;
                    }
                    else if (e.Button == MouseButtons.Right && Display_GDI.tuning_mode == TuneMode.Off)
                    {
                        tuning_mode = TuneMode.VFOA;
                        Display_GDI.tuning_mode = TuneMode.VFOA;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                    else if (e.Button == MouseButtons.Right && Display_GDI.tuning_mode == TuneMode.VFOA &&
                        Display_GDI.RX2Enabled)
                    {
                        tuning_mode = TuneMode.VFOB;
                        Display_GDI.tuning_mode = TuneMode.VFOB;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                    else if (e.Button == MouseButtons.Right && Display_GDI.tuning_mode == TuneMode.VFOA &&
                        !Display_GDI.RX2Enabled)
                    {
                        tuning_mode = TuneMode.Off;
                        Display_GDI.tuning_mode = TuneMode.Off;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                    else if (e.Button == MouseButtons.Right && Display_GDI.tuning_mode == TuneMode.VFOB)
                    {
                        tuning_mode = TuneMode.Off;
                        Display_GDI.tuning_mode = TuneMode.Off;
                        btnRX2On_CheckedChanged(this, EventArgs.Empty);
                        UpdateFilters();
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    Mode new_mode = op_mode_vfoA;
                    TuneMode new_tune_mode = tuning_mode;

                    if (tuning_mode == TuneMode.VFOB)
                        new_mode = op_mode_vfoB;

                    switch (new_mode)
                    {
                        case Mode.CW:
                            cWToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.RTTY:
                            rTTYToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.BPSK31:
                            bPSK31ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.BPSK63:
                            bPSK63ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.BPSK125:
                            bPSK125ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.BPSK250:
                            bPSK250ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.QPSK31:
                            qPSK31ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.QPSK63:
                            qPSK63ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.QPSK125:
                            qPSK125ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;

                        case Mode.QPSK250:
                            qPSK250ToolStripMenuItem_Click(this, EventArgs.Empty);
                            break;
                    }

                    tuning_mode = new_tune_mode;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Text box manipulation

        private string ch1_tmp_string = "";
        private string ch2_tmp_string = "";
        private bool start_call = false;
        private bool detect_call = false;
        private bool detect_name = false;
        private bool detect_qth = false;
        private bool detect_loc = false;
        private bool detect_rst = false;
        private bool detect_info = false;
        private bool detection = false;
        private string text = "";

        public void WriteOutputText(int message, string out_string)
        {
            try
            {
                if (Audio.SDRmode)
                {
                    switch (message)
                    {
                        case 5:                                     // rtb1 CH1
                            if (ROBOT && !mox)
                            {
                                rtbCH1.AppendText(out_string);

                                out_string = out_string.Replace("\0", "");

                                if (rtbCH1_scroll_down)
                                    SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 1);

                                if (text != " " && (out_string == " " || out_string == "\r" || out_string == "\n"))
                                {
                                    if (detection)
                                    {
                                        if (detect_name)
                                        {
                                            text = text.Replace(".", "");
                                            text = text.Replace("-", "");
                                            text = text.Replace(":", "");
                                            text = text.Replace(" ", "");
                                            txtLogName.AppendText(text);
                                        }
                                        else if (detect_qth)
                                        {
                                            text = text.Replace(".", "");
                                            text = text.Replace("-", "");
                                            text = text.Replace(" ", "");
                                            text = text.Replace(":", "");
                                            txtLogQTH.AppendText(text);
                                        }
                                        else if (detect_rst)
                                        {
                                            text = text.Replace(".", "");
                                            text = text.Replace("-", "");
                                            text = text.Replace(" ", "");
                                            text = text.Replace(":", "");

                                            if (IsNumber(text))
                                                txtLogRST.AppendText(text);
                                            else
                                                txtLogRST.Clear();
                                        }
                                        else if (detect_call)
                                        {
                                            if (text.StartsWith("CQ") || text.Contains("DX") ||
                                                text.Contains("TEST"))
                                                txtLogCall.Clear();
                                            else if (text.Length < 3 || !text.Contains("pse"))
                                            {
                                                txtLogCall.Clear();
                                                txtLogCall.AppendText(text.ToUpper());
                                                rtbCH1.Select(rtbCH1.Text.Length, 0);
                                                rtbCH1.SelectionColor = Color.Red;
                                            }
                                        }
                                        else if (detect_loc)
                                        {
                                            text = text.Replace(".", "");
                                            text = text.Replace("-", "");
                                            text = text.Replace(" ", "");
                                            text = text.Replace(":", "");

                                            if (IsLocator(text.ToUpper()))
                                                txtLOGLOC.AppendText(text.ToUpper());
                                            else
                                                txtLOGLOC.Clear();
                                        }
                                        else if (detect_info)
                                        {
                                            txtLogInfo.AppendText(text.ToUpper());
                                        }
                                    }

                                    if ((out_string == " " || out_string == "\r" || out_string == "\n") && detection)
                                    {
                                        if (detect_name && (txtLogName.Text.Trim() == "is" || txtLogName.Text.Trim() == "IS" ||
                                            txtLogName.Text.Trim() == "="))
                                        {
                                            txtLogName.Clear();
                                        }
                                        else if (detect_call && (txtLogCall.Text.Trim() == "DE" ||
                                            txtLogCall.Text == "" || txtLogCall.Text.StartsWith("DE") ||
                                            txtLogCall.Text == "CQ" || txtLogCall.Text.Length < 3))      // not finished
                                        {
                                            txtLogCall.Clear();
                                        }
                                        else if (detect_name && txtLogName.Text == "")      // not finished
                                        {
                                            txtLogName.Clear();
                                        }
                                        else if (detect_qth && (txtLogQTH.Text.Trim() == "is" || txtLogQTH.Text.Trim() == "IS" ||
                                            txtLogQTH.Text.Trim() == "="))
                                        {
                                            txtLogQTH.Clear();
                                        }
                                        else if (detect_qth && (txtLogQTH.Text.Trim() == "nr" || txtLogQTH.Text.Trim() == "NR" ||
                                            txtLogQTH.Text.Trim() == "near" || txtLogQTH.Text.Trim() == "Near") || txtLogQTH.Text.Trim() == "NEAR")
                                        {
                                            txtLogQTH.AppendText(" ");
                                        }
                                        else if (detect_qth && (txtLogQTH.Text == "" || txtLogQTH.Text == " "))
                                        {
                                            txtLogQTH.Clear();
                                        }
                                        else if (detect_loc && (txtLOGLOC.Text.Trim() == "is" || txtLOGLOC.Text.Trim() == "IS" ||
                                            txtLOGLOC.Text.Trim() == "="))
                                        {
                                            txtLOGLOC.Clear();
                                        }
                                        else if (detect_loc)
                                        {
                                            if (!IsLocator(txtLOGLOC.Text.Trim()))
                                            {
                                                txtLOGLOC.Clear();
                                                detect_loc = false;
                                                detection = false;
                                                rtbCH1.Select(rtbCH1.Text.Length, 0);
                                                rtbCH1.SelectionColor = Color.LawnGreen;
                                                Debug.Write("Detection ended!" + out_string + "\n");
                                            }
                                            else
                                            {
                                                detect_loc = false;
                                                detection = false;
                                                rtbCH1.Select(rtbCH1.Text.Length, 0);
                                                rtbCH1.SelectionColor = Color.LawnGreen;
                                                Debug.Write("Detection ended!" + out_string + "\n");
                                            }
                                        }
                                        else if (detect_rst && (txtLogRST.Text.Trim() == "is" || txtLogRST.Text.Trim() == "IS" ||
                                            txtLogRST.Text.Trim() == "="))
                                        {
                                            txtLogRST.Clear();
                                        }
                                        else if (detect_rst && (txtLogRST.Text == "" || txtLogRST.Text == " "))
                                        {
                                            txtLogRST.Clear();
                                        }
                                        else if (detect_info && (txtLogInfo.Text == "" || txtLogInfo.Text == " "))
                                        {
                                            txtLogInfo.Clear();
                                        }
                                        else if (detect_call)
                                        {
                                            string result = "";

                                            if (detect_call && text.ToUpper() != SetupForm.txtStnCALL.Text.Trim().ToUpper().ToString())
                                            {
                                                try
                                                {
                                                    if (dxcc.Analyze(txtLogCall.Text.Trim(), out result))
                                                    {
                                                        if (dxcc != null && dxcc.Visible)
                                                        {
                                                            dxcc.rtbDXCC.AppendText(result);
                                                        }

                                                        string[] vals = result.Split(' ');
                                                        txtCall.Text = vals[0];
                                                    }
                                                    else
                                                        txtLogCall.Clear();

                                                    rtbCH1.Select(rtbCH1.Text.Length, 0);
                                                    rtbCH1.SelectionColor = Color.LawnGreen;
                                                    detect_call = false;
                                                    detect_loc = false;
                                                    detect_name = false;
                                                    detect_qth = false;
                                                    detect_rst = false;
                                                    detect_info = false;
                                                    detection = false;
                                                    Debug.Write("Detection ended!" + out_string + "\n");
                                                    rtbCH1.SelectionColor = Color.LawnGreen;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Debug.Write(ex.ToString());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.LawnGreen;
                                            detect_call = false;
                                            detect_loc = false;
                                            detect_name = false;
                                            detect_qth = false;
                                            detect_rst = false;
                                            detect_info = false;
                                            detection = false;
                                            Debug.Write("Detection ended!" + out_string + "\n");
                                        }

                                        text = "";
                                    }
                                    else if ((text.StartsWith("NAME") || text.StartsWith("NAME:")) && !detect_name)
                                    {
                                        detection = true;
                                        detect_name = true;

                                        if (text.StartsWith("NAME.") && text.Length > 5)
                                        {
                                            text = text.Remove(0, 5);
                                            detection = false;
                                            detect_name = false;
                                            txtLogName.Text = text;
                                            Debug.Write("Detected Name!\n");
                                        }
                                        else if (text.StartsWith("NAME:") && text.Length > 5)
                                        {
                                            text = text.Remove(0, 5);
                                            detection = false;
                                            detect_name = false;
                                            txtLogName.Text = text;
                                            Debug.Write("Detected Name!\n");
                                        }
                                        else
                                        {
                                            text = "";
                                            Debug.Write("Detection Name!\n");
                                            Debug.Write("Detection Started!\n");
                                            txtLogName.Clear();
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.Red;
                                        }
                                    }
                                    else if ((text.StartsWith("OP") || text.StartsWith("OP:") || text.StartsWith("OP-") ||
                                        text.StartsWith("OPERATOR") || text.StartsWith("OPERATOR:")) && !detect_name)
                                    {
                                        detection = true;
                                        detect_name = true;

                                        if ((text.StartsWith("OP.") || text.StartsWith("OP:") ||
                                            text.StartsWith("OP-")) && text.Length > 3)
                                        {
                                            text = text.Remove(0, 3);
                                            detection = false;
                                            detect_name = false;
                                            txtLogName.Text = text;
                                            Debug.Write("Detected Name!\n");
                                        }
                                        else if ((text.StartsWith("OPERATOR.") || text.StartsWith("OPERATOR:") ||
                                            text.StartsWith("OPERATOR-")) && text.Length > 9)
                                        {
                                            text = text.Remove(0, 9);
                                            detection = false;
                                            detect_name = false;
                                            txtLogName.Text = text;
                                            Debug.Write("Detected Name!\n");
                                        }
                                        else
                                        {
                                            text = "";
                                            Debug.Write("Detection Name!\n");
                                            Debug.Write("Detection Started!\n");
                                            txtLogName.Clear();
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.Red;
                                        }
                                    }
                                    else if ((text.StartsWith("QTH") || text.StartsWith("QTH:")) && !detect_qth)
                                    {
                                        detection = true;
                                        detect_qth = true;

                                        if (text.StartsWith("QTH.") && text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_qth = false;
                                            txtLogQTH.Text = text;
                                            Debug.Write("Detected QTH!\n");
                                        }
                                        else if (text.StartsWith("QTH:") && text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_qth = false;
                                            txtLogQTH.Text = text;
                                            Debug.Write("Detected QTH!\n");
                                        }
                                        else
                                        {
                                            text = "";
                                            Debug.Write("Detection QTH!\n");
                                            Debug.Write("Detection Started!\n");
                                            txtLogQTH.Clear();
                                            string q = rtbCH1.Text.Remove(0, rtbCH1.Text.Length - 1);
                                            txtLogQTH.AppendText(q);
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.Red;
                                        }
                                    }
                                    else if ((text.ToUpper().Trim().StartsWith("CQ DE") || text.ToUpper().Trim().StartsWith("CQ DX") ||
                                        text.Trim().StartsWith("CQ TEST") || text.ToUpper().StartsWith("TEST ") ||
                                        text.ToUpper().StartsWith("QRZ DE")) && !detect_call)
                                    {
                                        detection = true;
                                        detect_call = true;
                                        text = "";
                                        txtLogCall.Clear();
                                        Debug.Write("Detection CALL!\n");
                                        //rtbCH1.Select(rtbCH1.Text.Length, 0);
                                        //rtbCH1.SelectionColor = Color.Red;
                                    }
                                    else if (text.ToUpper().Trim().StartsWith("CQ  DE") || text.ToUpper().Trim().StartsWith("CQ  TEST") ||
                                        text.ToUpper().StartsWith("CQ ") || text.ToUpper().Trim().StartsWith("DE") && !start_call)
                                    {
                                        detection = true;
                                        detect_call = true;
                                        text = "";
                                        txtLogCall.Clear();
                                        Debug.Write("Detection CALL!\n");
                                        //rtbCH1.Select(rtbCH1.Text.Length, 0);
                                        //rtbCH1.SelectionColor = Color.Red;
                                    }
                                    else if (text.Trim().StartsWith("CQ CQ") && !start_call)
                                    {
                                        detection = true;
                                        detect_call = true;
                                        text = "";
                                        txtLogCall.Clear();
                                        Debug.Write("Detection CALL!\n");
                                        //rtbCH1.Select(rtbCH1.Text.Length, 0);
                                        //rtbCH1.SelectionColor = Color.Red;
                                    }
                                    else if ((text.StartsWith("LOC") || text.StartsWith("LOCATOR") ||
                                        text.StartsWith("GRID") || text.StartsWith("LOK")) && !detect_loc)
                                    {
                                        detection = true;
                                        detect_loc = true;

                                        if ((text.StartsWith("LOC.") || text.StartsWith("LOK.")) && text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_loc = false;
                                            txtLOGLOC.Text = text;
                                            Debug.Write("Detected LOC!\n");
                                        }
                                        else if ((text.StartsWith("LOC:") || text.StartsWith("LOK:")) && text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_loc = false;
                                            txtLOGLOC.Text = text;
                                            Debug.Write("Detected LOC!\n");
                                        }
                                        else if (text.StartsWith("LOCATOR:") && text.Length > 8)
                                        {
                                            text = text.Remove(0, 8);
                                            detection = false;
                                            detect_loc = false;
                                            txtLOGLOC.Text = text;
                                            Debug.Write("Detected LOC!\n");
                                        }
                                        else if (text.StartsWith("LOCATOR") && text.Length > 7)
                                        {
                                            text = text.Remove(0, 7);
                                            detection = false;
                                            detect_loc = false;
                                            txtLOGLOC.Text = text;
                                            Debug.Write("Detected LOC!\n");
                                        }
                                        else if (text.StartsWith("GRID") && text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_loc = false;
                                            txtLOGLOC.Text = text;
                                            Debug.Write("Detected LOC!\n");
                                        }
                                        else if (text.StartsWith("GRID:") && text.Length > 5)
                                        {
                                            text = text.Remove(0, 5);
                                            detection = false;
                                            detect_loc = false;
                                            txtLOGLOC.Text = text;
                                            Debug.Write("Detected LOC!\n");
                                        }
                                        else
                                        {
                                            text = "";
                                            Debug.Write("Detection LOC!\n");
                                            Debug.Write("Detection Started!\n");
                                            txtLOGLOC.Clear();
                                            string q = rtbCH1.Text.Remove(0, rtbCH1.Text.Length - 1);
                                            txtLOGLOC.AppendText(q.ToUpper());
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.Red;
                                        }
                                    }
                                    else if ((text.ToUpper().StartsWith("RST") || text.ToUpper().StartsWith("RSQ") ||
                                        text.ToUpper().StartsWith("RAPORT")) && !detect_rst)
                                    {
                                        detection = true;
                                        detect_rst = true;

                                        if ((text.ToUpper().StartsWith("RST.") || text.ToUpper().StartsWith("RSQ.")) &&
                                            text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_rst = false;
                                            txtLogRST.Text = text;
                                            Debug.Write("Detected RST!\n");
                                        }
                                        else if ((text.ToUpper().StartsWith("RST:") || text.ToUpper().StartsWith("RSQ:")) &&
                                            text.Length > 4)
                                        {
                                            text = text.Remove(0, 4);
                                            detection = false;
                                            detect_rst = false;
                                            txtLogRST.Text = text;
                                            Debug.Write("Detected RST!\n");
                                        }
                                        else if (text.ToUpper().StartsWith("RAPORT") && text.Length > 6)
                                        {
                                            text = text.Remove(0, 6);
                                            detection = false;
                                            detect_rst = false;
                                            txtLogRST.Text = text;
                                            Debug.Write("Detected RST!\n");
                                        }
                                        else if (text.ToUpper().StartsWith("RAPORT:") && text.Length > 7)
                                        {
                                            text = text.Remove(0, 7);
                                            detection = false;
                                            detect_rst = false;
                                            txtLogRST.Text = text;
                                            Debug.Write("Detected RST!\n");
                                        }
                                        else
                                        {
                                            text = "";
                                            Debug.Write("Detection RST!\n");
                                            Debug.Write("Detection Started!\n");
                                            txtLogRST.Clear();
                                            string q = rtbCH1.Text.Remove(0, rtbCH1.Text.Length - 1);
                                            txtLogRST.AppendText(q.ToUpper());
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.Red;
                                        }
                                    }
                                    else if ((text.ToUpper().StartsWith("INFO ") || text.ToUpper().StartsWith("INFO:") ||
                                        text.ToUpper().StartsWith("INFO.")) && !detect_rst)
                                    {

                                        if (text.ToUpper().StartsWith("INFO:") && text.Length > 5)
                                        {
                                            text = text.Remove(0, 5);
                                            detection = false;
                                            detect_info = false;
                                            txtLogInfo.Text = text;
                                            Debug.Write("Detected Info!\n");
                                        }
                                        else if (text.ToUpper().StartsWith("INFO.") && text.Length > 5)
                                        {
                                            text = text.Remove(0, 5);
                                            detection = false;
                                            detect_info = false;
                                            txtLogInfo.Text = text;
                                            Debug.Write("Detected Info!\n");
                                        }
                                        else
                                        {
                                            text = "";
                                            Debug.Write("Detection Info!\n");
                                            Debug.Write("Detection Started!\n");
                                            txtLogInfo.Clear();
                                            string q = rtbCH1.Text.Remove(0, rtbCH1.Text.Length - 1);
                                            txtLogInfo.AppendText(q.ToUpper());
                                            rtbCH1.Select(rtbCH1.Text.Length, 0);
                                            rtbCH1.SelectionColor = Color.Red;
                                        }
                                    }
                                    else
                                    {
                                        if (detection)
                                            text += out_string;
                                        else
                                            text += out_string.ToUpper();

                                        if (!text.ToUpper().StartsWith("CQ ") && !text.ToUpper().StartsWith("CQ  ") &&
                                            !text.ToUpper().Contains("CQ "))
                                            text = "";
                                        else if (text.ToUpper().StartsWith("CQ CQ"))
                                            text = text.Remove(0, 3);
                                        else if (text.ToUpper().StartsWith("CQ  CQ"))
                                            text = text.Remove(0, 4);
                                        else if (text.Length > 6)
                                        {
                                            if (text.ToUpper().EndsWith("CQ DE "))
                                                text = text.Remove(0, text.Length - 6);
                                            else
                                                text = text.Remove(0, text.Length - 3);
                                        }
                                    }
                                }
                                else
                                {
                                    if (detection)
                                        text += out_string;
                                    else
                                    {
                                        out_string = out_string.Replace("\r", "");
                                        out_string = out_string.Replace("\n", "");
                                        out_string = out_string.Replace("\0", "");
                                        text += out_string.ToUpper();
                                    }
                                }
                            }
                            else
                            {
                                if (rtbCH1.SelectedText == "")
                                {
                                    rtbCH1.AppendText(out_string);

                                    if (rtbCH1_scroll_down)
                                        SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 1);
                                }
                                else
                                {
                                    ch1_tmp_string += out_string;  //.Replace("\r", "");
                                    //ch1_tmp_string = ch1_tmp_string.Replace("\0", "");
                                }
                            }

                            if (rtbCH1.TextLength == 8192)
                            {
                                rtbCH1.Clear();
                            }

                            break;

                        case 6:                                   // rtb2 CH2
                            if (rtbCH2.SelectedText == "")
                            {
                                rtbCH2.AppendText(out_string);

                                if (rtbCH2_scroll_down)
                                    SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
                            }
                            else
                            {
                                ch2_tmp_string += out_string.Replace("\r", "");
                                ch2_tmp_string = ch2_tmp_string.Replace("\0", "");
                            }

                            if (rtbCH2.TextLength == 8192)
                            {
                                rtbCH2.Clear();
                            }

                            break;

                        case 100:
                            lblPSKCH1Freq.Text = out_string;
                            break;

                        case 101:
                            lblPSKCH2Freq.Text = out_string;
                            break;

                        case 102:
                            txtFreq.Text = out_string;
                            break;
                    }
                }
                else
                {
                    if (message <= 20 && message >= 2)
                    {
                        switch (message)
                        {
                            case 2:
                                txtChannel2.Text = "2  " + out_string;
                                break;
                            case 3:
                                txtChannel3.Text = "3  " + out_string;
                                break;
                            case 4:
                                txtChannel4.Text = "4  " + out_string;
                                break;
                            case 5:
                                txtChannel5.Text = "5  " + out_string;
                                break;
                            case 6:
                                txtChannel6.Text = "6  " + out_string;
                                break;
                            case 7:
                                txtChannel7.Text = "7  " + out_string;
                                break;
                            case 8:
                                txtChannel8.Text = "8  " + out_string;
                                break;
                            case 9:
                                txtChannel9.Text = "9  " + out_string;
                                break;
                            case 10:
                                txtChannel10.Text = "10 " + out_string;
                                break;
                            case 11:
                                txtChannel11.Text = "11" + out_string;
                                break;
                            case 12:
                                txtChannel12.Text = "12" + out_string;
                                break;
                            case 13:
                                txtChannel13.Text = "13 " + out_string;
                                break;
                            case 14:
                                txtChannel14.Text = "14 " + out_string;
                                break;
                            case 15:
                                txtChannel15.Text = "15 " + out_string;
                                break;
                            case 16:
                                txtChannel16.Text = "16 " + out_string;
                                break;
                            case 17:
                                txtChannel17.Text = "17 " + out_string;
                                break;
                            case 18:
                                txtChannel18.Text = "18 " + out_string;
                                break;
                            case 19:
                                txtChannel19.Text = "19 " + out_string;
                                break;
                            case 20:
                                txtChannel20.Text = "20 " + out_string;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void txtCH1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void txtCH2_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        #endregion

        #region mouse events

        private void picWaterfall_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
        }

        private void picWaterfall_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                double freq = 0.0;
                picWaterfall.Focus();
                Display_GDI.DisplayCursorX = e.X;
#if(DirectX)
                DX.DisplayCursorX = e.X;
                DX.DisplayCursorY = e.Y;
                DX.waterfall_target_focused = true;
                DX.panadapter_target_focused = false;
#endif
                Display_GDI.DisplayCursorY = e.Y;
                float x = PixelToHz(e.X);

                if (Audio.SDRmode)
                {
                    freq = x / 1e6;
                    txtFreq.Text = freq.ToString("f6") + "Hz";

                    if (op_mode_vfoA == Mode.RTTY && rtty != null)
                    {
#if(DirectX)
                        DX.RTTYCursorX1 = (int)HzToPixel((float)((freq - losc - (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                        DX.RTTYCursorX2 = (int)HzToPixel((float)((freq - losc + (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
#endif
                        Display_GDI.RTTYCursorX1 = (int)HzToPixel((float)((freq - losc - (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                        Display_GDI.RTTYCursorX2 = (int)HzToPixel((float)((freq - losc + (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                    }
                }
                else
                    txtFreq.Text = Math.Round(x, 0).ToString() + "Hz";
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void picWaterfall_MouseEnter(object sender, EventArgs e)
        {
            picWaterfall.Focus();
        }

        private void picWaterfall_MouseDown(object sender, MouseEventArgs e)
        {
            if (display_mode == DisplayMode.PANAFALL || display_mode == DisplayMode.PANAFALL_INV ||
                display_mode == DisplayMode.PANASCOPE_INV)
                picPanadapter_MouseDown(sender, e);

            picWaterfall.Focus();
        }

        private void btnAudioMute_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAudioMute.Checked)
                Audio.Volume = 0.0;
            else
            {
                if (Audio.SDRmode)
                    Audio.Volume = tbAFGain.Value;
                else
                    Audio.Volume = tbVolume.Value * 10;
            }
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {
            if (!Audio.SDRmode)
            {
                if (!btnMute.Checked)
                    Audio.Volume = tbVolume.Value * 10;

                Audio.ScopeLevel = tbVolume.Value;
            }
        }

        private void tbVolume_MouseHover(object sender, EventArgs e)
        {
            tbVolume.Focus();
        }

        private void tbVolume_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
        }

        private void tbInputLevel_Scroll(object sender, EventArgs e)
        {
            Audio.InputLevel = (double)tbInputLevel.Value;
        }

        private void tbInputLevel_MouseHover(object sender, EventArgs e)
        {
            tbInputLevel.Focus();
        }

        private void tbInputLevel_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
        }

        private void MainFormMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (picPanadapter.Focused)
            {
                double freq = vfoa;
                int numberToMove = e.Delta / 120;
                double mult = 0.0;

                if (display_mode == DisplayMode.PANADAPTER || display_mode == DisplayMode.PANAFALL ||
                    display_mode == DisplayMode.PANASCOPE)
                {
                    if (freq_mult)
                        mult = 0.00005;
                    else
                        mult = 0.00001;
                }
                else if (display_mode == DisplayMode.PANAFALL_INV || display_mode == DisplayMode.PANASCOPE_INV)
                    mult = 0.000001;

                if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                {
                    if (numberToMove > 0)
                    {
                        VFOA += numberToMove * mult;
                        psk.trx.modem[0].scan_direction = PSK.ScanDirection.Down;
                    }
                    else
                    {
                        VFOA += numberToMove * mult;
                        psk.trx.modem[0].scan_direction = PSK.ScanDirection.Up;
                    }
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    if (numberToMove > 0)
                    {
                        VFOB += numberToMove * mult;
                        psk.trx.modem[1].scan_direction = PSK.ScanDirection.Down;
                    }
                    else
                    {
                        VFOB += numberToMove * mult;
                        psk.trx.modem[1].scan_direction = PSK.ScanDirection.Up;
                    }
                }
            }
            else if (picWaterfall.Focused)
            {
                double freq = vfoa;
                int numberToMove = e.Delta / 120;
                double mult = 0.0;

                if (display_mode == DisplayMode.PANADAPTER || display_mode == DisplayMode.PANAFALL ||
                    display_mode == DisplayMode.PANASCOPE)
                    mult = 0.000001;
                else if (display_mode == DisplayMode.PANAFALL_INV || display_mode == DisplayMode.PANASCOPE_INV)
                    mult = 0.00001;

                if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                {
                    if (numberToMove > 0)
                    {
                        VFOA += numberToMove * mult;
                        psk.trx.modem[0].scan_direction = PSK.ScanDirection.Down;
                    }
                    else
                    {
                        VFOA += numberToMove * mult;
                        psk.trx.modem[0].scan_direction = PSK.ScanDirection.Up;
                    }
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    if (numberToMove > 0)
                    {
                        VFOB += numberToMove * mult;
                        psk.trx.modem[1].scan_direction = PSK.ScanDirection.Down;
                    }
                    else
                    {
                        VFOB += numberToMove * mult;
                        psk.trx.modem[1].scan_direction = PSK.ScanDirection.Up;
                    }
                }
            }
            else if (txtLosc.Focused)
            {
                double freq = losc;
                int numberToMove = e.Delta / 120;

                if (freq_mult)
                {
                    if (numberToMove > 0)
                    {
                        LOSC += numberToMove * 0.01;
                    }
                    else
                    {
                        LOSC += numberToMove * 0.01;
                    }
                }
                else
                {
                    if (numberToMove > 0)
                    {
                        LOSC += numberToMove * 0.005;
                    }
                    else
                    {
                        LOSC += numberToMove * 0.005;
                    }
                }
            }
            else if (txtVFOA.Focused)
            {
                double freq = losc;
                int numberToMove = e.Delta / 120;

                if (numberToMove > 0)
                {
                    VFOA += numberToMove * 0.0001;
                    psk.trx.modem[0].scan_direction = PSK.ScanDirection.Down;
                }
                else
                {
                    VFOA += numberToMove * 0.0001;
                    psk.trx.modem[0].scan_direction = PSK.ScanDirection.Up;
                }
            }
            else if (txtVFOB.Focused)
            {
                double freq = losc;
                int numberToMove = e.Delta / 120;

                if (numberToMove > 0)
                {
                    VFOB += numberToMove * 0.0001;
                    psk.trx.modem[1].scan_direction = PSK.ScanDirection.Down;
                }
                else
                {
                    VFOB += numberToMove * 0.0001;
                    psk.trx.modem[1].scan_direction = PSK.ScanDirection.Up;
                }
            }
            else if (picMonitor.Focused)
            {
                int numberToMove = e.Delta / 120;

                if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                {
                    if (numberToMove > 0)
                    {
                        VFOA += numberToMove * 0.000001;
                        psk.trx.modem[0].scan_direction = PSK.ScanDirection.Down;
                    }
                    else
                    {
                        VFOA += numberToMove * 0.000001;
                        psk.trx.modem[0].scan_direction = PSK.ScanDirection.Up;
                    }
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    if (numberToMove > 0)
                    {
                        VFOB += numberToMove * 0.000001;
                        psk.trx.modem[1].scan_direction = PSK.ScanDirection.Down;
                    }
                    else
                    {
                        VFOB += numberToMove * 0.000001;
                        psk.trx.modem[1].scan_direction = PSK.ScanDirection.Up;
                    }
                }
            }
        }

        private void txtLosc_MouseHover(object sender, EventArgs e)
        {
            txtLosc.Focus();
            txtLosc.ForeColor = Color.Yellow;
        }

        private void txtLosc_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            txtLosc.ForeColor = Color.White;
        }

        private void txtVFOA_MouseHover(object sender, EventArgs e)
        {
            txtVFOA.Focus();
            txtVFOA.ForeColor = Color.Yellow;
        }

        private void txtVFOA_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            txtVFOA.ForeColor = Color.White;
        }

        private void txtVFOB_MouseHover(object sender, EventArgs e)
        {
            txtVFOB.Focus();
            txtVFOB.ForeColor = Color.Yellow;
        }

        private void txtVFOB_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            txtVFOB.ForeColor = Color.White;
        }

        #endregion

        #region G59

        private void btnAF_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAF.Checked)
            {
                genesis.WriteToDevice(5, 0);
                btnAF.BackColor = Color.LimeGreen;
                Audio.iq_balancer_reset = true;
            }
            else
            {
                genesis.WriteToDevice(6, 0);
                btnAF.BackColor = Color.WhiteSmoke;
                Audio.iq_balancer_reset = true;
            }
        }

        private void btnRF_CheckedChanged(object sender, EventArgs e)
        {
            if (btnRF.Checked)
            {
                genesis.WriteToDevice(11, 0);
                btnRF.BackColor = Color.LimeGreen;
                Audio.iq_balancer_reset = true;
            }
            else
            {
                genesis.WriteToDevice(12, 0);
                btnRF.BackColor = Color.WhiteSmoke;
                Audio.iq_balancer_reset = true;
            }
        }

        private void btnATT_CheckedChanged(object sender, EventArgs e)
        {
            if (btnATT.Checked)
            {
                genesis.WriteToDevice(16, 0);
                btnATT.BackColor = Color.LimeGreen;
                Audio.iq_balancer_reset = true;
            }
            else
            {
                genesis.WriteToDevice(17, 0);
                btnATT.BackColor = Color.WhiteSmoke;
                Audio.iq_balancer_reset = true;
            }
        }

        private void btnNB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetNB(0, 0, btnNB.Checked);
                SetNB(0, 1, btnNB.Checked);

                if (btnNB.Checked)
                {
                    btnNB.BackColor = Color.LimeGreen;
                }
                else
                {
                    btnNB.BackColor = Color.WhiteSmoke;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnMute_CheckedChanged(object sender, EventArgs e)
        {
            if (Audio.SDRmode)
            {
                if (btnMute.Checked)
                {
                    Audio.Volume = 0.0;
                    btnMute.BackColor = Color.LimeGreen;
                }
                else
                {
                    Audio.Volume = tbAFGain.Value;
                    btnMute.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void tbRFGain_Scroll(object sender, EventArgs e)
        {
            //Audio.InputLevel = (double)tbRFGain.Value;
            SetRXAGCMaxGain(0, 0, (double)tbRFGain.Value);
            SetRXAGCMaxGain(0, 1, (double)tbRFGain.Value);
            SetRXAGCMaxGain(0, 2, (double)tbRFGain.Value);
            SetRXAGCMaxGain(0, 3, (double)tbRFGain.Value);
        }

        private void tbRFGain_MouseHover(object sender, EventArgs e)
        {
            tbRFGain.Focus();
            lblRFGain.ForeColor = Color.Yellow;
        }

        private void tbRFGain_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblRFGain.ForeColor = Color.White;
        }

        private void tbAFGain_Scroll(object sender, EventArgs e)
        {
            if (Audio.SDRmode)
            {
                if (!btnMute.Checked)
                    Audio.Volume = tbAFGain.Value;

                Audio.ScopeLevel = tbAFGain.Value * 5;
            }
        }

        private void tbAFGain_MouseHover(object sender, EventArgs e)
        {
            tbAFGain.Focus();
            lblAFGain.ForeColor = Color.Yellow;
        }

        private void tbAFGain_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblAFGain.ForeColor = Color.White;
        }

        private void chkSplit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSplit.Checked)
                chkSplit.BackColor = Color.LimeGreen;
            else
                chkSplit.BackColor = Color.WhiteSmoke;

            tx_split = chkSplit.Checked;
            InitLOG();
            TUNE = false;
            MOX = false;
        }

        private void tbG59PWR_MouseHover(object sender, EventArgs e)
        {
            tbG59PWR.Focus();
            lblPWR.ForeColor = Color.Yellow;
        }

        private void tbG59PWR_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblPWR.ForeColor = Color.White;
        }

        private void tbG59PWR_Scroll(object sender, EventArgs e)
        {
            tx_pwr = tbG59PWR.Value;
            OutputPowerUpdate();
        }

        public void OutputPowerUpdate()
        {
            try
            {
                double val = tx_pwr;
                double target_dbm = 10 * (double)Math.Log10(val * 10000);
                target_dbm -= GainByBand(current_band);

                double target_volts = Math.Sqrt(Math.Pow(10, target_dbm * 0.1) * 0.05);		// E = Sqrt(P * R) 
                Audio.PWR = target_volts / Audio.AudioVolts1;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public bool G59Init()
        {
            try
            {
                switch (current_model)
                {
                    case Model.GENESIS_G59USB:
                        genesis.myPID = 6512;
                        genesis.myVID = -2;
                        break;

                    case Model.GENESIS_G11:
                        genesis.myPID = 6513;
                        genesis.myVID = -2;
                        break;
                }

                genesis.Connect();

                if (genesis.Connected)
                {
                    genesis.WriteToDevice(14, 0);
                    Thread.Sleep(1);
                    genesis.WriteToDevice(26, 0);                           // Line in
                    Thread.Sleep(1);
                    genesis.WriteToDevice(0, 0);
                    Thread.Sleep(1);
                    genesis.WriteToDevice(24, 0);                           // CW monitor off
                    Thread.Sleep(1);
                    genesis.WriteToDevice(28, g59_ptt_inverted);            // external PA inverted
                    Thread.Sleep(1);
                    genesis.KEYER = 0xff;                                   // reset data
                    G59_set_keyer();
                    Thread.Sleep(1);
                    genesis.WriteToDevice(22, 0);
                    Thread.Sleep(1);

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);

                    if (PA10_present)
                        genesis.WriteToDevice(21, 1);
                    else
                        genesis.WriteToDevice(21, 0);

                    Thread.Sleep(1);
                    btnATT_CheckedChanged(null, null);
                    Thread.Sleep(1);
                    btnAF_CheckedChanged(null, null);
                    Thread.Sleep(1);
                    btnRF_CheckedChanged(null, null);
                    Thread.Sleep(1);
                    genesis.SmoothTuning = true;
                    Thread.Sleep(1);
                    genesis.Set_frequency((long)(losc * 1e6), true);
                    Thread.Sleep(1);
                    //genesis.SetCallback(G59Callback);
                    lblUSB.BackColor = Color.Green;
                    GenesisSecRXAnt = genesis_sec_rx_ant;
                    Thread.Sleep(1);
                }
                else
                {
                    lblUSB.BackColor = Color.Red;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public void G59_set_keyer()
        {
            try
            {
                Thread.Sleep(1);

                if (genesis != null)
                {
                    genesis.KEYER = 0xff;   // reset data
                    genesis.WriteToDevice(4,
                        (long)(1200 / ((double)SetupForm.udCWSpeed.Value * 2.3)));              // write CW keyer speed
                    Thread.Sleep(1);
                    genesis.WriteToDevice(20, (long)(SetupForm.udG59DASHDOTRatio.Value * 10));  // write DASH/DOT ratio
                    Thread.Sleep(1);
                    genesis.WriteToDevice(24, 0);       // monitor off
                    Thread.Sleep(1);

                    switch (current_model)
                    {
                        case Model.GENESIS_G59USB:
                        case Model.GENESIS_G11:
                            {
                                switch (SetupForm.GenesisKeyerMode)
                                {
                                    case Keyer_mode.Iambic_B_Mode:
                                        genesis.WriteToDevice(18, (long)Keyer_mode.Iambic_B_Mode);
                                        genesis.KeyerMode = (short)Keyer_mode.Iambic_B_Mode;
                                        break;

                                    case Keyer_mode.Iambic:
                                        genesis.WriteToDevice(18, (long)Keyer_mode.Iambic);
                                        genesis.KeyerMode = (short)Keyer_mode.Iambic;
                                        break;

                                    case Keyer_mode.Iambic_Reverse_B_Mode:
                                        genesis.WriteToDevice(18, (long)Keyer_mode.Iambic_Reverse_B_Mode);
                                        genesis.KeyerMode = (short)Keyer_mode.Iambic_Reverse_B_Mode;
                                        break;

                                    case Keyer_mode.IambicReverse:
                                        genesis.WriteToDevice(18, (long)Keyer_mode.IambicReverse);
                                        genesis.KeyerMode = (short)Keyer_mode.IambicReverse;
                                        break;
                                    case Keyer_mode.HandKey:
                                        genesis.WriteToDevice(18, (long)Keyer_mode.HandKey);
                                        genesis.KeyerMode = (short)Keyer_mode.HandKey;
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void G59Callback(int type, int data)
        {
            if (op_mode_vfoA == Mode.CW)
            {
                switch (type)
                {
                    case 1:
                        switch (data)
                        {
                            case 0:
                                // "DASH_ON"
                                cwEncoder.keyer_state = KeyerState.Dash;
                                cwEncoder.KeyerEvent.Set();
                                break;

                            case 1:
                                // "DOT_ON"
                                cwEncoder.keyer_state = KeyerState.Dot;

                                if (!SetupForm.chkG59Iambic.Checked && !mox)
                                    MOX = true;

                                cwEncoder.KeyerEvent.Set();
                                break;

                            case 2:
                                // "DASH_OFF"
                                cwEncoder.keyer_state = KeyerState.Silence;
                                cwEncoder.KeyerEvent.Set();
                                break;

                            case 3:
                                // "DOT_OFF"
                                cwEncoder.keyer_state = KeyerState.Silence;
                                cwEncoder.KeyerEvent.Set();
                                break;

                            case 170:
                                cwEncoder.KeyerEvent.Set();
                                cwEncoder.watchdog_event.Set();
                                cwEncoder.local_mox = true;

                                while (output_ring_buf.ReadSpace() <= 2048)
                                    Thread.Sleep(1);

                                MOX = true;
                                // "TX_ON"
                                break;
                        }

                        cw_message_repeat = false;
                        cw_message_event.Set();
                        //cwEncoder.KeyerEvent.Set();
                        break;

                    case 2:
                        switch (data)
                        {
                            case 0:
                                // "TUNE_ON"
                                break;

                            case 1:
                                // "TUNE_OFF"

                                break;
                        }
                        break;
                }
            }
        }

        private float GainByBand(Band b)
        {
            float retval = 0;
            switch (b)
            {
                case Band.B160M:
                    retval = SetupForm.PAGain160;
                    break;
                case Band.B80M:
                    retval = SetupForm.PAGain80;
                    break;
                case Band.B40M:
                    retval = SetupForm.PAGain40;
                    break;
                case Band.B30M:
                    retval = SetupForm.PAGain30;
                    break;
                case Band.B20M:
                    retval = SetupForm.PAGain20;
                    break;
                case Band.B17M:
                    retval = SetupForm.PAGain17;
                    break;
                case Band.B15M:
                    retval = SetupForm.PAGain15;
                    break;
                case Band.B12M:
                    retval = SetupForm.PAGain12;
                    break;
                case Band.B10M:
                    retval = SetupForm.PAGain10;
                    break;
                case Band.B6M:
                    retval = SetupForm.PAGain6;
                    break;
                default:
                    retval = 48.0f;
                    break;
            }

            return retval;
        }

        public Band G11BandByFreq(double freq)     // yt7pwr
        {
            Band f = Band.B160M;

            if (freq >= 1.0 && freq <= 2.5)
            {
                f = Band.B160M;
            }
            else if (freq >= 2.5 && freq <= 4.5)
            {
                f = Band.B80M;
            }
            else if (freq >= 4.5 && freq <= 8.0)
            {
                f = Band.B40M;
            }
            else if (freq >= 8.0 && freq <= 12.5)
            {
                f = Band.B30M;
            }
            else if (freq >= 12.5 && freq <= 19.5)
            {
                f = Band.B20M;
            }
            else if (freq >= 19.5 && freq <= 34.0)
            {
                f = Band.B10M;
            }
            else if (freq >= 34.0 && freq <= 57.0)
            {
                f = Band.B6M;
            }
            else
                f = Band.B160M;

            return f;
        }

        public void G11SetBandFilter(double freq)
        {
            try
            {
                Thread.Sleep(1);
                Band b;

                if (g11_multiband)
                    b = G11BandByFreq(freq);
                else
                    b = current_band;

                if (g11_multiband)
                {
                    switch (b)
                    {
                        case Band.B160M:
                            genesis.WriteToDevice(2, (long)0);
                            break;

                        case Band.B80M:
                            genesis.WriteToDevice(2, (long)1);
                            break;

                        case Band.B40M:
                            genesis.WriteToDevice(2, (long)2);
                            break;

                        case Band.B30M:
                            genesis.WriteToDevice(2, (long)3);
                            break;

                        case Band.B17M:
                        case Band.B20M:
                            genesis.WriteToDevice(2, (long)4);
                            break;

                        case Band.B15M:
                        case Band.B12M:
                        case Band.B10M:
                            genesis.WriteToDevice(2, (long)5);
                            break;

                        case Band.B6M:
                            genesis.WriteToDevice(2, (long)6);
                            break;

                        default:
                            genesis.WriteToDevice(2, (long)7);      // A=B=C=1 general RX
                            break;
                    }
                }
                else
                {
                    switch (b)
                    {
                        case Band.B160M:
                            if (G11BandFiltersCH2[(int)Band.B160M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            break;
                        case Band.B80M:
                            if (G11BandFiltersCH2[(int)Band.B80M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B80M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B40M:
                            if (G11BandFiltersCH2[(int)Band.B40M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B40M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B30M:
                            if (G11BandFiltersCH2[(int)Band.B30M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B30M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B20M:
                            if (G11BandFiltersCH2[(int)Band.B20M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B20M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B17M:
                            if (G11BandFiltersCH2[(int)Band.B17M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B17M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B15M:
                            if (G11BandFiltersCH2[(int)Band.B15M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B15M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B12M:
                            if (G11BandFiltersCH2[(int)Band.B12M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B12M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B10M:
                            if (G11BandFiltersCH2[(int)Band.B10M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B10M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                        case Band.B6M:
                            if (G11BandFiltersCH2[(int)Band.B6M])
                            {
                                genesis.WriteToDevice(3, (long)1);
                                genesis.WriteToDevice(3, (long)3);
                            }
                            else if (G11BandFiltersCH1[(int)Band.B6M])
                            {
                                genesis.WriteToDevice(3, (long)2);
                                genesis.WriteToDevice(3, (long)0);
                            }
                            break;
                    }
                }

                Thread.Sleep(1);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region band buttons

        private void radBand160_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand160.Checked)
            {
                radBand160.BackColor = Color.LimeGreen;
                CurrentBand = Band.B160M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B160M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 1.84;
                    VFOA = 1.85;
                    VFOB = 1.83;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B160M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand160.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand80_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand80.Checked)
            {
                radBand80.BackColor = Color.LimeGreen;
                CurrentBand = Band.B80M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B80M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 3.54;
                    VFOA = 3.55;
                    VFOB = 3.53;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B80M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand80.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand40_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand40.Checked)
            {
                radBand40.BackColor = Color.LimeGreen;
                CurrentBand = Band.B40M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B40M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 7.04;
                    VFOA = 7.05;
                    VFOB = 7.03;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B40M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand40.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand30_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand30.Checked)
            {
                radBand30.BackColor = Color.LimeGreen;
                CurrentBand = Band.B30M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B30M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 10.12;
                    VFOA = 10.13;
                    VFOB = 10.11;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B30M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand30.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand20_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand20.Checked)
            {
                radBand20.BackColor = Color.LimeGreen;
                CurrentBand = Band.B20M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B20M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 14.04;
                    VFOA = 14.05;
                    VFOB = 14.03;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (!get_state)
                    if (current_band == Band.B20M)
                        DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                            tbZoom.Value, tbPan.Value);

                radBand20.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand17_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand17.Checked)
            {
                radBand17.BackColor = Color.LimeGreen;
                CurrentBand = Band.B17M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B17M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 18.11;
                    VFOA = 18.12;
                    VFOB = 18.105;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B17M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand17.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand15_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand15.Checked)
            {
                radBand15.BackColor = Color.LimeGreen;
                CurrentBand = Band.B15M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B15M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 21.04;
                    VFOA = 21.05;
                    VFOB = 21.03;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B15M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand15.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand12_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand12.Checked)
            {
                radBand12.BackColor = Color.LimeGreen;
                CurrentBand = Band.B12M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B12M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 24.92;
                    VFOA = 24.93;
                    VFOB = 24.91;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B12M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand12.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand10_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand10.Checked)
            {
                radBand10.BackColor = Color.LimeGreen;
                CurrentBand = Band.B10M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B10M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 28.04;
                    VFOA = 28.05;
                    VFOB = 28.03;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B10M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand10.BackColor = Color.WhiteSmoke;
            }
        }

        private void radBand6_CheckedChanged(object sender, EventArgs e)
        {
            if (radBand6.Checked)
            {
                radBand6.BackColor = Color.LimeGreen;
                CurrentBand = Band.B6M;

                if (current_model == Model.GENESIS_G59USB)
                    genesis.WriteToDevice(3, (long)(BandFilter.B6M));

                try
                {
                    GetBandStack();

                    if (current_model == Model.GENESIS_G11)
                        G11SetBandFilter(vfoa);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                    LOSC = 50.08;
                    VFOA = 50.09;
                    VFOB = 50.07;
                    FilterWidthVFOA = 100;
                    FilterWidthVFOB = 100;
                    tbPan.Value = 0;
                    tbZoom.Value = 4;
                    lblPan_Click(null, null);
                }
            }
            else
            {
                if (current_band == Band.B6M)
                    DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                        tbZoom.Value, tbPan.Value);

                radBand6.BackColor = Color.WhiteSmoke;
            }
        }

        private void GetBandStack()
        {
            double vfoa_freq, vfob_freq, losc_freq;
            int filter_vfoA, filter_vfoB, zoom, pan;
            DB.GetBandStack(current_band.ToString(), out vfoa_freq, out vfob_freq, out losc_freq, out filter_vfoA,
                out filter_vfoB, out zoom, out pan);
            LOSC = losc_freq;
            VFOA = vfoa_freq;
            VFOB = vfob_freq;
            FilterWidthVFOA = filter_vfoA;
            FilterWidthVFOB = filter_vfoB;
            tbZoom.Value = zoom;
            tbPan.Value = pan;
            lblPan_Click(null, null);
        }

        #endregion

        #region Filter width

        private void UpdateFilters()
        {
            try
            {
                if (!booting && btnStartMR.Checked)
                {
                    if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                    {
                        tbFilterWidth.Value = filter_width_vfoA;
                        txtFilterWidth.Text = filter_width_vfoA.ToString() + "Hz";

                        switch (filter_width_vfoA)
                        {
                            case 50:
                                radFilter50.Checked = true;
                                break;

                            case 100:
                                radFilter100.Checked = true;
                                break;

                            case 250:
                                radFilter250.Checked = true;
                                break;

                            case 500:
                                radFilter500.Checked = true;
                                break;

                            case 1000:
                                radFilter1K.Checked = true;
                                break;

                            default:
                                radFilterVar.Checked = true;
                                break;
                        }
                    }
                    else
                    {
                        tbFilterWidth.Value = filter_width_vfoB;
                        txtFilterWidth.Text = filter_width_vfoB.ToString() + "Hz";

                        switch (filter_width_vfoB)
                        {
                            case 50:
                                radFilter50.Checked = true;
                                break;

                            case 100:
                                radFilter100.Checked = true;
                                break;

                            case 250:
                                radFilter250.Checked = true;
                                break;

                            case 500:
                                radFilter500.Checked = true;
                                break;

                            case 1000:
                                radFilter1K.Checked = true;
                                break;

                            default:
                                radFilterVar.Checked = true;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbFilterWidth_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (!booting && btnStartMR.Checked)
                {
                    if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                    {
                        switch (op_mode_vfoA)
                        {
                            case Mode.CW:
                                {
                                    filter_width_vfoA = tbFilterWidth.Value;
                                    txtFilterWidth.Text = filter_width_vfoA.ToString() + "Hz";
                                    VFOA = vfoa;
                                }
                                break;

                            case Mode.RTTY:
                                {
                                    filter_width_vfoA = tbFilterWidth.Value;
                                    txtFilterWidth.Text = filter_width_vfoA.ToString() + "Hz";
                                    VFOA = vfoa;
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                {
                                    filter_width_vfoA = tbFilterWidth.Value;
                                    txtFilterWidth.Text = filter_width_vfoA.ToString() + "Hz";
                                    VFOA = vfoa;
                                    int result = SetRXFilter(0, 0, psk_pitch - (filter_width_vfoA / 2), psk_pitch + filter_width_vfoA / 2);
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (op_mode_vfoB)
                        {
                            case Mode.CW:
                                {
                                    filter_width_vfoB = tbFilterWidth.Value;
                                    txtFilterWidth.Text = filter_width_vfoB.ToString() + "Hz";
                                    VFOB = vfob;
                                }
                                break;

                            case Mode.RTTY:
                                {
                                    filter_width_vfoB = tbFilterWidth.Value;
                                    txtFilterWidth.Text = filter_width_vfoB.ToString() + "Hz";
                                    VFOB = vfob;
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                {
                                    filter_width_vfoB = tbFilterWidth.Value;
                                    txtFilterWidth.Text = filter_width_vfoB.ToString() + "Hz";
                                    VFOB = vfob;
                                    int result = SetRXFilter(0, 1, psk_pitch - (filter_width_vfoB / 2), psk_pitch + filter_width_vfoB / 2);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbFilterWidth_MouseHover(object sender, EventArgs e)
        {
            tbFilterWidth.Focus();
            lblFilterwidth.ForeColor = Color.Yellow;
        }

        private void tbFilterWidth_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblFilterwidth.ForeColor = Color.White;
        }

        private void radFilter1K_CheckedChanged(object sender, EventArgs e)
        {
            if (radFilter1K.Checked)
            {
                tbFilterWidth.Value = 1000;
                txtFilterWidth.Text = "1000";
                tbFilterWidth.Visible = false;
                radFilter1K.BackColor = Color.LimeGreen;
            }
            else
                radFilter1K.BackColor = Color.WhiteSmoke;
        }

        private void radFilter500_CheckedChanged(object sender, EventArgs e)
        {
            if (radFilter500.Checked)
            {
                tbFilterWidth.Value = 500;
                txtFilterWidth.Text = "500";
                tbFilterWidth.Visible = false;
                radFilter500.BackColor = Color.LimeGreen;
            }
            else
                radFilter500.BackColor = Color.WhiteSmoke;
        }

        private void radFilter100_CheckedChanged(object sender, EventArgs e)
        {
            if (radFilter100.Checked)
            {
                tbFilterWidth.Value = 100;
                txtFilterWidth.Text = "100";
                tbFilterWidth.Visible = false;
                radFilter100.BackColor = Color.LimeGreen;
            }
            else
                radFilter100.BackColor = Color.WhiteSmoke;
        }

        private void radFilter250_CheckedChanged(object sender, EventArgs e)
        {
            if (radFilter250.Checked)
            {
                tbFilterWidth.Value = 250;
                txtFilterWidth.Text = "250";
                tbFilterWidth.Visible = false;
                radFilter250.BackColor = Color.LimeGreen;
            }
            else
                radFilter250.BackColor = Color.WhiteSmoke;
        }

        private void radFilter50_CheckedChanged(object sender, EventArgs e)
        {
            if (radFilter50.Checked)
            {
                tbFilterWidth.Value = 50;
                txtFilterWidth.Text = "50";
                tbFilterWidth.Visible = false;
                radFilter50.BackColor = Color.LimeGreen;
            }
            else
                radFilter50.BackColor = Color.WhiteSmoke;
        }

        private void radFilterVar_CheckedChanged(object sender, EventArgs e)
        {
            if (radFilterVar.Checked)
            {
                tbFilterWidth.Visible = true;
                radFilterVar.BackColor = Color.LimeGreen;
            }
            else
                radFilterVar.BackColor = Color.WhiteSmoke;
        }

        #endregion

        #region Smeter

        private void Smeter_thread_function()
        {
            try
            {
                float num = 0f;
                double power = 0;
                float num_tmp = 0f;
                double swr = 1.0;
                double vswr = 1.0;

                while (runDisplay)
                {
                    if (mox)
                    {
                        switch (TX_meter_type)
                        {
                            case MeterType.DIR_PWR:
                                power = PAPower((int)genesis.fwd_PWR);
                                power = 0.8f * pwr_avg + 0.2f * power;
                                pwr_avg = (float)power;
                                SMeter.Value = (float)power;
                                break;

                            case MeterType.REFL_PWR:
                                power = PAPower((int)genesis.Ref_PWR);
                                power = 0.8f * pwr_avg + 0.21f * power;
                                pwr_avg = (float)power;
                                SMeter.Value = (float)power;
                                break;

                            case MeterType.SWR:
                                swr = SWR(genesis.fwd_PWR, genesis.Ref_PWR);
                                vswr = Math.Round(swr, 2);

                                if (vswr <= 1.1)
                                {
                                    vswr -= 1.0;
                                    vswr += 1;
                                }
                                else if (vswr <= 1.2)
                                {
                                    vswr -= 1.1;
                                    vswr *= 10;
                                    vswr += 1;
                                }
                                else if (vswr <= 1.3)
                                {
                                    vswr -= 1.2;
                                    vswr *= 9;
                                    vswr += 1.5;
                                }
                                else if (vswr <= 1.4)
                                {
                                    vswr -= 1.3;
                                    vswr *= 8;
                                    vswr += 2.5;
                                }
                                else if (vswr <= 1.5)
                                {
                                    vswr -= 1.4;
                                    vswr *= 7;
                                    vswr += 3.5;
                                }
                                else if (vswr <= 2.0)
                                {
                                    vswr -= 1.5;
                                    vswr *= 6;
                                    vswr += 4.2;
                                }
                                else if (vswr <= 3.0)
                                {
                                    vswr -= 2.0;
                                    vswr *= 2.5;
                                    vswr += 7.1;
                                }
                                else if (vswr <= 5.0)
                                {
                                    vswr -= 3.0;
                                    vswr *= 1.2;
                                    vswr += 9.6;
                                }
                                else if (vswr <= 50.0)
                                {
                                    vswr -= 5;
                                    vswr *= .1;
                                    vswr += 12;
                                }
                                else
                                {
                                    vswr -= 9;
                                    vswr *= .3;
                                    vswr += 15;
                                }

                                SMeter.Value = (float)vswr;
                                Debug.Write(vswr.ToString() + "\n");
                                break;
                        }


                        if (video_driver == DisplayDriver.DIRECTX)
                        {
#if(DirectX)
                            SMeter.RenderGauge();
#endif
                        }
                        else
                            SMeter.Invalidate();
                    }
                    else
                    {
                        power = 0.0f;
                        pwr_avg = 0.0f;

                        if (Audio.channel == 5)
                            num = CalculateRXMeter(0, 0, MeterType.SIGNAL_STRENGTH); // average signal strength
                        else
                        {
                            if (op_mode_vfoB == Mode.RTTY)
                                num = CalculateRXMeter(0, 2, MeterType.SIGNAL_STRENGTH);
                            else
                                num = CalculateRXMeter(0, 1, MeterType.SIGNAL_STRENGTH);
                        }

                        if (float.IsNaN(num) || float.IsInfinity(num))
                            num = 0.0f;

                        //num = Smeter_avg_mult_old * Smeter_avg + Smeter_avg_mult_new * num;
                        num = 0.85f * Smeter_avg + 0.15f * num;
                        Smeter_avg = num;
                        num += Smeter_cal_offset;
                        main_sql_data = num;
                        picSQL.Invalidate();

                        num_tmp = num + 127.0f;

                        if (num <= -73.0f && num >= -127.0f)
                        {
                            num_tmp = num_tmp / 6;
                        }
                        else if (num > -73.0f && num <= -13.0f)
                        {
                            num_tmp = -(-73.0f - num) / 6.6f + 9;
                        }
                        else if (num > -13.0f)
                        {
                            num_tmp = SMeter.MaxValue;
                        }
                        else
                            num_tmp = 0;

                        SMeter.Value = num_tmp;

                        if (video_driver == DisplayDriver.DIRECTX)
                        {
#if(DirectX)
                            try
                            {
                                SMeter.RenderGauge();
                            }
                            catch (Exception ex)
                            {
                                Debug.Write(ex.ToString());
                            }
#endif
                        }
                        else
                            SMeter.Invalidate();
                    }

                    Thread.Sleep(30);
                }

                while (SMeter.Value > 0)
                {
                    num -= 1f;
                    num = 0.75f * Smeter_avg + 0.25f * num;
                    Smeter_avg = num;
                    num += Smeter_cal_offset;
                    num_tmp = num + 127.0f;

                    if (num <= -73.0f && num >= -127.0f)
                    {
                        num_tmp = num_tmp / 6;
                    }
                    else if (num > -73.0f && num <= -13.0f)
                    {
                        num_tmp = -(-73.0f - num) / 6.6f + 9;
                    }
                    else if (num > -13.0f)
                    {
                        num_tmp = SMeter.MaxValue;
                    }
                    else
                        num_tmp = 0;

                    SMeter.Value = num_tmp;

                    if (video_driver == DisplayDriver.DIRECTX)
                    {
#if(DirectX)
                        SMeter.RenderGauge();
#endif
                    }
                    else
                        SMeter.Invalidate();

                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private float PAPower(int adc)
        {
            double v_out = adc;
            v_out *= 43.211;
            double pow = Math.Pow(v_out, 2) / 50;
            pow = Math.Max(pow, 0.0);
            pow /= 1000000.0f;
            return (float)pow;
        }

        private double SWR(int adc_fwd, int adc_rev)
        {
            adc_rev = Math.Max(adc_rev, 1);
            adc_fwd = Math.Max(adc_fwd, adc_rev);

            if (adc_rev == adc_fwd)
                return 1.0;

            if (adc_fwd == 0 && adc_rev == 0)
                return 1.0;
            else if (adc_rev > adc_fwd)
            {
                //HighSWR = true;
                return 50.0;
            }

            //double swr = (adc_fwd + adc_rev) / (adc_fwd - adc_rev);

            double Ef = ScaledVoltage(adc_fwd);
            double Er = ScaledVoltage(adc_rev);

            double swr = (Ef + Er) / (Ef - Er);

            /*if (swr > 3.0)
                HighSWR = true;
            else
                HighSWR = false;*/

            return Math.Min(150.0, swr);
        }

        private double ScaledVoltage(int adc)
        {
            double v_det = adc * 0.062963;			// scale factor in V/bit including pot ratio
            double v_out = v_det * 10.39853;		// scale factor in V/V for bridge output to detector voltage
            return v_out * 10 / 55;
        }

        #endregion

        #region Zoom&Pan

        private void tbPan_Scroll(object sender, EventArgs e)
        {
            CalcDisplayFreq();
        }

        private void tbPan_MouseHover(object sender, EventArgs e)
        {
            tbPan.Focus();
            lblPan.ForeColor = Color.Yellow;
        }

        private void tbZoom_MouseHover(object sender, EventArgs e)
        {
            tbZoom.Focus();
            lblZoom.ForeColor = Color.Yellow;
        }

        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            double zoom_factor = tbZoom.Value / 4;

            tbPan.Value = 0;
            CalcDisplayFreq();
        }

        private void tbPan_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblPan.ForeColor = Color.White;
        }

        private void tbZoom_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblZoom.ForeColor = Color.White;
        }

        public void CalcDisplayFreq()
        {
            double low, high, low_tmp, high_tmp;
            double zoom_factor = tbZoom.Value / 4;
            double pan_factor = tbPan.Value;
            double vfo = 0.0;
            int abs_low = (int)(-Audio.SampleRate / 2);
            int abs_high = -abs_low;
            double correction = 2 * (pan_factor * abs_high) / tbPan.Maximum;

            if (Audio.SDRmode)
            {
                if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                    vfo = -Math.Round(((losc - vfoa)) * 1e6, 6);
                else
                    vfo = -Math.Round(((losc - vfob)) * 1e6, 6);

                low = (int)(-Audio.SampleRate / (zoom_factor * 2));
                high = (int)(Audio.SampleRate / (zoom_factor * 2));

                if (tbZoom.Value == 4)
                {
                    tbPan.Value = 0;
                    low = (int)(-Audio.SampleRate / 2.0);
                    high = -low;
                }
                else
                {
                    if (losc < vfoa)
                    {
                        high_tmp = ((int)vfo + high + (int)correction);

                        if (high_tmp > abs_high)
                        {
                            high = abs_high;
                            low = (abs_high + low * 2.0);
                        }
                        else
                        {
                            low = (high_tmp + low * 2.0);
                            if (low < abs_low)
                            {
                                low = abs_low;
                                high = low + high * 2.0;
                            }
                            else
                                high = high_tmp;
                        }
                    }
                    else
                    {
                        if (losc > vfoa)
                        {
                            low_tmp = ((int)vfo + low + (int)correction);
                            if (low_tmp < abs_low)
                            {
                                low = abs_low;
                                high = abs_low + high * 2.0;
                            }
                            else
                            {
                                high = (low_tmp + high * 2.0);
                                if (high > abs_high)
                                {
                                    high = abs_high;
                                    low = abs_high + low * 2.0;
                                }
                                else
                                    low = low_tmp;
                            }
                        }
                    }
                }

#if(DirectX)
                DX.RXDisplayLow = (int)low;
                DX.RXDisplayHigh = (int)high;
#endif
                Display_GDI.RXDisplayLow = (int)low;
                Display_GDI.RXDisplayHigh = (int)high;
            }
            else
            {
                low = 0;
                high = (int)(Audio.SampleRate / (zoom_factor * 2));
#if(DirectX)
                DX.RXDisplayLow = (int)low;
                DX.RXDisplayHigh = (int)high;
#endif
                Display_GDI.RXDisplayLow = (int)low;
                Display_GDI.RXDisplayHigh = (int)high;
            }
        }

        private void lblZoom_Click(object sender, EventArgs e)
        {
            tbZoom.Value = 4;
            tbZoom_Scroll(null, null);
        }

        private void lblPan_Click(object sender, EventArgs e)
        {
            tbPan.Value = 0;
            tbPan_Scroll(null, null);
        }

        #endregion}

        #region WBIR

        public WBIR_State WBIR_state = WBIR_State.FastAdapt;
        private bool wbir_tuned = true;
        private bool wbir_delay_adapt = false;
        private bool wbir_run = false;

        private void WBIR_thread()
        {
            if (btnStartMR.Checked)
            {
                int fast_count = 0;
                int countdown = 1000;
                //                double rnd = 0.0;

                while (wbir_run)
                {
                    switch (WBIR_state)
                    {
                        case WBIR_State.FastAdapt:
                            if (MOX)
                            {
                                SetCorrectIQMu(0, 0, 0.0);
                                SetCorrectIQMu(0, 1, 0.0);
                                WBIR_state = WBIR_State.MOXAdapt;
                            }
                            else
                            {
                                if (wbir_delay_adapt)
                                {
                                    WBIR_state = WBIR_State.DelayAdapt;
                                    wbir_delay_adapt = false;
                                }
                                else if (wbir_tuned)
                                {
                                    wbir_tuned = false;
                                    fast_count = 0;
                                }

                                SetCorrectIQMu(0, 0, 0.5 - (fast_count * 0.005));
                                SetCorrectIQMu(0, 1, 0.5 - (fast_count * 0.005));

                                //Debug.WriteLine(" WBIR Fast, Mu: " + (0.05 - (fast_count * 0.005)).ToString("f6"));
                                fast_count++;
                                if (fast_count == 10)
                                {
                                    fast_count = 0;
                                    countdown = 1000;
                                    WBIR_state = WBIR_State.SlowAdapt;
                                }
                            }
                            break;
                        case WBIR_State.SlowAdapt:
                            if (MOX)
                            {
                                SetCorrectIQMu(0, 0, 0.0);    // reset wbir
                                SetCorrectIQMu(0, 1, 0.0);
                                WBIR_state = WBIR_State.MOXAdapt;
                            }
                            else
                            {
                                countdown -= 100;
                                //Debug.WriteLine("WBIR Slow, countdown: "+countdown);
                                if (wbir_delay_adapt)
                                {
                                    WBIR_state = WBIR_State.DelayAdapt;
                                    wbir_delay_adapt = false;
                                }
                                else if (countdown <= 0 || wbir_tuned)
                                {
                                    wbir_tuned = false;
                                    WBIR_state = WBIR_State.FastAdapt;
                                }
                            }
                            break;
                        case WBIR_State.NoAdapt:
                            //Debug.WriteLine("WBIR Off");
                            if (!MOX)
                            {
                                SetCorrectIQEnable(0, 0);
                                SetCorrectRXIQw(0, 0, 0, 0, 0);
                                SetCorrectRXIQw(0, 1, 0, 0, 0);
                                SetCorrectRXIQw(0, 0, 0, 0, 1);
                                SetCorrectRXIQw(0, 1, 0, 0, 1);
                                SetCorrectIQEnable(0, 1);
                                wbir_tuned = true;
                                countdown = 1000;
                                WBIR_state = WBIR_State.FastAdapt;
                            }
                            break;
                        case WBIR_State.StopAdapt:
                            {
                                SetCorrectIQEnable(0, 0);
                                SetCorrectRXIQw(0, 0, 0, 0, 0);
                                SetCorrectRXIQw(0, 1, 0, 0, 0);
                                SetCorrectRXIQw(0, 0, 0, 0, 1);
                                SetCorrectRXIQw(0, 1, 0, 0, 1);
                            }
                            break;
                        case WBIR_State.DelayAdapt:
                            {
                                //Thread.Sleep((int)SetupForm.udWBIRTime.Value);
                                WBIR_state = WBIR_State.NoAdapt;
                            }
                            break;
                        case WBIR_State.MOXAdapt:
                            if (!MOX)
                            {
                                Thread.Sleep(100);
                                WBIR_state = WBIR_State.DelayAdapt;
                            }
                            break;
                    }

                    Thread.Sleep(100);
                }
            }
        }

        #endregion

        #region SQL

        private void picSQL_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                int signal_x = (int)((main_sql_data + 155.0) * (picSQL.Width - 1) / 142.0);
                int sql_x = (int)((tbSQL.Value + 155.0) * (picSQL.Width - 1) / 142.0);

                if (signal_x > 80)
                    signal_x = 80;

                e.Graphics.FillRectangle(new SolidBrush(Color.LimeGreen), 0, 0, signal_x, picSQL.Height);

                if (sql_x < signal_x)
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), sql_x + 1, 0, signal_x - sql_x - 1, picSQL.Height);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbSQL_MouseHover(object sender, EventArgs e)
        {
            tbSQL.Focus();
            lblSQL.ForeColor = Color.Yellow;
        }

        private void tbSQL_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
            lblSQL.ForeColor = Color.White;
        }

        private void tbSQL_Scroll(object sender, EventArgs e)
        {
            switch (op_mode_vfoA)
            {
                case Mode.CW:
                    {
                        if (cwDecoder != null)
                            cwDecoder.SQL = (double)(tbSQL.Value / 20.0);
                    }
                    break;

                case Mode.RTTY:
                    {
                        if (rtty != null)
                            rtty.sql = (float)(tbSQL.Value / 20.0);
                    }
                    break;

                case Mode.BPSK31:
                case Mode.BPSK63:
                case Mode.BPSK125:
                case Mode.BPSK250:
                case Mode.QPSK31:
                case Mode.QPSK63:
                case Mode.QPSK125:
                case Mode.QPSK250:
                    {
                        if (psk != null)
                            psk.sql = (float)(tbSQL.Value / 20.0);
                    }
                    break;
            }
        }

        private float main_sql_data = -200.0f;

        #endregion

        #region MOX

        private void btnTX_Click(object sender, EventArgs e)
        {
            if (btnStartMR.Checked)
            {
                OutputPowerUpdate();

                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        {
                            if (sender != null)
                            {
                                cw_message_repeat = false;
                                cw_message_event.Set();
                            }

                            if (!MOX)
                                cwEncoder.Start(txtCall.Text);
                            else
                            {
                                cwEncoder.Stop();
                                tune = false;
                                btnTune.BackColor = Color.WhiteSmoke;
                            }
                        }
                        break;

                    case Mode.RTTY:
                        {
                            if (sender != null)
                            {
                                rtty_message_repeat = false;
                                rtty_message_event.Set();
                            }

                            if (!MOX)
                                rtty.Start(txtCall.Text);
                            else
                            {
                                rtty.Stop();
                                tune = false;
                                btnTune.BackColor = Color.WhiteSmoke;
                            }
                        }
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        {
                            if (sender != null)
                            {
                                psk_message_repeat = false;
                                psk_message_event.Set();
                            }

                            if (!MOX)
                                psk.Start(txtCall.Text);
                            else
                            {
                                psk.Stop();
                                tune = false;
                                btnTune.BackColor = Color.WhiteSmoke;
                            }
                        }
                        break;
                }
            }
        }

        private void btnTune_Click(object sender, EventArgs e)
        {
            TUNE = !tune;

            if (tune)
                btnTX_Click(this, EventArgs.Empty);
        }

        #endregion

        #region crossthread

        public void CommandCallback(string action, int param_1, string param_2)
        {
            try
            {
                switch (action)
                {
                    case "CW monitor":
                        if (!btnMute.Checked)
                            genesis.WriteToDevice(24, param_1);
                        else
                            genesis.WriteToDevice(24, 0);
                        break;

                    case "VFO":
                        double freq = double.Parse(param_2);

                        switch (param_1)
                        {
                            case 1:                                     // VFOA
                                //if (freq > psk_vfoa_min && freq < psk_vfoa_max)
                                {
                                    tune_psk = true;
                                    VFOA += freq;
                                    tune_psk = false;
                                }
                                break;

                            case 2:                                     // VFOB
                                //if (freq > psk_vfob_min && freq < psk_vfob_max)
                                {
                                    tune_psk = true;
                                    VFOB += freq;
                                    tune_psk = false;
                                }
                                break;
                        }
                        break;
                    case "MOX":
                        if (Audio.SDRmode)
                        {
                            if (param_1 == 0)
                                MOX = false;
                            else
                                MOX = true;
                        }
                        break;
                    case "Set text":
                        WriteOutputText(param_1, param_2);
                        break;
                    case "Append text":
                        {
                            if (param_1 <= 20 && param_1 >= 2)
                            {
                                switch (param_1)
                                {
                                    case 2:
                                        txtChannel2.AppendText(param_2);
                                        break;
                                    case 3:
                                        txtChannel3.AppendText(param_2);
                                        break;
                                    case 4:
                                        txtChannel4.AppendText(param_2);
                                        break;
                                    case 5:
                                        txtChannel5.AppendText(param_2);
                                        break;
                                    case 6:
                                        txtChannel6.AppendText(param_2);
                                        break;
                                    case 7:
                                        txtChannel7.AppendText(param_2);
                                        break;
                                    case 8:
                                        txtChannel8.AppendText(param_2);
                                        break;
                                    case 9:
                                        txtChannel9.AppendText(param_2);
                                        break;
                                    case 10:
                                        txtChannel10.AppendText(param_2);
                                        break;
                                    case 11:
                                        txtChannel11.AppendText(param_2);
                                        break;
                                    case 12:
                                        txtChannel12.AppendText(param_2);
                                        break;
                                    case 13:
                                        txtChannel13.AppendText(param_2);
                                        break;
                                    case 14:
                                        txtChannel14.AppendText(param_2);
                                        break;
                                    case 15:
                                        txtChannel15.AppendText(param_2);
                                        break;
                                    case 16:
                                        txtChannel16.AppendText(param_2);
                                        break;
                                    case 17:
                                        txtChannel17.AppendText(param_2);
                                        break;
                                    case 18:
                                        txtChannel18.AppendText(param_2);
                                        break;
                                    case 19:
                                        txtChannel19.AppendText(param_2);
                                        break;
                                    case 20:
                                        txtChannel20.AppendText(param_2);
                                        break;
                                }
                            }
                        }
                        break;
                    case "Set TX text":
                        if (param_1 == 0)
                        {
                            Color oldColor = rtbCH1.SelectionColor;
                            rtbCH1.SelectionColor = Color.HotPink;
                            WriteOutputText(5, param_2);
                            rtbCH1.SelectionColor = oldColor;
                        }
                        else if (param_1 == 1)
                        {
                            Color oldColor = rtbCH2.SelectionColor;
                            rtbCH2.SelectionColor = Color.HotPink;
                            WriteOutputText(6, param_2);
                            rtbCH2.SelectionColor = oldColor;
                        }
                        break;
                    case "Clear text":
                        txtChannelClear(0);
                        break;
                    case "Message repeat":
                        Mode mode = OpModeVFOA;

                        if (tx_split)
                            mode = OpModeVFOB;

                        switch (mode)
                        {
                            case Mode.CW:
                                {
                                    cw_message_repeat = true;

                                    if (cw_message_thread == null || !cw_message_thread.IsAlive)
                                    {
                                        cw_message_thread = new Thread(new ThreadStart(CW_Message_thread));
                                        cw_message_thread.Name = "CW Message Thread";
                                        cw_message_thread.Priority = ThreadPriority.Lowest;
                                        cw_message_thread.IsBackground = true;
                                        cw_message_thread.Start();
                                    }

                                    cw_message_event.Set();
                                }
                                break;

                            case Mode.RTTY:
                                {
                                    rtty_message_repeat = true;

                                    if (rtty_message_thread == null || !rtty_message_thread.IsAlive)
                                    {
                                        rtty_message_thread = new Thread(new ThreadStart(RTTY_Message_thread));
                                        rtty_message_thread.Name = "RTTY Message Thread";
                                        rtty_message_thread.Priority = ThreadPriority.Lowest;
                                        rtty_message_thread.IsBackground = true;
                                        rtty_message_thread.Start();
                                    }

                                    rtty_message_event.Set();
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                {
                                    psk_message_repeat = true;

                                    if (psk_message_thread == null || !psk_message_thread.IsAlive)
                                    {
                                        psk_message_thread = new Thread(new ThreadStart(PSK_Message_thread));
                                        psk_message_thread.Name = "PSK Message Thread";
                                        psk_message_thread.Priority = ThreadPriority.Lowest;
                                        psk_message_thread.IsBackground = true;
                                        psk_message_thread.Start();
                                    }

                                    psk_message_event.Set();
                                }
                                break;
                        }
                        break;
                    case "Message end":
                        cw_message_repeat = false;
                        cw_message_event.Set();
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void CrossThreadCommand(string action, string data)
        {
            try
            {
                if (!Audio.SDRmode)
                {
                    switch (action)
                    {
                        case "TX ON":
                            MOX = true;
                            break;

                        case "TX OFF":
                            MOX = false;
                            break;

                        case "DXCC text":
                            if (dxcc != null && dxcc.Visible)
                            {
                                dxcc.rtbDXCC.AppendText(data);
                            }
                            break;

                        case "Reinit DirectX":
#if(DirectX)
                            if (this.WindowState != FormWindowState.Minimized)
                            {
                                DX.DirectXRelease();
                                Thread.Sleep(100);
                                DX.DirectXInit();
                            }
#endif
                            break;

                        case "Send CALL":
                            {
                                txtCALL = data;
                                txtLogCall.Text = data;
                                txtLogRST.Text = "599";
                                txtLogMyRST.Text = "599";
                                EnsureMREditWindows();

                                if (topWindow != 0)
                                {
                                    msg.sendWindowsStringMessage(edits[0].hWnd, 0, data);
                                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_RETURN, 1 + (13 << 16));
                                }
                            }
                            break;

                        case "Send RST":
                            {
                                txtRst = data;
                                txtLogMyRST.Text = data;
                                btnSendRST_Click(null, null);
                            }
                            break;

                        case "Send NR":
                            {
                                txtNR = data;
                                txtLOGNR.Text = data;
                                EnsureMREditWindows();

                                if (topWindow != 0)
                                {
                                    msg.sendWindowsStringMessage(edits[2].hWnd, 0, data);
                                    msg.sendWindowsMessage(edits[2].hWnd, WM_KEYDOWN, VK_RETURN, 1 + (13 << 16));
                                }
                            }
                            break;

                        case "Send Exch":
                            btnF5_Click(this, EventArgs.Empty);     // hiss call
                            btnF2_Click(this, EventArgs.Empty);     // raport
                            Debug.Write("Send Exchange 1 \n");
                            break;

                        case "Add LOG entry":
                            {
                                AddLOGEntry();
                                btnLogClear_Click(this, EventArgs.Empty);
                                EnsureMREditWindows();

                                /*if (topWindow != 0)
                                {
                                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYDOWN, VK_F3, (1 + (61 << 16)));
                                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15616, VK_F3, (1 + (61 << 16)));
                                    msg.sendWindowsMessage(edits[0].hWnd, WM_KEYUP, VK_F3, (1 + (61 << 16) + (3 << 30)));
                                    msg.sendWindowsMessage(edits[0].hWnd, WM_APP + 15617, VK_F3, (1 + (61 << 16) + (3 << 30)));

                                    Debug.Write("Send TU 1 \n");
                                }*/
                            }
                            break;

                        case "Clear LOG":
                            btnLogClear_Click(this, EventArgs.Empty);
                            break;

                        case "Send F1":
                            {
                                btnF1_Click(null, null);
                            }
                            break;

                        case "Send F2":
                            {
                                btnF2_Click(null, null);
                            }
                            break;

                        case "Send F3":
                            {
                                btnF3_Click(null, null);
                            }
                            break;
                        case "Send F5":
                            {
                                btnF5_Click(null, null);
                            }
                            break;
                        case "Send F7":
                            {
                                btnF7_Click(null, null);
                            }
                            break;
                    }
                }
                else
                {
                    switch (action)
                    {
                        case "Reinit DirectX":
#if(DirectX)
                            if (this.WindowState != FormWindowState.Minimized)
                            {
                                DX.DirectXRelease();
                                DX.DirectXInit();
                            }
#endif
                            break;
                        case "UpdateOutputPower":
                            OutputPowerUpdate();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex + "\n\n" + ex.StackTrace.ToString());
            }
        }

        #endregion

        #region CW message thread

        private void CW_Message_thread()
        {
            while (cw_message_repeat)
            {
                cw_message_event.WaitOne();

                if (cw_message_repeat)
                    Thread.Sleep(msg_rpt_time);

                if (cw_message_repeat)
                {
                    if (keyboard.Visible)
                        this.Invoke(new KeyboardThreadCommand(keyboard.CommandCallback),
                                "Send Keyboard text", 0, "");
                }
            }
        }

        #endregion

        #region RTTY message thread

        private void RTTY_Message_thread()
        {
            while (rtty_message_repeat)
            {
                rtty_message_event.WaitOne();

                if (rtty_message_repeat)
                    Thread.Sleep(msg_rpt_time);

                if (rtty_message_repeat)
                {
                    if (keyboard.Visible)
                        this.Invoke(new KeyboardThreadCommand(keyboard.CommandCallback),
                                "Send Keyboard text", 0, "");
                }
            }
        }

        #endregion

        #region PSK message thread

        private void PSK_Message_thread()
        {
            while (psk_message_repeat)
            {
                psk_message_event.WaitOne();

                if (psk_message_repeat)
                    Thread.Sleep(msg_rpt_time);

                if (psk_message_repeat)
                {
                    if (keyboard.Visible)
                        this.Invoke(new KeyboardThreadCommand(keyboard.CommandCallback),
                                "Send Keyboard text", 0, "");
                }
            }
        }

        #endregion

        #region text box buttons

        private void btnClearCH1_Click(object sender, EventArgs e)
        {
            if (op_mode_vfoA == Mode.CW && cwDecoder.output != null)
                cwDecoder.output[5] = "";
            else if (op_mode_vfoA == Mode.RTTY && rtty.output_ch1 != null)
            {
                rtty.output_ch1 = "";
                rtty.index_ch1 = 0;
            }

            rtbCH1.Clear();
            SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 1);
            rtbCH1.Refresh();
        }

        private void btnCH1_Click(object sender, EventArgs e)
        {
            Audio.channel = 5;
            btnCH1.BackColor = Color.LimeGreen;
            btnCH2.BackColor = Color.WhiteSmoke;
        }

        private void btnCH2_Click(object sender, EventArgs e)
        {
            if (btnRX2On.BackColor == Color.LimeGreen && btnCH2.BackColor == Color.WhiteSmoke)
            {
                Audio.channel = 6;
                btnCH2.BackColor = Color.LimeGreen;
                btnCH1.BackColor = Color.WhiteSmoke;
            }
            else if (btnRX2On.BackColor == Color.LimeGreen && btnCH2.BackColor == Color.LimeGreen)
            {
                Audio.channel = 5;
                btnCH1.BackColor = Color.LimeGreen;
                btnCH2.BackColor = Color.WhiteSmoke;
            }
        }

        private void btnClearCh2_Click(object sender, EventArgs e)
        {
            if (op_mode_vfoA == Mode.RTTY && cwDecoder.output != null)
                cwDecoder.output[6] = "";
            else if (op_mode_vfoA == Mode.RTTY && rtty.output_ch2 != null)
            {
                rtty.output_ch2 = "";
                rtty.index_ch2 = 0;
            }

            rtbCH2.Clear();
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 1);
            rtbCH2.Refresh();
        }

        private void btnRX2On_CheckedChanged(object sender, EventArgs e)
        {
            if (btnRX2On.Checked)
            {
                btnRX2On.BackColor = Color.WhiteSmoke;
                Audio.RX2 = true;
                Display_GDI.RX2Enabled = true;
#if(DirectX)
                DX.RX2Enabled = true;
#endif
                btnRX2On.BackColor = Color.LimeGreen;

                switch (op_mode_vfoB)
                {
                    case Mode.CW:
                        cwDecoder.RX2Enabled = true;
                        cwDecoder.run_rx2 = true;
                        SetRXOn(0, 1, true);
                        rtty.run_rx2 = false;
                        psk.run_rx2 = false;

                        switch (op_mode_vfoA)
                        {
                            case Mode.CW:
                                SetRXOn(0, 2, false);
                                SetRXOn(0, 3, false);
                                break;

                            case Mode.RTTY:
                                SetRXOn(0, 2, true);
                                SetRXOn(0, 3, false);
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                SetRXOn(0, 2, false);
                                SetRXOn(0, 3, false);
                                break;
                        }
                        break;

                    case Mode.RTTY:
                        rtty.RX2Enabled = true;
                        rtty.run_rx2 = true;
                        SetRXOn(0, 2, true);
                        SetRXOn(0, 3, true);
                        cwDecoder.run_rx2 = false;
                        psk.run_rx2 = false;

                        switch (op_mode_vfoA)
                        {
                            case Mode.RTTY:
                                break;

                            case Mode.CW:
                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                SetRXOn(0, 1, false);
                                break;
                        }
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.RX2Enabled = true;
                        psk.run_rx2 = true;
                        SetRXOn(0, 1, true);
                        cwDecoder.run_rx2 = false;
                        rtty.run_rx2 = false;

                        switch (op_mode_vfoA)
                        {
                            case Mode.RTTY:
                                SetRXOn(0, 2, true);
                                SetRXOn(0, 3, false);
                                break;

                            case Mode.CW:
                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                SetRXOn(0, 2, false);
                                SetRXOn(0, 3, false);
                                break;
                        }

                        break;
                }
            }
            else
            {
                btnRX2On.BackColor = Color.LimeGreen;
                Audio.RX2 = false;
                Display_GDI.RX2Enabled = false;
#if(DirectX)
                DX.RX2Enabled = false;
#endif
                btnRX2On.BackColor = Color.WhiteSmoke;
                Audio.channel = 5;
                btnCH1.BackColor = Color.LimeGreen;
                btnCH2.BackColor = Color.WhiteSmoke;

                switch (op_mode_vfoA)
                {
                    case Mode.CW:
                        cwDecoder.RX2Enabled = false;
                        SetRXOn(0, 1, false);
                        SetRXOn(0, 2, false);
                        SetRXOn(0, 3, false);
                        cwDecoder.run_rx2 = false;
                        break;

                    case Mode.RTTY:
                        rtty.RX2Enabled = false;
                        SetRXOn(0, 2, false);
                        SetRXOn(0, 3, false);
                        rtty.run_rx2 = false;
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.RX2Enabled = false;
                        psk.run_rx2 = false;
                        SetRXOn(0, 1, false);
                        SetRXOn(0, 2, false);
                        SetRXOn(0, 3, false);
                        break;
                }

                if (tuning_mode == TuneMode.VFOB)
                {
                    tuning_mode = TuneMode.Off;
                    Display_GDI.tuning_mode = TuneMode.Off;
#if(DirectX)
                    DX.tuning_mode = TuneMode.Off;
#endif
                }
            }

            Mode new_mode = op_mode_vfoA;

            if (tuning_mode == TuneMode.VFOB)
                new_mode = op_mode_vfoB;

            switch (new_mode)
            {
                case Mode.CW:
                    lblRTTYMark.Visible = false;
                    lblRTTYMarkBox.Visible = false;
                    lblRTTYSpace.Visible = false;
                    lblRTTYSpaceBox.Visible = false;
                    radRTTYNormal.Visible = false;
                    radRTTYReverse.Visible = false;
                    lblDCDCh1.Visible = false;
                    lblDCDCh2.Visible = false;
                    lblPSKCH1Freq.Visible = false;
                    lblPSKCH2Freq.Visible = false;
                    lblPSKDCDCh1.Visible = false;
                    lblPSKDCDCh2.Visible = false;
                    chkSQL.Visible = true;
                    chkAFC.Visible = false;
                    break;

                case Mode.RTTY:
                    lblRTTYMark.Visible = true;
                    lblRTTYMarkBox.Visible = true;
                    lblRTTYSpace.Visible = true;
                    lblRTTYSpaceBox.Visible = true;
                    radRTTYNormal.Visible = true;
                    radRTTYReverse.Visible = true;
                    lblDCDCh1.Visible = false;
                    lblDCDCh2.Visible = false;
                    lblPSKCH1Freq.Visible = false;
                    lblPSKCH2Freq.Visible = false;
                    lblPSKDCDCh1.Visible = false;
                    lblPSKDCDCh2.Visible = false;
                    chkSQL.Visible = true;
                    chkAFC.Visible = false;
                    break;

                case Mode.BPSK31:
                case Mode.BPSK63:
                case Mode.BPSK125:
                case Mode.BPSK250:
                case Mode.QPSK31:
                case Mode.QPSK63:
                case Mode.QPSK125:
                case Mode.QPSK250:
                    lblRTTYMark.Visible = false;
                    lblRTTYMarkBox.Visible = false;
                    lblRTTYSpace.Visible = false;
                    lblRTTYSpaceBox.Visible = false;
                    radRTTYNormal.Visible = false;
                    radRTTYReverse.Visible = false;
                    lblDCDCh1.Visible = true;
                    lblDCDCh2.Visible = true;
                    lblPSKCH1Freq.Visible = true;
                    lblPSKCH2Freq.Visible = true;
                    lblPSKDCDCh1.Visible = true;
                    lblPSKDCDCh2.Visible = true;
                    chkSQL.Visible = false;
                    chkAFC.Visible = true;
                    break;
            }
        }

        #endregion

        #region Operational mode selection

        private void cWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;

            if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
            {
                chkSQL.Visible = true;
                chkAFC.Visible = false;
                rTTYToolStripMenuItem.Checked = false;
                cWToolStripMenuItem.Checked = true;
                bPSK250ToolStripMenuItem.Checked = false;
                bPSK125ToolStripMenuItem.Checked = false;
                bPSK63ToolStripMenuItem.Checked = false;
                bPSK31ToolStripMenuItem.Checked = false;
                qPSK31ToolStripMenuItem.Checked = false;
                qPSK63ToolStripMenuItem.Checked = false;
                qPSK125ToolStripMenuItem.Checked = false;
                qPSK250ToolStripMenuItem.Checked = false;
                tuning_mode = TuneMode.Off;
                OpModeVFOA = Mode.CW;
                cwDecoder.run_rx1 = true;
                rtty.run_rx1 = false;
                psk.run_rx1 = false;
                InitLOG();
            }
            else
            {
                OpModeVFOB = Mode.CW;
                cwDecoder.run_rx2 = true;
                rtty.run_rx2 = false;
                psk.run_rx2 = false;
            }

            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode CW";
            Audio.audio_paused = false;
        }

        public void rTTYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;

            if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
            {
                OpModeVFOA = Mode.RTTY;
                rtty.update_trx1 = true;
                SetRXOn(0, 1, true);
                cwDecoder.run_rx1 = false;
                rtty.run_rx1 = true;
                psk.run_rx1 = false;
                chkSQL.Visible = true;
                chkAFC.Visible = false;
                rTTYToolStripMenuItem.Checked = true;
                cWToolStripMenuItem.Checked = false;
                bPSK250ToolStripMenuItem.Checked = false;
                bPSK125ToolStripMenuItem.Checked = false;
                bPSK63ToolStripMenuItem.Checked = false;
                bPSK31ToolStripMenuItem.Checked = false;
                qPSK31ToolStripMenuItem.Checked = false;
                qPSK63ToolStripMenuItem.Checked = false;
                qPSK125ToolStripMenuItem.Checked = false;
                qPSK250ToolStripMenuItem.Checked = false;
                cwDecoder.run_rx1 = false;
                rtty.run_rx1 = true;
                psk.run_rx1 = false;
                InitLOG();
            }
            else
            {
                OpModeVFOB = Mode.RTTY;
                rtty.update_trx2 = true;
                cwDecoder.run_rx2 = false;
                rtty.run_rx2 = true;
                psk.run_rx2 = false;
            }

            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            Audio.audio_paused = false;
            //mmm warning            grpMonitor.Text = "Mode RTTY" + "  " + SetupForm.baudrate.ToString() + "baud";
            VFOA = vfoa;
            VFOB = vfob;
        }

        private void bPSK31ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = true;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.BPSK31;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.BPSK31;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false; ;
        }

        private void bPSK63ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = true;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.BPSK63;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.BPSK63;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        private void bPSK125ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = true;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.BPSK125;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.BPSK125;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        private void bPSK250ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK250ToolStripMenuItem.Checked = true;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.BPSK250;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.BPSK250;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        private void qPSK31ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = true;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.QPSK31;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.QPSK31;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        private void qPSK63ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = true;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.QPSK63;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.QPSK63;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        private void qPSK125ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = true;
            qPSK250ToolStripMenuItem.Checked = false;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.QPSK125;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.QPSK125;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        private void qPSK250ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Audio.audio_paused = true;
            chkSQL.Visible = false;
            chkAFC.Visible = true;
            rTTYToolStripMenuItem.Checked = false;
            cWToolStripMenuItem.Checked = false;
            bPSK250ToolStripMenuItem.Checked = false;
            bPSK125ToolStripMenuItem.Checked = false;
            bPSK63ToolStripMenuItem.Checked = false;
            bPSK31ToolStripMenuItem.Checked = false;
            qPSK31ToolStripMenuItem.Checked = false;
            qPSK63ToolStripMenuItem.Checked = false;
            qPSK125ToolStripMenuItem.Checked = false;
            qPSK250ToolStripMenuItem.Checked = true;

            if (psk != null)
            {
                if (tuning_mode == TuneMode.VFOA || tuning_mode == TuneMode.Off)
                {
                    OpModeVFOA = Mode.QPSK250;
                    psk.update_trx1 = true;
                    cwDecoder.run_rx1 = false;
                    rtty.run_rx1 = false;
                    psk.run_rx1 = true;
                }
                else if (tuning_mode == TuneMode.VFOB)
                {
                    OpModeVFOB = Mode.QPSK250;
                    psk.update_trx2 = true;
                    cwDecoder.run_rx2 = false;
                    rtty.run_rx2 = false;
                    psk.run_rx2 = true;
                }
            }

            InitLOG();
            btnRX2On_CheckedChanged(this, EventArgs.Empty);
            this.grpChannels.Text = "VFOA " + op_mode_vfoA.ToString() + "     VFOB " +
                op_mode_vfoB.ToString() + "     ";
            grpMonitor.Text = "Mode PSK";
            Audio.audio_paused = false;
        }

        #endregion

        #region databse save/restore

        public void SaveState()
        {
            try
            {
                ArrayList a = new ArrayList();
                ArrayList temp = new ArrayList();
                string s;

                ControlList(this, ref temp);

                foreach (Control c in temp)				// For each control
                {
                    if (c.GetType() == typeof(CheckBox))
                        a.Add(c.Name + "/" + ((CheckBox)c).Checked.ToString());
                    else if (c.GetType() == typeof(ComboBox))
                    {
                        a.Add(c.Name + "/" + ((ComboBox)c).Text);
                    }
                    else if (c.GetType() == typeof(NumericUpDown))
                        a.Add(c.Name + "/" + ((NumericUpDown)c).Value.ToString());
                    else if (c.GetType() == typeof(RadioButton))
                        a.Add(c.Name + "/" + ((RadioButton)c).Checked.ToString());
                    //else if (c.GetType() == typeof(TextBox))
                    //a.Add(c.Name + "/" + ((TextBox)c).Text);
                    else if (c.GetType() == typeof(TrackBar))
                        a.Add(c.Name + "/" + ((TrackBar)c).Value.ToString());
                }

                a.Add("OpModeVFOA/" + OpModeVFOA.ToString());
                a.Add("OpModeVFOB/" + OpModeVFOB.ToString());
                a.Add("VFOA/" + VFOA.ToString());
                a.Add("VFOB/" + VFOB.ToString());
                a.Add("LOSC/" + LOSC.ToString());

                s = "tx_image_phase_table/";
                for (int i = 0; i <= (int)Band.B6M; i++)
                    s += tx_image_phase_table[i].ToString("R") + "|";
                s = s.Substring(0, s.Length - 1);
                a.Add(s);

                s = "tx_image_gain_table/";
                for (int i = 0; i <= (int)Band.B6M; i++)
                    s += tx_image_gain_table[i].ToString("R") + "|";
                s = s.Substring(0, s.Length - 1);
                a.Add(s);

                s = "rx_image_phase_table/";
                for (int i = 0; i <= (int)Band.B6M; i++)
                    s += rx_image_phase_table[i].ToString("R") + "|";
                s = s.Substring(0, s.Length - 1);
                a.Add(s);

                s = "rx_image_gain_table/";
                for (int i = 0; i <= (int)Band.B6M; i++)
                    s += rx_image_gain_table[i].ToString("R") + "|";
                s = s.Substring(0, s.Length - 1);
                a.Add(s);

                s = "LOG file path/" + DB.LOGFilePath;
                a.Add(s);

                a.Add("main_top/" + this.Top.ToString());		    // save form positions
                a.Add("main_left/" + this.Left.ToString());
                a.Add("main_width/" + this.Width.ToString());
                a.Add("main_height/" + this.Height.ToString());

                DB.SaveVars("Options", ref a);		                // save the values to the DB
                DB.SaveBandStack(current_band.ToString(), vfoa, vfob, losc, filter_width_vfoA, filter_width_vfoB,
                    tbZoom.Value, tbPan.Value);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in SaveOptions function!\n" + ex.ToString());
            }
        }

        public void GetState()
        {
            try
            {
                double Losc = 14.04, VfoA = 14.05, VfoB = 14.05;
                // get list of live controls
                ArrayList temp = new ArrayList();		// list of all first level controls
                ControlList(this, ref temp);

                ArrayList checkbox_list = new ArrayList();
                ArrayList combobox_list = new ArrayList();
                ArrayList numericupdown_list = new ArrayList();
                ArrayList radiobutton_list = new ArrayList();
                ArrayList textbox_list = new ArrayList();
                ArrayList trackbar_list = new ArrayList();
                ArrayList colorbutton_list = new ArrayList();

                foreach (Control c in temp)
                {
                    if (c.GetType() == typeof(CheckBox))
                        checkbox_list.Add(c);
                    else if (c.GetType() == typeof(ComboBox))
                        combobox_list.Add(c);
                    else if (c.GetType() == typeof(NumericUpDown))
                        numericupdown_list.Add(c);
                    else if (c.GetType() == typeof(RadioButton))
                        radiobutton_list.Add(c);
                    //else if (c.GetType() == typeof(TextBox))
                    //textbox_list.Add(c);
                    else if (c.GetType() == typeof(TrackBar))
                        trackbar_list.Add(c);
                }

                temp.Clear();	// now that we have the controls we want, delete first list 

                ArrayList a = DB.GetVars("Options");
                a.Sort();

                // restore saved values to the controls
                foreach (string s in a)				// string is in the format "name,value"
                {
                    string[] vals = s.Split('/');

                    if (vals.Length > 2)
                    {
                        for (int i = 2; i < vals.Length; i++)
                            vals[1] += "/" + vals[i];
                    }

                    string name = vals[0];
                    string val = vals[1];

                    if (s.StartsWith("chk"))
                    {
                        for (int i = 0; i < checkbox_list.Count; i++)
                        {
                            CheckBox c = (CheckBox)checkbox_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Checked = bool.Parse(val);	// restore value
                                i = checkbox_list.Count + 1;
                            }
                            if (i == checkbox_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("combo"))	// control is a ComboBox
                    {
                        for (int i = 0; i < combobox_list.Count; i++)
                        {	// look through each control to find the matching name
                            ComboBox c = (ComboBox)combobox_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                if (c.Items.Count > 0 && c.Items[0].GetType() == typeof(string))
                                {
                                    c.Text = val;
                                }
                                else
                                {
                                    foreach (object o in c.Items)
                                    {
                                        if (o.ToString() == val)
                                            c.Text = val;	// restore value
                                    }
                                }
                                i = combobox_list.Count + 1;
                            }
                            if (i == combobox_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("ud"))
                    {
                        for (int i = 0; i < numericupdown_list.Count; i++)
                        {	// look through each control to find the matching name
                            NumericUpDown c = (NumericUpDown)numericupdown_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                decimal num = decimal.Parse(val);

                                if (num > c.Maximum) num = c.Maximum;		// check endpoints
                                else if (num < c.Minimum) num = c.Minimum;
                                c.Value = num;			// restore value
                                i = numericupdown_list.Count + 1;
                            }
                            if (i == numericupdown_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("rad"))
                    {	// look through each control to find the matching name
                        for (int i = 0; i < radiobutton_list.Count; i++)
                        {
                            RadioButton c = (RadioButton)radiobutton_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Checked = bool.Parse(val);	// restore value
                                i = radiobutton_list.Count + 1;
                            }
                            if (i == radiobutton_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("txt"))
                    {	// look through each control to find the matching name
                        for (int i = 0; i < textbox_list.Count; i++)
                        {
                            TextBox c = (TextBox)textbox_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Text = val;	// restore value
                                i = textbox_list.Count + 1;
                            }
                            if (i == textbox_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("tb"))
                    {
                        // look through each control to find the matching name
                        for (int i = 0; i < trackbar_list.Count; i++)
                        {
                            TrackBar c = (TrackBar)trackbar_list[i];
                            if (c.Name.Equals(name))		// name found
                            {
                                c.Value = Int32.Parse(val);
                                i = trackbar_list.Count + 1;
                            }
                            if (i == trackbar_list.Count)
                                MessageBox.Show("Control not found: " + name);
                        }
                    }
                    else if (s.StartsWith("OpModeVFOA"))
                    {
                        vals = s.Split('/');
                        if (vals.Length > 2)
                        {
                            for (int i = 2; i < vals.Length; i++)
                                vals[1] += "/" + vals[i];
                        }

                        val = vals[1];

                        tuning_mode = TuneMode.VFOA;

                        switch (val)
                        {
                            case "CW":
                                OpModeVFOA = Mode.CW;
                                cWToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;

                            case "RTTY":
                                OpModeVFOA = Mode.RTTY;
                                rTTYToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;

                            case "BPSK31":
                                OpModeVFOA = Mode.BPSK31;
                                bPSK31ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "BPSK63":
                                OpModeVFOA = Mode.BPSK63;
                                bPSK63ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "BPSK125":
                                OpModeVFOA = Mode.BPSK125;
                                bPSK125ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "BPSK250":
                                OpModeVFOA = Mode.BPSK250;
                                bPSK250ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK31":
                                OpModeVFOA = Mode.QPSK31;
                                qPSK31ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK63":
                                OpModeVFOA = Mode.QPSK63;
                                qPSK63ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK125":
                                OpModeVFOA = Mode.QPSK125;
                                qPSK125ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK250":
                                OpModeVFOA = Mode.QPSK250;
                                qPSK250ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                        }
                    }
                    else if (s.StartsWith("OpModeVFOB"))
                    {
                        vals = s.Split('/');
                        if (vals.Length > 2)
                        {
                            for (int i = 2; i < vals.Length; i++)
                                vals[1] += "/" + vals[i];
                        }

                        val = vals[1];

                        tuning_mode = TuneMode.VFOB;

                        switch (val)
                        {
                            case "CW":
                                OpModeVFOB = Mode.CW;
                                cWToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;

                            case "RTTY":
                                OpModeVFOB = Mode.RTTY;
                                rTTYToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;

                            case "BPSK31":
                                OpModeVFOB = Mode.BPSK31;
                                bPSK31ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "BPSK63":
                                OpModeVFOB = Mode.BPSK63;
                                bPSK63ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "BPSK125":
                                OpModeVFOB = Mode.BPSK125;
                                bPSK125ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "BPSK250":
                                OpModeVFOB = Mode.BPSK250;
                                bPSK250ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK31":
                                OpModeVFOB = Mode.QPSK31;
                                qPSK31ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK63":
                                OpModeVFOB = Mode.QPSK63;
                                qPSK63ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK125":
                                OpModeVFOB = Mode.QPSK125;
                                qPSK125ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                            case "QPSK250":
                                OpModeVFOB = Mode.QPSK250;
                                qPSK250ToolStripMenuItem_Click(this, EventArgs.Empty);
                                break;
                        }
                    }
                    else if (s.StartsWith("VFOA"))
                    {
                        vals = s.Split('/');
                        if (vals.Length > 2)
                        {
                            for (int i = 2; i < vals.Length; i++)
                                vals[1] += "/" + vals[i];
                        }

                        val = vals[1];

                        VfoA = double.Parse(val);
                    }
                    else if (s.StartsWith("VFOB"))
                    {
                        vals = s.Split('/');
                        if (vals.Length > 2)
                        {
                            for (int i = 2; i < vals.Length; i++)
                                vals[1] += "/" + vals[i];
                        }

                        val = vals[1];

                        VfoB = double.Parse(val);
                    }
                    else if (s.StartsWith("LOSC"))
                    {
                        vals = s.Split('/');
                        if (vals.Length > 2)
                        {
                            for (int i = 2; i < vals.Length; i++)
                                vals[1] += "/" + vals[i];
                        }

                        val = vals[1];

                        Losc = double.Parse(val);
                    }
                    else if (s.StartsWith("LOG file path"))
                    {
                        vals = s.Split('/');
                        log_file_path = vals[1];
                    }
                    else if (s.StartsWith("main_top"))
                    {
                        int top = Int32.Parse(vals[1]);
                        this.Top = top;
                    }
                    else if (s.StartsWith("main_left"))
                    {
                        int left = Int32.Parse(vals[1]);
                        this.Left = left;
                    }
                    else if (s.StartsWith("main_width"))
                    {
                        int width = Int32.Parse(vals[1]);
                        this.Width = width;
                    }
                    else if (s.StartsWith("main_height"))
                    {
                        int height = Int32.Parse(vals[1]);
                        this.Height = height;
                    }

                    switch (name)
                    {
                        case "tx_image_phase_table":
                            string[] list = val.Split('|');
                            for (int i = 0; i <= (int)Band.B6M && i < list.Length; i++)
                                tx_image_phase_table[i] = float.Parse(list[i]);
                            break;
                        case "tx_image_gain_table":
                            list = val.Split('|');
                            for (int i = 0; i <= (int)Band.B6M && i < list.Length; i++)
                                tx_image_gain_table[i] = float.Parse(list[i]);
                            break;
                        case "rx_image_phase_table":
                            list = val.Split('|');
                            for (int i = 0; i <= (int)Band.B6M && i < list.Length; i++)
                                rx_image_phase_table[i] = float.Parse(list[i]);
                            break;
                        case "rx_image_gain_table":
                            list = val.Split('|');
                            for (int i = 0; i <= (int)Band.B6M && i < list.Length; i++)
                                rx_image_gain_table[i] = float.Parse(list[i]);
                            break;
                    }
                }

                tbZoom.Value = 4;
                tbPan.Value = 0;
                CalcDisplayFreq();

                SetupForm.tbTXPhase.Value = (int)tx_image_phase_table[(int)current_band];
                SetupForm.tbTXGain.Value = (int)tx_image_gain_table[(int)current_band];
                SetupForm.tbRXPhase.Value = (int)rx_image_phase_table[(int)current_band];
                SetupForm.tbRXGain.Value = (int)rx_image_gain_table[(int)current_band];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in GetOption function!\n" + ex.ToString());
            }
        }

        private void ControlList(Control c, ref ArrayList a)
        {
            if (c.Controls.Count > 0)
            {
                foreach (Control c2 in c.Controls)
                    ControlList(c2, ref a);
            }

            if (c.GetType() == typeof(CheckBox) || c.GetType() == typeof(CheckBox) ||
                c.GetType() == typeof(ComboBox) || c.GetType() == typeof(ComboBox) ||
                c.GetType() == typeof(NumericUpDown) || c.GetType() == typeof(NumericUpDown) ||
                c.GetType() == typeof(RadioButton) || c.GetType() == typeof(RadioButton) ||
                c.GetType() == typeof(TextBox) || c.GetType() == typeof(TextBox) ||
                c.GetType() == typeof(TrackBar) || c.GetType() == typeof(TrackBar))
                a.Add(c);
        }

        #endregion

        #region Monitor

        private void picMonitor_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                double freq = 0.0;
                picMonitor.Focus();
                Display_GDI.DisplayCursorX = e.X;
#if(DirectX)
                DX.DisplayCursorX = e.X;
                DX.DisplayCursorY = e.Y;
                DX.waterfall_target_focused = false;
                DX.panadapter_target_focused = false;
#endif
                Display_GDI.DisplayCursorY = e.Y;

                double low = losc * 1e6 + Display_GDI.RXDisplayLow;
                double high = losc * 1e6 + Display_GDI.RXDisplayHigh;
                double width = high - low;
                float x = (float)(low + ((double)e.X / (double)picMonitor.Width * (double)width));

                if (Audio.SDRmode)
                {
                    freq = x / 1e6;
                    txtFreq.Text = freq.ToString("f6") + "Hz";

                    if (op_mode_vfoA == Mode.RTTY && rtty != null)
                    {
#if(DirectX)
                        DX.RTTYCursorX1 = (int)HzToPixel((float)((freq - losc - (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                        DX.RTTYCursorX2 = (int)HzToPixel((float)((freq - losc + (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
#endif
                        Display_GDI.RTTYCursorX1 = (int)HzToPixel((float)((freq - losc - (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                        Display_GDI.RTTYCursorX2 = (int)HzToPixel((float)((freq - losc + (rtty.trx.modem[0].shift / 2) / 1e6) * 1e6));
                    }
                }
                else
                    txtFreq.Text = Math.Round(x, 0).ToString() + "Hz";
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void picMonitor_MouseEnter(object sender, EventArgs e)
        {
            grpMonitor.Focus();
        }

        private void picMonitor_MouseLeave(object sender, EventArgs e)
        {
            btnStartMR.Focus();
        }

        private void picMonitor_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    double low = losc * 1e6 + Display_GDI.RXDisplayLow;
                    double high = losc * 1e6 + Display_GDI.RXDisplayHigh;

                    double width = high - low;
                    float x = (float)(low + ((double)e.X / (double)picMonitor.Width * (double)width));

                    if (tuning_mode == TuneMode.Off || tuning_mode == TuneMode.VFOA)
                        VFOA = x / 1e6;
                    else
                        VFOB = x / 1e6;

                    //Audio.monitor_paused = true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private bool UpdateDisplayPanadapterAverage()
        {
            try
            {
                if ((average_buffer[0] == -999.999F) ||
                    float.IsNaN(average_buffer[0]))
                {
                    //Debug.WriteLine("Clearing average buf"); 
                    for (int i = 0; i < 4096; i++)
                        average_buffer[i] = picMonitor_buffer[i];
                    return true;
                }

                float new_mult = 0.0f;
                float old_mult = 0.0f;
                new_mult = Display_GDI.display_avg_mult_new;
                old_mult = Display_GDI.display_avg_mult_old;

                for (int i = 0; i < 4096; i++)
                    average_buffer[i] = picMonitor_buffer[i] =
                        (float)(picMonitor_buffer[i] * new_mult +
                        average_buffer[i] * old_mult);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private void picMonitorRender()
        {
            try
            {
                try
                {
#if DirectX
                    float slope = 0.0f;
                    int num_samples = 0;
                    int start_sample_index = 0;
                    float max_y = float.MinValue;
                    int R = 0, G = 0, B = 0;
                    int i = 0;
                    float[] waterfall_data = new float[picMonitor.Width];
                    int end_sample_index = 0;
                    int yRange = 0;
                    int Low = Display_GDI.RXDisplayLow;
                    int High = Display_GDI.RXDisplayHigh;

                    if (video_driver == DisplayDriver.DIRECTX)
                        yRange = DX.SpectrumGridMax - DX.SpectrumGridMin;
                    else
                        yRange = Display_GDI.SpectrumGridMax - Display_GDI.SpectrumGridMin;

                    if (DX.AverageOn)
                    {
                        if (!UpdateDisplayPanadapterAverage())
                        {
                            average_buffer = null;
                            average_buffer = new float[4096];	// initialize averaging buffer array
                            average_buffer[0] = -999.999F;		// set the clear flag   
                            Debug.Write("Reset display average buffer!");
                        }
                    }

                    num_samples = picMonitor.Width;
                    double low = losc * 1e6 - Audio.SampleRate;
                    double high = losc * 1e6 + Audio.SampleRate;
                    start_sample_index = (int)((4096 >> 1) + (Low * 4096.0) / Audio.SampleRate + 1.0);
                    num_samples = (int)((High - Low) * 4096 / Audio.SampleRate);

                    if (start_sample_index < 0)
                        start_sample_index += 4096;

                    if ((num_samples - start_sample_index) > (4096 + 1))
                        num_samples = 4096 - start_sample_index;

                    slope = (float)num_samples / (float)picMonitor.Width;

                    if (start_sample_index < 0)
                    {
                        start_sample_index = 0;
                        end_sample_index = picMonitor.Width;
                    }

                    for (i = 0; i < picMonitor.Width; i++)
                    {
                        float max = float.MinValue;
                        float dval = i * slope + start_sample_index;
                        int lindex = (int)Math.Floor(dval);
                        int rindex = (int)Math.Floor(dval + slope);

                        if (slope <= 1.0 || lindex == rindex)
                        {
                            max = picMonitor_buffer[lindex % 4096] * ((float)lindex - dval + 1)
                                + picMonitor_buffer[(lindex + 1) % 4096] * (dval - (float)lindex);
                        }
                        else
                        {
                            for (int j = lindex; j < rindex; j++)
                                if (picMonitor_buffer[j % 4096] > max) max = picMonitor_buffer[j % 4096];
                        }

                        if (video_driver == DisplayDriver.DIRECTX)
                            max += DX.DisplayCalOffset;
                        else
                            max += Display_GDI.DisplayCalOffset;

                        if (max > max_y)
                            max_y = max;

                        waterfall_data[i] = max;
                        panadapterX_data[i] = (int)(Math.Floor((DX.SpectrumGridMax - max) * picMonitor.Height / yRange));
                    }

                    if (!mox && (monitor_mode == DisplayMode.WATERFALL || monitor_mode == DisplayMode.PANADAPTER))
                    {
                        if (monitor_mode == DisplayMode.WATERFALL)
                        {
                            int pixel_size = 4;
                            int ptr = 0;

                            if (DX.ReverseWaterfall)
                            {
                                Array.Copy(waterfall_memory, waterfall_bmp_stride, waterfall_memory, 0, waterfall_bmp_size - waterfall_bmp_stride);
                                ptr = waterfall_bmp_size - waterfall_bmp_stride;
                            }
                            else
                            {
                                Array.Copy(waterfall_memory, 0, waterfall_memory, waterfall_bmp_stride, waterfall_bmp_size - waterfall_bmp_stride);
                            }

                            i = 0;
                            // draw new data
                            for (i = 0; i < picMonitor.Width; i++)	// for each pixel in the new line
                            {
                                if (waterfall_data[i] <= Display_GDI.WaterfallLowThreshold)
                                {
                                    R = Display_GDI.WaterfallLowColor.R;
                                    G = Display_GDI.WaterfallLowColor.G;
                                    B = Display_GDI.WaterfallLowColor.B;
                                }
                                else if (waterfall_data[i] >= Display_GDI.WaterfallHighThreshold)
                                {
                                    R = 192;
                                    G = 124;
                                    B = 255;
                                }
                                else // value is between low and high
                                {
                                    float range = Display_GDI.WaterfallHighThreshold - Display_GDI.WaterfallLowThreshold;
                                    float offset = waterfall_data[i] - Display_GDI.WaterfallLowThreshold;
                                    float overall_percent = offset / range; // value from 0.0 to 1.0 where 1.0 is high and 0.0 is low.

                                    if (overall_percent < (float)2 / 9) // background to blue
                                    {
                                        float local_percent = overall_percent / ((float)2 / 9);
                                        R = (int)((1.0 - local_percent) * Display_GDI.WaterfallLowColor.R);
                                        G = (int)((1.0 - local_percent) * Display_GDI.WaterfallLowColor.G);
                                        B = (int)(Display_GDI.WaterfallLowColor.B + local_percent * (255 - Display_GDI.WaterfallLowColor.B));
                                    }
                                    else if (overall_percent < (float)3 / 9) // blue to blue-green
                                    {
                                        float local_percent = (overall_percent - (float)2 / 9) / ((float)1 / 9);
                                        R = 0;
                                        G = (int)(local_percent * 255);
                                        B = 255;
                                    }
                                    else if (overall_percent < (float)4 / 9) // blue-green to green
                                    {
                                        float local_percent = (overall_percent - (float)3 / 9) / ((float)1 / 9);
                                        R = 0;
                                        G = 255;
                                        B = (int)((1.0 - local_percent) * 255);
                                    }
                                    else if (overall_percent < (float)5 / 9) // green to red-green
                                    {
                                        float local_percent = (overall_percent - (float)4 / 9) / ((float)1 / 9);
                                        R = (int)(local_percent * 255);
                                        G = 255;
                                        B = 0;
                                    }
                                    else if (overall_percent < (float)7 / 9) // red-green to red
                                    {
                                        float local_percent = (overall_percent - (float)5 / 9) / ((float)2 / 9);
                                        R = 255;
                                        G = (int)((1.0 - local_percent) * 255);
                                        B = 0;
                                    }
                                    else if (overall_percent < (float)8 / 9) // red to red-blue
                                    {
                                        float local_percent = (overall_percent - (float)7 / 9) / ((float)1 / 9);
                                        R = 255;
                                        G = 0;
                                        B = (int)(local_percent * 255);
                                    }
                                    else // red-blue to purple end
                                    {
                                        float local_percent = (overall_percent - (float)8 / 9) / ((float)1 / 9);
                                        R = (int)((0.75 + 0.25 * (1.0 - local_percent)) * 255);
                                        G = (int)(local_percent * 255 * 0.5);
                                        B = 255;
                                    }
                                }

                                // set pixel color
                                waterfall_memory[i * pixel_size + 3 + ptr] = 255;
                                waterfall_memory[i * pixel_size + 2 + ptr] = (byte)R;	// set color in memory
                                waterfall_memory[i * pixel_size + 1 + ptr] = (byte)G;
                                waterfall_memory[i * pixel_size + 0 + ptr] = (byte)B;
                            }

                            try
                            {
                                if (!MOX)
                                {
                                    DataRectangle data;
                                    data = WaterfallTexture.LockRectangle(0, waterfall_rect, LockFlags.None);
                                    waterfall_data_stream = data.Data;
                                    waterfall_data_stream.Position = 0;
                                    waterfall_data_stream.Write(waterfall_memory, 0, (int)waterfall_data_stream.Length);
                                    WaterfallTexture.UnlockRectangle(0);
                                    waterfall_dx_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black.ToArgb(), 0.0f, 0);
                                    Waterfall_Sprite.Begin(SpriteFlags.None);
                                    Waterfall_Sprite.Draw(WaterfallTexture, Waterfall_texture_size, (Color4)Color.White);
                                    Waterfall_Sprite.End();
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.Write(ex.ToString());
                            }

                            waterfall_dx_device.BeginScene();
                            //waterfall_dx_device.SetRenderState(RenderState.AlphaBlendEnable, true);
                            //waterfall_dx_device.SetRenderState(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
                            //waterfall_dx_device.SetRenderState(RenderState.DestinationBlend, Blend.DestinationAlpha);
                        }
                        else if (monitor_mode == DisplayMode.PANADAPTER)
                        {
                            waterfall_dx_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black.ToArgb(), 0.0f, 0);
                            Panadapter_Sprite.Begin(SpriteFlags.AlphaBlend);
                            Panadapter_Sprite.Draw(PanadapterTexture, Panadapter_texture_size, (Color4)Color.White);
                            Panadapter_Sprite.End();

                            waterfall_dx_device.BeginScene();
                            //waterfall_dx_device.SetRenderState(RenderState.AlphaBlendEnable, true);
                            //waterfall_dx_device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                            //waterfall_dx_device.SetRenderState(RenderState.DestinationBlend, Blend.DestinationAlpha);

                            int j = 0;

                            for (i = 0; i < picMonitor.Width * 2; i++)
                            {
                                PanLine_verts_fill[i] = new Vertex();
                                PanLine_verts_fill[i].Color = DX.PanFillColor.ToArgb();
                                PanLine_verts_fill[i].Position = new Vector4(i / 2, panadapterX_data[j], 0.0f, 0.0f);
                                PanLine_verts_fill[i + 1] = new Vertex();
                                PanLine_verts_fill[i + 1].Color = DX.PanFillColor.ToArgb();
                                PanLine_verts_fill[i + 1].Position = new Vector4(i / 2, picMonitor.Height, 0.0f, 0.0f);
                                i++;
                                j++;
                            }

                            PanLine_vb_fill.Lock(0, 0, LockFlags.None).WriteRange(PanLine_verts_fill, 0, picMonitor.Width * 2);
                            PanLine_vb_fill.Unlock();

                            waterfall_dx_device.SetStreamSource(0, PanLine_vb_fill, 0, 20);
                            waterfall_dx_device.DrawPrimitives(PrimitiveType.LineList, 0, picMonitor.Width);

                            for (i = 0; i < picMonitor.Width; i++)
                            {
                                PanLine_verts[i] = new Vertex();
                                PanLine_verts[i].Color = DX.DataLineColor.ToArgb();
                                PanLine_verts[i].Position = new Vector4(i, panadapterX_data[i], 0.0f, 0.0f);
                            }

                            PanLine_vb.Lock(0, 0, LockFlags.None).WriteRange(PanLine_verts, 0, picMonitor.Width);
                            PanLine_vb.Unlock();

                            waterfall_dx_device.SetStreamSource(0, PanLine_vb, 0, 20);
                            waterfall_dx_device.DrawPrimitives(PrimitiveType.LineStrip, 0, picMonitor.Width - 1);

                        }

                        double vfoa_hz = vfoa * 1e6;
                        double vfob_hz = vfob * 1e6;
                        double losc_hz = losc * 1e6;
                        int vfoa_filter_low = 0;
                        int vfoa_filter_high = 0;
                        int vfoa_filter_left = 0;
                        int vfoa_filter_right = 0;
                        int vfob_filter_low = 0;
                        int vfob_filter_high = 0;
                        int vfob_filter_left = 0;
                        int vfob_filter_right = 0;

                        switch (op_mode_vfoA)
                        {
                            case Mode.RTTY:
                                {
                                    vfoa_filter_low = -FilterWidthVFOA / 2;
                                    vfoa_filter_high = FilterWidthVFOA / 2;
                                    vfoa_filter_left = (int)(((-Low + (vfoa_hz - (int)(rtty.trx.modem[0].shift / 2)) - losc_hz) /
                                        (High - Low) * picMonitor.Width));
                                    vfoa_filter_right = (int)(((-Low + (vfoa_hz + (int)(rtty.trx.modem[0].shift / 2)) - losc_hz) /
                                        (High - Low) * picMonitor.Width));

                                    if (vfoa_filter_left == vfoa_filter_right)
                                        vfoa_filter_right = vfoa_filter_left + 1;

                                    if (vfob_filter_left == vfob_filter_right)
                                        vfob_filter_right = vfob_filter_left + 1;

                                    double w = (vfoa_filter_right - vfoa_filter_left);
                                    int ww = (int)((w * rtty.trx.modem[0].shift / 2) / (int)(rtty.trx.modem[0].shift)) / 2;

                                    RenderVerticalLine(waterfall_dx_device, vfoa_filter_left - ww, 0,
                                        vfoa_filter_left - ww, 20, Color.White);
                                    RenderVerticalLine(waterfall_dx_device, vfoa_filter_left + ww, 0,
                                        vfoa_filter_left + ww, 20, Color.White);
                                    RenderVerticalLine(waterfall_dx_device, vfoa_filter_right + ww, 0,
                                        vfoa_filter_right + ww, 20, Color.White);
                                    RenderVerticalLine(waterfall_dx_device, vfoa_filter_right - ww, 0,
                                        vfoa_filter_right - ww, 20, Color.White);

                                    RenderHorizontalLine(waterfall_dx_device, vfoa_filter_left - ww, 0,
                                        vfoa_filter_right + ww, 0, Color.White);
                                    RenderHorizontalLine(waterfall_dx_device, vfoa_filter_left - ww, 1,
                                        vfoa_filter_right + ww, 1, Color.White);
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                vfoa_filter_low = -(int)psk.trx.modem[0].bandwidth / 2;
                                vfoa_filter_high = (int)psk.trx.modem[0].bandwidth / 2;
                                vfoa_filter_left = (int)(((-Low + vfoa_hz - filter_width_vfoA / 2 - losc_hz) /
                                    (High - Low) * picMonitor.Width));
                                vfoa_filter_right = (int)(((-Low + vfoa_hz + filter_width_vfoA / 2 - losc_hz) /
                                    (High - Low) * picMonitor.Width));

                                if (vfoa_filter_left == vfoa_filter_right)
                                    vfoa_filter_right = vfoa_filter_left + 1;

                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_left, 0,
                                    vfoa_filter_left, picMonitor.Height, Color.White);
                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_left - 1, 0,
                                    vfoa_filter_left - 1, picMonitor.Height, Color.White);
                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_right, 0,
                                    vfoa_filter_right, picMonitor.Height, Color.White);
                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_right + 1, 0,
                                    vfoa_filter_right + 1, picMonitor.Height, Color.White);
                                break;
                            case Mode.CW:
                                vfoa_filter_low = -FilterWidthVFOA / 2;
                                vfoa_filter_high = FilterWidthVFOA / 2;
                                vfoa_filter_left = (int)(((-Low + vfoa_hz - filter_width_vfoA / 2 - losc_hz) /
                                    (High - Low) * picMonitor.Width));
                                vfoa_filter_right = (int)(((-Low + vfoa_hz + filter_width_vfoA / 2 - losc_hz) /
                                    (High - Low) * picMonitor.Width));

                                if (vfoa_filter_left == vfoa_filter_right)
                                    vfoa_filter_right = vfoa_filter_left + 1;

                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_left, 0,
                                    vfoa_filter_left, picMonitor.Height, Color.White);
                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_left - 1, 0,
                                    vfoa_filter_left - 1, picMonitor.Height, Color.White);
                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_right, 0,
                                    vfoa_filter_right, picMonitor.Height, Color.White);
                                RenderVerticalLine(waterfall_dx_device, vfoa_filter_right + 1, 0,
                                    vfoa_filter_right + 1, picMonitor.Height, Color.White);
                                break;
                        }

                        switch (op_mode_vfoB)
                        {
                            case Mode.RTTY:
                                {
                                    vfob_filter_low = -FilterWidthVFOB / 2;
                                    vfob_filter_high = FilterWidthVFOB / 2;
                                    vfob_filter_left = (int)(((-Low + vfob_hz - (int)(rtty.trx.modem[0].shift / 2) - losc_hz) /
                                        (High - Low) * picMonitor.Width));
                                    vfob_filter_right = (int)(((-Low + vfob_hz + (int)(rtty.trx.modem[0].shift / 2) - losc_hz) /
                                        (High - Low) * picMonitor.Width));

                                    if (vfob_filter_left == vfob_filter_right)
                                        vfob_filter_right = vfob_filter_left + 1;

                                    double w = (vfob_filter_right - vfob_filter_left);
                                    int ww = (int)((w * rtty.trx.modem[0].shift / 2) / (int)(rtty.trx.modem[0].shift)) / 2;

                                    if (btnRX2On.BackColor == Color.LimeGreen)
                                    {
                                        RenderHorizontalLine(waterfall_dx_device, vfob_filter_left - ww, 0,
                                            vfob_filter_right + ww, 0, Color.Red);
                                        RenderHorizontalLine(waterfall_dx_device, vfob_filter_left - ww, 1,
                                            vfob_filter_right + ww, 1, Color.Red);
                                        RenderVerticalLine(waterfall_dx_device, vfob_filter_left - ww, 0,
                                            vfob_filter_left - ww, 20, Color.Red);
                                        RenderVerticalLine(waterfall_dx_device, vfob_filter_left + ww, 0,
                                            vfob_filter_left + ww, 20, Color.Red);
                                        RenderVerticalLine(waterfall_dx_device, vfob_filter_right + ww, 0,
                                            vfob_filter_right + ww, 20, Color.Red);
                                        RenderVerticalLine(waterfall_dx_device, vfob_filter_right - ww, 0,
                                            vfob_filter_right - ww, 20, Color.Red);
                                    }
                                }
                                break;

                            case Mode.BPSK31:
                            case Mode.BPSK63:
                            case Mode.BPSK125:
                            case Mode.BPSK250:
                            case Mode.QPSK31:
                            case Mode.QPSK63:
                            case Mode.QPSK125:
                            case Mode.QPSK250:
                                if (btnRX2On.BackColor == Color.LimeGreen)
                                {
                                    vfob_filter_low = -(int)psk.trx.modem[1].bandwidth / 2;
                                    vfob_filter_high = (int)psk.trx.modem[1].bandwidth / 2;
                                    vfob_filter_left = (int)(((-Low + vfob_hz - filter_width_vfoB / 2 - losc_hz) /
                                        (High - Low) * picMonitor.Width));
                                    vfob_filter_right = (int)(((-Low + vfob_hz + filter_width_vfoB / 2 - losc_hz) /
                                        (High - Low) * picMonitor.Width));

                                    if (vfob_filter_left == vfob_filter_right)
                                        vfob_filter_right = vfob_filter_left + 1;

                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_left, 0,
                                        vfob_filter_left, picMonitor.Height, Color.Red);
                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_left - 1, 0,
                                        vfob_filter_left - 1, picMonitor.Height, Color.Red);
                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_right, 0,
                                        vfob_filter_right, picMonitor.Height, Color.Red);
                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_right + 1, 0,
                                        vfob_filter_right + 1, picMonitor.Height, Color.Red);
                                }
                                break;
                            case Mode.CW:
                                if (btnRX2On.BackColor == Color.LimeGreen)
                                {
                                    vfob_filter_low = -FilterWidthVFOB / 2;
                                    vfob_filter_high = FilterWidthVFOB / 2;
                                    vfob_filter_left = (int)(((-Low + vfob_hz - filter_width_vfoB / 2 - losc_hz) /
                                        (High - Low) * picMonitor.Width));
                                    vfob_filter_right = (int)(((-Low + vfob_hz + filter_width_vfoB / 2 - losc_hz) /
                                        (High - Low) * picMonitor.Width));

                                    if (vfob_filter_left == vfob_filter_right)
                                        vfob_filter_right = vfob_filter_left + 1;

                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_left, 0,
                                        vfob_filter_left, picMonitor.Height, Color.Red);
                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_left - 1, 0,
                                        vfob_filter_left - 1, picMonitor.Height, Color.Red);
                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_right, 0,
                                        vfob_filter_right, picMonitor.Height, Color.Red);
                                    RenderVerticalLine(waterfall_dx_device, vfob_filter_right + 1, 0,
                                        vfob_filter_right + 1, picMonitor.Height, Color.Red);
                                }
                                break;
                        }

                        waterfall_dx_device.EndScene();
                        waterfall_dx_device.Present();
                    }
                    else if (monitor_mode == DisplayMode.SCOPE)
                    {
                        Mode new_mode = op_mode_vfoA;

                        if (tx_split)
                            new_mode = op_mode_vfoB;

                        if ((new_mode == Mode.CW && mox) || !mox)
                        {
                            DX.ConvertDataForScope(picMonitor.Width, picMonitor.Height);
                            waterfall_dx_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black.ToArgb(), 0.0f, 0);
                            Panadapter_Sprite.Begin(SpriteFlags.AlphaBlend);
                            Panadapter_Sprite.Draw(PanadapterTexture, Panadapter_texture_size, (Color4)Color.White);
                            Panadapter_Sprite.End();
                            waterfall_dx_device.BeginScene();
                            DX.RenderScopeLine(waterfall_dx_device, picMonitor.Width, true);
                            waterfall_dx_device.EndScene();
                            waterfall_dx_device.Present();
                        }
                    }
#endif
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void RenderVerticalLine(Device dev, int x1, int y1, int x2, int y2, Color color)
        {
            var vb = new VertexBuffer(dev, 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);

            vb.Lock(0, 0, LockFlags.None).WriteRange(new[] {
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x1, (float)y1, 0.0f, 0.0f) },
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x2, (float)y2, 0.0f, 0.0f) }
                 });
            vb.Unlock();

            dev.SetStreamSource(0, vb, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, 1);

            vb.Dispose();
        }

        private static void RenderHorizontalLine(Device dev, int x1, int y1, int x2, int y2, Color color)
        {
            var vb = new VertexBuffer(dev, 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);

            vb.Lock(0, 0, LockFlags.None).WriteRange(new[] {
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x1, (float)y1, 0.0f, 0.0f) },
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x2, (float)y2, 0.0f, 0.0f) }
                 });
            vb.Unlock();

            dev.SetStreamSource(0, vb, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, 1);

            vb.Dispose();
        }

        private void picMonitor_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (video_driver == DisplayDriver.GDI)
                {
                    float slope = 0.0f;
                    int num_samples = 0;
                    int start_sample_index = 0;
                    float max_y = float.MinValue;
                    int R = 0, G = 0, B = 0;
                    int i = 0;
                    int end_sample_index = 0;
                    int yRange = 0;
                    int Low = Display_GDI.RXDisplayLow;
                    int High = Display_GDI.RXDisplayHigh;

                    yRange = Display_GDI.SpectrumGridMax - Display_GDI.SpectrumGridMin;
                    num_samples = picMonitor.Width;
                    double low = losc * 1e6 - Audio.SampleRate;
                    double high = losc * 1e6 + Audio.SampleRate;
                    start_sample_index = (int)((4096 >> 1) + (Low * 4096.0) / Audio.SampleRate + 1.0);
                    num_samples = (int)((High - Low) * 4096 / Audio.SampleRate);

                    if (start_sample_index < 0)
                        start_sample_index += 4096;

                    if ((num_samples - start_sample_index) > (4096 + 1))
                        num_samples = 4096 - start_sample_index;

                    slope = (float)num_samples / (float)picMonitor.Width;

                    if (start_sample_index < 0)
                    {
                        start_sample_index = 0;
                        end_sample_index = picMonitor.Width;
                    }

                    for (i = 0; i < picMonitor.Width; i++)
                    {
                        float max = float.MinValue;
                        float dval = i * slope + start_sample_index;
                        int lindex = (int)Math.Floor(dval);
                        int rindex = (int)Math.Floor(dval + slope);

                        if (slope <= 1.0 || lindex == rindex)
                        {
                            max = picMonitor_buffer[lindex % 4096] * ((float)lindex - dval + 1)
                                + picMonitor_buffer[(lindex + 1) % 4096] * (dval - (float)lindex);
                        }
                        else
                        {
                            for (int j = lindex; j < rindex; j++)
                                if (picMonitor_buffer[j % 4096] > max) max = picMonitor_buffer[j % 4096];
                        }

                        max += Display_GDI.DisplayCalOffset;

                        if (max > max_y)
                            max_y = max;

                        picMonitor_buffer[i] = max;
                    }

                    Graphics g = e.Graphics;
                    picMonitor_buffer[0] = picMonitor_buffer[1];
                    BitmapData bitmapData = picMonitor_bmp.LockBits(
                            new Rectangle(0, 0, picMonitor_bmp.Width, picMonitor_bmp.Height),
                            ImageLockMode.ReadWrite,
                            picMonitor_bmp.PixelFormat);

                    int pixel_size = 3;
                    byte* row = null;

                    if (!mox && monitor_mode == DisplayMode.WATERFALL)
                    {
                        if (Display_GDI.ReverseWaterfall)
                        {
                            // first scroll image up
                            int total_size = bitmapData.Stride * bitmapData.Height;		// find buffer size
                            CWExpert.memcpy(bitmapData.Scan0.ToPointer(),
                                new IntPtr((int)bitmapData.Scan0 + bitmapData.Stride).ToPointer(),
                                total_size - bitmapData.Stride);

                            row = (byte*)(bitmapData.Scan0.ToInt32() + total_size - bitmapData.Stride);
                        }
                        else
                        {
                            // first scroll image down
                            int total_size = bitmapData.Stride * bitmapData.Height;		// find buffer size
                            CWExpert.memcpy(new IntPtr((int)bitmapData.Scan0 + bitmapData.Stride).ToPointer(),
                                bitmapData.Scan0.ToPointer(),
                                total_size - bitmapData.Stride);

                            row = (byte*)(bitmapData.Scan0.ToInt32());
                        }

                        // draw new data
                        for (i = 0; i < picMonitor.Width; i++)	// for each pixel in the new line
                        {
                            if (picMonitor_buffer[i] <= Display_GDI.WaterfallLowThreshold)
                            {
                                R = Color.Black.R;
                                G = Color.Black.G;
                                B = Color.Black.B;
                            }
                            else if (picMonitor_buffer[i] >= Display_GDI.WaterfallHighThreshold)
                            {
                                R = 192;
                                G = 124;
                                B = 255;
                            }
                            else // value is between low and high
                            {
                                float range = Display_GDI.WaterfallHighThreshold - Display_GDI.WaterfallLowThreshold;
                                float offset = picMonitor_buffer[i] - Display_GDI.WaterfallLowThreshold;
                                float overall_percent = offset / range;

                                if (overall_percent < (float)2 / 9) // background to blue
                                {
                                    float local_percent = overall_percent / ((float)2 / 9);
                                    R = (int)((1.0 - local_percent) * Color.Black.R);
                                    G = (int)((1.0 - local_percent) * Color.Black.G);
                                    B = (int)(Color.Black.B + local_percent * (255 - Color.Black.B));
                                }
                                else if (overall_percent < (float)3 / 9) // blue to blue-green
                                {
                                    float local_percent = (overall_percent - (float)2 / 9) / ((float)1 / 9);
                                    R = 0;
                                    G = (int)(local_percent * 255);
                                    B = 255;
                                }
                                else if (overall_percent < (float)4 / 9) // blue-green to green
                                {
                                    float local_percent = (overall_percent - (float)3 / 9) / ((float)1 / 9);
                                    R = 0;
                                    G = 255;
                                    B = (int)((1.0 - local_percent) * 255);
                                }
                                else if (overall_percent < (float)5 / 9) // green to red-green
                                {
                                    float local_percent = (overall_percent - (float)4 / 9) / ((float)1 / 9);
                                    R = (int)(local_percent * 255);
                                    G = 255;
                                    B = 0;
                                }
                                else if (overall_percent < (float)7 / 9) // red-green to red
                                {
                                    float local_percent = (overall_percent - (float)5 / 9) / ((float)2 / 9);
                                    R = 255;
                                    G = (int)((1.0 - local_percent) * 255);
                                    B = 0;
                                }
                                else if (overall_percent < (float)8 / 9) // red to red-blue
                                {
                                    float local_percent = (overall_percent - (float)7 / 9) / ((float)1 / 9);
                                    R = 255;
                                    G = 0;
                                    B = (int)(local_percent * 255);
                                }
                                else // red-blue to purple end
                                {
                                    float local_percent = (overall_percent - (float)8 / 9) / ((float)1 / 9);
                                    R = (int)((0.75 + 0.25 * (1.0 - local_percent)) * 255);
                                    G = (int)(local_percent * 255 * 0.5);
                                    B = 255;
                                }
                            }

                            row[i * pixel_size + 0] = (byte)B;
                            row[i * pixel_size + 1] = (byte)G;
                            row[i * pixel_size + 2] = (byte)R;
                        }
                    }

                    picMonitor_bmp.UnlockBits(bitmapData);

                    g.DrawImageUnscaled(picMonitor_bmp, 0, 0);

                    Pen vfoa_line = new Pen(Color.Yellow);
                    Pen vfob_line = new Pen(Color.Red);
                    double vfoa_hz = vfoa * 1e6;
                    double vfob_hz = vfob * 1e6;
                    double losc_hz = losc * 1e6;

                    int vfoa_filter_low = 0;
                    int vfoa_filter_high = 0;
                    int vfoa_filter_left = 0;
                    int vfoa_filter_right = 0;
                    int vfob_filter_low = 0;
                    int vfob_filter_high = 0;
                    int vfob_filter_left = 0;
                    int vfob_filter_right = 0;

                    switch (op_mode_vfoA)
                    {
                        case Mode.RTTY:
                            {
                                vfoa_filter_low = (int)(-rtty.trx.modem[0].shift / 2);
                                vfoa_filter_high = (int)(rtty.trx.modem[0].shift / 2);
                                vfoa_filter_left = (int)(((-Low + vfoa_filter_low + vfoa_hz - losc_hz) / (High - Low) * picMonitor.Width));
                                vfoa_filter_right = (int)(((-Low + vfoa_filter_high + vfoa_hz - losc_hz) / (High - Low) * picMonitor.Width));

                                if (vfoa_filter_left == vfoa_filter_right)
                                    vfoa_filter_right = vfoa_filter_left + 1;

                                vfob_filter_low = (int)(-rtty.trx.modem[0].shift / 2);
                                vfob_filter_high = (int)(rtty.trx.modem[0].shift / 2);
                                vfob_filter_left = (int)(((-Low + vfob_filter_low + vfob_hz - losc_hz) / (High - Low) * picMonitor.Width));
                                vfob_filter_right = (int)(((-Low + vfob_filter_high + vfob_hz - losc_hz) / (High - Low) * picMonitor.Width));

                                if (vfoa_filter_left == vfoa_filter_right)
                                    vfoa_filter_right = vfoa_filter_left + 1;

                                if (vfob_filter_left == vfob_filter_right)
                                    vfob_filter_right = vfob_filter_left + 1;
                            }
                            break;

                        case Mode.BPSK31:
                        case Mode.BPSK63:
                        case Mode.BPSK125:
                        case Mode.BPSK250:
                        case Mode.QPSK31:
                        case Mode.QPSK63:
                        case Mode.QPSK125:
                        case Mode.QPSK250:
                            vfoa_filter_low = -(int)psk.trx.modem[0].bandwidth / 2;
                            vfoa_filter_high = (int)psk.trx.modem[0].bandwidth / 2;
                            vfoa_filter_left = (int)(((-Low + vfoa_filter_low + vfoa_hz - losc_hz) / (High - Low) * picMonitor.Width));
                            vfoa_filter_right = (int)(((-Low + vfoa_filter_high + vfoa_hz - losc_hz) / (High - Low) * picMonitor.Width));

                            vfob_filter_low = -(int)psk.trx.modem[1].bandwidth / 2;
                            vfob_filter_high = (int)psk.trx.modem[1].bandwidth / 2;
                            vfob_filter_left = (int)(((-Low + vfob_filter_low + vfob_hz - losc_hz) / (High - Low) * picMonitor.Width));
                            vfob_filter_right = (int)(((-Low + vfob_filter_high + vfob_hz - losc_hz) / (High - Low) * picMonitor.Width));

                            if (vfoa_filter_left == vfoa_filter_right)
                                vfoa_filter_right = vfoa_filter_left + 1;

                            if (vfob_filter_left == vfob_filter_right)
                                vfob_filter_right = vfob_filter_left + 1;
                            break;
                        case Mode.CW:
                            vfoa_filter_low = -FilterWidthVFOA / 2;
                            vfoa_filter_high = FilterWidthVFOA / 2;
                            vfoa_filter_left = (int)(((-Low + vfoa_hz - losc_hz) / (High - Low) * picMonitor.Width));
                            vfoa_filter_right = vfoa_filter_left;

                            vfob_filter_low = -FilterWidthVFOA / 2;
                            vfob_filter_high = FilterWidthVFOA / 2;
                            vfob_filter_left = (int)(((-Low + vfob_hz - losc_hz) / (High - Low) * picMonitor.Width));
                            vfob_filter_right = vfob_filter_left;

                            //if (vfoa_filter_left == vfoa_filter_right)
                            //vfoa_filter_right = vfoa_filter_left + 1;

                            //if (vfob_filter_left == vfob_filter_right)
                            //vfob_filter_right = vfob_filter_left + 1;
                            break;
                    }

                    g.DrawLine(vfoa_line, vfoa_filter_left, 0, vfoa_filter_left, picMonitor.Height);
                    g.DrawLine(vfoa_line, vfoa_filter_left + 1, 0, vfoa_filter_left + 1, picMonitor.Height);
                    g.DrawLine(vfoa_line, vfoa_filter_right, 0, vfoa_filter_right, picMonitor.Height);
                    g.DrawLine(vfoa_line, vfoa_filter_right + 1, 0, vfoa_filter_right + 1, picMonitor.Height);

                    if (Display_GDI.RX2Enabled)
                    {
                        g.DrawLine(vfob_line, vfob_filter_left, 0, vfob_filter_left, picMonitor.Height);
                        g.DrawLine(vfob_line, vfob_filter_left + 1, 0, vfob_filter_left + 1, picMonitor.Height);
                        g.DrawLine(vfob_line, vfob_filter_right, 0, vfob_filter_right, picMonitor.Height);
                        g.DrawLine(vfob_line, vfob_filter_right + 1, 0, vfob_filter_right + 1, picMonitor.Height);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void radRTTYNormal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rtty != null)
                {
                    if (radRTTYNormal.Checked)
                    {
                        rtty.trx.modem[0].reverse = false;
                        rtty.trx.modem[1].reverse = false;
                        VFOA = vfoa;
                        VFOB = vfob;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void radRTTYReverse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rtty != null)
                {
                    if (radRTTYReverse.Checked)
                    {
                        rtty.trx.modem[0].reverse = true;
                        rtty.trx.modem[1].reverse = true;
                        VFOA = vfoa;
                        VFOB = vfob;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region QSO log

        public void InitLOG()
        {
            try
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        btnLOG1.Text = SetupForm.txtCWbtn1.Text;
                        btnLOG2.Text = SetupForm.txtCWbtn2.Text;
                        btnLOG3.Text = SetupForm.txtCWbtn3.Text;
                        btnLOG4.Text = SetupForm.txtCWbtn4.Text;
                        btnLOG5.Text = SetupForm.txtCWbtn5.Text;
                        btnLOG6.Text = SetupForm.txtCWbtn6.Text;
                        break;

                    case Mode.RTTY:
                        btnLOG1.Text = SetupForm.txtRTTYbtn1.Text;
                        btnLOG2.Text = SetupForm.txtRTTYbtn2.Text;
                        btnLOG3.Text = SetupForm.txtRTTYbtn3.Text;
                        btnLOG4.Text = SetupForm.txtRTTYbtn4.Text;
                        btnLOG5.Text = SetupForm.txtRTTYbtn5.Text;
                        btnLOG6.Text = SetupForm.txtRTTYbtn6.Text;
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        btnLOG1.Text = SetupForm.txtPSKbtn1.Text;
                        btnLOG2.Text = SetupForm.txtPSKbtn2.Text;
                        btnLOG3.Text = SetupForm.txtPSKbtn3.Text;
                        btnLOG4.Text = SetupForm.txtPSKbtn4.Text;
                        btnLOG5.Text = SetupForm.txtPSKbtn5.Text;
                        btnLOG6.Text = SetupForm.txtPSKbtn6.Text;
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private string MacroHandler(string msg)
        {
            try
            {
                System.DateTime date = DateTime.UtcNow;
                string freq = vfoa.ToString();
                string mode = op_mode_vfoA.ToString();
                string name = txtLogName.Text.ToString();

                if (name == "")
                    name = "OM";

                if (tx_split)
                {
                    freq = vfob.ToString();
                    mode = op_mode_vfoB.ToString();
                }

                msg = msg.Replace("<CALL>", txtLogCall.Text.ToUpper());
                msg = msg.Replace("<My CALL>", SetupForm.txtStnCALL.Text.ToString());
                msg = msg.Replace("<My RST>", txtLogMyRST.Text.ToString());
                msg = msg.Replace("<My NR>", udLOGMyNR.Text.ToString());
                msg = msg.Replace("<My QTH>", SetupForm.txtStnQTH.Text.ToString());
                msg = msg.Replace("<My LOC>", SetupForm.txtStnLOC.Text.ToString());
                msg = msg.Replace("<My Name>", SetupForm.txtStnName.Text.ToString());
                msg = msg.Replace("<Name>", name);
                msg = msg.Replace("<My Info>", SetupForm.txtStnInfoTxt.Text.ToString());
                msg = msg.Replace("<My Zone>", SetupForm.txtStnZone.Text.ToString());
                msg = msg.Replace("<Freq>", freq);
                msg = msg.Replace("<Mode>", mode);
                msg = msg.Replace("<Date>", date.Date.ToShortDateString());
                msg = msg.Replace("<Time>", date.ToUniversalTime().ToShortTimeString());
                msg = msg.Replace("<Band>", CurrentBand.ToString().Replace("B", ""));

                if (msg.Contains("<No>"))
                {
                    Mode opmode = op_mode_vfoA;
                    int cnt = 1;

                    if (tx_split)
                        opmode = op_mode_vfoB;

                    switch (opmode)
                    {
                        case Mode.CW:
                            cnt = int.Parse(log_book.txtCW.Text.ToString());
                            cnt++;
                            break;

                        case Mode.RTTY:
                            cnt = int.Parse(log_book.txtRTTY.Text.ToString());
                            cnt++;
                            break;

                        case Mode.BPSK31:
                            cnt = int.Parse(log_book.txtPSK31.Text.ToString());
                            cnt++;
                            break;

                        case Mode.BPSK63:
                            cnt = int.Parse(log_book.txtPSK63.Text.ToString());
                            cnt++;
                            break;

                        case Mode.BPSK125:
                            cnt = int.Parse(log_book.txtPSK125.Text.ToString());
                            cnt++;
                            break;

                        case Mode.BPSK250:
                            cnt = int.Parse(log_book.txtPSK250.Text.ToString());
                            cnt++;
                            break;

                        case Mode.QPSK31:
                            cnt = int.Parse(log_book.txtQPSK31.Text.ToString());
                            cnt++;
                            break;

                        case Mode.QPSK63:
                            cnt = int.Parse(log_book.txtQPSK63.Text.ToString());
                            cnt++;
                            break;

                        case Mode.QPSK125:
                            cnt = int.Parse(log_book.txtQPSK125.Text.ToString());
                            cnt++;
                            break;

                        case Mode.QPSK250:
                            cnt = int.Parse(log_book.txtQPSK250.Text.ToString());
                            cnt++;
                            break;
                    }

                    msg = msg.Replace("<No>", cnt.ToString());
                }

                return msg;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return "";
            }
        }

        private void LOGbtn1_Click(object sender, EventArgs e)      // CQ
        {
            if (sender != null)
            {
                rtty_message_repeat = false;
                rtty_message_event.Set();
            }

            if (!MOX)
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        string msg = "";
                        msg = SetupForm.txtCWMsg1.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            cwEncoder.Start(msg);
                        break;

                    case Mode.RTTY:
                        msg = "";
                        msg = SetupForm.txtRTTYMsg1.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            rtty.Start(msg);
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        msg = "";
                        msg = SetupForm.txtPSKMsg1.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            psk.Start(msg);
                        break;
                }
            }
            else
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        cwEncoder.Stop();
                        break;

                    case Mode.RTTY:
                        rtty.Stop();
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.Stop();
                        break;
                }
            }
        }

        private void LOGbtn2_Click(object sender, EventArgs e)      // QRZ?
        {
            if (sender != null)
            {
                rtty_message_repeat = false;
                rtty_message_event.Set();
            }

            if (!MOX)
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        string msg = "";
                        msg = SetupForm.txtCWMsg2.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            cwEncoder.Start(msg);
                        break;

                    case Mode.RTTY:
                        msg = "";
                        msg = SetupForm.txtRTTYMsg2.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            rtty.Start(msg);
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        msg = "";
                        msg = SetupForm.txtPSKMsg2.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            psk.Start(msg);
                        break;
                }
            }
            else
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        cwEncoder.Stop();
                        break;

                    case Mode.RTTY:
                        rtty.Stop();
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.Stop();
                        break;
                }
            }
        }

        private void LOGbtn3_Click(object sender, EventArgs e)      // answer
        {
            if (sender != null)
            {
                rtty_message_repeat = false;
                rtty_message_event.Set();
            }

            if (!MOX)
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        string msg = "";
                        msg = SetupForm.txtCWMsg3.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            cwEncoder.Start(msg);
                        break;

                    case Mode.RTTY:
                        msg = "";
                        msg = SetupForm.txtRTTYMsg3.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            rtty.Start(msg);
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        msg = "";
                        msg = SetupForm.txtPSKMsg3.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            psk.Start(msg);
                        break;
                }
            }
            else
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        cwEncoder.Stop();
                        break;

                    case Mode.RTTY:
                        rtty.Stop();
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.Stop();
                        break;
                }
            }
        }

        private void LOGbtn4_Click(object sender, EventArgs e)      // short info
        {
            if (sender != null)
            {
                rtty_message_repeat = false;
                rtty_message_event.Set();
            }

            if (!MOX)
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        string msg = "";
                        msg = SetupForm.txtCWMsg4.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            cwEncoder.Start(msg);
                        break;

                    case Mode.RTTY:
                        msg = "";
                        msg = SetupForm.txtRTTYMsg4.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            rtty.Start(msg);
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        msg = "";
                        msg = SetupForm.txtPSKMsg4.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            psk.Start(msg);
                        break;
                }
            }
            else
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        cwEncoder.Stop();
                        break;

                    case Mode.RTTY:
                        rtty.Stop();
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.Stop();
                        break;
                }
            }
        }

        private void LOGbtn5_Click(object sender, EventArgs e)      // long info
        {
            if (sender != null)
            {
                rtty_message_repeat = false;
                rtty_message_event.Set();
            }

            if (!MOX)
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        string msg = "";
                        msg = SetupForm.txtCWMsg5.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            cwEncoder.Start(msg);
                        break;

                    case Mode.RTTY:
                        msg = "";
                        msg = SetupForm.txtRTTYMsg5.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            rtty.Start(msg);
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        msg = "";
                        msg = SetupForm.txtPSKMsg5.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            psk.Start(msg);
                        break;
                }
            }
            else
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        cwEncoder.Stop();
                        break;

                    case Mode.RTTY:
                        rtty.Stop();
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.Stop();
                        break;
                }
            }
        }

        private void LOGbtn6_Click(object sender, EventArgs e)      // QSO end
        {
            if (sender != null)
            {
                rtty_message_repeat = false;
                rtty_message_event.Set();
            }

            if (!MOX)
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        string msg = "";
                        msg = SetupForm.txtCWMsg6.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            cwEncoder.Start(msg);
                        break;

                    case Mode.RTTY:
                        msg = "";
                        msg = SetupForm.txtRTTYMsg6.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            rtty.Start(msg);
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        msg = "";
                        msg = SetupForm.txtPSKMsg6.Text.ToString();
                        msg = MacroHandler(msg);

                        if (keyboard.Visible)
                            keyboard.Start(msg);
                        else
                            psk.Start(msg);
                        break;
                }
            }
            else
            {
                Mode new_mode = op_mode_vfoA;

                if (tx_split)
                    new_mode = op_mode_vfoB;

                switch (new_mode)
                {
                    case Mode.CW:
                        cwEncoder.Stop();
                        break;

                    case Mode.RTTY:
                        rtty.Stop();
                        break;

                    case Mode.BPSK31:
                    case Mode.BPSK63:
                    case Mode.BPSK125:
                    case Mode.BPSK250:
                    case Mode.QPSK31:
                    case Mode.QPSK63:
                    case Mode.QPSK125:
                    case Mode.QPSK250:
                        psk.Stop();
                        break;
                }
            }
        }

        public void AddLOGEntry()
        {
            try
            {
                int nr = 0;

                if (txtLOGNR.Text != "")
                    nr = int.Parse(txtLOGNR.Text.ToString());

                double freq = Math.Round(vfoa * 1000, 1);
                string mode = OpModeVFOA.ToString();

                if (tx_split)
                {
                    freq = Math.Round(vfob * 1000, 1);
                    mode = OpModeVFOB.ToString();
                }

                string f = freq.ToString("f1");
                f = f.Replace(',', '.');

                if (DB.AddQSO(txtLogCall.Text.ToUpper(), txtLogRST.Text.ToString(), txtLogMyRST.Text.ToString(),
                    txtLogName.Text.ToString(), txtLogQTH.Text.ToString(), txtLOGLOC.Text.ToString(), txtLogInfo.Text.ToString(),
                    DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToUniversalTime().ToShortTimeString(), mode,
                    f, current_band.ToString().Replace("B", ""), txtLOGZone.Text.ToString(), nr,
                    (int)udLOGMyNR.Value))
                {
                    if (log_book != null)
                        log_book.RowAdded();    // scroll to last row

                    txtLogCall.Text = "";
                    txtLogName.Text = "";
                    txtLogQTH.Text = "";
                    txtLogInfo.Text = "";
                    txtLOGLOC.Text = "";
                    txtLOGNR.Text = "";
                    txtLOGZone.Text = "";
                }

                if (udLOGMyNR.Value > 0)
                    udLOGMyNR.Value += 1;

                if (log_book != null)
                    log_book.LOGStatistic();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLogSave_Click(object sender, EventArgs e)
        {
            try
            {
                AddLOGEntry();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLogClear_Click(object sender, EventArgs e)
        {
            txtLogCall.Text = "";
            txtLogMyRST.Text = "";
            txtLogName.Text = "";
            txtLogQTH.Text = "";
            txtLogRST.Text = "";
            txtLogInfo.Text = "";
            txtLOGLOC.Text = "";
            txtLOGNR.Text = "";
            txtLOGZone.Text = "";
        }

        private void btnLogSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList list = new ArrayList();
                list = DB.SearchLOG(txtLogCall.Text.ToUpper().Trim());

                if (list.Count > 0)
                {
                    txtLogSearch.Clear();

                    foreach (string l in list)
                    {
                        l.Replace(".", "");
                        string[] vals = l.Split('\\');
                        string band = current_band.ToString().Replace("B", "");

                        if (band == vals[10])
                        {
                            txtLogSearch.Text = "Last: " + vals[1] + " " + vals[2] +
                                " on " + vals[10] + " " + vals[11] + "\n";
                        }
                    }
                }
                else
                    txtLogSearch.Clear();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLogFirst_Click(object sender, EventArgs e)
        {

        }

        private void btnLogPrev_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLogDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnLogNext_Click(object sender, EventArgs e)
        {

        }

        private void btnLogLast_Click(object sender, EventArgs e)
        {
            Audio.rb_mon_l.ResetReadPtr();
            Audio.rb_mon_r.ResetReadPtr();
        }

        private void CALLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLogCall.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void LOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLOGLOC.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void RSTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLogRST.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void NameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLogName.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void QTHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLogQTH.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void InfoTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLogInfo.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void NRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLOGNR.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void ZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH1.SelectedText.ToString();
            text = text.Replace("\n", " ");
            text = text.TrimStart(' ');
            text = text.TrimEnd(' ');
            txtLOGZone.Text = text;
            rtbCH1.AppendText(ch1_tmp_string);
            rtbCH1.SelectionStart = rtbCH1.TextLength;
            //SendMessage(rtbCH1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch1_tmp_string = "";
        }

        private void rtbCH1_MouseDown(object sender, MouseEventArgs e)
        {
            //Point p = new Point(this.Left + e.X, this.Top + e.Y);

            //if (e.Button == MouseButtons.Right)
            //ch1_contextQSO.Show();
        }

        private void rtbCH1_MouseEnter(object sender, EventArgs e)
        {
            rtbCH1_scroll_down = false;
        }

        private void rtbCH1_MouseLeave(object sender, EventArgs e)
        {
            rtbCH1_scroll_down = true;
            btnStartMR.Focus();
        }

        private void rtbCH1_DoubleClick(object sender, EventArgs e)
        {
            if (!ROBOT)
            {
                Point p;
                GetCursorPos(out p); ;

                ch1_contextQSO.Show(p);
            }
        }

        private void rtbCH2_DoubleClick(object sender, EventArgs e)
        {
            Point p;
            GetCursorPos(out p);
            ch2_contextQSO.Show(p);
        }

        private void rtbCH2_MouseEnter(object sender, EventArgs e)
        {
            rtbCH2_scroll_down = false;
        }

        private void rtbCH2_MouseLeave(object sender, EventArgs e)
        {
            rtbCH2_scroll_down = true;
            btnStartMR.Focus();
        }

        private void rtbCH1_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbCH1.SelectedText == "")
            {
                if (ch1_tmp_string != "")
                {
                    string text = ch1_tmp_string;
                    ch1_tmp_string = "";
                    rtbCH1.Text += text;
                }
            }
        }

        private void rtbCH2_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbCH2.SelectedText == "")
            {
                if (ch2_tmp_string != "")
                {
                    string text = ch2_tmp_string;
                    ch2_tmp_string = "";
                    rtbCH2.Text += text;
                }
            }
        }

        private void txtLogCall_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ArrayList list = new ArrayList();
                list = DB.SearchLOG(txtLogCall.Text.ToUpper().Trim());

                if (list.Count > 0)
                {
                    txtLogSearch.Clear();

                    foreach (string l in list)
                    {
                        l.Replace(".", "");
                        string[] vals = l.Split('\\');
                        string band = current_band.ToString().Replace("B", "");

                        if (band == vals[10])
                        {
                            txtLogSearch.Text = "Last: " + vals[1] + " " + vals[2] +
                                " on " + vals[10] + " " + vals[11] + "\n";
                        }
                    }
                }
                else
                    txtLogSearch.Clear();

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void ch2CALLMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLogCall.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2RSTMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLogRST.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2NameMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLogName.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2QTHMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLogQTH.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2LOCMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLOGLOC.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2ZoneMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLOGZone.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2NRMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLOGNR.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH2.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void ch2InfoMenuItem_Click(object sender, EventArgs e)
        {
            string text = rtbCH2.SelectedText.TrimEnd(' ');
            txtLogInfo.Text = text;
            rtbCH2.AppendText(ch2_tmp_string);
            rtbCH2.SelectionStart = rtbCH1.TextLength;
            SendMessage(rtbCH2.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            ch2_tmp_string = "";
        }

        private void btnLOG_Click(object sender, EventArgs e)
        {
            grpLogBook.Visible = !grpLogBook.Visible;

            if (grpLogBook.Visible)
            {
                LogVisible = true;
                Audio.ScopeDisplayWidth = picMonitor.Width;
            }
            else
            {
                LogVisible = false;
                Audio.ScopeDisplayWidth = picWaterfall.Width;
            }
        }

        #endregion

        #region DX Cluster

        private void dXClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DXClusterForm != null && !DXClusterForm.IsDisposed)
                    DXClusterForm.Show();
                else
                {
                    DXClusterForm = new DXClusterClient(SetupForm.txtTelnetHostAddress.Text.ToString(),
                        SetupForm.txtStnCALL.Text.ToString(), SetupForm.txtStnName.Text.ToString(),
                        SetupForm.txtStnQTH.Text.ToString());
                    DXClusterForm.Show();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region misc functions

        private void CWExpert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                freq_mult = true;
            else
            {
                switch (e.KeyData)
                {
                    case Keys.F1:
                        LOGbtn1_Click(this, EventArgs.Empty);
                        break;

                    case Keys.F2:
                        LOGbtn2_Click(this, EventArgs.Empty);
                        break;

                    case Keys.F3:
                        LOGbtn3_Click(this, EventArgs.Empty);
                        break;

                    case Keys.F4:
                        LOGbtn4_Click(this, EventArgs.Empty);
                        break;

                    case Keys.F5:
                        LOGbtn5_Click(this, EventArgs.Empty);
                        break;

                    case Keys.F6:
                        LOGbtn6_Click(this, EventArgs.Empty);
                        break;
                }
            }
        }

        public void RX_phase_gain()
        {
            try
            {
                if (!booting)
                {
                    if (!Audio.iq_balanced)
                    {
                        SetupForm.udRXGain.Value = (decimal)rx_image_gain_table[(int)current_band];
                        SetupForm.udRXPhase.Value = (decimal)rx_image_phase_table[(int)current_band];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in RX_phase_gain \n\n" + ex.ToString());
            }
        }

        public void SetRXIQGainPhase(uint type, float gain, float phase)
        {
            try
            {
                if (!booting)
                {
                    switch (type)
                    {
                        case 0:                                                     // balanced
                            SetIQBalanced(0, true);
                            SetIQFixed(0, 0, 0, gain, phase);
                            SetIQFixed(0, 1, 0, gain, phase);
                            SetIQFixed(0, 2, 0, gain, phase);
                            SetIQFixed(0, 3, 0, gain, phase);
                            break;

                        case 1:                                                      // fixed
                            SetIQBalanced(0, false);
                            SetIQFixed(0, 0, 1, gain, phase);
                            SetIQFixed(0, 1, 1, gain, phase);
                            SetIQFixed(0, 2, 1, gain, phase);
                            SetIQFixed(0, 3, 1, gain, phase);
                            break;

                        case 2:                                                      // wbir
                            SetIQBalanced(0, false);
                            SetIQFixed(0, 0, 0, gain, phase);
                            SetIQFixed(0, 1, 0, gain, phase);
                            SetIQFixed(0, 2, 0, gain, phase);
                            SetIQFixed(0, 3, 0, gain, phase);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void chkSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSQL.Checked)
            {
                chkSQL.BackColor = Color.LimeGreen;

                if (rtty != null)
                    rtty.SqlOn = true;
                if (cwDecoder != null)
                    cwDecoder.SqlOn = true;
            }
            else
            {
                chkSQL.BackColor = Color.WhiteSmoke;

                if (rtty != null)
                    rtty.SqlOn = false;
                if (cwDecoder != null)
                    cwDecoder.SqlOn = false;
            }
        }

        private void lOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (log_book == null || log_book.IsDisposed)
                log_book = new LOG(this);

            log_book.BringToFront();
            log_book.Show();
        }

        private void lblRTTYMark_Click(object sender, EventArgs e)
        {
            if (lblRTTYMark.ForeColor == Color.White)
            {
                lblRTTYMark.ForeColor = Color.Red;
                SetupForm.chkMarkOnly.Checked = true;
            }
            else
            {
                lblRTTYMark.ForeColor = Color.White;
                SetupForm.chkMarkOnly.Checked = false;
            }
        }

        private void chkAFC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAFC.Checked)
                {
                    chkAFC.BackColor = Color.LimeGreen;
                    psk.AFC = true;
                }
                else
                {
                    chkAFC.BackColor = Color.WhiteSmoke;
                    psk.AFC = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void CWExpert_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    int w = this.Width - grpGenesisRadio.Width - 45;
                    double q = w / 1.454;
                    double a = q / 2.2;
                    grpDisplay.Width = (int)q;
                    picPanadapter.Width = grpDisplay.Width - 35;
                    picWaterfall.Width = picPanadapter.Width;
                    grpLogBook.Width = picPanadapter.Width;
                    grpMonitor.Width = grpLogBook.Width - (grpMonitor.Location.X + 10);
                    grpChannels.Width = (int)a;
                    grpMRChannels.Width = (int)a;

                    ArrayList list = new ArrayList();
                    Control.ControlCollection c2 = grpMRChannels.Controls;
                    int width = grpMRChannels.Width - 28;

                    foreach (Control c in c2)
                    {
                        if (c.GetType() == typeof(TextBox))
                        {
                            c.Width = width;
                            c.Font = this.Font;
                        }
                    }

                    picMonitor.Width = grpMonitor.Width - 20;
                    Point loc = new Point(0, 0);
                    loc.X = 8;
                    loc.Y = grpChannels.Location.Y;
                    grpChannels.Location = loc;
                    loc.X += grpChannels.Width + 7;
                    loc.Y = grpGenesisRadio.Location.Y;
                    grpGenesisRadio.Location = loc;
                    grpMorseRunner.Location = loc;
                    loc.X += grpGenesisRadio.Width + 7;
                    grpDisplay.Location = loc;
                    rtbCH1.Width = grpChannels.Width - 22;
                    rtbCH2.Width = grpChannels.Width - 22;
                    loc.X = (grpChannels.Width + 15) / 2 - grpSMeter.Width / 2;
                    loc.Y = grpSMeter.Location.Y;
                    grpSMeter.Location = loc;
                    loc.X = (grpMRChannels.Width + 15) / 2 - grpMorseRunner2.Width / 2;
                    loc.Y = grpMorseRunner2.Location.Y;
                    grpMorseRunner2.Location = loc;
                    loc.X = btnCH1.Location.X;
                    loc.Y = btnCH1.Location.Y;
                    w = (grpChannels.Width - btnCH1.Width - btnCH2.Width - btnRX2On.Width - btnClearCH1.Width -
                        btnClearCH2.Width - btnLOG.Width - 38) / 5;
                    loc.X += btnCH1.Width + w;
                    btnClearCH1.Location = loc;
                    loc.X += btnClearCH1.Width + w;
                    btnLOG.Location = loc;
                    loc.X += btnLOG.Width + w;
                    btnRX2On.Location = loc;
                    loc.X += btnRX2On.Width + w;
                    btnClearCH2.Location = loc;
                    loc.X += btnClearCH2.Width + w;
                    btnCH2.Location = loc;
                    loc = lblFilterwidth.Location;
                    loc.X = picPanadapter.Location.X;
                    w = (picPanadapter.Width - (lblFilterwidth.Width + txtFilterWidth.Width + tbFilterWidth.Width +
                        txtFreq.Width + lblZoom.Width + tbZoom.Width + lblPan.Width + tbPan.Width)) / 9;
                    loc.X += w;
                    lblFilterwidth.Location = loc;
                    loc.X += lblFilterwidth.Width + w;
                    txtFilterWidth.Location = loc;
                    loc.X += txtFilterWidth.Width + w;
                    tbFilterWidth.Location = loc;
                    loc.X += tbFilterWidth.Width + w;
                    txtFreq.Location = loc;
                    loc.X += txtFreq.Width + w;
                    lblZoom.Location = loc;
                    loc.X += lblZoom.Width + w;
                    tbZoom.Location = loc;
                    loc.X += tbZoom.Width + w;
                    lblPan.Location = loc;
                    loc.X += lblPan.Width + w;
                    tbPan.Location = loc;

                    if (video_driver == DisplayDriver.DIRECTX)
                    {
#if DirectX
                        DX.DirectXRelease();
                        DX.DirectXInit();
                        DirectXRelease();
                        DirectXInit();
#endif
                    }
                    else
                    {
                        Display_GDI.Target = picPanadapter;

                        if (picMonitor_bmp != null)
                            picMonitor_bmp.Dispose();

                        picMonitor_bmp = new Bitmap(picMonitor.Width, picMonitor.Height,
                            System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    }
                }

                picMonitor_bmp = new Bitmap(picMonitor.Width, picMonitor.Height,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region DirectX

#if DirectX

        private void DirectXInit()
        {
            try
            {
                SMeter.displayEngine = AnalogGAuge.AGauge.DisplayDriver.DIRECTX;
                DX.MainForm = this;
                SMeter.GaugeTarget = SMeter;
                SMeter.DirectX_Init(Application.StartupPath + "\\SMeter.jpg");

                monitor_target = picMonitor;
                PresentParameters presentParms = new PresentParameters();
                presentParms.Windowed = true;
                presentParms.SwapEffect = SwapEffect.Discard;
                presentParms.BackBufferFormat = SlimDX.Direct3D9.Format.Unknown;
                presentParms.BackBufferHeight = monitor_target.Height;
                presentParms.BackBufferWidth = monitor_target.Width;
                presentParms.BackBufferCount = 1;
                presentParms.BackBufferFormat = Format.X8R8G8B8;

                try
                {
                    if (DX.DirectXRenderType == RenderType.HARDWARE)
                        waterfall_dx_device = new Device(new Direct3D(), 0,
                            DeviceType.Hardware, monitor_target.Handle,
                            CreateFlags.HardwareVertexProcessing | CreateFlags.FpuPreserve |
                             CreateFlags.Multithreaded, presentParms);
                    else
                        waterfall_dx_device = new Device(new Direct3D(), 0,
                            DeviceType.Hardware, monitor_target.Handle,
                            CreateFlags.SoftwareVertexProcessing | CreateFlags.FpuPreserve |
                            CreateFlags.Multithreaded, presentParms);
                }
                catch (Direct3D9Exception ex)
                {
                    MessageBox.Show("DirectX hardware init error!\n" + ex.ToString());

                    try
                    {
                        waterfall_dx_device = new Device(new Direct3D(), 0,
                            DeviceType.Hardware, monitor_target.Handle,
                            CreateFlags.SoftwareVertexProcessing | CreateFlags.FpuPreserve, presentParms);

                    }
                    catch (Direct3D9Exception exe)
                    {
                        MessageBox.Show("DirectX software init error!\n" + exe.ToString());
                    }
                }

                DX.PanadapterTarget = picPanadapter;
                DX.WaterfallTarget = picWaterfall;
                DX.WaterfallInit();
                DX.DirectXInit();

                waterfall_bmp = new System.Drawing.Bitmap(monitor_target.Width, monitor_target.Height,
                    System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                BitmapData bitmapData = waterfall_bmp.LockBits(
                    new Rectangle(0, 0, waterfall_bmp.Width, waterfall_bmp.Height),
                    ImageLockMode.ReadWrite, waterfall_bmp.PixelFormat);

                waterfall_bmp_size = bitmapData.Stride * waterfall_bmp.Height;
                waterfall_bmp_stride = bitmapData.Stride;
                waterfall_memory = new byte[waterfall_bmp_size];
                waterfall_bmp.UnlockBits(bitmapData);
                waterfall_rect = new Rectangle(0, 0, monitor_target.Width, monitor_target.Height);
                backbuf = waterfall_dx_device.GetBackBuffer(0, 0);

                if (File.Exists(DX.background_image))
                {
                    WaterfallTexture = Texture.FromFile(waterfall_dx_device, DX.background_image, monitor_target.Width, monitor_target.Height,
                        1, Usage.None, Format.X8R8G8B8, Pool.Managed, SlimDX.Direct3D9.Filter.Default, SlimDX.Direct3D9.Filter.Default, 0);
                    Waterfall_texture_size.Width = monitor_target.Width;
                    Waterfall_texture_size.Height = monitor_target.Height;
                    Waterfall_Sprite = new Sprite(waterfall_dx_device);

                    PanadapterTexture = Texture.FromFile(waterfall_dx_device, DX.background_image, monitor_target.Width, monitor_target.Height,
                        1, Usage.None, Format.X8R8G8B8, Pool.Managed, SlimDX.Direct3D9.Filter.Default, SlimDX.Direct3D9.Filter.Default, 0);
                    Panadapter_texture_size.Width = monitor_target.Width;
                    Panadapter_texture_size.Height = monitor_target.Height;
                    Panadapter_Sprite = new Sprite(waterfall_dx_device);
                }
                else
                {
                    Panadapter_Sprite = null;
                    WaterfallTexture = new Texture(waterfall_dx_device, monitor_target.Width, monitor_target.Height, 0,
                        Usage.None, Format.X8R8G8B8, Pool.Managed);
                    Waterfall_texture_size.Width = monitor_target.Width;
                    Waterfall_texture_size.Height = monitor_target.Height;
                    Waterfall_Sprite = new Sprite(waterfall_dx_device);
                }

                var vertexElems = new[] {
                        new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.PositionTransformed, 0),
                        new VertexElement(0, 16, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                        VertexElement.VertexDeclarationEnd
                        };

                var vertexDecl1 = new VertexDeclaration(waterfall_dx_device, vertexElems);
                waterfall_dx_device.VertexDeclaration = vertexDecl1;

                panadapterX_data = new float[picMonitor.Width];
                panadapterX_scope_data = new float[picMonitor.Width * 2];
                panadapterX_scope_data_mark = new float[picMonitor.Width * 2];
                panadapterX_scope_data_space = new float[picMonitor.Width * 2];
                ScopeLine_vb = new VertexBuffer(waterfall_dx_device, picMonitor.Width * 2 * 20, Usage.WriteOnly,
                    VertexFormat.None, Pool.Managed);
                ScopeLine_verts = new Vertex[picMonitor.Width * 2];

                PanLine_vb = new VertexBuffer(waterfall_dx_device, panadapterX_data.Length * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
                PanLine_vb_fill = new VertexBuffer(waterfall_dx_device, picMonitor.Width * 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
                PanLine_verts = new Vertex[picMonitor.Width];
                PanLine_verts_fill = new Vertex[picMonitor.Width * 2];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void DirectXReInit()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void DirectXRelease()
        {
            try
            {
                if (!booting)
                {
                    /*waterfallX_data = null;
                    panadapterX_data = null;
                    new_display_data = null;
                    new_waterfall_data = null;
                    current_display_data = null;
                    current_waterfall_data = null;
                    waterfall_display_data = null;
                    average_buffer = null;
                    average_waterfall_buffer = null;
                    peak_buffer = null;*/

                    if (waterfall_bmp != null)
                        waterfall_bmp.Dispose();
                    waterfall_bmp = null;
                    if (Panadapter_Sprite != null)
                        Panadapter_Sprite.Dispose();
                    Panadapter_Sprite = null;
                    if (Waterfall_Sprite != null)
                        Waterfall_Sprite.Dispose();
                    Waterfall_Sprite = null;

                    if (PanadapterTexture != null)
                    {
                        PanadapterTexture.Dispose();
                        PanadapterTexture = null;
                    }

                    if (WaterfallTexture != null)
                    {
                        WaterfallTexture.Dispose();
                        WaterfallTexture = null;
                    }

                    /*if (VerLine_vb != null)
                    {
                        VerLine_vb.Dispose();
                        VerLine_vb = null;
                    }

                    if (VerLines_vb != null)
                    {
                        VerLines_vb.Dispose();
                        VerLines_vb = null;
                    }

                    if (HorLine_vb != null)
                    {
                        HorLine_vb.Dispose();
                        HorLine_vb = null;
                    }

                    if (HorLines_vb != null)
                    {
                        HorLines_vb.Dispose();
                        HorLines_vb = null;
                    }

                    if (PanLine_vb != null)
                    {
                        PanLine_vb.Dispose();
                        PanLine_vb.Dispose();
                    }

                    if (PanLine_vb_fill != null)
                    {
                        PanLine_vb_fill.Dispose();
                        PanLine_vb_fill.Dispose();
                    }

                    if (vertical_label != null)
                        vertical_label = null;

                    if (horizontal_label != null)
                        horizontal_label = null;

                    if (Phase_vb != null)
                    {
                        Phase_vb.Dispose();
                        Phase_vb.Dispose();
                    }*/

                    if (panadapter_dx_device != null)
                    {
                        panadapter_dx_device.Dispose();
                        panadapter_dx_device = null;
                    }

                    if (waterfall_dx_device != null)
                    {
                        waterfall_dx_device.Dispose();
                        waterfall_dx_device = null;
                    }

                    foreach (var item in ObjectTable.Objects)
                        item.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

#endif
        #endregion

        #region Monitor context menu

        private void waterfallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monitor_mode = DisplayMode.WATERFALL;
        }

        private void scopeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monitor_mode = DisplayMode.SCOPE;
        }

        private void panadapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monitor_mode = DisplayMode.PANADAPTER;
        }
        #endregion

        #region SMeter context menu

        private void pWERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TX_meter_type = MeterType.DIR_PWR;
        }

        private void reflPWRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TX_meter_type = MeterType.REFL_PWR;
        }

        private void sWRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TX_meter_type = MeterType.SWR;
        }

        #endregion

        #region AGC

        AGCMode agc_mode = AGCMode.LONG;
        public void SetAGC(AGCMode newmode)
        {
            try
            {
                agc_mode = newmode;
                SetRXAGC(0, 0, newmode);
                SetRXAGC(0, 1, newmode);
                SetRXAGC(0, 2, newmode);
                SetRXAGC(0, 3, newmode);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region ROBOT

        bool IsNumber(string test)
        {
            try
            {
                bool result = false;

                foreach (char q in test)
                {
                    switch (q)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            result = true;
                            break;

                        default:
                            result = false;
                            goto end;
                        //break;
                    }
                }

            end:
                return result;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        bool IsLetter(char test)
        {
            try
            {
                bool result = false;
                int q = (int)test;

                if ((q >= 65 && q <= 90 || (q >= 97 && q <= 122)))      // a-z A-Z
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        bool IsLocator(string test)
        {
            try
            {
                bool result = false;

                if (test.Length != 6)
                    goto end;

                char[] loc = test.ToCharArray();

                if (!IsLetter(loc[0]) && !IsLetter(loc[1]))
                    goto end;

                if (!IsNumber(loc[2].ToString()) && !IsNumber(loc[3].ToString()))
                    goto end;

                if (!IsLetter(loc[4]) && !IsLetter(loc[5]))
                    goto end;

                result = true;

            end:
                return result;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        bool IsCALL(string test)
        {
            try
            {
                bool result = false;

                if (test.Length != 6)
                    goto end;

                char[] loc = test.ToCharArray();

                if (!IsLetter(loc[0]) && !IsLetter(loc[1]))
                    goto end;

                if (!IsNumber(loc[2].ToString()) && !IsNumber(loc[3].ToString()))
                    goto end;

                if (!IsLetter(loc[4]) && !IsLetter(loc[5]))
                    goto end;

                result = true;

            end:
                return result;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        #endregion

        #region Options menu

        private void recorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recorder == null || recorder.IsDisposed)
                recorder = new Recorder(this);

            recorder.Show();
            recorder.BringToFront();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KeyboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (keyboard == null || keyboard.IsDisposed)
                keyboard = new Keyboard(this);

            keyboard.Show();
            keyboard.BringToFront();
        }

        private void setupMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetupForm != null || SetupForm.IsDisposed)
                    SetupForm.Show();
                else
                {
                    SetupForm = new Setup(this);
                    SetupForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Setup!\n" + ex.ToString());
            }
        }

        #endregion

        #region DXCC

        private void dXCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dxcc == null || dxcc.IsDisposed)
                {
                    dxcc = new DXCC();
                    dxcc.Init(SetupForm.txtStnLOC.Text.ToString());
                }

                dxcc.Show();
                dxcc.BringToFront();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }
}
