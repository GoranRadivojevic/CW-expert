//=================================================================
// DXCC form
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections;

namespace CWExpert
{
    public partial class DXCC : Form
    {
        #region DLL import

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr window, int message, int wparam, int lparam);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        #endregion

        #region Structures

        public struct location
        {
            public double longit;
            public double latit;
        };

        public struct polar
        {
            public double distance;
            public double direction;
        };

        #endregion

        #region variable

        const int WM_VSCROLL = 0x115;
        const int SB_BOTTOM = 7;
        public string ba14 = new string(' ', 14);
        public string pfx = new string(' ', 6);
        public string sfx = new string(' ', 6);
        public string cty = new string(' ', 4);
        public string beam = new string(' ', 4);
        public string car = new string(' ', 4);
        public string mdf = new string(' ', 4);
        public string ba4 = new string(' ', 4);
        public string cont = new string(' ', 2);
        public string zon = new string(' ', 2);
        public string itu = new string(' ', 2);
        public string state = new string(' ', 2);
        public string ba2 = new string(' ', 2);
        public string bc = new string(' ', 18);
        public string ln = new string(' ', 13);
        public string pc = new string(' ', 7);
        public string ba8 = new string(' ', 8);
        public int dxp = 0;
        public int dxcnt = 0;
        public int stc = 0;
        public int execnt = 0;
        public int usacnt = 0;
        public int itucnt = 0;
        public bool wf = false;
        public bool cqww = false;
        public char stnr = ' ';
        string[] dxcl;
        string[] itul;
        string[] exebfr;
        const double PI = 3.141592653589;
        public string my_loc = "KN04BW";

        #endregion

        #region constructor/destructor

        public DXCC()
        {
            this.AutoScaleMode = AutoScaleMode.Inherit;
            InitializeComponent();
            float dpi = this.CreateGraphics().DpiX;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = 8.25f / ratio;
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;
            GetOptions();
            SetWindowPos(this.Handle.ToInt32(), -1, this.Left, this.Top,
                    this.Width, this.Height, 0);  // on top others
        }

        private void DXCC_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        #endregion

        #region Save/Restore settings

        public void SaveOptions()
        {
            try
            {
                ArrayList a = new ArrayList();

                a.Add("DXCC_top/" + this.Top.ToString());		// save form positions
                a.Add("DXCC_left/" + this.Left.ToString());

                DB.SaveVars("DXCCOptions", ref a);		        // save the values to the DB
                DB.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in DXCC SaveOptions function!\n" + ex.ToString());
            }
        }

        public void GetOptions()
        {
            try
            {
                ArrayList a = DB.GetVars("DXCCOptions");
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

                    if (s.StartsWith("DXCC_top"))
                    {
                        int top = Int32.Parse(vals[1]);
                        this.Top = top;
                    }
                    else if (s.StartsWith("DXCC_left"))
                    {
                        int left = Int32.Parse(vals[1]);
                        this.Left = left;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        public bool Sttfnd(string ulaz)
        {
            string s = "";
            string s2;
            bool f = false;
            int i = 0;

            do
            {
                //                s = wvest[i];
                s2 = s.Substring(0, 2);
                string s4 = s.Substring(2, 4);
                //             if (Pos(' ',s4)>2)
                //                s4=Copy(s4,1,Pos(' ',s4)-1);
                if ((ulaz.Equals(s2)) || (ulaz.StartsWith(s4)) || (s4.StartsWith(ulaz)))
                    f = true;
                else
                    i++;
            }
            while (!f & (i <= stc));

            if (f)
            {
                state = s2;
                stnr = s[7];
                if (!char.IsDigit(stnr))
                    stnr = s[9];
            }
            return f;
        }


        public string Pr4(string ct, string pfx, string sfx)
        {
            cty = ct + pfx[pfx.Length - 1] + sfx[0];
            return cty;
        }


        public string Russia(string pfx, string sfx)
        {
            int m = 2;
            if (char.IsDigit(pfx[1]))
                m = 1;
            bool cqww = true;
            zon = "16";
            itu = "29";
            cty = "UA";

            if ((pfx[m] == '0') || (pfx[m] == '8') || (pfx[m] == '9'))
                cty = cty + "9";
            else if (pfx[m] == '2' & (sfx[0] == 'F' || sfx[0] == 'K'))
                cty = cty + "2";
            else if (pfx == "RI1")
            {
                if (sfx.StartsWith("AN"))
                    cty = "ANT";
                else if (sfx.StartsWith("FJ"))
                    cty = "R1FJ";
            }

            string s = sfx.Substring(0, 1);

            if (cqww)
            {
                string z19 = "CDEFGIJKLMNQRXZ";
                string z16 = "STWX";
                string z18 = "HIOPUVYZ";

                if (pfx[m] == '0')
                {
                    if (z19.Contains(s))
                        zon = "19";
                    else if (s == "Y")
                        zon = "23";
                    else
                        zon = "18";
                }
                else if (pfx[m] == '8' || pfx[m] == '9')
                {
                    if (z16.Contains(s))
                        zon = "16";
                    else if (z18.Contains(s))
                        zon = "18";
                    else
                        zon = "17";
                }
            }
            else if (pfx[m] == '0')
            {
                string z32 = "AOPRSTWY";
                string z33 = "CDJUV";
                string z34 = "EFGLMN";

                if (z32.Contains(s))
                    itu = "32";
                else if (z33.Contains(s))
                    itu = "33";
                else if (z34.Contains(s))
                    itu = "34";
                else
                    switch (sfx[0])
                    {
                        case 'H': itu = "22"; break;
                        case 'B': itu = "22"; break;
                        case 'Q': itu = "23"; break;
                        case 'I': itu = "24"; break;
                        case 'X': itu = "25"; break;
                        case 'K': itu = "26"; break;
                        case 'Z': itu = "35"; break;
                    }
            }
            else if (pfx[m] == '1')
            {
                string z19 = "NOZ";
                if (z19.Contains(s))
                    itu = "19";
                else if (sfx[0] == 'P')
                    itu = "20";
            }
            else if (pfx[m] == '4')
            {
                string z30 = "HIW";
                if (z30.Contains(s))
                    itu = "30";
            }
            else if (pfx[m] == '8' || pfx[m] == '9')
            {
                string z31 = "HIOPUYZ";
                string z20 = "JKX";
                string z21 = "LMN";

                if (sfx[0] == 'V')
                    itu = "33";
                else if (z31.Contains(s))
                    itu = "31";
                else if (z20.Contains(s))
                    itu = "20";
                else if (z21.Contains(s))
                    itu = "21";
                else
                    itu = "30";
            }
            return cty;
        }


        public string USA(string pfx, string sfx)
        {
            cqww = true;
            zon = "05";
            state = "NY";
            cty = "K" + pfx.Substring(1);
            if (pfx[1] == 'P')
            {
                if (pfx[2] == '3')
                    cty = "KP4";
            }
            else if (pfx == "KG4" & sfx.Length == 2)
                cty = "KG4";
            else if (pfx[1] == 'H')
            {
                if (pfx[2] == '7' & sfx[0] != 'K')
                    cty = "KH6";
            }
            else if (pfx[1] == 'L')
            {
                //                if ((cty == "KL9K") &  /*  (sfx[2] IN ['A'..'H'])  &   */ (char.IsLetter(sfx[3])))
                //                    cty = "HL  ";
            }
            else
            {
                cty = "K";
                //                int i = 0;
                bool found = false;
                /*
                                while ((i < usacnt) & !found)
                                {
                                    if (usabfr[i].StartsWith(call))
                                        found = true;
                                    else
                                        i++;
                                }
                 */
                if (found)
                {
                    //                    state = usabfr[i].Substring(13, 2);
                    //                    bool f = Sttfnd(state);
                }
                else
                {
                    char stnr = pfx[pfx.Length - 1];
                    switch (stnr)
                    {
                        case '0': state = "MN"; break;
                        case '1': state = "MA"; break;
                        case '2': state = "NY"; break;
                        case '3': state = "PA"; break;
                        case '4': state = "FL"; break;
                        case '5': state = "TX"; break;
                        case '6': state = "CA"; break;
                        case '7': state = "AZ"; break;
                        case '8': state = "OH"; break;
                        case '9': state = "IL"; break;
                    }
                }

                string z5 = "1234";
                string z3 = "67";
                itu = "  ";
                //                car = cty;
                //                car.Replace(car[1], pfx[pfx.Length - 1]);
                string str = pfx.Substring(pfx.Length - 1, 1);
                if (cqww)
                {
                    if (z5.Contains(str))
                        zon = "05";
                    else if (z3.Contains(str))
                        zon = "03";
                    else
                        zon = "04";
                }
                else
                {
                    if (z5.Contains(str))
                        itu = "08";
                    else if (z3.Contains(str))
                        itu = "06";
                    else
                        itu = "07";
                }
            }
            return cty;
        }


        public string VEC(string pfx, string sfx)
        {
            itu = "09";
            zon = "04";
            cty = "VE";
            state = "ON";
            //            car = cty;
            //            car.Replace(car[2], pfx[2]);
            switch (pfx[2])
            {
                case '0': if (pfx == "CY0")
                    {
                        itu = "09";
                        zon = "05";
                        state = "SI";
                        cty = "CY0";
                    }
                    else if (pfx == "VY0")
                    {
                        itu = "03";
                        state = "NU";
                        zon = "02";
                    }
                    break;

                case '1': if (pfx[1] == 'Y')
                    {
                        itu = "02";
                        state = "YK";
                        zon = "01";
                    }
                    else
                    {
                        itu = "09";
                        zon = "05";
                        if (pfx[2] == 'O')
                            state = "NF";
                        else
                            state = "NS";
                    }
                    break;

                case '2': if (pfx[1] == 'O')
                    {
                        state = "LB";
                        zon = "02";
                        itu = "09";
                    }
                    else
                    {
                        itu = "09";
                        zon = "05";
                        if (pfx[2] == 'Y')
                            state = "PE";
                        else
                            state = "QU";
                    }
                    break;

                case '3':
                    {
                        itu = "04";
                        zon = "04";
                        state = "ON";
                    }
                    break;

                case '4':
                    {
                        itu = "03";
                        zon = "04";
                        state = "MB";
                    }
                    break;

                case '5':
                    {
                        itu = "03";
                        zon = "04";
                        state = "SK";
                    }
                    break;

                case '6':
                    {
                        itu = "02";
                        zon = "04";
                        state = "AB";
                    }
                    break;

                case '7':
                    {
                        itu = "02";
                        zon = "03";
                        state = "BC";
                    }
                    break;

                case '8':
                    {
                        itu = "02";
                        zon = "01";
                        state = "NW";
                    }
                    break;

                case '9':
                    {
                        itu = "09";
                        zon = "05";
                        if (pfx == "CY9")
                        {
                            state = "SP";
                            cty = "CY9";
                        }
                        else
                        {
                            state = "NB";
                        }
                    }
                    break;
            }
            return cty;
        }



        public string VKC(string pfx, string sfx)
        {
            cty = "VK";
            zon = "30";
            itu = "59";
            cqww = true;
            if (pfx[2] == '0' || pfx[2] == '9')
            {
                cty = Pr4(cty, pfx, sfx);
                if (cty[3] == 'W')
                    cty = "VK9Z";
            }
            else
                switch (pfx[2])
                {
                    case '4':
                        if (!cqww)
                            itu = "55";
                        break;
                    case '6':
                        if (cqww)
                            zon = "29";
                        else
                            itu = "58";
                        break;
                    case '8':
                        if (cqww)
                            zon = "29";
                        else
                            itu = "55";
                        break;
                }
            return cty;
        }


        public string BYC(string pfx, string sfx)
        {
            zon = "24";
            cqww = true;
            cty = "BY";
            if (cqww)
            {
                string s = sfx.Substring(0, 1);
                string z233 = "GHIJKL";
                string z239 = "MNOPQRS";

                int m = 2;
                if (char.IsDigit(pfx[1]))
                    m = 1;

                switch (pfx[m])
                {
                    case '0': zon = "23"; break;
                    case '3':
                        if (z233.Contains(s))
                            zon = "23";
                        break;
                    case '9':
                        if (!(z239.Contains(s)))
                            zon = "23";
                        break;
                }
            }
            return cty;
        }


        public string CEC(string pfx, string sfx)
        {
            cty = "CE";
            string s = pfx.Substring(2, 1);
            //            string z16 = "678";
            //            if (!cqww & z16.Contains(s))
            //                zon = "16";
            //            else 
            if (pfx[2] == '0')
                cty = Pr4(cty, pfx, sfx);
            else if (pfx[2] == '9')
                cty = "ANT";
            return cty;
        }

        public string EAC(string pfx, string sfx)
        {
            cty = "EA";
            string s = pfx.Substring(2, 1);
            string ea = "689";
            if (ea.Contains(s))
                cty = Pr4(cty, pfx, sfx);
            return cty;
        }


        public string Itus(string pfx, string sfx, string[] itul)
        {
            cqww = true;
            zon = "  ";
            itu = "  ";
            string pc;
            bool hit = false;
            int j = 1;
            int i = 0;
            int k = itul.Length;
            int l = 0;

            cty = pfx.Substring(0, 2);

            do
            {
                i = (j + k) / 2;
                pc = itul[i];
                l = pfx.CompareTo(pc);
                if (l > 0)
                    j = i + 1;
                else
                    k = i - 1;
                hit = ((pfx[0] == pc[0]) & (pfx[1].CompareTo(pc[1]) >= 0) & (pfx[1].CompareTo(pc[2]) <= 0));
            }
            while (!hit & j <= k);
            if (hit)
            {
                if (pc.Length > 4)
                    cty = pc.Substring(3, 2);
                else
                    cty = pc.Substring(3, 1);

                if (pc.Length > 6)
                {
                    if (pfx[2] == pc[6] || pfx[2] == pc[5])
                        cty = Pr4(cty, pfx, sfx);
                }
                else if (pc.Length > 5)
                {
                    if (pfx[2] == pc[5] || (pc[5] == 'N'))
                        cty = Pr4(cty, pfx, sfx);
                    else if (pc[5] == 'C')
                    {
                        switch (pc[3])
                        {
                            case '4': if (pc[4] == 'U')
                                {
                                    cty = cty + "/" + sfx[0];
                                }
                                else cty = Pr4(cty, pfx, sfx);
                                break;
                            case 'B': if (pfx[1] == 'S' || pfx[1] == 'V')
                                    cty = Pr4(cty, pfx, sfx);
                                else
                                    BYC(pfx, sfx);
                                break;
                            case 'C': CEC(pfx, sfx); break;
                            case 'E': EAC(pfx, sfx); break;
                            case 'L':
                                if (sfx[0] == 'Z')
                                    cty = "ANT";
                                else if (!cqww)
                                {
                                    string s = sfx.Substring(0, 1);
                                    string z16 = "VWX";
                                    if (z16.Contains(s))
                                        itu = "16";
                                }
                                break;
                            case 'O':
                                if (pfx[2] == '0')
                                {
                                    if (pfx[1] == 'J')
                                        cty = "OJ0";
                                    else
                                        cty = "OH0";
                                }
                                break;
                            case 'P':
                                if (pfx[1] == 'J')
                                    cty = Pr4(cty, pfx, sfx);
                                else if (pfx[2] == '0')
                                    cty = Pr4(cty, pfx, sfx);
                                else if (!cqww)
                                {
                                    string s = pfx.Substring(2, 1);
                                    string z13 = "678";
                                    if (z13.Contains(s))
                                        itu = "13";
                                }
                                break;
                            case 'V':
                                if (pc[4] == 'E')
                                    VEC(pfx, sfx);
                                else if (pc[4] == 'K')
                                    VKC(pfx, sfx);
                                else if (pfx[2] == '6')
                                    cty = Pr4(cty, pfx, sfx);
                                else
                                    cty = "VR";
                                break;
                        }
                    }
                }
            }
            return cty;
        }


        public int DXpos(string wntd)
        {
            cqww = true;
            zon = "  ";
            itu = "  ";
            int i = 1;
            int j = dxcl.Length;
            int k = 0;
            int l = 0;
            int dxpos = 0;
            cty = "    ";
            string[] vals;

            do
            {
                k = (i + j) / 2;
                vals = dxcl[k].Split('|');
                cty = vals[0];
                l = cty.IndexOf(' ');

                if (l > 0)
                    cty = cty.Substring(0, l);
                l = wntd.CompareTo(cty);

                if (l > 0)
                    i = k + 1;
                else
                    j = k - 1;
            }
            while ((cty != wntd) & (i <= j));

            if (wntd != cty)
                dxpos = j;
            else
                dxpos = k;

            ln = dxcl[dxpos];
            vals = dxcl[dxpos].Split('|');
            cty = vals[0];

            if (zon == "  ")
            {
                zon = vals[2];
                itu = vals[3];
            }

            return dxpos;
        }


        public string DXsts(string pfx, string sfx, string[] itul)
        {
            //            int i = 0;
            zon = "  ";
            cont = "  ";
            state = "  ";
            cty = pfx.Substring(0, 2);
            bool found = false;
            /*
                        if (execnt > 0)
                        {
                            do
                            {
                                if (exebfr[i].StartsWith(call))
                                    found = true;
                                else
                                    i++;
                            }
                            while (!found & i < execnt);

                            if (found)
                                cty = exebfr[i].Substring(11, 4) + ba2;
                        }
            */
            if (!found)
            {
                string uk = "DIJMUW";
                string altg = "TNHSPC";
                string usati = "ABCDEFGHIJKL";
                string taiwan = "SV";
                string nemci = "ABCDEFGHIJKLMNOPQR";
                string spanci = "ABCDEFGH";

                string s = cty.Substring(1, 1);

                switch (cty[0])
                {
                    case '2':
                        {
                            if (altg.Contains(s))
                                cty = "G" + s;
                            else if (uk.Contains(s))
                                cty = "G" + s;
                            else
                                cty = "G";
                        }
                        break;

                    case 'A':
                        if (usati.Contains(s))
                            cty = USA(pfx, sfx);
                        else
                            cty = Itus(pfx, sfx, itul);
                        break;
                    case 'B':
                        if (taiwan.Contains(s))
                            cty = Pr4(cty, pfx, sfx);
                        else
                            cty = BYC(pfx, sfx);
                        break;
                    case 'D':
                        if (nemci.Contains(s))
                            cty = "DL";
                        else
                            cty = Itus(pfx, sfx, itul);
                        break;
                    case 'E':
                        if (spanci.Contains(s))
                            cty = EAC(pfx, sfx);
                        else
                            cty = Itus(pfx, sfx, itul);
                        break;
                    case 'F':
                        {
                            string tro = "TRO";
                            string franki = "GHJKMPSWY";

                            if (tro.Contains(s))
                            {
                                cty = cty + "/" + sfx[0];
                                if (cty == "FT/Y")
                                    cty = "ANT";
                            }
                            else if (!franki.Contains(s))
                                cty = "F";
                        }
                        break;
                    case 'G':
                        {
                            if (uk.Contains(s))
                                cty = "G" + s;
                            else
                                cty = "G";
                        }
                        break;
                    case 'M':
                        {
                            if (altg.Contains(s))
                                cty = "G" + s;
                            else if (uk.Contains(s))
                                cty = "G" + s;
                            else
                                cty = "G";
                        }
                        break;
                    case 'I':
                        {
                            if (pfx.Length < 3)
                                cty = "I";
                            else if (pfx[2] == '0')
                            {
                                if (pfx[1] == 'M' || pfx[1] == 'S')
                                    cty = "IS";
                                else
                                    cty = "I";
                            }
                            else if (pfx[2] == '9')
                            {
                                if (pfx[1] == 'G' || pfx[1] == 'H')
                                    cty = "IG";
                                else
                                    cty = "IT";
                            }
                            else
                                cty = "I";
                        }
                        break;
                    case 'J':
                        {
                            string japs = "ABCDEFGHIJKLMNOPQRS";
                            if (japs.Contains(s))
                                if (pfx != "JD1")
                                    cty = "JA";
                                else
                                    cty = Itus(pfx, sfx, itul);
                        }
                        break;

                    case 'K': cty = USA(pfx, sfx); break;
                    case 'N': cty = USA(pfx, sfx); break;
                    case 'R': cty = Russia(pfx, sfx); break;
                    case 'U':
                        {
                            string j2m = "JKLM";
                            string rusi = "ABCDEFGHI";
                            if (char.IsDigit(pfx[1]))
                                cty = Russia(pfx, sfx);
                            else if (rusi.Contains(s))
                                cty = Russia(pfx, sfx);
                            else if (j2m.Contains(s))
                                cty = "UK";
                            else
                                cty = Itus(pfx, sfx, itul);
                        }
                        break;

                    case 'W': cty = USA(pfx, sfx); break;

                    default: cty = Itus(pfx, sfx, itul); break;
                }
                /*
                                if (cty.Equals(old))
                 //                   Itus();
                 //               dxp = DXpos(cty);

                                if (!wf)
                                {
                                    i = 3;
                                    while (!char.IsDigit(pfx[i])) { i--; }
                                    car = cty;
                                    int j = 1;
                                    while (char.IsLetter(car[j]) & j < 3) { j++; };
                                    car.Replace(car[j], pfx[i]);
                                }
                 */
            }
            return cty;
        }

        public string[] DXCCload()
        {
            int dxcnt = 0;
            string line = "";
            string[] dxcl = new string[1];
            char[] buff = new char[20];

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath + "\\DXCC\\DXCC.txt");

                if (file != null && file.BaseStream.Length > 0)
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        dxcnt++;
                    }

                    dxcl = new string[dxcnt];
                    dxcnt = 0;
                    file.BaseStream.Position = 0;

                    while ((line = file.ReadLine()) != null)
                    {
                        dxcl[dxcnt] = line;
                        dxcnt++;
                    }
                }

                file.Close();

                return dxcl;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return dxcl;
            }
        }

        public string[] ITUload()
        {
            int itucnt = 0;
            string line = "";
            string[] itul = new string[150];

            System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath + "\\DXCC\\ITU.txt");

            while ((line = file.ReadLine()) != null)
            {
                itul[itucnt] = line;
                itucnt++;
            }

            file.Close();
            Debug.Write("ITU " + itucnt.ToString());

            return itul;
        }

        public void EXCload()
        {
            int execnt = 0;
            string line = "";
            exebfr = new string[20];

            System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath + "\\DXCC\\Except.txt");

            while ((line = file.ReadLine()) != null)
            {
                exebfr[execnt] = line;
                execnt++;
            }

            file.Close();
        }

        public void USAload()
        {
            int usacnt = 0;
            string line = "";
            string[] usabfr = new string[50];

            System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath + "\\DXCC\\USAexe.txt");

            while ((line = file.ReadLine()) != null)
            {
                usabfr[usacnt] = line;
                usacnt++;
            }

            file.Close();
        }


        static void ARRLst()
        {
            int stc = 0;
            string line = "";
            string[] wvest = new string[64];

            System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath + "\\DXCC\\ARRLST.txt");

            while ((line = file.ReadLine()) != null)
            {
                wvest[stc] = line;
                stc++;
            }

            file.Close();
        }

        public string Prefix(string call)
        {
            try
            {
                int m = call.Length - 1;
                while (Char.IsLetter(call, m)) { m--; }
                pfx = call.Substring(0, m + 1);
                return pfx;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return "";
            }
        }

        public string Suffix(string call)
        {
            int m = call.Length - 1;
            while (Char.IsLetter(call, m)) { m--; }
            sfx = call.Substring(m + 1);
            return sfx;
        }

        public bool IsCall(string call)
        {
            try
            {
                if (call.StartsWith("Q"))
                    return false;
                else if (call.Length > 2)
                {
                    string pfx = Prefix(call);
                    int i = pfx.Length - 1;
                    while (i > 0 && char.IsDigit(call, i)) { i--; }
                    if (char.IsLetter(call, i))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public string Portable(string call)
        {
            string[] ends = { "A", "B", "M", "P", "AM", "MM", "QRP" };
            string[] parts;
            parts = call.Split('/');

            if (parts.Length > 1)
            {
                if (IsCall(parts[0]))
                {
                    string pfx = Prefix(parts[0]);

                    if (parts[1].Length == 1 && char.IsDigit(parts[1], 0))
                    {
                        pfx = pfx.Remove(pfx.Length - 1) + parts[1];
                    }
                    else
                    {
                        foreach (string kraj in ends)
                        {
                            if (parts[1].Equals(kraj))
                            {
                                call = parts[0];
                                return call;
                            }
                        }
                    }
                }

                return parts[0];
            }
            else
            {
                return call;
            }

            //return call;
        }

        public void Init(string loc)
        {
            try
            {
                my_loc = loc;
                dxcl = DXCCload();
                itul = ITUload();
                EXCload();
                USAload();
                //ARRLst();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public bool Analyze(string call, out string result)
        {
            try
            {
                bool isCall = false;
                string new_call = "";
                result = "";
                new_call = call.ToUpper();
                string inter = ":;',.<>?/!@#$%^&*()_+=\\{}]||[";
                char[] q = inter.ToCharArray();
                call = call.Trim(q);

                if (!call.StartsWith("Q"))
                {
                    if (call.Contains("/"))
                    {
                        new_call = Portable(call.ToUpper());
                    }

                    if (IsCall(new_call))
                    {
                        isCall = true;
                        pfx = Prefix(new_call);
                        sfx = Suffix(new_call);
                        cty = DXsts(pfx, sfx, itul);
                        int mm = DXpos(cty);
                        string[] vals = dxcl[mm].Split('|');
                        string text = (" " + call.ToUpper());
                        text = text.PadRight(13, ' ');
                        text += vals[0];
                        text = text.PadRight(15, ' ');
                        polar qrb = CalculateQRB(my_loc, vals[4]);
                        text += "      " + vals[1] +
                            "      " + vals[2] + "   " + vals[3] + "    " +  
                            Math.Round(qrb.direction, 0).ToString().PadLeft(3, ' ') + "  " + 
                            Math.Round(qrb.distance, 0).ToString() + "km";
                        result = text + "\n";
                    }
                    else if (call != "")
                    {
                        Debug.Write("Empty line! \n");
                    }
                }

                return isCall;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                result = "";
                return false;
            }
        }

        private void rtbDXCC_TextChanged(object sender, EventArgs e)
        {
            SendMessage(rtbDXCC.Handle, WM_VSCROLL, SB_BOTTOM, 1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                rtbDXCC.Clear();
                rtbDXCC.Refresh();
                SendMessage(rtbDXCC.Handle, WM_VSCROLL, SB_BOTTOM, 1);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #region QRB

        public polar CalculateQRB(string my_loc, string loc)
        {
            char[] from = new char[6];
            char[] to = new char[6];
            location from_spheric = new location();
            location to_spheric = new location();
            polar bearing = new polar();
            int from_status, to_status;

            try
            {
                my_loc = my_loc.ToLower();
                loc = loc.ToLower();

                from = my_loc.ToCharArray();
                to = loc.ToCharArray();

                from_status = convert(from, ref from_spheric);
                to_status = convert(to, ref to_spheric);

                if ((from_status + to_status) > 0)
                    return (bearing);

                transform(ref from_spheric, ref to_spheric, ref bearing);

                return bearing;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return bearing;
            }
        }

        private int convert(char[] loc, ref location place)
        {
            try
            {
                int i;

                for (i = 0; i <= 1; i++)
                {
                    if ((loc[i] < 'a') || (loc[i] > 'r')) return 1;
                }

                for (i = 2; i <= 3; i++)
                {
                    if ((loc[i] < '0') || (loc[i] > '9')) return 1;
                }

                for (i = 4; i <= 5; i++)
                {
                    if ((loc[i] < 'a') || (loc[i] > 'x')) return 1;
                }

                (place.latit) = 10 * (loc[1] - 'a') - 90;
                (place.latit) += loc[3] - '0';
                (place.latit) += (loc[5] - 'a') / 24.0 + 1 / 48.0;
                (place.latit) *= PI / 180.0;

                (place.longit) = 20 * (loc[0] - 'a') - 180;
                (place.longit) += 2 * (loc[2] - '0');
                (place.longit) += (loc[4] - 'a') / 12.0 + 1 / 24.0;
                (place.longit) *= PI / 180.0;

                return 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 1;
            }
        }

        private int transform(ref location from, ref location to, ref polar dd)
        {
            try
            {
                double temp, nominator, radius;

                radius = 180.0 / PI * (111.1451 + 0.56 * Math.Sin(to.latit + from.latit - PI / 2.0));

                (dd.distance) = radius * Math.Acos(temp = (Math.Sin(to.latit) * Math.Sin(from.latit)
                     + Math.Cos(to.latit) * Math.Cos(from.latit) * Math.Cos(to.longit - from.longit)));

                if (dd.distance < 0.1)
                {
                    dd.direction = 0;
                }
                else
                {
                    (dd.direction) = Math.Atan((Math.Cos(from.latit)
                                              * Math.Sin(to.longit - from.longit)
                                              * Math.Cos(to.latit))
                      / (nominator = (Math.Sin(to.latit) - temp * Math.Sin(from.latit))));
                    if (nominator < 0) dd.direction -= PI;
                    if (dd.direction < 0) dd.direction += 2 * PI;
                    dd.direction = (360.0 * dd.direction) / (2 * PI);
                }

                return 0;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return 0;
            }
        }

        #endregion
    }
}
