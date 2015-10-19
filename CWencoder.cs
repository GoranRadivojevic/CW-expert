//=================================================================
// CWencoder.cs
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
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace CWExpert
{
    unsafe public class CWEncode
    {
        #region DLL import

        [DllImport("Receiver.dll", EntryPoint = "StartTimer")]
        public static extern bool TimerInit(int period, int resolution);

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

        #region Enum

        enum OscType
        {
            ComplexTone = 1,
            floatTone = 0,
        }

        public enum CWToneState
        {
            CWTone_IDLE = 0,
            CWTone_WAIT = 1,
            CWTone_RISE = 2,
            CWTone_STDY = 3,
            CWTone_FALL = 4,
            CWTone_HOLD = 5,
        }

        #endregion

        #region structures

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerPrev
        {
            public int dit;
            public int dah;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerElement
        {
            public bool invtd;		// insert inverted element
            public bool psqam;			// paddles squeezed after mid-element
            public int curr;			// -1 = nothing, 0 = dit, 1 = dah
            public int iamb;			//  0 = none, 1 = squeezed, 2 = released
            public int last;			// -1 = nothing, 0 = dit, 1 = dah
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerTimeout
        {
            public float beep;
            public float dlay;
            public float elem;
            public float midl;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerLogic
        {

            public int dlay_type;		// 0 = none, 1 = interchar, 2 = interword
            public KeyerTimeout timeout;
            public KeyerElement element;
            public KeyerFlag flag;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerAutoSpace
        {
            public bool khar;
            public bool word;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerMemory
        {
            public bool dit;
            public bool dah;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyerFlag
        {
            public bool iambic;		// iambic or straight
            public bool mdlmdB;
            public KeyerMemory memory;
            public KeyerAutoSpace autospace;
            public KeyerPrev prev;
            public bool init;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Keyer_state
        {

            public int debounce;			// # seconds to read paddles
            public int mode;			// 0 = mode A, 1 = mode B
            public int weight;			// 15 -> 85%
            public float wpm;			// for iambic keyer
            public KeyerFlag flag;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rise
        {
            public float dur;
            public float incr;
            public int want;
            public int have;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Fall
        {
            public float dur;
            public float incr;
            public int want;
            public int have;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CWToneGen
        {
            public float curr;
            public float gain;
            public float mul;
            public float scl;
            public float sr;
            public oscillator osc;
            public oscillator mon_osc;
            public double harmonic;
            public double amplitude;
            public Rise rise;
            public Fall fall;
            public int size;
            public CWToneState stage;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IQ
        {
            public float phase;
            public float gain;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OSC
        {
            public int size;
            public ComplexF[] signalpoints;
            public double Phase;
            public double mon_phase;
            public double Frequency;
            public int OscillatorType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct oscillator
        {
            public double freq;
            public double phase;
            public OSC gen;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tx
        {
            public float samplerate;
            public IQ iqfix;
            public oscillator osc;
            public CWToneGen cwt;
        }

        #endregion

        #region constant

        const int NO_TIMEOUTS_SCHED = (-2);
        const int NO_ELEMENT = (-1);
        const int DIT = (0);
        const int DAH = (1);
        const int MODE_A = (0);
        const int MODE_B = (1);
        const int NO_PADDLE_SQUEEZE = (0);
        const int PADDLES_SQUEEZED = (1);
        const int PADDLES_RELEASED = (2);
        const int NO_DELAY = (0);
        const int CHAR_SPACING_DELAY = (1);
        const int WORD_SPACING_DELAY = (2);
        const int DEBOUNCE_BUF_MAX_SIZE = (30);

        #endregion

        #region variable

        delegate void CrossThreadSetText(string command, int channel_no, string out_txt);

        public tx Transmiter;
        public AutoResetEvent watchdog_event = new AutoResetEvent(false);
        private AutoResetEvent play_event = new AutoResetEvent(false);
        private AutoResetEvent audio_callback_event = new AutoResetEvent(false);
        private AutoResetEvent end_event = new AutoResetEvent(false);
        private bool play_sound = false;
        private const double TWOPI = 6.28318530717856;
        private const double M_PI = 3.14159265358928;
        public bool run_transmiter = false;
        private Thread CWEncode_thread;
        private Thread sound_thread;
        private string message = "";
        private int TONE_SIZE;
        private int TONE_DELAY = 4;
        private Mutex transmit_mutex = new Mutex();
        private Mutex tone_mutex = new Mutex();
        private CWExpert MainForm;
        private ComplexF[] zero_buffer;
        private ComplexF[] iq_buffer;
        private ComplexF[] mon_iq_buffer;
        private int time_base;					// Time base for millisecond timings. A standard 1WPM dit = 1200 ms mark, 1200 ms space
        private int wpm;					    // Word WPM
        private int ctime;					    // Character dit time (ms)
        private const string errorCode = "........";
        private int dottime;
        private int dashtime;
        delegate void CrossThreadCommand(string command, int param_1, string param_2);
        public Thread Keyer;
        public Thread watchdog;
        public bool runKeyer = false;
        public AutoResetEvent KeyerEvent = new AutoResetEvent(false);
        public KeyerState keyer_state = KeyerState.Silence;
        public Keyer_mode keyer_mode = Keyer_mode.Iambic;
        public int SemiBreak;
        private bool message_playing = false;
        unsafe private static void* cs_audio;
        private AutoResetEvent audio_event = new AutoResetEvent(false);
        public RingBufferChar RBmessage = new RingBufferChar(2048);
        bool manualMOX = false;

        private Dictionary<char, string> Morse = new Dictionary<char, string>()
		{
			{ 'A', ".-" },
			{ 'B', "-..." },
			{ 'C', "-.-." },
			{ 'D', "-.." },
			{ 'E', "." },
			{ 'F', "..-." },
			{ 'G', "--." },
			{ 'H', "...." },
			{ 'I', ".." },
			{ 'J', ".---" },
			{ 'K', "-.-" },
			{ 'L', ".-.." },
			{ 'M', "--" },
			{ 'N', "-." },
			{ 'O', "---" },
			{ 'P', ".--." },
			{ 'Q', "--.-" },
			{ 'R', ".-." },
			{ 'S', "..." },
			{ 'T', "-" },
			{ 'U', "..-" },
			{ 'V', "...-" },
			{ 'W', ".--" },
			{ 'X', "-..-" },
			{ 'Y', "-.--" },
			{ 'Z', "--.." },
            { 'a', ".-" },
			{ 'b', "-..." },
			{ 'c', "-.-." },
			{ 'd', "-.." },
			{ 'e', "." },
			{ 'f', "..-." },
			{ 'g', "--." },
			{ 'h', "...." },
			{ 'i', ".." },
			{ 'j', ".---" },
			{ 'k', "-.-" },
			{ 'l', ".-.." },
			{ 'm', "--" },
			{ 'n', "-." },
			{ 'o', "---" },
			{ 'p', ".--." },
			{ 'q', "--.-" },
			{ 'r', ".-." },
			{ 's', "..." },
			{ 't', "-" },
			{ 'u', "..-" },
			{ 'v', "...-" },
			{ 'w', ".--" },
			{ 'x', "-..-" },
			{ 'y', "-.--" },
			{ 'z', "--.." },
			{ '1', ".----" },
			{ '2', "..---" },
			{ '3', "...--" },
			{ '4', "....-" },
			{ '5', "....." },
			{ '6', "-...." },
			{ '7', "--..." },
			{ '8', "---.." },
			{ '9', "----." },
			{ '0', "-----" },
			{ '.', ".-.-.-" },
			{ ',', "--..--" },
			{ ':', "---..." },
			{ ';', "-.-.-." },
			{ '?', "..--.." },
			{ '-', "-....-" },
			{ '+', ".-.-." },
			{ '/', "-..-." },
			{ '=', "-...-" },
			{ '@', ".--.-." },
			{ '!', "-.-.--" },
			{ '%', ".----." },     //  \
			{ '(', "-.--." },
			{ ')', "-.--.-" },
			{ '$', "...-..-" },
			{ '&', ".-..." },
			{ '"', ".-..-." },
			{ '_', "..--.-" },
			{ '\r', " " },
			{ '\n', " " },
			{ '\t', " " },
			{ ' ', " " },
            { '#', "#" },
            { '*', "*" }
		};

        #endregion

        #region properties

        private bool mox = false;
        public bool MOX
        {
            get { return mox; }
            set { mox = value; }
        }

        private bool tun = false;
        public bool TUN
        {
            get { return tun; }
            set
            {
                tun = value;
                run_transmiter = false;
            }
        }

        private int tx_off_delay = 400;
        public int TXOffDelay
        {
            get { return tx_off_delay; }
            set { tx_off_delay = value; }
        }

        private double tx_if_shift = 15000.0;
        public double TXIfShift
        {
            get { return tx_if_shift; }
            set { tx_if_shift = value; }
        }

        private int cw_speed = 25;
        public int CWSpedd
        {
            get { return cw_speed; }
            set
            {
                cw_speed = value;
                wpm = cw_speed;
                ctime = time_base / wpm;
                dottime = ctime / TONE_DELAY;
                dashtime = dottime * 3;
            }
        }

        private float tx_phase = 0.0f;
        public float TXPhase
        {
            set { tx_phase = 0.001f * value; }
        }

        private float tx_gain = 0.0f;
        public float TXGain
        {
            set { tx_gain = (1.0f + 0.001f * value); }
        }

        private double mon_frequency = 700.0;
        public double MonFreq
        {
            set
            {
                mon_frequency = value;
                Transmiter.cwt.mon_osc.gen.Frequency = mon_frequency;
            }
        }

        #endregion

        #region constructor/destructor/init

        public CWEncode(CWExpert main_form)
        {
            MainForm = main_form;
            KeyerInit(MainForm.SetupForm.chkG59Iambic.Checked, (float)MainForm.SetupForm.udCWSpeed.Value);
            //TONE_SIZE = 48 * TONE_DELAY * Audio.SampleRate / 48000;
            time_base = 1200;
            wpm = cw_speed;
            ctime = time_base / wpm;                // dit time
            dottime = ctime / TONE_DELAY;
            dashtime = dottime * 3;
            Transmiter.osc.gen.Frequency = 0.0;
            Transmiter.cwt.osc.phase = 0.0;
            Transmiter.cwt.mon_osc.phase = 0.0;
            Transmiter.cwt.sr = Audio.SampleRate;
            Transmiter.cwt.osc.gen.Frequency = (TWOPI * tx_if_shift) / Transmiter.cwt.sr;
            Transmiter.cwt.mon_osc.gen.Frequency = (TWOPI * mon_frequency) / Transmiter.cwt.sr;
            MainForm.output_ring_buf.Restart();
            MainForm.mon_ring_buf.Restart();
            Transmiter = new tx();
            CreateTransmiter();
            zero_buffer = new ComplexF[2048];
            iq_buffer = new ComplexF[2048];
            mon_iq_buffer = new ComplexF[2048];
            cs_audio = (void*)0x0;
            cs_audio = NewCriticalSection();

            if (InitializeCriticalSectionAndSpinCount(cs_audio, 0x00000080) == 0)
            {
                MessageBox.Show("CriticalSection Failed", "Error!");
            }
        }

        ~CWEncode()
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

        private bool CreateTransmiter()
        {
            try
            {
                newOSC(ref Transmiter.osc, Audio.BlockSize, OscType.ComplexTone, 0.0f, 0.0f, (float)Audio.SampleRate);
                newCWToneGen(ref Transmiter.cwt.osc, 0.0f, (float)MainForm.SetupForm.udTXIfShift.Value, (float)MainForm.SetupForm.udCWRise.Value,
                    (float)MainForm.SetupForm.udCWFall.Value, TONE_SIZE, (float)Audio.SampleRate);
                newCWToneGen(ref Transmiter.cwt.mon_osc, 0.0f, (float)MainForm.SetupForm.udMonitorFrequncy.Value, (float)MainForm.SetupForm.udCWRise.Value,
                    (float)MainForm.SetupForm.udCWFall.Value, TONE_SIZE, (float)Audio.SampleRate);

                runKeyer = true;
                Keyer = new Thread(new ThreadStart(KeyerThread));
                Keyer.Name = "CW KeyThread";
                Keyer.Priority = ThreadPriority.Highest;
                Keyer.IsBackground = true;
                Keyer.Start();

                watchdog = new Thread(new ThreadStart(WatchdogThread));
                watchdog.Name = "CW KeyThread watchdog";
                watchdog.Priority = ThreadPriority.Highest;
                watchdog.IsBackground = true;
                watchdog.Start();

                sound_thread = new Thread(new ThreadStart(SoundThread));
                sound_thread.Name = "CW Encode Thread";
                sound_thread.Priority = ThreadPriority.Normal;
                sound_thread.IsBackground = true;
                sound_thread.Start();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        #endregion

        #region misc function

        public void Exchange_samples(ref ComplexF[] output, ref ComplexF[] monitor, int buflen)
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
            correctIQ(ref iq_buffer);
            MainForm.mon_ring_buf.Read(ref mon_iq_buffer, buflen);
            Array.Copy(iq_buffer, output, buflen);
            Array.Copy(mon_iq_buffer, monitor, buflen);

            LeaveCriticalSection(cs_audio);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool Start(string text)
        {
            try
            {
                if (run_transmiter)
                {
                    run_transmiter = false;
                    Stop();
                    return true;
                }

                MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");
                text = text.Replace("\n", " ");
                text = text.Replace("\r", " ");
                text = text.ToUpper();
                MainForm.genesis.WriteToDevice(18, (long)Keyer_mode.CWX);
                TONE_SIZE = 48 * TONE_DELAY * Audio.SampleRate / 48000;
                time_base = 1200;
                wpm = cw_speed;
                ctime = time_base / wpm;                // dit time
                dottime = ctime / TONE_DELAY;
                dashtime = dottime * 3;
                Transmiter.osc.gen.Frequency = 0.0;
                Transmiter.cwt.osc.phase = 0.0;
                Transmiter.cwt.mon_osc.phase = 0.0;
                Transmiter.cwt.sr = Audio.SampleRate;
                Transmiter.cwt.osc.gen.Frequency = (2.0 * M_PI * tx_if_shift) / Transmiter.cwt.sr;
                Transmiter.cwt.mon_osc.gen.Frequency = (2.0 * M_PI * mon_frequency) / Transmiter.cwt.sr;
                MainForm.output_ring_buf.Restart();
                MainForm.mon_ring_buf.Restart();
                play_sound = false;
                message = text;
                run_transmiter = true;
                audio_callback_event.Reset();

                if (CWEncode_thread != null && CWEncode_thread.IsAlive)
                {
                    audio_event.Set();
                    run_transmiter = false;
                }

                if (CWEncode_thread == null || !CWEncode_thread.IsAlive)
                {
                    manualMOX = false;
                    CWEncode_thread = new Thread(new ThreadStart(Play_message_thread));
                    CWEncode_thread.Name = "CW Encode Thread";
                    CWEncode_thread.Priority = ThreadPriority.Normal;
                    CWEncode_thread.IsBackground = true;
                    CWEncode_thread.Start();
                }
                else
                    CWEncode_thread.Abort();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_transmiter = false;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ManualStart()
        {
            try
            {
                if (run_transmiter)
                {
                    run_transmiter = false;
                    Stop();
                    return true;
                }

                MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");
                MainForm.genesis.WriteToDevice(18, (long)Keyer_mode.CWX);
                TONE_SIZE = 48 * TONE_DELAY * Audio.SampleRate / 48000;
                time_base = 1200;
                wpm = cw_speed;
                ctime = time_base / wpm;                // dit time
                dottime = ctime / TONE_DELAY;
                dashtime = dottime * 3;
                Transmiter.osc.gen.Frequency = 0.0;
                Transmiter.cwt.osc.phase = 0.0;
                Transmiter.cwt.mon_osc.phase = 0.0;
                Transmiter.cwt.sr = Audio.SampleRate;
                Transmiter.cwt.osc.gen.Frequency = (2.0 * M_PI * tx_if_shift) / Transmiter.cwt.sr;
                Transmiter.cwt.mon_osc.gen.Frequency = (2.0 * M_PI * mon_frequency) / Transmiter.cwt.sr;
                MainForm.output_ring_buf.Restart();
                MainForm.mon_ring_buf.Restart();
                play_sound = false;
                MainForm.MOX = true;
                run_transmiter = true;
                audio_callback_event.Reset();

                if (CWEncode_thread != null && CWEncode_thread.IsAlive)
                {
                    audio_event.Set();
                    run_transmiter = false;
                }

                if (CWEncode_thread == null || !CWEncode_thread.IsAlive)
                {
                    manualMOX = true;
                    CWEncode_thread = new Thread(new ThreadStart(Play_char_thread));
                    CWEncode_thread.Name = "CW Encode Char Thread";
                    CWEncode_thread.Priority = ThreadPriority.Normal;
                    CWEncode_thread.IsBackground = true;
                    CWEncode_thread.Start();
                }
                else
                    CWEncode_thread.Abort();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                run_transmiter = false;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            try
            {
                audio_event.Set();
                message_playing = false;
                Transmiter.cwt.stage = CWToneState.CWTone_FALL;
                run_transmiter = false;
                audio_callback_event.Set();
                end_event.Set();
                play_event.Set();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ManualStop()
        {
            try
            {
                audio_event.Set();
                message_playing = false;
                Transmiter.cwt.stage = CWToneState.CWTone_FALL;
                run_transmiter = false;
                audio_callback_event.Set();
                end_event.Set();
                play_event.Set();
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

        private string DotDash(string Text)
        {
            if (Text == null || Text == "")
            {
                Debug.Write("Empty message! \n");
                return string.Empty;
            }

            Dictionary<char, string> dict = (true ? Morse : Morse);
            string buf = "";
            bool inProsign = false;
            char b = ' ';

            foreach (char c in Text.ToUpper())
            {
                if (c == '\\')												// Prosign delimiter
                {
                    b = '%';
                }
                else
                    b = c;

                if (dict.ContainsKey(b))
                {
                    buf += dict[b];
                }
                else
                {
                    buf += "[" + b + "]";									// Print unknown character
                }
                if (b != ' ' && !inProsign && b != '#' && b != '*')                 // # last for repeat
                    buf += " ";
            }

            return buf;
        }

        private string DotDashChar(char c)
        {
            try
            {
                Dictionary<char, string> dict = (true ? Morse : Morse);
                string buf = "";
                bool inProsign = false;
                char b = ' ';

                if (c == '\\')
                {
                    b = '%';
                }
                else
                    b = c;

                if (dict.ContainsKey(b))
                {
                    buf += dict[b];
                }
                else
                {
                    buf += "[" + b + "]";
                }

                if (b != ' ' && !inProsign && b != '#' && b != '*')
                    buf += " ";

                return buf;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return "";
            }
        }

        #endregion

        #region Play sound

        /// <summary>
        /// 
        /// </summary>
        private void Play_message_thread()
        {
            try
            {
                char last = 'x';
                string msg = "";
                message_playing = true;
                MainForm.MOX = true;

                while (run_transmiter)
                {
                    if (tun)
                    {
                        while (tun && MainForm.MOX)
                        {
                            if (run_transmiter)
                            {
                                Dash();

                                while (MainForm.output_ring_buf.ReadSpace() >= 2048)
                                    audio_event.WaitOne(50);
                            }
                            else
                                return;
                        }

                        run_transmiter = false;
                        MainForm.MOX = false;
                        message_playing = false;
                        play_event.Set();
                        MainForm.output_ring_buf.Reset();
                        MainForm.mon_ring_buf.Reset();
                        return;
                    }

                    char[] m = message.ToCharArray();

                    foreach (char c in m)
                    {
                        if (run_transmiter)
                        {
                            msg = DotDashChar(c);
                            SemiBreak = 0;                  // reset watchdog

                            foreach (char t in msg)
                            {
                                if (last == '.' || last == '-')
                                    Space();

                                if (t == '.')
                                {
                                    Dot();
                                }
                                else if (t == '-' || t == '*')
                                {
                                    Dash();
                                }
                                else if (t == ' ')
                                    Pause();

                                last = t;

                                if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                                {
                                    while ((MainForm.output_ring_buf.ReadSpace() >= 2048))
                                    {
                                        if (run_transmiter)
                                        {
                                            SemiBreak = 0;                  // reset watchdog
                                            audio_event.WaitOne(5);
                                        }
                                        else
                                            goto end;
                                    }
                                }
                            }

                            MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback),
                                "Set TX text", 0, c.ToString());

                            if (MainForm.keyboard.Visible)
                                MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                    "Set Keyboard text", 0, "");
                        }
                        else
                        {
                            last = ' ';

                            if (MainForm.keyboard.Visible)
                                MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                    "Reload Keyboard text", 0, "");
                        }
                    }

                end:
                    run_transmiter = false;

                    if (last == '#')
                    {
                        MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message repeat", 0, "");

                        if (MainForm.keyboard.Visible)
                            MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                "Reload Keyboard text", 0, message);

                        run_transmiter = false;
                        MainForm.output_ring_buf.Reset();
                        MainForm.mon_ring_buf.Reset();
                    }
                    else
                    {
                        MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

                        if (MainForm.keyboard.Visible)
                            MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                "Set Keyboard text", 0, "");
                    }
                }

                MainForm.MOX = false;
                message_playing = false;
                play_event.Set();
                run_transmiter = false;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void Play_char_thread()
        {
            try
            {
                char last = 'x';
                string msg = "";
                message_playing = true;
                int index = 0;

                if (MainForm.TXSplit)
                    index = 1;

                while (run_transmiter)
                {
                    if (tun)
                    {
                        while (tun && MainForm.MOX)
                        {
                            if (run_transmiter)
                            {
                                Dash();

                                while (MainForm.output_ring_buf.ReadSpace() >= 2048)
                                    audio_event.WaitOne(50);
                            }
                            else
                                return;
                        }

                        run_transmiter = false;
                        MainForm.MOX = false;
                        message_playing = false;
                        play_event.Set();
                        MainForm.output_ring_buf.Reset();
                        MainForm.mon_ring_buf.Reset();
                        return;
                    }
                    else
                    {
                        char[] m = new char[1];

                        if (RBmessage.ReadSpace() > 100)
                            RBmessage.Restart();

                        if (RBmessage.ReadSpace() > 0)
                        {
                            RBmessage.Read(ref m, 1);

                            if (run_transmiter)
                            {
                                msg = DotDashChar(m[0]);
                                SemiBreak = 0;                  // reset watchdog

                                foreach (char t in msg)
                                {
                                    if (last == '.' || last == '-')
                                        Space();

                                    if (t == '.')
                                    {
                                        Dot();
                                    }
                                    else if (t == '-' || t == '*')
                                    {
                                        Dash();
                                    }
                                    else if (t == ' ')
                                        Pause();

                                    last = t;

                                    if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                                    {
                                        while ((MainForm.output_ring_buf.ReadSpace() >= 2048))
                                        {
                                            if (run_transmiter)
                                            {
                                                SemiBreak = 0;                  // reset watchdog
                                                audio_event.WaitOne(50);
                                            }
                                            else
                                                goto end;
                                        }
                                    }
                                }

                                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback),
                                    "Set TX text", index, m[0].ToString());

                                if (MainForm.keyboard.Visible)
                                    MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                        "Set Keyboard text", index, m[0].ToString());

                                //Debug.Write("CWencoder: " + m[0].ToString() + "\n");
                            }
                            else
                                goto end;
                        }
                        else
                        {
                            if (MainForm.output_ring_buf.ReadSpace() <= 2048)
                            {
                                while ((MainForm.output_ring_buf.ReadSpace() <= 2048))
                                {
                                    if (run_transmiter)
                                    {
                                        Pause();
                                        SemiBreak = 0;                  // reset watchdog
                                        audio_event.WaitOne(50);
                                    }
                                    else
                                        goto end;
                                }
                            }
                        }
                    }
                }

            end:
                run_transmiter = false;

                MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

                if (MainForm.keyboard.Visible)
                    MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                        "Set Keyboard text", 0, "");

                MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "MOX", 0, "");
                message_playing = false;
                play_event.Set();
                run_transmiter = false;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Play_char_thread_old()
        {
            try
            {
                message_playing = true;
                MainForm.MOX = true;

                while (run_transmiter)
                {
                    if (tun)
                    {
                        while (tun && mox)
                        {
                            if (run_transmiter)
                            {
                                Dash();

                                while (MainForm.output_ring_buf.ReadSpace() >= 2048)
                                    audio_event.WaitOne(50);
                            }
                            else
                                return;
                        }

                        run_transmiter = false;
                        MOX = false;
                        message_playing = false;
                        play_event.Set();
                        MainForm.output_ring_buf.Reset();
                        MainForm.mon_ring_buf.Reset();
                        return;
                    }

                    if (RBmessage.ReadSpace() > 0)
                    {
                        string msg = "cq yt7pwr +k";
                        string message = "";
                        char[] data = new char[1];
                        RBmessage.Read(ref data, 1);

                        if (run_transmiter)
                        {
                            foreach (char c in msg)
                            {
                                message = DotDashChar(c);
                                SemiBreak = 0;                  // reset watchdog

                                foreach (char t in message)
                                {
                                    Debug.Write(t.ToString());

                                    if (t == '.')
                                    {
                                        Dot();
                                    }
                                    else if (t == '-' || t == '*')
                                    {
                                        Dash();
                                    }
                                    else if (t == ' ')
                                        Pause();

                                    if (MainForm.output_ring_buf.ReadSpace() >= 2048)
                                    {
                                        while ((MainForm.output_ring_buf.ReadSpace() >= 2048))
                                        {
                                            if (run_transmiter)
                                            {
                                                SemiBreak = 0;                  // reset watchdog
                                                audio_event.WaitOne(5);
                                            }
                                            else
                                                goto end;
                                        }
                                    }
                                }

                                Debug.Write("\n");

                                MainForm.Invoke(new CrossThreadSetText(MainForm.CommandCallback),
                                    "Set TX text", 0, data[0].ToString());

                                if (MainForm.keyboard.Visible)
                                    MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                                        "Set Keyboard text", 0, data[0].ToString());
                            }
                        }
                    }
                    else
                    {
                        Space();
                        //Thread.Sleep(1);
                        SemiBreak = 0;
                    }
                }

                end:

                MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "Message end", 0, "");

                if (MainForm.keyboard.Visible)
                    MainForm.Invoke(new CrossThreadSetText(MainForm.keyboard.CommandCallback),
                        "Set Keyboard text", 0, "");

                MainForm.MOX = false;
                message_playing = false;
                play_event.Set();
                run_transmiter = false;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Dot()
        {
            if (!play_sound)
            {
                CWToneOn();
                play_sound = true;
            }

            for (int i = 0; i < dottime; i++)
            {
                if (run_transmiter)
                {
                    play_event.Set();
                    audio_callback_event.WaitOne();
                }
                else
                {

                }
            }

            //Debug.Write("Play dot! \n");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Dash()
        {
            if (!play_sound)
            {
                CWToneOn();
                play_sound = true;
            }

            for (int i = 0; i < dashtime; i++)
            {
                if (run_transmiter)
                {
                    play_event.Set();
                    audio_callback_event.WaitOne();
                }
                else
                {

                }
            }

            //Debug.Write("Play dash! \n");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Space()
        {
            if (play_sound)
            {
                Transmiter.cwt.stage = CWToneState.CWTone_FALL;
            }

            for (int i = 0; i < dottime; i++)
            {
                if (run_transmiter)
                {
                    play_event.Set();
                    audio_callback_event.WaitOne();
                }
                else
                {

                }
            }

            //Debug.Write("Play space! \n");
        }

        /// <summary>
        /// 
        /// </summary>
        private void Pause()
        {
            if (play_sound)
            {
                Transmiter.cwt.stage = CWToneState.CWTone_FALL;
            }

            for (int i = 0; i < dottime * 2; i++)
            {
                if (run_transmiter)
                {
                    play_event.Set();
                    audio_callback_event.WaitOne();
                }
                else
                {

                }
            }

            //Debug.Write("Play pause! \n");
        }

        /// <summary>
        /// 
        /// </summary>
        private void SoundThread()
        {
            try
            {
                while (true)
                {
                    play_event.WaitOne();

                    if (play_sound)
                    {
                        //MainForm.genesis.WriteToDevice(24, 1);
                        play_sound = CWTone();
                        EnterCriticalSection(cs_audio);
                        MainForm.output_ring_buf.Write(Transmiter.cwt.osc.gen.signalpoints, TONE_SIZE);
                        MainForm.mon_ring_buf.Write(Transmiter.cwt.mon_osc.gen.signalpoints, TONE_SIZE);
                        LeaveCriticalSection(cs_audio);
                        //Debug.Write("Write sound! \n");
                    }
                    else
                    {
                        //MainForm.genesis.WriteToDevice(24, 0);
                        EnterCriticalSection(cs_audio);
                        MainForm.output_ring_buf.Write(zero_buffer, TONE_SIZE);
                        MainForm.mon_ring_buf.Write(zero_buffer, TONE_SIZE);
                        LeaveCriticalSection(cs_audio);
                        //Debug.Write("Write silence! \n");
                    }

                    audio_callback_event.Set();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void correctIQ(ref ComplexF[] buffer)
        {
            int i;

            for (i = 0; i < 2048; i++)
            {
                buffer[i].Im += tx_phase * buffer[i].Re;
                buffer[i].Re *= tx_gain;
            }
        }

        #endregion

        #region CW generation

        private bool newCWToneGen(ref oscillator osc,
              float gain,	// dB
              float freq,	// ms
              float rise,	// ms
              float fall,	// ms
              int size,		// samples
              float samplerate) // samples/sec
        {
            setCWToneGenVals(ref Transmiter.cwt, gain, freq, rise, fall);
            Transmiter.cwt.size = size;
            Transmiter.cwt.sr = samplerate;

            newOSC(ref osc, Transmiter.cwt.size, OscType.ComplexTone,
                (double)freq, 0.0, Transmiter.cwt.sr);

            return true;
        }

        private void setCWToneGenVals(ref CWToneGen cwt, float gain, float freq, float rise, float fall)
        {
            cwt.gain = gain;
            cwt.osc.freq = freq;
            cwt.rise.dur = rise;
            cwt.fall.dur = fall;
        }

        private void newOSC(ref oscillator osc, int size, OscType TypeOsc, double Frequency,
            double Phase, float SampleRate)
        {
            try
            {
                osc.gen.signalpoints = new ComplexF[2048];
                osc.gen.size = size;
                osc.gen.Frequency = 2.0 * Math.PI * Frequency / SampleRate;
                osc.gen.Phase = Phase;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void CWComplexOSC()
        {
            int i;

            for (i = 0; i < TONE_SIZE; i++)
            {
                Transmiter.cwt.osc.gen.signalpoints[i].Re = (float)Math.Cos(Transmiter.cwt.osc.gen.Phase);
                Transmiter.cwt.osc.gen.signalpoints[i].Im = (float)(Math.Sin(Transmiter.cwt.osc.gen.Phase));

                Transmiter.cwt.mon_osc.gen.signalpoints[i].Re = (float)Math.Cos(Transmiter.cwt.mon_osc.gen.Phase);
                Transmiter.cwt.mon_osc.gen.signalpoints[i].Im = (float)(Math.Sin(Transmiter.cwt.mon_osc.gen.Phase));

                Transmiter.cwt.osc.gen.Phase += Transmiter.cwt.osc.gen.Frequency;
                Transmiter.cwt.mon_osc.gen.Phase += Transmiter.cwt.mon_osc.gen.Frequency;

                if (Transmiter.cwt.osc.gen.Phase > Math.PI)
                    Transmiter.cwt.osc.gen.Phase -= TWOPI;

                if (Transmiter.cwt.mon_osc.gen.Phase > Math.PI)
                    Transmiter.cwt.mon_osc.gen.Phase -= TWOPI;
            }
        }

        private bool CWTone()
        {
            int i, n = TONE_SIZE;

            CWComplexOSC();

            for (i = 0; i < n; i++)
            {
                // in an envelope stage?
                if (Transmiter.cwt.stage == CWToneState.CWTone_RISE)
                {
                    // still going?
                    if (Transmiter.cwt.rise.have++ < Transmiter.cwt.rise.want)
                    {
                        Transmiter.cwt.curr += Transmiter.cwt.rise.incr;
                        Transmiter.cwt.mul = Transmiter.cwt.scl * (float)Math.Sin(Transmiter.cwt.curr * M_PI / 2.0);
                    }
                    else
                    {
                        // no, assert steady-state, force level
                        Transmiter.cwt.curr = 1.0f;
                        Transmiter.cwt.mul = Transmiter.cwt.scl;
                        Transmiter.cwt.stage = CWToneState.CWTone_STDY;
                        // won't come back into envelopes
                        // until FALL asserted from outside
                    }
                }
                else if (Transmiter.cwt.stage == CWToneState.CWTone_FALL)
                {
                    // still going?
                    if (Transmiter.cwt.fall.have++ < Transmiter.cwt.fall.want)
                    {
                        Transmiter.cwt.curr -= Transmiter.cwt.fall.incr;
                        Transmiter.cwt.mul = Transmiter.cwt.scl * (float)Math.Sin(Transmiter.cwt.curr * M_PI / 2.0);
                    }
                    else
                    {
                        // no, assert trailing, force level
                        Transmiter.cwt.curr = 0.0f;
                        Transmiter.cwt.mul = 0.0f;
                        Transmiter.cwt.stage = CWToneState.CWTone_HOLD;
                        //play_sound = false;
                        // won't come back into envelopes hereafter
                    }
                }
                // apply envelope
                // (same base as osc.gen internal buf)
                Transmiter.cwt.osc.gen.signalpoints[i] = CsclF(Transmiter.cwt.osc.gen.signalpoints[i], Transmiter.cwt.mul);
                Transmiter.cwt.mon_osc.gen.signalpoints[i] = CsclF(Transmiter.cwt.mon_osc.gen.signalpoints[i], Transmiter.cwt.mul);
            }

            // indicate whether it's turned itself off
            // sometime during this pass
            return Transmiter.cwt.stage != CWToneState.CWTone_HOLD;
        }

        private void CWToneOn()
        {
            // gain is in dB
            Transmiter.cwt.scl = (float)Math.Pow(10.0, Transmiter.cwt.gain / 20.0);
            Transmiter.cwt.curr = Transmiter.cwt.mul = 0.0f;

            // A/R times are in msec
            Transmiter.cwt.rise.want = (int)(0.5 + Transmiter.cwt.sr * (Transmiter.cwt.rise.dur / 1e3));
            Transmiter.cwt.rise.have = 0;
            if (Transmiter.cwt.rise.want <= 1)
                Transmiter.cwt.rise.incr = 1.0f;
            else
                Transmiter.cwt.rise.incr = 1.0f / (Transmiter.cwt.rise.want - 1);

            Transmiter.cwt.fall.want = (int)(0.5 + Transmiter.cwt.sr * (Transmiter.cwt.fall.dur / 1e3));
            Transmiter.cwt.fall.have = 0;
            if (Transmiter.cwt.fall.want <= 1)
                Transmiter.cwt.fall.incr = 1.0f;
            else
                Transmiter.cwt.fall.incr = 1.0f / (Transmiter.cwt.fall.want - 1);

            Transmiter.cwt.osc.gen.Phase = 0.0;
            Transmiter.cwt.mon_osc.gen.Phase = 0.0;

            Transmiter.cwt.stage = CWToneState.CWTone_RISE;
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

        #endregion

        #region Keyer thread function

        HiPerfTimer timer = new HiPerfTimer();
        public bool local_mox = false;
        private void KeyerThread()
        {
            bool dot = false;
            bool dash = false;
            bool keydown = false;
            float del = 0.0f;

            try
            {
                TimerInit(4, 1);    // start 4*1ms timer

                while (runKeyer)
                {
                    timer.Start();
                    TimerWait();

                    if (MainForm.PWR && MainForm.genesis.KEYER == 170 && MainForm.genesis.KeyerNewData)
                    {
                        MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "MOX", 1, "");
                        MainForm.genesis.KEYER = 0xff;
                    }


                    if (MainForm.PWR && ((MainForm.OpModeVFOA == Mode.CW && !MainForm.TXSplit) ||
                        (MainForm.OpModeVFOB == Mode.CW && MainForm.TXSplit)) && MainForm.MOX)
                    {
                        run_transmiter = true;

                        if (!message_playing)
                        {
                            if (MainForm.genesis.KeyerNewData)
                            {
                                switch (MainForm.genesis.KEYER)
                                {
                                    case 1:
                                        dot = true;
                                        break;

                                    case 3:
                                        {
                                            dot = false;
                                        }
                                        break;

                                    case 0:
                                        {
                                            dash = true;
                                        }
                                        break;

                                    case 2:
                                        {
                                            dash = false;
                                        }
                                        break;
                                }

                                MainForm.genesis.KeyerNewData = false;
                            }

                            timer.Stop();
                            del = (float)timer.DurationMsec;
                            //Debug.Write(del.ToString() + "\n");

                            if (dot || dash)
                                SemiBreak = 0; // refresh cw watchdog

                            // read key; tell keyer elapsed time since last call
                            keydown = read_key(del, dot, dash);
                            //Debug.Write(keydown.ToString() + "\n");

                            if (!play_sound && keydown)
                            {
                                CWToneOn();
                                play_sound = true;
                            }
                            else if (play_sound && !keydown)
                            {
                                Transmiter.cwt.stage = CWToneState.CWTone_FALL;     // CWTone off
                            }

                            play_event.Set();
                        }

                        while ((MainForm.output_ring_buf.ReadSpace() >= 2048))
                        {
                            if (run_transmiter)
                            {
                                audio_event.WaitOne(2);
                            }
                        }
                    }
                    else if (!message_playing)
                        run_transmiter = false;

                    timer.Stop();
                }

                TimerStop();
            }
            catch (Exception ex)
            {
                TimerStop();
                MessageBox.Show("Error in KeyThread!\n" + ex.ToString());
            }
        }

        private void WatchdogThread()
        {
            try
            {
                while (true)
                {
                    if (MainForm.PWR && MainForm.OpModeVFOA == Mode.CW && MainForm.MOX && !manualMOX)
                    {
                        int time = dashtime;  // 21 * 96000 / Audio.SampleRate;

                        watchdog_event.WaitOne(time);

                        SemiBreak += dashtime;  // 20 * 96000 / Audio.SampleRate;

                        if (SemiBreak > tx_off_delay && !tun)
                        {
                            MainForm.Invoke(new CrossThreadCommand(MainForm.CommandCallback), "MOX", 0, "");
                            SemiBreak = 0;
                        }
                    }
                    else
                        watchdog_event.WaitOne(1000);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Keyer

        bool bug = false;
        bool iambic = false;
        Keyer_state ks;
        KeyerLogic kl;
        int debounce_buf_i = 0;
        bool[] debounce_buf = new bool[DEBOUNCE_BUF_MAX_SIZE];
        bool[] dah_debounce_buf = new bool[DEBOUNCE_BUF_MAX_SIZE];
        bool[] dit_debounce_buf = new bool[DEBOUNCE_BUF_MAX_SIZE];
        int keystate = 0;

        void KeyerInit(bool niambic, float wpm)
        {
            kl = new KeyerLogic();
            ks = new Keyer_state();
            ks.flag.iambic = niambic;
            ks.flag.autospace.khar = ks.flag.autospace.word = false;
            ks.flag.mdlmdB = true;
            ks.flag.memory.dah = true;
            ks.flag.memory.dit = true;
            ks.debounce = 2;		// could be more if sampled faster
            ks.mode = MODE_B;
            ks.weight = (int)MainForm.SetupForm.udCWWeight.Value;
            ks.wpm = wpm;
            iambic = niambic;
            TONE_SIZE = 192 * (int)(Audio.SampleRate / 48000.0);
        }

        bool read_key(float del, bool dot, bool dash)
        {
            if (bug)
            {
                if (dash)
                    return read_straight_key(ref ks, dash);
                else
                    return read_iambic_key(ref ks, dot, false, ref kl, del);
            }
            if (iambic)
                return read_iambic_key(ref ks, dot, dash, ref kl, del);
            return read_straight_key(ref ks, dot | dash);
        }

        bool read_straight_key(ref Keyer_state ks, bool keyed)
        {
            int i, j;
            debounce_buf[debounce_buf_i] = keyed;
            debounce_buf_i++;

            /* If the debounce buffer is full, determine the state of the key */
            if (debounce_buf_i >= ks.debounce)
            {
                debounce_buf_i = 0;

                j = 0;
                for (i = 0; i < ks.debounce; i++)
                    if (debounce_buf[i])
                        j++;

                keystate = (j > ks.debounce / 2) ? 1 : 0;
            }

            if (keystate == 1)
                return true;
            else
                return false;
        }

        bool read_iambic_key(ref Keyer_state ks, bool dot, bool dash, ref KeyerLogic kl, float ticklen)
        {
            int i, j;
            int dah = 0, dit = 0;

            dah_debounce_buf[debounce_buf_i] = dash;
            dit_debounce_buf[debounce_buf_i] = dot;
            debounce_buf_i++;

            //***************************************************
            // back to business as usual
            //***************************************************

            /* If the debounce buffer is full, determine the state of the keys */
            if (debounce_buf_i >= ks.debounce)
            {
                debounce_buf_i = 0;

                j = 0;

                for (i = 0; i < ks.debounce; i++)
                {
                    if (dah_debounce_buf[i])
                        j++;
                }
                dah = (j > ks.debounce / 2) ? 1 : 0;

                j = 0;
                for (i = 0; i < ks.debounce; i++)
                {
                    if (dit_debounce_buf[i])
                        j++;
                }
                dit = (j > ks.debounce / 2) ? 1 : 0;
            }

            return klogic(ref kl,
                dit,
                dah,
                ks.wpm,
                ks.mode,
                ks.flag.mdlmdB,
                ks.flag.memory.dit,
                ks.flag.memory.dah,
                ks.flag.autospace.khar,
                ks.flag.autospace.word, ks.weight, ticklen);
        }

        bool klogic(ref KeyerLogic kl,
    int dit,
    int dah,
    float wpm,
    int iambicmode,
    bool midelementmodeB,
    bool ditmemory,
    bool dahmemory,
    bool autocharspacing,
    bool autowordspacing, int weight, float ticklen)
        {
            float ditlen = 1200 / wpm;
            int set_element_timeouts = NO_TIMEOUTS_SCHED;

            /* Do we need to initialize the keyer? */
            if (!kl.flag.init)
            {
                kl.flag.prev.dit = dit;
                kl.flag.prev.dah = dah;
                kl.element.last = kl.element.curr = NO_ELEMENT;
                kl.element.iamb = NO_PADDLE_SQUEEZE;
                kl.element.psqam = false;
                kl.element.invtd = false;
                kl.timeout.midl = kl.timeout.beep = kl.timeout.elem = 0;
                kl.timeout.dlay = 0;
                kl.dlay_type = NO_DELAY;
                kl.flag.init = true;
            }

            /* Decrement the timeouts */
            kl.timeout.dlay -= kl.timeout.dlay > 0 ? ticklen : 0;
            if (kl.timeout.dlay <= 0)
            {
                /* If nothing is scheduled to play, and we just did a character
                spacing delay, and we do auto word spacing, wait for a word
                spacing delay, otherwise resume the normal element timeout
                countdowns */
                if (kl.timeout.elem <= 0 &&
                    kl.dlay_type == CHAR_SPACING_DELAY && autowordspacing)
                {
                    kl.timeout.dlay = ditlen * 4;
                    kl.dlay_type = WORD_SPACING_DELAY;
                }
                else
                {
                    kl.dlay_type = NO_DELAY;
                    kl.timeout.midl -= kl.timeout.midl > 0 ? ticklen : 0;
                    kl.timeout.beep -= kl.timeout.beep > 0 ? ticklen : 0;
                    kl.timeout.elem -= kl.timeout.elem > 0 ? ticklen : 0;
                }
            }

            /* Are both paddles squeezed? */
            if (dit == 1 && dah == 1)
            {
                kl.element.iamb = PADDLES_SQUEEZED;

                /* Are the paddles squeezed past the middle of the element? */
                if (kl.timeout.midl <= 0)
                    kl.element.psqam = true;
            }
            else
                /* Are both paddles released and we had gotten a squeeze in this element? */
                if (dit != 1 && dah != 1 && kl.element.iamb == PADDLES_SQUEEZED)
                    kl.element.iamb = PADDLES_RELEASED;

            /* Is the current element finished? */
            if (kl.timeout.elem <= 0 && kl.element.curr != NO_ELEMENT)
            {
                kl.element.last = kl.element.curr;

                /* Should we insert an inverted element? */
                if (((dit == 1 && dah == 1) ||
                    (kl.element.invtd &&
                    kl.element.iamb != PADDLES_RELEASED) ||
                    (kl.element.iamb == PADDLES_RELEASED &&
                    iambicmode == MODE_B && (!midelementmodeB || kl.element.psqam))))
                {
                    if (kl.element.last == DAH)
                        set_element_timeouts = kl.element.curr = DIT;
                    else
                        set_element_timeouts = kl.element.curr = DAH;
                }
                else
                {
                    /* No more element */
                    kl.element.curr = NO_ELEMENT;

                    /* Do we do automatic character spacing? */
                    if (autocharspacing && dit != 1 && dah != 1)
                    {
                        kl.timeout.dlay = ditlen * 2;
                        kl.dlay_type = CHAR_SPACING_DELAY;
                    }
                }

                kl.element.invtd = false;
                kl.element.iamb = NO_PADDLE_SQUEEZE;
                kl.element.psqam = false;
            }

            /* Is an element currently being played? */
            if (kl.element.curr == NO_ELEMENT)
            {
                if (dah == 1)			/* Dah paddle down ? */
                    set_element_timeouts = kl.element.curr = DAH;
                else if (dit == 1)		/* Dit paddle down ? */
                    set_element_timeouts = kl.element.curr = DIT;
            }

            /* Do the dah memory */
            if (kl.element.curr == DIT && kl.flag.prev.dah != 1 && dah == 1 && dahmemory)
                kl.element.invtd = true;

            /* Do the dit memory */
            if (kl.element.curr == DAH && kl.flag.prev.dit != 1 && dit == 1 && ditmemory)
                kl.element.invtd = true;

            /* If we had a dit (or dah) scheduled to be played after a delay,
            and the operator lifted both paddles before the end of the delay,
            and we have no dit (or dah) memory, forget it */

            if (kl.timeout.dlay > 0 &&
                dit != 1 &&
                dah != 1 &&
                ((kl.element.curr == DIT &&
                !ditmemory) || (kl.element.curr == DAH && !dahmemory)))
                set_element_timeouts = kl.element.curr = NO_ELEMENT;

            /* Do we need to set the playing timeouts of an element? */
            switch (set_element_timeouts)
            {
                case NO_ELEMENT:	/* Cancel any dit or dah */
                    kl.timeout.beep = 0;
                    kl.timeout.midl = 0;
                    kl.timeout.elem = 0;
                    break;

                case DIT:			/* Schedule a dit? */
                    kl.timeout.beep = (ditlen * (float)weight) / 50;
                    kl.timeout.midl = kl.timeout.beep / 2;
                    kl.timeout.elem = ditlen * 2;
                    break;

                case DAH:			/* Schedule a dah? */
                    kl.timeout.beep = (ditlen * (float)weight) / 50 + ditlen * 2;
                    kl.timeout.midl = kl.timeout.beep / 2;
                    kl.timeout.elem = ditlen * 4;
                    break;
            }

            kl.flag.prev.dit = dit;
            kl.flag.prev.dah = dah;

            bool result = kl.timeout.beep > 0 && kl.timeout.dlay <= 0;

            return result;
        }

        public void SetKeyerMode(int newmode)
        {
            EnterCriticalSection(cs_audio);
            switch (newmode)
            {
                case 0:
                    ks.mode = MODE_A;
                    ks.flag.mdlmdB = false;
                    break;
                case 1:
                    ks.mode = MODE_B;
                    ks.flag.mdlmdB = false;
                    break;
                default:
                    iambic = false;
                    break;
            }
            LeaveCriticalSection(cs_audio);
        }

        public void SetKeyerSpeed(float speed)
        {
            EnterCriticalSection(cs_audio);
            wpm = (int)speed;
            ks.wpm = speed;
            LeaveCriticalSection(cs_audio);
        }

        public void SetKeyerDebounce(int debounce)
        {
            EnterCriticalSection(cs_audio);
            ks.debounce = debounce;
            LeaveCriticalSection(cs_audio);
        }

        public void SetKeyerWeight(int newweight)
        {
            EnterCriticalSection(cs_audio);
            ks.weight = newweight;
            LeaveCriticalSection(cs_audio);
        }

        public void SetKeyerIambic(bool setit)
        {
            EnterCriticalSection(cs_audio);

            if (setit)
            {
                iambic = true;
                ks.flag.mdlmdB = true;
                ks.flag.memory.dah = true;
                ks.flag.memory.dit = true;
            }
            else
            {
                iambic = false;
                ks.flag.mdlmdB = false;
                ks.flag.memory.dah = false;
                ks.flag.memory.dit = false;
            }
            LeaveCriticalSection(cs_audio);
        }

        #endregion
    }
}