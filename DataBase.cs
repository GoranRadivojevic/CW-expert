//=================================================================
// DataBase.cs
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
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace CWExpert
{
    public static class DB
    {
        public static DataSet ds;
        public static DataSet log_ds;

        private static string app_data_path = "";
        public static string AppDataPath
        {
            set { app_data_path = value; }
        }

        private static string log_file_path = Application.StartupPath + "\\LogBook.xml";
        public static string LOGFilePath
        {
            get { return log_file_path; }
            set { log_file_path = value; }
        }

        #region Private Functions

        private static void AddBandStackTable()
        {
            ds.Tables.Add("BandStack");
            DataTable t = ds.Tables["BandStack"];

            t.Columns.Add("BandName", typeof(string));
            t.Columns.Add("VFOA", typeof(double));
            t.Columns.Add("VFOB", typeof(double));
            t.Columns.Add("LOSC", typeof(double));
            t.Columns.Add("FilterVFOA", typeof(int));
            t.Columns.Add("FilterVFOB", typeof(int));
            t.Columns.Add("Zoom", typeof(int));
            t.Columns.Add("Pan", typeof(int));

            double[] data = { 1.82, 3.54, 7.04, 10.12, 14.04, 18.12, 21.04, 24.93, 28.04, 50.08 };
            string[] band = { "B160M", "B80M", "B40M", "B30M", "B20M", "B17M", "B15M", "B12M", "B10M", "B6M" };

            for (int i = 0; i < data.Length; i++)
            {
                DataRow dr = ds.Tables["BandStack"].NewRow();
                dr["BandName"] = band[i];
                dr["VFOA"] = ((double)data[i]).ToString("f6");
                dr["VFOB"] = ((double)data[i]).ToString("f6");
                dr["LOSC"] = ((double)data[i]).ToString("f6");
                dr["FilterVFOA"] = 100.ToString();
                dr["FilterVFOB"] = 100.ToString();
                dr["Zoom"] = 4.ToString();
                dr["Pan"] = 0.ToString();
                ds.Tables["BandStack"].Rows.Add(dr);
            }
        }

        private static void AddLOGTable()
        {
            try
            {                
                log_ds.Tables.Add("LOG");
                DataTable QSOTable = log_ds.Tables["LOG"];
                QSOTable.Columns.Add("No", typeof(int));
                QSOTable.Columns.Add("Date", typeof(string));
                QSOTable.Columns.Add("Time", typeof(string));
                QSOTable.Columns.Add("CALL", typeof(string));
                QSOTable.Columns.Add("RST", typeof(string));
                QSOTable.Columns.Add("SNT", typeof(string));
                QSOTable.Columns.Add("MyNR", typeof(string));
                QSOTable.Columns.Add("NR", typeof(string));
                QSOTable.Columns.Add("Zone", typeof(string));
                QSOTable.Columns.Add("Freq", typeof(string));
                QSOTable.Columns.Add("Band", typeof(string));
                QSOTable.Columns.Add("Mode", typeof(string));
                QSOTable.Columns.Add("LOC", typeof(string));
                QSOTable.Columns.Add("QTH", typeof(string));
                QSOTable.Columns.Add("Name", typeof(string));
                QSOTable.Columns.Add("Info", typeof(string));
                QSOTable.Columns.Add("Version", typeof(int));
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static bool RemoveTable(string tableName)
        {
            try
            {
                ds.Tables.Remove(tableName);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        #endregion

        #region Public Member Functions
        // ======================================================
        // Public Member Functions 
        // ======================================================

        public static void Init()
        {
            try
            {
                ds = new DataSet("Data");

                if (File.Exists(app_data_path + "\\" + "database.xml"))
                    ds.ReadXml(app_data_path + "\\" + "database.xml");
                else
                {
                    AddBandStackTable();
                }

                if (!ds.Tables.Contains("BandStack"))
                    AddBandStackTable();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static void LOG_Init()
        {
            try
            {
                log_ds = new DataSet("Data");

                if (File.Exists(log_file_path))
                    log_ds.ReadXml(log_file_path);
                else
                {
                    log_file_path = Application.StartupPath + "\\LogBook.xml";
                    AddLOGTable();
                    LOG_Update();
                }

                if (!log_ds.Tables.Contains("LOG"))
                    AddLOGTable();
                else
                {
                    DataRowCollection rows = log_ds.Tables["LOG"].Rows;
                    int no = 1;

                    foreach (DataRow dr in rows)
                    {
                        dr["No"] = no;
                        no++;
                    }

                    log_ds.WriteXml(log_file_path, XmlWriteMode.WriteSchema);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static void Update()
        {
            ds.WriteXml(app_data_path + "\\" + "database.xml", XmlWriteMode.WriteSchema);
        }

        public static void Exit()
        {
            try
            {
                Update();
                ds = null;
                LOG_Update();
                log_ds = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB exit error!" + ex.ToString());
            }
        }

        private static void AddFormTable(string name)
        {
            ds.Tables.Add(name);
            ds.Tables[name].Columns.Add("Key", typeof(string));
            ds.Tables[name].Columns.Add("Value", typeof(string));
        }

        public static void SaveVars(string tableName, ref ArrayList list)
        {
            if (!ds.Tables.Contains(tableName))
                AddFormTable(tableName);
            else
            {
                RemoveTable(tableName);
                AddFormTable(tableName);
            }

            foreach (string s in list)
            {
                string[] vals = s.Split('/');

                if (vals.Length > 2)
                {
                    for (int i = 2; i < vals.Length; i++)
                        vals[1] += "/" + vals[i];
                }

                DataRow[] rows = ds.Tables[tableName].Select("Key = '" + vals[0] + "'");
                if (rows.Length == 0)	// name is not in list
                {
                    DataRow newRow = ds.Tables[tableName].NewRow();
                    newRow[0] = vals[0];
                    newRow[1] = vals[1];
                    ds.Tables[tableName].Rows.Add(newRow);
                }
                else if (rows.Length == 1)
                {
                    rows[0][1] = vals[1];
                }
            }
        }

        public static void SaveBandStack(string band, double VFOA, double VFOB, double losc_freq, int filter_vfoA,
            int filter_vfoB, int zoom, int pan)
        {
            try
            {
                DataRow[] rows = ds.Tables["BandStack"].Select("'" + band + "' = BandName");

                foreach (DataRow datarow in rows)			// prevent duplicates
                {
                    if ((string)datarow["BandName"] == band && (double)datarow["VFOA"] == VFOA)
                    {
                        datarow["VFOB"] = VFOB;
                        datarow["LOSC"] = losc_freq;
                        datarow["FilterVFOA"] = filter_vfoA;
                        datarow["FilterVFOB"] = filter_vfoB;
                        datarow["Zoom"] = zoom;
                        datarow["Pan"] = pan;
                        return;
                    }
                }

                DataRow d = (DataRow)rows[0];
                d["VFOA"] = VFOA;
                d["VFOB"] = VFOB;
                d["LOSC"] = losc_freq;
                d["FilterVFOA"] = filter_vfoA;
                d["FilterVFOB"] = filter_vfoB;
                d["Zoom"] = zoom;
                d["Pan"] = pan;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Database Bandstack error! \n" + ex.Message);
            }
        }

        public static bool GetBandStack(string band, out double freqA, out double freqB, out double losc_freq,
            out int filter_vfoA, out int filter_vfoB, out int zoom, out int pan)
        {
            try
            {
                DataRow[] rows = ds.Tables["BandStack"].Select("'" + band + "' = BandName");

                if (rows.Length == 0)
                {
                    MessageBox.Show("No Entries found for Band: " + band, "No Entry Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    freqA = 0.0f;
                    freqB = 0.0f;
                    losc_freq = 0.0f;
                    filter_vfoA = 100;
                    filter_vfoB = 100;
                    zoom = 4;
                    pan = 0;
                    return false;
                }

                freqA = (double)((DataRow)rows[0])["VFOA"];
                freqB = (double)((DataRow)rows[0])["VFOB"];
                losc_freq = (double)((DataRow)rows[0])["LOSC"];
                filter_vfoA = (int)((DataRow)rows[0])["FilterVFOA"];
                filter_vfoB = (int)((DataRow)rows[0])["FilterVFOB"];
                zoom = (int)((DataRow)rows[0])["Zoom"];
                pan = (int)((DataRow)rows[0])["Pan"];

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                freqA = 14.0f;
                freqB = 14.0f;
                losc_freq = 14.0f;
                filter_vfoA = 100;
                filter_vfoB = 100;
                zoom = 4;
                pan = 0;
                return false;
            }
        }

        public static ArrayList GetVars(string tableName)
        {
            ArrayList list = new ArrayList();
            if (!ds.Tables.Contains(tableName))
                return list;

            DataTable t = ds.Tables[tableName];

            for (int i = 0; i < t.Rows.Count; i++)
            {
                list.Add(t.Rows[i][0].ToString() + "/" + t.Rows[i][1].ToString());
            }

            return list;
        }

        #region QSO LOG

        public static ArrayList SearchLOG(string CALL)
        {
            try
            {
                ArrayList list = new ArrayList();

                DataRow[] rows = log_ds.Tables["LOG"].Select("'" + CALL + "' =CALL");

                foreach (DataRow dr in rows)
                {
                    list.Add(dr[0].ToString() + "\\" + dr[1].ToString() + "\\" + dr[2].ToString()
                        + "\\" + dr[3].ToString() + "\\" + dr[4].ToString() + "\\" + dr[5].ToString()
                        + "\\" + dr[6].ToString() + "\\" + dr[7].ToString() + "\\" + dr[8].ToString()
                        + "\\" + dr[9].ToString() + "\\" + dr[10].ToString() + "\\" + dr[11].ToString()
                        + "\\" + dr[12].ToString() + "\\" + dr[13].ToString());
                }

                return list;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return null;
            }
        }

        public static bool ImportLOG(string filename)
        {
            if (!File.Exists(filename)) return false;

            DataSet file = new DataSet();

            try
            {
                file.ReadXml(filename);
            }
            catch (Exception)
            {
                return false;
            }

            DataRow[] rows = file.Tables["LOG"].Select();
            DataRow[] old_rows = log_ds.Tables["LOG"].Select();
            bool unique = false;

            try
            {
                foreach (DataRow dr in rows)
                {
                    unique = true;

                    foreach (DataRow old_dr in old_rows)
                    {
                        if (old_dr["Date"].ToString() == dr["Date"].ToString() && 
                            old_dr["Time"].ToString() == dr["Time"].ToString())
                        {
                            unique = false;
                            break;
                        }
                    }

                    try
                    {
                        if (unique)
                        {
                            log_ds.Tables["LOG"].ImportRow(dr);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Write(e.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

            LOG_Update();
            log_ds.Dispose();
            LOG_Init();

            return true;
        }

        public static bool AddQSO(string CALL, string RST, string MyRST, string Name, string QTH, string LOC,
            string Info, string date, string time, string mode, string freq, string band, string zone, int NR, int myNR)
        {
            try
            {
                CALL = CALL.Replace(" ", "");
                CALL = CALL.Replace("\n", "");
                CALL = CALL.Replace("\r", "");
                RST = RST.Replace(" ", "");
                RST = RST.Replace("\n", "");
                RST = RST.Replace("\r", "");
                MyRST = MyRST.Replace(" ", "");
                MyRST = MyRST.Replace("\n", "");
                MyRST = MyRST.Replace("\r", "");
                LOC = LOC.Replace(" ", "");
                LOC = LOC.Replace("\n", "");
                LOC = LOC.Replace("\r", "");
                Info = Info.Replace("\n", "");
                Info = Info.Replace("\r", "");
                QTH = QTH.Replace("\n", "");
                QTH = QTH.Replace("\r", "");
                DataRow dr = log_ds.Tables["LOG"].NewRow();
                dr["CALL"] = CALL.ToUpper();
                dr["RST"] = RST.ToUpper();
                dr["SNT"] = MyRST.ToUpper();
                dr["Name"] = Name;
                dr["QTH"] = QTH;
                dr["LOC"] = LOC.ToUpper();
                dr["Info"] = Info;
                dr["Date"] = date;
                dr["Time"] = time;
                dr["Mode"] = mode;
                dr["Freq"] = freq;
                dr["Band"] = band;
                dr["Zone"] = zone;
                dr["No"] = log_ds.Tables["LOG"].Rows.Count + 1;

                if (NR > 0)
                    dr["NR"] = NR.ToString();

                if (myNR > 0)
                    dr["MyNR"] = myNR.ToString();

                log_ds.Tables["LOG"].Rows.Add(dr);
                LOG_Update();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public static void LOG_Update()
        {
            log_ds.WriteXml(log_file_path, XmlWriteMode.WriteSchema);
        }

        public static void ExportLOG(string file )
        {
            try
            {
                log_ds.WriteXml(file, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static bool CreateLOG(string file)
        {
            try
            {
                if (log_ds != null)
                {
                    LOG_Update();
                    log_ds.Dispose();
                }

                log_ds = new DataSet("Data");
                LOGFilePath = file;
                AddLOGTable();
                LOG_Update();
                log_ds.Dispose();
                LOG_Init();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public static bool OpenLOG(string file)
        {
            try
            {
                if (log_ds != null)
                {
                    LOG_Update();
                    log_ds.Dispose();
                }

                LOGFilePath = file;
                LOG_Init();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        #endregion

        #endregion
    }
}
