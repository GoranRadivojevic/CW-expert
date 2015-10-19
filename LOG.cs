//=================================================================
// LogBook
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CWExpert
{
    public partial class LOG : Form
    {
        #region variable

        int prevWidth;
        CWExpert MainForm;
        int[] search_list = new int[100];
        int search_pointer = 0;
        int search_count = 0;
        bool open_log = false;
        bool import_log = false;
        LOG_export export_form;

        #endregion

        #region constructor/destructor

        public LOG(CWExpert c)
        {
            this.AutoScaleMode = AutoScaleMode.Inherit;
            InitializeComponent();
            float dpi = this.CreateGraphics().DpiX;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = 8.25f / ratio;
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;

            MainForm = c;
            RestoreState();
            this.Text = "LOG Book  " + DB.LOGFilePath;
        }

        #endregion

        #region LOG file manipulation

        private void LOG_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridQSOLog.RowHeadersWidth = 35;
                dataGridQSOLog.DataSource = DB.log_ds.Tables["LOG"];
                prevWidth = dataGridQSOLog.Width;
                dataGridQSOLog.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
                float dpi = this.CreateGraphics().DpiX;
                float ratio = dpi / 96.0f;
                string font_name = this.dataGridQSOLog.Font.Name;
                float size = 9.0f / ratio;
                System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
                dataGridQSOLog.DefaultCellStyle.Font = new Font(font_name, size, FontStyle.Regular);
                LOG_Resize(this, EventArgs.Empty);
                LOGStatistic();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLogImport_Click(object sender, EventArgs e)
        {
            try
            {
                import_log = true;
                string path = Application.StartupPath;
                openFileDialog1.InitialDirectory = path;
                openFileDialog1.ShowReadOnly = true;
                openFileDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public void RowAdded()
        {
            try
            {
                dataGridQSOLog.CurrentCell = dataGridQSOLog[0, Math.Max(dataGridQSOLog.Rows.Count - 2, 0)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (import_log)
                {
                    DB.ImportLOG(openFileDialog1.FileName);
                    dataGridQSOLog.DataSource = DB.log_ds.Tables["LOG"];
                    dataGridQSOLog.RowHeadersWidth = 35;
                    prevWidth = dataGridQSOLog.Width;
                    dataGridQSOLog.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
                    dataGridQSOLog.DefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Regular);
                    LOG_Resize(this, EventArgs.Empty);
                    MainForm.udLOGMyNR.Value = DB.log_ds.Tables["LOG"].Rows.Count + 1;
                    import_log = false;
                }
                else if (open_log)
                {
                    if (DB.OpenLOG(openFileDialog1.FileName))
                    {
                        dataGridQSOLog.DataBindings.Clear();
                        dataGridQSOLog.DataSource = DB.log_ds.Tables["LOG"];
                        dataGridQSOLog.RowHeadersWidth = 35;
                        prevWidth = dataGridQSOLog.Width;
                        dataGridQSOLog.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
                        dataGridQSOLog.DefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Regular);
                        LOG_Resize(this, EventArgs.Empty);
                        MainForm.udLOGMyNR.Value = DB.log_ds.Tables["LOG"].Rows.Count + 1;
                        MainForm.log_file_path = DB.LOGFilePath;
                        this.Text = "LOG Book  " + openFileDialog1.FileName;
                        open_log = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                MessageBox.Show("Failed to import LogBook! \n" + ex.ToString(), "Error!");
            }
        }

        private void btnLOGSave_Click(object sender, EventArgs e)
        {
            try
            {
                DB.LOG_Update();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                System.DateTime date = DateTime.UtcNow;
                DataRow row = DB.log_ds.Tables["LOG"].NewRow();
                row["No"] = DB.log_ds.Tables["LOG"].Rows.Count + 1;
                row["Date"] = date.Date.ToShortDateString();
                row["Time"] = date.ToUniversalTime().ToShortTimeString();

                if (MainForm.udLOGMyNR.Value > 0)
                    row["MyNR"] = MainForm.udLOGMyNR.Value;

                double freq = Math.Round(MainForm.VFOA * 1000, 1);
                string mode = MainForm.OpModeVFOA.ToString();

                if (MainForm.TXSplit)
                {
                    freq = Math.Round(MainForm.VFOB * 1000, 1);
                    mode = MainForm.OpModeVFOB.ToString();
                }

                string f = freq.ToString();
                f = f.Replace(',', '.');
                row["Freq"] = f;
                row["Mode"] = mode;
                row["Band"] = MainForm.CurrentBand.ToString().Replace("B", "");
                DB.log_ds.Tables["LOG"].Rows.Add(row);
                DB.LOG_Update();

                if (MainForm.udLOGMyNR.Value > 0)
                    MainForm.udLOGMyNR.Value ++;

                LOGStatistic();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGEraseRow_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridQSOLog.Rows.RemoveAt(dataGridQSOLog.SelectedRows[0].Index);
                DB.LOG_Update();

                if (MainForm.udLOGMyNR.Value > 0)
                    MainForm.udLOGMyNR.Value = DB.log_ds.Tables["LOG"].Rows.Count + 1;

                LOGStatistic();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGPrev_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR", Math.Max(dataGridQSOLog.CurrentRow.Index - 1, 0)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGNext_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR", Math.Min(dataGridQSOLog.CurrentRow.Index + 1, 
                    dataGridQSOLog.Rows.Count - 2)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGFirst_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridQSOLog.CurrentCell = dataGridQSOLog[0, 0];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGLast_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR", dataGridQSOLog.Rows.Count - 2];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLOGNew_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath;
                NewLOGFileDialog.InitialDirectory = path;
                NewLOGFileDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void NewLOGFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (DB.CreateLOG(NewLOGFileDialog.FileName))
                {
                    MainForm.log_file_path = DB.LOGFilePath;
                    this.Text = "LOG Book     " + NewLOGFileDialog.FileName;
                    dataGridQSOLog.DataBindings.Clear();
                    dataGridQSOLog.RowHeadersWidth = 35;
                    dataGridQSOLog.DataSource = DB.log_ds.Tables["LOG"]; ;
                    prevWidth = dataGridQSOLog.Width;
                    dataGridQSOLog.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
                    dataGridQSOLog.DefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Regular);
                    LOG_Resize(this, EventArgs.Empty);
                    MainForm.udLOGMyNR.Value = DB.log_ds.Tables["LOG"].Rows.Count + 1;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                MessageBox.Show("Failed to create new LogBook! \n" + ex.ToString(), "Error!");
            }
        }

        private void btnLogOpen_Click(object sender, EventArgs e)
        {
            try
            {
                open_log = true;
                string path = Application.StartupPath;
                openFileDialog1.InitialDirectory = path;
                openFileDialog1.ShowReadOnly = true;
                openFileDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        public void LOGStatistic()
        {
            try
            {
                if (!MainForm.booting)
                {
                    int cw = 0, rtty = 0, ssb = 0, psk31 = 0, psk63 = 0, psk125 = 0, psk250 = 0;
                    int qpsk31 = 0, qpsk63 = 0, qpsk125 = 0, qpsk250 = 0;
                    DataRow[] rows = DB.log_ds.Tables["LOG"].Select();

                    foreach (DataRow row in rows)
                    {
                        switch (row["Mode"].ToString())
                        {
                            case "CW":
                                cw++;
                                break;

                            case "RTTY":
                                rtty++;
                                break;

                            case "USB":
                            case "LSB":
                                ssb++;
                                break;

                            case "BPSK31":
                                psk31++;
                                break;

                            case "BPSK63":
                                psk63++;
                                break;

                            case "BPSK125":
                                psk125++;
                                break;

                            case "BPSK250":
                                psk250++;
                                break;

                            case "QPSK31":
                                qpsk31++;
                                break;

                            case "QPSK63":
                                qpsk63++;
                                break;

                            case "QPSK125":
                                qpsk125++;
                                break;

                            case "QPSK250":
                                qpsk250++;
                                break;
                        }

                        txtCW.Text = cw.ToString();
                        txtRTTY.Text = rtty.ToString();
                        txtSSB.Text = ssb.ToString();
                        txtPSK31.Text = psk31.ToString();
                        txtPSK63.Text = psk63.ToString();
                        txtPSK125.Text = psk125.ToString();
                        txtPSK250.Text = psk250.ToString();
                        txtQPSK31.Text = qpsk31.ToString();
                        txtQPSK63.Text = qpsk63.ToString();
                        txtQPSK125.Text = qpsk125.ToString();
                        txtQPSK250.Text = qpsk250.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region LOG search

        private void LOGSearch()
        {
            int j = 0;

            try
            {
                txtSearchCount.Text = "0";

                for (int i = 0; i < dataGridQSOLog.Rows.Count - 1; i++)
                {
                    if (dataGridQSOLog.Rows[i].Cells["CALL"].Value.ToString() == txtCALL.Text.ToUpper())
                    {
                        search_list[j] = i;
                        j++;
                    }
                }

                search_count = j;

                if (search_count > 0)
                {
                    search_pointer = search_count - 1;
                    txtSearchCount.Text = (search_pointer + 1).ToString();
                    dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR",
                        Math.Min(search_list[search_pointer], dataGridQSOLog.Rows.Count - 2)];
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnLogSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LOGSearch();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void txtCALL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LOGSearch();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnSearchPrev_Click(object sender, EventArgs e)
        {
            try
            {
                search_pointer = Math.Max(0, search_pointer - 1);
                txtSearchCount.Text = (search_pointer + 1).ToString();
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR", 
                    Math.Min(search_list[search_pointer], dataGridQSOLog.Rows.Count - 2)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnSearchNext_Click(object sender, EventArgs e)
        {
            try
            {
                search_pointer = Math.Min(search_count - 1, search_pointer + 1);
                txtSearchCount.Text = (search_pointer + 1).ToString();
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR",
                    Math.Min(search_list[search_pointer], dataGridQSOLog.Rows.Count - 2)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnSearchFirst_Click(object sender, EventArgs e)
        {
            try
            {
                search_pointer = 0;
                txtSearchCount.Text = (search_pointer + 1).ToString();
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR",
                    Math.Min(search_list[search_pointer], dataGridQSOLog.Rows.Count - 2)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private void btnSearchLast_Click(object sender, EventArgs e)
        {
            try
            {
                search_pointer = search_count - 1;
                txtSearchCount.Text = (search_pointer + 1).ToString();
                dataGridQSOLog.CurrentCell = dataGridQSOLog["MyNR",
                    Math.Min(search_list[search_pointer], dataGridQSOLog.Rows.Count - 2)];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region Save/Restore

        public void SaveState()
        {
            try
            {
                ArrayList a = new ArrayList();

                a.Add("log_top/" + this.Top.ToString());		// save form positions
                a.Add("log_left/" + this.Left.ToString());
                a.Add("log_width/" + this.Width.ToString());
                a.Add("log_height/" + this.Height.ToString());

                DB.SaveVars("LOG Options", ref a);		        // save the values to the DB
                DB.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LOG SaveState function!\n" + ex.ToString());
            }
        }

        public void RestoreState()
        {
            try
            {
                ArrayList a = DB.GetVars("LOG Options");
                a.Sort();

                // restore saved values to the controls
                foreach (string s in a)
                {
                    string[] vals = s.Split('/');
                    string name = vals[0];
                    string val = vals[1];

                    if (s.StartsWith("log_top"))
                    {
                        int top = Int32.Parse(vals[1]);
                        this.Top = top;
                    }
                    else if (s.StartsWith("log_left"))
                    {
                        int left = Int32.Parse(vals[1]);
                        this.Left = left;
                    }
                    else if (s.StartsWith("log_width"))
                    {
                        int width = Int32.Parse(vals[1]);
                        this.Width = width;
                    }
                    else if (s.StartsWith("log_height"))
                    {
                        int height = Int32.Parse(vals[1]);
                        this.Height = height;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        #region resize

        private void LOG_Resize(object sender, EventArgs e)
        {
            Point new_location = new Point(0, 0);
            Size new_size = this.Size;
            new_size.Width -= 40;
            new_size.Height -= (grpLOG.Height + 70);
            dataGridQSOLog.Size = new_size;
            grpLOG.Width = new_size.Width;
            new_location.X = 12;
            new_location.Y = (this.Height - 50 - grpLOG.Height);
            grpLOG.Location = new_location;

            if (WindowState != FormWindowState.Minimized)
            {
                ResizeGrid(dataGridQSOLog);
            }
        }

        public void ResizeGrid(DataGridView dataGrid)
        {
            int fixedWidth = SystemInformation.VerticalScrollBarWidth +
            dataGrid.RowHeadersWidth + 2;
            double mul = 100 * (dataGrid.Width - fixedWidth) /
            (prevWidth - fixedWidth);
            int columnWidth;
            int total = 0;
            DataGridViewColumn lastVisibleCol = null;

            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                if (dataGrid.Columns[i].Visible)
                {
                    columnWidth = (int)(dataGrid.Columns[i].Width * (mul / 100));

                    if (dataGrid.Columns[i].Name == "NR" || dataGrid.Columns[i].Name == "MyNR" ||
                        dataGrid.Columns[i].Name == "Zone" || dataGrid.Columns[i].Name == "No")
                        dataGrid.Columns[i].Width = 45;
                    else if (dataGrid.Columns[i].Name == "RST" || dataGrid.Columns[i].Name == "SNT")
                    {
                        dataGrid.Columns[i].Width = 35;
                    }
                    else if (dataGrid.Columns[i].Name == "Date")
                    {
                        if (dataGrid.Columns[i].Width < 80)
                            dataGrid.Columns[i].Width = 80;
                    }
                    else if (dataGrid.Columns[i].Name == "Time")
                    {
                        if (dataGrid.Columns[i].Width < 60)
                            dataGrid.Columns[i].Width = 60;
                    }
                    else if (dataGrid.Columns[i].Name == "LOC")
                    {
                        if (dataGrid.Columns[i].Width < 60)
                            dataGrid.Columns[i].Width = 60;
                    }
                    else if (dataGrid.Columns[i].Name == "CALL")
                    {
                        if (dataGrid.Columns[i].Width < 80)
                            dataGrid.Columns[i].Width = 80;
                    }
                    else if (dataGrid.Columns[i].Name == "Name")
                    {
                        if (dataGrid.Columns[i].Width < 80)
                            dataGrid.Columns[i].Width = 80;
                    }
                    else if (dataGrid.Columns[i].Name == "QTH")
                    {
                        if (dataGrid.Columns[i].Width < 100)
                            dataGrid.Columns[i].Width = 100;
                    }
                    else if (dataGrid.Columns[i].Name == "Freq")
                    {
                        if (dataGrid.Columns[i].Width < 65)
                            dataGrid.Columns[i].Width = 65;
                    }
                    else if (dataGrid.Columns[i].Name == "Band")
                    {
                        if (dataGrid.Columns[i].Width < 38)
                            dataGrid.Columns[i].Width = 38;
                    }
                    else if (dataGrid.Columns[i].Name == "Mode")
                    {
                        if (dataGrid.Columns[i].Width < 60)
                            dataGrid.Columns[i].Width = 60;
                    }
                    else if (dataGrid.Columns[i].Name == "Info")
                        dataGrid.Columns[i].Width = 5;
                    else if (dataGrid.Columns[i].Name == "Version")
                    {
                        dataGrid.Columns[i].Visible = false;
                        dataGrid.Columns[i].Width = 5;
                    }
                    else
                        dataGrid.Columns[i].Width = Math.Max(columnWidth, 30);

                    total += dataGrid.Columns[i].Width;
                }
            }

            lastVisibleCol = dataGrid.Columns["Info"];

            if (lastVisibleCol == null)
                return;
            else
                lastVisibleCol.Width = dataGridQSOLog.Width - total - dataGrid.RowHeadersWidth - 15;

            prevWidth = dataGrid.Width;
        }

        #endregion

        #region DX cluster

        private void DXClusterMenuClick_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridQSOLog.SelectedRows[0];
                DataGridViewCellCollection cells = row.Cells;
                string msg = "";
                System.DateTime date = DateTime.UtcNow;

                if (MainForm.telnet_server != null && MainForm.telnet_server.run_server)
                {
                    msg = "DX DE " + MainForm.SetupForm.txtStnCALL.Text + " " + cells["Freq"].Value.ToString() + " " +
                        cells["CALL"].Value.ToString() + " " +
                        cells["Mode"].Value.ToString();
                    string Info = (string)cells["Info"].Value;

                    if (Info.StartsWith("CQ"))
                        msg += " " + Info;

                    string time = date.ToUniversalTime().ToShortTimeString();
                    time = time.Replace(":", "");

                    if (time.Length > 4)
                        time = time.Remove(3, 2);

                    msg = msg.PadRight(64, ' ');
                    msg += " " + time + "Z";
                    MainForm.telnet_server.SendMessage(msg);
                }
                else
                {
                    msg = "DX " + cells["Freq"].Value.ToString() + " " +
                        cells["CALL"].Value.ToString() + " " +
                        cells["Mode"].Value.ToString();

                    string Info = (string)cells["Info"].Value;
                    if (Info.StartsWith("CQ"))
                        msg += " " + Info;

                    if (MainForm.DXClusterForm != null)
                    {
                        MainForm.DXClusterForm.SendClusterMessage(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

        }

        #endregion

        #region LOG export

        private void btnLogExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (export_form == null || export_form.IsDisposed)
                    export_form = new LOG_export(MainForm);

                export_form.MyCALL = MainForm.SetupForm.txtStnCALL.Text;
                export_form.MyName = MainForm.SetupForm.txtStnName.Text;
                export_form.MyQTH = MainForm.SetupForm.txtStnQTH.Text;
                export_form.MyLOC = MainForm.SetupForm.txtStnLOC.Text;
                export_form.MyInfo = MainForm.SetupForm.txtStnInfoTxt.Text;
                export_form.MyZone = MainForm.SetupForm.txtStnZone.Text;
                export_form.Show();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }
}
