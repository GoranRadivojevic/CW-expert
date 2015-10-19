//=================================================================
// Setup.cs
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace CWExpert
{
    public partial class Setup : Form
    {
        private CWExpert MainForm;

        #region variable

        delegate void CrossThreadCallback(string command, string data);

        #endregion

        #region properties

        private Keyer_mode genesisKeyerMode = Keyer_mode.PHONE;
        public Keyer_mode GenesisKeyerMode
        {
            get { return genesisKeyerMode; }
            set { genesisKeyerMode = value; }
        }

        #endregion

        #region constructor

        public Setup(CWExpert main_form)
        {
            this.AutoScaleMode = AutoScaleMode.Inherit;
            InitializeComponent();
            float dpi = this.CreateGraphics().DpiX;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = 8.25f / ratio;
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;
            tbRTTYCarrierShift.Enabled = false;
            lblRTTYshift.Enabled = false;
            MainForm = main_form;
            MainForm.VideoDriver = DisplayDriver.GDI;
            comboRadioModel.Text = "Genesis G59";
            InitAudio();
            GetOptions();

            EventArgs e = EventArgs.Empty;

            chkSDRmode_CheckedChanged(this, e);
            chkRX2_CheckedChanged(this, e);
            chkPTTinv_CheckedChanged(this, e);
            chkRXOnly_CheckedChanged(this, e);
            chkPA10_CheckedChanged(this, e);
            udDisplayCalOffset_ValueChanged(this, e);
            udSmeterCalOffset_ValueChanged(this, e);
            udRTTYTXPreamble_ValueChanged(this, e);
            comboRTTYBaudRate_SelectedIndexChanged(this, e);
            comboRTTYBits_SelectedIndexChanged(this, e);
            comboRTTYParity_SelectedIndexChanged(this, e);
            comboRTTYStopBits_SelectedIndexChanged(this, e);
            udRTTYCarrierShift_SelectedIndexChanged(this, e);
            chkRX2_CheckedChanged(this, e);
            chkWaterfallReverse_CheckedChanged(this, EventArgs.Empty);
            radIQFixed_CheckedChanged(this, EventArgs.Empty);

#if(DirectX)
            DX.WaterfallHighThreshold = (int)udDisplayHigh.Value;
            DX.WaterfallLowThreshold = (int)udDisplayLow.Value;
#endif
            Display_GDI.WaterfallHighThreshold = (int)udDisplayHigh.Value;
            Display_GDI.WaterfallLowThreshold = (int)udDisplayLow.Value;

            int samples = (int)((double)udScopeTime.Value * Audio.SampleRate / 10000.0);
            Audio.ScopeSamplesPerPixel = samples;
        }

        #endregion

        #region Audio

        private void InitAudio()
        {
            try
            {
                GetHosts();

                if (comboAudioDriver.SelectedIndex < 0 &&
                    comboAudioDriver.Items.Count > 0)
                    comboAudioDriver.SelectedIndex = 0;

                if (comboMonitorDriver.SelectedIndex < 0 &&
                    comboMonitorDriver.Items.Count > 0)
                    comboMonitorDriver.SelectedIndex = 0;

                if (comboAudioSampleRate.Items.Count > 0)
                    comboAudioSampleRate.SelectedIndex = 0;

                GetDevices();
                GetMonitorDevices();
                comboAudioBuffer.Text = "2048";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in init audio!\n" + ex.ToString());
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                SaveOptions();
                MainForm.InitLOG();
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void comboAudioDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               Audio.Host = ((PADeviceInfo)comboAudioDriver.SelectedItem).Index;
               GetDevices();
               if (comboAudioInput.Items.Count != 0)
                   comboAudioInput.SelectedIndex = 0;
               if (comboAudioOutput.Items.Count != 0)
                   comboAudioOutput.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void comboAudioInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((PADeviceInfo)comboAudioInput.SelectedItem).Index;
                Audio.Input = index;
            }
            catch(Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void comboAudioOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((PADeviceInfo)comboAudioOutput.SelectedItem).Index;
                Audio.Output = index;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void comboMonitorDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Audio.MonitorHost = ((PADeviceInfo)comboMonitorDriver.SelectedItem).Index;
                GetMonitorDevices();

                if (comboAudioMonitor.Items.Count != 0)
                    comboAudioMonitor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void comboAudioMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((PADeviceInfo)comboAudioMonitor.SelectedItem).Index;
                Audio.Monitor = index;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                Audio.Monitor = -1;
            }
        }

        private void GetDevices()
        {
            try
            {
                comboAudioInput.Items.Clear();
                comboAudioOutput.Items.Clear();
                int host = ((PADeviceInfo)comboAudioDriver.SelectedItem).Index;
                ArrayList a = Audio.GetPAInputDevices(host);

                foreach (PADeviceInfo p in a)
                    comboAudioInput.Items.Add(p);

                a = Audio.GetPAOutputDevices(host);

                foreach (PADeviceInfo p in a)
                    comboAudioOutput.Items.Add(p);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while init audio devices!\n" + ex.ToString());
            }
        }

        private void GetMonitorDevices()
        {
            try
            {
                comboAudioMonitor.Items.Clear();
                int host = ((PADeviceInfo)comboMonitorDriver.SelectedItem).Index;
                ArrayList a = Audio.GetPAOutputDevices(host);

                foreach (PADeviceInfo p in a)
                    comboAudioMonitor.Items.Add(p);

                comboAudioMonitor.Items.Add("None");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while init Monitor devices!\n" + ex.ToString());
            }
        }

        private void GetHosts()
        {
            try
            {
                comboAudioDriver.Items.Clear();
                comboMonitorDriver.Items.Clear();
                int host_index = 0;

                foreach (string PAHostName in Audio.GetPAHosts())
                {
                    if (Audio.GetPAInputDevices(host_index).Count > 0)
                    {
                        comboAudioDriver.Items.Add(new PADeviceInfo(PAHostName, host_index));
                        comboMonitorDriver.Items.Add(new PADeviceInfo(PAHostName, host_index));
                    }

                    host_index++; //Increment host index
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while init audio devices!\n" + ex.ToString());
            }
        }

        private void comboAudioSampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool pwr = MainForm.PWR;
            int old_rate = Audio.SampleRate;
            int new_rate = Int32.Parse(comboAudioSampleRate.Text);

            if (new_rate != old_rate)
            {
                if (pwr)
                    MainForm.PWR = false;

                Audio.SampleRate = new_rate;

                if (!MainForm.booting)
                {
                    Audio.SetSampleRate(new_rate);
                    Thread.Sleep(100);
                    Audio.ResizeSDR(0, 4096);
                    Thread.Sleep(100);
                    Audio.SetAudioSize(Audio.BlockSize);
                }

                MainForm.PWR = pwr;
            }

            int samples = (int)((double)udScopeTime.Value * Audio.SampleRate / 10000.0);
            Audio.ScopeSamplesPerPixel = samples;

#if(DirectX)
            DX.SampleRate = new_rate;
#endif
            Display_GDI.SampleRate = new_rate;

            if (Audio.SDRmode)
            {
#if(DirectX)
                DX.RXDisplayHigh = new_rate / 2;
                DX.RXDisplayLow = -new_rate / 2;
#endif
                Display_GDI.RXDisplayHigh = new_rate / 2;
                Display_GDI.RXDisplayLow = -new_rate / 2;
            }
            else
            {
#if(DirectX)
                DX.RXDisplayHigh = new_rate / 4;
                DX.RXDisplayLow = 0;
#endif
                Display_GDI.RXDisplayHigh = new_rate / 4;
                Display_GDI.RXDisplayLow = 0;
            }
        }

        private void udLatency_ValueChanged(object sender, EventArgs e)
        {
            bool pwr = MainForm.PWR;
            int old_latency = Audio.Latency;
            int new_latency = (int)udLatency.Value;

            if (new_latency != old_latency)
            {
                if (pwr)
                    MainForm.PWR = false;

                Audio.Latency = new_latency;
                MainForm.PWR = pwr;
            }
        }

        private void comboAudioBuffer_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool pwr = MainForm.PWR;
            int old_block_size = Audio.BlockSize;
            int new_block_size = Int32.Parse(comboAudioBuffer.Text);

            //if (old_block_size != new_block_size)
            {
                if (pwr)
                    MainForm.PWR = false;

                Audio.BlockSize = new_block_size;

                if (!MainForm.booting)
                {
                    Thread.Sleep(100);
                    Audio.ResizeSDR(0, 4096);
                    Thread.Sleep(100);
                    Audio.SetAudioSize(Audio.BlockSize);
                }

                MainForm.PWR = pwr;
            }
        }

        private void btnAudioStreamInfo_Click(object sender, EventArgs e)
        {
            try
            {
                PA19.PaStreamInfo info;

                if (Audio.QSK)
                {
                    info = Audio.GetStreamInfo(1);
                    lblAudioStreamInputLatencyValue.Text = Math.Round((info.inputLatency * 1000), 1).ToString() + "mS";
                    info = Audio.GetStreamInfo(2);
                    lblAudioStreamOutputLatencyValuelabel.Text = Math.Round((info.outputLatency * 1000), 1).ToString() + "mS";
                    lblAudioStreamSampleRateValue.Text = info.sampleRate.ToString();
                }
                else
                {
                    info = Audio.GetStreamInfo(1);
                    lblAudioStreamInputLatencyValue.Text = Math.Round((info.inputLatency * 1000), 1).ToString() + "mS";
                    lblAudioStreamOutputLatencyValuelabel.Text = Math.Round((info.outputLatency * 1000), 1).ToString() + "mS";
                    lblAudioStreamSampleRateValue.Text = info.sampleRate.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udAudioOutputVoltage_ValueChanged(object sender, EventArgs e)
        {
            Audio.AudioVolts1 = (double)udAudioOutputVoltage.Value;
        }

        private void udMonitorFrequncy_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                    MainForm.cwEncoder.MonFreq = (double)udMonitorFrequncy.Value;

                if (MainForm.rtty != null)
                    MainForm.rtty.MonFreq = (double)udMonitorFrequncy.Value;

                if (MainForm.psk != null)
                    MainForm.psk.MonFreq = (double)udMonitorFrequncy.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void chkRXSwap_CheckedChanged(object sender, EventArgs e)
        {
            Audio.RXswap = chkRXSwap.Checked;
        }

        private void chkTXSwap_CheckedChanged(object sender, EventArgs e)
        {
            Audio.TXswap = chkTXSwap.Checked;
        }

        private void chkQSK_CheckedChanged(object sender, EventArgs e)
        {
            Audio.QSK = chkQSK.Checked;
        }

        #endregion

        #region misc function

        private void Setup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SaveOptions();
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Setup closing!\n" + ex.ToString());
            }
        }

        public void SaveOptions()
        {
            try
            {
                ArrayList a = new ArrayList();
                ArrayList temp = new ArrayList();

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
                    else if (c.GetType() == typeof(TextBox))
                        a.Add(c.Name + "/" + ((TextBox)c).Text);
                    else if (c.GetType() == typeof(TrackBar))
                        a.Add(c.Name + "/" + ((TrackBar)c).Value.ToString());
                }

                int alpha, r, g, b;
                alpha = Display_GDI.DataLineColor.A;
                r = Display_GDI.DataLineColor.R;
                g = Display_GDI.DataLineColor.G;
                b = Display_GDI.DataLineColor.B;
                a.Add("DisplayLineColor" + "/" + alpha.ToString() + "/" + r.ToString() + "/" +
                    g.ToString() + "/" + b.ToString());

                alpha = Display_GDI.PanFillColor.A;
                r = Display_GDI.PanFillColor.R;
                g = Display_GDI.PanFillColor.G;
                b = Display_GDI.PanFillColor.B;
                a.Add("PanFillColor" + "/" + alpha.ToString() + "/" + r.ToString() + "/" +
                    g.ToString() + "/" + b.ToString());

                a.Add("setup_top/" + this.Top.ToString());		// save form positions
                a.Add("setup_left/" + this.Left.ToString());
                a.Add("setup_width/" + this.Width.ToString());
                a.Add("setup_height/" + this.Height.ToString());

                DB.SaveVars("Setup Options", ref a);		// save the values to the DB
                DB.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in SaveOptions function!\n" + ex.ToString());
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

        public void GetOptions()
        {
            try
            {
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
                    else if (c.GetType() == typeof(TextBox))
                        textbox_list.Add(c);
                    else if (c.GetType() == typeof(TrackBar))
                        trackbar_list.Add(c);
                }

                temp.Clear();	// now that we have the controls we want, delete first list 

                ArrayList a = DB.GetVars("Setup Options");
                a.Sort();

                // restore saved values to the controls
                foreach (string s in a)				// string is in the format "name,value"
                {
                    string[] vals = s.Split('/');
                    string name = vals[0];

                    if (vals.Length > 2 && vals[0].StartsWith("txt"))
                    {
                        for (int i = 2; i < vals.Length; i++)
                            vals[1] += "/" + vals[i];
                    }

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
                    else if (s.StartsWith("DisplayLineColor"))
                    {
                        int alpha, r, g, b;
                        alpha = int.Parse(vals[1]);
                        r = int.Parse(vals[2]);
                        g = int.Parse(vals[3]);
                        b = int.Parse(vals[4]);
                        Color color = Color.FromArgb(alpha, r, g, b);
                        Display_GDI.DataLineColor = color;
                        btnLineColor.BackColor = color;

#if DirectX
                        DX.DataLineColor = color;
#endif
                    }
                    else if (s.StartsWith("PanFillColor"))
                    {
                        int alpha, r, g, b;
                        alpha = int.Parse(vals[1]);
                        r = int.Parse(vals[2]);
                        g = int.Parse(vals[3]);
                        b = int.Parse(vals[4]);
                        Color color = Color.FromArgb(alpha, r, g, b);
                        Display_GDI.PanFillColor = color;
                        btnFillColor.BackColor = color;

#if DirectX
                        DX.PanFillColor = color;
#endif
                    }
                    else if (s.StartsWith("setup_top"))
                    {
                        int top = Int32.Parse(vals[1]);
                        this.Top = top;
                    }
                    else if (s.StartsWith("setup_left"))
                    {
                        int left = Int32.Parse(vals[1]);
                        this.Left = left;
                    }
                    else if (s.StartsWith("setup_width"))
                    {
                        int width = Int32.Parse(vals[1]);
                        this.Width = width;
                    }
                    else if (s.StartsWith("setup_height"))
                    {
                        int height = Int32.Parse(vals[1]);
                        this.Height = height;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in GetOption function!\n" + ex.ToString());
            }
        }

        #endregion

        #region display settings

        private void udAveraging_ValueChanged(object sender, EventArgs e)
        {
            double buffer_time = (double)Audio.BlockSize / (double)Audio.SampleRate;
			int buffersToAvg = (int)((float)udAveraging.Value * 0.001 / buffer_time);
			buffersToAvg = Math.Max(2, buffersToAvg);
#if(DirectX)
            DX.DisplayAvgBlocks = buffersToAvg;
            DX.WaterfallAvgBlocks = buffersToAvg;
#endif
            Display_GDI.DisplayAvgBlocks = buffersToAvg;
        }

        private void udDisplayLow_ValueChanged(object sender, EventArgs e)
        {
#if(DirectX)
            DX.SpectrumGridMin = (int)udDisplayLow.Value;
            DX.WaterfallLowThreshold = (float)udDisplayLow.Value;
#endif
            Display_GDI.SpectrumGridMin = (int)udDisplayLow.Value;
            Display_GDI.WaterfallLowThreshold = (float)udDisplayLow.Value;
        }

        private void udDisplayHigh_ValueChanged(object sender, EventArgs e)
        {
#if(DirectX)
            DX.SpectrumGridMax = (int)udDisplayHigh.Value;
            DX.WaterfallHighThreshold = (float)udDisplayHigh.Value;
#endif
            Display_GDI.SpectrumGridMax = (int)udDisplayHigh.Value;
            Display_GDI.WaterfallHighThreshold = (float)udDisplayHigh.Value;
        }

        private void chkDisplayAveraging_CheckedChanged(object sender, EventArgs e)
        {
                Display_GDI.AverageOn = chkDisplayAveraging.Checked;
#if(DirectX)
                DX.AverageOn = chkDisplayAveraging.Checked;
#endif
        }

        private void udDisplayCalOffset_ValueChanged(object sender, EventArgs e)
        {
#if(DirectX)
            DX.DisplayCalOffset = (float)udDisplayCalOffset.Value;
#endif
            Display_GDI.DisplayCalOffset = (float)udDisplayCalOffset.Value;
        }

        private void comboDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDisplayMode.Text == "Panafall")
                MainForm.DisplayMode = DisplayMode.PANAFALL;
            else if (comboDisplayMode.Text == "Panafall_inv")
                MainForm.DisplayMode = DisplayMode.PANAFALL_INV;
            else if (comboDisplayMode.Text == "Panascope")
                MainForm.DisplayMode = DisplayMode.PANASCOPE;
            else if (comboDisplayMode.Text == "Panascope_inv")
                MainForm.DisplayMode = DisplayMode.PANASCOPE_INV;
            else
                MainForm.DisplayMode = DisplayMode.PANAFALL;
        }

        private void comboMonitorMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboMonitorMode.Text)
            {
                case "Scope":
                    MainForm.MonitorMode = DisplayMode.SCOPE;
                    break;

                case "Waterfall":
                    MainForm.MonitorMode = DisplayMode.WATERFALL;
                    break;

                case "Panadapter":
                    MainForm.MonitorMode = DisplayMode.PANADAPTER;
                    break;
            }
        }

        private void radDisplayGDI_CheckedChanged(object sender, EventArgs e)
        {
            if (radDisplayGDI.Checked)
            {
                MainForm.VideoDriver = DisplayDriver.GDI;
                Display_GDI.AverageOn = chkDisplayAveraging.Checked;
            }
        }

        private void radDisplayDirectX_CheckedChanged(object sender, EventArgs e)
        {
#if(DirectX)
            if (radDisplayDirectX.Checked)
            {
                MainForm.VideoDriver = DisplayDriver.DIRECTX;
                DX.AverageOn = chkDisplayAveraging.Checked;
            }
            else
#endif
            {
                MainForm.VideoDriver = DisplayDriver.GDI;
                Display_GDI.AverageOn = chkDisplayAveraging.Checked;
            }
        }

        private void radDirectXHW_CheckedChanged(object sender, EventArgs e)
        {
#if DirectX
            if (radDirectXHW.Checked)
                DX.DirectXRenderType = RenderType.HARDWARE;

            MainForm.SMeter.DirectXRenderType = AnalogGAuge.AGauge.RenderType.HARDWARE;
#endif
        }

        private void radDirectXSW_CheckedChanged(object sender, EventArgs e)
        {
#if DirectX
            if (radDirectXSW.Checked)
                DX.DirectXRenderType = RenderType.SOFTWARE;

            MainForm.SMeter.DirectXRenderType = AnalogGAuge.AGauge.RenderType.SOFTWARE;
#endif
        }

        private void chkWaterfallReverse_CheckedChanged(object sender, EventArgs e)
        {
            Display_GDI.ReverseWaterfall = chkWaterfallReverse.Checked;

#if DirectX
            DX.ReverseWaterfall = chkWaterfallReverse.Checked;
#endif
        }

        private void udScopeTime_ValueChanged(object sender, EventArgs e)
        {
            int samples = (int)((double)udScopeTime.Value * Audio.SampleRate / 10000.0);
            Audio.ScopeSamplesPerPixel = samples;
        }

        private void btnLineColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Color.White;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnLineColor.BackColor = colorDialog1.Color;
#if DirectX
                DX.DataLineColor = colorDialog1.Color;
#endif
                Display_GDI.DataLineColor = colorDialog1.Color;
            }
        }

        private void udDisplayRefresh_ValueChanged(object sender, EventArgs e)
        {
            MainForm.RefreshTime = (int)udDisplayRefresh.Value;
        }

        private void chkFillPanadapter_CheckedChanged(object sender, EventArgs e)
        {
#if DirectX
            DX.pan_fill = chkFillPanadapter.Checked;
#endif
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Display_GDI.PanFillColor;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnFillColor.BackColor = colorDialog1.Color;
#if DirectX
                DX.PanFillColor = colorDialog1.Color;
#endif
                Display_GDI.PanFillColor = colorDialog1.Color;
            }
        }

        #endregion

        #region options

        private void chkRXOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!MainForm.booting && MainForm.cwDecoder != null)
                MainForm.cwDecoder.rx_only = chkRXOnly.Checked;
        }

        private void chkAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.AlwaysOnTop = chkAlwaysOnTop.Checked;
        }

        private void chkTelnet_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.UseTelnet = chkTelnet.Checked;
        }

        #endregion

        #region Radio

        private void chkPA10_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPA10.Checked)
                MainForm.PA10_present = true;
            else
                MainForm.PA10_present = false;
        }

        private void chkPTTinv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPTTinv.Checked)
                MainForm.g59_ptt_inverted = 1;
            else
                MainForm.g59_ptt_inverted = 0;
        }

        private void chkRX2_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.GenesisSecRXAnt = chkRX2.Checked;
        }

        private void udSmeterCalOffset_ValueChanged(object sender, EventArgs e)
        {
            MainForm.Smeter_cal_offset = (float)udSmeterCalOffset.Value;
        }

        private void udCWPitch_ValueChanged(object sender, EventArgs e)
        {
            MainForm.CWPitch = (int)udCWPitch.Value;
        }

        private void udNBTreshold_ValueChanged(object sender, EventArgs e)
        {
            MainForm.NBvals = (0.165 * (double)(udNBThreshold.Value));
        }

        private void udCWSpeed_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!MainForm.booting)
                {
                    if (MainForm.cwEncoder != null)
                        MainForm.cwEncoder.CWSpedd = (int)udCWSpeed.Value;

                    MainForm.G59_set_keyer();
                    MainForm.cwEncoder.SetKeyerSpeed((float)udCWSpeed.Value);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void chkG59Iambic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeyerModeChange();
                MainForm.cwEncoder.SetKeyerIambic(chkG59Iambic.Checked);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void chkG59IambicBmode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeyerModeChange();

                if (chkG59IambicBmode.Checked)
                    MainForm.cwEncoder.SetKeyerMode(1);
                else
                    MainForm.cwEncoder.SetKeyerMode(0);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void chkG59IambicRev_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeyerModeChange();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udG59DASHDOTRatio_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.G59_set_keyer();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #region DSP

        private void udPhase_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                    MainForm.cwEncoder.TXPhase = (float)udTXPhase.Value;

                if (MainForm.rtty != null)
                    MainForm.rtty.TXPhase = (float)udTXPhase.Value;

                if (MainForm.psk != null)
                    MainForm.psk.TXPhase = (float)udTXPhase.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbTXPhase_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                udTXPhase.Value = tbTXPhase.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udGain_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                    MainForm.cwEncoder.TXGain = (float)udTXGain.Value;

                if (MainForm.rtty != null)
                    MainForm.rtty.TXGain = (float)udTXGain.Value;

                if (MainForm.psk != null)
                    MainForm.psk.TXGain = (float)udTXGain.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbGain_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                udTXGain.Value = tbTXGain.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udRXPhase_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.IQcorrection == IQ_correction.FIXED)
                    MainForm.SetRXIQGainPhase(1, (float)udRXGain.Value, (float)udRXPhase.Value);

                tbRXPhase.Value = (int)udRXPhase.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbRXPhase_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                udRXPhase.Value = tbRXPhase.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udRXGain_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.IQcorrection == IQ_correction.FIXED)
                    MainForm.SetRXIQGainPhase(1, (float)udRXGain.Value, (float)udRXPhase.Value);

                tbRXGain.Value = (int)udRXGain.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbRXGain_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                udRXGain.Value = tbRXGain.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void bttnRXCalBand_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.rx_image_gain_table[(int)MainForm.CurrentBand] = (float)udRXGain.Value;
                MainForm.rx_image_phase_table[(int)MainForm.CurrentBand] = (float)udRXPhase.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void bttnRXCallAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= (int)Band.B6M; i++)
                {
                    MainForm.rx_image_gain_table[i] = (float)tbRXGain.Value;
                    MainForm.rx_image_phase_table[i] = (float)tbRXPhase.Value;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void bttnRXClearBand_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.rx_image_gain_table[(int)MainForm.CurrentBand] = 0.0f;
                MainForm.rx_image_phase_table[(int)MainForm.CurrentBand] = 0.0f;
                udRXPhase.Value = 0;
                udRXGain.Value = 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void bttnRXClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= (int)Band.B6M; i++)
                {
                    MainForm.rx_image_gain_table[i] = 0.0f;
                    MainForm.rx_image_phase_table[i] = 0.0f;
                }

                udRXGain.Value = 0;
                udRXPhase.Value = 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void radIQFixed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radIQFixed.Checked)
                    MainForm.IQcorrection = IQ_correction.FIXED;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void radIQWBIR_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radIQWBIR.Checked)
                    MainForm.IQcorrection = IQ_correction.WBIR;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udCWRise_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                {
                    MainForm.cwEncoder.Transmiter.cwt.rise.dur = (float)udCWRise.Value;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udCWFall_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                    MainForm.cwEncoder.Transmiter.cwt.fall.dur = (float)udCWFall.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udTXIfShift_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                {
                    MainForm.cwEncoder.Transmiter.cwt.osc.freq = (double)udTXIfShift.Value;
                    MainForm.cwEncoder.TXIfShift = (double)udTXIfShift.Value;
                }
                if (MainForm.rtty != null)
                {
                    MainForm.rtty.TXIfShift = (double)udTXIfShift.Value;
                    MainForm.rtty.update_trx1 = true;
                    MainForm.rtty.update_trx2 = true;
                }

                if (MainForm.psk != null)
                {
                    MainForm.psk.TXIfShift = (double)udTXIfShift.Value;
                    MainForm.psk.update_trx1 = true;
                    MainForm.psk.update_trx2 = true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udTXOffDelay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.cwEncoder != null)
                    MainForm.cwEncoder.TXOffDelay = (int)udTXOffDelay.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        private void udMsgRptTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.MsgRptTime = (int)udMsgRptTime.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnSi570Test_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.genesis != null)
                {
                    MainForm.genesis.Si570_freq = 56000000;  // reinit!
                    MainForm.LOSC = MainForm.LOSC;       // force refresh
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udSi570I2CAddress_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.genesis != null)
                    MainForm.genesis.si570_i2c_address = (int)udSi570I2CAddress.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        private void comboRadioModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboRadioModel.Text)
                {
                    case "Genesis G59":
                        MainForm.CurrentModel = Model.GENESIS_G59USB;
                        break;

                    case "Genesis G11":
                        MainForm.CurrentModel = Model.GENESIS_G11;
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udTXSwicthTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.TXSwitchTime = (int)udTXSwicthTime.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void KeyerModeChange()
        {
            try
            {
                    if (chkG59Iambic.Checked)
                    {
                        if (chkG59IambicBmode.Checked & !chkG59IambicRev.Checked)
                            genesisKeyerMode = Keyer_mode.Iambic_B_Mode;
                        else if (chkG59IambicRev.Checked && !chkG59IambicBmode.Checked)
                            genesisKeyerMode = Keyer_mode.IambicReverse;
                        else if (chkG59IambicRev.Checked && chkG59IambicBmode.Checked)
                            genesisKeyerMode = Keyer_mode.Iambic_Reverse_B_Mode;
                        else
                            genesisKeyerMode = Keyer_mode.Iambic;
                    }
                    else
                        genesisKeyerMode = Keyer_mode.HandKey;

                        MainForm.G59_set_keyer();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region RTTY settings

        public double baudrate = 45;
        public double shift = 170;
        public int stopbits = 1;
        public int parity = 0;
        public int bits = 5;
        public bool reverse = false;

        private void comboRTTYBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboRTTYBaudRate.Text)
            {
                case "45":
                    baudrate = 45;
                    break;
                case "45.45":
                    baudrate = 45.45;
                    break;
                case "50":
                    baudrate = 50;
                    break;
                case "56":
                    baudrate = 56;
                    break;
                case "75":
                    baudrate = 75;
                    break;
                case "100":
                    baudrate = 100;
                    break;
                case "110":
                    baudrate = 110;
                    break;
                case "150":
                    baudrate = 150;
                    break;
                case "200":
                    baudrate = 200;
                    break;
                case "300":
                    baudrate = 300;
                    break;
            }

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }

            if (MainForm.OpModeVFOA == Mode.RTTY || MainForm.OpModeVFOB == Mode.RTTY)
                MainForm.rTTYToolStripMenuItem_Click(null, null);
        }

        private void udRTTYCarrierShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbRTTYCarrierShift.Enabled = false;
            lblRTTYshift.Enabled = false;

            switch (comboRTTYCarrierShift.Text)
            {
                case "85":
                    shift = 85;
                    break;
                case "160":
                    shift = 160;
                    break;
                case "170":
                    shift = 170;
                    break;
                case "182":
                    shift = 182;
                    break;
                case "200":
                    shift = 200;
                    break;
                case "240":
                    shift = 240;
                    break;
                case "350":
                    shift = 350;
                    break;
                case "425":
                    shift = 425;
                    break;
                case "850":
                    shift = 850;
                    break;
                case "Custom":
                    tbRTTYCarrierShift.Enabled = true;
                    lblRTTYshift.Enabled = true;
                    shift = tbRTTYCarrierShift.Value; ;
                    break;
            }

            MainForm.VFOA = MainForm.VFOA;      // refresh     

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void tbRTTYCarrierShift_Scroll(object sender, EventArgs e)
        {
            if (MainForm.rtty != null && tbRTTYCarrierShift.Enabled)
            {
                MainForm.rtty.trx.modem[0].shift = tbRTTYCarrierShift.Value;
            }

            shift = tbRTTYCarrierShift.Value;

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void comboRTTYStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboRTTYStopBits.Text)
            {
                case "1":
                    stopbits = 0;
                    break;
                case "1.5":
                    stopbits = 1;
                    break;
                case "2":
                    stopbits = 2;
                    break;
            }

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void comboRTTYParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboRTTYParity.Text)
            {
                case "None":
                    parity = 0;
                    break;
                case "Even":
                    parity = 1;
                    break;
                case "Odd":
                    parity = 2;
                    break;
                case "Zero":
                    parity = 3;
                    break;
                case "One":
                    parity = 4;
                    break;
            }

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void comboRTTYBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboRTTYBits.Text)
            {
                case "5":
                    bits = 5;
                    break;
                case "7":
                    bits = 7;
                    break;
                case "8":
                    bits = 8;
                    break;
            }

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void udRTTYTXPreamble_ValueChanged(object sender, EventArgs e)
        {
            if (MainForm.rtty != null)
                MainForm.rtty.TXPreamble = (int)udRTTYTXPreamble.Value;

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void udRTTYfreq_ValueChanged(object sender, EventArgs e)
        {
            MainForm.RTTYPitch = (int)udRTTYfreq.Value;

            if (MainForm.rtty != null)
            {
                MainForm.rtty.update_trx1 = true;
                MainForm.rtty.update_trx2 = true;
            }
        }

        private void chkMarkOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm.rtty != null)
                MainForm.rtty.MarkOnly = chkMarkOnly.Checked;
        }

        #endregion

        #region CW settings


        #endregion

        #region PA settings

        public float PAGain160
        {
            get { return (float)udPAGain160m.Value; }
            set { udPAGain160m.Value = (decimal)value; }
        }

        public float PAGain80
        {
            get { return (float)udPAGain80m.Value; }
            set { udPAGain80m.Value = (decimal)value; }
        }

        public float PAGain40
        {
            get { return (float)udPAGain40m.Value; }
            set { udPAGain40m.Value = (decimal)value; }
        }

        public float PAGain30
        {
            get { return (float)udPAGain30m.Value; }
            set { udPAGain30m.Value = (decimal)value; }
        }

        public float PAGain20
        {
            get { return (float)udPAGain20m.Value; }
            set { udPAGain20m.Value = (decimal)value; }
        }

        public float PAGain17
        {
            get { return (float)udPAGain17m.Value; }
            set { udPAGain17m.Value = (decimal)value; }
        }

        public float PAGain15
        {
            get { return (float)udPAGain15m.Value; }
            set { udPAGain15m.Value = (decimal)value; }
        }

        public float PAGain12
        {
            get { return (float)udPAGain12m.Value; }
            set { udPAGain12m.Value = (decimal)value; }
        }

        public float PAGain10
        {
            get { return (float)udPAGain10m.Value; }
            set { udPAGain10m.Value = (decimal)value; }
        }

        public float PAGain6
        {
            get { return (float)udPAGain6m.Value; }
            set { udPAGain6m.Value = (decimal)value; }
        }

        private void udPAGain_ValueChanged(object sender, EventArgs e)
        {
            if (!MainForm.booting)
                MainForm.Invoke(new CrossThreadCallback(MainForm.CrossThreadCommand), "UpdateOutputPower", "");
        }

        #endregion

        #region TX image save/clear

        private void bttnTXCalBand_Click(object sender, EventArgs e)
        {
            MainForm.tx_image_gain_table[(int)MainForm.CurrentBand] = (float)udTXGain.Value;
            MainForm.tx_image_phase_table[(int)MainForm.CurrentBand] = (float)udTXPhase.Value;
        }

        private void bttnTXCalAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= (int)Band.B6M; i++)
            {
                MainForm.tx_image_gain_table[i] = (float)udTXGain.Value;
                MainForm.tx_image_phase_table[i] = (float)udTXPhase.Value;
            }
        }

        private void bttnTXClearBand_Click(object sender, EventArgs e)
        {
            MainForm.tx_image_gain_table[(int)MainForm.CurrentBand] = 0.0f;
            MainForm.tx_image_phase_table[(int)MainForm.CurrentBand] = 0.0f;
            tbTXGain.Value = 0;
            tbTXPhase.Value = 0;
            udTXPhase.Value = 0;
            udTXGain.Value = 0;
        }

        private void bttnTXClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= (int)Band.B6M; i++)
            {
                MainForm.tx_image_gain_table[i] = 0.0f;
                MainForm.tx_image_phase_table[i] = 0.0f;
            }

            tbTXGain.Value = 0;
            tbTXPhase.Value = 0;
            udTXPhase.Value = 0;
            udTXGain.Value = 0;
        }

        #endregion

        #region PSK settings

        private void udPSKpitch_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.PSKPitch = (int)udPSKpitch.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udPSKPreamble_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.PSKPreamble = (int)udPSKPreamble.Value;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region G11 Filters

        private void chkG11MultiBand_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.g11_multiband = chkG11MultiBand.Checked;
            grpG11BandFilters.Enabled = !chkG11MultiBand.Checked;
        }

        private void chkG11B80_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B80_CH1.Checked)
            {
                chkG11B6040_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B80M] = true;
            }
        }

        private void chkG11B6040_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B6040_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B40M] = true;
            }
        }

        private void chkG11B4030_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B4030_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B40M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B30M] = true;
            }
        }

        private void chkG11B3020_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B3020_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B30M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B20M] = true;
            }
        }

        private void chkG11B2017_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B2017_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B20M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B17M] = true;
            }
        }

        private void chkG11B1715_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B1715_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B15M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B17M] = true;
            }
        }

        private void chkG11B201715_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B201715_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B20M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B17M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B15M] = true;
            }
        }

        private void chkG11B1210_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B1210_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B12M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B10M] = true;
            }
        }

        private void chkG11B151210_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B151210_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B15M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B12M] = true;
                MainForm.G11BandFiltersCH1[(int)Band.B10M] = true;
            }
        }

        private void chkG11B6_CH1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B6_CH1.Checked)
            {
                chkG11B80_CH1.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH1.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH1[i] = false;
                }

                MainForm.G11BandFiltersCH1[(int)Band.B6M] = true;
            }
        }

        private void chkG11B160_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B160_CH2.Checked)
            {
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B160M] = true;
            }
        }

        private void chkG11B80_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B80_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B80_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B80M] = true;
            }
        }

        private void chkG11B6040_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B6040_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B6040_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B40M] = true;
            }
        }

        private void chkG11B4030_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B4030_CH2.Checked)
            {
                chkG11B80_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH1.Checked = false;
                chkG11B160_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B40M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B30M] = true;
            }
        }

        private void chkG11B3020_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B3020_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B3020_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B30M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B20M] = true;
            }
        }

        private void chkG11B2017_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B2017_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B2017_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B20M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B17M] = true;
            }
        }

        private void chkG11B1715_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B1715_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B1715_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B17M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B15M] = true;
            }
        }

        private void chkG11B201715_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B201715_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B201715_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B20M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B17M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B15M] = true;
            }
        }

        private void chkG11B1210_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B1210_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B1210_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B12M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B10M] = true;
            }
        }

        private void chkG11B151210_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B151210_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B151210_CH1.Checked = false;
                chkG11B6_CH2.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B15M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B12M] = true;
                MainForm.G11BandFiltersCH2[(int)Band.B10M] = true;
            }
        }

        private void chkG11B6_CH2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkG11B6_CH2.Checked)
            {
                chkG11B160_CH2.Checked = false;
                chkG11B80_CH2.Checked = false;
                chkG11B3020_CH2.Checked = false;
                chkG11B2017_CH2.Checked = false;
                chkG11B1715_CH2.Checked = false;
                chkG11B6040_CH2.Checked = false;
                chkG11B1210_CH2.Checked = false;
                chkG11B201715_CH2.Checked = false;
                chkG11B151210_CH2.Checked = false;
                chkG11B6_CH1.Checked = false;
                chkG11B4030_CH2.Checked = false;

                for (int i = 1; i < 20; i++)
                {
                    MainForm.G11BandFiltersCH2[i] = false;
                }

                MainForm.G11BandFiltersCH2[(int)Band.B6M] = true;
            }
        }

        #endregion

        private void chkRobot_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.ROBOT = chkRobot.Checked;
        }

        public void chkIPV6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(MainForm.telnet_server != null)
                    MainForm.telnet_server.IPv6 = chkIPV6.Checked;

                if(MainForm.DXClusterForm.telnet_client != null)
                    MainForm.DXClusterForm.telnet_client.IPv6 = chkIPV6.Checked;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udCWDebounce_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.cwEncoder.SetKeyerDebounce((int)udCWDebounce.Value);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void udCWWeight_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.cwEncoder.SetKeyerSpeed((float)udCWWeight.Value);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #region AGC

        private void comboCWAGC_SelectedIndexChanged(object sender, EventArgs e)
        {
            AGCMode newmode = AGCMode.OFF;

            try
            {
                switch (comboCWAGC.Text)
                {
                    case "LONG":
                        newmode = AGCMode.LONG;
                        break;

                    case "SLOW":
                        newmode = AGCMode.SLOW;
                        break;

                    case "MED":
                        newmode = AGCMode.MED;
                        break;

                    case "FAST":
                        newmode = AGCMode.FAST;
                        break;

                    case "OFF":
                        newmode = AGCMode.OFF;
                        break;
                }

                MainForm.SetAGC(newmode);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Morse Runner

        private void chkSDRmode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.PWR)
                    MainForm.PWR = false;

                if (!chkSDRmode.Checked)    // SDR mode
                {
#if(DirectX)
                    DX.SDRmode = true;
#endif
                    Display_GDI.SDRmode = true;
                    Audio.SDRmode = true;
                    MainForm.grpGenesisRadio.Visible = true;
                    MainForm.grpSMeter.Visible = true;
                    MainForm.grpChannels.Visible = true;
                    MainForm.grpChannels.BringToFront();
                    MainForm.grpGenesisRadio.BringToFront();
                    MainForm.grpSMeter.BringToFront();
                    MainForm.grpMorseRunner.Visible = false;
                    MainForm.grpMorseRunner2.Visible = false;
                    MainForm.grpMorseRunner.SendToBack();
                    MainForm.grpMorseRunner2.SendToBack();
                    MainForm.lblCall.Visible = false;
                    MainForm.txtCall.Visible = false;
                    MainForm.grpMRChannels.Visible = false;
                    MainForm.grpMRChannels.SendToBack();
                    MainForm.lblUSB.Visible = true;

                    if (MainForm.genesis != null)
                    {
                        MainForm.genesis.booting = false;
                        MainForm.G59Init();
                        MainForm.genesis.si570_i2c_address = 170;
                        MainForm.genesis.si570_fxtal = 114272200.0;
                        MainForm.genesis.HSDiv = 11;
                        bool conn = MainForm.genesis.Connected;

                        if (conn)
                        {
                            MainForm.lblUSB.BackColor = Color.Green;
                        }
                        else
                        {
                            MainForm.lblUSB.BackColor = Color.Red;
                        }
                    }
                }
                else                              // MorseRunner mode
                {
#if(DirectX)
                    DX.SDRmode = false;
#endif
                    Display_GDI.SDRmode = false;
                    Audio.SDRmode = false;
                    MainForm.grpGenesisRadio.Visible = false;
                    MainForm.grpSMeter.Visible = false;
                    MainForm.grpChannels.Visible = false;
                    MainForm.grpChannels.SendToBack();
                    MainForm.grpGenesisRadio.SendToBack();
                    MainForm.grpSMeter.SendToBack();
                    MainForm.grpMRChannels.Visible = true;
                    MainForm.grpMRChannels.BringToFront();
                    MainForm.grpMorseRunner.Visible = true;
                    MainForm.grpMorseRunner2.Visible = true;
                    MainForm.grpMorseRunner.BringToFront();
                    MainForm.grpMorseRunner2.BringToFront();
                    MainForm.lblCall.Visible = true;
                    MainForm.txtCall.Visible = true;
                    MainForm.lblUSB.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void chkMRMedian_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm.cwDecoder != null)
                MainForm.cwDecoder.medijan = chkMRMedian.Checked;
        }

        private void chkMRLog_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm.cwDecoder != null)
                MainForm.cwDecoder.logmagn = chkMRLog.Checked;
        }

        #endregion
    }

    #region PADeviceInfo Helper Class

    public class PADeviceInfo
    {
        private string _Name;
        private int _Index;

        public string Name
        {
            get { return _Name; }
        }

        public int Index
        {
            get { return _Index; }
        }

        public PADeviceInfo(String argName, int argIndex)
        {
            _Name = argName;
            _Index = argIndex;
        }

        public override string ToString()
        {
            return _Name;
        }
    }

    #endregion
}
