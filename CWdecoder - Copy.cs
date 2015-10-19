//=================================================================
// CWDecoder.cs
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

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace CWExpert
{
    public class CWDecode
    {
        #region variable

        delegate void CrossThreadCallback(string command, string data);
        delegate void CrossThreadSetText(string command, int channel_no, string out_txt);
        delegate void CrossThreadSetMRText(int channel_no, string out_txt);

        public const int intg = 3;
        public int eot = 0;
        public int mytimer = 0;
        public double glblthd = 8;
        public int bot = 0;
        public float[] sigave = new float[2048];
        public int tmin = 2;
        public bool lid = false;
        private int frame_segment = 4;
        public bool rx_only = true;
        public bool once = true;
        public int rcvd = 0;
        public int moni = 12;
        public int ponovi = 32;
        public bool repeat = false;
        public int loopend = 0;
        public int totalsamples = 0;
        public bool medijan = false;
        public bool logmagn = false;
        public int logf2l = 0;
        public int rate = 8000;
        public int FFTlen = 64;
        public int F2L = 128;
        public int aver = 35;
        public int bwl = 1;
        public int bwh = 62;
        public bool run_thread = false;
        public Thread CWThread1;
        public Thread CWThread2;
        public AutoResetEvent AudioEvent1;
        public AutoResetEvent AudioEvent2;
        public float[] audio_buffer_l;
        public float[] audio_buffer_r;
        public float[] fft_buff_ch5;
        public float[] fft_buff_ch6;
        public bool key = false;
        public bool nr_agn = false;
        public bool call_found = false;
        public bool rprt_found = false;
        public bool transmit = false;
        public int serial = 0;
        public string mystr = new string(' ', 64);
        public string mycall = new string(' ', 14);
        public string rst = new string(' ', 3);
        public string report = new string(' ', 4);
        public string call_sent = new string(' ', 14);
        public string call = new string(' ', 14);
        public string nmbr = new string(' ', 7);
        public string morsealpha = new string(' ', 28);
        public string morsedigit = new string(' ', 30);
        public string[] scp = new string[40350];
        public string[] rprts;
        public double[] snr;
        public string[] calls;
        public double thld = 0.01;
        public int[] enable;
        public int active = 0;
        public string[] output;
        public int[] sum;
        public int[] ave;
        public double[] Noise;
        public double[] RealF;
        public double[] ImagF;
        public float[,] Mag;
        public double[,] medo;
        public double[] maxim;
        public double[] temp;
        public double[] signal;
        public int[] broj;
        public double[] si;
        public double[] co;
        public double[] wd;
        public int[] tim;
        public double thd = 0;
        public double max = 0;
        private double Period = 0.0f;
        public int[] ctr = new int[22];
        public int tx_timer = 0;
        public int rx_timer = 50;
        public int dotmin = 0;
        public int[] bitrev;
        public float[] old1;
        public bool[] keyes;
        public int[] valid;
        public int nofs = 0;
        private float[] agcvol;
        private double[] fagcvol;
        private HiPerfTimer display_timer;
        private CWExpert MainForm;
        public bool run_rx1 = false;
        public bool run_rx2 = false;
        private Mutex fft_mutex = new Mutex();
        public bool qso = false;
        public bool rip = false;
        public double[] prag;
        public bool[] lids;
        public const int agc = 32;
        public const int wndw = 64;
        public int ovrlp;
        public bool hamming = false;
        public int counter = 0;
        public Int32 txctr = 0;
        public int activech, donja, gornja;
        public int freq = 0;
        public float[] audio_buffer;
        string active_call = "";

        #endregion

        #region properties

        private double sql = 0.1;
        public double SQL
        {
            get { return sql; }
            set 
            {
                sql = value;

                if (Noise != null)
                {
                    for (int n = 0; n < FFTlen; n++)
                    {
                        Noise[n] = value * Math.Sqrt(0.05);
                    }
                }
            }
        }

        private bool sql_on = false;
        public bool SqlOn
        {
            get { return sql_on; }
            set { sql_on = value; }
        }

        private bool rx2_enabled = false;
        public bool RX2Enabled
        {
            get { return rx2_enabled; }
            set { rx2_enabled = value; }
        }

        #endregion

        #region constructor and destructor

        public CWDecode(CWExpert mainForm)
        {
            try
            {
                MainForm = mainForm;
                display_timer = new HiPerfTimer();
                audio_buffer_l = new float[2048];
                audio_buffer_r = new float[2048];
                fft_buff_ch5 = new float[2048];
                fft_buff_ch6 = new float[2048];
                AudioEvent1 = new AutoResetEvent(false);
                AudioEvent2 = new AutoResetEvent(false);
                once = true;
                audio_buffer = new float[2048];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        ~CWDecode()
        {

        }

        public void CWDecodeClose()
        {

        }

        #endregion

        public bool Init()
        {
            int n = 0;
            int n1 = 0;
            int w = 0;
            int x = 0;
            int y = 0;
            int z = 0;

            try
            {
                if (Audio.SDRmode)
                {
                    rate = Audio.SampleRate;
                    frame_segment = 16 * (96000 / Audio.SampleRate);
                    FFTlen = 64;
                    ctr[5] = 0;
                    ctr[6] = 0;
                    F2L = 2 * FFTlen;
                    output = new string[16384];
                    sum = new int[FFTlen];
                    ave = new int[FFTlen];
                    Noise = new double[FFTlen];
                    RealF = new double[F2L * 2];
                    ImagF = new double[F2L];
                    Mag = new float[16 * frame_segment + 1, 32];    // 32 * 8mS, 32 * 64Hz
                    medo = new double[FFTlen, 5];
                    maxim = new double[F2L];
                    temp = new double[FFTlen];
                    signal = new double[64];
                    broj = new int[FFTlen];
                    si = new double[F2L];
                    co = new double[F2L];
                    wd = new double[F2L];
                    tim = new int[FFTlen];
                    bitrev = new int[F2L];
                    old1 = new float[FFTlen];
                    keyes = new bool[FFTlen];
                    valid = new int[FFTlen];
                    agcvol = new float[FFTlen];
                    fagcvol = new double[FFTlen];
                    totalsamples = F2L;
                    logf2l = (int)(Math.Round(Math.Log(totalsamples) / Math.Log(2.0)));
                    global_counterA = 0;
                    Period = (1.0 / Audio.SampleRate) * 2048.0 / ((double)frame_segment);
                    rprts = new string[FFTlen];
                    snr = new double[FFTlen];
                    calls = new string[FFTlen];
                    enable = new int[FFTlen];

                    for (n = 0; n < totalsamples; n++)
                    {
                        x = n;
                        y = 0;
                        n1 = totalsamples;
                        for (w = 1; w <= logf2l; w++)
                        {
                            n1 = n1 >> 1;
                            if (x >= n1)
                            {
                                if (w == 1)
                                    y++;
                                else
                                    y = y + (2 << (w - 2));
                                x = x - n1;
                            }
                        }
                        bitrev[n] = (byte)y;
                    }

                    nofs = 16 * frame_segment;                                           // number of decoding segments               
                    dotmin = (int)Math.Truncate(0.25 * 1.2 / (40 * Period));            // 40 wpm, 40 msec dot
                    morsealpha = "ETIANMSURWDKGOHVF*L*PJBXCYZQ";
                    morsedigit = "54*3***2 *+****16=/***(*7***8*90";

                    for (n = 0; n < FFTlen; n++)
                    {
                        sigave[n] = (float)thld;
                        signal[n] = (float)thld;
                        agcvol[n] = 0.0f;
                        fagcvol[n] = 0.0;
                        old1[n] = 0;
                        Noise[n] = 1.0;
                        temp[n] = 0;
                        ave[n] = aver;
                        sum[n] = 0;
                        tim[n] = 1;
                        calls[n] = String.Empty;
                        rprts[n] = String.Empty;
                        output[n] = String.Empty;
                        keyes[n] = false;
                        valid[n] = -1;
                        broj[n] = 0;

                        for (z = 0; z < 5; z++)
                            medo[n, z] = 0;
                    }

                    double v = 2 * Math.PI / totalsamples;

                    for (n = 0; n < totalsamples; n++)
                    {
                        RealF[n] = 0;
                        ImagF[n] = 0;
                        si[n] = -Math.Sin(n * v);
                        co[n] = Math.Cos(n * v);
                        wd[n] = 1;
                    }

                    mycall = MainForm.SetupForm.txtStnCALL.Text;
                    call_sent = mycall;
                    call_found = false;
                    rprt_found = false;
                    nr_agn = false;
                    tx_timer = 0;
                    rx_timer = ponovi;
                    transmit = false;
                    serial = 1;
                    repeat = false;

                    return true;
                }
                else
                {                                       // MorseRunner mode
                    FFTlen = 64;
                    loopend = F2L;
                    F2L = 128;
                    ovrlp = F2L / wndw;
                    logf2l = 7;
                    FFTlen = F2L / 2;
                    lids = new bool[FFTlen];
                    logf2l = (int)(Math.Round(Math.Log(F2L) / Math.Log(2.0)));
                    enable = new int[FFTlen];
                    wd = new double[F2L];
                    bitrev = new int[F2L];
                    prag = new double[FFTlen];
                    Noise = new double[FFTlen];
                    temp = new double[FFTlen];
                    ave = new int[FFTlen];
                    sum = new int[FFTlen];
                    tim = new int[FFTlen];
                    keyes = new bool[FFTlen];
                    valid = new int[FFTlen];
                    calls = new string[FFTlen];
                    rprts = new string[FFTlen];
                    output = new string[FFTlen];
                    medo = new double[FFTlen, 5];
                    old1 = new float[F2L];
                    RealF = new double[F2L];
                    ImagF = new double[F2L];
                    si = new double[F2L];
                    co = new double[F2L];
                    signal = new double[FFTlen];
                    Mag = new float[FFTlen, 32];
                    thld = sql * Math.Sqrt(0.05);
                    rate = 8000;

                    for (n = 0; n < F2L; n++)
                    {
                        x = n;
                        y = 0;
                        n1 = F2L;
                        for (w = 1; w <= logf2l; w++)
                        {
                            n1 = n1 >> 1;
                            if (x >= n1)
                            {
                                if (w == 1)
                                    y++;
                                else
                                    y = y + (2 << (w - 2));
                                x = x - n1;
                            }
                        }
                        bitrev[n] = (byte)y;
                    }

                    mycall = MainForm.SetupForm.txtStnCALL.Text;
                    call_sent = mycall;

                    //aver = int.Parse(MainForm.stn[1]);  // wpm
                    aver = (int)Math.Round(2.4 * rate / (aver * wndw));  // dot timing
                    dotmin = aver / 4;

                    double period = (double)F2L / rate;
                    moni = 12; // int.Parse(MainForm.stn[2]); //Hz
                    bwl = 2; // int.Parse(MainForm.stn[3]);  //Hz
                    bwh = 20; // (int)Math.Round(period * (moni + bwl / 2)) + 1;
                    //bwl = (int)Math.Truncate(period * (moni - bwl / 2)) - 1;
                    //moni = (int)Math.Round(period * moni);

                    ovrlp = F2L / wndw;
                    nofs = Audio.BlockSize / wndw; // number of overlaped segments
                    morsealpha = "ETIANMSURWDKGOHVF*L*PJBXCYZQ";
                    morsedigit = "54*3***2*******16*/*****7***8*90";

                    for (n = 0; n < FFTlen; n++)
                    {
                        prag[n] = thld;
                        Noise[n] = thld;
                        temp[n] = 0;
                        ave[n] = aver;
                        sum[n] = 0;
                        tim[n] = 1;
                        lids[n] = false;
                        enable[n] = 0;
                        keyes[n] = false;
                        valid[n] = -1;
                        output[n] = "";
                        calls[n] = String.Empty;
                        rprts[n] = String.Empty;

                        for (z = 0; z < 5; z++)
                            medo[n, z] = Noise[n];
                    }

                    double v = 2 * Math.PI / F2L;

                    for (n = 0; n < F2L; n++)
                    {
                        old1[n] = 0;
                        RealF[n] = 0;
                        ImagF[n] = 0;
                        si[n] = -Math.Sin(n * v);
                        co[n] = Math.Cos(n * v);

                        if (hamming)
                            wd[n] = (0.54 - 0.46 * co[n]) / (F2L - 1);
                        else
                            wd[n] = 1.0 / F2L;
                    }

                    nr_agn = false;
                    tx_timer = 0;
                    transmit = false;
                    serial = 1;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public bool reset_after_mox = false;
        private void CW_ThreadRX1()
        {
            try
            {
                if (MainForm.OpModeVFOA == Mode.CW)
                    run_rx1 = true;

                if (Audio.SDRmode)
                {
                    while (run_thread)
                    {
                        AudioEvent1.WaitOne();
                        fft_mutex.WaitOne();

                        if (FFT_Spectrum(5))
                        {
                            if (run_rx1)
                            {
                                Sig2ASCII(5);
                            }
                        }

                        fft_mutex.ReleaseMutex();
                    }
                }
                else
                {
                    if (!MainForm.hst)
                        cqcqcq();

                    while (run_thread)
                    {
                        AudioEvent1.WaitOne();
                        Spectrum();
                        ctr[5]++;

                        if (MainForm.hst && ctr[5] > 2345)
                            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Stop MR", "");

                        TRtiming();

                        if (!transmit)
                        {
                            Sig2ASCII(5);

                            if (!rx_only)
                            {
                                Analyse();
                                Respond();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_thread = false;
            }
        }

        private void CW_ThreadRX2()
        {
            try
            {
                if (MainForm.OpModeVFOB == Mode.CW)
                    run_rx2 = true;

                if (Audio.SDRmode)
                {
                    while (run_thread)
                    {
                        AudioEvent2.WaitOne();

                        fft_mutex.WaitOne();

                        if (FFT_Spectrum(6))
                        {
                            Sig2ASCII(6);
                        }

                        fft_mutex.ReleaseMutex();
                    }
                }
            }

            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_thread = false;
            }
        }

        public bool CWdecodeStart()
        {
            bool result = false;
            once = true;
            bwl = 5;
            bwh = 6;

            if (Init())
            {
                rx_only = MainForm.SetupForm.chkRXOnly.Checked;
                run_thread = true;
                CWThread1 = new Thread(new ThreadStart(CW_ThreadRX1));
                CWThread1.Name = "CW Thread RX1";
                CWThread1.Priority = ThreadPriority.Normal;
                CWThread1.IsBackground = true;
                CWThread1.Start();

                CWThread2 = new Thread(new ThreadStart(CW_ThreadRX2));
                CWThread2.Name = "CW Thread RX2";
                CWThread2.Priority = ThreadPriority.Normal;
                CWThread2.IsBackground = true;
                CWThread2.Start();
            }

            return result;
        }

        public void CWdecodeStop()
        {
            try
            {
                run_thread = false;
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

        private void cw2asc(int z)
        {
            char ch = '*';

            if (sum[z] < 29)
                ch = morsealpha[sum[z] - 1];

            else if ((sum[z] >= 31) && (sum[z] <= 62))
            {
                ch = morsedigit[(sum[z] - 31)];

                if (Char.IsDigit(ch))
                {
                    valid[z] = output[z].Length;

                    if (!MainForm.hst)
                        txctr = ctr[5];
                }
            }
            else if (sum[z] >= 75)
            {
                switch (sum[z])
                {
                    case 75:
                        ch = '?';
                        break;
                    case 76:
                        ch = '_';
                        break;
                    case 81:
                        ch = '"';
                        break;
                    case 84:
                        ch = '.';
                        break;
                    case 89:
                        ch = '@';
                        break;
                    case 93:
                        ch = '\\';
                        break;
                    case 96:
                        ch = '-';
                        break;
                    case 103:
                        ch = ';';
                        break;
                    case 106:
                        ch = '!';
                        break;
                    case 108:
                        ch = ')';
                        break;
                    case 114:
                        ch = ',';
                        break;
                    case 119:
                        ch = ':';
                        break;
                    default:
                        //                       Debug.Write("[" + i.ToString() + "]"); 
                        ch = '*';
                        break;
                }
            }

            output[z] += ch;

            if (Audio.SDRmode)
                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", z, ch.ToString());
            else
                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Append text", z, ch.ToString());

            sum[z] = 0;
        }

        private void CWdecode(int z)
        {
            int t = 0;
            try
            {
                keyes[z] = key;
                t = ctr[z] - tim[z];
                tim[z] = ctr[z];

                if (!key)
                {
                    if (t > ave[z])
                        sum[z]++;
                    else if (t > dotmin)
                        ave[z] = (2 * t + ave[z]) / 2;

                    sum[z]++;
                }
                else if (t < ave[z])
                {
                    if (t > dotmin)
                        ave[z] = (2 * t + ave[z]) / 2;

                    sum[z] = 2 * sum[z];

                    if (sum[z] > 127)
                        sum[z] = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private double[] signal_avg = new double[22];
        private double[] sig_avg = new double[22];
        private double[] signal_maxavg = new double[22];
        private double[] sig_maxavg = new double[22];
        private double[] signal_minavg = new double[22];
        private double[] sig_minavg = new double[22];
        private void Sig2ASCII(int index)
        {
            try
            {
                if (Audio.SDRmode)
                {
                    int t0 = ctr[index];
                    int n = 0;
                    thd = sql * Math.Sqrt(0.03);
                    double[] sig_min = new double[22];
                    double[] sig_max = new double[22];
                    sig_min[index] = 1.0;
                    sig_avg[index] = 0.0;
                    double thd_const = sql * Math.Sqrt(0.03);

                    if (!sql_on)
                    {
                        for (n = 0; n < nofs - 4; n++)
                        {
                            if (Mag[n, index] < sig_min[index])
                            {
                                sig_min[index] = Mag[n, index];
                            }

                            if (Mag[n, index] > sig_max[index])
                            {
                                sig_max[index] = Mag[n, index];
                            }

                            sig_avg[index] += Mag[n, index];
                        }

                        sig_avg[index] /= nofs - 4;
                        signal_avg[index] = 0.7 * signal_avg[index] + 0.3 * sig_avg[index];
                        signal_maxavg[index] = 0.8 * signal_maxavg[index] + 0.2 * sig_max[index];
                        signal_minavg[index] = 0.8 * signal_minavg[index] + 0.2 * sig_min[index];
                        //thd = Math.Max((signal_maxavg[index] - signal_minavg[index]) * 0.2, thd_const);
                        //thd = (signal_maxavg[index] - signal_minavg[index]) * 0.2;
                        thd = signal_avg[index] * 0.8;
                       // thd = 0.8 * signal_maxavg[index];
                    }
                    else
                    {
                        thd = sql * Math.Sqrt(0.03);
                    }

                    for (n = 0; n < nofs - 4; n++)
                    {
                        int b = broj[index];

                        if (Mag[n, index] > thd && Mag[n + 1, index] > thd && Mag[n + 2, index] > thd
                            && Mag[n + 3, index] > thd && Mag[n + 4, index] > thd)
                        {
                            if (b < 3)
                                b++;
                        }
                        else if (Mag[n, index] < thd && Mag[n + 1, index] < thd && Mag[n + 2, index] < thd
                            && Mag[n + 3, index] < thd && Mag[n + 4, index] < thd)
                        {
                            //if (b > 0)
                                b--;
                            //else
                                //b = 0;

                                b = Math.Max(b, 0);
                        }

                        n += 3;
                        broj[index] = b;
                        key = b > 1;
                        ctr[index]++;

                        if (key != keyes[index])
                            CWdecode(index);
                        else if (!key)
                        {
                            int t = ctr[index] - tim[index];

                            if (t == ave[index])
                            {
                                if (sum[index] > 0)
                                {
                                    cw2asc(index);
                                }
                            }
                            else if (t == (2 * ave[index]))
                            {
                                output[index] += "|";
                                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", index, " ");
                            }
                            else if (t == (3 * ave[index]))
                            {
                                //MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", index, " ");
                            }
                        }
                    }

                    Mag[0, index] = Mag[n + 2, index];      // save

                    if (Audio.SDRmode && !rx2_enabled)
                        index = 22;
                }
                else
                {
                    int to = ctr[5] * nofs;

                    for (int z = bwl; z <= bwh; z++)
                    {
                        for (int n = 0; n < nofs; n++)
                        {
                            int t = n + to - tim[z];

                            if (Mag[z, n] > prag[z])
                                key = true;
                            else
                                key = false;

                            if (key != keyes[z])
                            {
                                keyes[z] = key;
                                tim[z] = n + to;

                                if (t > dotmin && t < ave[z])
                                    ave[z] = t + ave[z] / 2;

                                if (!key)
                                {
                                    if (t > ave[z]) { sum[z]++; }
                                    sum[z]++;
                                }
                                else if (t < ave[z])
                                {
                                    sum[z] = 2 * sum[z];
                                    if (sum[z] > 75) { sum[z] = 0; }
                                }
                            }
                            else if (!key && t % ave[z] == 0)
                            {
                                if (t == ave[z])
                                {
                                    if (sum[z] > 0)
                                        cw2asc(z);
                                }
                                else if (t == 2 * ave[z])
                                {
                                    output[z] += " ";
                                }
                                else if (t == 3 * ave[z])
                                {
                                    enable[z] = to + t;

                                    if (rx_only)
                                        Debug.WriteLine("");
                                }
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

        private void MyFFT()
        {
            int i, k, m, mx, I1, I2, I3, I4, I5, x;
            double A1, A2, B1, B2, Z1, Z2;

            I1 = F2L / 2;
            I2 = 1;
            for (i = 1; i <= logf2l; i++)
            {
                I3 = 0;
                I4 = I1;
                for (k = 1; k <= I2; k++)
                {
                    x = I3 / I1;
                    I5 = bitrev[x];
                    Z1 = co[I5];
                    Z2 = si[I5];
                    loopend = I4 - 1;
                    for (m = I3; m <= loopend; m++)
                    {
                        A1 = RealF[m];
                        A2 = ImagF[m];
                        mx = m + I1;
                        B1 = Z1 * RealF[mx] - Z2 * ImagF[mx];
                        B2 = Z2 * RealF[mx] + Z1 * ImagF[mx];
                        RealF[m] = (A1 + B1);
                        ImagF[m] = (A2 + B2);
                        RealF[mx] = (A1 - B1);
                        ImagF[mx] = (A2 - B2);
                    }
                    I3 = I3 + (I1 << 1);
                    I4 = I4 + (I1 << 1);
                }
                I1 = I1 >> 1;
                I2 = I2 << 1;
            }
        }

        private double Median(double mag, int z)
        {
            const int len = 4; // one less that odd number of atoms
            double[] srt = new double[len + 1];

            for (int i = 0; i < len; i++)
            {
                medo[z, i] = medo[z, i + 1];
                srt[i] = medo[z, i];
            }
            medo[z, len] = mag;
            srt[len] = mag;
            Array.Sort(srt);
            return srt[len / 2];
        }

        private int global_counterA = 0;
        private int global_counterB = 0;
        private bool FFT_Spectrum(int index)
        {
            try
            {
                bool return_value = false;

                int z = 0;
                int n = 0;
                int i = 0;
                int k = index;

                switch (index)
                {
                    case 5:
                        signal[5] = 0.0f;

                        for (z = 1 + global_counterA; z < global_counterA + frame_segment + 1; z++)
                        {
                            for (n = i; n < (i + 2048 / frame_segment); n++)
                            {
                                signal[5] += Math.Abs(fft_buff_ch5[n]);
                            }

                            signal[5] /= 2048 / frame_segment;
                            Mag[z, 5] = (float)signal[5];
                            i += 2048 / frame_segment;
                        }

                        global_counterA += frame_segment;

                        if (global_counterA == nofs)
                        {
                            global_counterA = 0;
                            return_value = true;
                        }
                        break;

                    case 6:
                        signal[6] = 0.0f;

                        for (z = 1 + global_counterB; z < global_counterB + frame_segment + 1; z++)
                        {
                            for (n = i; n < (i + 2048 / frame_segment); n++)
                            {
                                signal[6] += Math.Abs(fft_buff_ch6[n]);
                            }

                            signal[6] /= 2048 / frame_segment;
                            Mag[z, 6] = (float)signal[6];
                            i += 2048 / frame_segment;
                        }

                        global_counterB += frame_segment;

                        if (global_counterB == nofs)
                        {
                            global_counterB = 0;
                            return_value = true;
                        }
                        break;
                }

                signal[index] = 0;

                return return_value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private bool Check_number(string nrs)
        {
            int nr = 0;
            if (Int32.TryParse(nrs, out nr))
            {
                if ((nr < 5000) && (nr > 0))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool Report_Filter(string stev)
        {
            string nmbr;

            int i = stev.IndexOf("NN");
            if (i >= 0)
                nmbr = "5" + stev.Substring(i, stev.Length - i);
            else nmbr = stev;

            nmbr = nmbr.Replace("N", "9");
            nmbr = nmbr.Replace("O", "0");
            nmbr = nmbr.Replace("T", "0");
            //            nmbr = nmbr.Replace("A", "1");
            //            nmbr = nmbr.Replace("E", "5");

            rst = "599";
            report = nmbr;

            if (nmbr.Length > 3)
            {
                char er = nmbr[0];
                char es = nmbr[1];
                char te = nmbr[2];

                int ih = es.CompareTo('5');

                if (er.Equals('5') && te.Equals('9') && ih >= 0 && ih <= 4)
                {
                    report = nmbr.Substring(3, nmbr.Length - 3);
                    if (ih != 4)
                        rst = nmbr.Substring(0, 3);
                }
            }

            return (Check_number(report));
        }

        private bool regex_check(String testValue)
        {

            Regex _rgx = new Regex("[ETIANMSH456]");

            return (_rgx.Matches(testValue).Count == testValue.Length);

        }

        private bool similar(string s)
        {
            int eq = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                Char c = s[i];
                for (int j = 0; j < call_sent.Length - 1; j++)
                    if (c == call_sent[j])
                        eq++;
            }
            if (eq >= s.Length / 2) { return true; }
            else return false;
        }

        private bool IsCall(string s)
        {
            int i = s.Length;
            if (i >= 3 && i <= 6 && Char.IsLetter(s, i - 1))
            {
                if (Char.IsDigit(s, 0))
                {
                    if (s.StartsWith("3DA")) { return true; }
                    else if (Char.IsLetter(s, 1) && Char.IsDigit(s, 2)) { return true; }
                    else { return false; }
                }
                else if (!s.StartsWith("Q"))
                {
                    if (Char.IsDigit(s, 1) || Char.IsDigit(s, 2)) { return true; }
                    else { return false; }
                }
                else { return false; }
            }
            else { return false; }
        }

        private bool Call_Filter(string mystr1)
        {

            string cl, clsn;

            if (mystr1.StartsWith("DE") || mystr1.StartsWith("TU")) { clsn = mystr1.Substring(2, mystr1.Length - 2); }
            else { clsn = mystr1; }

            if ((((clsn.Length) % 2) == 0) && (clsn.Length > 5) && Doubled(clsn))
                cl = clsn.Substring(clsn.Length / 2, clsn.Length / 2);
            else cl = clsn;

            int i = cl.IndexOf("/");
            if (i > 1 && i < cl.Length - 1)
            {
                string[] ports = cl.Split('/');
                foreach (string part in ports)
                {
                    if (IsCall(part)) { call_found = true; }
                }
            }
            else if (i == -1) { call_found = IsCall(cl); }

            if (call_found)
            {
                if (regex_check(cl) || cl.Contains("*") || cl.Contains("?") || cl.Contains(" ") || cl.Contains(mycall)) { call_found = false; }
            }
            if (call_found) { call = cl; }

            return call_found;
        }

        private bool Doubled(string mystr0)
        {

            string s1 = mystr0.Substring(0, mystr0.Length / 2);
            string s2 = mystr0.Substring(mystr0.Length / 2, mystr0.Length / 2);
            if (s1.Equals(s2))
                return true;
            else
                return false;

        }

        private void Analyse()
        {
            for (int z = bwl; z <= bwh; z++)
            {
                if (enable[z] > 0)
                {
                    string[] words = output[z].Split(' ');
                    foreach (string mystr in words)
                    {
                        if (mystr.Length >= 3)
                        {
                            if (mystr.Contains("R?") || mystr.Equals("AGN"))
                                nr_agn = true;
                            else if (mystr.Contains("CQCQ") || mystr.Contains("TEST") || mystr.Contains("QRL?"))
                                lids[z] = true;
                            else if (valid[z] >= 0)
                            {
                                MainForm.Invoke(new CrossThreadSetMRText(MainForm.WriteOutputText), z, 
                                    " " + prag[z].ToString() + " " + mystr);

                                int i = mystr.IndexOf("5NN");

                                if (i >= 3)
                                {
                                    if (Call_Filter(mystr.Substring(0, i)))
                                        calls[z] = call;

                                    if (Report_Filter(mystr.Substring(i)))
                                        rprts[z] = rst + report;
                                }
                                else
                                {
                                    if (i < 0)
                                    {
                                        if (Call_Filter(mystr))
                                            calls[z] = call;
                                    }

                                    if (Report_Filter(mystr))
                                        rprts[z] = rst + report;
                                }
                                valid[z] = -1;
                            }
                        }
                        output[z] = mystr;
                        enable[z] = 0;
                    }
                }
            }
        }

        public int f2len()
        {
            string snr = "000";
            if (serial >= 1000) { snr = serial.ToString(); }
            else { snr = serial.ToString("000"); }

            snr = snr.Replace('0', 'T');
            snr = snr.Replace('9', 'N');

            return txdots(" 5NN" + snr);
        }


        public int txdots(String message)
        {
            int[] cwnrs = { 22, 20, 18, 16, 14, 12, 14, 16, 18, 20 };
            int[] cwltr = { 8, 12, 14, 10, 4, 12, 12, 10, 6, 16, 12, 12, 10, 8, 14, 14, 16, 10, 8, 6, 10, 12, 12, 14, 16, 14 };

            int i = 0;
            int tx_len = 0;

            transmit = true;
            rx_timer = ponovi;

            do
            {
                Char c = message[i];
                if (Char.IsLetter(c))
                    tx_len += cwltr[c.CompareTo('A')];
                else if (Char.IsDigit(c))
                    tx_len += cwnrs[c.CompareTo('0')];
                else if (c.Equals(' '))
                    tx_len += 4;
                else if (c.Equals('/'))
                    tx_len += 16;
                else if (c.Equals('?'))
                    tx_len += 18;
                else if (!c.Equals('*'))
                    Debug.Write(".");
                i++;
            }
            while (i < message.Length);

            return (tx_len * 5 / nofs - 1);
        }

        private void TRtiming()
        {
            try
            {
                if (tx_timer > 0 && !transmit)
                {
                    txctr = ctr[5] + tx_timer;
                    transmit = true;
                }
                else if (transmit && ctr[5] > txctr) // && prag[moni] < 2 * Noise[moni])
                {
                    for (int n = bwl; n <= bwh; n++)
                    {
                        calls[n] = "";
                        enable[n] = 0;
                        output[n] = "";
                        rprts[n] = "";
                        valid[n] = -1;
                        prag[n] = Noise[n];
                    }

                    Debug.WriteLine(" RX " + activech);
                    tx_timer = 0;
                    txctr = ctr[5];
                    transmit = false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cqcqcq()
        {
            tx_timer = dots("CQ ") + dots(mycall) + dots(" TEST");
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send F1", "");
            Debug.Write(" CQ " + tx_timer.ToString());
            rip = false;
            activech = 0;
            qso = false;
        }

        private void Boundaries()
        {
            if (qso && activech > bwl)
            {
                donja = activech - 1;
                gornja = activech + 1;
            }
            else
            {
                donja = bwl;
                gornja = bwh;
            }
        }

        private bool CallAvail()
        {
            bool cf = false;

            Boundaries();

            int z = donja;

            while (!cf && z <= gornja)
            {
                cf = calls[z].Length > 2;
                z++;
            }

            if (cf)
            {
                call = calls[z - 1];
                activech = z - 1;
            }

            return cf;
        }

        private bool RprtAvail()
        {
            bool rf = false;

            Boundaries();

            int z = donja;

            while (!rf && z <= gornja)
            {
                if (rprts[z].Length > 3)
                {
                    freq = z;
                    rst = rprts[z].Substring(0, 3);
                    report = rprts[z].Substring(3, rprts[z].Length - 3);
                    rf = true;
                }
                else
                    z++;
            }

            return rf;
        }

        private bool Silence()
        {
            bool quiet = true;
            if (active == 0)
            {
                for (int n = bwl; n <= bwh; n++)
                {
                    if (output[n].Length > 0) { quiet = false; }
                }
            }
            else
            {
                for (int n = active - 1; n <= active + 1; n++)
                {
                    if (output[n].Length > 0) { quiet = false; }
                }
            }

            return quiet;
        }

        public int dots(String message)
        {
            int i = 0;
            int tx_len = 0;
            int[] cwnrs = { 22, 20, 18, 16, 14, 12, 14, 16, 18, 20 };
            int[] cwltr = { 8, 12, 14, 10, 4, 12, 12, 10, 6, 16, 12, 12, 10, 8, 14, 14, 16, 10, 8, 6, 10, 12, 12, 14, 16, 14 };

            while (i < message.Length)
            {
                Char c = message[i];
                if (Char.IsLetter(c))
                    tx_len += cwltr[c.CompareTo('A')];
                else if (Char.IsDigit(c))
                    tx_len += cwnrs[c.CompareTo('0')];
                else if (c.Equals(' '))
                    tx_len += 4;
                else if (c.Equals('/'))
                    tx_len += 16;
                else if (c.Equals('?'))
                    tx_len += 18;
                else if (!c.Equals('*'))
                    Debug.Write(".");
                i++;
            }
            return (aver * tx_len) / (2 * nofs);
        }

        private void Spectrum()
        {
            try
            {
                int n = 0;
                int z = 0;
                int i = 0;

                for (n = bwl - 1; n <= bwh + 1; n++)
                    signal[n] = 0;

                while (z < nofs)
                {
                    if (z < ovrlp - 1)
                    {
                        int bp = wndw * (ovrlp - z - 1);
                        for (n = 0; n < F2L; n++)
                        {
                            ImagF[n] = 0;
                            if (n < bp)
                                RealF[n] = wd[n] * old1[n + (z * wndw)];
                            else
                                RealF[n] = wd[n] * audio_buffer[n - bp];
                        }
                    }
                    else
                    {
                        for (n = 0; n < F2L; n++)
                        {
                            ImagF[n] = 0;
                            RealF[n] = wd[n] * audio_buffer[i + n];
                        }
                        i += wndw;
                    }

                    MyFFT();

                    for (n = bwl - 1; n <= bwh + 1; n++)
                    {
                        int y = bitrev[n];
                        Mag[n, z] = (float)Math.Sqrt(RealF[y] * RealF[y] + ImagF[y] * ImagF[y]);

                        if (Mag[n, z] > 4 * (float)Noise[n])
                        {
                            Mag[n, z] = 4 * (float)Noise[n];
                            //Debug.Write("SQL!" + ctr[5].ToString() + "\n");
                        }

                        if (medijan)
                            Mag[n, z] = (float)Median(Mag[n, z], n);

                        if (logmagn)
                        {
                            if (Mag[n, z] > 0.001)
                                Mag[n, z] = (float)Math.Log10(Mag[n, z]);
                        }

                        signal[n] += Mag[n, z] / nofs;
                    }
                    z++;
                }

                for (n = 0; n < (ovrlp - 1) * wndw; n++)  // Save last 3 x  64 samples
                    old1[n] = audio_buffer[i + n];

                for (n = bwl; n <= bwh; n++)
                {
                    prag[n] = (prag[n] + signal[n - 1] + signal[n] + signal[n + 1]) / 4;

                    if (prag[n] < Noise[n])
                        prag[n] = Noise[n];

                    if (MainForm.hst)
                    {
                        signal[n] = ((agc - 1) * signal[n] + signal[n]) / agc;
                        //                        if (signal[n] > prag[n])
                        //                            prag[n] = signal[n];
                    }
                }
            }

            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        void Clear_channels()
        {
            try
            {
                for (int n = 0; n < FFTlen; n++)
                {
                    calls[n] = String.Empty;
                    rprts[n] = String.Empty;
                    output[n] = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void SendTU()
        {
            rprts[activech] = "";
            enable[activech] = 0;

            if (!rst.Equals("599"))
                MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send RST", rst);

            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send NR", report);
            Debug.Write("Send TU" + tx_timer.ToString());
        }

        private void Respond()
        {
            try
            {
                bool any_call = CallAvail();
                bool report_rcvd = RprtAvail();

                if (nr_agn)
                {
                    nr_agn = false;
                    tx_timer = f2len();
                    SendReport();
                }

                if (any_call)
                {
                    tx_timer = dots(call);

                    string result = "";
                    string[] vals;

                    if (!qso && MainForm.dxcc.Analyze(call, out result))
                    {
                        Clear_channels();
                        vals = result.Split(' ');
                        SendCall(vals[0]);
                        qso = true;
                        tx_timer += dots(vals[0] + "5NN");
                        active_call = vals[0];

                        if (MainForm.dxcc != null && MainForm.dxcc.Visible)
                        {
                            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "DXCC text", result);
                        }
                    }
                    /*else if (!qso && !report_rcvd)
                    {
                        SendExch();
                        qso = true;
                    }*/

                    if (qso && report_rcvd)
                    {
                        tx_timer += dots("TU");
                        qso = false;
                    }
                    else if (qso && MainForm.dxcc.Analyze(call, out result) && active_call != call)
                    {
                        Clear_channels();
                        vals = result.Split(' ');

                        if (active_call.StartsWith(vals[0]) || vals[0].Contains(active_call) || active_call.Contains(vals[0]) ||
                            active_call.Contains(call.Remove(0, 1)) || active_call.Contains(call.Remove(call.Length - 1, 1)))
                        {
                            SendCall(vals[0]);
                            tx_timer += dots(vals[0] + "5NN");
                            active_call = vals[0];

                            if (MainForm.dxcc != null && MainForm.dxcc.Visible)
                            {
                                MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "DXCC text", result);
                            }
                        }
                    }
                }
                else if (report_rcvd)
                {
                    if (qso)
                    {
                        tx_timer = dots("TU");
                        SendTU();
                        qso = false;
                        serial++;
                        MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Add LOG entry", "");
                    }
                    else if (Math.Abs(freq - activech) < 2)  // no qso, late report!
                    {
                        RetryLast();
                        qso = true;
                    }
                    else
                    {
                        ClearMR();
                    }
                }
                else if ((ctr[5] - txctr > ponovi) && Silence())
                {
                    txctr = ctr[5];

                    if (qso && rip)
                    {
                        tx_timer = dots("?");
                        SendQuest();
                        rip = false;
                    }
                    else
                    {
                        if (MainForm.hst)
                        {
                            ClearMR();
                        }
                        else
                        {
                            cqcqcq();
                            call_sent = "";
                            active_call = "";
                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Clear text", 0, "");
                            Clear_channels();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void RetryLast()
        {
            call = call_sent;
            tx_timer = dots(call_sent) + f2len();
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send CALL", call);
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send RST", "599");
            MainForm.Invoke(new CrossThreadSetMRText(MainForm.WriteOutputText), activech, prag[activech].ToString() + " " + call);
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send Exch", "");
            Debug.Write(activech.ToString() + " Retry " + tx_timer.ToString());
        }

        public void SendQuest()
        {
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send F7", "");
            Debug.Write(" ? " + tx_timer.ToString());
        }

        public void ClearMR()
        {
            active_call = "";
            rprts[activech] = "";
            enable[activech] = 0;
            tx_timer = dots("TU");
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Clear LOG", "");
            activech = 0;
            rip = false;
            qso = false;
            Debug.WriteLine("Clear LOG!\n");
        }

        public void SendExch()
        {
            tx_timer = dots(call_sent) + f2len();
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send Exch", "");
            Debug.Write(" Exch " + tx_timer.ToString());
            Debug.Write("Send Exch! \n");
        }

        public void SendReport()
        {
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send F2", "");
            Debug.Write(" Rprt " + tx_timer.ToString());
            Debug.Write("Send RPT! \n");
        }

        public void SendCall(string what)
        {
            call_sent = what;
            calls[activech] = "";
            MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "Send CALL", what);
            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback), "Set text", activech, " " + 
                prag[activech].ToString("f4") + " " + what);
            Debug.Write("Ch: " + activech.ToString() + " " + what + " " + tx_timer.ToString());
            Debug.Write("Send CALL! \n");
        }
    }
}
