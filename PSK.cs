//=================================================================
// PSK encoder/decoder
//=================================================================
// Copyright (C) 2011-2015 S56A YT7PWR
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
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Diagnostics;

namespace CWExpert
{
    unsafe public class PSK
    {
        #region enum

        public enum ScanDirection
        {
            Up = 0,
            Down,
        }

        public enum AGCMODE
        {
            agcOFF,
            agcLONG,
            agcSLOW,
            agcMED,
            agcFAST
        }

        #endregion

        #region DLL import

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

        #region structures

        [StructLayout(LayoutKind.Sequential)]
        public struct TRX
        {
            public double squelch;
            public bool reverse;
            public bool afcon;
            public int acquire;
            public bool squelchon;
            public bool stopflag;
            public bool tune;
            public float metric;
            public float txoffset;
            public int backspaces;
            public ComplexF[] outbuf;
            public ComplexF[] outbuf1;
            public ComplexF[] mon_outbuf;
            public double mon_frequency;
            public psk[] modem;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct psk
        {
            // Common stuff
            public Mode mode;
            public float[] filter;
            public float[] ibuffer;
            public float[] qbuffer;
            public int length;
            public int FIRBufferLen;
            public int pointer;
            public int counter;
            public bool qpsk;
            public double mon_phaseacc;
            public uint shreg;
            public double bandwidth;
            public bool freqlock;
            public double phase;
            public int bits;
            public int rx_samplerate;
            public int tx_samplerate;
            public int rx_symbollen;
            public int tx_symbollen;
            // RX related stuff
            public int decimate_ratio;
            public int decimate;
            public ComplexF rx_prevsymbol;
            public ComplexF tx_prevsymbol;
            public viterbi decoder;
            public enc encoder;
            public double bitclk;
            public double[] syncbuf;
            public uint dcdshreg;
            public bool dcd;
            public ComplexF quality;
            public double rx_frequency;
            public ScanDirection scan_direction;
            public double rx_phaseacc;
            // TX related stuff
            public double[] txshape;
            public int preamble;
            public int txmode;
            public float tx_phase;
            public float tx_gain;
            public double tx_frequency;
            public double tx_phaseacc;
            public double tmp_tx_phaseacc;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct viterbi
        {
            public int traceback;
            public int chunksize;
            public int nstates;
            public int[] output;
            public int[,] metrics;
            public int[,] history;
            public int[] sequence;
            public int[,] mettab;
            public uint ptr;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct enc
        {
            public int[] output;
            public int shreg;
            public int shregmask;
        }

        #endregion

        #region variable

        delegate void CrossThreadCallback(string command, string data);
        delegate void CrossThreadSetText(string command, int channel_no, string out_txt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CallbackFunction(int type, char data);
        public static CallbackFunction callback;

        public TRX trx;
        public float[] ch1_buffer;
        public float[] ch2_buffer;
        public float[] buffer_ch1;
        public float[] buffer_ch2;
        private static ComplexF[] zero_buffer = new ComplexF[2048];
        private CWExpert MainForm;
        private const int FILTERS_NUMBER = 2;
        private const double M_PI_2 = 1.57079632679464;
        private const double M_PI = 3.14159265358928;
        private const double TWOPI = 6.28318530717856;
        public Mutex update_receiver = new Mutex();
        public Mutex update_filter = new Mutex();
        public Mutex receive_mutex;
        private AutoResetEvent audio_event = new AutoResetEvent(false);
        public Thread PSKThread1;
        public Thread PSKThread2;
        public AutoResetEvent AudioEvent1;
        public AutoResetEvent AudioEvent2;
        public AutoResetEvent AudioEventEnd1 = new AutoResetEvent(false);
        public bool run_thread = false;
        public double sql = 1.0;
        private HiPerfTimer display_timer;
        private ComplexF[] iq_buffer;
        public bool run_transmiter = false;
        private Thread tx_thread;
        delegate void CrossThreadCommand(string command, int param_1, string param_2);
        private string message = "";
        private int PATHMEM = 64;
        private int K = 5;
        private int POLY1 = 0x17;
        private int POLY2 = 0x19;
        private const int MODEM_NR = 2;
        public bool update_trx1 = false;
        public bool update_trx2 = false;
        public bool run_rx1 = false;
        public bool run_rx2 = false;
        unsafe private static void* cs_audio;
        tx[] Tx = new tx[2];
        private Fourier fft;
        public RingBufferChar RBmessage = new RingBufferChar(2048);

        double[] gmfir2c =
            {
 	            -0.00044789,
 	            -0.00063167,
 	-0.00100256,
 	-0.00156531,
 	-0.00232266,
 	-0.00327063,
 	-0.00439342,
 	-0.00565850,
 	-0.00701253,
 	-0.00837907,
 	-0.00965823,
 	-0.01072878,
 	-0.01145278,
 	-0.01168246,
 	-0.01126911,
 	-0.01007335,
 	-0.00797606,
 	-0.00488924,
 	-0.00076591,
 	0.00439171,
 	0.01052660,
 	0.01752432,
 	0.02521404,
 	0.03337329,
 	0.04173643,
 	0.05000649,
 	0.05786981,
 	0.06501264,
 	0.07113857,
 	0.07598566,
 	0.07934199,
 	0.08105860,
 	0.08105860,
 	0.07934199,
 	0.07598566,
 	0.07113857,
 	0.06501264,
 	0.05786981,
 	0.05000649,
 	0.04173643,
 	0.03337329,
 	0.02521404,
 	0.01752432,
 	0.01052660,
 	0.00439171,
 	-0.00076591,
 	-0.00488924,
 	-0.00797606,
 	-0.01007335,
 	-0.01126911,
 	-0.01168246,
 	-0.01145278,
 	-0.01072878,
 	-0.00965823,
 	-0.00837907,
 	-0.00701253,
 	-0.00565850,
 	-0.00439342,
 	-0.00327063,
 	-0.00232266,
 	-0.00156531,
 	-0.00100256,
 	-0.00063167,
 	-0.00044789,
            };

        #endregion

        #region properties

        private bool mox = false;
        public bool MOX
        {
            get { return mox; }
            set { mox = value; }
        }

        private bool afc = false;
        public bool AFC
        {
            get { return afc; }
            set 
            {
                afc = value;
                trx.afcon = value;
            }
        }

        private bool rx2_enabled = false;
        public bool RX2Enabled
        {
            get { return rx2_enabled; }
            set { rx2_enabled = value; }
        }

        private int tx_preamble = 50;
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
                trx.modem[0].tx_phase = 0.001f * value;
                trx.modem[1].tx_phase = 0.001f * value;
            }
        }
        public float TXGain
        {
            set
            {
                trx.modem[0].tx_gain = 1 + 0.001f * value;
                trx.modem[1].tx_gain = 1 + 0.001f * value;
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

        public PSK(CWExpert form)
        {
            MainForm = form;
            AudioEvent1 = new AutoResetEvent(false);
            AudioEvent2 = new AutoResetEvent(false);
            iq_buffer = new ComplexF[2048];
            ch1_buffer = new float[2048];
            ch2_buffer = new float[2048];
            buffer_ch1 = new float[2048];
            buffer_ch2 = new float[2048];
            display_timer = new HiPerfTimer();
            trx = new TRX();
            trx.modem = new psk[MODEM_NR];
            trx.outbuf = new ComplexF[819];
            trx.outbuf1 = new ComplexF[8192];
            trx.mon_outbuf = new ComplexF[8192];

            cs_audio = (void*)0x0;
            cs_audio = NewCriticalSection();

            if (InitializeCriticalSectionAndSpinCount(cs_audio, 0x00000080) == 0)
            {
                Debug.WriteLine("CriticalSection Failed");
            }

            fft = new Fourier();
        }

        ~PSK()
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

        public bool PSKStart()
        {
            bool result = false;
            fft = new Fourier();

            try
            {
                if (Init())
                {
                    run_thread = true;
                    PSKThread1 = new Thread(new ThreadStart(PSK_ThreadRX1));
                    PSKThread1.Name = "PSK Thread RX1";
                    PSKThread1.Priority = ThreadPriority.Normal;
                    PSKThread1.IsBackground = true;
                    PSKThread1.Start();

                    PSKThread2 = new Thread(new ThreadStart(PSK_ThreadRX2));
                    PSKThread2.Name = "PSK Thread RX2";
                    PSKThread2.Priority = ThreadPriority.Normal;
                    PSKThread2.IsBackground = true;
                    PSKThread2.Start();
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

        public void PSKStop()
        {
            try
            {
                audio_event.Set();
                run_transmiter = false;
                run_thread = false;
                Thread.Sleep(100);
                AudioEvent1.Set();
                AudioEvent2.Set();
                Thread.Sleep(100);
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region PSK control

        public bool Init()
        {
            try
            {
                psk_init();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private void psk_txinit()
        {
            for (int i = 0; i < MODEM_NR; i++)
            {
                trx.modem[i].freqlock = false;
                trx.modem[i].tx_phaseacc = 0;
                trx.modem[i].tmp_tx_phaseacc = 0;
                trx.modem[i].tx_prevsymbol.Re = 1.0f;
                trx.modem[i].tx_prevsymbol.Im = 0.0f;
                trx.modem[i].preamble = MainForm.PSKPreamble;
                trx.modem[i].shreg = 0;
            }
        }

        private void psk_rxinit()
        {
            for (int i = 0; i < MODEM_NR; i++)
            {
                trx.modem[i].scan_direction = ScanDirection.Up;
                trx.modem[i].rx_phaseacc = 0;
                trx.modem[i].rx_prevsymbol.Re = 1.0f;
                trx.modem[i].rx_prevsymbol.Im = 0.0f;
                trx.modem[i].quality.Re = 0.0f;
                trx.modem[i].quality.Im = 0.0f;
                trx.modem[i].shreg = 0;
                trx.modem[i].dcdshreg = 0;
                trx.modem[i].dcd = false;
                trx.modem[i].bitclk = 0;
                trx.modem[i].rx_frequency = MainForm.PSKPitch;
            }
        }

        private void psk_init()
        {
            int i;

            TXIfShift = (double)MainForm.SetupForm.udTXIfShift.Value;
            psk_rxinit();
            psk_txinit();
            trx.acquire = 0;
            trx.afcon = afc;
            trx.mon_frequency = (double)MainForm.SetupForm.udMonitorFrequncy.Value;

            for (int index = 0; index < MODEM_NR; index++)
            {
                if (index == 0)
                    trx.modem[index].mode = MainForm.OpModeVFOA;
                else if (index == 1)
                {
                    if (MainForm.OpModeVFOB == Mode.RTTY || MainForm.OpModeVFOB == Mode.CW)
                        trx.modem[index].mode = Mode.BPSK31;
                    else
                        trx.modem[index].mode = MainForm.OpModeVFOB;
                }

                trx.modem[index].ibuffer = new float[2048];
                trx.modem[index].qbuffer = new float[2048];
                trx.modem[index].tx_frequency = (float)tx_if_shift;
                trx.squelch = 0.1;  // Math.Sqrt(0.03);
                trx.squelchon = true;
                trx.modem[index].syncbuf = new double[16];
                trx.modem[index].rx_samplerate = 12000;  // Audio.SampleRate;
                trx.modem[index].tx_samplerate = Audio.SampleRate;
                trx.modem[index].tx_phase = 0.001f * (float)MainForm.SetupForm.udTXPhase.Value;
                trx.modem[index].tx_gain = 1 + 0.001f * (float)MainForm.SetupForm.udTXGain.Value;

                switch (trx.modem[index].mode)
                {
                    case Mode.BPSK31:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 31.25));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 31.25));
                        trx.modem[index].bandwidth = 31.25;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.BPSK63:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 62.5));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 62.5));
                        trx.modem[index].bandwidth = 62.5;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.BPSK125:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 125.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 125.0));
                        trx.modem[index].bandwidth = 125.0;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.BPSK250:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 250.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 250.0));
                        trx.modem[index].bandwidth = 250.0;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.QPSK31:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 31.25));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 31.25));
                        trx.modem[index].bandwidth = 31.25;
                        trx.modem[index].qpsk = true;
                        break;
                    case Mode.QPSK63:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 62.5));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 62.5));
                        trx.modem[index].bandwidth = 62.5;
                        trx.modem[index].qpsk = true;
                        break;
                    case Mode.QPSK125:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 125.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 125.0));
                        trx.modem[index].bandwidth = 125.0;
                        trx.modem[index].qpsk = true;
                        break;
                    case Mode.QPSK250:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 250.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 250.0));
                        trx.modem[index].qpsk = true;
                        trx.modem[index].bandwidth = 250.0;
                        break;
                    default:
                        return;
                }

                trx.modem[index].encoder = encoder_init(K, POLY1, POLY2);
                trx.modem[index].decoder = viterbi_init(K, POLY1, POLY2);
                trx.modem[index].txshape = new double[trx.modem[index].tx_symbollen];

                /* raised cosine shape for the transmitter */
                for (i = 0; i < trx.modem[index].tx_symbollen; i++)
                    trx.modem[index].txshape[i] = 0.5 * Math.Cos(i * M_PI / trx.modem[index].tx_symbollen) + 0.5;

                trx.modem[index].filter = new float[64];

                for (int k = 0; k < 64; k++)
                {
                    trx.modem[index].filter[k] = (float)gmfir2c[k];
                }

                trx.modem[index].length = 64;
                trx.modem[index].FIRBufferLen = 2048;
                trx.modem[index].pointer = 64;
                trx.modem[index].decimate_ratio = trx.modem[index].rx_symbollen / 16;

                InitTXFilter(index, Audio.BlockSize, Audio.SampleRate);
            }
        }

        public void psk_reload(int index)
        {
            try
            {
                int i;

                TXIfShift = (double)MainForm.SetupForm.udTXIfShift.Value;
                psk_rxinit();
                psk_txinit();
                trx.acquire = 0;

                if (index == 0)
                    trx.modem[index].mode = MainForm.OpModeVFOA;
                else if (index == 1)
                {
                    if (MainForm.OpModeVFOB == Mode.CW || MainForm.OpModeVFOB == Mode.RTTY)
                    {
                        trx.modem[index].mode = Mode.BPSK31;
                    }
                    else
                        trx.modem[index].mode = MainForm.OpModeVFOB;
                }

                trx.modem[index].ibuffer = new float[2048];
                trx.modem[index].qbuffer = new float[2048];
                trx.modem[index].tx_frequency = (float)tx_if_shift;
                trx.squelch = 0.1;  // Math.Sqrt(0.03); //0.1;
                trx.squelchon = true;
                trx.modem[index].syncbuf = new double[16];
                trx.modem[index].rx_samplerate = 12000; // Audio.SampleRate;
                trx.modem[index].tx_samplerate = Audio.SampleRate;
                trx.modem[index].tx_phase = 0.001f * (float)MainForm.SetupForm.udTXPhase.Value;
                trx.modem[index].tx_gain = 1 + 0.001f * (float)MainForm.SetupForm.udTXGain.Value;

                switch (trx.modem[index].mode)
                {
                    case Mode.BPSK31:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 31.25));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 31.25));
                        trx.modem[index].bandwidth = 31.25;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.BPSK63:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 62.5));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 62.5));
                        trx.modem[index].bandwidth = 62.5;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.BPSK125:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 125.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 125.0));
                        trx.modem[index].bandwidth = 125.0;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.BPSK250:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 250.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 250.0));
                        trx.modem[index].bandwidth = 250.0;
                        trx.modem[index].qpsk = false;
                        break;
                    case Mode.QPSK31:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 31.25));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 31.25));
                        trx.modem[index].bandwidth = 31.25;
                        trx.modem[index].qpsk = true;
                        break;
                    case Mode.QPSK63:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 62.5));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 62.5));
                        trx.modem[index].bandwidth = 62.5;
                        trx.modem[index].qpsk = true;
                        break;
                    case Mode.QPSK125:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 125.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 125.0));
                        trx.modem[index].bandwidth = 125.0;
                        trx.modem[index].qpsk = true;
                        break;
                    case Mode.QPSK250:
                        trx.modem[index].rx_symbollen = (int)((trx.modem[index].rx_samplerate / 250.0));
                        trx.modem[index].tx_symbollen = (int)((Audio.SampleRate / 250.0));
                        trx.modem[index].qpsk = true;
                        trx.modem[index].bandwidth = 250.0;
                        break;
                    default:
                        return;
                }

                trx.modem[index].encoder = encoder_init(K, POLY1, POLY2);
                trx.modem[index].decoder = viterbi_init(K, POLY1, POLY2);
                trx.modem[index].txshape = new double[trx.modem[index].tx_symbollen];

                /* raised cosine shape for the transmitter */
                for (i = 0; i < trx.modem[index].tx_symbollen; i++)
                    trx.modem[index].txshape[i] = 0.5 * Math.Cos(i * M_PI / trx.modem[index].tx_symbollen) + 0.5;

                trx.modem[index].filter = new float[64];

                for (int k = 0; k < 64; k++)
                {
                    trx.modem[index].filter[k] = (float)gmfir2c[k];
                }

                trx.modem[index].length = 64;
                trx.modem[index].FIRBufferLen = 2048;
                trx.modem[index].pointer = 64;
                trx.modem[index].decimate_ratio = trx.modem[index].rx_symbollen / 16;

                InitTXFilter(index, Audio.BlockSize, Audio.SampleRate);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Viterbi

        private viterbi viterbi_init(int k, int poly1, int poly2)
        {
            viterbi v;
            int i;

            v = new viterbi();
            v.traceback = PATHMEM - 1;
            v.chunksize = 8;
            v.nstates = 1 << (k - 1);
            v.output = new int[1 << k];
            v.metrics = new int[PATHMEM, v.nstates];
            v.history = new int[PATHMEM, v.nstates];
            v.sequence = new int[PATHMEM];
            v.mettab = new int[2, 256];

            for (i = 0; i < (1 << k); i++)
                v.output[i] = parity(poly1 & i) | (parity(poly2 & i) << 1);

            for (i = 0; i < 256; i++)
            {
                v.mettab[0, i] = 128 - i;
                v.mettab[1, i] = i - 128;
            }

            v.ptr = 0;

            return v;
        }

        private int viterbi_set_traceback(viterbi v, int traceback)
        {
            if (traceback < 0 || traceback > PATHMEM - 1)
                return -1;

            v.traceback = traceback;
            return 0;
        }

        private int viterbi_set_chunksize(viterbi v, int chunksize)
        {
            if (chunksize < 1 || chunksize > v.traceback)
                return -1;

            v.chunksize = chunksize;
            return 0;
        }

        private void viterbi_reset(viterbi v)
        {
            v.metrics = new int[PATHMEM, v.nstates];
            v.history = new int[PATHMEM, v.nstates];
            v.ptr = 0;
        }

        private int traceback(viterbi v, int[] metric)
        {
            int i, bestmetric, beststate;
            uint p, c;

            p = (uint)((v.ptr - 1) % PATHMEM);

            /*
             * Find the state with the best metric
             */
            bestmetric = Int32.MinValue;  // INT_MIN;
            beststate = 0;

            for (i = 0; i < v.nstates; i++)
            {
                if (v.metrics[p, i] > bestmetric)
                {
                    bestmetric = v.metrics[p, i];
                    beststate = i;
                }
            }

            /*
             * Trace back 'traceback' steps, starting from the best state
             */
            v.sequence[p] = beststate;

            for (i = 0; i < v.traceback; i++)
            {
                uint prev = (uint)((p - 1) % PATHMEM);

                v.sequence[prev] = v.history[p, v.sequence[p]];
                p = prev;
            }

            if (metric != null)
                metric[0] = v.metrics[p, v.sequence[p]];

            /*
             * Decode 'chunksize' bits
             */
            c = 0;
            i = 0;
            for (i = 0; i < v.chunksize; i++)
            {
                /*
                 * low bit of state is the previous input bit
                 */
                c = (uint)(c << 1);
                c |= (uint)(v.sequence[p] & 1);
                p = (uint)((p + 1) % PATHMEM);
            }

            if (metric != null)
                metric[0] = v.metrics[p, v.sequence[p]] - metric[0];

            return (int)c;
        }

        private int viterbi_decode(int index, short[] sym, int[] metric)
        {
            uint currptr, prevptr;
            int i, j, n;
            int[] met = new int[4];

            currptr = (uint)trx.modem[index].decoder.ptr;
            prevptr = (uint)((currptr - 1) % PATHMEM);

            met[0] = trx.modem[index].decoder.mettab[0, sym[1]] + trx.modem[index].decoder.mettab[0, sym[0]];
            met[1] = trx.modem[index].decoder.mettab[0, sym[1]] + trx.modem[index].decoder.mettab[1, sym[0]];
            met[2] = trx.modem[index].decoder.mettab[1, sym[1]] + trx.modem[index].decoder.mettab[0, sym[0]];
            met[3] = trx.modem[index].decoder.mettab[1, sym[1]] + trx.modem[index].decoder.mettab[1, sym[0]];

            for (n = 0; n < trx.modem[index].decoder.nstates; n++)
            {
                int p0, p1, s0, s1, m0, m1;

                s0 = n;
                s1 = n + trx.modem[index].decoder.nstates;

                p0 = s0 >> 1;
                p1 = s1 >> 1;

                m0 = trx.modem[index].decoder.metrics[prevptr, p0] + met[trx.modem[index].decoder.output[s0]];
                m1 = trx.modem[index].decoder.metrics[prevptr, p1] + met[trx.modem[index].decoder.output[s1]];

                if (m0 > m1)
                {
                    trx.modem[index].decoder.metrics[currptr, n] = m0;
                    trx.modem[index].decoder.history[currptr, n] = p0;
                }
                else
                {
                    trx.modem[index].decoder.metrics[currptr, n] = m1;
                    trx.modem[index].decoder.history[currptr, n] = p1;
                }
            }

            trx.modem[index].decoder.ptr = (uint)((trx.modem[index].decoder.ptr + 1) % PATHMEM);

            if ((trx.modem[index].decoder.ptr % trx.modem[index].decoder.chunksize) == 0)
                return (int)traceback(index, metric);

            if (trx.modem[index].decoder.metrics[currptr, 0] > int.MaxValue / 2)
            {
                for (i = 0; i < PATHMEM; i++)
                    for (j = 0; j < trx.modem[index].decoder.nstates; j++)
                        trx.modem[index].decoder.metrics[i, j] -= int.MaxValue / 2;
            }
            if (trx.modem[index].decoder.metrics[currptr, 0] < int.MinValue / 2)
            {
                for (i = 0; i < PATHMEM; i++)
                    for (j = 0; j < trx.modem[index].decoder.nstates; j++)
                        trx.modem[index].decoder.metrics[i, j] += int.MinValue / 2;
            }

            return -1;
        }

        private uint traceback(int index, int[] metric)
        {
            int i, bestmetric, beststate;
            uint p, c = 0;

            p = (uint)((trx.modem[index].decoder.ptr - 1) % PATHMEM);
            /*
             * Find the state with the best metric
             */
            bestmetric = int.MinValue;
            beststate = 0;

            for (i = 0; i < trx.modem[index].decoder.nstates; i++)
            {
                if (trx.modem[index].decoder.metrics[p, i] > bestmetric)
                {
                    bestmetric = trx.modem[index].decoder.metrics[p, i];
                    beststate = i;
                }
            }

            /*
             * Trace back 'traceback' steps, starting from the best state
             */
            trx.modem[index].decoder.sequence[p] = beststate;

            for (i = 0; i < trx.modem[index].decoder.traceback; i++)
            {
                uint prev = (uint)((p - 1) % PATHMEM);

                trx.modem[index].decoder.sequence[prev] =
                    trx.modem[index].decoder.history[p, trx.modem[index].decoder.sequence[p]];
                p = prev;
            }

            if (metric != null)
                metric[0] = trx.modem[index].decoder.metrics[p, trx.modem[index].decoder.sequence[p]];

            /*
             * Decode 'chunksize' bits
             */
            for (i = 0; i < trx.modem[index].decoder.chunksize; i++)
            {
                /*
                 * low bit of state is the previous input bit
                 */
                c = (uint)(c << 1);
                c |= (uint)(trx.modem[index].decoder.sequence[p] & 1);
                p = (uint)((p + 1) % PATHMEM);
            }

            if (metric != null)
                metric[0] = trx.modem[index].decoder.metrics[p, trx.modem[index].decoder.sequence[p]] - metric[0];

            return c;
        }

        /* ---------------------------------------------------------------------- */

        private enc encoder_init(int k, int poly1, int poly2)
        {
            enc e = new enc();
            int i, size;

            size = 1 << k;	/* size of the output table */

            e.output = new int[size];

            for (i = 0; i < size; i++)
                e.output[i] = parity(poly1 & i) | (parity(poly2 & i) << 1);

            e.shreg = 0;
            e.shregmask = size - 1;

            return e;
        }

        private int encoder_encode(int index, int bit)
        {
            int q = 0;

            if (bit > 0)
                q = 1;

            trx.modem[index].encoder.shreg = (trx.modem[index].encoder.shreg << 1) | q;        // ???????
            int result = trx.modem[index].encoder.output[trx.modem[index].encoder.shreg & trx.modem[index].encoder.shregmask];
            return result;
        }

        private int parity(Int32 w)
        {
            return hweight32(w) & 1;
        }

        private Int32 hweight32(Int32 w)
        {
            w = (w & 0x55555555) + ((w >> 1) & 0x55555555);
            w = (w & 0x33333333) + ((w >> 2) & 0x33333333);
            w = (w & 0x0F0F0F0F) + ((w >> 4) & 0x0F0F0F0F);
            w = (w & 0x00FF00FF) + ((w >> 8) & 0x00FF00FF);
            w = (w & 0x0000FFFF) + ((w >> 16) & 0x0000FFFF);
            return w;
        }

        #endregion

        #region varicode

        // The PSK31 Varicode.

        string[] varicodetab1 =
{
	"1010101011",		/*   0 - <NUL>	*/
	"1011011011",		/*   1 - <SOH>	*/
	"1011101101",		/*   2 - <STX>	*/
	"1101110111",		/*   3 - <ETX>	*/
	"1011101011",		/*   4 - <EOT>	*/
	"1101011111",		/*   5 - <ENQ>	*/
	"1011101111",		/*   6 - <ACK>	*/
	"1011111101",		/*   7 - <BEL>	*/
	"1011111111",		/*   8 - <BS>	*/
	"11101111",		/*   9 - <TAB>	*/
	"11101",		/*  10 - <LF>	*/
	"1101101111",		/*  11 - <VT>	*/
	"1011011101",		/*  12 - <FF>	*/
	"11111",		/*  13 - <CR>	*/
	"1101110101",		/*  14 - <SO>	*/
	"1110101011",		/*  15 - <SI>	*/
	"1011110111",		/*  16 - <DLE>	*/
	"1011110101",		/*  17 - <DC1>	*/
	"1110101101",		/*  18 - <DC2>	*/
	"1110101111",		/*  19 - <DC3>	*/
	"1101011011",		/*  20 - <DC4>	*/
	"1101101011",		/*  21 - <NAK>	*/
	"1101101101",		/*  22 - <SYN>	*/
	"1101010111",		/*  23 - <ETB>	*/
	"1101111011",		/*  24 - <CAN>	*/
	"1101111101",		/*  25 - <EM>	*/
	"1110110111",		/*  26 - <SUB>	*/
	"1101010101",		/*  27 - <ESC>	*/
	"1101011101",		/*  28 - <FS>	*/
	"1110111011",		/*  29 - <GS>	*/
	"1011111011",		/*  30 - <RS>	*/
	"1101111111",		/*  31 - <US>	*/
	"1",			/*  32 - <SPC>	*/
	"111111111",		/*  33 - !	*/
	"101011111",		/*  34 - '"'	*/
	"111110101",		/*  35 - #	*/
	"111011011",		/*  36 - $	*/
	"1011010101",		/*  37 - %	*/
	"1010111011",		/*  38 - &	*/
	"101111111",		/*  39 - '	*/
	"11111011",		/*  40 - (	*/
	"11110111",		/*  41 - )	*/
	"101101111",		/*  42 - *	*/
	"111011111",		/*  43 - +	*/
	"1110101",		/*  44 - ,	*/
	"110101",		/*  45 - -	*/
	"1010111",		/*  46 - .	*/
	"110101111",		/*  47 - /	*/
	"10110111",		/*  48 - 0	*/
	"10111101",		/*  49 - 1	*/
	"11101101",		/*  50 - 2	*/
	"11111111",		/*  51 - 3	*/
	"101110111",		/*  52 - 4	*/
	"101011011",		/*  53 - 5	*/
	"101101011",		/*  54 - 6	*/
	"110101101",		/*  55 - 7	*/
	"110101011",		/*  56 - 8	*/
	"110110111",		/*  57 - 9	*/
	"11110101",		/*  58 - :	*/
	"110111101",		/*  59 - ;	*/
	"111101101",		/*  60 - <	*/
	"1010101",		/*  61 - =	*/
	"111010111",		/*  62 - >	*/
	"1010101111",		/*  63 - ?	*/
	"1010111101",		/*  64 - @	*/
	"1111101",		/*  65 - A	*/
	"11101011",		/*  66 - B	*/
	"10101101",		/*  67 - C	*/
	"10110101",		/*  68 - D	*/
	"1110111",		/*  69 - E	*/
	"11011011",		/*  70 - F	*/
	"11111101",		/*  71 - G	*/
	"101010101",		/*  72 - H	*/
	"1111111",		/*  73 - I	*/
	"111111101",		/*  74 - J	*/
	"101111101",		/*  75 - K	*/
	"11010111",		/*  76 - L	*/
	"10111011",		/*  77 - M	*/
	"11011101",		/*  78 - N	*/
	"10101011",		/*  79 - O	*/
	"11010101",		/*  80 - P	*/
	"111011101",		/*  81 - Q	*/
	"10101111",		/*  82 - R	*/
	"1101111",		/*  83 - S	*/
	"1101101",		/*  84 - T	*/
	"101010111",		/*  85 - U	*/
	"110110101",		/*  86 - V	*/
	"101011101",		/*  87 - W	*/
	"101110101",		/*  88 - X	*/
	"101111011",		/*  89 - Y	*/
	"1010101101",		/*  90 - Z	*/
	"111110111",		/*  91 - [	*/
	"111101111",		/*  92 - \	*/
	"111111011",		/*  93 - ]	*/
	"1010111111",		/*  94 - ^	*/
	"101101101",		/*  95 - _	*/
	"1011011111",		/*  96 - `	*/
	"1011",			/*  97 - a	*/
	"1011111",		/*  98 - b	*/
	"101111",		/*  99 - c	*/
	"101101",		/* 100 - d	*/
	"11",			/* 101 - e	*/
	"111101",		/* 102 - f	*/
	"1011011",		/* 103 - g	*/
	"101011",		/* 104 - h	*/
	"1101",			/* 105 - i	*/
	"111101011",		/* 106 - j	*/
	"10111111",		/* 107 - k	*/
	"11011",		/* 108 - l	*/
	"111011",		/* 109 - m	*/
	"1111",			/* 110 - n	*/
	"111",			/* 111 - o	*/
	"111111",		/* 112 - p	*/
	"110111111",		/* 113 - q	*/
	"10101",		/* 114 - r	*/
	"10111",		/* 115 - s	*/
	"101",			/* 116 - t	*/
	"110111",		/* 117 - u	*/
	"1111011",		/* 118 - v	*/
	"1101011",		/* 119 - w	*/
	"11011111",		/* 120 - x	*/
	"1011101",		/* 121 - y	*/
	"111010101",		/* 122 - z	*/
	"1010110111",		/* 123 - {	*/
	"110111011",		/* 124 - |	*/
	"1010110101",		/* 125 - }	*/
	"1011010111",		/* 126 - ~	*/
	"1110110101",		/* 127 - <DEL>	*/
	"1110111101",		/* 128 - 	*/
	"1110111111",		/* 129 - 	*/
	"1111010101",		/* 130 - 	*/
	"1111010111",		/* 131 - 	*/
	"1111011011",		/* 132 - 	*/
	"1111011101",		/* 133 - 	*/
	"1111011111",		/* 134 - 	*/
	"1111101011",		/* 135 - 	*/
	"1111101101",		/* 136 - 	*/
	"1111101111",		/* 137 - 	*/
	"1111110101",		/* 138 - 	*/
	"1111110111",		/* 139 - 	*/
	"1111111011",		/* 140 - 	*/
	"1111111101",		/* 141 - 	*/
	"1111111111",		/* 142 - 	*/
	"10101010101",		/* 143 - 	*/
	"10101010111",		/* 144 - 	*/
	"10101011011",		/* 145 - 	*/
	"10101011101",		/* 146 - 	*/
	"10101011111",		/* 147 - 	*/
	"10101101011",		/* 148 - 	*/
	"10101101101",		/* 149 - 	*/
	"10101101111",		/* 150 - 	*/
	"10101110101",		/* 151 - 	*/
	"10101110111",		/* 152 - 	*/
	"10101111011",		/* 153 - 	*/
	"10101111101",		/* 154 - 	*/
	"10101111111",		/* 155 - 	*/
	"10110101011",		/* 156 - 	*/
	"10110101101",		/* 157 - 	*/
	"10110101111",		/* 158 - 	*/
	"10110110101",		/* 159 - 	*/
	"10110110111",		/* 160 -  	*/
	"10110111011",		/* 161 - ˇ	*/
	"10110111101",		/* 162 - ˘	*/
	"10110111111",		/* 163 - Ł	*/
	"10111010101",		/* 164 - ¤	*/
	"10111010111",		/* 165 - Ą	*/
	"10111011011",		/* 166 - ¦	*/
	"10111011101",		/* 167 - §	*/
	"10111011111",		/* 168 - ¨	*/
	"10111101011",		/* 169 - ©	*/
	"10111101101",		/* 170 - Ş	*/
	"10111101111",		/* 171 - «	*/
	"10111110101",		/* 172 - ¬	*/
	"10111110111",		/* 173 - ­	*/
	"10111111011",		/* 174 - ®	*/
	"10111111101",		/* 175 - Ż	*/
	"10111111111",		/* 176 - °	*/
	"11010101011",		/* 177 - ±	*/
	"11010101101",		/* 178 - ˛	*/
	"11010101111",		/* 179 - ł	*/
	"11010110101",		/* 180 - ´	*/
	"11010110111",		/* 181 - µ	*/
	"11010111011",		/* 182 - ¶	*/
	"11010111101",		/* 183 - ·	*/
	"11010111111",		/* 184 - ¸	*/
	"11011010101",		/* 185 - ą	*/
	"11011010111",		/* 186 - ş	*/
	"11011011011",		/* 187 - »	*/
	"11011011101",		/* 188 - Ľ	*/
	"11011011111",		/* 189 - ˝	*/
	"11011101011",		/* 190 - ľ	*/
	"11011101101",		/* 191 - ż	*/
	"11011101111",		/* 192 - Ŕ	*/
	"11011110101",		/* 193 - Á	*/
	"11011110111",		/* 194 - Â	*/
	"11011111011",		/* 195 - Ă	*/
	"11011111101",		/* 196 - Ä	*/
	"11011111111",		/* 197 - Ĺ	*/
	"11101010101",		/* 198 - Ć	*/
	"11101010111",		/* 199 - Ç	*/
	"11101011011",		/* 200 - Č	*/
	"11101011101",		/* 201 - É	*/
	"11101011111",		/* 202 - Ę	*/
	"11101101011",		/* 203 - Ë	*/
	"11101101101",		/* 204 - Ě	*/
	"11101101111",		/* 205 - Í	*/
	"11101110101",		/* 206 - Î	*/
	"11101110111",		/* 207 - Ď	*/
	"11101111011",		/* 208 - Đ	*/
	"11101111101",		/* 209 - Ń	*/
	"11101111111",		/* 210 - Ň	*/
	"11110101011",		/* 211 - Ó	*/
	"11110101101",		/* 212 - Ô	*/
	"11110101111",		/* 213 - Ő	*/
	"11110110101",		/* 214 - Ö	*/
	"11110110111",		/* 215 - ×	*/
	"11110111011",		/* 216 - Ř	*/
	"11110111101",		/* 217 - Ů	*/
	"11110111111",		/* 218 - Ú	*/
	"11111010101",		/* 219 - Ű	*/
	"11111010111",		/* 220 - Ü	*/
	"11111011011",		/* 221 - Ý	*/
	"11111011101",		/* 222 - Ţ	*/
	"11111011111",		/* 223 - ß	*/
	"11111101011",		/* 224 - ŕ	*/
	"11111101101",		/* 225 - á	*/
	"11111101111",		/* 226 - â	*/
	"11111110101",		/* 227 - ă	*/
	"11111110111",		/* 228 - ä	*/
	"11111111011",		/* 229 - ĺ	*/
	"11111111101",		/* 230 - ć	*/
	"11111111111",		/* 231 - ç	*/
	"101010101011",		/* 232 - č	*/
	"101010101101",		/* 233 - é	*/
	"101010101111",		/* 234 - ę	*/
	"101010110101",		/* 235 - ë	*/
	"101010110111",		/* 236 - ě	*/
	"101010111011",		/* 237 - í	*/
	"101010111101",		/* 238 - î	*/
	"101010111111",		/* 239 - ď	*/
	"101011010101",		/* 240 - đ	*/
	"101011010111",		/* 241 - ń	*/
	"101011011011",		/* 242 - ň	*/
	"101011011101",		/* 243 - ó	*/
	"101011011111",		/* 244 - ô	*/
	"101011101011",		/* 245 - ő	*/
	"101011101101",		/* 246 - ö	*/
	"101011101111",		/* 247 - ÷	*/
	"101011110101",		/* 248 - ř	*/
	"101011110111",		/* 249 - ů	*/
	"101011111011",		/* 250 - ú	*/
	"101011111101",		/* 251 - ű	*/
	"101011111111",		/* 252 - ü	*/
	"101101010101",		/* 253 - ý	*/
	"101101010111",		/* 254 - ţ	*/
	"101101011011"		/* 255 - ˙	*/
};

   /*
   * The same in a format more suitable for decoding.
   */
    uint[] varicodetab2 = 
    {
	0x2AB, 0x2DB, 0x2ED, 0x377, 0x2EB, 0x35F, 0x2EF, 0x2FD, 
	0x2FF, 0x0EF, 0x01D, 0x36F, 0x2DD, 0x01F, 0x375, 0x3AB, 
	0x2F7, 0x2F5, 0x3AD, 0x3AF, 0x35B, 0x36B, 0x36D, 0x357, 
	0x37B, 0x37D, 0x3B7, 0x355, 0x35D, 0x3BB, 0x2FB, 0x37F, 
	0x001, 0x1FF, 0x15F, 0x1F5, 0x1DB, 0x2D5, 0x2BB, 0x17F, 
	0x0FB, 0x0F7, 0x16F, 0x1DF, 0x075, 0x035, 0x057, 0x1AF, 
	0x0B7, 0x0BD, 0x0ED, 0x0FF, 0x177, 0x15B, 0x16B, 0x1AD, 
	0x1AB, 0x1B7, 0x0F5, 0x1BD, 0x1ED, 0x055, 0x1D7, 0x2AF, 
	0x2BD, 0x07D, 0x0EB, 0x0AD, 0x0B5, 0x077, 0x0DB, 0x0FD, 
	0x155, 0x07F, 0x1FD, 0x17D, 0x0D7, 0x0BB, 0x0DD, 0x0AB, 
	0x0D5, 0x1DD, 0x0AF, 0x06F, 0x06D, 0x157, 0x1B5, 0x15D, 
	0x175, 0x17B, 0x2AD, 0x1F7, 0x1EF, 0x1FB, 0x2BF, 0x16D, 
	0x2DF, 0x00B, 0x05F, 0x02F, 0x02D, 0x003, 0x03D, 0x05B, 
	0x02B, 0x00D, 0x1EB, 0x0BF, 0x01B, 0x03B, 0x00F, 0x007, 
	0x03F, 0x1BF, 0x015, 0x017, 0x005, 0x037, 0x07B, 0x06B, 
	0x0DF, 0x05D, 0x1D5, 0x2B7, 0x1BB, 0x2B5, 0x2D7, 0x3B5, 
	0x3BD, 0x3BF, 0x3D5, 0x3D7, 0x3DB, 0x3DD, 0x3DF, 0x3EB, 
	0x3ED, 0x3EF, 0x3F5, 0x3F7, 0x3FB, 0x3FD, 0x3FF, 0x555, 
	0x557, 0x55B, 0x55D, 0x55F, 0x56B, 0x56D, 0x56F, 0x575, 
	0x577, 0x57B, 0x57D, 0x57F, 0x5AB, 0x5AD, 0x5AF, 0x5B5, 
	0x5B7, 0x5BB, 0x5BD, 0x5BF, 0x5D5, 0x5D7, 0x5DB, 0x5DD, 
	0x5DF, 0x5EB, 0x5ED, 0x5EF, 0x5F5, 0x5F7, 0x5FB, 0x5FD, 
	0x5FF, 0x6AB, 0x6AD, 0x6AF, 0x6B5, 0x6B7, 0x6BB, 0x6BD, 
	0x6BF, 0x6D5, 0x6D7, 0x6DB, 0x6DD, 0x6DF, 0x6EB, 0x6ED, 
	0x6EF, 0x6F5, 0x6F7, 0x6FB, 0x6FD, 0x6FF, 0x755, 0x757, 
	0x75B, 0x75D, 0x75F, 0x76B, 0x76D, 0x76F, 0x775, 0x777, 
	0x77B, 0x77D, 0x77F, 0x7AB, 0x7AD, 0x7AF, 0x7B5, 0x7B7, 
	0x7BB, 0x7BD, 0x7BF, 0x7D5, 0x7D7, 0x7DB, 0x7DD, 0x7DF, 
	0x7EB, 0x7ED, 0x7EF, 0x7F5, 0x7F7, 0x7FB, 0x7FD, 0x7FF, 
	0xAAB, 0xAAD, 0xAAF, 0xAB5, 0xAB7, 0xABB, 0xABD, 0xABF, 
	0xAD5, 0xAD7, 0xADB, 0xADD, 0xADF, 0xAEB, 0xAED, 0xAEF, 
	0xAF5, 0xAF7, 0xAFB, 0xAFD, 0xAFF, 0xB55, 0xB57, 0xB5B
    };

        private string psk_varicode_encode(int c)
        {
            return varicodetab1[c];
        }

        private int psk_varicode_decode(uint symbol)
        {
            int i;

            for (i = 0; i < 256; i++)
                if (symbol == varicodetab2[i])
                    return i;

            return -1;
        }

        #endregion

        #region complex math

        private ComplexF CmulF(ComplexF x, ComplexF y)
        {
            ComplexF z;
            z.Re = x.Re * y.Re - x.Im * y.Im;
            z.Im = x.Im * y.Re + x.Re * y.Im;
            return z;
        }

        private double carg(ComplexF x)
        {
            double z;
            z = Math.Atan2(x.Im, x.Re);
            return z;
        }

        ComplexF ccor(ComplexF x, ComplexF y)
        {
            ComplexF z;

            z.Re = x.Re * y.Re + x.Im * y.Im;
            z.Im = x.Re * y.Im - x.Im * y.Re;

            return z;
        }

        #endregion

        #region PSK decoder

        private void rx_bit(int index, int bit)
        {
            try
            {
                int c;
                uint BIT = 0;

                if (bit > 0)
                    BIT = 1;

                trx.modem[index].shreg = (trx.modem[index].shreg << 1) | BIT;

                if ((trx.modem[index].shreg & 3) == 0)
                {
                    c = psk_varicode_decode(trx.modem[index].shreg >> 2);

                    if (c != -1)
                    {
                        char a = (char)c;

                        //Debug.Write(a + " " + c.ToString() + "\n");

                        if (index == 0)
                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", 5, a.ToString());
                        else
                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", 6, a.ToString());
                    }

                    trx.modem[index].shreg = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void rx_qpsk(int index, int bits)
        {
            try
            {
                short[] sym = new short[2];
                int c;

                if (trx.modem[index].qpsk && !trx.reverse)
                    bits = (4 - bits) & 3;

                switch (bits)
                {
                    case 0:
                        sym[0] = 0;
                        sym[1] = 255;
                        break;
                    case 1:
                        sym[0] = 255;
                        sym[1] = 255;
                        break;
                    case 2:
                        sym[0] = 0;
                        sym[1] = 0;
                        break;
                    case 3:
                        sym[0] = 255;
                        sym[1] = 0;
                        break;
                }

                c = viterbi_decode(index, sym, null);

                if (c != -1)
                {
                    rx_bit(index, c & 0x80);
                    rx_bit(index, c & 0x40);
                    rx_bit(index, c & 0x20);
                    rx_bit(index, c & 0x10);
                    rx_bit(index, c & 0x08);
                    rx_bit(index, c & 0x04);
                    rx_bit(index, c & 0x02);
                    rx_bit(index, c & 0x01);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        int pll = 0;
        private void rx_symbol(int index, ComplexF symbol)
        {
            try
            {
                double error;
                int n;

                if ((trx.modem[index].phase = carg(ccor(trx.modem[index].rx_prevsymbol, symbol))) < 0)
                    trx.modem[index].phase += TWOPI;

                trx.modem[index].rx_prevsymbol = symbol;

                if (trx.modem[index].qpsk)
                {
                    trx.modem[index].bits = ((int)(trx.modem[index].phase / M_PI_2 + 0.5)) & 3;
                    n = 4;
                }
                else
                {
                    trx.modem[index].bits = (((int)(trx.modem[index].phase / M_PI + 0.5)) & 1) << 1;
                    n = 2;
                }

                trx.modem[index].quality.Re = (float)decayavg(trx.modem[index].quality.Re,
                    Math.Cos(n * trx.modem[index].phase), 50.0);
                trx.modem[index].quality.Im = (float)decayavg(trx.modem[index].quality.Im,
                    Math.Sin(n * trx.modem[index].phase), 50.0);

                /*trx.modem[index].quality.Re = (float)(0.02f * Math.Cos(n * trx.modem[index].phase) +
                    0.98f * trx.modem[index].quality.Re);
                trx.modem[index].quality.Im = (float)(0.02f * Math.Sin(n * trx.modem[index].phase) + 
                    0.98f * trx.modem[index].quality.Im);*/

                trx.modem[index].dcdshreg = (trx.modem[index].dcdshreg << 2) | (uint)trx.modem[index].bits;

                switch (trx.modem[index].dcdshreg)
                {
                    case 0xAAAAAAAA:	/* DCD on by preamble */
                        trx.modem[index].dcd = true;
                        trx.modem[index].quality.Re = 1;
                        trx.modem[index].quality.Im = 0;
                        trx.acquire = 0;
                        break;

                    case 0:			/* DCD off by postamble */
                        trx.modem[index].dcd = false;
                        trx.modem[index].quality.Re = 0;
                        trx.modem[index].quality.Im = 0;
                        break;

                    default:
                        double pwr = Math.Abs(trx.modem[index].quality.Re * trx.modem[index].quality.Re +
                            trx.modem[index].quality.Im * trx.modem[index].quality.Im);

                        if (pwr > (trx.squelch * sql))
                        {
                            trx.modem[index].dcd = true;
                        }
                        else
                        {
                            trx.modem[index].dcd = false;
                        }
                        break;
                }

                if (index == 0)
                {
                    if (trx.modem[index].dcd == true)
                    {
                        MainForm.lblPSKDCDCh1.BackColor = Color.Green;
                    }
                    else
                    {
                        MainForm.lblPSKDCDCh1.BackColor = Color.Red;
                    }
                }
                else if (index == 1)
                {
                    if (trx.modem[index].dcd == true)
                    {
                        MainForm.lblPSKDCDCh2.BackColor = Color.Green;
                    }
                    else
                    {
                        MainForm.lblPSKDCDCh2.BackColor = Color.Red;
                    }
                }

                if (trx.modem[index].dcd == true || trx.squelchon == false)
                {
                    if (trx.modem[index].qpsk)
                        rx_qpsk(index, trx.modem[index].bits);
                    else
                    {
                        if (trx.modem[index].bits > 0)
                            rx_bit(index, 0);
                        else
                            rx_bit(index, 1);
                    }

                    if (trx.modem[index].dcd)
                    {
                        error = (trx.modem[index].phase - trx.modem[index].bits * M_PI / 2);

                        if (error < M_PI / 2)
                            error += TWOPI;
                        if (error > M_PI / 2)
                            error -= TWOPI;

                        error *= (double)(trx.modem[index].rx_samplerate / (trx.modem[index].rx_symbollen * TWOPI));
                        trx.modem[index].rx_frequency -= (error / 16.0);
                        trx.modem[index].tx_frequency -= (error / 16.0);

                        if (trx.modem[index].rx_frequency > MainForm.PSKPitch + 7.0)
                        {
                            trx.modem[index].rx_frequency = MainForm.PSKPitch + 7.0;
                            trx.modem[index].tx_frequency = tx_if_shift + 7.0;
                        }
                        else if (trx.modem[index].rx_frequency < MainForm.PSKPitch - 7.0)
                        {
                            trx.modem[index].rx_frequency = MainForm.PSKPitch - 7.0;
                            trx.modem[index].tx_frequency = tx_if_shift - 7.0;
                        }

                        if (trx.afcon)
                        {
                            double difference = 0.0;
                            pll++;

                            if (pll > 20)
                            {
                                switch (index)
                                {
                                    case 0:
                                        double diff = trx.modem[index].rx_frequency - MainForm.PSKPitch;

                                        if (diff > 1.0)
                                        {
                                            difference = 0.000001;
                                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "VFO", 1,
                                                difference.ToString());
                                        }
                                        else if (diff < -1.0)
                                        {
                                            difference = -0.000001;
                                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "VFO", 1,
                                                difference.ToString());
                                        }
                                        break;

                                    case 1:
                                        diff = trx.modem[index].rx_frequency - MainForm.PSKPitch;

                                        if (diff > 1.0)
                                        {
                                            difference = 0.000001;
                                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "VFO", 2,
                                                difference.ToString());
                                        }
                                        else if (diff < -1.0)
                                        {
                                            difference = -0.000001;
                                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "VFO", 2,
                                                difference.ToString());
                                        }

                                        break;
                                }

                                pll = 0;
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

        private int RXprocess(int index, int len)
        {
            try
            {
                double delta;
                ComplexF z = new ComplexF();
                ComplexF z2 = new ComplexF();
                int i, j = 0;
                string dcd = "";
                int ptr = 0;

                delta = TWOPI * trx.modem[index].rx_frequency / trx.modem[index].rx_samplerate;

                while (len-- > 0)
                {
                    trx.modem[index].rx_phaseacc += delta;

                    if (trx.modem[index].rx_phaseacc >= M_PI)
                        trx.modem[index].rx_phaseacc -= TWOPI;

                    // Filter and downsample by 16 or 8
                    if (trx.modem[index].decimate >= trx.modem[index].decimate_ratio - 1)
                    {
                        trx.modem[index].decimate = 0;
                        double sum, ampsum;
                        int idx;

                        z2.Re = 0.0f;
                        z2.Im = 0.0f;

                        // NCO mixer
                        if (index == 0)
                        {
                            z.Re = (float)(buffer_ch1[j] * Math.Cos(trx.modem[index].rx_phaseacc));
                            z.Im = (float)(buffer_ch1[j] * Math.Sin(trx.modem[index].rx_phaseacc));
                        }
                        else if (index == 1)
                        {
                            z.Re = (float)(buffer_ch2[j] * Math.Cos(trx.modem[index].rx_phaseacc));
                            z.Im = (float)(buffer_ch2[j] * Math.Sin(trx.modem[index].rx_phaseacc));
                        }

                        // MAC filtering
                        trx.modem[index].ibuffer[trx.modem[index].pointer] = z.Re;
                        trx.modem[index].qbuffer[trx.modem[index].pointer] = z.Im;
                        trx.modem[index].pointer++;
                        ptr = trx.modem[index].pointer - trx.modem[index].length;

                        for (i = 0; i < trx.modem[index].length; i++)
                        {
                            z2.Re += (float)(trx.modem[index].ibuffer[ptr] * trx.modem[index].filter[i]);
                            z2.Im += (float)(trx.modem[index].qbuffer[ptr] * trx.modem[index].filter[i]);
                            ptr++;
                        }

                        if (trx.modem[index].pointer >= trx.modem[index].FIRBufferLen)
                        {
                            Array.Copy(trx.modem[index].ibuffer, (trx.modem[index].FIRBufferLen - trx.modem[index].length),
                                trx.modem[index].ibuffer, 0, trx.modem[index].length);
                            Array.Copy(trx.modem[index].qbuffer, (trx.modem[index].FIRBufferLen - trx.modem[index].length),
                                trx.modem[index].qbuffer, 0, trx.modem[index].length);
                            trx.modem[index].pointer = trx.modem[index].length;
                        }

                        // sync correction
                        idx = (int)Math.Min(trx.modem[index].bitclk, 15);
                        idx = Math.Max(0, idx);
                        trx.modem[index].syncbuf[idx] = 0.9 * trx.modem[index].syncbuf[idx] + 0.1 *
                            Math.Sqrt(z2.Re * z2.Re + z2.Im * z2.Im);

                        sum = 0.0;
                        ampsum = 0.0;

                        for (i = 0; i < 8; i++)
                        {
                            sum += trx.modem[index].syncbuf[i] - trx.modem[index].syncbuf[i + 8];
                            ampsum += trx.modem[index].syncbuf[i] + trx.modem[index].syncbuf[i + 8];
                        }

                        sum = (ampsum == 0 ? 0 : sum / ampsum);

                        if (sum == float.NaN)
                            sum = 0.0;

                        trx.modem[index].bitclk -= sum / 5.0f;

                        // bit clock
                        trx.modem[index].bitclk += 1;

                        if (trx.modem[index].bitclk >= 16.0)
                        {
                            trx.modem[index].bitclk -= 16.0;
                            rx_symbol(index, z2);
                        }
                    }
                    else
                        trx.modem[index].decimate++;

                    j++;
                }

                dcd = Math.Round(MainForm.PSKPitch - trx.modem[index].rx_frequency, 3).ToString("f3");
                dcd = dcd.PadRight(3, '0');
                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", 100 + index, dcd);

                return 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return -1;
            }
        }

        private double decayavg(double average, double input, double weight)
        {
            if (weight <= 1.0) return input;
            return input * (1.0 / weight) + average * (1.0 - (1.0 / weight));
        }


        #endregion

        #region PSK encoder

        public void Exchange_samples(int index, ref ComplexF[] output, ref ComplexF[] monitor, int buflen)
        {
            while (MainForm.output_ring_buf.ReadSpace() < buflen)
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
            //iq_balancer.Process(ref iq_buffer, buflen);
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

        ComplexF[] output = new ComplexF[2048];
        int ptr = 0;
        private void send_symbol(int index, int sym)
        {
            float delta, mon_delta;
            float ival, qval;
            ComplexF symbol = new ComplexF();
            int i;


            try
            {
                if (trx.modem[index].qpsk && trx.reverse)
                    sym = (4 - sym) & 3;

                /* differential QPSK modulation - top bit flipped */
                switch (sym)
                {
                    case 0:
                        symbol.Re = -1.0f;	/* 180 degrees */
                        symbol.Im = 0.0f;
                        break;
                    case 1:
                        symbol.Re = 0.0f;	/* 270 degrees */
                        symbol.Im = -1.0f;
                        break;
                    case 2:
                        symbol.Re = 1.0f;	/*   0 degrees */
                        symbol.Im = 0.0f;
                        break;
                    case 3:
                        symbol.Re = 0.0f;	/*  90 degrees */
                        symbol.Im = 1.0f;
                        break;
                }

                symbol = CmulF(trx.modem[index].tx_prevsymbol, symbol);
                delta = (float)(TWOPI * (trx.modem[index].tx_frequency) / trx.modem[index].tx_samplerate);
                mon_delta = (float)(TWOPI * trx.mon_frequency / Audio.SampleRate);

                if (trx.modem[index].qpsk)
                {
                    for (i = 0; i < trx.modem[index].tx_symbollen; i++)
                    {
                        ival = (float)(trx.modem[index].txshape[i] * trx.modem[index].tx_prevsymbol.Re +
                            (1.0 - trx.modem[index].txshape[i]) * symbol.Re);

                        qval = (float)(trx.modem[index].txshape[i] * trx.modem[index].tx_prevsymbol.Im +
                            (1.0 - trx.modem[index].txshape[i]) * symbol.Im);

                        if (ptr >= 2048)
                        {
                            ptr = 0;
                            Array.Copy(output, 0, Tx[index].input_buf, 2048, 2048);
                            Array.Copy(Tx[index].input_buf, Tx[index].filter.vector, 4096);
                            fft.FFT(Tx[index].filter.vector, 4096, FourierDirection.Forward);

                            for (int j = 0; j < 4096; j++)
                                Tx[index].filter.vector[j] = CmulF(Tx[index].filter.vector[j],
                                    Tx[index].filter.zfvec[j]);

                            fft.FFT(Tx[index].filter.vector, 4096, FourierDirection.Backward);
                            Array.Copy(Tx[index].input_buf, 2048, Tx[index].input_buf, 0, 2048);

                            if (MainForm.output_ring_buf.WriteSpace() < 2048)
                            {
                                if (run_transmiter)
                                {
                                    while (MainForm.output_ring_buf.WriteSpace() < 2048)
                                        audio_event.WaitOne(1);
                                }
                            }

                            EnterCriticalSection(cs_audio);
                            MainForm.output_ring_buf.Write(Tx[index].filter.vector, 2048);
                            LeaveCriticalSection(cs_audio);
                        }

                        output[ptr].Re = (float)((ival * (float)Math.Sin(trx.modem[index].tx_phaseacc) +
                            qval * (float)Math.Cos(trx.modem[index].tx_phaseacc)));
                        output[ptr].Im = -output[ptr].Re;

                        ptr++;

                        trx.mon_outbuf[i].Re = (float)(ival * (float)Math.Cos(trx.modem[index].mon_phaseacc));
                        trx.mon_outbuf[i].Im = (float)(qval * (float)Math.Sin(trx.modem[index].mon_phaseacc));

                        trx.modem[index].tx_phaseacc += delta;
                        trx.modem[index].mon_phaseacc += mon_delta;

                        if (trx.modem[index].tx_phaseacc > M_PI)
                            trx.modem[index].tx_phaseacc -= TWOPI;

                        if (trx.modem[index].mon_phaseacc > M_PI)
                            trx.modem[index].mon_phaseacc -= TWOPI;
                    }

                }
                else
                {
                    for (i = 0; i < trx.modem[index].tx_symbollen; i++)
                    {
                        ival = (float)(trx.modem[index].txshape[i] * trx.modem[index].tx_prevsymbol.Re +
                            (1.0 - trx.modem[index].txshape[i]) * symbol.Re);

                        if (ptr >= 2048)
                        {
                            ptr = 0;
                            Array.Copy(output, 0, Tx[index].input_buf, 2048, 2048);
                            Array.Copy(Tx[index].input_buf, Tx[index].filter.vector, 4096);
                            fft.FFT(Tx[index].filter.vector, 4096, FourierDirection.Forward);

                            for (int j = 0; j < 4096; j++)
                                Tx[index].filter.vector[j] = CmulF(Tx[index].filter.vector[j],
                                    Tx[index].filter.zfvec[j]);

                            fft.FFT(Tx[index].filter.vector, 4096, FourierDirection.Backward);
                            Array.Copy(Tx[index].input_buf, 2048, Tx[index].input_buf, 0, 2048);

                            if (MainForm.output_ring_buf.WriteSpace() < 2048)
                            {
                                if (run_transmiter)
                                {
                                    while (MainForm.output_ring_buf.WriteSpace() < 2048)
                                        audio_event.WaitOne(1);
                                }
                            }

                            EnterCriticalSection(cs_audio);
                            MainForm.output_ring_buf.Write(Tx[index].filter.vector, 2048);
                            LeaveCriticalSection(cs_audio);
                        }

                        output[ptr].Re = (float)(ival * (float)Math.Cos(trx.modem[index].tx_phaseacc));
                        output[ptr].Im = -output[ptr].Re;  // (float)(ival * (float)Math.Sin(trx.modem[index].tx_phaseacc));

                        ptr++;

                        trx.mon_outbuf[i].Im = (float)(ival * (float)Math.Cos(trx.modem[index].mon_phaseacc));
                        trx.mon_outbuf[i].Re = (float)(ival * (float)Math.Sin(trx.modem[index].mon_phaseacc));

                        trx.modem[index].tx_phaseacc += delta;
                        trx.modem[index].mon_phaseacc += mon_delta;

                        if (trx.modem[index].tx_phaseacc > M_PI)
                            trx.modem[index].tx_phaseacc -= TWOPI;

                        if (trx.modem[index].mon_phaseacc > M_PI)
                            trx.modem[index].mon_phaseacc -= TWOPI;
                    }
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
                MainForm.mon_ring_buf.Write(trx.mon_outbuf, trx.modem[index].tx_symbollen);
                LeaveCriticalSection(cs_audio);

                /* save the current symbol */
                trx.modem[index].tx_prevsymbol = symbol;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void send_bit(int index, int bit)
        {
            int sym;
            int BIT = 0;

            if (bit > 0)
                BIT = 1;

            if (trx.modem[index].qpsk)
            {
                //sym = encoder_encode(index, bit);
                trx.modem[index].encoder.shreg = (trx.modem[index].encoder.shreg << 1) | BIT;
                sym = trx.modem[index].encoder.output[trx.modem[index].encoder.shreg & trx.modem[index].encoder.shregmask];
            }
            else
                sym = bit << 1;

            send_symbol(index, sym);
        }

        private void send_char(int index, int c)
        {
            string code;

            code = psk_varicode_encode(c);

            foreach (char q in code)
            {
                if (q == '1')
                    send_bit(index, 1);
                else
                    send_bit(index, 0);
            }

            send_bit(index, 0);
            send_bit(index, 0);
        }

        private void flush_tx(int index)
        {
            int i;

            /* flush the encoder (QPSK only) */
            if (trx.modem[index].qpsk)
            {
                send_bit(index, 0);
                send_bit(index, 0);
                send_bit(index, 0);
                send_bit(index, 0);
                send_bit(index, 0);
            }

            /* DCD off sequence (unmodulated carrier) */
            for (i = 0; i < 32; i++)
                send_symbol(index, 2);
        }

        private int psk_txprocess_message(int index, string data)
        {
            char last_char = ' ';

            trx.modem[index].tx_phaseacc = 0.0;
            trx.modem[index].mon_phaseacc = 0.0;

            if (trx.tune)
            {
                while (trx.tune && MainForm.MOX)
                {
                    send_char(index, 'V');

                    if (update_trx1 && index == 0)
                        psk_reload(index);
                    else if(update_trx2 && index == 1)
                        psk_reload(index);
                }

                return 0;
            }

            while (trx.modem[index].preamble > 0)
            {
                trx.modem[index].preamble--;
                send_symbol(index, 0);

                if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                    audio_event.WaitOne(50);
            }

            foreach (char a in data)
            {
                if (run_transmiter)
                {
                    if (update_trx1 && index == 0)
                        psk_reload(index);
                    else if (update_trx2 && index == 1)
                        psk_reload(index);

                    send_char(index, a);

                    while (MainForm.output_ring_buf.ReadSpace() >= 2048)
                        audio_event.WaitOne(50);

                    MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), 
                        "Set TX text", index, a.ToString());

                    if (MainForm.keyboard.Visible)
                        MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback), 
                            "Set Keyboard text", index, a.ToString());
                }
                else
                {
                    MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set TX text", index, "");

                    if (MainForm.keyboard.Visible)
                        MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback), 
                            "Set Keyboard text", index, a.ToString());

                    trx.modem[0].tx_prevsymbol.Re = 1.0f;
                    trx.modem[0].tx_prevsymbol.Im = 0.0f;
                    return -1;
                }

                last_char = a;
            }

            trx.modem[index].preamble = tx_preamble / 2;

            while (trx.modem[index].preamble > 0)
            {
                if (run_transmiter)
                {
                    trx.modem[index].preamble--;
                    send_symbol(index, 0);

                    if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                        audio_event.WaitOne(50);
                }
                else
                    trx.modem[index].preamble = 0;
            }

            trx.modem[0].tx_prevsymbol.Re = 1.0f;
            trx.modem[0].tx_prevsymbol.Im = 0.0f;

            return 0;
        }

        private int psk_txprocess_char()
        {
            int c = 0, index = 0;

            if (MainForm.TXSplit)
                index = 1;

            trx.modem[index].tx_phaseacc = 0.0;
            trx.modem[index].mon_phaseacc = 0.0;

            if (trx.tune)
            {
                while (trx.tune && MainForm.MOX)
                {
                    send_char(index, 'V');

                    if (update_trx1 && index == 0)
                        psk_reload(index);
                    else if (update_trx2 && index == 1)
                        psk_reload(index);
                }

                return 0;
            }

            while (trx.modem[index].preamble > 0)
            {
                trx.modem[index].preamble--;
                send_symbol(index, 0);

                if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                    audio_event.WaitOne(50);
            }

            while (mox)
            {
                if (run_transmiter)
                {
                    if (update_trx1 && index == 0)
                    {
                        update_trx1 = false;
                        psk_reload(index);
                        RBmessage.Restart();
                    }
                    else if (update_trx2 && index == 1)
                    {
                        update_trx2 = false;
                        psk_reload(index);
                        RBmessage.Restart();
                    }

                    if (RBmessage.ReadSpace() > 100)
                        RBmessage.Restart();

                    if (RBmessage.ReadSpace() > 0)
                    {
                        char[] data = new char[1];
                        RBmessage.Read(ref data, 1);
                        c = data[0];
                        send_char(index, c);

                        while (MainForm.output_ring_buf.ReadSpace() >= 2048)
                            audio_event.WaitOne(50);

                        //Debug.Write(data[0].ToString());

                        MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback),
                            "Set TX text", index, data[0].ToString());

                        if (MainForm.keyboard.Visible)
                            MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                "Set Keyboard text", index, data[0].ToString());
                    }
                    else
                    /* send idle character */
                    send_char(index, ' ');
                }
            }

            trx.modem[index].preamble = tx_preamble / 2;

            while (trx.modem[index].preamble > 0)
            {
                trx.modem[index].preamble--;
                send_symbol(index, 0);

                if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                    audio_event.WaitOne(50);
            }

            MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

            if (MainForm.keyboard.Visible)
                MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                    "Set Keyboard text", 0, "");

            trx.modem[0].tx_prevsymbol.Re = 1.0f;
            trx.modem[0].tx_prevsymbol.Im = 0.0f;

            return 0;
        }

        #endregion

        #region PSK TX thread

        public bool Start(string text)
        {
            try
            {
                MainForm.genesis.WriteToDevice(18, (long)Keyer_mode.CWX);
                MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");
                message = text;

                if (tx_thread != null && tx_thread.IsAlive)
                {
                    run_transmiter = false;
                }

                if (tx_thread == null || !tx_thread.IsAlive)
                {
                    run_transmiter = true;
                    tx_thread = new Thread(new ThreadStart(TXthread));
                    tx_thread.Name = "PSK Encode Thread";
                    tx_thread.Priority = ThreadPriority.Normal;
                    tx_thread.IsBackground = true;
                    tx_thread.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
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
                    run_transmiter = true;
                    tx_thread = new Thread(new ThreadStart(TXthreadChar));
                    tx_thread.Name = "PSK Encode Thread";
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
                return false;
            }
        }

        public bool Stop()
        {
            try
            {
                trx.tune = false;
                run_transmiter = false;
                audio_event.Set();
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

        #endregion

        #region TX Thread

        public void TXthread()
        {
            try
            {
                char last = ' ';
                int index = 0;

                if (!trx.tune)
                {
                    char[] text = message.ToCharArray();

                    if (text.Length > 0 && text[text.Length - 1] == '#')
                    {
                        message = message.Remove(text.Length - 1, 1);
                        last = '#';
                    }
                }

                MainForm.MOX = true;

                if (MainForm.TXSplit)
                    index = 1;

                trx.modem[index].freqlock = true;
                trx.modem[index].preamble = tx_preamble;
                MainForm.output_ring_buf.Restart();
                MainForm.mon_ring_buf.Restart();

                if (psk_txprocess_message(index, message) < 0)
                    last = ' ';

                MainForm.MOX = false;
                MainForm.output_ring_buf.Reset();
                MainForm.mon_ring_buf.Reset();
                trx.modem[0].freqlock = false;
                trx.modem[1].freqlock = false;
                run_transmiter = false;

                if (last == '#')
                {
                    MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message repeat", 0, "");

                    MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                        "Reload Keyboard text", 0, message + "#");
                }
                else
                {
                    MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");
                    run_transmiter = false;
                }

                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                trx.modem[0].freqlock = false;
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
                psk_txprocess_char();
                run_transmiter = false;
                MainForm.MOX = false;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                trx.modem[0].freqlock = false;
            }
        }

        #endregion

        #region RX Threads

        public bool reset_after_mox = false;
        unsafe private void PSK_ThreadRX1()
        {
            float[] iq_buf = new float[4096];
            char[] out_text = new char[64];
            int step = Audio.SampleRate / 12000;
            int cnt = 0, i = 0;

            try
            {
                while (run_thread)
                {
                    AudioEvent1.WaitOne();

                    if (update_trx1)
                    {
                        update_trx1 = false;
                        psk_reload(0);
                    }

                    for (i = 0; i < 2048; i++)
                    {
                        buffer_ch1[cnt] = ch1_buffer[i];
                        cnt++;
                        i += step - 1;

                        if (cnt == 2048 / step)
                        {
                            if (!MainForm.MOX)
                            {
                                RXprocess(0, 2048 / step);
                            }

                            cnt = 0;
                        }
                    }

                    AudioEventEnd1.Set();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        unsafe private void PSK_ThreadRX2()
        {
            float[] iq_buf = new float[4096];
            char[] out_text = new char[64];
            int step = Audio.SampleRate / 12000;
            int cnt = 0, i = 0;

            try
            {
                while (run_thread)
                {
                    AudioEvent2.WaitOne();

                    if (update_trx2)
                    {
                        psk_reload(1);
                        update_trx2 = false;
                    }

                    for (i = 0; i < 2048; i++)
                    {
                        buffer_ch2[cnt] = ch2_buffer[i];
                        cnt++;
                        i += step - 1;

                        if (cnt == 2048 / step)
                        {
                            if (!MainForm.MOX)
                            {
                                RXprocess(1, 2048 / step);
                            }

                            cnt = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region TX filter

        #region structures

        [StructLayout(LayoutKind.Sequential)]
        public struct tx : IDisposable
        {
            public void Dispose()
            {
                Dispose();
                GC.SuppressFinalize(this);
            }

            public float samplerate;
            public ComplexF[] input_buf;
            public ComplexF[] output_buf;
            public Filter filter;
            public double Phase;
            public double Frequency;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Filter
        {
            public ComplexFIR coef;
            public int buflen;
            public int fftlen;
            public ComplexF[] vector;
            public ComplexF[] zfvec;
            public IntPtr pfwd;
            public IntPtr pinv;
            public float scale;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ComplexFIR
        {
            public ComplexF[] coef;
            public int size;
            public bool cplx;
            public freq frq;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct freq
        {
            public float lo;
            public float hi;
        }

        #endregion

        #region variables

        AutoResetEvent tx_event = new AutoResetEvent(false);

        #endregion

        unsafe private bool InitTXFilter(int index, int buflen, int samplerate)
        {
            try
            {
                int ncoef = buflen + 1;
                int fftlen = 4096;

                Tx[index].filter.vector = new ComplexF[fftlen];
                Tx[index].filter.zfvec = new ComplexF[fftlen];
                Tx[index].input_buf = new ComplexF[fftlen];
                Tx[index].output_buf = new ComplexF[fftlen];
                Tx[index].filter.buflen = buflen;
                Tx[index].filter.fftlen = fftlen;
                Tx[index].samplerate = Audio.SampleRate;
                Tx[index].filter.scale = (float)(1.0f / fftlen);

                CreateTXFilter(index, Audio.SampleRate, trx.modem[index].tx_frequency - trx.modem[index].bandwidth / 2,
                    trx.modem[index].tx_frequency + trx.modem[index].bandwidth / 2);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        unsafe private void CreateTXFilter(int index, int samplerate, double low_freq, double high_freq)
        {
            try
            {
                int fftlen = Tx[index].filter.fftlen;
                int ncoef = Tx[index].filter.buflen + 1;
                ComplexF[] vector = new ComplexF[fftlen];

                Tx[index].filter.coef = Create_FIR_Bandpass_COMPLEX((float)low_freq,
                    (float)high_freq, samplerate, ncoef);

                for (int i = 0; i < ncoef; i++)
                {
                    vector[fftlen - ncoef + i] = Tx[index].filter.coef.coef[i];
                }

                fft.FFT(vector, fftlen, FourierDirection.Forward);

                normalize_vec_COMPLEX(vector, Tx[index].filter.fftlen,
                    Tx[index].filter.scale);

                for (int i = 0; i < fftlen; i++)
                {
                    Tx[index].filter.zfvec[i] = vector[i];
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private ComplexFIR Create_FIR_Bandpass_COMPLEX(float lofreq, float hifreq, float samplerate, int size)
        {
            ComplexFIR p = new ComplexFIR();
            ComplexF[] h;
            float[] w;
            float fc, ff, midpoint;
            int i, msize;
            float pi = 3.14159265358928f;
            float twopi = 2 * pi;
            h = new ComplexF[size];

            if ((lofreq < -(samplerate / 2.0)) || (hifreq > (samplerate / 2.0)) || (hifreq <= lofreq))
                return p;
            else if (size < 1)
                return p;
            else
            {
                msize = size - 1;
                midpoint = (float)(0.5f * msize);
                p = Create_FIR_COMPLEX(size);
                p.frq.lo = lofreq;
                p.frq.hi = hifreq;
                h = p.coef;
                w = new float[size];
                w = makewindow(size, w);

                lofreq /= samplerate;
                hifreq /= samplerate;
                fc = (float)((hifreq - lofreq) / 2.0f);
                ff = (float)((lofreq + hifreq) * pi);

                for (i = 0; i < size; i++)
                {
                    float k = (float)i - midpoint;
                    float tmp, phs = ff * k;
                    if ((float)i != midpoint)
                        tmp = (float)((Math.Sin(twopi * k * fc) / (pi * k)) * w[i]);
                    else
                        tmp = (float)(2.0f * fc);
                    tmp *= 2.0f;
                    h[i].Re = (float)(tmp * Math.Cos(phs));
                    h[i].Im = (float)(tmp * Math.Sin(phs));
                }

                return p;
            }
        }

        private ComplexFIR Create_FIR_COMPLEX(int size)
        {
            try
            {
                ComplexFIR p = new ComplexFIR();
                p.coef = new ComplexF[size];
                p.size = size;
                p.cplx = true;
                p.frq.lo = p.frq.hi = -1.0f;
                return p;
            }
            catch (Exception ex)
            {
                ComplexFIR pr = new ComplexFIR();
                Debug.Write(ex.ToString());
                return pr;
            }
        }

        unsafe private float normalize_vec_COMPLEX(ComplexF[] z, int n, float scale)
        {
            if (z != null && n > 0)
            {
                int i;
                float big = -(float)1e15;

                for (i = 0; i < n; i++)
                {
                    float a = Cabs(z[i]);
                    big = Math.Max(big, a);
                }

                if (big > 0.0)
                {
                    float scl = (float)(scale / big);

                    for (i = 0; i < n; i++)
                        z[i] = CsclF(z[i], scl);

                    return scl;
                }
                else return 0.0f;
            }

            return 0.0f;
        }

        private float[] makewindow(int size, float[] window)
        {
            int i, j, midn;
            float freq, angle;
            midn = size >> 1;

            freq = (float)(TWOPI / size);

            for (i = 0, j = size - 1, angle = 0.0f; i <= midn;
             i++, j--, angle += freq)
                window[j] = (window[i] = (float)(0.5 - 0.5 * Math.Cos(angle)));

            return window;
        }

        #endregion

        #region COMPLEX calculations

        private float Cabs(ComplexF z)
        {
            return (float)Math.Sqrt(z.Re * z.Re + z.Im * z.Im);
        }

        private ComplexF CsclF(ComplexF x, float a)
        {
            ComplexF z;
            z.Re = x.Re * a;
            z.Im = x.Im * a;
            return z;
        }

        #endregion
    }
}