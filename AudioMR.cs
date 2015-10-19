//=================================================================
// AudioMR.cs
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
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

namespace CWExpert
{
    unsafe class Audio
    {
        #region Dll definition

        [DllImport("Receiver.dll", EntryPoint = "process_samples_thread")]
        public static extern void ProcessSamplesThread(uint thread);

        [DllImport("Receiver.dll", EntryPoint = "SetSampleRate")]
        public static extern int SetSampleRate(double sampleRate);

        [DllImport("Receiver.dll", EntryPoint = "Setup_SDR")]
        public static extern void SetupSDR(string data_path);

        [DllImport("Receiver.dll", EntryPoint = "Release_Update")]
        unsafe public static extern void ReleaseUpdate();

        [DllImport("Receiver.dll", EntryPoint = "Audio_Callback")]
        public static extern void ExchangeSamples(int thread, void* input_l, void* input_r,
            void* output_l, void* output_r, int numsamples);

        [DllImport("Receiver.dll", EntryPoint = "Audio_Callback_SubRX")]
        public static extern void ExchangeSamplesSubRX(int thread, int rx, void* output_l, void* output_r, int numsamples);

        [DllImport("Receiver.dll", EntryPoint = "SetDSPBuflen")]
        public static extern void ResizeSDR(uint thread, int DSPsize);

        [DllImport("Receiver.dll", EntryPoint = "Audio_Input_Callback")]
        public static extern void ExchangeInputSamples(int thread, void* input_l, void* input_r, int numsamples);

        [DllImport("Receiver.dll", EntryPoint = "Audio_Output_Callback")]
        public static extern void ExchangeOutputSamples(int thread, void* output_l, void* output_r, int numsamples);

        [DllImport("Receiver.dll", EntryPoint = "Audio_Output_Callback_SubRX")]
        public static extern void ExchangeOutputSamplesSubRX(int thread, int rx, void* output_l, void* output_r, int numsamples);

        [DllImport("Receiver.dll", EntryPoint = "SetAudioSize")]
        public static extern void SetAudioSize(int size);

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

        unsafe private static PA19.PaStreamCallback callback = new PA19.PaStreamCallback(Callback);
        unsafe private static PA19.PaStreamCallback monitor_callback = new PA19.PaStreamCallback(MonitorCallback);
        unsafe private static PA19.PaStreamCallback input_callback = new PA19.PaStreamCallback(InputCallback);
        unsafe private static PA19.PaStreamCallback outpu_callback = new PA19.PaStreamCallback(OutputCallback);
        unsafe private static void* stream1;
        unsafe private static void* stream2;
        unsafe private static void* stream3;
        public static int callback_return = 0;
        public static CWExpert MainForm = null;
        public static bool SDRmode = true;
        private static ComplexF[] buffer_mox = new ComplexF[2048];
        private static ComplexF[] monitor_buffer = new ComplexF[2048];
        private static float[] callback_monitor_buffer = new float[2048];
        public static Mutex audio_mutex = new Mutex(false);
        public static int channel = 5;
        public static int mox_switch_time = 0;
        private static ComplexF[] zero_buffer = new ComplexF[2048];
        private static float[] zero_bufferF = new float[2048];
        private static float[] mox_bufferF_l = new float[2048];
        private static float[] mox_bufferF_r = new float[2048];
        private static float[] tmp_buffer = new float[2048];
        private static float[] CH1_buffer = new float[2048];
        private static float[] CH2_buffer = new float[2048];
        private static float[] CH3_buffer = new float[2048];
        private static float[] CH4_buffer = new float[2048];
        public static bool RXswap = false;
        public static bool TXswap = false;
        public static bool QSK = false;
        private static RingBufferFloat rb_monIN_l;
        private static RingBufferFloat rb_monIN_r;
        private static RingBufferFloat rb_monOUT_l;
        private static RingBufferFloat rb_monOUT_r;
        unsafe private static void* cs_mon;
        private static bool monitor_enabled = false;
        private static int buffer_ptr_A = 0;
        private static int buffer_ptr_B = 0;
        public static bool audio_paused = false;
        public static bool RX2 = false;
        private static float[] tmp_buffer_ch1 = new float[2048];
        private static float[] tmp_buffer_ch2 = new float[2048];
        private static float[] tmp_buffer_ch3 = new float[2048];
        private static float[] tmp_buffer_ch4 = new float[2048];
        public static RingBufferFloat rb_mon_l; // = new RingBufferFloat(16 * 1048576);
        public static RingBufferFloat rb_mon_r; // = new RingBufferFloat(16 * 1048576);
        public static bool monitor_paused = false;
        private static int decimation = 6;
        private static int wptr = 0;
        static float[] MRbuffer = new float[2048];
        static float phaseacc = 0.0f;
        const float TWOPI = 6.28318530717856f;
        const float M_PI = 3.14159265358928f;
        public static bool loopback = false;
        public static bool record = false;
        delegate void CrossThreadCommand(string command, int param);
        static int play_progress = 0;
        static int rec_progress = 0;
        public static bool iq_balancer_reset = false;
        static ComplexF[] iq_buffer = new ComplexF[2048];
        static Mutex audio_run = new Mutex();
        static bool audio_running = false;
        public static bool iq_balanced = false;
        static int iq_progress = 0;

        #endregion

        #region properties

        private static bool mox = false;
        public static bool MOX
        {
            get { return mox; }
            set { mox = value; }
        }

        private static double vfoa_freq = 0.0;
        public static double VFOAfreq
        {
            get { return vfoa_freq; }
            set { vfoa_freq = value; }
        }

        private static double pwr = 0.5;
        public static double PWR
        {
            get { return pwr; }
            set { pwr = value; }
        }

        private static double scope_time = 50.0;
        public static double ScopeTime
        {
            set { scope_time = value; }
        }

        private static float scope_level = 0.0f;
        public static float ScopeLevel
        {
            set { scope_level = value / 100; }
        }

        private static double input_level = 0;
        public static double InputLevel
        {
            get { return input_level; }
            set { input_level = value / 10; }
        }

        private static double volume = 0;
        public static double Volume
        {
            get { return volume; }
            set { volume = value / 100; }
        }

        private static int host = 0;
        public static int Host
        {
            set { host = value; }
        }

        private static int block_size = 2048;
        public static int BlockSize
        {
            get { return block_size; }
            set { block_size = value; }
        }

        private static double audio_volts1 = 2.23;
        public static double AudioVolts1
        {
            get { return audio_volts1; }
            set { audio_volts1 = value; }
        }

        private static int sample_rate = 48000;
        public static int SampleRate
        {
            get { return sample_rate; }
            set { sample_rate = value; }
        }

        private static int input_dev = 0;
        public static int Input
        {
            set { input_dev = value; }
        }

        private static int output_dev = 0;
        public static int Output
        {
            set { output_dev = value; }
        }

        private static int monitor_dev = 0;
        public static int Monitor
        {
            set { monitor_dev = value; }
        }

        private static int monitor_host = 0;
        public static int MonitorHost
        {
            set { monitor_host = value; }
        }

        private static int latency = 50;
        public static int Latency
        {
            get { return latency; }
            set { latency = value; }
        }

        private static int num_channels = 2;
        public static int NumChannels
        {
            set { num_channels = value; }
        }

        #endregion

        #region misc function

        unsafe public static int Callback(void* input, void* output, int buflen,
            PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            try
            {
                if (audio_paused || !audio_running)
                    return callback_return;

                audio_run.WaitOne(100);

                if (SDRmode)
                {
#if(WIN64)
                Int64* array_ptr = (Int64*)input;
                float* in_l_ptr1 = (float*)array_ptr[0];
                float* in_r_ptr1 = (float*)array_ptr[1];
                double* VAC_in = (double*)input;
                array_ptr = (Int64*)output;
                float* out_l_ptr1 = (float*)array_ptr[1];
                float* out_r_ptr1 = (float*)array_ptr[0];
                array_ptr = (Int64*)input;
                in_l_ptr1 = (float*)array_ptr[0];
                in_r_ptr1 = (float*)array_ptr[1];
#endif

#if(WIN32)
                    int* array_ptr = (int*)input;
                    float* in_r_ptr1 = (float*)array_ptr[0];
                    float* in_l_ptr1 = (float*)array_ptr[1];
                    array_ptr = (int*)output;
                    float* out_l_ptr1 = (float*)array_ptr[1];
                    float* out_r_ptr1 = (float*)array_ptr[0];
                    array_ptr = (int*)input;
                    in_l_ptr1 = (float*)array_ptr[0];
                    in_r_ptr1 = (float*)array_ptr[1];
#endif
                    #region MOX

                    if (mox)
                    {
                        if (mox_switch_time >= 5)
                        {
                            Mode new_mode = MainForm.OpModeVFOA;

                            if (MainForm.TXSplit)
                                new_mode = MainForm.OpModeVFOB;

                            switch (new_mode)
                            {
                                case Mode.CW:
                                    {
                                        MainForm.cwEncoder.Exchange_samples(ref buffer_mox, ref monitor_buffer, buflen);

                                        if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                            (rb_monOUT_r.WriteSpace() >= buflen))
                                        {
                                            fixed (float* rptr = &mox_bufferF_l[0])
                                            fixed (float* lptr = &mox_bufferF_r[0])
                                            {
                                                for (int i = 0; i < buflen; i++)
                                                {
                                                    mox_bufferF_l[i] = monitor_buffer[i].Re;
                                                    mox_bufferF_r[i] = monitor_buffer[i].Im;
                                                    CH1_buffer[i] = monitor_buffer[i].Re;
                                                }

                                                EnterCriticalSection(cs_mon);
                                                rb_monOUT_l.WritePtr(lptr, buflen);
                                                rb_monOUT_r.WritePtr(rptr, buflen);
                                                LeaveCriticalSection(cs_mon);
                                            }
                                        }

                                        if (buffer_ptr_A >= 2048)
                                            buffer_ptr_A = 0;

                                        if (!TXswap)
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }

                                        if (buffer_ptr_A == 2048)
                                        {
                                            //Array.Copy(CH1_buffer, MainForm.cwDecoder.fft_buff_ch5, 2048);
                                            //MainForm.cwDecoder.AudioEvent1.Set();
                                            buffer_ptr_A = 0;
                                        }
                                    }
                                    break;

                                case Mode.RTTY:
                                    {
                                        MainForm.rtty.Exchange_samples(0, ref buffer_mox, ref monitor_buffer, buflen);

                                        if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                            (rb_monOUT_r.WriteSpace() >= buflen))
                                        {
                                            fixed (float* rptr = &mox_bufferF_l[0])
                                            fixed (float* lptr = &mox_bufferF_r[0])
                                            {
                                                for (int i = 0; i < buflen; i++)
                                                {
                                                    mox_bufferF_l[i] = monitor_buffer[i].Re;
                                                    mox_bufferF_r[i] = monitor_buffer[i].Im;
                                                }

                                                EnterCriticalSection(cs_mon);
                                                rb_monOUT_l.WritePtr(lptr, buflen);
                                                rb_monOUT_r.WritePtr(rptr, buflen);
                                                LeaveCriticalSection(cs_mon);
                                            }
                                        }

                                        if (buffer_ptr_A >= 2048)
                                            buffer_ptr_A = 0;

                                        if (!TXswap)
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_l_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                out_r_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }

                                        if (buffer_ptr_A == 2048)
                                            buffer_ptr_A = 0;
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
                                        MainForm.psk.Exchange_samples(0, ref buffer_mox, ref monitor_buffer, buflen);

                                        if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                            (rb_monOUT_r.WriteSpace() >= buflen))
                                        {
                                            fixed (float* rptr = &mox_bufferF_l[0])
                                            fixed (float* lptr = &mox_bufferF_r[0])
                                            {
                                                for (int i = 0; i < buflen; i++)
                                                {
                                                    mox_bufferF_l[i] = monitor_buffer[i].Re;
                                                    mox_bufferF_r[i] = monitor_buffer[i].Im;
                                                }

                                                EnterCriticalSection(cs_mon);
                                                rb_monOUT_l.WritePtr(lptr, buflen);
                                                rb_monOUT_r.WritePtr(rptr, buflen);
                                                LeaveCriticalSection(cs_mon);
                                            }
                                        }

                                        if (buffer_ptr_A >= 2048)
                                            buffer_ptr_A = 0;

                                        if (!TXswap)
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }

                                        if (buffer_ptr_A == 2048)
                                            buffer_ptr_A = 0;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            mox_switch_time++;

                            switch (MainForm.OpModeVFOA)
                            {
                                case Mode.CW:
                                    {

                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = 0.0f;
                                            out_l_ptr1[0] = 0.0f;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }

                                        //Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch5, 2048);
                                        //Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch6, 2048);
                                        //MainForm.cwDecoder.AudioEvent1.Set();
                                    }
                                    break;

                                case Mode.RTTY:
                                    {
                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = 0.0f;
                                            out_l_ptr1[0] = 0.0f;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }

                                        Array.Copy(zero_bufferF, MainForm.rtty.ch1_buffer, 2048);
                                        Array.Copy(zero_bufferF, MainForm.rtty.ch2_buffer, 2048);
                                        MainForm.rtty.AudioEventRX1.Set();
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
                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = 0.0f;
                                            out_l_ptr1[0] = 0.0f;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }

                                        Array.Copy(zero_bufferF, MainForm.psk.ch1_buffer, 2048);
                                        Array.Copy(zero_bufferF, MainForm.psk.ch2_buffer, 2048);
                                        MainForm.psk.AudioEvent1.Set();
                                    }
                                    break;
                            }

                            if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                (rb_monOUT_r.WriteSpace() >= buflen))
                            {
                                fixed (float* rptr = &mox_bufferF_l[0])
                                fixed (float* lptr = &mox_bufferF_r[0])
                                {
                                    for (int i = 0; i < buflen; i++)
                                    {
                                        mox_bufferF_l[i] = 0.0f;
                                        mox_bufferF_r[i] = 0.0f;
                                    }

                                    EnterCriticalSection(cs_mon);
                                    rb_monOUT_l.WritePtr(lptr, buflen);
                                    rb_monOUT_r.WritePtr(rptr, buflen);
                                    LeaveCriticalSection(cs_mon);
                                }
                            }
                        }
                    }

                    #endregion

                    #region RX

                    else
                    {
                        float* in_l = null, in_r = null, out_l = null, out_r = null;
                        out_l = out_l_ptr1;
                        out_r = out_r_ptr1;

                        if (!RXswap)
                        {
                            in_l = in_l_ptr1;
                            in_r = in_r_ptr1;
                        }
                        else
                        {
                            in_l = in_r_ptr1;
                            in_r = in_l_ptr1;
                        }

                        if (record)
                        {
                            if (rb_mon_l.WriteSpace() < 2 * buflen)
                            {
                                MainForm.recorder.Recording = false;
                                record = false;
                            }

                            rb_mon_l.WritePtr(in_l, buflen, true);
                            rb_mon_r.WritePtr(in_r, buflen, true);
                            rec_progress++;

                            if (rec_progress >= 50)
                            {
                                MainForm.recorder.Invoke(new CrossThreadCommand(MainForm.recorder.CommandCallback),
                                    "Set Recording progress", rb_mon_l.wptr);
                                rec_progress = 0;
                            }
                        }

                        if (loopback)
                        {
                            if (rb_mon_l.ReadSpace() < buflen)
                            {
                                rb_mon_l.ResetReadPtr();
                                rb_mon_r.ResetReadPtr();
                            }

                            if (!RXswap)
                            {
                                rb_mon_l.ReadPtr(in_l, buflen);
                                rb_mon_r.ReadPtr(in_r, buflen);
                            }
                            else
                            {
                                rb_mon_r.ReadPtr(in_l, buflen);
                                rb_mon_l.ReadPtr(in_r, buflen);
                            }

                            play_progress++;

                            if (play_progress >= 30)
                            {
                                MainForm.recorder.Invoke(new CrossThreadCommand(MainForm.recorder.CommandCallback),
                                    "Set Play progress", rb_mon_l.rptr);
                                play_progress = 0;
                            }
                        }

                        switch (MainForm.IQcorrection)
                        {
                            case IQ_correction.WBIR:
                            case IQ_correction.FIXED:
                                {
                                    if (iq_progress >= 50)
                                    {
                                        for (int i = 0; i < 2048; i++)
                                        {
                                            iq_buffer[i].Re = in_l[i];
                                            iq_buffer[i].Im = in_r[i];
                                        }

                                        for (int i = 0; i < 2048; i++)
                                        {
                                            in_l[i] = iq_buffer[i].Re;
                                            in_r[i] = iq_buffer[i].Im;
                                        }
                                    }
                                    else
                                        iq_progress++;
                                }
                                break;
                        }

                        fixed (float* output_l = &tmp_buffer_ch1[0])
                        fixed (float* output_r = &tmp_buffer_ch2[0])
                            ExchangeSamples(0, in_l, in_r, output_l, output_r, buflen);

                        if (RX2)
                        {
                            fixed (float* output_l = &tmp_buffer_ch3[0])
                            fixed (float* output_r = &tmp_buffer_ch4[0])
                            {
                                ExchangeSamplesSubRX(0, 0, output_l, output_r, buflen);
                            }
                        }

                        if (buffer_ptr_A >= 2048 || buffer_ptr_B >= 2048)
                        {
                            buffer_ptr_A = 0;
                            buffer_ptr_B = 0;
                        }

                        for (int i = 0; i < buflen; i++)
                        {
                            CH1_buffer[buffer_ptr_A] = tmp_buffer_ch1[i];
                            CH2_buffer[buffer_ptr_A] = tmp_buffer_ch2[i];
                            CH3_buffer[buffer_ptr_B] = tmp_buffer_ch3[i];
                            CH4_buffer[buffer_ptr_B] = tmp_buffer_ch4[i];
                            buffer_ptr_A++;
                            buffer_ptr_B++;
                        }

                        if (!monitor_paused)
                        {
                            switch (MainForm.OpModeVFOA)
                            {
                                case Mode.CW:
                                    {
                                        if (mox_switch_time >= 5)
                                        {
                                            if (buffer_ptr_A >= 2048)
                                            {
                                                Array.Copy(CH1_buffer, MainForm.cwDecoder.fft_buff_ch5, 2048);
                                                MainForm.cwDecoder.AudioEvent1.Set();
                                                buffer_ptr_A = 0;
                                            }

                                        }
                                        else
                                        {
                                            mox_switch_time++;

                                            if (buffer_ptr_A >= 2048)
                                            {
                                                Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch5, 2048);       // mute
                                                MainForm.cwDecoder.AudioEvent1.Set();
#if(WIN32)
                                                array_ptr = (int*)output;
#endif

#if(WIN64)
                                                array_ptr = (Int64*)output;
#endif
                                                out_l_ptr1 = (float*)array_ptr[1];
                                                out_r_ptr1 = (float*)array_ptr[0];
                                                buffer_ptr_A = 0;
                                            }
                                        }
                                    }
                                    break;

                                case Mode.RTTY:
                                    {
                                        if (MainForm.rtty.run_thread)
                                        {
                                            if (mox_switch_time >= 5)
                                            {
                                                if (buffer_ptr_A >= 2048)
                                                {
                                                    Array.Copy(CH1_buffer, MainForm.rtty.ch1_buffer, 2048);
                                                    Array.Copy(CH2_buffer, MainForm.rtty.ch2_buffer, 2048);
                                                    MainForm.rtty.AudioEventRX1.Set();
                                                    buffer_ptr_A = 0;
                                                }
                                            }
                                            else
                                            {
                                                mox_switch_time++;

                                                if (buffer_ptr_A >= 2048)
                                                {
                                                    Array.Copy(zero_bufferF, MainForm.rtty.ch1_buffer, 2048);
                                                    Array.Copy(zero_bufferF, MainForm.rtty.ch2_buffer, 2048);
                                                    MainForm.rtty.AudioEventRX1.Set();

#if(WIN32)
                                                    array_ptr = (int*)output;
#endif

#if(WIN64)
                                                    array_ptr = (Int64*)output;
#endif

                                                    out_l_ptr1 = (float*)array_ptr[1];
                                                    out_r_ptr1 = (float*)array_ptr[0];
                                                    buffer_ptr_A = 0;
                                                }
                                            }
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
                                        if (MainForm.psk.run_thread)
                                        {
                                            if (mox_switch_time >= 5)
                                            {
                                                if (buffer_ptr_A >= 2048)
                                                {
                                                    Array.Copy(CH1_buffer, MainForm.psk.ch1_buffer, 2048);
                                                    MainForm.psk.AudioEvent1.Set();
                                                    buffer_ptr_A = 0;
                                                }
                                            }
                                            else
                                            {
                                                mox_switch_time++;

                                                if (buffer_ptr_A >= 2048)
                                                {
                                                    Array.Copy(zero_bufferF, MainForm.psk.ch1_buffer, 2048);    // mute
                                                    MainForm.psk.AudioEvent1.Set();

#if(WIN32)
                                                    array_ptr = (int*)output;
#endif

#if(WIN64)
                                                    array_ptr = (Int64*)output;
#endif

                                                    out_l_ptr1 = (float*)array_ptr[1];
                                                    out_r_ptr1 = (float*)array_ptr[0];
                                                    buffer_ptr_A = 0;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }

                            if (RX2)
                            {
                                switch (MainForm.OpModeVFOB)
                                {
                                    case Mode.CW:
                                        {
                                            if (mox_switch_time >= 5)
                                            {
                                                if (buffer_ptr_B >= 2048)
                                                {
                                                    switch (MainForm.OpModeVFOA)
                                                    {
                                                        case Mode.CW:
                                                        case Mode.BPSK31:
                                                        case Mode.BPSK63:
                                                        case Mode.BPSK125:
                                                        case Mode.BPSK250:
                                                        case Mode.QPSK31:
                                                        case Mode.QPSK63:
                                                        case Mode.QPSK125:
                                                        case Mode.QPSK250:
                                                            Array.Copy(CH2_buffer, MainForm.cwDecoder.fft_buff_ch6, 2048);
                                                            break;

                                                        case Mode.RTTY:
                                                            Array.Copy(CH3_buffer, MainForm.cwDecoder.fft_buff_ch6, 2048);
                                                            break;
                                                    }

                                                    MainForm.cwDecoder.AudioEvent2.Set();
                                                    buffer_ptr_B = 0;
                                                }

                                            }
                                            else
                                            {
                                                mox_switch_time++;

                                                if (buffer_ptr_B >= 2048)
                                                {
                                                    Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch6, 2048);    // mute
                                                    MainForm.cwDecoder.AudioEvent2.Set();

#if(WIN32)
                                                    array_ptr = (int*)output;
#endif

#if(WIN64)
                                                    array_ptr = (Int64*)output;
#endif

                                                    out_l_ptr1 = (float*)array_ptr[1];
                                                    out_r_ptr1 = (float*)array_ptr[0];
                                                    buffer_ptr_B = 0;
                                                }
                                            }
                                        }
                                        break;

                                    case Mode.RTTY:
                                        {
                                            if (MainForm.rtty.run_thread)
                                            {
                                                if (mox_switch_time >= 5)
                                                {
                                                    if (buffer_ptr_B >= 2048)
                                                    {

                                                        Array.Copy(CH3_buffer, MainForm.rtty.ch3_buffer, 2048);
                                                        Array.Copy(CH4_buffer, MainForm.rtty.ch4_buffer, 2048);
                                                        MainForm.rtty.AudioEventRX2.Set();
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    mox_switch_time++;

                                                    if (buffer_ptr_B >= 2048)
                                                    {
                                                        Array.Copy(zero_bufferF, MainForm.rtty.ch3_buffer, 2048);
                                                        Array.Copy(zero_bufferF, MainForm.rtty.ch4_buffer, 2048);
                                                        MainForm.rtty.AudioEventRX2.Set();

#if(WIN32)
                                                        array_ptr = (int*)output;
#endif

#if(WIN64)
                                                        array_ptr = (Int64*)output;
#endif

                                                        out_l_ptr1 = (float*)array_ptr[1];
                                                        out_r_ptr1 = (float*)array_ptr[0];
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
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
                                            if (MainForm.psk.run_thread)
                                            {
                                                if (mox_switch_time >= 5)
                                                {
                                                    if (buffer_ptr_B >= 2048)
                                                    {
                                                        switch (MainForm.OpModeVFOA)
                                                        {
                                                            case Mode.CW:
                                                            case Mode.BPSK31:
                                                            case Mode.BPSK63:
                                                            case Mode.BPSK125:
                                                            case Mode.BPSK250:
                                                            case Mode.QPSK31:
                                                            case Mode.QPSK63:
                                                            case Mode.QPSK125:
                                                            case Mode.QPSK250:
                                                                Array.Copy(CH2_buffer, MainForm.psk.ch2_buffer, 2048);
                                                                break;

                                                            case Mode.RTTY:
                                                                Array.Copy(CH3_buffer, MainForm.psk.ch2_buffer, 2048);
                                                                break;
                                                        }

                                                        MainForm.psk.AudioEvent2.Set();
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    mox_switch_time++;

                                                    if (buffer_ptr_B >= 2048)
                                                    {
                                                        Array.Copy(zero_bufferF, MainForm.psk.ch2_buffer, 2048);    // mute
                                                        MainForm.psk.AudioEvent2.Set();

#if(WIN32)
                                                        array_ptr = (int*)output;
#endif

#if(WIN64)
                                                        array_ptr = (Int64*)output;
#endif

                                                        out_l_ptr1 = (float*)array_ptr[1];
                                                        out_r_ptr1 = (float*)array_ptr[0];
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        if (channel == 5)
                        {
                            switch (MainForm.OpModeVFOA)
                            {
                                case Mode.RTTY:
                                    {
                                        if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                            (rb_monOUT_r.WriteSpace() >= buflen))
                                        {
                                            fixed (float* output_l = &tmp_buffer_ch1[0])
                                            fixed (float* output_r = &tmp_buffer_ch2[0])
                                            {
                                                EnterCriticalSection(cs_mon);
                                                rb_monOUT_l.WritePtr(output_l, buflen);
                                                rb_monOUT_r.WritePtr(output_r, buflen);
                                                LeaveCriticalSection(cs_mon);
                                            }
                                        }

                                        if (mox_switch_time >= 5)
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_l_ptr1[0] = tmp_buffer_ch1[i] * (float)volume;
                                                out_r_ptr1[0] = tmp_buffer_ch2[i] * (float)volume;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                            }
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        if (monitor_enabled)
                                        {
                                            if (rb_monOUT_l.WriteSpace() >= buflen &&
                                            rb_monOUT_r.WriteSpace() >= buflen)
                                            {
                                                fixed (float* output_l = &tmp_buffer_ch1[0])
                                                {
                                                    EnterCriticalSection(cs_mon);
                                                    rb_monOUT_l.WritePtr(output_l, buflen);
                                                    rb_monOUT_r.WritePtr(output_l, buflen);
                                                    LeaveCriticalSection(cs_mon);
                                                }
                                            }
                                            else
                                            {
                                                rb_monOUT_l.Reset();
                                                rb_monOUT_r.Reset();
                                            }
                                        }

                                        if (mox_switch_time >= 5)
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_l_ptr1[0] = tmp_buffer_ch1[i] * (float)volume;
                                                out_r_ptr1[0] = tmp_buffer_ch1[i] * (float)volume;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else if (channel == 6)
                        {
                            switch (MainForm.OpModeVFOA)
                            {
                                case Mode.RTTY:
                                    {
                                        switch (MainForm.OpModeVFOB)
                                        {
                                            case Mode.RTTY:
                                                {
                                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                                    {
                                                        fixed (float* output_l = &tmp_buffer_ch3[0])
                                                        fixed (float* output_r = &tmp_buffer_ch4[0])
                                                        {
                                                            EnterCriticalSection(cs_mon);
                                                            rb_monOUT_l.WritePtr(output_l, buflen);
                                                            rb_monOUT_r.WritePtr(output_r, buflen);
                                                            LeaveCriticalSection(cs_mon);
                                                        }
                                                    }

                                                    if (mox_switch_time >= 5)
                                                    {
                                                        for (int i = 0; i < buflen; i++)
                                                        {
                                                            out_l_ptr1[0] = tmp_buffer_ch3[i] * (float)volume;
                                                            out_r_ptr1[0] = tmp_buffer_ch4[i] * (float)volume;
                                                            out_l_ptr1++;
                                                            out_r_ptr1++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        for (int i = 0; i < buflen; i++)
                                                        {
                                                            out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                            out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                            out_l_ptr1++;
                                                            out_r_ptr1++;
                                                        }
                                                    }
                                                }
                                                break;

                                            default:
                                                {
                                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                                    {
                                                        fixed (float* output_l = &tmp_buffer_ch3[0])
                                                        {
                                                            EnterCriticalSection(cs_mon);
                                                            rb_monOUT_l.WritePtr(output_l, buflen);
                                                            rb_monOUT_r.WritePtr(output_l, buflen);
                                                            LeaveCriticalSection(cs_mon);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        rb_monOUT_l.Reset();
                                                        rb_monOUT_r.Reset();
                                                    }

                                                    if (mox_switch_time >= 5)
                                                    {
                                                        for (int i = 0; i < buflen; i++)
                                                        {
                                                            out_l_ptr1[0] = tmp_buffer_ch3[i] * (float)volume;
                                                            out_r_ptr1[0] = tmp_buffer_ch3[i] * (float)volume;
                                                            out_l_ptr1++;
                                                            out_r_ptr1++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        for (int i = 0; i < buflen; i++)
                                                        {
                                                            out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                            out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                            out_l_ptr1++;
                                                            out_r_ptr1++;
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    break;

                                default:
                                    {
                                        switch (MainForm.OpModeVFOA)
                                        {
                                            case Mode.RTTY:
                                                {
                                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                                    {
                                                        fixed (float* output_l = &tmp_buffer_ch3[0])
                                                        fixed (float* output_r = &tmp_buffer_ch4[0])
                                                        {
                                                            EnterCriticalSection(cs_mon);
                                                            rb_monOUT_l.WritePtr(output_l, buflen);
                                                            rb_monOUT_r.WritePtr(output_r, buflen);
                                                            LeaveCriticalSection(cs_mon);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        rb_monOUT_l.Reset();
                                                        rb_monOUT_r.Reset();
                                                    }

                                                    if (mox_switch_time >= 5)
                                                    {
                                                        for (int i = 0; i < buflen; i++)
                                                        {
                                                            out_l_ptr1[0] = tmp_buffer_ch3[i] * (float)volume;
                                                            out_r_ptr1[0] = tmp_buffer_ch4[i] * (float)volume;
                                                            out_l_ptr1++;
                                                            out_r_ptr1++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        for (int i = 0; i < buflen; i++)
                                                        {
                                                            out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                            out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                            out_l_ptr1++;
                                                            out_r_ptr1++;
                                                        }
                                                    }
                                                }
                                                break;

                                            default:
                                                {
                                                    switch (MainForm.OpModeVFOB)
                                                    {
                                                        case Mode.RTTY:
                                                            {
                                                                if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                                                    (rb_monOUT_r.WriteSpace() >= buflen))
                                                                {
                                                                    fixed (float* output_l = &tmp_buffer_ch3[0])
                                                                    fixed (float* output_r = &tmp_buffer_ch4[0])
                                                                    {
                                                                        EnterCriticalSection(cs_mon);
                                                                        rb_monOUT_l.WritePtr(output_l, buflen);
                                                                        rb_monOUT_r.WritePtr(output_r, buflen);
                                                                        LeaveCriticalSection(cs_mon);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    rb_monOUT_l.Reset();
                                                                    rb_monOUT_r.Reset();
                                                                }

                                                                if (mox_switch_time >= 5)
                                                                {
                                                                    for (int i = 0; i < buflen; i++)
                                                                    {
                                                                        out_l_ptr1[0] = tmp_buffer_ch3[i] * (float)volume;
                                                                        out_r_ptr1[0] = tmp_buffer_ch4[i] * (float)volume;
                                                                        out_l_ptr1++;
                                                                        out_r_ptr1++;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    for (int i = 0; i < buflen; i++)
                                                                    {
                                                                        out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                                        out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                                        out_l_ptr1++;
                                                                        out_r_ptr1++;
                                                                    }
                                                                }
                                                            }
                                                            break;

                                                        default:
                                                            {
                                                                if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                                                    (rb_monOUT_r.WriteSpace() >= buflen))
                                                                {
                                                                    fixed (float* output_l = &tmp_buffer_ch2[0])
                                                                    {
                                                                        EnterCriticalSection(cs_mon);
                                                                        rb_monOUT_l.WritePtr(output_l, buflen);
                                                                        rb_monOUT_r.WritePtr(output_l, buflen);
                                                                        LeaveCriticalSection(cs_mon);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    rb_monOUT_l.Reset();
                                                                    rb_monOUT_r.Reset();
                                                                }

                                                                if (mox_switch_time >= 5)
                                                                {
                                                                    for (int i = 0; i < buflen; i++)
                                                                    {
                                                                        out_l_ptr1[0] = tmp_buffer_ch2[i] * (float)volume;
                                                                        out_r_ptr1[0] = tmp_buffer_ch2[i] * (float)volume;
                                                                        out_l_ptr1++;
                                                                        out_r_ptr1++;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    for (int i = 0; i < buflen; i++)
                                                                    {
                                                                        out_l_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                                        out_r_ptr1[0] = zero_bufferF[i] * (float)volume;
                                                                        out_l_ptr1++;
                                                                        out_r_ptr1++;
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }

                    #endregion
                }

                #region Morse runner

                else
                {                                           // Morse runner
#if(WIN32)
                    int* array_ptr = (int*)input;
                    float* in_ptr_l = (float*)array_ptr[0];
                    float* in_ptr_r = (float*)array_ptr[1];

                    int* out_array_ptr = (int*)output;
                    float* out_l_ptr1 = (float*)out_array_ptr[0];
                    float* out_r_ptr1 = (float*)out_array_ptr[1];
#endif

#if(WIN64)
                    Int64* array_ptr = (Int64*)input;
                    float* in_ptr_l = (float*)array_ptr[0];
                    float* in_ptr_r = (float*)array_ptr[1];
                    ushort[] buffer_l = new ushort[buflen];
                    ushort[] buffer_r = new ushort[buflen];

                    Int64* out_array_ptr = (Int64*)output;
                    float* out_l_ptr1 = (float*)out_array_ptr[0];
                    float* out_r_ptr1 = (float*)out_array_ptr[1];
#endif
                    fixed (float* output_l = &tmp_buffer_ch1[0])
                    fixed (float* output_r = &tmp_buffer_ch2[0])
                        ExchangeSamples(0, in_ptr_l, in_ptr_r, output_l, output_r, buflen);

                    for (int i = 0; i < buflen; i++)
                    {
                        tmp_buffer[i] = in_ptr_l[i];
                        out_l_ptr1[i] = (float)(in_ptr_l[i] * volume);
                        out_r_ptr1[i] = (float)(in_ptr_r[i] * volume);
                        in_ptr_l[i] *= (float)input_level;
                        in_ptr_r[i] *= (float)input_level;
                    }

                    for (int i = 0; i < buflen; i++)
                    {
                        if (wptr >= 2048)
                        {
                            if (MainForm.cwDecoder.audio_buffer != null &&
                                MainForm.cwDecoder.audio_buffer.Length == 2048)
                            {
                                Array.Copy(MRbuffer, MainForm.cwDecoder.audio_buffer, 2048);
                                MainForm.cwDecoder.AudioEvent1.Set();

                            }

                            wptr = 0;
                        }
                        else
                        {
                            MRbuffer[wptr] = in_ptr_r[i];
                            i += decimation - 1;
                            wptr++;
                        }
                    }
                }

                #endregion

                #region Scope

                if ((MainForm.DisplayMode == DisplayMode.PANASCOPE || MainForm.DisplayMode == DisplayMode.PANASCOPE_INV ||
                    MainForm.MonitorMode == DisplayMode.SCOPE) && !pause_scope && buffer_ptr_A == 0)
                {
                    Mode new_mode = MainForm.OpModeVFOA;

                    if (channel == 6)
                        new_mode = MainForm.OpModeVFOB;

                    switch (new_mode)
                    {
                        case Mode.RTTY:
                            if (channel == 5)
                            {
                                DoScopeSpace(CH1_buffer, 2048);
                                DoScopeMark(CH2_buffer, 2048);
                            }
                            else if (channel == 6)
                            {
                                DoScopeSpace(CH3_buffer, 2048);
                                DoScopeMark(CH4_buffer, 2048);
                            }
                            break;

                        default:
                            {
                                if (channel == 5)
                                {
                                    DoScope(CH1_buffer, 2048);
                                }
                                else if (channel == 6)
                                {
                                    DoScope(CH2_buffer, 2048);
                                }
                            }
                            break;
                    }
                }

                #endregion

                audio_run.ReleaseMutex();

                return callback_return;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 0;
            }
        }

        unsafe public static int InputCallback(void* input, void* output, int buflen,
    PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            try
            {
                if (audio_paused)
                    return callback_return;
#if(WIN64)
                Int64* array_ptr = (Int64*)input;
                float* in_l_ptr1 = (float*)array_ptr[0];
                float* in_r_ptr1 = (float*)array_ptr[1];
                array_ptr = (Int64*)input;
                in_l_ptr1 = (float*)array_ptr[0];
                in_r_ptr1 = (float*)array_ptr[1];
#endif

#if(WIN32)
                int* array_ptr = (int*)input;
                float* in_r_ptr1 = (float*)array_ptr[0];
                float* in_l_ptr1 = (float*)array_ptr[1];
                array_ptr = (int*)input;
                in_l_ptr1 = (float*)array_ptr[0];
                in_r_ptr1 = (float*)array_ptr[1];
#endif

                float* in_l = null, in_r = null;

                if (!RXswap)
                {
                    in_l = in_l_ptr1;
                    in_r = in_r_ptr1;
                }
                else
                {
                    in_l = in_r_ptr1;
                    in_r = in_l_ptr1;
                }

                if (SDRmode)
                {
                    if (!mox)
                    {
                        if (mox_switch_time >= 2)
                            ExchangeInputSamples(0, in_l, in_r, buflen);
                        else
                        {
                            mox_switch_time++;

                            /*fixed (float* rptr = &tmp_bufferF_l[0])
                            fixed (float* lptr = &tmp_bufferF_r[0])
                            {
                                ExchangeInputSamples(0, lptr, rptr, buflen);
                            }*/
                        }
                    }
                    else
                    {
                        /*if (mox_switch_time >= 2)
                        {
                            fixed (float* rptr = &tmp_bufferF_l[0])
                            fixed (float* lptr = &tmp_bufferF_r[0])
                            {
                                ExchangeInputSamples(0, lptr, rptr, buflen);
                            }
                        }
                        else
                        {
                            mox_switch_time++;

                            fixed (float* rptr = &tmp_bufferF_l[0])
                            fixed (float* lptr = &tmp_bufferF_r[0])
                            {
                                for (int i = 0; i < buflen; i++)
                                {
                                    tmp_bufferF_l[i] = in_l[i];
                                    tmp_bufferF_r[i] = in_r[i];
                                }

                                ExchangeInputSamples(0, lptr, rptr, buflen);
                            }
                        }*/
                    }
                }

                return callback_return;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 0;
            }
        }

        unsafe public static int OutputCallback(void* input, void* output, int buflen,
            PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            try
            {
                if (audio_paused)
                    return callback_return;
#if(WIN64)
                Int64* array_ptr = (Int64*)output;
                float* out_l_ptr1 = (float*)array_ptr[1];
                float* out_r_ptr1 = (float*)array_ptr[0];
#endif

#if(WIN32)
                int* array_ptr = (int*)output;
                float* out_l_ptr1 = (float*)array_ptr[1];
                float* out_r_ptr1 = (float*)array_ptr[0];
#endif

                if (SDRmode)
                {
                    #region MOX

                    if (mox)
                    {
                        switch (MainForm.OpModeVFOA)
                        {
                            case Mode.CW:
                                {
                                    /*if (mox_switch_time < 1)
                                    {
                                        mox_switch_time++;

                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = zero_buffer[i].Re;
                                            out_l_ptr1[0] = zero_buffer[i].Im;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }

                                        Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch5, buflen);
                                        Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch6, buflen);
                                        MainForm.cwDecoder.AudioEvent.Set();

                                        if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                            (rb_monOUT_r.WriteSpace() >= buflen))
                                        {
                                            fixed (float* rptr = &mox_bufferF_l[0])
                                            fixed (float* lptr = &mox_bufferF_r[0])
                                            {
                                                for (int i = 0; i < buflen; i++)
                                                {
                                                    mox_bufferF_l[i] = 0.0f;
                                                    mox_bufferF_r[i] = 0.0f;
                                                }

                                                EnterCriticalSection(cs_mon);
                                                rb_monOUT_l.WritePtr(lptr, buflen);
                                                rb_monOUT_r.WritePtr(rptr, buflen);
                                                LeaveCriticalSection(cs_mon);
                                            }
                                        }
                                    }
                                    else*/
                                    {
                                        MainForm.cwEncoder.Exchange_samples(ref buffer_mox, ref monitor_buffer, buflen);

                                        if (!TXswap)
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                out_r_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                                out_l_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                                CH1_buffer[buffer_ptr_A] = monitor_buffer[i].Re;
                                                out_l_ptr1++;
                                                out_r_ptr1++;
                                                buffer_ptr_A++;
                                            }
                                        }

                                        if (buffer_ptr_A == 2048)
                                        {
                                            Array.Copy(CH1_buffer, MainForm.cwDecoder.fft_buff_ch5, 2048);
                                            MainForm.cwDecoder.AudioEvent1.Set();
                                            buffer_ptr_A = 0;
                                        }

                                        if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                            (rb_monOUT_r.WriteSpace() >= buflen))
                                        {
                                            fixed (float* rptr = &mox_bufferF_l[0])
                                            fixed (float* lptr = &mox_bufferF_r[0])
                                            {
                                                for (int i = 0; i < buflen; i++)
                                                {
                                                    mox_bufferF_l[i] = monitor_buffer[i].Re;
                                                    mox_bufferF_r[i] = monitor_buffer[i].Im;
                                                }

                                                EnterCriticalSection(cs_mon);
                                                rb_monOUT_l.WritePtr(lptr, buflen);
                                                rb_monOUT_r.WritePtr(rptr, buflen);
                                                LeaveCriticalSection(cs_mon);
                                            }
                                        }
                                    }
                                }
                                break;

                            case Mode.RTTY:
                                {
                                    MainForm.rtty.Exchange_samples(1, ref buffer_mox, ref monitor_buffer, buflen);

                                    if (!TXswap)
                                    {
                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                            out_l_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                            CH1_buffer[i] = monitor_buffer[i].Re;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_l_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                            out_r_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                            CH1_buffer[i] = monitor_buffer[i].Re;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }
                                    }

                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                    {
                                        fixed (float* rptr = &mox_bufferF_l[0])
                                        fixed (float* lptr = &mox_bufferF_r[0])
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                mox_bufferF_l[i] = monitor_buffer[i].Re;
                                                mox_bufferF_r[i] = monitor_buffer[i].Im;
                                            }

                                            EnterCriticalSection(cs_mon);
                                            rb_monOUT_l.WritePtr(lptr, buflen);
                                            rb_monOUT_r.WritePtr(rptr, buflen);
                                            LeaveCriticalSection(cs_mon);
                                        }
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
                                    MainForm.psk.Exchange_samples(0, ref buffer_mox, ref monitor_buffer, buflen);

                                    if (!TXswap)
                                    {
                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                            out_l_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                            CH1_buffer[i] = monitor_buffer[i].Re;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < buflen; i++)
                                        {
                                            out_r_ptr1[0] = buffer_mox[i].Re * (float)pwr;
                                            out_l_ptr1[0] = buffer_mox[i].Im * (float)pwr;
                                            CH1_buffer[i] = monitor_buffer[i].Re;
                                            out_l_ptr1++;
                                            out_r_ptr1++;
                                        }
                                    }

                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                    {
                                        fixed (float* rptr = &mox_bufferF_l[0])
                                        fixed (float* lptr = &mox_bufferF_r[0])
                                        {
                                            for (int i = 0; i < buflen; i++)
                                            {
                                                mox_bufferF_l[i] = monitor_buffer[i].Re;
                                                mox_bufferF_r[i] = monitor_buffer[i].Im;
                                            }

                                            EnterCriticalSection(cs_mon);
                                            rb_monOUT_l.WritePtr(lptr, buflen);
                                            rb_monOUT_r.WritePtr(rptr, buflen);
                                            LeaveCriticalSection(cs_mon);
                                        }
                                    }
                                }
                                break;
                        }
                    }

                    #endregion

                    #region RX

                    else
                    {
                        float* out_l = null, out_r = null;
                        out_l = out_l_ptr1;
                        out_r = out_r_ptr1;

                        if (mox_switch_time > 2)
                        {
                            fixed (float* output_l = &tmp_buffer_ch1[0])
                            fixed (float* output_r = &tmp_buffer_ch2[0])
                                ExchangeOutputSamples(0, output_l, output_r, buflen);

                                if (RX2)
                                {
                                    fixed (float* output_l = &tmp_buffer_ch3[0])
                                    fixed (float* output_r = &tmp_buffer_ch4[0])
                                    {
                                        ExchangeSamplesSubRX(0, 0, output_l, output_r, buflen);
                                    }
                                }

                                if (buffer_ptr_A >= 2048 || buffer_ptr_B >= 2048)
                                {
                                    buffer_ptr_A = 0;
                                    buffer_ptr_B = 0;
                                }

                                for (int i = 0; i < buflen; i++)
                                {
                                    CH1_buffer[buffer_ptr_A] = tmp_buffer_ch1[i];
                                    CH2_buffer[buffer_ptr_A] = tmp_buffer_ch2[i];
                                    CH3_buffer[buffer_ptr_B] = tmp_buffer_ch3[i];
                                    CH4_buffer[buffer_ptr_B] = tmp_buffer_ch4[i];
                                    buffer_ptr_A++;
                                    buffer_ptr_B++;
                                }

                                if (channel == 5)
                                {
                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                    {
                                        fixed (float* output_l = &tmp_buffer_ch1[0])
                                        fixed (float* output_r = &tmp_buffer_ch2[0])
                                        {
                                            EnterCriticalSection(cs_mon);
                                            rb_monOUT_l.WritePtr(output_l, buflen);
                                            rb_monOUT_r.WritePtr(output_l, buflen);
                                            LeaveCriticalSection(cs_mon);
                                        }
                                    }

                                    /*for (int i = 0; i < buflen; i++)
                                    {
                                        output_l[i] *= (float)volume;
                                        output_r[i] = output_l[i];
                                    }*/
                                }
                                else if (channel == 6)
                                {
                                    if (monitor_enabled && (rb_monOUT_l.WriteSpace() >= buflen) &&
                                        (rb_monOUT_r.WriteSpace() >= buflen))
                                    {
                                        fixed (float* output_l = &tmp_buffer_ch1[0])
                                        fixed (float* output_r = &tmp_buffer_ch2[0])
                                        {
                                            EnterCriticalSection(cs_mon);
                                            rb_monOUT_l.WritePtr(output_r, buflen);
                                            rb_monOUT_r.WritePtr(output_r, buflen);
                                            LeaveCriticalSection(cs_mon);
                                        }
                                    }

                                    /*for (int i = 0; i < buflen; i++)
                                    {
                                        output_l[i] *= (float)volume;
                                        output_r[i] = output_l[i];
                                    }*/
                                }

                            switch (MainForm.OpModeVFOA)
                            {
                                case Mode.CW:
                                    {
                                        if (buffer_ptr_A == 2048)
                                        {
                                            Array.Copy(CH1_buffer, MainForm.cwDecoder.fft_buff_ch5, 2048);
                                            MainForm.cwDecoder.AudioEvent1.Set();
                                            buffer_ptr_A = 0;
                                        }
                                    }
                                    break;

                                case Mode.RTTY:
                                    {
                                        if (MainForm.rtty.run_thread)
                                        {
                                            if (buffer_ptr_A == 2048)
                                            {
                                                Array.Copy(CH1_buffer, MainForm.rtty.ch1_buffer, 2048);
                                                Array.Copy(CH2_buffer, MainForm.rtty.ch2_buffer, 2048);
                                                MainForm.rtty.AudioEventRX1.Set();
                                                buffer_ptr_A = 0;
                                            }
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
                                        if (MainForm.psk.run_thread)
                                        {
                                            if (buffer_ptr_A == 2048)
                                            {
                                                Array.Copy(CH1_buffer, MainForm.psk.ch1_buffer, 2048);
                                                Array.Copy(CH2_buffer, MainForm.psk.ch2_buffer, 2048);
                                                MainForm.psk.AudioEvent1.Set();
                                                buffer_ptr_A = 0;
                                            }
                                        }
                                    }
                                    break;
                            }

                            if (RX2)
                            {
                                switch (MainForm.OpModeVFOB)
                                {
                                    case Mode.CW:
                                        {
                                            if (mox_switch_time >= 5)
                                            {
                                                if (buffer_ptr_B >= 2048)
                                                {
                                                    switch (MainForm.OpModeVFOA)
                                                    {
                                                        case Mode.CW:
                                                        case Mode.BPSK31:
                                                        case Mode.BPSK63:
                                                        case Mode.BPSK125:
                                                        case Mode.BPSK250:
                                                        case Mode.QPSK31:
                                                        case Mode.QPSK63:
                                                        case Mode.QPSK125:
                                                        case Mode.QPSK250:
                                                            Array.Copy(CH2_buffer, MainForm.cwDecoder.fft_buff_ch6, 2048);
                                                            break;

                                                        case Mode.RTTY:
                                                            Array.Copy(CH3_buffer, MainForm.cwDecoder.fft_buff_ch6, 2048);
                                                            break;
                                                    }

                                                    MainForm.cwDecoder.AudioEvent2.Set();
                                                    buffer_ptr_B = 0;
                                                }

                                            }
                                            else
                                            {
                                                mox_switch_time++;

                                                if (buffer_ptr_B >= 2048)
                                                {
                                                    Array.Copy(zero_bufferF, MainForm.cwDecoder.fft_buff_ch6, 2048);    // mute
                                                    MainForm.cwDecoder.AudioEvent2.Set();

#if(WIN32)
                                                    array_ptr = (int*)output;
#endif

#if(WIN64)
                                                    array_ptr = (Int64*)output;
#endif

                                                    out_l_ptr1 = (float*)array_ptr[1];
                                                    out_r_ptr1 = (float*)array_ptr[0];
                                                    buffer_ptr_B = 0;
                                                }
                                            }
                                        }
                                        break;

                                    case Mode.RTTY:
                                        {
                                            if (MainForm.rtty.run_thread)
                                            {
                                                if (mox_switch_time >= 5)
                                                {
                                                    if (buffer_ptr_B >= 2048)
                                                    {

                                                        Array.Copy(CH3_buffer, MainForm.rtty.ch3_buffer, 2048);
                                                        Array.Copy(CH4_buffer, MainForm.rtty.ch4_buffer, 2048);
                                                        MainForm.rtty.AudioEventRX2.Set();
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    mox_switch_time++;

                                                    if (buffer_ptr_B >= 2048)
                                                    {
                                                        Array.Copy(zero_bufferF, MainForm.rtty.ch3_buffer, 2048);
                                                        Array.Copy(zero_bufferF, MainForm.rtty.ch4_buffer, 2048);
                                                        MainForm.rtty.AudioEventRX2.Set();

#if(WIN32)
                                                        array_ptr = (int*)output;
#endif

#if(WIN64)
                                                        array_ptr = (Int64*)output;
#endif

                                                        out_l_ptr1 = (float*)array_ptr[1];
                                                        out_r_ptr1 = (float*)array_ptr[0];
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
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
                                            if (MainForm.psk.run_thread)
                                            {
                                                if (mox_switch_time >= 5)
                                                {
                                                    if (buffer_ptr_B >= 2048)
                                                    {
                                                        switch (MainForm.OpModeVFOA)
                                                        {
                                                            case Mode.CW:
                                                            case Mode.BPSK31:
                                                            case Mode.BPSK63:
                                                            case Mode.BPSK125:
                                                            case Mode.BPSK250:
                                                            case Mode.QPSK31:
                                                            case Mode.QPSK63:
                                                            case Mode.QPSK125:
                                                            case Mode.QPSK250:
                                                                Array.Copy(CH2_buffer, MainForm.psk.ch2_buffer, 2048);
                                                                break;

                                                            case Mode.RTTY:
                                                                Array.Copy(CH3_buffer, MainForm.psk.ch2_buffer, 2048);
                                                                break;
                                                        }

                                                        MainForm.psk.AudioEvent2.Set();
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    mox_switch_time++;

                                                    if (buffer_ptr_B >= 2048)
                                                    {
                                                        Array.Copy(zero_bufferF, MainForm.psk.ch2_buffer, 2048);    // mute
                                                        MainForm.psk.AudioEvent2.Set();

#if(WIN32)
                                                        array_ptr = (int*)output;
#endif

#if(WIN64)
                                                        array_ptr = (Int64*)output;
#endif

                                                        out_l_ptr1 = (float*)array_ptr[1];
                                                        out_r_ptr1 = (float*)array_ptr[0];
                                                        buffer_ptr_B = 0;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        else
                            mox_switch_time++;
                    }

                    #endregion
                }

                #region Scope

                if ((MainForm.DisplayMode == DisplayMode.PANASCOPE || MainForm.DisplayMode == DisplayMode.PANASCOPE_INV)
                    && !pause_scope && buffer_ptr_A==0)
                {

                    if (channel == 5)
                    {
                        DoScope(CH1_buffer, 2048);
                    }
                    else if (channel == 6)
                    {
                        DoScope(CH2_buffer, 2048);
                    }
                }

                #endregion

                return callback_return;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 0;
            }
        }


        unsafe public static int MonitorCallback(void* input, void* output, int buflen,
    PA19.PaStreamCallbackTimeInfo* timeInfo, int statusFlags, void* userData)
        {
            try
            {
#if(WIN64)
                Int64* array_ptr = (Int64*)input;
                float* in_l_ptr1 = (float*)array_ptr[0];
                float* in_r_ptr1 = (float*)array_ptr[1];
                double* VAC_in = (double*)input;
                array_ptr = (Int64*)output;
                float* out_l_ptr1 = (float*)array_ptr[1];
                float* out_r_ptr1 = (float*)array_ptr[0];
                array_ptr = (Int64*)input;
                in_l_ptr1 = (float*)array_ptr[0];
                in_r_ptr1 = (float*)array_ptr[1];
#endif

#if(WIN32)
                int* array_ptr = (int*)output;
                float* out_l_ptr1 = (float*)array_ptr[1];
                float* out_r_ptr1 = (float*)array_ptr[0];
#endif

                if ((rb_monOUT_l.ReadSpace() >= buflen) && (rb_monOUT_r.ReadSpace() >= buflen))
                {
                    EnterCriticalSection(cs_mon);
                    rb_monOUT_l.ReadPtr(out_l_ptr1, buflen);
                    rb_monOUT_r.ReadPtr(out_r_ptr1, buflen);
                    LeaveCriticalSection(cs_mon);
                }

                for (int i = 0; i < buflen; i++)
                {
                    out_l_ptr1[i] *= (float)volume;
                    out_r_ptr1[i] *= (float)volume;
                }

                return callback_return;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 0;
            }
        }

        public static bool Start()
        {
            bool retval = false;
            int RB_size = 8192;

            try
            {
                decimation = Audio.SampleRate / MainForm.cwDecoder.rate;
                monitor_enabled = false;
                buffer_ptr_A = 0;
                phaseacc = TWOPI / block_size;

                if (rb_mon_l == null || rb_mon_l.Lenght() != Math.Max(MainForm.recorder.buffer_size, 1024) * 1024)
                {
                    rb_mon_l = null;
                    rb_mon_l = new RingBufferFloat(Math.Max(MainForm.recorder.buffer_size, 1024) * 1024);
                }

                if (rb_mon_r == null || rb_mon_r.Lenght() != Math.Max(MainForm.recorder.buffer_size, 1024) * 1024)
                {
                    rb_mon_r = null;
                    rb_mon_r = new RingBufferFloat(Math.Max(MainForm.recorder.buffer_size, 1024) * 1024);
                }

                for (int i = 0; i < 2048; i++)
                {
                    zero_buffer[i].Re = 0.0f;
                    zero_buffer[i].Im = 0.0f;
                }

                if (QSK)
                {
                    retval = StartAudio(ref input_callback, (uint)block_size, sample_rate,
                        host, input_dev, output_dev, num_channels, 0, latency);
                    retval = StartAudio(ref outpu_callback, (uint)block_size, sample_rate,
                        host, input_dev, output_dev, num_channels, 1, latency);
                }
                else
                {
                    retval = StartAudio(ref callback, (uint)block_size, sample_rate,
                        host, input_dev, output_dev, num_channels, 0, latency);
                }

                if (!retval)
                    return false;

                if (rb_monOUT_l == null) rb_monOUT_l = new RingBufferFloat(RB_size);
                rb_monOUT_l.Restart(RB_size);

                if (rb_monOUT_r == null) rb_monOUT_r = new RingBufferFloat(RB_size);
                rb_monOUT_r.Restart(RB_size);

                if (rb_monIN_l == null) rb_monIN_l = new RingBufferFloat(RB_size);
                rb_monIN_l.Restart(RB_size);

                if (rb_monIN_r == null) rb_monIN_r = new RingBufferFloat(RB_size);
                rb_monIN_r.Restart(RB_size);

                if (monitor_dev != -1)
                {
                    cs_mon = (void*)0x0;
                    cs_mon = NewCriticalSection();

                    if (InitializeCriticalSectionAndSpinCount(cs_mon, 0x00000080) == 0)
                    {
                        monitor_enabled = false;
                        Debug.WriteLine("CriticalSection Failed");
                    }
                    else
                    {
                        monitor_enabled = true;

                        retval = StartAudio(ref monitor_callback, 2048, sample_rate,
                            monitor_host, input_dev, monitor_dev, num_channels, 2, 50);  //latency);
                    }
                }

                audio_paused = false;
                audio_running = true;

                if (!retval)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting audio stream!\n" + ex.ToString());
                audio_running = false;
                return false;
            }
        }

        public unsafe static bool StartAudio(ref PA19.PaStreamCallback callback,
            uint block_size, double sample_rate, int host_api_index, int input_dev_index,
            int output_dev_index, int num_channels, int callback_num, int latency_ms)
        {
            try
            {
                int in_dev = PA19.PA_HostApiDeviceIndexToDeviceIndex(host_api_index, input_dev_index);
                int out_dev = PA19.PA_HostApiDeviceIndexToDeviceIndex(host_api_index, output_dev_index);

                PA19.PaStreamParameters inparam = new PA19.PaStreamParameters();
                PA19.PaStreamParameters outparam = new PA19.PaStreamParameters();

                inparam.device = in_dev;
                inparam.channelCount = num_channels;

                    inparam.sampleFormat = PA19.paFloat32 | PA19.paNonInterleaved;
                    inparam.suggestedLatency = ((float)latency_ms / 1000);
                    outparam.device = out_dev;
                    outparam.channelCount = num_channels;
                    outparam.sampleFormat = PA19.paFloat32 | PA19.paNonInterleaved;
                    outparam.suggestedLatency = ((float)latency_ms / 1000);

                    if (host_api_index == PA19.PA_HostApiTypeIdToHostApiIndex(PA19.PaHostApiTypeId.paWASAPI))
                    {
                        PA19.PaWasapiStreamInfo stream_info = new PA19.PaWasapiStreamInfo();
                        stream_info.hostApiType = PA19.PaHostApiTypeId.paWASAPI;
                        stream_info.version = 1;
                        stream_info.size = (UInt32)sizeof(PA19.PaWasapiStreamInfo);
                        inparam.hostApiSpecificStreamInfo = &stream_info;
                        outparam.hostApiSpecificStreamInfo = &stream_info;

                        if (QSK && callback_num != 2)
                        {
                            stream_info.flags = (UInt32)PA19.PaWasapiFlags.paWinWasapiExclusive |
                                PA19.AUDCLNT_STREAMFLAGS_EVENTCALLBACK;
                        }
                    }

                int error = 0;

                if (SDRmode)
                {
                    if (QSK)
                    {
                        if (callback_num == 0)
                            error = PA19.PA_OpenStream(out stream1, &inparam, null, sample_rate, block_size, 0, callback, 0, 0);
                        else if (callback_num == 1)
                            error = PA19.PA_OpenStream(out stream2, null, &outparam, sample_rate, block_size, 0, callback, 0, 1);
                        else if (callback_num == 2)
                            error = PA19.PA_OpenStream(out stream3, null, &outparam, sample_rate, block_size, 0, callback, 0, 2);
                    }
                    else
                    {
                        if (callback_num == 0)
                            error = PA19.PA_OpenStream(out stream1, &inparam, &outparam, sample_rate, block_size, 0, callback, 0, 0);
                        else if (callback_num == 2)
                            error = PA19.PA_OpenStream(out stream3, null, &outparam, sample_rate, block_size, 0, callback, 0, 2);
                    }

                    if (error != 0)
                    {
                        MessageBox.Show(PA19.PA_GetErrorText(error), "PortAudio Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {                                           // MorseRunner mode
                    error = 0;

                    if (callback_num == 0)
                        error = PA19.PA_OpenStream(out stream1, &inparam, &outparam, sample_rate, block_size, 0, callback, 0, 0);
                    else if (callback_num == 2)
                        error = PA19.PA_OpenStream(out stream3, null, &outparam, sample_rate, block_size, 0, callback, 0, 2);
                }

                if (callback_num == 0)
                    error = PA19.PA_StartStream(stream1);
                else if (callback_num == 1)
                    error = PA19.PA_StartStream(stream2);
                else if (callback_num == 2)
                    error = PA19.PA_StartStream(stream3);

                if (error != 0)
                {
                    MessageBox.Show(PA19.PA_GetErrorText(error), "PortAudio Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in StartAudio function!\n" + ex.ToString());
                return false;
            }
        }

        public unsafe static void StopAudio()
        {
            try
            {
                int count = 0;

                audio_run.WaitOne(1000);

                if (loopback)
                {
                    loopback = false;
                    MainForm.recorder.Stop();
                    Thread.Sleep(1000);
                }

                audio_paused = true;

                while (audio_running)
                {
                    Thread.Sleep(1);
                    count++;

                    if (count > 1000)
                        audio_running = false;
                }

                if (monitor_enabled)
                {
                    DeleteCriticalSection(cs_mon);
                    DestroyCriticalSection(cs_mon);
                }

                PA19.PA_AbortStream(stream1);
                PA19.PA_CloseStream(stream1);
                PA19.PA_AbortStream(stream2);
                PA19.PA_CloseStream(stream2);
                PA19.PA_AbortStream(stream3);
                PA19.PA_CloseStream(stream3);
                Debug.Write("Audio is stoped!\n");

                audio_run.ReleaseMutex();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                audio_run.ReleaseMutex();
            }
        }

        public static ArrayList GetPAInputDevices(int hostIndex)
        {
            ArrayList a = new ArrayList();
            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxInputChannels > 0)
                    a.Add(new PADeviceInfo(devInfo.name, i)/* + " - " + devIndex*/);
            }
            return a;
        }

        public static ArrayList GetPAOutputDevices(int hostIndex)
        {
            ArrayList a = new ArrayList();
            PA19.PaHostApiInfo hostInfo = PA19.PA_GetHostApiInfo(hostIndex);
            for (int i = 0; i < hostInfo.deviceCount; i++)
            {
                int devIndex = PA19.PA_HostApiDeviceIndexToDeviceIndex(hostIndex, i);
                PA19.PaDeviceInfo devInfo = PA19.PA_GetDeviceInfo(devIndex);
                if (devInfo.maxOutputChannels > 0)
                    a.Add(new PADeviceInfo(devInfo.name, i)/* + " - " + devIndex*/);
            }
            return a;
        }

        public static ArrayList GetPAHosts() // returns a text list of driver types
        {
            ArrayList a = new ArrayList();
            for (int i = 0; i < PA19.PA_GetHostApiCount(); i++)
            {
                PA19.PaHostApiInfo info = PA19.PA_GetHostApiInfo(i);
                a.Add(info.name);
            }
            return a;
        }

        public unsafe static PA19.PaStreamInfo GetStreamInfo(int type)
        {
            try
            {
                PA19.PaStreamInfo stream_info = new PA19.PaStreamInfo();

                switch (type)
                {
                    case 1:
                        stream_info = PA19.PA_GetStreamInfo(stream1);
                        break;
                    case 2:
                        stream_info = PA19.PA_GetStreamInfo(stream2);
                        break;
                    case 3:
                        stream_info = PA19.PA_GetStreamInfo(stream3);
                        break;
                }

                return stream_info;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                PA19.PaStreamInfo stream_info = new PA19.PaStreamInfo();
                return stream_info;
            }
        }

        #endregion

        #region Scope Stuff

        private static int scope_samples_per_pixel = 512;
        public static int ScopeSamplesPerPixel
        {
            get { return scope_samples_per_pixel; }
            set { scope_samples_per_pixel = value; }
        }

        private static int scope_display_width = 704;
        public static int ScopeDisplayWidth
        {
            get { return scope_display_width; }
            set { scope_display_width = value; }
        }

        public static bool pause_scope = false;
        private static int scope_sample_index = 0;
        private static int scope_pixel_index = 0;
        private static float scope_pixel_min = float.MaxValue;
        private static float scope_pixel_max = float.MinValue;

        public static float[] scope_min;
        public static float[] ScopeMin
        {
            set { scope_min = value; }
        }

        public static float[] scope_max;
        public static float[] ScopeMax
        {
            set { scope_max = value; }
        }

        unsafe private static void DoScope(float[] buf, int frameCount)
        {
            try
            {
                if (MainForm.VideoDriver == DisplayDriver.GDI)
                {
                    if (scope_min == null || scope_min.Length < scope_display_width)
                    {
                        if (Display_GDI.ScopeMin == null || Display_GDI.ScopeMin.Length < scope_display_width)
                            Display_GDI.ScopeMin = new float[scope_display_width];
                        scope_min = Display_GDI.ScopeMin;
                    }
                    if (scope_max == null || scope_max.Length < scope_display_width)
                    {
                        if (Display_GDI.ScopeMax == null || Display_GDI.ScopeMax.Length < scope_display_width)
                            Display_GDI.ScopeMax = new float[scope_display_width];
                        scope_max = Display_GDI.ScopeMax;
                    }
                }
#if(DirectX)
                else if (MainForm.VideoDriver == DisplayDriver.DIRECTX)
                {
                    if (scope_min == null || scope_min.Length < scope_display_width)
                    {
                        if (DX.ScopeMin == null || DX.ScopeMin.Length < scope_display_width)
                            DX.ScopeMin = new float[scope_display_width];
                        {
                            scope_min = DX.ScopeMin;
                            scope_pixel_index = 0;
                        }
                    }
                    if (scope_max == null || scope_max.Length < scope_display_width)
                    {
                        if (DX.ScopeMax == null || DX.ScopeMax.Length < scope_display_width)
                            DX.ScopeMax = new float[scope_display_width];
                        {
                            scope_max = DX.ScopeMax;
                            scope_pixel_index = 0;
                        }
                    }
                }
#endif

                for (int i = 0; i < frameCount; i++)
                {
                    if (buf[i] * scope_level < scope_pixel_min) scope_pixel_min = buf[i] * scope_level;
                    if (buf[i] * scope_level > scope_pixel_max) scope_pixel_max = buf[i] * scope_level;

                    scope_sample_index++;

                    if (scope_sample_index >= scope_samples_per_pixel)
                    {
                        scope_sample_index = 0;
                        scope_min[scope_pixel_index] = scope_pixel_min;
                        scope_max[scope_pixel_index] = scope_pixel_max;

                        scope_pixel_min = float.MaxValue;
                        scope_pixel_max = float.MinValue;

                        scope_pixel_index++;

                        if (scope_pixel_index >= scope_display_width)
                            scope_pixel_index = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #region RTTY Scope

        public static bool pause_scope_mark = false;
        private static int scope_sample_index_mark = 0;
        private static int scope_pixel_index_mark = 0;
        private static float scope_pixel_min_mark = float.MaxValue;
        private static float scope_pixel_max_mark = float.MinValue;

        public static float[] scope_min_mark;
        public static float[] ScopeMinMark
        {
            set { scope_min_mark = value; }
        }

        public static float[] scope_max_mark;
        public static float[] ScopeMaxMark
        {
            set { scope_max_mark = value; }
        }

        public static bool pause_scope_space = false;
        private static int scope_sample_index_space = 0;
        private static int scope_pixel_index_space = 0;
        private static float scope_pixel_min_space = float.MaxValue;
        private static float scope_pixel_max_space = float.MinValue;

        public static float[] scope_min_space;
        public static float[] ScopeMinSpace
        {
            set { scope_min_space = value; }
        }

        public static float[] scope_max_space;
        public static float[] ScopeMaxSpace
        {
            set { scope_max_space = value; }
        }

        unsafe private static void DoScopeMark(float[] buf, int frameCount)
        {
            try
            {
                if (MainForm.VideoDriver == DisplayDriver.GDI)
                {
                    if (scope_min_mark == null || scope_min_mark.Length != scope_display_width)
                    {
                        if (Display_GDI.ScopeMinMark == null || Display_GDI.ScopeMinMark.Length < scope_display_width)
                            Display_GDI.ScopeMinMark = new float[scope_display_width];
                        scope_min_mark = Display_GDI.ScopeMinMark;
                    }
                    if (scope_max_mark == null || scope_max_mark.Length != scope_display_width)
                    {
                        if (Display_GDI.ScopeMaxMark == null || Display_GDI.ScopeMaxMark.Length < scope_display_width)
                            Display_GDI.ScopeMaxMark = new float[scope_display_width];
                        scope_max_mark = Display_GDI.ScopeMaxMark;
                    }
                }
#if(DirectX)
                else if (MainForm.VideoDriver == DisplayDriver.DIRECTX)
                {
                    if (scope_min_mark == null || scope_min_mark.Length != scope_display_width)
                    {
                        if (DX.ScopeMinMark == null || DX.ScopeMinMark.Length < scope_display_width)
                        {
                            DX.ScopeMinMark = new float[scope_display_width];
                            scope_min_mark = DX.ScopeMinMark;
                            scope_pixel_index_mark = 0;
                        }
                    }
                    if (scope_max_mark == null || scope_max_mark.Length != scope_display_width)
                    {
                        if (DX.ScopeMaxMark == null || DX.ScopeMaxMark.Length < scope_display_width)
                        {
                            DX.ScopeMaxMark = new float[scope_display_width];
                            scope_max_mark = DX.ScopeMaxMark;
                            scope_pixel_index_mark = 0;
                        }
                    }
                }
#endif

                for (int i = 0; i < frameCount; i++)
                {
                    if (buf[i] * scope_level < scope_pixel_min_mark) scope_pixel_min_mark = buf[i] * scope_level;
                    if (buf[i] * scope_level > scope_pixel_max_mark) scope_pixel_max_mark = buf[i] * scope_level;

                    scope_sample_index_mark++;

                    if (scope_sample_index_mark >= scope_samples_per_pixel)
                    {
                        scope_sample_index_mark = 0;
                        scope_min_mark[scope_pixel_index_mark] = scope_pixel_min_mark;
                        scope_max_mark[scope_pixel_index_mark] = scope_pixel_max_mark;

                        scope_pixel_min_mark = float.MaxValue;
                        scope_pixel_max_mark = float.MinValue;

                        scope_pixel_index_mark++;

                        if (scope_pixel_index_mark >= scope_display_width)
                            scope_pixel_index_mark = 0;
                    }
                }

                if (scope_pixel_index_mark != scope_pixel_index_space)
                {
                    scope_pixel_index_space = 0;
                    scope_pixel_index_mark = 0;
                }

                if (scope_sample_index_mark != scope_sample_index_space)
                {
                    scope_sample_index_mark = 0;
                    scope_sample_index_space = 0;
                }

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        unsafe private static void DoScopeSpace(float[] buf, int frameCount)
        {
            try
            {
                if (MainForm.VideoDriver == DisplayDriver.GDI)
                {
                    if (scope_min_space == null || scope_min_space.Length != scope_display_width)
                    {
                        if (Display_GDI.ScopeMinSpace == null || Display_GDI.ScopeMinSpace.Length < scope_display_width)
                            Display_GDI.ScopeMinSpace = new float[scope_display_width];
                        scope_min_space = Display_GDI.ScopeMinSpace;
                    }
                    if (scope_max_space == null || scope_max_space.Length != scope_display_width)
                    {
                        if (Display_GDI.ScopeMaxSpace == null || Display_GDI.ScopeMaxSpace.Length < scope_display_width)
                            Display_GDI.ScopeMaxSpace = new float[scope_display_width];
                        scope_max_space = Display_GDI.ScopeMaxSpace;
                    }
                }
#if(DirectX)
                else if (MainForm.VideoDriver == DisplayDriver.DIRECTX)
                {
                    if (scope_min_space == null || scope_min_space.Length != scope_display_width)
                    {
                        if (DX.ScopeMinSpace == null || DX.ScopeMinSpace.Length < scope_display_width)
                        {
                            DX.ScopeMinSpace = new float[scope_display_width];
                            scope_min_space = DX.ScopeMinSpace;
                            scope_pixel_index_space = 0;
                        }
                    }
                    if (scope_max_space == null || scope_max_space.Length != scope_display_width)
                    {
                        if (DX.ScopeMaxSpace == null || DX.ScopeMaxSpace.Length < scope_display_width)
                        {
                            DX.ScopeMaxSpace = new float[scope_display_width];
                            scope_max_space = DX.ScopeMaxSpace;
                            scope_pixel_index_space = 0;
                        }
                    }
                }
#endif

                for (int i = 0; i < frameCount; i++)
                {
                    if (buf[i] * scope_level < scope_pixel_min_space) scope_pixel_min_space = buf[i] * scope_level;
                    if (buf[i] * scope_level > scope_pixel_max_space) scope_pixel_max_space = buf[i] * scope_level;

                    scope_sample_index_space++;

                    if (scope_sample_index_space >= scope_samples_per_pixel)
                    {
                        scope_sample_index_space = 0;
                        scope_min_space[scope_pixel_index_space] = scope_pixel_min_space;
                        scope_max_space[scope_pixel_index_space] = scope_pixel_max_space;

                        scope_pixel_min_space = float.MaxValue;
                        scope_pixel_max_space = float.MinValue;

                        scope_pixel_index_space++;

                        if (scope_pixel_index_space >= scope_display_width)
                            scope_pixel_index_space = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #endregion
    }

    public class ProcessSampleThreadController
    {
        uint thread;
        public ProcessSampleThreadController(uint threadno)
        {
            this.thread = threadno;
        }

        public void ProcessSampleThread()
        {
            Audio.ProcessSamplesThread(thread);
        }
    }
}
