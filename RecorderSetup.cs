//=================================================================
// RecorderSetup
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
using System.Collections;
using System.Runtime.InteropServices;

namespace CWExpert
{
    public partial class RecorderSetup : Form
    {
        #region DLL imports

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        #endregion

        const int WM_VSCROLL = 0x115;
        const int SB_BOTTOM = 7;
        Recorder ParrentForm;

        public RecorderSetup(Recorder form)
        {
            InitializeComponent();
            float dpi = this.CreateGraphics().DpiX;
            float ratio = dpi / 96.0f;
            string font_name = this.Font.Name;
            float size = 8.25f / ratio;
            System.Drawing.Font new_font = new System.Drawing.Font(font_name, size);
            this.Font = new_font;
            ParrentForm = form;

            SetWindowPos(this.Handle.ToInt32(), -1, ParrentForm.Left, ParrentForm.Top,
                this.Width, this.Height, 0);  // on top others

            GetOptions();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ParrentForm.buffer_size = (int)udRecBufferSize.Value;
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
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

                    if (s.StartsWith("BufferSize"))
                    {
                        udRecBufferSize.Value = int.Parse(vals[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
    }
}
