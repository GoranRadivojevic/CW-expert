//=================================================================
// Recorder/Player.cs
//=================================================================
// Copyright (C) 2012 S56A YT7PWR
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
using System.Drawing.Imaging;


namespace CWExpert
{
    public partial class Recorder : Form
    {
        #region DLL imports

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        #endregion

        #region variable

        CWExpert MainForm;
        private Button btnSaveToFile;
        private Button btnRecord;
        private Button btnSettings;
        private Button btnRecStop;
        RecorderSetup RecorderSetupForm;
        public int buffer_size = 1024;
        private GroupBox grpPlayer;
        private Label label1;
        private ProgressBar progressRecord;
        private Button btnPlayBack100;
        private Button btnPlayStop;
        private Button btnPlayFwd10;
        private Button btnPlayBack10;
        private Button btnPlayFwd100;
        private TrackBar tbPlayback;
        private Button btnPlay;
        private Button btnOpenFile;
        private OpenFileDialog openFileDialog1;
        private Label label2;
        BinaryReader reader;
        private GroupBox groupBox2;
        BinaryWriter writer;
        //bool playing = false;

        #endregion

        #region windows generate code

        private void InitializeComponent()
        {
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnRecStop = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.grpPlayer = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPlayStop = new System.Windows.Forms.Button();
            this.btnPlayFwd10 = new System.Windows.Forms.Button();
            this.btnPlayBack10 = new System.Windows.Forms.Button();
            this.btnPlayFwd100 = new System.Windows.Forms.Button();
            this.tbPlayback = new System.Windows.Forms.TrackBar();
            this.btnPlayBack100 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressRecord = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayback)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveToFile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveToFile.ForeColor = System.Drawing.Color.Black;
            this.btnSaveToFile.Location = new System.Drawing.Point(193, 81);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToFile.TabIndex = 2;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRecord.ForeColor = System.Drawing.Color.Black;
            this.btnRecord.Location = new System.Drawing.Point(26, 81);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(63, 23);
            this.btnRecord.TabIndex = 3;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnRecStop
            // 
            this.btnRecStop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRecStop.ForeColor = System.Drawing.Color.Black;
            this.btnRecStop.Location = new System.Drawing.Point(113, 81);
            this.btnRecStop.Name = "btnRecStop";
            this.btnRecStop.Size = new System.Drawing.Size(56, 23);
            this.btnRecStop.TabIndex = 4;
            this.btnRecStop.Text = "Stop";
            this.btnRecStop.UseVisualStyleBackColor = true;
            this.btnRecStop.Click += new System.EventHandler(this.btnRecStop_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.AutoSize = true;
            this.btnSettings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSettings.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSettings.ForeColor = System.Drawing.Color.Black;
            this.btnSettings.Location = new System.Drawing.Point(292, 81);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(56, 23);
            this.btnSettings.TabIndex = 13;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // grpPlayer
            // 
            this.grpPlayer.Controls.Add(this.label2);
            this.grpPlayer.Controls.Add(this.btnOpenFile);
            this.grpPlayer.Controls.Add(this.btnPlay);
            this.grpPlayer.Controls.Add(this.btnPlayStop);
            this.grpPlayer.Controls.Add(this.btnPlayFwd10);
            this.grpPlayer.Controls.Add(this.btnPlayBack10);
            this.grpPlayer.Controls.Add(this.btnPlayFwd100);
            this.grpPlayer.Controls.Add(this.tbPlayback);
            this.grpPlayer.Controls.Add(this.btnPlayBack100);
            this.grpPlayer.ForeColor = System.Drawing.Color.White;
            this.grpPlayer.Location = new System.Drawing.Point(10, 2);
            this.grpPlayer.Name = "grpPlayer";
            this.grpPlayer.Size = new System.Drawing.Size(375, 117);
            this.grpPlayer.TabIndex = 14;
            this.grpPlayer.TabStop = false;
            this.grpPlayer.Text = "Player";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Play progress";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOpenFile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOpenFile.ForeColor = System.Drawing.Color.Black;
            this.btnOpenFile.Location = new System.Drawing.Point(208, 81);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(58, 23);
            this.btnOpenFile.TabIndex = 24;
            this.btnOpenFile.Text = "Open file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.AutoSize = true;
            this.btnPlay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.Location = new System.Drawing.Point(108, 81);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(56, 23);
            this.btnPlay.TabIndex = 22;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPlayStop
            // 
            this.btnPlayStop.AutoSize = true;
            this.btnPlayStop.ForeColor = System.Drawing.Color.Black;
            this.btnPlayStop.Location = new System.Drawing.Point(166, 81);
            this.btnPlayStop.Name = "btnPlayStop";
            this.btnPlayStop.Size = new System.Drawing.Size(40, 23);
            this.btnPlayStop.TabIndex = 21;
            this.btnPlayStop.Text = "Stop";
            this.btnPlayStop.UseVisualStyleBackColor = true;
            this.btnPlayStop.Click += new System.EventHandler(this.btnPlayStop_Click);
            // 
            // btnPlayFwd10
            // 
            this.btnPlayFwd10.AutoSize = true;
            this.btnPlayFwd10.ForeColor = System.Drawing.Color.Black;
            this.btnPlayFwd10.Location = new System.Drawing.Point(268, 81);
            this.btnPlayFwd10.Name = "btnPlayFwd10";
            this.btnPlayFwd10.Size = new System.Drawing.Size(40, 23);
            this.btnPlayFwd10.TabIndex = 20;
            this.btnPlayFwd10.Text = ">";
            this.btnPlayFwd10.UseVisualStyleBackColor = true;
            this.btnPlayFwd10.Click += new System.EventHandler(this.btnPlayFwd10_Click);
            // 
            // btnPlayBack10
            // 
            this.btnPlayBack10.AutoSize = true;
            this.btnPlayBack10.ForeColor = System.Drawing.Color.Black;
            this.btnPlayBack10.Location = new System.Drawing.Point(66, 81);
            this.btnPlayBack10.Name = "btnPlayBack10";
            this.btnPlayBack10.Size = new System.Drawing.Size(40, 23);
            this.btnPlayBack10.TabIndex = 19;
            this.btnPlayBack10.Text = "<";
            this.btnPlayBack10.UseVisualStyleBackColor = true;
            this.btnPlayBack10.Click += new System.EventHandler(this.btnPlayBack10_Click);
            // 
            // btnPlayFwd100
            // 
            this.btnPlayFwd100.AutoSize = true;
            this.btnPlayFwd100.ForeColor = System.Drawing.Color.Black;
            this.btnPlayFwd100.Location = new System.Drawing.Point(310, 81);
            this.btnPlayFwd100.Name = "btnPlayFwd100";
            this.btnPlayFwd100.Size = new System.Drawing.Size(40, 23);
            this.btnPlayFwd100.TabIndex = 18;
            this.btnPlayFwd100.Text = ">>";
            this.btnPlayFwd100.UseVisualStyleBackColor = true;
            this.btnPlayFwd100.Click += new System.EventHandler(this.btnPlayFwd100_Click);
            // 
            // tbPlayback
            // 
            this.tbPlayback.AutoSize = false;
            this.tbPlayback.Location = new System.Drawing.Point(32, 34);
            this.tbPlayback.Maximum = 100;
            this.tbPlayback.Name = "tbPlayback";
            this.tbPlayback.Size = new System.Drawing.Size(311, 34);
            this.tbPlayback.TabIndex = 17;
            this.tbPlayback.TickFrequency = 5;
            this.tbPlayback.Scroll += new System.EventHandler(this.tbPlayback_Scroll);
            // 
            // btnPlayBack100
            // 
            this.btnPlayBack100.AutoSize = true;
            this.btnPlayBack100.ForeColor = System.Drawing.Color.Black;
            this.btnPlayBack100.Location = new System.Drawing.Point(24, 81);
            this.btnPlayBack100.Name = "btnPlayBack100";
            this.btnPlayBack100.Size = new System.Drawing.Size(40, 23);
            this.btnPlayBack100.TabIndex = 16;
            this.btnPlayBack100.Text = "<<";
            this.btnPlayBack100.UseVisualStyleBackColor = true;
            this.btnPlayBack100.Click += new System.EventHandler(this.btnPlayBack100_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Rec. progress";
            // 
            // progressRecord
            // 
            this.progressRecord.ForeColor = System.Drawing.Color.LimeGreen;
            this.progressRecord.Location = new System.Drawing.Point(153, 36);
            this.progressRecord.Name = "progressRecord";
            this.progressRecord.Size = new System.Drawing.Size(163, 18);
            this.progressRecord.TabIndex = 14;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"WAV file|*.wav|All files|*.*\"";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressRecord);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSaveToFile);
            this.groupBox2.Controls.Add(this.btnSettings);
            this.groupBox2.Controls.Add(this.btnRecStop);
            this.groupBox2.Controls.Add(this.btnRecord);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(10, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 117);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recorder";
            // 
            // Recorder
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.btnSaveToFile;
            this.ClientSize = new System.Drawing.Size(395, 250);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(405, 282);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(405, 282);
            this.Name = "Recorder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "  Recorder/Player";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Keyboard_Closing);
            this.grpPlayer.ResumeLayout(false);
            this.grpPlayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPlayback)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region Properties

        private bool recording = false;
        public bool Recording
        {
            set
            {
                recording = value;

                if (!value)
                    btnRecord.BackColor = Color.WhiteSmoke;
            }
        }

        #endregion

        #region constructor/destructor

        public Recorder(CWExpert form)
        {
            try
            {
                this.AutoScaleMode = AutoScaleMode.Inherit;
                InitializeComponent();
                float dpi = this.CreateGraphics().DpiX;
                float ratio = dpi / 96.0f;
                string font_name = this.Font.Name;
                float size = 8.25f / ratio;
                System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
                this.Font = new_font;
                MainForm = form;
                GetOptions();
                SetWindowPos(this.Handle.ToInt32(), -1, this.Left, this.Top,
                        this.Width, this.Height, 0);  // on top others
                RecorderSetupForm = new RecorderSetup(this);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        ~Recorder()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void Keyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Start/Stop

        public void Start()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void Stop()
        {
            try
            {
                Audio.rb_mon_l.ResetReadPtr();
                Audio.rb_mon_r.ResetReadPtr();
                btnPlay.BackColor = Color.WhiteSmoke;
                btnPlay.Text = "Play";
                tbPlayback.Value = 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void SendMessage(string msg)
        {
            try
            {
                btnRecStop_Click(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Save/Restore settings

        public void SaveOptions()
        {
            try
            {
                ArrayList a = new ArrayList();

                a.Add("Recorder_top/" + this.Top.ToString());		// save form positions
                a.Add("Recorder_left/" + this.Left.ToString());
                a.Add("BufferSize/" + buffer_size.ToString());

                DB.SaveVars("RecorderOptions", ref a);		        // save the values to the DB
                DB.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Recorder SaveOptions function!\n" + ex.ToString());
            }
        }

        public void GetOptions()
        {
            try
            {
                ArrayList a = DB.GetVars("RecorderOptions");
                a.Sort();

                foreach (string s in a)
                {
                    string[] vals = s.Split('/');
                    string name = vals[0];
                    string val = vals[1];

                    if (s.StartsWith("Recorder_top"))
                    {
                        int top = Int32.Parse(vals[1]);
                        this.Top = top;
                    }
                    else if (s.StartsWith("Recorder_left"))
                    {
                        int left = Int32.Parse(vals[1]);
                        this.Left = left;
                    }
                    else if (s.StartsWith("BufferSize"))
                    {
                        buffer_size = int.Parse(vals[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region buttons event

        unsafe private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                float[] left = new float[2048];
                float[] right = new float[2048];
                byte[] data = new byte[buffer_size * 1024 * 2 * 4];
                byte[] temp = new byte[4];
                int wptr = 0;

                System.DateTime date = DateTime.UtcNow;
                string datum = date.Date.ToShortDateString();
                datum = datum.Replace("/", "");
                string vreme = date.ToUniversalTime().ToShortTimeString();
                vreme = vreme.Replace(":", "");
                vreme = vreme.PadLeft(6, '0');
                string FileName = Application.StartupPath + "\\Recordings\\" + MainForm.LOSC.ToString("f3") + "MHz_" +
                    Audio.SampleRate.ToString() + "_" + datum + "_" + vreme + ".wav";

                writer = new BinaryWriter(File.Open(FileName, FileMode.Create));

                Audio.rb_mon_l.ResetReadPtr();
                Audio.rb_mon_r.ResetReadPtr();

                fixed (float* input_l = &left[0])
                fixed (float* input_r = &right[0])
                {
                    while (Audio.rb_mon_l.ReadSpace() >= 2048)
                    {
                        Audio.rb_mon_l.ReadPtr(input_r, 2048);
                        Audio.rb_mon_r.ReadPtr(input_l, 2048);

                        for (int i = 0; i < 2048; i++)
                        {
                            temp = BitConverter.GetBytes(input_l[i]);

                            for (int j = 0; j < 4; j++)
                                data[wptr + j] = temp[j];

                            wptr += 4;
                            temp = BitConverter.GetBytes(input_r[i]);

                            for (int j = 0; j < 4; j++)
                                data[wptr + j] = temp[j];

                            wptr += 4;
                        }
                    }
                }

                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(0x46464952);								// "RIFF"		-- descriptor chunk ID
                writer.Write(data.Length + 36);							// size of whole file
                writer.Write(0x45564157);								// "WAVE"		-- descriptor type
                writer.Write(0x20746d66);								// "fmt "		-- format chunk ID
                writer.Write((int)16);									// size of fmt chunk
                writer.Write((short)3);									// FormatTag	-- 3 for floats
                writer.Write((short)2);    								// wChannels
                writer.Write((int)Audio.SampleRate);					// dwSamplesPerSec
                writer.Write((int)(2 * Audio.SampleRate * 32 / 8));	    // dwAvgBytesPerSec
                writer.Write((short)(2 * 32 / 8));          			// wBlockAlign
                writer.Write((short)32);       							// wBitsPerSample
                writer.Write(0x61746164);								// "data" -- data chunk ID
                writer.Write(data.Length);								// chunkSize = length of data

                writer.Write(data, 0, wptr - 4);
                writer.Flush();
                writer.Close();
                writer = null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());

                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnRecord.BackColor == Color.WhiteSmoke)
                {
                    Audio.loopback = false;
                    Thread.Sleep(100);
                    btnPlay.BackColor = Color.WhiteSmoke;
                    btnPlay.Text = "Play";
                    Audio.rb_mon_l.Reset();
                    Audio.rb_mon_r.Reset();
                    Audio.record = true;
                    btnRecord.BackColor = Color.Red;
                    progressRecord.Value = 0;
                }
                else
                {
                    Audio.record = false;
                    btnPlay.BackColor = Color.WhiteSmoke;
                    btnPlay.Text = "Play";
                    btnRecord.BackColor = Color.WhiteSmoke;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnRecStop_Click(object sender, EventArgs e)
        {
            try
            {
                Audio.record = false;
                btnRecord.BackColor = Color.WhiteSmoke;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (RecorderSetupForm == null || RecorderSetupForm.IsDisposed)
                RecorderSetupForm = new RecorderSetup(this);

            RecorderSetupForm.Show();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnPlay.BackColor == Color.WhiteSmoke)
                {
                    Audio.record = false;
                    btnPlay.BackColor = Color.WhiteSmoke;
                    Audio.rb_mon_l.ResetReadPtr();
                    Audio.rb_mon_r.ResetReadPtr();
                    Audio.loopback = true;
                    btnPlay.BackColor = Color.Green;
                    btnPlay.Text = "Pause";
                    progressRecord.Value = 0;
                    //playing = true;
                }
                else
                {
                    if (btnPlay.Text == "Pause")
                    {
                        Audio.loopback = false;
                        btnPlay.Text = "Resume";
                    }
                    else
                    {
                        Audio.loopback = true;
                        btnPlay.Text = "Pause";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            try
            {
                Audio.loopback = false;
                Audio.rb_mon_l.ResetReadPtr();
                Audio.rb_mon_r.ResetReadPtr();
                btnPlay.BackColor = Color.WhiteSmoke;
                btnPlay.Text = "Play";
                tbPlayback.Value = 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnPlayBack100_Click(object sender, EventArgs e)
        {
            try
            {
                if (Audio.rb_mon_l.wptr > 0)
                {
                    Audio.rb_mon_l.rptr = Math.Max(0, Audio.rb_mon_l.rptr -
                        (int)(2 * (Audio.rb_mon_l.wptr / 10)));
                    Audio.rb_mon_r.rptr = Audio.rb_mon_l.rptr;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnPlayBack10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Audio.rb_mon_l.wptr > 0)
                {
                    Audio.rb_mon_l.rptr = Math.Max(0, Audio.rb_mon_l.rptr -
                        (int)(Audio.rb_mon_l.wptr / 10));
                    Audio.rb_mon_r.rptr = Audio.rb_mon_l.rptr;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnPlayFwd10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Audio.rb_mon_l.wptr > 0)
                {
                    Audio.rb_mon_l.rptr = Math.Min(Audio.rb_mon_l.wptr, Audio.rb_mon_l.rptr +
                        (int)(Audio.rb_mon_l.wptr / 10));
                    Audio.rb_mon_r.rptr = Audio.rb_mon_l.rptr;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnPlayFwd100_Click(object sender, EventArgs e)
        {
            try
            {
                if (Audio.rb_mon_l.wptr > 0)
                {
                    Audio.rb_mon_l.rptr = Math.Min(Audio.rb_mon_l.wptr, Audio.rb_mon_l.rptr +
                        (int)(2 * (Audio.rb_mon_l.wptr / 10)));
                    Audio.rb_mon_r.rptr = Audio.rb_mon_l.rptr;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void tbPlayback_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Audio.rb_mon_l.wptr > 0)
                {
                    Audio.rb_mon_l.rptr = Math.Min(Audio.rb_mon_l.wptr, (Audio.rb_mon_l.wptr * tbPlayback.Value) / 100);
                    Audio.rb_mon_r.rptr = Audio.rb_mon_l.rptr;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        unsafe private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                RIFFChunk riff = null;
                fmtChunk fmt = null;
                dataChunk data_chunk = null;

                Audio.loopback = false;

                if (Audio.rb_mon_l == null || Audio.rb_mon_r == null)
                {
                    Audio.rb_mon_l = new RingBufferFloat(Math.Max(buffer_size, 1024) * 1024);
                    Audio.rb_mon_r = new RingBufferFloat(Math.Max(buffer_size, 1024) * 1024);
                }
                else
                {
                    Audio.rb_mon_l.Reset();
                    Audio.rb_mon_r.Reset();
                }

                btnPlay.BackColor = Color.WhiteSmoke;
                btnPlay.Text = "Play";

                float[] buf_left = new float[2048];
                float[] buf_right = new float[2048];
                byte[] data = new byte[16384];
                byte[] temp = new byte[4];
                int count = 0, buflen = 0;
                string file = openFileDialog1.FileName;
                Stream file_stream = File.Open(file, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(file_stream);
                string[] vals = file.Split('\\');
                this.Text = " Recorder/Player   " + vals[vals.Length-1];

                try
                {
                    if (reader.PeekChar() != 'R')
                    {
                        reader.Close();
                        MessageBox.Show("File is not in the correct format.",
                            "Wrong File Format",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    while ((data_chunk == null ||
                        riff == null || fmt == null) &&
                        reader.PeekChar() != -1)
                    {
                        Chunk chunk = Chunk.ReadChunk(ref reader);
                        if (chunk.GetType() == typeof(RIFFChunk))
                            riff = (RIFFChunk)chunk;
                        else if (chunk.GetType() == typeof(fmtChunk))
                            fmt = (fmtChunk)chunk;
                        else if (chunk.GetType() == typeof(dataChunk))
                            data_chunk = (dataChunk)chunk;
                    }

                    if (reader.PeekChar() == -1)
                    {
                        reader.Close();
                        MessageBox.Show("File is not in the correct format.",
                            "Wrong File Format",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (riff.riff_type != 0x45564157)
                    {
                        reader.Close();
                        MessageBox.Show("File is not an RIFF Wave file.",
                            "Wrong file format",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if ((fmt.sample_rate != Audio.SampleRate) || fmt.format == 1)
                    {
                        reader.Close();
                        MessageBox.Show("File has the wrong sample rate.",
                            "Wrong Sample Rate",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (fmt.channels != 2)
                    {
                        reader.Close();
                        MessageBox.Show("Wave File is not stereo.",
                            "Wrong Number of Channels",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (reader != null)
                        reader.Close();

                    MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace.ToString(), "Fatal Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                fixed (float* input_l = &buf_left[0])
                fixed (float* input_r = &buf_right[0])
                {
                    while (Audio.rb_mon_l.WriteSpace() >= 2048)
                    {
                        count = reader.Read(data, 0, 16384);
                        buflen = Math.Min(count, 16384);

                        for (int i = 0; i < (buflen / 8); i++)
                        {
                            buf_left[i] = BitConverter.ToSingle(data, (i * 8));
                            buf_right[i] = BitConverter.ToSingle(data, (i * 8 + 4));
                        }

                        Audio.rb_mon_l.WritePtr(input_l, 2048);
                        Audio.rb_mon_r.WritePtr(input_r, 2048);

                        if (buflen < 16384)
                        {
                            reader.Close();
                            return;
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());

                if (reader != null)
                    reader.Close();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath;
                openFileDialog1.InitialDirectory = path;
                openFileDialog1.FileName = "";
                openFileDialog1.ShowReadOnly = true;
                openFileDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region cross thread

        public void CommandCallback(string action, int param_1)
        {
            try
            {
                switch (action)
                {
                    case "Set Play progress":
                        tbPlayback.Value = Math.Min(100, (param_1 * 100) / Math.Max(1, Audio.rb_mon_l.wptr));
                        break;

                    case "Set Recording progress":
                        progressRecord.Value = Math.Min(100, (param_1 * 100) / ((int)buffer_size * 1024));
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Wave File Header Helper Classes

        public class Chunk
        {
            public int chunk_id;

            public static Chunk ReadChunk(ref BinaryReader reader)
            {
                int data = reader.ReadInt32();
                if (data == 0x46464952)	// RIFF chunk
                {
                    RIFFChunk riff = new RIFFChunk();
                    riff.chunk_id = data;
                    riff.file_size = reader.ReadInt32();
                    riff.riff_type = reader.ReadInt32();
                    return riff;
                }
                else if (data == 0x20746D66)	// fmt chunk
                {
                    fmtChunk fmt = new fmtChunk();
                    fmt.chunk_id = data;
                    fmt.chunk_size = reader.ReadInt32();
                    fmt.format = reader.ReadInt16();
                    fmt.channels = reader.ReadInt16();
                    fmt.sample_rate = reader.ReadInt32();
                    fmt.bytes_per_sec = reader.ReadInt32();
                    fmt.block_align = reader.ReadInt16();
                    fmt.bits_per_sample = reader.ReadInt16();
                    return fmt;
                }
                else if (data == 0x61746164) // data chunk
                {
                    dataChunk data_chunk = new dataChunk();
                    data_chunk.chunk_id = data;
                    data_chunk.chunk_size = reader.ReadInt32();
                    return data_chunk;
                }
                else
                {
                    Chunk c = new Chunk();
                    c.chunk_id = data;
                    return c;
                }
            }
        }

        public class RIFFChunk : Chunk
        {
            public int file_size;
            public int riff_type;
        }

        public class fmtChunk : Chunk
        {
            public int chunk_size;
            public short format;
            public short channels;
            public int sample_rate;
            public int bytes_per_sec;
            public short block_align;
            public short bits_per_sample;
        }

        public class dataChunk : Chunk
        {
            public int chunk_size;
            public int[] data;
        }

        #endregion
    }
}