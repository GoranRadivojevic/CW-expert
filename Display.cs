//=================================================================
// Display.cs
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
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;

namespace CWExpert
{
    #region structures

    struct Vertex
    {
        public Vector4 Position;
        public int Color;
    }

    struct DXRectangle
    {
        public int x1;
        public int x2;
        public int x3;
        public int x4;
        public int y1;
        public int y2;
        public int y3;
        public int y4;
    }

    struct VerticalString
    {
        public int pos_x;
        public int pos_y;
        public string label;
        public Color color;
    }

    struct HorizontalString
    {
        public int pos_x;
        public int pos_y;
        public string label;
        public Color color;
    }

    #endregion

#if(DirectX)

#region DirectX

    static class DX
    {
        #region Variable Declaration

        public static CWExpert MainForm;
        public const float CLEAR_FLAG = -999.999F;				// for resetting buffers
        public const int BUFFER_SIZE = 4096;
        public static float[] new_display_data;					// Buffer used to store the new data from the DSP for the display
        public static float[] new_waterfall_data;    			// Buffer used to store the new data from the DSP for the waterfall
        public static float[] current_display_data;				// Buffer used to store the current data for the display
        public static float[] current_waterfall_data;		    // Buffer used to store the current data for the scope
        public static float[] waterfall_display_data;            // Buffer for waterfall
        public static float[] average_buffer;					// Averaged display data buffer for Panadapter
        public static float[] average_waterfall_buffer;  		// Averaged display data buffer for Waterfall
        public static float[] peak_buffer;						// Peak hold display data buffer
        public static string background_image = ".\\picDisplay.png";
        public static bool SDRmode = false;
        private static SlimDX.Direct3D9.Device device = null;
        private static Texture PanadapterTexture = null;
        private static Texture WaterfallTexture = null;
        private static Sprite Panadapter_Sprite = null;
        private static Sprite Waterfall_Sprite = null;
        private static Rectangle Panadapter_texture_size;
        private static Rectangle Waterfall_texture_size;
        private static AutoResetEvent Panadapter_Event;
        private static AutoResetEvent Waterfall_Event;
        private static DXRectangle VFOArect;
        private static DXRectangle VFOBrect;
        private static VertexBuffer VerLine_vb = null;
        private static VertexBuffer HorLine_vb = null;
        private static VertexBuffer VerLines_vb = null;
        private static VertexBuffer HorLines_vb = null;
        private static VertexBuffer PanLine_vb = null;
        private static VertexBuffer PanLine_vb_fill = null;
        private static Vertex[] PanLine_verts = null;
        private static Vertex[] PanLine_verts_fill = null;
        private static Vertex[] ScopeLine_verts = null;
        private static VertexBuffer ScopeLine_vb = null;
        private static VertexBuffer ScopeLine_vb_monitor = null;
        private static float[] waterfallX_data = null;
        private static float[] panadapterX_data = null;
        private static float[] panadapterX_scope_data = null;
        private static float[] panadapterX_scope_data_mark = null;
        private static float[] panadapterX_scope_data_space = null;
        private static Bitmap waterfall_bmp = null;
        private static Device waterfall_dx_device = null;
        public static TuneMode tuning_mode = TuneMode.Off;
        private static byte[] waterfall_memory;
        private static int waterfall_bmp_size;
        private static int waterfall_bmp_stride;
        private static DataStream waterfall_data_stream;
        private static Rectangle waterfall_rect;
        private static Mutex RenderMutex = new Mutex();

        #endregion

        #region Properties

        private static DisplayMode display_mode = DisplayMode.PANAFALL;
        public static DisplayMode DisplayMode
        {
            get { return display_mode; }
            set 
            {
                RenderMutex.WaitOne();
                display_mode = value;
                RenderMutex.ReleaseMutex();
            }
        }

        private static RenderType directx_render_type = RenderType.HARDWARE;
        public static RenderType DirectXRenderType
        {
            get { return directx_render_type; }
            set { directx_render_type = value; }
        }

        private static bool reverse_waterfall = false;
        public static bool ReverseWaterfall
        {
            get { return reverse_waterfall; }
            set { reverse_waterfall = value; }
        }

        private static double losc_hz;
        public static double LOSC
        {
            get { return losc_hz; }
            set { losc_hz = value; }
        }

        private static double vfoa_hz;
        public static double VFOA
        {
            get { return vfoa_hz; }
            set { vfoa_hz = value; }
        }

        private static double vfoa_hz_mark;
        public static double VFOA_MARK
        {
            get { return vfoa_hz_mark; }
            set { vfoa_hz_mark = value; }
        }

        private static double vfoa_hz_space;
        public static double VFOA_SPACE
        {
            get { return vfoa_hz_space; }
            set { vfoa_hz_space = value; }
        }

        private static double vfob_hz;
        public static double VFOB
        {
            get { return vfob_hz; }
            set { vfob_hz = value; }
        }

        private static double vfob_hz_mark;
        public static double VFOB_MARK
        {
            get { return vfob_hz_mark; }
            set { vfob_hz_mark = value; }
        }

        private static double vfob_hz_space;
        public static double VFOB_SPACE
        {
            get { return vfob_hz_space; }
            set { vfob_hz_space = value; }
        }

        private static float[] scope_min;
        public static float[] ScopeMin
        {
            get { return scope_min; }
            set { scope_min = value; }
        }

        private static float[] scope_max;
        public static float[] ScopeMax
        {
            get { return scope_max; }
            set { scope_max = value; }
        }

        private static float[] scope_min_mark;
        public static float[] ScopeMinMark
        {
            get { return scope_min_mark; }
            set { scope_min_mark = value; }
        }
        private static float[] scope_max_mark;
        public static float[] ScopeMaxMark
        {
            get { return scope_max_mark; }
            set { scope_max_mark = value; }
        }

        private static float[] scope_min_space;
        public static float[] ScopeMinSpace
        {
            get { return scope_min_space; }
            set { scope_min_space = value; }
        }
        private static float[] scope_max_space;
        public static float[] ScopeMaxSpace
        {
            get { return scope_max_space; }
            set { scope_max_space = value; }
        }


        private static bool refresh_panadapter_grid = false;                
        public static bool RefreshPanadapterGrid
        {
            set { refresh_panadapter_grid = value; }
        }

        public static bool smooth_line = false;                             
        public static bool pan_fill = false;

        private static System.Drawing.Font pan_font = new System.Drawing.Font("System", 10);
        public static System.Drawing.Font PanFont
        {
            get { return pan_font; }
            set
            {
                pan_font = value;
                refresh_panadapter_grid = true;
                if (!MainForm.booting)
                {
                    if (panadapter_font != null)
                        panadapter_font.Dispose();
                    panadapter_font = new SlimDX.Direct3D9.Font(device, pan_font);
                }
            }
        }

        private static SlimDX.Direct3D9.Font panadapter_font = null;
        public static SlimDX.Direct3D9.Font PanadapterFont
        {
            get { return panadapter_font; }
            set
            {
                panadapter_font = value;
            }
        }

        private static Color pan_fill_color = Color.FromArgb(100, 0, 0, 127);
        public static Color PanFillColor
        {
            get { return pan_fill_color; }
            set { pan_fill_color = value; }
        }

        private static Color display_text_background = Color.FromArgb(255, 127, 127, 127);
        public static Color DisplayTextBackground
        {
            get { return display_text_background; }
            set { display_text_background = value; }
        }

        private static Color display_filter_color = Color.FromArgb(65, 255, 255, 255);
        public static Color DisplayFilterColor
        {
            get { return display_filter_color; }
            set { display_filter_color = value; }
        }

        private static bool show_horizontal_grid = true;
        public static bool Show_Horizontal_Grid
        {
            set
            {
                show_horizontal_grid = value;
                refresh_panadapter_grid = true;
            }
        }

        private static bool show_vertical_grid = true;
        public static bool Show_Vertical_Grid
        {
            set
            {
                show_vertical_grid = value;
                refresh_panadapter_grid = true;
            }
        }

        private static int waterfall_update_period = 50; // in ms
        public static int WaterfallUpdatePeriod
        {
            get { return waterfall_update_period; }
            set { waterfall_update_period = value; }
        }

        private static Color main_rx_zero_line_color = Color.LightSkyBlue;
        public static Color MainRXZeroLine
        {
            get { return main_rx_zero_line_color; }
            set
            {
                main_rx_zero_line_color = value;
            }
        }

        private static Color sub_rx_zero_line_color = Color.LightSkyBlue;
        public static Color SubRXZeroLine
        {
            get { return sub_rx_zero_line_color; }
            set
            {
                sub_rx_zero_line_color = value;
            }
        }

        private static Color main_rx_filter_color = Color.FromArgb(100, 0, 100, 100);  // green
        public static Color MainRXFilterColor
        {
            get { return main_rx_filter_color; }
            set
            {
                main_rx_filter_color = value;
            }
        }

        private static Color second_rx_filter_color = Color.FromArgb(100, 0, 0, 255);  // blue
        public static Color SecondRXFilterColor
        {
            get { return second_rx_filter_color; }
            set
            {
                second_rx_filter_color = value;
            }
        }

        private static bool rx2_enabled = false;
        public static bool RX2Enabled
        {
            get { return rx2_enabled; }
            set
            {
                rx2_enabled = value;
            }
        }

        private static bool split_enabled = false;
        public static bool SplitEnabled
        {
            get { return split_enabled; }
            set
            {
                split_enabled = value;
            }
        }

        private static bool show_freq_offset = false;
        public static bool ShowFreqOffset
        {
            get { return show_freq_offset; }
            set
            {
                show_freq_offset = value;
            }
        }

        private static Color band_edge_color = Color.Red;
        public static Color BandEdgeColor
        {
            get { return band_edge_color; }
            set
            {
                band_edge_color = value;
            }
        }

        public static bool panadapter_target_focused = false;
        private static int panadapter_H = 0;	// target height
        private static int panadapter_W = 0;	// target width
        private static Control panadapter_target = null;
        public static Control PanadapterTarget
        {
            get { return panadapter_target; }
            set
            {
                panadapter_target = value;
                panadapter_H = panadapter_target.Height;
                panadapter_W = panadapter_target.Width;
            }
        }

        public static bool waterfall_target_focused = false;
        private static int waterfall_H = 0;	// target height
        private static int waterfall_W = 0;	// target width
        private static Control waterfall_target = null;
        public static Control WaterfallTarget
        {
            get { return waterfall_target; }
            set
            {
                waterfall_target = value;
                waterfall_H = waterfall_target.Height;
                waterfall_W = waterfall_target.Width;
            }
        }

        private static double rx_display_low = 0;
        public static double RXDisplayLow
        {
            get { return rx_display_low; }
            set 
            {
                rx_display_low = value;
                refresh_panadapter_grid = true;
            }
        }

        private static double rx_display_high = 2000;
        public static double RXDisplayHigh
        {
            get { return rx_display_high; }
            set
            {
                rx_display_high = value;
                refresh_panadapter_grid = true;
            }
        }

        private static float preamp_offset = 0.0f;
        public static float PreampOffset
        {
            get { return preamp_offset; }
            set { preamp_offset = value; }
        }

        private static float display_cal_offset;					// display calibration offset per volume setting in dB
        public static float DisplayCalOffset
        {
            get { return display_cal_offset; }
            set { display_cal_offset = value; }
        }

        private static int display_cursor_x;						// x-coord of the cursor when over the display
        public static int DisplayCursorX
        {
            get { return display_cursor_x; }
            set { display_cursor_x = value; }
        }

        private static int rtty_cursor_x1;						    // RTTY cursor when over the display
        public static int RTTYCursorX1
        {
            get { return rtty_cursor_x1; }
            set { rtty_cursor_x1 = value; }
        }

        private static int rtty_cursor_x2;						    // RTTY cursor when over the display
        public static int RTTYCursorX2
        {
            get { return rtty_cursor_x2; }
            set { rtty_cursor_x2 = value; }
        }

        private static int display_cursor_y;						// y-coord of the cursor when over the display
        public static int DisplayCursorY
        {
            get { return display_cursor_y; }
            set { display_cursor_y = value; }
        }

        private static int scope_time = 50;
        public static int ScopeTime
        {
            get { return scope_time; }
            set { scope_time = value; }
        }

        private static int sample_rate = 48000;
        public static int SampleRate
        {
            get { return sample_rate; }
            set { sample_rate = value; }
        }

        private static bool high_swr = false;
        public static bool HighSWR
        {
            get { return high_swr; }
            set { high_swr = value; }
        }

        private static bool mox = false;
        public static bool MOX
        {
            get { return mox; }
            set { mox = value; }
        }

        private static float max_x;								// x-coord of maxmimum over one display pass
        public static float MaxX
        {
            get { return max_x; }
            set { max_x = value; }
        }

        private static float scope_max_x;								// x-coord of maxmimum over one display pass
        public static float ScopeMaxX
        {
            get { return scope_max_x; }
            set { scope_max_x = value; }
        }

        private static float max_y;								// y-coord of maxmimum over one display pass
        public static float MaxY
        {
            get { return max_y; }
            set { max_y = value; }
        }

        private static float scope_max_y;						// y-coord of maxmimum over one display pass
        public static float ScopeMaxY
        {
            get { return scope_max_y; }
            set { scope_max_y = value; }
        }

        private static bool average_on = false;					// True if the Average button is pressed
        public static bool AverageOn
        {
            get { return average_on; }
            set
            {
                average_on = value;
                if (!average_on) ResetDisplayAverage();
            }
        }

        private static bool data_ready = false;			        // True when there is new display data ready from the DSP
        public static bool DataReady
        {
            get { return data_ready; }
            set { data_ready = value; }
        }

        private static bool waterfall_data_ready;	            // True when there is new display data ready from the DSP
        public static bool WaterfallDataReady
        {
            get { return waterfall_data_ready; }
            set { waterfall_data_ready = value; }
        }

        public static float display_avg_mult_old = 1 - (float)1 / 2;
        public static float display_avg_mult_new = (float)1 / 2;
        private static int display_avg_num_blocks = 100;
        public static int DisplayAvgBlocks
        {
            get { return display_avg_num_blocks; }
            set
            {
                display_avg_num_blocks = value;
                display_avg_mult_old = 1 - (float)1 / display_avg_num_blocks;
                display_avg_mult_new = (float)1 / display_avg_num_blocks;
            }
        }

        public static float waterfall_avg_mult_old = 1 - (float)1 / 18;
        public static float waterfall_avg_mult_new = (float)1 / 18;
        private static int waterfall_avg_num_blocks = 18;
        public static int WaterfallAvgBlocks
        {
            get { return waterfall_avg_num_blocks; }
            set
            {
                waterfall_avg_num_blocks = value;
                waterfall_avg_mult_old = 1 - (float)1 / waterfall_avg_num_blocks;
                waterfall_avg_mult_new = (float)1 / waterfall_avg_num_blocks;
            }
        }

        private static int spectrum_grid_max = -50;
        public static int SpectrumGridMax
        {
            get { return spectrum_grid_max; }
            set
            {
                spectrum_grid_max = value;
                refresh_panadapter_grid = true;
            }
        }

        private static int spectrum_grid_min = -150;
        public static int SpectrumGridMin
        {
            get { return spectrum_grid_min; }
            set
            {
                spectrum_grid_min = value;
                refresh_panadapter_grid = true;
            }
        }

        private static int spectrum_grid_step = 10;
        public static int SpectrumGridStep
        {
            get { return spectrum_grid_step; }
            set
            {
                spectrum_grid_step = value;
                refresh_panadapter_grid = true;
            }
        }

        private static Color grid_text_color = Color.Yellow;
        public static Color GridTextColor
        {
            get { return grid_text_color; }
            set
            {
                grid_text_color = value;
                refresh_panadapter_grid = true;
            }
        }

        private static Color grid_zero_color = Color.Red;
        public static Color GridZeroColor
        {
            get { return grid_zero_color; }
            set
            {
                grid_zero_color = value;
            }
        }

        private static Color grid_color = Color.FromArgb(30, Color.Blue);
        public static Color GridColor
        {
            get { return grid_color; }
            set
            {
                grid_color = Color.FromArgb(50, value);
                refresh_panadapter_grid = true;
            }
        }

        private static Pen data_line_pen = new Pen(new SolidBrush(Color.WhiteSmoke), display_line_width);
        private static Color data_line_color = Color.WhiteSmoke;
        public static Color DataLineColor
        {
            get { return data_line_color; }
            set
            {
                data_line_color = value;
                data_line_pen = new Pen(new SolidBrush(data_line_color), display_line_width);
            }
        }

        private static Color display_filter_tx_color = Color.Yellow;
        public static Color DisplayFilterTXColor
        {
            get { return display_filter_tx_color; }
            set
            {
                display_filter_tx_color = value;
            }
        }

        private static bool draw_tx_filter = false;
        public static bool DrawTXFilter
        {
            get { return draw_tx_filter; }
            set
            {
                draw_tx_filter = value;
            }
        }

        private static bool draw_tx_cw_freq = false;
        public static bool DrawTXCWFreq
        {
            get { return draw_tx_cw_freq; }
            set
            {
                draw_tx_cw_freq = value;
            }
        }

        private static Color display_background_color = Color.Black;
        public static Color DisplayBackgroundColor
        {
            get { return display_background_color; }
            set
            {
                display_background_color = value;
            }
        }

        private static Color waterfall_low_color = Color.Black;
        public static Color WaterfallLowColor
        {
            get { return waterfall_low_color; }
            set { waterfall_low_color = value; }
        }

        private static Color waterfall_mid_color = Color.Red;
        public static Color WaterfallMidColor
        {
            get { return waterfall_mid_color; }
            set { waterfall_mid_color = value; }
        }

        private static Color waterfall_high_color = Color.Pink;
        public static Color WaterfallHighColor
        {
            get { return waterfall_high_color; }
            set { waterfall_high_color = value; }
        }

        private static float waterfall_high_threshold = 200.0F;
        public static float WaterfallHighThreshold
        {
            get { return waterfall_high_threshold; }
            set { waterfall_high_threshold = value; }
        }

        private static float waterfall_low_threshold = -200.0F;
        public static float WaterfallLowThreshold
        {
            get { return waterfall_low_threshold; }
            set { waterfall_low_threshold = value; }
        }

        private static float display_line_width = 1.0F;
        public static float DisplayLineWidth
        {
            get { return display_line_width; }
            set
            {
                display_line_width = value;
                data_line_pen = new Pen(new SolidBrush(data_line_color), display_line_width);
            }
        }

        #endregion

        #region Misc routine

        public static void ResetDisplayAverage()
        {
            if (average_buffer != null)
            {
                average_buffer[0] = CLEAR_FLAG;	// set reset flag
                average_waterfall_buffer[0] = CLEAR_FLAG;
            }
        }

        #endregion

        #region Routines

        public static void WaterfallInit()
        {
            average_waterfall_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
            average_waterfall_buffer[0] = CLEAR_FLAG;		// set the clear flag
            new_waterfall_data = new float[BUFFER_SIZE];
            current_waterfall_data = new float[BUFFER_SIZE];
            waterfall_display_data = new float[BUFFER_SIZE];

            for (int i = 0; i < BUFFER_SIZE; i++)
            {
                new_waterfall_data[i] = -200.0f;
                current_waterfall_data[i] = -200.0f;
                waterfall_display_data[i] = -200.0f;
            }

            waterfall_bmp = new Bitmap(waterfall_target.Width, waterfall_target.Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        public static bool DirectXInit()
        {
            if (!MainForm.booting)
            {
                try
                {
                    if (!MainForm.booting)
                    {
                        rendering = false;

                        switch (display_mode)
                        {
                            case DisplayMode.PANADAPTER:
                            case DisplayMode.PANAFALL:
                            case DisplayMode.PANASCOPE:
                                panadapter_target = (Control)MainForm.picPanadapter;
                                panadapter_W = panadapter_target.Width;
                                panadapter_H = panadapter_target.Height;
                                waterfall_target = (Control)MainForm.picWaterfall;
                                waterfall_H = waterfall_target.Height;
                                waterfall_W = waterfall_target.Width;
                                waterfallX_data = new float[waterfall_W];
                                panadapterX_scope_data = new float[waterfall_W * 2];
                                panadapterX_scope_data_mark = new float[waterfall_W * 2];
                                panadapterX_scope_data_space = new float[waterfall_W * 2];
                                Audio.ScopeDisplayWidth = waterfall_W;
                                break;
                            case DisplayMode.PANAFALL_INV:
                            case DisplayMode.PANASCOPE_INV:
                                panadapter_target = (Control)MainForm.picWaterfall;
                                panadapter_W = panadapter_target.Width;
                                panadapter_H = panadapter_target.Height;
                                waterfall_target = (Control)MainForm.picPanadapter;
                                waterfall_H = waterfall_target.Height;
                                waterfall_W = waterfall_target.Width;
                                waterfallX_data = new float[waterfall_W];
                                panadapterX_scope_data = new float[waterfall_W * 2];
                                Audio.ScopeDisplayWidth = waterfall_W;
                                break;
                            case DisplayMode.WATERFALL:
                                waterfall_target = (Control)MainForm.picWaterfall;
                                break;
                            default:
                                panadapter_H = panadapter_target.Height;
                                panadapter_W = panadapter_target.Width;
                                panadapterX_scope_data = new float[panadapter_W];
                                panadapter_target = (Control)MainForm.picPanadapter;
                                break;
                        }

                        panadapterX_data = new float[panadapter_target.Width];
                        refresh_panadapter_grid = true;

                        average_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
                        average_buffer[0] = CLEAR_FLAG;		// set the clear flag

                        average_waterfall_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
                        average_waterfall_buffer[0] = CLEAR_FLAG;		// set the clear flag

                        peak_buffer = new float[BUFFER_SIZE];
                        peak_buffer[0] = CLEAR_FLAG;

                        new_display_data = new float[BUFFER_SIZE];
                        new_waterfall_data = new float[BUFFER_SIZE];
                        current_display_data = new float[BUFFER_SIZE];
                        current_waterfall_data = new float[BUFFER_SIZE];
                        waterfall_display_data = new float[BUFFER_SIZE];

                        for (int i = 0; i < BUFFER_SIZE; i++)
                        {
                            new_display_data[i] = 0.0f;
                            new_waterfall_data[i] = 0.0f;
                            current_display_data[i] = 0.0f;
                            current_waterfall_data[i] = 0.0f;
                            waterfall_display_data[i] = 0.0f;
                        }

                        PresentParameters presentParms = new PresentParameters();
                        presentParms.Windowed = true;
                        presentParms.SwapEffect = SwapEffect.Discard;
                        presentParms.BackBufferFormat = SlimDX.Direct3D9.Format.Unknown;
                        presentParms.BackBufferHeight = panadapter_target.Height;
                        presentParms.BackBufferWidth = panadapter_target.Width;
                        presentParms.BackBufferCount = 1;
                        presentParms.BackBufferFormat = Format.X8R8G8B8;

                        PresentParameters waterfall_presentParms = new PresentParameters();
                        waterfall_presentParms.Windowed = true;
                        waterfall_presentParms.SwapEffect = SwapEffect.Discard;
                        waterfall_presentParms.BackBufferFormat = SlimDX.Direct3D9.Format.Unknown;
                        waterfall_presentParms.BackBufferHeight = waterfall_target.Height;
                        waterfall_presentParms.BackBufferWidth = waterfall_target.Width;
                        waterfall_presentParms.BackBufferCount = 1;
                        waterfall_presentParms.BackBufferFormat = Format.X8R8G8B8;

                        switch (directx_render_type)
                        {
                            case RenderType.HARDWARE:
                                try
                                {
                                    device = new Device(new Direct3D(), 0, DeviceType.Hardware,
                                        panadapter_target.Handle, CreateFlags.HardwareVertexProcessing |
                                        CreateFlags.FpuPreserve | CreateFlags.Multithreaded, presentParms);

                                    waterfall_dx_device = new Device(new Direct3D(), 0,
                                        DeviceType.Hardware, waterfall_target.Handle,
                                        CreateFlags.HardwareVertexProcessing | CreateFlags.FpuPreserve |
                                         CreateFlags.Multithreaded, waterfall_presentParms);
                                }
                                catch (Direct3D9Exception ex)
                                {
                                    MessageBox.Show("DirectX hardware init error!" + ex.ToString());
                                }
                                break;

                            case RenderType.SOFTWARE:
                                {
                                    try
                                    {
                                        device = new Device(new Direct3D(), 0,
                                            DeviceType.Hardware,
                                            panadapter_target.Handle, CreateFlags.SoftwareVertexProcessing |
                                            CreateFlags.FpuPreserve, presentParms);

                                        waterfall_dx_device = new Device(new Direct3D(), 0,
                                            DeviceType.Hardware, waterfall_target.Handle,
                                            CreateFlags.SoftwareVertexProcessing | CreateFlags.FpuPreserve,
                                            waterfall_presentParms);

                                    }
                                    catch (Direct3D9Exception ex)
                                    {
                                        MessageBox.Show("DirectX software init error!\n" + ex.ToString());
                                        return false;
                                    }
                                }
                                break;
                        }

                        var vertexElems = new[] {
                        new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.PositionTransformed, 0),
                        new VertexElement(0, 16, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                        VertexElement.VertexDeclarationEnd
                        };

                        var vertexElems1 = new[] {
                        new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.PositionTransformed, 0),
                        new VertexElement(0, 16, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                        VertexElement.VertexDeclarationEnd
                        };

                        var vertexDecl = new VertexDeclaration(device, vertexElems);
                        device.VertexDeclaration = vertexDecl;
                        var vertexDecl1 = new VertexDeclaration(waterfall_dx_device, vertexElems1);
                        waterfall_dx_device.VertexDeclaration = vertexDecl1;

                        waterfall_bmp = new System.Drawing.Bitmap(waterfall_target.Width, waterfall_target.Height,
                            System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                        BitmapData bitmapData = waterfall_bmp.LockBits(
                            new Rectangle(0, 0, waterfall_bmp.Width, waterfall_bmp.Height),
                            ImageLockMode.ReadWrite, waterfall_bmp.PixelFormat);

                        waterfall_bmp_size = bitmapData.Stride * waterfall_bmp.Height;
                        waterfall_bmp_stride = bitmapData.Stride;
                        waterfall_memory = new byte[waterfall_bmp_size];
                        waterfall_bmp.UnlockBits(bitmapData);
                        waterfall_rect = new Rectangle(0, 0, waterfall_target.Width, waterfall_target.Height);

                        panadapter_font = new SlimDX.Direct3D9.Font(device, pan_font);

                        if (File.Exists(background_image))
                        {
                            PanadapterTexture = Texture.FromFile(device, background_image, panadapter_target.Width, panadapter_target.Height,
                                1, Usage.None, Format.Unknown, Pool.Managed, SlimDX.Direct3D9.Filter.Default, SlimDX.Direct3D9.Filter.Default, 0);
                            Panadapter_texture_size.Width = panadapter_target.Width;
                            Panadapter_texture_size.Height = panadapter_target.Height;
                            Panadapter_Sprite = new Sprite(device);
                            WaterfallTexture = Texture.FromFile(waterfall_dx_device, background_image, waterfall_target.Width, waterfall_target.Height,
                                1, Usage.None, Format.X8R8G8B8, Pool.Managed, SlimDX.Direct3D9.Filter.Default, SlimDX.Direct3D9.Filter.Default, 0);
                            Waterfall_texture_size.Width = waterfall_target.Width;
                            Waterfall_texture_size.Height = waterfall_target.Height;
                            Waterfall_Sprite = new Sprite(waterfall_dx_device);
                        }
                        else
                        {
                            Panadapter_Sprite = null;
                            WaterfallTexture = new Texture(waterfall_dx_device, waterfall_target.Width, waterfall_target.Height, 0,
                                Usage.None, Format.X8R8G8B8, Pool.Managed);
                            Waterfall_texture_size.Width = waterfall_target.Width;
                            Waterfall_texture_size.Height = waterfall_target.Height;
                            Waterfall_Sprite = new Sprite(waterfall_dx_device);
                        }

                        if (Panadapter_Event == null)
                            Panadapter_Event = new AutoResetEvent(true);
                        if (Waterfall_Event == null)
                            Waterfall_Event = new AutoResetEvent(true);

                        ScopeLine_vb = new VertexBuffer(waterfall_dx_device, waterfall_W * 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
                        ScopeLine_verts = new Vertex[waterfall_W * 2];
                        ScopeLine_vb_monitor = new VertexBuffer(MainForm.waterfall_dx_device, MainForm.picMonitor.Width * 2 * 20,
                            Usage.WriteOnly, VertexFormat.None, Pool.Managed);

                        PanLine_vb = new VertexBuffer(device, panadapterX_data.Length * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
                        PanLine_vb_fill = new VertexBuffer(device, panadapter_W * 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
                        PanLine_verts = new Vertex[panadapter_W];
                        PanLine_verts_fill = new Vertex[panadapter_W * 2];

                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Debug.Write("DirectX init general fault!\n" + ex.ToString());
                    return false;
                }
            }
            return false;
        }

        public static void DirectXRelease()
        {
            try
            {
                if (!MainForm.booting)
                {
                    waterfallX_data = null;
                    panadapterX_data = null;

                    new_display_data = null;
                    new_waterfall_data = null;
                    current_display_data = null;
                    current_waterfall_data = null;
                    waterfall_display_data = null;

                    average_buffer = null;
                    average_waterfall_buffer = null;

                    peak_buffer = null;
                    if (waterfall_bmp != null)
                        waterfall_bmp.Dispose();
                    waterfall_bmp = null;
                    if (Panadapter_Sprite != null)
                        Panadapter_Sprite.Dispose();
                    Panadapter_Sprite = null;
                    if (Waterfall_Sprite != null)
                        Waterfall_Sprite.Dispose();
                    Waterfall_Sprite = null;

                    if (PanadapterTexture != null)
                    {
                        PanadapterTexture.Dispose();
                        PanadapterTexture = null;
                    }

                    if (WaterfallTexture != null)
                    {
                        WaterfallTexture.Dispose();
                        WaterfallTexture = null;
                    }

                    if (Panadapter_Event != null)
                    {
                        Panadapter_Event.Close();
                        Panadapter_Event = null;
                    }

                    if (Waterfall_Event != null)
                    {
                        Waterfall_Event.Close();
                        Waterfall_Event = null;
                    }

                    if (VerLine_vb != null)
                    {
                        VerLine_vb.Dispose();
                        VerLine_vb = null;
                    }

                    if (VerLines_vb != null)
                    {
                        VerLines_vb.Dispose();
                        VerLines_vb = null;
                    }

                    if (HorLine_vb != null)
                    {
                        HorLine_vb.Dispose();
                        HorLine_vb = null;
                    }

                    if (HorLines_vb != null)
                    {
                        HorLines_vb.Dispose();
                        HorLines_vb = null;
                    }

                    if (PanLine_vb != null)
                    {
                        PanLine_vb.Dispose();
                        PanLine_vb.Dispose();
                    }

                    if (PanLine_vb_fill != null)
                    {
                        PanLine_vb_fill.Dispose();
                        PanLine_vb_fill.Dispose();
                    }

                    if (vertical_label != null)
                        vertical_label = null;

                    if (horizontal_label != null)
                        horizontal_label = null;

                    if (device != null)
                    {
                        device.Dispose();
                        device = null;
                    }

                    if (waterfall_dx_device != null)
                    {
                        waterfall_dx_device.Dispose();
                        waterfall_dx_device = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("DX release error!" + ex.ToString());
            }
        }

        public static void Render_VFOA()
        {
            try
            {
                if (Audio.SDRmode)
                {
                    double low = rx_display_low;
                    double high = rx_display_high;
                    int filter_low, filter_high;
                    int filter_left = 0;
                    int filter_right = 0;
                    int mark_left = 0;
                    int mark_right = 0;
                    int space_left = 0;
                    int space_right = 0;
                    double losc = losc_hz * 1e6;
                    double vfoa = vfoa_hz * 1e6;

                    switch (MainForm.OpModeVFOA)
                    {
                        case Mode.CW:
                            {
                                filter_low = -MainForm.FilterWidthVFOA / 2;
                                filter_high = MainForm.FilterWidthVFOA / 2;
                                filter_left = (int)(((-low + filter_low + vfoa - losc) / (high - low) * panadapter_W));
                                filter_right = (int)(((-low + filter_high + vfoa - losc) / (high - low) * panadapter_W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;
                            }
                            break;

                        case Mode.RTTY:
                            {
                                double vfoa_mark = vfoa_hz_mark * 1e6;
                                double vfoa_space = vfoa_hz_space * 1e6;
                                filter_low = -MainForm.FilterWidthVFOA / 2;
                                filter_high = MainForm.FilterWidthVFOA / 2;
                                filter_left = (int)(((-low + filter_low + vfoa_space - losc) / (high - low) * panadapter_W));
                                filter_right = (int)(((-low + filter_high + vfoa_mark - losc) / (high - low) * panadapter_W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;

                                filter_low = -40;
                                filter_high = 40;
                                mark_left = (int)(((-low + filter_low + vfoa_mark - losc) / (high - low) * panadapter_W));
                                mark_right = (int)(((-low + filter_high + vfoa_mark - losc) / (high - low) * panadapter_W));
                                space_left = (int)(((-low + filter_low + vfoa_space - losc) / (high - low) * panadapter_W));
                                space_right = (int)(((-low + filter_high + vfoa_space - losc) / (high - low) * panadapter_W));

                                if (mark_left == mark_right)
                                    mark_right = mark_left + 1;

                                if (space_left == space_right)
                                    space_right = space_left + 1;
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
                                filter_low = -MainForm.FilterWidthVFOA / 2;
                                filter_high = MainForm.FilterWidthVFOA / 2;
                                filter_left = (int)(((-low + filter_low + vfoa - losc) / (high - low) * panadapter_W));
                                filter_right = (int)(((-low + filter_high + vfoa - losc) / (high - low) * panadapter_W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;
                            }
                            break;
                    }

                    VFOArect.x1 = filter_right;
                    VFOArect.y1 = (int)(pan_font.Height);
                    VFOArect.x2 = filter_right;
                    VFOArect.y2 = panadapter_H;
                    VFOArect.x3 = filter_left;
                    VFOArect.y3 = (int)(pan_font.Height);
                    VFOArect.x4 = filter_left;
                    VFOArect.y4 = panadapter_H;
                    RenderRectangle(device, VFOArect, main_rx_filter_color);

                    if (MainForm.OpModeVFOA == Mode.RTTY)
                    {
                        VFOArect.x1 = mark_right;
                        VFOArect.y1 = (int)(pan_font.Height);
                        VFOArect.x2 = mark_right;
                        VFOArect.y2 = panadapter_H;
                        VFOArect.x3 = mark_left;
                        VFOArect.y3 = (int)(pan_font.Height);
                        VFOArect.x4 = mark_left;
                        VFOArect.y4 = panadapter_H;
                        RenderRectangle(device, VFOArect, main_rx_filter_color);

                        VFOArect.x1 = space_right;
                        VFOArect.y1 = (int)(pan_font.Height);
                        VFOArect.x2 = space_right;
                        VFOArect.y2 = panadapter_H;
                        VFOArect.x3 = space_left;
                        VFOArect.y3 = (int)(pan_font.Height);
                        VFOArect.x4 = space_left;
                        VFOArect.y4 = panadapter_H;
                        RenderRectangle(device, VFOArect, main_rx_filter_color);
                    }
                }
                else
                {
                    double low = rx_display_low;
                    double high = rx_display_high;
                    int filter_low, filter_high;
                    int filter_left = 0;
                    int filter_right = 0;

                    filter_low = -MainForm.FilterWidthVFOA / 2;
                    filter_high = MainForm.FilterWidthVFOA / 2;
                    // get filter screen coordinates
                    filter_left = (int)((float)(filter_low - low + vfoa_hz) / (high - low) * panadapter_W);
                    filter_right = (int)((float)(filter_high - low + vfoa_hz) / (high - low) * panadapter_W);

                    if (filter_left == filter_right)
                        filter_right = filter_left + 1;

                    VFOArect.x1 = filter_right;
                    VFOArect.y1 = (int)(pan_font.Height);
                    VFOArect.x2 = filter_right;
                    VFOArect.y2 = panadapter_H;
                    VFOArect.x3 = filter_left;
                    VFOArect.y3 = (int)(pan_font.Height);
                    VFOArect.x4 = filter_left;
                    VFOArect.y4 = panadapter_H;
                    RenderRectangle(device, VFOArect, main_rx_filter_color);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static void Render_VFOB()
        {
            try
            {
                if (Audio.SDRmode)
                {
                    double low = rx_display_low;
                    double high = rx_display_high;
                    int filter_low, filter_high;
                    int filter_left = 0;
                    int filter_right = 0;
                    int mark_left = 0;
                    int mark_right = 0;
                    int space_left = 0;
                    int space_right = 0;
                    double losc = losc_hz * 1e6;
                    double vfob = vfob_hz * 1e6;

                    switch (MainForm.OpModeVFOB)
                    {
                        case Mode.CW:
                            {
                                filter_low = -MainForm.FilterWidthVFOB / 2;
                                filter_high = MainForm.FilterWidthVFOB / 2;
                                filter_left = (int)(((-low + filter_low + vfob - losc) / (high - low) * panadapter_W));
                                filter_right = (int)(((-low + filter_high + vfob - losc) / (high - low) * panadapter_W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;
                            }
                            break;

                        case Mode.RTTY:
                            {
                                double vfob_mark = vfob_hz_mark * 1e6;
                                double vfob_space = vfob_hz_space * 1e6;

                                filter_low = -MainForm.FilterWidthVFOB / 2;
                                filter_high = MainForm.FilterWidthVFOB / 2;
                                filter_left = (int)(((-low + filter_low + vfob_space - losc) / (high - low) * panadapter_W));
                                filter_right = (int)(((-low + filter_high + vfob_mark - losc) / (high - low) * panadapter_W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;

                                filter_low = -40;
                                filter_high = 40;
                                mark_left = (int)(((-low + filter_low + vfob_mark - losc) / (high - low) * panadapter_W));
                                mark_right = (int)(((-low + filter_high + vfob_mark - losc) / (high - low) * panadapter_W));
                                space_left = (int)(((-low + filter_low + vfob_space - losc) / (high - low) * panadapter_W));
                                space_right = (int)(((-low + filter_high + vfob_space - losc) / (high - low) * panadapter_W));

                                if (mark_left == mark_right)
                                    mark_right = mark_left + 1;

                                if (space_left == space_right)
                                    space_right = space_left + 1;
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
                                filter_low = -MainForm.FilterWidthVFOB / 2;
                                filter_high = MainForm.FilterWidthVFOB / 2;
                                filter_left = (int)(((-low + filter_low + vfob - losc) / (high - low) * panadapter_W));
                                filter_right = (int)(((-low + filter_high + vfob - losc) / (high - low) * panadapter_W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;
                            }
                            break;
                    }

                    VFOBrect.x1 = filter_right;
                    VFOBrect.y1 = (int)(pan_font.Height);
                    VFOBrect.x2 = filter_right;
                    VFOBrect.y2 = panadapter_H;
                    VFOBrect.x3 = filter_left;
                    VFOBrect.y3 = (int)(pan_font.Height);
                    VFOBrect.x4 = filter_left;
                    VFOBrect.y4 = panadapter_H;
                    RenderRectangle(device, VFOBrect, second_rx_filter_color);

                    if (MainForm.OpModeVFOB == Mode.RTTY)
                    {
                        VFOBrect.x1 = mark_right;
                        VFOBrect.y1 = (int)(pan_font.Height);
                        VFOBrect.x2 = mark_right;
                        VFOBrect.y2 = panadapter_H;
                        VFOBrect.x3 = mark_left;
                        VFOBrect.y3 = (int)(pan_font.Height);
                        VFOBrect.x4 = mark_left;
                        VFOBrect.y4 = panadapter_H;
                        RenderRectangle(device, VFOBrect, second_rx_filter_color);
                        VFOBrect.x1 = space_right;
                        VFOBrect.y1 = (int)(pan_font.Height);
                        VFOBrect.x2 = space_right;
                        VFOBrect.y2 = panadapter_H;
                        VFOBrect.x3 = space_left;
                        VFOBrect.y3 = (int)(pan_font.Height);
                        VFOBrect.x4 = space_left;
                        VFOBrect.y4 = panadapter_H;
                        RenderRectangle(device, VFOBrect, second_rx_filter_color);
                    }
                }
                else
                {
                    double low = rx_display_low;
                    double high = rx_display_high;
                    int filter_low, filter_high;
                    int filter_left = 0;
                    int filter_right = 0;

                    filter_low = -MainForm.FilterWidthVFOB / 2;
                    filter_high = MainForm.FilterWidthVFOB / 2;
                    // get filter screen coordinates
                    filter_left = (int)((float)(filter_low - low + vfob_hz) / (high - low) * panadapter_W);
                    filter_right = (int)((float)(filter_high - low + vfob_hz) / (high - low) * panadapter_W);

                    if (filter_left == filter_right)
                        filter_right = filter_left + 1;

                    VFOBrect.x1 = filter_right;
                    VFOBrect.y1 = (int)(pan_font.Height);
                    VFOBrect.x2 = filter_right;
                    VFOBrect.y2 = panadapter_H;
                    VFOBrect.x3 = filter_left;
                    VFOBrect.y3 = (int)(pan_font.Height);
                    VFOBrect.x4 = filter_left;
                    VFOBrect.y4 = panadapter_H;
                    RenderRectangle(device, VFOBrect, second_rx_filter_color);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private static void RenderRectangle(Device dev, DXRectangle rect, Color color)
        {
            Vertex[] verts = new Vertex[4];

            var vb = new VertexBuffer(dev, 4 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);

            verts[0] = new Vertex();
            verts[0].Color = color.ToArgb();
            verts[0].Position = new Vector4(rect.x1, rect.y1, 0.0f, 0.0f);
            verts[1] = new Vertex();
            verts[1].Color = color.ToArgb();
            verts[1].Position = new Vector4(rect.x2, rect.y2, 0.0f, 0.0f);
            verts[2] = new Vertex();
            verts[2].Color = color.ToArgb();
            verts[2].Position = new Vector4(rect.x3, rect.y3, 0.0f, 0.0f);
            verts[3] = new Vertex();
            verts[3].Color = color.ToArgb();
            verts[3].Position = new Vector4(rect.x4, rect.y4, 0.0f, 0.0f);

            vb.Lock(0, 0, LockFlags.None).WriteRange(verts, 0, 4);
            vb.Unlock();
            device.SetStreamSource(0, vb, 0, 20);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);

            vb.Dispose();
        }

        private static void RenderVerticalLines(Device dev, VertexBuffer vertex, int count)        
        {
            dev.SetStreamSource(0, vertex, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, count);
        }

        private static void RenderHorizontalLines(Device dev, VertexBuffer vertex, int count)       
        {
            dev.SetStreamSource(0, vertex, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, count);
        }

        public static void RenderVerticalLine(Device dev, int x, int y, Color color)                
        {
            var vb = new VertexBuffer(dev, 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);
            x = Math.Max(x, 0);

            vb.Lock(0, 0, LockFlags.None).WriteRange(new[] {
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x, (float)(pan_font.Size + 5), 0.0f, 0.0f) },
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x, (float)y, 0.0f, 0.0f) }
                 });
            vb.Unlock();

            dev.SetStreamSource(0, vb, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, 1);

            vb.Dispose();
        }

        private static void RenderVerticalLine(Device dev, int x1, int y1, int x2, int y2, Color color)
        {
            var vb = new VertexBuffer(dev, 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);

            vb.Lock(0, 0, LockFlags.None).WriteRange(new[] {
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x1, (float)y1, 0.0f, 0.0f) },
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x2, (float)y2, 0.0f, 0.0f) }
                 });
            vb.Unlock();

            dev.SetStreamSource(0, vb, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, 1);

            vb.Dispose();
        }

        private static void RenderHorizontalLine(Device dev, int x, int y, Color color)              
        {
            var vb = new VertexBuffer(dev, 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);

            vb.Lock(0, 0, LockFlags.None).WriteRange(new[] {
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)x, (float)y, 0.0f, 0.0f) },
                new Vertex() { Color = color.ToArgb(), Position = new Vector4((float)panadapter_W, (float)y, 0.0f, 0.0f) }
                 });
            vb.Unlock();

            dev.SetStreamSource(0, vb, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineList, 0, 1);

            vb.Dispose();
        }

        private static void RenderPanadapterLine(Device dev)        
        {
            if (pan_fill)
            {
                int j = 0;
                int i = 0;

                for (i = 0; i < panadapter_W * 2; i++)
                {
                    PanLine_verts_fill[i] = new Vertex();
                    PanLine_verts_fill[i].Color = pan_fill_color.ToArgb();
                    PanLine_verts_fill[i].Position = new Vector4(i / 2, panadapterX_data[j], 0.0f, 0.0f);
                    PanLine_verts_fill[i + 1] = new Vertex();
                    PanLine_verts_fill[i + 1].Color = pan_fill_color.ToArgb();
                    PanLine_verts_fill[i + 1].Position = new Vector4(i / 2, panadapter_H, 0.0f, 0.0f);
                    i++;
                    j++;
                }

                PanLine_vb_fill.Lock(0, 0, LockFlags.None).WriteRange(PanLine_verts_fill, 0, panadapter_W * 2);
                PanLine_vb_fill.Unlock();

                dev.SetStreamSource(0, PanLine_vb_fill, 0, 20);
                dev.DrawPrimitives(PrimitiveType.LineList, 0, panadapter_W);
            }

            for (int i = 0; i < panadapter_W; i++)
            {
                PanLine_verts[i] = new Vertex();
                PanLine_verts[i].Color = data_line_color.ToArgb();
                PanLine_verts[i].Position = new Vector4(i, panadapterX_data[i], 0.0f, 0.0f);
            }

            PanLine_vb.Lock(0, 0, LockFlags.None).WriteRange(PanLine_verts, 0, panadapter_W);
            PanLine_vb.Unlock();

            dev.SetStreamSource(0, PanLine_vb, 0, 20);
            dev.DrawPrimitives(PrimitiveType.LineStrip, 0, panadapter_W-1);
        }

        private static int h_steps = 10;
        private static VerticalString[] vertical_label;
        private static int vgrid;
        private static HorizontalString[] horizontal_label;
        private static void RenderPanadapterGrid(int W, int H)      
        {
            double low = rx_display_low;					// initialize variables
            double high = rx_display_high;
            int mid_w = W / 2;
            int y_range = spectrum_grid_max - spectrum_grid_min;
            int center_line_x = W / 2;

            if (VerLines_vb == null)
                VerLines_vb = new VertexBuffer(device, 16 * 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);
            if (HorLines_vb == null)
                HorLines_vb = new VertexBuffer(device, h_steps * 2 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Default);
            if (vertical_label == null)
                vertical_label = new VerticalString[16 + 1];
            if (horizontal_label == null)
                horizontal_label = new HorizontalString[h_steps];

            double actual_fgrid = 0;
                            string label = "";

            if (Audio.SDRmode)
            {
                vgrid = 0;
                low = losc_hz * 1e6 + rx_display_low;
                high = losc_hz * 1e6 + rx_display_high;
                double width = high - low;

                for (int i = 0; i < 12; i++)
                {
                    actual_fgrid += panadapter_W / 10;
                    double x_pos = i * panadapter_target.Width / 10.0;
                    double x = (x_pos / (double)panadapter_target.Width) * (double)width;
                    label = Math.Round((double)(low + x) / 1e6, 4).ToString("f4");
                    label = label.PadRight(4, '0');
                    label = label.Replace(',', '.');
                    vertical_label[i].label = label;
                    vertical_label[i].pos_x = vgrid-20;
                    vertical_label[i].pos_y = 0;
                    vertical_label[i].color = grid_text_color;
                    vgrid += panadapter_W / 10;
                }
            }
            else
            {
                vgrid = 0;
                vertical_label[0].label = "0";
                vertical_label[0].pos_x = 5;
                vertical_label[0].pos_y = 0;
                vertical_label[0].color = grid_text_color;

                VerLines_vb.Lock(0, 40, LockFlags.None).WriteRange(new[] {
                        new Vertex() { Color = grid_color.ToArgb(), Position = new Vector4(0.0f, (float)pan_font.Height, 0.0f, 0.0f) },
                        new Vertex() { Color = grid_color.ToArgb(), Position = new Vector4(0.0f, (float)H, 0.0f, 0.0f) },
                    });
                VerLines_vb.Unlock();

                for (int i = 1; i < 12; i++)
                {
                    vgrid += panadapter_W / 12;
                    actual_fgrid += panadapter_W / 12;
                    vertical_label[i].label = (i * 170).ToString();
                    vertical_label[i].pos_x = vgrid - 15;
                    vertical_label[i].pos_y = 0;
                    vertical_label[i].color = grid_text_color;
                }
            }

            vgrid = 0;
            // Draw vertical lines
            for (int i = 1; i <= 16; i++)
            {
                vgrid += panadapter_W / 16;

                VerLines_vb.Lock(i * 40, 40, LockFlags.None).WriteRange(new[] {
                        new Vertex() { Color = grid_color.ToArgb(), Position = new Vector4((float)vgrid, (float)pan_font.Height, 0.0f, 0.0f) },
                        new Vertex() { Color = grid_color.ToArgb(), Position = new Vector4((float)vgrid, (float)H, 0.0f, 0.0f) },
                    });
                VerLines_vb.Unlock();

                RenderVerticalLine(device, vgrid, H, grid_color);
            }

            // Draw horizontal lines
            for (int i = 1; i < h_steps; i++)
            {
                int xOffset = 0;
                double num = spectrum_grid_max - i * (H / 10);
                int y = (int)((double)(spectrum_grid_max - num) ); // +(int)pan_font.Size;

                if (show_horizontal_grid)
                {
                    HorLines_vb.Lock(i * 40, 40, LockFlags.None).WriteRange(new[] {
                        new Vertex() { Color = grid_color.ToArgb(), Position = new Vector4(0.0f, (float)y, 0.0f, 0.0f) },
                        new Vertex() { Color = grid_color.ToArgb(), Position = new Vector4((float)W, (float)y, 0.0f, 0.0f) },
                    });
                    HorLines_vb.Unlock();

                    RenderHorizontalLine(device, 0, y, grid_color);
                }

                // Draw horizontal line labels
                num = spectrum_grid_max - i * (double)((double)y_range / 10.0);
                horizontal_label[i].label = num.ToString("f0");
                if (horizontal_label[i].label.Length == 2) xOffset = 10;
                else if (horizontal_label[i].label.Length == 1) xOffset = 20;
                else xOffset = 5;

                int x = 0;
                x = xOffset;
                y -= 8;
                if (y + 9 < H)
                {
                    panadapter_font.DrawString(null, horizontal_label[i].label, x, y, grid_text_color.ToArgb());
                    horizontal_label[i].pos_x = x;
                    horizontal_label[i].pos_y = y;
                    horizontal_label[i].color = grid_text_color;
                }
            }
        }


        private static float[] waterfall_data;
        private static bool ConvertDataForWaterfall()                 // yt7pwr
        {
            int W = waterfall_W;

            if (waterfall_data == null || waterfall_data.Length < W)
                waterfall_data = new float[W];			                    // array of points to display

            float slope = 0.0F;						                        // samples to process per pixel
            int num_samples = 0;					                        // number of samples to process
            int start_sample_index = 0;				                        // index to begin looking at samples
            int low = 0;
            int high = 0;
            low = (int)rx_display_low;
            high = (int)rx_display_high;
            max_y = Int32.MinValue;
            int R = 0, G = 0, B = 0;	                                	 // variables to save Red, Green and Blue component values

            if (MainForm.PWR)
            {
                int yRange = spectrum_grid_max - spectrum_grid_min;

                if (waterfall_data_ready && !MainForm.MOX)
                {
                    // get new data
                    Array.Copy(new_waterfall_data, current_waterfall_data, current_waterfall_data.Length);
                    waterfall_data_ready = false;
                }

                if (average_on)
                    UpdateDirectXDisplayWaterfallAverage();

                num_samples = (high - low);

                start_sample_index = (BUFFER_SIZE >> 1) + (int)((low * BUFFER_SIZE) / Audio.SampleRate);
                num_samples = (int)((high - low) * BUFFER_SIZE / Audio.SampleRate);
                start_sample_index = (start_sample_index + 4096) % 4096;
                if ((num_samples - start_sample_index) > (BUFFER_SIZE + 1))
                    num_samples = BUFFER_SIZE - start_sample_index;

                slope = (float)num_samples / (float)W;
                for (int i = 0; i < W; i++)
                {
                    float max = float.MinValue;
                    float dval = i * slope + start_sample_index;
                    int lindex = (int)Math.Floor(dval);
                    int rindex = (int)Math.Floor(dval + slope);

                    if (slope <= 1 || lindex == rindex)
                        max = current_waterfall_data[lindex] * ((float)lindex - dval + 1) +
                            current_waterfall_data[(lindex + 1) % 4096] * (dval - (float)lindex);
                    else
                    {
                        for (int j = lindex; j < rindex; j++)
                            if (current_waterfall_data[j % 4096] > max) max = current_waterfall_data[j % 4096];
                    }

                    max += display_cal_offset;

                    if (max > max_y)
                    {
                        max_y = max;
                        max_x = i;
                    }

                    waterfall_data[i] = max;
                }

                int pixel_size = 4;
                int ptr = 0;

                if (!MainForm.MOX)
                {
                    if (reverse_waterfall)
                    {
                        Array.Copy(waterfall_memory, waterfall_bmp_stride, waterfall_memory, 0, waterfall_bmp_size - waterfall_bmp_stride);
                        ptr = waterfall_bmp_size - waterfall_bmp_stride;
                    }
                    else
                    {
                        Array.Copy(waterfall_memory, 0, waterfall_memory, waterfall_bmp_stride, waterfall_bmp_size - waterfall_bmp_stride);
                    }

                    int i = 0;
                    // draw new data
                    for (i = 0; i < W; i++)	// for each pixel in the new line
                    {
                        if (waterfall_data[i] <= waterfall_low_threshold)
                        {
                            R = WaterfallLowColor.R;
                            G = WaterfallLowColor.G;
                            B = WaterfallLowColor.B;
                        }
                        else if (waterfall_data[i] >= WaterfallHighThreshold)
                        {
                            R = 192;
                            G = 124;
                            B = 255;
                        }
                        else // value is between low and high
                        {
                            float range = WaterfallHighThreshold - waterfall_low_threshold;
                            float offset = waterfall_data[i] - waterfall_low_threshold;
                            float overall_percent = offset / range; // value from 0.0 to 1.0 where 1.0 is high and 0.0 is low.

                            if (overall_percent < (float)2 / 9) // background to blue
                            {
                                float local_percent = overall_percent / ((float)2 / 9);
                                R = (int)((1.0 - local_percent) * WaterfallLowColor.R);
                                G = (int)((1.0 - local_percent) * WaterfallLowColor.G);
                                B = (int)(WaterfallLowColor.B + local_percent * (255 - WaterfallLowColor.B));
                            }
                            else if (overall_percent < (float)3 / 9) // blue to blue-green
                            {
                                float local_percent = (overall_percent - (float)2 / 9) / ((float)1 / 9);
                                R = 0;
                                G = (int)(local_percent * 255);
                                B = 255;
                            }
                            else if (overall_percent < (float)4 / 9) // blue-green to green
                            {
                                float local_percent = (overall_percent - (float)3 / 9) / ((float)1 / 9);
                                R = 0;
                                G = 255;
                                B = (int)((1.0 - local_percent) * 255);
                            }
                            else if (overall_percent < (float)5 / 9) // green to red-green
                            {
                                float local_percent = (overall_percent - (float)4 / 9) / ((float)1 / 9);
                                R = (int)(local_percent * 255);
                                G = 255;
                                B = 0;
                            }
                            else if (overall_percent < (float)7 / 9) // red-green to red
                            {
                                float local_percent = (overall_percent - (float)5 / 9) / ((float)2 / 9);
                                R = 255;
                                G = (int)((1.0 - local_percent) * 255);
                                B = 0;
                            }
                            else if (overall_percent < (float)8 / 9) // red to red-blue
                            {
                                float local_percent = (overall_percent - (float)7 / 9) / ((float)1 / 9);
                                R = 255;
                                G = 0;
                                B = (int)(local_percent * 255);
                            }
                            else // red-blue to purple end
                            {
                                float local_percent = (overall_percent - (float)8 / 9) / ((float)1 / 9);
                                R = (int)((0.75 + 0.25 * (1.0 - local_percent)) * 255);
                                G = (int)(local_percent * 255 * 0.5);
                                B = 255;
                            }
                        }

                        // set pixel color
                        waterfall_memory[i * pixel_size + 3 + ptr] = 255;
                        waterfall_memory[i * pixel_size + 2 + ptr] = (byte)R;	// set color in memory
                        waterfall_memory[i * pixel_size + 1 + ptr] = (byte)G;
                        waterfall_memory[i * pixel_size + 0 + ptr] = (byte)B;
                    }
                }
            }

            return true;
        }

        private static bool rendering = false;
        public static bool RenderDirectX() 
        {
            try
            {
                bool log_visible = false;

                if (rendering)
                    return true;

                rendering = true;

                if (!MainForm.pause_DisplayThread)
                {
                    if (device == null || waterfall_dx_device == null) return false;

                    RenderMutex.WaitOne();

                    // setup data
                    switch (display_mode)
                    {
                        case DisplayMode.PANASCOPE:
                        case DisplayMode.PANASCOPE_INV:
                            ConvertDataForPanadapter();
                            ConvertDataForScope(waterfall_W, waterfall_H);
                            break;
                        case DisplayMode.PANADAPTER:
                            ConvertDataForPanadapter();
                            break;
                        case DisplayMode.PANAFALL:
                        case DisplayMode.PANAFALL_INV:
                            ConvertDataForPanadapter();
                            ConvertDataForWaterfall();
                            break;
                        case DisplayMode.WATERFALL:
                            ConvertDataForWaterfall();
                            break;
                        default:
                            ConvertDataForPanadapter();
                            break;
                    }

                    device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, display_background_color.ToArgb(), 0.0f, 0);
                    waterfall_dx_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, display_background_color.ToArgb(), 0.0f, 0);

                    if (Panadapter_Sprite != null)
                    {
                        Panadapter_Sprite.Begin(SpriteFlags.AlphaBlend);
                        Panadapter_Sprite.Draw(PanadapterTexture, Panadapter_texture_size, (Color4)Color.White);
                        Panadapter_Sprite.End();
                    }

                    if ((!MainForm.LogVisible && display_mode == DisplayMode.PANASCOPE) ||
                        display_mode == DisplayMode.PANASCOPE_INV)
                    {
                        log_visible = true;
                        Waterfall_Sprite.Begin(SpriteFlags.AlphaBlend);
                        Waterfall_Sprite.Draw(WaterfallTexture, Waterfall_texture_size, (Color4)Color.White);
                        Waterfall_Sprite.End();
                        //Begin the scene
                        waterfall_dx_device.BeginScene();
                        waterfall_dx_device.SetRenderState(RenderState.AlphaBlendEnable, true);
                        waterfall_dx_device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                        waterfall_dx_device.SetRenderState(RenderState.DestinationBlend, Blend.DestinationAlpha);
                        RenderScopeLine(waterfall_dx_device, waterfall_W, false);
                    }

                    //Begin the scene
                    device.BeginScene();
                    device.SetRenderState(RenderState.AlphaBlendEnable, true);
                    device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                    device.SetRenderState(RenderState.DestinationBlend, Blend.DestinationAlpha);

                    switch (display_mode)
                    {
                        case DisplayMode.WATERFALL:
                        case DisplayMode.PANADAPTER:
                        case DisplayMode.PANAFALL:
                        case DisplayMode.PANAFALL_INV:
                        case DisplayMode.PANASCOPE:
                        case DisplayMode.PANASCOPE_INV:
                            if (MainForm.PWR)
                            {
                                if (display_mode == DisplayMode.PANAFALL ||
                                    display_mode == DisplayMode.PANAFALL_INV ||
                                display_mode == DisplayMode.WATERFALL)
                                {
                                    try
                                    {
                                        if (!MOX)
                                        {
                                            if (!log_visible && (!MainForm.LogVisible || (MainForm.LogVisible && display_mode == DisplayMode.PANAFALL_INV)))
                                            {
                                                DataRectangle data;
                                                data = WaterfallTexture.LockRectangle(0, waterfall_rect, LockFlags.None);
                                                waterfall_data_stream = data.Data;
                                                waterfall_data_stream.Position = 0;
                                                waterfall_data_stream.Write(waterfall_memory, 0, (int)waterfall_data_stream.Length);
                                                WaterfallTexture.UnlockRectangle(0);
                                                waterfall_dx_device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, display_background_color.ToArgb(), 0.0f, 0);
                                                Waterfall_Sprite.Begin(SpriteFlags.AlphaBlend);
                                                Waterfall_Sprite.Draw(WaterfallTexture, Waterfall_texture_size, (Color4)Color.White);
                                                Waterfall_Sprite.End();
                                                waterfall_dx_device.BeginScene();
                                                RenderVerticalLine(waterfall_dx_device, 0, 0, Color.Black);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.Write(ex.ToString());
                                    }
                                }
                            }

                            if (refresh_panadapter_grid)
                            {
                                vertical_label = null;
                                horizontal_label = null;
                                RenderPanadapterGrid(panadapter_W, panadapter_H);
                                refresh_panadapter_grid = false;
                            }
                            else
                            {
                                if (show_horizontal_grid)
                                    RenderHorizontalLines(device, HorLines_vb, h_steps);
                                if (show_vertical_grid)
                                    RenderVerticalLines(device, VerLines_vb, 16);
                            }

                            for (int i = 0; i < 12; i++)
                            {
                                panadapter_font.DrawString(null, vertical_label[i].label,
                                    vertical_label[i].pos_x, vertical_label[i].pos_y, vertical_label[i].color.ToArgb());
                            }

                            for (int i = 1; i < h_steps; i++)
                            {
                                panadapter_font.DrawString(null, horizontal_label[i].label,
                                    horizontal_label[i].pos_x, horizontal_label[i].pos_y, horizontal_label[i].color.ToArgb());
                            }

                            if (SDRmode)
                            {
                                Render_VFOA();

                                if (RX2Enabled)
                                    Render_VFOB();
                            }

                            RenderPanadapterLine(device);
                            break;
                    }

                    switch (tuning_mode)
                    {
                        case TuneMode.VFOA:
                            {
                                switch (MainForm.OpModeVFOA)
                                {
                                    case Mode.CW:
                                        {
                                            if (display_mode == DisplayMode.PANAFALL_INV ||
                                                display_mode == DisplayMode.PANASCOPE_INV)
                                            {
                                                if (waterfall_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                                else if (panadapter_target_focused &&
                                                    display_mode != DisplayMode.PANASCOPE_INV)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                            }
                                            else
                                            {
                                                if (panadapter_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                                else if (waterfall_target_focused && log_visible && !MainForm.LogVisible &&
                                                    display_mode != DisplayMode.PANASCOPE)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                            }
                                        }
                                        break;

                                    case Mode.RTTY:
                                        {
                                            if (display_mode == DisplayMode.PANAFALL_INV || display_mode == DisplayMode.PANASCOPE_INV)
                                            {
                                                if (waterfall_target_focused)
                                                {
                                                    RenderVerticalLine(device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, grid_text_color);
                                                    RenderVerticalLine(device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, grid_text_color);
                                                }
                                                else if (panadapter_target_focused && display_mode != DisplayMode.PANASCOPE_INV)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, grid_text_color);
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, grid_text_color);
                                                }
                                            }
                                            else
                                            {
                                                if (panadapter_target_focused)
                                                {
                                                    RenderVerticalLine(device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, grid_text_color);
                                                    RenderVerticalLine(device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, grid_text_color);
                                                }
                                                else if (waterfall_target_focused && display_mode != DisplayMode.PANASCOPE_INV &&
                                                    display_mode != DisplayMode.PANASCOPE)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, grid_text_color);
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, grid_text_color);
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
                                            if (display_mode == DisplayMode.PANAFALL_INV || display_mode == DisplayMode.PANASCOPE_INV)
                                            {
                                                if (waterfall_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                                else if (panadapter_target_focused && display_mode != DisplayMode.PANASCOPE_INV)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                            }
                                            else
                                            {
                                                if (panadapter_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                                else if (waterfall_target_focused && display_mode != DisplayMode.PANASCOPE)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, grid_text_color);
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            break;

                        case TuneMode.VFOB:
                            {
                                switch (MainForm.OpModeVFOB)
                                {
                                    case Mode.CW:
                                        {
                                            if (display_mode == DisplayMode.PANAFALL_INV ||
                                                display_mode == DisplayMode.PANASCOPE_INV)
                                            {
                                                if (waterfall_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                                else if (panadapter_target_focused &&
                                                    display_mode != DisplayMode.PANASCOPE_INV)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                            }
                                            else
                                            {
                                                if (panadapter_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                                else if (waterfall_target_focused && log_visible && !MainForm.LogVisible &&
                                                    display_mode != DisplayMode.PANASCOPE)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                            }
                                        }
                                        break;

                                    case Mode.RTTY:
                                        {
                                            if (display_mode == DisplayMode.PANAFALL_INV || display_mode == DisplayMode.PANASCOPE_INV)
                                            {
                                                if (waterfall_target_focused)
                                                {
                                                    RenderVerticalLine(device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, Color.Red);
                                                    RenderVerticalLine(device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, Color.Red);
                                                }
                                                else if (panadapter_target_focused && display_mode != DisplayMode.PANASCOPE_INV)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, Color.Red);
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, Color.Red);
                                                }
                                            }
                                            else
                                            {
                                                if (panadapter_target_focused)
                                                {
                                                    RenderVerticalLine(device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, Color.Red);
                                                    RenderVerticalLine(device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, Color.Red);
                                                }
                                                else if (waterfall_target_focused && display_mode != DisplayMode.PANASCOPE_INV &&
                                                    display_mode != DisplayMode.PANASCOPE)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x1, display_cursor_y - 20,
                                                        rtty_cursor_x1, display_cursor_y + 20, Color.Red);
                                                    RenderVerticalLine(waterfall_dx_device, rtty_cursor_x2, display_cursor_y - 20,
                                                        rtty_cursor_x2, display_cursor_y + 20, Color.Red);
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
                                            if (display_mode == DisplayMode.PANAFALL_INV || display_mode == DisplayMode.PANASCOPE_INV)
                                            {
                                                if (waterfall_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                                else if (panadapter_target_focused && display_mode != DisplayMode.PANASCOPE_INV)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                            }
                                            else
                                            {
                                                if (panadapter_target_focused)
                                                {
                                                    RenderVerticalLine(device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                                else if (waterfall_target_focused && display_mode != DisplayMode.PANASCOPE)
                                                {
                                                    RenderVerticalLine(waterfall_dx_device, display_cursor_x, panadapter_H, Color.Red);
                                                }
                                            }
                                        }
                                        break;
                                }
                                break;
                            }
                    }

                    //End the scene
                    if (!log_visible && (!MainForm.LogVisible || display_mode == DisplayMode.PANAFALL_INV ||
                        display_mode == DisplayMode.PANASCOPE_INV))
                    {
                        waterfall_dx_device.EndScene();
                        waterfall_dx_device.Present();
                    }
                    else if (log_visible)
                    {
                        waterfall_dx_device.EndScene();
                        waterfall_dx_device.Present();
                    }

                    device.EndScene();
                    device.Present();
                }

                RenderMutex.ReleaseMutex();
                rendering = false;
                return true;
            }
            catch (Exception ex)
            {
                rendering = false;
                Debug.Write("Error in RenderDirectX()\n" + ex.ToString());
                RenderMutex.ReleaseMutex();
                return false;
            }
        }

        unsafe private static void ConvertDataForPanadapter() 
        {
            try
            {
                if (panadapterX_data == null || panadapterX_data.Length != panadapter_W)
                    panadapterX_data = new float[panadapter_W];    			        // array of points to display
                if (waterfallX_data == null || waterfallX_data.Length != panadapter_W)
                    waterfallX_data = new float[panadapter_W];

                float slope = 0.0f;				        	            	// samples to process per pixel
                int num_samples = 0;					                    // number of samples to process
                int start_sample_index = 0;			        	            // index to begin looking at samples
                double Low = rx_display_low;
                double High = rx_display_high;
                int yRange = spectrum_grid_max - spectrum_grid_min;
                max_y = Int32.MinValue;

                //if (Audio.SDRmode)
                    num_samples = BUFFER_SIZE;
                //else
                    //num_samples = Audio.BlockSize;

                if (data_ready)
                {
                    Array.Copy(new_display_data, current_display_data, num_samples);
                    data_ready = false;
                }

                if (average_on)
                {
                    if (!UpdateDisplayPanadapterAverage())
                    {
                        average_buffer = null;
                        average_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
                        average_buffer[0] = CLEAR_FLAG;		// set the clear flag   
                        Debug.Write("Reset display average buffer!");
                    }
                }

                if (Audio.SDRmode)
                {
                    num_samples = (int)(High - Low);
                    start_sample_index = (BUFFER_SIZE >> 1) + (int)((Low * BUFFER_SIZE) / sample_rate);
                    num_samples = (int)((High - Low) * BUFFER_SIZE / sample_rate);

                    if (start_sample_index < 0)
                        start_sample_index += 4096;

                    if ((num_samples - start_sample_index) > (BUFFER_SIZE + 1))
                        num_samples = BUFFER_SIZE - start_sample_index;

                    slope = (float)num_samples / (float)panadapter_W;
                }
                else
                {
                    start_sample_index = (BUFFER_SIZE >> 1) + (int)((0 * BUFFER_SIZE) / sample_rate);
                    num_samples = (int)((High) * BUFFER_SIZE / sample_rate);
                    //num_samples = 2048;
                    slope = (float)num_samples / (float)panadapter_W;
                }

                for (int i = 0; i < panadapter_W; i++)
                {
                    float max = float.MinValue;
                    float dval = i * slope + start_sample_index;
                    int lindex = (int)Math.Floor(dval);
                    int rindex = (int)Math.Floor(dval + slope);

                    if (slope <= 1.0 || lindex == rindex)
                    {
                        max = current_display_data[lindex % 4096] * ((float)lindex - dval + 1) + 
                            current_display_data[(lindex + 1) % 4096] * (dval - (float)lindex);
                    }
                    else
                    {
                        for (int j = lindex; j < rindex; j++)
                            if (current_display_data[j % 4096] > max) max = current_display_data[j % 4096];
                    }

                    //if (Audio.SDRmode)
                        max += display_cal_offset;

                    if (max > max_y)
                    {
                        max_y = max;
                        max_x = i;
                    }

                    panadapterX_data[i] = (int)(Math.Floor((spectrum_grid_max - max) * panadapter_H / yRange));
                }

                panadapterX_data[0] = panadapterX_data[1];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private static bool UpdateDisplayPanadapterAverage()
        {
            try
            {
                if ((average_buffer[0] == CLEAR_FLAG) ||
                    float.IsNaN(average_buffer[0]))
                {
                    //Debug.WriteLine("Clearing average buf"); 
                    for (int i = 0; i < BUFFER_SIZE; i++)
                        average_buffer[i] = current_display_data[i];
                    return true;
                }

                float new_mult = 0.0f;
                float old_mult = 0.0f;
                new_mult = display_avg_mult_new;
                old_mult = display_avg_mult_old;

                for (int i = 0; i < BUFFER_SIZE; i++)
                    average_buffer[i] = current_display_data[i] =
                        (float)(current_display_data[i] * new_mult +
                        average_buffer[i] * old_mult);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public static void UpdateDirectXDisplayWaterfallAverage()
        {
            // Debug.WriteLine("last vfo: " + avg_last_ddsfreq + " vfo: " + DDSFreq); 
            if (average_waterfall_buffer[0] == CLEAR_FLAG ||
                float.IsNaN(average_waterfall_buffer[0]))
            {
                //Debug.WriteLine("Clearing average buf"); 
                for (int i = 0; i < BUFFER_SIZE; i++)
                    average_waterfall_buffer[i] = current_waterfall_data[i];
            }

            float new_mult = 0.0f;
            float old_mult = 0.0f;

            new_mult = waterfall_avg_mult_new;
            old_mult = waterfall_avg_mult_old;

            for (int i = 0; i < BUFFER_SIZE; i++)
                average_waterfall_buffer[i] = current_waterfall_data[i] =
                    (float)(current_waterfall_data[i] * new_mult +
                    average_waterfall_buffer[i] * old_mult);
        }

        #endregion

        #region Scope

        public static void RenderScopeLine(Device dev, int count, bool monitor)
        {
            try
            {
                Mode new_mode = MainForm.OpModeVFOA;

                if (Audio.channel == 6)
                    new_mode = MainForm.OpModeVFOB;

                switch (new_mode)
                {
                    case Mode.RTTY:
                        {
                            ////////////// mark ////////////////////

                            for (int i = 0; i < count * 2; i++)
                            {
                                ScopeLine_verts[i] = new Vertex();
                                ScopeLine_verts[i].Color = data_line_color.ToArgb();
                                ScopeLine_verts[i].Position = new Vector4(i / 2, panadapterX_scope_data_mark[i], 0.0f, 0.0f);
                                ScopeLine_verts[i + 1] = new Vertex();
                                ScopeLine_verts[i + 1].Color = data_line_color.ToArgb();
                                ScopeLine_verts[i + 1].Position = new Vector4(i / 2, panadapterX_scope_data_mark[i + 1], 0.0f, 0.0f);
                                i++;
                            }

                            if (monitor)
                            {
                                ScopeLine_vb_monitor.Lock(0, 0, LockFlags.None).WriteRange(ScopeLine_verts, 0, count * 2);
                                ScopeLine_vb_monitor.Unlock();
                                dev.SetStreamSource(0, ScopeLine_vb_monitor, 0, 20);
                                dev.DrawPrimitives(PrimitiveType.LineList, 0, count * 2);
                            }
                            else
                            {
                                ScopeLine_vb.Lock(0, 0, LockFlags.None).WriteRange(ScopeLine_verts, 0, count * 2);
                                ScopeLine_vb.Unlock();
                                dev.SetStreamSource(0, ScopeLine_vb, 0, 20);
                                dev.DrawPrimitives(PrimitiveType.LineList, 0, count * 2);
                            }

                            ////////////// space ///////////////////

                            for (int i = 0; i < count * 2; i++)
                            {
                                ScopeLine_verts[i] = new Vertex();
                                ScopeLine_verts[i].Color = data_line_color.ToArgb();
                                ScopeLine_verts[i].Position = new Vector4(i / 2, panadapterX_scope_data_space[i], 0.0f, 0.0f);
                                ScopeLine_verts[i + 1] = new Vertex();
                                ScopeLine_verts[i + 1].Color = data_line_color.ToArgb();
                                ScopeLine_verts[i + 1].Position = new Vector4(i / 2, panadapterX_scope_data_space[i + 1], 0.0f, 0.0f);
                                i++;
                            }

                            if (monitor)
                            {
                                ScopeLine_vb_monitor.Lock(0, 0, LockFlags.None).WriteRange(ScopeLine_verts, 0, count * 2);
                                ScopeLine_vb_monitor.Unlock();
                                dev.SetStreamSource(0, ScopeLine_vb_monitor, 0, 20);
                                dev.DrawPrimitives(PrimitiveType.LineList, 0, count * 2);
                            }
                            else
                            {
                                ScopeLine_vb.Lock(0, 0, LockFlags.None).WriteRange(ScopeLine_verts, 0, count * 2);
                                ScopeLine_vb.Unlock();
                                dev.SetStreamSource(0, ScopeLine_vb, 0, 20);
                                dev.DrawPrimitives(PrimitiveType.LineList, 0, count * 2);
                            }
                        }
                        break;

                    default:
                        {
                            for (int i = 0; i < count * 2; i++)
                            {
                                ScopeLine_verts[i] = new Vertex();
                                ScopeLine_verts[i].Color = data_line_color.ToArgb();// Color.Wheat.ToArgb();
                                ScopeLine_verts[i].Position = new Vector4(i / 2, panadapterX_scope_data[i], 0.0f, 0.0f);
                                ScopeLine_verts[i + 1] = new Vertex();
                                ScopeLine_verts[i + 1].Color = data_line_color.ToArgb(); // Color.Wheat.ToArgb();
                                ScopeLine_verts[i + 1].Position = new Vector4(i / 2, panadapterX_scope_data[i + 1], 0.0f, 0.0f);
                                i++;
                            }

                            if (monitor)
                            {
                                ScopeLine_vb_monitor.Lock(0, 0, LockFlags.None).WriteRange(ScopeLine_verts, 0, count * 2);
                                ScopeLine_vb_monitor.Unlock();
                                dev.SetStreamSource(0, ScopeLine_vb_monitor, 0, 20);
                                dev.DrawPrimitives(PrimitiveType.LineList, 0, count * 2);
                            }
                            else
                            {
                                ScopeLine_vb.Lock(0, 0, LockFlags.None).WriteRange(ScopeLine_verts, 0, count * 2);
                                ScopeLine_vb.Unlock();
                                dev.SetStreamSource(0, ScopeLine_vb, 0, 20);
                                dev.DrawPrimitives(PrimitiveType.LineList, 0, count * 2);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        public static void ConvertDataForScope(int scope_W, int scope_H)
        {
            try
            {
                if (!MainForm.booting)
                {
                    int i;
                    Mode new_mode = MainForm.OpModeVFOA;

                    if (Audio.channel == 6)
                        new_mode = MainForm.OpModeVFOB;

                    switch (new_mode)
                    {
                        case Mode.RTTY:
                            {
                                ///////////////// mark /////////////////////////////

                                if (scope_min_mark == null || scope_min_mark.Length != waterfall_target.Width)
                                {
                                    scope_min_mark = new float[waterfall_target.Width];
                                    Audio.ScopeMinMark = scope_min_mark;
                                }
                                if (scope_max_mark == null || scope_max_mark.Length != waterfall_target.Width)
                                {
                                    scope_max_mark = new float[waterfall_target.Width];
                                    Audio.ScopeMaxMark = scope_max_mark;
                                }

                                int h = scope_H / 3;

                                for (i = 0; i < scope_W * 2; i++)
                                {
                                    int pixel = 0;
                                    pixel = (int)(h * Audio.scope_max_mark[i / 2]);
                                    int y = h - pixel;
                                    panadapterX_scope_data_mark[i] = y;

                                    pixel = (int)(h * Audio.scope_min_mark[i / 2]);
                                    y = h - pixel;
                                    panadapterX_scope_data_mark[i + 1] = y;
                                    i++;
                                }

                                ///////////////// space ////////////////////////////

                                if (scope_min_space == null || scope_min_space.Length != waterfall_target.Width)
                                {
                                    scope_min_space = new float[waterfall_target.Width];
                                    Audio.ScopeMinSpace = scope_min_space;
                                }
                                if (scope_max_space == null || scope_max_space.Length != waterfall_target.Width)
                                {
                                    scope_max_space = new float[waterfall_target.Width];
                                    Audio.ScopeMaxSpace = scope_max_space;
                                }

                                for (i = 0; i < scope_W * 2; i++)
                                {
                                    int pixel = 0;
                                    pixel = (int)(h  * Audio.scope_max_space[i / 2]);
                                    int y = h * 2 - pixel;
                                    panadapterX_scope_data_space[i] = y;

                                    pixel = (int)(h * Audio.scope_min_space[i / 2]);
                                    y = h * 2 - pixel;
                                    panadapterX_scope_data_space[i + 1] = y;
                                    i++;
                                }
                            }
                            break;

                        default:
                            {
                                if (scope_min == null || scope_min.Length != waterfall_target.Width)
                                {
                                    scope_min = new float[waterfall_target.Width];
                                    Audio.ScopeMin = scope_min;
                                }
                                if (scope_max == null || scope_max.Length != waterfall_target.Width)
                                {
                                    scope_max = new float[waterfall_target.Width];
                                    Audio.ScopeMax = scope_max;
                                }

                                for (i = 0; i < scope_W * 2; i++)
                                {
                                    int pixel = 0;
                                    pixel = (int)(scope_H / 2 * Audio.scope_max[i / 2]);
                                    int y = scope_H / 2 - pixel;
                                    panadapterX_scope_data[i] = y;

                                    pixel = (int)(scope_H / 2 * Audio.scope_min[i / 2]);
                                    y = scope_H / 2 - pixel;
                                    panadapterX_scope_data[i + 1] = y;
                                    i++;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion
    }
#endregion

#endif

    #region GDI

    class Display_GDI
    {
        #region Variable Declaration

        public static CWExpert MainForm;
        private static Bitmap waterfall_bmp;					// saved waterfall picture for display
        public const float CLEAR_FLAG = -999.999F;				// for resetting buffers
        public const int BUFFER_SIZE = 4096;
        public static float[] new_display_data;					// Buffer used to store the new data from the DSP for the display
        public static float[] new_waterfall_data;    			// Buffer used to store the new data from the DSP for the waterfall
        public static float[] current_display_data;				// Buffer used to store the current data for the display
        public static float[] current_waterfall_data;		    // Buffer used to store the current data for the waterfall
        public static float[] waterfall_display_data;            // Buffer for waterfall
        public static float[] average_buffer;					// Averaged display data buffer for Panadapter
        public static float[] average_waterfall_buffer;  		// Averaged display data buffer for Waterfall
        public static bool SDRmode = false;
        public static TuneMode tuning_mode = TuneMode.Off;
        #endregion

        #region Properties

        private static bool reverse_waterfall = false;
        public static bool ReverseWaterfall
        {
            get { return reverse_waterfall; }
            set { reverse_waterfall = value; }
        }

        private static DisplayMode display_mode = DisplayMode.PANAFALL;
        public static DisplayMode DisplayMode
        {
            get { return display_mode; }
            set { display_mode = value; }
        }

        private static float display_cal_offset;					// display calibration offset per volume setting in dB
        public static float DisplayCalOffset
        {
            get { return display_cal_offset; }
            set { display_cal_offset = value; }
        }

        private static double losc_hz;
        public static double LOSC
        {
            get { return losc_hz; }
            set { losc_hz = value; }
        }

        private static double vfoa_hz;
        public static double VFOA
        {
            get { return vfoa_hz; }
            set { vfoa_hz = value; }
        }

        private static double vfoa_hz_mark;
        public static double VFOA_MARK
        {
            get { return vfoa_hz_mark; }
            set { vfoa_hz_mark = value; }
        }

        private static double vfoa_hz_space;
        public static double VFOA_SPACE
        {
            get { return vfoa_hz_space; }
            set { vfoa_hz_space = value; }
        }

        private static double vfob_hz;
        public static double VFOB
        {
            get { return vfob_hz; }
            set { vfob_hz = value; }
        }

        private static double vfob_hz_mark;
        public static double VFOB_MARK
        {
            get { return vfob_hz_mark; }
            set { vfob_hz_mark = value; }
        }

        private static double vfob_hz_space;
        public static double VFOB_SPACE
        {
            get { return vfob_hz_space; }
            set { vfob_hz_space = value; }
        }

        public static string display_font_name = "System";
        public static float display_font_size = 10;

        private static System.Drawing.Font panadapter_font = new System.Drawing.Font("System", 10);
        public static System.Drawing.Font PanadapterFont
        {
            get { return panadapter_font; }
            set { panadapter_font = value; }
        }

        private static Color pan_fill_color = Color.FromArgb(100, 0, 0, 127);
        public static Color PanFillColor
        {
            get { return pan_fill_color; }
            set { pan_fill_color = value; }
        }

        private static bool show_horizontal_grid = false;
        public static bool Show_Horizontal_Grid
        {
            get { return show_horizontal_grid; }
            set { show_horizontal_grid = value; }
        }

        private static bool show_vertical_grid = false;
        public static bool Show_Vertical_Grid
        {
            get { return show_vertical_grid; }
            set { show_vertical_grid = value; }
        }

        private static Color main_rx_filter_color = Color.FromArgb(255, 0, 128, 128);
        public static Color MainRXFilterColor
        {
            get { return main_rx_filter_color; }
            set
            {
                main_rx_filter_color = value;
            }
        }

        private static Color rx2_filter_color = Color.FromArgb(100, 0, 0, 255);  // blue
        public static Color RX2FilterColor
        {
            get { return rx2_filter_color; }
            set
            {
                rx2_filter_color = value;
            }
        }

        private static bool rx2_enabled = false;
        public static bool RX2Enabled
        {
            get { return rx2_enabled; }
            set
            {
                rx2_enabled = value;
            }
        }

        private static bool split_enabled = false;
        public static bool SplitEnabled
        {
            get { return split_enabled; }
            set
            {
                split_enabled = value;
            }
        }

        private static bool show_freq_offset = false;
        public static bool ShowFreqOffset
        {
            get { return show_freq_offset; }
            set
            {
                show_freq_offset = value;
            }
        }

        private static Color band_edge_color = Color.Red;
        public static Color BandEdgeColor
        {
            get { return band_edge_color; }
            set
            {
                band_edge_color = value;
            }
        }

        private static int cw_pitch = 600;
        public static int CWPitch
        {
            get { return cw_pitch; }
            set { cw_pitch = value; }
        }

        private static int H = 0;	// target height
        private static int W = 0;	// target width
        private static Control target = null;
        public static Control Target
        {
            get { return target; }
            set
            {
                target = value;
                H = target.Height;
                W = target.Width;

                if (waterfall_bmp != null)
                    waterfall_bmp.Dispose();

                waterfall_bmp = new Bitmap(W, H, PixelFormat.Format24bppRgb);	// initialize waterfall display
            }
        }

        private static int rx_display_low = 0;
        public static int RXDisplayLow
        {
            get { return rx_display_low; }
            set { rx_display_low = value; }
        }

        private static int rx_display_high = 2000;
        public static int RXDisplayHigh
        {
            get { return rx_display_high; }
            set { rx_display_high = value; }
        }

        private static int display_cursor_x;						// x-coord of the cursor when over the display
        public static int DisplayCursorX
        {
            get { return display_cursor_x; }
            set { display_cursor_x = value; }
        }

        private static int rtty_cursor_x1;						    // RTTY cursor when over the display
        public static int RTTYCursorX1
        {
            get { return rtty_cursor_x1; }
            set { rtty_cursor_x1 = value; }
        }

        private static int rtty_cursor_x2;						    // RTTY cursor when over the display
        public static int RTTYCursorX2
        {
            get { return rtty_cursor_x2; }
            set { rtty_cursor_x2 = value; }
        }

        private static int display_cursor_y;						// y-coord of the cursor when over the display
        public static int DisplayCursorY
        {
            get { return display_cursor_y; }
            set { display_cursor_y = value; }
        }

        private static int sample_rate = 48000;
        public static int SampleRate
        {
            get { return sample_rate; }
            set { sample_rate = value; }
        }

        private static float max_x;								// x-coord of maxmimum over one display pass
        public static float MaxX
        {
            get { return max_x; }
            set { max_x = value; }
        }

        private static float max_y;								// y-coord of maxmimum over one display pass
        public static float MaxY
        {
            get { return max_y; }
            set { max_y = value; }
        }

        private static bool average_on = false;							// True if the Average button is pressed
        public static bool AverageOn
        {
            get { return average_on; }
            set
            {
                average_on = value;
                if (!average_on) ResetDisplayAverage();
            }
        }

        private static bool data_ready;					// True when there is new display data ready from the DSP
        public static bool DataReady
        {
            get { return data_ready; }
            set { data_ready = value; }
        }

        private static bool waterfall_data_ready;	    // True when there is new display data ready from the DSP
        public static bool WaterfallDataReady
        {
            get { return waterfall_data_ready; }
            set { waterfall_data_ready = value; }
        }

        public static float display_avg_mult_old = 1 - (float)1 / 2;
        public static float display_avg_mult_new = (float)1 / 2;
        private static int display_avg_num_blocks = 2;
        public static int DisplayAvgBlocks
        {
            get { return display_avg_num_blocks; }
            set
            {
                display_avg_num_blocks = value;
                display_avg_mult_old = 1 - (float)1 / display_avg_num_blocks;
                display_avg_mult_new = (float)1 / display_avg_num_blocks;
            }
        }

        public static float waterfall_avg_mult_old = 1 - (float)1 / 18;
        public static float waterfall_avg_mult_new = (float)1 / 18;
        private static int waterfall_avg_num_blocks = 18;
        public static int WaterfallAvgBlocks
        {
            get { return waterfall_avg_num_blocks; }
            set
            {
                waterfall_avg_num_blocks = value;
                waterfall_avg_mult_old = 1 - (float)1 / waterfall_avg_num_blocks;
                waterfall_avg_mult_new = (float)1 / waterfall_avg_num_blocks;
            }
        }

        private static int spectrum_grid_max = 0;
        public static int SpectrumGridMax
        {
            get { return spectrum_grid_max; }
            set
            {
                spectrum_grid_max = value;
            }
        }

        private static int spectrum_grid_min = -150;
        public static int SpectrumGridMin
        {
            get { return spectrum_grid_min; }
            set
            {
                spectrum_grid_min = value;
            }
        }

        private static int spectrum_grid_step = 10;
        public static int SpectrumGridStep
        {
            get { return spectrum_grid_step; }
            set
            {
                spectrum_grid_step = value;
            }
        }

        private static Color grid_text_color = Color.Yellow;
        public static Color GridTextColor
        {
            get { return grid_text_color; }
            set
            {
                grid_text_color = value;
            }
        }

        private static Color grid_zero_color = Color.Red;
        public static Color GridZeroColor
        {
            get { return grid_zero_color; }
            set
            {
                grid_zero_color = value;
            }
        }

        private static Color grid_color = Color.FromArgb(75, Color.Blue);
        public static Color GridColor
        {
            get { return grid_color; }
            set
            {
                grid_color = value;
            }
        }

        private static Pen data_line_pen = new Pen(new SolidBrush(Color.LightGreen), display_line_width);
        private static Color data_line_color = Color.LightGreen;
        public static Color DataLineColor
        {
            get { return data_line_color; }
            set
            {
                data_line_color = value;
                data_line_pen = new Pen(new SolidBrush(data_line_color), display_line_width);
            }
        }

        private static Color display_filter_tx_color = Color.Yellow;
        public static Color DisplayFilterTXColor
        {
            get { return display_filter_tx_color; }
            set
            {
                display_filter_tx_color = value;
            }
        }

        private static bool draw_tx_filter = false;
        public static bool DrawTXFilter
        {
            get { return draw_tx_filter; }
            set
            {
                draw_tx_filter = value;
            }
        }

        private static bool draw_tx_cw_freq = false;
        public static bool DrawTXCWFreq
        {
            get { return draw_tx_cw_freq; }
            set
            {
                draw_tx_cw_freq = value;
            }
        }

        private static Color display_background_color = Color.Black;
        public static Color DisplayBackgroundColor
        {
            get { return display_background_color; }
            set
            {
                display_background_color = value;
            }
        }

        private static Color waterfall_low_color = Color.Black;
        public static Color WaterfallLowColor
        {
            get { return waterfall_low_color; }
            set { waterfall_low_color = value; }
        }

        private static Color waterfall_mid_color = Color.Red;
        public static Color WaterfallMidColor
        {
            get { return waterfall_mid_color; }
            set { waterfall_mid_color = value; }
        }

        private static Color waterfall_high_color = Color.Yellow;
        public static Color WaterfallHighColor
        {
            get { return waterfall_high_color; }
            set { waterfall_high_color = value; }
        }

        private static float waterfall_high_threshold = -80.0F;
        public static float WaterfallHighThreshold
        {
            get { return waterfall_high_threshold; }
            set { waterfall_high_threshold = value; }
        }

        private static float waterfall_low_threshold = -110.0F;
        public static float WaterfallLowThreshold
        {
            get { return waterfall_low_threshold; }
            set { waterfall_low_threshold = value; }
        }

        private static float display_line_width = 1.0F;
        public static float DisplayLineWidth
        {
            get { return display_line_width; }
            set
            {
                display_line_width = value;
                data_line_pen = new Pen(new SolidBrush(data_line_color), display_line_width);
            }
        }

        #region General Routines

        public static bool IsInitialized = false;
        public static bool Init(CWExpert form)
        {
            try
            {
                MainForm = form;
                IsInitialized = false;
                Audio.ScopeDisplayWidth = W;

                if (waterfall_bmp != null)
                    waterfall_bmp.Dispose();

                waterfall_bmp = new Bitmap(W, H, PixelFormat.Format24bppRgb);	// initialize waterfall display
                average_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
                average_buffer[0] = CLEAR_FLAG;		// set the clear flag
                average_waterfall_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
                average_waterfall_buffer[0] = CLEAR_FLAG;		// set the clear flag
                new_display_data = new float[BUFFER_SIZE];
                new_waterfall_data = new float[BUFFER_SIZE];
                current_display_data = new float[BUFFER_SIZE];
                current_waterfall_data = new float[BUFFER_SIZE];
                waterfall_display_data = new float[BUFFER_SIZE];
                scope_min = new float[W];
                scope_max = new float[W];

                for (int i = 0; i < BUFFER_SIZE; i++)
                {
                    new_display_data[i] = -200.0f;
                    new_waterfall_data[i] = -200.0f;
                    current_display_data[i] = -200.0f;
                    current_waterfall_data[i] = -200.0f;
                    waterfall_display_data[i] = -200.0f;
                }

                IsInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                IsInitialized = false;
                return false;
            }
        }

        public static void Close()
        {
            try
            {
                if (waterfall_bmp != null)
                    waterfall_bmp.Dispose();

                average_buffer = null;
                new_display_data = null;
                new_waterfall_data = null;
                current_display_data = null;
                current_waterfall_data = null;
                waterfall_display_data = null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        #endregion

        unsafe public static bool RenderWaterfall(ref PaintEventArgs e)
        {
            try
            {
                if (display_mode == DisplayMode.PANASCOPE)
                {
                    if (!DrawScope(e.Graphics, W, H))
                        return false;
                    else
                        return true;
                }
                else if (display_mode == DisplayMode.PANASCOPE_INV)
                {
                    if (!DrawPanadapter(e.Graphics, W, H))
                        return false;
                    else
                        return true;

                }
                else
                {
                    if (!DrawWaterfall(e.Graphics, W, H))
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        unsafe public static bool RenderPanadapter(ref PaintEventArgs e)
        {
            try
            {
                if (display_mode == DisplayMode.PANASCOPE_INV)
                {
                    if (!DrawScope(e.Graphics, W, H))
                        return false;
                    else
                        return true;
                }
                else
                {
                    if (!DrawPanadapter(e.Graphics, W, H))
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        #endregion

        #region Drawing Routines
        // ======================================================
        // Drawing Routines
        // ======================================================

        public static int center_line_x = 415;

        private static void DrawPanadapterGrid(ref Graphics g, int W, int H)
        {
            // draw background
            if (Target != null && Target.BackgroundImage == null)
                g.FillRectangle(new SolidBrush(display_background_color), 0, 0, W, H);

            int low = rx_display_low;					// initialize variables
            int high = rx_display_high;
            int grid_step = spectrum_grid_step;
            SolidBrush grid_text_brush = new SolidBrush(grid_text_color);
            Pen grid_pen = new Pen(grid_color);
            SolidBrush vfoa_pen = new SolidBrush(main_rx_filter_color);
            SolidBrush vfob_pen = new SolidBrush(rx2_filter_color);
            int y_range = spectrum_grid_max - spectrum_grid_min;

            center_line_x = W / 2;

            // Calculate horizontal step size
            int width = high - low;

            // calculate vertical step size
            int h_steps = 10;
            double h_pixel_step = (double)H / h_steps;
            int top = (int)((double)grid_step * H / y_range);

            // Draw vertical lines
            int vgrid = 0;
            string label = "";

            if (Audio.SDRmode)
            {
                vgrid = 0;
                low = (int)(losc_hz * 1e6 + rx_display_low);
                high = (int)(losc_hz * 1e6 + rx_display_high);
                width = high - low;

                for (int i = 0; i < 10; i++)
                {
                    double x_pos = i * W / 10.0;
                    double x = (x_pos / (double)W) * (double)width;
                    label = Math.Round((double)(low + x) / 1e6, 3).ToString();
                    g.DrawString(label, panadapter_font, grid_text_brush, vgrid, (float)Math.Floor(H * .01));
                    vgrid += W / 10;
                }
            }
            else
            {
                int vert_num = 12;
                label = "0";
                vgrid = 0;
                g.DrawString(label, panadapter_font, grid_text_brush, vgrid, (float)Math.Floor(H * .01));

                for (int i = 1; i < vert_num; i++)
                {
                    vgrid += W / 12;
                    label = (i * 170).ToString();

                    g.DrawString(label, panadapter_font, grid_text_brush, vgrid, (float)Math.Floor(H * .01));
                }
            }

            vgrid = 0;
            // Draw vertical lines
            for (int i = 1; i <= 16; i++)
            {
                vgrid += W / 16;
                g.DrawLine(grid_pen, vgrid, PanadapterFont.Height + 2, vgrid, H);
            }

            // Draw horizontal lines
            for (int i = 1; i < h_steps; i++)
            {
                int xOffset = 0;
                int num = spectrum_grid_max - i * spectrum_grid_step;
                int y = (int)((double)(spectrum_grid_max - num) * H / y_range);
                if (show_horizontal_grid)
                    g.DrawLine(grid_pen, 0, y, W, y);

                // Draw horizontal line labels
                if (i != 1)
                {
                    // Draw horizontal line labels
                    num = spectrum_grid_max - i * spectrum_grid_step;
                    label = num.ToString();
                    if (label.Length == 2) xOffset = 8;
                    else if (label.Length == 1) xOffset = 16;
                    else xOffset = 0;
                    SizeF size = g.MeasureString(label, panadapter_font);

                    int x = 0;
                    x = xOffset + 3;
                    y -= 8;

                    g.DrawString(label, panadapter_font, grid_text_brush, x, y);
                    g.DrawLine(grid_pen, 0, y, W, y);
                }
            }

            if (Audio.SDRmode)
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
                        {
                            low = rx_display_low;
                            high = rx_display_high;
                            int filter_low, filter_high;
                            int filter_left = 0;
                            int filter_right = 0;
                            double losc = losc_hz * 1e6;
                            double vfoa = vfoa_hz * 1e6;
                            double vfob = vfob_hz * 1e6;

                            filter_low = -MainForm.FilterWidthVFOA / 2;
                            filter_high = MainForm.FilterWidthVFOA / 2;
                            filter_left = (int)(((-low + filter_low + vfoa - losc) / (high - low) * W));
                            filter_right = (int)(((-low + filter_high + vfoa - losc) / (high - low) * W));
                            if (filter_left == filter_right)
                                filter_right = filter_left + 1;

                            g.FillRectangle(vfoa_pen, filter_left, PanadapterFont.Height + 2,
                                filter_right - filter_left, H - PanadapterFont.Height + 2);

                            if (RX2Enabled)
                            {
                                filter_low = -MainForm.FilterWidthVFOB / 2;
                                filter_high = MainForm.FilterWidthVFOB / 2;
                                filter_left = (int)(((-low + filter_low + vfob - losc) / (high - low) * W));
                                filter_right = (int)(((-low + filter_high + vfob - losc) / (high - low) * W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;

                                g.FillRectangle(vfob_pen, filter_left, PanadapterFont.Height + 2,
                                    filter_right - filter_left, H - PanadapterFont.Height + 2);
                            }
                        }
                        break;

                    case Mode.RTTY:
                        {
                            low = rx_display_low;
                            high = rx_display_high;
                            int filter_low, filter_high;
                            int filter_left = 0;
                            int filter_right = 0;
                            double losc = losc_hz * 1e6;
                            double vfoa_mark = vfoa_hz_mark * 1e6;
                            double vfoa_space = vfoa_hz_space * 1e6;

                            filter_low = -MainForm.FilterWidthVFOA / 2;
                            filter_high = MainForm.FilterWidthVFOA / 2;
                            filter_left = (int)(((-low + filter_low + vfoa_mark - losc) / (high - low) * W));
                            filter_right = (int)(((-low + filter_high + vfoa_mark - losc) / (high - low) * W));

                            g.FillRectangle(vfoa_pen, filter_left, PanadapterFont.Height + 2,
                                filter_right - filter_left, H - PanadapterFont.Height + 2);

                            filter_low = -MainForm.FilterWidthVFOA / 2;
                            filter_high = MainForm.FilterWidthVFOA / 2;
                            filter_left = (int)(((-low + filter_low + vfoa_space - losc) / (high - low) * W));
                            filter_right = (int)(((-low + filter_high + vfoa_space - losc) / (high - low) * W));

                            if (filter_left == filter_right)
                                filter_right = filter_left + 1;

                            g.FillRectangle(vfoa_pen, filter_left, PanadapterFont.Height + 2,
                                filter_right - filter_left, H - PanadapterFont.Height + 2);

                            if (RX2Enabled)
                            {
                                double vfob_mark = vfob_hz_mark * 1e6;
                                double vfob_space = vfob_hz_space * 1e6;

                                filter_low = -MainForm.FilterWidthVFOB / 2;
                                filter_high = MainForm.FilterWidthVFOB / 2;
                                filter_left = (int)(((-low + filter_low + vfob_mark - losc) / (high - low) * W));
                                filter_right = (int)(((-low + filter_high + vfob_mark - losc) / (high - low) * W));

                                g.FillRectangle(vfob_pen, filter_left, PanadapterFont.Height + 2,
                                    filter_right - filter_left, H - PanadapterFont.Height + 2);

                                filter_low = -MainForm.FilterWidthVFOB / 2;
                                filter_high = MainForm.FilterWidthVFOB / 2;
                                filter_left = (int)(((-low + filter_low + vfob_space - losc) / (high - low) * W));
                                filter_right = (int)(((-low + filter_high + vfob_space - losc) / (high - low) * W));

                                if (filter_left == filter_right)
                                    filter_right = filter_left + 1;

                                g.FillRectangle(vfob_pen, filter_left, PanadapterFont.Height + 2,
                                    filter_right - filter_left, H - PanadapterFont.Height + 2);
                            }
                        }
                        break;
                }
            }
        }

        private static Point[] points;
        unsafe static private bool DrawPanadapter(Graphics g, int W, int H)
        {
            try
            {
                if (points == null)
                    points = new Point[W + 2];			    // array of points to display
                else if (points != null && points.Length != W + 2)
                    points = new Point[W + 2];

                float slope = 0.0F;						// samples to process per pixel
                int num_samples = 0;					// number of samples to process
                int start_sample_index = 0;				// index to begin looking at samples
                int Low = rx_display_low;
                int High = rx_display_high;
                int y_range = spectrum_grid_max - spectrum_grid_min;
                int yRange = spectrum_grid_max - spectrum_grid_min;
                max_y = Int32.MinValue;

                if (Audio.SDRmode)
                    num_samples = BUFFER_SIZE;
                else
                    num_samples = Audio.BlockSize;

                if (data_ready)
                {
                    Array.Copy(new_display_data, current_display_data, num_samples);
                    data_ready = false;
                }

                //                if (average_on)
                {
                    if (!UpdateDisplayPanadapterAverage())
                    {
                        average_buffer = null;
                        average_buffer = new float[BUFFER_SIZE];	// initialize averaging buffer array
                        average_buffer[0] = CLEAR_FLAG;		// set the clear flag   
                        Debug.Write("Reset display average buffer!");
                    }
                }

                DrawPanadapterGrid(ref g, W, H);

                if (Audio.SDRmode)
                {
                    num_samples = (High - Low);
                    start_sample_index = (BUFFER_SIZE >> 1) + (int)((Low * BUFFER_SIZE) / sample_rate);
                    num_samples = (int)((High - Low) * BUFFER_SIZE / sample_rate);

                    if (start_sample_index < 0)
                        start_sample_index += 4096;

                    if ((num_samples - start_sample_index) > (BUFFER_SIZE + 1))
                        num_samples = BUFFER_SIZE - start_sample_index;

                    slope = (float)num_samples / (float)W;
                }
                else
                {
                    num_samples = 1024;
                    slope = (float)num_samples / (float)W;
                }

                for (int i = 0; i < W; i++)
                {
                    float max = float.MinValue;
                    float dval = i * slope + start_sample_index;
                    int lindex = (int)Math.Floor(dval);
                    int rindex = (int)Math.Floor(dval + slope);

                    if (slope <= 1.0 || lindex == rindex)
                    {
                        max = current_display_data[lindex % 4096] * ((float)lindex - dval + 1)
                            + current_display_data[(lindex + 1) % 4096] * (dval - (float)lindex);
                    }
                    else
                    {
                        for (int j = lindex; j < rindex; j++)
                            if (current_display_data[j % 4096] > max) max = current_display_data[j % 4096];
                    }

                    if (Audio.SDRmode)
                        max += display_cal_offset;

                    if (max > max_y)
                    {
                        max_y = max;
                        max_x = i;
                    }

                    points[i].X = i;
                    points[i].Y = (int)(Math.Floor((spectrum_grid_max - max) * H / yRange));
                }

                //if (pan_fill)
                {
                    points[W].X = W; 
                    points[W].Y = H;
                    points[W + 1].X = 0; 
                    points[W + 1].Y = H;
                    data_line_pen.Color = pan_fill_color;
                    g.FillPolygon(data_line_pen.Brush, points);
                    points[W] = points[W - 1];
                    points[W + 1] = points[W - 1];
                    data_line_pen.Color = data_line_color;
                    g.DrawLines(data_line_pen, points);
                }
                /*else
                {
                    points[0].X = 1;
                    points[0].Y = points[1].Y;
                    points[W - 1].Y = points[W - 2].Y;
                    g.DrawLines(data_line_pen, points);
                }*/

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
                        {
                            // draw cursor vertical line
                            if (tuning_mode == TuneMode.VFOA)
                            {
                                Pen p;
                                p = new Pen(grid_text_color);
                                g.DrawLine(p, display_cursor_x, 2 * panadapter_font.Size, display_cursor_x, H);
                            }
                            else if (tuning_mode == TuneMode.VFOB)
                            {
                                Pen p;
                                p = new Pen(Color.Red);
                                g.DrawLine(p, display_cursor_x, 2 * panadapter_font.Size, display_cursor_x, H);
                            }
                        }
                        break;

                    case Mode.RTTY:
                        {
                            if (tuning_mode == TuneMode.VFOA)
                            {
                                Pen p;
                                p = new Pen(grid_text_color);
                                g.DrawLine(p, rtty_cursor_x1, display_cursor_y - 20, rtty_cursor_x1, display_cursor_y + 20);
                                g.DrawLine(p, rtty_cursor_x2, display_cursor_y - 20, rtty_cursor_x2, display_cursor_y + 20);
                            }
                            else if(tuning_mode == TuneMode.VFOB)
                            {
                                Pen p;
                                p = new Pen(Color.Red);
                                g.DrawLine(p, rtty_cursor_x1, display_cursor_y - 20, rtty_cursor_x1, display_cursor_y + 20);
                                g.DrawLine(p, rtty_cursor_x2, display_cursor_y - 20, rtty_cursor_x2, display_cursor_y + 20);
                            }
                        }
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write("Error in DrawPanadapter function! \n" + ex.ToString());
                return false;
            }

        }

        private static float[] waterfall_data;
        unsafe static public bool DrawWaterfall(Graphics g, int W, int H)
        {
            if (waterfall_data == null || waterfall_data.Length < W)
                waterfall_data = new float[W];			                    // array of points to display
            float slope = 0.0F;						                        // samples to process per pixel
            int num_samples = 0;					                        // number of samples to process
            int start_sample_index = 0;				                        // index to begin looking at samples
            int low = 0;
            int high = 0;
            low = rx_display_low;
            high = rx_display_high;
            max_y = Int32.MinValue;
            int R = 0, G = 0, B = 0;	                                	// variables to save Red, Green and Blue component values
            int i = 0;

            if (MainForm.MRIsRunning || MainForm.standalone)
            {
                int yRange = spectrum_grid_max - spectrum_grid_min;

                if (Audio.SDRmode)
                    num_samples = BUFFER_SIZE;
                else
                    num_samples = Audio.BlockSize;

                if (waterfall_data_ready)
                {
                    Array.Copy(new_waterfall_data, current_waterfall_data, num_samples);
                    waterfall_data_ready = false;
                }

                if (!Audio.SDRmode)
                {
                    for (i = 0; i < 2048; i++)
                        current_waterfall_data[i] *= 100;
                }

                if (average_on)
                    UpdateDisplayPanadapterAverage();

                if (Audio.SDRmode)
                {
                    num_samples = (high - low);
                    start_sample_index = (BUFFER_SIZE >> 1) + (int)((low * BUFFER_SIZE) / sample_rate);
                    num_samples = (int)((high - low) * BUFFER_SIZE / sample_rate);

                    if (start_sample_index < 0)
                        start_sample_index += 4096;

                    if ((num_samples - start_sample_index) > (BUFFER_SIZE + 1))
                        num_samples = BUFFER_SIZE - start_sample_index;

                    slope = (float)num_samples / (float)W;
                }
                else
                {
                    num_samples = 2048;
                    slope = (float)num_samples / (float)W;
                }

                for (i = 0; i < W; i++)
                {
                    float max = float.MinValue;
                    float dval = i * slope + start_sample_index;
                    int lindex = (int)Math.Floor(dval);
                    int rindex = (int)Math.Floor(dval + slope);

                    if (slope <= 1.0 || lindex == rindex)
                    {
                        max = current_waterfall_data[lindex % 4096] * ((float)lindex - dval + 1)
                            + current_waterfall_data[(lindex + 1) % 4096] * (dval - (float)lindex);
                    }
                    else
                    {
                        for (int j = lindex; j < rindex; j++)
                            if (current_waterfall_data[j % 4096] > max) max = current_waterfall_data[j % 4096];
                    }

                    if (Audio.SDRmode)
                        max += display_cal_offset;

                    if (max > max_y)
                    {
                        max_y = max;
                        max_x = i;
                    }

                    waterfall_data[i] = max;
                }

                waterfall_data[0] = waterfall_data[1];

                BitmapData bitmapData = waterfall_bmp.LockBits(
                        new Rectangle(0, 0, waterfall_bmp.Width, waterfall_bmp.Height),
                        ImageLockMode.ReadWrite,
                        waterfall_bmp.PixelFormat);

                int pixel_size = 3;
                byte* row = null;

                if (reverse_waterfall)
                {
                    // first scroll image up
                    int total_size = bitmapData.Stride * bitmapData.Height;		// find buffer size
                    CWExpert.memcpy(bitmapData.Scan0.ToPointer(),
                        new IntPtr((int)bitmapData.Scan0 + bitmapData.Stride).ToPointer(),
                        total_size - bitmapData.Stride);

                    row = (byte*)(bitmapData.Scan0.ToInt32() + total_size - bitmapData.Stride);
                }
                else
                {
                    // first scroll image down
                    int total_size = bitmapData.Stride * bitmapData.Height;		// find buffer size
                    CWExpert.memcpy(new IntPtr((int)bitmapData.Scan0 + bitmapData.Stride).ToPointer(),
                        bitmapData.Scan0.ToPointer(),
                        total_size - bitmapData.Stride);

                    row = (byte*)(bitmapData.Scan0.ToInt32());
                }

                // draw new data
                for (i = 0; i < W; i++)	// for each pixel in the new line
                {
                    if (waterfall_data[i] <= waterfall_low_threshold)
                    {
                        R = WaterfallLowColor.R;
                        G = WaterfallLowColor.G;
                        B = WaterfallLowColor.B;
                    }
                    else if (waterfall_data[i] >= WaterfallHighThreshold)
                    {
                        R = 192;
                        G = 124;
                        B = 255;
                    }
                    else // value is between low and high
                    {
                        float range = WaterfallHighThreshold - waterfall_low_threshold;
                        float offset = waterfall_data[i] - waterfall_low_threshold;
                        float overall_percent = offset / range; // value from 0.0 to 1.0 where 1.0 is high and 0.0 is low.

                        if (overall_percent < (float)2 / 9) // background to blue
                        {
                            float local_percent = overall_percent / ((float)2 / 9);
                            R = (int)((1.0 - local_percent) * WaterfallLowColor.R);
                            G = (int)((1.0 - local_percent) * WaterfallLowColor.G);
                            B = (int)(WaterfallLowColor.B + local_percent * (255 - WaterfallLowColor.B));
                        }
                        else if (overall_percent < (float)3 / 9) // blue to blue-green
                        {
                            float local_percent = (overall_percent - (float)2 / 9) / ((float)1 / 9);
                            R = 0;
                            G = (int)(local_percent * 255);
                            B = 255;
                        }
                        else if (overall_percent < (float)4 / 9) // blue-green to green
                        {
                            float local_percent = (overall_percent - (float)3 / 9) / ((float)1 / 9);
                            R = 0;
                            G = 255;
                            B = (int)((1.0 - local_percent) * 255);
                        }
                        else if (overall_percent < (float)5 / 9) // green to red-green
                        {
                            float local_percent = (overall_percent - (float)4 / 9) / ((float)1 / 9);
                            R = (int)(local_percent * 255);
                            G = 255;
                            B = 0;
                        }
                        else if (overall_percent < (float)7 / 9) // red-green to red
                        {
                            float local_percent = (overall_percent - (float)5 / 9) / ((float)2 / 9);
                            R = 255;
                            G = (int)((1.0 - local_percent) * 255);
                            B = 0;
                        }
                        else if (overall_percent < (float)8 / 9) // red to red-blue
                        {
                            float local_percent = (overall_percent - (float)7 / 9) / ((float)1 / 9);
                            R = 255;
                            G = 0;
                            B = (int)(local_percent * 255);
                        }
                        else // red-blue to purple end
                        {
                            float local_percent = (overall_percent - (float)8 / 9) / ((float)1 / 9);
                            R = (int)((0.75 + 0.25 * (1.0 - local_percent)) * 255);
                            G = (int)(local_percent * 255 * 0.5);
                            B = 255;
                        }
                    }

                    // set pixel color
                    row[i * pixel_size + 0] = (byte)B;	// set color in memory
                    row[i * pixel_size + 1] = (byte)G;
                    row[i * pixel_size + 2] = (byte)R;
                }

                waterfall_bmp.UnlockBits(bitmapData);

                g.DrawImageUnscaled(waterfall_bmp, 0, 0);
            }
            return true;
        }

        private static bool UpdateDisplayWaterfallAverage()
        {
            try
            {
                // Debug.WriteLine("last vfo: " + avg_last_ddsfreq + " vfo: " + DDSFreq); 
                if (Display_GDI.average_waterfall_buffer[0] == Display_GDI.CLEAR_FLAG)
                {
                    //Debug.WriteLine("Clearing average buf"); 
                    for (int i = 0; i < Display_GDI.BUFFER_SIZE; i++)
                        Display_GDI.average_waterfall_buffer[i] = Display_GDI.current_waterfall_data[i];
                }

                float new_mult = 0.0f;
                float old_mult = 0.0f;

                new_mult = Display_GDI.waterfall_avg_mult_new;
                old_mult = Display_GDI.waterfall_avg_mult_old;

                for (int i = 0; i < Display_GDI.BUFFER_SIZE; i++)
                    Display_GDI.average_waterfall_buffer[i] = Display_GDI.current_waterfall_data[i] =
                        (float)(Display_GDI.current_waterfall_data[i] * new_mult +
                        Display_GDI.average_waterfall_buffer[i] * old_mult);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        private static bool UpdateDisplayPanadapterAverage()
        {
            try
            {
                // Debug.WriteLine("last vfo: " + avg_last_ddsfreq + " vfo: " + DDSFreq); 
                if (Display_GDI.average_buffer[0] == Display_GDI.CLEAR_FLAG ||
                    float.IsNaN(Display_GDI.average_buffer[0]))
                {
                    //Debug.WriteLine("Clearing average buf"); 
                    for (int i = 0; i < Display_GDI.BUFFER_SIZE; i++)
                        Display_GDI.average_buffer[i] = Display_GDI.current_display_data[i];
                }
                float new_mult = 0.0f;
                float old_mult = 0.0f;

                new_mult = Display_GDI.display_avg_mult_new;
                old_mult = Display_GDI.display_avg_mult_old;

                for (int i = 0; i < Display_GDI.BUFFER_SIZE; i++)
                    Display_GDI.average_buffer[i] = Display_GDI.current_display_data[i] =
                        (float)(Display_GDI.current_display_data[i] * new_mult +
                        Display_GDI.average_buffer[i] * old_mult);


                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }

        public static void ResetDisplayAverage()
        {
            try
            {
                average_buffer[0] = CLEAR_FLAG;	// set reset flag
                average_waterfall_buffer[0] = CLEAR_FLAG;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }

        private static float[] scope_min;
        public static float[] ScopeMin
        {
            get { return scope_min; }
            set { scope_min = value; }
        }
        private static float[] scope_max;
        public static float[] ScopeMax
        {
            get { return scope_max; }
            set { scope_max = value; }
        }

        private static float[] scope_min_mark;
        public static float[] ScopeMinMark
        {
            get { return scope_min_mark; }
            set { scope_min_mark = value; }
        }
        private static float[] scope_max_mark;
        public static float[] ScopeMaxMark
        {
            get { return scope_max_mark; }
            set { scope_max_mark = value; }
        }

        private static float[] scope_min_space;
        public static float[] ScopeMinSpace
        {
            get { return scope_min_space; }
            set { scope_min_space = value; }
        }
        private static float[] scope_max_space;
        public static float[] ScopeMaxSpace
        {
            get { return scope_max_space; }
            set { scope_max_space = value; }
        }

        unsafe private static bool DrawScope(Graphics g, int W, int H)
        {
            try
            {
                if (!MainForm.booting)
                {
                    switch (MainForm.OpModeVFOA)
                    {
                        case Mode.RTTY:
                            {
                                if (scope_min_mark == null || scope_min_mark.Length != W)
                                {
                                    scope_min_mark = new float[W];
                                    Audio.ScopeMinMark = scope_min_mark;
                                }
                                if (scope_max_mark == null || scope_max_mark.Length != W)
                                {
                                    scope_max_mark = new float[W];
                                    Audio.ScopeMaxMark = scope_max_mark;
                                }

                                if (scope_min_space == null || scope_min_space.Length != W)
                                {
                                    scope_min_space = new float[W];
                                    Audio.ScopeMinSpace = scope_min_space;
                                }
                                if (scope_max_space == null || scope_max_space.Length != W)
                                {
                                    scope_max_space = new float[W];
                                    Audio.ScopeMaxSpace = scope_max_space;
                                }

                                Point[] points = new Point[W * 2];
                                int h = H / 3;

                                for (int i = 0; i < W; i++)
                                {
                                    int pixel = 0;
                                    pixel = (int)(h * Audio.scope_max_mark[i]);
                                    int y = h - pixel;
                                    points[i].X = i;
                                    points[i].Y = y;

                                    pixel = (int)(h * Audio.scope_min_mark[i]);
                                    y = h - pixel;
                                    points[W * 2 - 1 - i].X = i;
                                    points[W * 2 - 1 - i].Y = y;
                                }

                                // draw the connected points
                                g.DrawLines(data_line_pen, points);
                                g.FillPolygon(new SolidBrush(data_line_color), points);

                                for (int i = 0; i < W; i++)
                                {
                                    int pixel = 0;
                                    pixel = (int)(h * Audio.scope_max_space[i]);
                                    int y = h * 2 - pixel;
                                    points[i].X = i;
                                    points[i].Y = y;

                                    pixel = (int)(h * Audio.scope_min_space[i]);
                                    y = h * 2 - pixel;
                                    points[W * 2 - 1 - i].X = i;
                                    points[W * 2 - 1 - i].Y = y;
                                }

                                // draw the connected points
                                g.DrawLines(data_line_pen, points);
                                g.FillPolygon(new SolidBrush(data_line_color), points);
                            }

                            break;

                        default:
                            {
                                if (scope_min == null || scope_min.Length != W)
                                {
                                    scope_min = new float[W];
                                    Audio.ScopeMin = scope_min;
                                }
                                if (scope_max == null || scope_max.Length != W)
                                {
                                    scope_max = new float[W];
                                    Audio.ScopeMax = scope_max;
                                }

                                Point[] points = new Point[W * 2];
                                for (int i = 0; i < W; i++)
                                {
                                    int pixel = 0;
                                    pixel = (int)(H / 2 * Audio.scope_max[i]);
                                    int y = H / 2 - pixel;
                                    points[i].X = i;
                                    points[i].Y = y;

                                    pixel = (int)(H / 2 * Audio.scope_min[i]);
                                    y = H / 2 - pixel;
                                    points[W * 2 - 1 - i].X = i;
                                    points[W * 2 - 1 - i].Y = y;
                                }

                                // draw the connected points
                                g.DrawLines(data_line_pen, points);
                                g.FillPolygon(new SolidBrush(data_line_color), points);
                            }
                            break;
                    }

                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                return false;
            }
        }
    }

    #endregion

#endregion
}