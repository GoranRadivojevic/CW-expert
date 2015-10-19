//=================================================================
// RTTY encoder/decodec
//=================================================================
// Copyright (C) 2011,2012 S56A YT7PWR
//
// This file is part of CWExpert.Adapted from code contained in fldigi
// source code distribution.
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
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace CWExpert
{
    unsafe public class RTTY
    {
        #region enum

        public enum trx_state_t
        {
            TRX_STATE_PAUSE = 0,
            TRX_STATE_RX,
            TRX_STATE_TX,
            TRX_STATE_TUNE,
            TRX_STATE_ABORT,
            TRX_STATE_FLUSH
        }

        public enum rtty_rx_state_t
        {
            RTTY_RX_STATE_IDLE = 0,
            RTTY_RX_STATE_START,
            RTTY_RX_STATE_DATA,
            RTTY_RX_STATE_PARITY,
            RTTY_RX_STATE_STOP,
            RTTY_RX_STATE_STOP2
        }

        public enum parity_t
        {
            PARITY_NONE = 0,
            PARITY_EVEN,
            PARITY_ODD,
            PARITY_ZERO,
            PARITY_ONE
        }

        #endregion

        #region DLL import

        [DllImport("Receiver.dll", EntryPoint = "StartTimer")]
        public static extern void TimerInit(int period);

        [DllImport("Receiver.dll", EntryPoint = "StopTimer")]
        public static extern void TimerStop();

        [DllImport("Receiver.dll", EntryPoint = "TimerWait")]
        public static extern void TimerWait();

        [DllImport("kernel32.dll", EntryPoint = "EnterCriticalSection")]
        public static extern void EnterCriticalSection(void* cs_ptr);

        [DllImport("kernel32.dll", EntryPoint = "LeaveCriticalSection")]
        public static extern void LeaveCriticalSection(void* cs_ptr);

        [DllImport("kernel32.dll", EntryPoint = "InitializeCriticalSection")]
        public static extern void InitializeCriticalSection(void* cs_ptr);

        [DllImport("kernel32.dll", EntryPoint = "InitializeCriticalSectionAndSpinCount")]
        public static extern int InitializeCriticalSectionAndSpinCount(void* cs_ptr, uint spincount);

        [DllImport("kernel32.dll", EntryPoint = "DeleteCriticalSection")]
        public static extern byte DeleteCriticalSection(void* cs_ptr);

        [DllImport("Receiver.dll", EntryPoint = "NewCriticalSection")]
        public static extern void* NewCriticalSection();

        [DllImport("Receiver.dll", EntryPoint = "DestroyCriticalSection")]
        public static extern void DestroyCriticalSection(void* cs_ptr);

        #endregion

        #region variable

        delegate void CrossThreadCallback(string command, string data);
        delegate void CrossThreadSetText(string command, int channel_no, string out_txt);

        public TRX trx;
        private float[,] Mag_mark;
        public float[,] Mag_space;
        public float[] ch1_buffer;
        public float[] ch2_buffer;
        public float[] ch3_buffer;
        public float[] ch4_buffer;
        public float[] ch1_buf;
        public float[] ch2_buf;
        public float[] ch3_buf;
        public float[] ch4_buf;
        private static ComplexF[] zero_buffer = new ComplexF[2048];
        private CWExpert MainForm;
        private const int FILTERS_NUMBER = 2;
        int MaxSymLen = 12 * 1024;
        int BAUDOT_LETS = 0x100;
        int BAUDOT_FIGS = 0x200;
        private double M_PI = 3.14159265358928;
        private double TWOPI = 2.0 * 3.14159265358928;
        public Mutex update_receiver = new Mutex();
        public Mutex update_filter = new Mutex();
        public Mutex receive_mutex;
        private AutoResetEvent receive_event = new AutoResetEvent(false);
        public Thread RTTYThread1;
        public Thread RTTYThread2;
        public AutoResetEvent AudioEventRX1;
        public AutoResetEvent AudioEventRX2;
        public bool run_thread = false;
        public double sql = 1.0;
        private HiPerfTimer display_timer;
        public string output_ch1;
        public string output_ch2;
        public int index_ch1 = 0;
        public int index_ch2 = 0;
        private Mutex update_mutex = new Mutex();
        private ComplexF[] iq_buffer;
        public bool run_transmiter = false;
        private Thread tx_thread;
        delegate void CrossThreadCommand(string command, int param_1, string param_2);
        private string message = "";
        private const int MODEM_NR = 2;
        public bool run_rx1 = false;
        public bool run_rx2 = false;
        unsafe private static void* cs_audio;
        private AutoResetEvent audio_event = new AutoResetEvent(false);
        public bool update_trx1 = false;
        public bool update_trx2 = false;

        char[] letters = {
	        ' ',	'E',	'\n',	'A',	' ',	'S',	'I',	'U',
	        '\r',	'D',	'R',	'J',	'N',	'F',	'C',	'K',
	        'T',	'Z',	'L',	'W',	'H',	'Y',	'P',	'Q',
	        'O',	'B',	'G',	'·',	'M',	'X',	'V',	'·'
                         };

        char[] figures = {
	        ' ',	'3',	'\n',	'-',	' ',	'\'',	'8',	'7',
	        '\r',	'$',	'4',	'\a',	',',	'!',	':',	'(',
	        '5',	'+',	')',	'2',	'#',	'6',	'0',	'1',
	        '9',	'?',	'&',	'·',	'.',	'/',	'=',	'·'
                         };

        static char[] msg1 = new char[20];
        double[] SHIFT = { 23, 85, 160, 170, 182, 200, 240, 350, 425, 850 };
        double[] BAUD = { 45, 45.45, 50, 56, 75, 100, 110, 150, 200, 300 };
        int[] BITS = { 5, 7, 8 };
        public RingBufferChar RBmessage = new RingBufferChar(2048);

        #endregion

        #region structures

        [StructLayout(LayoutKind.Sequential)]
        public struct TRX
        {
            public trx_state_t state;
            public int samplerate;
            public int afcon;
            public int squelchon;
            public int stopflag;
            public bool tune;
            public float mon_frequency;
            public float bandwidth;
            public float metric;
            public float txoffset;
            public int backspaces;
            public rtty[] modem;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct rtty
        {
            public ComplexF[] outbuf;
            public ComplexF[] mon_outbuf;
            public double shift;
            public int rx_symbollen;
            public int nbits;
            public int paritybits;
            public parity_t parity;
            public int stopbits;
            public bool reverse;
            public int msb;
            public double baud;
            public double phaseacc;
            public ComplexF[] z0;
            public double freqerr;
            // RX related stuff
            public int rx_stoplen;
            public int pipeptr;
            public int filterptr;
            public rtty_rx_state_t rxstate;
            public int counter;
            public int bitcntr;
            public int rxdata;
            public double prevsymbol;
            public int rxmode;
            public double rx_frequency;
            public double[] bbfilter;
            //TX related stuff
            public int txmode;
            public int preamble;
            public float tx_phase;
            public float tx_gain;
            public int tx_symbollen;
            public double tx_phaseacc;
            public double mon_phaseacc;
            public int tx_stoplen;
            public double tx_frequency;
        };

        #endregion

        #region properties

        private bool mox = false;
        public bool MOX
        {
            get { return mox; }
            set { mox = value; }
        }

        private bool sql_on = false;
        public bool SqlOn
        {
            get { return sql_on; }
            set { sql_on = value; }
        }

        private bool mark_only = false;
        public bool MarkOnly
        {
            get { return mark_only; }
            set { mark_only = value; }
        }

        private bool rx2_enabled = false;
        public bool RX2Enabled
        {
            get { return rx2_enabled; }
            set { rx2_enabled = value; }
        }

        private int tx_preamble = 20;
        public int TXPreamble
        {
            get { return tx_preamble; }
            set
            {
                tx_preamble = value;
                trx.modem[0].preamble = value;
                trx.modem[1].preamble = value;
            }
        }

        public float TXPhase
        {
            set
            {
                try
                {
                    trx.modem[0].tx_phase = 0.001f * value;
                    trx.modem[1].tx_phase = 0.001f * value;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
        }
        public float TXGain
        {
            set 
            {
                try
                {
                    trx.modem[0].tx_gain = (1.0f + 0.001f * value);
                    trx.modem[1].tx_gain = (1.0f + 0.001f) * value;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }
            }
        }

        private double tx_if_shift = 15000.0;
        public double TXIfShift
        {
            get { return tx_if_shift; }
            set 
            {
                tx_if_shift = value;
                trx.modem[0].tx_frequency = (float)tx_if_shift;
                trx.modem[1].tx_frequency = (float)tx_if_shift;
            }
        }

        private double mon_frequency = 700.0;
        public double MonFreq
        {
            set
            {
                mon_frequency = value;
                trx.mon_frequency = (float)mon_frequency;
            }
        }

        #endregion

        #region constructor/destructor

        public RTTY(CWExpert form)
        {
            MainForm = form;
            output_ch1 = new string(' ', 1);
            output_ch2 = new string(' ', 1);
            AudioEventRX1 = new AutoResetEvent(false);
            AudioEventRX2 = new AutoResetEvent(false);
            iq_buffer = new ComplexF[2048];
            ch1_buffer = new float[2048];
            ch2_buffer = new float[2048];
            ch3_buffer = new float[2048];
            ch4_buffer = new float[2048];
            ch1_buf = new float[2048];
            ch2_buf = new float[2048];
            ch3_buf = new float[2048];
            ch4_buf = new float[2048];
            Mag_mark = new float[2, 2048];
            Mag_space = new float[2, 2048];
            display_timer = new HiPerfTimer();
            trx = new TRX();
            trx.modem = new rtty[MODEM_NR];

            cs_audio = (void*)0x0;
            cs_audio = NewCriticalSection();

            if (InitializeCriticalSectionAndSpinCount(cs_audio, 0x00000080) == 0)
            {
                MessageBox.Show("CriticalSection Failed", "Error!");
            }

            for (int i = 0; i < 2048; i++)
            {
                zero_buffer[i].Re = 0.0f;
                zero_buffer[i].Im = 0.0f;
            }
        }

        ~RTTY()
        {
            try
            {
                if (cs_audio != null)
                {
                    DeleteCriticalSection(cs_audio);
                    DestroyCriticalSection(cs_audio);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Start/Stop

        public bool RTTYStart()
        {
            bool result = false;

            try
            {
                if (Init())
                {
                    run_thread = true;
                    RTTYThread1 = new Thread(new ThreadStart(RTTY_ThreadRX1));
                    RTTYThread1.Name = "RTTY Thread RX1";
                    RTTYThread1.Priority = ThreadPriority.Normal;
                    RTTYThread1.IsBackground = true;
                    RTTYThread1.Start();

                    RTTYThread2 = new Thread(new ThreadStart(RTTY_ThreadRX2));
                    RTTYThread2.Name = "RTTY Thread RX2";
                    RTTYThread2.Priority = ThreadPriority.Normal;
                    RTTYThread2.IsBackground = true;
                    RTTYThread2.Start();
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public void RTTYStop()
        {
            try
            {
                audio_event.Set();
                run_transmiter = false;
                run_thread = false;
                AudioEventRX1.Set();
                AudioEventRX2.Set();
                Thread.Sleep(100);
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region RTTY control

        public bool Init()
        {
            try
            {
                rtty_init();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        int frame_segment = 32;
        private void rtty_init()
        {
            TXIfShift = (double)MainForm.SetupForm.udTXIfShift.Value;
            MonFreq = (double)MainForm.SetupForm.udMonitorFrequncy.Value;
            trx.samplerate = Audio.SampleRate;

            for (int i = 0; i < MODEM_NR; i++)
            {
                trx.modem[i].bbfilter = new double[4096];
                trx.modem[i].shift = MainForm.SetupForm.shift;
                trx.modem[i].tx_frequency = (float)tx_if_shift;
                trx.modem[i].reverse = MainForm.radRTTYReverse.Checked;
                trx.afcon = 1;          // AFC
                trx.modem[i].baud = MainForm.SetupForm.baudrate;
                trx.modem[i].nbits = MainForm.SetupForm.bits;
                trx.modem[i].paritybits = MainForm.SetupForm.parity;
                trx.modem[i].stopbits = MainForm.SetupForm.stopbits;
                trx.modem[i].rx_symbollen = (int)(Audio.SampleRate / trx.modem[i].baud) / frame_segment;
                trx.modem[i].tx_symbollen = (int)(Audio.SampleRate / trx.modem[i].baud);
                trx.modem[i].tx_phase = 0.001f * (float)MainForm.SetupForm.udTXPhase.Value;
                trx.modem[i].tx_gain = 1 + 0.001f * (float)MainForm.SetupForm.udTXGain.Value;
                trx.modem[i].rxmode = BAUDOT_LETS;
                trx.modem[i].txmode = BAUDOT_LETS;
                trx.modem[i].preamble = 20;
                trx.modem[i].rx_frequency = MainForm.RTTYPitch;
                trx.modem[i].z0 = new ComplexF[2048];

                switch (trx.modem[i].nbits)
                {
                    case 5:
                        trx.modem[i].nbits = 5;
                        break;
                    case 7:
                        trx.modem[i].nbits = 7;
                        break;
                    case 8:
                        trx.modem[i].nbits = 8;
                        break;
                }

                switch (trx.modem[i].paritybits)
                {
                    case 0:
                        trx.modem[i].parity = parity_t.PARITY_NONE;
                        break;
                    case 1:
                        trx.modem[i].parity = parity_t.PARITY_EVEN;
                        break;
                    case 2:
                        trx.modem[i].parity = parity_t.PARITY_ODD;
                        break;
                    case 3:
                        trx.modem[i].parity = parity_t.PARITY_ZERO;
                        break;
                    case 4:
                        trx.modem[i].parity = parity_t.PARITY_ONE;
                        break;
                }

                switch (trx.modem[i].stopbits)
                {
                    case 0:
                        trx.modem[i].rx_stoplen = (int)(1.0 * trx.modem[i].rx_symbollen);
                        trx.modem[i].tx_stoplen = (int)(1.0 * trx.modem[i].tx_symbollen);
                        break;
                    case 1:
                        trx.modem[i].rx_stoplen = (int)(1.5 * trx.modem[i].rx_symbollen);
                        trx.modem[i].tx_stoplen = (int)(1.5 * trx.modem[i].tx_symbollen);
                        break;
                    case 2:
                        trx.modem[i].rx_stoplen = (int)(2.0 * trx.modem[i].rx_symbollen);
                        trx.modem[i].tx_stoplen = (int)(2.0 * trx.modem[i].tx_symbollen);
                        break;
                }

                if (trx.modem[i].rx_symbollen > MaxSymLen || trx.modem[i].rx_stoplen > MaxSymLen)
                {
                    Debug.Write("RTTY: symbol length too big\n");
                    return;
                }

                trx.modem[i].outbuf = new ComplexF[trx.modem[i].tx_symbollen * 2];
                trx.modem[i].mon_outbuf = new ComplexF[trx.modem[i].tx_symbollen * 2];
                update_trx1 = false;
                update_trx2 = false;
            }
        }

        private void rtty_reload(int index)
        {
            TXIfShift = (double)MainForm.SetupForm.udTXIfShift.Value;
            MonFreq = (double)MainForm.SetupForm.udMonitorFrequncy.Value;
            trx.samplerate = Audio.SampleRate;
            int i = index;

            trx.modem[i].bbfilter = new double[4096];
            trx.modem[i].shift = MainForm.SetupForm.shift;
            trx.modem[i].tx_frequency = (float)tx_if_shift;
            trx.modem[i].reverse = MainForm.radRTTYReverse.Checked;
            trx.modem[i].baud = MainForm.SetupForm.baudrate;
            trx.modem[i].nbits = MainForm.SetupForm.bits;
            trx.modem[i].paritybits = MainForm.SetupForm.parity;
            trx.modem[i].stopbits = MainForm.SetupForm.stopbits;
            trx.modem[i].rx_symbollen = (int)(Audio.SampleRate / trx.modem[i].baud) / frame_segment;
            trx.modem[i].tx_symbollen = (int)(Audio.SampleRate / trx.modem[i].baud);
            trx.modem[i].tx_phase = 0.001f * (float)MainForm.SetupForm.udTXPhase.Value;
            trx.modem[i].tx_gain = 1 + 0.001f * (float)MainForm.SetupForm.udTXGain.Value;
            trx.modem[i].rxmode = BAUDOT_LETS;
            trx.modem[i].txmode = BAUDOT_LETS;
            trx.modem[i].preamble = 20;
            trx.modem[i].rx_frequency = MainForm.RTTYPitch;
            trx.modem[i].z0 = new ComplexF[2048];

            switch (trx.modem[i].nbits)
            {
                case 5:
                    trx.modem[i].nbits = 5;
                    break;
                case 7:
                    trx.modem[i].nbits = 7;
                    break;
                case 8:
                    trx.modem[i].nbits = 8;
                    break;
            }

            switch (trx.modem[i].paritybits)
            {
                case 0:
                    trx.modem[i].parity = parity_t.PARITY_NONE;
                    break;
                case 1:
                    trx.modem[i].parity = parity_t.PARITY_EVEN;
                    break;
                case 2:
                    trx.modem[i].parity = parity_t.PARITY_ODD;
                    break;
                case 3:
                    trx.modem[i].parity = parity_t.PARITY_ZERO;
                    break;
                case 4:
                    trx.modem[i].parity = parity_t.PARITY_ONE;
                    break;
            }

            switch (trx.modem[i].stopbits)
            {
                case 0:
                    trx.modem[i].rx_stoplen = (int)(1.0 * trx.modem[i].rx_symbollen);
                    trx.modem[i].tx_stoplen = (int)(1.0 * trx.modem[i].tx_symbollen);
                    break;
                case 1:
                    trx.modem[i].rx_stoplen = (int)(1.5 * trx.modem[i].rx_symbollen);
                    trx.modem[i].tx_stoplen = (int)(1.5 * trx.modem[i].tx_symbollen);
                    break;
                case 2:
                    trx.modem[i].rx_stoplen = (int)(2.0 * trx.modem[i].rx_symbollen);
                    trx.modem[i].tx_stoplen = (int)(2.0 * trx.modem[i].tx_symbollen);
                    break;
            }

            if (trx.modem[i].rx_symbollen > MaxSymLen || trx.modem[i].rx_stoplen > MaxSymLen)
            {
                Debug.Write("RTTY: symbol length too big\n");
                return;
            }

            trx.modem[i].outbuf = new ComplexF[trx.modem[i].tx_symbollen * 2];
            trx.modem[i].mon_outbuf = new ComplexF[trx.modem[i].tx_symbollen * 2];
        }

        #endregion

        #region parity

        int rtty_parity(int c, int nbits, parity_t par)
        {
            c &= (1 << nbits) - 1;

            switch ((parity_t)par)
            {
                default:
                case parity_t.PARITY_NONE:
                    return 0;

                case parity_t.PARITY_ODD:
                    return parity(c);

                case parity_t.PARITY_EVEN:
                    return ~parity(c);

                case parity_t.PARITY_ZERO:
                    return 0;

                case parity_t.PARITY_ONE:
                    return 1;
            }
        }

        int parity(int w)
        {
            return hweight32(w) & 1;
        }

        int hweight32(int w)
        {
            int res = w - ((w >> 1) & 0x55555555);
            res = (res & 0x33333333) + ((res >> 2) & 0x33333333);
            res = (res + (res >> 4)) & 0x0F0F0F0F;
            res = res + (res >> 8);
            return (res + (res >> 16)) & 0x000000FF;
        }
        #endregion

        #region budot

        private char baudot_enc(char data)
        {
            int i, c, mode;

            mode = 0;
            c = -1;

            if (Char.IsLower(data))
                data = Char.ToUpper(data);

            for (i = 0; i < 32; i++)
            {
                if (data == letters[i])
                {
                    mode |= BAUDOT_LETS;
                    c = i;
                }

                if (data == figures[i])
                {
                    mode |= BAUDOT_FIGS;
                    c = i;
                }

                if (c != -1)
                    return (char)(mode | c);
            }

            return ' ';
        }

        private char baudot_dec(int index, char data)
        {
            int output = 0;

            switch (data)
            {
                case (char)0x1F:		/* letters */
                    trx.modem[index].rxmode = BAUDOT_LETS;
                    break;
                case (char)0x1B:		/* figures */
                    trx.modem[index].rxmode = BAUDOT_FIGS;
                    break;
                case (char)0x04:		/* unshift-on-space */
                    trx.modem[index].rxmode = BAUDOT_LETS;
                    return ' ';
                default:
                    if (trx.modem[index].rxmode == BAUDOT_LETS)
                        output = letters[data];
                    else
                        output = figures[data];
                    break;
            }

            return (char)output;
        }

        #endregion

        #region RX demodulator

        private char bitreverse(char input, int n)
        {
            char output = '0';
            int i;

            for (i = 0; i < n; i++)
                output = (char)((output << 1) | ((input >> i) & 1));

            return output;
        }

        private char decode_char(int index)
        {
            int parbit, par;
            char data;

            parbit = (trx.modem[index].rxdata >> trx.modem[index].nbits) & 1;
            par = rtty_parity(trx.modem[index].rxdata, trx.modem[index].nbits, trx.modem[index].parity);

            if (trx.modem[index].parity != parity_t.PARITY_NONE && parbit != par)
            {
                return ' ';
            }

            data = (char)(trx.modem[index].rxdata & ((1 << trx.modem[index].nbits) - 1));

            if (trx.modem[index].msb > 0)
                data = bitreverse(data, trx.modem[index].nbits);

            if (trx.modem[index].nbits == 5)
                return baudot_dec(index, (char)data);

            return data;
        }

        //bool bit_started = false;
        private int rttyrx(int index, bool bit)
        {
            int flag = 0;
            char c;

            switch (trx.modem[index].rxstate)
            {
                case rtty_rx_state_t.RTTY_RX_STATE_IDLE:
                    if (!bit)
                    {
                        trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_START;
                        trx.modem[index].counter = trx.modem[index].rx_symbollen / 2;
                    }
                    break;

                case rtty_rx_state_t.RTTY_RX_STATE_START:
                        if (--trx.modem[index].counter == 0)
                        {
                            if (!bit)
                            {
                                trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_DATA;
                                trx.modem[index].counter = trx.modem[index].rx_symbollen;
                                trx.modem[index].bitcntr = 0;
                                trx.modem[index].rxdata = 0;
                                flag = 1;
                            }
                            else
                                trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_IDLE;
                        }
                    else if(bit)
                        trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_IDLE;
                    break;

                case rtty_rx_state_t.RTTY_RX_STATE_DATA:
                    {
                        if (--trx.modem[index].counter == 0)
                        {
                            int q = 0;

                            if (bit)
                                q = 1;

                            //bit_started = false;
                            trx.modem[index].rxdata |= q << trx.modem[index].bitcntr++;
                            trx.modem[index].counter = trx.modem[index].rx_symbollen;
                            flag = 1;
                        }

                        if (trx.modem[index].bitcntr == trx.modem[index].nbits)
                        {
                            if (trx.modem[index].parity == parity_t.PARITY_NONE)
                                trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_STOP;
                            else
                                trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_PARITY;
                        }
                    }
                    break;

                case rtty_rx_state_t.RTTY_RX_STATE_PARITY:
                    if (--trx.modem[index].counter == 0)
                    {
                        int q = 0;

                        if (bit)
                            q = 1;

                        trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_STOP;
                        trx.modem[index].rxdata |= q << trx.modem[index].bitcntr++;
                        trx.modem[index].counter = trx.modem[index].rx_symbollen;
                        flag = 1;
                    }
                    break;

                case rtty_rx_state_t.RTTY_RX_STATE_STOP:
                    if (--trx.modem[index].counter == 0)
                    {
                        if (bit)
                        {
                            if (index == 0)
                            {
                                c = decode_char(index);

                                output_ch1 += c.ToString();
                                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", 5, c.ToString());
                                flag = 1;
                                output_ch1 = "";

                            }
                            else if (index == 1)
                            {
                                c = decode_char(index);
                                output_ch2 += c.ToString();
                                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", 6, c.ToString());
                                flag = 1;
                                output_ch2 = "";
                            }
                        }
                        //else
                        {

                        }

                        trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_STOP2;
                        trx.modem[index].counter = trx.modem[index].rx_symbollen / 2;
                    }
                    break;

                case rtty_rx_state_t.RTTY_RX_STATE_STOP2:
                    if (--trx.modem[index].counter == 0)
                        trx.modem[index].rxstate = rtty_rx_state_t.RTTY_RX_STATE_IDLE;
                    break;
            }

            return flag;
        }

        private int RXprocess(int index, int len)
        {
            int i;
            bool bit = true;
            bool rev = false;
            double thd = sql * Math.Sqrt(0.03);
            int mark;

            rev = trx.modem[index].reverse;

            for (i = 0; i < len; i++)
            {
                if (!rev)
                {
                    if (mark_only)
                    {
                        if (Mag_mark[index, i] > thd)
                            bit = true;
                        else
                            bit = false;
                    }
                    else
                    {
                        if (sql_on)
                        {
                            if (Mag_mark[index, i] > thd && Mag_mark[index, i] > Mag_space[index, i])
                                bit = true;
                            else
                                bit = false;
                        }
                        else
                        {
                            if (Mag_mark[index, i] > Mag_space[index, i])
                                bit = true;
                            else if (Mag_space[index, i] > Mag_mark[index, i])
                                bit = false;
                            else
                                bit = false;
                        }
                    }
                }
                else
                {
                    if (mark_only)
                    {
                        if (Mag_space[index, i] > thd)
                            bit = true;
                        else
                            bit = false;
                    }
                    else
                    {
                        if (sql_on)
                        {
                            if (Mag_space[index, i] > thd && Mag_space[index, i] > Mag_mark[index, i])
                                bit = true;
                            else
                                bit = false;
                        }
                        else
                        {
                            if (Mag_space[index, i] > Mag_mark[index, i])
                                bit = true;
                            else if (Mag_mark[index, i] > Mag_space[index, i])
                                bit = false;
                            else
                                bit = false;
                        }
                    }
                }

                mark = rttyrx(index, bit);

                if (!bit)
                {
                    MainForm.lblRTTYMarkBox.BackColor = Color.Red;
                    MainForm.lblRTTYSpaceBox.BackColor = Color.Green;
                }
                else
                {
                    MainForm.lblRTTYMarkBox.BackColor = Color.Green;
                    MainForm.lblRTTYSpaceBox.BackColor = Color.Red;
                }
            }

            return 0;
        }

        #endregion

        #region TX encoder

        public void Exchange_samples(int index, ref ComplexF[] output, ref ComplexF[] monitor, int buflen)
        {
            if (MainForm.output_ring_buf.ReadSpace() < buflen)
            {
                while (MainForm.output_ring_buf.ReadSpace() < buflen)
                {
                    if (run_transmiter)
                    {
                        audio_event.Set();
                        Thread.Sleep(1);
                    }
                    else
                        return;
                }
            }

            EnterCriticalSection(cs_audio);

            MainForm.output_ring_buf.Read(ref iq_buffer, buflen);
            correctIQ(index, ref iq_buffer);
            Array.Copy(iq_buffer, output, buflen);
            MainForm.mon_ring_buf.Read(ref iq_buffer, buflen);
            Array.Copy(iq_buffer, monitor, buflen);
            audio_event.Set();

            LeaveCriticalSection(cs_audio);
        }

        private void correctIQ(int index, ref ComplexF[] buffer)
        {
            int i;

            for (i = 0; i < 2048; i++)
            {
                buffer[i].Im += trx.modem[index].tx_phase * buffer[i].Re;
                buffer[i].Re *= trx.modem[index].tx_gain;
            }
        }

        private void send_symbol(int index, int symbol)
        {
            double freq, mon_freq;
            int i = 0;
            bool symb = false;

            if (Audio.TXswap)
            {
                if (trx.modem[index].reverse)
                {
                    if (symbol == 1)
                        symb = false;
                    else
                        symb = true;
                }
                else
                {
                    if (symbol == 1)
                        symb = true;
                    else
                        symb = false;
                }
            }
            else
            {
                if (trx.modem[index].reverse)
                {
                    if (symbol == 1)
                        symb = true;
                    else
                        symb = false;
                }
                else
                {
                    if (symbol == 1)
                        symb = false;
                    else
                        symb = true;
                }
            }

            if (Audio.TXswap)
            {
                if (symb)
                {
                    freq = trx.modem[index].tx_frequency + trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency + trx.modem[index].shift / 2.0;
                }
                else
                {
                    freq = trx.modem[index].tx_frequency - trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency - trx.modem[index].shift / 2.0;
                }
            }
            else
            {
                if (symb)
                {
                    freq = trx.modem[index].tx_frequency - trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency - trx.modem[index].shift / 2.0;
                }
                else
                {
                    freq = trx.modem[index].tx_frequency + trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency + trx.modem[index].shift / 2.0;
                }
            }

            if (trx.modem[index].tx_phaseacc > M_PI)
                trx.modem[index].tx_phaseacc -= TWOPI;

            if (trx.modem[index].mon_phaseacc > M_PI)
                trx.modem[index].mon_phaseacc -= TWOPI;

            for (i = 0; i < trx.modem[index].tx_symbollen; i++)
            {
                trx.modem[index].outbuf[i] = CmplxF((float)Math.Cos(trx.modem[index].tx_phaseacc),
                    (float)(Math.Sin(trx.modem[index].tx_phaseacc)));

                trx.modem[index].mon_outbuf[i] = CmplxF((float)Math.Cos(trx.modem[index].mon_phaseacc),
                    (float)(Math.Sin(trx.modem[index].mon_phaseacc)));

                trx.modem[index].tx_phaseacc += TWOPI * freq / trx.samplerate;
                trx.modem[index].mon_phaseacc += TWOPI * mon_freq / trx.samplerate;
            }

            if (MainForm.output_ring_buf.WriteSpace() < trx.modem[index].tx_symbollen)
            {
                if (run_transmiter)
                {
                    while (MainForm.output_ring_buf.WriteSpace() < trx.modem[index].tx_symbollen)
                        audio_event.WaitOne(10);
                }
            }

            EnterCriticalSection(cs_audio);
            MainForm.output_ring_buf.Write(trx.modem[index].outbuf, trx.modem[index].tx_symbollen);
            MainForm.mon_ring_buf.Write(trx.modem[index].mon_outbuf, trx.modem[index].tx_symbollen);
            LeaveCriticalSection(cs_audio);

            if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                audio_event.WaitOne(50);
        }

        private void send_stop(int index)
        {
            double freq, mon_freq;
            int i = 0;

            if (Audio.TXswap)
            {
                if (trx.modem[index].reverse)
                {
                    freq = trx.modem[index].tx_frequency - trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency - trx.modem[index].shift / 2.0;
                }
                else
                {
                    freq = trx.modem[index].tx_frequency + trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency + trx.modem[index].shift / 2.0;
                }
            }
            else
            {
                if (!trx.modem[index].reverse)
                {
                    freq = trx.modem[index].tx_frequency + trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency + trx.modem[index].shift / 2.0;
                }
                else
                {
                    freq = trx.modem[index].tx_frequency - trx.modem[index].shift / 2.0;
                    mon_freq = trx.mon_frequency - trx.modem[index].shift / 2.0;
                }
            }

            if (trx.modem[index].tx_phaseacc > M_PI)
                trx.modem[index].tx_phaseacc -= TWOPI;

            if (trx.modem[index].mon_phaseacc > M_PI)
                trx.modem[index].mon_phaseacc -= TWOPI;

            for (i = 0; i < trx.modem[index].tx_stoplen; i++)
            {
                trx.modem[index].outbuf[i] = CmplxF((float)Math.Cos(trx.modem[index].tx_phaseacc),
                    (float)(Math.Sin(trx.modem[index].tx_phaseacc)));
                trx.modem[index].tx_phaseacc += TWOPI * freq / trx.samplerate;
                trx.modem[index].mon_outbuf[i] = CmplxF((float)Math.Cos(trx.modem[index].mon_phaseacc),
                    (float)(Math.Sin(trx.modem[index].mon_phaseacc)));
                trx.modem[index].mon_phaseacc += TWOPI * mon_freq / trx.samplerate;
            }

            if (MainForm.output_ring_buf.WriteSpace() < trx.modem[index].tx_stoplen)
            {
                if (run_transmiter)
                {
                    while (MainForm.output_ring_buf.WriteSpace() < trx.modem[index].tx_stoplen)
                    audio_event.WaitOne(10);
                }
            }

            EnterCriticalSection(cs_audio);
            MainForm.output_ring_buf.Write(trx.modem[index].outbuf, trx.modem[index].tx_stoplen);
            MainForm.mon_ring_buf.Write(trx.modem[index].mon_outbuf, trx.modem[index].tx_stoplen);
            LeaveCriticalSection(cs_audio);

            if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                audio_event.WaitOne(50);
        }

        private void send_char(int index, int c)
        {
            int i, j;

            if (trx.modem[index].nbits == 5)
            {
                if (c == BAUDOT_LETS)
                    c = 0x1F;
                if (c == BAUDOT_FIGS)
                    c = 0x1B;
            }

            /* start bit */
            send_symbol(index, 0);

            /* data bits */
            for (i = 0; i < trx.modem[index].nbits; i++)
            {
                j = (trx.modem[index].msb > 0) ? (trx.modem[index].nbits - 1 - i) : i;
                send_symbol(index, (c >> j) & 1);
            }

            /* parity bit */
            if (trx.modem[index].parity != parity_t.PARITY_NONE)
                send_symbol(index, rtty_parity(c, trx.modem[index].nbits, trx.modem[index].parity));

            /* stop bit(s) */
            send_stop(index);

            if (trx.modem[index].nbits == 5)
                c = baudot_dec(index, (char)c);
        }

        private void send_idle(int index)
        {
            if (trx.modem[index].nbits == 5)
                send_char(index, BAUDOT_LETS);
            else
                send_char(index, 0);
        }

        private char rtty_txprocess_message(int index, string data)
        {
            int c;
            char last_char = ' ';

            if (trx.tune)
            {
                while (trx.tune && MainForm.MOX)
                {
                    if (run_transmiter)
                    {
                        //send_idle(index);
                        send_char(0, 'R' & 0x1F);
                        send_char(0, 'Y' & 0x1F);

                        if (MainForm.output_ring_buf.WriteSpace() >= 2048)
                            audio_event.WaitOne(50);
                    }
                    else
                        return ' ';
                }

                return ' ';
            }

            while (trx.modem[index].preamble > 0)
            {
                send_symbol(index, 1);
                trx.modem[index].preamble--;
            }

            foreach (char a in data.ToUpper())
            {
                if (run_transmiter)
                {
                    c = a;

                    /* TX buffer empty */
                    if (c == -1)
                    {
                        /* send idle character */
                        send_idle(index);
                        trx.modem[index].txmode = BAUDOT_LETS;
                    }

                    if (trx.modem[index].nbits != 5)
                    {
                        send_char(index, c);
                    }

                    // unshift-on-space
                    /*if (c == ' ')
                    {
                        send_char(index, BAUDOT_LETS);
                        send_char(index, 0x04);

                        trx.modem[index].txmode = BAUDOT_LETS;
                        send_char(index, c & 0x1F);
                    }*/

                    if ((c = baudot_enc((char)c)) < 0)
                        send_idle(index);

                    // switch case if necessary
                    if ((c & trx.modem[index].txmode) == 0)
                    {
                        if (trx.modem[index].txmode == BAUDOT_FIGS)
                        {
                            send_char(index, BAUDOT_LETS);
                            trx.modem[index].txmode = BAUDOT_LETS;
                        }
                        else
                        {
                            send_char(index, BAUDOT_FIGS);
                            trx.modem[index].txmode = BAUDOT_FIGS;
                        }
                    }

                    send_char(index, c & 0x1F);
                    last_char = a;
                    MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback),
                        "Set TX text", index, a.ToString());

                    if (MainForm.keyboard != null && MainForm.keyboard.Visible)
                        MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                            "Set Keyboard text", index, a.ToString());
                }
                else
                {
                    trx.modem[index].preamble = tx_preamble;
                    MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set TX text", index, "");

                    if (MainForm.keyboard.Visible)
                        MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                            "Set Keyboard text", index, a.ToString());

                    return last_char;
                }
            }

            trx.modem[index].preamble = tx_preamble;

            while (trx.modem[index].preamble > 0)
            {
                send_symbol(index, 1);
                trx.modem[index].preamble--;

                if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                    audio_event.WaitOne(50);
            }

            MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

            if (MainForm.keyboard.Visible)
                MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                    "Set Keyboard text", 0, "");

            trx.modem[index].preamble = tx_preamble;

            return last_char;
        }

        public void rtty_txprocess_char()
        {
            int c = 0, index = 0;
            char last_char = ' ';

            if (MainForm.TXSplit)
                index = 1;

            if (trx.tune)
            {
                while (trx.tune && MainForm.MOX)
                {
                    if (run_transmiter)
                    {
                        //send_idle(index);
                        send_char(0, 'R' & 0x1F);
                        send_char(0, 'Y' & 0x1F);

                        if (MainForm.output_ring_buf.WriteSpace() >= 2048)
                            audio_event.WaitOne(50);
                    }
                    else
                        return;
                }

                return;
            }

            while (trx.modem[index].preamble > 0)
            {
                send_symbol(index, 1);
                trx.modem[index].preamble--;
            }

            while(mox)
            {
                if (run_transmiter)
                {
                    if (RBmessage.ReadSpace() > 100)
                        RBmessage.Restart();

                    if (RBmessage.ReadSpace() > 0)
                    {
                        char[] data = new char[1];
                        RBmessage.Read(ref data, 1);

                        c = data[0];

                        if (trx.modem[index].nbits != 5)
                        {
                            send_char(index, c);
                        }

                        // unshift-on-space
                        /*if (c == ' ')
                        {
                            send_char(index, BAUDOT_LETS);
                            send_char(index, 0x04);

                            trx.modem[index].txmode = BAUDOT_LETS;
                            send_char(index, c & 0x1F);
                        }*/

                        if ((c = baudot_enc((char)c)) < 0)
                            send_idle(index);

                        // switch case if necessary
                        if ((c & trx.modem[index].txmode) == 0)
                        {
                            if (trx.modem[index].txmode == BAUDOT_FIGS)
                            {
                                send_char(index, BAUDOT_LETS);
                                trx.modem[index].txmode = BAUDOT_LETS;
                            }
                            else
                            {
                                send_char(index, BAUDOT_FIGS);
                                trx.modem[index].txmode = BAUDOT_FIGS;
                            }
                        }

                        send_char(index, c & 0x1F);
                        last_char = data[0];
                        MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback),
                            "Set TX text", index, data[0].ToString());

                        if (MainForm.keyboard != null && MainForm.keyboard.Visible)
                            MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                "Set Keyboard text", index, data[0].ToString());
                    }
                    else
                    {
                        /* send idle character */
                        send_idle(index);
                        trx.modem[index].txmode = BAUDOT_LETS;
                    }
                }
                else
                {
                    trx.modem[index].preamble = tx_preamble;
                    MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set TX text", index, "");

                    if (MainForm.keyboard.Visible)
                        MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                            "Set Keyboard text", index, "");

                    return;
                }
            }

            trx.modem[index].preamble = tx_preamble;
            run_transmiter = false;

            while (trx.modem[index].preamble > 0)
            {
                send_symbol(index, 1);
                trx.modem[index].preamble--;

                if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                    audio_event.WaitOne(50);
            }

            MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

            if (MainForm.keyboard.Visible)
                MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                    "Set Keyboard text", 0, "");

            trx.modem[index].preamble = tx_preamble;

            return;
        }

        #endregion

        #region complex math

        private ComplexF CmplxF(float x, float y)
        {
            ComplexF z;
            z.Re = x;
            z.Im = y;
            return z;
        }

        private ComplexF CsclF(ComplexF x, float a)
        {
            ComplexF z;
            z.Re = x.Re * a;
            z.Im = x.Im * a;
            return z;
        }

        private ComplexF CmulF(ComplexF x, ComplexF y)
        {
            ComplexF z;
            z.Re = x.Re * y.Re - x.Im * y.Im;
            z.Im = x.Im * y.Re + x.Re * y.Im;
            return z;
        }

        private ComplexF CmulF(double x_real, double x_imag, double y_real, double y_imag)
        {
            ComplexF z;
            z.Re = (float)(x_real * y_real - x_imag * y_imag);
            z.Im = (float)(x_imag * y_real + x_real * y_imag);
            return z;
        }

        private double carg(ComplexF x)
        {
            return Math.Atan2(x.Im, x.Re);
        }

        ComplexF ccor(ComplexF x, ComplexF y)
        {
            ComplexF z;

            z.Re = x.Re * y.Re + x.Im * y.Im;
            z.Im = x.Re * y.Im - x.Im * y.Re;

            return z;
        }

        #endregion

        #region RTTY thread

        public bool Start(string text)
        {
            try
            {
                MainForm.genesis.WriteToDevice(18, (long)Keyer_mode.CWX);
                message = text;
                run_transmiter = true;

                if (tx_thread == null || !tx_thread.IsAlive)
                {
                    tx_thread = new Thread(new ThreadStart(TXthread));
                    tx_thread.Name = "RTTY Encode Thread";
                    tx_thread.Priority = ThreadPriority.Normal;
                    tx_thread.IsBackground = true;
                }
                else if (tx_thread != null && tx_thread.ThreadState == (System.Threading.ThreadState.WaitSleepJoin |
                     System.Threading.ThreadState.Background))
                {
                    audio_event.Set();
                }

                tx_thread.Start();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_transmiter = false;
                return false;
            }
        }

        public bool ManualStart()
        {
            try
            {
                MainForm.genesis.WriteToDevice(18, (long)Keyer_mode.CWX);
                run_transmiter = true;

                if (tx_thread == null || !tx_thread.IsAlive)
                {
                    tx_thread = new Thread(new ThreadStart(TXthreadChar));
                    tx_thread.Name = "RTTY Encode Thread";
                    tx_thread.Priority = ThreadPriority.Normal;
                    tx_thread.IsBackground = true;
                    tx_thread.Start();
                }
                else if (tx_thread != null && tx_thread.ThreadState == (System.Threading.ThreadState.WaitSleepJoin |
                     System.Threading.ThreadState.Background))
                {
                    audio_event.Set();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_transmiter = false;
                return false;
            }
        }

        public bool Stop()
        {
            try
            {
                run_transmiter = false;
                audio_event.Set();
                MainForm.output_ring_buf.Reset();
                MainForm.mon_ring_buf.Reset();
                MainForm.MOX = false;
                System.GC.Collect();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public bool ManualStop()
        {
            try
            {
                mox = false;
                run_transmiter = false;
                audio_event.Set();
                MainForm.output_ring_buf.Reset();
                MainForm.mon_ring_buf.Reset();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public void TXthread()
        {
            try
            {
                int index = 0;
                char last = ' ';

                if (!trx.tune)
                {
                    message = message.ToUpper();
                    char[] text = message.ToCharArray();

                    if (text[text.Length - 1] == '#')
                    {
                        message = message.Remove(text.Length - 1, 1);
                        last = '#';
                    }
                }

                if (MainForm.TXSplit)
                    index = 1;

                MainForm.MOX = true;
                trx.modem[index].tx_phaseacc = 0.0;
                trx.modem[index].mon_phaseacc = 0.0;
                trx.modem[index].preamble = tx_preamble;
                rtty_txprocess_message(index, message);
                run_transmiter = false;
                MainForm.MOX = false;
                MainForm.output_ring_buf.Reset();
                MainForm.mon_ring_buf.Reset();

                if (last == '#')
                {
                    MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message repeat", 0, "");

                    MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                        "Reload Keyboard text", 0, message + "#");
                }
                else
                    MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_transmiter = false;
                MainForm.MOX = false;
                MainForm.output_ring_buf.Reset();
                MainForm.mon_ring_buf.Reset();
                System.GC.Collect();
            }
        }

        public void TXthreadChar()
        {
            try
            {
                int index = 0;

                if (MainForm.TXSplit)
                    index = 1;

                mox = true;
                MainForm.MOX = true;
                trx.modem[index].tx_phaseacc = 0.0;
                trx.modem[index].mon_phaseacc = 0.0;
                trx.modem[index].preamble = tx_preamble;
                rtty_txprocess_char();
                run_transmiter = false;
                MainForm.MOX = false;
                MainForm.output_ring_buf.Reset();
                MainForm.mon_ring_buf.Reset();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public bool reset_after_mox = false;
        private void RTTY_ThreadRX1()
        {
            int i;
            float mark_max = 0.0f;
            float space_max = 0.0f;
            float mark_avg = 0.0f;
            float space_avg = 0.0f;

            try
            {
                while (run_thread)
                {
                    AudioEventRX1.WaitOne();

                    if (update_trx1)
                    {
                        rtty_reload(0);
                        update_trx1 = false;
                    }

                    Array.Copy(ch1_buffer, ch1_buf, 2048);
                    Array.Copy(ch2_buffer, ch2_buf, 2048);

                    for (int j = 0; j < (2048 / (frame_segment)); j++)
                    {
                        for (i = 0; i < frame_segment; i++)
                        {
                            if (run_rx1)
                            {
                                mark_avg = mark_avg * 0.85f + 0.15f * Math.Abs(ch2_buf[j * frame_segment + i]);
                                space_avg = space_avg * 0.85f + 0.15f * Math.Abs(ch1_buf[j * frame_segment + i]);

                                mark_max = Math.Max(mark_max, mark_avg);
                                space_max = Math.Max(space_max, space_avg);
                            }
                        }

                        Mag_mark[0, j] = mark_max;
                        Mag_space[0, j] = space_max;
                        mark_max = 0.0f;
                        space_max = 0.0f;
                    }

                    if (run_rx1)
                        RXprocess(0, 2048 / frame_segment);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void RTTY_ThreadRX2()
        {
            int i;
            float mark2_max = 0.0f;
            float space2_max = 0.0f;
            float mark2_avg = 0.0f;
            float space2_avg = 0.0f;

            try
            {
                while (run_thread)
                {
                    AudioEventRX2.WaitOne();

                    if (update_trx2)
                    {
                        rtty_reload(1);
                        update_trx2 = false;
                    }

                    Array.Copy(ch3_buffer, ch3_buf, 2048);
                    Array.Copy(ch4_buffer, ch4_buf, 2048);

                    for (int j = 0; j < (2048 / (frame_segment)); j++)
                    {
                        for (i = 0; i < frame_segment; i++)
                        {
                            mark2_avg = mark2_avg * 0.85f + 0.15f * Math.Abs(ch4_buf[j * frame_segment + i]);
                            space2_avg = space2_avg * 0.85f + 0.15f * Math.Abs(ch3_buf[j * frame_segment + i]);

                            mark2_max = Math.Max(mark2_max, mark2_avg);
                            space2_max = Math.Max(space2_max, space2_avg);
                        }

                        Mag_mark[1, j] = mark2_max;
                        Mag_space[1, j] = space2_max;
                        mark2_max = 0.0f;
                        space2_max = 0.0f;
                    }

                    RXprocess(1, 2048 / frame_segment);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }
}
