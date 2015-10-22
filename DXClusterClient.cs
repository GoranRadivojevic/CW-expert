using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;

namespace CWExpert
{
    public partial class DXClusterClient : Form
    {
        #region DLL imports

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr window, int message, int wparam, int lparam);

        #endregion

        #region variable

        CWExpert MainWindow;
        const int WM_VSCROLL = 0x115;
        const int SB_BOTTOM = 7;
        public TelnetClient telnet_client;
        public string CALL = "";
        public string HOST = "";
        public string NAME = "";
        public string QTH = "";
        public string CMD1 = "";
        public string CMD2 = "";
        public string CMD3 = "";
        public string CMD4 = "";
        ClusterSetup ClusterSetupForm;

        #endregion

        #region constructor

        public DXClusterClient(CWExpert ptr, string host, string call, string name, string qth)
        {
            MainWindow = ptr;
            this.AutoScaleMode = AutoScaleMode.Inherit;
            InitializeComponent();
            float dpi = this.CreateGraphics().DpiX;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = 8.25f / ratio;
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;

            telnet_client = new TelnetClient(this);
            CALL = call;
            HOST = host;
            NAME = name;
            QTH = qth;
            GetOptions();
            ClusterSetupForm = new ClusterSetup(this);

            if (comboDXCluster.Items.Count > 0)
                comboDXCluster.SelectedIndex = 0;
        }

        ~DXClusterClient()
        {
            telnet_client.Close();
        }

        #endregion

        #region button events

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (telnet_client.ClusterClient == null)
            {
                telnet_client.Start(HOST, comboDXCluster.Text.ToString(), CALL);
            }
            else if (telnet_client.ClusterClient != null && !telnet_client.ClusterClient.Connected)
                telnet_client.Start(HOST, comboDXCluster.Text.ToString(), CALL);
        }

        private void btnBye_Click(object sender, EventArgs e)
        {
            if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                telnet_client.SendMessage(0, "Bye");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (ClusterSetupForm != null && !ClusterSetupForm.IsDisposed)
                ClusterSetupForm.Show();
            else
            {
                ClusterSetupForm = new ClusterSetup(this);
                ClusterSetupForm.Show();
            }
        }

        private void btnNoDX_Click(object sender, EventArgs e)          // button 1
        {
            if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                telnet_client.SendMessage(0, CMD1);
        }

        private void btnShowDX_Click(object sender, EventArgs e)        // button 2
        {
            if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                telnet_client.SendMessage(0, CMD2);
        }

        private void btnNoVHF_Click(object sender, EventArgs e)         // button 3
        {
            if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                telnet_client.SendMessage(0, CMD3);
        }

        private void btnVHFandUP_Click(object sender, EventArgs e)      // button 4
        {
            if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                telnet_client.SendMessage(0, CMD4);
        }

        private void btnClearTxt_Click(object sender, EventArgs e)
        {
            rtbDXClusterText.Clear();
            SendMessage(rtbDXClusterText.Handle, WM_VSCROLL, SB_BOTTOM, 0);
        }

        #endregion

        #region CrossThread

        public void CrossThreadCommand(int command, byte[] data, int count)
        {
            try
            {
                switch (command)
                {
                    case 0:
                        {                                                   // regular text
                            ASCIIEncoding buffer = new ASCIIEncoding();
                            string text = buffer.GetString(data, 0, count);
                            text = text.Replace('\a', ' ');
                            text = text.Replace('\r', ' ');
                            string[] txt = text.Split('\n');
                            rtbDXClusterText.AppendText(text);
                            SendMessage(rtbDXClusterText.Handle, WM_VSCROLL, SB_BOTTOM, 0);

                            foreach (string q in txt)
                            {
                                if (q.StartsWith("login:"))
                                {
                                    telnet_client.SendMessage(0, CALL);

                                    if (NAME != "")
                                        telnet_client.SendMessage(0, "set/name " + NAME + " ");

                                    if (QTH != "")
                                        telnet_client.SendMessage(0, "set/QTH " + QTH + " ");
                                }
                                else if (q.Contains("enter your call"))
                                {
                                    telnet_client.SendMessage(0, CALL);
                                    telnet_client.SendMessage(0, "set/station/name " + NAME);
                                    telnet_client.SendMessage(0, "set/station/qth " + QTH);
                                }
                            }
                        }
                        break;

                    case 1:
                        {                                                   // screen caption
                            ASCIIEncoding buffer = new ASCIIEncoding();
                            string text = buffer.GetString(data, 0, count);
                            this.Text = text;
                        }
                        break;
                }
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

                int items = comboDXCluster.Items.Count;

                for(int i=0; i<items; i++)
                {
                    comboDXCluster.SelectedIndex = i;

                    if (i.ToString() != "")
                        a.Add("Cluster" + i.ToString() + "/" + (string)comboDXCluster.SelectedItem);
                }

                a.Add("DXcluster_top/" + this.Top.ToString());		// save form positions
                a.Add("DXcluster_left/" + this.Left.ToString());
                a.Add("DXcluster_width/" + this.Width.ToString());
                a.Add("DXcluster_height/" + this.Height.ToString());
                a.Add("btn1text/" + btnNoDX.Text.ToString());
                a.Add("btn2text/" + btnShowDX.Text.ToString());
                a.Add("btn3text/" + btnNoVHF.Text.ToString());
                a.Add("btn4text/" + btnVHFandUP.Text.ToString());
                a.Add("btn1cmd/" + CMD1);
                a.Add("btn2cmd/" + CMD2);
                a.Add("btn3cmd/" + CMD3);
                a.Add("btn4cmd/" + CMD4);

                DB.SaveVars("DXClusterOptions", ref a);		        // save the values to the DB
                DB.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in DXCluster SaveOptions function!\n" + ex.ToString());
            }
        }

        public void GetOptions()
        {
            try
            {
                ArrayList a = DB.GetVars("DXClusterOptions");
                a.Sort();

                foreach (string s in a)
                {
                    string[] vals = s.Split('/');
                    string name = vals[0];

                    if (vals.Length > 2)
                    {
                        for (int i = 2; i < vals.Length; i++)
                            vals[1] += "/" + vals[i];
                    }

                    string val = vals[1];

                    if (s.StartsWith("DXcluster_top"))
                    {
                        int top = Int32.Parse(vals[1]);
                        this.Top = top;
                    }
                    else if (s.StartsWith("DXcluster_left"))
                    {
                        int left = Int32.Parse(vals[1]);
                        this.Left = left;
                    }
                    else if (s.StartsWith("DXcluster_width"))
                    {
                        int width = Int32.Parse(vals[1]);
                        this.Width = width;
                    }
                    else if (s.StartsWith("DXcluster_height"))
                    {
                        int height = Int32.Parse(vals[1]);
                        this.Height = height;
                    }
                    else if (s.StartsWith("Cluster"))
                    {
                        if (val != "")
                            comboDXCluster.Items.Add(val);
                    }
                    else if (s.StartsWith("btn1text"))
                    {
                        btnNoDX.Text = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.txtButton1.Text = vals[1];
                    }
                    else if (s.StartsWith("btn2text"))
                    {
                        btnShowDX.Text = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.txtButton2.Text = vals[1];
                    }
                    else if (s.StartsWith("btn3text"))
                    {
                        btnNoVHF.Text = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.txtButton3.Text = vals[1];
                    }
                    else if (s.StartsWith("btn4text"))
                    {
                        btnVHFandUP.Text = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.txtButton4.Text = vals[1];
                    }
                    else if (s.StartsWith("btn1cmd"))
                    {
                        CMD1 = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.btn1cmd.Text = vals[1];
                    }
                    else if (s.StartsWith("btn2cmd"))
                    {
                        CMD2 = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.btn2cmd.Text = vals[1];
                    }
                    else if (s.StartsWith("btn3cmd"))
                    {
                        CMD3 = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.btn3cmd.Text = vals[1];
                    }
                    else if (s.StartsWith("btn4cmd"))
                    {
                        CMD4 = vals[1];

                        if (ClusterSetupForm != null)
                            ClusterSetupForm.btn4cmd.Text = vals[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void UpdateHostsList(string[] vals)
        {
            try
            {
                comboDXCluster.Items.Clear();

                foreach (string b in vals)
                {
                    if (b != "")
                        comboDXCluster.Items.Add(b);
                }

                if (comboDXCluster.Items.Count > 0)
                {
                    comboDXCluster.SelectedIndex = 0;
                    SaveOptions();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void UpdateButtonsText()
        {
            try
            {
                btnNoDX.Text = ClusterSetupForm.txtButton1.Text;
                btnShowDX.Text = ClusterSetupForm.txtButton2.Text;
                btnNoVHF.Text = ClusterSetupForm.txtButton3.Text;
                btnVHFandUP.Text = ClusterSetupForm.txtButton4.Text;
                CMD1 = ClusterSetupForm.btn1cmd.Text;
                CMD2 = ClusterSetupForm.btn2cmd.Text;
                CMD3 = ClusterSetupForm.btn3cmd.Text;
                CMD4 = ClusterSetupForm.btn4cmd.Text;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region misc function

        private void rtbDXClusterText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.LinkText);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void rtbDXClusterText_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                double vfoa = 0.0;
                Band band = Band.B20M;

                if (double.TryParse(rtbDXClusterText.SelectedText, out vfoa))
                {
                    vfoa = Math.Round(vfoa / 1e3, 6);
                    DB.GetBandLimits(vfoa, out band);
                    MainWindow.CurrentBand = band;

                    if ((vfoa > (MainWindow.LOSC * 1e6 + Audio.SampleRate / 2) / 1e6) ||
                    (vfoa < (MainWindow.LOSC * 1e6 - Audio.SampleRate / 2) / 1e6))
                    {
                        double losc = Math.Round(vfoa - 0.01, 3);
                        MainWindow.LOSC = losc;
                    }

                    MainWindow.VFOA = Math.Round(vfoa, 6);
                    MainWindow.VFOB = Math.Round(vfoa, 6);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void txtMessage_KeyUP(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                        telnet_client.SendMessage(0, txtMessage.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void DXClusterClient_Resize(object sender, EventArgs e)
        {
            try
            {
                Point loc = new Point(12, 56);
                rtbDXClusterText.Location = loc;
                rtbDXClusterText.Width = this.Width - 40;
                rtbDXClusterText.Height = this.Height - 154;

                int space = Math.Max((rtbDXClusterText.Width - 24 - btnBye.Width * 8 - 5) / 7, 5);
                loc = btnConnect.Location;
                loc.Y = this.Height - 78;
                btnConnect.Location = loc;
                loc.X += btnBye.Width + space;
                btnBye.Location = loc;
                loc.X += btnBye.Width + space;
                btnNoDX.Location = loc;
                loc.X += btnBye.Width + space;
                btnShowDX.Location = loc;
                loc.X += btnShowDX.Width + space;
                btnNoVHF.Location = loc;
                loc.X += btnBye.Width + space;
                btnVHFandUP.Location = loc;
                loc.X += btnBye.Width + space;
                btnClearTxt.Location = loc;
                loc.X += btnBye.Width + space;
                btnSettings.Location = loc;
                space = (rtbDXClusterText.Width - (lblMessage.Width + txtMessage.Width + comboDXCluster.Width)) / 2;
                loc.X = space;
                loc.Y = 22;
                lblMessage.Location = loc;
                loc.X += 56;
                loc.Y = 19;
                txtMessage.Location = loc;
                loc.X += 280;
                loc.Y = 19;
                comboDXCluster.Location = loc;
                SendMessage(rtbDXClusterText.Handle, WM_VSCROLL, SB_BOTTOM, 0);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void SendClusterMessage(string msg)
        {
            try
            {
                if (telnet_client.ClusterClient != null && telnet_client.ClusterClient.Connected)
                    telnet_client.SendMessage(0, msg);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }
}
