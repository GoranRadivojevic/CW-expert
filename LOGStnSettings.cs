//=================================================================
// LOG Export settings
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

namespace CWExpert
{
    public partial class LOGStnSettings : Form
    {
        #region DLL imports

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        #endregion

        #region variable

        LOG_export ParrentForm;

        #endregion

        public LOGStnSettings(LOG_export form)
        {
            this.AutoScaleMode = AutoScaleMode.Inherit;
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

            txtContestName.Text = ParrentForm.ContestName;
            txtRClub.Text = ParrentForm.MyClub;
            txtOps1.Text = ParrentForm.Operators1;
            txtOps2.Text = ParrentForm.Operators2;
            txtAddr1.Text = ParrentForm.MyAddr1;
            txtAddr2.Text = ParrentForm.MyAddr2;
            txtPhone.Text = ParrentForm.MyPhone;
            txtRemarks.Text = ParrentForm.Remarks;
            txtCity.Text = ParrentForm.MyCity;
            txtCountry.Text = ParrentForm.MyCountry;
            txtCategory.Text = ParrentForm.Category;
            txtTXequ.Text = ParrentForm.TXequ;
            txtRXequ.Text = ParrentForm.RXequ;
            txtAntenna.Text = ParrentForm.Antenna;
            txtPower.Text = ParrentForm.TXPower;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ParrentForm.ContestName = txtContestName.Text.ToString();
                ParrentForm.MyClub = txtRClub.Text.ToString();
                ParrentForm.Operators1 = txtOps1.Text.ToString();
                ParrentForm.Operators2 = txtOps2.Text.ToString();
                ParrentForm.MyAddr1 = txtAddr1.Text.ToString();
                ParrentForm.MyAddr2 = txtAddr2.Text.ToString();
                ParrentForm.MyPhone = txtPhone.Text.ToString();
                ParrentForm.Remarks = txtRemarks.Text.ToString();
                ParrentForm.MyCity = txtCity.Text.ToString();
                ParrentForm.MyCountry = txtCountry.Text.ToString();
                ParrentForm.Category = txtCategory.Text.ToString();
                ParrentForm.TXequ = txtTXequ.Text.ToString();
                ParrentForm.RXequ = txtRXequ.Text.ToString();
                ParrentForm.Antenna = txtAntenna.Text.ToString();
                ParrentForm.TXPower = txtPower.Text.ToString();
                ParrentForm.SaveOptions();
                this.Hide();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
    }
}
